Option Explicit On

Imports System.Collections.Generic

Namespace Fachadas
    'Classe Padrão de Manipulação de Dados
    Public Class allClass
        Implements Interfaces.Primitivas.iAllClass

#Region "  Start  "

        Private varAllClass As New mAllClass
        Private AdmDB As NBdbm.Interfaces.iAdmDB
        Private mDirty As Boolean
        Private mVariosCPFCNPJ As Boolean
        Protected mTipoConexao As tipos.tiposConection

        Public Sub New(ByVal TableName As String)
            Me.New(TableName, tipos.tiposConection.Default_)
        End Sub
        Public Sub New(ByVal tableName As String, ByVal tipoConection As tipos.tiposConection)
            Me.tableName = tableName
            Me.mVariosCPFCNPJ = False
            If tipoConection <> tipos.tiposConection.Default_ Then
                self.Settings.tipoConexao = tipoConection.ToString
            End If
            Me.mTipoConexao = tipoConection
            AdmDB = self.AdmDB
            Me.InicializaPadrao()
        End Sub
        Public Sub New(ByVal tableName As String, ByVal cnn As NBdbm.Interfaces.iAdmDB)
            Me.tableName = tableName
            Me.mVariosCPFCNPJ = False
            AdmDB = cnn
            Me.InicializaPadrao()
        End Sub

        Private Sub InicializaPadrao()
            Try

                If NBdbm.self.Settings Is Nothing Then
                    'Implementar gravação de log de erro
                    'Erro: a setting deveria estar estanciada
                    Throw New Exception("A classe setting deveria estar estanciada.")
                End If
                If tableName <> "" Then
                    Dim mSqlBuscaFields As New System.Text.StringBuilder
                    Dim mDT As DataTable
                    mSqlBuscaFields.AppendFormat("SELECT Top 0 * From {0};", Me.tableName)
                    mDT = Me.GetDataTable(mSqlBuscaFields.ToString)
                    Me.varAllClass.var = New Fields(mDT.Rows(0))
                    Me.varAllClass.f = New Fachadas.priFilter
                End If
            Catch ex As Exception
                Throw New NBexception("Problemas na Inicialização do Objeto " & Me.GetType().Name, ex)
            End Try

        End Sub

        Public Overridable Sub Dispose() Implements System.IDisposable.Dispose
            If Not IsNothing(AdmDB) Then AdmDB.Dispose()
            If Not IsNothing(varAllClass) Then varAllClass.dispose()
            varAllClass = Nothing
            AdmDB = Nothing
            mDirty = Nothing
            'GC.SuppressFinalize(Me)
        End Sub

#End Region

#Region "  Code Base  "

        Friend Property var() As Fields
            Get
                Return varAllClass.var
            End Get
            Set(ByVal Value As Fields)
                varAllClass.var = Value
            End Set
        End Property

        Friend Property F() As priFilter
            Get
                Return varAllClass.f
            End Get
            Set(ByVal Value As priFilter)
                varAllClass.f = Value
            End Set
        End Property

        Property tableName() As String Implements Interfaces.Primitivas.iAllClass.tableName
            Get
                Return varAllClass.tbName
            End Get
            Set(ByVal Value As String)
                varAllClass.tbName = Value
            End Set
        End Property

        Friend Property CPFCNPJ() As String
            'retorna o CPF ou CNPJ da tabelas de CPF/CNPJ únicos!
            Get
                Dim mSQL As String
                Dim mDT As DataTable

                mSQL = String.Format("Select CPFCNPJ from CTRL_CPFCNPJ where idCPFCNPJ = {0}", Me.var("idCPFCNPJ").Value)

                Try
                    mDT = Me.GetDataTable(mSQL)
                    If Not IsNothing(mDT) Then
                        Return mDT.Rows(0).Item(0).ToString
                    Else
                        Return ""
                    End If
                Catch ex As Exception
                    'implementar método que insere o registro na tabela CTRL_CPFCNPJ
                    Throw New Exception("Não foi possível efetuar get em CPFCNPJ.", ex)
                End Try
            End Get
            Set(ByVal Value As String)
                Dim pF As Boolean
                'Dim mCnpjCpf_Validado As String
                Try
                    pF = var("pessoaFJ").Value
                    Me.var("CPFCNPJ").Value = NBFuncoes.validaCPFCNPJ(Value, pF)
                    var("idCPFCNPJ").Value = Me.idCPFCNPJ
                Catch ex As Exception
                    Dim mNBex As New NBexception("Problemas na Definição do CPFCNPJ", ex)
                    mNBex.Source = "allClass.CPFCNPJ"
                    Throw mNBex
                End Try
            End Set
        End Property

        Friend Overloads ReadOnly Property idCPFCNPJ() As Integer
            'retorna o id da tabela CTRL_CFPCNPJ 
            Get
                Dim mSQL As String
                Dim mDT As DataTable
                mSQL = String.Format("SELECT idCPFCNPJ from CTRL_CPFCNPJ where CPFCNPJ = '{0}'", Me.var("CPFCNPJ").Value)

                Try
                    mDT = Me.GetDataTable(mSQL)
                    If IsNothing(mDT) Then
                        mSQL = String.Format("INSERT INTO [CTRL_CPFCNPJ] (CPFCNPJ) VALUES ('{0}'); SELECT @@IDENTITY SET NOCOUNT OFF;", Me.var("CPFCNPJ").Value)
                        Return Convert.ToInt32(AdmDB.Command(mSQL).ExecuteScalar)
                    End If
                Catch ex As Exception
                    Throw New Exception("Não foi possível inserir CPFCNPJ na tabela.", ex)
                End Try
                Return Convert.ToInt32(mDT.Rows(0).Item(0))
            End Get
        End Property

        Friend Overloads ReadOnly Property idCPFCNPJ(ByVal pCPFCNPJ As String) As Integer
            Get
                Dim mSQL As String
                Dim mDT As DataTable

                mSQL = String.Format("Select idCPFCNPJ from CTRL_CPFCNPJ where [CPFCNPJ] = '{0}';", pCPFCNPJ)
                Try
                    mDT = Me.GetDataTable(mSQL)
                    If Not IsNothing(mDT) Then
                        If mDT.Rows.Count > 1 Then Me.mVariosCPFCNPJ = True
                        Return Convert.ToInt32(mDT.Rows(0)(0))
                    Else
                        Return 0
                    End If
                Catch ex As Exception
                    Dim NbEx As New NBexception("Não foi possível retornar o ID do CPF.", ex)
                    NbEx.Source = "allClass.RetornaIdCPFCNPJ"
                    Throw NbEx
                End Try
            End Get
        End Property

        Public ReadOnly Property VariosCPFCNPJ() As Boolean
            Get
                Return Me.mVariosCPFCNPJ
            End Get
        End Property

        Public Property Inclusao() As Boolean
            Get
                Return Me.varAllClass.inclusao
            End Get
            Set(ByVal Value As Boolean)
                Me.varAllClass.inclusao = Value
            End Set
        End Property

#End Region

#Region "  Code - Métodos  "
        Public ReadOnly Property stringSQL(Optional ByVal forceReplace As Boolean = False) As String Implements Interfaces.Primitivas.iAllClass.stringSQL
            Get
                mDirty = False
                Return "Select * from [" & tableName & "] " & _
                       Me.F.filterWhere(forceReplace) & _
                       Me.F.filterHaving(forceReplace) & _
                       Me.F.filterGroupBy(forceReplace) & _
                       Me.F.filterOrderBy(forceReplace)

            End Get
        End Property
        Public Sub Clear_filters() Implements Interfaces.Primitivas.iAllClass.Clear_filters
            If Not IsNothing(F) Then F.Dispose()
            F = Nothing
            F = New Fachadas.priFilter
        End Sub
        Public Overridable Sub editar(ByVal noCommit As Boolean) Implements Interfaces.Primitivas.iAllClass.editar
            Dim strSQL As New System.Text.StringBuilder(String.Format("Update [{0}] set  ", Me.tableName))
            Dim mTransaction As System.Data.IDbTransaction = Nothing

            For Each mField As Fields.field In Me.var
                If mField.Dirty = True Then
                    mDirty = True
                    strSQL.AppendFormat("[{0}] = ", mField.Caption)
                    Select Case mField.fdType.FullName
                        Case "System.DateTime"
                            strSQL.AppendFormat("CONVERT(DateTime,'{0}',1), ", CDate(mField.Value).ToString("MM-dd-yy HH:mm:ss"))
                        Case "System.Boolean"
                            Dim mValue As Integer = CInt(mField.Value)
                            If mValue < 0 Then mValue = 1 Else mValue = 0
                            strSQL.AppendFormat("'{0}'", mValue)
                        Case Else
                            strSQL.AppendFormat("'{0}'", mField.Value)
                    End Select
                End If
            Next
            strSQL = strSQL.Remove(strSQL.Length - 2, 2)
            strSQL.AppendLine(F.filterWhere)

            Try
                mDirty = True
                If AdmDB.Connection.State = ConnectionState.Closed Then AdmDB.Connection.Open()
                mTransaction = AdmDB.Transaction(IsolationLevel.ReadUncommitted)
                AdmDB.Command("set arithabort on", mTransaction).ExecuteNonQuery()
                Dim mID As Object
                mID = AdmDB.Command(Me.stringSQL, mTransaction).ExecuteScalar
                AdmDB.Command(strSQL.ToString, mTransaction).ExecuteNonQuery()
                AdmDB.Command("set arithabort off", mTransaction).ExecuteNonQuery()
                If Not mID Is Nothing Then
                    self.AdmDB.FinalizaTransaction(noCommit)
                    Me.Inclusao = False
                Else
                    Me.Inclusao = True
                End If
            Catch ex As Exception
                mTransaction.Rollback()
                AdmDB.Transaction = Nothing
                Dim mEx As New Exception("Problemas na Edição do registro na tabela - " & tableName, ex)
                mEx.Source = "NBdbm.Fachadas.allClass.Editar"
                Throw mEx
            End Try

        End Sub
        Public Overridable Sub excluir(ByVal NoCommit As Boolean) Implements Interfaces.Primitivas.iAllClass.excluir
            Dim TotalExcluido As Integer
            Try
                AdmDB.Command("set arithabort on", AdmDB.Transaction(IsolationLevel.ReadUncommitted)).ExecuteNonQuery()
                TotalExcluido = AdmDB.Command(Replace(Me.stringSQL, "Select *", "Delete", 1), self.AdmDB.Transaction(IsolationLevel.ReadUncommitted)).ExecuteNonQuery()
                AdmDB.Command("set arithabort off", AdmDB.Transaction(IsolationLevel.ReadUncommitted)).ExecuteNonQuery()
                If TotalExcluido = 0 Then
                    Throw New Exception("Não Foi possível encontrar o Registro com esta instrução SQL:" + vbCrLf + Me.stringSQL)
                End If

                self.AdmDB.FinalizaTransaction(NoCommit)

            Catch ex As Exception
                AdmDB.Transaction.Rollback()
                AdmDB.Transaction = Nothing
                Dim mEx As New Exception("Não foi possível excluir o registro da tabela - " + Me.tableName, ex)
                mEx.Source = "NBdbm.Fachadas.allClass.Excluir"

                Throw mEx
            End Try
        End Sub
        Public Overridable Sub inserir(ByVal noCommit As Boolean) Implements Interfaces.Primitivas.iAllClass.inserir

            Dim strSQL As New System.Text.StringBuilder(String.Format("SET NOCOUNT ON Insert into [{0}] ", Me.tableName))
            Dim strSQLFields As New System.Text.StringBuilder
            Dim strSQLValues As New System.Text.StringBuilder

            Try
                For Each mField As Fields.field In Me.var
                    If mField.Dirty = True Then
                        strSQLFields.AppendFormat("[{0}], ", mField.Caption)
                        Select Case mField.fdType.FullName
                            Case "System.DateTime"
                                strSQLValues.AppendFormat("CONVERT(DateTime,'{0}',1), ", CDate(mField.Value).ToString("MM-dd-yy HH:mm:ss"))
                            Case "System.Boolean"
                                Dim mValue As Integer = CInt(mField.Value)
                                If mValue < 0 Then mValue = 1 Else mValue = 0
                                strSQLValues.AppendFormat("'{0}', ", mValue)
                            Case Else
                                strSQLValues.AppendFormat("'{0}', ", mField.Value)
                        End Select
                    End If
                Next
                strSQLFields = strSQLFields.Remove(strSQLFields.Length - 2, 2)
                strSQLValues = strSQLValues.Remove(strSQLValues.Length - 2, 2)
                strSQL.AppendFormat("({0}) ", strSQLFields)
                strSQL.AppendFormat(" VALUES ({0}); ", strSQLValues)
                strSQL.AppendLine("SELECT @@IDENTITY SET NOCOUNT OFF;")

                AdmDB.Command("set arithabort on", AdmDB.Transaction(IsolationLevel.ReadUncommitted)).ExecuteNonQuery()
                Me.var("ID").Value = AdmDB.Command(strSQL.ToString, AdmDB.Transaction(IsolationLevel.ReadUncommitted)).ExecuteScalar()
                AdmDB.Command("set arithabort off", AdmDB.Transaction(IsolationLevel.ReadUncommitted)).ExecuteNonQuery()

                self.AdmDB.FinalizaTransaction(noCommit)

            Catch ex As System.Exception
                AdmDB.Transaction.Rollback()
                AdmDB.Transaction = Nothing
                Dim mEx As Exception = New Exception("Problemas na Inclusão do registro na tabela - " & tableName, ex)
                mEx.Source = "NBdbm.Fachadas.allClass.Inserir"
                Throw mEx
            End Try
        End Sub
        Public Overloads Sub getFields()
            Try
                If String.IsNullOrEmpty(Me.F.filterWhere) Then Throw New NBexception("O metodo getFields é usado para retornar somente um registro, portanto o mesmo deve ser invocado com um filtroWhere que obedeça esse requisito")
                Dim mDT As DataTable
                mDT = Me.GetDataTable(Me.stringSQL)
                If Not IsNothing(mDT) And mDT.Rows.Count > 0 Then
                    If mDT.Rows.Count > 1 Then Throw New NBexception("O filtroWhere especificado não retorna somente um registro, redefina e execute o metodo novamente")
                    Me.var = New Fields(mDT.Rows(0))
                End If
            Catch nbEx As NBexception
                Throw nbEx
            Catch ex As Exception
                Dim nbEx As New NBexception("Não foi possível Buscar o registro especificado")
                nbEx.Source = Me.GetType().FullName & ".getFields"
                Throw nbEx
            End Try
        End Sub
        Public Overloads Sub getFields(ByVal pFilterWhere As String)
            Me.Clear_filters()
            Me.filterWhere = pFilterWhere
            Me.getFields()
        End Sub
        Public Sub SalvarPadrao(ByVal nocommit As Boolean, ByVal filtro As String)
            Me.filterWhere = filtro
            editar(nocommit)
            If Me.Inclusao Then
                inserir(nocommit)
            End If
        End Sub
        Public Function DataSource() As Data.DataView
            Dim mDT As DataTable
            Try
                mDT = Me.GetDataTable(Me.stringSQL(True))
                If IsNothing(mDT) Then
                    Return Nothing
                Else
                    Return mDT.DefaultView
                End If
            Catch ex As Exception
                Throw New NBexception("Não foi possível executar a consulta.", ex)
            End Try
        End Function
        Private Sub AbreConexao(ByRef pConnection As Data.IDbConnection)
            If pConnection.State = ConnectionState.Closed Then
                pConnection.ConnectionString = Me.AdmDB.ConnString
                pConnection.Open()
            End If
        End Sub
        Protected Function GetDataTable(ByVal pSQL As String) As DataTable
            Dim mDS As DataSet
            mDS = Me.GetDataSet(pSQL)
            If IsNothing(mDS) Then
                Return Nothing
            Else
                Return mDS.Tables(0)
            End If
        End Function
        Protected Function GetDataSet(ByVal pSQL As String) As DataSet
            Dim mDS As DataSet = New DataSet()
            Me.AdmDB.dataAdapter(pSQL).Fill(mDS)
            If mDS.Tables.Count = 0 Then mDS = Nothing
            Return mDS
        End Function
#End Region

#Region "  Filtros  "
        Public WriteOnly Property filterGroupBy() As String Implements Interfaces.Primitivas.iAllClass.filterGroupBy
            Set(ByVal stringGroupBy As String)
                mDirty = True
                Me.F.filterGroupBy = stringGroupBy
            End Set
        End Property

        Public WriteOnly Property filterHaving() As String Implements Interfaces.Primitivas.iAllClass.filterHaving
            Set(ByVal stringHaving As String)
                mDirty = True
                Me.F.filterHaving = stringHaving
            End Set
        End Property

        Public WriteOnly Property filterOrderBy() As String Implements Interfaces.Primitivas.iAllClass.filterOrderBy
            Set(ByVal stringOrderBy As String)
                mDirty = True
                Me.F.filterOrderBy = stringOrderBy
            End Set
        End Property

        Public WriteOnly Property filterWhere() As String Implements Interfaces.Primitivas.iAllClass.filterWhere
            Set(ByVal stringWhere As String)
                mDirty = True
                Me.F.filterWhere = stringWhere
            End Set
        End Property

        Public WriteOnly Property filterTop() As Integer Implements Interfaces.Primitivas.iAllClass.filterTop

            Set(ByVal stringTop As Integer)
                mDirty = True
                Me.F.filterTop = stringTop
            End Set
        End Property
#End Region

        Friend Class mAllClass
            Implements IDisposable

            Public tbName As String
            Public f As Fachadas.priFilter
            Public var As Fields
            Public inclusao As Boolean
            Public Sub dispose() Implements System.IDisposable.Dispose
                tbName = Nothing
                If Not IsNothing(f) Then f.Dispose()
                f = Nothing
                ' If Not IsNothing(ds) Then ds.Dispose()
                'ds = Nothing
                If Not IsNothing(var) Then var.dispose()
                var = Nothing
            End Sub

        End Class

    End Class

    'A classe fields guarda um coleção com os campos de um registro.
    Friend Class Fields
        Implements IDisposable, IEnumerable

        'local variable to hold collection
        Private aColFields As Dictionary(Of String, field)

#Region "  Start End  "

        Public Sub New(ByRef pDataRow As DataRow)
            Me.aColFields = New Dictionary(Of String, field)
            Me.createFields(pDataRow)
        End Sub

        Public Sub dispose() Implements IDisposable.Dispose
            If Not aColFields Is Nothing Then
                For Each mField As field In aColFields.Values
                    mField.Dispose()
                Next
                Me.aColFields.Clear()
            End If
            aColFields = Nothing
        End Sub

#End Region

#Region "  Code collection  "

        Public Overloads Sub Add(ByVal field As field)
            Me.aColFields.Add(field.Caption, field)
        End Sub

        Public ReadOnly Property Count() As Integer
            Get
                Return aColFields.Count
            End Get
        End Property

        Default Public ReadOnly Property Item(ByVal CaptionKey As String) As field
            Get
                Return aColFields(Trim(CaptionKey))
            End Get
        End Property

        Private Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            Return aColFields.GetEnumerator
        End Function

        Public Sub Remove(ByVal captionKey As String)
            aColFields.Remove(Trim(captionKey))
        End Sub

        Private Sub createFields(ByRef pDataRow As DataRow)
            Try
                With pDataRow.Table
                    For Each col As DataColumn In .Columns
                        Me.Add(New field(col.ColumnName, pDataRow))
                    Next
                End With
            Catch
                Throw New Exception("Impossível adicionar os campos à coleção.")
            End Try
        End Sub

#End Region

        Public Class field
            Implements IDisposable

            Private aDR As DataRow
            Public Caption As String
            Public fdType As System.Type
            Public Dirty As Boolean

            Public Sub New(ByVal pCaption As String, ByRef pDataRow As DataRow)
                Me.Caption = pCaption
                Me.aDR = pDataRow
                Me.fdType = Me.aDR(pCaption).GetType
            End Sub

            Public Property Value() As Object
                Get
                    Return aDR(Caption)
                End Get
                Set(ByVal value As Object)
                    Me.aDR(Caption) = value
                End Set
            End Property

            Public Sub Dispose() Implements System.IDisposable.Dispose
                Me.aDR = Nothing
                Caption = Nothing
                fdType = Nothing
                Dirty = Nothing
            End Sub
        End Class

    End Class

End Namespace
