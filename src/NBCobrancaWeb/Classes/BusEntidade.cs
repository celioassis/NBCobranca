using System;
using System.Data;
using System.Configuration;
using System.Text;
using System.Collections.Generic;
using NBArvore;
using NBCobranca.Interfaces;
using NBCobranca.Entidades;

namespace NBCobranca.Classes
{
    public class BusEntidade : BusBase
    {
        public BusEntidade(Sistema sistema, Neobridge.NBDB.DBDirect dbDirect)
            : base(sistema, dbDirect)
        { }

        /// <summary>
        /// Cria um DataTable com todas as Entidades encontradas em uma determinada página aplicando os filtros definido nos parametros.
        /// </summary>
        /// <param name="pFiltroPesquisa">valor para filtrar a pesquisa caso contrario não filtra</param>
        /// <param name="pXmPath">Caminho XmPath onde se deseja fazer a busca</param>
        /// <param name="pCurrentPage">Página que se deseja buscar dentro do resultado da busca, este paramentro é usado para paginacao</param>
        /// <param name="pPageSize">Quantidade de registros retornados por página</param>
        /// <param name="pTotalRegistros">Quantidade total de registros retornados pela pesquisa</param>
        /// <param name="pTipoPesquisa">Define qual é o tipo de pesquisa, por Código, Nome, etc...</param>
        /// <param name="pTipoPessoa">Define se será filtrado por tipo de Pessoa, como fisica, jurídica ou todas para não aplicar o filtro.</param>
        /// <returns></returns>
        public DataTable LoadEntidades(string pFiltroPesquisa, string pXmPath, int pCurrentPage, int pPageSize, ref int pTotalRegistros, Tipos.TipoPesquisa pTipoPesquisa, Tipos.TipoPessoa pTipoPessoa)
        {
            try
            {
                //SQL que Retorna todas os Registros de uma entidade, como por ex: 
                //Todos os Clientes, Fornecedores ou Funcionários, conforme o parametro 
                //sqlWhere.
                string comandoSQL = strSQL;

                //Concatena o comandoSQL com o sqlWhere + outras funcionálidades do comando SQL.
                comandoSQL += this.WherePesq(pTipoPesquisa, pTipoPessoa, pFiltroPesquisa, pXmPath);

                System.Data.SqlClient.SqlCommand mSqlCmd = NBFuncoes.SqlCmdPaginacao(comandoSQL, "IdEntidade", 1, pCurrentPage, pPageSize, null);

                //Cria-se um novo DataSet para receber os dados da pesquisa.
                System.Data.DataSet DS = this.DbDirect.CriarDataSet(mSqlCmd);

                pTotalRegistros = Convert.ToInt32(mSqlCmd.Parameters["@ROWS_TOTAL"].Value);
                if (DS.Tables.Count == 0)
                    return null;
                else
                    return DS.Tables[0];

            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível realizar a busca das entidades", ex);
            }
        }

        public DataView LoadEntidades(string carteiraSelecionada, Tipos.TipoPesquisa pTipoPesquisa, string textoProcurar)
        {
            throw new NotImplementedException();
        }

        public List<entCTRL_Entidades> LoadClientes()
        {
            entCTRL_Entidades mEnt = new entCTRL_Entidades();
            StringBuilder comandoSQL = mEnt.SqlBuilderSelect;

            comandoSQL.AppendLine("join CTRL_Link_Entidade_No lnkEN  on Ent.idEntidade = lnkEN.idEntidade");
            comandoSQL.AppendLine("join CTRL_Nos Nos on Nos.IdNo = lnkEN.IdNo and Nos.XmPath = '<Entidades><Clientes>'");


            DataTable mDT = this.DbDirect.CriarDataTable(comandoSQL.ToString());
            if (mDT.Rows.Count == 0)
                return null;
            List<entCTRL_Entidades> mLista = new List<entCTRL_Entidades>();
            foreach (DataRow mDR in mDT.Rows)
            {
                mLista.Add(new entCTRL_Entidades(mDR));
            }
            return mLista;
        }

        public void Excluir(int pCodigoEntidade, string pNomeEntidade, Tipos.TipoEntidades pTipoEntidade, string pXmPath)
        {
            try
            {
                this.DbDirect.Transaction_Begin();
                if (pTipoEntidade != NBCobranca.Tipos.TipoEntidades.Clientes)
                {
                    if (this.EstaEntidadeEstaEmMuitosNos(pCodigoEntidade))
                        this.ExcluirLinkEntidadeNo(pCodigoEntidade, pXmPath);
                    else
                        this.Excluir(pCodigoEntidade);
                }
                else
                {
                    this.Excluir(pCodigoEntidade);
                    this.Sistema.BusClasses.ExcluirNo("Entidades\\Carteiras\\" + pNomeEntidade);
                    this.Sistema.BusClasses.ExcluirNo("Borderos\\" + pNomeEntidade);
                    this.Sistema.BusClasses.SalvarArvore();
                }
                this.DbDirect.Transaction_Commit();

            }
            catch (Exception ex)
            {
                this.DbDirect.Transaction_Cancel();
                throw new Exception("Não foi possível excluir a Entidade de código " + pCodigoEntidade.ToString(), ex);
            }
        }

        /// <summary>
        /// Carrega uma entidade e todas as suas dependencias se solicitado, conforme o seu tipo.
        /// </summary>
        /// <param name="pCodigo">Código da entidade</param>
        /// <param name="pTipoEntidade">Tipo da entidade que se deseja carregar.</param>
        /// <param name="pCompleta">Indica se a carga será completa com todas as suas dependecias ou somente a entidade 
        /// principal.</param>
        /// <returns></returns>
        public Entidades.entCTRL_Entidades Get(int pCodigo, Tipos.TipoEntidades pTipoEntidade, bool pCompleta)
        {
            try
            {
                DbDirect.VerificaAbreConexao(true);
                Entidades.entCTRL_Entidades mEntidade = new NBCobranca.Entidades.entCTRL_Entidades();
                DataTable mDT = this.DbDirect.CriarDataTable(mEntidade.SqlSelect(pCodigo));
                if (mDT.Rows.Count == 0)
                    return null;

                mEntidade.Preencher(mDT.Rows[0]);
                if (pCompleta)
                    this.PreencheDependencias(mEntidade, pTipoEntidade);

                return mEntidade;

            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível carregar a entidade de código " + pCodigo.ToString(), ex);
            }
            finally
            {
                DbDirect.VerificaFechaConexao(true);
            }
        }

        /// <summary>
        /// Carrega uma entidade e todas as suas dependencias se solicitado, conforme o seu tipo.
        /// </summary>
        /// <param name="pCPF_CNPJ">Número do CPF ou CNPJ sem formatação da entidade.</param>
        /// <param name="pTipoEntidade">Tipo da entidade que se deseja carregar.</param>
        /// <param name="pCompleta">Indica se a carga será completa com todas as suas dependecias ou somente a entidade 
        /// principal.</param>
        /// <returns></returns>
        public Entidades.entCTRL_Entidades Get(string pCPF_CNPJ_Nome, Tipos.TipoEntidades pTipoEntidade, bool pCompleta, bool pPesquisarPorNome)
        {
            try
            {
                Entidades.entCTRL_Entidades mEntidade = new NBCobranca.Entidades.entCTRL_Entidades();
                DataTable mDT = this.DbDirect.CriarDataTable(mEntidade.SqlSelect(pCPF_CNPJ_Nome, pPesquisarPorNome));

                if (mDT.Rows.Count == 0)
                    return null;

                if (mDT.Rows.Count > 1)
                    throw new ExceptionVariasEntidadesComMesmoCNPJ_CPF(string.Format("Existem mais de um {0} cadastrado com esse CPF/CNPJ, se deseja visualizar Cancele esta Operação e faça uma pesquisa por CPF/CNPJ",
                        (pTipoEntidade != NBCobranca.Tipos.TipoEntidades.Funcionarios && pTipoEntidade != NBCobranca.Tipos.TipoEntidades.Clientes)
                    ? pTipoEntidade.ToString().Replace("res", "r") : pTipoEntidade.ToString().Replace("s", "")));

                mEntidade.Preencher(mDT.Rows[0]);
                if (pCompleta)
                    this.PreencheDependencias(mEntidade, pTipoEntidade);

                return mEntidade;

            }
            catch (ExceptionVariasEntidadesComMesmoCNPJ_CPF) { throw; }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível carregar a entidade de CPF ou CNPJ " + pCPF_CNPJ_Nome, ex);
            }
        }

        public void Salvar(Entidades.entCTRL_Entidades pEntidade, List<Entidades.entBase> pEntidadesParaExcluir, string pXmPath)
        {
            if (string.IsNullOrEmpty(pEntidade.NomePrimary.Trim()))
                throw new ExceptionCampoObrigatorio("Nome ou Razão Social");

            string mSource = this.Source + ".Salvar";
            pXmPath = pXmPath.Replace(">", "");
            pXmPath = pXmPath.Replace('<', '\\');
            Dictionary<string, object> mKeyValor = new Dictionary<string, object>();
            try
            {
                NBArvore.No mNo = this.Sistema.BusClasses.GetNo(pXmPath);
                entCTRL_Link_Entidade_No mLinkEntNo = new entCTRL_Link_Entidade_No(0, pEntidade.IdEntidade, mNo.Id);

                this.DbDirect.Transaction_Begin();

                #region [ Salva as alterações ou inclui uma nova Entidade no Banco ]

                if (!string.IsNullOrEmpty(pEntidade.CPFCNPJ))
                {
                    entCTRL_CPFCNPJ mCPF_CNPJ = new entCTRL_CPFCNPJ(pEntidade.CPFCNPJ.Replace(".", "").Replace("-", "").Replace("/", ""));
                    mKeyValor.Clear();
                    mKeyValor.Add("CPFCNPJ", mCPF_CNPJ.CPFCNPJ);
                    DataTable mDT = this.DbDirect.CriarDataTable(mCPF_CNPJ.SqlSelect(mKeyValor));
                    if (mDT.Rows.Count == 0)
                        mCPF_CNPJ.Salvar(this.DbDirect);
                    else
                        mCPF_CNPJ.IdCPFCNPJ = Convert.ToInt32(mDT.Rows[0][0]);
                    pEntidade.IdCPFCNPJ = mCPF_CNPJ.IdCPFCNPJ;
                }

                if (pEntidade.Alterando)
                {
                    if (this.DbDirect.CriarDataTable(mLinkEntNo.SqlDefault).Rows.Count == 0)
                        mLinkEntNo.Salvar(this.DbDirect);

                    pEntidade.Salvar(this.DbDirect);
                }
                else
                {
                    pEntidade.Salvar(this.DbDirect);
                    mLinkEntNo.IdEntidade = pEntidade.IdEntidade;
                    mLinkEntNo.Salvar(this.DbDirect);
                }

                #endregion

                #region [ Salva as alterações ou inclui as entidades das coleções no banco]

                foreach (entCTRL_Enderecos mEndereco in pEntidade.Enderecos.Values)
                {
                    if (mEndereco.ID_BD == 0)
                        mEndereco.IdEntidade = pEntidade.IdEntidade;
                    mEndereco.Salvar(this.DbDirect);
                }

                foreach (entCTRL_Fones mTelefone in pEntidade.Telefones.Values)
                {
                    if (mTelefone.ID_BD == 0)
                        mTelefone.IdEntidade = pEntidade.IdEntidade;

                    mTelefone.Salvar(this.DbDirect);
                }

                foreach (entCTRL_Email mEmail in pEntidade.Emails.Values)
                {
                    if (mEmail.ID_BD == 0)
                        mEmail.IdEntidade = pEntidade.IdEntidade;

                    mEmail.Salvar(this.DbDirect);
                }

                foreach (entCTRL_Url mSite in pEntidade.Sites.Values)
                {
                    if (mSite.ID_BD == 0)
                        mSite.IdEntidade = pEntidade.IdEntidade;

                    mSite.Salvar(this.DbDirect);
                }

                foreach (entCOBR_Divida mDivida in pEntidade.Dividas.Values)
                {
                    if (mDivida.ID_BD == 0)
                        mDivida.IdEntidade = pEntidade.IdEntidade;

                    mDivida.Salvar(this.DbDirect);
                }

                #endregion

                #region [ Excluir as entidades das coleções marcadas para exclusão ]

                foreach (entBase mEntidade in pEntidadesParaExcluir)
                {
                    mEntidade.Excluir(this.DbDirect);
                }
                pEntidadesParaExcluir.Clear();

                #endregion

                this.DbDirect.Transaction_Commit();

            }
            catch (Exception ex)
            {
                this.DbDirect.Transaction_Cancel();
                throw new Exception("Não foi possível salvar os dados do(a) {0}, erro inesperado, entre em contato com o suporte técnico", ex);
            }

        }

        #region --- Métodos Auxiliares ---

        private bool EstaEntidadeEstaEmMuitosNos(int pCodigoEntidade)
        {
            StringBuilder mSQL = new StringBuilder();
            mSQL.AppendLine("SELECT COUNT(IdEntidade)");
            mSQL.AppendLine("FROM CTRL_Link_Entidade_No");
            mSQL.AppendFormat("where IdEntidade = {0}", pCodigoEntidade);
            int mTotalNos = (int)this.DbDirect.Execute_Scalar(mSQL.ToString());
            return mTotalNos > 1;
        }

        private void ExcluirLinkEntidadeNo(int pCodigoEntidade, string pXmPath)
        {
            try
            {
                StringBuilder mSQL = new StringBuilder();
                mSQL.AppendLine("declare @IdLinkEntidadeNo int");
                mSQL.AppendLine("select @IdLinkEntidadeNo = IdLink_Entidade_No ");
                mSQL.AppendLine("from CTRL_Link_Entidade_No");
                mSQL.AppendLine("join CTRL_Nos on CTRL_Nos.IdNo = CTRL_Link_Entidade_No.IdNo");
                mSQL.AppendFormat("and CTRL_Nos.XmPath = '{0}'\r\n", pXmPath);
                mSQL.AppendFormat("where IdEntidade = {0}\r\n", pCodigoEntidade);
                mSQL.AppendLine("delete CTRL_Link_Entidade_No where IdLink_Entidade_No = @IdLinkEntidadeNo");

                this.DbDirect.Execute_NonQuery(mSQL.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível excluir o link da Entidade com o Nó ", ex);
            }
        }

        private void Excluir(int pCodigoEntidade)
        {
            try
            {
                this.DbDirect.Execute_NonQuery("delete CTRL_Entidades where idEntidade = " + pCodigoEntidade.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível excluir a entidade " + pCodigoEntidade.ToString(), ex);
            }
        }

        /// <summary>
        /// String SQL de Select prédefinida para Entidades
        /// </summary>
        private string strSQL
        {
            get
            {
                StringBuilder mSQL = new StringBuilder();
                mSQL.AppendLine("SELECT CTRL_Entidades.IdEntidade, CTRL_Entidades.NomePrimary, CTRL_Enderecos.Logradouro, CTRL_Enderecos.Municipio, CTRL_Enderecos.UF ");
                mSQL.AppendLine("FROM CTRL_Entidades ");
                mSQL.AppendLine("LEFT JOIN CTRL_Enderecos ON CTRL_Entidades.IdEntidade = CTRL_Enderecos.IdEntidade AND CTRL_Enderecos.Principal = 1");
                mSQL.AppendLine("JOIN CTRL_Link_Entidade_No ON CTRL_Entidades.IdEntidade = CTRL_Link_Entidade_No.IdEntidade ");
                mSQL.AppendLine("JOIN CTRL_Nos ON CTRL_Link_Entidade_No.IdNo = CTRL_Nos.IdNo ");
                mSQL.AppendLine("JOIN CTRL_CPFCNPJ ON CTRL_Entidades.idCPFCNPJ = CTRL_CPFCNPJ.idCPFCNPJ ");
                return mSQL.ToString();
            }
        }

        /// <summary>
        /// WherePesq - Gera uma string com um filtro para uma SQL de Pesquisa
        /// </summary>
        /// <param name="TipoPesq">
        /// Tipo da Pesquisa, Ex: é por ID, Nome, CPF ou Cidade
        /// </param>
        /// <param name="TipoPess">
        /// Tipo da Pessoa, Ex: é Fisica ou Jurídica
        /// </param>
        /// <param name="TipoEnti">
        /// Tipo da Entidade: Ex: é um Cliente, Fornecedor ou Funcionário
        /// </param>
        /// <param name="Valor">
        /// Valor de Pesquisa: Ex: o Número do ID, Nome da Pessoa, Número do CPF 
        /// ou o Nome da Cidade
        /// </param>
        /// <returns>
        /// Retorna uma String contendo a instrução Where para um Comando SQL.
        /// </returns>
        private System.Text.StringBuilder WherePesq(Tipos.TipoPesquisa TipoPesq, Tipos.TipoPessoa TipoPess, string Valor, string XmPath)
        {
            System.Text.StringBuilder sqlWhere = new System.Text.StringBuilder();
            sqlWhere.AppendFormat("WHERE CTRL_Nos.XmPath LIKE '{0}%'\r\n", XmPath);

            //Verifica se o Tipo da Pessoa é diferente do Tipo Todas, isso significa que 
            //só poderá ser Física ou Jurídica.
            if (TipoPess != Tipos.TipoPessoa.Todas)
                sqlWhere.AppendFormat("AND CTRL_Entidades.PessoaFJ={0}\r\n", (int)TipoPess);

            //Verifica os Tipos de Pesquisa que podem ser por ID, Nome, CPF ou Cidade.
            switch (TipoPesq)
            {
                case Tipos.TipoPesquisa.ID:
                    if (Valor == "") Valor = "0";
                    sqlWhere.AppendFormat("AND CTRL_Entidades.IdEntidade = {0}\r\n", Valor);
                    break;

                case Tipos.TipoPesquisa.Nome:
                    sqlWhere.AppendFormat("AND CTRL_Entidades.NomePrimary Like '{0}%'\r\n", Valor);
                    break;

                case Tipos.TipoPesquisa.CPF:
                    if (Valor == "") Valor = "0";
                    sqlWhere.AppendFormat("AND CTRL_CPFCNPJ.CPFCNPJ = '{0}'\r\n", Valor);
                    break;

                case Tipos.TipoPesquisa.Cidade:
                    sqlWhere.AppendFormat("AND CTRL_Enderecos.Municipio Like '{0}%'\r\n", Valor);
                    break;
            }
            sqlWhere.AppendLine("GROUP BY dbo.CTRL_Entidades.IdEntidade, CTRL_Entidades.NomePrimary, dbo.CTRL_Enderecos.Logradouro");
            sqlWhere.AppendLine(", CTRL_Enderecos.Municipio, dbo.CTRL_Enderecos.UF");
            return sqlWhere;
        }

        private void PreencheDependencias(entCTRL_Entidades mEntidade, Tipos.TipoEntidades pTipoEntidade)
        {
            try
            {
                DataTable mDT = this.DbDirect.CriarDataTable(string.Format("Select * from CTRL_Enderecos where idEntidade = {0}", mEntidade.IdEntidade));
                foreach (DataRow mDR in mDT.Rows)
                {
                    Entidades.entCTRL_Enderecos mEndereco = new Entidades.entCTRL_Enderecos();
                    mEndereco.Preencher(mDR);
                    mEntidade.Enderecos.Add(mEndereco.UniqueID, mEndereco);
                }

                mDT = this.DbDirect.CriarDataTable(string.Format("Select * from CTRL_Fones where idEntidade = {0}", mEntidade.IdEntidade));
                foreach (DataRow mDR in mDT.Rows)
                {
                    Entidades.entCTRL_Fones mTelefone = new Entidades.entCTRL_Fones();
                    mTelefone.Preencher(mDR);
                    mEntidade.Telefones.Add(mTelefone.UniqueID, mTelefone);
                }

                mDT = this.DbDirect.CriarDataTable(string.Format("Select * from CTRL_Email where idEntidade = {0}", mEntidade.IdEntidade));
                foreach (DataRow mDR in mDT.Rows)
                {
                    Entidades.entCTRL_Email mEmail = new Entidades.entCTRL_Email();
                    mEmail.Preencher(mDR);
                    mEntidade.Emails.Add(mEmail.UniqueID, mEmail);
                }

                mDT = this.DbDirect.CriarDataTable(string.Format("Select * from CTRL_URL where idEntidade = {0}", mEntidade.IdEntidade));
                foreach (DataRow mDR in mDT.Rows)
                {
                    Entidades.entCTRL_Url mSite = new Entidades.entCTRL_Url();
                    mSite.Preencher(mDR);
                    mEntidade.Sites.Add(mSite.UniqueID, mSite);
                }
                if (pTipoEntidade == NBCobranca.Tipos.TipoEntidades.Devedores)
                {
                    List<entCOBR_Divida> mDividas = this.Sistema.busDividas.Load(mEntidade.IdEntidade, false, false);
                    if (mDividas != null)
                    {
                        foreach (Entidades.entCOBR_Divida mDivida in mDividas)
                            mEntidade.Dividas.Add(mDivida.UniqueID, mDivida);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível carregar as dependencias da entidade.", ex);
            }
        }
        #endregion

    }
}
