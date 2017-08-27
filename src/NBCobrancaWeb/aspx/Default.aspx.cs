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
using NBCobranca.Classes;
using NBCobranca.Tipos;

namespace NBCobranca.aspx
{
    public partial class _Default : System.Web.UI.Page
    {
        private Sistema Sistema;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            NBFuncoes.ValidarSistema(this, ref Sistema);
            this.Sistema.ValidaCredencial(Permissao.Todos);
            Sistema.Legenda.Titulo = "Menu Principal";
            lblUser.Text = Sistema.LimLogin.NomeCompletoUsuario;
            divFundo.Style["BACKGROUND"] = divFundo.Style["BACKGROUND"].Replace("logo_lugphil_fundo.gif", "Logo_Neobridge.gif");
            this.lblComprimento.Text = this.Hoje;
        }
        private string Hoje
        {
            get
            {
                string hoje = "Hoje é " + DateTime.Today.ToLongDateString();
                return hoje;
            }
        }
    }
}
