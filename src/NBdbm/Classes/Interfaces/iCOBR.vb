
Namespace Interfaces.iCOBR

    Public Interface iCadastroAcionamentos
        Inherits System.IDisposable
        Sub Salvar(ByVal NoCommit As Boolean)
        Sub Salvar()
        Property Acionamento() As iCOBR.Primitivas.iAcionamentos
        ReadOnly Property ColecaoAcionamentos() As NBdbm.Fachadas.NbCollection
        ReadOnly Property ColecaoNovosAcionamentos() As NBdbm.Fachadas.NbCollection
        ReadOnly Property FichaDevedor() As NBdbm.Fachadas.plxCOBR.CadastroEntidade
    End Interface
    Public Interface iLacamentoBaixa
        Inherits System.IDisposable
        Sub Salvar(ByVal NoCommit As Boolean)
        Sub Salvar()
        Property Baixa() As iCOBR.Primitivas.iBaixas
    End Interface
    Namespace Primitivas
        Public Interface iTipoDivida
            Inherits Interfaces.Primitivas.iObjetoTabela
            Property Descricao() As String
        End Interface
        Public Interface iTipoAcionamento
            Inherits Interfaces.Primitivas.iObjetoTabela
            Property Descricao() As String
            Property DiasReacionamento() As Integer
            Property CredencialExigida() As Integer
        End Interface
        Public Interface iDivida
            Inherits Interfaces.Primitivas.iObjetoTabela
            Property idEntidade() As Integer
            Property idTipoDivida() As Integer
            Property Contrato() As String
            Property NumDoc() As Integer
            Property DataVencimento() As Date
            Property ValorNominal() As Double
            Property ValorNominalParcial() As Double
            Property DataBaixa() As Date
            Property BorderoBaixa() As Integer
            Property NumRecibo() As Integer
            Property idCobrador() As Integer
            Property PerCobrador() As Double
            Property idUsuarioBaixa() As Integer
            Property BaixaNoCliente() As Boolean
            Property Baixada() As Boolean
            Property BaixaParcial() As Boolean
            Property XmPathCliente() As String
        End Interface
        Public Interface iAcionamentos
            Inherits Interfaces.Primitivas.iObjetoTabela
            Property idEntidade() As Integer
            Property idUsuario() As Integer
            Property idTipoAcionamento() As Integer
            Property TextoRespeito() As String
            Property DataAcionamento() As System.DateTime
            Property DataPromessa() As Date
        End Interface
        Public Interface iTarifas
            Inherits Interfaces.Primitivas.iObjetoTabela
            Property idEntidade() As Integer
            Property Juros() As Double
            Property Multa() As Double
        End Interface
        Public Interface iBaixas
            Inherits Interfaces.Primitivas.iObjetoTabela
            Property idEntidade() As Integer
            Property idDivida() As Integer
            Property BaixadoCliente() As Boolean
            Property NumBordero() As Integer
            Property DataBaixa() As Date
            Property ValorNominal() As Double
            Property ValorBaixa() As Double
            Property ValorRecebido() As Double
        End Interface
    End Namespace
End Namespace