using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Neobridge.NBDB
{

    public class DBSqlServer : IEdmDB
    {

        #region Variaveis
        /// <summary>
        /// Varivel contendo a linha de conexao do Sql-Server
        /// </summary>
        string aStringConn;
        /// <summary>
        /// Objeto responsabel pela conexao com o bando de dados
        /// </summary>
        System.Data.SqlClient.SqlConnection aConn;
        /// <summary>
        /// objeto responsavel pelo controle de transacao
        /// </summary>
        System.Data.SqlClient.SqlTransaction aTrans;
        /// <summary>
        /// Variavel para identicar se a conexao "aConn" deve ser fechado apos a execucao do comando
        /// Quando estiver em transacao a conexao tem que ficar ativa, por isso o controle.
        /// </summary>
        private bool aFecharConexao;
        /// <summary>
        /// Vari�vel que cont�m uma mensagem padr�o de erro.
        /// </summary>
        private string aMsgErroPadrao = "Problemas na Execu��o do Metodo, Consulte Suporte T�cnico";
        /// <summary>
        /// Flag que indica se foi executado um DataReader.
        /// </summary>
        private bool aExecutouDataReader = false;
        #endregion

        /// <summary>
        /// Recebe uma string contendo o nome de uma chave no arquivo de configura��o 
        /// da Aplica��o que ira retornar uma string de conex�o espec�fica.
        /// </summary>
        /// <param name="pKeyConnectionString">
        /// Nome da Chave que esta no arquivo de configura��o da Aplica��o que contenha a
        /// string de conex�o.
        /// </param>
        public DBSqlServer(string pKeyConnectionString)
        {
            this.aFecharConexao = false;
            try
            {
                aStringConn = @"" + System.Configuration.ConfigurationManager.AppSettings[pKeyConnectionString];
            }
            catch (Exception ex)
            {
                throw new NBUtil.NBException("N�o foi poss�vel encontrar a string de conex�o no arquivo de Configura��o", this.GetType().FullName, ex);
            }
        }
        /// <summary>
        /// Abre conexao com o banco e abre uma transacao.
        /// </summary>
        public void Transaction_Begin()
        {
            try
            {
                if (aTrans == null && (aConn == null || aConn.State == ConnectionState.Closed))
                {
                    if (aConn != null)
                    {
                        aConn.Dispose();
                        aConn = null;
                    }
                    aConn = new System.Data.SqlClient.SqlConnection(aStringConn);
                    aConn.Open();
                    aTrans = aConn.BeginTransaction();
                    aFecharConexao = false;
                }
            }
            catch (Exception ex)
            {
                throw new NBUtil.NBException(this.aMsgErroPadrao, this.GetType().FullName + ".Transaction_Begin", ex);
            }
        }
        /// <summary>
        /// Commit na transacao em aberto e fecha conexa com o banco
        /// </summary>
        public void Transaction_Commit()
        {
            try
            {
                if (aTrans == null) return;
                aTrans.Commit();
                aConn.Close();
                aFecharConexao = true;
                aTrans.Dispose(); //= null;
                aTrans = null;
            }
            catch (Exception erro)
            {
                throw new NBUtil.NBException(this.aMsgErroPadrao, this.GetType().FullName + ".Transaction_Commit", erro);
            }
        }
        /// <summary>
        /// Cancela transacao e fecha conexao com o banco
        /// </summary>
        public void Transaction_Cancel()
        {
            try
            {
                if (aTrans == null) return;
                aTrans.Rollback();
                aConn.Close();
                aTrans.Dispose(); //= null;
                aTrans = null;
                aFecharConexao = true;
            }
            catch (Exception erro)
            {
                throw new NBUtil.NBException(this.aMsgErroPadrao, this.GetType().FullName + ".Transaction_Cancel", erro);
            }
        }
        /// <summary>
        /// Cria um objeto DataTable atrav�s da string SQL passada por parametro.
        /// </summary>
        /// <param name="sql">string SQL que ser� usada para gerar o DataTable.</param>
        /// <returns>Objeto System.Data.DataTable</returns>
        public System.Data.DataTable CriarDataTable(string pSql)
        {
            try
            {
                //Verific se a conexao esta aberta
                VerificaAbreConexao();

                SqlDataAdapter mDap = new SqlDataAdapter(pSql, aConn);
                if (aTrans != null)
                    mDap.SelectCommand.Transaction = aTrans;

                //Alterado Jean 25/04/2006
                //oDap.SelectCommand.ExecuteNonQuery();
                DataTable oDt = new DataTable();
                mDap.SelectCommand.CommandTimeout = 120;
                mDap.Fill(oDt);
                VerificaFechaConexao();
                return oDt;
            }
            catch (Exception ex)
            {
                throw new NBUtil.NBException("Erro na Cria��o de DataTable", this.GetType().FullName + ".CriarDataTable(string pSql)", ex, pSql);
            }
        }
        /// <summary>
        /// Cria um objeto DataSet atrav�s da string SQL passada por parametro.
        /// </summary>
        /// <param name="pSql">string SQL que ser� usada para gerar o DataSet.</param>
        /// <returns>Objeto System.Data.DataSet</returns>
        public DataSet CriarDataSet(string pSql)
        {
            try
            {
                VerificaAbreConexao();
                //Cria-se um novo DataSet para receber os dados da pesquisa.
                System.Data.DataSet mDS = new System.Data.DataSet();
                //Cria-se um DataAdapter para receber o comando sql que ira fazer a consulta.
                System.Data.SqlClient.SqlDataAdapter mDA;
                //Instancia-se o DataAdapter com o ComandoSQL e a conex�o com o Banco de Dados.
                //mDA = new System.Data.SqlClient.SqlDataAdapter(pSql, mConn);
                mDA = new System.Data.SqlClient.SqlDataAdapter(pSql, aConn);
                if (aTrans != null)
                    mDA.SelectCommand.Transaction = aTrans;
                //Preenche o DataSet.
                mDA.Fill(mDS);

                VerificaFechaConexao();
                return mDS;
            }
            catch (Exception ex)
            {
                throw new NBUtil.NBException("Problemas na Cria��o do DataSet", this.GetType().FullName + ".CriarDataSet(string pSql)", ex, pSql);
            }
        }
        /// <summary>
        /// Cria um objeto DataSet atrav�s do objeto do tipo IDBCommand passado por parametro.
        /// </summary>
        /// <param name="pCommand">
        /// Objeto do tipo IDbCommand preenchido com a string SQL e seus parametro
        /// </param>
        /// <returns>Objeto System.Data.DataSet</returns>
        public DataSet CriarDataSet(System.Data.IDbCommand pCommand)
        {
            try
            {
                VerificaAbreConexao();
                //Cria-se um novo DataSet para receber os dados da pesquisa.
                System.Data.DataSet mDS = new System.Data.DataSet();
                //Cria-se um DataAdapter para receber o comando sql que ira fazer a consulta.
                System.Data.SqlClient.SqlDataAdapter mDA;
                //Atribui a Conex�o Atual ao SqlCommand.
                pCommand.Connection = this.aConn;
                //Instancia-se o DataAdapter com o ComandoSQL.
                mDA = new System.Data.SqlClient.SqlDataAdapter((System.Data.SqlClient.SqlCommand)pCommand);
                
                if (aTrans != null)
                    mDA.SelectCommand.Transaction = aTrans;

                //Preenche o DataSet.
                mDA.Fill(mDS);

                VerificaFechaConexao();
                return mDS;
            }
            catch (Exception ex)
            {
                throw new NBUtil.NBException("Problemas na Cria��o do DataSet", this.GetType().FullName + ".CriarDataSet(System.Data.IDbCommand pCommand)", ex, pCommand.CommandText);
            }
        }
        /// <summary>
        /// Executa uma string SQL e retorna um DataReader para a leitura dos Dados
        /// retornados pela instru��o SQL.
        /// </summary>
        /// <param name="pSql">
        /// Instru��o SQL para a busca de dados.
        /// </param>
        /// <returns>
        /// Retorna um Objeto que Implemente a Interface IDataReader.
        /// </returns>
        public System.Data.IDataReader ExecuteDataReader(string pSql)
        {
            try
            {
                VerificaAbreConexao();
                SqlCommand mCmd = new SqlCommand(pSql, aConn);
                SqlDataReader mReader = mCmd.ExecuteReader();
                return mReader;
            }
            catch (Exception erro)
            {
                throw new NBUtil.NBException(this.aMsgErroPadrao, this.GetType().FullName + ".ExecuteDataReader(string pSql)", erro, pSql);
            }
        }
        /// <summary>
        /// Verifica se foi Executado um DataReader e fecha a conex�o com o Banco.
        /// </summary>
        public void FechaConexaoPosDataReader()
        {
            if (this.aExecutouDataReader)
                VerificaFechaConexao();
            this.aExecutouDataReader = false;
        }
        /// <summary>
        /// Executa uma Instru��o SQL e retorna o n�mero de linhas afetadas pela Instru��o.
        /// </summary>
        /// <param name="pSql">
        /// Instru��o SQL para ser Executada.
        /// </param>
        /// <returns>
        /// Retorna um Inteiro com o N�mero de Linhas Afetadas pela Instru��o SQL.
        /// </returns>
        public int Execute_NonQuery(string pSql)
        {
            return Execute_NonQuery(GetSqlCommand(pSql));
        }
        /// <summary>
        /// Executa uma Instru��o SQL e retorna o n�mero de linhas afetadas pela Instru��o.
        /// </summary>
        /// <param name="pCommand">Objeto SQLCommand com a instru��o SQL que ser� executada.</param>
        /// <returns>
        /// Retorna um Inteiro com o N�mero de Linhas Afetadas pela Instru��o SQL.
        /// </returns>
        public int Execute_NonQuery(System.Data.IDbCommand pCommand)
        {
            int retorno = 0;
            try
            {
                VerificaAbreConexao();
                SqlDataAdapter oDap = new SqlDataAdapter((System.Data.SqlClient.SqlCommand)pCommand);

                if (aTrans != null)
                    oDap.SelectCommand.Transaction = aTrans;
                retorno = oDap.SelectCommand.ExecuteNonQuery();
                VerificaFechaConexao();
                SqlParameter mpar = new SqlParameter("", 22);
                return retorno;
            }
            catch (Exception ex)
            {
                throw new NBUtil.NBException(this.aMsgErroPadrao, this.GetType().FullName + ".Execute_NonQuery(string pSql)", ex, pCommand.CommandText);
            }
        }
        
        /// <summary>
        /// Retorna uma Inst�ncia do SQLCommand j� associado a uma SQLConnection.
        /// </summary>
        public IDbCommand GetSqlCommand(string pSql)
        {
            return new SqlCommand(pSql, aConn);
        }
        /// <summary>
        /// Cria uma instancia de um DataParameter
        /// </summary>
        /// <param name="pNomeParametro">Nome do parametro definido na instru��o SQL</param>
        /// <param name="Valor">Valor que o parametro ir� receber</param>
        /// <returns></returns>
        public IDataParameter GetParameterCommand(string pNomeParametro, object pValor)
        {
            return new SqlParameter(pNomeParametro, pValor);
        }

        /// <summary>
        /// Executa uma Instru��o SQL e Retorna o valor do Primeiro field de um comando 
        /// Select.
        /// </summary>
        /// <param name="pSQL">Instru��o SQL que ser� executada.</param>
        /// <returns>Retorna o valor do primeiro Field de um comando Select</returns>
        public object Execute_Scalar(string pSQL)
        {
            object tmp = null;
            string tmpSQL = pSQL;
            if (tmpSQL.ToUpper().Contains("INSERT"))
                pSQL += " SELECT @@IDENTITY SET NOCOUNT OFF";

            try
            {
                VerificaAbreConexao();
                SqlDataAdapter aDap = new SqlDataAdapter(pSQL, aConn);
                if (aTrans != null)
                    aDap.SelectCommand.Transaction = aTrans;
                tmp = aDap.SelectCommand.ExecuteScalar();
                VerificaFechaConexao();
                return tmp;
            }
            catch (Exception ex)
            {
                throw new NBUtil.NBException(this.aMsgErroPadrao, this.GetType().FullName + ".Execute_Scalar(string pSQL)", ex, pSQL);
            }
        }
        /// <summary>
        /// Tem a funcao de verifica se a funcao esta aberta. Caso esteja aberta � por que 
        /// existe uma transacao e por isso nao seta a variavel mFechar para true. 
        /// Para ter controle de transacao a conexao com o banco precisa ficar ativa.
        /// <para>
        /// Quando existe transa��o, quem abre e fecha a conex�o � a fun��o
        /// Transaction_Begin e Transaction_Commit
        /// </para>
        /// </summary>
        private void VerificaAbreConexao()
        {
            VerificaAbreConexao(false);
        }
        /// <summary>
        /// Tem a funcao de verifica se a funcao esta aberta. Caso esteja aberta � por que 
        /// existe uma transacao e por isso nao seta a variavel mFechar para true. 
        /// Para ter controle de transacao a conexao com o banco precisa ficar ativa.
        /// <para>
        /// Quando existe transa��o, quem abre e fecha a conex�o � a fun��o
        /// Transaction_Begin e Transaction_Commit
        /// </para>
        /// <param name="pManterConexaoAberta">Indica se a conex�o dever� permanecer aberta, at� que seja fechada manualmente.</param>
        /// </summary>
        public void VerificaAbreConexao(bool pManterConexaoAberta)
        {
            if (aConn == null)
            {
                aConn = new System.Data.SqlClient.SqlConnection(aStringConn);
                aConn.Open();
                aFecharConexao = !pManterConexaoAberta;
            }
            else
            {
                if (aConn.State == ConnectionState.Closed)
                {
                    aConn.Open();
                    aFecharConexao = !pManterConexaoAberta;
                }
            }

        }
        /// <summary>
        /// Tem a funcao de verifica se a funcao esta aberta. Caso a conexao for aberta por uma transacao o conexao nao pode ser fechada ,
        /// e por isso existe a variaval aFecharConexao como FALSE
        /// Para ter controle de transacao a conexao com o banco precisa ficar ativa.
        /// Obs. 
        /// <para>
        /// Quando existe transa��o, quem abre e fecha a conex�o � a fun��o
        /// Transaction_Begin e Transaction_Commit
        /// </para>
        /// </summary>
        private void VerificaFechaConexao()
        {
            VerificaFechaConexao(false);
        }
        public void VerificaFechaConexao(bool pForcarFechamento)
        {
            if ((aConn.State == ConnectionState.Open) && (aFecharConexao || pForcarFechamento))
            {
                aConn.Close();
                aFecharConexao = false;
                if (aTrans != null)
                    aTrans.Dispose();  // = null;
                aTrans = null;
            }
        }
        #region IDisposable Members

        public void Dispose()
        {
            if (this.aConn != null)
                this.aConn.Dispose();
            this.aConn = null;

            if (this.aTrans != null)
                this.aTrans.Dispose();
            this.aTrans = null;
        }

        #endregion

    }
}
