Option Explicit On 

Imports System
Imports System.Data

Namespace DotNetDataProviderTemplate
    Public Class TemplateCommand
        Implements IDbCommand

        Private m_connection As TemplateConnection
        Private m_txn As TemplateTransaction
        Private m_sCmdText As String
        Private m_updatedRowSource As UpdateRowSource = UpdateRowSource.None
        Private m_parameters As TemplateParameterCollection = New TemplateParameterCollection

        ' Implement the default constructor here.
        Public Sub New()
            MyBase.New()
        End Sub

        ' Implement other constructors here.
        Public Sub New(ByVal cmdText As String)
            MyBase.New()

            m_sCmdText = cmdText
        End Sub

        Public Sub New(ByVal cmdText As String, ByVal connection As TemplateConnection)
            MyBase.New()

            m_sCmdText = cmdText
            m_connection = connection
        End Sub

        Public Sub New(ByVal cmdText As String, ByVal connection As TemplateConnection, ByVal txn As TemplateTransaction)
            MyBase.New()

            m_sCmdText = cmdText
            m_connection = connection
            m_txn = txn
        End Sub

        '****
        '* IMPLEMENT THE REQUIRED PROPERTIES.
        '****
        Public Property CommandText() As String Implements IDbCommand.CommandText
            Get
                Return m_sCmdText
            End Get
            Set(ByVal Value As String)
                m_sCmdText = value
            End Set
        End Property

        Public Property CommandTimeout() As Integer Implements IDbCommand.CommandTimeout
            '*
            ' * The sample does not support a command time-out. As a result,
            ' * for the get, zero is returned because zero indicates an indefinite
            ' * time-out period. For the set, throw an exception.
            '*
            Get
                Return 0
            End Get
            Set(ByVal Value As Integer)
                If value <> 0 Then Throw New NotSupportedException
            End Set
        End Property

        Public Property CommandType() As CommandType Implements IDbCommand.CommandType
            '*
            '* The sample only supports CommandType.Text.
            '*
            Get
                Return CommandType.Text
            End Get
            Set(ByVal Value As CommandType)
                If value <> CommandType.Text Then Throw New NotSupportedException
            End Set
        End Property

        Public Property Connection() As IDbConnection Implements IDbCommand.Connection
            '**
            '* The user should be able to set or change the connection at 
            '* any time.
            '*
            Get
                Return m_connection
            End Get
            Set(ByVal Value As IDbConnection)
                '*
                '* Because the connection is associated with the transaction,
                '* setthe transaction object to return a null reference if the connection 
                '* is reset.
                '*
                If Not m_connection Is value Then Me.Transaction = Nothing

                m_connection = CType(value, TemplateConnection)
            End Set
        End Property

        Protected ReadOnly Property Parameters() As IDataParameterCollection Implements IDbCommand.Parameters
            Get
                Return CType(m_parameters, TemplateParameterCollection)
            End Get
        End Property

        Public Property Transaction() As IDbTransaction Implements IDbCommand.Transaction
            '*
            '* Set the transaction. Consider additional steps to ensure that the transaction
            '* is compatible with the connection, because the two are usually linked.
            '*
            Get
                Return m_txn
            End Get
            Set(ByVal Value As IDbTransaction)
                m_txn = CType(value, TemplateTransaction)
            End Set
        End Property

        Public Property UpdatedRowSource() As UpdateRowSource Implements IDbCommand.UpdatedRowSource
            Get
                Return m_updatedRowSource
            End Get
            Set(ByVal Value As UpdateRowSource)
                m_updatedRowSource = value
            End Set
        End Property

        '****
        '* IMPLEMENT THE REQUIRED METHODS.
        '****
        Public Sub Cancel() Implements IDbCommand.Cancel
            ' The sample does not support canceling a command
            ' once it has been initiated.
            Throw New NotSupportedException
        End Sub

        Public Function CreateParameter() As IDbDataParameter Implements IDbCommand.CreateParameter
            Return CType(New TemplateParameter, IDbDataParameter)
        End Function

        Public Function ExecuteNonQuery() As Integer Implements IDbCommand.ExecuteNonQuery
            '*
            '* ExecuteNonQuery is intended for commands that do
            '* not return results, instead returning only the number
            '* of records affected.
            '*

            ' There must be a valid and open connection.
            If m_connection Is Nothing Or m_connection.State <> ConnectionState.Open Then
                Throw New InvalidOperationException("Connection must valid and open")
            End If

            ' Execute the command.
            Dim resultSet As New SampleDb.SampleDbResultSet()
            'm_connection.SampleDb.Execute(m_sCmdText, resultSet)

            ' Return the number of records affected.
            Return resultSet.recordsAffected
        End Function

        Public Overloads Function ExecuteReader() As IDataReader Implements IDbCommand.ExecuteReader
            '*
            '* ExecuteReader should retrieve results from the data source
            '* and return a DataReader that allows the user to process 
            '* the results.
            '*
            ' There must be a valid and open connection.
            If m_connection Is Nothing Or m_connection.State <> ConnectionState.Open Then
                Throw New InvalidOperationException("Connection must valid and open")
            End If

            ' Execute the command.
            Dim resultSet As SampleDb.SampleDbResultSet = New SampleDb.SampleDbResultSet
            m_connection.SampleDb.Execute(m_sCmdText, resultSet)

            Return New TemplateDataReader(resultSet)
        End Function

        Public Overloads Function ExecuteReader(ByVal behavior As CommandBehavior) As IDataReader Implements IDbCommand.ExecuteReader
            '*
            '* ExecuteReader should retrieve results from the data source
            '* and return a DataReader that allows the user to process 
            '* the results.
            '*

            ' There must be a valid and open connection.
            If m_connection Is Nothing Or m_connection.State <> ConnectionState.Open Then
                Throw New InvalidOperationException("Connection must valid and open")
            End If

            ' Execute the command.
            Dim resultSet As SampleDb.SampleDbResultSet = New SampleDb.SampleDbResultSet
            m_connection.SampleDb.Execute(m_sCmdText, resultSet)

            '*
            '* The only CommandBehavior option supported by this
            '* sample is the automatic closing of the connection
            '* when the user is done with the reader.
            '*
            If behavior = CommandBehavior.CloseConnection Then
                Return New TemplateDataReader(resultSet, m_connection)
            Else
                Return New TemplateDataReader(resultSet)
            End If
        End Function

        Public Function ExecuteScalar() As Object Implements IDbCommand.ExecuteScalar
            '*
            '* ExecuteScalar assumes that the command will return a single
            '* row with a single column, or if more rows/columns are returned
            '* it will return the first column of the first row.
            '*

            ' There must be a valid and open connection.
            If m_connection Is Nothing Or m_connection.State <> ConnectionState.Open Then
                Throw New InvalidOperationException("Connection must valid and open")
            End If

            ' Execute the command.
            Dim resultSet As SampleDb.SampleDbResultSet = New SampleDb.SampleDbResultSet
            m_connection.SampleDb.Execute(m_sCmdText, resultSet)

            ' Return the first column of the first row.
            ' Return a null reference if there is no data.
            If resultSet.data.Length = 0 Then Return Nothing

            Return resultSet.data(0, 0)
        End Function

        Public Sub Prepare() Implements IDbCommand.Prepare
            ' The sample Prepare is a no-op.
        End Sub

        Public Sub Dispose() Implements IDisposable.Dispose
            Me.Dispose(True)
            System.GC.SuppressFinalize(Me)
        End Sub

        Private Sub Dispose(ByVal disposing As Boolean)
            '
            ' Dispose of the object and perform any cleanup.
            '
        End Sub

    End Class
End Namespace

