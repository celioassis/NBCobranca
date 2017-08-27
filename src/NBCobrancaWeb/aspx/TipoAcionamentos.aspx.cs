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
	public partial class TipoAcionamento: System.Web.UI.Page
	{
		protected System.Web.UI.HtmlControls.HtmlForm frmFuncionarios;

		private Sistema Sistema;
		private LimTipoAcionamentos obj;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			NBFuncoes.ValidarSistema(this,ref this.Sistema);
			obj = Sistema.LimTipoAcionamento;
			if (!IsPostBack || Session["Sistema"]==null)
			{
				this.Sistema.ValidaCredencial(Permissao.Administrador);
				Sistema.Legenda.Titulo = "Tipos de Acionamento";
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

		}
		#endregion

		private void imgBtnPesquisar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			obj.TipoPesquisa = (Tipos.TipoPesquisa)int.Parse(selProcurarCampo.Value);
			obj.StringPesquisa = txtProcurar.Value;
			dgDados.DataSource = obj.DataSource();
			dgDados.DataBind();
		}

		private void dgDados_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			try
			{
				obj.IdTmp = int.Parse(e.Item.Cells[0].Text);
				obj.GetTipoAcionamento();
				this.MessageBox.ModalShow("TipoAcionamentos_cadastro.aspx");
				this.MessageBox.BotaoSubmit = imgBtnPesquisar.ClientID;
			}
			catch(NBdbm.COBR_Exception ExCobr)
			{
				this.MessageBox.Show(ExCobr.Message);
			}
			catch(Exception Ex)
			{
				this.MessageBox.Show(Ex.Message);
			}
			
		}

		private void MessageBox_YesChoosed(object sender, string Key)
		{
			if (Key=="excluir")
			{
				obj.Excluir();
				dgDados.DataSource = obj.DataSource();
				dgDados.DataBind();
			}
		
		}

		private void imgBtnAdicionar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			this.MessageBox.BotaoSubmit=this.imgBtnPesquisar.ClientID;
			this.MessageBox.ModalShow("TipoAcionamentos_cadastro.aspx");
		}


	
	}
}
