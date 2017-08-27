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

namespace NBCobranca.aspx
{
    /// <summary>
    /// Cadastro de Clientes e Fornecedores
    /// </summary>
    public partial class entidades_cadastro : FrmBase
    {
        #region === Declaração de Variáveis ===
        protected System.Web.UI.WebControls.DataGrid dgEnderecos;
        protected System.Web.UI.WebControls.DataGrid dgSites;
        protected System.Web.UI.WebControls.DataGrid dgTelefones;
        protected System.Web.UI.WebControls.DataGrid dgEmails;
        protected System.Web.UI.WebControls.DataGrid dgDividas;
        protected System.Web.UI.WebControls.DataGrid dgAcionamentos;
        
        private ctrCadastroEntidades aCtrCadEntidades;

        //Cria um objeto não instanciado do tipo LimEntidades.
        private Classes.LimEntidades obj;
        private Classes.Sistema Sistema;
        public string TituloCadastro = "";

#endregion

        #region === Metodos da Página ===
        protected override void OnInit(EventArgs e)
        {
            this.aPaginaAbertaEmJanelaModal = true;
            base.OnInit(e);
            if (MessageBox.FechandoModal)
                return;
            this.aCtrCadEntidades = this.aController.ctrCadEntidades;
            //this.aCtrBaixas.OnDividaSelecionadaParaBaixar += new NBCobranca.Controllers.ctrBaixas.DividaSelecionadaParaBaixarHandler(aCtrBaixas_OnDividaSelecionadaParaBaixar);
        }

        protected override void OnUnload(EventArgs e)
        {
            //if (this.aCtrBaixas != null)
            //    this.aCtrBaixas.OnDividaSelecionadaParaBaixar -= new NBCobranca.Controllers.ctrBaixas.DividaSelecionadaParaBaixarHandler(aCtrBaixas_OnDividaSelecionadaParaBaixar);
            base.OnUnload(e);
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            this.dgEnderecos = (DataGrid)this.SnapEndereco.FindControl("dgSnapEnderecos");
            this.dgTelefones = (DataGrid)this.SnapTelefone.FindControl("dgSnapTelefones");
            this.dgEmails = (DataGrid)this.SnapEmail.FindControl("dgSnapEmails");
            this.dgSites = (DataGrid)this.SnapSite.FindControl("dgSnapSites");
            this.dgDividas = (DataGrid)this.SnapDividas.FindControl("dgSnapDividas");
            this.dgAcionamentos = (DataGrid)this.SnapAcionamentos.FindControl("dgAcionamentos");

            //Verifica o Tipo da Pessoa, se é Juridica ou Física
            if (!radPessoaF.Checked)
                MostraTabelaPessoaFJ(false);
            else
                MostraTabelaPessoaFJ(true);

            NBFuncoes.ValidarSistema(this, ref Sistema, this.MessageBox);
            if (this.MessageBox.FechandoModal)
                return;
            obj = Sistema.LimEntidades;

            if (!this.pnGeral.Visible)
            {
                this.pnGeral.Visible = true;
                dgDividas.DataSource = obj.ColecaoDataSource(Tipos.TipoColecoes.Dividas);
                dgDividas.DataBind();
            }
            //Verifica se é o Primeiro PostBack
            if (!IsPostBack)
            {
                if (this.Sistema.Legenda.SubTitulo != "Acionamento")
                    this.Sistema.ValidaCredencial();

                this.lblNomeEntidade.Text = this.Sistema.Legenda.SubTitulo;
                switch (this.Sistema.TipoEntidade)
                {
                    case NBdbm.tipos.TipoEntidade.Clientes:
                        this.pnlTarifas.Visible = true;
                        this.pnlDividas.Visible = false;
                        break;
                    case NBdbm.tipos.TipoEntidade.Devedores:
                        if (Sistema.Legenda.SubTitulo != "Acionamento")
                        {
                            this.pnlTarifas.Visible = false;
                            this.pnlDividas.Visible = true;
                            //Preenche o DropDownList de Tipo de Dívidas se a Entidade  for
                            //do Tipo Devedores.
                            this.ddlDivTipoDivida.DataSource = this.obj.TipoDividaDataSource;
                            this.ddlDivTipoDivida.DataBind();
                        }
                        break;
                }
                //Tenta Buscar o id do Cliente/Fornecedor para mostrar os seus dados.
                try
                {
                    if (this.obj.Entidade.ID > 0)
                        MostraDados();
                    else
                    {
                        lbtnMais_Endereco.Text = "Salvar";
                        lbtnMais_Telefone.Text = "Salvar";
                        lbtnMais_Email.Text = "Salvar";
                        lbtnMais_Site.Text = "Salvar";
                        lbtnMais_Dividas.Text = "Salvar";
                        if (this.Sistema.TipoEntidade == NBdbm.tipos.TipoEntidade.Devedores)
                            this.obj.NovaDivida();
                        lbtnParcelasAutomaticas.Visible = true;
                    }
                }
                catch (Exception ex)
                {
                    this.MessageBox.Show(ex.Message);
                }

            }
            TituloCadastro = "Cadastro de " + Sistema.Legenda.Titulo;

        }

        protected void LinkButton_Click(object sender, System.EventArgs e)
        {
            LinkButton mLbtn = (LinkButton)sender;
            string[] mIDlbtn = mLbtn.ID.Split('_');

            switch (mLbtn.Text)
            {
                case "Mais":
                    this.lbtnMais(mIDlbtn[1]);
                    break;
                case "Salvar":
                    this.lbtnSalvar(mIDlbtn[1], sender);
                    break;
                case "Cancelar":
                    this.lbtnCancelar(mIDlbtn[1]);
                    break;
            }

        }

        public void dgEnderecos_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            ItemCommandVerifica(TipoColecoes.Endereco, lbtnMais_Endereco, "txtEndereco_TUD", dgEnderecos, 0, e);
        }

        public void dgTelefones_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            ItemCommandVerifica(TipoColecoes.Telefone, lbtnMais_Telefone, "txtFone_FONE", dgTelefones, 0, e);
        }

        public void dgEmails_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            ItemCommandVerifica(TipoColecoes.Email, lbtnMais_Email, "txtEmail_EMAIL", dgEmails, 0, e);
        }

        public void dgSites_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            ItemCommandVerifica(TipoColecoes.Site, lbtnMais_Site, "txtSite_SITE", dgSites, 0, e);
        }

        public void dgDividas_ItemCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            ItemCommandVerifica(TipoColecoes.Dividas, lbtnMais_Dividas, txtDivDataVencimento_DATA.ClientID, dgDividas, 0, e);
        }

        public void dgDividas_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex >= 0)
            {
                NBdbm.Interfaces.iCOBR.Primitivas.iDivida mDivida = (NBdbm.Interfaces.iCOBR.Primitivas.iDivida)e.Item.DataItem;
                string txt = this.obj.GetDescricaoTipoDivida(mDivida.idTipoDivida);
                e.Item.Cells[1].Text = obj.GetDescricaoCarteira(mDivida.XmPathCliente);
                e.Item.Cells[2].Text = txt;

                if (mDivida.BaixaParcial && !mDivida.Baixada)
                    e.Item.Cells[6].Text = this.Sistema.LimAcionamentos.ValorNominalParcial(mDivida).ToString("C");

                e.Item.Cells[7].Text = this.Sistema.LimBaixa.StatusBaixa(mDivida);

                if (mDivida.Baixada)
                    e.Item.ForeColor = Color.Blue;

            }

        }

        public void dgAcionamentos_ItemDataBound(object sender, System.Web.UI.WebControls.DataGridItemEventArgs e)
        {
            if (e.Item.ItemIndex >= 0)
            {
                string mTxtUsuario = this.Sistema.LimAcionamentos.GetNomeUsuario(int.Parse(e.Item.Cells[0].Text));
                string mTxtTipoAcionamento = this.Sistema.LimAcionamentos.GetDescricaoTipoAcionamento(int.Parse(e.Item.Cells[3].Text));
                e.Item.Cells[0].Text = mTxtUsuario;
                e.Item.Cells[3].Text = mTxtTipoAcionamento;
                if (e.Item.Cells[2].Text == "01/01/0001")
                    e.Item.Cells[2].Text = "";
            }

        }

        //Metodos da MessageBox
        protected void MessageBox_YesChoosed(object sender, string Key)
        {
            switch (Key)
            {
                case "ExcluirEndereco":
                    ItemCommandDelete(TipoColecoes.Endereco, dgEnderecos);
                    break;
                case "ExcluirTelefone":
                    ItemCommandDelete(TipoColecoes.Telefone, dgTelefones);
                    break;
                case "ExcluirEmail":
                    ItemCommandDelete(TipoColecoes.Email, dgEmails);
                    break;
                case "ExcluirSite":
                    ItemCommandDelete(TipoColecoes.Site, dgSites);
                    break;
                case "ExcluirDividas":
                    ItemCommandDelete(TipoColecoes.Dividas, dgDividas);
                    break;
                case "ExcluirTodasDividas":
                    this.obj.ColecaoRemoveTudo(TipoColecoes.Dividas);
                    this.dgDividas.DataSource = this.obj.ColecaoDataSource(TipoColecoes.Dividas);
                    this.dgDividas.DataBind();
                    this.lbltnExcluirTudo.Visible = false;
                    break;
                case "NovoRegistro":
                    this.obj.NovaEntidade(false);
                    Response.Redirect("entidades_cadastro.aspx");
                    break;
                case "NomeJaCadastrado":
                    string tmpIdJaCad = this.obj.IdEntidadeDuplicada;
                    this.obj.IdEntidadeDuplicada = "0";
                    this.obj.Consulta(tmpIdJaCad);
                    Response.Redirect("entidades_cadastro.aspx");
                    break;
                case "Acionamento":
                    this.Sistema.LimAcionamentos.GetDevedor(obj.Entidade.ID);
                    this.Response.Redirect("Acionamento_Ficha.aspx");
                    break;
            }

        }

        protected void MessageBox_NoChoosed(object sender, string Key)
        {
            switch (Key)
            {
                case "Acionamento":
                case "NovoRegistro":
                    MessageBox.ModalClose();
                    break;
                case "NomeJaCadastrado":
                    MessageBox.MoverFoco(this.obj.CampoRecebeFoco);
                    break;
            }

        }

        protected void MessageBox_CloseModalChoosed(object sender, string Key, string pValorRetorno)
        {
            System.DateTime mDtaVencimento = DateTime.Parse(txtDivDataVencimento_DATA.Value);
            int mDocInicial = int.Parse(txtDivNumDoc_INT.Value);
            int mNumParcelas = int.Parse(pValorRetorno);
            try
            {
                for (int a = 1; a <= mNumParcelas; a++)
                {
                    this.obj.NovaDivida();
                    if (a > 1)
                    {
                        mDtaVencimento = mDtaVencimento.AddMonths(1);
                        txtDivDataVencimento_DATA.Value = mDtaVencimento.ToShortDateString();
                        mDocInicial++;
                    }
                    txtDivNumDoc_INT.Value = mDocInicial.ToString();
                    this.lbtnSalvar("Dividas", sender);
                }
            }
            catch (NBdbm.COBR_Exception CobrEx)
            {
                MessageBox.Show(CobrEx.Message);
            }
        }

        //Metodo de Gravação
        protected void imgBtnSalvar_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
                #region === Verificação do Tipo de Pessoas ===

                //Verifica se é o Campo radPessoaF esta marcado, se estiver
                //popula os Campos da Entidade com os Dados da Pessoa Física.
                if (radPessoaF.Checked)
                {
                    if (txtClienteFornecedor_STR.Text == "")
                    {
                        //Como é Pessoa Física o Campo acima não pode ficar
                        //em branco, por isso foi executado um excessão para o campo.
                        throw new NBdbm.EVTexception("O Campo Cliente devem ser preenchido", obj.ExceptionSource);
                    }
                    else
                    {
                        obj.Entidade.NomeRazaoSocial_key = txtClienteFornecedor_STR.Text;
                        obj.Entidade.PessoaFisica = true;
                        try
                        {
                            obj.Entidade.CPFCNPJ_key = txtCPF_CPF.Value;
                        }
                        catch
                        {
                            obj.Entidade.CPFCNPJ_key = "0";
                        }
                        obj.Entidade.RgIE = txtRG_RG.Value;
                    }

                }
                //Verifica se é o Campo radPessoaJ esta marcado, se estiver
                //popula os Campos da Entidade com os Dados da Pessoa Jurídica.
                if (radPessoaJ.Checked)
                {
                    if (txtRazaoSocial_TUD.Text == "")
                    {
                        //Como é Pessoa Jurídica o campo acima não pode ficar
                        //em branco, por isso muda o estado da variável para false.
                        throw new NBdbm.EVTexception("O Campo Razão Social devem ser preenchido", obj.ExceptionSource);
                    }
                    else
                    {
                        obj.Entidade.NomeRazaoSocial_key = txtRazaoSocial_TUD.Text;
                        obj.Entidade.ApelidoNomeFantasia = txtNomeFantasia_TUD.Value;
                        obj.Entidade.PessoaFisica = false;
                        try
                        {
                            obj.Entidade.CPFCNPJ_key = txtCNPJ_CNPJ.Value;
                        }
                        catch
                        {
                            obj.Entidade.CPFCNPJ_key = "0";
                        }
                        obj.Entidade.RgIE = txtInscricaoEstadual_TUD.Value;
                    }

                }
                #endregion

                obj.Entidade.TextoRespeito = txaAnotacoesAdicionais_TUD.Value;

                #region Verificações de Endereço
                //Essa Verificação é feita caso o usuário Cadastre um Endereco, 
                //mas não clique no link para Salvar, caso isso ocorra o sistema
                //se adianta e coloca esse endereço na Coleção para que possa ser
                //salvo quando o usuário clicar no botão salvar do Cadastro.

                //Verifica se o atributo Count da Coleção de Endereços é igual a 0
                //e se o Campo txtEndereco_TUD contem dados.
                if (obj.ColecaoCount(Tipos.TipoColecoes.Endereco) == 0 && txtEndereco_TUD.Value != "")
                    this.lbtnSalvar("Endereco", sender);

                    //Esta outra verificação é muito parecida com a anterior, mas neste 
                //caso ela verifique se o atributo Count da Coleção de Endereços é maior
                //que 0, se o Campo txtEndereco_TUD contem dados e Texto do LinkButtonMais
                //é igual a SALVAR.
                else if (obj.ColecaoCount(Tipos.TipoColecoes.Endereco) > 0 && txtEndereco_TUD.Value != "" && lbtnMais_Endereco.Text == "Salvar")
                    this.lbtnSalvar("Endereco", sender);

                    //Esta outra verificação é para evitar que uma entidade seja salva sem endereço.
                else if (obj.ColecaoCount(Tipos.TipoColecoes.Endereco) == 0 && txtEndereco_TUD.Value == "")
                {
                    txtEndereco_TUD.Value = "Sem Endereço";
                    this.lbtnSalvar("Endereco", sender);
                }

                #endregion

                #region Verificações de Telefones
                //Segue o mesmo estilo de verificação do Endereço

                if (obj.ColecaoCount(Tipos.TipoColecoes.Telefone) == 0 && txtFone_FONE.Value != "")
                {
                    this.lbtnSalvar("Telefone", sender);
                }
                else if (obj.ColecaoCount(Tipos.TipoColecoes.Telefone) > 0 && txtFone_FONE.Value != "" && lbtnMais_Telefone.Text == "Salvar")
                {
                    this.lbtnSalvar("Telefone", sender);
                }
                #endregion

                #region Verificações de Emails
                //Segue o mesmo estilo de verificação do Endereço

                if (obj.ColecaoCount(Tipos.TipoColecoes.Email) == 0 && txtEmail_EMAIL.Value != "")
                {
                    this.lbtnSalvar("Email", sender);
                }
                else if (obj.ColecaoCount(Tipos.TipoColecoes.Email) > 0 && txtEmail_EMAIL.Value != "" && lbtnMais_Email.Text == "Salvar")
                {
                    this.lbtnSalvar("Email", sender);
                }
                #endregion

                #region Verificações de Sites
                //Segue o mesmo estilo de verificação do Endereço

                if (obj.ColecaoCount(Tipos.TipoColecoes.Site) == 0 && txtSite_SITE.Value != "")
                {
                    this.lbtnSalvar("Site", sender);
                }
                else if (obj.ColecaoCount(Tipos.TipoColecoes.Site) > 0 && txtSite_SITE.Value != "" && lbtnMais_Site.Text == "Salvar")
                {
                    this.lbtnSalvar("Site", sender);
                }
                #endregion

                #region Verificações de Dividas
                //Segue o mesmo estilo de verificação do Endereço
                if (this.Sistema.TipoEntidade == NBdbm.tipos.TipoEntidade.Devedores)
                {
                    if (obj.ColecaoCount(Tipos.TipoColecoes.Dividas) == 0 && txtDivDataVencimento_DATA.Value != "")
                    {
                        lbtnSalvar("Dividas", sender);
                    }
                    else if (obj.ColecaoCount(Tipos.TipoColecoes.Dividas) > 0 && txtDivDataVencimento_DATA.Value != "" && lbtnMais_Dividas.Text == "Salvar")
                    {
                        lbtnSalvar("Dividas", sender);
                    }
                }
                #endregion

                #region Verifica se a Entidade é do Tipo Cliente
                if (this.Sistema.TipoEntidade == NBdbm.tipos.TipoEntidade.Clientes)
                {
                    this.obj.Tarifas.Juros = double.Parse(txtTarJuros_MOEDA.Value);
                    this.obj.Tarifas.Multa = double.Parse(txtTarMulta_MOEDA.Value);
                }
                #endregion

                if (obj.IdEntidadeDuplicada != "0" && obj.ChaveDuplicada(txtClienteFornecedor_STR.Text, txtCNPJ_CNPJ.Value))
                    throw new NBdbm.EVTexception("Já Existe uma Entidade Cadastrada com esse Nome e CPF/CNPJ.", obj.ExceptionSource);

                #region ==== Procedimento para concluir gravação ===
                //Executa o Metodo Salvar do Objeto
                obj.Salvar();
                if (obj.Alteracao)
                {
                    if (this.Sistema.Legenda.SubTitulo == "Acionamento")
                    {
                        this.MessageBox.ShowConfirma("Alteração concluída com sucesso, Deseja Voltar para a Ficha de Acionamentos ?", "Acionamento", true, true);
                        return;
                    }
                    MessageBox.ShowConfirma(obj.ExceptionSource + "\\r\\rA Alteração desse Registro foi Salva Com Sucesso, deseja continuar incluindo novos Registros? ", "NovoRegistro", true, true);
                }
                else
                    MessageBox.ShowConfirma(obj.ExceptionSource + "\\r\\rA Inclusão do novo registro foi concluida com sucesso, deseja continuar incluindo novos registros? ", "NovoRegistro", true, true);
                #endregion
            }
            catch (NBdbm.COBR_Exception CobrEx)
            {
                //Caso não obtenha sucesso na Gravação será mostrado uma mensagem informando
                //por que não foi possível gravar a entidade.
                if (CobrEx.ClientID != "")
                    MessageBox.Show(CobrEx.Message, CobrEx.ClientID);
                else
                    MessageBox.Show(CobrEx.Message);
            }
        }

        protected void lbtnParcelasAutomaticas_Click(object sender, System.EventArgs e)
        {
            if (txtDivContrato_TUD.Value == "" ||
                txtDivNumDoc_INT.Value == "" ||
                txtDivDataVencimento_DATA.Value == "" ||
                txtDivValorNominal_MOEDA.Value == "")
            {
                MessageBox.Show("Os Campos Contrato, Número do Documento, Data de Vencimento e Valor Nominal, devem Estar Preenchidos para que possa ser feito um parcelamento Automático");
                return;
            }

            string mTitulo = HttpUtility.UrlEncode("Parcelamento Automático");
            string mDescricao = HttpUtility.UrlEncode("Número de Parcelas");
            string mHelp = HttpUtility.UrlEncode("Será Criado uma dívida para cada parcela com base no Valor Nominal e na Data de Vencimento que será incrementada em um Mês, mantendo o Dia.");

            selUF.Visible = false;
            MessageBox.ManterScroll = false;
            //MessageBox.TipoSubmit = NBWebControls.MessageBox.enTipoSubmit.Form;
            MessageBox.ModalShow("ModalInput.aspx?Titulo=" + mTitulo + "&Descricao=" + mDescricao + "&Help=" + mHelp, true);
        }

        protected void lbltnExcluirTudo_Click(object sender, System.EventArgs e)
        {
            MessageBox.ShowConfirma("Confirma a Exclusão de Todas as Dívidas?", "ExcluirTodasDividas", true, false);
        }

        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            #region ==== Snap Endereço ====
            int TotalEndereco = obj.ColecaoCount(TipoColecoes.Endereco);
            if (TotalEndereco >= 1 && lbtnMais_Endereco.Text.ToUpper() == "MAIS")
            {
                SnapEndereco.Visible = true;
                Label lblSnapTitulo = (Label)SnapEndereco.FindControl("lblTituloSnapEnd");
                lblSnapTitulo.Text = "Lista de Endereços: " + TotalEndereco.ToString();
            }
            #endregion

            #region === Snap Telefone ===
            int TotalTelefone = obj.ColecaoCount(TipoColecoes.Telefone);
            if (TotalTelefone >= 1 && lbtnMais_Telefone.Text.ToUpper() == "MAIS")
            {
                SnapTelefone.Visible = true;
                Label lblSnapTitulo = (Label)SnapTelefone.FindControl("lblTituloSnapTel");
                lblSnapTitulo.Text = "Lista de Telefones: " + TotalTelefone.ToString();
            }
            #endregion

            #region ==== Snap e-mail ====
            int TotalEmail = obj.ColecaoCount(TipoColecoes.Email);
            if (TotalEmail >= 1 && lbtnMais_Email.Text.ToUpper() == "MAIS")
            {
                SnapEmail.Visible = true;
                Label lblSnapTitulo = (Label)SnapEmail.FindControl("lblTituloSnapEma");
                lblSnapTitulo.Text = "Lista de Emails: " + TotalEmail.ToString();
            }
            #endregion

            #region ==== Snap Site ====
            int TotalSite = obj.ColecaoCount(TipoColecoes.Site);
            if (TotalSite >= 1 && lbtnMais_Site.Text.ToUpper() == "MAIS")
            {
                SnapSite.Visible = true;
                Label lblSnapTitulo = (Label)SnapSite.FindControl("lblTituloSnapSit");
                lblSnapTitulo.Text = "Lista de Sites: " + TotalSite.ToString();
            }
            #endregion

            #region ==== Snap Dividas ====
            if (Sistema.TipoEntidade == NBdbm.tipos.TipoEntidade.Devedores)
            {
                int TotalDividas = obj.ColecaoCount(TipoColecoes.Dividas);
                if (TotalDividas >= 1 && lbtnMais_Dividas.Text.ToUpper() == "MAIS")
                {
                    SnapDividas.Visible = true;
                    Label lblSnapTitulo = (Label)SnapDividas.FindControl("lbtnTituloSnapDivida");
                    lblSnapTitulo.Text = "Lista de Dividas: " + TotalDividas.ToString();
                    lbltnExcluirTudo.Visible = true;
                }
            }
            #endregion
        }

        protected void txtClienteFornecedor_STR_TextChanged(object sender, System.EventArgs e)
        {
            string NomeClienteFornecedor = "";
            switch (radPessoaF.Checked)
            {
                case true:
                    NomeClienteFornecedor = txtClienteFornecedor_STR.Text;
                    this.obj.CampoRecebeFoco = txtCPF_CPF.ClientID;
                    break;
                case false:
                    NomeClienteFornecedor = txtRazaoSocial_TUD.Text;
                    this.obj.CampoRecebeFoco = txtNomeFantasia_TUD.ClientID;
                    break;
            }
            if (!obj.NomeEntidadeJaCadastrada(NomeClienteFornecedor, MessageBox))
                MessageBox.MoverFoco(this.obj.CampoRecebeFoco);
        }

        #endregion

        #region === Metodos de Apoio ===
        /// <summary>
        /// Mostra os Dados na página, verificando se é pessoa Física ou Jurídica.
        /// </summary>
        private void MostraDados()
        {

            #region Pessoas Fisicas ou Jurídicas
            MostraTabelaPessoaFJ(obj.Entidade.PessoaFisica);
            if (!obj.Entidade.PessoaFisica)
            {
                //Pessoa Jurídica
                #region Dados Pessoais - Pessoa Jurídica
                txtRazaoSocial_TUD.Text = obj.Entidade.NomeRazaoSocial_key;
                txtNomeFantasia_TUD.Value = obj.Entidade.ApelidoNomeFantasia;
                txtInscricaoEstadual_TUD.Value = obj.Entidade.RgIE;

                //Tenta Atribuir um Valor para o campo txtCPF_CPF caso não
                //obtenha sucesso atribui um valor em branco.
                try
                {
                    if (double.Parse(obj.Entidade.CPFCNPJ_key) > 0)
                        txtCNPJ_CNPJ.Value = double.Parse(obj.Entidade.CPFCNPJ_key).ToString("00'.'000'.'000'/'0000-00");
                }
                catch
                {
                    txtCNPJ_CNPJ.Value = "";
                }

                if (Sistema.Legenda.SubTitulo == "Acionamento")
                {
                    this.txtRazaoSocial_TUD.Enabled = false;
                    this.txtNomeFantasia_TUD.Disabled = true;
                    this.txtInscricaoEstadual_TUD.Disabled = true;
                    this.txtCNPJ_CNPJ.Disabled = true;
                }
                #endregion
            }
            else
            {
                //Pessoa Física
                #region Dados Pessoais - Pessoa Física
                txtClienteFornecedor_STR.Text = obj.Entidade.NomeRazaoSocial_key;

                //Tenta Atribuir um Valor para o campo txtCPF_CPF caso não
                //obtenha sucesso atribui um valor em branco.
                try
                {
                    txtCPF_CPF.Value = double.Parse(obj.Entidade.CPFCNPJ_key).ToString("###'.'###'.'###-##");
                    if (txtCPF_CPF.Value == "..-")
                        txtCPF_CPF.Value = "";
                }
                catch
                {
                    txtCPF_CPF.Value = "";
                }
                txtRG_RG.Value = obj.Entidade.RgIE;

                if (Sistema.Legenda.SubTitulo == "Acionamento")
                {
                    this.txtClienteFornecedor_STR.Enabled = false;
                    this.txtCPF_CPF.Disabled = false;
                    this.txtRG_RG.Disabled = false;
                }

                #endregion
            }

            //Desabilita os RadioButton de Tipo de Pessoa para que o usuário
            //não possa alterar o Tipo da Pessoa.
            radPessoaF.Disabled = true;
            radPessoaJ.Disabled = true;
            #endregion

            #region Endereco
            //Verifica se a Coleção de Endereços esta com mais de um item
            //caso esteja  não preenche os campos do Endereço mas mostra
            //o DataGrid com o DataSource da Coleção.
            //Caso tenha somente 1 item, preenche os campos do Endereço com
            //o único endereço da coleção caso o tenha.
            if (obj.ColecaoCount(TipoColecoes.Endereco) > 1)
            {
                dgEnderecos.DataSource = obj.ColecaoDataSource(TipoColecoes.Endereco);
                dgEnderecos.DataBind();
                BloquearCampos(Tipos.TipoColecoes.Endereco, true);
                SnapEndereco.Visible = true;
            }
            else if (obj.ColecaoCount(TipoColecoes.Endereco) == 1)
            {
                PreencheCampos(TipoColecoes.Endereco);
                lbtnMais_Endereco.Text = "Salvar";
            }
            else
            {
                lbtnMais_Endereco.Text = "Salvar";
            }
            #endregion

            #region Telefone
            //Segue a mesma metodologia do Endereço
            if (obj.ColecaoCount(TipoColecoes.Telefone) > 1)
            {
                dgTelefones.DataSource = obj.ColecaoDataSource(TipoColecoes.Telefone);
                dgTelefones.DataBind();
                BloquearCampos(Tipos.TipoColecoes.Telefone, true);
            }
            else if (obj.ColecaoCount(TipoColecoes.Telefone) == 1)
            {
                PreencheCampos(TipoColecoes.Telefone);
                lbtnMais_Telefone.Text = "Salvar";

            }
            else
            {
                lbtnMais_Telefone.Text = "Salvar";
            }
            #endregion

            #region Emails
            //Segue a mesma metodologia do Endereço
            if (obj.ColecaoCount(TipoColecoes.Email) > 1)
            {
                dgEmails.DataSource = obj.ColecaoDataSource(Tipos.TipoColecoes.Email);
                dgEmails.DataBind();
                BloquearCampos(Tipos.TipoColecoes.Email, true);
            }
            else if (obj.ColecaoCount(TipoColecoes.Email) == 1)
            {
                PreencheCampos(Tipos.TipoColecoes.Email);
                lbtnMais_Email.Text = "Salvar";
            }
            else
            {
                lbtnMais_Email.Text = "Salvar";

            }
            #endregion

            #region Sites
            //Segue a mesma metodologia do Endereço
            if (obj.ColecaoCount(TipoColecoes.Site) > 1)
            {
                dgSites.DataSource = obj.ColecaoDataSource(Tipos.TipoColecoes.Site);
                dgSites.DataBind();
                BloquearCampos(Tipos.TipoColecoes.Site, true);
            }
            else if (obj.ColecaoCount(TipoColecoes.Site) == 1)
            {
                PreencheCampos(Tipos.TipoColecoes.Site);
                lbtnMais_Site.Text = "Salvar";
            }
            else
            {
                lbtnMais_Site.Text = "Salvar";

            }
            #endregion

            #region Dados de Devedores
            if (this.Sistema.TipoEntidade == NBdbm.tipos.TipoEntidade.Devedores && this.Sistema.Legenda.SubTitulo != "Acionamento")
            {
                //Segue a mesma metodologia do Endereço
                if (obj.ColecaoCount(TipoColecoes.Dividas) >= 1)
                {
                    dgDividas.DataSource = obj.ColecaoDataSource(Tipos.TipoColecoes.Dividas);
                    dgDividas.DataBind();
                    this.SnapDividas.Visible = true;
                    BloquearCampos(Tipos.TipoColecoes.Dividas, true);
                    lbltnExcluirTudo.Visible = true;

                    this.dgAcionamentos.DataSource = this.obj.DataSourceAcionamentos;
                    this.dgAcionamentos.DataBind();

                    if (this.dgAcionamentos.Items.Count > 0)
                        this.SnapAcionamentos.Visible = true;

                }
                else if (obj.xmPath_LinkEntNo == "<Entidades><Carteiras>")
                {
                    this.BloquearCampos(TipoColecoes.Dividas, true);
                    lbtnMais_Dividas.Text = "Mais";
                }
                else
                {
                    lbtnMais_Dividas.Text = "Salvar";
                    this.obj.NovaDivida();
                    lbtnParcelasAutomaticas.Visible = true;
                }
            }
            #endregion

            #region Anotações
            txaAnotacoesAdicionais_TUD.Value = HttpUtility.HtmlDecode(obj.Entidade.TextoRespeito);
            #endregion

            #region Tarifas

            if (this.Sistema.TipoEntidade == NBdbm.tipos.TipoEntidade.Clientes)
            {
                txtTarJuros_MOEDA.Value = this.obj.Tarifas.Juros.ToString("0.00#,##");
                txtTarMulta_MOEDA.Value = this.obj.Tarifas.Multa.ToString("0.00#,##");
            }

            #endregion


        }

        /// <summary>
        /// Limpa um conjunto de Campos, divididos em Endereco, Telefone, email e Site.
        /// <br></br>
        /// </summary>
        /// <param name="LimpaOque">
        /// Especifica Qual Conjunto de Campos que será limpado, por Exemplo:
        /// <br>TipoColecoes.Endereco - campos que estão na Tabela de Endereço.</br>
        /// <br>TipoColecoes.Telefone - campos que estão na Tabela de Telefone.</br>
        /// <br>TipoColecoes.Email - campos que estão na Tabela de Emails.</br>
        /// <br>TipoColecoes.Site - campos que estão na Tabela de Sites.</br>
        /// </param>
        private void LimpaCampos(Tipos.TipoColecoes LimpaOque)
        {
            switch (LimpaOque)
            {
                case TipoColecoes.Endereco: //Limpa os Campos de Endereço
                    txtEndereco_TUD.Value = "";
                    txtComplemento_TUD.Value = "";
                    txtBairro_TUD.Value = "";
                    txtCEP_CEP.Value = "";
                    txtCidade_STR.Value = "";
                    selUF.SelectedIndex = -1;
                    txtComentarios_TUD.Value = "";
                    txtContatoEndereco_TUD.Value = "";
                    radEnderecoPrincipal.Checked = false;
                    break;
                case TipoColecoes.Telefone: //Limpa os Campos de Telefone
                    txtDescricaoTelefone_TUD.Value = "";
                    txtContatoTelefone_TUD.Value = "";
                    txtDDD_INT.Value = "";
                    txtFone_FONE.Value = "";
                    txtRamal_INT.Value = "";
                    break;
                case TipoColecoes.Email: //Limpa os Campos de Email
                    txtContatoEmail_STR.Value = "";
                    txtEmail_EMAIL.Value = "";
                    break;
                case TipoColecoes.Site: //Limpa os Campos de Email
                    txtDescricaoSite_STR.Value = "";
                    txtSite_SITE.Value = "";
                    break;
                case TipoColecoes.Dividas:
                    txtDivContrato_TUD.Value = "";
                    txtDivDataVencimento_DATA.Value = "";
                    txtDivNumDoc_INT.Value = "";
                    txtDivValorNominal_MOEDA.Value = "";
                    ddlDivTipoDivida.SelectedIndex = 0;
                    break;
            }
        }

        /// <summary>
        /// Bloqueia um conjunto de Campos, divididos em Endereco, Telefone, email e Site.
        /// <br></br>
        /// </summary>
        /// <param name="BloquearOque">
        /// Especifica Qual Conjunto de Campos que será Bloqueado ou Não, por Exemplo:
        /// <br>TipoColecoes.Endereco - campos que estão na Tabela de Endereço.</br>
        /// <br>TipoColecoes.Telefone - campos que estão na Tabela de Telefone.</br>
        /// <br>TipoColecoes.Email - campos que estão na Tabela de Emails.</br>
        /// <br>TipoColecoes.Site - campos que estão na Tabela de Sites.</br>
        /// </param>
        /// <param name="Bloquear">
        /// Especifica se os Campos serão Bloqueados (true) ou não (false).
        /// </param>
        private void BloquearCampos(Tipos.TipoColecoes BloquearOque, bool Bloquear)
        {
            switch (BloquearOque)
            {
                case TipoColecoes.Endereco: //Limpa os Campos de Endereço
                    txtEndereco_TUD.Disabled = Bloquear;
                    txtComplemento_TUD.Disabled = Bloquear;
                    txtBairro_TUD.Disabled = Bloquear;
                    txtCEP_CEP.Disabled = Bloquear;
                    txtCidade_STR.Disabled = Bloquear;
                    selUF.Disabled = Bloquear;
                    txtComentarios_TUD.Disabled = Bloquear;
                    txtContatoEndereco_TUD.Disabled = Bloquear;
                    radEnderecoPrincipal.Disabled = Bloquear;
                    break;
                case TipoColecoes.Telefone: //Limpa os Campos de Telefone
                    txtDescricaoTelefone_TUD.Disabled = Bloquear;
                    txtContatoTelefone_TUD.Disabled = Bloquear;
                    txtDDD_INT.Disabled = Bloquear;
                    txtFone_FONE.Disabled = Bloquear;
                    txtRamal_INT.Disabled = Bloquear;
                    break;
                case TipoColecoes.Email: //Limpa os Campos de Email
                    txtContatoEmail_STR.Disabled = Bloquear;
                    txtEmail_EMAIL.Disabled = Bloquear;
                    break;
                case TipoColecoes.Site: //Limpa os Campos do Site
                    txtDescricaoSite_STR.Disabled = Bloquear;
                    txtSite_SITE.Disabled = Bloquear;
                    break;
                case TipoColecoes.Dividas://Limpa os Campos da Dívida
                    //txtDivContrato_TUD.Disabled = this.obj.AlteraDivida ? true : Bloquear;
                    //txtDivNumDoc_INT.Disabled = this.obj.AlteraDivida ? true : Bloquear;
                    //ddlDivTipoDivida.Enabled = this.obj.AlteraDivida ? false : !Bloquear;
                    txtDivContrato_TUD.Disabled = Bloquear;
                    txtDivNumDoc_INT.Disabled = Bloquear;
                    ddlDivTipoDivida.Enabled = !Bloquear;
                    txtDivDataVencimento_DATA.Disabled = Bloquear;
                    txtDivValorNominal_MOEDA.Disabled = Bloquear;
                    break;

            }

        }

        /// <summary>
        /// Define qual conjunto de Campos será Preenchido com os Dados do Banco de Dados.
        /// <br>que estão divididos em Endereco, Telefone, email e Site.</br>
        /// </summary>
        /// <param name="TipoColecoes">
        /// Especifica Qual Conjunto de Campos que será Preenchido, por Exemplo:
        /// <br>TipoColecoes.Endereco - campos que estão na Tabela de Endereço.</br>
        /// <br>TipoColecoes.Telefone - campos que estão na Tabela de Telefone.</br>
        /// <br>TipoColecoes.Email - campos que estão na Tabela de Emails.</br>
        /// <br>TipoColecoes.Site - campos que estão na Tabela de Sites.</br>
        /// </param>
        /// <param name="obj">
        /// objeto do Sistema que tem os valores do Banco de Dados, que será do Tipo LimEntidades.
        /// </param>
        private void PreencheCampos(Tipos.TipoColecoes TipoColecoes)
        {
            switch (TipoColecoes)
            {
                case TipoColecoes.Endereco:
                    txtEndereco_TUD.Value = obj.Endereco.Logradouro_key;
                    txtComplemento_TUD.Value = obj.Endereco.complemento;
                    txtBairro_TUD.Value = obj.Endereco.Bairro;
                    try
                    {
                        txtCEP_CEP.Value = double.Parse(obj.Endereco.CEP).ToString("##'.'###-###");
                    }
                    catch
                    {
                        txtCEP_CEP.Value = "";
                    }
                    txtCidade_STR.Value = obj.Endereco.Municipio;
                    selUF.Value = obj.Endereco.UF;
                    txtContatoEndereco_TUD.Value = obj.Endereco.Contato;
                    txtComentarios_TUD.Value = obj.Endereco.Comentario;
                    radEnderecoPrincipal.Checked = obj.Endereco.Principal;
                    break;

                case TipoColecoes.Telefone:
                    txtDescricaoTelefone_TUD.Value = obj.Telefone.Descricao;
                    txtContatoTelefone_TUD.Value = obj.Telefone.Contato;
                    txtDDD_INT.Value = obj.Telefone.DDD_key;
                    txtFone_FONE.Value = obj.Telefone.Fone_key;
                    txtRamal_INT.Value = obj.Telefone.Ramal;
                    break;

                case TipoColecoes.Email:
                    txtContatoEmail_STR.Value = obj.Email.Descricao;
                    txtEmail_EMAIL.Value = obj.Email.eMail_key;
                    break;

                case TipoColecoes.Site:
                    txtDescricaoSite_STR.Value = obj.Site.Descricao;
                    txtSite_SITE.Value = obj.Site.Url_key;
                    break;

                case TipoColecoes.Dividas:
                    txtDivContrato_TUD.Value = obj.Divida.Contrato;
                    txtDivDataVencimento_DATA.Value = obj.Divida.DataVencimento.ToString("dd/MM/yyyy");
                    txtDivNumDoc_INT.Value = obj.Divida.NumDoc.ToString();
                    txtDivValorNominal_MOEDA.Value = obj.Divida.ValorNominal.ToString("0.00#,##");
                    ddlDivTipoDivida.SelectedValue = obj.Divida.idTipoDivida.ToString();
                    break;
            }
        }
        /// <summary>
        /// Modifica o Texto dos botões de LinkMais para Salvar, 
        /// Limpando os Campos do Tipo especificado no parametro TipoCampo, 
        /// Move o Foco para o Campo Especificado  no parametro MoverFocoPara, 
        /// Atualiza a DataGrid especificada no paramatro dg e mostra o 
        /// LinkButon Cancelar do Respectivo Tipo de Campo.
        /// 
        /// </summary>
        /// <param name="TipoCampo">
        /// Especifica Qual o tipo do linkButonMais que terá a Situação Mudada e 
        /// também qual o LinkButtonCancelar será Ativado.
        /// 
        /// <br>TipoColecoes.Endereco - lbtnMaisEndereco e lbtnCancelarEndereco.</br>
        /// <br>TipoColecoes.Telefone - lbtnMaisTelefone e lbtnCancelarTelefone.</br>
        /// <br>TipoColecoes.Email - lbtnMaisEmail e lbtnCancelarEmail.</br>
        /// <br>TipoColecoes.Site - lbtnMaisSite e lbtnCancelarSite.</br>
        /// </param>
        /// <param name="lbtnMais">
        /// Especifique qual é o LinkButtonMais que terá a Situação Mudada.
        /// </param>
        /// <param name="MoverFocoPara">
        /// Indique Qual o Campo Receberá o Foto Após a execução desse procedimento.
        /// </param>
        /// <param name="dg">
        /// Indique qual a DataGrid terá os Dados Atualizados.
        /// </param>
        private void MudaSituacaoLinkMais(Tipos.TipoColecoes TipoCampo, LinkButton lbtnMais, string MoverFocoPara, DataGrid dg)
        {

            //Limpa todos os Campos do Conjunto de Campos que pertence ao
            //Tipo de Campo informado no parametro [TipoCampo]
            LimpaCampos(TipoCampo);

            //DesBloqueia todos os Campos do Conjunto de Campos que pertence ao
            //Tipo de Campo informado no parametro [TipoCampo]
            BloquearCampos(TipoCampo, false);

            //Muda o Texto do LinkButtonMais para Salvar para que depois
            //que se clicar no mesmo ele venha a executar a função de 
            //Salvar o Conjunto de Dados na Coleção.
            lbtnMais.Text = "Salvar";

            if (TipoCampo == Tipos.TipoColecoes.Dividas)
            {
                lbtnParcelasAutomaticas.Visible = true;
                lbltnExcluirTudo.Visible = false;
            }

            //Procura o LinkButtonCancelar do respectivo Tipo de Campo
            //e muda o seu atributo visible para true, para que o mesmo
            //possa cancelar a operação de inclusão ou alteração do 
            //Conjunto de Campos.
            ((LinkButton)this.FindControl("lbtnCancelar_" + TipoCampo.ToString())).Visible = true;

            //Executa o Metodo MoverFoco para Gerar um script em
            //JavaScript onde move o Foco para o Campo definido no Parametro
            //MoverFocoPara.
            MessageBox.MoverFoco(MoverFocoPara);

            //Verifica se a propriedade Count da Coleção é igual a 1, caso
            //seja atribui o DataSource da Coleção ao DataSource da DataGrid.
            //Isso é Feito quando se vai adicionar um novo endereço, Telefone
            //Email ou Site e é verificado que já existe um item nesta coleção
            //Então mostra o Item já existente na DataGrid para o Usuário não
            //Achar que o Item Existente foi eliminado quando ele pediu para
            //Adicionar um Novo.
            if (obj.ColecaoCount(TipoCampo) == 1)
            {
                dg.DataSource = obj.ColecaoDataSource(TipoCampo);
                dg.DataBind();
            }

        }

        /// <summary>
        /// Exibe um alerta indicando se o Item Especificado para exclusão teve sucesso ou não.
        /// </summary>
        /// <param name="TipoCampo">
        /// Indique qual o Tipo do Conjunto de Dados deverá ser Excluido.
        /// <br>Por Exemplo: TipoColecoes.Endereco, Telefone, Email ou Site.</br>
        /// </param>
        /// <param name="dg">
        /// Conforme o Tipo de Dados será será excluido, informe a DataGrid Correspondente 
        /// para ser atualizado o DataSource. Ex: dgEnderecos, dgTelefones, dgEmails, dgSites.
        /// </param>
        private void ItemCommandDelete(Tipos.TipoColecoes TipoCampo, DataGrid dg)
        {
            try
            {
                obj.ColecaoRemove(TipoCampo, obj.ChaveTmp);
                LimpaCampos(TipoCampo);
                dg.DataSource = obj.ColecaoDataSource(TipoCampo);
                dg.DataBind();
            }
            catch (NBdbm.EVTexception evtEx)
            {
                MessageBox.Show(evtEx.Message);
            }

        }
        /// <summary>
        /// Verifica Qual comando foi executado nas DataGrids dos Conjutos de Dados 
        /// como por exemplo o comando Editar ou Deletar.
        /// </summary>
        /// <param name="TipoCampo">
        /// Especifique o Tipo da Coleção de Dados que será editado ou excluido.
        /// <br>Por Ex: TipoColecoes.Endereco, Telefone, Email, Site.</br>
        /// </param>
        /// <param name="lbtnMais">
        /// Indique Qual linkButtonMais terá a Situação Modificada.
        /// </param>
        /// <param name="MoverFocoPara">
        /// Indique qual o nome do campo receberá o Foto após o Comando de Editar.
        /// </param>
        /// <param name="dg">
        /// Indique Qual da DataGrid tera o DataSource Atualizado, com base 
        /// no Tipo do Campo. Ex: DgEnderecos, dgTelefone, etc.
        /// </param>
        /// <param name="ColunaGrid">
        /// Número da Coluna da DataGrid que tem o Valor da Chave para
        /// buscar o Item a Ser Editado ou Excluido da Coleção de Dados.
        /// </param>
        /// <param name="e">
        /// Evento do ItemCommand Da DataGrid que indicará qual foi o comando 
        /// Selecionado pelo usuário. por exemplo Edição ou Exclução.
        /// </param>
        private void ItemCommandVerifica(Tipos.TipoColecoes TipoCampo, LinkButton lbtnMais, string MoverFocoPara, DataGrid dg, int ColunaGrid, System.Web.UI.WebControls.DataGridCommandEventArgs e)
        {
            string ValorChaveEdicao = e.Item.Cells[ColunaGrid].Text;
            string mDescricaoColecao = TipoCampo.ToString().EndsWith("s") ? TipoCampo.ToString().Remove(TipoCampo.ToString().Length - 1, 1) : TipoCampo.ToString();
            switch (e.CommandName)
            {
                case "Edit":
                    if (lbtnMais.Text == "Salvar")
                        MessageBox.Show("Este Procedimento não esta habilitado, porque existe uma edição ou inclusão de " + mDescricaoColecao);
                    else
                    {
                        obj.ColecaoGet(TipoCampo, ValorChaveEdicao);
                        MudaSituacaoLinkMais(TipoCampo, lbtnMais, MoverFocoPara, dg);
                        if (TipoCampo == Tipos.TipoColecoes.Dividas)
                            lbtnParcelasAutomaticas.Visible = false;
                        PreencheCampos(TipoCampo);

                    }
                    break;
                case "Delete":
                    this.obj.ChaveTmp = ValorChaveEdicao;
                    if (TipoCampo == TipoColecoes.Dividas)
                        ValorChaveEdicao = e.Item.Cells[1].Text.Remove(e.Item.Cells[1].Text.Length - 1, 1) + " de Número: " + e.Item.Cells[3].Text + " e Valor: " + e.Item.Cells[5].Text;
                    MessageBox.ShowConfirma("Confirma a Exclusão do(a) " + mDescricaoColecao + " - " + ValorChaveEdicao, "Excluir" + TipoCampo.ToString(), true, true);
                    break;
                case "Baixar":
                    try
                    {
                        this.obj.BaixarDivida(e.Item.Cells[0].Text);
                        this.aController.ctrBaixas.Inicializar();
                        this.aController.ctrBaixas.CodigoDevedor = obj.CadEntidade.Entidade.ID;
                        this.aController.ctrBaixas.SelecionarDividaParaBaixar(obj.CadEntidade.Divida.ID, TipoSelecaoDividas.MarcarUma, true);
                        this.pnGeral.Visible = false;
                        this.MessageBox.Largura = 550;
                        this.MessageBox.Altura = 350;
                        this.MessageBox.TipoSubmit = NBWebControls.MessageBox.enTipoSubmit.Form;
                        this.MessageBox.ModalShow("Baixa.aspx");
                    }
                    catch (Exception Ex)
                    {
                        this.MessageBox.Show(Ex.Message);
                    }
                    break;
            }


        }
        /// <summary>
        /// Mostra qual tabela esta em uso se é com os dados da Pessoa Física 
        /// ou Pessoa Jurídica.
        /// </summary>
        /// <param name="PessoaJuridica">
        /// Ser for true será Pessoa Jurídica caso seja false, será Pessoa Física.
        /// </param>
        private void MostraTabelaPessoaFJ(bool PessoaFisica)
        {
            switch (PessoaFisica)
            {
                case true:
                    radPessoaF.Checked = true;
                    radPessoaJ.Checked = false;
                    break;
                case false:
                    radPessoaF.Checked = false;
                    radPessoaJ.Checked = true;
                    tabPJ.Style.Remove("DISPLAY");
                    tabPF.Style.Add("DISPLAY", "none");
                    break;
            }
        }

        /// <summary>
        /// Muda Situação do LinkButtonCancelar dos Tipos de Campos
        /// para Visible = false e  
        /// executando as seguintes tarefas:
        /// <br>Se a Session["Editando"] for Nula, então ira Limpar,  
        /// Bloquear os campos e mudar o Texto dos LinkButtonMais 
        /// conforme o seu Tipo.
        /// <br>Caso não seja Nula será executado o metodo dos 
        /// LinkButtonMais confoem o Tipo e será anulada a Session["Editando"].
        /// </summary>
        /// <param name="TipoCampo">
        /// Indique o Tipo do Conjunto de Dados que deverá sofrear as ações 
        /// do Método. ex: TipoColecoes.Endereco, Telefone, Email, Site.
        /// </param>
        /// <param name="Sender">Objeto que Chamou o Método.</param>
        /// <param name="e">Evento do Objeto que Chamou o Método.</param>
        private void MudaSituacaoLinkCancelar(Tipos.TipoColecoes TipoCampo, object Sender, System.EventArgs e)
        {
            ((LinkButton)this.FindControl("lbtnCancelar" + TipoCampo.ToString())).Visible = false;
            if (Session["Editando"] != null)
            {
                switch (TipoCampo)
                {
                    case TipoColecoes.Endereco:
                        LinkButton_Click(Sender, e);
                        break;
                    case TipoColecoes.Telefone:
                        LinkButton_Click(Sender, e);
                        break;
                    case TipoColecoes.Email:
                        LinkButton_Click(Sender, e);
                        break;
                }
                Session["Editando"] = null;
            }
            else
            {
                LimpaCampos(TipoCampo);
                BloquearCampos(TipoCampo, true);
                switch (TipoCampo)
                {
                    case TipoColecoes.Endereco:
                        lbtnMais_Endereco.Text = "mais";
                        break;
                    case TipoColecoes.Telefone:
                        lbtnMais_Telefone.Text = "mais";
                        break;
                    case TipoColecoes.Email:
                        lbtnCancelar_Email.Text = "mais";
                        break;
                }
            }
        }

        /// <summary>
        /// Excuta as ações para um novo objeto de acordo com o tipo da Coleção
        /// </summary>
        /// <param name="tipoColecao">Tipo da Coleção que deverá executar a Ação</param>
        private void lbtnMais(string tipoColecao)
        {
            switch (tipoColecao)
            {
                case "Endereco":
                    this.obj.NovoEndereco();
                    MudaSituacaoLinkMais(TipoColecoes.Endereco, lbtnMais_Endereco, "txtEndereco_TUD", dgEnderecos);
                    break;
                case "Telefone":
                    this.obj.NovoFone();
                    MudaSituacaoLinkMais(TipoColecoes.Telefone, lbtnMais_Telefone, "txtDescricaoTelefone_TUD", dgTelefones);
                    break;
                case "Email":
                    this.obj.NovoEmail();
                    MudaSituacaoLinkMais(TipoColecoes.Email, lbtnMais_Email, "txtContatoEmail_STR", dgEmails);
                    break;
                case "Site":
                    this.obj.NovoSite();
                    MudaSituacaoLinkMais(TipoColecoes.Site, lbtnMais_Site, "txtDescricaoSite_STR", dgSites);
                    break;
                case "Dividas":
                    if (obj.xmPath_LinkEntNo == "<Entidades><Carteiras>")
                    {
                        this.MessageBox.Show("Não é Possível Adicionar uma Nova Dívida, sem antes definir de qual Carteira ela pertence.");
                        return;
                    }
                    this.obj.NovaDivida();
                    MudaSituacaoLinkMais(TipoColecoes.Dividas, lbtnMais_Dividas, ddlDivTipoDivida.ClientID, dgDividas);
                    break;
            }
        }
        /// <summary>
        /// Excuta as ações para Salvar a inclusão ou alteração de um objeto de acordo com o
        /// tipo da coleção.
        /// </summary>
        /// <param name="tipoColecao">Tipo da Coleção que deverá executar a Ação</param>
        private void lbtnSalvar(string tipoColecao, Object Source)
        {
            switch (tipoColecao)
            {
                #region *** Endereço ***
                case "Endereco":
                    this.obj.Endereco.Logradouro_key = txtEndereco_TUD.Value;
                    this.obj.Endereco.complemento = txtComplemento_TUD.Value;
                    this.obj.Endereco.Bairro = txtBairro_TUD.Value;
                    this.obj.Endereco.CEP = txtCEP_CEP.Value;
                    this.obj.Endereco.Municipio = txtCidade_STR.Value;
                    this.obj.Endereco.UF = selUF.Value;
                    this.obj.Endereco.Comentario = txtComentarios_TUD.Value;
                    this.obj.Endereco.Contato = txtContatoEndereco_TUD.Value;
                    this.obj.Endereco.Principal = radEnderecoPrincipal.Checked;

                    try
                    {
                        obj.ColecaoAdd(Tipos.TipoColecoes.Endereco);
                        dgEnderecos.DataSource = obj.ColecaoDataSource(Tipos.TipoColecoes.Endereco);
                        dgEnderecos.DataBind();

                        lbtnMais_Endereco.Text = "Mais";
                        lbtnCancelar_Endereco.Visible = false;
                        BloquearCampos(Tipos.TipoColecoes.Endereco, true);
                    }
                    catch (NBdbm.COBR_Exception CobrEx)
                    {
                        if (Source.GetType().Name == "LinkButton")
                            this.MessageBox.Show(CobrEx.Message, txtEndereco_TUD.ClientID);
                        else
                        {
                            CobrEx.ClientID = txtEndereco_TUD.ClientID;
                            throw CobrEx;
                        }
                    }
                    break;
                #endregion

                #region *** Telefone ***
                case "Telefone":
                    this.obj.Telefone.Descricao = txtDescricaoTelefone_TUD.Value;
                    this.obj.Telefone.DDD_key = txtDDD_INT.Value;
                    this.obj.Telefone.Fone_key = txtFone_FONE.Value;
                    this.obj.Telefone.Contato = txtContatoTelefone_TUD.Value;
                    this.obj.Telefone.Ramal = txtRamal_INT.Value;
                    try
                    {
                        //Adiciona o novo Objeto na Coleção.
                        obj.ColecaoAdd(Tipos.TipoColecoes.Telefone);
                        //Busca o DataSource da Coleção conforme o seu tipo que é
                        //Telefone e atribui ao Datasource da DataGrid Telefones.
                        dgTelefones.DataSource = obj.ColecaoDataSource(Tipos.TipoColecoes.Telefone);
                        dgTelefones.DataBind();
                        //Muda o Texto da LinkButtonMais para "mais"
                        //Possibilitando a inclusão ou alteração de um Telefone.
                        lbtnMais_Telefone.Text = "Mais";
                        //Desabilita o Atributo Visible da LinkButtonCancelar.
                        lbtnCancelar_Telefone.Visible = false;
                        //Bloqueia Todos os Campos do Tipo Telefone.
                        BloquearCampos(Tipos.TipoColecoes.Telefone, true);
                    }
                    catch (NBdbm.COBR_Exception CobrEx)
                    {
                        if (Source.GetType().Name == "LinkButton")
                            this.MessageBox.Show(CobrEx.Message, txtFone_FONE.ClientID);
                        else
                        {
                            CobrEx.ClientID = txtFone_FONE.ClientID;
                            throw CobrEx;
                        }

                    }
                    break;
                #endregion

                #region *** Email ***
                case "Email":
                    this.obj.Email.Descricao = txtContatoEmail_STR.Value;
                    this.obj.Email.eMail_key = txtEmail_EMAIL.Value;
                    try
                    {
                        obj.ColecaoAdd(Tipos.TipoColecoes.Email);

                        dgEmails.DataSource = obj.ColecaoDataSource(Tipos.TipoColecoes.Email);
                        dgEmails.DataBind();

                        lbtnMais_Email.Text = "Mais";
                        lbtnCancelar_Email.Visible = false;
                        BloquearCampos(Tipos.TipoColecoes.Email, true);
                    }
                    catch (NBdbm.COBR_Exception CobrEx)
                    {
                        if (Source.GetType().Name == "LinkButton")
                            this.MessageBox.Show(CobrEx.Message, txtEmail_EMAIL.ClientID);
                        else
                        {
                            CobrEx.ClientID = txtEmail_EMAIL.ClientID;
                            throw CobrEx;
                        }

                    }
                    break;
                #endregion

                #region *** Site ***
                case "Site":
                    this.obj.Site.Descricao = txtDescricaoSite_STR.Value;
                    this.obj.Site.Url_key = txtSite_SITE.Value;
                    try
                    {
                        obj.ColecaoAdd(Tipos.TipoColecoes.Site);

                        dgSites.DataSource = obj.ColecaoDataSource(Tipos.TipoColecoes.Site);
                        dgSites.DataBind();

                        lbtnMais_Site.Text = "Mais";
                        lbtnCancelar_Site.Visible = false;
                        BloquearCampos(Tipos.TipoColecoes.Site, true);
                    }
                    catch (NBdbm.COBR_Exception CobrEx)
                    {
                        if (Source.GetType().Name == "LinkButton")
                            this.MessageBox.Show(CobrEx.Message, txtSite_SITE.ClientID);
                        else
                        {
                            CobrEx.ClientID = txtSite_SITE.ClientID;
                            throw CobrEx;
                        }

                    }
                    break;
                #endregion

                #region *** Divida ***
                case "Dividas":
                    this.obj.Divida.idEntidade = obj.Entidade.ID;
                    this.obj.Divida.idTipoDivida = int.Parse(ddlDivTipoDivida.SelectedValue);
                    this.obj.Divida.Contrato = txtDivContrato_TUD.Value;
                    this.obj.Divida.NumDoc = int.Parse(txtDivNumDoc_INT.Value);
                    this.obj.Divida.DataVencimento = DateTime.Parse(txtDivDataVencimento_DATA.Value);
                    this.obj.Divida.ValorNominal = Double.Parse(txtDivValorNominal_MOEDA.Value);
                    try
                    {
                        obj.ColecaoAdd(Tipos.TipoColecoes.Dividas);

                        dgDividas.DataSource = obj.ColecaoDataSource(Tipos.TipoColecoes.Dividas);
                        dgDividas.DataBind();

                        lbtnMais_Dividas.Text = "Mais";
                        lbtnCancelar_Dividas.Visible = false;
                        lbtnParcelasAutomaticas.Visible = false;
                        BloquearCampos(Tipos.TipoColecoes.Dividas, true);
                    }
                    catch (NBdbm.COBR_Exception CobrEx)
                    {
                        if (Source.GetType().Name == "LinkButton")
                            this.MessageBox.Show(CobrEx.Message, txtDivContrato_TUD.ClientID);
                        else
                        {
                            CobrEx.ClientID = txtDivContrato_TUD.ClientID;
                            throw CobrEx;
                        }

                    }
                    break;
                #endregion
            }
        }
        /// <summary>
        /// Executa a Ação de Cancelar um processo de Inclusão ou Alteração de 
        /// Acordo com o tipo da coleção.
        /// </summary>
        /// <param name="tipoColecao">Tipo da Coleção que deverá executar a Ação</param>
        private void lbtnCancelar(string tipoColecao)
        {
            switch (tipoColecao)
            {
                case "Endereco":
                    lbtnMais_Endereco.Text = "Mais";
                    lbtnCancelar_Endereco.Visible = false;
                    LimpaCampos(Tipos.TipoColecoes.Endereco);
                    BloquearCampos(Tipos.TipoColecoes.Endereco, true);
                    dgEnderecos.DataSource = obj.ColecaoDataSource(Tipos.TipoColecoes.Endereco);
                    dgEnderecos.DataBind();
                    break;

                case "Telefone":
                    lbtnMais_Telefone.Text = "Mais";
                    lbtnCancelar_Telefone.Visible = false;
                    LimpaCampos(Tipos.TipoColecoes.Telefone);
                    BloquearCampos(Tipos.TipoColecoes.Telefone, true);
                    dgTelefones.DataSource = obj.ColecaoDataSource(Tipos.TipoColecoes.Telefone);
                    dgTelefones.DataBind();
                    break;

                case "Email":
                    lbtnMais_Email.Text = "Mais";
                    lbtnCancelar_Email.Visible = false;
                    LimpaCampos(Tipos.TipoColecoes.Email);
                    BloquearCampos(Tipos.TipoColecoes.Email, true);
                    dgEmails.DataSource = obj.ColecaoDataSource(Tipos.TipoColecoes.Email);
                    dgEmails.DataBind();
                    break;

                case "Site":
                    lbtnMais_Site.Text = "Mais";
                    lbtnCancelar_Site.Visible = false;
                    LimpaCampos(Tipos.TipoColecoes.Site);
                    BloquearCampos(Tipos.TipoColecoes.Site, true);
                    dgSites.DataSource = obj.ColecaoDataSource(Tipos.TipoColecoes.Site);
                    dgSites.DataBind();
                    break;

                case "Dividas":
                    lbtnMais_Dividas.Text = "Mais";
                    lbtnCancelar_Dividas.Visible = false;
                    lbtnParcelasAutomaticas.Visible = false;
                    LimpaCampos(Tipos.TipoColecoes.Dividas);
                    BloquearCampos(Tipos.TipoColecoes.Dividas, true);
                    dgDividas.DataSource = obj.ColecaoDataSource(Tipos.TipoColecoes.Dividas);
                    dgDividas.DataBind();
                    break;
            }
        }
        #endregion

    }
}
