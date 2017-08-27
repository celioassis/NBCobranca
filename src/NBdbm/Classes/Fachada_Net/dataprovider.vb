Option Explicit On 

Imports System
Imports System.Data
Imports System.Globalization

Namespace DotNetDataProviderTemplate

    Public Class TemplateDataReader
        Implements IDataReader
        Implements IDataRecord

        ' The DataReader should always be open when returned to the user.
        Private m_fOpen As Boolean = True

        ' Keep track of the results and position
        ' within that resultset (starts prior to first record).
        Private m_resultset As SampleDb.SampleDbResultSet
        Private Shared m_STARTPOS As Integer = -1
        Private m_nPos As Integer = m_STARTPOS

        '* 
        '* Keep track of the connection to implement the
        '* CommandBehavior.CloseConnection flag. Nothing (a null reference) means
        '* normal behavior (do not automatically close).
        '*
        Private m_connection As TemplateConnection = Nothing

        '*
        '* Because the user should not be able to directly create a 
        '* DataReader object, the constructor is
        '* marked as internal.
        '*

        Friend Sub New(ByRef resultset As SampleDb.SampleDbResultSet)
            MyBase.New()
            m_resultset = resultset
        End Sub

        Friend Sub New(ByRef resultset As SampleDb.SampleDbResultSet, ByRef connection As TemplateConnection)
            MyBase.New()
            m_resultset = resultset
            m_connection = connection
        End Sub

        '****
        '* METHODS / PROPERTIES FROM IDataReader.
        '****
        Public ReadOnly Property Depth() As Integer Implements IDataReader.Depth
            '*
            '* Always return a value of zero if nesting is not supported.
            '*
            Get
                Return 0
            End Get
        End Property

        Public ReadOnly Property IsClosed() As Boolean Implements IDataReader.IsClosed
            '*
            '* Keep track of the reader state - some methods should be
            '* disallowed if the reader is closed.
            '*
            Get
                Return Not m_fOpen
            End Get
        End Property

        Public ReadOnly Property RecordsAffected() As Integer Implements IDataReader.RecordsAffected
            '*
            '* RecordsAffected is only applicable to batch statements
            '* that include inserts/updates/deletes. The sample always
            '* returns -1.
            '*
            Get
                Return -1
            End Get
        End Property

        Public Sub Close() Implements IDataReader.Close
            '*
            '* Close the reader. The sample only changes the state,
            '* but an actual implementation would also clean up any 
            '* resources used by the operation. For example,
            '* cleaning up any resources waiting for data to be
            '* returned by the server.
            '*
            m_fOpen = False
        End Sub

        Public Function NextResult() As Boolean Implements IDataReader.NextResult
            ' The sample only returns a single resultset. However,
            ' DbDataAdapter expects NextResult to return a value.
            Return False
        End Function


        Public Function Read() As Boolean Implements IDataReader.Read
            ' Return True if it is possible to advance and if you are still positioned
            ' on a valid row. Because the data array in the resultset
            ' is two-dimensional, you must divide by the number of columns.
            m_nPos = m_nPos + 1
            If m_nPos + 1 >= (m_resultset.data.Length / m_resultset.metaData.Length) Then
                Return False
            Else
                Return True
            End If
        End Function

        Public Function GetSchemaTable() As DataTable Implements IDataReader.GetSchemaTable
            '$
            Throw New NotSupportedException
        End Function

        '****
        '* METHODS / PROPERTIES FROM IDataRecord.
        '****

        Default Public Overloads ReadOnly Property Item(ByVal i As Integer) As Object Implements IDataRecord.Item
            Get
                Return GetValue(i)
            End Get
        End Property

        Default Public Overloads ReadOnly Property Item(ByVal name As String) As Object Implements IDataRecord.Item
            Get
                ' Look up the ordinal and return the value at that position.
                Return Me.GetOrdinal(name)
            End Get
        End Property

        Public ReadOnly Property FieldCount() As Integer Implements IDataRecord.FieldCount
            ' Return a count of the number of columns, which in
            ' this case is the size of the column metadata array.
            Get
                Return m_resultset.metaData.Length - 1
            End Get
        End Property

        Public Function GetName(ByVal i As Integer) As String Implements IDataRecord.GetName
            Return m_resultset.metaData(i).name
        End Function

        Public Function GetDataTypeName(ByVal i As Integer) As String Implements IDataRecord.GetDataTypeName
            '*
            '* Usually this would return the name of the type
            '* as used on the back end, for example 'smallint' or 'varchar'.
            '* In this case use the simple name of the .NET Framework type.
            '*
            Return m_resultset.metaData(i).type.Name
        End Function

        Public Function GetFieldType(ByVal i As Integer) As Type Implements IDataRecord.GetFieldType
            ' Return the actual Type class for the data type.
            Return m_resultset.metaData(i).type
        End Function

        Public Function GetValue(ByVal i As Integer) As Object Implements IDataRecord.GetValue
            Return m_resultset.data(m_nPos, i)
        End Function

        Public Function GetValues(ByVal values() As Object) As Integer Implements IDataRecord.GetValues
            Dim i As Integer = 0
            Dim j As Integer = 0

            Do While i < values.Length And j < m_resultset.metaData.Length
                values(i) = m_resultset.data(m_nPos, j)

                i += 1
                j += 1
            Loop

            Return i
        End Function

        Public Function GetOrdinal(ByVal name As String) As Integer Implements IDataRecord.GetOrdinal
            ' Look for the ordinal of the column with the same name and return it.
            Dim i As Integer
            For i = 0 To m_resultset.metaData.Length - 1
                If 0 = _cultureAwareCompare(name, m_resultset.metaData(i).name) Then Return i
            Next

            ' Throw an exception if the ordinal cannot be found.
            Throw New IndexOutOfRangeException("Could not find the specified column in the results")
        End Function

        Public Function GetBoolean(ByVal i As Integer) As Boolean Implements IDataRecord.GetBoolean
            '*
            '* Force the cast to return the type. InvalidCastException
            '* should be thrown if the data is not already of the correct type.
            '*
            Return CType(m_resultset.data(m_nPos, i), Boolean)
        End Function

        Public Function GetByte(ByVal i As Integer) As Byte Implements IDataRecord.GetByte
            '*
            '* Force the cast to return the type. InvalidCastException
            '* should be thrown if the data is not already of the correct type.
            '*
            Return CType(m_resultset.data(m_nPos, i), Byte)
        End Function

        Public Function GetBytes(ByVal i As Integer, ByVal fieldOffset As Long, ByVal buffer() As Byte, ByVal bufferOffSet As Integer, ByVal length As Integer) As Long Implements IDataRecord.GetBytes
            ' The sample does not support this method.
            Throw New NotSupportedException("GetBytes not supported.")
        End Function

        Public Function GetChar(ByVal i As Integer) As Char Implements IDataRecord.GetChar
            '*
            '* Force the cast to return the type. InvalidCastException
            '* should be thrown if the data is not already of the correct type.
            '*
            Return CType(m_resultset.data(m_nPos, i), Char)
        End Function

        Public Function GetChars(ByVal i As Integer, ByVal fieldOffSet As Long, ByVal buffer() As Char, ByVal bufferOffSet As Integer, ByVal length As Integer) As Long Implements IDataRecord.GetChars
            ' The sample code does not support this method.
            Throw New NotSupportedException("GetChars not supported.")
        End Function

        Public Function GetGuid(ByVal i As Integer) As Guid Implements IDataRecord.GetGuid
            '*
            '* Force the cast to return the type. InvalidCastException
            '* should be thrown if the data is not already of the correct type.
            '*
            Return CType(m_resultset.data(m_nPos, i), Guid)
        End Function

        Public Function GetInt16(ByVal i As Integer) As Int16 Implements IDataRecord.GetInt16
            '*
            '* Force the cast to return the type. InvalidCastException
            '* should be thrown if the data is not already of the correct type.
            '*
            Return CType(m_resultset.data(m_nPos, i), Int16)
        End Function

        Public Function GetInt32(ByVal i As Integer) As Int32 Implements IDataRecord.GetInt32
            '*
            '* Force the cast to return the type. InvalidCastException
            '* should be thrown if the data is not already of the correct type.
            '*
            Return CType(m_resultset.data(m_nPos, i), Int32)
        End Function

        Public Function GetInt64(ByVal i As Integer) As Int64 Implements IDataRecord.GetInt64
            '*
            '* Force the cast to return the type. InvalidCastException
            '* should be thrown if the data is not already of the correct type.
            '*
            Return CType(m_resultset.data(m_nPos, i), Int64)
        End Function

        Public Function GetFloat(ByVal i As Integer) As Single Implements IDataRecord.GetFloat
            '*
            '* Force the cast to return the type. InvalidCastException
            '* should be thrown if the data is not already of the correct type.
            '*
            Return CType(m_resultset.data(m_nPos, i), Single)
        End Function

        Public Function GetDouble(ByVal i As Integer) As Double Implements IDataRecord.GetDouble
            '*
            '* Force the cast to return the type. InvalidCastException
            '* should be thrown if the data is not already of the correct type.
            '*
            Return CType(m_resultset.data(m_nPos, i), Double)
        End Function

        Public Function GetString(ByVal i As Integer) As String Implements IDataRecord.GetString
            '*
            '* Force the cast to return the type. InvalidCastException
            '* should be thrown if the data is not already of the correct type.
            '*
            Return CType(m_resultset.data(m_nPos, i), String)
        End Function

        Public Function GetDecimal(ByVal i As Integer) As Decimal Implements IDataRecord.GetDecimal
            '*
            '* Force the cast to return the type. InvalidCastException
            '* should be thrown if the data is not already of the correct type.
            '*
            Return CType(m_resultset.data(m_nPos, i), Decimal)
        End Function

        Public Function GetDateTime(ByVal i As Integer) As DateTime Implements IDataRecord.GetDateTime
            '*
            '* Force the cast to return the type. InvalidCastException
            '* should be thrown if the data is not already of the correct type.
            '*
            Return CType(m_resultset.data(m_nPos, i), DateTime)
        End Function

        Public Function GetData(ByVal i As Integer) As IDataReader Implements IDataRecord.GetData
            '*
            '* The sample code does not support this method. Normally,
            '* this would be used to expose nested tables and
            '* other hierarchical data.
            '*
            Throw New NotSupportedException("GetData not supported.")
        End Function

        Public Function IsDBNull(ByVal i As Integer) As Boolean Implements IDataRecord.IsDBNull
            Return m_resultset.data(m_nPos, i) Is DBNull.Value
        End Function

        '*
        '* Implementation specific methods.
        '*
        Private Function _cultureAwareCompare(ByVal strA As String, ByVal strB As String) As Integer
            Return CultureInfo.CurrentCulture.CompareInfo.Compare(strA, strB, CompareOptions.IgnoreKanaType Or CompareOptions.IgnoreWidth Or CompareOptions.IgnoreCase)
        End Function

        Public Sub Dispose() Implements IDisposable.Dispose
            Me.Dispose(True)
            System.GC.SuppressFinalize(Me)
        End Sub

        Public Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                Try
                    Me.Close()
        Catch ex As Exception
          Throw New SystemException("An exception of type " & ex.GetType.ToString & _
                                    " was encountered while closing the TemplateDataReader.")
        End Try
      End If
        End Sub

    End Class
End Namespace
