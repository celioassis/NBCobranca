using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace NBCobranca.Controllers
{
    /// <summary>
    /// Classe de controle da p�gina de Baixas de dividas.
    /// </summary>
    public class ctrBaixas : CtrBase
    {
        Classes.BusBaixas aBaixas;
        int aIdUsuarioUltimoAcionamento;
        bool aBaixandoDividaUnica;
        double aTotalValorNominalDivida = 0;
        double aTotalValorCorrigidoDivida = 0;
        double aTotalValorNominalSelecionado = 0;
        double aTotalValorCorrigidoSelecionado = 0;
        string aCarteiraUltimaDividaSelecionada = "";
        Entidades.entCTRL_Entidades aDevedor;
        List<Entidades.entCOBR_Divida> aDividasParaBaixar;
        List<Entidades.entCOBR_Divida> aDividasSelecionadas;

        public ctrBaixas(Classes.Sistema sistema, CtrFactory ctrFactory)
            : base(sistema, ctrFactory)
        {
            this.aBaixas = sistema.busBaixas;

        }

        public delegate void DividaSelecionadaParaBaixarHandler(int pTotalDividasSelecionadas, double pTotalValorNominal, double pTotalValorCorrigido);

        /// <summary>
        /// Evento que � disparado assim que o metodo AddDividaBaixar � executado.
        /// </summary>
        public event DividaSelecionadaParaBaixarHandler OnDividaSelecionadaParaBaixar;

        /// <summary>
        /// Inicializa o controle para ser utilizado em uma nova baixa de d�vidas.
        /// </summary>
        public void Inicializar()
        {
            aIdUsuarioUltimoAcionamento = 0;
            aBaixandoDividaUnica = false;
            aTotalValorNominalDivida = 0;
            aTotalValorCorrigidoDivida = 0;
            aTotalValorNominalSelecionado = 0;
            aTotalValorCorrigidoSelecionado = 0;
            aCarteiraUltimaDividaSelecionada = "";
            aDevedor = null;

            if (aDividasParaBaixar != null)
                this.aDividasParaBaixar.Clear();
            this.aDividasParaBaixar = null;

            if (this.aDividasSelecionadas != null)
                this.aDividasSelecionadas.Clear();
            this.aDividasSelecionadas = null;

        }

        /// <summary>
        /// get - define ou indica se a baixa ser� feita de uma unica d�vida.
        /// </summary>
        public bool BaixandoDividaUnica
        {
            get { return this.aBaixandoDividaUnica; }
        }

        /// <summary>
        /// Set - Define qual � o devedor que ser� feito a baixa;
        /// </summary>
        public int CodigoDevedor
        {
            set
            {
                this.aDevedor = this.aBaixas.GetDevedor(value);
                this.aDividasParaBaixar = this.aBaixas.LoadDividas(this.aDevedor.IdEntidade, ref this.aTotalValorNominalDivida, ref this.aTotalValorCorrigidoDivida);
                if (this.aDividasParaBaixar == null)
                    throw new Exception(string.Format("{0} n�o tem d�vidas para serem baixadas.", this.aDevedor.NomePrimary));
            }
        }

        /// <summary>
        /// Dados da Entidade Devedor que dever� ser dado baixa de suas dividas.
        /// </summary>
        public Entidades.entCTRL_Entidades Devedor
        {
            get
            {
                return this.aDevedor;
            }
        }

        /// <summary>
        /// Retorna os ultimos 100 acionamentos do devedor, o campos retornados s�o:
        /// <para>
        /// ID, Usuario, TipoAcionamento, DataAcionamento, DataPromessa e TextoRespeito
        /// </para>
        /// </summary>
        public DataTable Acionamentos
        {
            get
            {
                DataTable mDT = this.aBaixas.LoadAcionamentos(aDevedor.IdEntidade);
                this.aIdUsuarioUltimoAcionamento = 0;
                if (mDT.Rows.Count > 0)
                    aIdUsuarioUltimoAcionamento = Convert.ToInt32(mDT.Rows[mDT.Rows.Count - 1]["idUsuario"]);
                return mDT;
            }
        }

        /// <summary>
        /// Retorna todas as d�vidas vencidas e n�o baixadas totalmente do devedor.
        /// </summary>
        public List<Entidades.entCOBR_Divida> Dividas
        {
            get
            {
                return this.aDividasParaBaixar;
            }
        }

        /// <summary>
        /// Lista de Colaboradores que efetuaram os acionamentos, incluindo os inativos.
        /// </summary>
        public DataTable Colaboradores
        {
            get { return this.aBaixas.LoadColaboradores; }
        }

        /// <summary>
        /// c�digo do Usu�rio que fez o ultimo acionamento ao devedor.
        /// </summary>
        public int IdUsuarioUltimoAcionamento
        {
            get
            {
                return this.aIdUsuarioUltimoAcionamento;
            }
        }

        /// <summary>
        /// get - Soma total do valor nominal da divida do devedor.
        /// </summary>
        public double TotalValorNominalDivida
        {
            get { return this.aTotalValorNominalDivida; }
        }

        /// <summary>
        /// get - Soma total do valor corrigido da divida do devedor
        /// </summary>
        public double TotalValorCorrigidoDivida
        {
            get { return this.aTotalValorCorrigidoDivida; }
        }

        /// <summary>
        /// Realiza a baixa das dividas selecionadas.
        /// </summary>
        /// <param name="pPagouNoCliente">Indica se a d�vida foi paga no cliente.</param>
        /// <param name="pBaixaParcial">Indica se esta sendo realizada uma baixa parcial</param>
        /// <param name="pIdColaborador">C�digo do colaborador que receber� a comiss�o pela baixa</param>
        /// <param name="pComissao">Percentual de comiss�o do colaborador</param>
        /// <param name="pNumeroBordero">N�mero do bordero que ser�o registradas as baixas realizadas</param>
        /// <param name="pDataBaixa">Data em que as baixas foram realizadas</param>
        /// <param name="pNumeroRecibo">N�mero do recibo entregue ao devedor</param>
        /// <param name="pValorBaixa">Soma do valor nominal das d�vidas selecionadas para baixa</param>
        /// <param name="pValorRecebido">Valor recebido referente as baixas</param>
        public void BaixarDividasSelecionadas(bool pPagouNoCliente, bool pBaixaParcial, string pIdColaborador, string pComissao, string pNumeroBordero, string pDataBaixa, string pNumeroRecibo, string pValorBaixa, string pValorRecebido)
        {
            int mNumeroBordero = 0, mIdColaborador = 0, mNumeroRecibo = 0;
            double mValorBaixa = 0, mValorRecebido = 0, mComissao = 0;
            DateTime mDataBaixa = DateTime.Now;

            if (string.IsNullOrEmpty(pIdColaborador) || !int.TryParse(pIdColaborador, out mIdColaborador))
                throw new Exception("O Campo Colaborador � inv�lido ou esta em branco");

            if (string.IsNullOrEmpty(pComissao) || !double.TryParse(pComissao, out mComissao))
                throw new Exception("Valor inv�lido para o campo comiss�o do colaborador");

            if (string.IsNullOrEmpty(pNumeroBordero) || !int.TryParse(pNumeroBordero, out mNumeroBordero))
                throw new Exception("O Campo N�mero do Bordero � inv�lido ou esta em branco");
            if (mNumeroBordero <= 0)
                throw new Exception("O Campo N�mero do Bordero n�o pode conter o valor zero ou negativo");

            if (string.IsNullOrEmpty(pNumeroRecibo) || !int.TryParse(pNumeroRecibo, out mNumeroRecibo))
                throw new Exception("O Campo N�mero do Recibo � inv�lido ou esta em branco");

            if (string.IsNullOrEmpty(pDataBaixa) || !DateTime.TryParse(pDataBaixa, out mDataBaixa))
                throw new Exception("A data informada no campo Data da Baixa � inv�lido");

            if (string.IsNullOrEmpty(pValorBaixa) || !double.TryParse(pValorBaixa, out mValorBaixa))
                throw new Exception("Valor inv�lido para o campo valor da baixa");

            if (string.IsNullOrEmpty(pValorRecebido) || !double.TryParse(pValorRecebido, out mValorRecebido))
                throw new Exception("Valor inv�lido para o campo valor recebido");

            if (pBaixaParcial && mValorBaixa > this.aDividasSelecionadas[0].ValorNominal)
                throw new Exception("N�o � Permitido Baixar um Valor Acima do Valor Nominal");

            try
            {
                this.aBaixas.Baixar(pPagouNoCliente, pBaixaParcial, mIdColaborador, mComissao, mNumeroBordero,
                    mDataBaixa, mNumeroRecibo, mValorBaixa, mValorRecebido, this.aDividasSelecionadas);
            }
            catch (Exception ex)
            {
                throw new Exception("N�o foi poss�vel fazer a baixa das dividas selecionadas", ex);
            }
        }

        /// <summary>
        /// Seleciona todas as d�vidas para serem Marcas ou Desmarcadas para serem baixadas.
        /// </summary>
        /// <param name="pSelecaoDividas">
        /// Define o Tipo de Sele��o, sendo aceito somente MarcarTodas ou DesmarcarTodas, caso contr�rio ser� gerado uma exce��o.
        /// </param>
        public void SelecionarDividaParaBaixar(Tipos.TipoSelecaoDividas pSelecaoDividas)
        {
            if (pSelecaoDividas != NBCobranca.Tipos.TipoSelecaoDividas.MarcarTodas &&
                pSelecaoDividas != NBCobranca.Tipos.TipoSelecaoDividas.DesmarcarTodas)
                throw new Exception("Este metodo n�o pode ser executado com os tipos de sele��o de d�vida diferentes de MarcarTodas e DesmarcaTodas.");

            if (pSelecaoDividas == NBCobranca.Tipos.TipoSelecaoDividas.MarcarTodas)
                foreach (Entidades.entCOBR_Divida mDivida in this.aDividasParaBaixar)
                {
                    if (!string.IsNullOrEmpty(this.aCarteiraUltimaDividaSelecionada) && mDivida.XmPathCliente != this.aCarteiraUltimaDividaSelecionada)
                    {
                        this.aCarteiraUltimaDividaSelecionada = null;
                        throw new Exception("N�o � permitido a Baixa de Dividas com carteiras Diferentes ao mesmo tempo, favor Baixar a D�vida de Uma Carteira primeiro depois a outra");
                    }
                    this.aCarteiraUltimaDividaSelecionada = mDivida.XmPathCliente;
                }
            try
            {
                if (pSelecaoDividas == NBCobranca.Tipos.TipoSelecaoDividas.MarcarTodas)
                {
                    if (this.aDividasSelecionadas == null)
                        this.aDividasSelecionadas = new List<NBCobranca.Entidades.entCOBR_Divida>();
                    this.aDividasSelecionadas.Clear();
                    this.aDividasSelecionadas.AddRange(this.aDividasParaBaixar);
                    this.aTotalValorCorrigidoSelecionado = this.aTotalValorCorrigidoDivida;
                    this.aTotalValorNominalSelecionado = this.aTotalValorNominalDivida;
                }
                else
                {
                    this.aDividasSelecionadas.Clear();
                    this.aTotalValorCorrigidoSelecionado = 0;
                    this.aTotalValorNominalSelecionado = 0;
                    this.aCarteiraUltimaDividaSelecionada = null;
                }

                if (OnDividaSelecionadaParaBaixar != null)
                    OnDividaSelecionadaParaBaixar(this.aDividasSelecionadas.Count, this.aTotalValorNominalSelecionado, this.aTotalValorCorrigidoSelecionado);

            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("N�o foi poss�vel {0} as dividas do devedor", pSelecaoDividas.ToString()), ex);
            }
        }

        /// <summary>
        /// Seleciona uma d�vida conforme o Indice para ser Marcada ou Desmarcada para ser baixada.
        /// </summary>
        /// <param name="pIndiceDivida">Indice da d�vida que esta na Cole��o de d�vidas para ser baixada</param>
        /// <param name="pSelecaoDividas">
        /// Define o Tipo de Sele��o, sendo aceito somente MarcarUma ou DesmarcarUma, caso contr�rio ser� gerado uma exce��o.
        /// </param>
        public void SelecionarDividaParaBaixar(int pIndiceDivida, Tipos.TipoSelecaoDividas pSelecaoDividas)
        {
            this.SelecionarDividaParaBaixar(pIndiceDivida, pSelecaoDividas, false);
        }

        /// <summary>
        /// Seleciona uma d�vida conforme o Indice para ser Marcada ou Desmarcada para ser baixada.
        /// </summary>
        /// <param name="pIndiceDivida">Indice da d�vida que esta na Cole��o de d�vidas para ser baixada</param>
        /// <param name="pSelecaoDividas">
        /// Define o Tipo de Sele��o, sendo aceito somente MarcarUma ou DesmarcarUma, caso contr�rio ser� gerado uma exce��o.
        /// </param>
        /// <param name="pBaixaUnica">Indica se a d�vida que esta sendo baixada ser� a unica, isso ocorre quando a baixa 
        /// � feita pela ficha de cadastro.</param>
        public void SelecionarDividaParaBaixar(int pIndiceDivida, Tipos.TipoSelecaoDividas pSelecaoDividas, bool pBaixaUnica)
        {
            if (pSelecaoDividas != NBCobranca.Tipos.TipoSelecaoDividas.MarcarUma &&
                pSelecaoDividas != NBCobranca.Tipos.TipoSelecaoDividas.DesmarcarUma)
                throw new Exception("Este metodo n�o pode ser executado com os tipos de sele��o de d�vida diferentes de MarcarUma e DesmarcaUma.");

            Entidades.entCOBR_Divida mDivida = null;
            
            if (!pBaixaUnica)
            {
                mDivida = this.aDividasParaBaixar[pIndiceDivida];

                if (!string.IsNullOrEmpty(this.aCarteiraUltimaDividaSelecionada) && mDivida.XmPathCliente != this.aCarteiraUltimaDividaSelecionada)
                    throw new Exception("N�o � permitido a Baixa de Dividas com carteiras Diferentes ao mesmo tempo, favor Baixar a D�vida de Uma Carteira primeiro depois a outra");
            }
            try
            {
                this.aBaixandoDividaUnica = pBaixaUnica;

                if (this.aDividasSelecionadas == null)
                    this.aDividasSelecionadas = new List<NBCobranca.Entidades.entCOBR_Divida>();

                if (pBaixaUnica)
                {
                    foreach (Entidades.entCOBR_Divida mmDivida in this.aDividasParaBaixar)
                    {
                        if (mmDivida.Id == pIndiceDivida)
                        {
                            mDivida = mmDivida;
                            this.aDividasSelecionadas.Add(mmDivida);
                            this.aTotalValorNominalDivida = mmDivida.ValorNominalReal;
                            this.aTotalValorCorrigidoDivida = mmDivida.ValorCorrigido;
                            break;
                        }
                    }
                    this.aDividasParaBaixar = this.aDividasSelecionadas;
                }
                else
                {

                    if (pSelecaoDividas == NBCobranca.Tipos.TipoSelecaoDividas.MarcarUma)
                    {
                        this.aTotalValorCorrigidoSelecionado += mDivida.ValorCorrigido;
                        this.aTotalValorNominalSelecionado += mDivida.ValorNominalReal;
                        this.aDividasSelecionadas.Add(mDivida);
                    }
                    else
                    {
                        this.aTotalValorCorrigidoSelecionado -= mDivida.ValorCorrigido;
                        this.aTotalValorNominalSelecionado -= mDivida.ValorNominalReal;
                        this.aDividasSelecionadas.Remove(mDivida);
                    }
                }
                if (aDividasSelecionadas.Count > 0)
                    this.aCarteiraUltimaDividaSelecionada = mDivida.XmPathCliente;
                else
                    this.aCarteiraUltimaDividaSelecionada = null;

                if (OnDividaSelecionadaParaBaixar != null)
                    OnDividaSelecionadaParaBaixar(this.aDividasSelecionadas.Count, this.aTotalValorNominalSelecionado, this.aTotalValorCorrigidoSelecionado);
            }
            catch (Exception ex)
            {
                throw new Exception("N�o foi poss�vel adicionar a d�vida selecionada para baixar.", ex);
            }
        }

        public void SelecionarBaixaUnica(Entidades.entCTRL_Entidades pDevedor, Entidades.entCOBR_Divida pDivida)
        {
            if (pDivida.Baixada)
                throw new Exception("Esta d�vida j� esta baixada.");

            if (pDivida.Id == 0)
                throw new Exception("Esta d�vida precisar ser salva antes de ser baixada.");

            this.Inicializar();
            this.aDevedor = pDevedor;
            this.aDividasParaBaixar = new List<NBCobranca.Entidades.entCOBR_Divida>();
            this.aDividasParaBaixar.Add(pDivida);
            this.SelecionarDividaParaBaixar(pDivida.Id, NBCobranca.Tipos.TipoSelecaoDividas.MarcarUma, true);
        }

        /// <summary>
        /// get - Quantidade total de d�vidas j� selecionadas para serem baixadas.
        /// </summary>
        public int TotalDividasParaBaixar
        {
            get
            {
                if (this.aDividasSelecionadas != null)
                    return this.aDividasSelecionadas.Count;
                else
                    return 0;
            }
        }

        /// <summary>
        /// Recalcula os juros sobre um determinado valor, usado quando o pagamento da d�vida � parcial e o valor nominal � alterado.
        /// </summary>
        /// <param name="pValorASerCalculado"></param>
        /// <returns></returns>
        public double ReCalcularJuros(double pValorASerCalculado)
        {
            if (aDividasSelecionadas[0].ValorNominalReal < pValorASerCalculado)
                throw new Exception("O valor de baixa n�o pode ser maior que o valor nominal da d�vida.");

            try
            {
                return this.aBaixas.RecalcularJuros(aDividasSelecionadas[0].DataVencimento.Value, pValorASerCalculado, aDividasSelecionadas[0].Carteira);
            }
            catch (Exception ex)
            {
                throw new Exception("N�o foi poss�vel calcular os Juros sobre " + pValorASerCalculado.ToString("N2"), ex);
            }
        }

    }
}
