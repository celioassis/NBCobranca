Option Explicit On
Namespace Interfaces

    Public Interface iManager

        Function createDB(ByVal commandString As String) As Object

        Function createTable(ByVal commandString As String) As Object

        Function createView(ByVal commandString As String) As Object

        Function createField(ByVal commandString As String) As Object

        Function readDB(ByVal SQL As String) As Data.DataView

    End Interface

End Namespace