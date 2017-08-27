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
using System.Collections.Generic;
using System.Reflection;

namespace NBCobranca.Entidades
{
    public class entCTRL_Entidades : entBase
    {
        int _IdEntidade;
        bool _PessoaFJ;
        int _IdCPFCNPJ;
        string _CPFCNPJ;
        string _NomePrimary;
        string _NomeSecundary;
        string _RGIE;
        string _OrgaoEmissor_RGIE;
        DateTime? _DtNascimento;
        string _TxtRespeito;
        DateTime? _DtCriacao;
        DateTime? _DtAlteracao;
        Boolean _Alterando;
        Dictionary<long, Entidades.entCTRL_Enderecos> _Enderecos;
        Dictionary<long, entCTRL_Fones> _Telefones;
        Dictionary<long, entCTRL_Email> _Emails;
        Dictionary<long, entCTRL_Url> _Sites;
        Dictionary<long, entCOBR_Divida> _Dividas;

        public entCTRL_Entidades()
            : base()
        {
            this._NomeCamposChave.Add("IdEntidade");
            this._NomeCamposTabela.AddRange(new string[]
            {   "PessoaFJ",
                "IdCPFCNPJ",
                "NomePrimary",
                "NomeSecundary",
                "RGIE",
                "OrgaoEmissor_RGIE",
                "DtNascimento",
                "TxtRespeito",
                "DtCriacao",
                "DtAlteracao"
            });
            this.Clear();
        }

        public entCTRL_Entidades(DataRow pDrCtrlEntidade)
            : this()
        {
            this.Preencher(pDrCtrlEntidade);
        }

        public override string Key
        {
            get { return string.Format("{0}_{1}", _CPFCNPJ.Trim(), _NomePrimary.Trim()); }
        }

        public override int ID_BD
        {
            get
            {
                return this._IdEntidade;
            }
            set
            {
                this._IdEntidade = value;
            }
        }

        /// <summary>
        /// Indica se a entidade esta sendo alterada.
        /// </summary>
        public Boolean Alterando
        {
            get { return _Alterando; }
            set { _Alterando = value; }
        }

        public int IdEntidade
        {
            get { return _IdEntidade; }
            set { _IdEntidade = value; }
        }

        public bool PessoaFJ
        {
            get { return _PessoaFJ; }
            set { this.ValidaAlteracao<bool>(ref _PessoaFJ, value, "PessoaFJ"); }
        }

        public int IdCPFCNPJ
        {
            get
            {
                if (_IdCPFCNPJ == 0 && this._PessoaFJ)
                    _IdCPFCNPJ = 2;
                else if (_IdCPFCNPJ == 0)
                    _IdCPFCNPJ = 1;
                return _IdCPFCNPJ;
            }
            set
            {
                this.ValidaAlteracao<int>(ref _IdCPFCNPJ, value, "IdCPFCNPJ");
            }
        }

        public string CPFCNPJ
        {
            get { return this._CPFCNPJ; }
            set { this._CPFCNPJ = value; }
        }

        public string NomePrimary
        {
            get { return _NomePrimary; }
            set { this.ValidaAlteracao<string>(ref _NomePrimary, value, "NomePrimary"); }
        }

        public string NomeSecundary
        {
            get { return _NomeSecundary; }
            set { this.ValidaAlteracao<string>(ref _NomeSecundary, value, "NomeSecundary"); }
        }

        public string RGIE
        {
            get { return _RGIE; }
            set { this.ValidaAlteracao<string>(ref _RGIE, value, "RGIE"); }
        }

        public string OrgaoEmissor_RGIE
        {
            get { return _OrgaoEmissor_RGIE; }
            set { this.ValidaAlteracao<string>(ref _OrgaoEmissor_RGIE, value, "OrgaoEmissor_RGIE"); }
        }

        public DateTime? DtNascimento
        {
            get { return _DtNascimento; }
            set { this.ValidaAlteracao<DateTime?>(ref _DtNascimento, value, "DtNascimento"); }
        }

        public string TxtRespeito
        {
            get { return _TxtRespeito; }
            set { this.ValidaAlteracao<string>(ref _TxtRespeito, value, "TxtRespeito"); }
        }

        public DateTime? DtCriacao
        {
            get { return _DtCriacao; }
            set { this.ValidaAlteracao<DateTime?>(ref _DtCriacao, value, "DtCriacao"); }
        }

        public DateTime? DtAlteracao
        {
            get { return _DtAlteracao; }
            set { this.ValidaAlteracao<DateTime?>(ref _DtAlteracao, value, "DtAlteracao"); }
        }

        public Dictionary<long, Entidades.entCTRL_Enderecos> Enderecos
        {
            get
            {
                if (_Enderecos == null)
                    _Enderecos = new Dictionary<long, entCTRL_Enderecos>();
                return _Enderecos;
            }
            set { _Enderecos = value; }
        }

        public Dictionary<long, entCTRL_Fones> Telefones
        {
            get
            {
                if (_Telefones == null)
                    _Telefones = new Dictionary<long, entCTRL_Fones>();
                return _Telefones;
            }
            set { _Telefones = value; }
        }

        public Dictionary<long, entCTRL_Email> Emails
        {
            get
            {
                if (_Emails == null)
                    _Emails = new Dictionary<long, entCTRL_Email>();
                return _Emails;
            }
            set { _Emails = value; }
        }

        public Dictionary<long, entCTRL_Url> Sites
        {
            get
            {
                if (_Sites == null)
                    _Sites = new Dictionary<long, entCTRL_Url>();
                return _Sites;
            }
            set { _Sites = value; }
        }

        public Dictionary<long, entCOBR_Divida> Dividas
        {
            get
            {
                if (_Dividas == null)
                    _Dividas = new Dictionary<long, entCOBR_Divida>();
                return _Dividas;
            }
            set { _Dividas = value; }
        }

        public override void Preencher(DataRow pDrCtrlEntidade)
        {
            this._Alterando = true;
            foreach (DataColumn mColuna in pDrCtrlEntidade.Table.Columns)
            {
                var mProperty = GetPropertyInfo(mColuna.ColumnName);
                if (mProperty != null && !(pDrCtrlEntidade[mColuna.ColumnName] is DBNull))
                    mProperty.SetValue(this, pDrCtrlEntidade[mColuna.ColumnName], null);
            }
        }

        public override void Preencher(IDataReader pDrCtrlEntidade)
        {
            throw new NotImplementedException();
        }


        /// <summary>
        /// Instrução SQL usando como filtro de pesquisa o código(IdEntidade) da Entidade.
        /// </summary>
        /// <param name="pCodigo">Código da entidade (IdEntidade)</param>
        /// <returns></returns>
        public string SqlSelect(int? pCodigo)
        {
            StringBuilder mSQL = this.SqlBuilderSelect;
            mSQL.AppendFormat("where IdEntidade = {0}", pCodigo);
            return mSQL.ToString();
        }

        /// <summary>
        /// Instrução SQL usando como filtro de pesquisa o CNPJ ou CPF da Entidade.
        /// </summary>
        /// <param name="pCNPJ_CPF">Número do CNPJ ou CPF da entidade sem formatação</param>
        /// <returns></returns>
        public string SqlSelect(string pCNPJ_CPF_Nome, bool pPesquisarPorNome)
        {
            StringBuilder mSQL = this.SqlBuilderSelect;
            if (pPesquisarPorNome)
                mSQL.AppendFormat("where NOMEPRIMARY = '{0}'", pCNPJ_CPF_Nome);
            else
                mSQL.AppendFormat("where CTRL_CPFCNPJ.CPFCNPJ = '{0}'", pCNPJ_CPF_Nome);
            return mSQL.ToString();
        }

        public override void Clear()
        {
            _IdEntidade = 0;
            _PessoaFJ = true;
            _IdCPFCNPJ = 0;
            _NomePrimary = "";
            _NomeSecundary = "";
            _RGIE = "";
            _OrgaoEmissor_RGIE = "";
            _DtNascimento = null;
            _TxtRespeito = "";
            _DtCriacao = null;
            _DtAlteracao = null;

            if (_Enderecos != null)
                _Enderecos.Clear();
            _Enderecos = null;
            if (_Telefones != null)
                _Telefones.Clear();
            _Telefones = null;
            if (_Emails != null)
                _Emails.Clear();
            _Emails = null;
            if (_Sites != null)
                _Sites.Clear();
            _Sites = null;
            if (_Dividas != null)
                _Dividas.Clear();
            _Dividas = null;
        }

        /// <summary>
        /// Retorna uma Instrução SQL para popular uma entidade entCTRLEntidades.
        /// </summary>
        public StringBuilder SqlBuilderSelect
        {
            get
            {
                StringBuilder mSQL = new StringBuilder();
                mSQL.AppendLine("select Ent.*, CTRL_CPFCNPJ.CPFCNPJ ");
                mSQL.AppendLine("from CTRL_Entidades Ent");
                mSQL.AppendLine("join CTRL_CPFCNPJ on CTRL_CPFCNPJ.idCPFCNPJ = Ent.idCPFCNPJ");
                return mSQL;
            }
        }

    }
}
