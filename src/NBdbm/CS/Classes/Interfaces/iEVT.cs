using System.Data;
using System.Collections;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System;

namespace NBdbm.Interfaces.iEVT
{
    public interface iCadastroFuncionarios : NBdbm.Interfaces.iCTR.iCadastroUsuario
    {
    }

    public interface iCadastroLocalidades
    {
        //Inherits NBdbm.Interfaces.iCTR.iCadastroEntidade
        void Salvar(bool NoCommit);
        void Salvar();
        void Excluir(bool NoCommit);
        void Excluir();
        Interfaces.iEVT.Primitivas.iLocalidades Localidade
        {
            get;
            set;
        }
    }

    public interface iCadastroItens : System.IDisposable
    {
        void Salvar(bool NoCommit);
        void Salvar();
        void Excluir(bool NoCommit);
        void Excluir();
        Interfaces.iEVT.Primitivas.iItem Item
        {
            get;
            set;
        }
        string xmPath_LinkItemNo
        {
            get;
            set;
        }
    }

    public interface iCadatroKit : System.IDisposable
    {
        //Inherits Interfaces.Primitivas.allClass - > Não herda, porque ela só aglutina!
        void Salvar(bool NoCommit);
        void Salvar();
        void Excluir(bool NoCommit);
        void Excluir();
        Interfaces.iEVT.Primitivas.iKit Kit
        {
            get;
            set;
        }
        //Property xmPath_LinkEntNo() As String 'Se houver nó para os kits
        Interfaces.iEVT.Primitivas.iKitItem KitItem
        {
            get;
            set;
        }
    }

    public interface iCadatroOrcamento : System.IDisposable
    {
        //Inherits Interfaces.Primitivas.allClass - > Não herda, porque ela só aglutina!
        void Salvar(bool NoCommit);
        void Salvar();
        void Excluir(bool NoCommit);
        void Excluir();
        Interfaces.iEVT.Primitivas.iOrcamento Orc
        {
            get;
            set;
        }
        Interfaces.iEVT.Primitivas.iOrcamentoObjs OrcItem
        {
            get;
            set;
        }
        Interfaces.iEVT.Primitivas.iOrcamentoObjs OrcKit
        {
            get;
            set;
        }
        Interfaces.iEVT.Primitivas.iOrcamentoDespAdicional OrcDespAdic
        {
            get;
            set;
        }
        Interfaces.iEVT.Primitivas.iOrcamentoParcelas OrcParcela
        {
            get;
            set;
        }
    }

    public interface iCadastroCustomROrc : System.IDisposable
    {
        void Salvar(bool nocommit);
        void Salvar();
        void Excluir(bool nocommit);
        void Excluir();
        Interfaces.iEVT.Primitivas.iCustomROrc CustomROrc
        {
            get;
            set;
        }
        Interfaces.iEVT.Primitivas.iCustomROrc_Grupos CustomROrc_Grupos
        {
            get;
            set;
        }
        Interfaces.iEVT.Primitivas.iCustomROrc_ItensGrupo CustomROrc_ItemGrupo
        {
            get;
            set;
        }
        System.Collections.Hashtable ColCustomROrc_Grupos
        {
            get;
        }
        System.Collections.Hashtable ColCustomROrc_ItensGrupo
        {
            get;
        }
    }

    public interface iCadatroOrdemServico : System.IDisposable
    {
        void Salvar(bool NoCommit);
        void Salvar();
        void Excluir(bool NoCommit);
        void Excluir();
        Interfaces.iEVT.Primitivas.iOrdemServico OS
        {
            get;
            set;
        }
        Interfaces.iEVT.Primitivas.iOrdemServicoObjs OSObjs
        {
            get;
            set;
        }
    }

    public interface iCadastroContasPagarReceber : IDisposable
    {
        void Salvar(bool NoCommit);
        void Salvar();
        void Excluir(bool NoCommit);
        void Excluir();
        Interfaces.iEVT.Primitivas.iContasPagarReceber Conta
        {
            get;
            set;
        }
        Interfaces.iEVT.Primitivas.iContasPagarReceber_Parcelas Parcela
        {
            get;
            set;
        }
        NBdbm.Fachadas.NbCollection Parcelas
        {
            get;
        }
    }


    namespace Primitivas
    {

        public interface iQualidade : Interfaces.Primitivas.iObjetoTabela
        {
            string qualidade_key
            {
                get;
                set;
            }
        }

        public interface iSituacao : Interfaces.Primitivas.iObjetoTabela
        {
            string situacao_key
            {
                get;
                set;
            }
        }

        public interface iLocalidades : Interfaces.Primitivas.iObjetoTabela
        {
            int idEntidade
            {
                get;
                set;
            }
            string Nome_key
            {
                get;
                set;
            }
            string comoChegar
            {
                get;
                set;
            }
        }

        public interface iDeposito : Interfaces.Primitivas.iObjetoTabela
        {
            string Nome_Key
            {
                get;
                set;
            }
            string Endereco
            {
                get;
                set;
            }
        }

        public interface iItem : Interfaces.Primitivas.iObjetoTabela
        {

            string IdObj
            {
                get;
            }
            string Descricao_key
            {
                get;
                set;
            }
            int Quantidade
            {
                get;
                set;
            }
            double ValorLocacao
            {
                get;
                set;
            }
            decimal DescMax
            {
                get;
                set;
            }
            int VidaUtil
            {
                get;
                set;
            }
            string NumeroSerie
            {
                get;
                set;
            }
            int IdQualidade
            {
                get;
                set;
            }
            int IdDeposito
            {
                get;
                set;
            }
            int IdFornecedor
            {
                get;
                set;
            }
            string Comentario
            { //Usar o Text.StringBuilder na implementação.
                get;
                set;
            }
        }

        public interface iKit : Interfaces.Primitivas.iObjetoTabela
        {
            string idObj
            {
                get;
            }
            string Nome_key
            {
                get;
                set;
            }
            double ValorLocacao
            {
                get;
                set;
            }
            double DescMax
            {
                get;
                set;
            }
            string Instrucoes
            { //Usar o Text.StringBuilder na implementação.
                get;
                set;
            }
            int Quantidade
            {
                get;
                set;
            }
        }

        public interface iKitItem : Interfaces.Primitivas.iObjetoTabela
        {
            int idKit_key
            {
                get;
                set;
            }
            string CodBarrasItem_key
            {
                get;
                set;
            }
            float Status
            {
                get;
                set;
            }
            string TransferidoPara
            {
                get;
                set;
            }
            string Descricao
            {
                get;
                set;
            }
            double ValorLocacao
            {
                get;
                set;
            }
        }

        public interface iNotaFiscal : Interfaces.Primitivas.iObjetoTabela
        {
            //Property idNotaFiscal() As Integer
            int numeroFiscal_key
            {
                get;
                set;
            }
            int idOrdemServico
            {
                get;
                set;
            }
            bool cancelada
            {
                get;
                set;
            }
            string beneficiario
            {
                get;
                set;
            }
            string endereco
            {
                get;
                set;
            }
            string municipio
            {
                get;
                set;
            }
            string UF
            {
                get;
                set;
            }
            string CNPJ
            {
                get;
                set;
            }
            string IE
            {
                get;
                set;
            }
            string IM
            {
                get;
                set;
            }
            string formaPagamento
            {
                get;
                set;
            }
            string fone
            {
                get;
                set;
            }
            DateTime dataFiscal
            {
                get;
                set;
            }
        }

        public interface iNotaFiscalItem : Interfaces.Primitivas.iObjetoTabela
        {
            //Property idNotaFiscalItem() As Integer
            int idNotaFiscal_key
            {
                get;
                set;
            }
            string unidade
            {
                get;
                set;
            }
            int quantidade
            {
                get;
                set;
            }
            string descricao_key
            {
                get;
                set;
            }
            decimal valor
            {
                get;
                set;
            }
        }

        public interface iOrcamento : Interfaces.Primitivas.iObjetoTabela
        {
            short Confirmado
            {
                get;
                set;
            }
            string Descricao_Key
            {
                get;
                set;
            }
            DateTime DataEmissao_Key
            {
                get;
                set;
            }
            DateTime DataValidade
            {
                get;
                set;
            }
            DateTime DataVencimento
            {
                get;
                set;
            }
            int IdLocalidade
            {
                get;
                set;
            }
            int IdEntidade
            {
                get;
                set;
            }
            string FormaPagamento
            {
                get;
                set;
            }
            double PercDesconto
            {
                get;
                set;
            }
            string Contato
            {
                get;
                set;
            }
            short IsModelo
            {
                get;
                set;
            }
            double Valor
            {
                get;
                set;
            }
        }

        public interface iOrcamentoDespAdicional : Interfaces.Primitivas.iObjetoTabela
        {
            int IdOrcamento_Key
            {
                get;
                set;
            }
            string Descricao_Key
            {
                get;
                set;
            }
            int Quantidade
            {
                get;
                set;
            }
            double ValorDespesa
            {
                get;
                set;
            }
        }

        public interface iOrcamentoObjs : Interfaces.Primitivas.iObjetoTabela
        {
            int IdOrcamento_key
            {
                get;
                set;
            }
            string IdObj_key
            {
                get;
                set;
            }
            DateTime DataLocacaoInicio_key
            {
                get;
                set;
            }
            DateTime DataLocacaoFinal
            {
                get;
                set;
            }
            string Descricao
            {
                get;
                set;
            }
            double Quantidade
            {
                get;
                set;
            }
            double ValorUnitario
            {
                get;
                set;
            }
            double ValorLocacao
            {
                get;
                set;
            }
            iCtrObj CtrObj
            {
                get;
                set;
            }
        }

        public interface iOrcamentoParcelas : Interfaces.Primitivas.iObjetoTabela
        {
            int IdOrcamento_key
            {
                get;
                set;
            }
            int NumeroParcela_key
            {
                get;
                set;
            }
            DateTime DataVencimento
            {
                get;
                set;
            }
            double ValorParcela
            {
                get;
                set;
            }
        }

        public interface iOrdemServico : Interfaces.Primitivas.iObjetoTabela
        {
            int IdOrcamento
            {
                get;
                set;
            }
            DateTime DataEmissao
            {
                get;
                set;
            }
            string Descricao
            {
                get;
                set;
            }
            int IdUsuario
            {
                get;
                set;
            }
            DateTime DataAlteracao
            {
                get;
                set;
            }
        }

        public interface iOrdemServicoObjs : Interfaces.Primitivas.iObjetoTabela
        {
            int IdOS
            {
                get;
                set;
            }
            string IdObj
            {
                get;
                set;
            }
            DateTime DataDevolucao
            {
                get;
                set;
            }
            int Quantidade
            {
                get;
                set;
            }
            iCtrObj CtrObj
            {
                get;
                set;
            }
        }

        public interface iStatusReal : Interfaces.Primitivas.iObjetoTabela
        {

            int IdItem
            {
                get;
                set;
            }
            int IdKit
            {
                get;
                set;
            }
            string IdObj
            {
                get;
                set;
            }
            int IdEntidade
            {
                get;
                set;
            }
            string CodBarras
            {
                get;
                set;
            }
            DateTime DataDevolucao
            {
                get;
                set;
            }
            float Status
            {
                get;
                set;
            }
        }

        public interface iStatusHistorico : Interfaces.Primitivas.iObjetoTabela
        {

            int IdItem
            {
                get;
                set;
            }
            int IdKit
            {
                get;
                set;
            }
            string IdObj
            {
                get;
                set;
            }
            int IdEntidade
            {
                get;
                set;
            }
            DateTime Dia
            {
                get;
                set;
            }
            string CodBarras
            {
                get;
                set;
            }
            float Status
            {
                get;
                set;
            }
        }

        public interface iStatusPresumido : Interfaces.Primitivas.iObjetoTabela
        {

            int IdItem
            {
                get;
                set;
            }
            int IdKit
            {
                get;
                set;
            }
            int idOrcObj
            {
                get;
                set;
            }
            string IdObj_Key
            {
                get;
                set;
            }
            int IdOrcamento_Key
            {
                get;
                set;
            }
            DateTime Dia_Key
            {
                get;
                set;
            }
            int Quantidade
            {
                get;
                set;
            }
            float Status
            {
                get;
                set;
            }
        }

        public interface iLinkItemNo : Interfaces.Primitivas.iObjetoTabela
        {
            int idNo
            {
                get;
                set;
            }
            int idItem
            {
                get;
                set;
            }
        }

        public interface iHistoricoItem : Interfaces.Primitivas.iObjetoTabela
        {
            int idFix
            {
                get;
                set;
            }
            int idItem_key
            {
                get;
                set;
            }
            int idSituacao_key
            {
                get;
                set;
            }
            DateTime Data_key
            {
                get;
                set;
            }
            DateTime Hora
            {
                get;
                set;
            }
            int idUsuario
            {
                get;
                set;
            }
            string IP
            {
                get;
                set;
            }
        }

        public interface iContasPagarReceber : Interfaces.Primitivas.iObjetoTabela
        {
            int TipoConta
            {
                get;
                set;
            }
            string Descricao
            {
                get;
                set;
            }
            DateTime Vencimento
            {
                get;
                set;
            }
            double ValorTotal
            {
                get;
                set;
            }
            int Parcelas
            {
                get;
                set;
            }
            string AnotacoesAdicionais
            {
                get;
                set;
            }
        }

        public interface iContasPagarReceber_Parcelas : Interfaces.Primitivas.iObjetoTabela
        {
            int idConta
            {
                get;
                set;
            }
            DateTime Data
            {
                get;
                set;
            }
            double Valor
            {
                get;
                set;
            }
        }

        public interface iCtrStatus
        {
            void SalvarPresumido();
            void SalvarReal();
        }

        public interface iCtrObj
        {
            string IdObj
            {
                get;
            }
            void SalvarPresumido(bool noCommit, int idorcamento, int idorcobj);
        }

        public interface iCustomROrc : Interfaces.Primitivas.iObjetoTabela
        {
            int IdOrcamento
            {
                get;
                set;
            }
            string PathImgCab
            {
                get;
                set;
            }
            string TxtApresentacao
            {
                get;
                set;
            }
        }

        public interface iCustomROrc_Grupos : Interfaces.Primitivas.iObjetoTabela
        {
            int IdCustomRO
            {
                get;
                set;
            }
            string Titulo
            {
                get;
                set;
            }
            string SubTitulo
            {
                get;
                set;
            }
        }

        public interface iCustomROrc_ItensGrupo : System.IDisposable
        {
            int IdCustomRO_Grupos
            {
                get;
                set;
            }
            string IdObj
            {
                get;
                set;
            }
            int Quantidade
            {
                get;
                set;
            }
            string Descricao
            {
                get;
                set;
            }
            string Marca
            {
                get;
                set;
            }
            string Modelo
            {
                get;
                set;
            }
            void Salvar(bool nocommit);
            void Salvar();
        }

    }
}
