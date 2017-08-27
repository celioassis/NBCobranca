using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using NBCobranca.Classes;
using NBCobranca.Interfaces;
using System.Collections.Generic;

namespace NBCobranca.Controllers
{
    public class ctrCadastroEntidades : CtrBase
    {

        IpresCadastroEntidades aView;
        string aXmPathCliente;
        Entidades.entCTRL_Entidades aEntidade;
        Entidades.entCTRL_Entidades aEntidadeJaCadastrada;
        Tipos.TipoEntidades aTipoEntidade;
        bool aEditando = false;
        bool aCriandoParcelas = false;
        int aTotalParcelas = 0;
        List<Entidades.entBase> aEntidadesParaExcluir;
        Dictionary<Tipos.TipoColecoes, string> aKeyColecoesEmEdicao = new Dictionary<NBCobranca.Tipos.TipoColecoes, string>();

        public ctrCadastroEntidades(Classes.Sistema sistema, CtrFactory ctrFactory)
            : base(sistema, ctrFactory)
        {
            this.aEntidadesParaExcluir = new List<NBCobranca.Entidades.entBase>();
        }

        /// <summary>
        /// Inicializa o controller com o XmPath do Cliente e a Página que esta sendo usada para cadastro da 
        /// Entidade.
        /// </summary>
        /// <param name="pXmPathCliente"></param>
        /// <param name="pView"></param>
        public void SetView(IpresCadastroEntidades pView)
        {
            this.aView = pView;

            if (this.aView == null)
                return;

            if (!aView.IsPostBack)
            {
                this.aView.TituloCadastro = "Cadastro de " + this.TipoCadastro(false);
                this.aView.LabelNomeEntidade = this.TipoCadastro(true);
                if (this.aTipoEntidade == NBCobranca.Tipos.TipoEntidades.Devedores)
                    ((IpresDivida)this.aView).SetDataSourceTipoDivida(this.Sistema.busTiposDivida.DataSource);
                if (this.aEditando)
                {
                    this.aView.BloquearLimparCampos(NBCobranca.Tipos.TipoColecoes.Todos, true, true);
                    this.aView.TipoPessoa = this.aEntidade.PessoaFJ ? Tipos.TipoPessoa.Fisica : NBCobranca.Tipos.TipoPessoa.Juridica;
                    this.aView.NomeRazaoSocial = this.aEntidade.NomePrimary;
                    this.aView.ApelidoNomeFantasia = this.aEntidade.NomeSecundary;
                    this.aView.CPFCNPJ = this.aEntidade.CPFCNPJ;
                    this.aView.RgIE = this.aEntidade.RGIE;
                    this.aView.TextoRespeito = this.aEntidade.TxtRespeito;

                    if (this.aEntidade.Sites.Count == 0)
                        this.aView.BloquearLimparCampos(Tipos.TipoColecoes.Site, false, true);
                    this.aView.AtualizaDataSourceColecoes(this.aEntidade.Sites.Values, Tipos.TipoColecoes.Site);

                    if (this.aEntidade.Emails.Count == 0)
                        this.aView.BloquearLimparCampos(Tipos.TipoColecoes.Email, false, true);
                    this.aView.AtualizaDataSourceColecoes(this.aEntidade.Emails.Values, Tipos.TipoColecoes.Email);

                    if (this.aEntidade.Telefones.Count == 0)
                        this.aView.BloquearLimparCampos(Tipos.TipoColecoes.Telefone, false, true);
                    this.aView.AtualizaDataSourceColecoes(this.aEntidade.Telefones.Values, Tipos.TipoColecoes.Telefone);

                    if (this.aEntidade.Enderecos.Count == 0)
                        this.aView.BloquearLimparCampos(NBCobranca.Tipos.TipoColecoes.Endereco, false, true);
                    this.aView.AtualizaDataSourceColecoes(this.aEntidade.Enderecos.Values, NBCobranca.Tipos.TipoColecoes.Endereco);

                    this.aView.AtualizaDataSourceColecoes(this.aEntidade.Dividas.Values, Tipos.TipoColecoes.Dividas);

                    this.aView.AtualizaDataSourceColecoes(Sistema.busAcionamentos.Load(aEntidade.IdEntidade).DefaultView, Tipos.TipoColecoes.Acionamentos);

                }
            }
        }

        public void NovaEntidade()
        {
            if (string.IsNullOrEmpty(this.aXmPathCliente) || this.aTipoEntidade == NBCobranca.Tipos.TipoEntidades.Todos)
                throw new Exception("É preciso definir um XMLPathCliente");

            this.aKeyColecoesEmEdicao.Clear();
            this.aEntidade = new NBCobranca.Entidades.entCTRL_Entidades();
            this.aEditando = false;
        }
        /// <summary>
        /// Inicia o Cadastro de uma nova entidade
        /// </summary>
        /// <param name="pXmPathCliente">Informe o XmPath que ira pertencer a entidade</param>
        /// <param name="pTipoEntidade">Informe o tipo da entidade que será cadastrada.</param>
        public void NovaEntidade(string pXmPathCliente, Tipos.TipoEntidades pTipoEntidade)
        {
            this.aXmPathCliente = pXmPathCliente;
            this.aTipoEntidade = pTipoEntidade;
            this.NovaEntidade();
        }

        /// <summary>
        /// get - Indica se a operação de cadastro é uma edição de entidade já existente ou se é 
        /// o cadastro de uma nova entidade.
        /// </summary>
        public bool Editando
        {
            get { return this.aEditando; }
        }

        /// <summary>
        /// Carrega uma Entidade conforme o parametro pCodigoEntidade.
        /// </summary>
        /// <param name="pCodigoEntidade">Código da Entidade que se deseja carregar.</param>
        public void CarregaEntidade(int pCodigoEntidade, Tipos.TipoEntidades pTipoEntidade, string pXmPathCliente)
        {
            try
            {
                this.aTipoEntidade = pTipoEntidade;
                this.aXmPathCliente = pXmPathCliente;
                this.aKeyColecoesEmEdicao.Clear();
                aEntidade = this.Sistema.busEntidade.Get(pCodigoEntidade, this.aTipoEntidade, true);
                aEditando = true;
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possivel carregar a Entidade de código " + pCodigoEntidade.ToString(), ex);
            }
        }

        /// <summary>
        /// Carrega uma entidade que já foi detectada como existente na validação do CPF ou CNPJ.
        /// </summary>
        public void CarregaEntidadeQueJaExiste()
        {
            if (this.aEntidadeJaCadastrada == null)
                throw new Exception("Não existe nenhuma entidade validada por um CNPJ ou CPF");
            this.aKeyColecoesEmEdicao.Clear();
            this.aEntidade = this.aEntidadeJaCadastrada;
            this.aEditando = true;
            this.aEntidadeJaCadastrada = null;
        }

        /// <summary>
        /// get - Retorna a descrição do Tipo da Entidade.
        /// </summary>
        public string TipoCadastro(bool pNomeLabel)
        {
            if (!pNomeLabel)
                return this.aTipoEntidade.ToString();
            else
            {
                switch (this.aTipoEntidade.ToString())
                {
                    case "Funcionarios":
                        return "Funcionario";
                    case "Devedores":
                        return "Devedor";
                    case "Clientes":
                        return "Cliente";
                    case "Fornecedores":
                        return "Fornecedor";
                    default:
                        return "";
                }

            }
        }

        /// <summary>
        /// Mostra qual é o Tipo de Entidades que o cadastro esta trabalhando.
        /// </summary>
        public Tipos.TipoEntidades TipoEntidade
        {
            get
            {
                return this.aTipoEntidade;
            }
        }

        /// <summary>
        /// Salva o cadastro de uma entidade.
        /// </summary>
        public void Salvar()
        {
            try
            {
                IpresEntidade mViewEntidade = this.aView;
                this.aEntidade.NomePrimary = mViewEntidade.NomeRazaoSocial;
                this.aEntidade.NomeSecundary = mViewEntidade.ApelidoNomeFantasia;
                this.aEntidade.PessoaFJ = mViewEntidade.TipoPessoa == NBCobranca.Tipos.TipoPessoa.Fisica ? true : false;
                this.aEntidade.CPFCNPJ = mViewEntidade.CPFCNPJ;
                this.aEntidade.RGIE = mViewEntidade.RgIE;
                this.aEntidade.TxtRespeito = mViewEntidade.TextoRespeito;
                this.aEntidade.DtAlteracao = DateTime.Now;
                if (!this.aEntidade.Alterando)
                    this.aEntidade.DtCriacao = DateTime.Now;
                this.Sistema.busEntidade.Salvar(this.aEntidade, this.aEntidadesParaExcluir, this.aXmPathCliente);
                this.aEditando = true;
            }
            catch (ExceptionCampoObrigatorio)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível salvar o {0} de nome {1}", this.aTipoEntidade, this.aEntidade.NomePrimary), ex);
            }
        }

        /// <summary>
        /// Retorna o Count de cada coleção.
        /// </summary>
        /// <param name="pColecao">Qual é a coleção que se deseja recuperar o seu Count.</param>
        /// <returns></returns>
        public int Count(Tipos.TipoColecoes pColecao)
        {
            switch (pColecao)
            {
                case NBCobranca.Tipos.TipoColecoes.Endereco:
                    return this.aEntidade.Enderecos.Count;
                case NBCobranca.Tipos.TipoColecoes.Telefone:
                    return this.aEntidade.Telefones.Count;
                case NBCobranca.Tipos.TipoColecoes.Email:
                    return this.aEntidade.Emails.Count;
                case NBCobranca.Tipos.TipoColecoes.Site:
                    return this.aEntidade.Sites.Count;
                case NBCobranca.Tipos.TipoColecoes.Dividas:
                    return this.aEntidade.Dividas.Count;
                default:
                    return 0;
            }
        }

        public void EditarColecao(Tipos.TipoColecoes pColecao, string pKeyColecao)
        {
            this.aKeyColecoesEmEdicao.Add(pColecao, pKeyColecao);
            this.aView.BloquearLimparCampos(pColecao, false);
            long mKeyColecao = Convert.ToInt64(pKeyColecao);
            switch (pColecao)
            {
                case NBCobranca.Tipos.TipoColecoes.Endereco:
                    Entidades.entCTRL_Enderecos mEndereco = this.aEntidade.Enderecos[mKeyColecao];
                    IpresEndereco mPresEndereco = this.aView;
                    mPresEndereco.Logradouro = mEndereco.Logradouro;
                    mPresEndereco.Bairro = mEndereco.Bairro;
                    mPresEndereco.CEP = mEndereco.CEP;
                    mPresEndereco.Comentario = mEndereco.Comentario;
                    mPresEndereco.complemento = mEndereco.Complemento;
                    mPresEndereco.Contato = mEndereco.Contato;
                    mPresEndereco.Municipio = mEndereco.Municipio;
                    mPresEndereco.Principal = mEndereco.Principal;
                    mPresEndereco.UF = mEndereco.UF;
                    break;
                case NBCobranca.Tipos.TipoColecoes.Telefone:
                    Entidades.entCTRL_Fones mTelefone = this.aEntidade.Telefones[mKeyColecao];
                    IpresTelefone mPresTelefone = this.aView;
                    mPresTelefone.DDD = mTelefone.DDD;
                    mPresTelefone.Fone = mTelefone.Fone;
                    mPresTelefone.Fone_Contato = mTelefone.Contato;
                    mPresTelefone.Fone_Descricao = mTelefone.Descricao;
                    mPresTelefone.Ramal = mTelefone.Ramal;
                    break;
                case NBCobranca.Tipos.TipoColecoes.Email:
                    Entidades.entCTRL_Email mEmail = this.aEntidade.Emails[mKeyColecao];
                    IpresEmail mPresEmail = this.aView;
                    mPresEmail.eMail = mEmail.EMail;
                    mPresEmail.eMail_Descricao = mEmail.Descricao;
                    break;
                case NBCobranca.Tipos.TipoColecoes.Site:
                    Entidades.entCTRL_Url mSite = this.aEntidade.Sites[mKeyColecao];
                    IpresSite mPresSite = (IpresSite)this.aView;
                    mPresSite.Url = mSite.Url;
                    mPresSite.Url_Descricao = mSite.Descricao;
                    break;
                case NBCobranca.Tipos.TipoColecoes.Dividas:
                    Entidades.entCOBR_Divida mDivida = this.aEntidade.Dividas[mKeyColecao];
                    IpresDivida mPresDivida = (IpresDivida)this.aView;
                    mPresDivida.Contrato = mDivida.Contrato;
                    mPresDivida.DataVencimento = mDivida.DataVencimento.Value;
                    mPresDivida.NumeroDocumento = mDivida.NumDoc;
                    mPresDivida.TipoDivida = mDivida.IdTipoDivida;
                    mPresDivida.ValorNominal = mDivida.ValorNominal;
                    break;
            }
        }

        public void ExcluirColecao(Tipos.TipoColecoes pColecao, string pKeyColecao)
        {
            Entidades.entBase mEntidade = null;
            System.Collections.ICollection mDataSource = null;
            long mKeyColecao = 0;
            if (pKeyColecao != "Todas")
                mKeyColecao = Convert.ToInt64(pKeyColecao);
            switch (pColecao)
            {
                case NBCobranca.Tipos.TipoColecoes.Endereco:
                    mEntidade = this.aEntidade.Enderecos[mKeyColecao];
                    this.ValidaEnderecoPrincipal((Entidades.entCTRL_Enderecos)mEntidade, true);
                    this.aEntidade.Enderecos.Remove(mKeyColecao);
                    mDataSource = this.aEntidade.Enderecos.Values;
                    break;
                case NBCobranca.Tipos.TipoColecoes.Telefone:
                    mEntidade = this.aEntidade.Telefones[mKeyColecao];
                    this.aEntidade.Telefones.Remove(mKeyColecao);
                    mDataSource = this.aEntidade.Telefones.Values;
                    break;
                case NBCobranca.Tipos.TipoColecoes.Email:
                    mEntidade = this.aEntidade.Emails[mKeyColecao];
                    this.aEntidade.Emails.Remove(mKeyColecao);
                    mDataSource = this.aEntidade.Emails.Values;
                    break;
                case NBCobranca.Tipos.TipoColecoes.Site:
                    mEntidade = this.aEntidade.Sites[mKeyColecao];
                    this.aEntidade.Sites.Remove(mKeyColecao);
                    mDataSource = this.aEntidade.Sites.Values;
                    break;
                case NBCobranca.Tipos.TipoColecoes.Dividas:
                    if (pKeyColecao != "Todas")
                    {
                        mEntidade = this.aEntidade.Dividas[mKeyColecao];
                        this.aEntidade.Dividas.Remove(mKeyColecao);
                    }
                    else
                    {
                        foreach (Entidades.entCOBR_Divida mDivida in this.aEntidade.Dividas.Values)
                        {
                            if (mDivida.ID_BD > 0)
                                this.aEntidadesParaExcluir.Add(mDivida);
                        }
                        this.aEntidade.Dividas.Clear();
                    }
                    mDataSource = this.aEntidade.Dividas.Values;
                    break;
            }
            if (pKeyColecao != "Todas" && mEntidade.ID_BD > 0)
                this.aEntidadesParaExcluir.Add(mEntidade);
            this.aView.AtualizaDataSourceColecoes(mDataSource, pColecao);
        }

        public void CriarAutoParcelamentoDividas(int pTotalDeParcelas)
        {
            this.aTotalParcelas = pTotalDeParcelas;
            this.aCriandoParcelas = true;
        }

        public void SelecionarDividaParaBaixaUnica(string pKeyDivida)
        {
            IpresBaixa mBaixa = this.aView as IpresBaixa;
            ctrBaixas mCtrBaixas = this.CtrFactory.ctrBaixas;
            Entidades.entCOBR_Divida mDivida = this.aEntidade.Dividas[Convert.ToInt64(pKeyDivida)];
            mCtrBaixas.SelecionarBaixaUnica(this.aEntidade, mDivida);
            mBaixa.SetDataSourceBaixa(this.CtrFactory.ctrBaixas.Dividas);
            mBaixa.SetDataSourceCobrador(mCtrBaixas.Colaboradores);
            mBaixa.ValorBaixa = mDivida.ValorNominalReal;
            mBaixa.ValorRecebido = mDivida.ValorCorrigido;
            mBaixa.DataBaixa = DateTime.Today;
            this.aView.TituloCadastro = "Cadastro de Devedores - Baixa Única";
        }

        public void BaixarDividaUnica()
        {
            IpresBaixa mBaixa = this.aView as IpresBaixa;
            this.CtrFactory.ctrBaixas.BaixarDividasSelecionadas(mBaixa.PagouNoCliente, mBaixa.BaixaParcial,
                mBaixa.Cobrador, mBaixa.Comissao.ToString(), mBaixa.Bordero.ToString(), mBaixa.DataBaixa.ToShortDateString(),
                mBaixa.Recibo.ToString(), mBaixa.ValorBaixa.ToString(), mBaixa.ValorRecebido.ToString());
            this.aView.AtualizaDataSourceColecoes(this.aEntidade.Dividas.Values, NBCobranca.Tipos.TipoColecoes.Dividas);
            this.aView.TituloCadastro = "Cadastro de Devedores";
        }

        public void RecalcularJuros_BaixaUnica()
        {
            IpresBaixa mBaixa = (IpresBaixa)this.aView;

            if (mBaixa.PagouNoCliente && mBaixa.BaixaParcial)
                mBaixa.ValorRecebido = mBaixa.ValorBaixa;
            else if (mBaixa.BaixaParcial)
                mBaixa.ValorRecebido = this.CtrFactory.ctrBaixas.ReCalcularJuros(mBaixa.ValorBaixa);
        }

        /// <summary>
        /// Valida se um CPF ou CNPJ é válido e se já existe alguma entidade relacionada com a mesma, caso exista 
        /// deverá gerar um Exception de Documento já utilizado e perguntar se deseja alternar para a entidade já 
        /// existente.
        /// </summary>
        public void ValidarCPFCNPJ()
        {
            VerificaSeEntidadeJaExiste(false);
        }
        public void VerificaSeEntidadeJaExiste(bool pVerificarPorNome)
        {
            this.aEntidadeJaCadastrada = null;
            if (pVerificarPorNome)
                this.aEntidadeJaCadastrada = this.Sistema.busEntidade.Get(this.aView.NomeRazaoSocial, this.aTipoEntidade, true, true);
            else
                this.aEntidadeJaCadastrada = this.Sistema.busEntidade.Get(this.aView.CPFCNPJ.Replace(".", "").Replace("/", "").Replace("-", ""), this.aTipoEntidade, true, false);

            if (this.aEntidadeJaCadastrada != null)
            {
                throw new ExceptionEntidadeJaExiste(string.Format("Já Existe um {0} cadastrado com esse {1}, deseja visualizar o seu cadastro?",
                    (aTipoEntidade != NBCobranca.Tipos.TipoEntidades.Funcionarios && aTipoEntidade != NBCobranca.Tipos.TipoEntidades.Clientes)
                    ? aTipoEntidade.ToString().Replace("res", "r") : aTipoEntidade.ToString().Replace("s", ""),
                    pVerificarPorNome ? "NOME" : "CPF/CNPJ"));
            }
        }

        public void AcaoLinkButton(NBCobranca.Tipos.TipoAcaoLinkButtons pAcao, NBCobranca.Tipos.TipoColecoes pColecao, bool pFromBotaoSalvarEntidade)
        {
            if (pAcao == NBCobranca.Tipos.TipoAcaoLinkButtons.Salvar)
            {
                string mKeyItemColecaoEditando = null;
                if (aKeyColecoesEmEdicao.ContainsKey(pColecao))
                    mKeyItemColecaoEditando = this.aKeyColecoesEmEdicao[pColecao];

                switch (pColecao)
                {
                    case NBCobranca.Tipos.TipoColecoes.Endereco:
                        if (!pFromBotaoSalvarEntidade || !string.IsNullOrEmpty(this.aView.Logradouro))
                            if (!string.IsNullOrEmpty(mKeyItemColecaoEditando))
                                this.SalvarEndereco(this.aEntidade.Enderecos[Convert.ToInt64(mKeyItemColecaoEditando)]);
                            else
                                this.SalvarEndereco(new NBCobranca.Entidades.entCTRL_Enderecos());
                        break;
                    case NBCobranca.Tipos.TipoColecoes.Telefone:
                        if (!pFromBotaoSalvarEntidade || !string.IsNullOrEmpty(this.aView.DDD + this.aView.Fone))
                            if (!string.IsNullOrEmpty(mKeyItemColecaoEditando))
                                this.SalvarTelefone(this.aEntidade.Telefones[Convert.ToInt64(mKeyItemColecaoEditando)]);
                            else
                                this.SalvarTelefone(new NBCobranca.Entidades.entCTRL_Fones());
                        break;
                    case NBCobranca.Tipos.TipoColecoes.Email:
                        if (!pFromBotaoSalvarEntidade || !string.IsNullOrEmpty(this.aView.eMail))
                            if (!string.IsNullOrEmpty(mKeyItemColecaoEditando))
                                this.SalvarEmail(this.aEntidade.Emails[Convert.ToInt64(mKeyItemColecaoEditando)]);
                            else
                                this.SalvarEmail(new NBCobranca.Entidades.entCTRL_Email());
                        break;
                    case NBCobranca.Tipos.TipoColecoes.Site:
                        if (!pFromBotaoSalvarEntidade || !string.IsNullOrEmpty((this.aView as IpresSite).Url))
                            if (!string.IsNullOrEmpty(mKeyItemColecaoEditando))
                                this.SalvarSite(this.aEntidade.Sites[Convert.ToInt64(mKeyItemColecaoEditando)]);
                            else
                                this.SalvarSite(new NBCobranca.Entidades.entCTRL_Url());
                        break;
                    case NBCobranca.Tipos.TipoColecoes.Dividas:
                        if (!pFromBotaoSalvarEntidade || !string.IsNullOrEmpty((this.aView as IpresDivida).Contrato))
                            if (!string.IsNullOrEmpty(mKeyItemColecaoEditando))
                                this.SalvarDivida(this.aEntidade.Dividas[Convert.ToInt64(mKeyItemColecaoEditando)]);
                            else
                                this.SalvarDivida(new NBCobranca.Entidades.entCOBR_Divida());
                        break;
                }
                this.aView.BloquearLimparCampos(pColecao, true);
            }
            else if (pAcao == NBCobranca.Tipos.TipoAcaoLinkButtons.Novo)
            {
                if (aEditando && pColecao == NBCobranca.Tipos.TipoColecoes.Dividas && aXmPathCliente == "<Entidades><Carteiras>")
                    throw new Exception("Não é Possível Adicionar uma Nova Dívida, sem antes definir de qual Carteira ela pertence.");
                this.aView.BloquearLimparCampos(pColecao, false);
            }
            else if (pAcao == NBCobranca.Tipos.TipoAcaoLinkButtons.AutoParcelar)
            {
                IpresDivida mPresDivida = (IpresDivida)this.aView;
                if (mPresDivida.Contrato == "" ||
                    mPresDivida.NumeroDocumento == 0 ||
                    mPresDivida.DataVencimento == new DateTime(1900, 01, 01) ||
                    mPresDivida.ValorNominal == 0)
                    throw new Exception("Os Campos Contrato, Número do Documento, Data de Vencimento e Valor Nominal, devem Estar Preenchidos para que possa ser feito um parcelamento Automático");
            }
            else
                this.aView.BloquearLimparCampos(pColecao, true);

            if (pAcao == Tipos.TipoAcaoLinkButtons.Salvar || pAcao == Tipos.TipoAcaoLinkButtons.Cancelar)
                this.aKeyColecoesEmEdicao.Remove(pColecao);

        }

        #region === Métodos para Salvar as coleções ===

        /// <summary>
        /// Salva os dados do endereço alterado ou incluso.
        /// </summary>
        /// <param name="pEndereco"></param>
        private void SalvarEndereco(Entidades.entCTRL_Enderecos pEndereco)
        {
            IpresEndereco mPresEndereco = this.aView;

            string mKeyItemColecaoEditando = this.aKeyColecoesEmEdicao.ContainsKey(Tipos.TipoColecoes.Endereco)
                ? this.aKeyColecoesEmEdicao[Tipos.TipoColecoes.Endereco] : "";

            if (string.IsNullOrEmpty(mPresEndereco.Logradouro))
                throw new Exception("Não é permitido salvar um endereço sem logradouro");

            /*            if (string.IsNullOrEmpty(mPresEndereco.Municipio))
                            throw new Exception("Não é permitido salvar um endereço sem Cidade");*/

            try
            {

                pEndereco.CEP = mPresEndereco.CEP;
                pEndereco.Logradouro = mPresEndereco.Logradouro;
                pEndereco.Bairro = mPresEndereco.Bairro;
                pEndereco.Municipio = mPresEndereco.Municipio;
                pEndereco.UF = mPresEndereco.UF;
                pEndereco.Comentario = mPresEndereco.Comentario;
                pEndereco.Complemento = mPresEndereco.complemento;
                pEndereco.Contato = mPresEndereco.Contato;

                if (this.aEntidade.Enderecos.Count == 0)
                    pEndereco.Principal = true;
                else
                    pEndereco.Principal = mPresEndereco.Principal;

                if (string.IsNullOrEmpty(mKeyItemColecaoEditando))
                {
                    pEndereco.VerificaDuplicataKey<Entidades.entCTRL_Enderecos>(this.aEntidade.Enderecos, pEndereco);
                    this.aEntidade.Enderecos.Add(pEndereco.UniqueID, pEndereco);
                }
                this.ValidaEnderecoPrincipal(pEndereco, false);
                this.aView.AtualizaDataSourceColecoes(this.aEntidade.Enderecos.Values, Tipos.TipoColecoes.Endereco);
            }
            catch (ArgumentException)
            {
                throw new Exception("Não é permitido duplicação de endereço");
            }
            catch (Exception Ex)
            {
                throw new Exception("Não foi possível salvar o endereço, é possível que tenha alguma informação iválida.", Ex);
            }
        }

        /// <summary>
        /// Salva os dados do telefone
        /// </summary>
        /// <param name="pTelefone">Entidade de telefone</param>
        private void SalvarTelefone(Entidades.entCTRL_Fones pTelefone)
        {
            IpresTelefone mPresTelefone = this.aView;

            string mKeyItemColecaoEditando = this.aKeyColecoesEmEdicao.ContainsKey(Tipos.TipoColecoes.Telefone)
                ? this.aKeyColecoesEmEdicao[Tipos.TipoColecoes.Telefone] : "";

            if (string.IsNullOrEmpty(mPresTelefone.DDD))
                throw new Exception("Não é permitido salvar um telefone sem DDD");

            if (string.IsNullOrEmpty(mPresTelefone.Fone))
                throw new Exception("Não é permitido salvar um telefone sem o número do telefone");

            try
            {
                pTelefone.Contato = mPresTelefone.Fone_Contato;
                pTelefone.DDD = mPresTelefone.DDD;
                pTelefone.Descricao = mPresTelefone.Fone_Descricao;
                pTelefone.Fone = mPresTelefone.Fone;
                pTelefone.Ramal = mPresTelefone.Ramal;

                if (string.IsNullOrEmpty(mKeyItemColecaoEditando))
                {
                    pTelefone.VerificaDuplicataKey<Entidades.entCTRL_Fones>(this.aEntidade.Telefones, pTelefone);
                    this.aEntidade.Telefones.Add(pTelefone.UniqueID, pTelefone);
                }
                this.aView.AtualizaDataSourceColecoes(this.aEntidade.Telefones.Values, Tipos.TipoColecoes.Telefone);
            }
            catch (ArgumentException)
            {
                throw new Exception("Não é permitido duplicação de Telefone");
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível salvar os dados do telefone. É possível que exitam informações inválidas.", ex);
            }
        }

        /// <summary>
        /// Salva os dados de email
        /// </summary>
        /// <param name="pEmail">Entidade que contém os dados do email</param>
        private void SalvarEmail(Entidades.entCTRL_Email pEmail)
        {
            IpresEmail mPresEmail = this.aView;

            string mKeyItemColecaoEditando = this.aKeyColecoesEmEdicao.ContainsKey(Tipos.TipoColecoes.Email)
                ? this.aKeyColecoesEmEdicao[Tipos.TipoColecoes.Email] : "";

            if (string.IsNullOrEmpty(mPresEmail.eMail))
                throw new Exception("Não é permitido salvar um email em branco");

            try
            {
                pEmail.Descricao = mPresEmail.eMail_Descricao;
                pEmail.EMail = mPresEmail.eMail;

                if (string.IsNullOrEmpty(mKeyItemColecaoEditando))
                {
                    pEmail.VerificaDuplicataKey<Entidades.entCTRL_Email>(this.aEntidade.Emails, pEmail);
                    this.aEntidade.Emails.Add(pEmail.UniqueID, pEmail);
                }
                this.aView.AtualizaDataSourceColecoes(this.aEntidade.Emails.Values, Tipos.TipoColecoes.Email);
            }
            catch (ArgumentException)
            {
                throw new Exception("Não é permitido duplicação de Email");
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível salvar os dados do email. É possível que exitam informações inválidas.", ex);
            }
        }

        /// <summary>
        /// Salva os dados do Site
        /// </summary>
        /// <param name="pEmail">Entidade que contém os dados do Site</param>
        private void SalvarSite(Entidades.entCTRL_Url pSite)
        {
            IpresSite mPresSite = (IpresSite)this.aView;

            string mKeyItemColecaoEditando = this.aKeyColecoesEmEdicao.ContainsKey(Tipos.TipoColecoes.Site)
                ? this.aKeyColecoesEmEdicao[Tipos.TipoColecoes.Site] : "";

            if (string.IsNullOrEmpty(mPresSite.Url))
                throw new Exception("Não é permitido salvar um Site em branco");

            try
            {
                pSite.Descricao = mPresSite.Url_Descricao;
                pSite.Url = mPresSite.Url;

                if (string.IsNullOrEmpty(mKeyItemColecaoEditando))
                {
                    pSite.VerificaDuplicataKey<Entidades.entCTRL_Url>(this.aEntidade.Sites, pSite);
                    this.aEntidade.Sites.Add(pSite.UniqueID, pSite);
                }
                this.aView.AtualizaDataSourceColecoes(this.aEntidade.Sites.Values, Tipos.TipoColecoes.Site);
            }
            catch (ArgumentException)
            {
                throw new Exception("Não é permitido duplicação de Site");
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível salvar os dados do Site. É possível que exitam informações inválidas.", ex);
            }
        }

        /// <summary>
        /// Salva os dados de uma Divida que esta sendo alterada ou inclusa.
        /// </summary>
        /// <param name="pDivida">Entidade que representa a dívida que esta sendo alterada ou inclusão</param>
        private void SalvarDivida(Entidades.entCOBR_Divida pDivida)
        {
            IpresDivida mPresDivida = (IpresDivida)this.aView;

            string mKeyItemColecaoEditando = this.aKeyColecoesEmEdicao.ContainsKey(Tipos.TipoColecoes.Dividas)
                ? this.aKeyColecoesEmEdicao[Tipos.TipoColecoes.Dividas] : "";

            if (mPresDivida.TipoDivida <= 0)
                throw new Exception("Não é permitido salvar uma dívida se ter definido o seu tipo.");

            if (string.IsNullOrEmpty(mPresDivida.Contrato))
                throw new Exception("Não é permitido salvar uma dívida sem um contrato.");

            if (mPresDivida.NumeroDocumento <= 0)
                throw new Exception("Não é permitido salvar uma dívida sem um número de documento.");

            if (pDivida.Baixada || pDivida.BaixaParcial)
                if (pDivida.DataVencimento != mPresDivida.DataVencimento || pDivida.ValorNominal != mPresDivida.ValorNominal)
                    throw new Exception("Não é permitido a alteração da Data de Vencimento ou Valor nominal da Dívida após a mesma ter sido baixada parcial ou totalmente.");

            pDivida.Contrato = mPresDivida.Contrato;
            pDivida.DataVencimento = mPresDivida.DataVencimento;
            pDivida.IdTipoDivida = mPresDivida.TipoDivida;
            pDivida.TipoDivida = mPresDivida.DescricaoTipoDivida;
            pDivida.NumDoc = mPresDivida.NumeroDocumento;
            pDivida.ValorNominal = mPresDivida.ValorNominal;
            pDivida.XmPathCliente = this.aXmPathCliente;

            try
            {
                if (string.IsNullOrEmpty(mKeyItemColecaoEditando))
                {
                    pDivida.VerificaDuplicataKey<Entidades.entCOBR_Divida>(this.aEntidade.Dividas, pDivida);
                    this.aEntidade.Dividas.Add(pDivida.UniqueID, pDivida);
                }

                this.aView.AtualizaDataSourceColecoes(this.aEntidade.Dividas.Values, Tipos.TipoColecoes.Dividas);
                if (this.aCriandoParcelas && string.IsNullOrEmpty(mKeyItemColecaoEditando))
                {
                    this.aCriandoParcelas = false;
                    DateTime mDataVencimento = mPresDivida.DataVencimento;
                    for (int i = 1; i < this.aTotalParcelas; i++)
                    {
                        mPresDivida.DataVencimento = mDataVencimento.AddMonths(i);
                        mPresDivida.NumeroDocumento++;
                        this.SalvarDivida(new NBCobranca.Entidades.entCOBR_Divida());
                    }
                    this.aTotalParcelas = 0;
                }
            }
            catch (ArgumentException)
            {
                throw new Exception("Não é permitido duplicação de dívida");
            }
            catch (Exception Ex)
            {
                throw new Exception("Não foi possível salvar a dívida, é possível que tenha alguma informação iválida.", Ex);
            }
        }

        #endregion

        /// <summary>
        /// Valida o endereço para que exista somente um endereço principal.
        /// </summary>
        /// <param name="pEndereco"></param>
        private void ValidaEnderecoPrincipal(Entidades.entCTRL_Enderecos pEndereco, bool pExclusao)
        {
            if (pEndereco.Principal)
                foreach (Entidades.entCTRL_Enderecos mEndereco in this.aEntidade.Enderecos.Values)
                {
                    if (pExclusao && mEndereco.UniqueID != pEndereco.UniqueID)
                    {
                        pEndereco.Principal = false;
                        mEndereco.Principal = true;
                        return;
                    }
                    if (mEndereco.Principal && mEndereco.UniqueID != pEndereco.UniqueID)
                        mEndereco.Principal = false;
                }
        }
    }
}
