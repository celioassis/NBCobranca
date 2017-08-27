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
    /// <summary>
    /// Entidade responsável por gravar as informações de CPF e CNPJ.
    /// </summary>
    public class entCTRL_CPFCNPJ: entBase
    {
        int _idCPFCNPJ;
        string _CPFCNPJ;

        public entCTRL_CPFCNPJ()
            :base()
        {
            _NomeCamposChave.Add("idCPFCNPJ");
            _NomeCamposTabela.AddRange(new string[]
            {"idCPFCNPJ"
            ,"CPFCNPJ"});

            this.Clear();
        }

        public entCTRL_CPFCNPJ(string pCPF_CNPJ)
            :this()
        {
            _CPFCNPJ = pCPF_CNPJ;
        }

        public entCTRL_CPFCNPJ(int pIdCPF_CNPJ)
            : this()
        {
            _idCPFCNPJ = pIdCPF_CNPJ;
        }

        public override string Key
        {
            get { return _CPFCNPJ; }
        }

        public override int ID_BD
        {
            get
            {
                return _idCPFCNPJ;
            }
            set
            {
                this._idCPFCNPJ = value;
            }
        }

        public override void Preencher(DataRow pDrCtrlEntidade)
        {
            _idCPFCNPJ = Convert.ToInt32(pDrCtrlEntidade[0]);
            _CPFCNPJ = pDrCtrlEntidade[1].ToString().Trim();
        }

        public override void Preencher(IDataReader pDrCtrlEntidade)
        {
            throw new NotImplementedException();
        }

        public override void Clear()
        {
            _idCPFCNPJ = 0;
            _CPFCNPJ = null;
        }

        public int IdCPFCNPJ
        {
            get { return _idCPFCNPJ; }
            set { _idCPFCNPJ = value; }
        }

        public string CPFCNPJ
        {
            get { return _CPFCNPJ; }
            set { this.ValidaAlteracao<string>(ref _CPFCNPJ, value, "CPFCNPJ"); }
        }

    }
}
