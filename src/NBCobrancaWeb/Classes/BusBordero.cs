using System;
using System.Collections;
using System.Data;
using System.Text;
using NBCobranca.Tipos;
using NBdbm;

namespace NBCobranca.Classes
{
    /// <summary>
    /// Regras de Negócio para a Geração de Borderos
    /// </summary>
    public class BusBordero : IDisposable
    {
        private Sistema _sistema;

        public BusBordero(Sistema pSistema)
        {
            _sistema = pSistema;
        }

        public string FiltroCarteira { get; set; } = "";

        public string FiltroBordero { get; set; } = "";

        public DateTime? FiltroDataInicial { get; set; } = null;

        public DateTime? FiltroDataFinal { get; set; } = null;

        public DtoRelBordero.DtoRegistroResumoRelBordero ItemResumo { get; private set; }

        public DtoRelBordero RelatorioBordero { get; private set; }

        public bool ImpressaoOK { get; set; }

        public DataView DataSource
        {
            get
            {
                StringBuilder mSB = new StringBuilder();
                DataView mDV;
                mSB.Append(SQL_Padrao);
                mSB.Append(Where);
                if (FiltroCarteira == "Todas")
                    mSB.Append(" ORDER BY XmPathCliente, NomePrimary, NumDoc ");
                else
                    mSB.Append(" ORDER BY NomePrimary, NumDoc ");
                mDV = NBFuncoes.DataView(mSB.ToString(), _sistema.Connection);

                foreach (DataRow mDR in mDV.Table.Rows)
                {
                    ValorBaixaParcial(mDR);
                }
                return mDV;
            }
        }

        public void GerarRelatorio()
        {
            var sql = new StringBuilder();
            sql.Append(SQL_Padrao);
            sql.Append(Where);
            sql.Append(FiltroCarteira == "Todas"
                ? " ORDER BY XmPathCliente, NomePrimary, NumDoc "
                : " ORDER BY NomePrimary, NumDoc ");
            var dv = NBFuncoes.DataView(sql.ToString(), _sistema.Connection);
            RelatorioBordero = new DtoRelBordero
            {
                Carteira = FiltroCarteira,
                DataInicial = FiltroDataInicial,
                DataFinal = FiltroDataFinal,
                NumeroDoBordero = string.IsNullOrEmpty(FiltroBordero) ? 0 : Convert.ToInt32(FiltroBordero)
            };

            foreach (DataRow dr in dv.Table.Rows)
            {
                RelatorioBordero.AddRegistro(new DtoRelBordero.DtoRegistroRelBordero
                {
                    Carteira = dr["NomeCarteira"].ToString(),
                    CodigoDevedor = Convert.ToInt32(dr["IdEntidade"]),
                    Contrato = dr["Contrato"].ToString(),
                    DataBaixa = Convert.ToDateTime(dr["DataBaixa"]),
                    DataVencimento = Convert.ToDateTime(dr["DataVencimento"]),
                    NomeDevedor = dr["NomePrimary"].ToString(),
                    NumDoc = dr["NumDoc"].ToString(),
                    TipoDivida = dr["TipoDivida"].ToString(),
                    ValorNominal = Convert.ToDouble(dr["ValorNominal"]),
                    BaixaParcial = Convert.ToBoolean(dr["BaixaParcial"]),
                    ValorRecebido = ValorBaixaParcial(dr)
                });
            }
        }

        public string GetNomeCarteira(string pXmPathCliente)
        {
            string mNomeCarteira = pXmPathCliente.Replace("<Entidades><Carteiras><", "");
            mNomeCarteira = mNomeCarteira.Replace(">", "");
            return mNomeCarteira.ToUpper();
        }

        public double ValorBaixaParcial(DataRow pDR)
        {
            if (!Convert.ToBoolean(pDR["BaixaParcial"])) return Convert.ToDouble(pDR["ValorNominal"]);

            var mSb = new StringBuilder();
            mSb.AppendLine("select Sum(ValorBaixa) TotalBaixa");
            mSb.AppendLine("from COBR_Baixas");
            mSb.AppendFormat("where idDivida = {0}\r\n", pDR["idDivida"]);

            if (FiltroDataInicial != null && FiltroDataFinal != null)
                mSb.AppendFormat("and DataBaixa between {0} and {1}\r\n", NBFuncoes.FormatCampoToSQL(FiltroDataInicial), NBFuncoes.FormatCampoToSQL(FiltroDataFinal));

            if (FiltroBordero != "")
                mSb.AppendFormat("and NumBordero = {0}", FiltroBordero);

            var command = _sistema.Connection.CreateCommand();
            if (_sistema.Connection.State == ConnectionState.Closed)
                _sistema.Connection.Open();
            command.CommandText = mSb.ToString();
            return Convert.ToDouble(command.ExecuteScalar());
        }

        public void NovoItemResumo()
        {
            ItemResumo = new DtoRelBordero.DtoRegistroResumoRelBordero();
        }

        public void NovoResumo()
        {
            if (RelatorioBordero == null)
                throw new COBR_Exception("Primeiro faça a pesquisa dos dados do relatório antes de criar um resumo sobre o mesmo", "BusBordero.NovoResumo");

            RelatorioBordero.ResumoRelBorderos.Clear();
            var resumo = new DtoRelBordero.DtoRegistroResumoRelBordero
            {
                NumeroLinha = 1,
                Descricao = "TOTAL CAPITAL RECEBIDO R$",
                Valor = RelatorioBordero.TotalValorRecebido
            };

            RelatorioBordero.ResumoRelBorderos.Add(resumo.NumeroLinha, resumo);
        }

        public void SalvaItemResumo()
        {
            try
            {
                RelatorioBordero.ResumoRelBorderos.Add(ItemResumo.NumeroLinha, ItemResumo);
            }
            catch
            {
                throw new COBR_Exception("Item de Resumo Já Existente na Coleção, Para Incluir um Item com esse mesmo número de Linha, Primeiro Apague o Existente", "BusBordero");
            }
        }

        public void CarregaItemResumo(int pKey)
        {
            ItemResumo = RelatorioBordero.ResumoRelBorderos[pKey];
            RelatorioBordero.ResumoRelBorderos.Remove(pKey);
        }

        public void RemoveItemResumo(int pKey)
        {
            RelatorioBordero.ResumoRelBorderos.Remove(pKey);
        }

        public IEnumerable Resumo => RelatorioBordero.ResumoRelBorderos.Values;

        private string SQL_Padrao
        {
            get
            {
                var mSb = new StringBuilder();

                mSb.AppendLine("SELECT COBR_Divida.Id as IdDivida, NomePrimary, ");
                mSb.AppendLine("COBR_TipoDivida.Descricao as TipoDivida, Contrato, NumDoc, ");
                mSb.AppendLine("DataVencimento, DataBaixa, ValorNominal, Baixada, BaixaParcial, XmPathCliente, BorderoBaixa, CTRL_Entidades.IdEntidade, CTRL_Nos.Nome NomeCarteira");
                mSb.AppendLine("FROM COBR_Divida");
                mSb.AppendLine("join CTRL_Entidades on  CTRL_Entidades.IdEntidade = COBR_Divida.IdEntidade");
                mSb.AppendLine("join COBR_TipoDivida on COBR_TipoDivida.Id = COBR_Divida.IdTipoDivida ");
                mSb.AppendLine("join CTRL_Nos on XmPath = XmPathCliente");
                mSb.AppendLine("WHERE 1=1");

                return mSb.ToString();
            }
        }

        private string Where
        {
            get
            {
                var mSb = new StringBuilder();
                mSb.AppendLine("and (Baixada = 1 OR BaixaParcial = 1)");
                if (FiltroCarteira != "Todas")
                    mSb.AppendFormat("and XmPathCliente = '<Entidades><Carteiras><{0}>'\r\n", FiltroCarteira);

                if (FiltroDataInicial != null && FiltroDataFinal != null)
                    mSb.AppendFormat("and DataBaixa between {0} and {1}\r\n", NBFuncoes.FormatCampoToSQL(FiltroDataInicial), NBFuncoes.FormatCampoToSQL(FiltroDataFinal));

                if (FiltroBordero != "")
                    mSb.AppendFormat("and BorderoBaixa = {0}", FiltroBordero);

                return mSb.ToString();

            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            _sistema = null;
            ItemResumo = null;
        }

        #endregion
    }
}
