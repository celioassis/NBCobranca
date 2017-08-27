using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.UI;
using Anthem;
using NBCobranca.Controllers;
using Neobridge.NBUtil;
using Label = System.Web.UI.WebControls.Label;

namespace NBCobranca.view
{
    public class PageBase : Page
    {
        protected CtrFactory Controller;

        protected bool PaginaAbertaEmJanelaModal = false;

        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            Manager.Register(this);

            Controller = Session["Controller"] as CtrFactory;

            if (Controller == null)
            {
                Response.Redirect("Login.aspx");
                return;
            }

            Controller.Sistema.PaginaWeb = this;

            var lblUsuario = Master?.FindControl("lblUsuario") as ITextControl;
            var lblVersao = Master?.FindControl("lblVersao") as Label;
            var mVersion = Assembly.GetExecutingAssembly().GetName().Version;
            if (lblUsuario != null) lblUsuario.Text = Controller.UsuarioLogado;
            if (lblVersao != null) lblVersao.Text = "Versão: " + mVersion;
        }

        protected string GetAnthemCallbackEvent(ICallBackControl componente, string paramThis)
        {
            return Manager.GetCallbackEventReference(componente, false, "").Replace("javascript:Anthem_FireCallBackEvent(this,event", "Anthem_FireCallBackEvent(" + paramThis + ",null");
        }

        /// <summary>
        /// Envia ao Usuário uma notificação
        /// </summary>
        /// <param name="pMensagem">Mensagem a ser enviada</param>
        /// <param name="pTipoMensagem"></param>
        public void EnviarMensagem(string pMensagem, BootStrapDialog.TypeMessage pTipoMensagem)
        {
            var mensagemTradata = pMensagem.Replace("'", "&quot;").Replace("\"", "&quot;").Replace("\r\n", "<br>");
            Manager.AddScriptForClientSideEval(BootStrapDialog.Mensagem("Mensagem do Sistema", mensagemTradata, pTipoMensagem));
        }

        /// <summary>
        /// Enviar mensagem de erro ao usuário.
        /// </summary>
        /// <param name="pMensagem"></param>
        protected void EnviarMensagem(string pMensagem)
        {
            EnviarMensagem(pMensagem, BootStrapDialog.TypeMessage.TYPE_DANGER);
        }

        public void ShowModal(string titulo, string pathAspx, int altura = 480)
        {
            Manager.AddScriptForClientSideEval($"ShowModal('{titulo}', '{pathAspx}', {altura});");
        }
    }
}
