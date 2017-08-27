using System.Data;
using System.Collections;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System;


namespace NBdbm
{
    namespace ADM
    {

        internal class admDB_SQLsvr : Interfaces.iAdmDB
        {
            private System.Data.SqlClient.SqlConnection conn;
            private System.Data.SqlClient.SqlConnection connReader;
            private System.Data.SqlClient.SqlCommand comm;
            private System.Data.SqlClient.SqlTransaction trans;
            private string aConnString;

            public admDB_SQLsvr()
            {
                this.aConnString = self.Settings.stringConnection;
            }
            public void Dispose()
            {
                try
                {

                    if (comm != null)
                    {
                        comm.Dispose();
                        comm = null;
                    }
                    if (trans != null)
                    {
                        trans.Dispose();
                        trans = null;
                    }
                    if (conn != null)
                    {
                        conn.Dispose();
                        conn = null;
                    }
                }
                catch
                {

                }

            }

            public System.Data.IDbConnection Connection
            {
                get
                {
                    return this.connection;
                }
                set
                {
                    this.connection = value;
                }
            }

            public System.Data.IDbConnection connection
            {
                get
                {
                    cnnSQLServer();
                    return this.conn;
                }
                set
                {
                    this.conn = value as System.Data.SqlClient.SqlConnection;
                }
            }

            public System.Data.IDbConnection ConnectionReader
            {
                get
                {
                    return this.connectionReader;
                }
                set
                {
                    this.connectionReader = value;
                }
            }

            public System.Data.IDbConnection connectionReader
            {
                get
                {

                    this.cnnSQLServer();
                    return this.connReader;
                }
                set
                {
                    this.connReader = value as System.Data.SqlClient.SqlConnection;
                }
            }

            //Exemplo de String de Conexão
            //= "Provider=SQLOLEDB.1;Persist Security Info=False;User ID=ProSystem_; Password=nitromate;Initial Catalog=NBCOBBBO; Data Source=LocalHost
            //= "DSN=NBCOBBBO;User ID=ProSystem_; Password=nitromate;SystemDB=\\Stone\X\Bancos de Dados\Edgar\BBO\Logo.bmp
            //= "Provider=SQLOLEDB.1;Persist Security Info=False;User ID=ProSystem_; Password=nitromate;Initial Catalog=NBCOBBBO; Data Source=LocalHost"
            //= "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Dados.mdb"
            //= "data source=Stone;initial catalog=Neobridge;user id=ProSystem_; password=nitromate;"
            //= "Data Source=(local);Integrated Security=yes"
            //= "Network Library=DBMSSOCN; Data Source=127.0.0.1, 1433; Initial Catalog=EDGAR; User ID=ProSystem_; Password=nitromate"
            //= "Network Library=DBMSSOCN; Data Source=127.0.0.1, 1433; Initial Catalog=NEOBRIDGE; User ID=" & self.Settings.UserId & "; Password=" & self.Settings.Password
            private void cnnSQLServer()
            {
                //Dim stringConnection As String
                try
                {

                    if (this.conn == null)
                    {
                        this.conn = new System.Data.SqlClient.SqlConnection();
                        this.conn.ConnectionString = this.aConnString;
                    }

                    if (this.connReader == null)
                    {
                        this.connReader = new System.Data.SqlClient.SqlConnection();
                        this.connReader.ConnectionString = this.aConnString;
                    }

                }
                catch (Exception ex)
                {
                    throw (ex);
                }

            }
            //Cria uma nova Conexão
            public System.Data.IDbConnection NewConnection
            {
                get
                {
                    return new System.Data.SqlClient.SqlConnection(this.ConnString);
                }
            }

            public System.Data.IDbCommand Command(string stringCommand, System.Data.IDbConnection pNewConnection)
            {
                System.Data.SqlClient.SqlCommand mCommand = new System.Data.SqlClient.SqlCommand(stringCommand, (System.Data.SqlClient.SqlConnection)pNewConnection);
                return (IDbCommand)mCommand;
            }

            public System.Data.IDbCommand Command(string stringCommand)
            {
                try
                {
                    //Verificar se é necessário este if logo abaixo e se ele não ocasiona erro
                    bool mNovaConexao = false;
                    System.Data.SqlClient.SqlConnection mConnection = (System.Data.SqlClient.SqlConnection)this.connection;
                    System.Data.SqlClient.SqlCommand mCommand;
                    string mEtapa;

                    mEtapa = "1";
                    mCommand = new System.Data.SqlClient.SqlCommand(stringCommand, mConnection);

                    mEtapa = "2";
                    if (mConnection.ConnectionString != "")
                    {
                        if (mConnection.State == ConnectionState.Closed)
                        {
                            mConnection.Open();
                        }
                    }
                    mEtapa = "3";
                    return mCommand;
                }
                catch (Exception ex)
                {
                    throw (ex);
                }

            }

            public System.Data.IDbCommand Command(string stringCommand, System.Data.IDbTransaction Transaction)
            {
                System.Data.IDbCommand returnValue;
                if (comm == null)
                {
                    comm = new System.Data.SqlClient.SqlCommand(string.Empty, conn, (System.Data.SqlClient.SqlTransaction)Transaction);
                }

                if (comm.Connection.State != ConnectionState.Closed)
                {
                    comm.CommandText = stringCommand;
                    comm.Transaction = Transaction as System.Data.SqlClient.SqlTransaction;
                    returnValue = comm;
                }
                else
                {
                    return null;
                }
                return returnValue;
            }

            //Public ReadOnly Property dataAdapter(ByVal stringCommand As String, ByVal Transaction As System.Data.IDbTransaction) As System.Data.IDbDataAdapter
            //  Get

            //    Return New SqlClient.SqlDataAdapter(Me.Command(stringCommand, Transaction).CommandText, Me.connection)

            //  End Get
            //End Property
            public System.Data.IDbDataAdapter dataAdapter(string stringCommand)
            {
                try
                {
                    System.Data.SqlClient.SqlConnection mConn = new System.Data.SqlClient.SqlConnection(this.connection.ConnectionString);
                    mConn.Open();
                    System.Data.SqlClient.SqlCommand mComm = new System.Data.SqlClient.SqlCommand(stringCommand, mConn);
                    System.Data.SqlClient.SqlDataAdapter mSDA = new System.Data.SqlClient.SqlDataAdapter(mComm);
                    //Return New SqlClient.SqlDataAdapter(Me.Command(stringCommand))
                    return mSDA;
                }
                catch (Exception ex)
                {
                    throw (ex);
                }
            }

            public System.Data.IDbDataParameter dataParameter(string stringCommand)
            {
                return (System.Data.IDbDataParameter)comm.Parameters[stringCommand];
            }

            public string sqlListaTabelas
            {
                get
                {
                    return "Select name from sysobjects where [xtype] = \'U\' order by name";
                }
            }

            public string sqlListaCampos(string tableName)
            {
                //strSQL = "Select [name] from syscolumns where [id] = (Select [id] from sysobjects where [name] = '" & tableName & "')"
                //strSQL = "SELECT syscolumns.colorder, syscolumns.name, systypes.name, syscolumns.length FROM syscolumns INNER JOIN systypes ON syscolumns.xtype = systypes.xtype WHERE (syscolumns.id = (SELECT [id] FROM  sysobjects WHERE [name] = '" & tableName & "')) ORDER BY syscolumns.colorder, syscolumns.name"
                return "SELECT  syscolumns.colorder, syscolumns.name, systypes.name, syscolumns.length FROM syscolumns INNER JOIN systypes ON syscolumns.xtype = systypes.xtype WHERE (syscolumns.id = (SELECT [id] FROM  sysobjects WHERE [name] = \'" + tableName + "\')) ORDER BY syscolumns.colorder, syscolumns.name";
            }

            public System.Data.IDbTransaction Transaction(System.Data.IsolationLevel isolationLevel, string transactionName)
            {
                if (trans == null)
                {
                    if (conn == null)
                    {
                        cnnSQLServer();
                    }
                    if (conn.State == ConnectionState.Closed)
                    {
                        conn.Open();
                    }
                    trans = this.conn.BeginTransaction(isolationLevel, transactionName);
                }
                return trans;
            }

            public void SetTransaction(System.Data.IsolationLevel isolationLevel, string transactionName, System.Data.IDbTransaction Value)
            {
                trans = Value as System.Data.SqlClient.SqlTransaction;
            }

            public string ConnString
            {
                get
                {
                    return this.aConnString;
                }
            }
        }
    }

}
