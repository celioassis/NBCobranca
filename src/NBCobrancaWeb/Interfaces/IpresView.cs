using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Neobridge.NBUtil;

namespace NBCobranca.Interfaces
{
    /// <summary>
    /// Interface base para as outras presenters, é utilizada na Interface de ControllerPresenter responsável pelo vinculo entre a view e o controller.
    /// </summary>
    public interface IPresView
    {
        string Titulo { set;}

        /// <summary>
        /// Envia ao Usuário uma notificação
        /// </summary>
        /// <param name="pMensagem"></param>
        void EnviarMensagem(string pMensagem, BootStrapDialog.TypeMessage pTipoMensagem);

        bool IsCallBack { get; }
    }
}
