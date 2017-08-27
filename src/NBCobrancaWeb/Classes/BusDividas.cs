using System;
using System.Data;
using System.Configuration;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

namespace NBCobranca.Classes
{
    /// <summary>
    /// Classe responsável por toda a regra de negócio referente as dívidas do devedor.
    /// </summary>
    public class BusDividas : BusBase
    {
        public BusDividas(Sistema sistema, Neobridge.NBDB.DBDirect dbDirect)
            : base(sistema, dbDirect)
        { }

        public List<Entidades.entCOBR_Divida> Load(int pCodigoDevedor, bool pSomenteVencidas, bool pSomenteNaoBaixadas)
        {
            double mTotalValorNominal = 0, mTotalValorCorrigido = 0;
            return this.Load(pCodigoDevedor, pSomenteNaoBaixadas, pSomenteNaoBaixadas, ref mTotalValorNominal, ref mTotalValorCorrigido);
        }
        
        /// <summary>
        /// Lista as dividas de um determinado devedor conforme os filtros definidos por paramentros.
        /// <para>
        /// A lista é de objetos do tipo entCOBR_Divida, portanto tera dos os campos da mesma.
        /// </para>
        /// </summary>
        /// <param name="pCodigoDevedor">Código do devedor que será buscado a lista de dividas</param>
        /// <param name="pSomenteVencidas">indica se a pesquisa ira retornar somente dividas vencidas ou não, caso seja 
        /// false serão retornadas as dividas vencidas e para vencer.
        /// </param>
        /// <param name="pSomenteNaoBaixadas">Indica se a pesquisa ira retornar somente as dividas não baixadas, 
        /// caso contrário serão retornadas as baixadas e não baixadas.
        /// </param>
        /// <returns></returns>
        public List<Entidades.entCOBR_Divida> Load(int pCodigoDevedor, bool pSomenteVencidas, bool pSomenteNaoBaixadas, ref double pTotalValorNominal, ref double pTotalValorCorrigido)
        {
            try
            {
                DataTable mDT;
                StringBuilder mSQL = new StringBuilder();
                pTotalValorCorrigido = 0;
                pTotalValorNominal = 0;
                mSQL.AppendLine("SELECT COBR_Divida.Id, COBR_Divida.IdEntidade, IdTipoDivida, Contrato, NumDoc,");
                mSQL.AppendLine("DataVencimento, ValorNominal, DataBaixa, BorderoBaixa, NumRecibo, IdCobrador,");
                mSQL.AppendLine("PerCobrador, IdUsuarioBaixa, BaixaNoCliente, Baixada, BaixaParcial, XmPathCliente, ");
                mSQL.AppendLine("COBR_TipoDivida.Descricao TipoDivida");
                mSQL.AppendLine("from COBR_Divida");
                mSQL.AppendLine("join COBR_TipoDivida on COBR_TipoDivida.Id = COBR_Divida.IdTipoDivida");
                mSQL.AppendFormat("where IdEntidade = {0}\r\n", pCodigoDevedor);
                if (pSomenteVencidas)
                    mSQL.AppendLine("and DataVencimento < GETDATE()");
                if (pSomenteNaoBaixadas)
                    mSQL.AppendLine("and Baixada = 0");
                mSQL.AppendLine("Order by IdTipoDivida, DataVencimento, Contrato, NumDoc");

                mDT = this.DbDirect.CriarDataTable(mSQL.ToString());

                if (mDT.Rows.Count > 0)
                {
                    List<Entidades.entCOBR_Divida> mLista = new List<NBCobranca.Entidades.entCOBR_Divida>();
                    string mCarteiraAnterior = "";
                    double mJuros = 0;
                    double mMulta = 0;

                    foreach (DataRow mDR in mDT.Rows)
                    {
                        Entidades.entCOBR_Divida mDivida = new NBCobranca.Entidades.entCOBR_Divida();
                        mDivida.Preencher(mDR);
                        mDivida.TipoDivida = mDR["TipoDivida"].ToString();
                        this.AtualizaValorNominalBaixado(mDivida);
                        if (mCarteiraAnterior != mDivida.Carteira)
                        {
                            mCarteiraAnterior = mDivida.Carteira;
                            this.CarregaJurosMultasCarteira(ref mJuros, ref mMulta, mCarteiraAnterior);
                        }
                        this.AtualizaValorNominalBaixado(mDivida);
                        this.CalcularValorCorrigido(mDivida, mJuros, mMulta);
                        pTotalValorNominal += mDivida.ValorNominalReal;
                        pTotalValorCorrigido += mDivida.ValorCorrigido;
                        mLista.Add(mDivida);
                    }
                    return mLista;
                }
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível buscar as dividas do devedor de código " + pCodigoDevedor.ToString(), ex);
            }
        }

        /// <summary>
        /// Aplicar juros de uma carteira sobre um determinado valor.
        /// </summary>
        /// <param name="pDataVencimento">Data de vencimento para o calculo dos juros.</param>
        /// <param name="pValor">Valor que será aplicado os juros.</param>
        /// <param name="pCarteira">string nome da carteira que será usado para buscar os valores de juros e multa para ser aplicado</param>
        /// <returns>Retorna um double com o parametro pValor acrecido dos juros e multa calculado.</returns>
        public double AplicarJurosSobreValor(DateTime pDataVencimento, double pValor, string pCarteira)
        {
            try
            {
                double mJuros = 0, mMulta = 0;
                CarregaJurosMultasCarteira(ref mJuros, ref mMulta, pCarteira);
                return AplicarJurosSobreValor(pValor, pDataVencimento, mJuros, mMulta);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Format("Não foi possível aplicar os juros sobre o valor {0} da carteira {1}", pValor, pCarteira), ex);
            }
        }
        
        /// <summary>
        /// Aplica juros sobre um valor conforme os paramentros informados.
        /// </summary>
        /// <param name="pValor">Valor que será aplicado os juros</param>
        /// <param name="pDataVencimento">Data de vencimento do valor para calcular os juros.</param>
        /// <param name="pJuros">Percentual de juros que será aplicado.</param>
        /// <param name="pMulta">Percentual da multa que será aplicada.</param>
        /// <returns>Retorna um double com o parametro pValor acrecido dos juros e multa calculado.</returns>
        private double AplicarJurosSobreValor(double pValor, DateTime pDataVencimento, double pJuros, double pMulta)
        {
            double mJurosDia = 0;
            double mValorJuros = 0;
            double mValorMulta = 0;
            TimeSpan mPeriodoDivida;

            mPeriodoDivida = DateTime.Today.Subtract(pDataVencimento);

            mJurosDia = pJuros / 30;

            if (pDataVencimento < DateTime.Today)
            {
                mValorJuros = (pValor * (mPeriodoDivida.Days * mJurosDia)) / 100;
                mValorMulta = (pValor * pMulta) / 100;
            }
            return pValor + mValorMulta + mValorJuros;
        }

        /// <summary>
        /// Calcula os Juros sobre o valor nominal real da dívida, este valor já desconta as baixas parciais.
        /// </summary>
        /// <param name="pDivida">Entidade da divida que será atualizado a propriedade ValorCorrigido</param>
        /// <param name="pJuros">Percentual de juros da carteira</param>
        /// <param name="pMulta">Percentual de mula da carteira</param>
        private void CalcularValorCorrigido(Entidades.entCOBR_Divida pDivida, double pJuros, double pMulta)
        {
            try
            {
                pDivida.ValorCorrigido = AplicarJurosSobreValor(pDivida.ValorNominalReal, pDivida.DataVencimento.Value, pJuros, pMulta);
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível calcular os juros da dívida " + pDivida.Id.ToString(), ex);
            }
        }

        /// <summary>
        /// Atualiza o Valor nominal da Dívida, pois a mesma pode ter sido baixada parcialmente.
        /// </summary>
        /// <param name="pDivida"></param>
        /// <returns></returns>
        private void AtualizaValorNominalBaixado(Entidades.entCOBR_Divida pDivida)
        {
            try
            {
                if (!pDivida.Baixada && pDivida.BaixaParcial)
                {
                    double mValorBaixaParcial = 0;
                    string mSQL = string.Format("Select ValorBaixa from COBR_Baixas where idDivida = {0}", pDivida.Id);
                    DataTable mDT = this.DbDirect.CriarDataTable(mSQL);
                    foreach (System.Data.DataRow mDR in mDT.Rows)
                    {
                        mValorBaixaParcial += Convert.ToDouble(mDR["ValorBaixa"]);
                    }
                    pDivida.ValorNominalBaixado = mValorBaixaParcial;
                }

            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível carregar as baixas parciais da divida " + pDivida.Id.ToString(), ex);
            }
        }

        /// <summary>
        /// Carrega o valor dos juros e multa de uma carteira.
        /// </summary>
        /// <param name="pJuros">Variável que receberá o valor dos Juros</param>
        /// <param name="pMulta">variável que receberá o valor da Multa</param>
        /// <param name="pCarteira">Nome da Carteira que será recuperado os valores de juros e multa.</param>
        private void CarregaJurosMultasCarteira(ref double pJuros, ref double pMulta, string pCarteira)
        {
            try
            {
                pJuros = 0;
                pMulta = 0;
                StringBuilder mSQL = new StringBuilder();
                mSQL.AppendLine("SELECT Juros, Multa");
                mSQL.AppendLine("FROM COBR_Tarifas");
                mSQL.AppendLine("join CTRL_Entidades on CTRL_Entidades.IdEntidade = COBR_Tarifas.IdEntidade ");
                mSQL.AppendFormat("and NomePrimary = '{0}'", pCarteira);
                DataTable mDT = this.DbDirect.CriarDataTable(mSQL.ToString());
                if (mDT.Rows.Count > 0)
                {
                    pJuros = Convert.ToDouble(mDT.Rows[0]["Juros"]);
                    pMulta = Convert.ToDouble(mDT.Rows[0]["Multa"]);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível carregar a tabela de juros e multa da carteira " + pCarteira, ex);
            }

        }

    }
}
