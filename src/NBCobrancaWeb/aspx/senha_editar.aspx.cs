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
using NBCobranca.Tipos;
using NBCobranca.Classes;

namespace NBCobranca.aspx
{
	/// <summary>
	/// Summary description for senha_edicao.
	/// </summary>
	public partial class senha_edicao : System.Web.UI.Page
	{
	
		private Sistema Sistema;
		private LimLogin obj;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			NBFuncoes.ValidarSistema(this,ref this.Sistema);
			obj = Sistema.LimLogin;
			if (!IsPostBack || Session["Sistema"]==null)
			{
				this.Sistema.ValidaCredencial(Permissao.Todos);
				Sistema.Legenda.Titulo = "Troca de Senha";
				this.lblUsuario.Text = "Usuário: " + Sistema.LimLogin.UsuarioLogado;
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
			this.MessageBox.YesChoosed += new NBWebControls.YesChoosedEventHandler(this.MessageBox_YesChoosed);
			this.imgBtnEntrar.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnEntrar_Click);

		}
		#endregion

		private void imgBtnEntrar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				obj.AlterarSenha(this.txtSenhaAtual_TUD.Text,this.txtSenha_TUD.Text);
				MessageBox.ShowConfirma("Senha Alterada com sucesso, deseja voltar para a tela de login?","Sucesso",true,false);
			}
			catch(NBdbm.EVTexception evtEx)
			{
				MessageBox.Show(evtEx.Message,txtSenhaAtual_TUD.ClientID);
			}
		}

		private void MessageBox_YesChoosed(object sender, string Key)
		{
			Response.Redirect("login.aspx");
		}
	}
}
