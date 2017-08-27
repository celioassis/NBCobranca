Namespace Interfaces

  Namespace iEVT

    Public Interface iCadastroFuncionarios
      Inherits NBdbm.Interfaces.iCTR.iCadastroUsuario
    End Interface

    Public Interface iCadastroLocalidades
      'Inherits NBdbm.Interfaces.iCTR.iCadastroEntidade
      Sub Salvar(ByVal NoCommit As Boolean)
      Sub Salvar()
      Sub Excluir(ByVal NoCommit As Boolean)
      Sub Excluir()
      Property Localidade() As Interfaces.iEVT.Primitivas.iLocalidades
    End Interface

    Public Interface iCadastroItens
      Inherits System.IDisposable
      Sub Salvar(ByVal NoCommit As Boolean)
      Sub Salvar()
      Sub Excluir(ByVal NoCommit As Boolean)
      Sub Excluir()
      Property Item() As Interfaces.iEVT.Primitivas.iItem
      Property xmPath_LinkItemNo() As String
    End Interface

    Public Interface iCadatroKit
      'Inherits Interfaces.Primitivas.allClass - > Não herda, porque ela só aglutina!
      Inherits System.IDisposable
      Sub Salvar(ByVal NoCommit As Boolean)
      Sub Salvar()
      Sub Excluir(ByVal NoCommit As Boolean)
      Sub Excluir()
      Property Kit() As Interfaces.iEVT.Primitivas.iKit
      'Property xmPath_LinkEntNo() As String 'Se houver nó para os kits
      Property KitItem() As Interfaces.ievt.Primitivas.iKitItem
    End Interface

    Public Interface iCadatroOrcamento
      'Inherits Interfaces.Primitivas.allClass - > Não herda, porque ela só aglutina!
      Inherits System.IDisposable
      Sub Salvar(ByVal NoCommit As Boolean)
      Sub Salvar()
      Sub Excluir(ByVal NoCommit As Boolean)
      Sub Excluir()
      Property Orc() As Interfaces.iEVT.Primitivas.iOrcamento
      Property OrcItem() As Interfaces.iEVT.Primitivas.iOrcamentoObjs
      Property OrcKit() As Interfaces.iEVT.Primitivas.iOrcamentoObjs
      Property OrcDespAdic() As Interfaces.iEVT.Primitivas.iOrcamentoDespAdicional
      Property OrcParcela() As Interfaces.iEVT.Primitivas.iOrcamentoParcelas
    End Interface

    Public Interface iCadastroCustomROrc
      Inherits System.IDisposable
      Sub Salvar(ByVal nocommit As Boolean)
      Sub Salvar()
      Sub Excluir(ByVal nocommit As Boolean)
      Sub Excluir()
      Property CustomROrc() As Interfaces.iEVT.Primitivas.iCustomROrc
      Property CustomROrc_Grupos() As Interfaces.iEVT.Primitivas.iCustomROrc_Grupos
      Property CustomROrc_ItemGrupo() As Interfaces.iEVT.Primitivas.iCustomROrc_ItensGrupo
      ReadOnly Property ColCustomROrc_Grupos() As System.Collections.Hashtable
      ReadOnly Property ColCustomROrc_ItensGrupo() As System.Collections.Hashtable
    End Interface

    Public Interface iCadatroOrdemServico
      Inherits System.IDisposable
      Sub Salvar(ByVal NoCommit As Boolean)
      Sub Salvar()
      Sub Excluir(ByVal NoCommit As Boolean)
      Sub Excluir()
      Property OS() As Interfaces.iEVT.Primitivas.iOrdemServico
      Property OSObjs() As Interfaces.iEVT.Primitivas.iOrdemServicoObjs
    End Interface

    Public Interface iCadastroContasPagarReceber
      Inherits IDisposable
      Sub Salvar(ByVal NoCommit As Boolean)
      Sub Salvar()
      Sub Excluir(ByVal NoCommit As Boolean)
      Sub Excluir()
      Property Conta() As Interfaces.iEVT.Primitivas.iContasPagarReceber
      Property Parcela() As Interfaces.iEVT.Primitivas.iContasPagarReceber_Parcelas
      ReadOnly Property Parcelas() As NBdbm.Fachadas.NbCollection
    End Interface


    Namespace Primitivas

      Public Interface iQualidade
        Inherits Interfaces.Primitivas.iObjetoTabela
        Property qualidade_key() As String
      End Interface

      Public Interface iSituacao
        Inherits Interfaces.Primitivas.iObjetoTabela
        Property situacao_key() As String
      End Interface

      Public Interface iLocalidades
        Inherits Interfaces.Primitivas.iObjetoTabela
        Property idEntidade() As Integer
        Property Nome_key() As String
        Property comoChegar() As String
      End Interface

      Public Interface iDeposito
        Inherits Interfaces.Primitivas.iObjetoTabela
        Property Nome_Key() As String
        Property Endereco() As String
      End Interface

      Public Interface iItem
        Inherits Interfaces.Primitivas.iObjetoTabela

        ReadOnly Property IdObj() As String
        Property Descricao_key() As String
        Property Quantidade() As Integer
        Property ValorLocacao() As Double
        Property DescMax() As Decimal
        Property VidaUtil() As Integer
        Property NumeroSerie() As String
        Property IdQualidade() As Integer
        Property IdDeposito() As Integer
        Property IdFornecedor() As Integer
        Property Comentario() As String 'Usar o Text.StringBuilder na implementação.
      End Interface

      Public Interface iKit
        Inherits Interfaces.Primitivas.iObjetoTabela
        ReadOnly Property idObj() As String
        Property Nome_key() As String
        Property ValorLocacao() As Double
        Property DescMax() As Double
        Property Instrucoes() As String 'Usar o Text.StringBuilder na implementação.
        Property Quantidade() As Integer
      End Interface

      Public Interface iKitItem
        Inherits Interfaces.Primitivas.iObjetoTabela
        Property idKit_key() As Integer
        Property CodBarrasItem_key() As String
        Property Status() As Single
        Property TransferidoPara() As String
        Property Descricao() As String
        Property ValorLocacao() As Double
      End Interface

      Public Interface iNotaFiscal
        Inherits Interfaces.Primitivas.iObjetoTabela
        'Property idNotaFiscal() As Integer
        Property numeroFiscal_key() As Integer
        Property idOrdemServico() As Integer
        Property cancelada() As Boolean
        Property beneficiario() As String
        Property endereco() As String
        Property municipio() As String
        Property UF() As String
        Property CNPJ() As String
        Property IE() As String
        Property IM() As String
        Property formaPagamento() As String
        Property fone() As String
        Property dataFiscal() As Date
      End Interface

      Public Interface iNotaFiscalItem
        Inherits Interfaces.Primitivas.iObjetoTabela
        'Property idNotaFiscalItem() As Integer
        Property idNotaFiscal_key() As Integer
        Property unidade() As String
        Property quantidade() As Integer
        Property descricao_key() As String
        Property valor() As Decimal
      End Interface

      Public Interface iOrcamento
        Inherits Interfaces.Primitivas.iObjetoTabela
        Property Confirmado() As Short
        Property Descricao_Key() As String
        Property DataEmissao_Key() As Date
        Property DataValidade() As Date
        Property DataVencimento() As Date
        Property IdLocalidade() As Integer
        Property IdEntidade() As Integer
        Property FormaPagamento() As String
        Property PercDesconto() As Double
        Property Contato() As String
        Property IsModelo() As Short
        Property Valor() As Double
      End Interface

      Public Interface iOrcamentoDespAdicional
        Inherits Interfaces.Primitivas.iObjetoTabela
        Property IdOrcamento_Key() As Integer
        Property Descricao_Key() As String
        Property Quantidade() As Integer
        Property ValorDespesa() As Double
      End Interface

      Public Interface iOrcamentoObjs
        Inherits Interfaces.Primitivas.iObjetoTabela
        Property IdOrcamento_key() As Integer
        Property IdObj_key() As String
        Property DataLocacaoInicio_key() As Date
        Property DataLocacaoFinal() As Date
        Property Descricao() As String
        Property Quantidade() As Double
        Property ValorUnitario() As Double
        Property ValorLocacao() As Double
        Property CtrObj() As iCtrObj
      End Interface

      Public Interface iOrcamentoParcelas
        Inherits Interfaces.Primitivas.iObjetoTabela
        Property IdOrcamento_key() As Integer
        Property NumeroParcela_key() As Integer
        Property DataVencimento() As Date
        Property ValorParcela() As Double
      End Interface

      Public Interface iOrdemServico
        Inherits Interfaces.Primitivas.iObjetoTabela
        Property IdOrcamento() As Integer
        Property DataEmissao() As Date
        Property Descricao() As String
        Property IdUsuario() As Integer
        Property DataAlteracao() As Date
      End Interface

      Public Interface iOrdemServicoObjs
        Inherits Interfaces.Primitivas.iObjetoTabela
        Property IdOS() As Integer
        Property IdObj() As String
        Property DataDevolucao() As Date
        Property Quantidade() As Integer
        Property CtrObj() As iCtrObj
      End Interface

      Public Interface iStatusReal
        Inherits Interfaces.Primitivas.iObjetoTabela

        Property IdItem() As Integer
        Property IdKit() As Integer
        Property IdObj() As String
        Property IdEntidade() As Integer
        Property CodBarras() As String
        Property DataDevolucao() As Date
        Property Status() As Single
      End Interface

      Public Interface iStatusHistorico
        Inherits Interfaces.Primitivas.iObjetoTabela

        Property IdItem() As Integer
        Property IdKit() As Integer
        Property IdObj() As String
        Property IdEntidade() As Integer
        Property Dia() As Date
        Property CodBarras() As String
        Property Status() As Single
      End Interface

      Public Interface iStatusPresumido
        Inherits Interfaces.Primitivas.iObjetoTabela

        Property IdItem() As Integer
        Property IdKit() As Integer
        Property idOrcObj() As Integer
        Property IdObj_Key() As String
        Property IdOrcamento_Key() As Integer
        Property Dia_Key() As Date
        Property Quantidade() As Integer
        Property Status() As Single
      End Interface

      Public Interface iLinkItemNo
        Inherits Interfaces.Primitivas.iObjetoTabela
        Property idNo() As Integer
        Property idItem() As Integer
      End Interface

      Public Interface iHistoricoItem
        Inherits Interfaces.Primitivas.iObjetoTabela
        Property idFix() As Integer
        Property idItem_key() As Integer
        Property idSituacao_key() As Integer
        Property Data_key() As DateTime
        Property Hora() As DateTime
        Property idUsuario() As Integer
        Property IP() As String
      End Interface

      Public Interface iContasPagarReceber
        Inherits Interfaces.Primitivas.iObjetoTabela
        Property TipoConta() As Integer
        Property Descricao() As String
        Property Vencimento() As Date
        Property ValorTotal() As Double
        Property Parcelas() As Integer
        Property AnotacoesAdicionais() As String
      End Interface

      Public Interface iContasPagarReceber_Parcelas
        Inherits Interfaces.Primitivas.iObjetoTabela
        Property idConta() As Integer
        Property Data() As Date
        Property Valor() As Double
      End Interface

      Public Interface iCtrStatus
        Sub SalvarPresumido()
        Sub SalvarReal()
      End Interface

      Public Interface iCtrObj
        ReadOnly Property IdObj() As String
        Sub SalvarPresumido(ByVal noCommit As Boolean, ByVal idorcamento As Integer, ByVal idorcobj As Integer)
      End Interface

      Public Interface iCustomROrc
        Inherits Interfaces.Primitivas.iObjetoTabela
        Property IdOrcamento() As Integer
        Property PathImgCab() As String
        Property TxtApresentacao() As String
      End Interface

      Public Interface iCustomROrc_Grupos
        Inherits Interfaces.Primitivas.iObjetoTabela
        Property IdCustomRO() As Integer
        Property Titulo() As String
        Property SubTitulo() As String
      End Interface

      Public Interface iCustomROrc_ItensGrupo
        Inherits System.IDisposable
        Property IdCustomRO_Grupos() As Integer
        Property IdObj() As String
        Property Quantidade() As Integer
        Property Descricao() As String
        Property Marca() As String
        Property Modelo() As String
        Sub Salvar(ByVal nocommit As Boolean)
        Sub Salvar()
      End Interface

    End Namespace
  End Namespace
End Namespace
