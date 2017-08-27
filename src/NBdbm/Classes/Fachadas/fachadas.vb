Option Explicit On

Namespace Fachadas

    'Classe Padrão de Manipulação de Dados
    Public Class allClass
        Implements Interfaces.Primitivas.iAllClass, IDisposable

#Region "  Start  "

        Private varAllClass As New mAllClass
        Private AdmDB As NBdbm.Interfaces.iAdmDB
        Private mDirty As Boolean
        Private mVariosCPFCNPJ As Boolean
        Protected mTipoConexao As tipos.tiposConection
        Protected aSelf As self

        'Overload de Construtor
        Public Sub New(ByRef pSelf As self, ByVal TableName As String)
            Me.New(pSelf, TableName, tipos.tiposConection.Default_, Nothing, False, False)
        End Sub

        Public Sub New(ByRef pSelf As self, ByVal tableName As String, ByVal cnn As NBdbm.Interfaces.iAdmDB)
            aSelf = pSelf
            Me.tableName = tableName
            AdmDB = cnn
            Me.InicializaPadrao(Nothing, False)
        End Sub

        'Overload de Construtor
        Public Sub New(ByRef pSelf As self, ByVal TableName As String, ByVal pManterConexaoAberta As Boolean)
            Me.New(pSelf, TableName, tipos.tiposConection.Default_, Nothing, pManterConexaoAberta, True)
        End Sub

        'Overload de Construtor
        Friend Sub New(ByRef pSelf As self, ByVal TableName As String, ByRef pFields As Fields)
            Me.New(pSelf, TableName, tipos.tiposConection.Default_, pFields, False, True)
        End Sub

        'Overload de Construtor
        Friend Sub New(ByRef pSelf As self, ByVal TableName As String, ByVal pComInicializacaoPadrao As Boolean, ByRef pFields As Fields)
            Me.New(pSelf, TableName, tipos.tiposConection.Default_, pFields, False, pComInicializacaoPadrao)
        End Sub

        'Overload de Construtor
        Friend Sub New(ByRef pSelf As self, ByVal TableName As String, ByRef pFields As Fields, ByVal pManterConexaoAberta As Boolean)
            Me.New(pSelf, TableName, tipos.tiposConection.Default_, pFields, pManterConexaoAberta, True)
        End Sub

        Friend Sub New(ByRef pSelf As self, ByVal tableName As String, ByVal tipoConection As tipos.tiposConection, ByRef pFields As Fields, ByVal pManterConexaoAberta As Boolean)
            Me.New(pSelf, tableName, tipoConection, pFields, pManterConexaoAberta, True)
        End Sub

        Friend Sub New(ByRef pSelf As self, ByVal tableName As String, ByVal tipoConection As tipos.tiposConection, ByVal pFields As Fields, ByVal pManterConexaoAberta As Boolean, ByVal pComInicializacaoPadrao As Boolean)
            aSelf = pSelf
            Me.tableName = tableName
            If tipoConection <> tipos.tiposConection.Default_ Then
                aSelf.Settings.tipoConexao = tipoConection.ToString
            End If
            Me.mTipoConexao = tipoConection
            AdmDB = aSelf.AdmDB
            If (pComInicializacaoPadrao) Then
                Me.InicializaPadrao(pFields, pManterConexaoAberta)
            Else
                Me.varAllClass.Campos = pFields 'New Fields(aSelf, pFields)
                Me.varAllClass.Filtros = New Fachadas.priFilter(aSelf)
            End If
        End Sub

        Private Sub InicializaPadrao(ByVal pFields As Fields, ByVal pManterConexaoAberta As Boolean)
            Try

                If aSelf.Settings Is Nothing Then
                    Throw New Exception("A classe setting deveria estar estanciada.")
                End If

                If tableName <> "" Then
                    If (pFields Is Nothing And aSelf.AdmDB.EstruturaTabelas.ContainsKey(tableName)) Then
                        pFields = aSelf.AdmDB.EstruturaTabelas(tableName)
                        If (pFields.Count = 0) Then
                            aSelf.AdmDB.EstruturaTabelas.Remove(tableName)
                            pFields.dispose()
                            pFields = Nothing
                        End If
                    End If
                    If pFields Is Nothing Then
                        Dim mSqlBuscaFields As New System.Text.StringBuilder
                        Dim mDataTable As DataTable = Nothing
                        mSqlBuscaFields.AppendFormat("SELECT Top 0 * FROM {0}", tableName)
                        Me.varAllClass.Campos = New Fields(aSelf)
                        Me.varAllClass.Filtros = New Fachadas.priFilter(aSelf)
                        mDataTable = Me.GetDataTable(mSqlBuscaFields.ToString(), pManterConexaoAberta)
                        If Not mDataTable Is Nothing Then
                            Me.varAllClass.Campos.createFields(mDataTable.Columns)
                            aSelf.AdmDB.EstruturaTabelas.Add(tableName, New Fields(aSelf, Me.varAllClass.Campos))
                        End If
                    Else
                        Me.varAllClass.Campos = New Fields(aSelf, pFields)
                        Me.varAllClass.Campos.clearFields()
                        Me.varAllClass.Filtros = New Fachadas.priFilter(aSelf)
                    End If
                End If
            Catch ex As Exception
                Throw New NBexception("Problemas na Inicialização do Objeto " & Me.GetType().Name, ex)
            End Try

        End Sub

        Public Overridable Sub Dispose() Implements System.IDisposable.Dispose
            varAllClass.dispose()
            varAllClass = Nothing
            AdmDB = Nothing
            mDirty = Nothing
            'GC.SuppressFinalize(Me)
        End Sub

        Friend Property toObject() As allClass
            Get
                Return Me
            End Get
            Set(ByVal Value As allClass)
                varAllClass = Value.varAllClass
            End Set
        End Property

#End Region

#Region "  Code Base  "

        Friend Property var() As Fields
            Get
                Return varAllClass.Campos
            End Get
            Set(ByVal Value As Fields)
                varAllClass.Campos = Value
            End Set
        End Property

        Friend Property F() As priFilter
            Get
                If (varAllClass.Filtros Is Nothing) Then
                    varAllClass.Filtros = New Fachadas.priFilter(Self)
                End If
                Return varAllClass.Filtros
            End Get
            Set(ByVal Value As priFilter)
                varAllClass.Filtros = Value
            End Set
        End Property

        Property tableName() As String Implements Interfaces.Primitivas.iAllClass.tableName
            Get
                Return varAllClass.TableName.ToString
            End Get
            Set(ByVal Value As String)
                varAllClass.TableName = Value
            End Set
        End Property

        Friend Property CPFCNPJ() As String
            'retorna o CFP ou CNPJ da tabelas de CPF/CNPJ únicos!
            Get
                Dim strSQL As String
                Dim dsFields As Data.DataSet

                strSQL = "Select CPFCNPJ from CTRL_CPFCNPJ where [idCPFCNPJ] = " & Me.var("idCPFCNPJ").value & ";"

                dsFields = New DataSet("Campos")
                Try
                    AdmDB.dataAdapter(strSQL).Fill(dsFields)
                    ' dsFields.Tables(0).DefaultView
                    If dsFields.Tables(0).DefaultView.Table.Rows.Count > 0 Then
                        Return dsFields.Tables(0).DefaultView.Table.Rows(0).Item(0).ToString
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
                    pF = var("pessoaFJ").value
                    Me.var("CPFCNPJ").value = NBFuncoes.validaCPFCNPJ(Value, pF)
                    var("idCPFCNPJ").value = Me.idCPFCNPJ
                Catch ex As Exception
                    Dim mNBex As New NBexception("Problemas na Definição do CPFCNPJ", ex)
                    mNBex.Source = "allClass.CPFCNPJ"
                    Throw mNBex
                End Try
            End Set
        End Property

        Friend ReadOnly Property idCPFCNPJ() As Integer
            'retorna o id da tabela CTRL_CFPCNPJ 
            Get
                Dim strSQL As String
                Dim dsFields As Data.DataSet

                strSQL = "Select idCPFCNPJ from CTRL_CPFCNPJ where [CPFCNPJ] = '" & Me.var("CPFCNPJ").value & "'"

                dsFields = New DataSet("Campos")
                Try
                    AdmDB.dataAdapter(strSQL).Fill(dsFields)
                    If dsFields.Tables(0).DefaultView.Table.Rows.Count = 0 Then
                        strSQL = "insert into [CTRL_CPFCNPJ] (CPFCNPJ) VALUES ('" & Me.var("CPFCNPJ").value & "')"
                        Call AdmDB.Command(strSQL).ExecuteNonQuery()
                        strSQL = "Select idCPFCNPJ from CTRL_CPFCNPJ where [CPFCNPJ] = '" & Me.var("CPFCNPJ").value & "'"
                        AdmDB.dataAdapter(strSQL).Fill(dsFields)
                    End If
                Catch ex As Exception
                    Throw New Exception("Não foi possível inserir CPFCNPJ na tabela.", ex)
                End Try
                ' dsFields.Tables(0).DefaultView
                Return CInt(dsFields.Tables(0).DefaultView.Table.Rows(0).Item(0).ToString)
            End Get
        End Property

        Public ReadOnly Property VariosCPFCNPJ() As Boolean
            Get
                Return Me.mVariosCPFCNPJ
            End Get
        End Property

        Friend Function RetornaIdCPFCNPJ(ByVal CPFCNPJ As String) As Integer
            Dim strSQL As String
            Dim dsFields As Data.DataSet
            Dim ID As Integer
            strSQL = "Select idCPFCNPJ from CTRL_CPFCNPJ where [CPFCNPJ] = '" & CPFCNPJ & "'"
            dsFields = New DataSet("Campos")
            Try
                AdmDB.dataAdapter(strSQL).Fill(dsFields)
                If dsFields.Tables(0).DefaultView.Table.Rows.Count = 1 Then
                    ID = CInt(dsFields.Tables(0).DefaultView.Table.Rows(0).Item(0).ToString)
                ElseIf dsFields.Tables(0).DefaultView.Table.Rows.Count > 1 Then
                    ID = CInt(dsFields.Tables(0).DefaultView.Table.Rows(0).Item(0).ToString)
                    Me.mVariosCPFCNPJ = True
                Else
                    ID = 0
                End If
            Catch ex As Exception
                Dim NbEx As New NBexception("Não foi possível retornar o ID do CPF.", ex)
                NbEx.Source = "allClass.RetornaIdCPFCNPJ"
                Throw NbEx
            End Try
            Return ID
        End Function

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
                Dim mSqlFormato As String = "Select {0}* FROM {1} {2}"
                Dim mSQLFiltro As String
                mSQLFiltro = Me.F.filterWhere(forceReplace)
                If Me.F.filterDelete = False Then
                    mSQLFiltro = mSQLFiltro & _
                       Me.F.filterHaving(forceReplace) & _
                       Me.F.filterGroupBy(forceReplace) & _
                       Me.F.filterOrderBy(forceReplace)
                Else
                    mSqlFormato = "Delete FROM {0} {1}"
                End If
                Dim mSQLretorno As String

                If Me.F.filterTop < 1 Then
                    If Me.F.filterDelete Then
                        mSQLretorno = String.Format(mSqlFormato, Me.tableName, mSQLFiltro)
                    Else
                        mSQLretorno = String.Format(mSqlFormato, "", Me.tableName, mSQLFiltro)
                    End If
                Else
                    mSQLretorno = String.Format(mSqlFormato, "Top " & Me.F.filterTop.ToString & " ", Me.tableName, mSQLFiltro)
                End If

                Return mSQLretorno
            End Get
        End Property
        Public Sub Clear_filters() Implements Interfaces.Primitivas.iAllClass.Clear_filters
            If Not IsNothing(F) Then F.Dispose()
            F = Nothing
            F = New Fachadas.priFilter(Self)
        End Sub
        Public Sub Clear_vars() Implements Interfaces.Primitivas.iAllClass.Clear_vars
            Me.var.clearFields()
        End Sub
        Public Sub getFields(ByVal pManterConexaoAberta As Boolean) Implements Interfaces.Primitivas.iAllClass.getFields
            Dim mDataTable As DataTable = Me.GetDataTable(Me.stringSQL, pManterConexaoAberta)
            If Not mDataTable Is Nothing And mDataTable.Rows.Count > 0 Then
                var.PreencheFields(mDataTable.Rows(0))
            Else
                var.clearFields()
            End If
        End Sub
        Public Sub getFields(ByVal FiltroWhere As String, ByVal pManterConexaoAberta As Boolean)
            Me.Clear_filters()
            Me.filterWhere = FiltroWhere
            Me.getFields(pManterConexaoAberta)
        End Sub

        Public Overridable Function CriaColecaoFields(ByVal pTipoEntidade As Type, ByVal pManterConexaoAberta As Boolean) As NbCollection
            Return CriaColecaoFields(pTipoEntidade, pManterConexaoAberta, "")
        End Function
        Public Overridable Function CriaColecaoFields(ByVal pTipoEntidade As Type, ByVal pManterConexaoAberta As Boolean, ByVal pSortField As String) As NbCollection
            If F.filterWhere(False) = Nothing Then
                Throw New NBexception("É preciso definir o FilterWhere para que possa ser criado uma coleção")
            End If
            Dim mColection As New NbCollection
            Dim mDataTable As DataTable
            Dim mDataView As DataView
            mDataTable = Me.GetDataTable(Me.stringSQL, pManterConexaoAberta)

            If mDataTable Is Nothing Then
                Me.Clear_vars()
                Return mColection
            End If
            mDataView = mDataTable.DefaultView
            mDataView.Sort = pSortField
            Dim mFields As Fields
            mFields = Nothing

            Try
                For Each mDR As DataRow In mDataView.Table.Rows
                    Dim mCampos As Interfaces.Primitivas.iObjetoTabela
                    Dim mObjCampos As Object
                    Dim mParam As Object

                    mFields = New Fields(aSelf, Me.var)

                    'Preenche os Fields da nova Entidade criada
                    mFields.PreencheFields(mDR)

                    'Cria-se uma instancia do Objeto AllClass para ser passado como parametro para o novo objeto
                    'de entidade que será criado
                    mParam = New Object() {New allClass(aSelf, Me.tableName, False, mFields)}

                    'Instancia-se um novo objeto do Tipo Específico da Entidade
                    mObjCampos = pTipoEntidade.Assembly.CreateInstance(pTipoEntidade.FullName, True, System.Reflection.BindingFlags.CreateInstance, Nothing, mParam, Nothing, Nothing)

                    'Joga a referencia da entidade criada na variável do tipo IObjetoTabela para poder buscar a 
                    'Chave da Coleção Key
                    mCampos = mObjCampos
                    mColection.Add(mCampos.Key, mObjCampos)
                    'Preenche os Fields do Objeto de Entidade Atual para futura manipulação fora da entidade.
                Next
                If Not mFields Is Nothing Then Me.var = mFields

            Catch ex As Exception
                Throw ex
            End Try
            If mColection.Count = 0 Then
                Me.Clear_vars()
            End If
            Return mColection
        End Function
        Public Overridable Sub editar(ByVal noCommit As Boolean) Implements Interfaces.Primitivas.iAllClass.editar
            Dim strSQL As String
            Dim fields As Collection
            Dim field As Fields.field
            Dim mTransaction As System.Data.IDbTransaction = Nothing
            fields = Me.var.Collection

            strSQL = "Update [" & tableName & "] set  "
            For Each field In fields
                If field.Dirty = True Then
                    mDirty = True
                    strSQL = strSQL & "[" & field.Caption & "] = "
                    Select Case field.fdType.FullName
                        Case "System.DateTime"
                            strSQL = strSQL & "CONVERT(DateTime,'" & CDate(field.value).ToString("MM-dd-yy HH:mm:ss") & "',1), "

                        Case "System.Boolean"
                            Dim mValue As Integer = CInt(field.value)
                            If mValue < 0 Then mValue = 1 Else mValue = 0
                            strSQL = strSQL & "'" & mValue.ToString & "', "

                        Case Else
                            strSQL = strSQL & "'" & field.value & "', "
                    End Select
                End If
            Next
            strSQL = Left(strSQL, Len(strSQL) - 2) & " "
            strSQL = strSQL & F.filterWhere

            Try
                mDirty = True
                'AdmDB.Command("set arithabort on", mTransaction).ExecuteNonQuery()
                Me.var(1).value = AdmDB.Command(Me.stringSQL).ExecuteScalar
                AdmDB.Command(strSQL).ExecuteNonQuery()
                'AdmDB.Command("set arithabort off", mTransaction).ExecuteNonQuery()
                If Not Me.var(1).value Is Nothing Then
                    self.AdmDB.FinalizaTransaction(noCommit)
                    Me.Inclusao = False
                Else
                    Me.Inclusao = True
                End If
            Catch ex As Exception
                Self.AdmDB.CancelarTransaction()
                Dim mEx As New Exception("Problemas na Edição do registro na tabela - " & tableName, ex)
                mEx.Source = "NBdbm.Fachadas.allClass.Editar"
                Throw mEx
            End Try
        End Sub
        Public Overridable Sub excluir(ByVal NoCommit As Boolean) Implements Interfaces.Primitivas.iAllClass.excluir
            Dim TotalExcluido As Integer
            Try
                'AdmDB.Command("set arithabort on", mTransaction).ExecuteNonQuery()
                Me.F.filterDelete = True
                TotalExcluido = AdmDB.Command(Me.stringSQL).ExecuteNonQuery()
                'AdmDB.Command("set arithabort off", mTransaction).ExecuteNonQuery()
                If TotalExcluido = 0 Then
                    Throw New Exception("Não Foi possível encontrar o Registro com esta com esta instrução SQL:" + vbCrLf + Me.stringSQL)
                End If

                Self.AdmDB.FinalizaTransaction(NoCommit)
                Me.F.filterDelete = False

            Catch ex As Exception
                Self.AdmDB.CancelarTransaction()
                Dim mEx As New Exception("Não foi possível excluir o registro da tabela - " + Me.tableName, ex)
                mEx.Source = "NBdbm.Fachadas.allClass.Excluir"

                Throw mEx
            End Try
        End Sub
        Public Overridable Sub inserir(ByVal noCommit As Boolean) Implements Interfaces.Primitivas.iAllClass.inserir

            Dim strSQL As String
            Dim strSQLFields As String = ""
            Dim strSQLValues As String = ""
            Dim fields As Collection
            Dim field As Fields.field

            Try
                fields = Me.var.Collection
                strSQL = "SET NOCOUNT ON Insert into [" & tableName & "] "
                For Each field In fields
                    If field.Dirty = True Then
                        strSQLFields = strSQLFields & "[" & field.Caption & "], "
                        Select Case field.fdType.FullName
                            Case "System.DateTime"
                                strSQLValues = strSQLValues & "CONVERT(DateTime,'" & CDate(field.value).ToString("MM-dd-yy HH:mm:ss") & "',1), "

                            Case "System.Boolean"
                                Dim mValue As Integer = CInt(field.value)
                                If mValue < 0 Then mValue = 1 Else mValue = 0
                                strSQLValues = strSQLValues & "'" & mValue.ToString & "', "

                            Case Else
                                strSQLValues = strSQLValues & "'" & field.value.ToString & "', "
                        End Select
                    End If
                Next
                strSQLFields = Left(strSQLFields, Len(strSQLFields) - 2) & " "
                strSQLValues = Left(strSQLValues, Len(strSQLValues) - 2) & " "
                strSQL = strSQL & "(" & strSQLFields & ") "
                strSQL = strSQL & " VALUES (" & strSQLValues & ") "
                strSQL = strSQL & " SELECT @@IDENTITY SET NOCOUNT OFF"

                'AdmDB.Command("set arithabort on", mTransaction).ExecuteNonQuery()
                Me.var(1).value = AdmDB.Command(strSQL).ExecuteScalar
                'AdmDB.Command("set arithabort off", mTransaction).ExecuteNonQuery()

                self.AdmDB.FinalizaTransaction(noCommit)

            Catch ex As System.Exception
                Self.AdmDB.CancelarTransaction()
                Dim mEx As Exception = New Exception("Problemas na Inclusão do registro na tabela - " & tableName, ex)
                mEx.Source = "NBdbm.Fachadas.allClass.Inserir"
                Throw mEx
            End Try
        End Sub
        Public Sub SalvarPadrao(ByVal nocommit As Boolean, ByVal filtro As String)
            Me.filterWhere = filtro
            editar(nocommit)
            If Me.Inclusao Then
                inserir(nocommit)
            End If
        End Sub
        Private Function GetDataTable(ByVal pSql As String, ByVal pMantemConexaoAberta As Boolean) As System.Data.DataTable
            Try
                Dim mDataSet As DataSet = New DataSet()

                If AdmDB.Connection.State = ConnectionState.Closed Then
                    AdmDB.Connection.Open()
                End If

                AdmDB.dataAdapter(pSql).Fill(mDataSet)

                If mDataSet.Tables.Count > 0 Then
                    Return mDataSet.Tables(0)
                Else
                    Return Nothing
                End If
            Catch ex As Exception
                If (AdmDB.Connection.State = ConnectionState.Open) Then
                    AdmDB.Connection.Close()
                End If
                Throw New NBexception("Problemas na Execução do DataReader", ex)
            Finally
                If pMantemConexaoAberta = False Then
                    AdmDB.Connection.Close()
                End If
            End Try

        End Function
        Public Function datasourceTables() As Data.DataView Implements Interfaces.Primitivas.iAllClass.datasourceTables

            Dim strSQL As String
            Dim dsFields As Data.DataSet

            strSQL = AdmDB.sqlListaTabelas

            dsFields = New DataSet("Tables")
            Try
                AdmDB.dataAdapter(strSQL).Fill(dsFields)
            Catch e As Exception
                'implementar salvar log de erro
            End Try
            Return dsFields.Tables(0).DefaultView
        End Function
        Public Function datasourceFields() As Data.DataView Implements Interfaces.Primitivas.iAllClass.datasourceFields

            Dim strSQL As String
            Dim dsFields As Data.DataSet

            strSQL = AdmDB.sqlListaCampos(tableName)

            dsFields = New DataSet("Campos")
            Try
                AdmDB.dataAdapter(strSQL).Fill(dsFields)
            Catch e As Exception
                'implementar salvar log de erro
            End Try
            Return dsFields.Tables(0).DefaultView
        End Function
        Public Function DataSource() As Data.DataView Implements Interfaces.Primitivas.iAllClass.DataSource
            Dim mDS As New DataSet("ds_" & tableName)
            Try
                AdmDB.dataAdapter(Me.stringSQL).Fill(mDS)
            Catch ex As Exception
                Throw New NBexception("Não foi possível executar a consulta.", ex)
            End Try
            If mDS.Tables.Count > 0 Then
                Return mDS.Tables(0).DefaultView
            Else
                Return Nothing
            End If
        End Function
        Public Function xmlColFields(ByVal colFields As Collection) As String
            Dim retorno As String 'New tipos.Retorno
            Dim xDoc As New Xml.XmlDocument
            Dim newElem As Xml.XmlElement
            Dim newAttr As Xml.XmlAttribute
            Dim v As NBdbm.Fachadas.Fields.field
            Try
                newElem = xDoc.CreateElement("dataROW")
                xDoc.AppendChild(newElem)
                For Each v In colFields
                    newElem = xDoc.CreateElement(v.Caption)
                    newAttr = xDoc.CreateAttribute("value")
                    newAttr.Value = v.value.ToString
                    newElem.Attributes.Append(newAttr)
                    xDoc.DocumentElement.AppendChild(newElem)
                Next
                retorno = xDoc.InnerXml
            Catch ex As Exception
                retorno = "</>"
            End Try
            Return retorno
        End Function
        Private Sub AbreConexao(ByRef pConnection As Data.IDbConnection)
            If pConnection.State = ConnectionState.Closed Then
                pConnection.ConnectionString = Me.AdmDB.ConnString
                pConnection.Open()
            End If
        End Sub
        Public ReadOnly Property Self() As self
            Get
                Return aSelf
            End Get
        End Property
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

            Public TableName As String
            Public Filtros As Fachadas.priFilter
            Public Campos As Fields
            Public inclusao As Boolean
            Public Sub dispose() Implements System.IDisposable.Dispose
                TableName = Nothing
                If Not IsNothing(Filtros) Then Filtros.Dispose()
                Filtros = Nothing
                If Not IsNothing(Campos) Then Campos.dispose()
                Campos = Nothing
            End Sub

            Public Sub New()

            End Sub
        End Class

    End Class

    'Classe de Filtros para instruções SQL
    Friend Class priFilter
        Implements IDisposable

        Private mWhere As String
        Private mHaving As String
        Private mOrderBy As String
        Private mGroupBy As String
        Private mTop As Integer
        Private mDelete As Boolean
        Private tipoConexao As String
        Private aSelf As self

        Public Sub New(ByRef pSelf As self)
            aSelf = pSelf
            tipoConexao = aSelf.Settings.tipoConexao '= "SQLSERVER"
            Call Me.Clear()
        End Sub

        Private Function replaceFilter(ByVal str As String, ByVal forceReplace As Boolean) As String
            If forceReplace = True Then
                str = Replace(str, "*", "%")
                'Else
                '    str = Replace(str, "%", "*")
            End If
            'If tipoConexao = "SQLSERVER" Then
            'End If
            If str Is Nothing Then str = ""
            Return str
        End Function

        Public Sub Clear()
            mTop = 0
            mDelete = False
            mWhere = String.Empty
            mHaving = String.Empty
            mOrderBy = String.Empty
            mGroupBy = String.Empty
        End Sub

        Public Property filterWhere(Optional ByVal forceReplace As Boolean = False) As String
            Get
                Return replaceFilter(Me.mWhere, forceReplace).ToString
            End Get
            Set(ByVal stringWhere As String)
                If Trim(stringWhere) <> "" Then
                    Me.mWhere = " where " & stringWhere
                Else
                    Me.mWhere = ""
                End If
            End Set
        End Property

        Public Property filterHaving(Optional ByVal forceReplace As Boolean = False) As String
            Get
                Return replaceFilter(Me.mHaving, forceReplace).ToString
            End Get
            Set(ByVal stringHaving As String)
                If Trim(stringHaving) <> "" Then
                    Me.mHaving = " Having " & stringHaving
                Else
                    Me.mHaving = ""
                End If
            End Set
        End Property

        Public Property filterOrderBy(Optional ByVal forceReplace As Boolean = False) As String
            Get
                Return Me.mOrderBy.ToString
            End Get
            Set(ByVal stringOrderBy As String)
                If Trim(stringOrderBy) <> "" Then
                    Me.mOrderBy = " Order by " & stringOrderBy
                Else
                    Me.mOrderBy = ""
                End If
            End Set
        End Property

        Public Property filterGroupBy(Optional ByVal forceReplace As Boolean = False) As String
            Get
                Return Me.mGroupBy.ToString
            End Get
            Set(ByVal stringGroupBy As String)
                If Trim(stringGroupBy) <> "" Then
                    Me.mGroupBy = " Group By " & stringGroupBy
                Else
                    Me.mGroupBy = ""
                End If
            End Set
        End Property

        Public Property filterTop() As Integer
            Get
                Return Me.mTop
            End Get
            Set(ByVal value As Integer)
                Me.mTop = value
            End Set
        End Property

        ''' <summary>
        ''' Define se Filtro é para exclusão ou não.
        ''' </summary>
        ''' <value></value>
        ''' <returns></returns>
        ''' <remarks></remarks>
        Public Property filterDelete() As Boolean
            Get
                Return Me.mDelete
            End Get
            Set(ByVal value As Boolean)
                Me.mDelete = value
            End Set
        End Property

        Public Sub Dispose() Implements System.IDisposable.Dispose
            mWhere = Nothing
            mHaving = Nothing
            mOrderBy = Nothing
            mGroupBy = Nothing
            Me.mTop = Nothing
        End Sub
    End Class

    'A classe fields guarda uma coleção com os campos e valores de um registro.
    Public Class Fields
        Implements IDisposable, IEnumerable, ICloneable

        'local variable to hold collection
        Private CollIndex As Collection
        Private aSelf As self

#Region "  Start End  "

        Public Sub New(ByRef pSelf As self)
            aSelf = pSelf
            CollIndex = New Collection
        End Sub

        Public Sub New(ByRef pSelf As self, ByVal field As field)
            Me.New(pSelf)
            Me.Add(field)
        End Sub

        Public Sub New(ByRef pSelf As self, ByVal pFields As Fields)
            Me.New(pSelf)

            For Each mField As Fields.field In pFields
                Me.Add(mField.Caption, mField.value, mField.fdType)
            Next

        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            'verificar este código
            Clear()
            CollIndex = Nothing
            aSelf = Nothing
        End Sub

        Private Sub Clear()
            If (Not CollIndex Is Nothing) Then
                For Each F As field In CollIndex
                    F.Dispose()
                Next
                CollIndex.Clear()
            End If
        End Sub
#End Region

#Region "  Code collection  "

        Public Sub Add(ByVal caption As String, ByVal value As Object, ByVal fdType As System.Type, Optional ByVal Dirty As Boolean = False)
            Dim field As New field(aSelf)
            Dim index As Integer

            index = CollIndex.Count + 1
            field.Caption = caption
            field.value = value
            field.fdType = fdType
            field.Dirty = Dirty
            Add(field)
        End Sub

        Public Sub Add(ByVal field As field)
            Dim mKey As String
            mKey = Trim(field.Caption)
            If (Not CollIndex.Contains(mKey)) Then
                CollIndex.Add(field, mKey)
            End If
        End Sub

        Public ReadOnly Property Count() As Integer
            Get
                If (CollIndex Is Nothing) Then Return 0
                Return CollIndex.Count
            End Get
        End Property

        Public Property Collection() As Collection
            Get
                Return CollIndex
            End Get
            Set(ByVal Value As Collection)
                If Not CollIndex Is Nothing Then
                    Clear()
                    Do While CollIndex.Count > 0
                        CollIndex.Remove(1)
                    Loop
                End If
                CollIndex = Value
            End Set
        End Property

        Default Public ReadOnly Property Item(ByVal CaptionKey As String) As field
            Get
                Return CType(CollIndex(Trim(CaptionKey)), field)
            End Get
        End Property

        Default Public ReadOnly Property Item(ByVal index As Integer) As field
            Get
                Return CollIndex(index)
            End Get
        End Property

        Private Function GetEnumerator() As System.Collections.IEnumerator Implements System.Collections.IEnumerable.GetEnumerator
            'Me.CollIndex = Me.GetAllItems()
            'Return New Iterator(Me.CollIndex)
            Return CollIndex.GetEnumerator
        End Function

        Public Sub Remove(ByVal captionKey As String)
            CollIndex.Remove(captionKey)
        End Sub

        Public Sub createFields(ByRef pDataColums As Data.DataColumnCollection)
            Try
                For Each mDC As DataColumn In pDataColums
                    Me.Add(mDC.ColumnName, String.Empty, mDC.DataType)
                Next
            Catch ex As Exception
                Throw New NBexception("Impossível adicionar os campos à coleção.", ex)
            End Try
        End Sub

        Public Sub clearFields()
            For Each f As field In CollIndex
                f.value = String.Empty
                f.Dirty = False
            Next
        End Sub

        Public Sub PreencheFields(ByRef pDataRow As DataRow)
            Try
                If CollIndex Is Nothing And CollIndex.Count = 0 Then
                    Throw New NBexception("A coleção dos campos na classe Fields está 'nothing' ou vazia.")
                End If
                For i As Integer = 0 To pDataRow.Table.Columns.Count - 1
                    If pDataRow(i).ToString.Trim = "" Then
                        Me.Item(i + 1).value = String.Empty
                    Else
                        Me.Item(i + 1).value = pDataRow(i)
                    End If
                Next
            Catch nbEx As NBexception
                Throw nbEx
            Catch ex As Exception
                'implementar a gravação de LOG
                Throw New NBexception("Não foi possível preencher coleção de campos.", ex)
            End Try
        End Sub

#End Region

        Public Class field
            Implements IDisposable, ICloneable

            Private mValue As Object
            Public Caption As String
            Public fdType As System.Type
            Public Dirty As Boolean
            Private aSelf As self

            Public Sub New(ByRef pSelf As self)
                aSelf = pSelf
            End Sub
            Friend Sub New(ByRef pSelf As self, ByVal pCaption As String, ByVal pfdType As Type, ByVal pValue As Object)
                aSelf = pSelf
                Me.Caption = pCaption
                Me.fdType = pfdType
                Me.mValue = pValue
                Me.Dirty = False
            End Sub

            Property value() As Object
                Get
                    If Not mValue Is Nothing Then
                        If fdType.FullName = "System.Boolean" Then Return mValue
                        Dim n As String
                        If IsNumeric(mValue) = True Then
                            n = mValue.ToString
                            If aSelf.Settings.sintaxePontoDecimal = "." Then
                                n = n.Replace(",", ".")
                            End If
                            Return n
                        Else
                            If (System.Type.GetTypeCode(fdType) <> System.TypeCode.String) And (mValue.ToString() = "") Then
                                If (System.Type.GetTypeCode(fdType) = System.TypeCode.DateTime) And (mValue.ToString() = "") Then
                                    Return Nothing
                                End If
                                Return 0
                            Else
                                Return mValue
                            End If
                        End If
                    Else
                        Return mValue
                    End If
                End Get
                Set(ByVal Value As Object)
                    mValue = Value
                End Set
            End Property

            Public Sub Dispose() Implements System.IDisposable.Dispose
                mValue = Nothing
                Caption = Nothing
                fdType = Nothing
                Dirty = Nothing
            End Sub

            Public Function Clone() As Object Implements System.ICloneable.Clone
                Dim mField As New field(aSelf, Me.Caption, Me.fdType, Me.mValue)
                Return mField
            End Function
        End Class

        Public Function Clone() As Object Implements System.ICloneable.Clone
            Return Me.MemberwiseClone
        End Function
    End Class

    Public Class NbCollection
        Inherits System.Collections.SortedList
        Implements IDisposable
        Public Overrides Sub Clear()
            For Each Item As IDisposable In Me.Values
                Item.Dispose()
            Next
            MyBase.Clear()
        End Sub
        Public Sub Dispose() Implements System.IDisposable.Dispose
            Me.Clear()
        End Sub
    End Class

End Namespace
