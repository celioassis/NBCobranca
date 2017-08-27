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
    public class entCTRL_Url : entBase
    {

        int _idURL;
        int _idEntidade;
        string _url;
        string _descricao;

        public entCTRL_Url()
            :base()
        {
            _NomeCamposChave.Add("IdURL");
            _NomeCamposTabela.AddRange(new string[]
            {"IdURL"
            ,"IdEntidade"
            ,"Url"
            ,"Descricao"});

            this.Clear();
        }
        
        public override string Key
        {
            get { return string.Format("{0}", _url); }
        }

        public override int ID_BD
        {
            get
            {
                return this._idURL;
            }
            set
            {
                this._idURL = value;
            }
        }

        public int IdURL
        {
            get { return _idURL; }
            set { _idURL = value; }
        }

        public int IdEntidade
        {
            get { return _idEntidade; }
            set { this.ValidaAlteracao<int>(ref _idEntidade, value, "IdEntidade"); }
        }

        public string Url
        {
            get { return _url; }
            set { this.ValidaAlteracao<string>(ref _url, value, "Url"); }
        }

        public string Descricao
        {
            get { return _descricao; }
            set { this.ValidaAlteracao<string>(ref _descricao, value, "Descricao"); }
        }


        public override void Preencher(DataRow pDrCtrlEntidade)
        {
            _idURL = Convert.ToInt32(pDrCtrlEntidade["idURL"]);
            _idEntidade = Convert.ToInt32(pDrCtrlEntidade["idEntidade"]);
            _url = pDrCtrlEntidade["url"] is DBNull ? "" : pDrCtrlEntidade["url"].ToString();
            _descricao = pDrCtrlEntidade["descricao"] is DBNull ? "" : pDrCtrlEntidade["descricao"].ToString();
        }

        public override void Preencher(IDataReader pDrCtrlEntidade)
        {
            throw new NotImplementedException();
        }

        public override void Clear()
        {
            _idURL = 0;
            _idEntidade = 0;
            _url = null;
            _descricao = null;
        }

    }
}
