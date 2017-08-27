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
    public class CtrDistribuicaoRodizio : CtrBase, IControllerPresenter
    {
        IpresDistribuicaoRodizio _view;

        public CtrDistribuicaoRodizio(Classes.Sistema sistema, CtrFactory ctrFactory)
            : base(sistema, ctrFactory)
        { }

        public void SetView(IPresView view)
        {
            ValidaCredencial();
            _view = (IpresDistribuicaoRodizio)view;
            _view.Titulo = "Rodízio de fichas entre acionadores";
        }

        public void ProcessarRodizio(string motivo)
        {
            try
            {
                Sistema.busDistribuirFichas.ProcessarRodizio(CtrFactory.UsuarioLogado, false, motivo);
                _view.EnviarMensagem("Rodízio realizado com sucesso", BootStrapDialog.TypeMessage.TYPE_SUCCESS);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocorreu um erro inesperado ao processar o rodízio das fichas, o erro ocorrido foi: {ex.Message}");
            }
        }
    }
}
