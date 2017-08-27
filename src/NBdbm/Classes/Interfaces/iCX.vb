Namespace Interfaces

  Namespace iCX

    Public Interface iCentroCusto
      Inherits Primitivas.iObjetoTabela
      Property centroCusto() As String
    End Interface

    Public Interface iCaixa
      Inherits Primitivas.iObjetoTabela
      Property idCentroCusto() As Integer
      Property idEntidade() As Integer
      Property pagarOUreceber() As Boolean
      Property descricaoConta() As String
      Property vlrConta() As Integer
      Property dtVencimento() As Date
      Property dtPagamento() As Date
      Property acrescimos() As Integer
      Property abatimento() As Integer
      Property vlrPago() As Integer
    End Interface

  End Namespace
End Namespace