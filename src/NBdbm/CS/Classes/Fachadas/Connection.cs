using System.Data;
using System.Collections;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System;


namespace NBdbm
{
	namespace Fachadas
	{
		
		public class Connection
		{
			
			
			private NBdbm.ADM.admDB myDB;
			
			public Connection() : this(self.Settings.Credencial)
			{
			}
			
			public Connection(tipos.Retorno credencial)
			{
				if (credencial.Sucesso == true)
				{
					if (self.Settings.Password == NBFuncoes.decripto(credencial.Tag).ToString())
					{
						if (credencial.ToString == "NBdbm")
						{
							self.Settings.Credencial = credencial;
							this.myDB = new NBdbm.ADM.admDB();
						}
					}
				}
			}
			public Connection(tipos.Retorno credencial, tipos.tiposConection TipoConexao)
			{
				if (credencial.Sucesso == true)
				{
					if (self.Settings.Password == NBFuncoes.decripto(credencial.Tag).ToString())
					{
						if (credencial.ToString == "NBdbm")
						{
							self.Settings.Credencial = credencial;
							this.myDB = new NBdbm.ADM.admDB(TipoConexao);
						}
					}
				}
			}
			public System.Data.IDbCommand Command(string stringCommand)
			{
				return myDB.Command(stringCommand);
			}
			
			public System.Data.IDbConnection Connection_Renamed
			{
				get
				{
					return myDB.ConnectionReader;
				}
			}
			
			public System.Data.IDbDataAdapter dataAdapter(string stringCommand)
			{
				return myDB.dataAdapter(stringCommand);
			}
			
			public System.Data.IDbDataParameter dataParameter(string stringCommand)
			{
				return myDB.dataParameter(stringCommand);
			}
			
			public void Dispose()
			{
				myDB.Dispose();
				myDB = null;
			}
			
		}
		
	}
	
	
	
}
