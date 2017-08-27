using System.Data;
using System.Collections;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System;


namespace NBdbm
{
	namespace Fachadas
	{
		namespace plxCOBR
		{
			
			public class CadastroEntidade : Fachadas.CTR.CadastroEntidade
			{
				
				
				protected tipos.TipoEntidade mTipoEntidade;
				protected Fachadas.NbCollection mColDividas = new Fachadas.NbCollection();
				protected Fachadas.NbCollection mColAcionamentos = new Fachadas.NbCollection();
				protected Fachadas.plxCOBR.primitivas.Divida mDivida;
				protected Fachadas.plxCOBR.primitivas.Acionamentos mAcionamentos;
				protected Fachadas.plxCOBR.primitivas.Tarifas mTarifas;
				
				public CadastroEntidade(tipos.tiposConection TipoConexao, tipos.TipoEntidade TipoEntidade) : base(TipoConexao)
				{
					mTipoEntidade = TipoEntidade;
					mDivida = new Fachadas.plxCOBR.primitivas.Divida(TipoConexao);
					mAcionamentos = new Fachadas.plxCOBR.primitivas.Acionamentos(TipoConexao);
					mTarifas = new Fachadas.plxCOBR.primitivas.Tarifas(TipoConexao);
				}
				
				public NbCollection colecaoDividas
				{
					get
					{
						return mColDividas;
					}
				}
				
				public NbCollection colecaoAcionamentos
				{
					get
					{
						return this.mColAcionamentos;
					}
				}
				
				public void NovaDivida()
				{
					mDivida = new Fachadas.plxCOBR.primitivas.Divida(mTipoConexao);
				}
				
				public Interfaces.iCOBR.Primitivas.iAcionamentos Acionamentos
				{
					get
					{
						return this.mAcionamentos.Campos;
					}
					set
					{
						this.mAcionamentos.Campos = value;
					}
				}
				
				public Interfaces.iCOBR.Primitivas.iDivida Divida
				{
					get
					{
						return this.mDivida.Campos;
					}
					set
					{
						this.mDivida.Campos = value;
					}
				}
				
				public Interfaces.iCOBR.Primitivas.iTarifas Tarifa
				{
					get
					{
						return this.mTarifas.Campos;
					}
					set
					{
						this.mTarifas.Campos = value;
					}
				}
				
				public new void Salvar(bool noCommit)
				{
					try
					{
						//Salvando a Entidade
						base.Salvar(true);
						
						//Salvando as Dividas
						if (this.mTipoEntidade == tipos.TipoEntidade.Devedores)
						{
							foreach (Interfaces.iCOBR.Primitivas.iDivida mDiv in mColDividas.Values)
							{
								mDiv.idEntidade = this.Entidade.ID;
								mDiv.salvar(true);
							}
						}
						
						//Salvando as Tarifas
						if (this.mTipoEntidade == tipos.TipoEntidade.Clientes)
						{
							mTarifas.Campos.idEntidade = this.Entidade.ID;
							mTarifas.Campos.salvar(true);
						}
						
						//Finaliza a Transação
						self.AdmDB.FinalizaTransaction(noCommit);
						
					}
					catch (Exception ex)
					{
						NBexception mNBEx = new NBexception("Não foi possível Salvar o Cadastro de " + mTipoEntidade.ToString(), ex);
						mNBEx.Source = "NBdbm.Fachadas.plxCOBR.CadastroEntidade.Salvar";
						throw (mNBEx);
					}
				}
				
				//Public Overloads Sub Excluir(ByVal noCommit As Boolean)
				//    Me.Entidade.tipo()
				//End Sub
				public void getFieldsFromEntidade(double idEntidade)
				{
					this.mColDividas.Clear();
					this.mColAcionamentos.Clear();
					base.getFieldsFromEntidade(idEntidade);
					
					//Se a Entidade for um Devedor então busca as suas dividas.
					if (this.mTipoEntidade == tipos.TipoEntidade.Devedores)
					{
						
						//Busca os dados das dívidas
						this.mDivida.filterWhere = "idEntidade = " + idEntidade;
						foreach (Fields mFields in this.mDivida.CriaColecaoFields().Values)
						{
							NBdbm.Fachadas.plxCOBR.primitivas.Divida mmItemColecao;
							//Verifica se foi usado outro tipo de conexao que não a padrao
							if (mTipoConexao != null)
							{
								mmItemColecao = new NBdbm.Fachadas.plxCOBR.primitivas.Divida(mTipoConexao);
							}
							else
							{
								mmItemColecao = new NBdbm.Fachadas.plxCOBR.primitivas.Divida();
							}
							mmItemColecao.var = mFields;
							this.mColDividas.Add(mmItemColecao.Campos.Key, ((object) mmItemColecao.Campos));
						}
						
						//Busca os dados dos Acionamentos
						this.mAcionamentos.filterWhere = "idEntidade = " + idEntidade;
						this.mAcionamentos.filterTop = 15;
						this.mAcionamentos.filterOrderBy = "ID desc";
						
						foreach (Fields mFields in this.mAcionamentos.CriaColecaoFields().Values)
						{
							NBdbm.Fachadas.plxCOBR.primitivas.Acionamentos mmItemColecao;
							//Verifica se foi usado outro tipo de conexao que não a padrao
							if (mTipoConexao != null)
							{
								mmItemColecao = new NBdbm.Fachadas.plxCOBR.primitivas.Acionamentos(mTipoConexao);
							}
							else
							{
								mmItemColecao = new NBdbm.Fachadas.plxCOBR.primitivas.Acionamentos();
							}
							mmItemColecao.var = mFields;
							this.mColAcionamentos.Add(mmItemColecao.Campos.Key, ((object) mmItemColecao.Campos));
						}
						
					}
					
					//Se a Entidade for um Cliente então busca as tarifas.
					if (this.mTipoEntidade == tipos.TipoEntidade.Clientes)
					{
						this.mTarifas.getFields("idEntidade = " + idEntidade);
					}
					
				}
				
				public void Dispose()
				{
					base.Dispose();
					if (mColDividas != null)
					{
						mColDividas.Dispose();
					}
					mColDividas = null;
					if (mColAcionamentos != null)
					{
						mColAcionamentos.Dispose();
					}
					mColAcionamentos = null;
					if (mDivida != null)
					{
						mDivida.Dispose();
					}
					mDivida = null;
					if (mAcionamentos != null)
					{
						mAcionamentos.Dispose();
					}
					mAcionamentos = null;
					if (mTarifas != null)
					{
						mTarifas.Dispose();
					}
					mTarifas = null;
				}
				
			}
			
			public class CadastroAcionamentos : NBdbm.Interfaces.iCOBR.iCadastroAcionamentos
			{
				
				
				protected NBdbm.Fachadas.plxCOBR.CadastroEntidade mCadDevedor;
				protected NbCollection mColAcionamentosNovos;
				protected NBdbm.tipos.tiposConection mTipoConexao;
				
				
				public CadastroAcionamentos(tipos.tiposConection pTipoConexao)
				{
					mTipoConexao = pTipoConexao;
					mCadDevedor = new NBdbm.Fachadas.plxCOBR.CadastroEntidade(pTipoConexao, tipos.TipoEntidade.Devedores);
					mColAcionamentosNovos = new NbCollection();
				}
				
				public Interfaces.iCOBR.Primitivas.iAcionamentos Acionamento
				{
					get
					{
						return this.mCadDevedor.Acionamentos;
					}
					set
					{
						this.mCadDevedor.Acionamentos = value;
					}
				}
				
				public NbCollection ColecaoAcionamentos
				{
					get
					{
						return this.mCadDevedor.colecaoAcionamentos;
					}
				}
				
				public NbCollection ColecaoNovosAcionamentos
				{
					get
					{
						return this.mColAcionamentosNovos;
					}
				}
				
				public CadastroEntidade FichaDevedor
				{
					get
					{
						return this.mCadDevedor;
					}
				}
				
				public void GetFieldsFromEntidade(double pCodigoDevedor)
				{
					
					this.mColAcionamentosNovos.Clear();
					this.mCadDevedor.getFieldsFromEntidade(pCodigoDevedor);
					
				}
				
				public void Salvar()
				{
					this.Salvar(false);
				}
				
				public void Salvar(bool NoCommit)
				{
					try
					{
						//Salvando os Acionamentos
						System.Collections.SortedList mColAcionamentosClassificas = new System.Collections.SortedList(this.mColAcionamentosNovos.Hastable);
						foreach (Interfaces.iCOBR.Primitivas.iAcionamentos mAciona in mColAcionamentosClassificas.Values)
						{
							mAciona.idEntidade = this.mCadDevedor.Entidade.ID;
							mAciona.salvar(true);
						}
						
						//Finaliza a Transação
						self.AdmDB.FinalizaTransaction(NoCommit);
						
					}
					catch (Exception ex)
					{
						NBexception mNBEx = new NBexception("Não foi possível Salvar o Cadastro de Acionamentos", ex);
						mNBEx.Source = "NBdbm.Fachadas.plxCOBR.CadastroAcionamentos.Salvar";
						throw (mNBEx);
						
					}
				}
				
				
				public virtual void Dispose()
				{
					
					if (mCadDevedor != null)
					{
						mCadDevedor.Dispose();
						mCadDevedor = null;
					}
					
					if (mColAcionamentosNovos != null)
					{
						mColAcionamentosNovos.Dispose();
						mColAcionamentosNovos = null;
					}
					
				}
				
			}
			
			public class LancamentoBaixa : Interfaces.iCOBR.iLacamentoBaixa
			{
				
				
				private tipos.tiposConection mTipoConexao;
				private NbCollection mDividasParaBaixar;
				private NBdbm.Fachadas.plxCOBR.primitivas.Baixas mBaixa;
				
				public LancamentoBaixa(tipos.tiposConection pTipoConexao)
				{
					mTipoConexao = pTipoConexao;
					mBaixa = new NBdbm.Fachadas.plxCOBR.primitivas.Baixas(mTipoConexao);
					mDividasParaBaixar = new NbCollection();
				}
				
				public Interfaces.iCOBR.Primitivas.iBaixas Baixa
				{
					get
					{
						return this.mBaixa.Campos;
					}
					set
					{
						this.mBaixa.Campos = value;
					}
				}
				
				public NbCollection DividasParaBaixar
				{
					get
					{
						return this.mDividasParaBaixar;
					}
				}
				
				public void Salvar()
				{
					this.Salvar(false);
				}
				
				public void Salvar(bool NoCommit)
				{
					
					try
					{
						
						//Atualizando Dívidas que serão Baixadas
						foreach (Interfaces.iCOBR.Primitivas.iDivida mmDivida in mDividasParaBaixar.Values)
						{
							//Salvando Baixas
							NBdbm.Fachadas.plxCOBR.primitivas.Baixas mmBaixa = new NBdbm.Fachadas.plxCOBR.primitivas.Baixas();
							mmBaixa.Campos.idDivida = mmDivida.ID;
							mmBaixa.Campos.idEntidade = mmDivida.idEntidade;
							mmBaixa.Campos.DataBaixa = mmDivida.DataBaixa;
							mmBaixa.Campos.BaixadoCliente = mmDivida.BaixaNoCliente;
							mmBaixa.Campos.NumBordero = mmDivida.BorderoBaixa;
							
							//Ocorre quando esta sendo feito uma Baixa parcial.
							//Obs: Não é feito Multiplas Baixas Parcial, então esta condição
							//só será válida quando estiver sendo feito a baixa de uma única
							//dívida parcial.
							if (mmDivida.BaixaParcial)
							{
								mmBaixa.Campos.ValorNominal = mBaixa.Campos.ValorNominal;
								mmBaixa.Campos.ValorBaixa = mBaixa.Campos.ValorBaixa;
								mmBaixa.Campos.ValorRecebido = mBaixa.Campos.ValorRecebido;
							}
							else
							{
								//Ocorre em baixas multiplas e que uma das dívidas
								//foi baixada parcialmente em outro processo de baixa.
								if (mmDivida.ValorNominal > mmDivida.ValorNominalParcial)
								{
									mmBaixa.Campos.ValorNominal = mmDivida.ValorNominalParcial;
									mmBaixa.Campos.ValorBaixa = mmDivida.ValorNominalParcial;
									mmDivida.BaixaParcial = true;
								}
								else
								{
									mmBaixa.Campos.ValorNominal = mmDivida.ValorNominal;
									mmBaixa.Campos.ValorBaixa = mmDivida.ValorNominal;
								}
								//Valor Recebido sempre será o valor recebido da baixa
								//dividido pelo número de dividas que estão sendo baixadas.
								mmBaixa.Campos.ValorRecebido = mBaixa.Campos.ValorRecebido / mDividasParaBaixar.Count;
							}
							//Salva a baixa.
							mmBaixa.Campos.salvar(true);
							//Atualiza o status da divida que esta sendo baixada.
							mmDivida.salvar(true);
						}
						
						//Finaliza a Transação
						self.AdmDB.FinalizaTransaction(NoCommit);
						
					}
					catch (Exception ex)
					{
						NBexception mNBEx = new NBexception("Não foi possível Salvar a Baixa", ex);
						mNBEx.Source = "NBdbm.Fachadas.plxCOBR.LancamentoBaixa.Salvar";
						throw (mNBEx);
					}
					
				}
				
				public void Dispose()
				{
					if (this.mBaixa != null)
					{
						this.mBaixa.Dispose();
						this.mBaixa = null;
					}
					if (this.mDividasParaBaixar != null)
					{
						this.mDividasParaBaixar.Dispose();
						this.mDividasParaBaixar = null;
					}
				}
				
			}
			
			namespace primitivas
			{
				
				#region ===[ Tipo de Divida ]===
				public class TipoDivida : allClass
				{
					
					private TipoDividaCampos mCampos; //Interfaces.iEVT.Primitivas.iLocalidades
					
					public TipoDivida() : base("COBR_TipoDivida")
					{
						mCampos = new TipoDividaCampos(this);
					}
					public TipoDivida(tipos.tiposConection TipoConexao) : base("COBR_TipoDivida", TipoConexao)
					{
						mCampos = new TipoDividaCampos(this);
					}
					public Interfaces.iCOBR.Primitivas.iTipoDivida Campos
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
					
					private class TipoDividaCampos : Interfaces.iCOBR.Primitivas.iTipoDivida
					{
						
						
						private allClass mParent;
						
						public TipoDividaCampos(allClass allClass)
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
							filtro = " descricao = \'" + this.Descricao + "\'";
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
								return Parent.var["ID"].value;
							}
						}
						
						public string Key
						{
							get
							{
								return this.ID.ToString() + this.Descricao;
							}
						}
						
						public string Descricao
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
						
						#endregion
						
					}
				}
				#endregion
				
				#region ===[ Tipo de Acionamento ]===
				public class TipoAcionamento : allClass
				{
					
					private TipoAcionamentoCampos mCampos;
					
					public TipoAcionamento() : base("COBR_TipoAcionamento")
					{
						mCampos = new TipoAcionamentoCampos(this);
					}
					public TipoAcionamento(tipos.tiposConection TipoConexao) : base("COBR_TipoAcionamento", TipoConexao)
					{
						mCampos = new TipoAcionamentoCampos(this);
					}
					public Interfaces.iCOBR.Primitivas.iTipoAcionamento Campos
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
					
					private class TipoAcionamentoCampos : Interfaces.iCOBR.Primitivas.iTipoAcionamento
					{
						
						
						private allClass mParent;
						
						public TipoAcionamentoCampos(allClass allClass)
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
							filtro = " descricao = \'" + this.Descricao + "\'";
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
								return Parent.var["ID"].value;
							}
						}
						
						public string Key
						{
							get
							{
								return this.ID.ToString() + this.Descricao;
							}
						}
						
						public string Descricao
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
						
						public int DiasReacionamento
						{
							get
							{
								return Parent.var["DiasReacionamento"].value;
							}
							set
							{
								Parent.var["DiasReacionamento"].Dirty = true;
								Parent.var["DiasReacionamento"].value = value;
							}
						}
						
						public int CredencialExigida
						{
							get
							{
								return Parent.var["CredencialExigida"].value;
							}
							set
							{
								Parent.var["CredencialExigida"].Dirty = true;
								Parent.var["CredencialExigida"].value = value;
							}
						}
						
						#endregion
						
					}
				}
				#endregion
				
				#region ===[ Divida ]===
				public class Divida : allClass
				{
					
					private DividaCampos mCampos;
					
					public Divida() : base("COBR_Divida")
					{
						mCampos = new DividaCampos(this);
					}
					public Divida(tipos.tiposConection TipoConexao) : base("COBR_Divida", TipoConexao)
					{
						mCampos = new DividaCampos(this);
					}
					public Interfaces.iCOBR.Primitivas.iDivida Campos
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
					
					private class DividaCampos : Interfaces.iCOBR.Primitivas.iDivida
					{
						
						
						private allClass mParent;
						private double mValorNominalParcial = 0;
						
						public DividaCampos(allClass allClass)
						{
							this.mParent = allClass;
							this.Baixada = false;
							this.BaixaNoCliente = false;
							this.BaixaParcial = false;
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
							filtro = " id = " + this.ID.ToString() + " And idEntidade = " + this.idEntidade.ToString();
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
								return Parent.var["ID"].value;
							}
						}
						
						public string Key
						{
							get
							{
								return this.XmPathCliente + this.idTipoDivida.ToString("000000") + this.DataVencimento.Ticks.ToString() + this.Contrato + this.NumDoc.ToString("000000");
							}
						}
						
						public int BorderoBaixa
						{
							get
							{
								return Parent.var["BorderoBaixa"].value;
							}
							set
							{
								Parent.var["BorderoBaixa"].Dirty = true;
								Parent.var["BorderoBaixa"].value = value;
							}
						}
						
						public DateTime DataBaixa
						{
							get
							{
								return Parent.var["DataBaixa"].value;
							}
							set
							{
								Parent.var["DataBaixa"].Dirty = true;
								Parent.var["DataBaixa"].value = value;
							}
						}
						
						public DateTime DataVencimento
						{
							get
							{
								return Parent.var["DataVencimento"].value;
							}
							set
							{
								Parent.var["DataVencimento"].Dirty = true;
								Parent.var["DataVencimento"].value = value;
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
						
						public int idTipoDivida
						{
							get
							{
								return Parent.var["idTipoDivida"].value;
							}
							set
							{
								Parent.var["idTipoDivida"].Dirty = true;
								Parent.var["idTipoDivida"].value = value;
							}
						}
						
						public int NumDoc
						{
							get
							{
								return Parent.var["NumDoc"].value;
							}
							set
							{
								Parent.var["NumDoc"].Dirty = true;
								Parent.var["NumDoc"].value = value;
							}
						}
						
						public double ValorNominal
						{
							get
							{
								string tmp;
								tmp = Parent.var["ValorNominal"].value;
								if (mValorNominalParcial == 0)
								{
									mValorNominalParcial = Convert.ToDouble(tmp.Replace(".", ","));
								}
								return Convert.ToDouble(tmp.Replace(".", ","));
							}
							set
							{
								Parent.var["ValorNominal"].Dirty = true;
								Parent.var["ValorNominal"].value = value;
								if (mValorNominalParcial == 0)
								{
									mValorNominalParcial = value;
								}
							}
						}
						
						public double ValorNominalParcial
						{
							get
							{
								return this.mValorNominalParcial;
							}
							set
							{
								this.mValorNominalParcial = value;
							}
						}
						
						public string Contrato
						{
							get
							{
								return Parent.var["Contrato"].value;
							}
							set
							{
								Parent.var["Contrato"].Dirty = true;
								Parent.var["Contrato"].value = value;
							}
						}
						
						public int NumRecibo
						{
							get
							{
								return Parent.var["NumRecibo"].value;
							}
							set
							{
								Parent.var["NumRecibo"].Dirty = true;
								Parent.var["NumRecibo"].value = value;
							}
						}
						
						public string XmPathCliente
						{
							get
							{
								return Parent.var["XmPathCliente"].value;
							}
							set
							{
								Parent.var["XmPathCliente"].Dirty = true;
								Parent.var["XmPathCliente"].value = value;
							}
						}
						
						public int idCobrador
						{
							get
							{
								return Parent.var["idCobrador"].value;
							}
							set
							{
								Parent.var["idCobrador"].Dirty = true;
								Parent.var["idCobrador"].value = value;
							}
						}
						
						public int idUsuarioBaixa
						{
							get
							{
								return Parent.var["IdUsuarioBaixa"].value;
							}
							set
							{
								Parent.var["IdUsuarioBaixa"].Dirty = true;
								Parent.var["IdUsuarioBaixa"].value = value;
							}
						}
						
						public double PerCobrador
						{
							get
							{
								string tmp;
								tmp = Parent.var["PerCobrador"].value;
								return Convert.ToDouble(tmp.Replace(".", ","));
							}
							set
							{
								Parent.var["PerCobrador"].Dirty = true;
								Parent.var["PerCobrador"].value = value;
							}
						}
						
						public bool BaixaNoCliente
						{
							get
							{
								return Parent.var["BaixaNoCliente"].value;
							}
							set
							{
								Parent.var["BaixaNoCliente"].Dirty = true;
								Parent.var["BaixaNoCliente"].value = value;
							}
						}
						
						public bool Baixada
						{
							get
							{
								return Parent.var["Baixada"].value;
							}
							set
							{
								Parent.var["Baixada"].Dirty = true;
								Parent.var["Baixada"].value = value;
							}
						}
						
						public bool BaixaParcial
						{
							get
							{
								return Parent.var["BaixaParcial"].value;
							}
							set
							{
								Parent.var["BaixaParcial"].Dirty = true;
								Parent.var["BaixaParcial"].value = value;
							}
						}
						
						#endregion
						
					}
				}
				#endregion
				
				#region ===[ Acionamentos ]===
				public class Acionamentos : allClass
				{
					
					private AcionamentosCampos mCampos;
					
					public Acionamentos() : base("COBR_Acionamentos")
					{
						mCampos = new AcionamentosCampos(this);
					}
					public Acionamentos(tipos.tiposConection TipoConexao) : base("COBR_Acionamentos", TipoConexao)
					{
						mCampos = new AcionamentosCampos(this);
					}
					public Interfaces.iCOBR.Primitivas.iAcionamentos Campos
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
					
					private class AcionamentosCampos : Interfaces.iCOBR.Primitivas.iAcionamentos
					{
						
						
						private allClass mParent;
						
						public AcionamentosCampos(allClass allClass)
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
							filtro = " idUsuario = " + this.idUsuario.ToString() + " And DataAcionamento = CONVERT(DateTime,\'" + this.DataAcionamento.ToString("MM-dd-yy HH:mm:ss") + "\',1)";
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
								return Parent.var["ID"].value;
							}
						}
						
						public string Key
						{
							get
							{
								return this.DataAcionamento.Ticks.ToString();
							}
						}
						
						public DateTime DataAcionamento
						{
							get
							{
								return Parent.var["DataAcionamento"].value;
							}
							set
							{
								Parent.var["DataAcionamento"].Dirty = true;
								Parent.var["DataAcionamento"].value = value;
							}
						}
						
						public DateTime DataPromessa
						{
							get
							{
								return Parent.var["DataPromessa"].value;
							}
							set
							{
								Parent.var["DataPromessa"].Dirty = true;
								Parent.var["DataPromessa"].value = value;
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
						
						public int idTipoAcionamento
						{
							get
							{
								return Parent.var["idTipoAcionamento"].value;
							}
							set
							{
								Parent.var["idTipoAcionamento"].Dirty = true;
								Parent.var["idTipoAcionamento"].value = value;
							}
						}
						
						public int idUsuario
						{
							get
							{
								return Parent.var["idUsuario"].value;
							}
							set
							{
								Parent.var["idUsuario"].Dirty = true;
								Parent.var["idUsuario"].value = value;
							}
						}
						
						public string TextoRespeito
						{
							get
							{
								return Parent.var["TextoRespeito"].value;
							}
							set
							{
								Parent.var["TextoRespeito"].Dirty = true;
								Parent.var["TextoRespeito"].value = value;
							}
						}
						
						#endregion
						
					}
				}
				#endregion
				
				#region ===[ Tarifas ]===
				public class Tarifas : allClass
				{
					
					private TarifasCampos mCampos;
					
					public Tarifas() : base("COBR_Tarifas")
					{
						mCampos = new TarifasCampos(this);
					}
					public Tarifas(tipos.tiposConection TipoConexao) : base("COBR_Tarifas", TipoConexao)
					{
						mCampos = new TarifasCampos(this);
					}
					public Interfaces.iCOBR.Primitivas.iTarifas Campos
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
					
					private class TarifasCampos : Interfaces.iCOBR.Primitivas.iTarifas
					{
						
						
						
						private allClass mParent;
						
						public TarifasCampos(allClass allClass)
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
							filtro = " idEntidade = " + this.idEntidade;
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
								return Parent.var["ID"].value;
							}
						}
						
						public string Key
						{
							get
							{
								return this.ID.ToString();
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
						
						public double Juros
						{
							get
							{
								return Parent.var["Juros"].value;
							}
							set
							{
								Parent.var["Juros"].Dirty = true;
								Parent.var["Juros"].value = value;
							}
						}
						
						public double Multa
						{
							get
							{
								return Parent.var["Multa"].value;
							}
							set
							{
								Parent.var["Multa"].Dirty = true;
								Parent.var["Multa"].value = value;
							}
						}
						#endregion
					}
				}
				#endregion
				
				#region ===[ Baixas ]===
				public class Baixas : allClass
				{
					
					private BaixasCampos mCampos;
					
					public Baixas() : base("COBR_Baixas")
					{
						mCampos = new BaixasCampos(this);
					}
					public Baixas(tipos.tiposConection TipoConexao) : base("COBR_Baixas", TipoConexao)
					{
						mCampos = new BaixasCampos(this);
					}
					public Interfaces.iCOBR.Primitivas.iBaixas Campos
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
					
					private class BaixasCampos : Interfaces.iCOBR.Primitivas.iBaixas
					{
						
						
						private allClass mParent;
						
						public BaixasCampos(allClass allClass)
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
							filtro = "idDivida = " + this.idDivida + " And DataBaixa = \'" + this.DataBaixa.ToString(self.Settings.sintaxeData) + "\'" + " And NumBordero = " + this.NumBordero + " and ValorBaixa = " + this.ValorBaixa.ToString().Replace(",", ".");
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
								return Parent.var["Id"].value;
							}
						}
						
						public string Key
						{
							get
							{
								return this.idEntidade + this.DataBaixa.ToShortDateString();
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
						
						public int idDivida
						{
							get
							{
								return Parent.var["idDivida"].value;
							}
							set
							{
								Parent.var["idDivida"].Dirty = true;
								Parent.var["idDivida"].value = value;
							}
						}
						public bool BaixadoCliente
						{
							get
							{
								return Parent.var["BaixadoCliente"].value;
							}
							set
							{
								Parent.var["BaixadoCliente"].Dirty = true;
								Parent.var["BaixadoCliente"].value = value;
							}
						}
						
						public int NumBordero
						{
							get
							{
								return Parent.var["NumBordero"].value;
							}
							set
							{
								Parent.var["NumBordero"].Dirty = true;
								Parent.var["NumBordero"].value = value;
							}
						}
						public double ValorNominal
						{
							get
							{
								string tmp;
								tmp = Parent.var["ValorNominal"].value;
								return Convert.ToDouble(tmp.Replace(".", ","));
							}
							set
							{
								Parent.var["ValorNominal"].Dirty = true;
								Parent.var["ValorNominal"].value = value;
							}
						}
						
						public double ValorBaixa
						{
							get
							{
								string tmp;
								tmp = Parent.var["ValorBaixa"].value;
								return Convert.ToDouble(tmp.Replace(".", ","));
							}
							set
							{
								Parent.var["ValorBaixa"].Dirty = true;
								Parent.var["ValorBaixa"].value = value;
							}
						}
						
						public double ValorRecebido
						{
							get
							{
								string tmp;
								tmp = Parent.var["ValorRecebido"].value;
								return Convert.ToDouble(tmp.Replace(".", ","));
							}
							set
							{
								Parent.var["ValorRecebido"].Dirty = true;
								Parent.var["ValorRecebido"].value = value;
							}
						}
						
						public DateTime DataBaixa
						{
							get
							{
								return Parent.var["DataBaixa"].value;
							}
							set
							{
								Parent.var["DataBaixa"].Dirty = true;
								Parent.var["DataBaixa"].value = value;
							}
						}
						#endregion
						
					}
				}
				#endregion
				
			}
		}
	}
	
}
