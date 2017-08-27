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
    public class entCTRL_Email : entBase
    {
        int _idEMail;
        int _IdEntidade;
        string _eMail;
        string _descricao;

        public entCTRL_Email()
            :base()
        {
            this._NomeCamposChave.Add("IdEMail");
            this._NomeCamposTabela.AddRange(new string[]
            {   "IdEMail",
                "IdEntidade",
                "EMail",
                "Descricao"
            });
            
            this.Clear();
        }

        public override string Key
        {
            get { return string.Format("{0}", _eMail.Trim()); }
        }

        public override int ID_BD
        {
            get
            {
                return this._idEMail;
            }
            set
            {
                this._idEMail = value;
            }
        }

        public int IdEMail
        {
            get { return _idEMail; }
            set { _idEMail = value; }
        }

        public int IdEntidade
        {
            get { return _IdEntidade; }
            set { this.ValidaAlteracao<int>(ref _IdEntidade, value, "IdEntidade"); }
        }

        public string EMail
        {
            get { return _eMail; }
            set { this.ValidaAlteracao<string>(ref _eMail, value, "EMail"); }
        }

        public string Descricao
        {
            get { return _descricao; }
            set { this.ValidaAlteracao<string>(ref _descricao, value, "Descricao"); }
        }


        public override void Preencher(DataRow pDrCtrlEntidade)
        {
            _idEMail = Convert.ToInt32(pDrCtrlEntidade["idEMail"]);
            _IdEntidade = Convert.ToInt32(pDrCtrlEntidade["IdEntidade"]);
            _eMail = pDrCtrlEntidade["eMail"] is DBNull ? "" : pDrCtrlEntidade["eMail"].ToString();
            _descricao = pDrCtrlEntidade["descricao"] is DBNull ? "" : pDrCtrlEntidade["descricao"].ToString();
        }

        public override void Preencher(IDataReader pDrCtrlEntidade)
        {
            throw new NotImplementedException();
        }

        public override void Clear()
        {
            _idEMail = 0;
            _IdEntidade = 0;
            _eMail = null;
            _descricao = null;
        }
    }
}
