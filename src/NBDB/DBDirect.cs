using System;
using System.Data;
using System.Configuration;

namespace Neobridge.NBDB
{
    public class DBDirect : IEdmDB
    {
        private string aKeyConnectionString = "";
        private IEdmDB aDB;

        /// <summary>
        /// Recebe uma string contendo o nome de uma chave no arquivo de configura��o 
        /// da Aplica��o que ira retornar uma string de conex�o espec�fica.
        /// </summary>
        /// <param name="pKeyConnectionString">
        /// Nome da Chave que esta no arquivo de configura��o da Aplica��o que contenha a
        /// string de conex�o.
        /// </param>
        public DBDirect(string pKeyConnectionString)
        {
            this.aKeyConnectionString = pKeyConnectionString;
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
        public int Execute_NonQuery(string pSQL)
        {
            return this.DB.Execute_NonQuery(pSQL);
        }

        /// <summary>
        /// Executa uma Instru��o SQL e retorna o n�mero de linhas afetadas pela Instru��o.
        /// </summary>
        /// <param name="pCommand">Objeto SQLCommand com a instru��o SQL que ser� executada.</param>
        /// <returns>
        /// Retorna um Inteiro com o N�mero de Linhas Afetadas pela Instru��o SQL.
        /// </returns>
        public int Execute_NonQuery(IDbCommand pCommand)
        {
            return aDB.Execute_NonQuery(pCommand);
        }

        /// <summary>
        /// Executa uma Instru��o SQL e Retorna o valor do Primeiro field de um comando 
        /// Select.
        /// </summary>
        /// <param name="pSQL">Instru��o SQL que ser� executada.</param>
        /// <returns>Retorna o valor do primeiro Field de um comando Select</returns>
        public object Execute_Scalar(string pSql)
        {
            return this.DB.Execute_Scalar(pSql);
        }

        /// <summary>
        /// Cria um objeto DataTable atrav�s da string SQL passada por parametro.
        /// </summary>
        /// <param name="sql">string SQL que ser� usada para gerar o DataTable.</param>
        /// <returns>Objeto System.Data.DataTable</returns>
        public DataTable CriarDataTable(string sSql)
        {
            return this.DB.CriarDataTable(sSql);
        }
        
        /// <summary>
        /// Cria um objeto DataSet atrav�s da string SQL passada por parametro.
        /// </summary>
        /// <param name="pSql">string SQL que ser� usada para gerar o DataSet.</param>
        /// <returns>Objeto System.Data.DataSet</returns>
        public DataSet CriarDataSet(string pSql)
        {
            return this.DB.CriarDataSet(pSql);
        }
        
        /// <summary>
        /// Cria um objeto DataSet atrav�s do objeto do tipo IDBCommand passado por parametro.
        /// </summary>
        /// <param name="pCommand">
        /// Objeto do tipo IDbCommand preenchido com a string SQL e seus parametro
        /// </param>
        /// <returns>Objeto System.Data.DataSet</returns>
        public DataSet CriarDataSet(System.Data.IDbCommand pSqlCmd)
        {
            return this.DB.CriarDataSet(pSqlCmd);
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
        public IDataReader ExecuteDataReader(string pSql)
        {
            return this.DB.ExecuteDataReader(pSql);
        }
        
        /// <summary>
        /// Verifica se foi Executado um DataReader e fecha a conex�o com o Banco.
        /// </summary>
        public void FechaConexaoPosDataReader()
        {
            this.DB.FechaConexaoPosDataReader();
        }
        
        /// <summary>
        /// Abre conexao com o banco e abre uma transacao.
        /// </summary>
        public void Transaction_Begin()
        {
            this.DB.Transaction_Begin();
        }
        
        /// <summary>
        /// Commit na transacao em aberto e fecha conexa com o banco
        /// </summary>
        public void Transaction_Commit()
        {
            this.DB.Transaction_Commit();
        }
        
        /// <summary>
        /// Cancela transacao e fecha conexao com o banco
        /// </summary>
        public void Transaction_Cancel()
        {
            this.DB.Transaction_Cancel();
        }
        
        /// <summary>
        /// Abre uma conex�o com o Banco que ir� ficar aberta at� que seja executado o comando FechaConexao.
        /// </summary>
        public void VerificaAbreConexao(bool pManterConexaoAberta)
        {
            this.DB.VerificaAbreConexao(pManterConexaoAberta);
        }
        
        /// <summary>
        /// For�a fechar uma conex�o
        /// </summary>
        public void VerificaFechaConexao(bool pForcarFechamento)
        {
            this.DB.VerificaFechaConexao(pForcarFechamento);
        }

        /// <summary>
        /// Retorna uma Inst�ncia do SQLCommand j� associado a uma SQLConnection.
        /// </summary>
        public IDbCommand GetSqlCommand(string pSql)
        {
            return aDB.GetSqlCommand(pSql);
        }

        /// <summary>
        /// Cria uma instancia de um DataParameter
        /// </summary>
        /// <param name="pNomeParametro">Nome do parametro definido na instru��o SQL</param>
        /// <param name="Valor">Valor que o parametro ir� receber</param>
        /// <returns></returns>
        public IDataParameter GetParameterCommand(string pNomeParametro, object pValor)
        {
            return aDB.GetParameterCommand(pNomeParametro, pValor);
        }
        
        /// <summary>
        /// Verifica se o Objeto de Acesso a banco de dados esta instanciado e o retorna
        /// caso n�o esteja ser� verificado qual o tipo de Conex�o e criado a instancia.
        /// </summary>
        private IEdmDB DB
        {
            get
            {
                try
                {
                    //SE J� FOI INICIALIZADO O OBJETO DE BD, SAI FORA
                    if (aDB != null) return aDB;

                    //SEN�O VERIFICA QUAL O TIPO DE OBJETO UTILIZAR
                    string Tipo_Conexao = System.Configuration.ConfigurationManager.AppSettings["TipoBanco"];
                    switch (Tipo_Conexao.ToUpper())
                    {
                        case "SQLSERVER":
                            aDB = new DBSqlServer(this.aKeyConnectionString);
                            break;
                        default:
                            aDB = new DBOleDB(this.aKeyConnectionString);
                            break;
                    }
                    return aDB;
                }
                catch (Exception erro)
                {
                    throw new NBUtil.NBException("N�o foi Poss�vel Instanciar o Objeto de Acesso a Banco de Dados", "EDM.DB.DBDirect.DB", erro);
                }
            }

        }

        #region IDisposable Members

        public void Dispose()
        {
            if (this.aDB != null)
                this.aDB.Dispose();
            this.aDB = null;
        }

        #endregion

    }
}
