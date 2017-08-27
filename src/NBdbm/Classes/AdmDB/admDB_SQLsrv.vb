Option Explicit On

Namespace ADM

    Friend Class admDB_SQLsvr
        Implements Interfaces.iAdmDB

        Private aConn As SqlClient.SqlConnection
        Private aConnReader As SqlClient.SqlConnection
        Private aComm As SqlClient.SqlCommand
        Private aTrans As SqlClient.SqlTransaction
        Private aConnString As String
        Private aSelf As self

        Public Sub New(ByRef pSelf As self)
            aSelf = pSelf
            Me.aConnString = aSelf.Settings.stringConnection
        End Sub

        Public Sub Dispose() Implements System.IDisposable.Dispose
            Try

                If Not aComm Is Nothing Then
                    aComm.Dispose()
                    aComm = Nothing
                End If
                If Not aTrans Is Nothing Then
                    aTrans.Dispose()
                    aTrans = Nothing
                End If
                If Not aConn Is Nothing Then
                    aConn.Dispose()
                    aConn = Nothing
                End If
            Catch

            End Try

        End Sub

        Friend Property connection() As System.Data.IDbConnection Implements Interfaces.iAdmDB.Connection
            Get
                Call cnnSQLServer()
                Return Me.aConn
            End Get
            Set(ByVal Value As System.Data.IDbConnection)
                Me.aConn = Value
            End Set
        End Property

        Friend Property connectionReader() As System.Data.IDbConnection Implements Interfaces.iAdmDB.ConnectionReader
            Get

                Me.cnnSQLServer()
                Return Me.aConnReader
            End Get
            Set(ByVal Value As System.Data.IDbConnection)
                Me.aConnReader = Value
            End Set
        End Property

        'Exemplo de String de Conexão
        '= "Provider=SQLOLEDB.1;Persist Security Info=False;User ID=ProSystem_; Password=nitromate;Initial Catalog=NBCOBBBO; Data Source=LocalHost
        '= "DSN=NBCOBBBO;User ID=ProSystem_; Password=nitromate;SystemDB=\\Stone\X\Bancos de Dados\Edgar\BBO\Logo.bmp
        '= "Provider=SQLOLEDB.1;Persist Security Info=False;User ID=ProSystem_; Password=nitromate;Initial Catalog=NBCOBBBO; Data Source=LocalHost"
        '= "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Dados.mdb"
        '= "data source=Stone;initial catalog=Neobridge;user id=ProSystem_; password=nitromate;"
        '= "Data Source=(local);Integrated Security=yes"
        '= "Network Library=DBMSSOCN; Data Source=127.0.0.1, 1433; Initial Catalog=EDGAR; User ID=ProSystem_; Password=nitromate"
        '= "Network Library=DBMSSOCN; Data Source=127.0.0.1, 1433; Initial Catalog=NEOBRIDGE; User ID=" & self.Settings.UserId & "; Password=" & self.Settings.Password
        Private Sub cnnSQLServer()
            Try
                If Me.aConn Is Nothing Then
                    Me.aConn = New SqlClient.SqlConnection(Me.aConnString)
                End If

                If Me.aConnReader Is Nothing Then
                    Me.aConnReader = New SqlClient.SqlConnection(Me.aConnString)
                End If

            Catch ex As Exception
                Throw New Exception("Não foi possível inicializar o Objeto de conexão com o  banco de dados", ex)
            End Try

        End Sub

        'Cria uma nova Conexão
        Friend ReadOnly Property NewConnection() As System.Data.IDbConnection Implements Interfaces.iAdmDB.NewConnection
            Get
                Return New System.Data.SqlClient.SqlConnection(Me.ConnString)
            End Get
        End Property

        Public Overloads ReadOnly Property Command(ByVal stringCommand As String, ByVal pNewConnection As System.Data.IDbConnection) As System.Data.IDbCommand Implements Interfaces.iAdmDB.Command
            Get
                Return New SqlClient.SqlCommand(stringCommand, pNewConnection)
            End Get
        End Property

        Friend Overloads ReadOnly Property Command(ByVal stringCommand As String) As System.Data.IDbCommand Implements Interfaces.iAdmDB.Command
            Get
                Try
                    'Verificar se é necessário este if logo abaixo e se ele não ocasiona erro
                    Dim mCommand As SqlClient.SqlCommand
                    mCommand = New SqlClient.SqlCommand(stringCommand, Me.aConn, Me.aTrans)
                    If (Me.aConn.State = ConnectionState.Closed) Then Me.aConn.Open()
                    Return mCommand
                Catch ex As Exception
                    Throw New Exception("Não foi possível Criar o Objeto para Executar comandos SQL", ex)
                End Try

            End Get
        End Property

        Friend Overloads ReadOnly Property Command(ByVal stringCommand As String, ByVal Transaction As System.Data.IDbTransaction) As System.Data.IDbCommand Implements Interfaces.iAdmDB.Command
            Get
                If aComm Is Nothing Then
                    aComm = New SqlClient.SqlCommand(stringCommand, Me.connection, Transaction)
                End If

                If aComm.Connection.State = ConnectionState.Closed Then Me.aConn.Open()
                Return aComm
            End Get
        End Property

        'Public ReadOnly Property dataAdapter(ByVal stringCommand As String, ByVal Transaction As System.Data.IDbTransaction) As System.Data.IDbDataAdapter
        '  Get

        '    Return New SqlClient.SqlDataAdapter(Me.Command(stringCommand, Transaction).CommandText, Me.connection)

        '  End Get
        'End Property
        Public ReadOnly Property dataAdapter(ByVal stringCommand As String) As System.Data.IDbDataAdapter Implements Interfaces.iAdmDB.dataAdapter
            Get
                Try
                    Return New SqlClient.SqlDataAdapter(Me.Command(stringCommand))
                Catch ex As Exception
                    Throw ex
                End Try
            End Get
        End Property

        Public ReadOnly Property dataParameter(ByVal stringCommand As String) As System.Data.IDbDataParameter Implements Interfaces.iAdmDB.dataParameter
            Get
                Return aComm.Parameters
            End Get
        End Property

        Public ReadOnly Property sqlListaTabelas() As String Implements Interfaces.iAdmDB.sqlListaTabelas
            Get
                Return "Select name from sysobjects where [xtype] = 'U' order by name"
            End Get
        End Property

        Public ReadOnly Property sqlListaCampos(ByVal tableName As String) As String Implements Interfaces.iAdmDB.sqlListaCampos
            Get
                'strSQL = "Select [name] from syscolumns where [id] = (Select [id] from sysobjects where [name] = '" & tableName & "')"
                'strSQL = "SELECT syscolumns.colorder, syscolumns.name, systypes.name, syscolumns.length FROM syscolumns INNER JOIN systypes ON syscolumns.xtype = systypes.xtype WHERE (syscolumns.id = (SELECT [id] FROM  sysobjects WHERE [name] = '" & tableName & "')) ORDER BY syscolumns.colorder, syscolumns.name"
                Return "SELECT  syscolumns.colorder, syscolumns.name, systypes.name, syscolumns.length FROM syscolumns INNER JOIN systypes ON syscolumns.xtype = systypes.xtype WHERE (syscolumns.id = (SELECT [id] FROM  sysobjects WHERE [name] = '" & tableName & "')) ORDER BY syscolumns.colorder, syscolumns.name"
            End Get
        End Property

        Friend Property Transaction() As System.Data.IDbTransaction Implements Interfaces.iAdmDB.Transaction
            Get
                If aConn Is Nothing Or aTrans Is Nothing Then
                    aConn = connection
                    If aConn.State = ConnectionState.Closed Then aConn.Open()
                    aTrans = Me.aConn.BeginTransaction()
                End If
                Return aTrans
            End Get
            Set(ByVal Value As System.Data.IDbTransaction)
                aTrans = Value
            End Set
        End Property

        Public Sub CancelarTransaction() Implements Interfaces.iAdmDB.CancelarTransaction
            If Not aTrans Is Nothing Then
                aTrans.Rollback()
                aTrans = Nothing
                aConn.Close()
            ElseIf Not aConn Is Nothing And aConn.State = ConnectionState.Open Then
                aConn.Close()
            End If
        End Sub

        Public ReadOnly Property ConnString() As String Implements Interfaces.iAdmDB.ConnString
            Get
                Return Me.aConnString
            End Get
        End Property

        Public Sub AbreTransaction() Implements Interfaces.iAdmDB.AbreTransaction
            If aTrans Is Nothing Then
                If aConn.State = ConnectionState.Closed Then aConn.Open()
                aTrans = Me.aConn.BeginTransaction()
            ElseIf aTrans.Connection Is Nothing Then
                If aConn.State = ConnectionState.Closed Then aConn.Open()
                aTrans = Me.aConn.BeginTransaction()
            End If
        End Sub
    End Class
End Namespace
