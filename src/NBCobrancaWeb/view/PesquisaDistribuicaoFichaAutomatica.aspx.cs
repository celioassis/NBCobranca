using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using Anthem;
using NBCobranca.Controllers;
using NBCobranca.Interfaces;

namespace NBCobranca.view
{
    public partial class PesquisaDistribuicaoFichaAutomatica : PageBaseComController<CtrDistribuicaoFichas>, IPresPesquisaDistribuicaoFichas
    {
        

        #region IpresPesquisaDistribuicaoFichas Members

        public void CarregaListaClientes(ICollection pListaClientes)
        {
            this.ddlCarteiras.DataSource = pListaClientes;
            this.ddlCarteiras.DataBind();
            this.ddlCarteiras.Items.Insert(0, new ListItem("Todas"));
            this.ddlCarteiras.SelectedIndex = 0;
        }

        public void CarregaFichasPesquisadas(ICollection pListaFichasPesquisadas)
        {
            this.gvResultado.DataSource = pListaFichasPesquisadas;
            this.gvResultado.DataBind();
            if (pListaFichasPesquisadas.Count > 0)
            {
                this.gvResultado.HeaderRow.TableSection = TableRowSection.TableHeader;
                StringBuilder mSBScript = new StringBuilder();
                mSBScript.AppendFormat("$('#{0}').dataTable( \r\n", this.gvResultado.ClientID);
                mSBScript.AppendLine("{'language': {");
                mSBScript.AppendLine("'url': 'Portuguese-Brasil.json'");
                mSBScript.AppendLine("}");
                mSBScript.AppendLine("} );");
                Manager.AddScriptForClientSideEval(mSBScript.ToString());
            }
        }

        public string GetCarteiraSelecionada => (ddlCarteiras.SelectedValue == "Todas") ? null : ddlCarteiras.SelectedValue;

        public int GetTipoDeDividaSelecionada => (ddlTipoDividas.SelectedValue == "Todas") ? 0 : Convert.ToInt32(ddlTipoDividas.SelectedValue);

        public DateTime? GetFiltroDataVencimento => (string.IsNullOrEmpty(txtFiltroDataVencimento.Text)) ? new DateTime?() : Convert.ToDateTime(txtFiltroDataVencimento.Text);

        public int? GetFiltroMes => (string.IsNullOrEmpty(txtFiltroMesVencimento.Text)) ? new int?() : Convert.ToInt32(txtFiltroMesVencimento.Text);

        public int? GetFiltroAno => (string.IsNullOrEmpty(txtFiltroAnoVencimento.Text)) ? new int?() : Convert.ToInt32(txtFiltroAnoVencimento.Text);

        public void CarregaListaTiposDeDivida(ICollection pListaTiposDeDivida)
        {
            ddlTipoDividas.DataSource = pListaTiposDeDivida;
            ddlTipoDividas.DataBind();
            ddlTipoDividas.Items.Insert(0, new ListItem("Todas", "0"));
            ddlTipoDividas.SelectedIndex = 0;
        }

        public void CarregaListaDeCobradores(ICollection pListaDeCobradores)
        {
           this.chkListAcionadores.DataSource = pListaDeCobradores;
           this.chkListAcionadores.DataBind();
           
        }

        public List<int> GetAcionadoresSelecionados
        {
            get
            {
                return (chkListAcionadores.Items.Cast<ListItem>()
                    .Where(acionador => acionador.Selected)
                    .Select(acionador => Convert.ToInt32(acionador.Value))).ToList();
            }
        }

        #endregion

        protected void btnDistribuir_Click(object sender, EventArgs e)
        {
            try
            {
                this.CtrPage.Distribuir();
                
            }
            catch (Exception ex)
            {
                this.EnviarMensagem(ex.Message);
            }
            
        }

        protected void imgBtnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                this.CtrPage.Pesquisar();
            }
            catch (Exception ex)
            {
                this.EnviarMensagem("Não foi possível realizar a pesquisa das fichas a serem distribuidas, ocorreu o seguinte erro: " + ex.Message);
            }
        }

        protected void btnZerarDistribuicao_Click(object sender, EventArgs e)
        {
            try
            {
                this.CtrPage.ZerarDistribuicao(hfMotivo.Value);
            }
            catch (Exception ex)
            {
                this.EnviarMensagem("Não foi possível zerar a distribuição das fichas, ocorreu o seguinte erro: " + ex.Message);
            }
        }

        /// <summary>
        /// Evento de botão que será chamado via ajax
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnExecutaZerarDistribuicao_Click(object sender, EventArgs e)
        {
            try
            {
                this.CtrPage.ZerarDistribuicao(hfMotivo.Value);
                Response.Write("Distribuição zerada com sucesso!");
            }
            catch (Exception ex)
            {
                Response.Write("Não foi possível zerar a distriubição das fichas, ocorreu o seguinte erro: " + ex.Message);
            }
            Response.End();
        }
    }
}
