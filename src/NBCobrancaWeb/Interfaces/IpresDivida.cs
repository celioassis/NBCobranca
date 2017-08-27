using System;
using System.Collections.Generic;
using System.Text;

namespace NBCobranca.Interfaces
{
    /// <summary>
    /// Interface para cadastro de d�vida, cont�m s� os dados que est�o na Tela.
    /// </summary>
    public interface IpresDivida
    {
        int TipoDivida { get;set;}
        string Contrato { get;set;}
        int NumeroDocumento { get;set;}
        DateTime DataVencimento { get;set;}
        double ValorNominal { get;set;}
        string DescricaoTipoDivida { get;}
        void SetDataSourceTipoDivida(System.Data.DataTable pDataSource);
    }
}
