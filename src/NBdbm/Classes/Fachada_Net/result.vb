Imports System
Imports System.Data

Namespace DotNetDataProviderTemplate
    '*
    '* This class provides database-like operations to simulate a real
    '* data source. The class generates sample data and uses a
    '* fixed set of commands.
    '*
    Public Class SampleDb
        Private Const m_sSelectCmd As String = "select "
        Private Const m_sUpdateCmd As String = "update "

        Public Class SampleDbResultSet
            Public Structure MetaDataStruct
                Public name As String
                Public type As Type
                Public maxSize As Integer
            End Structure

            Public recordsAffected As Integer
            Public metaData() As MetaDataStruct
            Public data(,) As Object
        End Class

        Private m_resultset As SampleDbResultSet

        Public Sub Execute(ByVal sCmd As String, ByRef resultset As SampleDbResultSet)
            '*
            '* The sample code simulates SELECT and UPDATE operations.
            '*
            If 0 = String.Compare(sCmd, 0, m_sSelectCmd, 0, m_sSelectCmd.Length, True) Then
                _executeSelect(resultset)
            ElseIf 0 = String.Compare(sCmd, 0, m_sUpdateCmd, 0, m_sUpdateCmd.Length, True) Then
                _executeUpdate(resultset)
            Else
                Throw New NotSupportedException("Command string was not recognized")
            End If
        End Sub

        Private Sub _executeSelect(ByRef resultset As SampleDbResultSet)
            ' If no sample data exists, create it.
            If m_resultset Is Nothing Then _resultsetCreate()

            ' Return the sample results.
            resultset = m_resultset
        End Sub

        Private Sub _executeUpdate(ByRef resultset As SampleDbResultSet)
            ' If no sample data exists, create it.
            If m_resultset Is Nothing Then _resultsetCreate()

            ' Change a row to simulate an update command.
            m_resultset.data(2, 2) = 4199

            ' Create a result set object that is empty except for the RecordsAffected field.
            resultset = New SampleDbResultSet
            resultset.recordsAffected = 1
        End Sub

        Private Sub _resultsetCreate()
            m_resultset = New SampleDbResultSet

            ' RecordsAffected is always zero for a SELECT.
            m_resultset.recordsAffected = 0

            Const numCols As Integer = 3

            Dim metaDataArray() As SampleDbResultset.MetaDataStruct
            ReDim metaDataArray(numCols)
            m_resultset.metaData = metaDataArray

            _resultsetFillColumn(0, "id", Type.GetType("System.Int32"), 0)
            _resultsetFillColumn(1, "name", Type.GetType("System.String"), 64)
            _resultsetFillColumn(2, "orderid", Type.GetType("System.Int32"), 0)

            Dim dataArray As Object
            ReDim dataArray(5, numCols)
            m_resultset.data = dataArray

            _resultsetFillRow(0, 1, "Biggs", 2001)
            _resultsetFillRow(1, 2, "Brown", 2121)
            _resultsetFillRow(2, 3, "Jones", 2543)
            _resultsetFillRow(3, 4, "Smith", 2772)
            _resultsetFillRow(4, 5, "Tyler", 3521)
        End Sub

        Private Sub _resultsetFillColumn(ByVal nIdx As Integer, ByVal name As String, ByVal type As Type, ByVal maxSize As Integer)
            m_resultset.metaData(nIdx).name = name
            m_resultset.metaData(nIdx).type = type
            m_resultset.metaData(nIdx).maxSize = maxSize
        End Sub

        Private Sub _resultsetFillRow(ByVal nIdx As Integer, ByVal id As Integer, ByVal name As String, ByVal orderid As Integer)
            m_resultset.data(nIdx, 0) = id
            m_resultset.data(nIdx, 1) = name
            m_resultset.data(nIdx, 2) = orderid
        End Sub
    End Class
End Namespace
