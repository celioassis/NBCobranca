using System;
using System.Drawing;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using NBCobranca.Classes;

namespace NBCobranca.aspx
{
    public partial class AcionamentoFicha : System.Web.UI.Page
    {
        protected ComponentArt.Web.UI.Snap SnapAcionamentos;
        protected System.Web.UI.WebControls.RadioButton rbEnderecoPrincipal;
        protected System.Web.UI.WebControls.DataGrid dgEnderecos;
        protected System.Web.UI.WebControls.DataGrid dgTelefones;
        protected System.Web.UI.WebControls.DataGrid dgEmails;
        protected System.Web.UI.WebControls.DataGrid dgDividas;
        protected System.Web.UI.WebControls.TextBox txtDescEmail;

        private Classes.Sistema Sistema;
        private Classes.LimAcionamentos obj;

        protected void Page_Load(object sender, EventArgs e)
        {
            this.dgEnderecos = (DataGrid)this.SnapEndereco.FindControl("dgSnapEnderecos");
            this.dgTelefones = (DataGrid)this.SnapTelefone.FindControl("dgSnapTelefones");
            this.dgEmails = (DataGrid)this.SnapEmail.FindControl("dgSnapEmails");
            //this.dgDividas = (DataGrid)this.SnapDividas.FindControl("dgSnapDividas");
            this.dgDividas = dgSnapDividas;
            NBFuncoes.ValidarSistema(this, ref Sistema, this.MessageBox);
            if (this.MessageBox.FechandoModal)
                return;
            obj = this.Sistema.LimAcionamentos;
            if (!this.IsPostBack)
            {
                this.Sistema.ValidaCredencial(NBCobranca.Tipos.Permissao.Todos);
                this.ddlTipoAcionamento.DataSource = obj.TiposAcionamento;
                this.ddlTipoAcionamento.DataBind();
                this.ddlTipoAcionamento.SelectedValue = obj.TipoAcionamentoPadrao;
                this.Sistema.Legenda.SubTitulo = "Acionamento";
                this.MostrarFicha();
            }
            this.MessageBox.TipoSubmit = NBWebControls.MessageBox.enTipoSubmit.AnthemCallBack;

        }

        private void MostrarFicha()
        {
            System.Text.StringBuilder mSB = new System.Text.StringBuilder();

            #region === Tipo de Pessoa ===
            if (obj.FichaDevedor.Entidade.PessoaFisica)
            {
                this.pnPF.Visible = true;
                txtNome.Text = obj.FichaDevedor.Entidade.NomeRazaoSocial_key;
                if (double.Parse(obj.FichaDevedor.Entidade.CPFCNPJ_key) > 0)
                    txtCPF.Text = obj.FichaDevedor.Entidade.CPFCNPJ_key;
                txtRG.Text = obj.FichaDevedor.Entidade.RgIE;

            }
            else
            {
                this.pnPJ.Visible = true;
                txtRazaoSocial.Text = obj.FichaDevedor.Entidade.NomeRazaoSocial_key;
                if (double.Parse(obj.FichaDevedor.Entidade.CPFCNPJ_key) > 0)
                    txtCNPJ.Text = obj.FichaDevedor.Entidade.CPFCNPJ_key;
                txtNomeFantasia.Text = obj.FichaDevedor.Entidade.ApelidoNomeFantasia;
                txtInscEstadual.Text = obj.FichaDevedor.Entidade.RgIE;
            }
            mSB.Append("Nome=" + this.obj.FichaDevedor.Entidade.NomeRazaoSocial_key + "&");
            #endregion

            #region === Endereço ===
            if (obj.FichaDevedor.colecaoEnderecos.Count == 0)
                this.pnEndereco.Visible = false;
            else if (obj.FichaDevedor.colecaoEnderecos.Count == 1)
            {
                this.txtEndereco.Text = obj.FichaDevedor.Endereco.Logradouro_key;
                this.txtComplemento.Text = obj.FichaDevedor.Endereco.complemento;
                this.txtBairro.Text = obj.FichaDevedor.Endereco.Bairro;
                this.txtCEP.Text = obj.FichaDevedor.Endereco.CEP;
                this.txtCidade.Text = obj.FichaDevedor.Endereco.Municipio;
                this.txtUF.Text = obj.FichaDevedor.Endereco.UF;
                this.txtComentarios.Text = obj.FichaDevedor.Endereco.Comentario;
                this.txtContato.Text = obj.FichaDevedor.Endereco.Contato;

            }
            else
            {
                this.pnEndereco.Visible = false;
                this.pnSnapEndereco.Visible = true;
                dgEnderecos.DataSource = this.obj.FichaDevedor.colecaoEnderecos.Values;
                dgEnderecos.DataBind();
            }
            mSB.Append("Endereco=" + obj.FichaDevedor.Endereco.Logradouro_key);
            if (obj.FichaDevedor.Endereco.complemento != "")
                mSB.Append(" - " + obj.FichaDevedor.Endereco.complemento);
            mSB.Append("&");
            mSB.Append("Bairro=" + obj.FichaDevedor.Endereco.Bairro + "&");
            mSB.Append("CepMunUF=" + obj.FichaDevedor.Endereco.CEP + " - " +
                        obj.FichaDevedor.Endereco.Municipio + " - " +
                        obj.FichaDevedor.Endereco.UF);
            if (obj.FichaDevedor.Endereco.Comentario != "")
                mSB.Append(" - " + obj.FichaDevedor.Endereco.Comentario);
            #endregion

            #region === Telefone ===
            if (this.obj.FichaDevedor.colecaoTelefones.Count == 0)
                this.pnTelefone.Visible = false;
            else if (this.obj.FichaDevedor.colecaoTelefones.Count == 1)
            {
                txtDescricaoTelefone.Text = this.obj.FichaDevedor.Telefone.Descricao;
                txtContatoTelefone.Text = this.obj.FichaDevedor.Telefone.Contato;
                txtDDDTelefone.Text = this.obj.FichaDevedor.Telefone.DDD_key;
                txtTelefone.Text = this.obj.FichaDevedor.Telefone.Fone_key;
                txtRamal.Text = this.obj.FichaDevedor.Telefone.Ramal;
            }
            else
            {
                this.pnTelefone.Visible = false;
                this.pnSnapTelefones.Visible = true;
                dgTelefones.DataSource = this.obj.FichaDevedor.colecaoTelefones.Values;
                dgTelefones.DataBind();
            }

            #endregion

            #region === Email ===
            if (this.obj.FichaDevedor.colecaoEmail.Count == 0)
                this.pnEmail.Visible = false;
            else if (this.obj.FichaDevedor.colecaoEmail.Count == 1)
                this.txtEmail.Text = obj.FichaDevedor.eMail.eMail_key;
            else
            {
                this.pnEmail.Visible = false;
                this.pnSnapEmails.Visible = true;
                dgEmails.DataSource = this.obj.FichaDevedor.colecaoEmail.Values;
                dgEmails.DataBind();
            }
            #endregion

            #region === Dividas ===
            this.dgDividas.DataSource = this.obj.DataSourceDividas;
            this.dgDividas.DataBind();
            //if ((dgDividas.DataSource as ICollection).Count > 10)
            //    this.SnapDividas.IsCollapsed = true;
            #endregion

            #region === Acionamentos ===
            dgAcionamentos.DataSource = this.obj.DataSourceAcionamentos;
            dgAcionamentos.DataBind();
            #endregion

            #region === Texto a Respeito do Devedor ===
            if (obj.FichaDevedor.Entidade.TextoRespeito != "")
            {
                this.pnObsCadastro.Visible = true;
                this.txtObsDevedor.Text = HttpUtility.HtmlDecode(obj.FichaDevedor.Entidade.TextoRespeito);
            }
            #endregion

            string mFunctionClickFormat = "VisualizaRelatorio('Relatorios/rel_cartasCustomizado.aspx?idDevedor={0}','_Blank','&2Aviso=' + {1}.checked + '&IdCarta=' + document.getElementById('ddlCartas').value);";
            this.btnEmitirCarta.Attributes.Add("onclick", string.Format(mFunctionClickFormat, obj.FichaDevedor.Entidade.ID, this.ckbSegundoAviso.ClientID));

        }

        protected void dgDividas_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex >= 0)
            {
                NBdbm.Interfaces.iCOBR.Primitivas.iDivida mDivida = (NBdbm.Interfaces.iCOBR.Primitivas.iDivida)e.Item.DataItem;
                string txt = this.obj.GetDescricaoTipoDivida(mDivida.idTipoDivida);
                e.Item.Cells[2].Text = txt;
                if (mDivida.BaixaParcial && !mDivida.Baixada)
                    e.Item.Cells[6].Text = this.obj.ValorNominalParcial(mDivida).ToString("N");
                e.Item.Cells[7].Text = obj.ValorCorrigido(mDivida, mDivida.ValorNominal).ToString("N");
                e.Item.Cells[8].Text = this.Sistema.LimBaixa.StatusBaixa(mDivida);
                e.Item.Cells[1].Text = obj.GetCarteiraAtual;
                if (mDivida.Baixada)
                    e.Item.ForeColor = Color.Blue;
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                e.Item.Cells[1].Text = "Soma Total da Dívida:&nbsp;";
                e.Item.Cells[2].Text = obj.DividaTotalNominal.ToString("N");
                e.Item.Cells[3].Text = obj.DividaTotalCorrigida.ToString("N");
            }

        }

        protected void dgDividas_ItemCreated(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                for (int i = 4; i >= 1; i--)
                    e.Item.Cells.RemoveAt(i);
                e.Item.Cells[1].ColumnSpan = 5;
                e.Item.Cells[1].HorizontalAlign = HorizontalAlign.Right;
                e.Item.Cells[2].HorizontalAlign = HorizontalAlign.Right;
                e.Item.Cells[3].HorizontalAlign = HorizontalAlign.Right;
            }
        }

        protected void dgAcionamentos_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex >= 0)
            {
                string mTxtUsuario = this.obj.GetNomeUsuario(int.Parse(e.Item.Cells[0].Text));
                string mTxtTipoAcionamento = this.obj.GetDescricaoTipoAcionamento(int.Parse(e.Item.Cells[3].Text));
                e.Item.Cells[0].Text = mTxtUsuario;
                e.Item.Cells[3].Text = mTxtTipoAcionamento;
                if (e.Item.Cells[2].Text == "01/01/0001")
                    e.Item.Cells[2].Text = "";
            }

        }

        protected void ddlTipoAcionamento_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (this.MessageBox.FechandoModal)
                return;
            //this.MessageBox.ManterScroll = true;
            string[] mTipoAcionamento = ddlTipoAcionamento.SelectedValue.Split('|');
            if (Convert.ToInt32(this.Sistema.LimLogin.Credencial) < int.Parse(mTipoAcionamento[2]))
            {
                this.MessageBox.Show("O usuário não tem Credencial para usar este Tipo de Acionamento.", ddlTipoAcionamento.ClientID);
                this.ddlTipoAcionamento.SelectedIndex = 1;
                this.ddlTipoAcionamento.UpdateAfterCallBack = true;
                return;
            }
            switch (ddlTipoAcionamento.SelectedItem.Text.ToUpper())
            {
                case "PROMESSA":
                    this.pnPromessa.Visible = true;
                    this.pnCartas.Visible = false;
                    this.MessageBox.MoverFoco(this.txtDataPromessa_DATA.ClientID);
                    break;
                case "CARTA":
                    this.pnCartas.Visible = true;
                    this.pnPromessa.Visible = false;
                    break;
                default:
                    this.pnCartas.Visible = false;
                    this.pnPromessa.Visible = false;
                    break;
            }
            this.pnPromessa.UpdateAfterCallBack = true;
            this.pnCartas.UpdateAfterCallBack = true;
        }

        protected void btnAdicAcionamento_Click(object sender, System.EventArgs e)
        {
            if (this.MessageBox.FechandoModal)
                return;

            try
            {
                string mTmpAcionamento = "";
                //this.MessageBox.ManterScroll = true;
                this.obj.NovoAcionamento();
                this.obj.Acionamento.DataAcionamento = DateTime.Now;
                if (ddlTipoAcionamento.SelectedItem.Text == "Promessa")
                    this.obj.Acionamento.DataPromessa = DateTime.Parse(txtDataPromessa_DATA.Text);
                this.obj.Acionamento.idUsuario = Sistema.LimLogin.UsuarioID;
                string[] mTipoAcionamento = ddlTipoAcionamento.SelectedValue.Split('|');
                this.obj.Acionamento.idTipoAcionamento = int.Parse(mTipoAcionamento[0]);
                if (ddlTipoAcionamento.SelectedItem.Text.ToUpper() == "CARTA")
                {
                    string txtAviso;
                    if (ckbSegundoAviso.Checked)
                        txtAviso = "Segundo Aviso ";
                    else
                        txtAviso = "Primeiro Aviso ";
                    if (txtAcionamentos.Text != "")
                        this.obj.Acionamento.TextoRespeito = txtAviso + " - " + txtAcionamentos.Text;
                    else
                        this.obj.Acionamento.TextoRespeito = txtAviso;
                }
                else
                    this.obj.Acionamento.TextoRespeito = txtAcionamentos.Text;

                mTmpAcionamento = this.obj.Acionamento.TextoRespeito.Replace((char)13, '|');
                this.obj.Acionamento.TextoRespeito = mTmpAcionamento.Replace("|", "<br>");
                this.obj.AdicionarAcionamento();
                this.txtAcionamentos.Text = "";
                this.txtAcionamentos.UpdateAfterCallBack = true;
                dgAcionamentos.Visible = true;
                dgAcionamentos.DataSource = this.obj.DataSourceAcionamentos;
                dgAcionamentos.DataBind();
                dgAcionamentos.UpdateAfterCallBack = true;
                //if (obj.FichaDevedor.colecaoDividas.Count > 10)
                //    this.SnapDividas.IsCollapsed = true;


            }
            catch (NBdbm.COBR_Exception CobEx)
            {
                MessageBox.Show(CobEx.Message);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }

        }

        protected void btnEditarFichar_Click(object sender, System.EventArgs e)
        {
            if (this.MessageBox.FechandoModal)
                return;

            this.Sistema.Legenda.Titulo = "Devedor - Alteração de Dados";
            this.Sistema.Legenda.SubTitulo = "Acionamento";
            this.obj.EditarFichaDevedor();
            this.Response.Redirect("entidades_cadastro.aspx");
        }

        protected void btnSalvar_Click(object sender, System.EventArgs e)
        {
            if (this.MessageBox.FechandoModal)
                return;

            try
            {
                obj.Salvar();
                MessageBox.ShowConfirma("Acionamentos Salvo com Sucesso, Deseja continuar Acionando??", "", false, true);
            }
            catch (NBdbm.COBR_Exception CobEx)
            {
                MessageBox.Show(CobEx.Message);
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        protected void MessageBox_NoChoosed(object sender, string Key)
        {
            this.MessageBox.ModalClose();
        }

        protected void btnSimulacao_Click(object sender, System.EventArgs e)
        {
            this.dgAcionamentos.Visible = false;
            this.dgAcionamentos.UpdateAfterCallBack = true;
            this.SnapToggleExpand();
            this.MessageBox.Altura = 350;
            this.MessageBox.Largura = 550;
            this.MessageBox.ModalShow("Acionamento_Simulacao.aspx", true);
        }

        protected void MessageBox_CloseModalChoosed(object sender, string Key, string pValorRetorno)
        {
            if (Key == "Simulacao")
            {
                this.txtAcionamentos.Text = pValorRetorno.Replace("|", "\r");
                this.txtAcionamentos.UpdateAfterCallBack = true;
            }
        }

        /// <summary>
        /// Metodo usado para ser executado junto com o tipo de submit do MessageBox.TipoSubmit
        /// </summary>
        [Anthem.Method]
        public void AnthemCallBack()
        {
            if (!this.dgAcionamentos.Visible)
            {
                this.dgAcionamentos.Visible = true;
                this.dgAcionamentos.UpdateAfterCallBack = true;
                this.SnapToggleExpand();
            }
        }

        private void SnapToggleExpand()
        {
            //Anthem.Manager.AddScriptForClientSideEval(SnapDividas.ClientID + ".ToggleExpand();");
            if (this.SnapEndereco.Visible)
                Anthem.Manager.AddScriptForClientSideEval(SnapEndereco.ClientID + ".ToggleExpand();");
            if (this.SnapTelefone.Visible)
                Anthem.Manager.AddScriptForClientSideEval(SnapTelefone.ClientID + ".ToggleExpand();");
            if (this.SnapEmail.Visible)
                Anthem.Manager.AddScriptForClientSideEval(SnapEmail.ClientID + ".ToggleExpand();");
        }

        protected void btnCadastroAlertas_Click(object sender, EventArgs e)
        {
            this.dgAcionamentos.Visible = false;
            this.dgAcionamentos.UpdateAfterCallBack = true;
            this.SnapToggleExpand();
            this.MessageBox.Altura = 230;
            this.MessageBox.Largura = 450;
            this.MessageBox.ModalShow("CadastroAlertas.aspx", true);
        }
    }
}
