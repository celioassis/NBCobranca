using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using NBCobranca.Classes;

namespace NBCobranca.aspx.relatorios
{
	/// <summary>
	/// Summary description for relatorio.
	/// </summary>
	public partial class rel_acionamentos : Page
	{
	
		private Sistema Sistema;
		private LimAcionamentos obj;
		private DataGridItem aDTICabProxPage = new DataGridItem(0,0,ListItemType.Header);

		protected void Page_Load(object sender, EventArgs e)
		{
			NBFuncoes.ValidarSistema(this,ref Sistema);
			obj = Sistema.LimAcionamentos;

			if(obj.relatorio.TipoAcionamento == 2)
				dgDados.Columns[3].Visible = true;
			else
				dgDados.Columns[3].Visible = false;
			lblPeriodo.Text = "<br><b>Carteira:&nbsp;" + obj.relatorio.Carteira + "</b>";
			lblPeriodo.Text += "<br><b>Acionador:&nbsp;" + obj.relatorio.Acionador + "</b>";
			if(obj.relatorio.Periodo != "")
				lblPeriodo.Text += "<br><b>" + obj.relatorio.Periodo + "</b><br>";
			dgDados.DataSource = obj.relatorio.DataSource;
			dgDados.DataBind();
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
			this.dgDados.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgDados_ItemDataBound);

		}
		#endregion

		private void dgDados_ItemDataBound(object sender, DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Footer)
			{
				TableCell mTabCell = new TableCell();
				int mTotalPaginas = dgDados.PageCount;
				int mPaginaAtual = dgDados.CurrentPageIndex + 1;
				if(obj.relatorio.TipoAcionamento == 2)
					mTabCell.ColumnSpan = 4;
				else
					mTabCell.ColumnSpan = 3;
				mTabCell.Text = "Total de Acionamentos: " + obj.relatorio.TotalFichasAcionadas;
				e.Item.Cells.Clear();
				e.Item.Cells.Add(mTabCell);
			}

		}
	}
}
