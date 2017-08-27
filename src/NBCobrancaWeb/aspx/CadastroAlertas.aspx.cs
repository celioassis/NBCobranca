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
using NBCobranca.Interfaces;
using NBCobranca.Controllers;


namespace NBCobranca.aspx
{
    public partial class CadastroAlertas : FrmBase, IpresAlertas
    {
        ctrCadastroAlertas aCtrCadAlertas;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
                this.ddlUsuarioDestinoAlerta.DataSource = this.aCtrCadAlertas.LoginsAtivos;
                this.DataBind();
                this.ddlUsuarioDestinoAlerta.Text = aCtrCadAlertas.IDUsuarioLogado.ToString();
                this.Form.DefaultFocus = txtDataAlerta_DATA.ClientID;
            }
        }

        protected override void OnInit(EventArgs e)
        {
            this.aPaginaAbertaEmJanelaModal = true;
            base.OnInit(e);
            if (MessageBox.FechandoModal)
                return;
            this.aCtrCadAlertas = this.aController.ctrCadAlertas;
            this.aCtrCadAlertas.SetView(this);
        }

        protected override void OnUnload(EventArgs e)
        {
            if (this.aCtrCadAlertas != null)
                this.aCtrCadAlertas.SetView(null);
            base.OnUnload(e);
        }

        protected void imgBtnSalvar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                this.aCtrCadAlertas.Salvar();
                this.MessageBox.Show("Alerta salvo com sucesso!\\r\\n O mesmo será disparado na data e hora marcada para o usuário selecionado.", true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region IpresAlertas Members

        public DateTime DataHora
        {
            get
            {
                if (string.IsNullOrEmpty(txtDataAlerta_DATA.Text))
                    throw new Exception("A Data do alerta é inválida!");
                if (string.IsNullOrEmpty(txtHoraAlerta_HORA.Text))
                    throw new Exception("A Hora do alerta é inválida!");

                DateTime mDataHoraAlerta;

                if (!DateTime.TryParse(txtDataAlerta_DATA.Text + " " + txtHoraAlerta_HORA.Text, out mDataHoraAlerta))
                    throw new Exception("A data ou hora do alerta são inválidos");

                return mDataHoraAlerta;
            }
            set
            {
                txtDataAlerta_DATA.Text = value.ToShortDateString();
                txtHoraAlerta_HORA.Text = value.ToShortTimeString();
            }
        }

        public int ID_UsuarioDestino
        {
            get
            {
                int mID_UsuarioDestino = 0;
                if (!Int32.TryParse(ddlUsuarioDestinoAlerta.SelectedValue, out mID_UsuarioDestino))
                    throw new Exception("O código do Usuário de destino é inválido, é preciso selecionar um usuário");

                return mID_UsuarioDestino;
            }
            set
            {
                ddlUsuarioDestinoAlerta.Text = value.ToString();
            }
        }

        public string MensagemAlerta
        {
            get
            {
                return this.txtMensagemAlerta.Text;
            }
            set
            {
                this.txtMensagemAlerta.Text = value;
            }
        }

        #endregion
    }
}
