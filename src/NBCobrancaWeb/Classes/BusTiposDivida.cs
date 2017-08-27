using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace NBCobranca.Classes
{
    public class BusTiposDivida : BusBase
    {
        public BusTiposDivida(Sistema sistema, Neobridge.NBDB.DBDirect dbDirect)
            : base(sistema, dbDirect)
        { }

        /// <summary>
        /// get - Retorna um DataTable com todos os registros de Tipos de dívidas existentes no banco de dados.
        /// </summary>
        public DataTable DataSource
        {
            get
            {
                try
                {
                    return this.DbDirect.CriarDataTable("SELECT * FROM COBR_TipoDivida");
                }
                catch (Exception ex)
                {
                    throw new Exception("Não foi possível carregar os tipos de dívida", ex);
                }
            }
        }
    }
}
