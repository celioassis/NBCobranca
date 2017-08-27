using NBCobranca.Controllers;
using NBCobranca.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NBCobranca.view
{
    public partial class Configuracoes : PageBaseComController<CtrConfiguracoes>, IpresConfiguracoes
    {
        #region Implementação da IpreConfiguracoes

        public string ConfirmaSenha
        {
            get { return txtConfirmaSenha.Text; }

            set { txtConfirmaSenha.Text = value; }
        }

        public int PortaSMTP
        {
            get { if (string.IsNullOrEmpty(txtPortaSMTP.Text)) return 0; else return Convert.ToInt32(txtPortaSMTP.Text); }

            set { txtPortaSMTP.Text = value.ToString(); }
        }

        public string Senha
        {
            get { return txtSenha.Text; }

            set { txtSenha.Text = value; }
        }

        public string ServidorSMTP
        {
            get { return txtServidorSMTP.Text; }

            set { txtServidorSMTP.Text = value; }
        }

        public string UsuarioSMTP
        {
            get { return txtUsuarioSMTP.Text; }

            set { txtUsuarioSMTP.Text = value; }
        }

        public string EmailDeValidacao
        {
            get { return hfEmailDestino.Value; }
            set { hfEmailDestino.Value = value; }
        }

        public int IDConfig
        {
            get { return Convert.ToInt32(hfID.Value); }
            set { hfID.Value = value.ToString(); }
        }

        #endregion

        protected void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                CtrPage.SalvarConfiguracoes();
                hfID.UpdateAfterCallBack = true;
                EnviarMensagem("Configurações salvas com sucesso!", Neobridge.NBUtil.BootStrapDialog.TypeMessage.TYPE_SUCCESS);
            }
            catch (Exception ex)
            {
                EnviarMensagem(ex.Message);
            }
        }

        protected void btnValidarConfiguracoes_Click(object sender, EventArgs e)
        {
            try
            {
                CtrPage.ValidarConfiguracoes();
                EnviarMensagem($"Foi enviado um email para {EmailDeValidacao} favor verificar se recebeu para confirmar que as configurações estão corretas.", Neobridge.NBUtil.BootStrapDialog.TypeMessage.TYPE_SUCCESS);
            }
            catch (Exception ex)
            {
                EnviarMensagem(ex.Message);
            }
        }

    }
}