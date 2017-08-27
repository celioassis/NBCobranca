using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using NBCobranca.Interfaces;
using Neobridge.NBUtil;


namespace NBCobranca.Controllers
{
    public class CtrTrocaSenha : CtrBase, IControllerPresenter
    {
        IpresTrocaSenha _view;

        public CtrTrocaSenha(Classes.Sistema sistema, CtrFactory ctrFactory)
            : base(sistema, ctrFactory)
        { }

        public void SetView(IPresView view)
        {
            _view = (IpresTrocaSenha)view;
            _view.Titulo = "Troca de senha";
        }

        public void TrocarSenha()
        {
            if (!_view.NovaSenha.Equals(_view.ConfirmaNovaSenha))
            {
                _view.EnviarMensagem("A confirmação da senha é diferente da nova senha",
                    BootStrapDialog.TypeMessage.TYPE_DANGER);
                return;
            }
            try
            {
                Sistema.LimLogin.AlterarSenha(_view.SenhaAtual, _view.NovaSenha);
                _view.SenhaAtual = "";
                _view.NovaSenha = "";
                _view.ConfirmaNovaSenha = "";
                _view.AtualizaCampos = true;
                _view.EnviarMensagem("Senha alterada com sucesso", BootStrapDialog.TypeMessage.TYPE_SUCCESS);
            }
            catch (Exception ex)
            {
                _view.EnviarMensagem(ex.Message, BootStrapDialog.TypeMessage.TYPE_DANGER);
            }

        }
    }
}
