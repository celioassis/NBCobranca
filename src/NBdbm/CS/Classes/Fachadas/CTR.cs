using System.Data;
using System.Collections;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System;


namespace NBdbm
{
	namespace Fachadas
	{
		
		namespace CTR
		{
			
			public class CadastroEntidade : Interfaces.iCTR.iCadastroEntidade
			{
				
				
				protected string mSource;
				protected Fachadas.NbCollection mColTelefone = new Fachadas.NbCollection();
				protected Fachadas.NbCollection mColEndereco = new Fachadas.NbCollection();
				protected Fachadas.NbCollection mColEmail = new Fachadas.NbCollection();
				protected Fachadas.NbCollection mColUrl = new Fachadas.NbCollection();
				protected Fachadas.CTR.primitivas.LinkEntidadeNo mLinkNo;
				protected Fachadas.CTR.primitivas.Entidade mEntidade;
				protected Fachadas.CTR.primitivas.Endereco mEndereco;
				protected Fachadas.CTR.primitivas.Telefone mTelefone;
				protected Fachadas.CTR.primitivas.eMail mEMail;
				protected Fachadas.CTR.primitivas.Url mUrl;
				protected tipos.tiposConection mTipoConexao;
				private string xmPath_EntNo;
				
				public CadastroEntidade()
				{
					mEntidade = new Fachadas.CTR.primitivas.Entidade();
					mEndereco = new Fachadas.CTR.primitivas.Endereco();
					mTelefone = new Fachadas.CTR.primitivas.Telefone();
					mEMail = new Fachadas.CTR.primitivas.eMail();
					mUrl = new Fachadas.CTR.primitivas.Url();
					mLinkNo = new Fachadas.CTR.primitivas.LinkEntidadeNo();
				}
				public CadastroEntidade(tipos.tiposConection TipoConexao)
				{
					mTipoConexao = TipoConexao;
					mEntidade = new Fachadas.CTR.primitivas.Entidade(TipoConexao);
					mEndereco = new Fachadas.CTR.primitivas.Endereco(TipoConexao);
					mTelefone = new Fachadas.CTR.primitivas.Telefone(TipoConexao);
					mEMail = new Fachadas.CTR.primitivas.eMail(TipoConexao);
					mUrl = new Fachadas.CTR.primitivas.Url(TipoConexao);
					mLinkNo = new Fachadas.CTR.primitivas.LinkEntidadeNo(TipoConexao);
				}
				public virtual void Dispose()
				{
					if (mColEndereco != null)
					{
						mColEndereco.Dispose();
					}
					mColEndereco = null;
					if (mColTelefone != null)
					{
						mColTelefone.Dispose();
					}
					mColTelefone = null;
					if (mColEmail != null)
					{
						mColEmail.Dispose();
					}
					mColEmail = null;
					if (mColUrl != null)
					{
						mColUrl.Dispose();
					}
					mColUrl = null;
					if (mEntidade != null)
					{
						mEntidade.Dispose();
					}
					mEntidade = null;
					if (mEndereco != null)
					{
						mEndereco.Dispose();
					}
					mEndereco = null;
					if (mTelefone != null)
					{
						mTelefone.Dispose();
					}
					mTelefone = null;
					if (mEMail != null)
					{
						mEMail.Dispose();
					}
					mEMail = null;
					if (mUrl != null)
					{
						mUrl.Dispose();
					}
					mUrl = null;
					if (mLinkNo != null)
					{
						mLinkNo.Dispose();
					}
					mLinkNo = null;
					
					mTipoConexao = null;
					xmPath_EntNo = null;
					
				}
				
				public NbCollection colecaoTelefones
				{
					get
					{
						return mColTelefone;
					}
				}
				public NbCollection colecaoEnderecos
				{
					get
					{
						return mColEndereco;
					}
				}
				public NbCollection colecaoEmail
				{
					get
					{
						return mColEmail;
					}
				}
				public NbCollection colecaoUrl
				{
					get
					{
						return mColUrl;
					}
				}
				
				protected void LimparColecoes()
				{
					mColEmail.Clear();
					mColEndereco.Clear();
					mColTelefone.Clear();
					mColUrl.Clear();
				}
				
				public bool EstaEmMuitosNos(bool ExcluirClasseAtual)
				{
					Primitivas.No obNo;
					Fachadas.CTR.primitivas.LinkEntidadeNo obLinkNo;
					
					//Localizando o Nó pelo xmPath
					//Dim idNo As Integer
					//Verifica se foi usado outro tipo de conexao que não a padrao
					if (mTipoConexao != null)
					{
						obNo = new primitivas.No(mTipoConexao);
						obLinkNo = new primitivas.LinkEntidadeNo(mTipoConexao);
					}
					else
					{
						obNo = new primitivas.No();
						obLinkNo = new primitivas.LinkEntidadeNo();
					}
					obNo.filterWhere = "xmPath like \'*" + xmPath_EntNo + "\'";
					obNo.getFields();
					
					//Verificando se a entidade esta em mais de um Nó.
					obLinkNo.filterWhere = "idEntidade = " + this.Entidade.ID.ToString();
					if (obLinkNo.DataSource().Count > 1)
					{
						if (ExcluirClasseAtual)
						{
							obLinkNo.Clear_filters();
							obLinkNo.Clear_vars();
							obLinkNo.filterWhere = "idEntidade = " + this.Entidade.ID.ToString() + " and idNo = " + obNo.Campos.idNo_key.ToString();
							obLinkNo.getFields();
							obLinkNo.excluir(false);
						}
						return true;
					}
					else
					{
						return false;
					}
					obNo.Dispose();
					obNo = null;
					obLinkNo.Dispose();
					obLinkNo = null;
				}
				
				public virtual void getFieldsFromEntidade(double idEntidade)
				{
					this.mEntidade.Clear_vars();
					this.LimparColecoes();
					//Busca os dados da Entidade
					this.mEntidade.filterWhere = "IdEntidade = " + idEntidade;
					this.mEntidade.getFields();
					
					//Busca os dados de Endereços
					this.mEndereco.filterWhere = "idEntidade = " + idEntidade;
					foreach (Fields mFields in this.mEndereco.CriaColecaoFields().Values)
					{
						NBdbm.Fachadas.CTR.primitivas.Endereco mmEndereco;
						//Verifica se foi usado outro tipo de conexao que não a padrao
						if (mTipoConexao != null)
						{
							mmEndereco = new NBdbm.Fachadas.CTR.primitivas.Endereco(mTipoConexao);
						}
						else
						{
							mmEndereco = new NBdbm.Fachadas.CTR.primitivas.Endereco();
						}
						mmEndereco.var = mFields;
						this.mColEndereco.Add(mmEndereco.Campos.Logradouro_key, ((object) mmEndereco.Campos));
					}
					
					//If Me.mEndereco.DataSource.Count > 0 Then
					//  For Each DR In Me.mEndereco.DataSource.Table.Rows
					//    Dim endereco_ As NBdbm.Fachadas.CTR.primitivas.Endereco
					//    'Verifica se foi usado outro tipo de conexao que não a padrao
					//    If Not IsNothing(mTipoConexao) Then
					//      endereco_ = New NBdbm.Fachadas.CTR.primitivas.Endereco(mTipoConexao)
					//    Else
					//      endereco_ = New NBdbm.Fachadas.CTR.primitivas.Endereco
					//    End If
					//    endereco_.filterWhere = "idEntidade = " & idEntidade & " and IdEndereco = " & DR.Item("IdEndereco").ToString
					//    endereco_.filterOrderBy = "principal"
					//    endereco_.getFields()
					//    Me.Endereco = endereco_.Campos
					//    Me.colecaoEnderecos.Add(endereco_.Campos.Logradouro_key, endereco_.Campos)
					//  Next
					//Else
					//  mEndereco.Clear_vars()
					//End If
					
					//Busca os dados de emails
					this.mEMail.filterWhere = "idEntidade = " + idEntidade;
					foreach (Fields mFields in this.mEMail.CriaColecaoFields().Values)
					{
						NBdbm.Fachadas.CTR.primitivas.eMail mmItemColecao;
						//Verifica se foi usado outro tipo de conexao que não a padrao
						if (mTipoConexao != null)
						{
							mmItemColecao = new NBdbm.Fachadas.CTR.primitivas.eMail(mTipoConexao);
						}
						else
						{
							mmItemColecao = new NBdbm.Fachadas.CTR.primitivas.eMail();
						}
						mmItemColecao.var = mFields;
						this.mColEmail.Add(mmItemColecao.Campos.eMail_key, ((object) mmItemColecao.Campos));
					}
					
					//Busca os dados de Sites
					this.mUrl.filterWhere = "idEntidade = " + idEntidade;
					foreach (Fields mFields in this.mUrl.CriaColecaoFields().Values)
					{
						NBdbm.Fachadas.CTR.primitivas.Url mmItemColecao;
						//Verifica se foi usado outro tipo de conexao que não a padrao
						if (mTipoConexao != null)
						{
							mmItemColecao = new NBdbm.Fachadas.CTR.primitivas.Url(mTipoConexao);
						}
						else
						{
							mmItemColecao = new NBdbm.Fachadas.CTR.primitivas.Url();
						}
						mmItemColecao.var = mFields;
						this.mColUrl.Add(mmItemColecao.Campos.Url_key, ((object) mmItemColecao.Campos));
					}
					
					//Busca os dados de telefones
					this.mTelefone.filterWhere = "idEntidade = " + idEntidade;
					foreach (Fields mFields in this.mTelefone.CriaColecaoFields().Values)
					{
						NBdbm.Fachadas.CTR.primitivas.Telefone mmItemColecao;
						//Verifica se foi usado outro tipo de conexao que não a padrao
						if (mTipoConexao != null)
						{
							mmItemColecao = new NBdbm.Fachadas.CTR.primitivas.Telefone(mTipoConexao);
						}
						else
						{
							mmItemColecao = new NBdbm.Fachadas.CTR.primitivas.Telefone();
						}
						mmItemColecao.var = mFields;
						this.mColTelefone.Add(mmItemColecao.Campos.Fone_key, ((object) mmItemColecao.Campos));
					}
					
				}
				
				public virtual void Salvar(bool noCommit)
				{
					//implementar: Salvar primeiro a entidade, depois os telefones, endereços e emails
					double idE;
					double idEnd;
					//Dim strParser As String
					long idNo;
					Primitivas.No ob;
					try
					{
						
						//Localizando o Nó pelo xmPath
						//Verifica se foi usado outro tipo de conexao que não a padrao
						if (this.xmPath_EntNo != null)
						{
							if (mTipoConexao != null)
							{
								ob = new primitivas.No(mTipoConexao);
							}
							else
							{
								ob = new primitivas.No();
							}
							
							try
							{
								ob.filterWhere = "xmPath like \'*" + xmPath_EntNo + "\'";
								ob.getFields();
								idNo = ob.Campos.idNo_key;
							}
							catch (Exception)
							{
								ob.filterWhere = "xmPath like \'*\'";
								ob.getFields();
								idNo = ob.Campos.idNo_key;
							}
						}
						//Salvando a Entidade
						this.Entidade.salvar(true);
						idE = this.Entidade.ID;
						
						if (this.xmPath_EntNo != null)
						{
							//Salvando o Link Entidade Nó
							this.LinkEntidadeNo.idEntidade = idE;
							this.LinkEntidadeNo.idNo = idNo;
							this.LinkEntidadeNo.salvar(true);
						}
						
						//Salvando Endereços
						foreach (Interfaces.iCTR.Primitivas.iEndereco endereco in mColEndereco.Values)
						{
							endereco.idEntidade_key = idE;
							endereco.salvar(true);
							if (endereco.Principal == true)
							{
								idEnd = endereco.ID;
							}
						}
						
						//Salvando os Telefones
						foreach (Interfaces.iCTR.Primitivas.iTelefone Fone in mColTelefone.Values)
						{
							Fone.idEntidade_key = idE;
							if (idEnd > 0)
							{
								Fone.idEndereco = idEnd;
							}
							Fone.salvar(true);
						}
						
						//Salvando os Emails
						foreach (Interfaces.iCTR.Primitivas.iEmail email in mColEmail.Values)
						{
							email.idEntidade_key = idE;
							email.salvar(true);
						}
						
						//Salvando as URLs
						foreach (Interfaces.iCTR.Primitivas.iUrl url in mColUrl.Values)
						{
							url.idEntidade_key = idE;
							url.salvar(true);
						}
						
						//Finaliza a Transação
						self.AdmDB.FinalizaTransaction(noCommit);
						
					}
					catch (Exception ex)
					{
						NBexception mNBEx = new NBexception("Não foi possível Salvar o Cadastro da Entidade - " + this.Entidade.NomeRazaoSocial_key, ex);
						mNBEx.Source = "NBdbm.Fachadas.CTR.CadastroEntidade.Salvar";
						throw (mNBEx);
					}
				}
				
				public virtual void Salvar()
				{
					this.Salvar(false);
				}
				
				public virtual void Excluir(bool NoCommit)
				{
					mEntidade.excluir(NoCommit);
				}
				
				public virtual void Excluir()
				{
					this.Excluir(false);
				}
				
				public Interfaces.iCTR.Primitivas.iEntidade Entidade
				{
					get
					{
						return mEntidade.campos;
					}
					set
					{
						mEntidade.campos = value;
					}
				}
				
				public virtual string xmPath_LinkEntNo
				{
					get
					{
						return xmPath_EntNo;
					}
					set
					{
						xmPath_EntNo = value;
					}
				}
				
				private Interfaces.iCTR.Primitivas.iLinkEntidadeNo LinkEntidadeNo
				{
					get
					{
						return mLinkNo.Campos;
					}
					set
					{
						mLinkNo.Campos = value;
					}
				}
				
				public Interfaces.iCTR.Primitivas.iEndereco Endereco
				{
					get
					{
						return mEndereco.Campos;
					}
					set
					{
						mEndereco.Campos = value;
					}
				}
				
				public void NovoEndereco()
				{
					this.mEndereco = new Fachadas.CTR.primitivas.Endereco(this.mTipoConexao);
				}
				
				public Interfaces.iCTR.Primitivas.iEmail eMail
				{
					get
					{
						return mEMail.Campos;
					}
					set
					{
						mEMail.Campos = value;
					}
				}
				
				public void NovoEmail()
				{
					this.mEMail = new Fachadas.CTR.primitivas.eMail(this.mTipoConexao);
				}
				
				public Interfaces.iCTR.Primitivas.iUrl Url
				{
					get
					{
						return mUrl.Campos;
					}
					set
					{
						mUrl.Campos = value;
					}
				}
				
				public void NovaUrl()
				{
					this.mUrl = new Fachadas.CTR.primitivas.Url(this.mTipoConexao);
				}
				
				public Interfaces.iCTR.Primitivas.iTelefone Telefone
				{
					get
					{
						return mTelefone.Campos;
					}
					set
					{
						mTelefone.Campos = value;
					}
				}
				
				public void NovoTelefone()
				{
					this.mTelefone = new Fachadas.CTR.primitivas.Telefone(this.mTipoConexao);
				}
				
				
			}
			public class CadastroUsuario : Interfaces.iCTR.iCadastroUsuario
			{
				
				
				
				protected string mSource = "Cadastro de Usuários";
				protected Fachadas.CTR.primitivas.Entidade mEntidade;
				protected Fachadas.CTR.primitivas.Endereco mEndereco;
				protected Fachadas.CTR.primitivas.Telefone mTelefone;
				protected Fachadas.CTR.primitivas.Telefone mCelular;
				protected Fachadas.CTR.primitivas.eMail mEMail;
				protected Fachadas.CTR.primitivas.Usuario mUsuario;
				protected Fachadas.CTR.primitivas.UsuarioConfig mConfig;
				protected Fachadas.CTR.primitivas.LinkEntidadeNo mLinkNo;
				protected tipos.tiposConection mTipoConexao;
				protected string mXmPath_EntNo;
				
				public CadastroUsuario()
				{
					mEntidade = new Fachadas.CTR.primitivas.Entidade();
					mEndereco = new Fachadas.CTR.primitivas.Endereco();
					mTelefone = new Fachadas.CTR.primitivas.Telefone();
					mCelular = new Fachadas.CTR.primitivas.Telefone();
					mEMail = new Fachadas.CTR.primitivas.eMail();
					mUsuario = new Fachadas.CTR.primitivas.Usuario();
					mConfig = new Fachadas.CTR.primitivas.UsuarioConfig();
					mLinkNo = new Fachadas.CTR.primitivas.LinkEntidadeNo();
				}
				public CadastroUsuario(tipos.tiposConection TipoConexao)
				{
					mTipoConexao = TipoConexao;
					mEntidade = new Fachadas.CTR.primitivas.Entidade(TipoConexao);
					mEndereco = new Fachadas.CTR.primitivas.Endereco(TipoConexao);
					mTelefone = new Fachadas.CTR.primitivas.Telefone(TipoConexao);
					mCelular = new Fachadas.CTR.primitivas.Telefone(TipoConexao);
					mEMail = new Fachadas.CTR.primitivas.eMail(TipoConexao);
					mUsuario = new Fachadas.CTR.primitivas.Usuario(TipoConexao);
					mConfig = new Fachadas.CTR.primitivas.UsuarioConfig(TipoConexao);
					mLinkNo = new Fachadas.CTR.primitivas.LinkEntidadeNo(TipoConexao);
				}
				public void getFieldsFromEntidade(double idEntidade)
				{
					
					this.mEntidade.filterWhere = "IdEntidade = " + idEntidade;
					this.mEntidade.getFields();
					
					this.mEndereco.filterWhere = "idEntidade = " + idEntidade;
					this.mEndereco.getFields();
					
					this.mTelefone.filterWhere = "Descricao = \'Res\' and idEntidade =" + idEntidade;
					this.mTelefone.getFields();
					
					this.mCelular.filterWhere = "Descricao = \'Cel\' and idEntidade = " + idEntidade;
					this.mCelular.getFields();
					
					this.mEMail.filterWhere = "idEntidade = " + idEntidade;
					this.mEMail.getFields();
					
					this.mUsuario.filterWhere = "idEntidade = " + idEntidade;
					this.mUsuario.getFields();
					
					this.mConfig.filterWhere = "idUsuario = " + this.Usuario.ID;
					this.mConfig.getFields();
					
				}
				public void getFieldsFromUsuario(double idUsuario)
				{
					
					this.mUsuario.filterWhere = "idUsuario = " + idUsuario;
					this.mUsuario.getFields();
					
					this.mConfig.filterWhere = "idUsuario = " + idUsuario;
					this.mConfig.getFields();
					
					this.getFieldsFromEntidade(this.Usuario.idEntidade);
					
				}
				public void getFieldsFromUsuario(string Login)
				{
					
					this.mUsuario.filterWhere = "Login = \'" + Login + "\'";
					this.mUsuario.getFields();
					
					this.mConfig.filterWhere = "idUsuario = " + mUsuario.Campos.ID;
					this.mConfig.getFields();
					
					this.getFieldsFromEntidade(this.Usuario.idEntidade);
					
				}
				public void Salvar(bool NoCommit)
				{
					Primitivas.No ob;
					
					try
					{
						//Verifica se foi usado outro tipo de conexao que não a padrao
						if (mTipoConexao != null)
						{
							ob = new primitivas.No(mTipoConexao);
						}
						else
						{
							ob = new primitivas.No();
						}
						
						//Localizando o Nó pelo xmPath
						try
						{
							ob.filterWhere = "xmPath like \'*" + XmPath_EntNo + "\'";
							ob.getFields();
						}
						catch (Exception)
						{
							ob.filterWhere = "xmPath like \'*\'";
							ob.getFields();
						}
						
						//Salvando a Entidade
						this.mEntidade.campos.salvar(true);
						
						//Salvando o Link Entidade Nó
						this.mLinkNo.Campos.idEntidade = this.Entidade.ID;
						this.mLinkNo.Campos.idNo = ob.Campos.idNo_key;
						this.mLinkNo.Campos.salvar(true);
						
						
						//Salvar o Endereco
						this.Endereco.idEntidade_key = this.Entidade.ID;
						this.Endereco.salvar(true);
						
						//Salvar o Telefone
						this.Telefone.idEndereco = this.Endereco.ID;
						this.Telefone.idEntidade_key = this.Entidade.ID;
						this.Telefone.salvar(true);
						
						//Salvar Celular
						this.Celular.idEndereco = this.Endereco.ID;
						this.Celular.idEntidade_key = this.Entidade.ID;
						this.Celular.salvar(true);
						
						//Salvar Email
						this.Email.idEntidade_key = this.Entidade.ID;
						this.Email.salvar(true);
						
						//Salvando o Usuario
						this.Usuario.idEntidade = this.Entidade.ID;
						this.Usuario.salvar(true);
						
						//Salvando as Configurações
						this.UsuarioConfig.idUsuario_key = this.Usuario.ID;
						UsuarioConfig.salvar(true);
						
						//Finaliza a Transação
						self.AdmDB.FinalizaTransaction(NoCommit);
						
					}
					catch (Exception ex)
					{
						NBexception mNBEx = new NBexception("Não foi possível Salvar o Cadastro do Usuário - " + this.Entidade.NomeRazaoSocial_key, ex);
						mNBEx.Source = "NBdbm.Fachadas.CTR.CadastroUsuario.Salvar";
						throw (mNBEx);
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
						this.mEntidade.excluir(NoCommit);
					}
					catch (NBexception ex)
					{
						ex.Source = this.mSource;
						throw (ex);
					}
				}
				public void Excluir()
				{
					this.Excluir(false);
				}
				public void ExcluirUsuarioAtual(bool NoCommit)
				{
					try
					{
						mUsuario.excluir(NoCommit);
					}
					catch (NBexception ex)
					{
						ex.Source = this.mSource;
						throw (ex);
					}
				}
				public Interfaces.iCTR.Primitivas.iEntidade Entidade
				{
					get
					{
						return this.mEntidade.campos;
					}
					set
					{
						this.mEntidade.campos = value;
					}
				}
				public Interfaces.iCTR.Primitivas.iEndereco Endereco
				{
					get
					{
						return this.mEndereco.Campos;
					}
					set
					{
						this.mEndereco.Campos = value;
					}
				}
				public Interfaces.iCTR.Primitivas.iTelefone Telefone
				{
					get
					{
						return this.mTelefone.Campos;
					}
					set
					{
						this.mTelefone.Campos = value;
					}
				}
				public Interfaces.iCTR.Primitivas.iTelefone Celular
				{
					get
					{
						return this.mCelular.Campos;
					}
					set
					{
						this.mCelular.Campos = value;
					}
				}
				public Interfaces.iCTR.Primitivas.iEmail Email
				{
					get
					{
						return this.mEMail.Campos;
					}
					set
					{
						this.mEMail.Campos = value;
					}
				}
				public Interfaces.iCTR.Primitivas.iUsuario Usuario
				{
					get
					{
						return mUsuario.Campos;
					}
					set
					{
						mUsuario.Campos = value;
					}
				}
				public Interfaces.iCTR.Primitivas.iUsuarioConfig UsuarioConfig
				{
					get
					{
						return mConfig.Campos;
					}
					set
					{
						mConfig.Campos = value;
					}
				}
				public string XmPath_EntNo
				{
					get
					{
						return mXmPath_EntNo;
					}
					set
					{
						mXmPath_EntNo = value;
					}
				}
				public virtual void Dispose()
				{
					if (mEntidade != null)
					{
						mEntidade.Dispose();
					}
					if (mEndereco != null)
					{
						mEndereco.Dispose();
					}
					if (mTelefone != null)
					{
						mTelefone.Dispose();
					}
					if (mCelular != null)
					{
						mCelular.Dispose();
					}
					if (mEMail != null)
					{
						mEMail.Dispose();
					}
					if (mUsuario != null)
					{
						mUsuario.Dispose();
					}
					if (mConfig != null)
					{
						mConfig.Dispose();
					}
					if (mLinkNo != null)
					{
						mLinkNo.Dispose();
					}
					mEntidade = null;
					mEndereco = null;
					mTelefone = null;
					mCelular = null;
					mEMail = null;
					mUsuario = null;
					mConfig = null;
					mLinkNo = null;
				}
			}
			
			namespace primitivas
			{
				public class Entidade : allClass
				{
					
					private EntidadeCampos mCampos;
					
					public Entidade() : base("CTRL_Entidades")
					{
						base.var.Add("CPFCNPJ", this.CPFCNPJ, System.Type.GetType("System.String"), false);
						mCampos = new EntidadeCampos(this);
					}
					public Entidade(tipos.tiposConection TipoConexao) : base("CTRL_Entidades", TipoConexao)
					{
						base.var.Add("CPFCNPJ", this.CPFCNPJ, System.Type.GetType("System.String"), false);
						mCampos = new EntidadeCampos(this);
					}
					public Interfaces.iCTR.Primitivas.iEntidade campos
					{
						get
						{
							return mCampos;
						}
						set
						{
							mCampos = value;
							//Atenção: Quando vc chama uma property ou uma function
							//E não atribui o seu retorno, o value vem nothing mesmo,
							//isso é normal, não invalida a função que apenas se comporta
							//momentaneamente como uma 'sub'
							if (value != null)
							{
								base.toObject = mCampos.Parent;
							}
						}
					}
					public void getFieldsFromNomeEntidade(string NomePrimary)
					{
						this.Clear_filters();
						this.filterWhere = "NomePrimary=\'" + NomePrimary + "\'";
						this.getFields();
					}
					public void getFieldsFromCnpjCpf(string CNPJ_CPF)
					{
						int mIdCPFCNPJ;
						//Dim mPessoaFisica As Boolean
						try
						{
							mIdCPFCNPJ = this.RetornaIdCPFCNPJ(NBFuncoes.validaCPFCNPJ(CNPJ_CPF, true));
							if (mIdCPFCNPJ > 0)
							{
								this.Clear_vars();
								this.Clear_filters();
								this.filterWhere = "idCPFCNPJ=" + mIdCPFCNPJ;
								this.getFields();
							}
						}
						catch (Exception ex)
						{
							NBexception NBEx = new NBexception("Não foi Possível Buscar a Entidade Pelo CNPJ_CPF", ex);
							NBEx.Source = "Entidade.getFieldsFromCnpjCpf";
							throw (NBEx);
						}
						
					}
					public override void Dispose()
					{
						if (mCampos != null)
						{
							mCampos.Dispose();
						}
						mCampos = null;
						base.Dispose();
					}
					private class EntidadeCampos : Interfaces.iCTR.Primitivas.iEntidade
					{
						
						
						private allClass mParent;
						
						public EntidadeCampos(allClass allClass)
						{
							this.mParent = allClass;
						}
						internal allClass Parent
						{
							get
							{
								return mParent;
							}
							set
							{
								mParent = value;
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
							//Implentar:
							//Aqui será feita a verificação do me.id = null para inserir ou caso
							//contrário será realizado uma edição
							//Dim retorno As tipos.Retorno
							Parent.var["dtAlteracao"].Dirty = true;
							Parent.var["dtAlteracao"].value = DateTime.Now;
							Parent.filterWhere = "idEntidade = " + this.ID + " and idCPFCNPJ = " + Parent.var["idCPFCNPJ"].value + " and NomePrimary = \'" + this.NomeRazaoSocial_Key + "\' ";
							Parent.editar(noCommit);
							if (this.Parent.Inclusao)
							{
								Parent.var["dtCriacao"].Dirty = true;
								Parent.var["dtCriacao"].value = DateTime.Now;
								Parent.inserir(noCommit);
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
								return Parent.var["IdEntidade"].value;
							}
						}
						
						public string Key
						{
							get
							{
								return this.CPFCNPJ_Key.ToString() + this.NomeRazaoSocial_Key.ToString();
							}
						}
						
						public string ApelidoNomeFantasia
						{
							get
							{
								return Parent.var["NomeSecundary"].value;
							}
							set
							{
								Parent.var["NomeSecundary"].value = value;
								Parent.var["NomeSecundary"].Dirty = true;
							}
						}
						
						public string CPFCNPJ_key
						{
							get
							{
								return this.CPFCNPJ_Key;
							}
							set
							{
								this.CPFCNPJ_Key = value;
							}
						}
						
						public string CPFCNPJ_Key
						{
							get
							{
								return Parent.CPFCNPJ;
							}
							set
							{
								Parent.CPFCNPJ = value;
								Parent.var["idCPFCNPJ"].Dirty = true;
							}
						}
						
						public DateTime dtAlteracao
						{
							get
							{
								//parent.var("dtAlteracao").Value = CDate(Now).ToString(self.Settings.sintaxeData)
								return System.Convert.ToDateTime(Parent.var["dtAlteracao"].value);
							}
						}
						
						public DateTime dtCriacao
						{
							get
							{
								//CONVERT(DATETIME, '12/08/1973',102)
								return System.Convert.ToDateTime(Parent.var["dtCriacao"].value);
							}
						}
						
						public DateTime dtNascimentoInicioAtividades
						{
							get
							{
								return System.Convert.ToDateTime(Parent.var["dtNascimento"].value);
							}
							set
							{
								Parent.var["dtNascimento"].value = value;
								Parent.var["dtNascimento"].Dirty = true;
							}
						}
						
						public int IdadeTempoExistencia
						{
							get
							{
								object d = new object();
								(System.Convert.ToDateTime(d)).op_Subtraction(DateTime.Now, Parent.var["dtNascimento"].value);
								return System.Convert.ToInt32(d);
							}
						}
						
						public string NomeRazaoSocial_key
						{
							get
							{
								return this.NomeRazaoSocial_Key;
							}
							set
							{
								this.NomeRazaoSocial_Key = value;
							}
						}
						
						public string NomeRazaoSocial_Key
						{
							get
							{
								return Parent.var["NomePrimary"].value;
							}
							set
							{
								if (value == "")
								{
									NBdbm.NBexception nbEX;
									nbEX = new NBexception("Este Campo não pode conter valor em branco");
									nbEX.Source = "Entidade - NomeRazaoSocial_Key";
									throw (nbEX);
								}
								
								Parent.var["NomePrimary"].value = value;
								Parent.var["NomePrimary"].Dirty = true;
							}
						}
						
						public string OrgaoEmissorIM
						{
							get
							{
								return Parent.var["OrgaoEmissor_RGIE"].value;
							}
							set
							{
								Parent.var["OrgaoEmissor_RGIE"].value = value;
								Parent.var["OrgaoEmissor_RGIE"].Dirty = true;
							}
						}
						
						public bool PessoaFisica
						{
							get
							{
								return Parent.var["PessoaFJ"].value;
							}
							set
							{
								Parent.var["PessoaFJ"].Dirty = true;
								Parent.var["PessoaFJ"].value = value;
							}
						}
						
						public string RgIE
						{
							get
							{
								return this.RGIE;
							}
							set
							{
								this.RGIE = value;
							}
						}
						
						public string RGIE
						{
							get
							{
								return Parent.var["RGIE"].value;
							}
							set
							{
								Parent.var["RGIE"].value = value;
								Parent.var["RGIE"].Dirty = true;
							}
						}
						
						public string TextoRespeito
						{
							get
							{
								return Parent.var["txtRespeito"].value;
							}
							set
							{
								Parent.var["txtRespeito"].value = value;
								Parent.var["txtRespeito"].Dirty = true;
							}
						}
						#endregion
						
					}
				}
				public class Endereco : allClass
				{
					
					private EnderecoCampos mCampos; //Interfaces.iCTR.Primitivas.iEndereco
					
					public Endereco() : base("CTRL_Enderecos")
					{
						mCampos = new EnderecoCampos(this);
					}
					public Endereco(tipos.tiposConection TipoConexao) : base("CTRL_Enderecos", TipoConexao)
					{
						mCampos = new EnderecoCampos(this);
					}
					public Interfaces.iCTR.Primitivas.iEndereco Campos
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
					public override void Dispose()
					{
						if (mCampos != null)
						{
							mCampos.Dispose();
						}
						mCampos = null;
						base.Dispose();
					}
					private class EnderecoCampos : Interfaces.iCTR.Primitivas.iEndereco
					{
						
						
						private allClass mParent;
						
						public EnderecoCampos(allClass allClass)
						{
							this.mParent = allClass;
						}
						internal allClass Parent
						{
							get
							{
								return mParent;
							}
							set
							{
								mParent = value;
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
							try
							{
								string filtro;
								filtro = " idEndereco = " + this.ID + " And idEntidade = " + this.idEntidade_key.ToString();
								this.Parent.SalvarPadrao(noCommit, filtro);
								
								//Finaliza a Transação
								self.AdmDB.FinalizaTransaction(noCommit);
								
							}
							catch (Exception ex)
							{
								NBexception mNBEx = new NBexception("Não foi possível Salvar o Endereço - " + this.Logradouro_key, ex);
								mNBEx.Source = "NBdbm.Fachadas.CTR.primitivas.Endereco.Salvar";
								throw (mNBEx);
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
								return Parent.var["IDEndereco"].value;
							}
						}
						
						public string Key
						{
							get
							{
								return this.ID.ToString() + this.Logradouro_key.ToString() + this.Municipio.ToString();
							}
						}
						
						public int idEntidade_key
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
						
						public string Logradouro_key
						{
							get
							{
								return Parent.var["Logradouro"].value;
							}
							set
							{
								if (value == "")
								{
									NBdbm.NBexception nbEX;
									nbEX = new NBexception("Este Campo não pode conter valor em branco");
									nbEX.Source = "Endereço - Logradouro_Key";
									throw (nbEX);
								}
								Parent.var["Logradouro"].value = value;
								Parent.var["Logradouro"].Dirty = true;
							}
						}
						
						public string complemento
						{
							get
							{
								return Parent.var["Complemento"].value;
							}
							set
							{
								Parent.var["Complemento"].value = value;
								Parent.var["Complemento"].Dirty = true;
							}
						}
						
						public string Bairro
						{
							get
							{
								return Parent.var["Bairro"].value;
							}
							set
							{
								Parent.var["Bairro"].value = value;
								Parent.var["Bairro"].Dirty = true;
							}
						}
						
						public string CEP
						{
							get
							{
								return Parent.var["CEP"].value;
							}
							set
							{
								Parent.var["CEP"].value = NBFuncoes.soNumero(value);
								Parent.var["CEP"].Dirty = true;
							}
						}
						
						public string Municipio
						{
							get
							{
								return Parent.var["Municipio"].value;
							}
							set
							{
								Parent.var["Municipio"].value = value;
								Parent.var["Municipio"].Dirty = true;
							}
						}
						
						public string UF
						{
							get
							{
								return Parent.var["UF"].value;
							}
							set
							{
								Parent.var["UF"].value = value;
								Parent.var["UF"].Dirty = true;
							}
						}
						
						public string Comentario
						{
							get
							{
								return Parent.var["Comentario"].value;
							}
							set
							{
								Parent.var["Comentario"].value = value;
								Parent.var["Comentario"].Dirty = true;
							}
						}
						
						public string Contato
						{
							get
							{
								return Parent.var["Contato"].value;
							}
							set
							{
								Parent.var["Contato"].Dirty = true;
								Parent.var["Contato"].value = value.Substring(0, 15);
							}
						}
						
						public bool Principal
						{
							get
							{
								return Convert.ToBoolean(Parent.var["Principal"].value);
							}
							set
							{
								Parent.var["Principal"].value = value;
								Parent.var["Principal"].Dirty = true;
							}
						}
						#endregion
						
					}
					
				}
				public class eMail : allClass
				{
					
					private EmailCampos mCampos; //Interfaces.iCTR.Primitivas.iEmail
					
					public eMail() : base("CTRL_Email")
					{
						mCampos = new EmailCampos(this);
					}
					public eMail(NBdbm.tipos.tiposConection TipoConexao) : base("CTRL_Email", TipoConexao)
					{
						mCampos = new EmailCampos(this);
					}
					public Interfaces.iCTR.Primitivas.iEmail Campos
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
					public override void Dispose()
					{
						if (mCampos != null)
						{
							mCampos.Dispose();
						}
						mCampos = null;
						base.Dispose();
					}
					
					private class EmailCampos : Interfaces.iCTR.Primitivas.iEmail
					{
						
						
						
						private allClass mParent;
						public EmailCampos(allClass allClass)
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
							try
							{
								string filtro;
								filtro = " idEmail = " + this.ID.ToString() + " And idEntidade = " + this.idEntidade_key.ToString();
								if (this.eMail_key != "")
								{
									this.Parent.SalvarPadrao(noCommit, filtro);
								}
								
								//Finaliza a Transação
								self.AdmDB.FinalizaTransaction(noCommit);
								
							}
							catch (Exception ex)
							{
								NBexception mNBEx = new NBexception("Não foi possível Salvar o Email - " + this.eMail_key, ex);
								mNBEx.Source = "NBdbm.Fachadas.CTR.primitivas.Email.Salvar";
								throw (mNBEx);
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
								return this.ID.ToString() + this.Descricao.ToString() + this.eMail_key.ToString();
							}
						}
						
						public int ID
						{
							get
							{
								return Parent.var["idEMail"].value;
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
								Parent.var["Descricao"].Dirty = true;
							}
						}
						
						public string eMail_key
						{
							get
							{
								return Parent.var["eMail"].value;
							}
							set
							{
								Parent.var["eMail"].value = value;
								Parent.var["eMail"].Dirty = true;
							}
						}
						
						public int idEntidade_key
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
						#endregion
						
					}
					
				}
				public class Url : allClass
				{
					
					private UrlCampos mCampos; //Interfaces.iCTR.Primitivas.iUrl
					
					public Url() : base("CTRL_Url")
					{
						mCampos = new UrlCampos(this);
					}
					public Url(NBdbm.tipos.tiposConection TipoConexao) : base("CTRL_Url", TipoConexao)
					{
						mCampos = new UrlCampos(this);
					}
					
					public Interfaces.iCTR.Primitivas.iUrl Campos
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
					public override void Dispose()
					{
						if (mCampos != null)
						{
							mCampos.Dispose();
						}
						mCampos = null;
						base.Dispose();
					}
					
					private class UrlCampos : Interfaces.iCTR.Primitivas.iUrl
					{
						
						
						private allClass mParent;
						public UrlCampos(allClass allClass)
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
							try
							{
								string filtro;
								filtro = " idUrl = " + this.ID.ToString() + " And idEntidade = " + this.idEntidade_key.ToString();
								if (this.URL_key != "")
								{
									this.Parent.SalvarPadrao(noCommit, filtro);
								}
								
								//Finaliza a Transação
								self.AdmDB.FinalizaTransaction(noCommit);
								
							}
							catch (Exception ex)
							{
								NBexception mNBEx = new NBexception("Não foi possível Salvar a URL - " + this.URL_key, ex);
								mNBEx.Source = "NBdbm.Fachadas.CTR.primitivas.URL.Salvar";
								throw (mNBEx);
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
								return this.ID.ToString() + this.Descricao.ToString() + this.URL_key.ToString();
							}
						}
						
						public int ID
						{
							get
							{
								return Parent.var["idUrl"].value;
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
								Parent.var["Descricao"].Dirty = true;
							}
						}
						
						public string Url_key
						{
							get
							{
								return this.URL_key;
							}
							set
							{
								this.URL_key = value;
							}
						}
						
						public string URL_key
						{
							get
							{
								return Parent.var["url"].value;
							}
							set
							{
								Parent.var["url"].value = value;
								Parent.var["url"].Dirty = true;
							}
						}
						
						public int idEntidade_key
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
						#endregion
						
					}
					
				}
				public class Telefone : allClass
				{
					
					private TelefoneCampos mCampos; //Interfaces.iCTR.Primitivas.iTelefone
					
					public Telefone() : base("CTRL_Fones")
					{
						mCampos = new TelefoneCampos(this);
					}
					public Telefone(tipos.tiposConection TipoConexao) : base("CTRL_Fones", TipoConexao)
					{
						mCampos = new TelefoneCampos(this);
					}
					
					public Interfaces.iCTR.Primitivas.iTelefone Campos
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
								base.toObject = mCampos.parent;
							}
						}
					}
					public override void Dispose()
					{
						if (mCampos != null)
						{
							mCampos.Dispose();
						}
						mCampos = null;
						base.Dispose();
					}
					
					private class TelefoneCampos : Interfaces.iCTR.Primitivas.iTelefone
					{
						
						
						
						private allClass mParent;
						
						public TelefoneCampos(allClass allClass)
						{
							this.mParent = allClass;
						}
						internal allClass parent
						{
							get
							{
								return mParent;
							}
							set
							{
								mParent = value;
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
							try
							{
								string filtro;
								filtro = " idFone = " + this.ID.ToString() + " And idEntidade = " + this.idEntidade_key.ToString();
								if (this.Fone_key != "")
								{
									this.parent.SalvarPadrao(noCommit, filtro);
								}
								
								//Finaliza a Transação
								self.AdmDB.FinalizaTransaction(noCommit);
								
							}
							catch (Exception ex)
							{
								NBexception mNBEx = new NBexception("Não foi possível Salvar o Telefone - " + this.Fone_key, ex);
								mNBEx.Source = "NBdbm.Fachadas.CTR.primitivas.Telefone.Salvar";
								throw (mNBEx);
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
						public void Clear_filters()
						{
							parent.Clear_filters();
						}
						public void Clear_vars()
						{
							parent.Clear_vars();
						}
						#region    Propriedades - Fields
						
						public string Key
						{
							get
							{
								return this.ID.ToString() + this.DDD_key.ToString() + this.Fone_key.ToString();
							}
						}
						
						public int ID
						{
							get
							{
								return parent.var["idFone"].value;
							}
						}
						
						public string Descricao
						{
							get
							{
								return parent.var["Descricao"].value;
							}
							set
							{
								parent.var["Descricao"].value = value;
								parent.var["Descricao"].Dirty = true;
							}
						}
						
						public string DDD_key
						{
							get
							{
								return parent.var["DDD"].value;
							}
							set
							{
								parent.var["DDD"].value = NBFuncoes.soNumero(value).ToString();
								parent.var["DDD"].Dirty = true;
							}
						}
						
						public string Fone_key
						{
							get
							{
								return parent.var["Fone"].value;
							}
							set
							{
								parent.var["Fone"].value = NBFuncoes.soNumero(value).ToString();
								parent.var["Fone"].Dirty = true;
							}
						}
						
						public string Ramal
						{
							get
							{
								return parent.var["Ramal"].value;
							}
							set
							{
								parent.var["Ramal"].value = value;
								parent.var["Ramal"].Dirty = true;
							}
						}
						
						public string Contato
						{
							get
							{
								return parent.var["Contato"].value;
							}
							set
							{
								parent.var["Contato"].value = value;
								parent.var["Contato"].Dirty = true;
							}
						}
						
						public int idEndereco
						{
							get
							{
								return parent.var["idEndereco"].value;
							}
							set
							{
								parent.var["idEndereco"].value = value;
								parent.var["idEndereco"].Dirty = true;
							}
						}
						
						public int idEntidade_key
						{
							get
							{
								return parent.var["idEntidade"].value;
							}
							set
							{
								parent.var["idEntidade"].value = value;
								parent.var["idEntidade"].Dirty = true;
							}
						}
						#endregion
						
					}
					
				}
				public class Usuario : allClass
				{
					
					private UsuarioCampos mCampos; //Interfaces.iCTR.Primitivas.iUsuario
					
					public Usuario() : base("CTRL_Usuario")
					{
						mCampos = new UsuarioCampos(this);
					}
					public Usuario(tipos.tiposConection TipoConexao) : base("CTRL_Usuario", TipoConexao)
					{
						mCampos = new UsuarioCampos(this);
					}
					
					public Interfaces.iCTR.Primitivas.iUsuario Campos
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
					public override void Dispose()
					{
						if (mCampos != null)
						{
							mCampos.Dispose();
						}
						mCampos = null;
						base.Dispose();
					}
					public void getUsuarioFromLogin(string Login)
					{
						this.Clear_filters();
						this.Clear_vars();
						this.filterWhere = "Login=\'" + Login + "\'";
						this.getFields();
					}
					private class UsuarioCampos : Interfaces.iCTR.Primitivas.iUsuario
					{
						
						
						private allClass mParent;
						
						public UsuarioCampos(allClass allClass)
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
							
							Parent = null;
						}
						public void salvar(bool noCommit)
						{
							this.Salvar(noCommit);
						}
						
						public void Salvar(bool noCommit)
						{
							try
							{
								string filtro;
								filtro = " Login = \'" + this.login_key + "\' ";
								this.Parent.SalvarPadrao(noCommit, filtro);
								
								//Finaliza a Transação
								self.AdmDB.FinalizaTransaction(noCommit);
								
							}
							catch (Exception ex)
							{
								NBexception mNBEx = new NBexception("Não foi possível Salvar o Usuário - " + this.login_key, ex);
								mNBEx.Source = "NBdbm.Fachadas.CTR.primitivas.Usuario.Salvar";
								throw (mNBEx);
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
								return this.login_key.ToString();
							}
						}
						
						public int ID
						{
							get
							{
								return Parent.var["idUsuario"].value;
							}
						}
						
						public string login_key
						{
							get
							{
								return Parent.var["Login"].value;
							}
							set
							{
								if (value == "")
								{
									NBdbm.NBexception nbEX;
									nbEX = new NBexception("Este Campo não pode conter valor em branco");
									nbEX.Source = "Usuário - Login_Key";
									throw (nbEX);
								}
								Parent.var["Login"].Dirty = true;
								Parent.var["Login"].value = value;
							}
						}
						
						public string senha
						{
							get
							{
								return NBFuncoes.decripto(Parent.var["Senha"].value);
							}
							set
							{
								if (value == "")
								{
									NBdbm.NBexception nbEX;
									nbEX = new NBexception("Este Campo não pode conter valor em branco");
									nbEX.Source = "Usuário - Senha";
									throw (nbEX);
								}
								Parent.var["Senha"].Dirty = true;
								Parent.var["Senha"].value = NBFuncoes.cripto(value);
							}
						}
						
						public DateTime dtAdmissao
						{
							get
							{
								return Parent.var["dtAdmissao"].value;
							}
							set
							{
								Parent.var["dtAdmissao"].Dirty = true;
								Parent.var["dtAdmissao"].value = value;
								//                        'Return CDate(Parent.var("dtCriacao").Value).ToString(self.Settings.sintaxeData)
							}
						}
						
						public DateTime dtDesligamento
						{
							get
							{
								return Parent.var["dtDesligamento"].value;
							}
							set
							{
								Parent.var["dtDesligamento"].Dirty = true;
								Parent.var["dtDesligamento"].value = value;
							}
						}
						
						public int idUsuarioCadastrador
						{
							get
							{
								return Parent.var["idUsuarioCadastrador"].value;
							}
							set
							{
								Parent.var["idUsuarioCadastrador"].Dirty = true;
								Parent.var["idUsuarioCadastrador"].value = value;
							}
						}
						
						public string idEmpresa
						{
							get
							{
								return Parent.var["idEmpresa"].value;
							}
							set
							{
								Parent.var["idEmpresa"].Dirty = true;
								Parent.var["idEmpresa"].value = value;
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
						
						public string matricula
						{
							get
							{
								return Parent.var["matricula"].value;
							}
							set
							{
								Parent.var["matricula"].Dirty = true;
								Parent.var["matricula"].value = value;
							}
						}
						#endregion
						
					}
					
				}
				public class UsuarioConfig : allClass
				{
					
					private UsuarioConfigCampos mCampos; //Interfaces.iCTR.Primitivas.iUsuarioConfig
					
					public UsuarioConfig() : base("CTRL_UsuarioConfig")
					{
						mCampos = new UsuarioConfigCampos(this);
					}
					public UsuarioConfig(tipos.tiposConection TipoConexao) : base("CTRL_UsuarioConfig", TipoConexao)
					{
						mCampos = new UsuarioConfigCampos(this);
					}
					public Interfaces.iCTR.Primitivas.iUsuarioConfig Campos
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
					public override void Dispose()
					{
						if (mCampos != null)
						{
							mCampos.Dispose();
						}
						mCampos = null;
						base.Dispose();
					}
					
					private class UsuarioConfigCampos : Interfaces.iCTR.Primitivas.iUsuarioConfig
					{
						
						
						private allClass mParent;
						
						public UsuarioConfigCampos(allClass allClass)
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
							
							Parent = null;
						}
						public void salvar(bool noCommit)
						{
							this.Salvar(noCommit);
						}
						
						public void Salvar(bool noCommit)
						{
							try
							{
								string filtro;
								filtro = " idUsuario = " + this.idUsuario_key;
								this.Parent.SalvarPadrao(noCommit, filtro);
								
								//Finaliza a Transação
								self.AdmDB.FinalizaTransaction(noCommit);
								
							}
							catch (Exception ex)
							{
								NBexception mNBEx = new NBexception("Não foi possível Salvar as Configurações do Usuário - " + this.idUsuario_key.ToString(), ex);
								mNBEx.Source = "NBdbm.Fachadas.CTR.primitivas.UsuarioConfig.Salvar";
								throw (mNBEx);
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
						public void Clear_filters()
						{
							Parent.Clear_filters();
						}
						public void Clear_vars()
						{
							Parent.Clear_vars();
						}
						public void AtivaDesativa(bool ativa)
						{
							try
							{
								this.UsuarioAtivo = ativa;
								this.Salvar();
							}
							catch (NBexception ex)
							{
								ex.Source = "**** Configurações do Usuário ****";
								throw (ex);
							}
						}
						
						#region    Propriedades - Fields
						
						public string Key
						{
							get
							{
								return this.idUsuario_key.ToString();
							}
						}
						
						
						public int ID
						{
							get
							{
								return Parent.var["idUsuarioConfig"].value;
							}
						}
						
						public int idUsuario_key
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
						
						public string Funcao
						{
							get
							{
								return this.funcao;
							}
							set
							{
								this.funcao = value;
							}
						}
						
						public string funcao
						{
							get
							{
								return Parent.var["funcao"].value;
							}
							set
							{
								Parent.var["funcao"].Dirty = true;
								Parent.var["funcao"].value = value;
							}
						}
						
						public bool UsuarioAtivo
						{
							get
							{
								return Parent.var["ativo"].value;
							}
							set
							{
								Parent.var["ativo"].Dirty = true;
								Parent.var["ativo"].value = value;
							}
						}
						
						public bool pmLer
						{
							get
							{
								return Parent.var["pmler"].value;
							}
							set
							{
								Parent.var["pmler"].Dirty = true;
								Parent.var["pmler"].value = value;
							}
						}
						
						public bool pmEditar
						{
							get
							{
								return Parent.var["pmEditar"].value;
							}
							set
							{
								Parent.var["pmEditar"].Dirty = true;
								Parent.var["pmEditar"].value = value;
							}
						}
						
						public bool pmIncluir
						{
							get
							{
								return Parent.var["pmIncluir"].value;
							}
							set
							{
								Parent.var["pmIncluir"].Dirty = true;
								Parent.var["pmIncluir"].value = value;
							}
						}
						
						public bool pmExcluir
						{
							get
							{
								return Parent.var["pmExcluir"].value;
							}
							set
							{
								Parent.var["pmExcluir"].Dirty = true;
								Parent.var["pmExcluir"].value = value;
							}
						}
						
						public bool pmSisExecutavel
						{
							get
							{
								return Parent.var["pmSisExecutavel"].value;
							}
							set
							{
								Parent.var["pmSisExecutavel"].Dirty = true;
								Parent.var["pmSisExecutavel"].value = value;
							}
						}
						
						public bool pmSisWeb
						{
							get
							{
								return Parent.var["pmSisWeb"].value;
							}
							set
							{
								Parent.var["pmSisWeb"].Dirty = true;
								Parent.var["pmSisWeb"].value = value;
							}
						}
						
						public System.Text.StringBuilder xmlConfig
						{
							get
							{
								return Parent.var["xmlConfig"].value;
							}
							set
							{
								Parent.var["xmlConfig"].Dirty = true;
								Parent.var["xmlConfig"].value = value.ToString();
							}
						}
						
						public byte Credencial
						{
							get
							{
								return Parent.var["Credencial"].value;
							}
							set
							{
								Parent.var["Credencial"].Dirty = true;
								Parent.var["Credencial"].value = value;
							}
						}
						
						#endregion
						
					}
				}
				public class HistoricoLogin : allClass
				{
					
					private HistoricoLoginCampos mCampos; //Interfaces.iCTR.Primitivas.iHistoricoLogin
					
					public HistoricoLogin() : base("CTRL_HistoricoLogin")
					{
						mCampos = new HistoricoLoginCampos(this);
					}
					public HistoricoLogin(tipos.tiposConection TipoConexao) : base("CTRL_HistoricoLogin", TipoConexao)
					{
						mCampos = new HistoricoLoginCampos(this);
					}
					public Interfaces.iCTR.Primitivas.iHistoricoLogin Campos
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
					public override void Dispose()
					{
						if (mCampos != null)
						{
							mCampos.Dispose();
						}
						mCampos = null;
						base.Dispose();
					}
					
					private class HistoricoLoginCampos : Interfaces.iCTR.Primitivas.iHistoricoLogin
					{
						
						
						private allClass mParent;
						
						public HistoricoLoginCampos(allClass allClass)
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
							
							Parent = null;
						}
						public void salvar(bool noCommit)
						{
							this.Salvar(noCommit);
						}
						
						public void Salvar(bool noCommit)
						{
							string filtro;
							filtro = " IdHistoricoLogin = " + this.ID;
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
								return this.ID.ToString();
							}
						}
						
						public int ID
						{
							get
							{
								return Parent.var["idHistoricoLogin"].value;
							}
						}
						
						public DateTime dtHoraLogoff
						{
							get
							{
								return Parent.var["dtHoraLogOff"].value;
							}
							set
							{
								Parent.var["dtHoraLogOff"].Dirty = true;
								Parent.var["dtHoraLogOff"].value = value;
							}
						}
						
						public DateTime dtHoraLogon
						{
							get
							{
								return Parent.var["dtHoraLogon"].value;
							}
							set
							{
								Parent.var["dtHoraLogOff"].Dirty = true;
								Parent.var["dtHoraLogOff"].value = value;
							}
						}
						
						public string nomeLogin
						{
							get
							{
								return Parent.var["nomeLogin"].value;
							}
							set
							{
								Parent.var["nomeLogin"].Dirty = true;
								Parent.var["nomeLogin"].value = value;
							}
						}
						
						public string nomeMaquina
						{
							get
							{
								return Parent.var["nomeMaquina"].value;
							}
							set
							{
								Parent.var["nomeMaquina"].Dirty = true;
								Parent.var["nomeMaquina"].value = value;
							}
						}
						#endregion
						
					}
				}
				public class HistoricoTabela : allClass
				{
					
					private HistoricoTBCampos mCampos; //Interfaces.iCTR.Primitivas.iHistoricoTabela
					
					public HistoricoTabela() : base("CTRL_HistoricoTB")
					{
						mCampos = new HistoricoTBCampos(this);
					}
					public HistoricoTabela(tipos.tiposConection TipoConexao) : base("CTRL_HistoricoTB", TipoConexao)
					{
						mCampos = new HistoricoTBCampos(this);
					}
					public Interfaces.iCTR.Primitivas.iHistoricoTabela Campos
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
					public override void Dispose()
					{
						if (mCampos != null)
						{
							mCampos.Dispose();
						}
						mCampos = null;
						base.Dispose();
					}
					
					private class HistoricoTBCampos : NBdbm.Interfaces.iCTR.Primitivas.iHistoricoTabela
					{
						
						
						private allClass mParent;
						private Collection mColFields;
						
						public HistoricoTBCampos(allClass allClass)
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
							
							Parent = null;
						}
						public void salvar(bool noCommit)
						{
							this.Salvar(noCommit);
						}
						
						public void Salvar(bool noCommit)
						{
							Parent.inserir(noCommit);
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
								return this.idAutoNum_key.ToString() + this.tbLog_key.ToString();
							}
						}
						
						public int ID
						{
							get
							{
								return Parent.var["idHistoricoTB"].value;
							}
						}
						
						public int idAutoNum_key
						{
							get
							{
								return Parent.var["idAutoNum"].value;
							}
							set
							{
								Parent.var["idAutoNum"].Dirty = true;
								Parent.var["idAutoNum"].value = value;
							}
						}
						
						public Microsoft.VisualBasic.Collection colFields
						{
							set
							{
								mColFields = value;
							}
						}
						
						public DateTime dtHistorico
						{
							get
							{
								return Parent.var["dtHistorico"].value;
							}
							set
							{
								Parent.var["dtHistorico"].Dirty = true;
								Parent.var["dtHistorico"].value = value;
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
						
						public string tbLog_key
						{
							get
							{
								return Parent.var["tb_Log"].value;
							}
							set
							{
								Parent.var["tb_Log"].Dirty = true;
								Parent.var["tb_Log"].value = value;
							}
						}
						
						public string xmlLog
						{
							get
							{
								return Parent.xmlColFields(mColFields);
							}
							
						}
						#endregion
						
					}
				}
				public class No : allClass
				{
					
					private NoCampos mCampos; //Interfaces.iCTR.Primitivas.iNo
					
					public No() : base("CTRL_Nos")
					{
						mCampos = new NoCampos(this);
					}
					public No(tipos.tiposConection TipoConexao) : base("CTRL_Nos", TipoConexao)
					{
						mCampos = new NoCampos(this);
					}
					public Interfaces.iCTR.Primitivas.iNo Campos
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
					public override void Dispose()
					{
						if (mCampos != null)
						{
							mCampos.Dispose();
						}
						mCampos = null;
						base.Dispose();
					}
					
					private class NoCampos : Interfaces.iCTR.Primitivas.iNo
					{
						
						
						private allClass mParent;
						
						public NoCampos(allClass allClass)
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
							throw (new NBexception("Somente a NBArvore Grava, Altera ou Exclui dados na Tabela de Nós."));
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
								return this.idNo_key.ToString() + this.xmPath_key.ToString();
							}
						}
						
						public int ID
						{
							get
							{
								return Parent.var["IDNo"].value;
							}
						}
						
						public int idNo_key
						{
							get
							{
								return Parent.var["IDNo"].value;
							}
							set
							{
								Parent.var["IDNo"].value = value;
								Parent.var["IDNo"].Dirty = true;
							}
						}
						
						public string filhos
						{
							get
							{
								return Parent.var["filhos"].value;
							}
							set
							{
								Parent.var["filhos"].value = value;
								Parent.var["filhos"].Dirty = true;
							}
						}
						
						public string indice
						{
							get
							{
								return Parent.var["Indice"].value;
							}
							set
							{
								Parent.var["Indice"].value = value;
								Parent.var["Indice"].Dirty = true;
							}
						}
						
						public string nome
						{
							get
							{
								return Parent.var["nome"].value;
							}
							set
							{
								Parent.var["nome"].value = value;
								Parent.var["nome"].Dirty = true;
							}
						}
						
						public string xmPath_key
						{
							get
							{
								return Parent.var["xmPath"].value;
							}
							set
							{
								Parent.var["xmPath"].value = value;
								Parent.var["xmPath"].Dirty = true;
							}
						}
						
						#endregion
						
					}
					
				}
				public class LinkUsuarioNo : allClass
				{
					
					private LinkUsuarioNoCampos mCampos; //Interfaces.iCTR.Primitivas.iLinkUsuarioNo
					
					public LinkUsuarioNo() : base("CTRL_Link_usuario_No")
					{
						mCampos = new LinkUsuarioNoCampos(this);
					}
					public LinkUsuarioNo(tipos.tiposConection TipoConexao) : base("CTRL_Link_usuario_No", TipoConexao)
					{
						mCampos = new LinkUsuarioNoCampos(this);
					}
					public Interfaces.iCTR.Primitivas.iLinkUsuarioNo Campos
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
					public override void Dispose()
					{
						if (mCampos != null)
						{
							mCampos.Dispose();
						}
						mCampos = null;
						base.Dispose();
					}
					
					private class LinkUsuarioNoCampos : Interfaces.iCTR.Primitivas.iLinkUsuarioNo
					{
						
						
						private allClass mParent;
						public LinkUsuarioNoCampos(allClass allClass)
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
							filtro = " IdLink_Usuario_No = " + this.ID;
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
							
							mParent = null;
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
								return Parent.var["IdLink_Usuario_No"].value;
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
						
						public int idUsuario
						{
							get
							{
								return Parent.var["IdUsuario"].value;
							}
							set
							{
								Parent.var["IdUsuario"].value = value;
								Parent.var["IdUsuario"].Dirty = true;
							}
						}
						#endregion
						
					}
					
				}
				public class LinkEntidadeNo : allClass
				{
					
					private LinkEntidadeNoCampos mCampos; //Interfaces.iCTR.Primitivas.iLinkEntidadeNo
					
					public LinkEntidadeNo() : base("CTRL_Link_Entidade_No")
					{
						mCampos = new LinkEntidadeNoCampos(this);
					}
					public LinkEntidadeNo(tipos.tiposConection TipoConexao) : base("CTRL_Link_Entidade_No", TipoConexao)
					{
						mCampos = new LinkEntidadeNoCampos(this);
					}
					public Interfaces.iCTR.Primitivas.iLinkEntidadeNo Campos
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
					public override void Dispose()
					{
						if (mCampos != null)
						{
							mCampos.Dispose();
						}
						mCampos = null;
						base.Dispose();
					}
					
					private class LinkEntidadeNoCampos : Interfaces.iCTR.Primitivas.iLinkEntidadeNo
					{
						
						
						private allClass mParent;
						public LinkEntidadeNoCampos(allClass allClass)
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
							filtro = " idEntidade = " + this.idEntidade + " and idNo = " + this.idNo;
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
							
							mParent = null;
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
								return Parent.var["IdLink_Entidade_No"].value;
							}
						}
						
						public int idEntidade
						{
							get
							{
								return Parent.var["IdEntidade"].value;
							}
							set
							{
								Parent.var["IdEntidade"].value = value;
								Parent.var["IdEntidade"].Dirty = true;
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
				public class LinkEntidadeUsuario : allClass
				{
					
					private LinkEntidadeUsuarioCampos mCampos; // Interfaces.iCTR.Primitivas.iLinkEntidadeUsuario
					
					public LinkEntidadeUsuario() : base("CTRL_Link_Usuario_Entidade")
					{
						mCampos = new LinkEntidadeUsuarioCampos(this);
					}
					public LinkEntidadeUsuario(tipos.tiposConection TipoConexao) : base("CTRL_Link_Usuario_Entidade", TipoConexao)
					{
						mCampos = new LinkEntidadeUsuarioCampos(this);
					}
					public Interfaces.iCTR.Primitivas.iLinkEntidadeUsuario Campos
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
					public override void Dispose()
					{
						if (mCampos != null)
						{
							mCampos.Dispose();
						}
						mCampos = null;
						base.Dispose();
					}
					
					private class LinkEntidadeUsuarioCampos : Interfaces.iCTR.Primitivas.iLinkEntidadeUsuario
					{
						
						private allClass mParent;
						public LinkEntidadeUsuarioCampos(allClass allClass)
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
							filtro = " IdLink_Entidade_Usuario = " + this.ID;
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
							
							mParent = null;
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
								return Parent.var["IdLink_Entidade_Usuario"].value;
							}
						}
						
						public int idEntidade
						{
							get
							{
								return Parent.var["IdEntidade"].value;
							}
							set
							{
								Parent.var["IdEntidade"].value = value;
								Parent.var["IdEntidade"].Dirty = true;
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
								Parent.var["idUsuario"].value = value;
								Parent.var["idusuario"].Dirty = true;
							}
						}
						
						#endregion
						
					}
				}
				public class LinkEntidadePlx : allClass
				{
					
					private LinkEntidadePlxCampos mCampos; //Interfaces.iCTR.Primitivas.iLinkEntidadePlx
					
					public LinkEntidadePlx() : base("CTRL_Link_Entidade_PLX")
					{
						mCampos = new LinkEntidadePlxCampos(this);
					}
					public LinkEntidadePlx(tipos.tiposConection TipoConexao) : base("CTRL_Link_Entidade_PLX", TipoConexao)
					{
						mCampos = new LinkEntidadePlxCampos(this);
					}
					public Interfaces.iCTR.Primitivas.iLinkEntidadePlx Campos
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
						base.Dispose();
					}
					
					private class LinkEntidadePlxCampos : Interfaces.iCTR.Primitivas.iLinkEntidadePlx
					{
						
						private allClass mParent;
						
						public LinkEntidadePlxCampos(allClass allClass)
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
							filtro = " IdLink = " + this.ID;
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
							
							mParent = null;
						}
						
						#region    Propriedades - Fields
						
						public string Key
						{
							get
							{
								return this.idEntidade_key.ToString() + this.Plx_key.ToString();
							}
						}
						
						public int ID
						{
							get
							{
								return Parent.var["IdLink"].value;
							}
						}
						
						public int idEntidade_key
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
						
						public int idAutoNumPlx_Key
						{
							get
							{
								return this.idAutoNumPlx;
							}
							set
							{
								this.idAutoNumPlx = value;
							}
						}
						
						public int idAutoNumPlx
						{
							get
							{
								return Parent.var["idAutoNumPlx"].value;
							}
							set
							{
								Parent.var["idAutoNumPlx"].value = value;
								Parent.var["idAutoNumPlx"].Dirty = true;
							}
						}
						
						public string Plx_key
						{
							get
							{
								return Parent.var["PLX"].value;
							}
							set
							{
								Parent.var["PLX"].value = value;
								Parent.var["PLX"].Dirty = true;
							}
						}
						#endregion
						
					}
				}
				public class LinkEntidadeEntidade : allClass
				{
					
					private LinkEntidadeEntidadeCampos mCampos; //Interfaces.iCTR.Primitivas.iLinkEntidadeEntidade
					
					public LinkEntidadeEntidade() : base("CTRL_LinkEntidadeEntidade")
					{
						mCampos = new LinkEntidadeEntidadeCampos(this);
					}
					public LinkEntidadeEntidade(tipos.tiposConection TipoConexao) : base("CTRL_LinkEntidadeEntidade", TipoConexao)
					{
						mCampos = new LinkEntidadeEntidadeCampos(this);
					}
					public Interfaces.iCTR.Primitivas.iLinkEntidadeEntidade Campos
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
					public override void Dispose()
					{
						if (mCampos != null)
						{
							mCampos.Dispose();
						}
						mCampos = null;
						base.Dispose();
					}
					
					private class LinkEntidadeEntidadeCampos : Interfaces.iCTR.Primitivas.iLinkEntidadeEntidade
					{
						
						private allClass mParent;
						
						public LinkEntidadeEntidadeCampos(allClass allClass)
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
							filtro = " IdRelacionamento = " + this.ID;
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
							
							mParent = null;
						}
						
						#region    Propriedades - Fields
						
						public string Key
						{
							get
							{
								return this.idEntidadeBase_key.ToString();
							}
						}
						
						public int ID
						{
							get
							{
								return Parent.var["idLinkEntidadeEntidade"].value;
							}
						}
						
						public int idEntidadeBase_key
						{
							get
							{
								return Parent.var["idEntidadeBase"].value;
							}
							set
							{
								Parent.var["idEntidadeBase"].value = value;
								Parent.var["idEntidadeBase"].Dirty = true;
							}
						}
						
						public int idEntidadeLink_Key
						{
							get
							{
								return this.idAutoNumPlx;
							}
							set
							{
								this.idAutoNumPlx = value;
							}
						}
						
						public int idAutoNumPlx
						{
							get
							{
								return Parent.var["idEntidadeLink"].value;
							}
							set
							{
								Parent.var["idEntidadeLink"].value = value;
								Parent.var["idEntidadeLink"].Dirty = true;
							}
						}
						
						public string grauRelacionamento
						{
							get
							{
								return Parent.var["grauRelacionamento"].value;
							}
							set
							{
								Parent.var["grauRelacionamento"].value = value;
								Parent.var["grauRelacionamento"].Dirty = true;
							}
						}
						#endregion
						
					}
				}
				public class Spool : allClass
				{
					
					private spoolCampos mCampos; //Interfaces.iCTR.Primitivas.iSpool
					
					public Spool() : base("CTRL_Spool")
					{
						mCampos = new spoolCampos(this);
					}
					public Spool(tipos.tiposConection TipoConexao) : base("CTRL_Spool", TipoConexao)
					{
						mCampos = new spoolCampos(this);
					}
					public Interfaces.iCTR.Primitivas.iSpool Campos
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
					public override void Dispose()
					{
						if (mCampos != null)
						{
							mCampos.Dispose();
						}
						mCampos = null;
						base.Dispose();
					}
					
					private class spoolCampos : Interfaces.iCTR.Primitivas.iSpool
					{
						
						private allClass mParent;
						
						public spoolCampos(allClass allClass)
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
							filtro = " idSpool = " + this.ID;
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
							
							mParent = null;
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
								return Parent.var["idSpool"].value;
							}
						}
						
						public int idAutoNum
						{
							get
							{
								return Parent.var["idAutoNum"].value;
							}
							set
							{
								Parent.var["idAutoNum"].Dirty = true;
								Parent.var["idAutoNum"].value = value;
							}
						}
						
						public bool impressa
						{
							get
							{
								bool retorno;
								if (Parent.var["impressa"].value == 1)
								{
									retorno = true;
								}
								else
								{
									retorno = false;
								}
								return retorno;
							}
							set
							{
								Parent.var["impressa"].value = value;
								Parent.var["impressa"].Dirty = true;
							}
						}
						
						public int quantidade
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
						
						public string tabela
						{
							get
							{
								return Parent.var["tabela"].value;
							}
							set
							{
								Parent.var["tabela"].Dirty = true;
								Parent.var["tabela"].value = value;
							}
						}
						
						public string xmlCSS
						{
							get
							{
								return Parent.var["xmlcss"].value;
							}
							set
							{
								Parent.var["xmlcss"].Dirty = true;
								Parent.var["xmlcss"].value = value;
							}
						}
						
						public string xmlInfo
						{
							get
							{
								return Parent.var["xmlInfo"].value;
							}
							set
							{
								Parent.var["xmlInfo"].Dirty = true;
								Parent.var["xmlInfo"].value = value;
							}
						}
						#endregion
						
					}
				}
				
			}
			
		}
		
	}
	
	
	
	
	
	
	
	
	
	
}
