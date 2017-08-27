using System.Data;
using System.Collections;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System;

namespace NBdbm
{
	namespace Manager
	{
		public class mng
		{
			
			
			#region   Start
			
			private NBdbm.Interfaces.iManager clmng;
			
			public mng()
			{
				
				string tipoBanco;
				tipoBanco = self.Settings.tipoBanco;
				if (tipoBanco == "SQLSERVER")
				{
					clmng = new NBdbm.Manager.SQLSERVER.mngSQLsvr();
				}
				else if (tipoBanco == "PostGreSQL")
				{
					clmng = new NBdbm.Manager.PostGreSQL.mngPGsql();
					//Beep()
				}
				else if (tipoBanco == "ACCESS97")
				{
					//clmng = New NBdbm.Manager.Access97.mngAccess
				}
				else if (tipoBanco == "ACCESS2000")
				{
					//clmng = New NBdbm.Manager.Access2000.mngAccess
				}
				else
				{
					//implementar gravação de log de erro por não haver um tipo correspondente
					//makelog("Os parametro validos são: SQLSERVER, PostGreSQL, ACCESS97, ACCESS2000")
					//Throw New ApplicationException("Sem um tipo de conexão especificado não há como prosseguir")
					//Throw New ArgumentNullException("...") não apresenta a mensagem.
					throw (new ArgumentOutOfRangeException("Sem um tipo de banco de dados especificado não há como prosseguir"));
					//Throw New NotSupportedException
				}
				
			}
			
			public mng(NBdbm.Interfaces.iManager manager_DB)
			{
				this.clmng = manager_DB;
			}
			
			public void dispose()
			{
				this.clmng = null;
			}
			
			~mng()
			{
				base.Finalize();
				dispose();
			}
			#endregion
			
			public object createDB(string commandString)
			{
				return clmng.createDB(commandString);
			}
			
			public object createField(string commandString)
			{
				return clmng.createField(commandString);
			}
			
			public object createTable(string commandString)
			{
				return clmng.createTable(commandString);
			}
			
			public object createView(string commandString)
			{
				return clmng.createView(commandString);
			}
			
			public System.Data.DataView readDB(string SQL)
			{
				return clmng.readDB(SQL);
			}
			
		}
	}
}
