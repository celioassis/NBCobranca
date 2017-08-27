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

namespace NBCobranca.aspx
{
	/// <summary>
	/// Cadastro de Clientes e Fornecedores
	/// </summary>
	public partial class TipoDividas_cadastro : System.Web.UI.Page
	{

		private Classes.LimTipoDivida obj;
		private Classes.Sistema Sistema;
		public string TituloCadastro = "Cadastro de Tipos de Dívidas";

		protected void Page_Load(object sender, System.EventArgs e)
		{
			NBFuncoes.ValidarSistema(this,ref Sistema);
			obj = Sistema.LimTipoDivida;
			if(!this.IsPostBack)
			{
				this.Sistema.ValidaCredencial(Permissao.Padrao);
				if(obj.TipoDivida.ID>0)
					txtDescricao.Text = obj.TipoDivida.Descricao;
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
			this.MessageBox.NoChoosed += new NBWebControls.NoChoosedEventHandler(this.MessageBox_NoChoosed);
			this.imgBtnSalvar.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnSalvar_Click);

		}
		#endregion

		//Metodos da MessageBox
		private void MessageBox_YesChoosed(object sender, string Key)
		{
			switch(Key)
			{
				case "Novo":
					this.Page.Response.Redirect("TipoDivida_Cadastro.aspx");
					break;
			}
		
		}
		
		private void MessageBox_NoChoosed(object sender, string Key)
		{
			switch(Key)
			{
				case "Novo":
					this.MessageBox.ModalClose();
					break;
			}
		}

		//Metodo de Gravação
		private void imgBtnSalvar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				this.obj.TipoDivida.Descricao = this.txtDescricao.Text;
				this.obj.Salvar();
				MessageBox.ShowConfirma("Tipo de Dívida Salvo com Sucesso, Deseja Incluir uma nova Dívida?","Novo",true,true);
			}
			catch(NBdbm.COBR_Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}		
	}
}
