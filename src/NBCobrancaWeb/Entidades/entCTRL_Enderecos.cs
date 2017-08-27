using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text;

namespace NBCobranca.Entidades
{
    public class entCTRL_Enderecos : entBase
    {
        int _IdEndereco;
        int _IdEntidade;
        bool _principal;
        string _Contato;
        string _Logradouro;
        string _Complemento;
        string _Bairro;
        string _Municipio;
        string _UF;
        string _CEP;
        string _comentario;


        public entCTRL_Enderecos()
            : base()
        {
            this._NomeCamposChave.Add("IdEndereco");
            this._NomeCamposTabela.AddRange(new string[] 
            {"IdEndereco"
            ,"IdEntidade"
            ,"Principal"
            ,"Contato"
            ,"Logradouro"
            ,"Complemento"
            ,"Bairro"
            ,"Municipio"
            ,"UF"
            ,"CEP"
            ,"Comentario"});
            this.Clear();
        }

        public override string Key
        {
            get { return string.Format("{0}_{1}_{2}", _Logradouro.Trim(), _Bairro.Trim(), _Municipio.Trim()); }
        }

        public override int ID_BD
        {
            get
            {
                return this._IdEndereco;
            }
            set
            {
                this._IdEndereco = value;
            }
        }

        public int IdEndereco
        {
            get { return _IdEndereco; }
            set { _IdEndereco = value; }
        }

        public int IdEntidade
        {
            get { return _IdEntidade; }
            set
            {
                this.ValidaAlteracao<int>(ref _IdEntidade, value, "IdEntidade");
            }
        }

        public bool Principal
        {
            get { return _principal; }
            set
            {
                this.ValidaAlteracao<bool>(ref _principal, value, "Principal");
            }
        }

        public string Contato
        {
            get { return _Contato; }
            set
            {
                this.ValidaAlteracao<string>(ref _Contato, value, "Contato");
            }
        }

        public string Logradouro
        {
            get { return _Logradouro; }
            set
            {
                this.ValidaAlteracao<string>(ref _Logradouro, value, "Logradouro");
            }
        }

        public string Complemento
        {
            get { return _Complemento; }
            set
            {
                this.ValidaAlteracao<string>(ref _Complemento, value, "Complemento");
            }
        }

        public string Bairro
        {
            get { return _Bairro; }
            set
            {
                this.ValidaAlteracao<string>(ref _Bairro, value, "Bairro");
            }
        }

        public string Municipio
        {
            get { return _Municipio; }
            set
            {
                this.ValidaAlteracao<string>(ref _Municipio, value, "Municipio");
            }
        }

        public string UF
        {
            get { return _UF; }
            set
            {
                this.ValidaAlteracao<string>(ref _UF, value, "UF");
            }
        }

        public string CEP
        {
            get { return _CEP; }
            set
            {
                value = value.Replace("-", "");
                this.ValidaAlteracao<string>(ref _CEP, value, "CEP");
            }
        }

        public string Comentario
        {
            get { return _comentario; }
            set
            {
                this.ValidaAlteracao<string>(ref _comentario, value, "Comentario");
            }
        }

        public override void Preencher(DataRow pDrCtrlEntidade)
        {
            _CamposAlterados.Clear();
            _IdEndereco = Convert.ToInt32(pDrCtrlEntidade["IdEndereco"]);
            _IdEntidade = Convert.ToInt32(pDrCtrlEntidade["IdEntidade"]);
            _principal = pDrCtrlEntidade["principal"] is DBNull ? false : Convert.ToBoolean(pDrCtrlEntidade["principal"]);
            _Contato = pDrCtrlEntidade["Contato"] is DBNull ? "" : pDrCtrlEntidade["Contato"].ToString();
            _Logradouro = pDrCtrlEntidade["Logradouro"].ToString();
            _Complemento = pDrCtrlEntidade["Complemento"] is DBNull ? "" : pDrCtrlEntidade["Complemento"].ToString();
            _Bairro = pDrCtrlEntidade["Bairro"] is DBNull ? "" : pDrCtrlEntidade["Bairro"].ToString();
            _Municipio = pDrCtrlEntidade["Municipio"] is DBNull ? "" : pDrCtrlEntidade["Municipio"].ToString();
            _UF = pDrCtrlEntidade["UF"] is DBNull ? "" : pDrCtrlEntidade["UF"].ToString();
            _CEP = pDrCtrlEntidade["CEP"] is DBNull ? "" : pDrCtrlEntidade["CEP"].ToString();
            _comentario = pDrCtrlEntidade["comentario"] is DBNull ? "" : pDrCtrlEntidade["comentario"].ToString();
        }

        public override void Preencher(IDataReader pDrCtrlEntidade)
        {
            throw new NotImplementedException();
        }

        public override void Clear()
        {
            _IdEndereco = 0;
            _IdEntidade = 0;
            _principal = false;
            _Contato = null;
            _Logradouro = null;
            _Complemento = null;
            _Bairro = null;
            _Municipio = null;
            _UF = null;
            _CEP = null;
            _comentario = null;
        }

    }
}
