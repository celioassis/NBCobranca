using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using NBCobranca.Classes;
using NBCobranca.Tipos;
using Neobridge.NBUtil;

namespace NBCobranca.view
{
    /// <summary>
    /// Login do Sistema
    /// </summary>
    public partial class Login : System.Web.UI.Page
    {
        private Controllers.CtrFactory aController;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            Anthem.Manager.Register(this);

            if (!IsPostBack)
            {
                if (Session["Controller"] != null)
                    ((Controllers.CtrFactory)Session["Controller"]).Dispose();
                Session.Clear();
            }
            else
                this.aController = (Controllers.CtrFactory)Session["Controller"];
        }

        protected void btnEntrar_Click(object sender, EventArgs e)
        {
            if (Session["Controller"] == null)
            {
                this.aController = new Controllers.CtrFactory();
                Session["Controller"] = this.aController;
            }
            if (Session["Sistema"] == null)
            {
                Session["Sistema"] = aController.Sistema;
            }
            
            try
            {
                this.aController.ValidarLogin(txtUsuario.Text, txtSenha.Text);
                Response.Redirect("Default.aspx", false);
            }
            catch (Exception ex)
            {
                Anthem.Manager.AddScriptForClientSideEval(BootStrapDialog.Mensagem("Erro de login", ex.Message, BootStrapDialog.TypeMessage.TYPE_DANGER));
            }

        }
    }
}
