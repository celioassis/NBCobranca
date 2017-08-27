using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NBCobranca.Controllers;
using NBCobranca.Interfaces;
using Neobridge.NBUtil;

namespace NBCobranca.view
{
    public partial class Devedores : PageBaseComController<ctrDevedores>, IPresDevedores
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        [Anthem.Method]
        protected void Pesquisar()
        {
            try
            {
                CtrPage.PesquisaDevedores();
            }
            catch (Exception ex)
            {
                EnviarMensagem(ex.Message, BootStrapDialog.TypeMessage.TYPE_DANGER);
            }
            
            
        }

        public void CarregaListaClientes(ICollection pListaClientes)
        {
            ddlCarteiras.DataSource = pListaClientes;
            ddlCarteiras.DataBind();
        }

        public void CarregaFichasPesquisadas(ICollection pListaFichasPesquisadas)
        {
            gvResultado.DataSource = pListaFichasPesquisadas;
            gvResultado.DataBind();
        }

        public string GetCarteiraSelecionada => ddlCarteiras.SelectedValue;

        public int GetCampoFiltro => Convert.ToInt32(selCamposProcurar.SelectedValue);

        public string GetTextoProcurar => txtProcurar.Text;
    }
}