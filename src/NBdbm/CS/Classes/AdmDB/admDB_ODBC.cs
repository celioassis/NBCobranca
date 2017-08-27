using System.Data;
using System.Collections;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System;


namespace NBdbm
{
	namespace ADM
	{
		
		internal class admDB_ODBC : Interfaces.iAdmDB
		{
			
			
			private System.Data.Odbc.OdbcConnection conn;
			private System.Data.Odbc.OdbcCommand comm;
			private System.Data.Odbc.OdbcTransaction trans;
			private string aConnString;
			
			public admDB_ODBC()
			{
				this.aConnString = self.Settings.stringConnection;
			}
			
			public void Dispose()
			{
				conn = null;
				comm = null;
			}
			
			~admDB_ODBC()
			{
				base.Finalize();
				this.Dispose();
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
					System.Data.IDbConnection returnValue;
					returnValue = cnnODBCdb(false);
					return returnValue;
				}
				set
				{
					this.conn = value;
				}
			}
			
			public System.Data.IDbConnection ConnectionReader
			{
				get
				{
					connection = cnnODBCdb(false);
					return connection;
				}
				set
				{
					this.conn = value;
				}
			}
			
			public System.Data.IDbConnection NewConnection
			{
				get
				{
					return cnnODBCdb(true);
				}
			}
			
			//Exemplo de String de Conexão
			//="DRIVER={SQL Server}; Server=127.0.0.1;Database=NEOBRIDGE;User ID=sa; Password=tonetto"
			//="Driver={SQL Server}; Server=127.0.0.1;Database=NEOBRIDGE;User ID=sa; Password=tonetto;Trusted_Connection=true"
			//Estas não funcionam!!!!
			//Não usar DSN
			//"DRIVER={SQL Server}; Server=127.0.0.1;DSN=NEOBRIDGE;User ID=sa; Password=tonetto"
			
			//ConnectionString  - Gets or sets the string used to open a data source.
			//ConnectionTimeout - Gets or sets the time to wait while trying to establish a connection before terminating the attempt and generating an error.
			//Container         - (inherited from Component) Gets the IContainer that contains the Component.
			//Database          - Gets the name of the current database or the database to be used after a connection is opened.
			//DataSource        - Gets the server name or file name of the data source.
			//Driver            - Gets the name of the ODBC driver specified for the current connection.
			//ServerVersion     - Gets a string containing the version of the server to which the client is connected.
			//Site              - (inherited from Component) Gets or sets the ISite of the Component.
			//State             - Gets the current state of the connection.
			private System.Data.Odbc.OdbcConnection cnnODBCdb(bool pNova)
			{
				try
				{
					if (conn == null || pNova)
					{
						conn = new System.Data.Odbc.OdbcConnection();
					}
					
					if (conn.ConnectionString == "")
					{
						//stringConnection = "Driver={SQL Server};Server=127.0.0.1;Database=NEOBRIDGE;User ID=sa;Password=tonetto;Trusted_Connection=true"
						//conn.ConnectionString = stringConnection
						conn.ConnectionString = self.Settings.stringConnection;
					}
					
					if (conn.State == ConnectionState.Closed)
					{
						conn.Open();
					}
					
					return conn;
					
				}
				catch (Exception)
				{
					//implementar o log de erro
					//ex.toString
					throw (new Exception("Não foi possível estabelecer uma conexão com o banco de dados!"));
				}
				
			}
			
			
			public System.Data.IDbCommand Command(string stringCommand, System.Data.IDbConnection pNewConnection)
			{
				return new System.Data.Odbc.OdbcCommand(stringCommand, pNewConnection);
			}
			public System.Data.IDbCommand Command(string stringCommand, System.Data.IDbTransaction transaction)
			{
				return null;
			}
			//Friend ReadOnly Property Command(ByVal stringCommand As String) As System.Data.IDbCommand Implements Interfaces.iAdmDB.Command
			public System.Data.IDbCommand Command(string stringCommand)
			{
				//Verificar se é necessário este if logo abaixo e se ele não ocasiona erro
				if (comm == null)
				{
					comm = new System.Data.Odbc.OdbcCommand(string.Empty, this.connection);
				}
				
				comm.CommandText = stringCommand;
				return comm;
			}
			
			public System.Data.IDbDataAdapter dataAdapter(string stringCommand)
			{
				//If comm.Connection.State = ConnectionState.Open Then
				//  Return New OleDb.OleDbDataAdapter(stringCommand, Me.connection)
				//End If
				return new System.Data.Odbc.OdbcDataAdapter(this.Command(stringCommand).CommandText, this.connection.ConnectionString);
			}
			
			public System.Data.IDbDataParameter dataParameter(string stringCommand)
			{
				return comm.Parameters;
			}
			
			public string sqlListaCampos(string tableName)
			{
				return "";
			}
			
			public string sqlListaTabelas
			{
				get
				{
					//Throw New NotImplementedException
					return "";
				}
			}
			
			public System.Data.IDbTransaction Transaction(System.Data.IsolationLevel isolationLevel, string transactionName)
			{
				if (trans == null)
				{
					trans = conn.BeginTransaction(isolationLevel);
				}
				return trans;
			}
			public void SetTransaction(System.Data.IsolationLevel isolationLevel, string transactionName, System.Data.IDbTransaction Value)
			{
				trans = Value;
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
