using System.Data;
using System.Collections;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System;


namespace NBdbm.Interfaces.iCOBR
{


    public interface iCadastroAcionamentos : System.IDisposable
    {
        void Salvar(bool NoCommit);
        void Salvar();
        iCOBR.Primitivas.iAcionamentos Acionamento
        {
            get;
            set;
        }
        NBdbm.Fachadas.NbCollection ColecaoAcionamentos
        {
            get;
        }
        NBdbm.Fachadas.NbCollection ColecaoNovosAcionamentos
        {
            get;
        }
        NBdbm.Fachadas.plxCOBR.CadastroEntidade FichaDevedor
        {
            get;
        }
    }
    public interface iLacamentoBaixa : System.IDisposable
    {
        void Salvar(bool NoCommit);
        void Salvar();
        iCOBR.Primitivas.iBaixas Baixa
        {
            get;
            set;
        }
    }
    namespace Primitivas
    {
        public interface iTipoDivida : Interfaces.Primitivas.iObjetoTabela
        {
            string Descricao
            {
                get;
                set;
            }
        }
        public interface iTipoAcionamento : Interfaces.Primitivas.iObjetoTabela
        {
            string Descricao
            {
                get;
                set;
            }
            int DiasReacionamento
            {
                get;
                set;
            }
            int CredencialExigida
            {
                get;
                set;
            }
        }
        public interface iDivida : Interfaces.Primitivas.iObjetoTabela
        {
            int idEntidade
            {
                get;
                set;
            }
            int idTipoDivida
            {
                get;
                set;
            }
            string Contrato
            {
                get;
                set;
            }
            int NumDoc
            {
                get;
                set;
            }
            DateTime DataVencimento
            {
                get;
                set;
            }
            double ValorNominal
            {
                get;
                set;
            }
            double ValorNominalParcial
            {
                get;
                set;
            }
            DateTime DataBaixa
            {
                get;
                set;
            }
            int BorderoBaixa
            {
                get;
                set;
            }
            int NumRecibo
            {
                get;
                set;
            }
            int idCobrador
            {
                get;
                set;
            }
            double PerCobrador
            {
                get;
                set;
            }
            int idUsuarioBaixa
            {
                get;
                set;
            }
            bool BaixaNoCliente
            {
                get;
                set;
            }
            bool Baixada
            {
                get;
                set;
            }
            bool BaixaParcial
            {
                get;
                set;
            }
            string XmPathCliente
            {
                get;
                set;
            }
        }
        public interface iAcionamentos : Interfaces.Primitivas.iObjetoTabela
        {
            int idEntidade
            {
                get;
                set;
            }
            int idUsuario
            {
                get;
                set;
            }
            int idTipoAcionamento
            {
                get;
                set;
            }
            string TextoRespeito
            {
                get;
                set;
            }
            System.DateTime DataAcionamento
            {
                get;
                set;
            }
            DateTime DataPromessa
            {
                get;
                set;
            }
        }
        public interface iTarifas : Interfaces.Primitivas.iObjetoTabela
        {
            int idEntidade
            {
                get;
                set;
            }
            double Juros
            {
                get;
                set;
            }
            double Multa
            {
                get;
                set;
            }
        }
        public interface iBaixas : Interfaces.Primitivas.iObjetoTabela
        {
            int idEntidade
            {
                get;
                set;
            }
            int idDivida
            {
                get;
                set;
            }
            bool BaixadoCliente
            {
                get;
                set;
            }
            int NumBordero
            {
                get;
                set;
            }
            DateTime DataBaixa
            {
                get;
                set;
            }
            double ValorNominal
            {
                get;
                set;
            }
            double ValorBaixa
            {
                get;
                set;
            }
            double ValorRecebido
            {
                get;
                set;
            }
        }
    }
}
