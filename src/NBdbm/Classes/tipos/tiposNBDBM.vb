Option Explicit On 
Namespace tipos

  Public Enum tiposConection
    Default_ = 0
    SQLSERVER = 1
    OLEDB = 2
    OUTDB = 3
    SQLSVR_LUG = 4
  End Enum

  Public Enum BoolYes
    vbTrue = -1  'Microsoft.VisualBasic.TriState = -1
    vbFalse = 0
    vbYes = 6
    vbNo = 7
  End Enum

  Public Enum TipoEntidade
    Funcionários = 1
    Clientes = 2
    Fornecedores = 3
    Devedores = 4
    Localidades = 5
    Todas = 6
  End Enum
  Public Class Versao
    Implements IDisposable


    Public major As Integer
    Public minor As Integer
    Public revision As Integer

    Public Overrides Function toString() As String
      toString = Format(major, "00") & "." & Format(minor, "00") & "." & Format(revision, "0000")
      toString = toString.Trim()
    End Function

    Public Sub Dispose() Implements System.IDisposable.Dispose
      major = Nothing
      minor = Nothing
      revision = Nothing
    End Sub
  End Class

  Public Class Retorno
    Implements IDisposable


    Private vSucesso As Boolean
    Private vTag As String
    Private vObjeto As Object
    Private vException As NBdbm.NBexception

    Public Property Sucesso() As Boolean
      Get
        Return vSucesso
      End Get
      Set(ByVal Value As Boolean)
        vSucesso = Value
      End Set
    End Property

    Public Property Tag() As String
      Get
        Return vTag
      End Get
      Set(ByVal Value As String)
        vTag = Value
      End Set
    End Property

    Public Property Objeto() As Object
      Get
        Return vObjeto
      End Get
      Set(ByVal Value As Object)
        vObjeto = Value
      End Set
    End Property

    Public Shadows ReadOnly Property ToString() As String
      Get
        Return vObjeto.ToString
      End Get
    End Property

    Public Property Exception() As NBdbm.NBexception
      Get
        Return vException
      End Get
      Set(ByVal Value As NBdbm.NBexception)
        vException = Value
      End Set
    End Property

    Public ReadOnly Property MensagemErro() As String
      Get
        Return vException.Source & vException.Message & "**** Mensagem Original ****\r\r" & vException.InnerException.Message & "\r\r*** Entre em Contato com o Desenvolvedor do Sistema ***"
      End Get
    End Property
    Public Sub Dispose() Implements System.IDisposable.Dispose
      vSucesso = Nothing
      vTag = Nothing
      vObjeto = Nothing
      vException = Nothing
    End Sub
  End Class

End Namespace