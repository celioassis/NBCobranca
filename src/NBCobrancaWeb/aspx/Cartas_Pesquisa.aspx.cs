using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using NBCobranca.Classes;
using NBCobranca.Tipos;

namespace NBCobranca.aspx
{
    public partial class Cartas_Pesquisa : System.Web.UI.Page
    {
        Classes.Sistema Sistema;
        Classes.LimAcionamentos obj;

        protected void Page_Load(object sender, EventArgs e)
        {
            NBFuncoes.ValidarSistema(this, ref this.Sistema);
            obj = Sistema.LimAcionamentos;
            this.Sistema.Legenda.Titulo = "Pesquisa para Envio de Cartas";
            if (!this.IsPostBack)
            {
                this.Sistema.ValidaCredencial(Permissao.Todos);
                this.PreencheDDLs();
            }

        }

        protected void imgBtnPesquisar_Click(object sender, ImageClickEventArgs e)
        {
            this.obj.CriarFiltros(this.ddlCarteiras.SelectedValue,
                "",
                int.Parse(this.ddlTiposDivida.SelectedValue),
                int.Parse(ddlQuantDivida.SelectedValue), null, null);
            this.dgDados.CurrentPageIndex = 0;
            this.AtualizaDataGrid();
            this.obj.CartasRegistradas = false;
            if ((this.dgDados.DataSource as System.Data.DataView).Count > 0)
            {
                this.blqImprimir.Visible = true;
                this.dgDados.Visible = true;
            }
            else
            {
                this.blqImprimir.Visible = false;
                this.dgDados.Visible = false;
            }

        }

        protected void btnRegistrarCartas_Click(object sender, EventArgs e)
        {
            try
            {
                obj.RegistrarCartas(this.ckbSegundoAviso.Checked);
                this.AtualizaDataGrid();
                this.MessageBox.Show("Cartas Registradas com Sucesso !!!");
            }
            catch (Exception ex)
            {
                this.MessageBox.Show(ex.Message);
            }
        }

        protected void dgDados_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            this.dgDados.CurrentPageIndex = e.NewPageIndex;
            this.AtualizaDataGrid();
        }

        protected void dgDados_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                TableCell mTabCell = new TableCell();
                System.Data.DataView mDV = dgDados.DataSource as DataView;
                int mTotalPaginas = dgDados.PageCount;
                int mPaginaAtual = dgDados.CurrentPageIndex + 1;
                mTabCell.ColumnSpan = 6;
                mTabCell.Text = "Total de Cartas: " + mDV.Count.ToString();
                mTabCell.Text += "&nbsp;-&nbsp;Página " + mPaginaAtual.ToString() + "/" + mTotalPaginas.ToString();
                e.Item.Cells.Clear();
                e.Item.Cells.Add(mTabCell);
            }            
        }

        protected void dgDados_ItemCommand(object source, DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Acionar")
            {
                this.obj.GetDevedor(int.Parse(e.Item.Cells[0].Text));
                this.MessageBox.Largura = 900;
                this.MessageBox.ModalShow("Acionamento_Ficha.aspx");
                this.AtualizaDataGrid();
            }

        }

        private void PreencheDDLs()
        {
            System.Web.UI.WebControls.ListItem mLI = new ListItem("Todas");
            mLI.Selected = true;
            this.ddlCarteiras.DataSource = obj.Carteiras;
            this.ddlCarteiras.DataBind();
            this.ddlCarteiras.Items.Insert(0, mLI);
            mLI = new ListItem("Todos", "0");
            mLI.Selected = true;
            this.ddlTiposDivida.DataSource = obj.TiposDivida;
            this.ddlTiposDivida.DataBind();
            this.ddlTiposDivida.Items.Insert(0, mLI);
        }

        private void AtualizaDataGrid()
        {
            this.dgDados.DataSource = this.obj.DataSourceCartas;
            this.dgDados.DataBind();
        }

    }
}
