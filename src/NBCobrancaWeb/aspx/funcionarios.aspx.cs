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
	/// Summary description for funcionarios.
	/// </summary>
	public partial class funcionarios : System.Web.UI.Page
	{

		private Sistema Sistema;
		private LimFuncionarios obj;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			NBFuncoes.ValidarSistema(this,ref this.Sistema);
			obj = Sistema.LimFuncionarios;
			if (!IsPostBack || Session["Sistema"]==null)
			{
				this.Sistema.ValidaCredencial(Permissao.Administrador);
				Sistema.Legenda.Titulo = "Funcionários";
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
			this.imgBtnAdicionar.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnAdicionar_Click);
			this.imgBtnPesquisar.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnPesquisar_Click);
			this.dgDados.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgDados_ItemCommand);
			this.dgDados.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgDados_ItemDataBound);

		}
		#endregion

		private void imgBtnPesquisar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			obj.TipoPesqAtual = (Tipos.TipoPesquisa)int.Parse(selProcurarCampo.Value);
			dgDados.DataSource = obj.DataSource(txtProcurar.Value);
			dgDados.DataBind();
		}

		private void dgDados_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "Edit":
					MessageBox.ModalShow("funcionarios_cadastro.aspx?idFunc=" + e.Item.Cells[0].Text);
					break;
				case "Delete":
					obj.IdExcluir = int.Parse(e.Item.Cells[0].Text);
					obj.FuncionarioAtivo = Convert.ToBoolean(e.Item.Cells[5].Text);
					if (obj.FuncionarioAtivo)
						MessageBox.ShowConfirma("Confirma a Desativação do Funcionário - " + e.Item.Cells[1].Text,"excluir",true,false);
					else
						MessageBox.ShowConfirma("Confirma a Ativação do Funcionário - " + e.Item.Cells[1].Text,"excluir",true,false);
					break;

			}
		}

		private void MessageBox_YesChoosed(object sender, string Key)
		{
			if (Key=="excluir")
			{
				obj.AtivaDesativa();
				dgDados.DataSource = obj.DataSource(txtProcurar.Value);
				dgDados.DataBind();
			}
		
		}

		private void dgDados_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if (e.Item.Cells[7].Text =="")
			{
				System.Web.UI.WebControls.LinkButton dgLB;
				dgLB = (System.Web.UI.WebControls.LinkButton)e.Item.Cells[7].Controls[0];
				switch(e.Item.Cells[5].Text)
				{
					case "True":
						
						dgLB.Text = "<img src='../imagens/botoes/c_ativado.jpg' alt='Funcionário Ativo - Click para desativar.' border='0'>";
						break;
					case "False":
						dgLB.Text = "<img src='../imagens/botoes/c_desativado.jpg' alt='Funcionário Inativo - Click para ativar.' border='0'>";
						break;
				}
			}
		}

		private void imgBtnAdicionar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
            this.obj.NovoFuncionario();
			this.MessageBox.BotaoSubmit=this.imgBtnPesquisar.ClientID;
			this.MessageBox.ModalShow("funcionarios_cadastro.aspx");
		}


	
	}
}
