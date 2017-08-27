using System.Data;
using System.Collections;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System;


namespace NBdbm
{
	namespace ADM
	{
		
		internal class admDB_OLEDB : Interfaces.iAdmDB
		{
			
			
			private System.Data.OleDb.OleDbConnection conn;
			private System.Data.OleDb.OleDbCommand comm;
			private System.Data.OleDb.OleDbTransaction trans;
			private string aConnString;
			
			public admDB_OLEDB() : this("")
			{
			}
			
			public admDB_OLEDB(string ConnectionString)
			{
				if (conn == null)
				{
					conn = new System.Data.OleDb.OleDbConnection();
				}
				conn.ConnectionString = ConnectionString;
				this.aConnString = ConnectionString;
			}
			
			public void Dispose()
			{
				conn = null;
				comm = null;
			}
			
			~admDB_OLEDB()
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
					return cnnOLEdb(false);
				}
				set
				{
					
				}
			}
			
			public System.Data.IDbConnection ConnectionReader
			{
				get
				{
					return cnnOLEdb(false);
				}
				set
				{
					
				}
			}
			
			public System.Data.IDbConnection NewConnection
			{
				get
				{
					return cnnOLEdb(true);
				}
			}
			//Exemplo de String de Conexão
			//= "Provider=SQLOLEDB.1;Persist Security Info=False;User ID=ProSystem_; Password=nitromate;Initial Catalog=NBCOBBBO; Data Source=LocalHost"
			//= "DSN=NBCOBBBO;User ID=ProSystem_; Password=nitromate;SystemDB=\\Stone\X\Bancos de Dados\Edgar\BBO\Logo.bmp"
			//= "Provider=SQLOLEDB.1;Persist Security Info=False;User ID=ProSystem_; Password=nitromate;Initial Catalog=NBCOBBBO; Data Source=LocalHost"
			//= "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=Dados.mdb"
			//= "data source=Stone;initial catalog=Neobridge;user id=ProSystem_; password=nitromate;"
			//= "Data Source=(local);Integrated Security=yes"
			//= "Network Library=DBMSSOCN; Data Source=127.0.0.1, 1433;Initial Catalog=EDGAR; User ID=ProSystem_; Password=nitromate"
			//= "Data Source=127.0.0.1;Initial Catalog=Neobridge;User ID=sa; Password=tonetto"
			//= "Data Source=172.17.0.66;User ID=marcos; Password=tonetto"
			//= "Driver={Microsoft Access Driver (*.mdb)}; Dbq=c:\banco.mdb; SystemDB=c:\logo.bmp;"
			//= "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=..\Northwind.mdb"
			private System.Data.OleDb.OleDbConnection cnnOLEdb(bool pNova)
			{
				try
				{
					if (conn == null || pNova)
					{
						conn = new System.Data.OleDb.OleDbConnection();
					}
					
					if (conn.ConnectionString == "")
					{
						//stringConnection = "Provider=Microsoft.Jet.OLEDB.4.0;data source=X:\Bancos de Dados\Neobridge.mdb"
						//stringConnection = "Provider=SQLOLEDB.1;database=x:\bancos de dados\neo.mdb;User ID=ProSystem_;Password=nitromate;SystemDB=x:\bancos de dados\logo.bmp"
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
				return new System.Data.OleDb.OleDbCommand(stringCommand, pNewConnection);
			}
			
			public System.Data.IDbCommand Command(string stringCommand)
			{
				//Verificar se é necessário este if logo abaixo e se ele não ocasiona erro
				if (comm == null)
				{
					comm = new System.Data.OleDb.OleDbCommand(string.Empty, this.connection);
				}
				
				comm.CommandText = stringCommand;
				return comm;
			}
			
			public System.Data.IDbCommand Command(string stringCommand, System.Data.IDbTransaction transaction)
			{
				//Verificar se é necessário este if logo abaixo e se ele não ocasiona erro
				if (comm == null)
				{
					comm = new System.Data.OleDb.OleDbCommand(string.Empty, this.connection, transaction);
				}
				
				comm.CommandText = stringCommand;
				return comm;
			}
			public System.Data.IDbDataAdapter dataAdapter(string stringCommand)
			{
				//If comm.Connection.State = ConnectionState.Open Then
				//  Return New OleDb.OleDbDataAdapter(stringCommand, Me.connection)
				//End If
				return new System.Data.OleDb.OleDbDataAdapter(this.Command(stringCommand).CommandText, this.connection.ConnectionString);
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
