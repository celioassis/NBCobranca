namespace NBCobranca.ascx
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for NovoRodape.
	/// </summary>
	public partial class NovoRodape : System.Web.UI.UserControl
	{
		private Classes.Sistema Sistema;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (Session["Sistema"]!=null)
			{
				this.Sistema = (Classes.Sistema)Session["Sistema"];
				hlUsuario.Text = this.Sistema.LimLogin.UsuarioLogado;
			}
			System.Version mVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version;
			lblVersao.Text = "Versão: " + mVersion.ToString();

		}
	}
}
