using System.Data;
using System.Collections;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System;
using System.Windows.Forms;


namespace NBdbm
{
	namespace Parsers
	{
		
		namespace Finan32
		{
			
			public class parser_Finan32
			{
				
				
				private NBdbm.ADM.admDB_OLEDB myCNN;
				private string DataBase;
				
				public NBdbm.tipos.Retorno Action(string DBQ, string SystemDB, string xmPath)
				{
					this.DataBase = DBQ;
					P_Fachadas.admDB_Finan32 C = new P_Fachadas.admDB_Finan32(DBQ, SystemDB);
					myCNN = C.cnn;
					
					this.script_run(xmPath);
					return null;
				}
				
				private Interfaces.iAdmDB CNN
				{
					get
					{
						return myCNN;
					}
				}
				
				private NBdbm.tipos.Retorno script_run(string xmPath)
				{
					NBdbm.tipos.Retorno resultado = new NBdbm.tipos.Retorno();
					P_Fachadas.Entidade entidade = new P_Fachadas.Entidade(CNN);
					string txt;
					string cReg;
					double I;
					
					cReg = entidade.DataSource.Count.ToString();
					//txt = entidade.getFields(12).Count & " campos."
					MessageBox.Show(cReg + " registros à importar.");
					do
					{
						entidade.getFields(I);
						txt = entidade.campos.Key;
						resultado = addSQLEntidade(xmPath, entidade);
						//If I / 50 = Int(I / 50) Then
						//  MsgBox(I + 1 & " registros," & vbCrLf & resultado.ToString)
						//End If
						I++;
					} while (!(I == cReg));
					
					resultado.Sucesso = true;
					resultado.Tag = "A importação da tabela:[Entidade] com " + cReg + "registros, do banco:" + this.DataBase + " foi completada com sucesso.";
					return resultado;
				}
				
				private NBdbm.tipos.Retorno addSQLEntidade(string xmPath, NBdbm.Parsers.Finan32.P_Fachadas.Entidade field)
				{
					NBdbm.tipos.Retorno resultado = new NBdbm.tipos.Retorno();
					NBdbm.Fachadas.CTR.CadastroEntidade oB = new NBdbm.Fachadas.CTR.CadastroEntidade();
					
					try
					{
						oB.xmPath_LinkEntNo = xmPath;
						oB.Entidade.PessoaFisica = field.campos.PessoaFisica;
						oB.Entidade.CPFCNPJ_key = field.campos.CPFCNPJ_Key;
						oB.Entidade.NomeRazaoSocial_key = field.campos.NomeRazaoSocial_Key;
						oB.Entidade.ApelidoNomeFantasia = field.campos.ApelidoNomeFantasia;
						oB.Entidade.RgIE = field.campos.RGIE;
						oB.Entidade.OrgaoEmissorIM = "SSP-" + field.campos.UF;
						oB.Entidade.dtNascimentoInicioAtividades = field.campos.dtNascimentoInicioAtividades;
						oB.Entidade.TextoRespeito = field.campos.TextoRespeito;
						
						oB.Endereco.Logradouro_key = field.campos.Logradouro;
						oB.Endereco.complemento = field.campos.Complemento;
						oB.Endereco.Comentario = field.campos.Comentario;
						oB.Endereco.Bairro = field.campos.Bairro;
						oB.Endereco.Municipio = field.campos.Municipio;
						oB.Endereco.UF = field.campos.UF;
						oB.Endereco.Contato = field.campos.Contato_Empresa;
						oB.Endereco.Principal = false;
						oB.colecaoEnderecos.Add(oB.Endereco.Key, ((object) oB.Endereco));
						
						if (Conversion.Val(NBdbm.NBFuncoes.soNumero(field.campos.Fone1).ToString()) > 0)
						{
							oB.Telefone.Contato = field.campos.Contato_Empresa;
							oB.Telefone.DDD_key = field.campos.Fone1.Substring(0, 4);
							oB.Telefone.Fone_key = field.campos.Fone1;
							oB.Telefone.Descricao = field.campos.FormaTratamento + "-" + field.campos.Cargo;
							try
							{
								oB.colecaoTelefones.Add(oB.Telefone.Key, ((object) oB.Telefone));
							}
							catch
							{
								
							}
						}
						
						if (Conversion.Val(NBdbm.NBFuncoes.soNumero(field.campos.Fone2).ToString()) > 0)
						{
							oB.Telefone.Contato = field.campos.Contato_Empresa;
							oB.Telefone.DDD_key = field.campos.Fone2.Substring(0, 4);
							oB.Telefone.Fone_key = field.campos.Fone2;
							oB.Telefone.Descricao = field.campos.FormaTratamento + "-" + field.campos.Cargo;
							try
							{
								oB.colecaoTelefones.Add(oB.Telefone.Key, ((object) oB.Telefone));
							}
							catch
							{
								
							}
						}
						
						if (Conversion.Val(NBdbm.NBFuncoes.soNumero(field.campos.Fone3).ToString()) > 0)
						{
							oB.Telefone.Contato = field.campos.Contato_Empresa;
							oB.Telefone.DDD_key = field.campos.Fone3.Substring(0, 4);
							oB.Telefone.Fone_key = field.campos.Fone3;
							oB.Telefone.Descricao = field.campos.FormaTratamento + "-" + field.campos.Cargo;
							try
							{
								oB.colecaoTelefones.Add(oB.Telefone.Key, ((object) oB.Telefone));
							}
							catch
							{
								
							}
						}
						
						if (Conversion.Val(NBdbm.NBFuncoes.soNumero(field.campos.Fone4).ToString()) > 0)
						{
							oB.Telefone.Contato = field.campos.Contato_Empresa;
							oB.Telefone.DDD_key = field.campos.Fone4.Substring(0, 4);
							oB.Telefone.Fone_key = field.campos.Fone4;
							oB.Telefone.Descricao = field.campos.FormaTratamento + "-" + field.campos.Cargo;
							try
							{
								oB.colecaoTelefones.Add(oB.Telefone.Key, ((object) oB.Telefone));
							}
							catch
							{
								
							}
						}
						
						if (field.campos.Comentario.IndexOf("@") + 1 > 1)
						{
							oB.eMail.eMail_key = field.campos.Comentario;
							oB.colecaoEmail.Add(oB.eMail.Key, ((object) oB.eMail));
						}
						
						if (field.campos.Comentario.IndexOf("www") + 1 > 1 || field.campos.Comentario.IndexOf("http") + 1 > 1)
						{
							oB.Url.Url_key = field.campos.Comentario;
							
							oB.colecaoUrl.Add(oB.Url.Key, ((object) oB.Url));
						}
						
						oB.Salvar(false);
					}
					catch (Exception)
					{
						Interaction.Beep();
					}
					
					return resultado;
				}
				
				private NBdbm.tipos.Retorno script_run_teste()
				{
					string txt = "";
					txt = CNN.sqlListaCampos("txt");
					MessageBox.Show("Olá mundo!");
					NBdbm.Fachadas.allClass db = new NBdbm.Fachadas.allClass("Usuarios", CNN);
					
					txt = db.getFields.Item(1).caption + db.getFields.Item(1).value;
					txt = db.getFields.Item(2).caption + db.getFields.Item(2).value;
					txt = db.getFields.Item(3).caption + db.getFields.Item(3).value;
					
					txt = "";
					return null;
				}
				
			}
			
		}
	}
	
}
