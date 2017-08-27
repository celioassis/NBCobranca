using System;
using System.Data;

namespace NBCobranca.Entidades
{
    public class entCTRL_Fones : entBase
    {
        int _IdFone;
        int _IdEntidade;
        int _idEndereco;
        string _Contato;
        string _DDD;
        string _Fone;
        string _Ramal;
        string _descricao;

        public entCTRL_Fones()
        {
            _NomeCamposChave.Add("IdFone");
            _NomeCamposTabela.AddRange(new[]
            {"IdFone",
            "IdEntidade",
            "IdEndereco",
            "Contato",
            "DDD",
            "Fone",
            "Ramal",
            "Descricao"});
            Clear();
        }

        public override string Key
        {
            get { return string.Format("{0}_{1}", _DDD.Trim(), _Fone.Trim()); }
        }

        public override int ID_BD
        {
            get
            {
                return _IdFone;
            }
            set
            {
                _IdFone = value;
            }
        }

        public int IdFone
        {
            get { return _IdFone; }
            set { _IdFone = value; }
        }

        public int IdEntidade
        {
            get { return _IdEntidade; }
            set
            {
                ValidaAlteracao(ref _IdEntidade, value, "IdEntidade");
            }
        }

        public int IdEndereco
        {
            get { return _idEndereco; }
            set
            {
                ValidaAlteracao(ref _idEndereco, value, "idEndereco");
            }
        }

        public string Contato
        {
            get { return _Contato; }
            set
            {
                ValidaAlteracao(ref _Contato, value, "Contato");
            }
        }

        public string DDD
        {
            get { return _DDD; }
            set
            {
                if (!string.IsNullOrEmpty(value))
                    value = value.Trim();
                ValidaAlteracao(ref _DDD, value, "DDD");
            }
        }

        public string Fone
        {
            get { return _Fone; }
            set
            {
                value = value.Trim().Replace("-", "");
                ValidaAlteracao(ref _Fone, value, "Fone");
            }
        }

        public string Ramal
        {
            get { return _Ramal; }
            set { ValidaAlteracao(ref _Ramal, value, "Ramal"); }
        }

        public string Descricao
        {
            get { return _descricao; }
            set { ValidaAlteracao(ref _descricao, value, "Descricao"); }
        }

        public override void Preencher(DataRow pDrCtrlEntidade)
        {
            _IdFone = pDrCtrlEntidade["IdFone"] is DBNull ? 0 : Convert.ToInt32(pDrCtrlEntidade["IdFone"]);
            _IdEntidade = pDrCtrlEntidade["IdEntidade"] is DBNull ? 0 : Convert.ToInt32(pDrCtrlEntidade["IdEntidade"]);
            _idEndereco = pDrCtrlEntidade["idEndereco"] is DBNull ? 0 : Convert.ToInt32(pDrCtrlEntidade["idEndereco"]);
            _Contato = pDrCtrlEntidade["Contato"] is DBNull ? "" : pDrCtrlEntidade["Contato"].ToString();
            _DDD = pDrCtrlEntidade["DDD"] is DBNull ? "" : pDrCtrlEntidade["DDD"].ToString();
            _Fone = pDrCtrlEntidade["Fone"] is DBNull ? "" : pDrCtrlEntidade["Fone"].ToString();
            _Ramal = pDrCtrlEntidade["Ramal"] is DBNull ? "" : pDrCtrlEntidade["Ramal"].ToString();
            _descricao = pDrCtrlEntidade["descricao"] is DBNull ? "" : pDrCtrlEntidade["descricao"].ToString();
        }

        public override void Preencher(IDataReader pDrCtrlEntidade)
        {
            throw new NotImplementedException();
        }

        public override void Clear()
        {
            _IdFone = 0;
            _IdEntidade = 0;
            _idEndereco = 0;
            _Contato = null;
            _DDD = null;
            _Fone = null;
            _Ramal = null;
            _descricao = null;
        }
    }
}
