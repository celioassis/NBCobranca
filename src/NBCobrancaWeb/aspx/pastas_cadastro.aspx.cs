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
	/// Summary description for itens_ep_classes.
	/// </summary>
	public partial class pastas_cadastro : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.ImageButton imgBtnSalvar;
	

		private Sistema Sistema;
		private BusClasses obj;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			NBFuncoes.ValidarSistema(this,ref this.Sistema);
			this.obj = this.Sistema.BusClasses;
            this.obj.TreeView.SelectedNode = this.TreeView1.SelectedNode;
            this.TreeView1 = this.obj.TreeView;
			if (!IsPostBack)
			{
				this.Sistema.ValidaCredencial(Permissao.Padrao);
				Sistema.Legenda.Titulo = "Cadastro de Pastas";
				this.obj.CarregaArvore("Entidades\\Carteiras",true);
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
			this.TreeView1.NodeMoved += new ComponentArt.Web.UI.TreeView.NodeMovedEventHandler(this.TreeView1_NodeMoved);
			this.imgBtnExcluir.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnExcluir_Click);
			this.imgBtnSalvarNoBanco.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnSalvarNoBanco_Click);
			this.imgBtnCancelar.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnCancelar_Click);
			this.imgBtnAdicionar.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnAdicionar_Click);
			this.imgBtnRenomear.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnRenomear_Click);

		}
		#endregion

		private void TreeView1_NodeMoved(object sender, ComponentArt.Web.UI.TreeViewNodeMovedEventArgs e)
		{
			try
			{
				obj.MoverClasse(sender,e);
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void imgBtnSalvarNoBanco_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				obj.SalvarArvoreTreeView();
				MessageBox.ShowConfirma("Estrutura de Classificação Atualizada com Sucesso no Banco de Dados. Deseja fazer mais alguma alteração?","FinalizarEdicao",false,true);
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}

		private void imgBtnExcluir_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				obj.ExcluirClasse();
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message.Replace("'",""));
			}
		}

		private void imgBtnAdicionar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				obj.AdicionarClasse(txtNovaClasse.Value);
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
										
		}

		private void imgBtnRenomear_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				obj.RenomearClasse(txtRenomearClasse.Value);
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

		}

		private void imgBtnCancelar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			if (obj.ArvoreAlterada)
				MessageBox.ShowConfirma("A Estrutura das Pastas foi modificada e ainda não foi salva, Deseja Sair assim mesmo?","Cancelar",true,false);
			else
				this.Response.Redirect("Default.aspx");
		}


		private void MessageBox_YesChoosed(object sender, string Key)
		{
			switch(Key)
			{
				case "Cancelar":
					this.Response.Redirect("Default.aspx");
					break;

			}

		}

		private void MessageBox_NoChoosed(object sender, string Key)
		{
			switch(Key)
			{
				case "FinalizarEdicao":
					this.Response.Redirect("Default.aspx");
					break;
			}
		}
	}
}