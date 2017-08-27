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
    /// WebForm para Lançamento de Baixa de Dívidas
    /// </summary>
    public partial class Baixa : FrmBase
    {
        public string Titulo = "";
        private Controllers.ctrBaixas aCtrBaixas;
        private GridView gvDividas;
        private GridView gvAcionamentos;

        protected override void OnInit(EventArgs e)
        {
            this.aPaginaAbertaEmJanelaModal = true;
            base.OnInit(e);
            if (MessageBox.FechandoModal)
                return;
            this.aCtrBaixas = this.aController.ctrBaixas;
            this.aCtrBaixas.OnDividaSelecionadaParaBaixar += new NBCobranca.Controllers.ctrBaixas.DividaSelecionadaParaBaixarHandler(aCtrBaixas_OnDividaSelecionadaParaBaixar);
        }

        protected override void OnUnload(EventArgs e)
        {
            if (this.aCtrBaixas != null)
                this.aCtrBaixas.OnDividaSelecionadaParaBaixar -= new NBCobranca.Controllers.ctrBaixas.DividaSelecionadaParaBaixarHandler(aCtrBaixas_OnDividaSelecionadaParaBaixar);
            base.OnUnload(e);
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {

            this.gvAcionamentos = (GridView)this.SnapAcionamentos.FindControl("gvAcionamentos");
            this.gvDividas = gvSnapDividas;

            if (this.MessageBox.FechandoModal)
                return;

            if (!this.IsPostBack)
            {
                try
                {
                    this.aCtrBaixas.ValidaCredencial(NBCobranca.Tipos.Permissao.Administrador);
                    this.PrimeiraInicializacao();
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
        }

        protected void btnBaixar_Click(object sender, System.EventArgs e)
        {
            if (this.MessageBox.FechandoModal)
                return;

            try
            {
                this.aCtrBaixas.BaixarDividasSelecionadas(this.ckbPagouNoCliente.Checked, this.ckbBaixaParcial.Checked,
                    ddlCobrador.SelectedValue, txtPerComissao_MOEDA.Text, txtBordero_INT.Text,
                    txtDataBaixa_DATA.Text, txtNumRecibo_INT.Text, txtValorBaixa_MOEDA.Text,
                    txtValorRecebido_MOEDA.Text);

                this.MessageBox.Show("Baixa realizada com sucesso", true);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        protected void gvSnapDividas_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (this.MessageBox.FechandoModal)
                return;

            if (e.Row.RowType == DataControlRowType.Footer)
            {
                for (int i = 4; i >= 1; i--)
                    e.Row.Cells.RemoveAt(i);
                e.Row.Cells[1].ColumnSpan = 5;
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[3].HorizontalAlign = HorizontalAlign.Right;
                e.Row.Cells[1].Text = "Soma Total da Dívida:&nbsp;";
                e.Row.Cells[2].Text = this.aCtrBaixas.TotalValorNominalDivida.ToString("N");
                e.Row.Cells[3].Text = this.aCtrBaixas.TotalValorCorrigidoDivida.ToString("N");
            }
        }

        protected void ckbHeader_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.MessageBox.FechandoModal)
                return;

            CheckBox mckbHeader = (CheckBox)sender;

            try
            {
                if (mckbHeader.Checked)
                    this.aCtrBaixas.SelecionarDividaParaBaixar(TipoSelecaoDividas.MarcarTodas);
                else
                    this.aCtrBaixas.SelecionarDividaParaBaixar(TipoSelecaoDividas.DesmarcarTodas);

                foreach (GridViewRow mDGI in gvDividas.Rows)
                {
                    CheckBox mCkbItem = (CheckBox)mDGI.Cells[0].FindControl("ckbItem");
                    mCkbItem.Checked = mckbHeader.Checked;
                }

            }
            catch (Exception ex)
            {
                mckbHeader.Checked = !mckbHeader.Checked;
                this.MessageBox.Show(ex.Message);
            }

        }

        protected void ckbItem_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.MessageBox.FechandoModal)
                return;

            CheckBox mckbItem = (CheckBox)sender;
            try
            {
                int mIndiceDivida = ((GridViewRow)mckbItem.Parent.Parent).RowIndex;

                if (mckbItem.Checked)
                    this.aCtrBaixas.SelecionarDividaParaBaixar(mIndiceDivida, TipoSelecaoDividas.MarcarUma);
                else
                    this.aCtrBaixas.SelecionarDividaParaBaixar(mIndiceDivida, TipoSelecaoDividas.DesmarcarUma);
            }
            catch (Exception ex)
            {
                mckbItem.Checked = !mckbItem.Checked;
                this.MessageBox.Show(ex.Message);
            }
        }

        protected void ckbBaixaParcial_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.MessageBox.FechandoModal)
                return;

            if (this.ckbBaixaParcial.Checked)
            {
                this.txtValorBaixa_MOEDA.ReadOnly = false;
                this.MessageBox.MoverFoco(txtValorBaixa_MOEDA.ClientID);
            }
            else
                this.txtValorBaixa_MOEDA.ReadOnly = true;
        }

        protected void ckbPagouNoCliente_CheckedChanged(object sender, EventArgs e)
        {
            if (this.MessageBox.FechandoModal)
                return;

            this.ddlCobrador.Enabled = !this.ckbPagouNoCliente.Checked;
            this.txtPerComissao_MOEDA.Enabled = !this.ckbPagouNoCliente.Checked;
            this.txtValorRecebido_MOEDA.Enabled = !this.ckbPagouNoCliente.Checked;
            this.txtNumRecibo_INT.Enabled = !this.ckbPagouNoCliente.Checked;
            if (this.ckbPagouNoCliente.Checked)
                this.txtValorRecebido_MOEDA.Text = txtValorBaixa_MOEDA.Text;
        }

        protected void txtValorBaixa_MOEDA_TextChanged(object sender, EventArgs e)
        {
            if (this.MessageBox.FechandoModal)
                return;

            if (this.ckbPagouNoCliente.Checked && this.ckbBaixaParcial.Checked)
                this.txtValorRecebido_MOEDA.Text = txtValorBaixa_MOEDA.Text;
            else if (ckbBaixaParcial.Checked)
                this.txtValorRecebido_MOEDA.Text = this.aCtrBaixas.ReCalcularJuros(Convert.ToDouble(txtValorBaixa_MOEDA.Text)).ToString("N2");

            this.txtValorRecebido_MOEDA.Focus();
        }

        private void PrimeiraInicializacao()
        {
            if (this.aCtrBaixas.BaixandoDividaUnica)
            {
                this.Titulo = "- Unica";
                this.ckbPagouNoCliente.Enabled = true;
                this.ckbBaixaParcial.Enabled = true;
                txtValorRecebido_MOEDA.Text = this.aCtrBaixas.TotalValorCorrigidoDivida.ToString("N2");
                txtValorBaixa_MOEDA.Text = this.aCtrBaixas.TotalValorNominalDivida.ToString("N2");
                this.btnBaixar.Enabled = true;
            }
            else
                this.Titulo = "- Completa";

            gvDividas.DataSource = this.aCtrBaixas.Dividas;
            gvDividas.DataBind();

            if (this.aCtrBaixas.Dividas.Count == 1)
            {
                if (!aCtrBaixas.BaixandoDividaUnica)
                    this.aCtrBaixas.SelecionarDividaParaBaixar(TipoSelecaoDividas.MarcarTodas);

                CheckBox mCkbItem = (CheckBox)gvDividas.Rows[0].Cells[0].FindControl("ckbItem");
                CheckBox mCkbHeader = (CheckBox)gvDividas.HeaderRow.FindControl("ckbHeader");
                mCkbHeader.Enabled = false;
                mCkbHeader.Checked = true;
                mCkbItem.Checked = true;
                mCkbItem.Enabled = false;

            }

            gvAcionamentos.DataSource = this.aCtrBaixas.Acionamentos;
            gvAcionamentos.DataBind();

            ddlCobrador.DataSource = this.aCtrBaixas.Colaboradores;
            ddlCobrador.DataBind();

            if (this.aCtrBaixas.IdUsuarioUltimoAcionamento > 0)
                ddlCobrador.SelectedValue = this.aCtrBaixas.IdUsuarioUltimoAcionamento.ToString();

            if (this.aCtrBaixas.Devedor.PessoaFJ)
            {
                this.txtNome.Text = this.aCtrBaixas.Devedor.NomePrimary;
                this.txtCPF.Text = this.aCtrBaixas.Devedor.CPFCNPJ;
                this.txtRG.Text = this.aCtrBaixas.Devedor.RGIE;
            }
            else
            {
                this.pnPF.Visible = false;
                this.pnPJ.Visible = true;
                this.txtNomeFantasia.Text = this.aCtrBaixas.Devedor.NomeSecundary;
                this.txtRazaoSocial.Text = this.aCtrBaixas.Devedor.NomePrimary;
                this.txtCNPJ.Text = this.aCtrBaixas.Devedor.CPFCNPJ;
                this.txtInscEstadual.Text = this.aCtrBaixas.Devedor.RGIE;
            }
            txtDataBaixa_DATA.Text = DateTime.Today.ToString("dd/MM/yyyy");

        }

        private void aCtrBaixas_OnDividaSelecionadaParaBaixar(int pTotalDividasSelecionadas, double pTotalValorNominal, double pTotalValorCorrigido)
        {
            CheckBox mckbHeader = (CheckBox)gvDividas.HeaderRow.FindControl("ckbHeader");
            mckbHeader.Checked = this.gvDividas.Rows.Count == pTotalDividasSelecionadas;

            this.ckbPagouNoCliente.Enabled = pTotalDividasSelecionadas > 0;

            this.ckbBaixaParcial.Enabled = pTotalDividasSelecionadas == 1;
            if (this.ckbBaixaParcial.Checked && pTotalDividasSelecionadas > 1)
                this.ckbBaixaParcial.Checked = false;

            txtValorRecebido_MOEDA.Text = pTotalValorCorrigido.ToString("N2");
            txtValorBaixa_MOEDA.Text = pTotalValorNominal.ToString("N2");

            this.btnBaixar.Enabled = this.ckbPagouNoCliente.Enabled;

        }

    }
}
