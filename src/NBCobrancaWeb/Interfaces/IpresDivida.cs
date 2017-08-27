using System;
using System.Collections.Generic;
using System.Text;

namespace NBCobranca.Interfaces
{
    /// <summary>
    /// Interface para cadastro de dívida, contém só os dados que estão na Tela.
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
