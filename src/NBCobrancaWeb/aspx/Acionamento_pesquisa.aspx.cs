using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Collections.Generic;
using System.Drawing;
using System.Web;
using System.Text;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using NBCobranca.Classes;
using NBCobranca.Tipos;

namespace NBCobranca.aspx
{
    /// <summary>
    /// Summary description for Acionamento_pesquisa.
    /// </summary>
    public partial class Acionamento_pesquisa : System.Web.UI.Page
    {

        Classes.LimAcionamentos obj;
        Classes.Sistema Sistema;

        protected void Page_Load(object sender, System.EventArgs e)
        {
            NBFuncoes.ValidarSistema(this, ref this.Sistema);
            this.Sistema.Legenda.Titulo = "Acionamentos";
            obj = Sistema.LimAcionamentos;
            if (this.Sistema.Legenda.SubTitulo == "Acionamento")
            {
                this.Sistema.Legenda.SubTitulo = "";
                this.obj.LimparAcionamentosTemporarios();
            }

            if (!this.IsPostBack)
            {
                this.Sistema.TipoEntidade = NBdbm.tipos.TipoEntidade.Devedores;
                this.Sistema.ValidaCredencial(Permissao.Todos);
                this.PreencheDDLs();
                this.MessageBox.BotaoSubmit = this.imgBtnPesquisar.ClientID;
                this.txtProcurar.Focus();
            }
        }

        private void PreencheDDLs()
        {

            var mLi = new ListItem("Todas") {Selected = true};
            this.ddlCarteiras.DataSource = obj.Carteiras;
            this.ddlCarteiras.DataBind();
            this.ddlCarteiras.Items.Insert(0, mLi);

            mLi = new ListItem("Todos", "0") { Selected = true };
            this.ddlTiposDivida.DataSource = obj.TiposDivida;
            this.ddlTiposDivida.DataBind();
            this.ddlTiposDivida.Items.Insert(0, mLi);

            mLi = new ListItem("Todos", "0") { Selected = true };
            this.ddlTiposAcionamento.DataSource = obj.TiposAcionamento;
            this.ddlTiposAcionamento.DataBind();
            this.ddlTiposAcionamento.Items.Insert(0, mLi);

            mLi = new ListItem("Todos", "0") { Selected = true };
            ddlAcionadores.DataSource = Sistema.busFuncionarios.ListaSomenteCobradores();
            ddlAcionadores.DataBind();
            ddlAcionadores.Items.Insert(0, mLi);

            if (Sistema.LimLogin.Credencial != TipoCredencial.Acionador) return;
            var item = ddlAcionadores.Items.FindByText(Sistema.LimLogin.NomeCompletoUsuario);
            ddlAcionadores.SelectedIndex = ddlAcionadores.Items.IndexOf(item);
            ddlAcionadores.Enabled = false;
        }

        protected void imgBtnPesquisar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            DateTime? mPeriodoInicial = null;
            DateTime? mPeriodoFinal = null;

            if (!string.IsNullOrEmpty(txtDataInicial_DATA.Text) && !string.IsNullOrEmpty(txtDataFinal_DATA.Text))
            {
                mPeriodoInicial = Convert.ToDateTime(txtDataInicial_DATA.Text);
                mPeriodoFinal = Convert.ToDateTime(txtDataFinal_DATA.Text);
            }
            else
            {
                txtDataInicial_DATA.Text = "";
                txtDataFinal_DATA.Text = "";
            }

            this.obj.CriarFiltros(this.ddlCarteiras.SelectedValue,
                this.txtProcurar.Value,
                int.Parse(this.ddlTiposDivida.SelectedValue),
                int.Parse(ddlQuantDivida.SelectedValue),
                mPeriodoInicial,
                mPeriodoFinal,
                ddlAcionadores.SelectedValue,
                ddlTiposAcionamento.SelectedValue);

            if (txtProcurar.Value != "")
                this.dgDados.CurrentPageIndex = 0;

            obj.FillDataGrid(this.dgDados, dgDados.CurrentPageIndex);
        }

        protected void dgDados_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            if (e.CommandName == "Acionar")
            {
                this.obj.GetDevedor(int.Parse(e.Item.Cells[0].Text));
                this.MessageBox.Largura = 900;
                this.MessageBox.ModalShow("Acionamento_Ficha.aspx");
            }

        }

        protected void dgDados_PageIndexChanged(object source, System.Web.UI.WebControls.DataGridPageChangedEventArgs e)
        {
            this.dgDados.CurrentPageIndex = e.NewPageIndex;
            this.obj.FillDataGrid(this.dgDados, e.NewPageIndex);
            this.dgDados.UpdateAfterCallBack = true;

        }

        protected void dgDados_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex >= 0)
            {
                if (e.Item.Cells[3].Text == "&nbsp;")
                {
                    e.Item.Cells[3].Text = "Sem Acionamento";
                    e.Item.Cells[3].ForeColor = Color.Red;
                }
            }

        }

        protected void timerAgenda_Tick(object sender, EventArgs e)
        {
            string mMensagensAlerta = obj.ListaAlertas;
            if (string.IsNullOrEmpty(mMensagensAlerta))
                return;

            string[] mAlertas = mMensagensAlerta.Split('#');

            MessageBox.ShowConfirma(mAlertas[0].Replace("\r\n", "\\r\\n"), string.Format("KeyAlertas|{0}", mAlertas[1]), true, true);
        }

        protected void MessageBox_YesChoosed(object sender, string Key)
        {
            List<string> mChave = new List<string>(Key.Split('|'));
            if (mChave[0].Equals("KeyAlertas"))
            {
                mChave.RemoveAt(0);
                foreach (string mIdAlerta in mChave)
                {
                    obj.ConfirmaLeituraAlerta(mIdAlerta);
                }
            }
        }

        protected void MessageBox_NoChoosed(object sender, string Key)
        {
            List<string> mChave = new List<string>(Key.Split('|'));
            if (mChave[0].Equals("KeyAlertas"))
            {
                mChave.RemoveAt(0);
                foreach (string mIdAlerta in mChave)
                {
                    obj.AdicionaMaisTempoParaAlerta(mIdAlerta);
                }
            }
        }

        protected void imgBtnSMS_Click(object sender, ImageClickEventArgs e)
        {
            if (this.dgDados.VirtualItemCount == 0)
            {
                this.MessageBox.Show("É preciso realizar uma pesquisa que retorne dados para que seja possível o envio de SMS");
                return;
            }
            this.MessageBox.Altura = 180;
            this.MessageBox.ModalShow("EnvioSMS.aspx");
        }
    }
}
