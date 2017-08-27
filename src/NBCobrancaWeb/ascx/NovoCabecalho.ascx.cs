namespace NBCobranca.ascx
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;

    /// <summary>
    ///		Summary description for NovoCabecalho.
    /// </summary>
    public partial class NovoCabecalho : System.Web.UI.UserControl
    {
        private Classes.Sistema Sistema;
        public string Titulo = "Login";
        protected System.Web.UI.WebControls.Panel limitador;
        public string Logo = "logo.jpg";

        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (Session["Sistema"] != null)
            {
                this.Sistema = (Classes.Sistema)Session["Sistema"];
                Logo = this.Sistema.LimLogin.Logo;
                Titulo = this.Sistema.Legenda.Titulo;
            }
        }
    }
}
