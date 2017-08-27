Option Explicit On

Namespace ADM

    Friend Class admDB_ODBC
        Implements Interfaces.iAdmDB

        Private conn As Odbc.OdbcConnection
        Private comm As Odbc.OdbcCommand
        Private trans As Odbc.OdbcTransaction
        Private aConnString As String
        Private aSelf As self

        Public Sub New(ByRef pSelf As self)
            aSelf = pSelf
            Me.aConnString = aSelf.Settings.stringConnection
        End Sub

        Public Sub Dispose() Implements System.IDisposable.Dispose
            conn = Nothing
            comm = Nothing
        End Sub

        Protected Overrides Sub Finalize()
            MyBase.Finalize()
            Call Me.Dispose()
        End Sub

        Friend Property connection() As System.Data.IDbConnection Implements Interfaces.iAdmDB.Connection
            Get
                connection = cnnODBCdb()
            End Get
            Set(ByVal Value As System.Data.IDbConnection)
                Me.conn = Value
            End Set
        End Property

        Friend Property ConnectionReader() As System.Data.IDbConnection Implements Interfaces.iAdmDB.ConnectionReader
            Get
                connection = cnnODBCdb()
                Return connection
            End Get
            Set(ByVal Value As System.Data.IDbConnection)
                Me.conn = Value
            End Set
        End Property

        Public ReadOnly Property NewConnection() As System.Data.IDbConnection Implements Interfaces.iAdmDB.NewConnection
            Get
                Return cnnODBCdb(True)
            End Get
        End Property

        'Exemplo de String de Conexão
        '="DRIVER={SQL Server}; Server=127.0.0.1;Database=NEOBRIDGE;User ID=sa; Password=tonetto"
        '="Driver={SQL Server}; Server=127.0.0.1;Database=NEOBRIDGE;User ID=sa; Password=tonetto;Trusted_Connection=true"
        'Estas não funcionam!!!!
        'Não usar DSN
        '"DRIVER={SQL Server}; Server=127.0.0.1;DSN=NEOBRIDGE;User ID=sa; Password=tonetto"

        'ConnectionString  - Gets or sets the string used to open a data source. 
        'ConnectionTimeout - Gets or sets the time to wait while trying to establish a connection before terminating the attempt and generating an error. 
        'Container         - (inherited from Component) Gets the IContainer that contains the Component. 
        'Database          - Gets the name of the current database or the database to be used after a connection is opened. 
        'DataSource        - Gets the server name or file name of the data source. 
        'Driver            - Gets the name of the ODBC driver specified for the current connection. 
        'ServerVersion     - Gets a string containing the version of the server to which the client is connected. 
        'Site              - (inherited from Component) Gets or sets the ISite of the Component. 
        'State             - Gets the current state of the connection. 
        Private Function cnnODBCdb(Optional ByVal pNova As Boolean = False) As Odbc.OdbcConnection
            Try
                If conn Is Nothing Or pNova Then
                    conn = New Odbc.OdbcConnection
                End If

                If conn.ConnectionString = "" Then
                    'stringConnection = "Driver={SQL Server};Server=127.0.0.1;Database=NEOBRIDGE;User ID=sa;Password=tonetto;Trusted_Connection=true"
                    'conn.ConnectionString = stringConnection
                    conn.ConnectionString = aSelf.Settings.stringConnection
                End If

                If conn.State = ConnectionState.Closed Then
                    conn.Open()
                End If

                Return conn

            Catch ex As Exception
                'implementar o log de erro
                'ex.toString
                Throw New Exception("Não foi possível estabelecer uma conexão com o banco de dados!")
            End Try

        End Function


        Friend Overloads ReadOnly Property Command(ByVal stringCommand As String, ByVal pNewConnection As System.Data.IDbConnection) As System.Data.IDbCommand Implements Interfaces.iAdmDB.Command
            Get
                Return New Odbc.OdbcCommand(stringCommand, pNewConnection)
            End Get
        End Property
        Public Overloads ReadOnly Property Command(ByVal stringCommand As String, ByVal transaction As System.Data.IDbTransaction) As System.Data.IDbCommand Implements Interfaces.iAdmDB.Command
            Get
                Return Nothing
            End Get
        End Property
        Friend Overloads ReadOnly Property Command(ByVal stringCommand As String) As System.Data.IDbCommand Implements Interfaces.iAdmDB.Command
            'Friend ReadOnly Property Command(ByVal stringCommand As String) As System.Data.IDbCommand Implements Interfaces.iAdmDB.Command
            Get
                'Verificar se é necessário este if logo abaixo e se ele não ocasiona erro
                If comm Is Nothing Then
                    comm = New Odbc.OdbcCommand(String.Empty, Me.connection)
                End If

                comm.CommandText = stringCommand
                Return comm
            End Get
        End Property

        Public ReadOnly Property dataAdapter(ByVal stringCommand As String) As System.Data.IDbDataAdapter Implements Interfaces.iAdmDB.dataAdapter
            Get
                'If comm.Connection.State = ConnectionState.Open Then
                '  Return New OleDb.OleDbDataAdapter(stringCommand, Me.connection)
                'End If
                Return New Odbc.OdbcDataAdapter(Me.Command(stringCommand).CommandText, Me.connection.ConnectionString)
            End Get
        End Property

        Public ReadOnly Property dataParameter(ByVal stringCommand As String) As System.Data.IDbDataParameter Implements Interfaces.iAdmDB.dataParameter
            Get
                Return CType(comm.Parameters, System.Data.IDbDataParameter)
            End Get
        End Property

        Public ReadOnly Property sqlListaCampos(ByVal tableName As String) As String Implements Interfaces.iAdmDB.sqlListaCampos
            Get
                Return ""
            End Get
        End Property

        Public ReadOnly Property sqlListaTabelas() As String Implements Interfaces.iAdmDB.sqlListaTabelas
            Get
                'Throw New NotImplementedException
                Return ""
            End Get
        End Property

        Public Property Transaction(Optional ByVal isolationLevel As System.Data.IsolationLevel = System.Data.IsolationLevel.ReadCommitted, Optional ByVal transactionName As String = "default") As System.Data.IDbTransaction Implements Interfaces.iAdmDB.Transaction
            Get
                If trans Is Nothing Then
                    trans = conn.BeginTransaction(isolationLevel)
                End If
                Return trans
            End Get
            Set(ByVal Value As System.Data.IDbTransaction)
                trans = Value
            End Set
        End Property

        Public Sub CancelarTransaction() Implements Interfaces.iAdmDB.CancelarTransaction
            If Not trans Is Nothing Then
                trans.Rollback()
            End If
        End Sub
        Public ReadOnly Property ConnString() As String Implements Interfaces.iAdmDB.ConnString
            Get
                Return Me.aConnString
            End Get
        End Property
    End Class
End Namespace

