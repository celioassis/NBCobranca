using System.Data;
using System.Collections;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System;


namespace NBdbm
{
	namespace Fachadas
	{
		namespace plxEVT
		{
			
			
			#region ===[ Cadastro de Funcionario ]===
			//===[ Cadastro de Funcionario ]=======================================================================================
			public class CadastroFuncionario : Fachadas.CTR.CadastroUsuario
			{
				
				
				public CadastroFuncionario()
				{
					this.InicializacaoPadrao();
				}
				public CadastroFuncionario(tipos.tiposConection TipoConexao) : base(TipoConexao)
				{
					this.mTipoConexao = TipoConexao;
					this.InicializacaoPadrao();
				}
				public void NovoEndereco()
				{
					this.mEndereco = new NBdbm.Fachadas.CTR.primitivas.Endereco(this.mTipoConexao);
				}
				public void NovoEmail()
				{
					this.mEMail = new NBdbm.Fachadas.CTR.primitivas.eMail(this.mTipoConexao);
				}
				public void NovoTelefone()
				{
					this.mTelefone = new NBdbm.Fachadas.CTR.primitivas.Telefone(this.mTipoConexao);
				}
				public void NovoCelular()
				{
					this.mCelular = new NBdbm.Fachadas.CTR.primitivas.Telefone(this.mTipoConexao);
				}
				public string Source
				{
					get
					{
						return this.mSource;
					}
				}
				private void InicializacaoPadrao()
				{
					mSource = "**** Cadastro de Funcionários ****";
					this.mXmPath_EntNo = "<Entidades><Funcionários>";
				}
			}
			#endregion
			
			#region ===[ Cadastro de Itens ]===
			//===[ cadastro de Estoque Patrimonial ]===============================================================================
			public class CadastroItens : Interfaces.iEVT.iCadastroItens
			{
				
				
				private const string mSource = "**** Cadastro de Itens ****";
				private Fachadas.plxEVT.primitivas.Item mItem;
				private Fachadas.plxEVT.primitivas.LinkItemNo mLinkNo;
				private Fachadas.plxEVT.primitivas.StatusReal mStatusReal;
				private string mXmPathItemNo;
				private int mQuantAtualItem;
				private tipos.tiposConection mTipoConexao;
				
				public CadastroItens()
				{
					mItem = new Fachadas.plxEVT.primitivas.Item();
					mLinkNo = new Fachadas.plxEVT.primitivas.LinkItemNo();
					mStatusReal = new Fachadas.plxEVT.primitivas.StatusReal();
				}
				public CadastroItens(tipos.tiposConection TipoConexao)
				{
					mTipoConexao = TipoConexao;
					mItem = new Fachadas.plxEVT.primitivas.Item(TipoConexao);
					mLinkNo = new Fachadas.plxEVT.primitivas.LinkItemNo(TipoConexao);
					mStatusReal = new Fachadas.plxEVT.primitivas.StatusReal(TipoConexao);
				}
				public void Excluir(bool NoCommit)
				{
					try
					{
						//Excluindo o Kit
						mItem.excluir(NoCommit);
					}
					catch (NBexception ex)
					{
						ex.Source = mSource;
						throw (ex);
					}
				}
				public void Excluir(int idItem)
				{
					mItem.Campos.Clear_filters();
					mItem.filterWhere = "idItem=" + idItem.ToString();
					this.Excluir(false);
				}
				public void Excluir()
				{
					this.Excluir(false);
				}
				public void Salvar()
				{
					Salvar(false);
				}
				public void Salvar(bool NoCommit)
				{
					//implementar: Salvar primeiro o Item do Estoque
					int idItem;
					//Dim strParser As String
					long idNo;
					Fachadas.CTR.primitivas.No ob;
					
					try
					{
						//Verifica se foi usado outro tipo de conexao que não a padrao
						if (mTipoConexao != null)
						{
							ob = new Fachadas.CTR.primitivas.No(mTipoConexao);
						}
						else
						{
							ob = new Fachadas.CTR.primitivas.No();
						}
						
						//Localizando o Nó pelo xmPath
						ob.filterWhere = "xmPath like \'*" + this.mXmPathItemNo + "\'";
						ob.getFields();
						idNo = ob.Campos.idNo_key;
						
						//Salvando o Item.
						this.Item.salvar(true);
						
						//Criando StatusReal
						this.AdicionarItensQuantitativos();
						
						//Salvando o link da Classe do EstoquePatrimonial
						idItem = this.Item.ID;
						this.mLinkNo.Clear_filters();
						this.mLinkNo.Campos.idItem = idItem;
						this.mLinkNo.Campos.idNo = idNo;
						this.mLinkNo.Campos.salvar(true);
						
						//Finaliza a Transação
						self.AdmDB.FinalizaTransaction(NoCommit);
						
					}
					catch (Exception ex)
					{
						ex.Source = CadastroItens.mSource;
						throw (ex);
					}
					
				}
				public System.Data.DataView DataSource(string Filtro)
				{
					if (Filtro != "")
					{
						this.mItem.Clear_filters();
						this.mItem.filterWhere = Filtro;
					}
					return this.mItem.DataSource();
				}
				public Interfaces.iEVT.Primitivas.iItem Item
				{
					get
					{
						return this.mItem.Campos;
					}
					set
					{
						this.mItem.Campos = value;
					}
				}
				public string xmPath_LinkItemNo
				{
					get
					{
						return this.mXmPathItemNo;
					}
					set
					{
						this.mXmPathItemNo = value;
					}
				}
				public void Dispose()
				{
					this.mXmPathItemNo = null;
					this.mLinkNo.Dispose();
					this.mLinkNo = null;
					this.mItem.Dispose();
					this.mItem = null;
					this.mStatusReal.Dispose();
					this.mStatusReal = null;
				}
				public void getFieldsFromItem(string iditem)
				{
					this.mItem.Clear_filters();
					this.mItem.filterWhere = "idItem = " + iditem;
					this.mItem.getFields();
					this.mQuantAtualItem = this.mItem.Campos.Quantidade;
					
					//Localizando o idNó pelo IdEstoque
					this.mLinkNo.filterWhere = "idItem=" + iditem;
					this.mLinkNo.getFields();
					
					//Localizando o Nó pelo xmPath
					Fachadas.CTR.primitivas.No ob;
					//Verifica se foi usado outro tipo de conexao que não a padrao
					if (mTipoConexao != null)
					{
						ob = new Fachadas.CTR.primitivas.No(mTipoConexao);
					}
					else
					{
						ob = new Fachadas.CTR.primitivas.No();
					}
					
					ob.filterWhere = "idNo=" + mLinkNo.Campos.idNo.ToString();
					ob.getFields();
					this.mXmPathItemNo = ob.Campos.xmPath_key;
					
					
				}
				public void AlterarKey()
				{
					try
					{
						this.mItem.filterWhere = "idItem=" + this.Item.ID.ToString();
						this.mItem.editar(false);
					}
					catch (NBexception ex)
					{
						ex.Source = CadastroItens.mSource;
						throw (ex);
					}
				}
				public void AdicionarItensQuantitativos()
				{
					int mInicio;
					
					if (this.mItem.Inclusao == true)
					{
						mInicio = 1;
					}
					else
					{
						mInicio = this.mQuantAtualItem + 1;
					}
					for (int cont = mInicio; cont <= this.Item.Quantidade; cont++)
					{
						mStatusReal.Campos.IdItem = this.Item.ID;
						mStatusReal.Campos.IdObj = "I" + this.Item.ID.ToString();
						mStatusReal.Campos.CodBarras = "I" + Strings.Format(this.Item.ID, "0000000") + Strings.Format(cont, "0000000");
						mStatusReal.Campos.Status = 3;
						mStatusReal.Campos.salvar(true);
					}
				}
            }
			#endregion
			
			#region ===[ Cadastro de Localidades ]===
			//===[ cadastro de Localidades ]=======================================================================================
			public class CadastroLocalidades : Fachadas.CTR.CadastroEntidade, Interfaces.iEVT.iCadastroLocalidades
			{
				
				
				private new string Const = "**** Cadastro de Localidades ****";
				protected Fachadas.plxEVT.primitivas.Localidades mLocalidade;
				
				public CadastroLocalidades()
				{
					mLocalidade = new Fachadas.plxEVT.primitivas.Localidades();
				}
				public CadastroLocalidades(tipos.tiposConection TipoConexao)
				{
					mLocalidade = new Fachadas.plxEVT.primitivas.Localidades(TipoConexao);
				}
				
				public override void getFieldsFromEntidade(double idEntidade)
				{
					
					base.getFieldsFromEntidade(idEntidade);
					
					this.mLocalidade.filterWhere = "idEntidade = " + idEntidade;
					this.mLocalidade.getFields();
					
				}
				
				public void getFieldsFromLocalidade(double idLocalidade)
				{
					
					this.mLocalidade.filterWhere = "idLocalidade = " + idLocalidade;
					this.mLocalidade.getFields();
					
					base.getFieldsFromEntidade(this.Localidade.idEntidade);
					
				}
				
				public void Salvar(bool NoCommit)
				{
					double idE;
					try
					{
						
						//Salva a Entidade
						base.Salvar(true);
						idE = this.Entidade.ID;
						
						//Salva a Localidade
						this.Localidade.idEntidade = idE;
						this.Localidade.salvar(true);
						
						//Finaliza a Transação
						self.AdmDB.FinalizaTransaction(NoCommit);
						
					}
					catch (Exception ex)
					{
						ex.Source = CadastroLocalidades.mSource;
						throw (ex);
					}
				}
				
				public void Salvar()
				{
					this.Salvar(false);
				}
				
				public void Excluir(bool NoCommit)
				{
					try
					{
						//Excluindo a Entidade
						base.Excluir(true);
						//Excluindo a Localidade
						this.mLocalidade.excluir(NoCommit);
					}
					catch (NBexception ex)
					{
						ex.Source = CadastroLocalidades.mSource;
						throw (ex);
					}
				}
				
				public void Excluir()
				{
					this.Excluir(false);
				}
				
				public Interfaces.iEVT.Primitivas.iLocalidades Localidade
				{
					get
					{
						return mLocalidade.Campos;
					}
					set
					{
						mLocalidade.Campos = value;
					}
				}
				public override void Dispose()
				{
					mLocalidade.Dispose();
					mLocalidade = null;
					base.Dispose();
				}
				
			}
			#endregion
			
			#region ===[ Cadastro de Kits ]===
			//===[ cadastro de Kits ]=======================================================================================
			public class CadastroKit : Interfaces.iEVT.iCadatroKit
			{
				
				//Inherits Fachadas.allClass
				
				private const string mSource = "**** Cadastro de Kits ****";
				protected Fachadas.NbCollection mColKitItem = new Fachadas.NbCollection();
				protected Fachadas.plxEVT.primitivas.kit mKit;
				protected Fachadas.plxEVT.primitivas.KitItem mKitItem;
				protected tipos.tiposConection mTipoConexao;
				private Fachadas.plxEVT.primitivas.StatusReal mStatusReal;
				
				
				public CadastroKit()
				{
					mKit = new Fachadas.plxEVT.primitivas.kit();
					mKitItem = new Fachadas.plxEVT.primitivas.KitItem();
					mStatusReal = new Fachadas.plxEVT.primitivas.StatusReal();
				}
				public CadastroKit(tipos.tiposConection TipoConexao)
				{
					mTipoConexao = TipoConexao;
					mKit = new Fachadas.plxEVT.primitivas.kit(TipoConexao);
					mKitItem = new Fachadas.plxEVT.primitivas.KitItem(TipoConexao);
					mStatusReal = new Fachadas.plxEVT.primitivas.StatusReal(TipoConexao);
				}
				
				public void Dispose()
				{
					mColKitItem.Dispose();
					mColKitItem = null;
					mKit.Dispose();
					mKit = null;
					mKitItem.Dispose();
					mKitItem = null;
					mTipoConexao = null;
				}
				
				public void Excluir(bool NoCommit)
				{
					try
					{
						//Excluindo o Kit
						this.mKit.excluir(NoCommit);
					}
					catch (NBexception ex)
					{
						ex.Source = mSource;
						throw (ex);
					}
				}
				
				public void Excluir()
				{
					this.Excluir(false);
				}
				
				public void Salvar(bool NoCommit)
				{
					this.salvar(NoCommit);
				}
				
				public void salvar(bool NoCommit)
				{
					//implementar: Salvar primeiro o Kit, depois os Itens do Kit
					double idK;
					try
					{
						//Salvando o Kit
						this.Kit.salvar(true);
						
						//Criando StatusReal
						if (this.mKit.Inclusao == true)
						{
							int cont;
							for (cont = 1; cont <= this.Kit.Quantidade; cont++)
							{
								mStatusReal.Campos.IdKit = this.Kit.ID;
								mStatusReal.Campos.IdObj = "K" + this.Kit.ID.ToString();
								mStatusReal.Campos.CodBarras = "K" + Strings.Format(this.Kit.ID, "0000000") + Strings.Format(cont, "0000000");
								mStatusReal.Campos.Status = 3;
								mStatusReal.Campos.salvar(true);
							}
						}
						
						//Salvando os Itens do Kit
						idK = this.Kit.ID;
						foreach (Interfaces.iEVT.Primitivas.iKitItem kItem in mColKitItem.Values)
						{
							kItem.idKit_key = idK;
							kItem.salvar(true);
						}
						
						
						//Finaliza a Transação
						self.AdmDB.FinalizaTransaction(NoCommit);
						
					}
					catch (Exception ex)
					{
						NBexception mNBEx = new NBexception("Não foi possível salvar o Kit - " + this.Kit.Nome_key, ex);
						mNBEx.Source = "NBdbm.Fachadas.plxEVT.CadastroKit.Salvar";
						throw (mNBEx);
					}
				}
				
				public void Salvar()
				{
					this.salvar(false);
				}
				
				public NbCollection colecaoKitItens
				{
					get
					{
						return mColKitItem;
					}
				}
				
				public virtual void getFieldsFromKit(double idKit)
				{
					
					this.mColKitItem.Clear();
					this.mKit.filterWhere = " IdKit = " + idKit;
					this.mKit.getFields();
					this.mKitItem.filterWhere = " idKit = " + idKit;
					if (this.mKitItem.DataSource.Count > 0)
					{
						foreach (System.Data.DataRow DR in this.mKitItem.DataSource.Table.Rows)
						{
							NBdbm.Fachadas.plxEVT.primitivas.KitItem KItem;
							Fachadas.plxEVT.primitivas.Item Item;
							//Verifica se foi usado outro tipo de conexao que não a padrao
							if (mTipoConexao != null)
							{
								KItem = new NBdbm.Fachadas.plxEVT.primitivas.KitItem(mTipoConexao);
								Item = new Fachadas.plxEVT.primitivas.Item(mTipoConexao);
							}
							else
							{
								KItem = new NBdbm.Fachadas.plxEVT.primitivas.KitItem();
								Item = new Fachadas.plxEVT.primitivas.Item();
							}
							KItem.filterWhere = " idKit = " + idKit + " and IdKitItem = " + DR["IdKitItem"].ToString();
							KItem.getFields();
							Item.filterWhere = " idItem = " + int.Parse(KItem.Campos.CodBarrasItem_key.Substring(1, 7)).ToString();
							Item.getFields();
							KItem.Campos.Descricao = Item.Campos.Descricao_key;
							KItem.Campos.ValorLocacao = Item.Campos.ValorLocacao;
							this.KitItem = KItem.Campos; //preenchendo a property com o último.
							this.colecaoKitItens.Add(KItem.Campos.Key, ((object) KItem.Campos));
						}
					}
					else
					{
						mKitItem.Clear_vars();
					}
				}
				
				public Interfaces.iEVT.Primitivas.iKit Kit
				{
					get
					{
						return mKit.campos;
					}
					set
					{
						mKit.campos = value;
					}
				}
				
				public Interfaces.iEVT.Primitivas.iKitItem KitItem
				{
					get
					{
						if (mKitItem == null)
						{
							mKitItem = new Fachadas.plxEVT.primitivas.KitItem();
						}
						if (mKitItem.Campos == null)
						{
							mKitItem = new Fachadas.plxEVT.primitivas.KitItem();
						}
						return mKitItem.Campos;
					}
					set
					{
						mKitItem.Campos = value;
					}
				}
			}
			#endregion
			
			#region ===[ Cadastro de Orçamento ]===
			//===[ cadastro de Ordem de Serviços ]====================================================================================
			public class CadastroOrcamento : Interfaces.iEVT.iCadatroOrcamento
			{
				
				//Inherits Fachadas.allClass
				
				private const string mSource = "**** Cadastro de Orçamento ****";
				protected Fachadas.NbCollection mColOrcItem = new Fachadas.NbCollection();
				protected Fachadas.NbCollection mColOrcKit = new Fachadas.NbCollection();
				protected Fachadas.NbCollection mColOrcDespAdic = new Fachadas.NbCollection();
				protected Fachadas.NbCollection mColOrcParcelas = new Fachadas.NbCollection();
				protected Fachadas.plxEVT.primitivas.Orcamento mOrc;
				protected Fachadas.plxEVT.primitivas.OrcamentoObjs mOrcItem;
				protected Fachadas.plxEVT.primitivas.OrcamentoObjs mOrcKit;
				protected Fachadas.plxEVT.primitivas.OrcamentoDespAdicional mOrcDespAdic;
				protected Fachadas.plxEVT.primitivas.OrcamentoParcelas mOrcParcela;
				protected tipos.tiposConection mTipoConexao;
				
				public CadastroOrcamento()
				{
					this.mOrc = new Fachadas.plxEVT.primitivas.Orcamento();
					this.mOrcItem = new Fachadas.plxEVT.primitivas.OrcamentoObjs();
					this.mOrcKit = new Fachadas.plxEVT.primitivas.OrcamentoObjs();
					this.mOrcDespAdic = new Fachadas.plxEVT.primitivas.OrcamentoDespAdicional();
					this.mOrcParcela = new Fachadas.plxEVT.primitivas.OrcamentoParcelas();
				}
				public CadastroOrcamento(tipos.tiposConection TipoConexao)
				{
					this.mTipoConexao = TipoConexao;
					this.mOrc = new Fachadas.plxEVT.primitivas.Orcamento(TipoConexao);
					this.mOrcItem = new Fachadas.plxEVT.primitivas.OrcamentoObjs(TipoConexao);
					this.mOrcKit = new Fachadas.plxEVT.primitivas.OrcamentoObjs(TipoConexao);
					this.mOrcDespAdic = new Fachadas.plxEVT.primitivas.OrcamentoDespAdicional(TipoConexao);
					this.mOrcParcela = new Fachadas.plxEVT.primitivas.OrcamentoParcelas(TipoConexao);
				}
				public void Dispose()
				{
					if (this.mColOrcItem != null)
					{
						this.mColOrcItem.Dispose();
					}
					this.mColOrcItem = null;
					if (this.mColOrcKit != null)
					{
						this.mColOrcKit.Dispose();
					}
					this.mColOrcKit = null;
					if (this.mColOrcDespAdic != null)
					{
						this.mColOrcDespAdic.Dispose();
					}
					this.mColOrcDespAdic = null;
					if (this.mOrc != null)
					{
						this.mOrc.Dispose();
					}
					this.mOrc = null;
					if (this.mOrcItem != null)
					{
						this.mOrcItem.Dispose();
					}
					this.mOrcItem = null;
					if (this.mOrcKit != null)
					{
						this.mOrcKit.Dispose();
					}
					this.mOrcKit = null;
					if (this.mOrcDespAdic != null)
					{
						this.mOrcDespAdic.Dispose();
					}
					this.mOrcDespAdic = null;
					if (this.mOrcParcela != null)
					{
						this.mOrcParcela.Dispose();
					}
					this.mOrcParcela = null;
				}
				public void Excluir(bool NoCommit)
				{
					try
					{
						mOrc.excluir(NoCommit);
					}
					catch (NBexception ex)
					{
						ex.Source = mSource;
						throw (ex);
					}
				}
				public void Excluir()
				{
					this.Excluir(false);
				}
				public void Salvar(bool NoCommit)
				{
					//implementar: Salvar primeiro o Orçamento, depois os Itens, Kits, Despesas Adicionais e Parcelas.
					double idOrc;
					try
					{
						
						//Salvando o Orçamento
						this.Orc.salvar(true);
						idOrc = this.Orc.ID;
						
						//Salvando os Itens do Orçamento
						foreach (Interfaces.iEVT.Primitivas.iOrcamentoObjs OrcItem in mColOrcItem.Values)
						{
							OrcItem.IdOrcamento_key = idOrc;
							OrcItem.salvar(true);
							if (this.Orc.IsModelo == false)
							{
								OrcItem.CtrObj.SalvarPresumido(true, idOrc, OrcItem.ID);
							}
						}
						
						//Salvando os Kits do Orçamento
						foreach (Interfaces.iEVT.Primitivas.iOrcamentoObjs OrcKit in mColOrcKit.Values)
						{
							OrcKit.IdOrcamento_key = idOrc;
							OrcKit.salvar(true);
							if (this.Orc.IsModelo == false)
							{
								OrcKit.CtrObj.SalvarPresumido(true, idOrc, OrcKit.ID);
							}
						}
						
						//Salvando as Despesas Adicionais do Orçamento
						foreach (Interfaces.iEVT.Primitivas.iOrcamentoDespAdicional OrcDespAdic in mColOrcDespAdic.Values)
						{
							OrcDespAdic.IdOrcamento_Key = idOrc;
							OrcDespAdic.salvar(true);
						}
						
						//Salvando o Parcelamento do Orçamento.
						foreach (Interfaces.iEVT.Primitivas.iOrcamentoParcelas OrcParcela in this.mColOrcParcelas.Values)
						{
							OrcParcela.IdOrcamento_key = idOrc;
							OrcParcela.salvar(true);
						}
						
						//Finaliza a Transação
						self.AdmDB.FinalizaTransaction(NoCommit);
						
					}
					catch (NBexception ex)
					{
						ex.Source = mSource;
						throw (ex);
					}
					
				}
				public void Salvar()
				{
					this.Salvar(false);
				}
				public NbCollection colecaoOrcItens
				{
					get
					{
						return mColOrcItem;
					}
				}
				public NbCollection colecaoOrcKits
				{
					get
					{
						return mColOrcKit;
					}
				}
				public NbCollection colecaoOrcDespAdic
				{
					get
					{
						return mColOrcDespAdic;
					}
				}
				public NbCollection colecaoOrcParcelas
				{
					get
					{
						return this.mColOrcParcelas;
					}
				}
				
				public virtual void getFieldsFromOrc(double idOrc)
				{
					System.Data.DataRow DR;
					this.mOrc.filterWhere = " idOrcamento = " + idOrc;
					this.mOrc.getFields();
					
					//=== Carrega os Dados dos Itens ===
					this.mOrcItem.filterWhere = " idOrcamento = " + idOrc + " AND idObj LIKE \'I%\'";
					this.mOrcItem.getFields();
					if (this.mOrcItem.DataSource.Count > 0)
					{
						foreach (System.Data.DataRow tempLoopVar_DR in this.mOrcItem.DataSource.Table.Rows)
						{
							DR = tempLoopVar_DR;
							NBdbm.Fachadas.plxEVT.primitivas.OrcamentoObjs OrcItem;
							//Verifica se esta sendo usado outro Tipo de Conexão que não seja a Padrão.
							if (mTipoConexao != null)
							{
								OrcItem = new NBdbm.Fachadas.plxEVT.primitivas.OrcamentoObjs(mTipoConexao);
							}
							else
							{
								OrcItem = new NBdbm.Fachadas.plxEVT.primitivas.OrcamentoObjs();
							}
							OrcItem.filterWhere = " idorcamento = " + idOrc + " and idObj = \'" + DR["idObj"].ToString() + "\'";
							OrcItem.getFields();
							this.mOrcItem.Campos = OrcItem.Campos; //preenchendo a property com o último.
							this.colecaoOrcItens.Add(OrcItem.Campos.Key, ((object) OrcItem.Campos));
						}
					}
					else
					{
						mOrcItem.Clear_vars();
					}
					
					//=== Carrega os Dados dos Kits ===
					this.mOrcKit.filterWhere = " idOrcamento = " + idOrc + " AND idObj LIKE \'K%\'";
					this.mOrcKit.getFields();
					if (this.mOrcKit.DataSource.Count > 0)
					{
						foreach (System.Data.DataRow tempLoopVar_DR in this.mOrcKit.DataSource.Table.Rows)
						{
							DR = tempLoopVar_DR;
							NBdbm.Fachadas.plxEVT.primitivas.OrcamentoObjs OrcKit;
							//Verifica se esta sendo usado outro Tipo de Conexão que não seja a Padrão.
							if (mTipoConexao != null)
							{
								OrcKit = new NBdbm.Fachadas.plxEVT.primitivas.OrcamentoObjs(mTipoConexao);
							}
							else
							{
								OrcKit = new NBdbm.Fachadas.plxEVT.primitivas.OrcamentoObjs();
							}
							OrcKit.filterWhere = " idorcamento = " + idOrc + " and idObj = \'" + DR["idObj"].ToString() + "\'";
							OrcKit.getFields();
							this.mOrcItem.Campos = OrcKit.Campos; //preenchendo a property com o último.
							this.colecaoOrcKits.Add(OrcKit.Campos.Key, ((object) OrcKit.Campos));
						}
					}
					else
					{
						mOrcItem.Clear_vars();
					}
					
					//=== Carrega os Dados das Despesas Adicionais ===
					this.mOrcDespAdic.filterWhere = " idOrcamento = " + idOrc;
					this.mOrcDespAdic.getFields();
					if (this.mOrcDespAdic.DataSource.Count > 0)
					{
						foreach (System.Data.DataRow tempLoopVar_DR in this.mOrcDespAdic.DataSource.Table.Rows)
						{
							DR = tempLoopVar_DR;
							NBdbm.Fachadas.plxEVT.primitivas.OrcamentoDespAdicional OrcDespAdic;
							//Verifica se esta sendo usado outro tipo de conexão que não seja a padrão.
							if (mTipoConexao != null)
							{
								OrcDespAdic = new NBdbm.Fachadas.plxEVT.primitivas.OrcamentoDespAdicional(mTipoConexao);
							}
							else
							{
								OrcDespAdic = new NBdbm.Fachadas.plxEVT.primitivas.OrcamentoDespAdicional();
							}
							OrcDespAdic.filterWhere = " idorcamento = " + idOrc + " and idDespAdicional = " + DR["idDespAdicional"].ToString();
							OrcDespAdic.getFields();
							this.mOrcDespAdic.Campos = OrcDespAdic.Campos; //preenchendo a property com o último.
							this.colecaoOrcDespAdic.Add(OrcDespAdic.Campos.Key, ((object) OrcDespAdic.Campos));
						}
					}
					else
					{
						mOrcDespAdic.Clear_vars();
					}
					
					//" === Carrega os Dados de Parcelamentos ==="
					this.mOrcParcela.filterWhere = " idOrcamento = " + idOrc;
					this.mOrcParcela.getFields();
					if (this.mOrcParcela.DataSource.Count > 0)
					{
						foreach (System.Data.DataRow tempLoopVar_DR in this.mOrcParcela.DataSource.Table.Rows)
						{
							DR = tempLoopVar_DR;
							NBdbm.Fachadas.plxEVT.primitivas.OrcamentoParcelas OrcParcela;
							//Verifica se esta sendo usado outro tipo de conexão que não seja a padrão.
							if (mTipoConexao != null)
							{
								OrcParcela = new NBdbm.Fachadas.plxEVT.primitivas.OrcamentoParcelas(mTipoConexao);
							}
							else
							{
								OrcParcela = new NBdbm.Fachadas.plxEVT.primitivas.OrcamentoParcelas();
							}
							OrcParcela.filterWhere = " idorcamento = " + idOrc + " and idparcela = " + DR["idparcela"].ToString();
							OrcParcela.getFields();
							this.mOrcParcela.Campos = OrcParcela.Campos; //preenchendo a property com o último.
							this.colecaoOrcParcelas.Add(OrcParcela.Campos.Key, ((object) OrcParcela.Campos));
						}
					}
					else
					{
						mOrcParcela.Clear_vars();
					}
				}
				public void LimpaColecoes()
				{
					this.mColOrcItem.Clear();
					this.mColOrcKit.Clear();
					this.mColOrcDespAdic.Clear();
					this.mColOrcParcelas.Clear();
				}
				public void LimpaVars()
				{
					this.mOrc.Clear_vars();
					this.mOrcDespAdic.Clear_vars();
					this.mOrcItem.Clear_vars();
					this.mOrcKit.Clear_vars();
				}
				public Interfaces.iEVT.Primitivas.iOrcamento Orc
				{
					get
					{
						return mOrc.campos;
					}
					set
					{
						mOrc.campos = value;
					}
				}
				public Interfaces.iEVT.Primitivas.iOrcamentoObjs OrcItem
				{
					get
					{
						return mOrcItem.Campos;
					}
					set
					{
						mOrcItem.Campos = value;
					}
				}
				public Interfaces.iEVT.Primitivas.iOrcamentoObjs OrcKit
				{
					get
					{
						return mOrcKit.Campos;
					}
					set
					{
						mOrcKit.Campos = value;
					}
				}
				public Interfaces.iEVT.Primitivas.iOrcamentoDespAdicional OrcDespAdic
				{
					get
					{
						return mOrcDespAdic.Campos;
					}
					set
					{
						mOrcDespAdic.Campos = value;
					}
				}
				public Interfaces.iEVT.Primitivas.iOrcamentoParcelas OrcParcela
				{
					get
					{
						return this.mOrcParcela.Campos;
					}
					set
					{
						this.mOrcParcela.Campos = value;
					}
				}
			}
			#endregion
			
			#region ===[ Cadastro de Ordem de Serviço ]==============================================================
			public class CadastroOrdemServico : Interfaces.iEVT.iCadatroOrdemServico
			{
				
				//Inherits Fachadas.allClass
				
				private const string mSource = "**** Cadastro de Ordem de Serviço ****";
				protected Fachadas.NbCollection mColOSObjs = new Fachadas.NbCollection();
				protected Fachadas.plxEVT.primitivas.OrdemServico mOS;
				protected Fachadas.plxEVT.primitivas.OrdemServicoObjs mOSObj;
				protected tipos.tiposConection mTipoConexao;
				
				public CadastroOrdemServico()
				{
					this.mOS = new Fachadas.plxEVT.primitivas.OrdemServico();
					this.mOSObj = new Fachadas.plxEVT.primitivas.OrdemServicoObjs();
				}
				public CadastroOrdemServico(tipos.tiposConection TipoConexao)
				{
					this.mTipoConexao = TipoConexao;
					this.mOS = new Fachadas.plxEVT.primitivas.OrdemServico(TipoConexao);
					this.mOSObj = new Fachadas.plxEVT.primitivas.OrdemServicoObjs(TipoConexao);
				}
				public void Dispose()
				{
					if (this.mColOSObjs != null)
					{
						this.mColOSObjs.Dispose();
					}
					this.mColOSObjs = null;
					if (this.mOS != null)
					{
						this.mOS.Dispose();
					}
					this.mOS = null;
					if (this.mOSObj != null)
					{
						this.mOSObj.Dispose();
					}
					this.mOSObj = null;
				}
				public void Excluir(bool NoCommit)
				{
					try
					{
						//Excluindo o Kit
						mOS.excluir(NoCommit);
					}
					catch (NBexception ex)
					{
						ex.Source = mSource;
						throw (ex);
					}
				}
				public void Excluir()
				{
					this.Excluir(false);
				}
				public void Salvar(bool NoCommit)
				{
					//implementar: Salvar primeiro o Orçamento, depois os Itens, Kits, Despesas Adicionais e Parcelas.
					double idOS;
					try
					{
						
						//Salvando o Orçamento
						this.OS.salvar(true);
						idOS = this.OS.ID;
						
						//Salvando os Objetos da Ordem Serviço
						foreach (Interfaces.iEVT.Primitivas.iOrdemServicoObjs OSOBJ in mColOSObjs.Values)
						{
							OSOBJ.IdOS = idOS;
							OSOBJ.salvar(true);
						}
						
						//Finaliza a Transação
						self.AdmDB.FinalizaTransaction(NoCommit);
						
					}
					catch (NBexception ex)
					{
						ex.Source = mSource;
						throw (ex);
					}
				}
				public void Salvar()
				{
					this.Salvar(false);
				}
				public NbCollection ColecaoOSObjs
				{
					get
					{
						return mColOSObjs;
					}
				}
				
				public virtual void getFieldsFromOS(double idOS)
				{
					
					this.mOS.filterWhere = " idOS = " + idOS;
					this.mOS.getFields();
					
					//=== Carrega os Dados dos Itens ===
					this.mOSObj.filterWhere = " idOrcamento = " + idOS + " AND idObj LIKE \'I%\'";
					this.mOSObj.getFields();
					if (this.mOSObj.DataSource.Count > 0)
					{
						foreach (System.Data.DataRow DR in this.mOSObj.DataSource.Table.Rows)
						{
							NBdbm.Fachadas.plxEVT.primitivas.OrdemServicoObjs OSObj;
							//Verifica se esta sendo usado outro Tipo de Conexão que não seja a Padrão.
							if (mTipoConexao != null)
							{
								OSObj = new NBdbm.Fachadas.plxEVT.primitivas.OrdemServicoObjs(mTipoConexao);
							}
							else
							{
								OSObj = new NBdbm.Fachadas.plxEVT.primitivas.OrdemServicoObjs();
							}
							OSObj.filterWhere = " idorcamento = " + idOS + " and idObj = " + DR["idObj"].ToString();
							OSObj.getFields();
							this.OSObj = OSObj.Campos; //preenchendo a property com o último.
							this.ColecaoOSObjs.Add(OSObj.Campos.Key, ((object) OSObj.Campos));
						}
					}
					else
					{
						mOSObj.Clear_vars();
					}
				}
				public void LimpaColecoes()
				{
					this.mColOSObjs.Clear();
				}
				public void LimpaVars()
				{
					this.mOS.Clear_vars();
					this.mOSObj.Clear_vars();
				}
				public Interfaces.iEVT.Primitivas.iOrdemServico OS
				{
					get
					{
						return mOS.campos;
					}
					set
					{
						mOS.campos = value;
					}
				}
				public Interfaces.iEVT.Primitivas.iOrdemServicoObjs OSObjs
				{
					get
					{
						return this.OSObj;
					}
					set
					{
						this.OSObj = value;
					}
				}
				
				public Interfaces.iEVT.Primitivas.iOrdemServicoObjs OSObj
				{
					get
					{
						return mOSObj.Campos;
					}
					set
					{
						mOSObj.Campos = value;
					}
				}
			}
			#endregion
			
			#region ===[ cadastro de Contas a Pagar e a Receber ]===
			//===[ cadastro de Contas a Pagar e a Receber ]=======================================================================================
			public class CadastroContasPagarReceber : Interfaces.iEVT.iCadastroContasPagarReceber
			{
				
				
				private const string mSource = "**** Cadastro de Contas a Pagar e a Receber ****";
				protected Fachadas.NbCollection aParcelas = new Fachadas.NbCollection();
				protected Fachadas.plxEVT.primitivas.ContasPagarReceber aConta;
				protected Fachadas.plxEVT.primitivas.ContasPagarReceber_Parcelas aParcela;
				protected tipos.tiposConection aTipoConexao;
				
				
				public CadastroContasPagarReceber()
				{
					this.aConta = new Fachadas.plxEVT.primitivas.ContasPagarReceber();
					this.aParcela = new Fachadas.plxEVT.primitivas.ContasPagarReceber_Parcelas();
				}
				
				public CadastroContasPagarReceber(tipos.tiposConection TipoConexao)
				{
					aTipoConexao = TipoConexao;
					this.aConta = new Fachadas.plxEVT.primitivas.ContasPagarReceber(TipoConexao);
					this.aParcela = new Fachadas.plxEVT.primitivas.ContasPagarReceber_Parcelas(TipoConexao);
				}
				
				public void Dispose()
				{
					this.aParcelas.Dispose();
					this.aParcelas = null;
					this.aParcela.Dispose();
					this.aParcela = null;
					aTipoConexao = null;
				}
				
				public virtual void getFieldsFromConta(double idconta)
				{
					
					this.aConta.filterWhere = " ID = " + idconta;
					this.aConta.getFields();
					this.aParcela.filterWhere = " idConta = " + idconta;
					if (this.aParcela.DataSource.Count > 0)
					{
						foreach (System.Data.DataRow DR in this.aParcela.DataSource.Table.Rows)
						{
							NBdbm.Fachadas.plxEVT.primitivas.ContasPagarReceber_Parcelas parcela;
							//Verifica se foi usado outro tipo de conexao que não a padrao
							if (this.aTipoConexao != null)
							{
								parcela = new NBdbm.Fachadas.plxEVT.primitivas.ContasPagarReceber_Parcelas(this.aTipoConexao);
							}
							else
							{
								parcela = new NBdbm.Fachadas.plxEVT.primitivas.ContasPagarReceber_Parcelas();
							}
							parcela.filterWhere = " idConta = " + idconta + " and ID = " + DR["ID"].ToString();
							parcela.getFields();
							this.aParcela = parcela.Campos; //preenchendo a property com o último.
							this.aParcelas.Add(Parcela.Campos.Key, ((object) parcela.Campos));
						}
					}
					else
					{
						this.aParcela.Clear_vars();
					}
				}
				
				public Interfaces.iEVT.Primitivas.iContasPagarReceber Conta
				{
					get
					{
						return this.aConta.Campos;
					}
					set
					{
						this.aConta.Campos = value;
					}
				}
				
				public void Excluir(bool NoCommit)
				{
					try
					{
						//Excluindo o Kit
						this.aConta.excluir(NoCommit);
					}
					catch (NBexception ex)
					{
						ex.Source = mSource;
						throw (ex);
					}
					
				}
				
				public void Excluir()
				{
					this.Excluir(false);
				}
				
				public void Salvar(bool NoCommit)
				{
					double idConta;
					try
					{
						
						//Salvando a Conta
						this.aConta.Campos.salvar(true);
						idConta = this.aConta.Campos.ID;
						
						//Salvando as Parcelas da Conta
						foreach (Interfaces.iEVT.Primitivas.iContasPagarReceber_Parcelas parcela in this.aParcelas.Values)
						{
							parcela.idConta = idConta;
							parcela.salvar(true);
						}
						
						//Finaliza a Transação
						self.AdmDB.FinalizaTransaction(NoCommit);
						
					}
					catch (Exception ex)
					{
						ex.Source = mSource;
						throw (ex);
					}
				}
				
				public void Salvar()
				{
					this.Salvar(false);
				}
				
				public Interfaces.iEVT.Primitivas.iContasPagarReceber_Parcelas Parcela
				{
					get
					{
						return this.aParcela.Campos;
					}
					set
					{
						this.aParcela.Campos = value;
					}
				}
				
				public NbCollection Parcelas
				{
					get
					{
						return this.aParcelas;
					}
				}
				
			}
			#endregion
			
			#region ===[ Cadastro de Relatórios Customizados ]===
			//Criador - Célio em 24/03/2006
			
			public class CadastroCustomROrc : Interfaces.iEVT.iCadastroCustomROrc
			{
				
				
				private const string mSource = "**** Cadastro de Relatórios de Orçamento - Customizados ****";
				private Fachadas.plxEVT.primitivas.CustomROrc mCustomROrc;
				private Fachadas.plxEVT.primitivas.CustomROrc_Grupos mCustomROrc_Grupos;
				private Fachadas.plxEVT.primitivas.CustomROrc_ItensGrupo mCustomROrc_ItemGrupo;
				private System.Collections.Hashtable mColCustomROrc_Grupos;
				private System.Collections.Hashtable mColCustomROrc_ItensGrupo;
				
				public CadastroCustomROrc() : this(tipos.tiposConection.Default_)
				{
				}
				public CadastroCustomROrc(tipos.tiposConection tipoconexao)
				{
					mCustomROrc = new Fachadas.plxEVT.primitivas.CustomROrc(tipoconexao);
					mCustomROrc_Grupos = new Fachadas.plxEVT.primitivas.CustomROrc_Grupos(tipoconexao);
					mCustomROrc_ItemGrupo = new Fachadas.plxEVT.primitivas.CustomROrc_ItensGrupo(tipoconexao);
					mColCustomROrc_Grupos = new System.Collections.Hashtable();
					mColCustomROrc_ItensGrupo = new System.Collections.Hashtable();
				}
				public System.Collections.Hashtable ColCustomROrc_Grupos
				{
					get
					{
						return this.mColCustomROrc_Grupos;
					}
				}
				
				public System.Collections.Hashtable ColCustomROrc_ItensGrupo
				{
					get
					{
						return this.mColCustomROrc_ItensGrupo;
					}
				}
				
				public Interfaces.iEVT.Primitivas.iCustomROrc CustomROrc
				{
					get
					{
						return this.mCustomROrc.Campos;
					}
					set
					{
						this.mCustomROrc = value;
					}
				}
				
				public Interfaces.iEVT.Primitivas.iCustomROrc_Grupos CustomROrc_Grupos
				{
					get
					{
						return this.mCustomROrc_Grupos.Campos;
					}
					set
					{
						this.mCustomROrc_Grupos = value;
					}
				}
				
				public Interfaces.iEVT.Primitivas.iCustomROrc_ItensGrupo CustomROrc_ItemGrupo
				{
					get
					{
						return this.mCustomROrc_ItemGrupo.Campos;
					}
					set
					{
						this.mCustomROrc_ItemGrupo.Campos = value;
					}
				}
				
				public void Excluir()
				{
					this.Excluir(false);
				}
				
				public void Excluir(bool nocommit)
				{
					this.mCustomROrc.excluir(nocommit);
				}
				
				public void Salvar()
				{
					this.Salvar(false);
				}
				
				public void Salvar(bool nocommit)
				{
					int idCustomOrc;
					
					try
					{
						//Salvando o CustomOrc
						this.CustomROrc.salvar(true);
						
						//Salvando os Grupos de CustomOrc.
						foreach (Interfaces.iEVT.Primitivas.iCustomROrc_Grupos Grupo in this.mColCustomROrc_Grupos.Values)
						{
							Grupo.IdCustomRO = idCustomOrc;
							Grupo.salvar(true);
							//Salvando os Itens do Grupo.
							foreach (Interfaces.iEVT.Primitivas.iCustomROrc_ItensGrupo ItemGrupo in this.mColCustomROrc_ItensGrupo)
							{
								ItemGrupo.Salvar(true);
							}
						}
						
						//Finaliza a Transação
						self.AdmDB.FinalizaTransaction(nocommit);
						
					}
					catch (NBexception ex)
					{
						ex.Source = mSource;
						throw (ex);
					}
					
				}
				
				public void Dispose()
				{
					if (this.mCustomROrc != null)
					{
						this.mCustomROrc.Dispose();
					}
					this.mCustomROrc = null;
					
					if (this.mCustomROrc_Grupos != null)
					{
						this.mCustomROrc_Grupos.Dispose();
					}
					this.mCustomROrc_Grupos = null;
					
					if (this.mCustomROrc_ItemGrupo != null)
					{
						this.mCustomROrc_ItemGrupo.Dispose();
					}
					this.mCustomROrc_ItemGrupo = null;
					
					if (this.mColCustomROrc_Grupos != null)
					{
						this.mColCustomROrc_Grupos.Clear();
					}
					this.mColCustomROrc_Grupos = null;
					
					if (this.mColCustomROrc_ItensGrupo != null)
					{
						this.mColCustomROrc_ItensGrupo.Clear();
					}
					this.mColCustomROrc_ItensGrupo = null;
				}
			}
			#endregion
			
			namespace primitivas
			{
				
				#region ===[ SPOOL ]===
				//===[ SPOOL ]=======================================================================================
				//Off - Edgar
				public class Spool : Fachadas.CTR.primitivas.Spool
				{
					
				}
				#endregion
				
				#region ===[ QUALIDADE ]===
				//===[ QUALIDADE ]=======================================================================================
				//Off - Edgar
				public class Qualidade : allClass
				{
					
					protected Interfaces.iEVT.Primitivas.iQualidade mCampos;
					
					public Qualidade() : base("EVT_Qualidade")
					{
						mCampos = new QualidadeCampos(this);
					}
					public Qualidade(tipos.tiposConection TipoConexao) : base("EVT_Qualidade", TipoConexao)
					{
						mCampos = new QualidadeCampos(this);
					}
					public Interfaces.iEVT.Primitivas.iQualidade campos
					{
						get
						{
							return mCampos;
						}
						set
						{
							mCampos = value;
						}
					}
					public void getFieldsFromQualidade(double idQualidade)
					{
						this.filterWhere = "idQualidade = " + idQualidade;
						this.getFields();
					}
					
					private class QualidadeCampos : Interfaces.iEVT.Primitivas.iQualidade
					{
						
						private allClass parent;
						public QualidadeCampos(allClass parent)
						{
							this.parent = parent;
						}
						public void Dispose()
						{
							parent = null;
						}
						public void Clear_filters()
						{
							parent.Clear_filters();
						}
						public void Clear_vars()
						{
							parent.Clear_vars();
						}
						public void salvar(bool noCommit)
						{
							this.Salvar(noCommit);
						}
						
						public void Salvar(bool noCommit)
						{
							string filtro;
							filtro = " qualidade = \'" + this.qualidade_key + "\' ";
							this.parent.SalvarPadrao(noCommit, filtro);
						}
						public void salvar()
						{
							this.Salvar();
						}
						
						public void Salvar()
						{
							this.Salvar(false);
						}
						
						
						#region    Propriedades - Fields
						
						public string Key
						{
							get
							{
								return this.qualidade_key;
							}
						}
						
						public int ID
						{
							get
							{
								return parent.var["idQualidade"].value;
							}
						}
						
						public string qualidade_key
						{
							get
							{
								return parent.var["qualidade"].value;
							}
							set
							{
								parent.var["qualidade"].Dirty = true;
								parent.var["qualidade"].value = value;
							}
						}
						#endregion
					}
				}
				#endregion
				
				#region ===[ NOTA FISCAL ]===
				//===[ NOTA FISCAL ]=======================================================================================
				//Off - Edgar
				public class NotaFiscal : allClass
				{
					
					protected Interfaces.iEVT.Primitivas.iNotaFiscal mCampos;
					
					public NotaFiscal() : base("EVT_NotaFiscal")
					{
						mCampos = new NotaFiscalCampos(this);
					}
					public NotaFiscal(tipos.tiposConection TipoConexao) : base("EVT_NotaFiscal", TipoConexao)
					{
						mCampos = new NotaFiscalCampos(this);
					}
					
					public Interfaces.iEVT.Primitivas.iNotaFiscal campos
					{
						get
						{
							return mCampos;
						}
						set
						{
							mCampos = value;
						}
					}
					
					private class NotaFiscalCampos : Interfaces.iEVT.Primitivas.iNotaFiscal
					{
						
						private allClass parent;
						public NotaFiscalCampos(allClass parent)
						{
							this.parent = parent;
						}
						public void Dispose()
						{
							parent = null;
						}
						public void Clear_filters()
						{
							parent.Clear_filters();
						}
						public void Clear_vars()
						{
							parent.Clear_vars();
						}
						public void salvar(bool noCommit)
						{
							this.Salvar(noCommit);
						}
						
						public void Salvar(bool noCommit)
						{
							string filtro;
							filtro = " numeroFiscal = " + this.numeroFiscal_key;
							this.parent.SalvarPadrao(noCommit, filtro);
						}
						public void salvar()
						{
							this.Salvar();
						}
						
						public void Salvar()
						{
							this.Salvar(false);
						}
						
						#region    Propriedades - Fields
						
						public string Key
						{
							get
							{
								return this.numeroFiscal_key;
							}
						}
						
						public int ID
						{
							get
							{
								return parent.var["idNotaFiscal"].value;
							}
						}
						
						public int numeroFiscal_key
						{
							get
							{
								return parent.var["numeroFiscal"].value;
							}
							set
							{
								parent.var["numeroFiscal"].Dirty = true;
								parent.var["numeroFiscal"].value = value;
							}
						}
						
						public string beneficiario
						{
							get
							{
								return parent.var["beneficiario"].value;
							}
							set
							{
								parent.var["beneficiario"].Dirty = true;
								parent.var["beneficiario"].value = value;
							}
						}
						
						public bool cancelada
						{
							get
							{
								return parent.var["cancelada"].value;
							}
							set
							{
								parent.var["cancelada"].Dirty = true;
								parent.var["cancelada"].value = value;
							}
						}
						
						public string CNPJ
						{
							get
							{
								return parent.var["CNPJ"].value;
							}
							set
							{
								parent.var["CNPJ"].Dirty = true;
								parent.var["CNPJ"].value = value;
							}
						}
						
						public DateTime dataFiscal
						{
							get
							{
								return parent.var["dataFiscal"].value;
							}
							set
							{
								parent.var["dataFiscal"].Dirty = true;
								parent.var["dataFiscal"].value = value;
							}
						}
						
						public string endereco
						{
							get
							{
								return parent.var["endereco"].value;
							}
							set
							{
								parent.var["endereco"].Dirty = true;
								parent.var["endereco"].value = value;
							}
						}
						
						public string fone
						{
							get
							{
								return parent.var["fone"].value;
							}
							set
							{
								parent.var["fone"].Dirty = true;
								parent.var["fone"].value = value;
							}
						}
						
						public string formaPagamento
						{
							get
							{
								return parent.var["formaPagamento"].value;
							}
							set
							{
								parent.var["fone"].Dirty = true;
								parent.var["fone"].value = value;
							}
						}
						
						public int idOrdemServico
						{
							get
							{
								return parent.var["idPrdemServico"].value;
							}
							set
							{
								parent.var["idPrdemServico"].Dirty = true;
								parent.var["idPrdemServico"].value = value;
							}
						}
						
						public string IE
						{
							get
							{
								return parent.var["IE"].value;
							}
							set
							{
								parent.var["IE"].Dirty = true;
								parent.var["IE"].value = value;
							}
						}
						
						public string IM
						{
							get
							{
								return parent.var["IM"].value;
							}
							set
							{
								parent.var["IM"].Dirty = true;
								parent.var["IM"].value = value;
							}
						}
						
						public string municipio
						{
							get
							{
								return parent.var["municipio"].value;
							}
							set
							{
								parent.var["municipio"].Dirty = true;
								parent.var["municipio"].value = value;
							}
						}
						
						public string UF
						{
							get
							{
								return parent.var["UF"].value;
							}
							set
							{
								parent.var["UF"].Dirty = true;
								parent.var["UF"].value = value;
							}
						}
						
						#endregion
						
					}
				}
				#endregion
				
				#region ===[ ITEM DA NOTA FISCAL ]===
				//===[ ITEM DA NOTA FISCAL ]=======================================================================================
				//Off - Edgar
				public class NotaFiscalItem : allClass
				{
					
					protected Interfaces.iEVT.Primitivas.iNotaFiscalItem mCampos;
					
					public NotaFiscalItem() : base("EVT_NotaFiscalItem")
					{
						mCampos = new NotaFiscalItemCampos(this);
					}
					public NotaFiscalItem(tipos.tiposConection TipoConexao) : base("EVT_NotaFiscalItem", TipoConexao)
					{
						mCampos = new NotaFiscalItemCampos(this);
					}
					
					public Interfaces.iEVT.Primitivas.iNotaFiscalItem Campos
					{
						get
						{
							return mCampos;
						}
						set
						{
							mCampos = value;
						}
					}
					
					private class NotaFiscalItemCampos : Interfaces.iEVT.Primitivas.iNotaFiscalItem
					{
						
						private allClass parent;
						
						public NotaFiscalItemCampos(allClass parent)
						{
							this.parent = parent;
						}
						public void Dispose()
						{
							parent = null;
						}
						public void Clear_filters()
						{
							parent.Clear_filters();
						}
						public void Clear_vars()
						{
							parent.Clear_vars();
						}
						public void salvar(bool noCommit)
						{
							this.Salvar(noCommit);
						}
						
						public void Salvar(bool noCommit)
						{
							string filtro;
							filtro = " idnotafiscal = " + this.idNotaFiscal_key + " and descricao = \'" + this.descricao_key + "\' ";
							this.parent.SalvarPadrao(noCommit, filtro);
						}
						public void salvar()
						{
							this.Salvar();
						}
						
						public void Salvar()
						{
							this.Salvar(false);
						}
						
						#region    Propriedades - Fields
						
						public string Key
						{
							get
							{
								return this.idNotaFiscal_key + this.descricao_key;
							}
						}
						
						public int ID
						{
							get
							{
								return parent.var["idNotaFiscalItem"].value;
							}
						}
						
						public string descricao_key
						{
							get
							{
								return parent.var["descricao"].value;
							}
							set
							{
								parent.var["descricao"].Dirty = true;
								parent.var["descricao"].value = value;
							}
						}
						
						public int idNotaFiscal_key
						{
							get
							{
								return parent.var["idNotaFiscal"].value;
							}
							set
							{
								parent.var["idNotaFiscal"].Dirty = true;
								parent.var["idNotaFiscal"].value = value;
							}
						}
						
						public int quantidade
						{
							get
							{
								return parent.var["quantidade"].value;
							}
							set
							{
								parent.var["quantidade"].Dirty = true;
								parent.var["quantidade"].value = value;
							}
						}
						
						public string unidade
						{
							get
							{
								return parent.var["unidade"].value;
							}
							set
							{
								parent.var["unidade"].Dirty = true;
								parent.var["unidade"].value = value;
							}
						}
						
						public decimal valor
						{
							get
							{
								return parent.var["valor"].value;
							}
							set
							{
								parent.var["valor"].Dirty = true;
								parent.var["valor"].value = value;
							}
						}
						
						#endregion
						
					}
				}
				#endregion
				
				#region ===[ ITEM ]===
				//===[ ITEM ]=======================================================================================
				//Off - Célio
				public class Item : allClass
				{
					
					private ItemCampos mCampos; //Interfaces.iEVT.Primitivas.iLocalidades
					
					public Item() : base("EVT_Itens")
					{
						mCampos = new ItemCampos(this);
					}
					public Item(tipos.tiposConection TipoConexao) : base("EVT_Itens", TipoConexao)
					{
						mCampos = new ItemCampos(this);
					}
					public Interfaces.iEVT.Primitivas.iItem Campos
					{
						get
						{
							return mCampos;
						}
						set
						{
							mCampos = value;
							if (value != null)
							{
								base.toObject = mCampos.Parent;
							}
						}
					}
					
					private class ItemCampos : Interfaces.iEVT.Primitivas.iItem
					{
						
						
						private allClass mParent;
						
						public ItemCampos(allClass allClass)
						{
							this.mParent = allClass;
						}
						
						internal allClass Parent
						{
							get
							{
								return this.mParent;
							}
							set
							{
								this.mParent = value;
							}
						}
						
						public void Dispose()
						{
							mParent = null;
						}
						
						public void salvar(bool noCommit)
						{
							this.Salvar(noCommit);
						}
						
						public void Salvar(bool noCommit)
						{
							string filtro;
							filtro = " descricao = \'" + this.Descricao_key + "\' and NumeroSerie=\'" + this.NumeroSerie + "\' ";
							this.Parent.SalvarPadrao(noCommit, filtro);
						}
						
						public void salvar()
						{
							this.Salvar();
						}
						
						public void Salvar()
						{
							this.Salvar(false);
						}
						
						public void Clear_filters()
						{
							Parent.Clear_filters();
						}
						
						public void Clear_vars()
						{
							Parent.Clear_vars();
						}
						
						#region    Propriedades - Fields
						
						public int ID
						{
							get
							{
								return Parent.var["idItem"].value;
							}
						}
						
						public string Key
						{
							get
							{
								return this.Descricao_key + this.NumeroSerie;
							}
						}
						
						public string IdObj
						{
							get
							{
								return Parent.var["idObj"].value;
							}
						}
						
						public string Descricao_key
						{
							get
							{
								return Parent.var["descricao"].value;
							}
							set
							{
								Parent.var["descricao"].Dirty = true;
								Parent.var["descricao"].value = value;
							}
						}
						
						public int Quantidade
						{
							get
							{
								return Parent.var["quantidade"].value;
							}
							set
							{
								Parent.var["quantidade"].Dirty = true;
								Parent.var["quantidade"].value = value;
							}
						}
						
						public double ValorLocacao
						{
							get
							{
								return (System.Convert.ToString(Parent.var["ValorLocacao"].value)).Replace(".", ",");
							}
							set
							{
								Parent.var["ValorLocacao"].Dirty = true;
								Parent.var["ValorLocacao"].value = value;
							}
						}
						
						public decimal DescMax
						{
							get
							{
								return Parent.var["DescMax"].value;
							}
							set
							{
								Parent.var["DescMax"].Dirty = true;
								Parent.var["DescMax"].value = value;
							}
						}
						
						public int VidaUtil
						{
							get
							{
								return Parent.var["vidaUtil"].value;
							}
							set
							{
								Parent.var["vidaUtil"].Dirty = true;
								Parent.var["vidaUtil"].value = value;
							}
						}
						
						public string NumeroSerie
						{
							get
							{
								return Parent.var["NumeroSerie"].value;
							}
							set
							{
								Parent.var["NumeroSerie"].Dirty = true;
								Parent.var["NumeroSerie"].value = value;
							}
						}
						
						public int IdQualidade
						{
							get
							{
								return Parent.var["idQualidade"].value;
							}
							set
							{
								Parent.var["idQualidade"].Dirty = true;
								Parent.var["idQualidade"].value = value;
							}
						}
						
						public int IdDeposito
						{
							get
							{
								return Parent.var["idDeposito"].value;
							}
							set
							{
								Parent.var["idDeposito"].Dirty = true;
								Parent.var["idDeposito"].value = value;
							}
						}
						
						public int IdFornecedor
						{
							get
							{
								return Parent.var["idFornecedor"].value;
							}
							set
							{
								Parent.var["idFornecedor"].Dirty = true;
								Parent.var["idFornecedor"].value = value;
							}
						}
						
						public string Comentario
						{
							get
							{
								return Parent.var["comentarios"].value;
							}
							set
							{
								Parent.var["comentarios"].Dirty = true;
								Parent.var["comentarios"].value = value;
							}
						}
						
						
						#endregion
						
					}
				}
				#endregion
				
				#region ===[ LOCALIDADES ]===
				//===[ LOCALIDADES ]=======================================================================================
				//Off - Edgar
				public class Localidades : allClass
				{
					
					private LocalidadesCampos mCampos; //Interfaces.iEVT.Primitivas.iLocalidades
					
					public Localidades() : base("EVT_Localidades")
					{
						mCampos = new LocalidadesCampos(this);
					}
					public Localidades(tipos.tiposConection TipoConexao) : base("EVT_Localidades", TipoConexao)
					{
						mCampos = new LocalidadesCampos(this);
					}
					public Interfaces.iEVT.Primitivas.iLocalidades Campos
					{
						get
						{
							return mCampos;
						}
						set
						{
							mCampos = value;
							if (value != null)
							{
								base.toObject = mCampos.Parent;
							}
						}
					}
					
					private class LocalidadesCampos : Interfaces.iEVT.Primitivas.iLocalidades
					{
						
						
						private allClass mParent;
						public LocalidadesCampos(allClass allClass)
						{
							this.mParent = allClass;
						}
						internal allClass Parent
						{
							get
							{
								return this.mParent;
							}
							set
							{
								this.mParent = value;
							}
						}
						
						public void Dispose()
						{
							mParent = null;
						}
						public void salvar(bool noCommit)
						{
							this.Salvar(noCommit);
						}
						
						public void Salvar(bool noCommit)
						{
							string filtro;
							filtro = " idLocalidade = " + this.ID + " and Nome = \'" + this.Nome_Key + "\'  ";
							this.Parent.SalvarPadrao(noCommit, filtro);
						}
						public void salvar()
						{
							this.Salvar();
						}
						
						public void Salvar()
						{
							this.Salvar(false);
						}
						public void Clear_filters()
						{
							Parent.Clear_filters();
						}
						public void Clear_vars()
						{
							Parent.Clear_vars();
						}
						#region    Propriedades - Fields
						
						public string Key
						{
							get
							{
								return this.idEntidade.ToString() + this.Nome_Key.ToString();
							}
						}
						
						public int ID
						{
							get
							{
								return Parent.var["idLocalidade"].value;
							}
						}
						
						public string Nome_key
						{
							get
							{
								return this.Nome_Key;
							}
							set
							{
								this.Nome_Key = value;
							}
						}
						
						public string Nome_Key
						{
							get
							{
								return Parent.var["nome"].value;
							}
							set
							{
								Parent.var["nome"].Dirty = true;
								Parent.var["nome"].value = value;
							}
						}
						
						public int idEntidade
						{
							get
							{
								return Parent.var["idEntidade"].value;
							}
							set
							{
								Parent.var["idEntidade"].Dirty = true;
								Parent.var["idEntidade"].value = value;
							}
						}
						
						public string comoChegar
						{
							get
							{
								return this.ComoChegar;
							}
							set
							{
								this.ComoChegar = value;
							}
						}
						
						public string ComoChegar
						{
							get
							{
								return Parent.var["comoChegar"].value;
							}
							set
							{
								Parent.var["comoChegar"].Dirty = true;
								Parent.var["comoChegar"].value = value;
							}
						}
						#endregion
						
					}
					
				}
				#endregion
				
				#region ===[ DEPÓSITOS ]===
				//===[ DEPÓSITOS ]=======================================================================================
				//Off - Célio
				public class Depositos : allClass
				{
					
					private DepositoCampos mCampos; //Interfaces.iEVT.Primitivas.iDeposito
					
					public Depositos() : base("EVT_Deposito")
					{
						mCampos = new DepositoCampos(this);
					}
					public Depositos(tipos.tiposConection TipoConexao) : base("EVT_Deposito", TipoConexao)
					{
						mCampos = new DepositoCampos(this);
					}
					public Interfaces.iEVT.Primitivas.iDeposito Campos
					{
						get
						{
							return mCampos;
						}
						set
						{
							mCampos = value;
							if (value != null)
							{
								base.toObject = mCampos.Parent;
							}
						}
					}
					public void getFieldsFromDeposito(double idDeposito)
					{
						this.filterWhere = "idDeposito = " + idDeposito;
						this.getFields();
					}
					private class DepositoCampos : Interfaces.iEVT.Primitivas.iDeposito
					{
						
						
						private allClass mParent;
						
						public DepositoCampos(allClass allClass)
						{
							this.mParent = allClass;
						}
						internal allClass Parent
						{
							get
							{
								return this.mParent;
							}
							set
							{
								this.mParent = value;
							}
						}
						public void Dispose()
						{
							mParent = null;
						}
						public void salvar(bool noCommit)
						{
							this.Salvar(noCommit);
						}
						
						public void Salvar(bool noCommit)
						{
							string filtro;
							filtro = " idDeposito = " + this.ID + " and Nome = \'" + this.Nome_Key + "\'  ";
							this.Parent.SalvarPadrao(noCommit, filtro);
						}
						public void salvar()
						{
							this.Salvar();
						}
						
						public void Salvar()
						{
							this.Salvar(false);
						}
						public void Clear_filters()
						{
							Parent.Clear_filters();
						}
						public void Clear_vars()
						{
							Parent.Clear_vars();
						}
						#region    Propriedades - Fields
						
						public string Key
						{
							get
							{
								return this.ID.ToString() + this.Nome_Key.ToString();
							}
						}
						
						public int ID
						{
							get
							{
								return Parent.var["idDeposito"].value;
							}
						}
						
						public string Nome_Key
						{
							get
							{
								return Parent.var["Nome"].value;
							}
							set
							{
								Parent.var["Nome"].Dirty = true;
								Parent.var["Nome"].value = value;
							}
						}
						
						public string Endereco
						{
							get
							{
								return Parent.var["Endereco"].value;
							}
							set
							{
								Parent.var["Endereco"].Dirty = true;
								Parent.var["Endereco"].value = value;
							}
						}
						#endregion
						
					}
					
				}
				#endregion
				
				#region ===[ KIT ]===
				//===[ KIT ]=======================================================================================
				//Off - Edgar
				public class kit : allClass
				{
					
					protected Interfaces.iEVT.Primitivas.iKit mCampos;
					
					public kit() : base("EVT_Kit")
					{
						mCampos = new KitCampos(this);
					}
					
					public kit(tipos.tiposConection TipoConexao) : base("EVT_Kit", TipoConexao)
					{
						mCampos = new KitCampos(this);
					}
					
					public Interfaces.iEVT.Primitivas.iKit campos
					{
						get
						{
							return mCampos;
						}
						set
						{
							mCampos = value;
						}
					}
					
					private class KitCampos : Interfaces.iEVT.Primitivas.iKit
					{
						
						
						private allClass parent;
						public KitCampos(allClass parent)
						{
							this.parent = parent;
						}
						public void Dispose()
						{
							parent = null;
						}
						public void Clear_filters()
						{
							parent.Clear_filters();
						}
						public void Clear_vars()
						{
							parent.Clear_vars();
						}
						public void salvar(bool noCommit)
						{
							this.Salvar(noCommit);
						}
						
						public void Salvar(bool noCommit)
						{
							string filtro;
							filtro = " idKit = " + this.ID.ToString();
							this.parent.SalvarPadrao(noCommit, filtro);
						}
						public void salvar()
						{
							this.Salvar();
						}
						
						public void Salvar()
						{
							this.Salvar(false);
						}
						
						#region    Propriedades - Fields
						
						public int ID
						{
							get
							{
								return parent.var["idKit"].value;
							}
						}
						
						public string Key
						{
							get
							{
								return this.Nome_key;
							}
						}
						
						public string idObj
						{
							get
							{
								return parent.var["idObj"].value;
							}
						}
						
						public string Nome_key
						{
							get
							{
								return parent.var["nome"].value;
							}
							set
							{
								parent.var["nome"].Dirty = true;
								parent.var["nome"].value = value;
							}
						}
						
						public double DescMax
						{
							get
							{
								return (System.Convert.ToString(parent.var["DescMax"].value)).Replace(".", ",");
							}
							set
							{
								parent.var["DescMax"].Dirty = true;
								parent.var["DescMax"].value = value;
							}
						}
						
						public double ValorLocacao
						{
							get
							{
								return (System.Convert.ToString(parent.var["ValorLocacao"].value)).Replace(".", ",");
							}
							set
							{
								parent.var["ValorLocacao"].Dirty = true;
								parent.var["ValorLocacao"].value = value;
							}
						}
						
						public string Instrucoes
						{
							get
							{
								return this.instrucoes;
							}
							set
							{
								this.instrucoes = value;
							}
						}
						
						public string instrucoes
						{
							get
							{
								return parent.var["instrucoes"].value;
							}
							set
							{
								parent.var["instrucoes"].Dirty = true;
								parent.var["instrucoes"].value = value;
							}
						}
						
						public int Quantidade
						{
							get
							{
								return parent.var["Quantidade"].value;
							}
							set
							{
								parent.var["Quantidade"].Dirty = true;
								parent.var["Quantidade"].value = value;
							}
						}
						#endregion
						
					}
				}
				#endregion
				
				#region ===[ KIT ITEM ]===
				//===[ KIT ITEM ]=======================================================================================
				//Off - Edgar
				public class KitItem : allClass
				{
					
					protected Interfaces.iEVT.Primitivas.iKitItem mCampos;
					
					public KitItem() : base("EVT_KitItem")
					{
						mCampos = new KitItemCampos(this);
					}
					
					public KitItem(tipos.tiposConection TipoConexao) : base("EVT_KitItem", TipoConexao)
					{
						mCampos = new KitItemCampos(this);
					}
					
					public Interfaces.iEVT.Primitivas.iKitItem Campos
					{
						get
						{
							return mCampos;
						}
						set
						{
							mCampos = value;
						}
					}
					
					private class KitItemCampos : Interfaces.iEVT.Primitivas.iKitItem
					{
						
						
						private allClass parent;
						private string aDescricao;
						private double aValorLocacao;
						
						public KitItemCampos(allClass parent)
						{
							this.parent = parent;
						}
						
						public void Dispose()
						{
							parent = null;
						}
						
						public void Clear_filters()
						{
							parent.Clear_filters();
						}
						
						public void Clear_vars()
						{
							parent.Clear_vars();
						}
						
						public void salvar(bool noCommit)
						{
							this.Salvar(noCommit);
						}
						
						public void Salvar(bool noCommit)
						{
							string filtro;
							filtro = " idKit = " + this.idKit_key + " and CodBarrasItem = \'" + (this.CodBarrasItem_key + "\'");
							this.parent.SalvarPadrao(noCommit, filtro);
						}
						public void salvar()
						{
							this.Salvar();
						}
						
						public void Salvar()
						{
							this.Salvar(false);
						}
						
						#region    Propriedades - Fields
						
						public int ID
						{
							get
							{
								return parent.var["idKitItem"].value;
							}
						}
						
						public string Key
						{
							get
							{
								return this.idKit_key.ToString() + this.CodBarrasItem_key.ToString();
							}
						}
						
						public int idKit_key
						{
							get
							{
								return parent.var["idKit"].value;
							}
							set
							{
								parent.var["idKit"].Dirty = true;
								parent.var["idKit"].value = value;
							}
						}
						
						public string CodBarrasItem_key
						{
							get
							{
								return parent.var["CodBarrasItem"].value;
							}
							set
							{
								parent.var["CodBarrasItem"].Dirty = true;
								parent.var["CodBarrasItem"].value = value;
							}
						}
						
						public float Status
						{
							get
							{
								return parent.var["Status"].value;
							}
							set
							{
								parent.var["Status"].Dirty = true;
								parent.var["Status"].value = value;
							}
						}
						
						public string TransferidoPara
						{
							get
							{
								return parent.var["TransferidoPara"].value;
							}
							set
							{
								parent.var["TransferidoPara"].Dirty = true;
								parent.var["TransferidoPara"].value = value;
							}
						}
						
						//Propriedades Usadas somente para apresentar os dados nas Grids dos Kits.
						public string Descricao
						{
							get
							{
								return this.aDescricao;
							}
							set
							{
								this.aDescricao = value;
							}
						}
						
						public double ValorLocacao
						{
							get
							{
								return this.aValorLocacao;
							}
							set
							{
								this.aValorLocacao = value;
							}
						}
						
						#endregion
					}
				}
				#endregion
				
				#region ===[ Orçamento ]===
				//===[ Orçamento ]=======================================================================================
				//Off - Celio
				public class Orcamento : allClass
				{
					
					protected Interfaces.iEVT.Primitivas.iOrcamento mCampos;
					
					public Orcamento() : base("EVT_Orcamento")
					{
						mCampos = new OrcamentoCampos(this);
					}
					
					public Orcamento(tipos.tiposConection TipoConexao) : base("EVT_Orcamento", TipoConexao)
					{
						mCampos = new OrcamentoCampos(this);
					}
					
					public Interfaces.iEVT.Primitivas.iOrcamento campos
					{
						get
						{
							return mCampos;
						}
						set
						{
							mCampos = value;
						}
					}
					
					private class OrcamentoCampos : Interfaces.iEVT.Primitivas.iOrcamento
					{
						
						private allClass parent;
						public OrcamentoCampos(allClass parent)
						{
							this.parent = parent;
						}
						public void Dispose()
						{
							parent = null;
						}
						public void Clear_filters()
						{
							parent.Clear_filters();
						}
						public void Clear_vars()
						{
							parent.Clear_vars();
						}
						public void salvar(bool noCommit)
						{
							this.Salvar(noCommit);
						}
						
						public void Salvar(bool noCommit)
						{
							string filtro;
							filtro = " descricao = \'" + this.Descricao_Key + "\' ";
							this.parent.SalvarPadrao(noCommit, filtro);
						}
						public void salvar()
						{
							this.Salvar();
						}
						
						public void Salvar()
						{
							this.Salvar(false);
						}
						
						#region    Propriedades - Fields
						
						public int ID
						{
							get
							{
								return parent.var["idOrcamento"].value;
							}
						}
						
						public string Key
						{
							get
							{
								return this.Descricao_Key.ToString() + this.DataEmissao_Key.ToString("dd/MM/yyyy");
							}
						}
						
						public string Contato
						{
							get
							{
								return parent.var["contato"].value;
							}
							set
							{
								parent.var["contato"].Dirty = true;
								parent.var["contato"].value = value;
							}
						}
						
						public string Descricao_Key
						{
							get
							{
								return parent.var["descricao"].value;
							}
							set
							{
								if (value == "")
								{
									throw (new EVTexception("Este campo é uma chave portanto ele não pode conter valor em branco", "*** Orçamento - Descrição ***"));
								}
								parent.var["descricao"].Dirty = true;
								parent.var["descricao"].value = value;
							}
						}
						
						public DateTime DataEmissao_Key
						{
							get
							{
								return parent.var["DataEmissao"].value;
							}
							set
							{
								DateTime dt;
								try
								{
									dt = parent.var["DataEmissao"].value;
								}
								catch (Exception)
								{
									dt = "01/01/00";
								}
								if (value != dt && value < DateAndTime.Today)
								{
									throw (new EVTexception("A data de emissão não pode ser menor que a data de hoje", "*** Orçamento - Data de Emissão ***"));
								}
								parent.var["DataEmissao"].Dirty = true;
								parent.var["DataEmissao"].value = value;
							}
						}
						
						public DateTime DataValidade
						{
							get
							{
								return parent.var["DataValidade"].value;
							}
							set
							{
								DateTime dt;
								try
								{
									dt = parent.var["DataValidade"].value;
								}
								catch (Exception)
								{
									dt = "01/01/00";
								}
								if (value != dt && value < DateAndTime.Today && ! Convert.ToBoolean(this.IsModelo))
								{
									throw (new EVTexception("A data de validade não pode ser menor que a data de hoje", "*** Orçamento - Data de Validade ***"));
								}
								parent.var["DataValidade"].Dirty = true;
								parent.var["DataValidade"].value = value;
							}
						}
						
						public DateTime DataVencimento
						{
							get
							{
								return parent.var["DataVencimento"].value;
							}
							set
							{
								parent.var["DataVencimento"].Dirty = true;
								parent.var["DataVencimento"].value = value;
							}
						}
						
						public int IdEntidade
						{
							get
							{
								return this.idEntidade;
							}
							set
							{
								this.idEntidade = value;
							}
						}
						
						public int idEntidade
						{
							get
							{
								return parent.var["idEntidade"].value;
							}
							set
							{
								parent.var["idEntidade"].Dirty = true;
								parent.var["idEntidade"].value = value;
							}
						}
						
						public int IdLocalidade
						{
							get
							{
								return this.idLocalidade;
							}
							set
							{
								this.idLocalidade = value;
							}
						}
						
						public int idLocalidade
						{
							get
							{
								return parent.var["idLocalidade"].value;
							}
							set
							{
								parent.var["idLocalidade"].Dirty = true;
								parent.var["idLocalidade"].value = value;
							}
						}
						
						public short Confirmado
						{
							get
							{
								bool tmp;
								tmp = parent.var["confirmado"].value;
								return Convert.ToInt32(tmp);
							}
							set
							{
								parent.var["confirmado"].Dirty = true;
								parent.var["confirmado"].value = value;
							}
						}
						
						public string FormaPagamento
						{
							get
							{
								return parent.var["formaPagamento"].value;
							}
							set
							{
								parent.var["formaPagamento"].Dirty = true;
								parent.var["formaPagamento"].value = value;
							}
						}
						
						public double PercDesconto
						{
							get
							{
								return (System.Convert.ToString(parent.var["percDesc"].value)).Replace(".", ",");
							}
							set
							{
								parent.var["percDesc"].Dirty = true;
								parent.var["percDesc"].value = value;
							}
						}
						
						public short IsModelo
						{
							get
							{
								bool tmp;
								tmp = parent.var["ismodelo"].value;
								return Convert.ToInt32(tmp);
							}
							set
							{
								parent.var["ismodelo"].Dirty = true;
								parent.var["ismodelo"].value = value;
							}
						}
						
						public double Valor
						{
							get
							{
								return (System.Convert.ToString(parent.var["Valor"].value)).Replace(".", ",");
							}
							set
							{
								parent.var["Valor"].Dirty = true;
								parent.var["Valor"].value = value;
							}
						}
						
						#endregion
					}
				}
				#endregion
				
				#region ===[ Orçamento Objetos ]===
				//===[ Orçamento Objetos ]=======================================================================================
				//Off - Edgar
				public class OrcamentoObjs : allClass
				{
					
					protected Interfaces.iEVT.Primitivas.iOrcamentoObjs mCampos;
					
					public OrcamentoObjs() : base("EVT_OrcamentoObjs")
					{
						mCampos = new OrcamentoObjsCampos(this);
					}
					
					public OrcamentoObjs(tipos.tiposConection TipoConexao) : base("EVT_OrcamentoObjs", TipoConexao)
					{
						mCampos = new OrcamentoObjsCampos(this);
					}
					
					public Interfaces.iEVT.Primitivas.iOrcamentoObjs Campos
					{
						get
						{
							return mCampos;
						}
						set
						{
							mCampos = value;
						}
					}
					
					private class OrcamentoObjsCampos : Interfaces.iEVT.Primitivas.iOrcamentoObjs
					{
						
						
						private allClass parent;
						private Interfaces.iEVT.Primitivas.iCtrObj mCtrObj;
						
						public OrcamentoObjsCampos(allClass parent)
						{
							this.parent = parent;
						}
						public void Dispose()
						{
							parent = null;
						}
						public void Clear_filters()
						{
							parent.Clear_filters();
						}
						public void Clear_vars()
						{
							parent.Clear_vars();
						}
						public void salvar(bool noCommit)
						{
							this.Salvar(noCommit);
						}
						
						public void Salvar(bool noCommit)
						{
							string filtro;
							filtro = " idobj = \'" + this.IdObj_key + "\' and idOrcamento = " + this.idOrcamento_key.ToString() + " and dataLocacaoInicio = \'" + this.DataLocacaoInicio_key.ToString("MM/dd/yyyy") + "\' ";
							this.parent.SalvarPadrao(noCommit, filtro);
						}
						public void salvar()
						{
							this.Salvar();
						}
						
						public void Salvar()
						{
							this.Salvar(false);
						}
						public Interfaces.iEVT.Primitivas.iCtrObj CtrObj
						{
							get
							{
								return this.mCtrObj;
							}
							set
							{
								this.mCtrObj = value;
							}
						}
						
						#region    Propriedades - Fields
						
						public int ID
						{
							get
							{
								return parent.var["idOrcObj"].value;
							}
						}
						
						public string Key
						{
							get
							{
								return this.idOrcamento_key.ToString() + this.IdObj_key.ToString() + this.DataLocacaoInicio_key.ToShortDateString();
							}
						}
						
						public string IdObj_key
						{
							get
							{
								return parent.var["idobj"].value;
							}
							set
							{
								parent.var["idobj"].Dirty = true;
								parent.var["idobj"].value = value;
							}
						}
						
						public int IdOrcamento_key
						{
							get
							{
								return this.idOrcamento_key;
							}
							set
							{
								this.idOrcamento_key = value;
							}
						}
						
						public int idOrcamento_key
						{
							get
							{
								return parent.var["idOrcamento"].value;
							}
							set
							{
								parent.var["idOrcamento"].Dirty = true;
								parent.var["idOrcamento"].value = value;
							}
						}
						
						public DateTime DataLocacaoFinal
						{
							get
							{
								return parent.var["dataLocacaoFim"].value;
							}
							set
							{
								parent.var["dataLocacaoFim"].Dirty = true;
								parent.var["dataLocacaoFim"].value = value;
							}
						}
						
						public DateTime DataLocacaoInicio_key
						{
							get
							{
								return parent.var["dataLocacaoInicio"].value;
							}
							set
							{
								parent.var["dataLocacaoInicio"].Dirty = true;
								parent.var["dataLocacaoInicio"].value = value;
							}
						}
						
						public double Quantidade
						{
							get
							{
								return (System.Convert.ToString(parent.var["quantidade"].value)).Replace(".", ",");
							}
							set
							{
								parent.var["quantidade"].Dirty = true;
								parent.var["quantidade"].value = value;
							}
						}
						
						public double ValorUnitario
						{
							get
							{
								return (System.Convert.ToString(parent.var["ValorUnitario"].value)).Replace(".", ",");
							}
							set
							{
								parent.var["ValorUnitario"].Dirty = true;
								parent.var["ValorUnitario"].value = value;
							}
						}
						
						public double ValorLocacao
						{
							get
							{
								return (System.Convert.ToString(parent.var["ValorLocacao"].value)).Replace(".", ",");
							}
							set
							{
								parent.var["valorLocacao"].Dirty = true;
								parent.var["valorLocacao"].value = value;
							}
						}
						
						public string Descricao
						{
							get
							{
								return parent.var["descricao"].value;
							}
							set
							{
								parent.var["descricao"].Dirty = true;
								parent.var["descricao"].value = value;
							}
						}
						
						#endregion
						
					}
				}
				#endregion
				
				#region ===[ Orçamento Despesas Adicionais ]===
				//===[ Ordem de Servico Despesas Adicionais ]=======================================================================================
				//Off - Edgar
				public class OrcamentoDespAdicional : allClass
				{
					
					protected Interfaces.iEVT.Primitivas.iOrcamentoDespAdicional mCampos;
					
					public OrcamentoDespAdicional() : base("EVT_OrcamentoDespAdicional")
					{
						mCampos = new OrcamentoDespAdicionalCampos(this);
					}
					
					public OrcamentoDespAdicional(tipos.tiposConection TipoConexao) : base("EVT_OrcamentoDespAdicional", TipoConexao)
					{
						mCampos = new OrcamentoDespAdicionalCampos(this);
					}
					
					public Interfaces.iEVT.Primitivas.iOrcamentoDespAdicional Campos
					{
						get
						{
							return mCampos;
						}
						set
						{
							mCampos = value;
						}
					}
					
					private class OrcamentoDespAdicionalCampos : Interfaces.iEVT.Primitivas.iOrcamentoDespAdicional
					{
						
						
						private allClass parent;
						
						public OrcamentoDespAdicionalCampos(allClass parent)
						{
							this.parent = parent;
						}
						public void Dispose()
						{
							parent = null;
						}
						public void Clear_filters()
						{
							parent.Clear_filters();
						}
						public void Clear_vars()
						{
							parent.Clear_vars();
						}
						public void salvar(bool noCommit)
						{
							this.Salvar(noCommit);
						}
						
						public void Salvar(bool noCommit)
						{
							string filtro;
							filtro = " idOrcamento = " + this.IdOrcamento_Key + " and Descricao = \'" + this.Descricao_Key + "\' ";
							this.parent.SalvarPadrao(noCommit, filtro);
						}
						public void salvar()
						{
							this.Salvar();
						}
						
						public void Salvar()
						{
							this.Salvar(false);
						}
						
						#region    Propriedades - Fields
						
						public int ID
						{
							get
							{
								return parent.var["idDespAdicional"].value;
							}
						}
						
						public string Key
						{
							get
							{
								return this.IdOrcamento_Key.ToString() + this.Descricao_Key.ToString();
							}
						}
						
						public int IdOrcamento_Key
						{
							get
							{
								return parent.var["idOrcamento"].value;
							}
							set
							{
								parent.var["idOrcamento"].Dirty = true;
								parent.var["idOrcamento"].value = value;
							}
						}
						
						public string Descricao_Key
						{
							get
							{
								return parent.var["descricao"].value;
							}
							set
							{
								parent.var["descricao"].Dirty = true;
								parent.var["descricao"].value = value;
							}
						}
						
						public int Quantidade
						{
							get
							{
								return parent.var["Quantidade"].value;
							}
							set
							{
								parent.var["Quantidade"].Dirty = true;
								parent.var["Quantidade"].value = value;
							}
						}
						
						public double ValorDespesa
						{
							get
							{
								return (System.Convert.ToString(parent.var["ValorDespesa"].value)).Replace(".", ",");
							}
							set
							{
								parent.var["ValorDespesa"].Dirty = true;
								parent.var["ValorDespesa"].value = value;
							}
						}
						
						#endregion
						
					}
				}
				#endregion
				
				#region ===[ Orçameto Parcelas ]===
				//===[ Orçameto Parcelas ]=======================================================================================
				//Off - Célio
				public class OrcamentoParcelas : allClass
				{
					
					protected Interfaces.iEVT.Primitivas.iOrcamentoParcelas mCampos;
					
					public OrcamentoParcelas() : base("EVT_OrcamentoParcelas")
					{
						mCampos = new OrcamentoParcelasCampos(this);
					}
					
					public OrcamentoParcelas(tipos.tiposConection TipoConexao) : base("EVT_OrcamentoParcelas", TipoConexao)
					{
						mCampos = new OrcamentoParcelasCampos(this);
					}
					
					public Interfaces.iEVT.Primitivas.iOrcamentoParcelas Campos
					{
						get
						{
							return mCampos;
						}
						set
						{
							mCampos = value;
						}
					}
					
					private class OrcamentoParcelasCampos : Interfaces.iEVT.Primitivas.iOrcamentoParcelas
					{
						
						
						private allClass parent;
						
						public OrcamentoParcelasCampos(allClass parent)
						{
							this.parent = parent;
						}
						public void Dispose()
						{
							parent = null;
						}
						public void Clear_filters()
						{
							parent.Clear_filters();
						}
						public void Clear_vars()
						{
							parent.Clear_vars();
						}
						public void salvar(bool noCommit)
						{
							this.Salvar(noCommit);
						}
						
						public void Salvar(bool noCommit)
						{
							string filtro;
							filtro = " idOrcamento = " + this.IdOrcamento_key.ToString() + " and numeroParcela = " + this.NumeroParcela_key.ToString() + " ";
							this.parent.SalvarPadrao(noCommit, filtro);
						}
						public void salvar()
						{
							this.Salvar();
						}
						
						public void Salvar()
						{
							this.Salvar(false);
						}
						
						#region    Propriedades - Fields
						
						public int ID
						{
							get
							{
								return parent.var["idParcela"].value;
							}
						}
						
						public string Key
						{
							get
							{
								return this.IdOrcamento_key.ToString() + this.NumeroParcela_key.ToString("000");
							}
						}
						
						public DateTime DataVencimento
						{
							get
							{
								return parent.var["dataVencimento"].value;
							}
							set
							{
								parent.var["dataVencimento"].Dirty = true;
								parent.var["dataVencimento"].value = value;
							}
						}
						
						public int IdOrcamento_key
						{
							get
							{
								return parent.var["idOrcamento"].value;
							}
							set
							{
								parent.var["idOrcamento"].Dirty = true;
								parent.var["idOrcamento"].value = value;
							}
						}
						
						public int NumeroParcela_key
						{
							get
							{
								return parent.var["numeroParcela"].value;
							}
							set
							{
								parent.var["numeroParcela"].Dirty = true;
								parent.var["numeroParcela"].value = value;
							}
						}
						
						public double ValorParcela
						{
							get
							{
								string tmp;
								tmp = parent.var["valorParcela"].value;
								return Convert.ToDouble(tmp.Replace(".", ","));
							}
							set
							{
								parent.var["valorParcela"].Dirty = true;
								parent.var["valorParcela"].value = value;
							}
						}
						#endregion
					}
				}
				#endregion
				
				#region ===[ Ordem de Serviço ]===
				//===[ Ordem de Serviço ]=======================================================================================
				//Off - Celio
				public class OrdemServico : allClass
				{
					
					protected Interfaces.iEVT.Primitivas.iOrdemServico mCampos;
					
					public OrdemServico() : base("EVT_OrdemServico")
					{
						mCampos = new OrdemServicoCampos(this);
					}
					
					public OrdemServico(tipos.tiposConection TipoConexao) : base("EVT_OrdemServico", TipoConexao)
					{
						mCampos = new OrdemServicoCampos(this);
					}
					
					public Interfaces.iEVT.Primitivas.iOrdemServico campos
					{
						get
						{
							return mCampos;
						}
						set
						{
							mCampos = value;
						}
					}
					
					private class OrdemServicoCampos : Interfaces.iEVT.Primitivas.iOrdemServico
					{
						
						private allClass parent;
						public OrdemServicoCampos(allClass parent)
						{
							this.parent = parent;
						}
						public void Dispose()
						{
							parent = null;
						}
						public void Clear_filters()
						{
							parent.Clear_filters();
						}
						public void Clear_vars()
						{
							parent.Clear_vars();
						}
						public void salvar(bool noCommit)
						{
							this.Salvar(noCommit);
						}
						
						public void Salvar(bool noCommit)
						{
							string filtro;
							filtro = " descricao = " + this.Descricao + " ";
							this.parent.SalvarPadrao(noCommit, filtro);
						}
						public void salvar()
						{
							this.Salvar();
						}
						
						public void Salvar()
						{
							this.Salvar(false);
						}
						
						#region    Propriedades - Fields
						
						public int ID
						{
							get
							{
								return parent.var["idOS"].value;
							}
						}
						
						public string Key
						{
							get
							{
								return this.ID;
							}
						}
						
						public int IdOrcamento
						{
							get
							{
								return parent.var["idorcamento"].value;
							}
							set
							{
								parent.var["idorcamento"].Dirty = true;
								parent.var["idorcamento"].value = value;
							}
						}
						
						public string Descricao
						{
							get
							{
								return parent.var["descricao"].value;
							}
							set
							{
								parent.var["descricao"].Dirty = true;
								parent.var["descricao"].value = value;
							}
						}
						
						public DateTime DataEmissao
						{
							get
							{
								return parent.var["DataEmissao"].value;
							}
							set
							{
								parent.var["DataEmissao"].Dirty = true;
								parent.var["DataEmissao"].value = value;
							}
						}
						
						public DateTime DataAlteracao
						{
							get
							{
								return parent.var["DataAlteracao"].value;
							}
							set
							{
								parent.var["DataAlteracao"].Dirty = true;
								parent.var["DataAlteracao"].value = value;
							}
						}
						
						public int IdUsuario
						{
							get
							{
								return this.idUsuario;
							}
							set
							{
								this.idUsuario = value;
							}
						}
						
						public int idUsuario
						{
							get
							{
								return parent.var["idUsuario"].value;
							}
							set
							{
								parent.var["idUsuario"].Dirty = true;
								parent.var["idUsuario"].value = value;
							}
						}
						#endregion
					}
				}
				#endregion
				
				#region ===[ Ordem de Serviço Objetos ]===
				//===[ Ordem de Serviço Objetos ]=======================================================================================
				//Off - Edgar
				public class OrdemServicoObjs : allClass
				{
					
					protected Interfaces.iEVT.Primitivas.iOrdemServicoObjs mCampos;
					public OrdemServicoObjs() : base("EVT_OrdemServicoObjs")
					{
						mCampos = new OrdemServicoObjsCampos(this);
					}
					
					public OrdemServicoObjs(tipos.tiposConection TipoConexao) : base("EVT_OrdemServicoObjs", TipoConexao)
					{
						mCampos = new OrdemServicoObjsCampos(this);
					}
					
					public Interfaces.iEVT.Primitivas.iOrdemServicoObjs Campos
					{
						get
						{
							return mCampos;
						}
						set
						{
							mCampos = value;
						}
					}
					
					private class OrdemServicoObjsCampos : Interfaces.iEVT.Primitivas.iOrdemServicoObjs
					{
						
						
						private allClass parent;
						private Interfaces.iEVT.Primitivas.iCtrObj mCtrObj;
						
						public OrdemServicoObjsCampos(allClass parent)
						{
							this.parent = parent;
						}
						public void Dispose()
						{
							parent = null;
						}
						public void Clear_filters()
						{
							parent.Clear_filters();
						}
						public void Clear_vars()
						{
							parent.Clear_vars();
						}
						public void salvar(bool noCommit)
						{
							this.Salvar(noCommit);
						}
						
						public void Salvar(bool noCommit)
						{
							string filtro;
							filtro = " idOS = " + this.IdOS_key.ToString() + " and idobj = \'" + this.IdObj_key.ToString() + "\'";
							this.parent.SalvarPadrao(noCommit, filtro);
						}
						public void salvar()
						{
							this.Salvar();
						}
						
						public void Salvar()
						{
							this.Salvar(false);
						}
						public Interfaces.iEVT.Primitivas.iCtrObj CtrObj
						{
							get
							{
								return this.mCtrObj;
							}
							set
							{
								this.mCtrObj = value;
							}
						}
						
						#region    Propriedades - Fields
						
						public int ID
						{
							get
							{
								return parent.var["idOSObj"].value;
							}
						}
						
						public string Key
						{
							get
							{
								return this.IdOS_key.ToString() + this.IdObj_key.ToString();
							}
						}
						
						public string IdObj
						{
							get
							{
								return this.IdObj_key;
							}
							set
							{
								this.IdObj_key = value;
							}
						}
						
						public string IdObj_key
						{
							get
							{
								return parent.var["idobj"].value;
							}
							set
							{
								parent.var["idobj"].Dirty = true;
								parent.var["idobj"].value = value;
							}
						}
						
						public int IdOS
						{
							get
							{
								return this.IdOS_key;
							}
							set
							{
								this.IdOS_key = value;
							}
						}
						
						public int IdOS_key
						{
							get
							{
								return parent.var["idOS"].value;
							}
							set
							{
								parent.var["idOS"].Dirty = true;
								parent.var["idOS"].value = value;
							}
						}
						
						public DateTime DataDevolucao
						{
							get
							{
								return parent.var["DataDevolucao"].value;
							}
							set
							{
								parent.var["DataDevolucao"].Dirty = true;
								parent.var["DataDevolucao"].value = value;
							}
						}
						
						public int Quantidade
						{
							get
							{
								return parent.var["quantidade"].value;
							}
							set
							{
								parent.var["quantidade"].Dirty = true;
								parent.var["quantidade"].value = value;
							}
						}
						
						#endregion
						
					}
				}
				#endregion
				
				#region ===[ LinkItemNO ]===
				//===[ LinkEstoqueNO ]==================================================================================================
				//Ok - Célio em 05/07/2005
				public class LinkItemNo : allClass
				{
					
					private LinkItemNoCampos mCampos; //Interfaces.iEVT.Primitivas.iLinkEstoqueNo
					
					public LinkItemNo() : base("EVT_Link_Item_No")
					{
						mCampos = new LinkItemNoCampos(this);
					}
					
					public LinkItemNo(tipos.tiposConection TipoConexao) : base("EVT_Link_Item_No", TipoConexao)
					{
						mCampos = new LinkItemNoCampos(this);
					}
					
					public Interfaces.iEVT.Primitivas.iLinkItemNo Campos
					{
						get
						{
							return mCampos;
						}
						set
						{
							mCampos = value;
							if (value != null)
							{
								base.toObject = mCampos.Parent;
							}
						}
					}
					
					private class LinkItemNoCampos : Interfaces.iEVT.Primitivas.iLinkItemNo
					{
						
						
						private allClass mParent;
						public LinkItemNoCampos(allClass allClass)
						{
							this.mParent = allClass;
						}
						internal allClass Parent
						{
							get
							{
								return this.mParent;
							}
							set
							{
								this.mParent = value;
							}
						}
						public void Clear_filters()
						{
							Parent.Clear_filters();
						}
						
						public void Clear_vars()
						{
							Parent.Clear_vars();
						}
						
						public void salvar(bool noCommit)
						{
							this.Salvar(noCommit);
						}
						
						public void Salvar(bool noCommit)
						{
							string filtro;
							filtro = " idItem = " + this.idItem;
							this.Parent.SalvarPadrao(noCommit, filtro);
						}
						public void salvar()
						{
							this.Salvar();
						}
						
						public void Salvar()
						{
							this.Salvar(false);
						}
						
						public void Dispose()
						{
							Parent = null;
						}
						
						#region    Propriedades - Fields
						
						public string Key
						{
							get
							{
								return this.ID.ToString();
							}
						}
						
						public int ID
						{
							get
							{
								return Parent.var["IdLink_Item_No"].value;
							}
						}
						
						public int idItem
						{
							get
							{
								return Parent.var["idItem"].value;
							}
							set
							{
								Parent.var["idItem"].value = value;
								Parent.var["idItem"].Dirty = true;
							}
						}
						
						public int idNo
						{
							get
							{
								return Parent.var["IdNo"].value;
							}
							set
							{
								Parent.var["IdNo"].value = value;
								Parent.var["IdNo"].Dirty = true;
							}
						}
						#endregion
						
						
					}
				}
				#endregion
				
				#region ===[ StatusReal ]===
				//===[ StatusReal ]==================================================================================================
				//Ok - Célio em 17/11/2005
				public class StatusReal : allClass
				{
					
					private StatusRealCampos mCampos; //Interfaces.iEVT.Primitivas.iStatusReal
					
					public StatusReal() : base("EVT_StatusReal")
					{
						mCampos = new StatusRealCampos(this);
					}
					
					public StatusReal(tipos.tiposConection TipoConexao) : base("EVT_StatusReal", TipoConexao)
					{
						mCampos = new StatusRealCampos(this);
					}
					
					public void GetFieldsFromIdObj(string idobj)
					{
						this.Clear_filters();
						this.filterWhere = "idobj = \'" + idobj + "\'";
						this.getFields();
					}
					public void GetFieldsFromCodBarras(string codbarras)
					{
						this.Clear_filters();
						this.filterWhere = "codbarras=\'" + codbarras + "\'";
						this.getFields();
					}
					public Interfaces.iEVT.Primitivas.iStatusReal Campos
					{
						get
						{
							return mCampos;
						}
						set
						{
							mCampos = value;
							if (value != null)
							{
								base.toObject = mCampos.Parent;
							}
						}
					}
					
					private class StatusRealCampos : Interfaces.iEVT.Primitivas.iStatusReal
					{
						
						
						private allClass mParent;
						
						public StatusRealCampos(allClass allClass)
						{
							this.mParent = allClass;
						}
						
						internal allClass Parent
						{
							get
							{
								return this.mParent;
							}
							set
							{
								this.mParent = value;
							}
						}
						
						public void Clear_filters()
						{
							Parent.Clear_filters();
						}
						
						public void Clear_vars()
						{
							Parent.Clear_vars();
						}
						
						public void salvar(bool noCommit)
						{
							this.Salvar(noCommit);
						}
						
						public void Salvar(bool noCommit)
						{
							string filtro;
							filtro = " CodBarras = \'" + this.CodBarras + "\'";
							this.Parent.SalvarPadrao(noCommit, filtro);
						}
						
						public void salvar()
						{
							this.Salvar();
						}
						
						public void Salvar()
						{
							this.Salvar(false);
						}
						
						public void Dispose()
						{
							Parent = null;
						}
						
						#region    Propriedades - Fields
						
						public string Key
						{
							get
							{
								return this.ID.ToString();
							}
						}
						
						public int ID
						{
							get
							{
								return Parent.var["ID"].value;
							}
						}
						
						public int IdItem
						{
							get
							{
								return Parent.var["idItem"].value;
							}
							set
							{
								Parent.var["idItem"].value = value;
								Parent.var["idItem"].Dirty = true;
							}
						}
						
						public int IdKit
						{
							get
							{
								return Parent.var["IdKit"].value;
							}
							set
							{
								Parent.var["IdKit"].value = value;
								Parent.var["IdKit"].Dirty = true;
							}
						}
						
						public string IdObj
						{
							get
							{
								return Parent.var["IdObj"].value;
							}
							set
							{
								Parent.var["IdObj"].value = value;
								Parent.var["IdObj"].Dirty = true;
							}
						}
						
						public int IdEntidade
						{
							get
							{
								return Parent.var["idEntidade"].value;
							}
							set
							{
								Parent.var["idEntidade"].value = value;
								Parent.var["idEntidade"].Dirty = true;
							}
						}
						
						public string CodBarras
						{
							get
							{
								return Parent.var["CodBarras"].value;
							}
							set
							{
								Parent.var["CodBarras"].value = value;
								Parent.var["CodBarras"].Dirty = true;
							}
						}
						
						public DateTime DataDevolucao
						{
							get
							{
								return Parent.var["DataDevolucao"].value;
							}
							set
							{
								Parent.var["DataDevolucao"].value = value;
								Parent.var["DataDevolucao"].Dirty = true;
							}
						}
						
						public float Status
						{
							get
							{
								return Parent.var["Status"].value;
							}
							set
							{
								Parent.var["Status"].value = value;
								Parent.var["Status"].Dirty = true;
							}
						}
						
						#endregion
					}
				}
				#endregion
				
				#region ===[ StatusHistorico ]===
				//===[ StatusHistorico ]==================================================================================================
				//Ok - Célio em 17/11/2005
				public class StatusHistorico : allClass
				{
					
					private StatusHistoricoCampos mCampos; //Interfaces.iEVT.Primitivas.iStatusReal
					
					public StatusHistorico() : base("EVT_StatusHistorico")
					{
						mCampos = new StatusHistoricoCampos(this);
					}
					
					public StatusHistorico(tipos.tiposConection TipoConexao) : base("EVT_StatusHistorico", TipoConexao)
					{
						mCampos = new StatusHistoricoCampos(this);
					}
					
					public Interfaces.iEVT.Primitivas.iStatusReal Campos
					{
						get
						{
							return mCampos;
						}
						set
						{
							mCampos = value;
							if (value != null)
							{
								base.toObject = mCampos.Parent;
							}
						}
					}
					
					private class StatusHistoricoCampos : Interfaces.iEVT.Primitivas.iStatusHistorico
					{
						
						
						private allClass mParent;
						
						public StatusHistoricoCampos(allClass allClass)
						{
							this.mParent = allClass;
						}
						
						internal allClass Parent
						{
							get
							{
								return this.mParent;
							}
							set
							{
								this.mParent = value;
							}
						}
						
						public void Clear_filters()
						{
							Parent.Clear_filters();
						}
						
						public void Clear_vars()
						{
							Parent.Clear_vars();
						}
						
						public void salvar(bool noCommit)
						{
							this.Salvar(noCommit);
						}
						
						public void Salvar(bool noCommit)
						{
							string filtro;
							filtro = " CodBarras = \'" + this.CodBarras + "\'";
							this.Parent.SalvarPadrao(noCommit, filtro);
						}
						
						public void salvar()
						{
							this.Salvar();
						}
						
						public void Salvar()
						{
							this.Salvar(false);
						}
						
						public void Dispose()
						{
							Parent = null;
						}
						
						#region    Propriedades - Fields
						
						public string Key
						{
							get
							{
								return this.ID.ToString();
							}
						}
						
						public int ID
						{
							get
							{
								return Parent.var["ID"].value;
							}
						}
						
						public int IdItem
						{
							get
							{
								return Parent.var["idItem"].value;
							}
							set
							{
								Parent.var["idItem"].value = value;
								Parent.var["idItem"].Dirty = true;
							}
						}
						
						public int IdKit
						{
							get
							{
								return Parent.var["IdKit"].value;
							}
							set
							{
								Parent.var["IdKit"].value = value;
								Parent.var["IdKit"].Dirty = true;
							}
						}
						
						public string IdObj
						{
							get
							{
								return Parent.var["IdObj"].value;
							}
							set
							{
								Parent.var["IdObj"].value = value;
								Parent.var["IdObj"].Dirty = true;
							}
						}
						
						public int IdEntidade
						{
							get
							{
								return Parent.var["idEntidade"].value;
							}
							set
							{
								Parent.var["idEntidade"].value = value;
								Parent.var["idEntidade"].Dirty = true;
							}
						}
						
						public DateTime Dia
						{
							get
							{
								return Parent.var["Dia"].value;
							}
							set
							{
								Parent.var["Dia"].value = value;
								Parent.var["Dia"].Dirty = true;
							}
						}
						
						public string CodBarras
						{
							get
							{
								return Parent.var["CodBarras"].value;
							}
							set
							{
								Parent.var["CodBarras"].value = value;
								Parent.var["CodBarras"].Dirty = true;
							}
						}
						
						public float Status
						{
							get
							{
								return Parent.var["Status"].value;
							}
							set
							{
								Parent.var["Status"].value = value;
								Parent.var["Status"].Dirty = true;
							}
						}
						
						#endregion
					}
				}
				#endregion
				
				#region ===[ StatusPresumido ]===
				//===[ StatusReal ]==================================================================================================
				//Ok - Célio em 17/11/2005
				public class StatusPresumido : allClass
				{
					
					private StatusPresumidoCampos mCampos; //Interfaces.iEVT.Primitivas.iStatusPresumido
					
					public StatusPresumido() : base("EVT_StatusPresumido")
					{
						mCampos = new StatusPresumidoCampos(this);
					}
					
					public StatusPresumido(tipos.tiposConection TipoConexao) : base("EVT_StatusPresumido", TipoConexao)
					{
						mCampos = new StatusPresumidoCampos(this);
					}
					
					public void GetFieldsFromIdObjDia(string idObj, DateTime Dia)
					{
						this.Clear_filters();
						this.filterWhere = "idobj = \'" + idObj.Trim() + "\' and Dia=\'" + Dia.ToString("MM/dd/yyyy") + "\'";
						this.getFields();
						if (this.Campos.ID > 0)
						{
							this.Campos.Dia_Key = this.Campos.Dia_Key;
							this.Campos.IdObj_Key = this.Campos.IdObj_Key;
							this.Campos.IdOrcamento_Key = this.Campos.IdOrcamento_Key;
						}
					}
					
					public Interfaces.iEVT.Primitivas.iStatusPresumido Campos
					{
						get
						{
							return mCampos;
						}
						set
						{
							mCampos = value;
							if (value != null)
							{
								base.toObject = mCampos.Parent;
							}
						}
					}
					
					private class StatusPresumidoCampos : Interfaces.iEVT.Primitivas.iStatusPresumido
					{
						
						
						private allClass mParent;
						
						public StatusPresumidoCampos(allClass allClass)
						{
							this.mParent = allClass;
						}
						
						internal allClass Parent
						{
							get
							{
								return this.mParent;
							}
							set
							{
								this.mParent = value;
							}
						}
						
						public void Clear_filters()
						{
							Parent.Clear_filters();
						}
						
						public void Clear_vars()
						{
							Parent.Clear_vars();
						}
						
						public void salvar(bool noCommit)
						{
							this.Salvar(noCommit);
						}
						
						public void Salvar(bool noCommit)
						{
							if (this.IdObj_Key.Substring(0, 1) == "I")
							{
								this.IdItem = this.IdObj_Key.Substring(1);
							}
							else
							{
								this.IdKit = this.IdObj_Key.Substring(1);
							}
							string filtro;
							filtro = " idObj = \'" + this.IdObj_Key + "\' and idOrcamento = " + this.IdOrcamento_Key + " and Dia = \'" + this.Dia_Key.ToString("MM/dd/yyyy") + "\'";
							this.Parent.SalvarPadrao(noCommit, filtro);
						}
						
						public void salvar()
						{
							this.Salvar();
						}
						
						public void Salvar()
						{
							this.Salvar(false);
						}
						
						public void Dispose()
						{
							Parent = null;
						}
						
						#region    Propriedades - Fields
						
						public string Key
						{
							get
							{
								return this.ID.ToString();
							}
						}
						
						public int ID
						{
							get
							{
								return Parent.var["ID"].value;
							}
						}
						
						public int IdItem
						{
							get
							{
								return Parent.var["idItem"].value;
							}
							set
							{
								Parent.var["idItem"].value = value;
								Parent.var["idItem"].Dirty = true;
							}
						}
						
						public int IdKit
						{
							get
							{
								return Parent.var["IdKit"].value;
							}
							set
							{
								Parent.var["IdKit"].value = value;
								Parent.var["IdKit"].Dirty = true;
							}
						}
						
						public int idOrcObj
						{
							get
							{
								return this.IdOrcObj;
							}
							set
							{
								this.IdOrcObj = value;
							}
						}
						
						public int IdOrcObj
						{
							get
							{
								return Parent.var["IdOrcObj"].value;
							}
							set
							{
								Parent.var["IdOrcObj"].value = value;
								Parent.var["IdOrcObj"].Dirty = true;
							}
						}
						
						public string IdObj_Key
						{
							get
							{
								return Parent.var["IdObj"].value;
							}
							set
							{
								Parent.var["IdObj"].value = value;
								Parent.var["IdObj"].Dirty = true;
							}
						}
						
						public int IdOrcamento_Key
						{
							get
							{
								return Parent.var["idOrcamento"].value;
							}
							set
							{
								Parent.var["idOrcamento"].value = value;
								Parent.var["idOrcamento"].Dirty = true;
							}
						}
						
						public DateTime Dia_Key
						{
							get
							{
								return Parent.var["Dia"].value;
							}
							set
							{
								Parent.var["Dia"].value = value;
								Parent.var["Dia"].Dirty = true;
							}
						}
						
						public int Quantidade
						{
							get
							{
								return Parent.var["Quantidade"].value;
							}
							set
							{
								Parent.var["Quantidade"].value = value;
								Parent.var["Quantidade"].Dirty = true;
							}
						}
						
						public float Status
						{
							get
							{
								return Parent.var["Status"].value;
							}
							set
							{
								Parent.var["Status"].value = value;
								Parent.var["Status"].Dirty = true;
							}
						}
						
						#endregion
						
					}
				}
				#endregion
				
				#region ===[ Contas a Pagar e a Receber ]===
				//===[ Contas a Pagar e a Receber ]==================================================================================================
				//Ok - Célio em 27/09/2005
				public class ContasPagarReceber : allClass
				{
					
					private Interfaces.iEVT.Primitivas.iContasPagarReceber mCampos;
					
					public ContasPagarReceber() : base("EVT_ContasPagarReceber")
					{
						mCampos = new ContasPagarReceberCampos(this);
					}
					
					public ContasPagarReceber(tipos.tiposConection TipoConexao) : base("EVT_ContasPagarReceber", TipoConexao)
					{
						mCampos = new ContasPagarReceberCampos(this);
					}
					
					public Interfaces.iEVT.Primitivas.iContasPagarReceber Campos
					{
						get
						{
							return mCampos;
						}
						set
						{
							mCampos = value;
						}
					}
					
					public override void Dispose()
					{
						if (mCampos != null)
						{
							mCampos.Dispose();
						}
						mCampos = null;
					}
					
					private class ContasPagarReceberCampos : Interfaces.iEVT.Primitivas.iContasPagarReceber
					{
						
						private allClass mParent;
						
						public ContasPagarReceberCampos(allClass allClass)
						{
							this.mParent = allClass;
						}
						
						internal allClass Parent
						{
							get
							{
								return this.mParent;
							}
							set
							{
								this.mParent = value;
							}
						}
						
						#region    Propriedades - Fields
						
						public int ID
						{
							get
							{
								return Parent.var["ID"].value;
							}
						}
						
						public int TipoConta
						{
							get
							{
								return Parent.var["TipoConta"].value;
							}
							set
							{
								Parent.var["TipoConta"].value = value;
							}
						}
						
						public string Descricao
						{
							get
							{
								return Parent.var["Descricao"].value;
							}
							set
							{
								Parent.var["Descricao"].value = value;
							}
						}
						
						public DateTime Vencimento
						{
							get
							{
								return Parent.var["Vencimento"].value;
							}
							set
							{
								Parent.var["Vencimento"].Dirty = true;
								Parent.var["Vencimento"].value = value;
							}
						}
						
						public double ValorTotal
						{
							get
							{
								return Parent.var["ValorTotal"].value;
							}
							set
							{
								Parent.var["ValorTotal"].value = value;
							}
						}
						
						public int Parcelas
						{
							get
							{
								return Parent.var["Parcelas"].value;
							}
							set
							{
								Parent.var["Parcelas"].value = value;
							}
						}
						
						public string AnotacoesAdicionais
						{
							get
							{
								return Parent.var["AnotacoesAdicionais"].value;
							}
							set
							{
								Parent.var["AnotacoesAdicionais"].value = value;
							}
						}
						
						#endregion
						
						public void Clear_filters()
						{
							mParent.Clear_filters();
						}
						
						public void Clear_vars()
						{
							mParent.Clear_vars();
						}
						
						public string Key
						{
							get
							{
								return this.ID.ToString();
							}
						}
						
						public void salvar()
						{
							this.Salvar();
						}
						
						public void Salvar()
						{
							this.Salvar(false);
						}
						
						public void salvar(bool noCommit)
						{
							this.Salvar(noCommit);
						}
						
						public void Salvar(bool noCommit)
						{
							string filtro;
							filtro = " ID = " + this.ID.ToString();
							this.Parent.SalvarPadrao(noCommit, filtro);
						}
						
						public void Dispose()
						{
							this.mParent = null;
						}
					}
					
				}
				#endregion
				
				#region ===[ Contas a Pagar e a Receber Parcela]===
				//===[ Contas a Pagar e a Receber Parcela]==================================================================================================
				//Ok - Célio em 27/09/2005
				public class ContasPagarReceber_Parcelas : allClass
				{
					
					
					private Interfaces.iEVT.Primitivas.iContasPagarReceber_Parcelas mCampos;
					
					public ContasPagarReceber_Parcelas() : base("EVT_ContasPagarReceber_Parcelas")
					{
						mCampos = new ContasPagarReceber_ParcelasCampos(this);
					}
					
					public ContasPagarReceber_Parcelas(tipos.tiposConection TipoConexao) : base("EVT_ContasPagarReceber", TipoConexao)
					{
						mCampos = new ContasPagarReceber_ParcelasCampos(this);
					}
					
					public Interfaces.iEVT.Primitivas.iContasPagarReceber_Parcelas Campos
					{
						get
						{
							return mCampos;
						}
						set
						{
							mCampos = value;
						}
					}
					
					public override void Dispose()
					{
						if (mCampos != null)
						{
							mCampos.Dispose();
						}
						mCampos = null;
					}
					
					private class ContasPagarReceber_ParcelasCampos : Interfaces.iEVT.Primitivas.iContasPagarReceber_Parcelas
					{
						
						
						private allClass mParent;
						
						public ContasPagarReceber_ParcelasCampos(allClass allClass)
						{
							this.mParent = allClass;
						}
						
						internal allClass Parent
						{
							get
							{
								return this.mParent;
							}
							set
							{
								this.mParent = value;
							}
						}
						
						#region Propriedade - Fields
						public int ID
						{
							get
							{
								return Parent.var["ID"].value;
							}
						}
						
						public int idConta
						{
							get
							{
								return Parent.var["idConta"].value;
							}
							set
							{
								Parent.var["idConta"].Dirty = true;
								Parent.var["idConta"].value = value;
							}
						}
						
						public DateTime Data
						{
							get
							{
								return Parent.var["Data"].value;
							}
							set
							{
								Parent.var["Data"].Dirty = true;
								Parent.var["Data"].value = value;
							}
						}
						
						public double Valor
						{
							get
							{
								return Parent.var["Valor"].value;
							}
							set
							{
								Parent.var["Valor"].Dirty = true;
								Parent.var["Valor"].value = value;
							}
						}
						#endregion
						
						public void Clear_filters()
						{
							mParent.Clear_filters();
						}
						
						public void Clear_vars()
						{
							mParent.Clear_vars();
						}
						
						public string Key
						{
							get
							{
								return this.idConta.ToString() + this.Data.ToShortDateString();
							}
						}
						
						public void salvar()
						{
							this.Salvar();
						}
						
						public void Salvar()
						{
							this.Salvar(false);
						}
						
						public void salvar(bool noCommit)
						{
							this.Salvar(noCommit);
						}
						
						public void Salvar(bool noCommit)
						{
							string filtro;
							filtro = " ID = " + this.ID.ToString();
							this.Parent.SalvarPadrao(noCommit, filtro);
						}
						
						public void Dispose()
						{
							mParent = null;
						}
					}
					
					
				}
				#endregion
				
				#region ===[ Custom Relatório de Orçamentos ]===
				//Ok - Célio em 24/03/2006
				public class CustomROrc : allClass
				{
					
					private Interfaces.iEVT.Primitivas.iCustomROrc mCampos;
					
					public CustomROrc() : base("EVT_CustomROrc")
					{
						mCampos = new CustomROrc_Campos(this);
					}
					
					public CustomROrc(tipos.tiposConection TipoConexao) : base("EVT_CustomROrc", TipoConexao)
					{
						mCampos = new CustomROrc_Campos(this);
					}
					
					public Interfaces.iEVT.Primitivas.iCustomROrc Campos
					{
						get
						{
							return this.mCampos;
						}
						set
						{
							this.mCampos = value;
						}
					}
					
					public override void Dispose()
					{
						if (mCampos != null)
						{
							mCampos.Dispose();
						}
						mCampos = null;
					}
					
					private class CustomROrc_Campos : Interfaces.iEVT.Primitivas.iCustomROrc
					{
						
						
						private allClass mParent;
						
						public CustomROrc_Campos(allClass parent)
						{
							this.mParent = parent;
						}
						
						public allClass Parent
						{
							get
							{
								return this.mParent;
							}
							set
							{
								this.mParent = value;
							}
						}
						
						#region Propriedades - Fields
						public int ID
						{
							get
							{
								return Parent.var["idCustomRO"].value;
							}
						}
						
						public int IdOrcamento
						{
							get
							{
								return Parent.var["idOrcamento"].value;
							}
							set
							{
								Parent.var["idOrcamento"].Dirty = true;
								Parent.var["idOrcamento"].value = value;
							}
						}
						
						public string PathImgCab
						{
							get
							{
								return Parent.var["PathImgCab"].value;
							}
							set
							{
								Parent.var["PathImgCab"].Dirty = true;
								Parent.var["PathImgCab"].value = value;
							}
						}
						
						public string TxtApresentacao
						{
							get
							{
								return Parent.var["TxtApresentacao"].value;
							}
							set
							{
								Parent.var["TxtApresentacao"].Dirty = true;
								Parent.var["TxtApresentacao"].value = value;
							}
						}
						#endregion
						
						public void Clear_filters()
						{
							mParent.Clear_filters();
						}
						
						public void Clear_vars()
						{
							mParent.Clear_vars();
						}
						
						public string Key
						{
							get
							{
								return this.IdOrcamento.ToString();
							}
						}
						
						public void salvar()
						{
							this.Salvar();
						}
						
						public void Salvar()
						{
							this.Salvar(false);
						}
						
						public void salvar(bool noCommit)
						{
							this.Salvar(noCommit);
						}
						
						public void Salvar(bool noCommit)
						{
							string filtro;
							filtro = " idCustomRO = " + this.ID.ToString();
							this.Parent.SalvarPadrao(noCommit, filtro);
						}
						
						public void Dispose()
						{
							mParent = null;
						}
					}
					
				}
				#endregion
				
				#region ===[ Custom Relatório de Orcamentos - Grupos ] ===
				//Ok - Célio em 24/03/2006
				public class CustomROrc_Grupos : allClass
				{
					
					private Interfaces.iEVT.Primitivas.iCustomROrc_Grupos mCampos;
					
					public CustomROrc_Grupos() : this(tipos.tiposConection.Default_)
					{
					}
					
					public CustomROrc_Grupos(tipos.tiposConection TipoConexao) : base("EVT_CustomROrc_Grupos", TipoConexao)
					{
						mCampos = new CustomROrc_Grupos_Campos(this);
					}
					
					public Interfaces.iEVT.Primitivas.iCustomROrc_Grupos Campos
					{
						get
						{
							return this.mCampos;
						}
						set
						{
							this.mCampos = value;
						}
					}
					
					public override void Dispose()
					{
						if (mCampos != null)
						{
							mCampos.Dispose();
						}
						mCampos = null;
					}
					
					private class CustomROrc_Grupos_Campos : Interfaces.iEVT.Primitivas.iCustomROrc_Grupos
					{
						
						
						private allClass mParent;
						
						public CustomROrc_Grupos_Campos(allClass parent)
						{
							this.mParent = parent;
						}
						
						public allClass Parent
						{
							get
							{
								return this.mParent;
							}
							set
							{
								this.mParent = value;
							}
						}
						
						#region Propriedades - Fields
						public int ID
						{
							get
							{
								return Parent.var["IdCustomRO_Grupos"].value;
							}
						}
						
						public int IdCustomRO
						{
							get
							{
								return Parent.var["IdCustomRO"].value;
							}
							set
							{
								Parent.var["IdCustomRO"].Dirty = true;
								Parent.var["IdCustomRO"].value = value;
							}
						}
						
						public string SubTitulo
						{
							get
							{
								return Parent.var["SubTitulo"].value;
							}
							set
							{
								Parent.var["SubTitulo"].Dirty = true;
								Parent.var["SubTitulo"].value = value;
							}
						}
						
						public string Titulo
						{
							get
							{
								return Parent.var["Titulo"].value;
							}
							set
							{
								Parent.var["Titulo"].Dirty = true;
								Parent.var["Titulo"].value = value;
							}
						}
						#endregion
						
						public void Clear_filters()
						{
							mParent.Clear_filters();
						}
						
						public void Clear_vars()
						{
							mParent.Clear_vars();
						}
						
						public string Key
						{
							get
							{
								return this.IdCustomRO.ToString() + this.Titulo + this.SubTitulo;
							}
						}
						
						public void salvar()
						{
							this.Salvar();
						}
						
						public void Salvar()
						{
							this.Salvar(false);
						}
						
						public void salvar(bool noCommit)
						{
							this.Salvar(noCommit);
						}
						
						public void Salvar(bool noCommit)
						{
							string filtro;
							filtro = " idCustomRO_Grupos = " + this.ID.ToString();
							this.Parent.SalvarPadrao(noCommit, filtro);
						}
						
						public void Dispose()
						{
							mParent = null;
						}
					}
				}
				#endregion
				
				#region ===[ Custom Relatório de Orçamentos - Itens dos Grupos ]===
				//Ok - Célio em 24/03/2006
				public class CustomROrc_ItensGrupo : allClass
				{
					
					private Interfaces.iEVT.Primitivas.iCustomROrc_ItensGrupo mCampos;
					
					public CustomROrc_ItensGrupo() : this(tipos.tiposConection.Default_)
					{
					}
					
					public CustomROrc_ItensGrupo(tipos.tiposConection TipoConexao) : base("EVT_CustomROrc_ItensGrupo", TipoConexao)
					{
						mCampos = new CustomROrc_ItensGrupo_Campos(this);
					}
					
					public Interfaces.iEVT.Primitivas.iCustomROrc_ItensGrupo Campos
					{
						get
						{
							return this.mCampos;
						}
						set
						{
							this.mCampos = value;
						}
					}
					
					public override void Dispose()
					{
						if (mCampos != null)
						{
							mCampos.Dispose();
						}
						mCampos = null;
					}
					
					public class CustomROrc_ItensGrupo_Campos : Interfaces.iEVT.Primitivas.iCustomROrc_ItensGrupo
					{
						
						private allClass mParent;
						
						public CustomROrc_ItensGrupo_Campos(allClass parent)
						{
							this.mParent = parent;
						}
						
						public allClass Parent
						{
							get
							{
								return this.mParent;
							}
							set
							{
								this.mParent = value;
							}
						}
						
						#region Propriedades - Fields
						public string Descricao
						{
							get
							{
								return Parent.var["Descricao"].value;
							}
							set
							{
								Parent.var["Descricao"].Dirty = true;
								Parent.var["Descricao"].value = value;
							}
						}
						
						public int IdCustomRO_Grupos
						{
							get
							{
								return Parent.var["IdCustomRO_Grupos"].value;
							}
							set
							{
								Parent.var["IdCustomRO_Grupos"].Dirty = true;
								Parent.var["IdCustomRO_Grupos"].value = value;
							}
						}
						
						public string IdObj
						{
							get
							{
								return Parent.var["IdObj"].value;
							}
							set
							{
								Parent.var["IdObj"].Dirty = true;
								Parent.var["IdObj"].value = value;
							}
						}
						
						public string Marca
						{
							get
							{
								return Parent.var["Marca"].value;
							}
							set
							{
								Parent.var["Marca"].Dirty = true;
								Parent.var["Marca"].value = value;
							}
						}
						
						public string Modelo
						{
							get
							{
								return Parent.var["Modelo"].value;
							}
							set
							{
								Parent.var["Modelo"].Dirty = true;
								Parent.var["Modelo"].value = value;
							}
						}
						
						public int Quantidade
						{
							get
							{
								return Parent.var["Quantidade"].value;
							}
							set
							{
								Parent.var["Quantidade"].Dirty = true;
								Parent.var["Quantidade"].value = value;
							}
						}
						#endregion
						
						public void Clear_filters()
						{
							mParent.Clear_filters();
						}
						
						public void Clear_vars()
						{
							mParent.Clear_vars();
						}
						
						public string Key
						{
							get
							{
								return this.IdCustomRO_Grupos.ToString() + this.IdObj.ToString();
							}
						}
						
						public void Salvar()
						{
							this.Salvar(false);
						}
						
						public void Salvar(bool noCommit)
						{
							string filtro;
							filtro = " idCustomRO_Grupos = " + this.IdCustomRO_Grupos.ToString() + "and idObj=" + this.IdObj.ToString();
							this.Parent.SalvarPadrao(noCommit, filtro);
						}
						
						public void Dispose()
						{
							mParent = null;
						}
						
					}
				}
				#endregion
			}
			
		}
	}
	
}
