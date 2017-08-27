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
    /// Summary description for Acionamento_pesquisa.
    /// </summary>
    public partial class Cartas_pesquisa : System.Web.UI.Page
    {
        Classes.Sistema Sistema;
        Classes.LimAcionamentos obj;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            NBFuncoes.ValidarSistema(this, ref this.Sistema);
            obj = Sistema.LimAcionamentos;
            this.Sistema.Legenda.Titulo = "Pesquisa para emissão de Cartas de Aviso";

            if (!this.IsPostBack)
            {
                this.Sistema.ValidaCredencial(Permissao.Todos);
                this.PreencheDDLs();
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
            this.dgDados.PageIndexChanged += new System.Web.UI.WebControls.DataGridPageChangedEventHandler(this.dgDados_PageIndexChanged);
            this.dgDados.ItemDataBound += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dgDados_ItemDataBound);

        }
        #endregion

        private void dgDados_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            this.dgDados.CurrentPageIndex = e.NewPageIndex;
            this.AtualizaDataGrid();
        }
        protected void btnRegistrarCartas_Click(object sender, System.EventArgs e)
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
        private void AtualizaDataGrid()
        {
            this.dgDados.DataSource = this.obj.DataSourceCartas;
            this.dgDados.DataBind();
        }

        private void dgDados_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                TableCell mTabCell = new TableCell();
                System.Data.DataView mDV = dgDados.DataSource as DataView;
                int mTotalPaginas = dgDados.PageCount;
                int mPaginaAtual = dgDados.CurrentPageIndex + 1;
                mTabCell.ColumnSpan = 5;
                mTabCell.Text = "Total de Cartas: " + mDV.Count.ToString();
                mTabCell.Text += "&nbsp;-&nbsp;Página " + mPaginaAtual.ToString() + "/" + mTotalPaginas.ToString();
                e.Item.Cells.Clear();
                e.Item.Cells.Add(mTabCell);
            }

        }

        public void imgBtnPesquisar_Click(object sender, ImageClickEventArgs e)
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
    }
}
