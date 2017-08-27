Option Explicit On 

Public Class NBexception
  Inherits System.Exception

  Public Sub New()
    MyBase.new()
  End Sub
  Public Sub New(ByVal message As String)
    MyBase.New(message)
  End Sub
  Public Sub New(ByVal message As String, ByVal innerException As System.Exception)
    MyBase.New(message, innerException)
  End Sub
End Class

Public Class EVTexception
  Inherits Exception
  Public Sub New()
    MyBase.New()
  End Sub

  Public Sub New(ByVal message As String, ByVal source As String, ByVal exception As System.Exception)
    MyBase.New(message, exception)
    Me.Source = source
  End Sub

  Public Sub New(ByVal source As String, ByVal exception As Exception)
    Me.New("", source, exception)
  End Sub

  Public Sub New(ByVal message As String, ByVal source As String)
    MyBase.New(message)
    Me.Source = source
  End Sub
  Public Sub New(ByVal exception As Exception)
    Me.New(exception.Source, exception)
  End Sub
  Public Overrides ReadOnly Property Message() As String
    Get
      Dim msg As New System.Text.StringBuilder
      msg.Append("*** " & MyBase.Source & " ***\r")
      msg.Append(MyBase.Message.Replace(vbCrLf, "\r") & "\r\r")
      msg.Append(Me.InnerMessage(Me.InnerException))
      msg.Append("*** Qualquer dúvida entre em contato com o suporte técnico ***")
      Return msg.ToString.Replace("'", "#")
    End Get
  End Property
  Protected Function InnerMessage(ByRef pException As Exception) As String
    Dim msg As New System.Text.StringBuilder
    If Not pException Is Nothing Then
      msg.Append("*** " & pException.Source & " ***\r")
      msg.Append(pException.Message.Replace(vbCrLf, "\r") & "\r\r")
      msg.Append(InnerMessage(pException.InnerException))
    End If
    Return msg.ToString()
  End Function
End Class

Public Class COBR_Exception
  Inherits EVTexception

  Private aClientID As String
  'Novo construtor informando o ClientID, que serve para definir em
  'Qual campo ocorreu a exception.

  Public Sub New()
    MyBase.New()
  End Sub

  Public Sub New(ByVal message As String, ByVal clientID As String, ByVal source As String)
    MyBase.New(message, source)
    Me.aClientID = clientID
  End Sub

  Public Sub New(ByVal message As String, ByVal source As String, ByVal exception As System.Exception)
    MyBase.New(message, source, exception)
  End Sub

  Public Sub New(ByVal source As String, ByVal exception As Exception)
    MyBase.New(source, exception)
  End Sub

  Public Sub New(ByVal message As String, ByVal source As String)
    MyBase.New(message, source)
  End Sub

  Public Sub New(ByVal exception As Exception)
    MyBase.New(exception)
  End Sub

  Public Property ClientID() As String
    Set(ByVal Value As String)
      Me.aClientID = Value
    End Set
    Get
      Return Me.aClientID
    End Get
  End Property
End Class

Public Class testaClasseException

  Public Function doIt() As tipos.Retorno
    On Error GoTo errdoit
    Dim retorno As tipos.Retorno

        retorno = teste1()
        Return retorno
fimdoIt:
    Exit Function
errdoit:
        System.Windows.Forms.MessageBox.Show(Err.Number & " - " & Err.Description & " - " & Err.Source)
    End Function

  Private Function teste1() As tipos.Retorno
    Dim retorno As New tipos.Retorno
    Dim ob As New teste
    Try
      retorno = ob.meuErro
      If retorno.Sucesso = False Then
        Throw New NBdbm.NBexception("Duplicou a ferração", retorno.Exception)
      End If
    Catch ex As Exception

      Dim rEX As New NBdbm.NBexception("triplicou a ferração", ex)
      retorno.Exception = rEX

    End Try

    Return retorno
  End Function

  Private Class teste
    Dim o As NBdbm.NBexception

    Public Function meuErro() As tipos.Retorno
      Dim retorno As New tipos.Retorno
      Try
        Throw New NBdbm.NBexception("ferrou o barraco")
        '        ob.BeginTransaction(IsolationLevel.Chaos)
      Catch ex As NBdbm.NBexception

        Beep()
        retorno.Exception = ex
      End Try

      Return retorno
    End Function

  End Class

End Class