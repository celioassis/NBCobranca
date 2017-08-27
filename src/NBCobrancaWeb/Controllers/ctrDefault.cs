using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace NBCobranca.Controllers
{
    public class ctrDefault : CtrBase, Interfaces.IControllerPresenter
    {
        Interfaces.IPresDefault aView;

        public ctrDefault(Classes.Sistema sistema, CtrFactory ctrFactory)
            : base(sistema, ctrFactory)
        { }

        #region IcontrollerPresenter Members

        void NBCobranca.Interfaces.IControllerPresenter.SetView(NBCobranca.Interfaces.IPresView view)
        {
            this.aView = view as Interfaces.IPresDefault;

            this.aView.Titulo = "Menu Principal";
            this.aView.MensagemDeBoasVindas = "Seja bem-vindo, " + this.CtrFactory.NomeCompletoUsuario;
            this.aView.ImagemFundo = this.aView.ImagemFundo.Replace("logo_lugphil_fundo.gif", "Logo_Neobridge.gif");
            this.aView.DataCompletaDoDia = "Hoje é " + DateTime.Today.ToLongDateString();
        }

        #endregion
    }
}
