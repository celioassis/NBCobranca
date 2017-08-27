using System.Data;
using System.Collections;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System;


namespace NBdbm
{
	namespace Fachadas
	{
		
		//Classe Padrão de Manipulação de Dados
		public class allClass : Interfaces.Primitivas.iAllClass
		{
			
			
			
			#region   Start
			
			private mAllClass varAllClass = new mAllClass();
			private NBdbm.Interfaces.iAdmDB AdmDB;
			private bool mDirty;
			private bool mVariosCPFCNPJ;
			private System.Data.IDataReader mDataReader;
			protected tipos.tiposConection mTipoConexao;
			
			public allClass() : this("")
			{
				mVariosCPFCNPJ = false;
			}
			public allClass(string TableName) : this(TableName, tipos.tiposConection.Default_)
			{
			}
			public allClass(string tableName, tipos.tiposConection tipoConection)
			{
				this.tableName = tableName;
				if (tipoConection != tipos.tiposConection.Default_)
				{
					self.Settings.tipoConexao = tipoConection.ToString();
				}
				this.mTipoConexao = tipoConection;
				AdmDB = self.AdmDB;
				this.InicializaPadrao();
			}
			
			public allClass(string tableName, NBdbm.Interfaces.iAdmDB cnn)
			{
				this.tableName = tableName;
				AdmDB = cnn;
				this.InicializaPadrao();
			}
			
			private void InicializaPadrao()
			{
				try
				{
					
					if (NBdbm.self.Settings == null)
					{
						//Implementar gravação de log de erro
						//Erro: a setting deveria estar estanciada
						throw (new Exception("A classe setting deveria estar estanciada."));
					}
					if (tableName != "")
					{
						System.Text.StringBuilder mSqlBuscaFields = new System.Text.StringBuilder();
						mSqlBuscaFields.Append("SELECT Top 1 * ");
						mSqlBuscaFields.Append("FROM ");
						mSqlBuscaFields.Append(tableName);
						this.varAllClass.var = new Fields();
						this.varAllClass.f = new Fachadas.priFilter();
						this.varAllClass.var.createFields(this.DataReader(mSqlBuscaFields.ToString()));
						this.Clear_vars();
					}
				}
				catch (Exception ex)
				{
					throw (new NBexception("Problemas na Inicialização do Objeto " + this.GetType().Name, ex));
				}
				
			}
			public virtual void Dispose()
			{
				if (AdmDB != null)
				{
					AdmDB.Dispose();
				}
				if (varAllClass != null)
				{
					varAllClass.dispose();
				}
				varAllClass = null;
				AdmDB = null;
				mDirty = null;
				//GC.SuppressFinalize(Me)
			}
			
			internal allClass toObject
			{
				get
				{
					return this;
				}
				set
				{
					varAllClass = value.varAllClass;
				}
			}
			
			#endregion
			
			#region   Code Base
			
			internal Fields var
			{
				get
				{
					return varAllClass.var;
				}
				set
				{
					varAllClass.var = value;
				}
			}
			
			internal priFilter F
			{
				get
				{
					return varAllClass.f;
				}
				set
				{
					varAllClass.f = value;
				}
			}
			
			public string tableName
			{
				get
				{
					return varAllClass.tbName.ToString();
				}
				set
				{
					varAllClass.tbName = value;
				}
			}
			
			internal string CPFCNPJ
			{
				//retorna o CFP ou CNPJ da tabelas de CPF/CNPJ únicos!
				get
				{
					string strSQL;
					System.Data.DataSet dsFields;
					
					strSQL = "Select CPFCNPJ from CTRL_CPFCNPJ where [idCPFCNPJ] = " + this.var["idCPFCNPJ"].value + ";";
					
					dsFields = new DataSet("Campos");
					try
					{
						AdmDB.dataAdapter(strSQL).Fill(dsFields);
						// dsFields.Tables(0).DefaultView
						if (dsFields.Tables[0].DefaultView.Table.Rows.Count > 0)
						{
							return dsFields.Tables[0].DefaultView.Table.Rows[0][0].ToString();
						}
						else
						{
							return "";
						}
					}
					catch (Exception ex)
					{
						//implementar método que insere o registro na tabela CTRL_CPFCNPJ
						throw (new Exception("Não foi possível efetuar get em CPFCNPJ.", ex));
					}
				}
				set
				{
					bool pF;
					//Dim mCnpjCpf_Validado As String
					try
					{
						pF = var["pessoaFJ"].value;
						this.var["CPFCNPJ"].value = NBFuncoes.validaCPFCNPJ(value, pF);
						var["idCPFCNPJ"].value = this.idCPFCNPJ;
					}
					catch (Exception ex)
					{
						NBexception mNBex = new NBexception("Problemas na Definição do CPFCNPJ", ex);
						mNBex.Source = "allClass.CPFCNPJ";
						throw (mNBex);
					}
				}
			}
			
			internal int idCPFCNPJ
			{
				//retorna o id da tabela CTRL_CFPCNPJ
				get
				{
					string strSQL;
					System.Data.DataSet dsFields;
					
					strSQL = "Select idCPFCNPJ from CTRL_CPFCNPJ where [CPFCNPJ] = \'" + this.var["CPFCNPJ"].value + "\'";
					
					dsFields = new DataSet("Campos");
					try
					{
						AdmDB.dataAdapter(strSQL).Fill(dsFields);
						if (dsFields.Tables[0].DefaultView.Table.Rows.Count == 0)
						{
							strSQL = "insert into [CTRL_CPFCNPJ] (CPFCNPJ) VALUES (\'" + this.var["CPFCNPJ"].value + "\')";
							AdmDB.Command(strSQL).ExecuteNonQuery();
							strSQL = "Select idCPFCNPJ from CTRL_CPFCNPJ where [CPFCNPJ] = \'" + this.var["CPFCNPJ"].value + "\'";
							AdmDB.dataAdapter(strSQL).Fill(dsFields);
						}
					}
					catch (Exception ex)
					{
						throw (new Exception("Não foi possível inserir CPFCNPJ na tabela.", ex));
					}
					// dsFields.Tables(0).DefaultView
					return int.Parse(dsFields.Tables[0].DefaultView.Table.Rows[0][0].ToString());
				}
			}
			
			public bool VariosCPFCNPJ
			{
				get
				{
					return this.mVariosCPFCNPJ;
				}
			}
			
			internal int RetornaIdCPFCNPJ(string CPFCNPJ)
			{
				string strSQL;
				System.Data.DataSet dsFields;
				int ID;
				strSQL = "Select idCPFCNPJ from CTRL_CPFCNPJ where [CPFCNPJ] = \'" + CPFCNPJ + "\'";
				dsFields = new DataSet("Campos");
				try
				{
					AdmDB.dataAdapter(strSQL).Fill(dsFields);
					if (dsFields.Tables[0].DefaultView.Table.Rows.Count == 1)
					{
						ID = int.Parse(dsFields.Tables[0].DefaultView.Table.Rows[0][0].ToString());
					}
					else if (dsFields.Tables[0].DefaultView.Table.Rows.Count > 1)
					{
						ID = int.Parse(dsFields.Tables[0].DefaultView.Table.Rows[0][0].ToString());
						this.mVariosCPFCNPJ = true;
					}
					else
					{
						ID = 0;
					}
				}
				catch (Exception ex)
				{
					NBexception NbEx = new NBexception("Não foi possível retornar o ID do CPF.", ex);
					NbEx.Source = "allClass.RetornaIdCPFCNPJ";
					throw (NbEx);
				}
				return ID;
			}
			
			public bool Inclusao
			{
				get
				{
					return this.varAllClass.inclusao;
				}
				set
				{
					this.varAllClass.inclusao = value;
				}
			}
			
			#endregion
			
			#region   Code - Métodos
			
			public string stringSQL(bool forceReplace)
			{
				mDirty = false;
				string mSqlFormato = "Select {0}* FROM {1} {2}";
				string mSQLFiltro;
				mSQLFiltro = this.F.filterWhere(forceReplace);
				if (this.F.filterDelete == false)
				{
					mSQLFiltro = mSQLFiltro + this.F.filterHaving(forceReplace) + this.F.filterGroupBy(forceReplace) + this.F.filterOrderBy(forceReplace);
				}
				else
				{
					mSqlFormato = "Delete FROM {0} {1}";
				}
				string mSQLretorno;
				
				if (this.F.filterTop < 1)
				{
					if (this.F.filterDelete)
					{
						mSQLretorno = string.Format(mSqlFormato, this.tableName, mSQLFiltro);
					}
					else
					{
						mSQLretorno = string.Format(mSqlFormato, "", this.tableName, mSQLFiltro);
					}
				}
				else
				{
					mSQLretorno = string.Format(mSqlFormato, "Top " + this.F.filterTop.ToString() + " ", this.tableName, mSQLFiltro);
				}
				
				return mSQLretorno;
			}
			public void Clear_filters()
			{
				if (F != null)
				{
					F.Dispose();
				}
				F = null;
				F = new Fachadas.priFilter();
			}
			public void Clear_vars()
			{
				
				this.var.clearFields();
				
			}
			public Collection getFields()
			{
				return getFields(0);
			}
			public Collection getFields(double row)
			{
				//Call var.getFields(Me.DataSource, row)
				var.getFields(this.DataReader());
				return var.Collection;
			}
			public void getFields(string FiltroWhere)
			{
				this.Clear_filters();
				this.filterWhere = FiltroWhere;
				this.getFields();
			}
			public NbCollection CriaColecaoFields()
			{
				if (F.filterWhere(false) == null)
				{
					throw (new NBexception("É preciso definir o FilterWhere para que possa ser criado uma coleção"));
				}
				NbCollection mColection = new NbCollection();
				System.Data.IDataReader mDRC;
				mDRC = this.DataReader();
				
				if (mDRC == null)
				{
					this.Clear_vars();
					return mColection;
				}
				
				while (mDRC.Read())
				{
					Fields mmFields = new Fields();
					mmFields = new Fields();
					foreach (Fields.field mmField in this.var.Collection)
					{
						mmFields.Add(mmField.Caption, mmField.value, mmField.fdType, false);
					}
					mmFields.PreencheFields(mDRC);
					this.var = mmFields;
					mColection.Add(mDRC.GetValue(0), ((object) mmFields));
				}
				mDRC.Dispose();
				mDRC = null;
				if (mColection.Count == 0)
				{
					this.Clear_vars();
				}
				return mColection;
			}
			public virtual void editar(bool noCommit)
			{
				string strSQL;
				Collection fields;
				
				System.Data.IDbTransaction mTransaction = null;
				fields = this.var.Collection;
				
				strSQL = "Update [" + tableName + "] set  ";
				foreach (Fields.field field in fields)
				{
					if (field.Dirty == true)
					{
						mDirty = true;
						strSQL = strSQL + "[" + field.Caption + "] = ";
						switch (field.fdType.FullName)
						{
							case "System.DateTime":
								strSQL = strSQL + "CONVERT(DateTime,\'" + (System.Convert.ToDateTime(field.value)).ToString("MM-dd-yy HH:mm:ss") + "\',1), ";
								break;
								
							case "System.Boolean":
								int mValue = System.Convert.ToInt32(field.value);
								if (mValue < 0)
								{
									mValue = 1;
								}
								else
								{
									mValue = 0;
								}
								strSQL = strSQL + "\'" + mValue.ToString() + "\', ";
								break;
								
							default:
								strSQL = strSQL + "\'" + field.value + "\', ";
								break;
						}
					}
				}
				strSQL = strSQL.Substring(0, strSQL.Length - 2) + " ";
				strSQL = strSQL + F.filterWhere;
				
				try
				{
					mDirty = true;
					if (AdmDB.Connection.State == ConnectionState.Closed)
					{
						AdmDB.Connection.Open();
					}
					mTransaction = AdmDB.Transaction(IsolationLevel.ReadUncommitted);
					AdmDB.Command("set arithabort on", mTransaction).ExecuteNonQuery();
					this.var[1].value = AdmDB.Command(this.stringSQL, mTransaction).ExecuteScalar;
					AdmDB.Command(strSQL, mTransaction).ExecuteNonQuery();
					AdmDB.Command("set arithabort off", mTransaction).ExecuteNonQuery();
					if (! this.var[1].value == null)
					{
						self.AdmDB.FinalizaTransaction(noCommit);
						this.Inclusao = false;
					}
					else
					{
						this.Inclusao = true;
					}
				}
				catch (Exception ex)
				{
					mTransaction.Rollback();
					AdmDB.Transaction = null;
					Exception mEx = new Exception("Problemas na Edição do registro na tabela - " + tableName, ex);
					mEx.Source = "NBdbm.Fachadas.allClass.Editar";
					throw (mEx);
				}
				
			}
			public virtual void excluir(bool NoCommit)
			{
				int TotalExcluido;
				try
				{
					AdmDB.Command("set arithabort on", AdmDB.Transaction(IsolationLevel.ReadUncommitted)).ExecuteNonQuery();
					this.F.filterDelete = true;
					TotalExcluido = AdmDB.Command(this.stringSQL, self.AdmDB.Transaction(IsolationLevel.ReadUncommitted)).ExecuteNonQuery();
					AdmDB.Command("set arithabort off", AdmDB.Transaction(IsolationLevel.ReadUncommitted)).ExecuteNonQuery();
					if (TotalExcluido == 0)
					{
						throw (new Exception("Não Foi possível encontrar o Registro com esta com esta instrução SQL:" + "\r\n" + this.stringSQL));
					}
					
					self.AdmDB.FinalizaTransaction(NoCommit);
					this.F.filterDelete = false;
					
				}
				catch (Exception ex)
				{
					AdmDB.Transaction.Rollback();
					AdmDB.Transaction = null;
					Exception mEx = new Exception("Não foi possível excluir o registro da tabela - " + this.tableName, ex);
					mEx.Source = "NBdbm.Fachadas.allClass.Excluir";
					
					throw (mEx);
				}
			}
			public virtual void inserir(bool noCommit)
			{
				
				string strSQL;
				string strSQLFields = "";
				string strSQLValues = "";
				Collection fields;
				
				
				try
				{
					fields = this.var.Collection;
					strSQL = "SET NOCOUNT ON Insert into [" + tableName + "] ";
					foreach (Fields.field field in fields)
					{
						if (field.Dirty == true)
						{
							strSQLFields = strSQLFields + "[" + field.Caption + "], ";
							switch (field.fdType.FullName)
							{
								case "System.DateTime":
									strSQLValues = strSQLValues + "CONVERT(DateTime,\'" + (System.Convert.ToDateTime(field.value)).ToString("MM-dd-yy HH:mm:ss") + "\',1), ";
									break;
									
								case "System.Boolean":
									int mValue = System.Convert.ToInt32(field.value);
									if (mValue < 0)
									{
										mValue = 1;
									}
									else
									{
										mValue = 0;
									}
									strSQLValues = strSQLValues + "\'" + mValue.ToString() + "\', ";
									break;
									
								default:
									strSQLValues = strSQLValues + "\'" + field.value.ToString() + "\', ";
									break;
							}
						}
					}
					strSQLFields = strSQLFields.Substring(0, strSQLFields.Length - 2) + " ";
					strSQLValues = strSQLValues.Substring(0, strSQLValues.Length - 2) + " ";
					strSQL = strSQL + "(" + strSQLFields + ") ";
					strSQL = strSQL + " VALUES (" + strSQLValues + ") ";
					strSQL = strSQL + " SELECT @@IDENTITY SET NOCOUNT OFF";
					
					AdmDB.Command("set arithabort on", AdmDB.Transaction(IsolationLevel.ReadUncommitted)).ExecuteNonQuery();
					this.var[1].value = AdmDB.Command(strSQL, AdmDB.Transaction(IsolationLevel.ReadUncommitted)).ExecuteScalar();
					AdmDB.Command("set arithabort off", AdmDB.Transaction(IsolationLevel.ReadUncommitted)).ExecuteNonQuery();
					
					self.AdmDB.FinalizaTransaction(noCommit);
					
				}
				catch (System.Exception ex)
				{
					AdmDB.Transaction.Rollback();
					AdmDB.Transaction = null;
					Exception mEx = new Exception("Problemas na Inclusão do registro na tabela - " + tableName, ex);
					mEx.Source = "NBdbm.Fachadas.allClass.Inserir";
					throw (mEx);
				}
			}
			public void SalvarPadrao(bool nocommit, string filtro)
			{
				this.filterWhere = filtro;
				editar(nocommit);
				if (this.Inclusao)
				{
					inserir(nocommit);
				}
			}
			public System.Data.IDataReader DataReader()
			{
				int mRetorno;
				System.Data.IDbConnection mConnection = this.AdmDB.ConnectionReader;
				System.Data.IDbCommand mCommand;
				try
				{
					
					mCommand = this.AdmDB.Command(this.stringSQL, mConnection);
					
					this.AbreConexao(mConnection);
					mRetorno = Convert.ToInt32(mCommand.ExecuteScalar());
					if (this.mDataReader != null)
					{
						if (this.mDataReader.IsClosed == false)
						{
							this.mDataReader.Close();
						}
					}
					if (mRetorno > 0)
					{
						this.AbreConexao(mConnection);
						this.mDataReader = mCommand.ExecuteReader();
					}
					else
					{
						mCommand = this.AdmDB.Command(this.stringSQL(true), mConnection);
						this.AbreConexao(mConnection);
						mRetorno = Convert.ToInt32(mCommand.ExecuteScalar());
						if (mDataReader != null)
						{
							mDataReader.Dispose();
							mDataReader = null;
						}
					}
					
					if (mRetorno > 0 && mDataReader == null)
					{
						this.AbreConexao(mConnection);
						this.mDataReader = mCommand.ExecuteReader();
					}
					if (mRetorno == 0 && mDataReader != null)
					{
						mDataReader.Dispose();
						mConnection = null;
						mCommand.Dispose();
						mCommand = null;
						mDataReader = null;
					}
					return mDataReader;
				}
				catch (Exception ex)
				{
					throw (new NBexception("Problemas na Execução do DataReader do Objeto " + this.GetType().Name, ex));
				}
				
			}
			public System.Data.IDataReader DataReader(string pSql)
			{
				try
				{
					System.Data.IDbCommand mSqlCommand;
					System.Data.IDbConnection mConnection;
					mConnection = AdmDB.ConnectionReader;
					mSqlCommand = AdmDB.Command(pSql, mConnection);
					mConnection = mSqlCommand.Connection;
					
					if (mDataReader != null)
					{
						if (mDataReader.IsClosed == false)
						{
							mDataReader.Close();
						}
					}
					this.AbreConexao(mConnection);
					mDataReader = mSqlCommand.ExecuteReader();
					return mDataReader;
				}
				catch (Exception ex)
				{
					throw (new NBexception("Problemas na Execução do DataReader", ex));
				}
				
			}
			public System.Data.DataView datasourceTables()
			{
				
				string strSQL;
				System.Data.DataSet dsFields;
				
				strSQL = AdmDB.sqlListaTabelas;
				
				dsFields = new DataSet("Tables");
				try
				{
					AdmDB.dataAdapter(strSQL).Fill(dsFields);
				}
				catch (Exception)
				{
					//implementar salvar log de erro
				}
				return dsFields.Tables[0].DefaultView;
			}
			public System.Data.DataView datasourceFields()
			{
				
				string strSQL;
				System.Data.DataSet dsFields;
				
				strSQL = AdmDB.sqlListaCampos(tableName);
				
				dsFields = new DataSet("Campos");
				try
				{
					AdmDB.dataAdapter(strSQL).Fill(dsFields);
				}
				catch (Exception)
				{
					//implementar salvar log de erro
				}
				return dsFields.Tables[0].DefaultView;
			}
			public System.Data.DataView DataSource()
			{
				//This function return a object compatíble with:
				//(DataView, DataTable, DataSet)
				if (mDirty == true || varAllClass.ds == null)
				{
					varAllClass.ds = new DataSet("ds_" + tableName);
					try
					{
						AdmDB.dataAdapter(this.stringSQL).Fill(varAllClass.ds);
						if (varAllClass.ds.Tables[0].DefaultView.Count == 0)
						{
							AdmDB.dataAdapter(this.stringSQL(true)).Fill(varAllClass.ds);
						}
						//Atenção:
						//Não necessita aqui, pois já é feito no metodo New!
						//Call Me.getFields()
						//Call var.createFields(ds.Tables(0).DefaultView)
					}
					catch (Exception ex)
					{
						//implementar salvar log de erro
						throw (new NBexception("Não foi possível executar a consulta.", ex));
					}
				}
				return varAllClass.ds.Tables[0].DefaultView;
			}
			public string xmlColFields(Collection colFields)
			{
				string retorno; //New tipos.Retorno
				System.Xml.XmlDocument xDoc = new System.Xml.XmlDocument();
				System.Xml.XmlElement newElem;
				System.Xml.XmlAttribute newAttr;
				
				try
				{
					newElem = xDoc.CreateElement("dataROW");
					xDoc.AppendChild(newElem);
					foreach (NBdbm.Fachadas.Fields.field v in colFields)
					{
						newElem = xDoc.CreateElement(v.Caption);
						newAttr = xDoc.CreateAttribute("value");
						newAttr.Value = v.value.ToString();
						newElem.Attributes.Append(newAttr);
						xDoc.DocumentElement.AppendChild(newElem);
					}
					retorno = xDoc.InnerXml;
				}
				catch (Exception)
				{
					retorno = "</>";
				}
				return retorno;
			}
			private void AbreConexao(System.Data.IDbConnection pConnection)
			{
				if (pConnection.State == ConnectionState.Closed)
				{
					pConnection.ConnectionString = this.AdmDB.ConnString;
					pConnection.Open();
				}
			}
			#endregion
			
			#region   Filtros
			public string filterGroupBy
			{
				set
				{
					mDirty = true;
					this.F.filterGroupBy = value;
				}
			}
			
			public string filterHaving
			{
				set
				{
					mDirty = true;
					this.F.filterHaving = value;
				}
			}
			
			public string filterOrderBy
			{
				set
				{
					mDirty = true;
					this.F.filterOrderBy = value;
				}
			}
			
			public string filterWhere
			{
				set
				{
					mDirty = true;
					this.F.filterWhere = value;
				}
			}
			
			public int filterTop
			{
				
				set
				{
					mDirty = true;
					this.F.filterTop = value;
				}
			}
			#endregion
			
			internal class mAllClass : IDisposable
			{
				
				
				public string tbName;
				public Fachadas.priFilter f;
				public System.Data.DataSet ds;
				public Fields var;
				public bool inclusao;
				public void Dispose()
				{
					this.dispose();
				}
				
				public void dispose()
				{
					tbName = null;
					if (f != null)
					{
						F.Dispose();
					}
					f = null;
					if (ds != null)
					{
						ds.Dispose();
					}
					ds = null;
					if (var != null)
					{
						var.dispose();
					}
					var = null;
				}
				
			}
			
			
			
			
		}
		
		//Classe de Filtros para instruções SQL
		internal class priFilter : IDisposable
		{
			
			
			private string mWhere;
			private string mHaving;
			private string mOrderBy;
			private string mGroupBy;
			private int mTop;
			private bool mDelete;
			private string tipoConexao;
			
			public priFilter()
			{
				tipoConexao = self.Settings.tipoConexao; //= "SQLSERVER"
				this.Clear();
			}
			
			private string replaceFilter(string str, bool forceReplace)
			{
				if (forceReplace == true)
				{
					str = str.Replace("*", "%");
					//Else
					//    str = Replace(str, "%", "*")
				}
				//If tipoConexao = "SQLSERVER" Then
				//End If
				if (str == null)
				{
					str = "";
				}
				return str;
			}
			
			public void Clear()
			{
				mTop = 0;
				mDelete = false;
				mWhere = string.Empty;
				mHaving = string.Empty;
				mOrderBy = string.Empty;
				mGroupBy = string.Empty;
			}
			
			public string filterWhere(bool forceReplace)
			{
				return replaceFilter(this.mWhere, forceReplace).ToString();
			}
			public void SetfilterWhere(bool forceReplace, string stringWhere)
			{
				if (stringWhere.Trim() != "")
				{
					this.mWhere = " where " + stringWhere;
				}
				else
				{
					this.mWhere = "";
				}
			}
			
			public string filterHaving(bool forceReplace)
			{
				return replaceFilter(this.mHaving, forceReplace).ToString();
			}
			public void SetfilterHaving(bool forceReplace, string stringHaving)
			{
				if (stringHaving.Trim() != "")
				{
					this.mHaving = " Having " + stringHaving;
				}
				else
				{
					this.mHaving = "";
				}
			}
			
			public string filterOrderBy(bool forceReplace)
			{
				return this.mOrderBy.ToString();
			}
			public void SetfilterOrderBy(bool forceReplace, string stringOrderBy)
			{
				if (stringOrderBy.Trim() != "")
				{
					this.mOrderBy = " Order by " + stringOrderBy;
				}
				else
				{
					this.mOrderBy = "";
				}
			}
			
			public string filterGroupBy(bool forceReplace)
			{
				return this.mGroupBy.ToString();
			}
			public void SetfilterGroupBy(bool forceReplace, string stringGroupBy)
			{
				if (stringGroupBy.Trim() != "")
				{
					this.mGroupBy = " Group By " + stringGroupBy;
				}
				else
				{
					this.mGroupBy = "";
				}
			}
			
			public int filterTop
			{
				get
				{
					return this.mTop;
				}
				set
				{
					this.mTop = value;
				}
			}
			
			/// <summary>
			/// Define se Filtro é para exclusão ou não.
			/// </summary>
			/// <value></value>
			/// <returns></returns>
			/// <remarks></remarks>
			public bool filterDelete
			{
				get
				{
					return this.mDelete;
				}
				set
				{
					this.mDelete = value;
				}
			}
			
			public void Dispose()
			{
				mWhere = null;
				mHaving = null;
				mOrderBy = null;
				mGroupBy = null;
				this.mTop = null;
			}
		}
		
		//A classe fields guarda um coleção com os campos de um registro.
		internal class Fields : IDisposable, IEnumerable
		{
			
			
			//local variable to hold collection
			private Collection CollIndex;
			
			#region   Start End
			
			public Fields()
			{
				CollIndex = new Collection();
			}
			
			public Fields(field field)
			{
				this.Add(field);
			}
			
			public void Dispose()
			{
				this.dispose();
			}
			
			public void dispose()
			{
				//verificar este código
				Clear();
				if (CollIndex != null)
				{
					while (CollIndex.Count > 0)
					{
						CollIndex.Remove(1);
					}
				}
				CollIndex = null;
			}
			
			private void Clear()
			{
				foreach (field F in CollIndex)
				{
					F.Dispose();
				}
			}
			#endregion
			
			#region   Code collection
			
			public field Add(string caption, object value, System.Type fdType, bool Dirty)
			{
				field field = new field();
				int index;
				
				index = CollIndex.Count + 1;
				field.Caption = caption;
				field.value = value;
				field.fdType = fdType;
				field.Dirty = Dirty;
				
				return Add(field);
			}
			
			public field Add(field field)
			{
				
				//CollIndex.Add(objNewMember, "k=" & Trim(index))
				CollIndex.Add(field, field.Caption.Trim(), null, null);
				
				return field;
			}
			
			public int Count
			{
				get
				{
					return CollIndex.Count;
				}
			}
			
			public Collection Collection
			{
				get
				{
					return CollIndex;
				}
				set
				{
					if (CollIndex != null)
					{
						Clear();
						while (CollIndex.Count > 0)
						{
							CollIndex.Remove(1);
						}
					}
					CollIndex = value;
				}
			}
			
			public field this[string CaptionKey]
			{
				get
				{
					return CollIndex[CaptionKey.Trim()];
				}
			}
			
			public field this[int index]
			{
				get
				{
					return CollIndex[index];
				}
			}
			
			public System.Collections.IEnumerator GetEnumerator()
			{
				//Me.CollIndex = Me.GetAllItems()
				//Return New Iterator(Me.CollIndex)
				return CollIndex.GetEnumerator();
			}
			
			public void Remove(string captionKey)
			{
				CollIndex.Remove(captionKey);
			}
			
			public void getFields(System.Data.DataView objDataView, double row)
			{
				System.Data.DataColumn col;
				System.Data.DataView DV; //Data.DataRowView
				
				try
				{
					//Para nova coleçao utilizar o createfields
					//CollIndex = Nothing
					//CollIndex = New Collection
					if (CollIndex == null)
					{
						//Call createFields(objDataView)
						throw (new Exception("A coleção dos campos na classe Fields está \'nothing\'."));
					}
					else
					{
						if (CollIndex.Count == 0)
						{
							throw (new Exception("A coleção dos campos na classe Fields está \'vazia\'."));
						}
					}
					DV = objDataView;
					try
					{
						System.Data.DataRowView with_1 = DV[row];
						foreach (System.Data.DataColumn tempLoopVar_col in with_1.DataView.Table.Columns)
						{
							col = tempLoopVar_col;
							if (with_1[col.ColumnName].ToString().Trim() == "")
							{
								//Me.Add(col.ColumnName, String.Empty, System.Type.GetType(col.DataType.FullName))
								this[col.ColumnName].value = string.Empty;
							}
							else
							{
								//Me.Add(col.ColumnName, .Item(col.ColumnName), System.Type.GetType(col.DataType.FullName))
								this[col.ColumnName].value = with_1[col.ColumnName];
							}
						}
					}
					catch (Exception)
					{
						try
						{
							foreach (System.Data.DataColumn tempLoopVar_col in DV.Table.Columns)
							{
								col = tempLoopVar_col;
								//Me.Add(col.ColumnName, String.Empty, System.Type.GetType(col.DataType.FullName))
								this[col.ColumnName].value = string.Empty;
							}
						}
						catch (Exception e)
						{
							throw (new Exception("Verifique se há algum problema com o DataSources.", e));
						}
					}
				}
				catch (Exception ex)
				{
					//implementar a gravação de LOG
					throw (new Exception("Não foi possível preencher coleção de campos.", ex));
				}
			}
			
			public void getFields(ref IDataReader pDataReader)
			{
				try
				{
					if (CollIndex == null)
					{
						throw (new NBexception("A coleção dos campos na classe Fields está \'nothing\'."));
					}
					else
					{
						if (CollIndex.Count == 0)
						{
							throw (new NBexception("A coleção dos campos na classe Fields está \'vazia\'."));
						}
					}
					
					if (pDataReader != null)
					{
						pDataReader.Read();
						this.PreencheFields(pDataReader);
						pDataReader.Close();
						pDataReader.Dispose();
						pDataReader = null;
					}
					
				}
				catch (NBexception nbEx)
				{
					throw (nbEx);
				}
				catch (Exception ex)
				{
					//implementar a gravação de LOG
					throw (new NBexception("Não foi possível preencher coleção de campos.", ex));
				}
				
			}
			
			public void createFields(System.Data.DataView objDataView)
			{
				
				try
				{
					CollIndex = null;
					CollIndex = new Collection();
					
					System.Data.DataView DV; //Data.DataRowView
					DV = objDataView;
					try
					{
						foreach (System.Data.DataColumn col in DV.Table.Columns)
						{
							//Me.Add(col.ColumnName, System.Type.GetType(col.DataType.FullName).Missing, System.Type.GetType(col.DataType.FullName))
							this.Add(col.ColumnName, string.Empty, System.Type.GetType(col.DataType.FullName), false);
						}
					}
					catch
					{
						throw (new Exception("Impossível adicionar os campos à coleção."));
					}
				}
				catch (Exception ex)
				{
					//implementar a gravação de LOG
					throw (new Exception("Não foi possível criar coleção de campos.", ex));
				}
			}
			
			public void createFields(ref IDataReader pDataReader)
			{
				try
				{
					for (int i = 0; i <= pDataReader.FieldCount - 1; i++)
					{
						this.Add(pDataReader.GetName(i), string.Empty, System.Type.GetType(pDataReader.GetFieldType(i).FullName), false);
					}
					pDataReader.Close();
					pDataReader.Dispose();
					pDataReader = null;
					self.AdmDB.ConnectionReader.Close();
				}
				catch (Exception ex)
				{
					throw (new NBexception("Impossível adicionar os campos à coleção.", ex));
				}
			}
			
			public void clearFields()
			{
				
				
				Collection fs;
				fs = CollIndex;
				foreach (field f in fs)
				{
					f.value = string.Empty;
					f.Dirty = false;
				}
			}
			
			public void PreencheFields(IDataReader pDataReader)
			{
				for (int i = 0; i <= pDataReader.FieldCount - 1; i++)
				{
					if (pDataReader[i].ToString().Trim() == "")
					{
						this[i + 1].value = string.Empty;
					}
					else
					{
						this[i + 1].value = pDataReader[i];
					}
				}
			}
			
			#endregion
			
			public class field : IDisposable
			{
				
				
				private object mValue;
				public string Caption;
				public System.Type fdType;
				public bool Dirty;
				public object value
				{
					get
					{
						if (mValue != null)
						{
							if (fdType.FullName == "System.Boolean")
							{
								return mValue;
							}
							string n;
							if (Information.IsNumeric(mValue) == true)
							{
								n = mValue.ToString();
								if (self.Settings.sintaxePontoDecimal == ".")
								{
									n = n.Replace(",", ".");
								}
								return n;
							}
							else
							{
								if ((System.Type.GetTypeCode(fdType) != System.TypeCode.String) && (mValue.ToString() == ""))
								{
									if ((System.Type.GetTypeCode(fdType) == System.TypeCode.DateTime) && (mValue.ToString() == ""))
									{
										return null;
									}
									return 0;
								}
								else
								{
									return mValue;
								}
							}
						}
						else
						{
							return mValue;
						}
					}
					set
					{
						mValue = value;
					}
				}
				
				public void Dispose()
				{
					mValue = null;
					Caption = null;
					fdType = null;
					Dirty = null;
				}
			}
			
		}
		
		public class NbCollection : System.Collections.DictionaryBase, IDisposable
		{
			
			
			public void Add(string key, ref object objeto)
			{
				object obj = new object();
				obj = objeto;
				objeto = null;
				this.Dictionary.Add(key, obj);
			}
			public void Remove(string key)
			{
				this.Hastable.Remove(key);
			}
			public System.Collections.ICollection Values
			{
				get
				{
					return this.Dictionary.Values;
				}
			}
			public System.Collections.Hashtable Hastable
			{
				get
				{
					return this.InnerHashtable;
				}
			}
			
			public void Dispose()
			{
				this.Dictionary.Clear();
				this.Hastable.Clear();
			}
		}
		
	}
	
}
