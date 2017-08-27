Option Explicit On 
Namespace Manager
  Public Class mng

#Region "  Start  "
    Implements NBdbm.Interfaces.iManager

    Private clmng As NBdbm.Interfaces.iManager
        Private aSelf As self

    Public Sub New()
            aSelf = New self

      Dim tipoBanco As String
            tipoBanco = aSelf.Settings.tipoBanco
      If tipoBanco = "SQLSERVER" Then
        clmng = New NBdbm.Manager.SQLSERVER.mngSQLsvr
      ElseIf tipoBanco = "PostGreSQL" Then
        clmng = New NBdbm.Manager.PostGreSQL.mngPGsql
        'Beep()
      ElseIf tipoBanco = "ACCESS97" Then
        'clmng = New NBdbm.Manager.Access97.mngAccess
      ElseIf tipoBanco = "ACCESS2000" Then
        'clmng = New NBdbm.Manager.Access2000.mngAccess
      Else
        'implementar gravação de log de erro por não haver um tipo correspondente
        'makelog("Os parametro validos são: SQLSERVER, PostGreSQL, ACCESS97, ACCESS2000")
        'Throw New ApplicationException("Sem um tipo de conexão especificado não há como prosseguir")
        'Throw New ArgumentNullException("...") não apresenta a mensagem.
        Throw New ArgumentOutOfRangeException("Sem um tipo de banco de dados especificado não há como prosseguir")
        'Throw New NotSupportedException
      End If

    End Sub

    Public Sub New(ByVal manager_DB As NBdbm.Interfaces.iManager)
      Me.clmng = manager_DB
    End Sub

    Public Sub dispose()
      Me.clmng = Nothing
    End Sub

    Protected Overrides Sub Finalize()
      MyBase.Finalize()
      Call dispose()
    End Sub
#End Region

    Public Function createDB(ByVal commandString As String) As Object Implements Interfaces.iManager.createDB
            Return clmng.createDB(commandString)
    End Function

    Public Function createField(ByVal commandString As String) As Object Implements NBdbm.Interfaces.iManager.createField
            Return clmng.createField(commandString)
    End Function

    Public Function createTable(ByVal commandString As String) As Object Implements NBdbm.Interfaces.iManager.createTable
            Return clmng.createTable(commandString)
    End Function

    Public Function createView(ByVal commandString As String) As Object Implements NBdbm.Interfaces.iManager.createView
            Return clmng.createView(commandString)
    End Function

    Public Function readDB(ByVal SQL As String) As System.Data.DataView Implements Interfaces.iManager.readDB
            Return clmng.readDB(SQL)
    End Function

  End Class
End Namespace