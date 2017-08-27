using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NBCobranca.Controllers;
using NBCobranca.Interfaces;

namespace NBCobranca.view
{
    public partial class TrocarSenha : PageBaseComController<CtrTrocaSenha>, IpresTrocaSenha
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        public string SenhaAtual
        {
            get { return txtSenhaAtual.Text; }
            set { txtSenhaAtual.Text = value; }
        }

        public string NovaSenha
        {
            get { return txtNovaSenha.Text; }
            set { txtNovaSenha.Text = value; }
        }

        public string ConfirmaNovaSenha
        {
            get { return txtConfirmaNovaSenha.Text; }
            set { txtConfirmaNovaSenha.Text = value; }
        }

        public bool AtualizaCampos
        {
            set { pnCampos.UpdateAfterCallBack = value; }
        }

        protected void btnTrocarSenha_OnClick(object sender, EventArgs e)
        {
            CtrPage.TrocarSenha();
        }

    }
}