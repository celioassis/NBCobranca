using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using NBCobranca.Classes;
using NBCobranca.Tipos;
using NBCobranca.Controllers;
using NBCobranca.Interfaces;

namespace NBCobranca.aspx
{
    /// <summary>
    /// Cadastro das Seguintes entidades: Devedores e Clientes.
    /// </summary>
    public partial class Cadastro_Entidades : FrmBase, IpresCadastroEntidades, IpresSite, IpresDivida, IpresBaixa
    {
        private ctrCadastroEntidades aCtrCadEntidades;

        #region --- Eventos de Inicialização ---

        protected override void OnInit(EventArgs e)
        {
            this.aPaginaAbertaEmJanelaModal = true;
            base.OnInit(e);
            if (MsgBox.FechandoModal)
                return;
            this.aCtrCadEntidades = this.aController.ctrCadEntidades;
            this.aCtrCadEntidades.SetView(this);
        }

        protected override void OnUnload(EventArgs e)
        {
            if (this.aCtrCadEntidades != null)
                this.aCtrCadEntidades.SetView(null);
            base.OnUnload(e);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
            int mCount = 0;
            foreach (TipoColecoes mColecao in Enum.GetValues(typeof(TipoColecoes)))
            {
                if (mColecao != TipoColecoes.DividasVencidas && mColecao != TipoColecoes.Todos)
                {
                    string mNomeLinkButton = string.Format("lbtn_Novo_{0}", mColecao);
                    LinkButton mLinkButton = this.FindControl(mNomeLinkButton) as LinkButton;
                    ComponentArt.Web.UI.Snap mSnapColecao = this.FindControl("Snap" + mColecao.ToString()) as ComponentArt.Web.UI.Snap;

                    mCount = this.aCtrCadEntidades.Count(mColecao);
                    if (mCount >= 1 && mLinkButton.Text.ToUpper() == "MAIS")
                    {
                        mSnapColecao.Visible = true;
                        Label lblSnapTitulo = mSnapColecao.FindControl("lblTituloSnap" + mColecao.ToString().Substring(0, 3)) as Label;
                        lblSnapTitulo.Text = string.Format("{0}: {1}", lblSnapTitulo.Text.Split(':')[0], mCount);
                    }
                    else if (mCount == 1 && mLinkButton.Text.ToUpper() == "SALVAR")
                        mSnapColecao.Visible = false;
                }
            }
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            MsgBox.ManterScroll = true;
            this.MostraTabelaPessoaFJ(((IpresCadastroEntidades)this).TipoPessoa);
            if (!this.IsPostBack)
            {
                if (!this.aCtrCadEntidades.Editando)
                {
                    lbtn_Novo_Endereco.Text = "Salvar";
                    lbtn_Novo_Telefone.Text = "Salvar";
                    lbtn_Novo_Email.Text = "Salvar";
                    lbtn_Novo_Site.Text = "Salvar";
                }
                else
                {
                    this.radPessoaF.Disabled = true;
                    this.radPessoaJ.Disabled = true;
                }
                this.pnlDividas.Visible = this.aCtrCadEntidades.TipoEntidade == TipoEntidades.Devedores;
            }
        }

        #endregion

        protected void imgBtnSalvar_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                foreach (string mNomeColecao in Enum.GetNames(typeof(TipoColecoes)))
                {
                    if (mNomeColecao != "Todos" && mNomeColecao != "DividasVencidas")
                    {
                        string mNomeLinkButton = "lbtn_Novo_" + mNomeColecao;
                        LinkButton mLinkButton = this.FindControl(mNomeLinkButton) as LinkButton;
                        if (mLinkButton != null && mLinkButton.Text.ToUpper() == "SALVAR")
                            this.LinkButton_Click(mLinkButton, new EventArgsSalvarEntidade());
                    }
                }

                this.aCtrCadEntidades.Salvar();
                this.MsgBox.ShowConfirma("Os dados cadastrais foram salvo com sucesso, deseja realizar um novo cadastro?", "NovaEntidade", true, true);
            }
            catch (ExceptionCampoObrigatorio exCampo)
            {
                this.MsgBox.Show(string.Format("O preenchimento do campo {0} é obrigatório para que {1} possa ser salvo.", exCampo.CampoObrigatorio, this.aCtrCadEntidades.TipoEntidade));
            }
            catch (Exception ex)
            {
                this.MsgBox.Show(ex.Message);
            }
        }

        protected void LinkButton_Click(object sender, EventArgs e)
        {
            bool mFromSalvarEntidade = e is EventArgsSalvarEntidade;
            TipoAcaoLinkButtons mAcao = TipoAcaoLinkButtons.Novo;
            TipoColecoes mColecao = TipoColecoes.Todos;
            LinkButton mLinkButton = (LinkButton)sender;
            LinkButton mLbtnCancelar = new LinkButton();
            LinkButton mLinkAutoParcelar = (LinkButton)this.FindControl("lbtn_AutoParcelar_Dividas");
            LinkButton mLinkExcluir = (LinkButton)this.FindControl("lbtn_Excluir_Dividas");
            string[] mDefinicaoLinkButton = mLinkButton.ID.Split('_');

            try
            {
                mAcao = (TipoAcaoLinkButtons)Enum.Parse(typeof(TipoAcaoLinkButtons), mDefinicaoLinkButton[1], true);
                mColecao = (TipoColecoes)Enum.Parse(typeof(TipoColecoes), mDefinicaoLinkButton[2], true);

                if (mLinkButton.Text.ToUpper() == "SALVAR") mAcao = TipoAcaoLinkButtons.Salvar;

                if (mAcao == TipoAcaoLinkButtons.Novo || mAcao == TipoAcaoLinkButtons.Salvar)
                {
                    string mNomeLinkButtonCancelar = "lbtn_Cancelar_" + mDefinicaoLinkButton[2];
                    mLbtnCancelar = (LinkButton)this.FindControl(mNomeLinkButtonCancelar);
                }

                #region === Ações exclusivas para dívidas ===
                if (mAcao == TipoAcaoLinkButtons.Excluir)
                {
                    this.MsgBox.ShowConfirma("Deseja realmente excluir todas as dívidas?", "Dividas|Todas", true, false);
                    return;
                }
                #endregion

                this.aCtrCadEntidades.AcaoLinkButton(mAcao, mColecao, mFromSalvarEntidade);

                switch (mAcao)
                {
                    case TipoAcaoLinkButtons.Novo:
                        mLbtnCancelar.Visible = true;
                        mLinkButton.Text = "Salvar";
                        break;
                    case TipoAcaoLinkButtons.Salvar:
                        mLbtnCancelar.Visible = false;
                        mLinkButton.Text = "Mais";
                        break;
                    case TipoAcaoLinkButtons.Cancelar:
                        LinkButton mLinkButtonMais = (LinkButton)this.FindControl("lbtn_Novo_" + mDefinicaoLinkButton[2]);
                        mLinkButtonMais.Text = "Mais";
                        mLinkButton.Visible = false;
                        break;
                    case TipoAcaoLinkButtons.AutoParcelar:
                        string mTitulo = HttpUtility.UrlEncode("Parcelamento Automático");
                        string mDescricao = HttpUtility.UrlEncode("Número de Parcelas");
                        string mHelp = HttpUtility.UrlEncode("Será Criado uma dívida para cada parcela com base no Valor Nominal e na Data de Vencimento que será incrementada em um Mês, mantendo o Dia.");
                        this.MsgBox.ModalShow(string.Format("ModalInput.aspx?Titulo={0}&Descricao={1}&Help={2}", mTitulo, mDescricao, mHelp), true);
                        this.MsgBox.ManterScroll = false;
                        return;
                }
                if (mColecao == TipoColecoes.Dividas)
                {
                    bool mmAcao = mAcao == TipoAcaoLinkButtons.Novo;
                    mLinkAutoParcelar.Visible = (mmAcao || mAcao == TipoAcaoLinkButtons.AutoParcelar || mAcao == TipoAcaoLinkButtons.Excluir);
                    mLinkExcluir.Visible = !mmAcao;
                    SnapEndereco.IsCollapsed = mmAcao;
                    SnapTelefone.IsCollapsed = mmAcao;
                    SnapEmail.IsCollapsed = mmAcao;
                    SnapSite.IsCollapsed = mmAcao;
                }
            }
            catch (Exception ex)
            {
                if (mFromSalvarEntidade)
                    throw;
                this.MsgBox.Show(ex.Message);
            }
        }

        protected void Colecao_RowEditing(object sender, GridViewEditEventArgs e)
        {
            try
            {
                e.Cancel = true; //A ação foi cancelada para evitar que a gridview mude a linha para modo de edição colocando inputs dentro das tds.
                GridView mGV = (GridView)sender;
                TipoColecoes mColecao = (TipoColecoes)Enum.Parse(typeof(TipoColecoes), mGV.ID.Replace("gvSnap_", ""), true);
                this.aCtrCadEntidades.EditarColecao(mColecao, mGV.DataKeys[e.NewEditIndex].Value.ToString());
                LinkButton mLbtn_Novo = (LinkButton)this.FindControl("lbtn_Novo_" + mColecao.ToString());
                LinkButton mLbtn_Cancelar = (LinkButton)this.FindControl("lbtn_Cancelar_" + mColecao.ToString());
                mLbtn_Novo.Text = "Salvar";
                mLbtn_Cancelar.Visible = true;
                bool mIsDivida = mColecao == TipoColecoes.Dividas;
                if (mIsDivida)
                {
                    lbtn_Excluir_Dividas.Visible = false;
                    SnapEndereco.IsCollapsed = mIsDivida;
                    SnapTelefone.IsCollapsed = mIsDivida;
                    SnapEmail.IsCollapsed = mIsDivida;
                    SnapSite.IsCollapsed = mIsDivida;
                    
                }

            }
            catch (Exception ex)
            {
                this.MsgBox.Show(ex.Message);
            }
        }

        protected void Colecao_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            e.Cancel = true;
            GridView mGV = (GridView)sender;
            string mKeyValor;
            TipoColecoes mColecao = (TipoColecoes)Enum.Parse(typeof(TipoColecoes), mGV.ID.Replace("gvSnap_", ""), true);
            mKeyValor = string.Format("{0}|{1}", mColecao, mGV.DataKeys[e.RowIndex].Value);
            GridViewRow mGVR = mGV.Rows[e.RowIndex];
            switch (mColecao)
            {
                case TipoColecoes.Endereco:
                    this.MsgBox.ShowConfirma(string.Format("Deseja realmente excluir o endereço {0} ?", HttpUtility.HtmlDecode(mGVR.Cells[0].Text)), mKeyValor, true, false);
                    break;
                case TipoColecoes.Telefone:
                    this.MsgBox.ShowConfirma(string.Format("Deseja realmente excluir o telefone ({0}){1} ?", HttpUtility.HtmlDecode(mGVR.Cells[2].Text), HttpUtility.HtmlDecode(mGVR.Cells[3].Text)), mKeyValor, true, false);
                    break;
                case TipoColecoes.Email:
                    this.MsgBox.ShowConfirma(string.Format("Deseja realmente excluir o email {0} ?", HttpUtility.HtmlDecode(mGVR.Cells[1].Text)), mKeyValor, true, false);
                    break;
                case TipoColecoes.Site:
                    this.MsgBox.ShowConfirma(string.Format("Deseja realmente excluir o site {0} ?", HttpUtility.HtmlDecode(mGVR.Cells[1].Text)), mKeyValor, true, false);
                    break;
                case TipoColecoes.Dividas:
                    this.MsgBox.ShowConfirma(string.Format("Deseja realmente excluir a divida do tipo {0} de contrato {1} e numero de documento {2} ?", HttpUtility.HtmlDecode(mGVR.Cells[1].Text), HttpUtility.HtmlDecode(mGVR.Cells[2].Text), HttpUtility.HtmlDecode(mGVR.Cells[3].Text)), mKeyValor, true, false);
                    break;
            }
        }

        protected void MsgBox_YesChoosed(object sender, string Key)
        {
            if (Key.Contains("EntidadeJaExiste"))
            {
                this.aCtrCadEntidades.CarregaEntidadeQueJaExiste();
                this.Response.Redirect(this.Request.RawUrl);
            }
            if (Key.Contains("NovaEntidade"))
            {
                this.aCtrCadEntidades.NovaEntidade();
                this.Response.Redirect(this.Request.RawUrl);
            }
            {
                string[] mKeyValor = Key.Split('|');
                TipoColecoes mColecao = (TipoColecoes)Enum.Parse(typeof(TipoColecoes), mKeyValor[0], true);
                this.aCtrCadEntidades.ExcluirColecao(mColecao, mKeyValor[1]);
                if (mColecao == TipoColecoes.Dividas)
                    lbtn_Excluir_Dividas.Visible = this.aCtrCadEntidades.Count(mColecao) > 0;
            }
        }

        protected void MsgBox_NoChoosed(object sender, string Key)
        {
            if (Key.Contains("NovaEntidade"))
                this.MsgBox.ModalClose(true);
        }

        protected void gvSnap_Dividas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (!this.aCtrCadEntidades.Editando)
            {
                if (e.Row.RowType == DataControlRowType.Header)
                    ((GridView)sender).Columns[6].Visible = false;
                if (e.Row.RowType == DataControlRowType.DataRow)
                    ((ImageButton)e.Row.Cells[7].FindControl("imgBtnBaixar")).Visible = false;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Entidades.entCOBR_Divida mDivida = e.Row.DataItem as Entidades.entCOBR_Divida;
                ImageButton mImgBtnEditar = e.Row.Cells[7].FindControl("imgBtnEditar") as ImageButton;
                if (mDivida.Baixada || mDivida.BaixaParcial)
                    e.Row.ToolTip = string.Format("O Valor nominal e data de vencimento desta dívida não poderão ser alterados porque já foi {0} baixada.", mDivida.Baixada ? "totalmente" : "parcialmente");
                if (mDivida.Baixada)
                    e.Row.Style[HtmlTextWriterStyle.Color] = "blue";
            }
        }

        protected void MsgBox_CloseModalChoosed(object sender, string Key, string pValorRetorno)
        {
            try
            {
                this.aCtrCadEntidades.CriarAutoParcelamentoDividas(Convert.ToInt32(pValorRetorno));
                this.LinkButton_Click(lbtn_Novo_Dividas, null);
            }
            catch (Exception ex)
            {
                this.MsgBox.Show(ex.Message);
            }
        }

        protected void VerificarCadastroExistente_TextChanged(object sender, EventArgs e)
        {
            try
            {
                TextBox mTxt = sender as TextBox;
                if (mTxt.ID.Contains("CPF") || mTxt.ID.Contains("CNPJ"))
                    this.aCtrCadEntidades.ValidarCPFCNPJ();
                else
                    this.aCtrCadEntidades.VerificaSeEntidadeJaExiste(true);
            }
            catch (ExceptionEntidadeJaExiste exEntJaExiste)
            {
                this.MsgBox.ShowConfirma(exEntJaExiste.Message, "EntidadeJaExiste", true, false);
            }
            catch (Exception ex)
            {
                this.MsgBox.Show(ex.Message);
            }
        }

        #region --- Eventos da Baixa Unica ---

        protected void gvSnap_Dividas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName != "Baixar")
                return;
            try
            {
                GridView mGrid = (GridView)sender;
                string mKey = mGrid.DataKeys[Convert.ToInt32(e.CommandArgument)].Value.ToString();
                this.LimpaBaixaUnica();
                this.aCtrCadEntidades.SelecionarDividaParaBaixaUnica(mKey);
                this.mvCadastro.SetActiveView(vBaixaUnica);
            }
            catch (Exception ex)
            {
                this.MsgBox.Show(ex.Message);
            }
        }

        protected void btnBaixar_Click(object sender, EventArgs e)
        {
            try
            {
                this.aCtrCadEntidades.BaixarDividaUnica();
                this.mvCadastro.SetActiveView(vCadastro);
                this.MsgBox.Show("Baixa realizada com sucesso", txaAnotacoesAdicionais_TUD.ClientID);
            }
            catch (Exception Ex)
            {
                MsgBox.Show(Ex.Message);
            }

        }

        protected void btnCancelarBaixa_Click(object sender, EventArgs e)
        {
            this.lblTituloCadastro.Text = "Cadastro de " + this.aCtrCadEntidades.TipoEntidade.ToString();
            this.mvCadastro.SetActiveView(vCadastro);
        }

        protected void ckbBaixaParcial_CheckedChanged(object sender, System.EventArgs e)
        {
            if (this.ckbBaixaParcial.Checked)
            {
                this.txtValorBaixa_MOEDA.ReadOnly = false;
                this.MsgBox.MoverFoco(txtValorBaixa_MOEDA.ClientID);
            }
            else
                this.txtValorBaixa_MOEDA.ReadOnly = true;
        }

        protected void ckbPagouNoCliente_CheckedChanged(object sender, EventArgs e)
        {
            this.ddlCobrador.Enabled = !this.ckbPagouNoCliente.Checked;
            this.txtPerComissao_MOEDA.Enabled = !this.ckbPagouNoCliente.Checked;
            this.txtValorRecebido_MOEDA.Enabled = !this.ckbPagouNoCliente.Checked;
            this.txtNumRecibo_INT.Enabled = !this.ckbPagouNoCliente.Checked;
            if (this.ckbPagouNoCliente.Checked)
                this.txtValorRecebido_MOEDA.Text = txtValorBaixa_MOEDA.Text;
        }

        protected void txtValorBaixa_MOEDA_TextChanged(object sender, EventArgs e)
        {
            this.aCtrCadEntidades.RecalcularJuros_BaixaUnica();
            this.txtValorRecebido_MOEDA.Focus();
        }

        #endregion

        #region --- Métodos Auxiliares ---

        /// <summary>
        /// Mostra qual tabela esta em uso se é com os dados da Pessoa Física 
        /// ou Pessoa Jurídica.
        /// </summary>
        /// <param name="PessoaJuridica">
        /// Ser for true será Pessoa Jurídica caso seja false, será Pessoa Física.
        /// </param>
        private void MostraTabelaPessoaFJ(Tipos.TipoPessoa PessoaFisica)
        {
            switch (PessoaFisica)
            {
                case TipoPessoa.Fisica:
                    tabPF.Style.Remove("DISPLAY");
                    tabPJ.Style.Add("DISPLAY", "none");
                    break;
                case TipoPessoa.Juridica:
                    tabPJ.Style.Remove("DISPLAY");
                    tabPF.Style.Add("DISPLAY", "none");
                    break;
            }
            this.tblPF_Baixa.Visible = radPessoaF.Checked;
            this.tblPJ_Baixa.Visible = radPessoaJ.Checked;
        }

        /// <summary>
        /// Realiza a limpeza dos campos referente a baixa única.
        /// </summary>
        private void LimpaBaixaUnica()
        {
            this.ckbBaixaParcial.Checked = false;
            this.ckbPagouNoCliente.Checked = false;
            this.ddlCobrador.Enabled = true;
            this.txtPerComissao_MOEDA.Enabled = true;
            this.txtPerComissao_MOEDA.Text = "";
            this.txtBordero_INT.Text = "";
            this.txtDataBaixa_DATA.Text = "";
            this.txtNumRecibo_INT.Enabled = true;
            this.txtNumRecibo_INT.Text = "";
            this.txtValorBaixa_MOEDA.Text = "";
            this.txtValorRecebido_MOEDA.Enabled = true;
            this.txtValorRecebido_MOEDA.Text = "";
        }

        #endregion

        #region IpresCadastroEntidades Members

        bool IpresCadastroEntidades.IsPostBack
        {
            get { return this.IsPostBack; }
        }

        string IpresCadastroEntidades.TituloCadastro
        {
            set { lblTituloCadastro.Text = value; }
        }

        string IpresCadastroEntidades.LabelNomeEntidade
        {
            set
            {
                lblNomeEntidade.Text = value;
                lblNomeEntidade_Baixa.Text = value;
            }
        }

        void IpresCadastroEntidades.AtualizaDataSourceColecoes(ICollection pDataSource, TipoColecoes pColecao)
        {
            try
            {
                GridView mGridView = null;

                switch (pColecao)
                {
                    case TipoColecoes.Endereco:
                        mGridView = (GridView)this.SnapEndereco.FindControl("gvSnap_" + pColecao.ToString());
                        this.SnapEndereco.Visible = pDataSource.Count > 0;
                        break;
                    case TipoColecoes.Telefone:
                        mGridView = (GridView)this.SnapTelefone.FindControl("gvSnap_" + pColecao.ToString());
                        this.SnapTelefone.Visible = pDataSource.Count > 0;
                        break;
                    case TipoColecoes.Email:
                        mGridView = (GridView)this.SnapEmail.FindControl("gvSnap_" + pColecao.ToString());
                        this.SnapEmail.Visible = pDataSource.Count > 0;
                        break;
                    case TipoColecoes.Site:
                        mGridView = (GridView)this.SnapSite.FindControl("gvSnap_" + pColecao.ToString());
                        this.SnapSite.Visible = pDataSource.Count > 0;
                        break;
                    case TipoColecoes.Dividas:
                        mGridView = (GridView)this.SnapDividas.FindControl("gvSnap_" + pColecao.ToString());
                        this.SnapDividas.Visible = pDataSource.Count > 0;
                        break;
                    case TipoColecoes.Acionamentos:
                        mGridView = (GridView)this.SnapAcionamentos.FindControl("gvSnap_" + pColecao.ToString());
                        this.SnapAcionamentos.Visible = pDataSource.Count > 0;
                        break;
                }
                mGridView.DataSource = pDataSource;
                mGridView.DataBind();
                if (mGridView.Rows.Count == 1 && !IsPostBack && pColecao != TipoColecoes.Dividas && pColecao != TipoColecoes.Acionamentos)
                    this.Colecao_RowEditing(mGridView, new GridViewEditEventArgs(0));
            }
            catch (Exception ex)
            {
                this.MsgBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// Bloqueia um conjunto de Campos, divididos em Endereco, Telefone, email e Site.
        /// <br></br>
        /// </summary>
        /// <param name="pColecaoCamposParaBloquear">
        /// Especifica Qual Conjunto de Campos que será Bloqueado ou Não, por Exemplo:
        /// <para>TipoColecoes.Endereco - campos que estão na Tabela de Endereço.</para>
        /// <para>TipoColecoes.Telefone - campos que estão na Tabela de Telefone.</para>
        /// <para>TipoColecoes.Email - campos que estão na Tabela de Emails.</para>
        /// <para>TipoColecoes.Site - campos que estão na Tabela de Sites.</para>
        /// </param>
        /// <param name="Bloquear">
        /// Especifica se os Campos serão Bloqueados (true) ou não (false).
        /// </param>
        void IpresCadastroEntidades.BloquearLimparCampos(Tipos.TipoColecoes pColecaoCamposParaBloquear, bool pBloquear)
        {
            ((IpresCadastroEntidades)this).BloquearLimparCampos(pColecaoCamposParaBloquear, pBloquear, true);
        }

        /// <summary>
        /// Bloqueia um conjunto de Campos, divididos em Endereco, Telefone, email e Site.
        /// <br></br>
        /// </summary>
        /// <param name="pColecaoCamposParaBloquear">
        /// Especifica Qual Conjunto de Campos que será Bloqueado ou Não, por Exemplo:
        /// <para>TipoColecoes.Endereco - campos que estão na Tabela de Endereço.</para>
        /// <para>TipoColecoes.Telefone - campos que estão na Tabela de Telefone.</para>
        /// <para>TipoColecoes.Email - campos que estão na Tabela de Emails.</para>
        /// <para>TipoColecoes.Site - campos que estão na Tabela de Sites.</para>
        /// </param>
        /// <param name="pBloquear">
        /// Especifica se os Campos serão Bloqueados (true) ou não (false).
        /// </param>
        /// <param name="pLimpar">Indica se é para limpar o campos após a ação de bloquear ou desbloquear os campos.</param>
        void IpresCadastroEntidades.BloquearLimparCampos(Tipos.TipoColecoes pColecaoCamposParaBloquear, bool pBloquear, bool pLimpar)
        {
            switch (pColecaoCamposParaBloquear)
            {
                case TipoColecoes.Endereco:
                    txtEndereco_TUD.Disabled = pBloquear;
                    txtEndereco_TUD.Value = pLimpar ? "" : txtEndereco_TUD.Value;
                    txtComplemento_TUD.Disabled = pBloquear;
                    txtComplemento_TUD.Value = pLimpar ? "" : txtComplemento_TUD.Value;
                    txtBairro_TUD.Disabled = pBloquear;
                    txtBairro_TUD.Value = pLimpar ? "" : txtBairro_TUD.Value;
                    txtCEP_CEP.Disabled = pBloquear;
                    txtCEP_CEP.Value = pLimpar ? "" : txtCEP_CEP.Value;
                    txtCidade_STR.Disabled = pBloquear;
                    txtCidade_STR.Value = pLimpar ? "" : txtCidade_STR.Value;
                    selUF.Disabled = pBloquear;
                    selUF.Value = pLimpar ? "SC" : selUF.Value;
                    txtComentarios_TUD.Disabled = pBloquear;
                    txtComentarios_TUD.Value = pLimpar ? "" : txtComentarios_TUD.Value;
                    txtContatoEndereco_TUD.Disabled = pBloquear;
                    txtContatoEndereco_TUD.Value = pLimpar ? "" : txtContatoEndereco_TUD.Value;
                    radEnderecoPrincipal.Disabled = pBloquear;
                    radEnderecoPrincipal.Checked = pLimpar ? false : radEnderecoPrincipal.Checked;
                    if (!pBloquear)
                    {
                        txtEndereco_TUD.Focus();
                        this.lbtn_Novo_Endereco.Text = "Salvar";
                    }
                    break;
                case TipoColecoes.Telefone:
                    txtDescricaoTelefone_TUD.Disabled = pBloquear;
                    txtDescricaoTelefone_TUD.Value = pLimpar ? "" : txtDescricaoTelefone_TUD.Value;
                    txtContatoTelefone_TUD.Disabled = pBloquear;
                    txtContatoTelefone_TUD.Value = pLimpar ? "" : txtContatoTelefone_TUD.Value;
                    txtDDD_INT.Disabled = pBloquear;
                    txtDDD_INT.Value = pLimpar ? "" : txtDDD_INT.Value;
                    txtFone_FONE.Disabled = pBloquear;
                    txtFone_FONE.Value = pLimpar ? "" : txtFone_FONE.Value;
                    txtRamal_INT.Disabled = pBloquear;
                    txtRamal_INT.Value = pLimpar ? "" : txtRamal_INT.Value;
                    if (!pBloquear)
                    {
                        txtDescricaoTelefone_TUD.Focus();
                        this.lbtn_Novo_Telefone.Text = "Salvar";
                    }
                    break;
                case TipoColecoes.Email:
                    txtContatoEmail_STR.Disabled = pBloquear;
                    txtContatoEmail_STR.Value = pLimpar ? "" : txtContatoEmail_STR.Value;
                    txtEmail_EMAIL.Disabled = pBloquear;
                    txtEmail_EMAIL.Value = pLimpar ? "" : txtEmail_EMAIL.Value;
                    if (!pBloquear)
                    {
                        this.lbtn_Novo_Email.Text = "Salvar";
                        txtContatoEmail_STR.Focus();
                    }
                    break;
                case TipoColecoes.Site:
                    txtDescricaoSite_STR.Disabled = pBloquear;
                    txtDescricaoSite_STR.Value = pLimpar ? "" : txtDescricaoSite_STR.Value;
                    txtSite_SITE.Disabled = pBloquear;
                    txtSite_SITE.Value = pLimpar ? "" : txtSite_SITE.Value;
                    if (!pBloquear)
                    {
                        this.lbtn_Novo_Site.Text = "Salvar";
                        this.txtDescricaoSite_STR.Focus();
                    }
                    break;
                case TipoColecoes.Dividas:
                    txtDivContrato_TUD.Disabled = pBloquear;
                    txtDivContrato_TUD.Value = pLimpar ? "" : txtDivContrato_TUD.Value;
                    txtDivNumDoc_INT.Disabled = pBloquear;
                    txtDivNumDoc_INT.Value = pLimpar ? "" : txtDivNumDoc_INT.Value;
                    ddlDivTipoDivida.Enabled = !pBloquear;
                    ddlDivTipoDivida.SelectedIndex = pLimpar ? -1 : ddlDivTipoDivida.SelectedIndex;
                    txtDivDataVencimento_DATA.Disabled = pBloquear;
                    txtDivDataVencimento_DATA.Value = pLimpar ? "" : txtDivDataVencimento_DATA.Value;
                    txtDivValorNominal_MOEDA.Disabled = pBloquear;
                    txtDivValorNominal_MOEDA.Value = pLimpar ? "" : txtDivValorNominal_MOEDA.Value;
                    if (!pBloquear)
                    {
                        this.lbtn_Novo_Dividas.Text = "Salvar";
                        this.txtDivContrato_TUD.Focus();
                    }
                    break;
                case TipoColecoes.Todos:
                    IpresCadastroEntidades mView = this;
                    mView.BloquearLimparCampos(TipoColecoes.Endereco, pBloquear, pLimpar);
                    mView.BloquearLimparCampos(TipoColecoes.Telefone, pBloquear, pLimpar);
                    mView.BloquearLimparCampos(TipoColecoes.Email, pBloquear, pLimpar);
                    mView.BloquearLimparCampos(TipoColecoes.Site, pBloquear, pLimpar);
                    mView.BloquearLimparCampos(TipoColecoes.Dividas, pBloquear, pLimpar);
                    break;
            }

        }

        #endregion

        #region IpresEntidade Members

        TipoPessoa IpresEntidade.TipoPessoa
        {
            get
            {
                if (this.radPessoaJ.Checked)
                    return TipoPessoa.Juridica;
                else
                    return TipoPessoa.Fisica;
            }
            set
            {
                if (value == TipoPessoa.Juridica) this.radPessoaJ.Checked = true;
                if (value == TipoPessoa.Fisica) this.radPessoaF.Checked = true;
            }
        }

        string IpresEntidade.TextoRespeito
        {
            get
            {
                return this.txaAnotacoesAdicionais_TUD.Value;
            }
            set
            {
                this.txaAnotacoesAdicionais_TUD.Value = value;
            }
        }

        string IpresEntidade.NomeRazaoSocial
        {
            get
            {
                if (this.radPessoaF.Checked)
                    return this.txtClienteFornecedor_STR.Text;
                else
                    return this.txtRazaoSocial_TUD.Text;
            }
            set
            {
                if (this.radPessoaF.Checked)
                {
                    this.txtClienteFornecedor_STR.Text = value;
                    this.txtClienteFornecedor_Baixa.Text = value;
                }
                if (this.radPessoaJ.Checked)
                {
                    this.txtRazaoSocial_TUD.Text = value;
                    this.txtRazaoSocial_Baixa.Text = value;
                }
            }
        }

        string IpresEntidade.CPFCNPJ
        {
            get
            {
                if (((IpresEntidade)this).TipoPessoa == TipoPessoa.Fisica)
                    return this.txtCPF_CPF.Text;
                else
                    return this.txtCNPJ_CNPJ.Text;
            }
            set
            {
                if (((IpresEntidade)this).TipoPessoa == TipoPessoa.Fisica)
                {
                    this.txtCPF_CPF.Text = NBFuncoes.FormataCPF(value);
                    this.txtCPF_Baixa.Value = NBFuncoes.FormataCPF(value);
                }
                else
                {
                    this.txtCNPJ_CNPJ.Text = NBFuncoes.FormataCNPJ(value);
                    this.txtCNPJ_Baixa.Value = NBFuncoes.FormataCNPJ(value);
                }
            }
        }

        string IpresEntidade.ApelidoNomeFantasia
        {
            get
            {
                if (this.radPessoaJ.Checked)
                    return this.txtNomeFantasia_TUD.Value;
                else
                    return "";
            }
            set
            {
                if (this.radPessoaJ.Checked)
                {
                    this.txtNomeFantasia_TUD.Value = value;
                    this.txtNomeFantasia_Baixa.Value = value;
                }
            }
        }

        string IpresEntidade.RgIE
        {
            get
            {
                if (this.radPessoaJ.Checked)
                    return this.txtInscricaoEstadual_TUD.Value;
                else
                    return this.txtRG_RG.Value;
            }
            set
            {
                if (this.radPessoaJ.Checked)
                {
                    this.txtInscricaoEstadual_TUD.Value = value;
                    this.txtInscricaoEstadual_Baixa.Value = value;
                }
                else
                {
                    this.txtRG_RG.Value = value;
                    this.txtRG_Baixa.Value = value;
                }
            }
        }

        #endregion

        #region IpresEndereco Members

        string IpresEndereco.Logradouro
        {
            get
            {
                return this.txtEndereco_TUD.Value;
            }
            set
            {
                this.txtEndereco_TUD.Value = value;
            }
        }

        string IpresEndereco.complemento
        {
            get
            {
                return this.txtComplemento_TUD.Value;
            }
            set
            {
                this.txtComplemento_TUD.Value = value;
            }
        }

        string IpresEndereco.Bairro
        {
            get
            {
                return this.txtBairro_TUD.Value;
            }
            set
            {
                this.txtBairro_TUD.Value = value;
            }
        }

        string IpresEndereco.CEP
        {
            get
            {
                return this.txtCEP_CEP.Value;
            }
            set
            {
                this.txtCEP_CEP.Value = value;
            }
        }

        string IpresEndereco.Municipio
        {
            get
            {
                return this.txtCidade_STR.Value;
            }
            set
            {
                this.txtCidade_STR.Value = value;
            }
        }

        string IpresEndereco.UF
        {
            get
            {
                return this.selUF.Value;
            }
            set
            {
                this.selUF.Value = value;
            }
        }

        string IpresEndereco.Comentario
        {
            get
            {
                return this.txtComentarios_TUD.Value;
            }
            set
            {
                this.txtComentarios_TUD.Value = value;
            }
        }

        string IpresEndereco.Contato
        {
            get
            {
                return this.txtContatoEndereco_TUD.Value;
            }
            set
            {
                this.txtContatoEndereco_TUD.Value = value;
            }
        }

        bool IpresEndereco.Principal
        {
            get
            {
                return this.radEnderecoPrincipal.Checked;
            }
            set
            {
                this.radEnderecoPrincipal.Checked = value;
            }
        }
        #endregion

        #region IpresTelefone Members

        string IpresTelefone.Fone_Descricao
        {
            get
            {
                return this.txtDescricaoTelefone_TUD.Value;
            }
            set
            {
                this.txtDescricaoTelefone_TUD.Value = value;
            }
        }

        string IpresTelefone.DDD
        {
            get
            {
                return this.txtDDD_INT.Value;
            }
            set
            {
                this.txtDDD_INT.Value = value;
            }
        }

        string IpresTelefone.Fone
        {
            get
            {
                return this.txtFone_FONE.Value;
            }
            set
            {
                this.txtFone_FONE.Value = value;
            }
        }

        string IpresTelefone.Ramal
        {
            get
            {
                return this.txtRamal_INT.Value;
            }
            set
            {
                this.txtRamal_INT.Value = value;
            }
        }

        string IpresTelefone.Fone_Contato
        {
            get
            {
                return this.txtContatoTelefone_TUD.Value;
            }
            set
            {
                this.txtContatoTelefone_TUD.Value = value;
            }
        }

        #endregion

        #region IpresEmail Members

        string IpresEmail.eMail
        {
            get
            {
                return this.txtEmail_EMAIL.Value;
            }
            set
            {
                this.txtEmail_EMAIL.Value = value;
            }
        }

        string IpresEmail.eMail_Descricao
        {
            get
            {
                return this.txtContatoEmail_STR.Value;
            }
            set
            {
                this.txtContatoEmail_STR.Value = value;
            }
        }

        #endregion

        #region IpresSite Members

        string IpresSite.Url
        {
            get
            {
                return this.txtSite_SITE.Value;
            }
            set
            {
                this.txtSite_SITE.Value = value;
            }
        }

        string IpresSite.Url_Descricao
        {
            get
            {
                return this.txtDescricaoSite_STR.Value;
            }
            set
            {
                this.txtDescricaoSite_STR.Value = value;
            }
        }

        #endregion

        #region IpresDivida Members

        int IpresDivida.TipoDivida
        {
            get
            {
                return Convert.ToInt32(ddlDivTipoDivida.SelectedValue);
            }
            set
            {
                ddlDivTipoDivida.SelectedValue = value.ToString();
            }
        }

        string IpresDivida.Contrato
        {
            get
            {
                return this.txtDivContrato_TUD.Value;
            }
            set
            {
                this.txtDivContrato_TUD.Value = value;
            }
        }

        int IpresDivida.NumeroDocumento
        {
            get
            {
                if (string.IsNullOrEmpty(txtDivNumDoc_INT.Value))
                    throw new Exception("O número do documento da dívida é inválido");
                return Convert.ToInt32(txtDivNumDoc_INT.Value);
            }
            set
            {
                this.txtDivNumDoc_INT.Value = value.ToString();
            }
        }

        DateTime IpresDivida.DataVencimento
        {
            get
            {
                if (string.IsNullOrEmpty(txtDivDataVencimento_DATA.Value))
                    throw new Exception("Não foi definido uma Data de Vencimento para a dívida.");
                return Convert.ToDateTime(txtDivDataVencimento_DATA.Value);
            }
            set
            {
                txtDivDataVencimento_DATA.Value = value.ToString("dd/MM/yyyy");
            }
        }

        double IpresDivida.ValorNominal
        {
            get
            {
                if (string.IsNullOrEmpty(txtDivValorNominal_MOEDA.Value))
                    throw new Exception("O valor nominal da dívida é inválido");
                return Convert.ToDouble(this.txtDivValorNominal_MOEDA.Value);
            }
            set
            {
                this.txtDivValorNominal_MOEDA.Value = value.ToString("N2");
            }
        }

        string IpresDivida.DescricaoTipoDivida
        {
            get { return this.ddlDivTipoDivida.SelectedItem.Text; }
        }

        void IpresDivida.SetDataSourceTipoDivida(System.Data.DataTable pDataSource)
        {
            this.ddlDivTipoDivida.DataSource = pDataSource;
            this.ddlDivTipoDivida.DataBind();
        }

        #endregion

        #region IpresBaixa Members

        bool IpresBaixa.PagouNoCliente
        {
            get
            {
                return this.ckbPagouNoCliente.Checked;
            }
            set
            {
                this.ckbPagouNoCliente.Checked = value;
            }
        }

        bool IpresBaixa.BaixaParcial
        {
            get
            {
                return this.ckbBaixaParcial.Checked;
            }
            set
            {
                this.ckbBaixaParcial.Checked = value;
            }
        }

        string IpresBaixa.Cobrador
        {
            get
            {
                return ddlCobrador.SelectedValue;
            }
            set
            {
                ddlCobrador.SelectedValue = value;
            }
        }

        double IpresBaixa.Comissao
        {
            get
            {
                return string.IsNullOrEmpty(txtPerComissao_MOEDA.Text) ? 0 : Convert.ToDouble(txtPerComissao_MOEDA.Text);
            }
            set
            {
                txtPerComissao_MOEDA.Text = value.ToString("N2");
            }
        }

        int IpresBaixa.Bordero
        {
            get
            {
                return string.IsNullOrEmpty(txtBordero_INT.Text) ? 0 : Convert.ToInt32(txtBordero_INT.Text);
            }
            set
            {
                this.txtBordero_INT.Text = value.ToString();
            }
        }

        DateTime IpresBaixa.DataBaixa
        {
            get
            {
                return Convert.ToDateTime(txtDataBaixa_DATA.Text);
            }
            set
            {
                this.txtDataBaixa_DATA.Text = value.ToString("dd/MM/yyyy");
            }
        }

        int IpresBaixa.Recibo
        {
            get
            {
                return string.IsNullOrEmpty(txtNumRecibo_INT.Text) ? 0 : Convert.ToInt32(this.txtNumRecibo_INT.Text);
            }
            set
            {
                this.txtNumRecibo_INT.Text = value.ToString();
            }
        }

        double IpresBaixa.ValorBaixa
        {
            get
            {
                return string.IsNullOrEmpty(txtValorBaixa_MOEDA.Text) ? 0 : Convert.ToDouble(this.txtValorBaixa_MOEDA.Text);
            }
            set
            {
                this.txtValorBaixa_MOEDA.Text = value.ToString("N2");
            }
        }

        double IpresBaixa.ValorRecebido
        {
            get
            {
                return string.IsNullOrEmpty(txtValorRecebido_MOEDA.Text) ? 0 : Convert.ToDouble(txtValorRecebido_MOEDA.Text);
            }
            set
            {
                txtValorRecebido_MOEDA.Text = value.ToString("N2");
            }
        }

        void IpresBaixa.SetDataSourceCobrador(System.Data.DataTable pDataSource)
        {
            this.ddlCobrador.DataSource = pDataSource;
            this.ddlCobrador.DataBind();
        }

        void IpresBaixa.SetDataSourceBaixa(System.Collections.ICollection pDataSource)
        {
            this.gvDividaBaixar.DataSource = pDataSource;
            this.gvDividaBaixar.DataBind();
        }

        #endregion

    }
}