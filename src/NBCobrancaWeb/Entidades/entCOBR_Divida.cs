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

namespace NBCobranca.Entidades
{
    public class entCOBR_Divida : entBase
    {
        int _Id;
        int _IdEntidade;
        int _IdTipoDivida;
        string _Contrato;
        int _NumDoc;
        DateTime? _DataVencimento;
        double _ValorNominal;
        double _ValorNominalBaixado;
        DateTime? _DataBaixa;
        int? _BorderoBaixa;
        int _NumRecibo;
        int _IdCobrador;
        double _PerCobrador;
        int _IdUsuarioBaixa;
        bool _BaixaNoCliente;
        bool _Baixada;
        bool _BaixaParcial;
        string _XmPathCliente;
        double _ValorCorrigido;
        string _TipoDivida;

        public entCOBR_Divida()
            : base()
        {
            this._NomeCamposChave.Add("Id");
            this._NomeCamposTabela.AddRange(new string[] 
                {"Id"
                ,"IdEntidade"
                ,"IdTipoDivida"
                ,"Contrato"
                ,"NumDoc"
                ,"DataVencimento"
                ,"ValorNominal"
                ,"DataBaixa"
                ,"BorderoBaixa"
                ,"NumRecibo"
                ,"IdCobrador"
                ,"PerCobrador"
                ,"IdUsuarioBaixa"
                ,"BaixaNoCliente"
                ,"Baixada"
                ,"BaixaParcial"
                ,"XmPathCliente"});
            this.Clear();
        }

        #region === Propriedades dos campos da Tabela ===

        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public int IdEntidade
        {
            get { return _IdEntidade; }
            set { this.ValidaAlteracao<int>(ref _IdEntidade, value, "IdEntidade"); }
        }

        public int IdTipoDivida
        {
            get { return _IdTipoDivida; }
            set { this.ValidaAlteracao<int>(ref _IdTipoDivida, value, "IdTipoDivida"); }
        }

        public string Contrato
        {
            get { return _Contrato; }
            set { this.ValidaAlteracao<string>(ref _Contrato, value, "Contrato"); }
        }

        public int NumDoc
        {
            get { return _NumDoc; }
            set { this.ValidaAlteracao<int>(ref _NumDoc, value, "NumDoc"); }
        }

        public DateTime? DataVencimento
        {
            get { return _DataVencimento; }
            set { this.ValidaAlteracao<DateTime?>(ref _DataVencimento, value, "DataVencimento"); }
        }

        public double ValorNominal
        {
            get { return _ValorNominal; }
            set { this.ValidaAlteracao<double>(ref _ValorNominal, value, "ValorNominal"); }
        }

        public DateTime? DataBaixa
        {
            get { return _DataBaixa; }
            set { this.ValidaAlteracao<DateTime?>(ref _DataBaixa, value, "DataBaixa"); }
        }

        public int? BorderoBaixa
        {
            get { return _BorderoBaixa; }
            set { this.ValidaAlteracao<int?>(ref _BorderoBaixa, value, "BorderoBaixa"); }
        }

        public int NumRecibo
        {
            get { return _NumRecibo; }
            set { this.ValidaAlteracao<int>(ref _NumRecibo, value, "NumRecibo"); }
        }

        public int IdCobrador
        {
            get { return _IdCobrador; }
            set { this.ValidaAlteracao<int>(ref _IdCobrador, value, "IdCobrador"); }
        }

        public double PerCobrador
        {
            get { return _PerCobrador; }
            set { this.ValidaAlteracao<double>(ref _PerCobrador, value, "PerCobrador"); }
        }

        public int IdUsuarioBaixa
        {
            get { return _IdUsuarioBaixa; }
            set { this.ValidaAlteracao<int>(ref _IdUsuarioBaixa, value, "IdUsuarioBaixa"); }
        }

        public bool BaixaNoCliente
        {
            get { return _BaixaNoCliente; }
            set { this.ValidaAlteracao<bool>(ref _BaixaNoCliente, value, "BaixaNoCliente"); }
        }

        public bool Baixada
        {
            get { return _Baixada; }
            set { this.ValidaAlteracao<bool>(ref _Baixada, value, "Baixada"); }
        }

        public bool BaixaParcial
        {
            get { return _BaixaParcial; }
            set { this.ValidaAlteracao<bool>(ref _BaixaParcial, value, "BaixaParcial"); }
        }

        public string XmPathCliente
        {
            get { return _XmPathCliente; }
            set { this.ValidaAlteracao<string>(ref _XmPathCliente, value, "XmPathCliente"); }
        }

        #endregion

        public override int ID_BD
        {
            get
            {
                return this._Id;
            }
            set
            {
                this._Id = value;
            }
        }

        public override string Key
        {
            get
            {
                return string.Format("{0}_{1}_{2}_{3}_{4}", _IdTipoDivida, _Contrato.Trim(),
              _NumDoc, _DataVencimento.ToString(), _ValorNominal);
            }
        }

        public double ValorNominalBaixado
        {
            get { return _ValorNominalBaixado; }
            set { _ValorNominalBaixado = value; }
        }

        public double ValorNominalReal
        {
            get
            {
                return this._ValorNominal - _ValorNominalBaixado;
            }
        }

        /// <summary>
        /// É o resultado do valor nominal acrecido dos juros da carteira conforme a data de vencimento.
        /// <para>
        /// obs.: este valor não esta registrado no banco, portanto o mesmo tem que ser calculado pela regra 
        /// de negócio BusDividas.
        /// </para>
        /// </summary>
        public double ValorCorrigido
        {
            get { return this._ValorCorrigido; }
            set { this._ValorCorrigido = value; }
        }

        /// <summary>
        /// Representa a descriçao do tipo da dívida, o mesmo tem que ser atribuido manualmente.
        /// </summary>
        public string TipoDivida
        {
            get { return this._TipoDivida; }
            set { this._TipoDivida = value; }
        }

        /// <summary>
        /// get - Nome da carteira retirado do XmPathCliente
        /// </summary>
        public string Carteira
        {
            get
            {
                return _XmPathCliente.Remove(0, 23).Replace(">", "");
            }
        }

        public override void Preencher(DataRow pDataRowDivida)
        {
            _Id = Convert.ToInt32(pDataRowDivida["Id"]);
            _IdEntidade = Convert.ToInt32(pDataRowDivida["IdEntidade"]);
            _IdTipoDivida = Convert.ToInt32(pDataRowDivida["IdTipoDivida"]);
            _Contrato = pDataRowDivida["Contrato"].ToString();
            _NumDoc = Convert.ToInt32(pDataRowDivida["NumDoc"]);
            _DataVencimento = Convert.ToDateTime(pDataRowDivida["DataVencimento"]);
            _ValorNominal = Convert.ToDouble(pDataRowDivida["ValorNominal"]);
            if (!(pDataRowDivida["DataBaixa"] is DBNull))
                _DataBaixa = Convert.ToDateTime(pDataRowDivida["DataBaixa"]);
            if (!(pDataRowDivida["BorderoBaixa"] is DBNull))
                _BorderoBaixa = Convert.ToInt32(pDataRowDivida["BorderoBaixa"]);
            if (!(pDataRowDivida["NumRecibo"] is DBNull))
                _NumRecibo = Convert.ToInt32(pDataRowDivida["NumRecibo"]);
            if (!(pDataRowDivida["IdCobrador"] is DBNull))
                _IdCobrador = Convert.ToInt32(pDataRowDivida["IdCobrador"]);
            if (!(pDataRowDivida["PerCobrador"] is DBNull))
                _PerCobrador = Convert.ToDouble(pDataRowDivida["PerCobrador"]);
            if (!(pDataRowDivida["IdUsuarioBaixa"] is DBNull))
                _IdUsuarioBaixa = Convert.ToInt32(pDataRowDivida["IdUsuarioBaixa"]);
            if (!(pDataRowDivida["BaixaNoCliente"] is DBNull))
                _BaixaNoCliente = Convert.ToBoolean(pDataRowDivida["BaixaNoCliente"]);
            if (!(pDataRowDivida["Baixada"] is DBNull))
                _Baixada = Convert.ToBoolean(pDataRowDivida["Baixada"]);
            if (!(pDataRowDivida["BaixaParcial"] is DBNull))
                _BaixaParcial = Convert.ToBoolean(pDataRowDivida["BaixaParcial"]);
            _XmPathCliente = pDataRowDivida["XmPathCliente"].ToString();

        }

        public override void Preencher(IDataReader pDrCtrlEntidade)
        {
            throw new NotImplementedException();
        }

        public string SqlUpdate
        {
            get
            {
                StringBuilder mSQL = new StringBuilder();

                mSQL.AppendLine("UPDATE COBR_Divida");
                mSQL.AppendFormat("   SET [IdEntidade] = {0}\r\n", _IdEntidade);
                mSQL.AppendFormat("      ,[IdTipoDivida] = {0}\r\n", _IdTipoDivida);
                mSQL.AppendFormat("      ,[Contrato] = '{0}'\r\n", _Contrato);
                mSQL.AppendFormat("      ,[NumDoc] = {0}\r\n", _NumDoc);
                mSQL.AppendFormat("      ,[DataVencimento] = convert(Date,'{0}',102)\r\n", _DataVencimento.Value.ToString("yyyy/MM/dd"));
                mSQL.AppendFormat("      ,[ValorNominal] = {0}\r\n", _ValorNominal.ToString("0.00").Replace(",", "."));
                mSQL.AppendFormat("      ,[DataBaixa] = convert(Date,'{0}',102)\r\n", _DataBaixa.Value.ToString("yyyy/MM/dd"));
                mSQL.AppendFormat("      ,[BorderoBaixa] = {0}\r\n", _BorderoBaixa);
                mSQL.AppendFormat("      ,[NumRecibo] = {0}\r\n", _NumRecibo);
                mSQL.AppendFormat("      ,[IdCobrador] = {0}\r\n", _IdCobrador);
                mSQL.AppendFormat("      ,[PerCobrador] = {0}\r\n", _PerCobrador.ToString("0.00").Replace(",", "."));
                mSQL.AppendFormat("      ,[IdUsuarioBaixa] = {0}\r\n", _IdUsuarioBaixa);
                mSQL.AppendFormat("      ,[BaixaNoCliente] = {0}\r\n", Convert.ToInt32(_BaixaNoCliente));
                mSQL.AppendFormat("      ,[Baixada] = {0}\r\n", Convert.ToInt32(_Baixada));
                mSQL.AppendFormat("      ,[BaixaParcial] = {0}\r\n", Convert.ToInt32(_BaixaParcial));
                mSQL.AppendFormat("      ,[XmPathCliente] = '{0}'\r\n", _XmPathCliente);
                mSQL.AppendFormat(" WHERE id = {0}\r\n", _Id);
                return mSQL.ToString();

            }
        }

        public override void Clear()
        {
            _Id = 0;
            _IdEntidade = 0;
            _IdTipoDivida = 0;
            _Contrato = "";
            _NumDoc = 0;
            _DataVencimento = null;
            _ValorNominal = 0;
            _ValorNominalBaixado = 0;
            _DataBaixa = null;
            _BorderoBaixa = 0;
            _NumRecibo = 0;
            _IdCobrador = 0;
            _PerCobrador = 0;
            _IdUsuarioBaixa = 0;
            _BaixaNoCliente = false;
            _Baixada = false;
            _BaixaParcial = false;
            _XmPathCliente = "";

        }

        public string ImageStatus
        {
            get
            {
                string mTagImage = "<img src='../imagens/{0}' class='webuiPopover' data-content='{1}' data-delay-show='0' data-delay-hide='1000' data-title='Resumo da Baixa' data-placement='left' border='0'>";
                string mToolTipBaixaParcial = "<p>Divida Parcialmente Baixada {0}</p>O Valor Nominal Total é de {1}<br>A Ultima Baixa Parcial foi em: {2}";
                string mToolTipBaixada = "<p>Divida Baixada {0}</p>Em {1}";
                string mOrigemBaixa = "na Cobradora";
                string mGif = "baixa_cob.gif";
                if (this.BaixaNoCliente)
                {
                    mOrigemBaixa = "no Cliente";
                    mGif = "baixa_cli.gif";
                }

                if (this.Baixada)
                {
                    mToolTipBaixada = string.Format(mToolTipBaixada, mOrigemBaixa, this.DataBaixa == null ? "" : this.DataBaixa.Value.ToString("dd/MM/yyyy"));
                    mTagImage = string.Format(mTagImage, mGif, mToolTipBaixada);
                }
                else
                {
                    if (this.BaixaParcial)
                    {
                        mGif = "baixa_par.gif";
                        mToolTipBaixaParcial = string.Format(mToolTipBaixaParcial, mOrigemBaixa, this.ValorNominal.ToString("C2"), this.DataBaixa.Value.ToString("dd/MM/yyyy"));
                        mTagImage = string.Format(mTagImage, mGif, mToolTipBaixaParcial);
                    }
                    else
                        mTagImage = string.Format(mTagImage, "Baixa_Nao.Gif", "Divida não Baixada");

                }
                return mTagImage;

            }
        }

    }
}
