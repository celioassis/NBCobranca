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
using System.Text;

namespace NBCobranca.aspx
{
    /// <summary>
    /// Summary description for itens_ep.
    /// </summary>
    public partial class devedores : FrmBase
    {

        Controllers.ctrDevedores aCtrDevedores;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.aCtrDevedores = this.aController.ctrDevedores;
            this.aCtrDevedores.ArvoreCarteiras = this.TreeView1;

            if (!IsPostBack)
            {
                this.aCtrDevedores.ValidaCredencial(Permissao.Padrao);
            }
            this.aCtrDevedores.AtualizaLegenda();
        }

        protected void TreeView1_NodeSelected(object sender, ComponentArt.Web.UI.TreeViewNodeEventArgs e)
        {
            this.gvDados.PageIndex = 0;
            this.AtualizaDataGrid(0);
        }

        protected void imgBtnPesquisar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            AtualizaDataGrid(0);
        }

        protected void imgBtnNovoDevedor_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (this.aCtrDevedores.CarteiraSelecionada == "<Entidades><Carteiras>")
            {
                this.MessageBox.Show("É Preciso Selecionar uma Carteira para poder adicionar um novo devedor.");
                return;
            }
            this.aCtrDevedores.NovoDevedor();
            this.MessageBox.ModalShow("Cadastro_Entidades.aspx", true);

        }

        protected void gvDados_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                switch (e.CommandName)
                {
                    case "Baixar":
                        this.aController.ctrBaixas.Inicializar();
                        this.aController.ctrBaixas.CodigoDevedor = Convert.ToInt32(gvDados.DataKeys[Convert.ToInt32(e.CommandArgument)].Value);
                        MessageBox.ModalShow("Baixa.aspx");
                        break;
                }

            }
            catch (Exception Ex)
            {
                this.MessageBox.Show(Ex.Message);
            }
        }

        protected void gvDados_RowEditing(object sender, GridViewEditEventArgs e)
        {
            e.Cancel = true;
            string mCodigoEntidade = gvDados.DataKeys[e.NewEditIndex].Value.ToString();
            if (this.aCtrDevedores.CarteiraSelecionada == "<Entidades><Carteiras>")
                MessageBox.ShowConfirma("É preciso Selecionar uma Carteira para que seja possível adicionar novas Dividas para o Devedor, deseja editar o cadastro deste Devedor assim mesmo?", "Edit|" + mCodigoEntidade, true, false);
            else
                EditarFicha(mCodigoEntidade);
        }

        protected void gvDados_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            e.Cancel = true;
            DataKey mValorChaves = gvDados.DataKeys[e.RowIndex];
            string mStrChaves = string.Format("{0}|{1}|{2}", "Delete", mValorChaves[0], mValorChaves[1]);

            MessageBox.ShowConfirma("Confirma a Exclusão do Devedor - " + mValorChaves[1].ToString(), mStrChaves, true, false);
        }

        protected void gvDados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            this.AtualizaDataGrid(e.NewPageIndex);
        }

        protected void txtPage_INT_TextChanged(object sender, EventArgs e)
        {
            TextBox mTxtPage = (TextBox)sender;
            if (mTxtPage.Text == "") mTxtPage.Text = "1";
            int mPaginaEscolhida = Convert.ToInt32(mTxtPage.Text);
            if (mPaginaEscolhida == 0 || mPaginaEscolhida > gvDados.PageCount)
            {
                if (mPaginaEscolhida == 0)
                    mPaginaEscolhida = 1;
                else
                    mPaginaEscolhida = gvDados.PageCount;
            }
            AtualizaDataGrid(mPaginaEscolhida - 1);
        }

        protected void ObjDS_Devedores_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {
            e.ObjectInstance = this.aCtrDevedores;
        }

        protected void MessageBox_YesChoosed(object sender, string Key)
        {
            string[] mKeys = Key.Split('|');
            switch (mKeys[0])
            {
                case "Delete":
                    this.aCtrDevedores.DeletarDevedor(mKeys[1], mKeys[2]);
                    if (this.gvDados.Rows.Count == 1)
                        this.AtualizaDataGrid(this.gvDados.PageIndex - 1);
                    else
                        this.AtualizaDataGrid(this.gvDados.PageIndex);
                    break;
                case "Edit":
                    EditarFicha(mKeys[1]);
                    break;
            }
        }

        private void AtualizaDataGrid(int pCurrentPage)
        {
            this.aCtrDevedores.DefineValoresPesquisa((Tipos.TipoPesquisa)int.Parse(selProcurarCampo.SelectedValue),
                txtProcurar.Value);
            if (pCurrentPage < 0)
                pCurrentPage = 0;
            this.gvDados.PageIndex = pCurrentPage;
            this.gvDados.DataSource = this.ObjDS_Devedores;
            this.gvDados.DataBind();
            if (gvDados.BottomPagerRow != null)
            {
                Button mButton = (Button)gvDados.BottomPagerRow.FindControl("btnAnterior");
                mButton.Enabled = !(gvDados.PageIndex == 0);
                mButton = (Button)gvDados.BottomPagerRow.FindControl("btnProximo");
                mButton.Enabled = !(gvDados.PageIndex + 1 == gvDados.PageCount);
            }
        }

        private void EditarFicha(string pCodigoDevedor)
        {
            this.aCtrDevedores.EditarDevedor(pCodigoDevedor);
            MessageBox.ModalShow("Cadastro_Entidades.aspx", true);
        }

        protected void MessageBox_CloseModalChoosed(object sender, string Key, string pValorRetorno)
        {
            this.imgBtnPesquisar_Click(imgBtnPesquisar, new ImageClickEventArgs(1, 1));
        }
    }
}
