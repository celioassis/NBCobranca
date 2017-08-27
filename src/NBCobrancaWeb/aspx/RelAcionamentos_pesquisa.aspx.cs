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

namespace NBCobranca.aspx
{
	/// <summary>
	/// Pesquisa para o Relatório de Acionamentos.
	/// </summary>
	public partial class RelAcionamentosPesquisa_pesquisa : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.TextBox txtNumBordero_INT;
	
		Sistema Sistema;
		protected System.Web.UI.WebControls.RadioButtonList rblOutros;
		LimAcionamentos Obj;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			NBFuncoes.ValidarSistema(this,ref this.Sistema);
			
			this.Obj = this.Sistema.LimAcionamentos;
			this.Sistema.Legenda.Titulo = "Relatório de Acionamentos";

			if(!this.IsPostBack)
			{
				this.Sistema.ValidaCredencial();

				System.Web.UI.WebControls.ListItem mLI = new ListItem("Todas");
				System.Web.UI.WebControls.ListItem mLiTA = new ListItem("Todos","0");
				mLI.Selected = true;
				mLiTA.Selected = true;
				
				this.ddlCarteiras.DataSource = Obj.Carteiras;
				this.ddlCarteiras.DataBind();
				this.ddlCarteiras.Items.Insert(0,mLI);
				
				this.ddlTipoAcionamento.DataSource = Obj.TiposAcionamento;
				this.ddlTipoAcionamento.DataBind();
				this.ddlTipoAcionamento.Items.Insert(0,mLiTA);

				this.ddlAcionadores.DataSource = Obj.relatorio.DataSourceUsuarios(true);
				this.ddlAcionadores.DataBind();
				this.ddlAcionadores.Items.Insert(0,mLiTA);
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
			this.imgBtnPesquisar.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnPesquisar_Click);
			this.dgDados.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgDados_PageIndexChanged);
			this.dgDados.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgDados_ItemDataBound);

		}
		#endregion

		private void imgBtnPesquisar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			DateTime mDataInicial = new DateTime(), mDataFinal = new DateTime();
			string mCarteira;
			int mTipoAcionamento, mIdUsuario;

			mCarteira = this.ddlCarteiras.SelectedValue;
			mIdUsuario = int.Parse(this.ddlAcionadores.SelectedValue);
			mTipoAcionamento = int.Parse(this.ddlTipoAcionamento.SelectedValue);
			this.Obj.relatorio.Acionador = ddlAcionadores.SelectedItem.Text;
			if(this.txtDataInicial_DATA.Text != "")
				mDataInicial = DateTime.Parse(txtDataInicial_DATA.Text);
			
			if(this.txtDataFinal_DATA.Text != "")
				mDataFinal = DateTime.Parse(txtDataFinal_DATA.Text);
			else if(this.txtDataInicial_DATA.Text != "")
			{
				mDataFinal = mDataInicial;
				this.txtDataFinal_DATA.Text = txtDataInicial_DATA.Text;
			}
			this.Obj.relatorio.CriarFiltros(mCarteira,mTipoAcionamento,mIdUsuario,mDataInicial,mDataFinal);
			if(ddlTipoAcionamento.SelectedValue == "2")
				dgDados.Columns[3].Visible=true;
			else
				dgDados.Columns[3].Visible=false;
			this.dgDados.CurrentPageIndex = 0;
			this.Obj.relatorio.BuscaTotalFichasAcionadas();
			this.dgDados.DataSource = this.Obj.relatorio.DataSource;
			this.dgDados.DataBind();
			if(this.Obj.relatorio.TotalFichasAcionadas > 0)
			{
				this.imgBtnPrint.Style["VISIBILITY"]="visible";
				this.dgDados.Visible = true;
			}
			else
			{
				this.imgBtnPrint.Style["VISIBILITY"]="hidden";
				this.dgDados.Visible = false;
			}
		}

		private void dgDados_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
		{
			this.dgDados.CurrentPageIndex = e.NewPageIndex;
            this.AtualizaDataGrid();
		}

		private void dgDados_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Footer)
			{
				TableCell mTabCell = new TableCell();
				int mTotalPaginas = dgDados.PageCount;
				int mPaginaAtual = dgDados.CurrentPageIndex + 1;
				if(ddlTipoAcionamento.SelectedValue == "2")
				    mTabCell.ColumnSpan = 4;
				else
					mTabCell.ColumnSpan = 3;
				mTabCell.Text = "Total de Fichas Acionadas: " + this.Obj.relatorio.TotalFichasAcionadas.ToString();
				mTabCell.Text += "&nbsp;-&nbsp;Página " + mPaginaAtual.ToString() + "/" + mTotalPaginas.ToString();
				e.Item.Cells.Clear();
				e.Item.Cells.Add(mTabCell);
                e.Item.Cells.Add(new TableCell());
			}
		}

        protected void dgDados_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Acionar")
            {
                this.Obj.GetDevedor(int.Parse(e.Item.Cells[0].Text));
                this.MessageBox.Largura = 900;
                this.MessageBox.ModalShow("Acionamento_Ficha.aspx");
                this.AtualizaDataGrid();
            }
            
        }

        private void AtualizaDataGrid()
        {
            this.dgDados.DataSource = this.Obj.relatorio.DataSource;
            this.dgDados.DataBind();
        }

	}
}
