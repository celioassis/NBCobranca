using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace NBCobranca.Interfaces
{
    public interface IpresBaixa
    {
        bool PagouNoCliente { get;set;}
        bool BaixaParcial { get;set;}
        string Cobrador { get;set;}
        double Comissao { get;set;}
        int Bordero { get;set;}
        DateTime DataBaixa { get;set;}
        int Recibo { get;set;}
        double ValorBaixa { get;set;}
        double ValorRecebido { get;set;}
        void SetDataSourceCobrador(System.Data.DataTable pDataSource);
        void SetDataSourceBaixa(System.Collections.ICollection pDataSource);        
    }
}
