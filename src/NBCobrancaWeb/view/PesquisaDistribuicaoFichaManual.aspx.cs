using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;
using NBCobranca.Interfaces;
using NBCobranca.Classes;
using NBCobranca.Tipos;
using Neobridge.NBUtil;

namespace NBCobranca.view
{
    public partial class PesquisaDistribuicaoFichaManual : PageBaseComController<Controllers.CtrDistribuicaoFichasManual>, IPresPesquisaDistribuicaoFichasManual
    {

        #region IpresPesquisaDistribuicaoFichasManual Members

        public List<int> ListaEntidadesSelecionadas
        {
            get
            {
                var retorno = new List<int>();

                foreach (GridViewRow gvRow in gvResultado.Rows)
                {
                    var ckbItem = (CheckBox) gvRow.Cells[0].FindControl("ckbItem");
                    if(ckbItem.Checked)
                        retorno.Add(Convert.ToInt32(gvRow.Cells[1].Text));
                }
                return retorno;
            }
        }

        public int AcionadorDeDestino => Convert.ToInt32(ddlAcionadores.SelectedValue);

        public FiltroQuantidadeDeDividas QuantidadeDeDividas => (FiltroQuantidadeDeDividas)Convert.ToInt16(ddlQuantDivida.SelectedValue);

        public int IdTipoDivida => Convert.ToInt32(ddlTipoDividas.SelectedValue);

        public void CarregaListaTiposDeDivida(ICollection pListaTiposDeDivida)
        {
            ddlTipoDividas.DataSource = pListaTiposDeDivida;
            ddlTipoDividas.DataBind();
            ddlTipoDividas.Items.Insert(0, new ListItem("Todas", "0"));
            ddlTipoDividas.SelectedIndex = 0;
        }

        public void CarregaListaDeCobradores(ICollection pListaDeCobradores)
        {
            this.ddlAcionadores.DataSource = pListaDeCobradores;
            this.ddlAcionadores.DataBind();
        }

        public List<int> GetAcionadoresSelecionados
        {
            get { throw new NotImplementedException();}
        }

        public bool SomenteDisponiveis => chkDisponiveis.Checked;

        #endregion

        #region IpresPesquisaDistribuicaoFichas Members

        public void CarregaListaClientes(ICollection pListaClientes)
        {
            this.ddlCarteiras.DataSource = pListaClientes;
            this.ddlCarteiras.DataBind();
            this.ddlCarteiras.Items.Insert(0, new ListItem("Todas", "0"));
            this.ddlCarteiras.SelectedIndex = 0;
        }

        public void CarregaFichasPesquisadas(ICollection pListaFichasPesquisadas)
        {
            gvResultado.Columns[3].Visible = !chkDisponiveis.Checked;
            gvResultado.DataSource = pListaFichasPesquisadas;
            gvResultado.DataBind();
            pnlAcoes.Visible = pListaFichasPesquisadas.Count > 0;
            pnlAcoes.UpdateAfterCallBack = true;
            if (!pnlAcoes.Visible) return;

            // ==== Habilita a paginação da GridView ====
            this.gvResultado.HeaderRow.TableSection = TableRowSection.TableHeader;
            var ckbHeader = gvResultado.HeaderRow.FindControl("CkbHeader");            
            var script = new StringBuilder();
            script.AppendFormat("configuraDataTable('#{0}');",gvResultado.ClientID);
            script.AppendFormat("configuraClickCkbHeader('#{0}','#{1}');", ckbHeader.ClientID, gvResultado.ClientID);
            Anthem.Manager.AddScriptForClientSideEval(script.ToString());
        }

        public string GetCarteiraSelecionada => ddlCarteiras.SelectedValue == "0" ? null : ddlCarteiras.SelectedValue;

        public int GetTipoDeDividaSelecionada => (ddlTipoDividas.SelectedValue == "Todas") ? 0 : Convert.ToInt32(ddlTipoDividas.SelectedValue);

        public DateTime? GetFiltroDataVencimento => (string.IsNullOrEmpty(txtFiltroDataVencimento.Text)) ? new DateTime?() : Convert.ToDateTime(txtFiltroDataVencimento.Text);

        public int? GetFiltroMes => (string.IsNullOrEmpty(txtFiltroMesVencimento.Text)) ? new int?() : Convert.ToInt32(txtFiltroMesVencimento.Text);

        public int? GetFiltroAno => (string.IsNullOrEmpty(txtFiltroAnoVencimento.Text)) ? new int?() : Convert.ToInt32(txtFiltroAnoVencimento.Text);

        #endregion

        protected void imgBtnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                CtrPage.Pesquisar();
            }
            catch (Exception ex)
            {
                EnviarMensagem(ex.Message);
            }
        }

        protected void btnDistribuir_Click(object sender, EventArgs e)
        {
            try
            {
                CtrPage.Distribuir();
            }
            catch (Exception ex)
            {
                EnviarMensagem(ex.Message);
            }

        }

        protected void btnZerarDistribuicao_Click(object sender, EventArgs e)
        {
            try
            {
                CtrPage.ZerarDistribuicao(hfMotivo.Value);
            }
            catch (Exception ex)
            {
                this.EnviarMensagem(ex.Message);
            }

        }

    }
}
