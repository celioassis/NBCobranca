using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using MigraDoc.DocumentObjectModel;
using NBCobranca.Entidades;
using NBCobranca.Tipos;
using System.Linq;

namespace NBCobranca.Classes
{
    /// <summary>
    /// Manipula os Acionamentos em Geral.
    /// </summary>
    public class LimAcionamentos : IDisposable
    {
        protected Classes.Sistema aParent;
        protected NBdbm.tipos.tiposConection aTipoConexao;
        protected NBdbm.Fachadas.plxCOBR.CadastroAcionamentos aCadAcionamentos;
        protected System.Data.DataView aDataSourceTipoDivida;
        protected System.Data.DataView aDataSourceTipoAcionamento;
        protected System.Data.DataView aDataSourceUsuarios;
        //protected System.Collections.Hashtable aSortedList = new System.Collections.Hashtable();
        private Relatorio aRelatorio;
        private string aFiltroCarteira = "";
        private string aFiltroTipoDivida = "";
        private string aFiltroNumDividas = "";
        private string aFiltroNome = "";
        private string aFiltroPeriodo = "";
        private string aFiltroDevedor = null;
        private string aFiltroTipoAcionamento = "";
        private string aFiltroAcionador = "";
        private string aCarteira = "";
        private double aJuros;
        private double aMulta;
        protected double aTotalNominal;
        protected double aTotalCorrigido;
        private bool aCartasRegistradas = false;
        internal bool aSomenteUsuariosAtivos = false;
        private NBdbm.self aSelf;
        private List<NBCobranca.Tipos.DtoSms> aListaDtoSMS = new List<DtoSms>();
        protected entCTRL_Entidades AcionadorDaFicha;

        public LimAcionamentos(Classes.Sistema pParent)
        {
            this.aParent = pParent;
            this.aSelf = pParent.Self;
            this.aTipoConexao = pParent.LimLogin.TipoConexao;
            if (pParent.TipoEntidade != NBdbm.tipos.TipoEntidade.Devedores)
            {
                pParent.TipoEntidade = NBdbm.tipos.TipoEntidade.Devedores;
                pParent.LimEntidades.NovaEntidade(false);
            }
            this.aCadAcionamentos = new NBdbm.Fachadas.plxCOBR.CadastroAcionamentos(this.aParent.LimLogin.TipoConexao, pParent.LimEntidades.CadEntidade);

            this.aDataSourceUsuarios = this.CriarDataSourceUsuarios(false);
            this.CriarDataSourceTipoDivida();
            this.CriarDataSourceTipoAcionamento();
        }
        /// <summary>
        /// Retorna uma DataView com as Carteiras de Cobrança Existentes.
        /// </summary>
        public System.Data.DataView Carteiras
        {
            get
            {
                string mSQL = "SELECT Nome FROM dbo.CTRL_Nos WHERE Path LIKE '\\Entidades\\Carteiras\\%'";
                return NBFuncoes.DataView(mSQL, this.aParent.Connection);
            }
        }
        /// <summary>
        /// Retorna uma DataView com os Tipos de Dívidas.
        /// </summary>
        public System.Data.DataView TiposDivida
        {
            get
            {
                return this.aDataSourceTipoDivida;
            }
        }
        public System.Data.DataView TiposAcionamento
        {
            get
            {
                return this.aDataSourceTipoAcionamento;
            }
        }
        /// <summary>
        /// Preenche o DataGrid de Acionamentos fazendo a paginação.
        /// </summary>
        /// <param name="pDataGrid">DataGrid de Acionamentos</param>
        /// <param name="pCurrentPage">Página Corrente</param>
        public void FillDataGrid(Anthem.DataGrid pDataGrid, int pCurrentPage)
        {
            //System.Data.SqlClient.SqlCommand mSqlCmd;
            System.Data.DataView mDV;

            //mSqlCmd = NBFuncoes.SqlCmdPaginacao(this.SqlAcionamento,"idEntidade",1,pCurrentPage,pDataGrid.PageSize,this.aTipoConexao);
            //mDV = NBFuncoes.DataView(mSqlCmd);
            //pDataGrid.VirtualItemCount = ((int)mSqlCmd.Parameters["@ROWS_TOTAL"].Value);

            //Faz a Consulta sem a paginação
            mDV = this.FazerReacionamento(NBFuncoes.DataView(this.SqlAcionamento, this.aParent.Connection));
            pDataGrid.VirtualItemCount = mDV.Count;
            if (pCurrentPage > (pDataGrid.VirtualItemCount / pDataGrid.PageSize))
                pDataGrid.CurrentPageIndex = 0;
            pDataGrid.DataSource = mDV;
            pDataGrid.DataBind();
        }
        public void CriarFiltros(string pCarteira, string pNome, int pTipoDivida, int pQuantasDividas, DateTime? pPeriodoInicial, DateTime? pPeriodoFinal, string IdEntidadeDoUsuario = null, string IdTipoAcionamento = null)
        {
            //Cria o Filtro para Carteiras
            this.aFiltroCarteira = "";
            if (pCarteira != "Todas")
                this.aFiltroCarteira = string.Format("<Entidades><Carteiras><{0}>", pCarteira);

            //Cria o Filtro para Tipo de Dívida
            this.aFiltroTipoDivida = "";
            if (pTipoDivida > 0)
                aFiltroTipoDivida = pTipoDivida.ToString();

            //Cria o Filtro de Quantidade de Dívidas
            this.aFiltroNumDividas = "";
            if (pQuantasDividas > 0)
            {
                this.aFiltroNumDividas = "COUNT(COBR_Divida.Id) {0} {1}";

                if (pQuantasDividas > 3)
                    this.aFiltroNumDividas = string.Format(aFiltroNumDividas, ">=", pQuantasDividas);
                else
                    this.aFiltroNumDividas = string.Format(aFiltroNumDividas, "=", pQuantasDividas);
            }

            //Cria o Filtro para Nome de Devedor
            this.aFiltroNome = "";
            if (pNome != "")
                this.aFiltroNome = pNome;

            this.aFiltroPeriodo = "";
            if (pPeriodoInicial != null && pPeriodoFinal != null)
            {
                pPeriodoFinal = pPeriodoFinal.Value.AddDays(1).AddSeconds(-1);
                this.aFiltroPeriodo = string.Format("COBR_Divida.DataVencimento >= {0} and COBR_Divida.DataVencimento <= {1}",
                    NBFuncoes.FormatCampoToSQL(pPeriodoInicial.Value), NBFuncoes.FormatCampoToSQL(pPeriodoFinal.Value));
            }

            aFiltroAcionador = string.Empty;
            if (IdEntidadeDoUsuario != null && IdEntidadeDoUsuario != "0" && aParent.LimLogin.Credencial != TipoCredencial.Acionador)
                aFiltroAcionador = $"and lnkEE.idEntidadeBase ={IdEntidadeDoUsuario}";

            aFiltroTipoAcionamento = string.Empty;
            if (IdTipoAcionamento != null && IdTipoAcionamento != "0")
                aFiltroTipoAcionamento = $"COBR_Acionamentos.IdTipoAcionamento={IdTipoAcionamento}";

        }
        public List<Tipos.dtoCarta> ListaCartas(int pIDCarta, bool p2Aviso, int? pIDEntidade)
        {
            if (pIDEntidade != null)
                this.aFiltroDevedor = pIDEntidade.ToString();

            List<dtoCarta> mListaCartas = null;
            DataSet mCartas = NBFuncoes.DataSet(this.SqlCartas, this.aParent.Connection);
            DataView mCartasFiltradas = this.FazerReacionamento(mCartas.Tables[0].DefaultView);

            this.aFiltroDevedor = null;

            if (mCartasFiltradas.Count == 0)
                return mListaCartas;


            mListaCartas = new List<dtoCarta>();
            var busEmail = aParent.GetInstance<BusEmail>();

            foreach (DataRowView mDRV in mCartasFiltradas)
            {
                DtoCartaDadosDevedor mDevedor = new DtoCartaDadosDevedor();
                mDevedor.Nome = mDRV["NomePrimary"].ToString();
                mDevedor.Logradouro = mDRV["Logradouro"].ToString();
                mDevedor.Complemento = mDRV["Complemento"].ToString();
                mDevedor.Comentario = mDRV["Comentario"].ToString();
                mDevedor.Bairro = mDRV["Bairro"].ToString();
                mDevedor.Cidade = mDRV["Municipio"].ToString();
                mDevedor.UF = mDRV["UF"].ToString();
                mDevedor.CEP = mDRV["CEP"].ToString();
                mDevedor.Protocolo = mDRV["Contrato"].ToString();
                if (pIDEntidade != null)
                    mDevedor.Email = busEmail.EmailsDoDevedor(pIDEntidade.Value);
                mListaCartas.Add(new dtoCarta(GetCarta(pIDCarta, p2Aviso), mDevedor));
            }
            return mListaCartas;
        }

        private NBCobranca.Tipos.dtoCarta GetCarta(int pIDCarta, bool p2Aviso)
        {
            var rodape = new List<DtoCartaLinha>
                    {
                        new DtoCartaLinha {Texto = "Cordialmente.\n\n", ParagraphAlignment = ParagraphAlignment.Center},
                        new DtoCartaLinha {Texto = "AÇÃO – COBRANÇAS EXTRAS JUDICIAIS LTDA.\n", ParagraphAlignment = ParagraphAlignment.Center, TextFormat=TextFormat.Italic},
                        new DtoCartaLinha {Texto = "Departamento Jurídico.\n", ParagraphAlignment = ParagraphAlignment.Center, TextFormat=TextFormat.Italic}
                    };

            switch (pIDCarta)
            {
                case 1:
                    var tituloSegundoAviso = new List<DtoCartaLinha>
                    {
                        new DtoCartaLinha {Texto = "NOTIFICAÇÃO URGENTE\n", TextFormat = TextFormat.Bold},
                        new DtoCartaLinha {Texto = "2° AVISO C/ PROTOCOLO DE NEGATIVAÇÃO", TextFormat = TextFormat.Bold}
                    };
                    return new dtoCarta(p2Aviso, tituloSegundoAviso, ConteudoCarta3, rodape);
                default:
                    return new NBCobranca.Tipos.dtoCarta(p2Aviso,
                        new List<DtoCartaLinha>
                        {
                            new DtoCartaLinha { Texto = "2º NOTIFICAÇÃO EXTRAJUDICIAL", TextFormat = TextFormat.Bold }
                        },
                        ConteudoCarta2,
                        rodape
                        );
            }
        }
        public System.Data.DataView DataSourceCartas
        {
            get
            {
                return this.FazerReacionamento(NBFuncoes.DataView(this.SqlCartas, this.aParent.Connection), true);
            }
        }
        public virtual void GetDevedor(int pCodigo)
        {
            this.ZerarTotalizadores();
            this.CadAcionamentos.GetFieldsFromEntidade(pCodigo);
            AcionadorDaFicha = aParent.busAcionamentos.GetAcionador(pCodigo);
        }

        public void EditarFichaDevedor()
        {
            //this.aParent.LimEntidades.Consulta(this.FichaDevedor.Entidade.ID.ToString());
            this.aParent.LimEntidades.EditarCadastro(this.FichaDevedor);
        }
        public NBdbm.Fachadas.plxCOBR.CadastroEntidade FichaDevedor
        {
            get
            {
                return this.CadAcionamentos.FichaDevedor;
            }
        }
        public string GetDescricaoTipoDivida(int pIdTipoDivida)
        {
            string mSelect = "id = " + pIdTipoDivida.ToString();
            System.Data.DataRow mDR = this.aDataSourceTipoDivida.Table.Select(mSelect)[0];
            return (string)mDR[1];
        }

        public string GetDescricaoTipoAcionamento(int id)
        {
            string mSelect = "id = " + id.ToString();
            System.Data.DataRow mDR = this.aDataSourceTipoAcionamento.Table.Select(mSelect)[0];
            return (string)mDR[1];

        }
        public string GetNomeUsuario(int id)
        {
            string mSelect = "idUsuario = " + id.ToString();
            if (this.aSomenteUsuariosAtivos)
                this.aDataSourceUsuarios = CriarDataSourceUsuarios();
            System.Data.DataRow mDR = this.aDataSourceUsuarios.Table.Select(mSelect)[0];
            return (string)mDR[2];
        }
        public double DividaTotalNominal
        {
            get
            {
                return this.aTotalNominal;
            }
        }
        public double DividaTotalCorrigida
        {
            get
            {
                return this.aTotalCorrigido;
            }
        }
        public double ValorCorrigido(NBdbm.Interfaces.iCOBR.Primitivas.iDivida pDivida, double pValorNominal)
        {

            string mCarteira;
            double mJurosDia;
            double mValorJuros = 0;
            double mValorMulta = 0;
            double mValorNominal;
            TimeSpan mPeriodoDivida;
            System.Text.StringBuilder mSQL = new System.Text.StringBuilder();
            System.Data.DataTable mDT;

            if (!pDivida.Baixada && pDivida.BaixaParcial)
                mValorNominal = this.ValorNominalParcial(pDivida);
            else
                mValorNominal = pValorNominal;

            mPeriodoDivida = DateTime.Today.Subtract(pDivida.DataVencimento);
            mCarteira = pDivida.XmPathCliente.Remove(0, 23);
            mCarteira = mCarteira.Replace(">", "");
            if (this.aCarteira != mCarteira)
            {
                this.aCarteira = mCarteira;
                mSQL.Append("SELECT dbo.COBR_Tarifas.Juros, dbo.COBR_Tarifas.Multa ");
                mSQL.Append("FROM dbo.CTRL_Entidades INNER JOIN ");
                mSQL.Append("dbo.COBR_Tarifas ON dbo.CTRL_Entidades.IdEntidade = dbo.COBR_Tarifas.IdEntidade ");
                mSQL.Append("WHERE dbo.CTRL_Entidades.NomePrimary = N'");
                mSQL.Append(mCarteira);
                mSQL.Append("'");

                mDT = NBFuncoes.DataView(mSQL.ToString(), this.aParent.Connection).Table;
                if (mDT.Rows.Count > 0)
                {
                    this.aJuros = Convert.ToDouble(mDT.Rows[0][0]);
                    this.aMulta = Convert.ToDouble(mDT.Rows[0][1]);
                }
                else
                {
                    this.aJuros = 2;
                    this.aMulta = 2;
                }
            }

            mJurosDia = this.aJuros / 30;
            if (pDivida.DataVencimento < DateTime.Today)
            {
                mValorJuros = (mValorNominal * (mPeriodoDivida.Days * mJurosDia)) / 100;
                mValorMulta = (mValorNominal * this.aMulta) / 100;
            }
            if (!pDivida.Baixada)
            {
                this.aTotalNominal += mValorNominal;
                this.aTotalCorrigido += (mValorNominal + mValorMulta + mValorJuros);
            }
            return (mValorNominal + mValorMulta + mValorJuros);
        }
        /// <summary>
        /// Corrige um Valor informado com base no número de parcelas e no número
        /// de dias entre cada parcela e retorna o valor de uma parcela.
        /// </summary>
        /// <param name="pNumTotalParcelas"></param>
        /// <param name="pNumDiasEntreParcela"></param>
        /// <param name="pTotalDividaCorrigida"></param>
        /// <returns></returns>
        public void CalculaParcelamento(DateTime pDataParcela1, int pNumTotalParcelas, int pNumDiasEntreParcela, double pTotalDividaCorrigida, double pValorEntrada, ref double pRetValorTotalParcelamento, ref double pRetValorParcela, ref DateTime pRetDataUltimaParcela, double? pJurosPersonalizados)
        {
            double mJurosDia;
            double mValorJuros = 0;
            double mValorCorrigido = 0;
            int mPeriodoDivida;
            TimeSpan mPeriodoDivida1;

            mPeriodoDivida1 = pDataParcela1.Subtract(DateTime.Today);
            if (pNumTotalParcelas > 1)
                mPeriodoDivida = (mPeriodoDivida1.Days + pNumDiasEntreParcela * (pNumTotalParcelas - 1));
            else
                mPeriodoDivida = mPeriodoDivida1.Days;
            mJurosDia = pJurosPersonalizados == null ? this.aJuros / 30 : pJurosPersonalizados.Value / 30;
            mValorJuros = ((pTotalDividaCorrigida - pValorEntrada) * (mPeriodoDivida * mJurosDia)) / 100;
            mValorCorrigido = (pTotalDividaCorrigida - pValorEntrada + mValorJuros);

            pRetValorTotalParcelamento = mValorCorrigido;
            pRetValorParcela = mValorCorrigido / pNumTotalParcelas;
            if (pNumTotalParcelas == 1)
                pRetDataUltimaParcela = pDataParcela1;
            else
            {
                pDataParcela1 = pDataParcela1.Subtract(mPeriodoDivida1);
                pRetDataUltimaParcela = pDataParcela1.AddDays(mPeriodoDivida);
            }

        }
        public double ValorNominalParcial(NBdbm.Interfaces.iCOBR.Primitivas.iDivida pDivida)
        {
            double mValorNominalParcial = this.ValorNominalParcial(pDivida.ID, pDivida.ValorNominal);
            pDivida.ValorNominalParcial = mValorNominalParcial;
            return mValorNominalParcial;
        }
        public double ValorNominalParcial(int pIdDivida, double pValorNominal)
        {
            System.Data.DataView mDV;
            double mValorBaixaParcial = pValorNominal;
            NBdbm.Fachadas.plxCOBR.primitivas.Baixas mBaixas = new NBdbm.Fachadas.plxCOBR.primitivas.Baixas(ref aSelf, this.aParent.LimLogin.TipoConexao, false);
            mBaixas.filterWhere = "idDivida = " + pIdDivida.ToString();
            mDV = mBaixas.DataSource();
            foreach (System.Data.DataRow mDR in mDV.Table.Rows)
            {
                mValorBaixaParcial -= double.Parse(mDR["ValorBaixa"].ToString());
            }
            return mValorBaixaParcial;
        }

        public string GetCarteiraAtual
        {
            get
            {
                return this.aCarteira;
            }
        }
        public string TipoAcionamentoPadrao
        {
            get
            {
                string mSelect = "Descricao = 'Só Chama'";
                System.Data.DataRow mDR = this.aDataSourceTipoAcionamento.Table.Select(mSelect)[0];
                return (string)mDR[2];
            }
        }
        public System.Data.DataView DataSourceUsuarios
        {
            get
            {
                return this.aDataSourceUsuarios;
            }
        }
        public System.Data.DataView DataSourceUsuariosAtivos
        {
            get { return CriarDataSourceUsuarios(true); }

        }
        public System.Collections.ICollection DataSourceAcionamentos
        {
            get
            {
                return this.CadAcionamentos.ColecaoAcionamentos.Values;
                //System.Collections.SortedList mSL = new System.Collections.SortedList(this.CadAcionamentos.ColecaoAcionamentos);
                //if (aSortedList.Contains(Tipos.TipoColecoes.Dividas))
                //    aSortedList[Tipos.TipoColecoes.Dividas] = mSL;
                //else
                //    aSortedList.Add(Tipos.TipoColecoes.Dividas, mSL);
                //return mSL.Values;
            }
        }
        public virtual System.Collections.ICollection DataSourceDividas
        {
            get
            {
                this.ZerarTotalizadores();
                return this.FichaDevedor.colecaoDividas.Values;
                /*
                System.Collections.SortedList slDividas = new System.Collections.SortedList(this.FichaDevedor.colecaoDividas);
                if (aSortedList.Contains(Tipos.TipoColecoes.Dividas))
                    aSortedList.Remove(Tipos.TipoColecoes.Dividas);
                aSortedList.Add(Tipos.TipoColecoes.Dividas, slDividas);

                return slDividas.Values;
                */
            }
        }
        public System.Collections.ICollection DataSourceDividasVencidas
        {
            get
            {
                this.ZerarTotalizadores();
                System.Collections.SortedList mSL = new System.Collections.SortedList();
                foreach (NBdbm.Interfaces.iCOBR.Primitivas.iDivida mDivida in FichaDevedor.colecaoDividas.Values)
                {
                    if (!mDivida.Baixada)
                        mSL.Add(mDivida.Key, mDivida);
                }
                //if (aSortedList.Contains(Tipos.TipoColecoes.DividasVencidas))
                //    aSortedList.Remove(Tipos.TipoColecoes.DividasVencidas);
                //aSortedList.Add(Tipos.TipoColecoes.DividasVencidas, mSL);

                return mSL.Values;
            }
        }
        //public System.Collections.Hashtable ColSortedList
        //{
        //    get
        //    {
        //        return this.aSortedList;
        //    }
        //}
        public void NovoAcionamento()
        {
            this.CadAcionamentos.NovoAcionamento();
        }
        public void Salvar()
        {
            try
            {
                this.CadAcionamentos.Salvar();
                this.GetDevedor(FichaDevedor.Entidade.ID);
            }
            catch (NBdbm.NBexception NBEx)
            {
                throw new NBdbm.COBR_Exception("Problemas na hora de salvar o Acionamento", "NBCobranca - LimAcionamentos.Salvar", NBEx);
            }
        }
        public void CriarSMS(int pCodigoDevedor)
        {
            this.CriarSMS(pCodigoDevedor, false);
        }
        public void CriarSMS(int pCodigoDevedor, bool pUnico)
        {
            if (pUnico)
            {
                this.aListaDtoSMS = null;
                this.aListaDtoSMS = new List<DtoSms>();
            }
            this.aListaDtoSMS.Add(new DtoSms(pCodigoDevedor, null, null, null));
        }

        public List<DtoSms> ListaDtoSMS
        {
            get
            {
                if (aListaDtoSMS == null)
                    this.aListaDtoSMS = new List<DtoSms>();
                return aListaDtoSMS;
            }
        }

        public void AdicionarAcionamento()
        {
            this.CadAcionamentos.ColecaoNovosAcionamentos.Add(this.CadAcionamentos.Acionamento.Key, this.CadAcionamentos.Acionamento);
            this.CadAcionamentos.ColecaoAcionamentos.Add(this.CadAcionamentos.Acionamento.Key, this.CadAcionamentos.Acionamento);
        }

        public NBdbm.Interfaces.iCOBR.Primitivas.iAcionamentos Acionamento
        {
            get
            {
                return this.CadAcionamentos.Acionamento;
            }
        }

        public void RegistrarCartas(bool pSegundoAviso)
        {
            if (this.aCartasRegistradas)
                throw new NBdbm.COBR_Exception("Cartas Já Registradas", "Registro de Cartas");
            try
            {
                System.Data.SqlClient.SqlCommand mComand = new System.Data.SqlClient.SqlCommand(this.SqlCartas, (System.Data.SqlClient.SqlConnection)this.aParent.Connection);
                if (this.aParent.Connection.State == System.Data.ConnectionState.Closed)
                    this.aParent.Connection.Open();
                System.Data.IDataReader mDReader = mComand.ExecuteReader();
                int idAcionaCartas = Convert.ToInt32(this.TiposAcionamento.Table.Select("Descricao='Carta'")[0][0]);
                this.CadAcionamentos.ColecaoNovosAcionamentos.Clear();
                DateTime mDataAcionamento = DateTime.Now;
                while (mDReader.Read())
                {
                    if (mDReader["DescricaoAcionamento"].ToString().ToUpper() == "PROMESSA")
                    {
                        DateTime mDataPromesa = Convert.ToDateTime(mDReader["DataPromessa"]);
                        //Verifica se o devedor deve ser reacionado pela sua Data de Promessa.
                        if (DateTime.Today < mDataPromesa)
                        {
                            int mCredencial;
                            System.Data.DataRow mDR;
                            mDR = this.relatorio.DataSourceUsuarios(true).Table.Select("idUsuario = " + mDReader["idUsuario"].ToString())[0];
                            mCredencial = Convert.ToInt32(mDR["Credencial"]);
                            if (mCredencial == 3)
                                continue;
                        }
                    }
                    if (mDReader["DescricaoAcionamento"].ToString().ToUpper() == "NÃO COBRAR")
                        continue;
                    mDataAcionamento = mDataAcionamento.AddSeconds(1);
                    this.NovoAcionamento();
                    this.Acionamento.idEntidade = Convert.ToInt32(mDReader["idEntidade"]);
                    this.Acionamento.idTipoAcionamento = idAcionaCartas;
                    this.Acionamento.idUsuario = this.aParent.LimLogin.UsuarioID;
                    if (pSegundoAviso)
                        this.Acionamento.TextoRespeito = "Segundo Aviso";
                    else
                        this.Acionamento.TextoRespeito = "Primeiro Aviso";
                    this.Acionamento.DataAcionamento = mDataAcionamento;
                    this.AdicionarAcionamento();
                }
                int a = this.CadAcionamentos.ColecaoNovosAcionamentos.Count;
                int b = 0;
                foreach (NBdbm.Interfaces.iCOBR.Primitivas.iAcionamentos mAllAcionamentos in this.CadAcionamentos.ColecaoNovosAcionamentos.Values)
                {
                    b++;
                    if (b == a)
                        mAllAcionamentos.salvar();
                    else
                        mAllAcionamentos.salvar(true);
                }
                this.aParent.Connection.Close();
                this.aCartasRegistradas = true;
            }
            catch (Exception ex)
            {
                this.aParent.Connection.Close();
                throw new NBdbm.NBexception("Não foi Possível Registrar as Cartas, contate o Suporte Técnico", ex);
            }
        }
        public bool CartasRegistradas
        {
            get
            {
                return this.aCartasRegistradas;
            }
            set
            {
                this.aCartasRegistradas = value;
            }
        }
        public Relatorio relatorio
        {
            get
            {
                if (this.aRelatorio == null)
                    this.aRelatorio = new Relatorio(this);
                return this.aRelatorio;
            }
        }
        /// <summary>
        /// Busca a Chave de pesquisa das coleções classificadas conforme o seu Index
        /// </summary>
        /// <param name="pTipoColecao">Tipo da coleção que deverá buscar a Chave</param>
        /// <param name="pIndex">Indice do Item na Coleção</param>
        /// <returns>Retorna uma string com a Chave</returns>
        public string GetKey(Tipos.TipoColecoes pTipoColecao, int pIndex)
        {
            //return ((System.Collections.SortedList)aSortedList[pTipoColecao]).GetKey(pIndex).ToString();
            switch (pTipoColecao)
            {
                case NBCobranca.Tipos.TipoColecoes.Endereco:
                    return aCadAcionamentos.FichaDevedor.colecaoEnderecos.GetKey(pIndex) as string;
                case NBCobranca.Tipos.TipoColecoes.Telefone:
                    return aCadAcionamentos.FichaDevedor.colecaoTelefones.GetKey(pIndex) as string;
                case NBCobranca.Tipos.TipoColecoes.Email:
                    return aCadAcionamentos.FichaDevedor.colecaoEmail.GetKey(pIndex) as string;
                case NBCobranca.Tipos.TipoColecoes.Site:
                    return aCadAcionamentos.FichaDevedor.colecaoUrl.GetKey(pIndex) as string;
                case NBCobranca.Tipos.TipoColecoes.Dividas:
                    return aCadAcionamentos.FichaDevedor.colecaoDividas.GetKey(pIndex) as string;
                default:
                    return "";
            }
        }
        public void ZerarTotalizadores()
        {
            this.aTotalCorrigido = 0;
            this.aTotalNominal = 0;
        }
        public void LimparAcionamentosTemporarios()
        {
            foreach (NBdbm.Interfaces.iCOBR.Primitivas.iAcionamentos mAcionamento in this.aCadAcionamentos.ColecaoNovosAcionamentos.Values)
            {
                this.aCadAcionamentos.ColecaoAcionamentos.Remove(mAcionamento.Key);
            }
            this.aCadAcionamentos.ColecaoNovosAcionamentos.Clear();

        }
        /// <summary>
        /// Verifica se um Devedor deve ser acionado ou não, com base nos seus
        /// parametros.
        /// </summary>
        /// <param name="pDataView">
        /// DataView com o resultado da pesquisa
        /// </param>
        /// <param name="pCartas">
        /// Indica se a verificação é para cartas.
        /// </param>
        private System.Data.DataView FazerReacionamento(System.Data.DataView pDataView, bool pCartas)
        {
            if (this.aFiltroNome != "")
                return pDataView;
            foreach (System.Data.DataRow dr in pDataView.Table.Rows)
            {

                System.DateTime mDataPromesa, mDataUltimoAcionamento;
                int mDiasReacionar;

                //Verifica se já houve acionamento
                if (dr["UltimoAcionamento"].ToString() != "")
                    mDataUltimoAcionamento = Convert.ToDateTime(dr["UltimoAcionamento"]);
                else
                    continue;

                //Verifica se Existe Dias para Reacionamento
                if (dr["DiasReacionamento"].ToString() != "")
                    mDiasReacionar = int.Parse(dr["DiasReacionamento"].ToString());
                else
                    continue;
                if (mDiasReacionar == 1)
                    continue;
                //Se for maior que zero é verificado se a data de Devedor deve
                //ser reacionado.
                if (mDiasReacionar > 0 && !pCartas)
                {
                    DateTime mDataReacionar = mDataUltimoAcionamento.Date.AddDays(mDiasReacionar);
                    if (DateTime.Today >= mDataReacionar)
                        continue;
                    else
                    {
                        dr.Delete();
                        continue;
                    }
                }

                if (pCartas)
                    if (dr["DescricaoAcionamento"].ToString().ToUpper() == "NÃO COBRAR")
                    {
                        dr.Delete();
                        continue;
                    }

                //Verifica se Existe uma Data de Promessa
                if (dr["DataPromessa"].ToString() != "")
                    mDataPromesa = Convert.ToDateTime(dr["DataPromessa"]);
                else
                    continue;

                //Verifica se o devedor deve ser reacionado pela sua Data de Promessa.
                if (DateTime.Today > mDataPromesa)
                    continue;
                else
                {
                    if (pCartas)
                    {
                        int mCredencial;
                        System.Data.DataRow[] mUsuarios;
                        mUsuarios = this.relatorio.DataSourceUsuarios(true).Table.Select("idUsuario = " + dr["idUsuario"].ToString());
                        if (mUsuarios.Length > 0)
                        {
                            mCredencial = Convert.ToInt32(mUsuarios[0]["Credencial"]);

                            if (mCredencial == 3)
                                dr.Delete();
                        }
                    }
                    else
                        dr.Delete();
                }
            }
            return pDataView;
        }

        private System.Data.DataView FazerReacionamento(System.Data.DataView pDataView)
        {
            return this.FazerReacionamento(pDataView, false);
        }
        /// <summary>
        /// Retorna a SQL para gerar a DataView da Grid de Acionamentos.
        /// </summary>
        private string SqlAcionamento
        {
            get
            {
                System.Text.StringBuilder mSqlPadrao = new System.Text.StringBuilder();
                mSqlPadrao.AppendLine("SELECT  CTRL_Entidades.IdEntidade, NomePrimary, NomeAcionador, ");
                mSqlPadrao.AppendLine("CTRL_Entidades.dtAlteracao, DataAcionamento AS UltimoAcionamento, DataPromessa");
                mSqlPadrao.AppendLine(", DiasReacionamento");
                mSqlPadrao.AppendLine("FROM (SELECT MAX(ID) AS IdAcionamento, IdEntidade");
                mSqlPadrao.AppendLine("		FROM COBR_Acionamentos");
                mSqlPadrao.AppendLine("		GROUP BY idEntidade");
                mSqlPadrao.AppendLine(") LastAC JOIN COBR_Acionamentos ON LastAC.IdAcionamento = COBR_Acionamentos.Id");
                if (!string.IsNullOrEmpty(aFiltroTipoAcionamento))
                    mSqlPadrao.AppendLine($"   AND {aFiltroTipoAcionamento}");
                mSqlPadrao.AppendLine("JOIN COBR_TipoAcionamento ON COBR_Acionamentos.idTipoAcionamento = COBR_TipoAcionamento.Id");
                if (string.IsNullOrEmpty(aFiltroTipoAcionamento) && string.IsNullOrEmpty(aFiltroAcionador))
                    mSqlPadrao.Append("RIGHT OUTER ");
                mSqlPadrao.AppendLine("JOIN dbo.CTRL_Entidades ON CTRL_Entidades.IdEntidade = LastAC.IdEntidade");
                mSqlPadrao.AppendLine("JOIN COBR_Divida ON CTRL_Entidades.IdEntidade = COBR_Divida.IdEntidade");
                mSqlPadrao.AppendLine(" AND COBR_Divida.Baixada = 0");
                if (!string.IsNullOrEmpty(aFiltroCarteira))
                    mSqlPadrao.AppendFormat("	AND COBR_Divida.XmPathCliente = N'{0}'\r\n", aFiltroCarteira);
                if (!string.IsNullOrEmpty(aFiltroTipoDivida))
                    mSqlPadrao.AppendFormat("   AND COBR_Divida.IdTipoDivida = {0}\r\n", aFiltroTipoDivida);
                if (!string.IsNullOrEmpty(aFiltroPeriodo))
                    mSqlPadrao.AppendFormat("   AND {0}\r\n", aFiltroPeriodo);

                //Traz somente as fichas da carteira do usuário logado.
                if (aParent.LimLogin.Credencial == TipoCredencial.Acionador && string.IsNullOrEmpty(aFiltroNome))
                    mSqlPadrao.AppendFormat(
                        "JOIN CTRL_Link_EntidadeEntidade lnkEE ON lnkEE.idEntidadeLink = CTRL_Entidades.IdEntidade and lnkEE.idEntidadeBase = {0}",
                        this.aParent.LimLogin.IdEntidade);
                else if (!string.IsNullOrEmpty(aFiltroAcionador))
                    mSqlPadrao.AppendLine($"JOIN CTRL_Link_EntidadeEntidade lnkEE ON lnkEE.idEntidadeLink = CTRL_Entidades.IdEntidade {aFiltroAcionador}");
                else
                    mSqlPadrao.AppendLine("left JOIN CTRL_Link_EntidadeEntidade lnkEE ON lnkEE.idEntidadeLink = CTRL_Entidades.IdEntidade ");

                //left Join para Trazer a Carteira de Acionamento do devedor
                if (string.IsNullOrEmpty(aFiltroAcionador))
                    mSqlPadrao.Append("left ");
                mSqlPadrao.AppendLine("join (select NomePrimary NomeAcionador, idEntidadeLink");
                mSqlPadrao.AppendLine("            from CTRL_Entidades");
                mSqlPadrao.AppendLine("            join CTRL_Link_EntidadeEntidade on idEntidadeBase = IdEntidade");
                mSqlPadrao.AppendLine(") AcionadoPor on AcionadoPor.idEntidadeLink = CTRL_Entidades.IdEntidade");

                if (!string.IsNullOrEmpty(aFiltroNome))
                    mSqlPadrao.AppendFormat("Where CTRL_Entidades.NomePrimary like '{0}%'\r\n", aFiltroNome);
                mSqlPadrao.AppendLine("GROUP BY CTRL_Entidades.IdEntidade, NomePrimary, dtAlteracao, DataAcionamento");
                mSqlPadrao.AppendLine(", DataPromessa, DiasReacionamento, NomeAcionador");
                if (!string.IsNullOrEmpty(aFiltroNumDividas))
                    mSqlPadrao.AppendFormat("Having 	{0}\r\n", aFiltroNumDividas);
                mSqlPadrao.AppendLine("ORDER BY dbo.CTRL_Entidades.NomePrimary");
                return mSqlPadrao.ToString();
            }

        }
        private string SqlCartas
        {
            get
            {
                System.Text.StringBuilder mSqlPadrao = new System.Text.StringBuilder();

                mSqlPadrao.AppendLine("SELECT  CTRL_Entidades.IdEntidade, NomePrimary, Logradouro, Complemento, Comentario, Bairro, ");
                mSqlPadrao.AppendLine("Municipio, UF, CEP, dtAlteracao, DataAcionamento AS UltimoAcionamento, DataPromessa, ");
                mSqlPadrao.AppendLine("Descricao AS DescricaoAcionamento, DiasReacionamento, idUsuario, Contrato");
                mSqlPadrao.AppendLine("FROM (SELECT MAX(ID) AS IdAcionamento, IdEntidade");
                mSqlPadrao.AppendLine("		FROM COBR_Acionamentos");
                mSqlPadrao.AppendLine("		GROUP BY idEntidade");
                mSqlPadrao.AppendLine(") LastAC ");
                mSqlPadrao.AppendLine("Join (");
                mSqlPadrao.AppendLine("	select Max(id) IdDivida, IdEntidade, Min(Contrato) Contrato");
                mSqlPadrao.AppendLine("	from Cobr_Divida");
                mSqlPadrao.AppendLine("	where Baixada = 0");
                if (!string.IsNullOrEmpty(aFiltroCarteira) && string.IsNullOrEmpty(aFiltroDevedor))
                    mSqlPadrao.AppendLine($" and XmPathCliente = N'{aFiltroCarteira}'");
                if (!string.IsNullOrEmpty(aFiltroTipoDivida) && string.IsNullOrEmpty(aFiltroDevedor))
                    mSqlPadrao.AppendLine($" and IdTipoDivida = {aFiltroTipoDivida}");
                if (!string.IsNullOrEmpty(aFiltroDevedor))
                    mSqlPadrao.AppendLine($" and IdEntidade = {aFiltroDevedor}");
                mSqlPadrao.AppendLine("	Group by IdEntidade");
                mSqlPadrao.AppendLine("	having 1=1");
                if (!string.IsNullOrEmpty(aFiltroNumDividas) && string.IsNullOrEmpty(aFiltroDevedor))
                    mSqlPadrao.AppendLine($" and {aFiltroNumDividas}"); //COUNT(COBR_Divida.Id) = 3
                mSqlPadrao.AppendLine(") as UltimasDividasComContratos ON UltimasDividasComContratos.IdEntidade = LastAC.idEntidade");
                mSqlPadrao.AppendLine("JOIN COBR_Acionamentos ON LastAC.IdAcionamento = COBR_Acionamentos.Id");
                mSqlPadrao.AppendLine("JOIN COBR_TipoAcionamento ON COBR_Acionamentos.idTipoAcionamento = COBR_TipoAcionamento.Id");
                mSqlPadrao.AppendLine("JOIN CTRL_Entidades ON CTRL_Entidades.IdEntidade = UltimasDividasComContratos.IdEntidade");
                mSqlPadrao.AppendLine("JOIN CTRL_Enderecos on CTRL_Enderecos.idEntidade = CTRL_Entidades.idEntidade");
                mSqlPadrao.AppendLine("ORDER BY NomePrimary;");

                return mSqlPadrao.ToString();

            }
        }
        private NBdbm.tipos.tiposConection TipoConexao
        {
            get
            {
                return this.aParent.LimLogin.TipoConexao;
            }
        }
        protected DataView CriarDataSourceUsuarios()
        {
            return CriarDataSourceUsuarios(false);
        }
        protected DataView CriarDataSourceUsuarios(bool pSomenteAtivos)
        {
            StringBuilder mSB = new StringBuilder();
            mSB.AppendLine("SELECT CTRL_Usuario.*, CTRL_UsuarioConfig.Credencial");
            mSB.AppendLine("FROM CTRL_Usuario, CTRL_UsuarioConfig");
            mSB.AppendLine("WHERE CTRL_Usuario.idUsuario = CTRL_UsuarioConfig.idUsuario");
            mSB.AppendLine("and login <> 'ProSystem_'");
            if (pSomenteAtivos)
            {
                mSB.AppendLine("and CTRL_UsuarioConfig.ativo <> '0'");
                mSB.AppendLine("Order by login");
            }
            this.aSomenteUsuariosAtivos = pSomenteAtivos;
            return NBFuncoes.DataView(mSB.ToString(), this.aParent.Connection);
        }
        private NBdbm.Fachadas.plxCOBR.CadastroAcionamentos CadAcionamentos
        {
            get
            {
                return this.aCadAcionamentos;
            }
        }
        protected void CriarDataSourceTipoDivida()
        {
            string mSQL = "SELECT * FROM COBR_TipoDivida";
            if (this.aDataSourceTipoDivida == null)
                this.aDataSourceTipoDivida = NBFuncoes.DataView(mSQL, this.aParent.Connection);
        }

        protected void CriarDataSourceTipoAcionamento()
        {
            string mSQL = "SELECT Id, Descricao, LTRIM(STR(Id)) + '|' + LTRIM(STR(DiasReacionamento)) + '|' + LTRIM(STR(CredencialExigida)) AS Codigo FROM COBR_TipoAcionamento";
            if (this.aDataSourceTipoAcionamento == null)
                this.aDataSourceTipoAcionamento = NBFuncoes.DataView(mSQL, this.aParent.Connection);
        }

        /// <summary>
        /// Get - Juros Padrão de parcelamento referente a carteira.
        /// </summary>
        public double Juros
        {
            get { return this.aJuros; }
        }

        public string ListaAlertas
        {
            get
            {
                StringBuilder mMensagensAlerta = new StringBuilder("*** Mensagens de alerta agendadas ***\r\n");
                DateTime mDataAlerta = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, 0);

                List<Entidades.entCTRL_Alertas> mListaAlertas = this.aParent.busAlertas.ListaAlertasParaUsuario(aParent.LimLogin.UsuarioID, mDataAlerta);

                if (mListaAlertas != null)
                {
                    string mIdAlertas = "";
                    foreach (Entidades.entCTRL_Alertas mAlerta in mListaAlertas)
                    {
                        mMensagensAlerta.AppendLine("--- //\\ ---");
                        mMensagensAlerta.AppendLine(mAlerta.Mensagem);
                        mMensagensAlerta.AppendLine("--- \\// ---");
                        mIdAlertas += string.Format("{0}|", mAlerta.ID_BD);
                    }
                    mIdAlertas = mIdAlertas.Remove(mIdAlertas.Length - 1);
                    mMensagensAlerta.AppendLine("Confirma a leitura desta mensagem? \r\nCaso não confirme esta mensagem será mostrada a cada 1 minuto.");
                    return string.Format("{0}#{1}", mMensagensAlerta, mIdAlertas);
                }
                else
                    return null;

            }
        }

        public void ConfirmaLeituraAlerta(string pId)
        {
            int mID = Convert.ToInt32(pId);
            aParent.busAlertas.MarcarAlertaComoLido(mID);
        }
        public void AdicionaMaisTempoParaAlerta(string pId)
        {
            int mID = Convert.ToInt32(pId);
            aParent.busAlertas.AumentaTempoAlerta(mID, 1);

        }
        public class Relatorio
        {
            private Classes.LimAcionamentos aParent;
            private string aFiltroIdAcionador = "";
            private string aFiltroTipoAcionamento = "";
            private string aFiltroCarteira = "";
            private string aFiltroPeriodo = "";
            private int aTipoAcionamento = 0;
            private string aCarteira = "Todas";
            private string aAcionador = "Todos";
            private string aPeriodo = "";
            private int aTotalFichasAcionadas = 0;

            public void BuscaTotalFichasAcionadas()
            {
                System.Text.StringBuilder mSB = new System.Text.StringBuilder();
                mSB.Append("SELECT dbo.CTRL_Entidades.NomePrimary ");
                mSB.Append("FROM (SELECT     MAX(ID) AS IdAcionamento, IdEntidade       FROM          dbo.COBR_Acionamentos       ");
                if (this.aFiltroPeriodo != "")
                {
                    mSB.Append("WHERE id > 0 ");
                    mSB.Append(this.aFiltroPeriodo);
                }
                mSB.Append("GROUP BY idEntidade) LastAC ");
                mSB.Append("INNER JOIN dbo.COBR_Acionamentos ON LastAC.IdAcionamento = dbo.COBR_Acionamentos.Id ");
                mSB.Append("INNER JOIN dbo.CTRL_Entidades ");
                mSB.Append("INNER JOIN dbo.COBR_Divida ON dbo.CTRL_Entidades.IdEntidade = dbo.COBR_Divida.IdEntidade ON LastAC.IdEntidade = dbo.CTRL_Entidades.IdEntidade ");
                mSB.Append("INNER JOIN dbo.COBR_TipoAcionamento ON dbo.COBR_Acionamentos.idTipoAcionamento = dbo.COBR_TipoAcionamento.Id ");
                mSB.Append("INNER JOIN dbo.COBR_TipoDivida ON dbo.COBR_Divida.IdTipoDivida = dbo.COBR_TipoDivida.Id ");
                mSB.Append("WHERE CTRL_Entidades.idEntidade > 0 ");
                if (this.aFiltroCarteira != "")
                    mSB.Append(this.aFiltroCarteira);
                if (this.aFiltroTipoAcionamento != "")
                    mSB.Append(this.aFiltroTipoAcionamento);
                if (this.aFiltroIdAcionador != "")
                    mSB.Append(this.aFiltroIdAcionador);
                if (this.aFiltroPeriodo != "")
                    mSB.Append(this.aFiltroPeriodo);
                mSB.Append("GROUP BY dbo.CTRL_Entidades.NomePrimary ");
                mSB.Append("ORDER BY dbo.CTRL_Entidades.NomePrimary");
                System.Data.SqlClient.SqlCommand mCommand = new System.Data.SqlClient.SqlCommand(mSB.ToString(), (System.Data.SqlClient.SqlConnection)this.aParent.aParent.Connection);
                if (mCommand.Connection.State == System.Data.ConnectionState.Closed)
                    mCommand.Connection.Open();
                System.Data.IDataReader mDReader = mCommand.ExecuteReader();
                this.aTotalFichasAcionadas = 0;
                while (mDReader.Read())
                {
                    this.aTotalFichasAcionadas++;
                }
                mDReader.Close();
            }

            public Relatorio(LimAcionamentos pParent)
            {
                this.aParent = pParent;
            }

            public void CriarFiltros(string pCarteira, int pTipoAcionamento, int pIdAcionador, System.DateTime pDataInicial, DateTime pDataFinal)
            {
                //Cria o Filtro para Carteiras
                this.aCarteira = pCarteira;
                if (pCarteira != "Todas")
                    this.aFiltroCarteira = "AND COBR_Divida.XmPathCliente = N'<Entidades><Carteiras><" + pCarteira + ">' ";
                else
                    this.aFiltroCarteira = "";

                //Cria o Filtro para Tipo de Acionamento
                this.aTipoAcionamento = pTipoAcionamento;
                if (pTipoAcionamento > 0)
                    if (pTipoAcionamento == 2)
                        this.aFiltroTipoAcionamento = "AND dbo.COBR_Acionamentos.DataPromessa > GETDATE() ";
                    else
                        this.aFiltroTipoAcionamento = "AND COBR_Acionamentos.idTipoAcionamento = " + pTipoAcionamento.ToString() + " ";
                else
                    this.aFiltroTipoAcionamento = "";

                //Cria o Filtro para ID do Acionador
                if (pIdAcionador > 0)
                    this.aFiltroIdAcionador = "AND COBR_Acionamentos.idUsuario = " + pIdAcionador.ToString() + " ";
                else
                    this.aFiltroIdAcionador = "";

                //Cria o Filtro para o Periodo
                if (pDataInicial.ToString("dd/MM/yy") != "01/01/01" && pDataFinal.ToString("dd/MM/yy") != "01/01/01")
                {
                    this.aFiltroPeriodo = "AND COBR_Acionamentos.DataAcionamento BETWEEN " +
                        "CONVERT(DATETIME, '" + pDataInicial.ToString("yyyy-MM-dd") + " 00:00:00', 102) AND " +
                        "CONVERT(DATETIME, '" + pDataFinal.ToString("yyyy-MM-dd") + " 23:59:59', 102) ";
                    this.aPeriodo = "Período de " + pDataInicial.ToString("dd/MM/yy") + " à " + pDataFinal.ToString("dd/MM/yy");
                }
                else
                {
                    this.aFiltroPeriodo = "";
                    this.aPeriodo = "";
                }

            }

            public System.Data.DataView DataSource
            {
                get
                {
                    System.Text.StringBuilder mSB = new System.Text.StringBuilder();
                    mSB.Append("SELECT dbo.CTRL_Entidades.IdEntidade, dbo.CTRL_Entidades.NomePrimary, dbo.COBR_TipoDivida.Descricao AS TipoDivida, ");
                    mSB.Append("dbo.COBR_TipoAcionamento.Descricao AS TipoAcionamento, dbo.COBR_Acionamentos.DataPromessa ");
                    mSB.Append("FROM (SELECT     MAX(ID) AS IdAcionamento, IdEntidade ");
                    mSB.Append("      FROM          dbo.COBR_Acionamentos ");
                    if (this.aFiltroPeriodo != "")
                    {
                        mSB.Append("WHERE id > 0 ");
                        mSB.Append(this.aFiltroPeriodo);
                    }
                    mSB.AppendLine("      GROUP BY idEntidade) LastAC ");
                    mSB.AppendLine("JOIN dbo.COBR_Acionamentos ON LastAC.IdAcionamento = dbo.COBR_Acionamentos.Id ");
                    mSB.AppendLine("JOIN CTRL_Usuario usu on usu.idUsuario = COBR_Acionamentos.idUsuario ");
                    mSB.AppendLine("JOIN CTRL_Link_EntidadeEntidade linkEE on linkEE.idEntidadeLink = COBR_Acionamentos.idEntidade and linkEE.idEntidadeBase = usu.idEntidade ");
                    mSB.AppendLine("JOIN dbo.CTRL_Entidades ON LastAC.IdEntidade = dbo.CTRL_Entidades.IdEntidade");
                    mSB.AppendLine("JOIN dbo.COBR_Divida ON dbo.CTRL_Entidades.IdEntidade = dbo.COBR_Divida.IdEntidade");
                    mSB.AppendLine("JOIN dbo.COBR_TipoAcionamento ON dbo.COBR_Acionamentos.idTipoAcionamento = dbo.COBR_TipoAcionamento.Id ");
                    mSB.AppendLine("JOIN dbo.COBR_TipoDivida ON dbo.COBR_Divida.IdTipoDivida = dbo.COBR_TipoDivida.Id ");
                    mSB.AppendLine("WHERE CTRL_Entidades.idEntidade > 0 ");
                    if (this.aFiltroCarteira != "")
                        mSB.Append(this.aFiltroCarteira);
                    if (this.aFiltroTipoAcionamento != "")
                        mSB.Append(this.aFiltroTipoAcionamento);
                    if (this.aFiltroIdAcionador != "")
                        mSB.Append(this.aFiltroIdAcionador);
                    if (this.aFiltroPeriodo != "")
                        mSB.Append(this.aFiltroPeriodo);
                    mSB.Append("GROUP BY dbo.CTRL_Entidades.IdEntidade, dbo.CTRL_Entidades.NomePrimary, dbo.COBR_Acionamentos.DataPromessa, dbo.COBR_TipoDivida.Descricao, ");
                    mSB.Append("dbo.COBR_TipoAcionamento.Descricao ");
                    mSB.Append("ORDER BY dbo.CTRL_Entidades.NomePrimary");

                    return NBFuncoes.DataView(mSB.ToString(), this.aParent.aParent.Connection);

                }
            }

            public System.Data.DataView DataSourceUsuarios(bool pSomenteAtivos)
            {
                if (this.aParent.aDataSourceUsuarios == null || !this.aParent.aSomenteUsuariosAtivos)
                    this.aParent.aDataSourceUsuarios = this.aParent.CriarDataSourceUsuarios(pSomenteAtivos);
                return this.aParent.aDataSourceUsuarios;
            }

            public int TipoAcionamento
            {
                get
                {
                    return this.aTipoAcionamento;
                }
            }
            public string Carteira
            {
                get
                {
                    return this.aCarteira;
                }
            }
            public string Acionador
            {
                get
                {
                    return this.aAcionador;
                }
                set
                {
                    this.aAcionador = value;
                }
            }
            public string Periodo
            {
                get
                {
                    return this.aPeriodo;
                }
            }
            public int TotalFichasAcionadas
            {
                get
                {
                    return this.aTotalFichasAcionadas;
                }
            }
        }

        private List<DtoCartaLinha> ConteudoCarta1
        {
            get
            {
                var mConteudo = new List<DtoCartaLinha>
                {
                    new DtoCartaLinha
                    {
                        Texto ="Consta em nossos registros dívida de sua responsabilidade, VENCIDA junto INSTITUIÇÃO DE LAGES.\n"
                    },
                    new DtoCartaLinha
                    {
                        Texto = new StringBuilder("Solicitamos o comparecimento de Vossa Senhoria em nosso escritório de endereço acima ")
                            .Append("especificado, no prazo de 05(cinco) dias, para tratar do assunto, a fim de evitar maiores problemas.\n").ToString()
                    },
                    new DtoCartaLinha
                    {
                        Texto = "Dentro do prazo supra, além de evitar despesas maiores, podemos estudar as condições da para sua liquidação PARCELADAMENTE.\n"
                    },
                    new DtoCartaLinha
                    {
                        Texto = new StringBuilder("Caso Vossa Senhoria tenha quitado seu compromisso, aceite nossas desculpas ")
                        .Append("e nos informe através de nossos escritórios, ou pelo fone (49) 3251-6000, ")
                        .AppendLine("para que possamos atualizar nossos registros.").ToString()
                    },
                    new DtoCartaLinha {Texto = "\n\n\n"}
                };


                return mConteudo;
            }
        }

        private List<DtoCartaLinha> ConteudoCarta2
        {
            get
            {
                var mConteudo = new List<DtoCartaLinha>
                {
                    new DtoCartaLinha {Texto = new StringBuilder("Pela presente, rogo a Vossa Senhoria mais uma vez a ")
                    .Append("comparecer no prazo de 48 horas (após o recebimento desta notificação), a nosso ")
                    .Append("escritório profissional, a fim que se examine a possibilidade de suspensão da Ação ")
                    .Append("de Execução e / ou cobrança de título Executivo Extrajudicial, ou para que se ")
                    .Append("encontre a solução para o pagamento do débito.\n").ToString()},
                    new DtoCartaLinha {Texto = "\n"},
                    new DtoCartaLinha {Texto = new StringBuilder("Sendo assim, no caso de não comparecimento no prazo supra ")
                    .Append("estipulado, ingressaremos em juízo no foro local com o procedimento judicial que o ")
                    .Append("caso requer, imediatamente.\n").ToString()},
                    new DtoCartaLinha {Texto = "\n"},
                    new DtoCartaLinha {Texto = new StringBuilder("O atendimento a esta notificação extrajudicial no prazo ")
                    .Append("estipulado, poderão sustar as medidas, bem como evitar maiores aborrecimentos e ")
                    .Append("despesas, uma vez que visamos e objetivamos, contudo, a busca e a solução do ")
                    .Append("débito, primeiramente de forma amistosa, além de servir à presente como medida ")
                    .Append("precausiosa e de inteiro respeito por Vossa Senhoria. Aguardamos o seu comparecimento.\n").ToString()},
                    new DtoCartaLinha {Texto = "\n\n"},
                    new DtoCartaLinha {Texto = "PROCESSO: 370.[Protocolo]\n", TextFormat = TextFormat.Bold},
                    new DtoCartaLinha {Texto = "\n\n"}
                };



                return mConteudo;
            }
        }

        private List<DtoCartaLinha> ConteudoCarta3
        {
            get
            {
                var mConteudo = new List<DtoCartaLinha>
                {
                    new DtoCartaLinha {Texto = "NOTIFICAÇÃO EXTRAJUDICIAL\n", TextFormat = TextFormat.Underline, FirstLineIndent = Unit.FromCentimeter(0), ParagraphAlignment = ParagraphAlignment.Center },
                    new DtoCartaLinha {Texto = "\n\n"},
                    new DtoCartaLinha {Texto = new StringBuilder("Serve a presente para NOTIFICÁ-LO de que o título vencido, ")
                    .Append("não apresenta QUITAÇÃO por parte de V. As. até a presente data.\n").ToString()},
                    new DtoCartaLinha {Texto = "\n"},
                    new DtoCartaLinha {Texto = new StringBuilder("Assim, concedemos o prazo de 48 horas (após o recebimento desta notificação), ")
                    .Append("para que efetue o pagamento integral do título, sob pena, de não o fazendo, ser protestado o título, ")
                    .Append("cadastrado V.Sa.Junto ao SPC, SERASA, bem como, AÇÃO JUDICIAL cabível ao caso.\n").ToString()},
                    new DtoCartaLinha {Texto = "\n"},
                    new DtoCartaLinha {Texto = new StringBuilder("Lembramos, que o valor deverá, na data de sua liquidação, ")
                    .Append("ser atualizado conforme condições e cláusulas previstas.\n").ToString()},
                    new DtoCartaLinha {Texto = "\n"},
                    new DtoCartaLinha {Texto = new StringBuilder("O atendimento a esta notificação extrajudicial no prazo estipulado, ")
                    .Append("poderão sustar as medidas, bem como evitar maiores aborrecimentos e despesas, uma vez que visamos e objetivamos, ")
                    .Append("contudo, a busca e a solução do débito, primeiramente de forma amistosa, além de servir à presente como medida precausiosa e de inteiro respeito por Vossa Senhoria.\n").ToString()},
                    new DtoCartaLinha {Texto = "\n"},
                    new DtoCartaLinha {Texto = "Aguardamos o seu comparecimento.\n"},
                    new DtoCartaLinha {Texto = "\n\n"},
                    new DtoCartaLinha {Texto = "CONTRATO: [Protocolo]\n", TextFormat = TextFormat.Bold, FirstLineIndent = Unit.FromCentimeter(0)}
                };



                return mConteudo;
            }
        }

        public bool HabilitarSalvar
        {
            get
            {
                if (aParent.LimLogin.Credencial != TipoCredencial.Acionador)
                    return true;
                return (AcionadorDaFicha != null && AcionadorDaFicha.IdEntidade.Equals(aParent.LimLogin.IdEntidade));
            }
        }

        #region IDisposable Members
        public virtual void Dispose()
        {
            this.aParent = null;

            if (this.aCadAcionamentos != null)
            {
                this.aCadAcionamentos.Dispose();
                this.aCadAcionamentos = null;
            }

            if (this.aDataSourceTipoAcionamento != null)
            {
                this.aDataSourceTipoAcionamento.Dispose();
                this.aDataSourceTipoAcionamento = null;
            }

            if (this.aDataSourceTipoDivida != null)
            {
                this.aDataSourceTipoDivida.Dispose();
                this.aDataSourceTipoDivida = null;
            }

            if (this.aDataSourceUsuarios != null)
            {
                this.aDataSourceUsuarios.Dispose();
                this.aDataSourceUsuarios = null;
            }
        }

        #endregion
    }
}
