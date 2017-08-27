using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace NBCobranca.aspx
{
    public partial class LayoutPagina : System.Web.UI.MasterPage
    {
        private Classes.Sistema Sistema;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (Session["Sistema"] != null)
            {
                this.Sistema = (Classes.Sistema)Session["Sistema"];
                hlUsuario.Text = this.Sistema.LimLogin.UsuarioLogado;
            }
            System.Version mVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
            lblVersao.Text = "Versão: " + mVersion.ToString();
        }
        public string Titulo
        {
            get
            {
                return this.Sistema == null ? "Login" : this.Sistema.Legenda.Titulo;
            }
        }
    }
}
