Option Explicit On

Namespace ADM

    Public Class admDB
        Implements Interfaces.iAdmDB


        Private tipoConexao As String
        Private myDB As NBdbm.Interfaces.iAdmDB
        Private myEstruturaTabelas As System.Collections.Generic.Dictionary(Of String, Fachadas.Fields)
        Private aSelf As self


        Public Sub New(ByRef pSelf As self)
            aSelf = pSelf
            Me.tipoConexao = aSelf.Settings.tipoConexao
            myEstruturaTabelas = New System.Collections.Generic.Dictionary(Of String, Fachadas.Fields)()
        End Sub
        Public Sub New(ByRef pSelf As self, ByVal TipoConexao As tipos.tiposConection)
            Me.New(pSelf)
            aSelf.Settings.tipoConexao = TipoConexao.ToString()
            Me.tipoConexao = aSelf.Settings.tipoConexao
        End Sub
        Public Sub Dispose() Implements Interfaces.iAdmDB.Dispose
            If Not myDB Is Nothing Then
                Me.myDB.Dispose()
                Me.myDB = Nothing
            End If
            'MyBase.dispose(True)
        End Sub

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            Call Me.Dispose()
        End Sub

        Private ReadOnly Property mDB() As NBdbm.Interfaces.iAdmDB
            Get
                If myDB Is Nothing Then
                    'tipoConexao = self.Settings.tipoConexao
                    Select Case (tipoConexao)
                        Case "SQLSERVER", "SQLSVR_LUG"
                            myDB = New NBdbm.ADM.admDB_SQLsvr(aSelf)
                        Case Else
                            'implementar gravação de log de erro por não haver um tipo correspondente
                            'makelog("Os parametro validos são: SLQLWL,SASKD, ASAKS"
                            'Throw New ApplicationException("Sem um tipo de conexão especificado não há como prosseguir")
                            'Throw New ArgumentNullException("...") não apresenta a mensagem.
                            Throw New ArgumentOutOfRangeException("Sem um tipo de conexão especificado não há como prosseguir")
                            'Throw New NotSupportedException
                    End Select
                End If
                Return myDB
            End Get
        End Property

        Friend Property connection() As System.Data.IDbConnection Implements Interfaces.iAdmDB.Connection
            Get
                Return mDB.Connection
            End Get
            Set(ByVal Value As System.Data.IDbConnection)
                mDB.Connection = Value
            End Set
        End Property

        Friend Property ConnectionReader() As System.Data.IDbConnection Implements Interfaces.iAdmDB.ConnectionReader
            Get
                Return mDB.ConnectionReader
            End Get
            Set(ByVal Value As System.Data.IDbConnection)
                mDB.ConnectionReader = Value
            End Set
        End Property

        Public ReadOnly Property NewConnection() As System.Data.IDbConnection Implements Interfaces.iAdmDB.NewConnection
            Get
                Return mDB.NewConnection
            End Get
        End Property

        Public Overloads ReadOnly Property Command(ByVal stringCommand As String, ByVal pNewConnection As System.Data.IDbConnection) As System.Data.IDbCommand Implements Interfaces.iAdmDB.Command
            Get
                Return mDB.Command(stringCommand, pNewConnection)
            End Get
        End Property

        Friend Overloads ReadOnly Property Command(ByVal stringCommand As String) As System.Data.IDbCommand Implements Interfaces.iAdmDB.Command
            Get
                Return mDB.Command(stringCommand)
            End Get
        End Property

        Friend Overloads ReadOnly Property Command(ByVal stringCommand As String, ByVal transaction As System.Data.IDbTransaction) As System.Data.IDbCommand Implements Interfaces.iAdmDB.Command
            Get
                Return mDB.Command(stringCommand, transaction)
            End Get
        End Property

        Public ReadOnly Property dataAdapter(ByVal stringCommand As String) As System.Data.IDbDataAdapter Implements Interfaces.iAdmDB.dataAdapter
            Get
                Return mDB.dataAdapter(stringCommand)
            End Get
        End Property

        Public ReadOnly Property dataParameter(ByVal stringCommand As String) As System.Data.IDbDataParameter Implements Interfaces.iAdmDB.dataParameter
            Get
                Return mDB.dataParameter(stringCommand)
            End Get
        End Property

        Public ReadOnly Property sqlListaTabelas() As String Implements Interfaces.iAdmDB.sqlListaTabelas
            Get
                Return mDB.sqlListaTabelas
            End Get
        End Property

        Public ReadOnly Property sqlListaCampos(ByVal tableName As String) As String Implements Interfaces.iAdmDB.sqlListaCampos
            Get
                Return mDB.sqlListaCampos(tableName)
            End Get
        End Property

        Public Property Transaction() As System.Data.IDbTransaction Implements Interfaces.iAdmDB.Transaction
            Get
                Return mDB.Transaction()
            End Get
            Set(ByVal Value As System.Data.IDbTransaction)
                mDB.Transaction = Value
            End Set
        End Property

        Public Sub AbreTransaction() Implements Interfaces.iAdmDB.AbreTransaction
            mDB.AbreTransaction()
        End Sub

        Public Sub FinalizaTransaction(ByVal nocommit As Boolean)
            If nocommit = False Then
                Me.mDB.Transaction.Commit()
                Me.mDB.Transaction.Dispose()
                Me.mDB.Transaction = Nothing
                Me.mDB.Connection.Close()
            End If
        End Sub

        Public Sub CancelarTransaction() Implements Interfaces.iAdmDB.CancelarTransaction
            mDB.CancelarTransaction()
        End Sub

        Public ReadOnly Property EstruturaTabelas() As System.Collections.Generic.Dictionary(Of String, Fachadas.Fields)
            Get
                Return Me.myEstruturaTabelas
            End Get
        End Property

        Friend ReadOnly Property ConnString() As String Implements Interfaces.iAdmDB.ConnString
            Get
                Return Me.myDB.ConnString
            End Get
        End Property
    End Class

    Friend Class stringConnection

#Region "  Variaveis Locais  "
        Private scVar As Fachadas.Fields
        Private aSelf As self
        Public Dirty As Boolean
#End Region

#Region "  Start & End  "

        Public Sub New(pSelf as self)
            aSelf = pSelf
            scVar = New Fachadas.Fields(aSelf)
        End Sub

        Public Sub Dispose()
            If Not IsNothing(scVar) Then Me.scVar.dispose()
            Me.scVar = Nothing
        End Sub

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            Call Me.Dispose()
        End Sub

#End Region

#Region "  Métodos & Subs  "

        Public ReadOnly Property ConnProperty() As Fachadas.Fields
            Get
                If Me.scVar Is Nothing Then scVar = New Fachadas.Fields(aSelf)
                Return Me.scVar
            End Get
        End Property

        Public Sub ZeraConnProperty()
            Me.scVar = Nothing
        End Sub
        Private Sub getScVar()
            Call scVar.Add("ApplicationName", "", System.Type.GetType("System.String"), False)
            Call scVar.Add("PassWord", "", System.Type.GetType("System.String"), False)
            Call scVar.Add("UserID", "", System.Type.GetType("System.String"), False)
        End Sub

#End Region

        Public ReadOnly Property StringConnection() As String
            Get
                Dim stringCnn As New System.Text.StringBuilder
                Dim f As Fachadas.Fields.field

                Try
                    For Each f In Me.scVar
                        If f.Dirty = True Then
                            stringCnn.Append(f.value & ";")
                        End If
                    Next
                    If Right(stringCnn.ToString, 1) = ";" Then
                        stringCnn.Remove(stringCnn.Length - 1, 1)
                    End If
                    stringCnn.Replace("login", aself.Settings.UserId)
                    stringCnn.Replace("senha", aself.Settings.Password)

                Catch ex As Exception
                    Beep()
                    Throw New Exception("Não foi possível recuperar a string de conexão, gerando o seguinte erro:" & ex.GetType.ToString & ".", ex)
                End Try

                Return stringCnn.ToString
            End Get
        End Property
    End Class

End Namespace

