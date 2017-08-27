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
    public class entCOBR_Baixas
    {
        int _Id;
        int _IdEntidade;
        int? _IdDivida;
        bool? _BaixadoCliente;
        int? _NumBordero;
        DateTime _DataBaixa;
        double _ValorNominal;
        double _ValorBaixa;
        double _ValorRecebido;

        public entCOBR_Baixas()
        {
            this.Clear();
        }

        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public int IdEntidade
        {
            get { return _IdEntidade; }
            set { _IdEntidade = value; }
        }

        public int? IdDivida
        {
            get { return _IdDivida; }
            set { _IdDivida = value; }
        }

        public bool? BaixadoCliente
        {
            get { return _BaixadoCliente; }
            set { _BaixadoCliente = value; }
        }

        public int? NumBordero
        {
            get { return _NumBordero; }
            set { _NumBordero = value; }
        }

        public DateTime DataBaixa
        {
            get { return _DataBaixa; }
            set { _DataBaixa = value; }
        }

        public double ValorNominal
        {
            get { return _ValorNominal; }
            set { _ValorNominal = value; }
        }

        public double ValorBaixa
        {
            get { return _ValorBaixa; }
            set { _ValorBaixa = value; }
        }

        public double ValorRecebido
        {
            get { return _ValorRecebido; }
            set { _ValorRecebido = value; }
        }

        public void Clear()
        {
            _Id = 0;
            _IdEntidade = 0;
            _IdDivida = 0;
            _BaixadoCliente = false;
            _NumBordero = 0;
            _DataBaixa = DateTime.Now;
            _ValorNominal = 0;
            _ValorBaixa = 0;
            _ValorRecebido = 0;
        }

        public string SqlInsert
        {
            get
            {
                StringBuilder mSQL = new StringBuilder();
                mSQL.AppendLine("INSERT INTO COBR_Baixas");
                mSQL.AppendLine("           ([IdEntidade]");
                mSQL.AppendLine("           ,[IdDivida]");
                mSQL.AppendLine("           ,[BaixadoCliente]");
                mSQL.AppendLine("           ,[NumBordero]");
                mSQL.AppendLine("           ,[DataBaixa]");
                mSQL.AppendLine("           ,[ValorNominal]");
                mSQL.AppendLine("           ,[ValorBaixa]");
                mSQL.AppendLine("           ,[ValorRecebido])");
                mSQL.AppendLine("     VALUES");
                mSQL.AppendFormat("           ({0}\r\n", _IdEntidade);
                mSQL.AppendFormat("           ,{0}\r\n", _IdDivida);
                mSQL.AppendFormat("           ,{0}\r\n", Convert.ToInt32(_BaixadoCliente.Value));
                mSQL.AppendFormat("           ,{0}\r\n", _NumBordero);
                mSQL.AppendFormat("           ,convert(Date,'{0}',102)\r\n", _DataBaixa.ToString("yyyy/MM/dd"));
                mSQL.AppendFormat("           ,{0}\r\n", _ValorNominal.ToString("0.00").Replace(",","."));
                mSQL.AppendFormat("           ,{0}\r\n", _ValorBaixa.ToString("0.00").Replace(",", "."));
                mSQL.AppendFormat("           ,{0})", _ValorRecebido.ToString("0.00").Replace(",", "."));
                return mSQL.ToString();
            }
        }

    }
}
