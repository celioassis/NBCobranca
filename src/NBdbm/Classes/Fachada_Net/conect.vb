Option Explicit On 

Imports System
Imports System.Data

Namespace DotNetDataProviderTemplate

    Public Class TemplateConnection
        Implements IDbConnection

        Private m_state As ConnectionState
        Private m_sConnString As String

        ' Use the "SampleDb" class to simulate a database connection.
        Private m_sampleDb As SampleDb

        ' Always have a default constructor.
        Public Sub New()
            MyBase.New()
            Me.InitClass()
        End Sub

        ' Have a constructor that takes a connection string.
        Public Sub New(ByVal sConnString As String)
            MyBase.New()
            Me.InitClass()
            Me.ConnectionString = sConnString
        End Sub

        Private Sub InitClass()
            m_state = ConnectionState.Closed

            '*
            '* Obtain a connection to the database. In this case,
            '* use the SampleDb class to simulate a connection to 
            '* a real database.
            '*
            m_sampleDb = New SampleDb
        End Sub

        '****
        '* IMPLEMENT THE REQUIRED PROPERTIES.
        '****
        Public Property ConnectionString() As String Implements IDbConnection.ConnectionString
            Get
                ' Always return exactly what the user set.
                ' Security-sensitive information may be removed.
                Return m_sConnString
            End Get
            Set(ByVal Value As String)
                m_sConnString = value
            End Set
        End Property

        Public ReadOnly Property ConnectionTimeout() As Integer Implements IDbConnection.ConnectionTimeout
            Get
                ' Returns the connection time-out value set in the connection
                ' string. Zero indicates an indefinite time-out period.
                Return 0
            End Get
        End Property

        Public ReadOnly Property Database() As String Implements IDbConnection.Database
            Get
                ' Returns an initial database as set in the connection string.
                ' An empty string indicates not set - do not return a null reference.
                Return ""
            End Get
        End Property

        Public ReadOnly Property State() As ConnectionState Implements IDbConnection.State
            Get
                Return m_state
            End Get
        End Property

        '****
        '* IMPLEMENT THE REQUIRED METHODS.
        '****

        Public Overloads Function BeginTransaction() As IDbTransaction Implements IDbConnection.BeginTransaction
            Throw New NotSupportedException
        End Function

        Public Overloads Function BeginTransaction(ByVal level As IsolationLevel) As IDbTransaction Implements IDbConnection.BeginTransaction
            Throw New NotSupportedException
        End Function

        Public Sub ChangeDatabase(ByVal dbName As String) Implements IDbConnection.ChangeDatabase
            ' Change the database setting on the back-end. Note that it is a method
            ' and not a property because the operation requires an expensive
            ' round trip.
        End Sub

        Public Sub Open() Implements IDbConnection.Open
            '*
            '* Open the database connection and set the ConnectionState
            '* property. If the underlying connection to the server is 
            '* expensive to obtain, the implementation should provide
            '* implicit pooling of that connection.
            '* 
            '* If the provider also supports automatic enlistment in 
            '* distributed transactions, it should enlist during Open().
            '*
            m_state = ConnectionState.Open
        End Sub

        Public Sub Close() Implements IDbConnection.Close
            '*
            '* Close the database connection and set the ConnectionState
            '* property. If the underlying connection to the server is
            '* being pooled, Close() will release it back to the pool.
            '*
            m_state = ConnectionState.Closed
        End Sub

        Public Function CreateCommand() As IDbCommand Implements IDbConnection.CreateCommand
            ' Return a new instance of a command object.
            Return New TemplateCommand
        End Function

        '*
        '* Implementation specific properties / methods.
        '*
        Friend ReadOnly Property SampleDb() As SampleDb
            Get
                Return m_sampleDb
            End Get
        End Property

        Public Sub Dispose() Implements IDisposable.Dispose
            Me.Dispose(True)
            System.GC.SuppressFinalize(Me)
        End Sub

        Private Sub Dispose(ByVal disposing As Boolean)
            '
            ' Dispose of the object and perform any cleanup.
            '

            If m_state = ConnectionState.Open Then Me.Close()
        End Sub

    End Class
End Namespace
