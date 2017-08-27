using System.Data;
using System.Collections;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System;

namespace NBdbm
{
	namespace Parsers
	{
		
		namespace Finan32
		{
			
			namespace P_Fachadas
			{
				
				public class admDB_Finan32
				{
					
					
					private ADM.admDB_OLEDB mCNN;
					private string DBQ;
					private string SystemDB;
					
					public admDB_Finan32(string DBQ, string SystemDB)
					{
						//Public Function Action(ByVal DBQ As String, ByVal SystemDB As String) As NBdbm.tipos.Retorno
						
						this.DBQ = DBQ;
						this.SystemDB = SystemDB;
						
					}
					
					internal ADM.admDB_OLEDB cnn
					{
						get
						{
							if (mCNN == null)
							{
								//este não funciona! "Driver={Microsoft Access Driver (*.mdb)}; Dbq=" & DBQ & "; SystemDB=" & SystemDB & ";"
								//mCNN.connection.ConnectionString = "Provider=SQLOLEDB.1;database=" & DBQ & ";User ID=ProSystem_;Password=nitromate;SystemDB=" & SystemDB
								//cnnSTR = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" & DBQ
								NBdbm.ADM.stringConnection cnnSTR = new NBdbm.ADM.stringConnection();
								cnnSTR.ConnProperty.Add("Provider", "Provider=Microsoft.Jet.OLEDB.4.0", System.Type.GetType("System.String"), true);
								cnnSTR.ConnProperty.Add("DataSource", "Data Source=" + DBQ, System.Type.GetType("System.String"), true);
								//Call cnnSTR.ConnProperty.Add("Login", "User ID=login", System.Type.GetType("System.String"), True)
								//Call cnnSTR.ConnProperty.Add("Senha", "Password=senha", System.Type.GetType("System.String"), True)
								//Call cnnSTR.ConnProperty.Add("SystemDB", "SystemDB=" & SystemDB, System.Type.GetType("System.String"), True)
								mCNN = new ADM.admDB_OLEDB(cnnSTR.StringConnection);
							}
							return mCNN;
						}
					}
				}
				
				//===[ Entidade ]==================================================================================================
				//Off - Edgar em construção
				public class Entidade : NBdbm.Fachadas.allClass
				{
					
					
					//Private admDB As Interfaces.iAdmDB
					private EntidadeCampos mCampos;
					
					public Entidade(Interfaces.iAdmDB cnn) : base("Cadastros de Entidades", cnn)
					{
						mCampos = new EntidadeCampos(this);
					}
					
					public EntidadeCampos campos
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
					
					public class EntidadeCampos
					{
						
						
						private NBdbm.Fachadas.allClass mParent;
						
						public EntidadeCampos(NBdbm.Fachadas.allClass allClass)
						{
							this.mParent = allClass;
						}
						internal NBdbm.Fachadas.allClass Parent
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
							Parent = null;
						}
						public tipos.Retorno salvar(bool noCommit)
						{
							//Implentar:
							//Aqui será feita a verificação do me.id = null para inserir ou caso
							//contrário será realizado uma edição
							tipos.Retorno retorno = new tipos.Retorno();
							//Parent.var("dtAlteracao").Dirty = True
							//Parent.var("dtAlteracao").value = CDate(Now).ToString(self.Settings.sintaxeData)
							//Parent.filterWhere = " idCPFCNPJ = " & Parent.var("idCPFCNPJ").value & " and NomePrimary = '" & Me.NomeRazaoSocial_Key & "' "
							//retorno = Parent.editar(noCommit)
							//retorno.Objeto = "Editado "
							//If retorno.Sucesso = False Then
							//  Parent.var("dtCriacao").Dirty = True
							//  Parent.var("dtCriacao").value = CDate(Now).ToString(self.Settings.sintaxeData)
							//  retorno = Parent.inserir(noCommit)
							//  retorno.Objeto = "Inserido "
							//End If
							//retorno.Objeto = retorno.Objeto & " | CTRL_Entidade | Reg:" & Me.ID.ToString
							return retorno;
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
						
						//SELECT
						//[Cadastros de entidades].IdFix
						//[Cadastros de entidades].Id
						//[Cadastros de entidades].CPF
						//[Cadastros de entidades].RG
						//[Cadastros de entidades].Nome
						//[Cadastros de entidades].[Conhecido como]
						//[Cadastros de entidades].Cargo
						//[Cadastros de entidades].Empresa
						//[Cadastros de entidades].[Forma de tratamento]
						//[Cadastros de entidades].[Data de nascimento]
						//[Cadastros de entidades].Atividade
						//[Cadastros de entidades].Endereço
						//[Cadastros de entidades].Complemento
						//[Cadastros de entidades].Bairro
						//[Cadastros de entidades].Cidade
						//[Cadastros de entidades].UF
						//[Cadastros de entidades].Cep
						//[Cadastros de entidades].Coment
						//[Cadastros de entidades].Mais
						//[Cadastros de entidades].Classificação
						//[Cadastros de entidades].[Sub-classificação]
						//[Cadastros de entidades].P1
						//[Cadastros de entidades].P2
						//[Cadastros de entidades].P3
						//[Cadastros de entidades].P4
						//[Cadastros de entidades].P5
						//[Cadastros de entidades].P6
						//[Cadastros de entidades].N1
						//[Cadastros de entidades].N2
						//[Cadastros de entidades].N3
						//[Cadastros de entidades].N4
						//[Cadastros de entidades].N5
						//[Cadastros de entidades].N6
						//[Cadastros de entidades].Fone1
						//[Cadastros de entidades].Fone2
						//[Cadastros de entidades].Fone3
						//[Cadastros de entidades].Fone4
						//[Cadastros de entidades].Fornecedor
						//[Cadastros de entidades].Cliente
						//[Cadastros de entidades].Outros
						//[Cadastros de entidades].[Pessoa Juridica]
						//[Cadastros de entidades].[Prazo de carencia em dias]
						//[Cadastros de entidades].[Multa por atraso]
						//[Cadastros de entidades].[Juro mensal sobre inadimplencia]
						//[Cadastros de entidades].[Data de criação]
						//[Cadastros de entidades].[Ultima alteração]
						//[Cadastros de entidades].Foto
						//[Cadastros de entidades].Versao
						//[Cadastros de entidades].Rep_Atu
						//FROM [Cadastros de entidades];
						
						public int ID
						{
							get
							{
								return Parent.var("ID").value;
							}
						}
						public string Key
						{
							get
							{
								return this.CPFCNPJ_Key.ToString() + this.NomeRazaoSocial_Key.ToString();
							}
						}
						public string CPFCNPJ_Key
						{
							get
							{
								return Parent.var("CPF").value;
							}
						}
						public string RGIE
						{
							get
							{
								return Parent.var("RG").value;
							}
						}
						public string NomeRazaoSocial_Key
						{
							get
							{
								return Parent.var("nome").value;
							}
						}
						public string ApelidoNomeFantasia
						{
							get
							{
								return Parent.var("Conhecido como").value;
							}
						}
						public string Cargo
						{
							get
							{
								return Parent.var("cargo").value;
							}
						}
						public string Contato_Empresa
						{
							get
							{
								return Parent.var("Empresa").value;
							}
						}
						public string FormaTratamento
						{
							get
							{
								return Parent.var("Forma de Tratamento").value;
							}
						}
						public DateTime dtNascimentoInicioAtividades
						{
							get
							{
								DateTime d;
								try
								{
									d = System.Convert.ToDateTime(Parent.var("Data de Nascimento").value);
								}
								catch (Exception)
								{
									d = DateTime.Now;
								}
								return d;
							}
						}
						public string Atividade
						{
							get
							{
								return Parent.var("Atividade").value;
							}
						}
						public string Logradouro
						{
							get
							{
								return Parent.var("Endereço").value;
							}
						}
						public string Complemento
						{
							get
							{
								return Parent.var("Complemento").value;
							}
						}
						public string Bairro
						{
							get
							{
								return Parent.var("Bairro").value;
							}
						}
						public string Municipio
						{
							get
							{
								return Parent.var("Cidade").value;
							}
						}
						public string UF
						{
							get
							{
								return Parent.var("UF").value;
							}
						}
						public string Cep
						{
							get
							{
								return Parent.var("CEP").value;
							}
						}
						public string Comentario
						{
							get
							{
								return Parent.var("Coment").value;
							}
						}
						public string TextoRespeito
						{
							get
							{
								return Parent.var("Mais").value;
							}
						}
						public string Classificacao
						{
							get
							{
								return Parent.var("Classificação").value;
							}
						}
						public string SubClassificacao
						{
							get
							{
								return Parent.var("Sub-Classificação").value;
							}
						}
						public string P1
						{
							get
							{
								return Parent.var("P1").value;
							}
						}
						public string P2
						{
							get
							{
								return Parent.var("P2").value;
							}
						}
						public string P3
						{
							get
							{
								return Parent.var("P3").value;
							}
						}
						public string P4
						{
							get
							{
								return Parent.var("P4").value;
							}
						}
						public string P5
						{
							get
							{
								return Parent.var("P5").value;
							}
						}
						public string P6
						{
							get
							{
								return Parent.var("P6").value;
							}
						}
						public string N1
						{
							get
							{
								return Parent.var("N1").value;
							}
						}
						public string N2
						{
							get
							{
								return Parent.var("N2").value;
							}
						}
						public string N3
						{
							get
							{
								return Parent.var("N3").value;
							}
						}
						public string N4
						{
							get
							{
								return Parent.var("N4").value;
							}
						}
						public string N5
						{
							get
							{
								return Parent.var("N5").value;
							}
						}
						public string N6
						{
							get
							{
								return Parent.var("N6").value;
							}
						}
						public string Fone1
						{
							get
							{
								return Parent.var("Fone1").value;
							}
						}
						public string Fone2
						{
							get
							{
								return Parent.var("Fone2").value;
							}
						}
						public string Fone3
						{
							get
							{
								return Parent.var("Fone3").value;
							}
						}
						public string Fone4
						{
							get
							{
								return Parent.var("Fone4").value;
							}
						}
						public string Fornecedor
						{
							get
							{
								return Parent.var("Fornecedor").value;
							}
						}
						public string Cliente
						{
							get
							{
								return Parent.var("Cliente").value;
							}
						}
						public string Outros
						{
							get
							{
								return Parent.var("Outros").value;
							}
						}
						public bool PessoaFisica
						{
							get
							{
								bool pF;
								if (Parent.var("Pessoa Juridica").value == false)
								{
									pF = true;
								}
								return pF;
							}
						}
						public string PrazoCarencia
						{
							get
							{
								return Parent.var("Prazo de carencia em dias").value;
							}
						}
						public string MultaAtraso
						{
							get
							{
								return Parent.var("Multa por atraso").value;
							}
						}
						public string JuroMensal
						{
							get
							{
								return Parent.var("Juro mensal sobre inadimplencia").value;
							}
						}
						public DateTime dtCriacao
						{
							get
							{
								//CONVERT(DATETIME, '12/08/1973',102)
								return (System.Convert.ToDateTime(Parent.var("Data de Criação").value)).ToString(self.Settings.sintaxeData);
							}
						}
						public DateTime dtAlteracao
						{
							get
							{
								//parent.var("dtAlteracao").Value = CDate(Now).ToString(self.Settings.sintaxeData)
								return (System.Convert.ToDateTime(Parent.var("ultima alteração").value)).ToString(self.Settings.sintaxeData);
							}
						}
						public byte[] Foto
						{
							get
							{
								return Parent.var("foto").value;
							}
						}
						#endregion
						
					}
				}
				
			}
			
		}
	}
}
