using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace NBCobranca.Entidades
{
    public class entConfiguracoes : entBase
    {
        int _ID;
        string _ServidorSMTP;
        int _PortaSMTP;
        string _UsuarioSMTP;
        string _Senha;

        public entConfiguracoes() : base()
        {
            _NomeCamposChave.Add("ID");
            _NomeCamposTabela.AddRange(new string[]
            {"ID",
             "ServidorSMTP",
             "PortaSMTP",
             "UsuarioSMTP",
             "Senha"});

            Clear();

        }

        #region Campos da Tabela Configuracoes

        public override int ID_BD
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public int ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public string ServidorSMTP
        {
            get { return _ServidorSMTP; }

            set { ValidaAlteracao(ref _ServidorSMTP, value, "ServidorSMTP"); }
        }

        public int PortaSMTP
        {
            get { return _PortaSMTP; }

            set { ValidaAlteracao(ref _PortaSMTP, value, "PortaSMTP"); }
        }

        public string UsuarioSMTP
        {
            get { return _UsuarioSMTP; }

            set { ValidaAlteracao(ref _UsuarioSMTP, value, "UsuarioSMTP"); }

        }

        public string Senha
        {
            get { return _Senha; }
            set { ValidaAlteracao(ref _Senha, value, "Senha"); }
        }

        #endregion

        public override string Key
        {
            get { return string.Format("{0}", _ID); }
        }

        public override void Clear()
        {
            _ID = 0;
            ServidorSMTP = string.Empty;
            PortaSMTP = 0;
            UsuarioSMTP = string.Empty;
            Senha = string.Empty;
        }

        public override void Preencher(IDataReader pDrCtrlEntidade)
        {
            throw new NotImplementedException();
        }

        public override void Preencher(DataRow pDrCtrlEntidade)
        {
            _ID = Convert.ToInt32(pDrCtrlEntidade["ID"]);
            ServidorSMTP = pDrCtrlEntidade["ServidorSMTP"].ToString();
            PortaSMTP = Convert.ToInt32(pDrCtrlEntidade["PortaSMTP"]);
            UsuarioSMTP = pDrCtrlEntidade["UsuarioSMTP"].ToString();
            Senha = pDrCtrlEntidade["Senha"].ToString();
        }
    }
}
