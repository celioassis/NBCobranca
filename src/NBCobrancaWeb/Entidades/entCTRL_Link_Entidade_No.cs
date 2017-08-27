using System;
using System.Data;
using System.Configuration;
using System.Text;
using System.Collections.Generic;

namespace NBCobranca.Entidades
{
    public class entCTRL_Link_Entidade_No : entBase
    {
        int _IdLink_Entidade_No;
        int _IdEntidade;
        int _IdNo;

        public entCTRL_Link_Entidade_No()
            : base()
        {
            _NomeCamposChave.Add("IdLink_Entidade_No");
            _NomeCamposTabela.AddRange(new string[]
            {"IdEntidade"
            ,"IdNo"});

            this.Clear();
        }

        public entCTRL_Link_Entidade_No(int pIdLink_Entidade_No, int pIdEntidade, int pIdNo)
            :this()
        {
            _IdLink_Entidade_No = pIdLink_Entidade_No;
            _IdEntidade = pIdEntidade;
            _IdNo = pIdNo;
        }

        public int IdLink_Entidade_No
        {
            get { return _IdLink_Entidade_No; }
            set { _IdLink_Entidade_No = value; }
        }

        public int IdEntidade
        {
            get { return _IdEntidade; }
            set { _IdEntidade = value; }
        }

        public int IdNo
        {
            get { return _IdNo; }
            set { _IdNo = value; }
        }

        #region [ Métodos da entBase ]

        public override int ID_BD
        {
            get
            {
                return this._IdLink_Entidade_No;
            }
            set
            {
                this._IdLink_Entidade_No = value;
            }
        }

        public override void Preencher(DataRow pDrCtrlEntidade)
        {
            _IdLink_Entidade_No = Convert.ToInt32(pDrCtrlEntidade[0]);
            _IdEntidade = Convert.ToInt32(pDrCtrlEntidade[1]);
            _IdNo = Convert.ToInt32(pDrCtrlEntidade[2]);
        }

        public override void Preencher(IDataReader pDrCtrlEntidade)
        {
            throw new NotImplementedException();
        }

        public override void Clear()
        {
            _IdLink_Entidade_No = 0;
            _IdEntidade = 0;
            _IdNo = 0;
            
        }

        #endregion

        public override string Key
        {
            get { return string.Format("{0}_{1}", _IdEntidade, _IdNo); }
        }

        public string SqlDefault
        {
            get
            {
                Dictionary<string, object> mKeyValor = new Dictionary<string, object>();
                mKeyValor.Clear();
                mKeyValor.Add("IdEntidade", _IdEntidade);
                mKeyValor.Add("IdNo", _IdNo);
                return SqlSelect(mKeyValor);
            }
        }

    }
}
