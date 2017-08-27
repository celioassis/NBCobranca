using System.Data;
using System.Collections;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System;


namespace NBdbm
{
	namespace ADM
	{
		
		internal class admDB : Interfaces.iAdmDB
		{
			
			
			
			private string tipoConexao;
			private NBdbm.Interfaces.iAdmDB myDB;
			
			public admDB()
			{
				this.tipoConexao = self.Settings.tipoConexao;
			}
			public admDB(tipos.tiposConection TipoConexao)
			{
				self.Settings.tipoConexao = tipoConexao.ToString();
				this.tipoConexao = self.Settings.tipoConexao;
			}
			public void Dispose()
			{
				if (myDB != null)
				{
					this.myDB.Dispose();
					this.myDB = null;
				}
				//MyBase.dispose(True)
			}
			
			~admDB()
			{
				this.Dispose();
			}
			
			private NBdbm.Interfaces.iAdmDB mDB
			{
				get
				{
					if (myDB == null)
					{
						//tipoConexao = self.Settings.tipoConexao
						if (((tipoConexao) == "SQLSERVER") || ((tipoConexao) == "SQLSVR_LUG"))
						{
							myDB = new NBdbm.ADM.admDB_SQLsvr();
						}
						else if ((tipoConexao) == "OLEDB")
						{
							myDB = new NBdbm.ADM.admDB_OLEDB();
						}
						else if ((tipoConexao) == "ODBC")
						{
							myDB = new NBdbm.ADM.admDB_ODBC();
						}
						else if ((tipoConexao) == "OUTDB")
						{
							myDB = new NBdbm.ADM.admDB_OLEDB();
						}
						else
						{
							//implementar gravação de log de erro por não haver um tipo correspondente
							//makelog("Os parametro validos são: SLQLWL,SASKD, ASAKS"
							//Throw New ApplicationException("Sem um tipo de conexão especificado não há como prosseguir")
							//Throw New ArgumentNullException("...") não apresenta a mensagem.
							throw (new ArgumentOutOfRangeException("Sem um tipo de conexão especificado não há como prosseguir"));
							//Throw New NotSupportedException
						}
						//If tipoConexao = "SQLSERVER" Then
						//  myDB = New NBdbm.ADM.admDB_SQLsvr
						//  'Beep()
						//ElseIf tipoConexao = "OLEDB" Then
						//  myDB = New NBdbm.ADM.admDB_OLEDB
						//  'Beep()
						//ElseIf tipoConexao = "ODBC" Then
						//  myDB = New NBdbm.ADM.admDB_ODBC
						//  'Beep()
						//ElseIf tipoConexao = "OUTDB" Then
						//  myDB = New NBdbm.ADM.admDB_OLEDB
						//  'Beep()
						//Else
						//  'implementar gravação de log de erro por não haver um tipo correspondente
						//  'makelog("Os parametro validos são: SLQLWL,SASKD, ASAKS"
						//  'Throw New ApplicationException("Sem um tipo de conexão especificado não há como prosseguir")
						//  'Throw New ArgumentNullException("...") não apresenta a mensagem.
						//  Throw New ArgumentOutOfRangeException("Sem um tipo de conexão especificado não há como prosseguir")
						//  'Throw New NotSupportedException
						//End If
					}
					return myDB;
				}
			}
			
			public System.Data.IDbConnection Connection
			{
				get
				{
					return mDB.Connection;
				}
				set
				{
					mDB.Connection = value;
				}
			}
			
			public System.Data.IDbConnection ConnectionReader
			{
				get
				{
					return mDB.ConnectionReader;
				}
				set
				{
					mDB.ConnectionReader = value;
				}
			}
			
			public System.Data.IDbConnection NewConnection
			{
				get
				{
					return mDB.NewConnection;
				}
			}
			
			public System.Data.IDbCommand Command(string stringCommand, System.Data.IDbConnection pNewConnection)
			{
				return mDB.Command(stringCommand, pNewConnection);
			}
			
			public System.Data.IDbCommand Command(string stringCommand)
			{
				return mDB.Command(stringCommand);
			}
			
			public System.Data.IDbCommand Command(string stringCommand, System.Data.IDbTransaction transaction)
			{
				return mDB.Command(stringCommand, transaction);
			}
			
			public System.Data.IDbDataAdapter dataAdapter(string stringCommand)
			{
				return mDB.dataAdapter(stringCommand);
			}
			
			public System.Data.IDbDataParameter dataParameter(string stringCommand)
			{
				return mDB.dataParameter(stringCommand);
			}
			
			public string sqlListaTabelas
			{
				get
				{
					return mDB.sqlListaTabelas;
				}
			}
			
			public string sqlListaCampos(string tableName)
			{
				return mDB.sqlListaCampos(tableName);
			}
			
			public System.Data.IDbTransaction Transaction(System.Data.IsolationLevel isolationLevel, string transactionName)
			{
				return mDB.Transaction(isolationLevel, transactionName);
			}
			public void SetTransaction(System.Data.IsolationLevel isolationLevel, string transactionName, System.Data.IDbTransaction Value)
			{
				mDB.Transaction = Value;
			}
			
			public void FinalizaTransaction(bool nocommit)
			{
				if (nocommit == false)
				{
					this.Transaction.Commit();
					this.Transaction.Dispose();
					this.Transaction = null;
					this.connection.Close();
				}
			}
			
			public string ConnString
			{
				get
				{
					return this.myDB.ConnString;
				}
			}
		}
		
		internal class stringConnection
		{
			
			
			#region   Variaveis Locais
			private Fachadas.Fields scVar;
			public bool Dirty;
			#endregion
			
			#region   Start & End
			
			public stringConnection()
			{
				scVar = new Fachadas.Fields();
			}
			
			public void Dispose()
			{
				if (scVar != null)
				{
					this.scVar.dispose();
				}
				this.scVar = null;
			}
			
			~stringConnection()
			{
				base.Finalize();
				this.Dispose();
			}
			
			#endregion
			
			#region   Métodos & Subs
			
			public Fachadas.Fields ConnProperty
			{
				get
				{
					if (this.scVar == null)
					{
						scVar = new Fachadas.Fields();
					}
					return this.scVar;
				}
			}
			
			public void ZeraConnProperty()
			{
				this.scVar = null;
			}
			private void getScVar()
			{
				scVar.Add("ApplicationName", "", System.Type.GetType("System.String"), false);
				scVar.Add("PassWord", "", System.Type.GetType("System.String"), false);
				scVar.Add("UserID", "", System.Type.GetType("System.String"), false);
			}
			
			#endregion
			
			public string StringConnection
			{
				get
				{
					System.Text.StringBuilder stringCnn = new System.Text.StringBuilder();
					
					
					try
					{
						foreach (Fachadas.Fields.field f in this.scVar)
						{
							if (f.Dirty == true)
							{
								stringCnn.Append(f.value + ";");
							}
						}
						if (stringCnn.ToString().Substring(stringCnn.ToString().Length - 1, 1) == ";")
						{
							stringCnn.Remove(stringCnn.Length - 1, 1);
						}
						stringCnn.Replace("login", self.Settings.UserId);
						stringCnn.Replace("senha", self.Settings.Password);
						
					}
					catch (Exception ex)
					{
						Interaction.Beep();
						throw (new Exception("Não foi possível recuperar a string de conexão, gerando o seguinte erro:" + ex.GetType().ToString() + ".", ex));
					}
					
					return stringCnn.ToString();
				}
			}
		}
		
	}
	
	
}
