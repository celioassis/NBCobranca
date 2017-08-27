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
    /// Summary description for Bordero_pesquisa.
    /// </summary>
    public partial class RelDevedores_pesquisa : System.Web.UI.Page
    {

        Sistema Sistema;
        LimEntidades Obj;
        LimAcionamentos ObjAcionamento;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            NBFuncoes.ValidarSistema(this, ref this.Sistema);

            this.Obj = this.Sistema.LimEntidades;
            this.ObjAcionamento = this.Sistema.LimAcionamentos;

            this.Sistema.Legenda.Titulo = "Pesquisa para Relatório de Devedores";

            if (!this.IsPostBack)
            {
                this.Sistema.ValidaCredencial();

                System.Web.UI.WebControls.ListItem mLI = new ListItem("Todas");
                mLI.Selected = true;

                this.ddlCarteiras.DataSource = this.Sistema.LimAcionamentos.Carteiras;
                this.ddlCarteiras.DataBind();
                this.ddlCarteiras.Items.Insert(0, mLI);

                mLI = new ListItem("Todos", "0");
                mLI.Selected = true;
                this.ddlTiposDivida.DataSource = Sistema.LimAcionamentos.TiposDivida;
                this.ddlTiposDivida.DataBind();
                this.ddlTiposDivida.Items.Insert(0, mLI);

            }
        }

        protected void imgBtnPesquisar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            DateTime? mDataInicial = null;
            DateTime? mDataFinal = null;

            try
            {
                if (txtDataInicial_DATA.Text != "")
                    mDataInicial = Convert.ToDateTime(txtDataInicial_DATA.Text);
                if (txtDataFinal_DATA.Text != "")
                    mDataFinal = Convert.ToDateTime(txtDataFinal_DATA.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("As datas inicial e final informadas são inválidas");
                return;
            }

            this.Obj.Relatorio.CriarFiltros(this.ddlCarteiras.SelectedValue,
                int.Parse(ddlTiposDivida.SelectedValue), chkAgruparDividas.Checked, mDataInicial, mDataFinal);

            dgDados.Visible = false;
            dgDadosAgrupados.Visible = false;
            
            DataGrid mDadosDevedores = this.chkAgruparDividas.Checked ? dgDadosAgrupados : dgDados;
            this.AtualizaDataGrid(0, mDadosDevedores);
        }

        protected void dgDados_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            DataGrid mDataGrid = sender as DataGrid;

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                System.Data.DataRowView mDRW = (System.Data.DataRowView)e.Item.DataItem;
                if (ddlCarteiras.SelectedValue == "Todas")
                    e.Item.Cells[0].Text = Obj.Relatorio.GetNomeCarteira(mDRW["XmPathCliente"].ToString());
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                TableCell mTabCell1 = new TableCell();
                mTabCell1.BorderStyle = BorderStyle.None;
                TableCell mTabCell2 = new TableCell();
                mTabCell2.BorderStyle = BorderStyle.None;
                TableCell mTabCell3 = new TableCell();
                mTabCell3.BorderStyle = BorderStyle.None;
                TableCell mTabCell4 = new TableCell();
                mTabCell4.BorderStyle = BorderStyle.None;

                int mTotalRegistros = mDataGrid.VirtualItemCount;
                int mTotalPaginas = mDataGrid.PageCount;
                int mPaginaAtual = mDataGrid.CurrentPageIndex + 1;

                if (ddlCarteiras.SelectedValue == "Todas")
                    mTabCell1.ColumnSpan = 3;
                else
                    mTabCell1.ColumnSpan = 2;

                mTabCell1.Text = "Total de Registros: " + mTotalRegistros.ToString();
                mTabCell1.Text += "&nbsp;-&nbsp;Página " + mPaginaAtual.ToString() + "/" + mTotalPaginas.ToString();

                mTabCell2.ColumnSpan = 4;
                mTabCell2.Text = "Valor Nominal Total Geral:&nbsp;";
                mTabCell2.HorizontalAlign = HorizontalAlign.Right;

                mTabCell3.Text = Obj.Relatorio.ValorTotalNominal.ToString("N");
                mTabCell3.HorizontalAlign = HorizontalAlign.Right;

                e.Item.Cells.Clear();

                if (!chkAgruparDividas.Checked)
                {
                    if (mTabCell1.ColumnSpan == 2)
                        e.Item.Cells.Add(new TableCell());
                    e.Item.Cells.Add(mTabCell1);
                    e.Item.Cells.Add(mTabCell2);
                    e.Item.Cells.Add(mTabCell3);
                    e.Item.Cells.Add(mTabCell4);
                }
                else
                {
                    mTabCell1.Text = string.Format("<table width='100%' class='dg_header'><tr><td>{0}</td><td width='100px'>{1}</td></tr></table>", mTabCell1.Text, "Valor total geral:");
                    if (ddlCarteiras.SelectedValue == "Todas")
                        mTabCell1.ColumnSpan = 3;
                    else
                    {
                        e.Item.Cells.Add(new TableCell());
                        mTabCell1.ColumnSpan = 2;
                    }

                    e.Item.Cells.Add(mTabCell1);
                    e.Item.Cells.Add(mTabCell3);
                    e.Item.Cells.Add(mTabCell4);
                }
            }
        }

        protected void dgDados_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            this.AtualizaDataGrid(e.NewPageIndex, source as DataGrid);
        }

        protected void dgDados_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Acionar")
            {
                this.ObjAcionamento.GetDevedor(int.Parse(e.Item.Cells[1].Text));
                this.MessageBox.Largura = 900;
                this.MessageBox.ModalShow("Acionamento_Ficha.aspx");
                DataGrid mDadosDevedores = source as DataGrid;
                this.AtualizaDataGrid(mDadosDevedores.CurrentPageIndex, mDadosDevedores);
            }

        }

        private void AtualizaDataGrid(int pPageIndex, DataGrid mDadosDevedores)
        {            

            if (ddlCarteiras.SelectedValue == "Todas")
                mDadosDevedores.Columns[0].Visible = true;
            else
                mDadosDevedores.Columns[0].Visible = false;

            mDadosDevedores.CurrentPageIndex = pPageIndex;

            mDadosDevedores.DataSource = this.Obj.Relatorio.DataSourceDevedores;
            mDadosDevedores.VirtualItemCount = (mDadosDevedores.DataSource as System.Data.DataView).Count;
            mDadosDevedores.DataBind();

            if (mDadosDevedores.VirtualItemCount > 0)
            {
                this.imgBtnPrint.Style["VISIBILITY"] = "visible";
                mDadosDevedores.Visible = true;
            }
            else
            {
                this.imgBtnPrint.Style["VISIBILITY"] = "hidden";
                mDadosDevedores.Visible = false;
            }

        }
    }
}
