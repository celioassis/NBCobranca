using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace NBCobranca.Entidades
{
    public class entCTRL_Alertas : entBase
    {
        int _ID;
        DateTime _DataHora;
        String _Mensagem;
        int _ID_UsuarioCriador;
        int _ID_UsuarioMensagem;
        DateTime _Criacao;
        bool _Lido;

        public entCTRL_Alertas()
            : base()
        {
            _NomeCamposChave.Add("ID");
            _NomeCamposTabela.AddRange(new string[]
            {"ID",
             "DataHora",
             "Mensagem",
             "ID_UsuarioCriador",
             "ID_UsuarioMensagem",
             "Criacao",
             "Lido"});

            this.Clear();
        }

        #region === Campos das Tabelas ===
        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        public DateTime DataHora
        {
            get { return _DataHora; }
            set { this.ValidaAlteracao<DateTime>(ref _DataHora, value, "DataHora"); }
        }
        public String Mensagem
        {
            get { return _Mensagem; }
            set { this.ValidaAlteracao<string>(ref _Mensagem, value, "Mensagem"); }
        }
        public int ID_UsuarioCriador
        {
            get { return _ID_UsuarioCriador; }
            set { this.ValidaAlteracao<int>(ref _ID_UsuarioCriador, value, "ID_UsuarioCriador"); }
        }
        public int ID_UsuarioMensagem
        {
            get { return _ID_UsuarioMensagem; }
            set { this.ValidaAlteracao<int>(ref _ID_UsuarioMensagem, value, "ID_UsuarioMensagem"); }
        }
        public DateTime Criacao
        {
            get { return _Criacao; }
            set { this.ValidaAlteracao<DateTime>(ref _Criacao, value, "Criacao"); }
        }
        public bool Lido
        {
            get { return _Lido; }
            set { this.ValidaAlteracao<bool>(ref _Lido, value, "Lido"); }
        }
        #endregion

        public override void Preencher(DataRow pDrCtrlEntidade)
        {
            _ID = Convert.ToInt32(pDrCtrlEntidade["ID"]);
            _DataHora = Convert.ToDateTime(pDrCtrlEntidade["DataHora"]);
            _Mensagem = pDrCtrlEntidade["Mensagem"].ToString();
            _ID_UsuarioCriador = Convert.ToInt32(pDrCtrlEntidade["ID_UsuarioCriador"]);
            _ID_UsuarioMensagem = Convert.ToInt32(pDrCtrlEntidade["ID_UsuarioMensagem"]);
            _Criacao = Convert.ToDateTime(pDrCtrlEntidade["Criacao"]);
            _Lido = Convert.ToBoolean(pDrCtrlEntidade["Lido"]);

        }

        public override void Preencher(IDataReader pDrCtrlEntidade)
        {
            throw new NotImplementedException();
        }

        public override void Clear()
        {
            _ID = 0;
            _DataHora = DateTime.MinValue;
            _Mensagem = null;
            _ID_UsuarioCriador = 0;
            _ID_UsuarioMensagem = 0;
            _Criacao = DateTime.MinValue;
            _Lido = false;

        }

        public override string Key
        {
            get { return string.Format("{0}{1}", _ID, _DataHora); }
        }

        public override int ID_BD
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
            }
        }
    }
}
