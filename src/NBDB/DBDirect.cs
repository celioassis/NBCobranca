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
        /// Recebe uma string contendo o nome de uma chave no arquivo de configuração 
        /// da Aplicação que ira retornar uma string de conexão específica.
        /// </summary>
        /// <param name="pKeyConnectionString">
        /// Nome da Chave que esta no arquivo de configuração da Aplicação que contenha a
        /// string de conexão.
        /// </param>
        public DBDirect(string pKeyConnectionString)
        {
            this.aKeyConnectionString = pKeyConnectionString;
        }
        
        /// <summary>
        /// Executa uma Instrução SQL e retorna o número de linhas afetadas pela Instrução.
        /// </summary>
        /// <param name="pSql">
        /// Instrução SQL para ser Executada.
        /// </param>
        /// <returns>
        /// Retorna um Inteiro com o Número de Linhas Afetadas pela Instrução SQL.
        /// </returns>
        public int Execute_NonQuery(string pSQL)
        {
            return this.DB.Execute_NonQuery(pSQL);
        }

        /// <summary>
        /// Executa uma Instrução SQL e retorna o número de linhas afetadas pela Instrução.
        /// </summary>
        /// <param name="pCommand">Objeto SQLCommand com a instrução SQL que será executada.</param>
        /// <returns>
        /// Retorna um Inteiro com o Número de Linhas Afetadas pela Instrução SQL.
        /// </returns>
        public int Execute_NonQuery(IDbCommand pCommand)
        {
            return aDB.Execute_NonQuery(pCommand);
        }

        /// <summary>
        /// Executa uma Instrução SQL e Retorna o valor do Primeiro field de um comando 
        /// Select.
        /// </summary>
        /// <param name="pSQL">Instrução SQL que será executada.</param>
        /// <returns>Retorna o valor do primeiro Field de um comando Select</returns>
        public object Execute_Scalar(string pSql)
        {
            return this.DB.Execute_Scalar(pSql);
        }

        /// <summary>
        /// Cria um objeto DataTable através da string SQL passada por parametro.
        /// </summary>
        /// <param name="sql">string SQL que será usada para gerar o DataTable.</param>
        /// <returns>Objeto System.Data.DataTable</returns>
        public DataTable CriarDataTable(string sSql)
        {
            return this.DB.CriarDataTable(sSql);
        }
        
        /// <summary>
        /// Cria um objeto DataSet através da string SQL passada por parametro.
        /// </summary>
        /// <param name="pSql">string SQL que será usada para gerar o DataSet.</param>
        /// <returns>Objeto System.Data.DataSet</returns>
        public DataSet CriarDataSet(string pSql)
        {
            return this.DB.CriarDataSet(pSql);
        }
        
        /// <summary>
        /// Cria um objeto DataSet através do objeto do tipo IDBCommand passado por parametro.
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
        /// retornados pela instrução SQL.
        /// </summary>
        /// <param name="pSql">
        /// Instrução SQL para a busca de dados.
        /// </param>
        /// <returns>
        /// Retorna um Objeto que Implemente a Interface IDataReader.
        /// </returns>
        public IDataReader ExecuteDataReader(string pSql)
        {
            return this.DB.ExecuteDataReader(pSql);
        }
        
        /// <summary>
        /// Verifica se foi Executado um DataReader e fecha a conexão com o Banco.
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
        /// Abre uma conexão com o Banco que irá ficar aberta até que seja executado o comando FechaConexao.
        /// </summary>
        public void VerificaAbreConexao(bool pManterConexaoAberta)
        {
            this.DB.VerificaAbreConexao(pManterConexaoAberta);
        }
        
        /// <summary>
        /// Força fechar uma conexão
        /// </summary>
        public void VerificaFechaConexao(bool pForcarFechamento)
        {
            this.DB.VerificaFechaConexao(pForcarFechamento);
        }

        /// <summary>
        /// Retorna uma Instância do SQLCommand já associado a uma SQLConnection.
        /// </summary>
        public IDbCommand GetSqlCommand(string pSql)
        {
            return aDB.GetSqlCommand(pSql);
        }

        /// <summary>
        /// Cria uma instancia de um DataParameter
        /// </summary>
        /// <param name="pNomeParametro">Nome do parametro definido na instrução SQL</param>
        /// <param name="Valor">Valor que o parametro irá receber</param>
        /// <returns></returns>
        public IDataParameter GetParameterCommand(string pNomeParametro, object pValor)
        {
            return aDB.GetParameterCommand(pNomeParametro, pValor);
        }
        
        /// <summary>
        /// Verifica se o Objeto de Acesso a banco de dados esta instanciado e o retorna
        /// caso não esteja será verificado qual o tipo de Conexão e criado a instancia.
        /// </summary>
        private IEdmDB DB
        {
            get
            {
                try
                {
                    //SE JÁ FOI INICIALIZADO O OBJETO DE BD, SAI FORA
                    if (aDB != null) return aDB;

                    //SENÃO VERIFICA QUAL O TIPO DE OBJETO UTILIZAR
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
                    throw new NBUtil.NBException("Não foi Possível Instanciar o Objeto de Acesso a Banco de Dados", "EDM.DB.DBDirect.DB", erro);
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
