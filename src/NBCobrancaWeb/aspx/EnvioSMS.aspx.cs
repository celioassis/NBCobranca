using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using NBCobranca.Controllers;

namespace NBCobranca.aspx
{
    public partial class EnvioSMS : FrmBase
    {
        ctrEnvioSMS aCtrEnvioSMS;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                if (this.aCtrEnvioSMS.ListaSMS.Count == 0)
                {
                    Anthem.Manager.Register(this); 
                    MessageBox.Show("O Devedor não contém celulares para o envio de SMS", true);
                    return;
                }
                this.gvDevedores.DataSource = this.aCtrEnvioSMS.ListaSMS;
                this.gvDevedores.DataBind();
                if (!string.IsNullOrEmpty(aCtrEnvioSMS.DevedorSelecionado))
                {
                    txtMensagemSMS.Text = txtMensagemSMS.Text.Replace("[NOME_DEVEDOR]", aCtrEnvioSMS.DevedorSelecionado.ToUpper());
                }
            }
        }

        protected override void OnInit(EventArgs e)
        {
            this.aPaginaAbertaEmJanelaModal = true;
            base.OnInit(e);
            if (MessageBox.FechandoModal)
                return;
            this.aCtrEnvioSMS = this.aController.ctrEnvioSMS;
        }

        protected void imgBtnEnviarSMS_Click(object sender, ImageClickEventArgs e)
        {
            foreach (GridViewRow mGVR in gvDevedores.Rows)
            {
                CheckBox mCK = mGVR.FindControl("ckbItem") as CheckBox;
                if (mCK.Checked)
                {
                    string mTelefone = mGVR.Cells[2].Text;
                    aCtrEnvioSMS.AddSMS(txtMensagemSMS.Text, mTelefone);
                }
            }
            try
            {
                this.aCtrEnvioSMS.Enviar();
                this.MessageBox.ModalClose("SMS enviado com sucesso", "ENVIO_SMS");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        protected void ckbAll_CheckedChanged(object sender, EventArgs e)
        {

            foreach (GridViewRow mGVR in gvDevedores.Rows)
            {
                CheckBox mCK = mGVR.FindControl("ckbItem") as CheckBox;
                mCK.Checked = (sender as CheckBox).Checked;
            }
        }
    }
}
