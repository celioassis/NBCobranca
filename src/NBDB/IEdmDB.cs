using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace Neobridge.NBDB
{
    public interface IEdmDB: IDisposable
    {
        /// <summary>
        /// Executa uma Instrução SQL e retorna o número de linhas afetadas pela Instrução.
        /// </summary>
        /// <param name="pSql">
        /// Instrução SQL para ser Executada.
        /// </param>
        /// <returns>
        /// Retorna um Inteiro com o Número de Linhas Afetadas pela Instrução SQL.
        /// </returns>
        int Execute_NonQuery(string pSQL);

        /// <summary>
        /// Executa uma Instrução SQL e retorna o número de linhas afetadas pela Instrução.
        /// </summary>
        /// <param name="pCommand">Objeto SQLCommand com a instrução SQL que será executada.</param>
        /// <returns>
        /// Retorna um Inteiro com o Número de Linhas Afetadas pela Instrução SQL.
        /// </returns>
        int Execute_NonQuery(System.Data.IDbCommand pCommand);

        /// <summary>
        /// Executa uma Instrução SQL e Retorna o valor do Primeiro field de um comando 
        /// Select.
        /// </summary>
        /// <param name="pSQL">Instrução SQL que será executada.</param>
        /// <returns>Retorna o valor do primeiro Field de um comando Select</returns>
        object Execute_Scalar(string pSql);
        
        /// <summary>
        /// Cria um objeto DataTable através da string SQL passada por parametro.
        /// </summary>
        /// <param name="sql">string SQL que será usada para gerar o DataTable.</param>
        /// <returns>Objeto System.Data.DataTable</returns>
        System.Data.DataTable CriarDataTable(string sSql);
        
        /// <summary>
        /// Cria um objeto DataSet através da string SQL passada por parametro.
        /// </summary>
        /// <param name="pSql">string SQL que será usada para gerar o DataSet.</param>
        /// <returns>Objeto System.Data.DataSet</returns>
        System.Data.DataSet CriarDataSet(string pSql);
        
        /// <summary>
        /// Cria um objeto DataSet através do objeto do tipo IDBCommand passado por parametro.
        /// </summary>
        /// <param name="pCommand">
        /// Objeto do tipo IDbCommand preenchido com a string SQL e seus parametro
        /// </param>
        /// <returns>Objeto System.Data.DataSet</returns>
        System.Data.DataSet CriarDataSet(System.Data.IDbCommand pCommand);
        
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
        System.Data.IDataReader ExecuteDataReader(string pSql);
        
        /// <summary>
        /// Verifica se foi Executado um DataReader e fecha a conexão com o Banco.
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
        /// Abre uma conexão com o Banco que irá ficar aberta até que seja executado o comando FechaConexao.
        /// </summary>
        void VerificaAbreConexao(bool pManterConexaoAberta);
        
        /// <summary>
        /// Força fechar uma conexão
        /// </summary>
        void VerificaFechaConexao(bool pForcarFechamento);
        
        /// <summary>
        /// Retorna uma Instância do SQLCommand já associado a uma SQLConnection.
        /// </summary>
        IDbCommand GetSqlCommand(string pSql);
        
        /// <summary>
        /// Cria uma instancia de um DataParameter
        /// </summary>
        /// <param name="pNomeParametro">Nome do parametro definido na instrução SQL</param>
        /// <param name="Valor">Valor que o parametro irá receber</param>
        /// <returns></returns>
        IDataParameter GetParameterCommand(string pNomeParametro, object pValor);
       
    }
}
