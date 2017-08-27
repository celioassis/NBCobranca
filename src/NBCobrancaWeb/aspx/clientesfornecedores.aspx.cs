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
	/// Summary description for clientes.
	/// </summary>
	public partial class clientesfornecedores : System.Web.UI.Page
	{
		
		private Classes.Sistema Sistema;
		private Classes.LimEntidades obj;
		public string SubTitulo = "";
		protected void Page_Load(object sender, System.EventArgs e)
		{
			NBFuncoes.ValidarSistema(this,ref Sistema);
			obj = Sistema.LimEntidades;
			if (!IsPostBack || Session["Sistema"]==null)
			{
				this.Sistema.ValidaCredencial();
				if (Request.QueryString["TipoEntidade"]!=null)
				{
					NBdbm.tipos.TipoEntidade mTipoEntidade;
					System.Text.StringBuilder Titulo;

					mTipoEntidade = (NBdbm.tipos.TipoEntidade)int.Parse(Request.QueryString["TipoEntidade"].ToString());
					Sistema.TipoEntidade = mTipoEntidade;
					Titulo = new System.Text.StringBuilder(mTipoEntidade.ToString());
					Sistema.Legenda.Titulo = Titulo.ToString();
					if (mTipoEntidade == NBdbm.tipos.TipoEntidade.Clientes)
					{
						Titulo.Remove(Titulo.Length-1,1);
					}
					else
					{
						Titulo.Remove(Titulo.Length-2,2);
					}
					Sistema.Legenda.SubTitulo = Titulo.ToString();
				}

			}
			SubTitulo = Sistema.Legenda.SubTitulo;

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
			this.MessageBox.NoChoosed += new NBWebControls.NoChoosedEventHandler(this.MessageBox_YesChoosed);
			this.imgBtnAdicionar.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnAdicionar_Click);
			this.imgBtnPesquisar.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnPesquisar_Click);
			this.dgDados.ItemCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dgDados_ItemCommand);

		}
		#endregion

		private void imgBtnPesquisar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			obj.TipoPesqAtual = (Tipos.TipoPesquisa)int.Parse(selProcurarCampo.Value);
			obj.TipoPessoaAtual = (Tipos.TipoPessoa)int.Parse(selProcurarPessoa.Value);
			obj.ValorPesquisa = txtProcurar.Value;
			dgDados.DataSource = obj.DataSource();			
			dgDados.DataBind();
		
		}

		private void dgDados_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
		{
			switch (e.CommandName)
			{
				case "Edit":
					this.obj.Consulta(e.Item.Cells[0].Text);
					MessageBox.ModalShow("entidades_cadastro.aspx");
					break;
				case "Delete":
					obj.IdExcluir = double.Parse(e.Item.Cells[0].Text);
					MessageBox.ShowConfirma("Confirma a Exclusão do "+ this.SubTitulo + " - " + e.Item.Cells[1].Text,"excluir",true,false);
					break;
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
			this.obj.NovaEntidade(false);
			this.MessageBox.ModalShow("entidades_cadastro.aspx");
		}

	}
}
