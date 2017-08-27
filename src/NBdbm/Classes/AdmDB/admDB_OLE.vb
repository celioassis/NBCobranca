Option Explicit On

Namespace ADM

    Friend Class admDB_OLEDB
        Implements Interfaces.iAdmDB

        Private conn As OleDb.OleDbConnection
        Private comm As OleDb.OleDbCommand
        Private trans As OleDb.OleDbTransaction
        Private aConnString As String
        Private aSelf As self

        Public Sub New(ByRef pSelf As self)
            aSelf = pSelf
        End Sub

        Public Sub New(ByRef pSelf As self, ByVal ConnectionString As String)
            Me.New(pSelf)
            If conn Is Nothing Then
                conn = New OleDb.OleDbConnection
            End If
            conn.ConnectionString = ConnectionString
            Me.aConnString = ConnectionString
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
                Return cnnOLEdb()
            End Get
            Set(ByVal Value As System.Data.IDbConnection)

            End Set
        End Property

        Friend Property ConnectionReader() As System.Data.IDbConnection Implements Interfaces.iAdmDB.ConnectionReader
            Get
                Return cnnOLEdb()
            End Get
            Set(ByVal Value As System.Data.IDbConnection)

            End Set
        End Property

        Friend ReadOnly Property NewConnection() As System.Data.IDbConnection Implements Interfaces.iAdmDB.NewConnection
            Get
                Return cnnOLEdb(True)
            End Get
        End Property
        'Exemplo de String de Conexão
        '= "Provider=SQLOLEDB.1;Persist Security Info=False;User ID=ProSystem_; Password=nitromate;Initial Catalog=NBCOBBBO; Data Source=LocalHost"
        '= "DSN=NBCOBBBO;User ID=ProSystem_; Password=nitromate;SystemDB=\\Stone\X\Bancos de Dados\Edgar\BBO\Logo.bmp"
        '= "Provider=SQLOLEDB.1;Persist Security Info=False;User ID=ProSystem_; Password=nitromate;Initial Catalog=NBCOBBBO; Data Source=LocalHost"
        '= "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Dados.mdb"
        '= "data source=Stone;initial catalog=Neobridge;user id=ProSystem_; password=nitromate;"
        '= "Data Source=(local);Integrated Security=yes"
        '= "Network Library=DBMSSOCN; Data Source=127.0.0.1, 1433;Initial Catalog=EDGAR; User ID=ProSystem_; Password=nitromate"
        '= "Data Source=127.0.0.1;Initial Catalog=Neobridge;User ID=sa; Password=tonetto"
        '= "Data Source=172.17.0.66;User ID=marcos; Password=tonetto"
        '= "Driver={Microsoft Access Driver (*.mdb)}; Dbq=c:\banco.mdb; SystemDB=c:\logo.bmp;"
        '= "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=..\Northwind.mdb"
        Private Function cnnOLEdb(Optional ByVal pNova As Boolean = False) As OleDb.OleDbConnection
            Try
                If conn Is Nothing Or pNova Then
                    conn = New OleDb.OleDbConnection
                End If

                If conn.ConnectionString = "" Then
                    'stringConnection = "Provider=Microsoft.Jet.OLEDB.4.0;data source=X:\Bancos de Dados\Neobridge.mdb"
                    'stringConnection = "Provider=SQLOLEDB.1;database=x:\bancos de dados\neo.mdb;User ID=ProSystem_;Password=nitromate;SystemDB=x:\bancos de dados\logo.bmp"
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
                Return New OleDb.OleDbCommand(stringCommand, pNewConnection)
            End Get
        End Property

        Friend Overloads ReadOnly Property Command(ByVal stringCommand As String) As System.Data.IDbCommand Implements Interfaces.iAdmDB.Command
            Get
                'Verificar se é necessário este if logo abaixo e se ele não ocasiona erro
                If comm Is Nothing Then
                    comm = New OleDb.OleDbCommand(String.Empty, Me.connection)
                End If

                comm.CommandText = stringCommand
                Return comm
            End Get
        End Property

        Public Overloads ReadOnly Property Command(ByVal stringCommand As String, ByVal transaction As System.Data.IDbTransaction) As System.Data.IDbCommand Implements Interfaces.iAdmDB.Command
            Get
                'Verificar se é necessário este if logo abaixo e se ele não ocasiona erro
                If comm Is Nothing Then
                    comm = New OleDb.OleDbCommand(String.Empty, Me.connection, transaction)
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
                Return New OleDb.OleDbDataAdapter(Me.Command(stringCommand).CommandText, Me.connection.ConnectionString)
            End Get
        End Property

        Public ReadOnly Property dataParameter(ByVal stringCommand As String) As System.Data.IDbDataParameter Implements Interfaces.iAdmDB.dataParameter
            Get
                Return comm.Parameters
            End Get
        End Property

        Public ReadOnly Property sqlListaCampos(ByVal tableName As String) As String Implements Interfaces.iAdmDB.sqlListaCampos
            Get
                Return ""
            End Get
        End Property

        Public ReadOnly Property sqlListaTabelas() As String Implements Interfaces.iAdmDB.sqlListaTabelas
            Get
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

