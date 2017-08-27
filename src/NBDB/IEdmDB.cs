using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Neobridge.NBDB
{
    public interface IEdmDB: IDisposable
    {
        /// <summary>
        /// Executa uma Instru��o SQL e retorna o n�mero de linhas afetadas pela Instru��o.
        /// </summary>
        /// <param name="pSql">
        /// Instru��o SQL para ser Executada.
        /// </param>
        /// <returns>
        /// Retorna um Inteiro com o N�mero de Linhas Afetadas pela Instru��o SQL.
        /// </returns>
        int Execute_NonQuery(string pSQL);

        /// <summary>
        /// Executa uma Instru��o SQL e retorna o n�mero de linhas afetadas pela Instru��o.
        /// </summary>
        /// <param name="pCommand">Objeto SQLCommand com a instru��o SQL que ser� executada.</param>
        /// <returns>
        /// Retorna um Inteiro com o N�mero de Linhas Afetadas pela Instru��o SQL.
        /// </returns>
        int Execute_NonQuery(System.Data.IDbCommand pCommand);

        /// <summary>
        /// Executa uma Instru��o SQL e Retorna o valor do Primeiro field de um comando 
        /// Select.
        /// </summary>
        /// <param name="pSQL">Instru��o SQL que ser� executada.</param>
        /// <returns>Retorna o valor do primeiro Field de um comando Select</returns>
        object Execute_Scalar(string pSql);
        
        /// <summary>
        /// Cria um objeto DataTable atrav�s da string SQL passada por parametro.
        /// </summary>
        /// <param name="sql">string SQL que ser� usada para gerar o DataTable.</param>
        /// <returns>Objeto System.Data.DataTable</returns>
        System.Data.DataTable CriarDataTable(string sSql);
        
        /// <summary>
        /// Cria um objeto DataSet atrav�s da string SQL passada por parametro.
        /// </summary>
        /// <param name="pSql">string SQL que ser� usada para gerar o DataSet.</param>
        /// <returns>Objeto System.Data.DataSet</returns>
        System.Data.DataSet CriarDataSet(string pSql);
        
        /// <summary>
        /// Cria um objeto DataSet atrav�s do objeto do tipo IDBCommand passado por parametro.
        /// </summary>
        /// <param name="pCommand">
        /// Objeto do tipo IDbCommand preenchido com a string SQL e seus parametro
        /// </param>
        /// <returns>Objeto System.Data.DataSet</returns>
        System.Data.DataSet CriarDataSet(System.Data.IDbCommand pCommand);
        
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
        System.Data.IDataReader ExecuteDataReader(string pSql);
        
        /// <summary>
        /// Verifica se foi Executado um DataReader e fecha a conex�o com o Banco.
        /// </summary>
        void FechaConexaoPosDataReader();
        
        /// <summary>
        /// Abre conexao com o banco e abre uma transacao.
        /// </summary>
        void Transaction_Begin();
        
        /// <summary>
        /// Commit na transacao em aberto e fecha conexa com o banco
        /// </summary>
        void Transaction_Commit();
        
        /// <summary>
        /// Cancela transacao e fecha conexao com o banco
        /// </summary>
        void Transaction_Cancel();
        
        /// <summary>
        /// Abre uma conex�o com o Banco que ir� ficar aberta at� que seja executado o comando FechaConexao.
        /// </summary>
        void VerificaAbreConexao(bool pManterConexaoAberta);
        
        /// <summary>
        /// For�a fechar uma conex�o
        /// </summary>
        void VerificaFechaConexao(bool pForcarFechamento);
        
        /// <summary>
        /// Retorna uma Inst�ncia do SQLCommand j� associado a uma SQLConnection.
        /// </summary>
        IDbCommand GetSqlCommand(string pSql);
        
        /// <summary>
        /// Cria uma instancia de um DataParameter
        /// </summary>
        /// <param name="pNomeParametro">Nome do parametro definido na instru��o SQL</param>
        /// <param name="Valor">Valor que o parametro ir� receber</param>
        /// <returns></returns>
        IDataParameter GetParameterCommand(string pNomeParametro, object pValor);
       
    }
}
