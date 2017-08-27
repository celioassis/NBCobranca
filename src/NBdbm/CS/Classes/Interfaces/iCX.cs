using System.Data;
using System.Collections;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System;

namespace NBdbm.Interfaces.iCX
{
    public interface iCentroCusto : Primitivas.iObjetoTabela
    {
        string centroCusto
        {
            get;
            set;
        }
    }

    public interface iCaixa : Primitivas.iObjetoTabela
    {
        int idCentroCusto
        {
            get;
            set;
        }
        int idEntidade
        {
            get;
            set;
        }
        bool pagarOUreceber
        {
            get;
            set;
        }
        string descricaoConta
        {
            get;
            set;
        }
        int vlrConta
        {
            get;
            set;
        }
        DateTime dtVencimento
        {
            get;
            set;
        }
        DateTime dtPagamento
        {
            get;
            set;
        }
        int acrescimos
        {
            get;
            set;
        }
        int abatimento
        {
            get;
            set;
        }
        int vlrPago
        {
            get;
            set;
        }
    }
}
