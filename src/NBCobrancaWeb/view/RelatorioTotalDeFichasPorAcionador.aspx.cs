using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Anthem;
using NBCobranca.Controllers;
using NBCobranca.Interfaces;
using NBCobranca.Tipos;
using Neobridge.NBUtil;
using static Neobridge.NBUtil.BootStrapDialog.TypeMessage;

namespace NBCobranca.view
{
    public partial class RelatorioTotalDeFichasPorAcionador : PageBaseComController<Controllers.CtrRelatorioTotaisFichasPorAcionador>, IPresRelatorioTotalDeFichasPorAcionador
    {
        protected void Page_Load(object sender, EventArgs e)
        { }

        public int? GetTipoDeDividaSelecionada => (ddlTipoDividas.SelectedValue == "0") ? new int?() : Convert.ToInt32(ddlTipoDividas.SelectedValue);

        public int? GetTipoDeAcionamentoSelecionada => (ddlTipoAcionamentos.SelectedValue == "0") ? new int?() : Convert.ToInt32(ddlTipoAcionamentos.SelectedValue);

        public int? GetAcionadorSelecionado => (ddlAcionadores.SelectedValue == "0") ? new int?() : Convert.ToInt32(ddlAcionadores.SelectedValue);

        public DateTime? GetDataVencimentoInicial => (string.IsNullOrEmpty(txtFiltroDataVencimentoInicial.Text)) ? new DateTime?() : Convert.ToDateTime(txtFiltroDataVencimentoInicial.Text);

        public DateTime? GetDataVencimentoFinal => (string.IsNullOrEmpty(txtFiltroDataVencimentoFinal.Text)) ? new DateTime?() : Convert.ToDateTime(txtFiltroDataVencimentoFinal.Text);

        public DateTime? GetDataAcionamentoInicial => (string.IsNullOrEmpty(txtFiltroDataAcionamentoInicial.Text)) ? new DateTime?() : Convert.ToDateTime(txtFiltroDataAcionamentoInicial.Text);

        public DateTime? GetDataAcionamentoFinal => (string.IsNullOrEmpty(txtFiltroDataAcionamentoFinal.Text)) ? new DateTime?() : Convert.ToDateTime(txtFiltroDataAcionamentoFinal.Text);

        public void CarregaPesquisa(IEnumerable listaTotaisPorAcionador)
        {
            gvResultado.DataSource = listaTotaisPorAcionador;
            gvResultado.DataBind();
        }

        public void CarregaListaTiposDeDivida(IEnumerable listaTiposDeDivida)
        {
            ddlTipoDividas.DataSource = listaTiposDeDivida;
            ddlTipoDividas.DataBind();
            ddlTipoDividas.Items.Insert(0, new ListItem("Todas", "0"));
            ddlTipoDividas.SelectedIndex = 0;
        }

        public void CarregaListaDeCobradores(IEnumerable listaSomenteCobradores)
        {
            ddlAcionadores.DataSource = listaSomenteCobradores;
            ddlAcionadores.DataBind();
            ddlAcionadores.Items.Insert(0, new ListItem("Todos", "0"));
            ddlAcionadores.SelectedIndex = 0;

        }

        public void CarregaListaTipoDeAcionamentos(IEnumerable listaTipoAcionamentos)
        {
            ddlTipoAcionamentos.DataSource = listaTipoAcionamentos;
            ddlTipoAcionamentos.DataBind();
            ddlTipoAcionamentos.Items.Insert(0, new ListItem("Todos", "0"));
            ddlTipoAcionamentos.SelectedIndex = 0;

        }

        public void PreencherDetalhesDoAcionador(DtoDetalhesDoAcionador detalhesDoAcionador)
        {
            lblAcionador.Text = detalhesDoAcionador.NomeAcionador;
            lblFichasDistribuidas.Text = detalhesDoAcionador.FichasDistribuidas.ToString("N0");
            lblFichasLiberadasParaAcionar.Text = detalhesDoAcionador.FichasLiberadasParaAcionar.ToString("N0");
            lblTotalParaCobrar.Text = detalhesDoAcionador.ValorTotalACobrar.ToString("C2");

            gvFichasPorTipoAcionamento.DataSource = detalhesDoAcionador.TotaisPorTipoAcionamentos;
            gvFichasPorTipoAcionamento.DataBind();

            gvFichasDoAcionador.DataSource = detalhesDoAcionador.FichasDoAcionador;
            gvFichasDoAcionador.DataBind();

            detalhesAcionador.Visible = true;
            detalhesAcionador.UpdateAfterCallBack = true;

            var mSbScript = new StringBuilder();
            mSbScript.AppendLine("$('html,body').animate({scrollTop:$('#ctl00_phConteudo_detalhesAcionador').offset().top - 60}, 800);");
            if (detalhesDoAcionador.FichasDoAcionador.Count > 0)
            {
                gvFichasDoAcionador.HeaderRow.TableSection = TableRowSection.TableHeader;
                mSbScript.AppendFormat("$('#{0}').dataTable( \r\n", gvFichasDoAcionador.ClientID);
                mSbScript.AppendLine("{'language': {");
                mSbScript.AppendLine("'url': 'Portuguese-Brasil.json'");
                mSbScript.AppendLine("}");
                mSbScript.AppendLine("} );");
            }
            Manager.AddScriptForClientSideEval(mSbScript.ToString());


        }

        public void MostrarFicha()
        {
            ShowModal("Ficha de Devedor", "../aspx/Acionamento_Ficha.aspx");
        }

        protected void imgBtnPesquisar_Click(object sender, EventArgs e)
        {
            try
            {
                CtrPage.Pesquisar();
                detalhesAcionador.Visible = false;
                detalhesAcionador.UpdateAfterCallBack = true;
            }
            catch (Exception ex)
            {
                EnviarMensagem(ex.Message, TYPE_DANGER);
            }
        }

        protected void gvResultado_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                CtrPage.BuscarDetalhesDoAcionador(Convert.ToInt32(gvResultado.DataKeys[Convert.ToInt32(e.CommandArgument)].Value));
            }
            catch (Exception ex)
            {
                EnviarMensagem(ex.Message, TYPE_DANGER);
            }
        }

        protected void gvFichasDoAcionador_OnRowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                CtrPage.CarregarFicha(Convert.ToInt32(gvFichasDoAcionador.DataKeys[Convert.ToInt32(e.CommandArgument)].Value));
            }
            catch (Exception ex)
            {
                EnviarMensagem(ex.Message, TYPE_DANGER);
            }
        }
    }
}