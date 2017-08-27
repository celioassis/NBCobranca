using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using NBCobranca.Classes;

namespace NBCobranca.aspx.relatorios
{
	/// <summary>
	/// Summary description for relatorio.
	/// </summary>
	public partial class rel_devedores : Page
	{
	
		private DataGridItem aDTICabProxPage = new DataGridItem(0,0,ListItemType.Header);
		private Sistema Sistema;
		private LimEntidades.relatorio obj;
		protected void Page_Load(object sender, EventArgs e)
		{
			NBFuncoes.ValidarSistema(this,ref Sistema);
			obj = Sistema.LimEntidades.Relatorio;

            DataGrid mDataGrid = obj.AgruparDividas ? dgDadosAgrupados : dgDados;

            mDataGrid.Visible = true;
			mDataGrid.DataSource = obj.DataSourceDevedores;
			mDataGrid.DataBind();
			
			lblPeriodo.Text = "<br>Data de Emissão: " + DateTime.Now.ToString("dd/MM/yyyy") + "<br>";
			
			if (obj.FiltroCarteira != "Todas")
			{
				lblPeriodo.Text += "Carteira: " + obj.FiltroCarteira + "<br>";
                mDataGrid.Columns[0].Visible = false;
			}
			
			lblPeriodo.Text += "<br>";
		}

		protected void dgDados_ItemDataBound(object sender, DataGridItemEventArgs e)
		{

			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView mDRW =(DataRowView)e.Item.DataItem;
				if(obj.FiltroCarteira == "Todas")
					e.Item.Cells[0].Text = obj.GetNomeCarteira(mDRW["XmPathCliente"].ToString());
			}
			if (e.Item.ItemType == ListItemType.Footer)
			{
				TableCell mTabCell1 = new TableCell();
				mTabCell1.BorderStyle = BorderStyle.None;
				TableCell mTabCell2 = new TableCell();
				mTabCell2.BorderStyle = BorderStyle.None;
				TableCell mTabCell3 = new TableCell();
				mTabCell3.BorderStyle = BorderStyle.None;
				
				if(obj.FiltroCarteira == "Todas")
					mTabCell1.ColumnSpan = 3;
				else
					mTabCell1.ColumnSpan = 2;

				mTabCell2.ColumnSpan = 3;
				mTabCell2.Text = "Valor Nominal Total Geral:&nbsp;";
				mTabCell2.HorizontalAlign = HorizontalAlign.Right;
				
				mTabCell3.Text = obj.ValorTotalNominal.ToString("N");
				mTabCell3.HorizontalAlign = HorizontalAlign.Right;


                e.Item.Cells.Clear();

                if (!obj.AgruparDividas)
                {
                    if (mTabCell1.ColumnSpan == 2)
                        e.Item.Cells.Add(new TableCell());
                    e.Item.Cells.Add(mTabCell1);
                    e.Item.Cells.Add(mTabCell2);
                    e.Item.Cells.Add(mTabCell3);
                }
                else
                {
                    if (obj.FiltroCarteira == "Todas")
                        mTabCell2.ColumnSpan = 2;
                    else
                    {
                        e.Item.Cells.Add(new TableCell());
                        mTabCell2.ColumnSpan = 0;
                    }

                    e.Item.Cells.Add(mTabCell2);
                    e.Item.Cells.Add(mTabCell3);
                }

			}
		
		}
	}
}
