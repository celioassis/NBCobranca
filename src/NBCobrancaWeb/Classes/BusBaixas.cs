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

namespace NBCobranca.Classes
{
    public class BusBaixas : BusBase
    {
        BusAcionamentos aAcionamentos;
        BusEntidade aEntidades;
        BusDividas aDividas;

        public BusBaixas(Sistema sistema, Neobridge.NBDB.DBDirect dbDirect)
            : base(sistema, dbDirect)
        {
            this.aAcionamentos = sistema.busAcionamentos;
            this.aEntidades = sistema.busEntidade;
            this.aDividas = sistema.busDividas;
        }

        public DataTable LoadAcionamentos(int pCodigoDevedor)
        {
            return this.aAcionamentos.Load(pCodigoDevedor);
        }
        public Entidades.entCTRL_Entidades GetDevedor(int pCodigoDevedor)
        {
            return this.aEntidades.Get(pCodigoDevedor, NBCobranca.Tipos.TipoEntidades.Devedores, false);
        }
        public List<Entidades.entCOBR_Divida> LoadDividas(int pCodigoDevedor, ref double pTotalValorNominal, ref double pTotalValorCorrigido)
        {
            return this.aDividas.Load(pCodigoDevedor, false, true, ref pTotalValorNominal, ref pTotalValorCorrigido);
        }
        public DataTable LoadColaboradores
        {
            get
            {
                return this.aAcionamentos.LoadUsuarios();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pPagouNoCliente"></param>
        /// <param name="pBaixaParcial"></param>
        /// <param name="pIdColaborador"></param>
        /// <param name="pComissao"></param>
        /// <param name="pNumeroBordero"></param>
        /// <param name="pDataBaixa"></param>
        /// <param name="pNumeroRecibo"></param>
        /// <param name="pValorBaixa"></param>
        /// <param name="pValorRecebido"></param>
        /// <param name="pDividasParaBaixar"></param>
        public void Baixar(bool pPagouNoCliente, bool pBaixaParcial, int pIdColaborador, double pComissao, int pNumeroBordero, DateTime pDataBaixa, int pNumeroRecibo, double pValorBaixa, double pValorRecebido, List<Entidades.entCOBR_Divida> pDividasParaBaixar)
        {
            try
            {
                double mValorRecebido = pValorRecebido / pDividasParaBaixar.Count;
                this.DbDirect.Transaction_Begin();
                foreach (Entidades.entCOBR_Divida mDivida in pDividasParaBaixar)
                {
                    Entidades.entCOBR_Baixas mBaixa = new NBCobranca.Entidades.entCOBR_Baixas();
                    mBaixa.BaixadoCliente = pPagouNoCliente;
                    mBaixa.DataBaixa = pDataBaixa;
                    mBaixa.NumBordero = pNumeroBordero;
                    mBaixa.ValorBaixa = mDivida.ValorNominalReal;
                    if (pBaixaParcial)
                        mBaixa.ValorBaixa = pValorBaixa;
                    mBaixa.ValorRecebido = mValorRecebido;
                    mBaixa.IdDivida = mDivida.Id;
                    mBaixa.IdEntidade = mDivida.IdEntidade;
                    mBaixa.ValorNominal = mDivida.ValorNominalReal;
                    this.DbDirect.Execute_NonQuery(mBaixa.SqlInsert);

                    mDivida.IdUsuarioBaixa = this.Sistema.LimLogin.UsuarioID;
                    mDivida.DataBaixa = pDataBaixa;
                    if (!pPagouNoCliente)
                    {
                        mDivida.IdCobrador = pIdColaborador;
                        mDivida.PerCobrador = pComissao;
                        mDivida.NumRecibo = pNumeroRecibo;
                    }
                    mDivida.BorderoBaixa = pNumeroBordero;
                    mDivida.BaixaNoCliente = pPagouNoCliente;
                    if (!mDivida.BaixaParcial)
                        mDivida.BaixaParcial = pBaixaParcial;
                    mDivida.Baixada = true;
                    if (pBaixaParcial)
                        mDivida.Baixada = (Math.Round(mDivida.ValorNominalReal, 2) == Math.Round(pValorBaixa, 2));
                    this.DbDirect.Execute_NonQuery(mDivida.SqlUpdate);
                    if (pBaixaParcial && !mDivida.Baixada)
                        mDivida.ValorNominalBaixado += mBaixa.ValorBaixa;
                    else if (mDivida.Baixada)
                        mDivida.ValorNominalBaixado = 0;
                }
                this.DbDirect.Transaction_Commit();
            }
            catch (Exception ex)
            {
                this.DbDirect.Transaction_Cancel();
                throw new Exception("Não foi possível registrar a baixa das Dívidas selecionadas", ex);
            }
        }

        /// <summary>
        /// Recalcula os juros sobre um determinado valor
        /// </summary>
        /// <param name="pDataVencimento">Data de vencimento do valor</param>
        /// <param name="pValor">Valor que será aplicado os juros</param>
        /// <param name="pCarteira">Nome da Carteira que contém os percentuais de juros e multa</param>
        /// <returns>double com os juros aplicado sobre o valor do parametro pValor</returns>
        public double RecalcularJuros(DateTime pDataVencimento, double pValor, string pCarteira)
        {
            return Sistema.busDividas.AplicarJurosSobreValor(pDataVencimento, pValor, pCarteira);
        }

        public override void Dispose()
        {
            base.Dispose();
            aAcionamentos = null;
            aEntidades = null;
            aDividas = null;

        }        
    }
}
