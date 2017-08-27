using System;
using System.Data;
using System.Configuration;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using NBCobranca.Classes;

namespace NBCobranca.Entidades
{
    public abstract class entBase
    {
        protected List<string> _NomeCamposChave = new List<string>();
        protected List<string> _NomeCamposTabela = new List<string>();
        protected List<string> _CamposAlterados = new List<string>();
        private long _UniqueID;

        public entBase()
        {
            System.Threading.Thread.Sleep(20);
            _UniqueID = DateTime.Now.Ticks;
        }

        /// <summary>
        /// Representa um ID unico para o Objeto.
        /// </summary>
        public long UniqueID
        {
            get { return _UniqueID; }
        }

        public abstract void Preencher(DataRow pDrCtrlEntidade);
        public abstract void Preencher(IDataReader pDrCtrlEntidade);

        public abstract void Clear();

        /// <summary>
        /// Valor utilizado para fazer verifica��es de duplica��o de primary key em cole��es.
        /// </summary>
        public abstract string Key
        { get;}

        /// <summary>
        /// C�digo de Identifica��o da entidade no Banco de dados.
        /// </summary>
        public abstract int ID_BD
        {
            get;
            set;
        }

        /// <summary>
        /// Salva as altera��es ou inclui a entidade no banco de dados
        /// </summary>
        /// <param name="pDB">Objeto que implemente a interface IEdmDB ser� respons�vel por fazer a comunica��o com o banco de dados</param>
        public void Salvar(Neobridge.NBDB.IEdmDB pDB)
        {

            if (ID_BD > 0)
            {
                if (_CamposAlterados.Count > 0)
                    pDB.Execute_NonQuery(SqlUpdate);
            }
            else
                ID_BD = Convert.ToInt32(pDB.Execute_Scalar(SqlInsert));
            _CamposAlterados.Clear();
        }

        /// <summary>
        /// Exclui a entidade do banco de dados
        /// </summary>
        /// <param name="pDB">Objeto que implemente a interface IEdmDB ser� respons�vel por fazer a comunica��o com o banco de dados</param>
        public void Excluir(Neobridge.NBDB.IEdmDB pDB)
        {
            if (ID_BD > 0)
                pDB.Execute_NonQuery(SqlDelete);
        }

        /// <summary>
        /// Monta uma instru��o SQL de select com base na cole��o de Cheve Valor do parametro pChaveValor
        /// </summary>
        /// <param name="pChaveValor">Cole��o contendo os nomes de campos chave e seus respectivos valores ex:
        /// <para>
        /// Representa��o da chave prim�ria da tabela CTRL_Link_Entidade_No, CampoChave: IdEntidade Valor: 1, 
        /// CampoChave: IdNo Valor: 10, a cole�ao ira ficar assim {["idEntidade",1],["IdNo",10]}
        /// </para>
        /// </param>
        /// <returns></returns>
        public virtual string SqlSelect(Dictionary<string, object> pChaveValor)
        {
            string mNomeTabela = GetType().Name.Substring(3);
            StringBuilder mSQL = new StringBuilder();
            mSQL.AppendLine("select * from " + mNomeTabela);
            if (pChaveValor != null)
            {
                mSQL.AppendLine("Where 1=1");
                foreach (string mNomeCampoChave in pChaveValor.Keys)
                {
                    object mValorChave = pChaveValor[mNomeCampoChave];
                    string mFiltroFormat = "and " + mNomeCampoChave + " = {0}\r\n";
                    mSQL.AppendFormat(mFiltroFormat, NBFuncoes.FormatCampoToSQL(mValorChave));
                }
            }
            return mSQL.ToString();
        }

        /// <summary>
        /// Ira verificar se a Key n�o esta duplicada na cole��o indicada por parametro.
        /// </summary>
        /// <typeparam name="T">Tipo da cole��o e entidade que ser� verifica a duplica��o</typeparam>
        /// <param name="pColecao">Cole��o que ser� verifica a duplica��o</param>
        /// <param name="pEntidade">Entidade ser� verifica a duplica��o</param>
        public void VerificaDuplicataKey<T>(Dictionary<long, T> pColecao, T pEntidade)
        {
            foreach (T mEntidade in pColecao.Values)
            {
                if ((mEntidade as entBase).Key == (pEntidade as entBase).Key)
                    throw new ArgumentException();
            }
        }

        /// <summary>
        /// Faz a valida��o de altera��o de um determinado campo, caso o mesmo tenha sido alterado, ser� adicionado 
        /// o nome do campo na lista de campos alterados, para quando for fazer um Update o metodo saiba quais 
        /// campos devam ser atualizados no banco de dados.
        /// </summary>
        /// <typeparam name="T">Qual o tipo do campo que esta sendo validado</typeparam>
        /// <param name="pValorAtual">Qual � o valor atual desse campo</param>
        /// <param name="pNovoValor">Qual � o novo valor para esse campo</param>
        /// <param name="pNomeCampo">Nome do campo como esta no banco de dados</param>
        protected void ValidaAlteracao<T>(ref T pValorAtual, T pNovoValor, string pNomeCampo)
        {
            object mValorAtual = pValorAtual;
            object mNovoValor = pNovoValor;
            if ((mValorAtual == null) || (!mValorAtual.Equals(mNovoValor) && !_CamposAlterados.Contains(pNomeCampo)))
                _CamposAlterados.Add(pNomeCampo);
            pValorAtual = pNovoValor;
        }

        protected PropertyInfo GetPropertyInfo(string nomePropriedade)
        {
            return GetType().GetProperties().FirstOrDefault(prop => prop.Name.ToLowerInvariant().Equals(nomePropriedade.ToLowerInvariant()));
        }

        private string SqlUpdate
        {
            get
            {
                Type mType = GetType();
                StringBuilder mSQL = new StringBuilder();
                mSQL.AppendFormat("UPDATE {0}\r\n", GetType().Name.Replace("ent", ""));
                mSQL.Append("   SET ");
                foreach (string mNomeCampo in _CamposAlterados)
                {
                    PropertyInfo mProperty = mType.GetProperty(mNomeCampo);
                    mSQL.AppendFormat("[{0}] = {1}\r\n,", mNomeCampo, NBFuncoes.FormatCampoToSQL(mProperty.GetValue(this, null)));
                }
                mSQL.Remove(mSQL.Length - 1, 1);
                WhereBuilder(mSQL, mType);
                return mSQL.ToString();
            }
        }

        private string SqlInsert
        {
            get
            {
                Type mType = GetType();
                string mSqlCamposFormat = string.Format("INSERT INTO {0}\r\n", mType.Name.Remove(0, 3)) + "           (\r\n           {0})\r\n";
                string mSqlValuesFormat = "     VALUES(\r\n           {0})\r\n";
                StringBuilder mSQLCampos = new StringBuilder();
                StringBuilder mSQLValues = new StringBuilder();
                StringBuilder mSQL = new StringBuilder();
                foreach (string mNomeCampo in _NomeCamposTabela)
                {
                    if (_NomeCamposChave.Contains(mNomeCampo))
                        continue;
                    
                    PropertyInfo mProperty = mType.GetProperty(mNomeCampo);
                    
                    if (mProperty.GetValue(this, null) == null)
                        continue;
                    
                    mSQLCampos.AppendFormat("           [{0}],\r\n", mNomeCampo);
                    mSQLValues.AppendFormat("           {0},\r\n", NBFuncoes.FormatCampoToSQL(mProperty.GetValue(this, null)));
                }
                mSQL.AppendFormat(mSqlCamposFormat, mSQLCampos.ToString().Substring(0, mSQLCampos.Length - 3));
                mSQL.AppendFormat(mSqlValuesFormat, mSQLValues.ToString().Substring(0, mSQLValues.Length - 3));
                return mSQL.ToString();
            }
        }

        private string SqlDelete
        {
            get
            {
                Type mType = GetType();
                StringBuilder mSQL = new StringBuilder();
                mSQL.AppendFormat("DELETE {0}\r\n", GetType().Name.Replace("ent", ""));
                WhereBuilder(mSQL, mType);
                return mSQL.ToString();
            }
        }

        private void WhereBuilder(StringBuilder pSQL, Type pTypeEntidade)
        {
            pSQL.AppendLine("Where");
            int mSeqChave = 0;
            foreach (string mNomeCampoChave in _NomeCamposChave)
            {
                PropertyInfo mProperty = pTypeEntidade.GetProperty(mNomeCampoChave);
                object mValorChave = mProperty.GetValue(this, null);
                string mFiltroFormat = "and {0} = {1}\r\n";
                if (mSeqChave == 0)
                    mFiltroFormat = "{0} = {1}\r\n";

                pSQL.AppendFormat(mFiltroFormat, mNomeCampoChave, NBFuncoes.FormatCampoToSQL(mValorChave));
                mSeqChave++;
            }
        }
    }
}
