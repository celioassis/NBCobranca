using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace NBCobranca.aspx
{
    /// <summary>
    /// Classe base para as demais classes de ASPX
    /// </summary>
    public class FrmBase : Page
    {
        protected Controllers.CtrFactory aController;
        protected bool aPaginaAbertaEmJanelaModal = false;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
            Classes.NBFuncoes.ValidarSistema(this, ref this.aController, aPaginaAbertaEmJanelaModal);
        }
    }
}
