using System;

namespace NBCobranca.Classes
{
    /// <summary>
    /// Regras de Negócio da Baixa de Dívidas
    /// </summary>
    public class LimBaixa : IDisposable
    {
        private bool aBaixandoDividaUnica;
        private bool aExisteBaixaParcial;
        private NBdbm.Fachadas.plxCOBR.LancamentoBaixa aLancBaixa;
        private Sistema aParent;

        public LimBaixa(Sistema pSistema)
        {
            this.aParent = pSistema;
        }

        public System.Data.DataView DataSourceCobradores
        {
            get
            {
                return this.aParent.LimAcionamentos.DataSourceUsuarios;
            }
        }

        public System.Collections.ICollection DataSourceDividas
        {
            get
            {

                if (this.aBaixandoDividaUnica)
                {
                    System.Collections.SortedList slDividas = new System.Collections.SortedList();
                    slDividas.Add(this.aParent.LimAcionamentos.FichaDevedor.Divida.Key, this.aParent.LimAcionamentos.FichaDevedor.Divida);
                    //if (this.aParent.LimAcionamentos.ColSortedList.Contains(Tipos.TipoColecoes.Dividas))
                    //    this.aParent.LimAcionamentos.ColSortedList.Remove(Tipos.TipoColecoes.Dividas);
                    //this.aParent.LimAcionamentos.ColSortedList.Add(Tipos.TipoColecoes.Dividas, slDividas);

                    return slDividas.Values;
                }
                else
                    return this.aParent.LimAcionamentos.DataSourceDividasVencidas;
            }
        }
        
        public int UsuarioUltimoAcionamento
        {
            get
            {
                return this.aParent.LimAcionamentos.Acionamento.idUsuario;
            }
        }

        public NBdbm.Interfaces.iCOBR.Primitivas.iBaixas Baixa
        {
            get
            {
                return this.aLancBaixa.Baixa;
            }
        }
        /// <summary>
        /// Adiciona a Dívida que deverá ser dado baixa.
        /// </summary>
        /// <param name="pKey">Chave de Acesso da Dívida na Coleção de Dívidas</param>
        /// <param name="pIdCobrador">Código do Cobrador(Acionador) que receberá a comisão</param>
        /// <param name="pPerCobrador">Porcentagem que o Cobrador(Acionador) recebe pela Dívida</param>
        /// <param name="pNumBordero">Número do Bordero que estará a Baixa.</param>
        /// <param name="pNumRecibo">Número do Recibo que foi dado ao Devedor</param>
        /// <param name="pPagoNoCliente">Boolen indicando se a dívida foi paga no Cliente ou Não. true - Sim, false - Não</param>
        public void AddDividaParaBaixar(string pKey, int pIdCobrador, double pPerCobrador, int pNumBordero, int pNumRecibo, bool pPagoNoCliente, DateTime pDataBaixa, bool pBaixaParcial)
        {
            NBdbm.Interfaces.iCOBR.Primitivas.iDivida mDivida;

            if (this.aBaixandoDividaUnica)
                mDivida = this.aParent.LimAcionamentos.FichaDevedor.Divida;
            else
                mDivida = (NBdbm.Interfaces.iCOBR.Primitivas.iDivida)this.aParent.LimAcionamentos.FichaDevedor.colecaoDividas[pKey];

            double mValorNominalParcial = this.aParent.LimAcionamentos.ValorNominalParcial(mDivida);

            mDivida.idUsuarioBaixa = this.aParent.LimLogin.UsuarioID;

            if (!pBaixaParcial || (pBaixaParcial && (this.aLancBaixa.Baixa.ValorBaixa == Math.Round(mValorNominalParcial, 2))))
                mDivida.Baixada = true;

            if (pDataBaixa.ToString("dd/MM/yyyy") != "01/01/0001")
                mDivida.DataBaixa = pDataBaixa;

            if (!pPagoNoCliente)
            {
                mDivida.idCobrador = pIdCobrador;
                mDivida.PerCobrador = pPerCobrador;
                mDivida.NumRecibo = pNumRecibo;
            }
            mDivida.BaixaParcial = pBaixaParcial;
            mDivida.BaixaNoCliente = pPagoNoCliente;
            mDivida.BorderoBaixa = pNumBordero;

            this.aExisteBaixaParcial = pBaixaParcial;
            this.aLancBaixa.Baixa.idDivida = mDivida.ID;
            this.aLancBaixa.Baixa.BaixadoCliente = pPagoNoCliente;
            this.aLancBaixa.Baixa.NumBordero = pNumBordero;
            this.aLancBaixa.Baixa.ValorNominal += mValorNominalParcial;

            this.aLancBaixa.DividasParaBaixar.Add(mDivida.Key, mDivida);
        }

        public void GetDevedor(int pCodigo)
        {
            this.aParent.LimAcionamentos.GetDevedor(pCodigo);
            if (this.TotalDividasNaoBaixadas == 0)
                throw new NBdbm.COBR_Exception("Não Existem Dívidas a Serem Baixadas", "LimBaixa.GetDevedor");
        }

        public void BaixaUnica(NBdbm.Interfaces.iCOBR.Primitivas.iDivida pDivida)
        {
            this.aBaixandoDividaUnica = true;
            this.aParent.LimAcionamentos.ZerarTotalizadores();
            this.aParent.LimAcionamentos.FichaDevedor.Divida = pDivida;
        }
        public void Salvar()
        {
            try
            {
                this.Baixa.idEntidade = this.aParent.LimAcionamentos.FichaDevedor.Entidade.ID;
                if (this.aExisteBaixaParcial && (this.aLancBaixa.DividasParaBaixar.Count > 1))
                {
                    NBdbm.Fachadas.plxCOBR.LancamentoBaixa mLancBaixa = this.aLancBaixa.CriaNovoLancamento(this.aParent.LimLogin.TipoConexao);
                    System.Collections.ArrayList mKeys = new System.Collections.ArrayList();

                    foreach (NBdbm.Interfaces.iCOBR.Primitivas.iDivida mDivida in this.aLancBaixa.DividasParaBaixar.Values)
                    {
                        if (mDivida.BaixaParcial)
                        {
                            mLancBaixa.Baixa.idDivida = mDivida.ID;
                            mLancBaixa.Baixa.idEntidade = this.LimAcionamentos.FichaDevedor.Entidade.ID;
                            mLancBaixa.Baixa.ValorBaixa = this.LimAcionamentos.ValorNominalParcial(mDivida);
                            mLancBaixa.Baixa.ValorNominal = mLancBaixa.Baixa.ValorBaixa;
                            mLancBaixa.Baixa.ValorRecebido = this.LimAcionamentos.ValorCorrigido(mDivida, mLancBaixa.Baixa.ValorNominal);
                            mLancBaixa.Baixa.DataBaixa = this.aLancBaixa.Baixa.DataBaixa;
                            mDivida.BaixaParcial = true;
                            mDivida.Baixada = true;
                            mLancBaixa.DividasParaBaixar.Add(mDivida.Key, mDivida);
                            mLancBaixa.Salvar(false);

                            this.aLancBaixa.Baixa.ValorBaixa -= mLancBaixa.Baixa.ValorBaixa;
                            this.aLancBaixa.Baixa.ValorNominal -= mLancBaixa.Baixa.ValorNominal;
                            this.aLancBaixa.Baixa.ValorRecebido -= mLancBaixa.Baixa.ValorRecebido;
                            mKeys.Add(mDivida.Key);
                        }
                    }
                    foreach (object mkey in mKeys)
                        this.aLancBaixa.DividasParaBaixar.Remove((string)mkey);

                }
                this.aLancBaixa.Salvar();
                this.aBaixandoDividaUnica = false;
                this.aExisteBaixaParcial = false;
            }
            catch (NBdbm.NBexception NBEx)
            {
                throw new NBdbm.COBR_Exception("Problemas na hora de salvar o lançamento de Baixas.", "NBCobranca.Classes.LimBaixa.Salvar", NBEx);
            }
        }
        public void LimpaColecaoDividasParaBaixar()
        {
            this.aLancBaixa.DividasParaBaixar.Clear();
        }
        public bool BaixandoDividaUnica
        {
            get
            {
                return this.aBaixandoDividaUnica;
            }

            set
            {
                this.aBaixandoDividaUnica = value;
            }
        }
        public void NovaBaixa()
        {
            if (this.aLancBaixa == null)
                this.aLancBaixa = new NBdbm.Fachadas.plxCOBR.LancamentoBaixa(this.aParent.LimLogin.TipoConexao, false);
            else
                this.aLancBaixa = this.aLancBaixa.CriaNovoLancamento(this.aParent.LimLogin.TipoConexao);
        }
        public string StatusBaixa(NBdbm.Interfaces.iCOBR.Primitivas.iDivida pDivida)
        {
            string mTagImage = "<img src='../imagens/{0}' class='webuiPopover' data-content='{1}' data-delay-show='0' data-delay-hide='1000' data-title='Resumo da Baixa' data-placement='left' border='0'>";
            string mToolTipBaixaParcial = "<p>Divida Parcialmente Baixada {0}</p>O Valor Nominal Total é de {1}<br>A Ultima Baixa Parcial foi em: {2}";
            string mToolTipBaixada = "<p>Divida Baixada {0}</p>Em {1}";
            string mOrigemBaixa = "na Cobradora";
            string mGif = "baixa_cob.gif";
            if (pDivida.BaixaNoCliente)
            {
                mOrigemBaixa = "no Cliente";
                mGif = "baixa_cli.gif";
            }

            switch (pDivida.Baixada)
            {
                case false:
                    if (pDivida.BaixaParcial)
                    {
                        mGif = "baixa_par.gif";
                        mToolTipBaixaParcial = string.Format(mToolTipBaixaParcial, mOrigemBaixa, pDivida.ValorNominal.ToString("C"), pDivida.DataBaixa.ToString("dd/MM/yyyy"));
                        mTagImage = string.Format(mTagImage, mGif, mToolTipBaixaParcial);
                    }
                    else
                        mTagImage = string.Format(mTagImage, "Baixa_Nao.Gif", "Divida não Baixada");
                    break;
                case true:
                    mToolTipBaixada = string.Format(mToolTipBaixada, mOrigemBaixa, pDivida.DataBaixa.ToString("dd/MM/yyyy"));
                    mTagImage = string.Format(mTagImage, mGif, mToolTipBaixada);
                    break;
            }
            return mTagImage;
        }
        public LimAcionamentos LimAcionamentos
        {
            get { return this.aParent.LimAcionamentos; }
        }

        private int TotalDividasNaoBaixadas
        {
            get
            {
                System.Data.SqlClient.SqlCommand mSC = new System.Data.SqlClient.SqlCommand();
                System.Text.StringBuilder mSB = new System.Text.StringBuilder();

                mSB.Append("SELECT COUNT(Id)FROM dbo.COBR_Divida ");
                mSB.Append("WHERE Baixada = 0 AND IdEntidade = ");
                mSB.Append(this.aParent.LimAcionamentos.FichaDevedor.Entidade.ID.ToString());

                mSC.CommandText = mSB.ToString();
                mSC.Connection = (System.Data.SqlClient.SqlConnection)this.aParent.Connection;
                if (mSC.Connection.State == System.Data.ConnectionState.Closed)
                    mSC.Connection.Open();
                return (int)mSC.ExecuteScalar();
            }
        }

        #region IDisposable Members
        public void Dispose()
        {
            if (this.aLancBaixa != null)
            {
                this.aLancBaixa.Dispose();
                this.aLancBaixa = null;
            }
        }

        #endregion
    }
}
