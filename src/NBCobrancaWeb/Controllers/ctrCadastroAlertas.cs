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


namespace NBCobranca.Controllers
{
    public class ctrCadastroAlertas : CtrBase
    {
        IpresAlertas aView;
        Classes.BusAlertas aBusAlertas;

        public ctrCadastroAlertas(Classes.Sistema sistema, CtrFactory ctrFactory)
            : base(sistema, ctrFactory)
        {
            aBusAlertas = Sistema.busAlertas;
        }

        public void SetView(IpresAlertas pView)
        {
            this.aView = pView;
        }

        public void Salvar()
        {
            try
            {
                this.aBusAlertas.AddAlerta(this.IDUsuarioLogado, this.aView.ID_UsuarioDestino, this.aView.MensagemAlerta, this.aView.DataHora);
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível cadastrar o alerta - " + ex.Message, ex);
            }
        }

        public void Excluir(int pID)
        {
            this.aBusAlertas.ExcluirAlerta(pID);
        }

        public string GetMensagem(int pID)
        {
            return null;
        }

        public DataView LoginsAtivos
        {
            get
            {
                return aBusAlertas.LoadLoginsAtivos;
            }
        }

        /// <summary>
        /// Get - Código de identificação do Usuário que esta logado no sistema na sessão atual.
        /// </summary>
        public int IDUsuarioLogado
        {
            get
            {
                return this.Sistema.LimLogin.UsuarioID;
            }
        }

    }
}
