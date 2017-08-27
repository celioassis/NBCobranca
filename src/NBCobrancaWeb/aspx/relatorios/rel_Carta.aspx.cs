using System;
using System.Web.UI;
using NBCobranca.Classes;

namespace NBCobranca.aspx.relatorios
{
	/// <summary>
	/// Summary description for rel_Cartas.
	/// </summary>
	public partial class rel_Carta : Page
	{
		private Sistema Sistema;
		private LimAcionamentos obj;
		protected void Page_Load(object sender, EventArgs e)
		{
			NBFuncoes.ValidarSistema(this, ref Sistema);
			obj = Sistema.LimAcionamentos;
			if(!IsPostBack)
			{
				if(Request.QueryString["2Aviso"].ToUpper() == "FALSE")
					lblNotificacao.Visible=false;
				lblDevedor.Text = Request.QueryString[0];
				lblEndereco.Text = Request.QueryString[1];
				lblBairro.Text = Request.QueryString[2];
				lblCepMunUF.Text = Request.QueryString[3];
			}
			
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		private void btnRegistrar_Click(object sender, EventArgs e)
		{
		}
	}
}
