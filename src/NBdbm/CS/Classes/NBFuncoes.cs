using System.Data;
using System.Collections;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System;
using System.Windows.Forms;


namespace NBdbm
{
	internal class NBFuncoes
	{
		
		
		//Esta função retorna somente os número
		//de uma seqüência Alfanumérica
		internal static string soNumero(string string_)
		{
			
			double i;
			System.Text.StringBuilder str = new System.Text.StringBuilder();
			try
			{
				for (i = 1; i <= string_.Length; i++)
				{
					
					if (Information.IsNumeric(string_.Substring(i - 1, 1)) == true)
					{
						str.Append(string_.Substring(i - 1, 1));
					}
				}
				return str.ToString();
			}
			catch (Exception ex)
			{
				NBexception nbEx = new NBexception("Não foi possível retirar os Números da String", ex);
				nbEx.Source = "NBFuncoes.soNumero";
				throw (nbEx);
			}
		}
		
		internal static string formataData(DateTime date_, DateFormat formato)
		{
			return date_.ToString(formato);
		}
		
		internal static string validaCPFCNPJ(string cpfcnpj, bool pessoaFisica)
		{
			
			string str;
			
			str = soNumero(cpfcnpj).ToString();
			if (pessoaFisica && str.Length < 12)
			{
				str = Strings.Format(Conversion.Val(str), "00000000000");
			}
			else if (str.Length <= 15)
			{
				str = Strings.Format(Conversion.Val(str), "000000000000000");
			}
			else
			{
				NBexception mNBEx = new NBexception("CNPJ_CPF inválido");
				mNBEx.Source = "NBFuncoes.validaCPFCNPJ";
				throw (mNBEx);
			}
			return str;
		}
		
		internal static void SalvaArquivo(string arqName, string StrWrite)
		{
			System.IO.Stream mStream;
			
			mStream = System.IO.File.OpenWrite(arqName);
			
			System.IO.StreamWriter SrWriter = new System.IO.StreamWriter(mStream, System.Text.Encoding.Default);
			
			SrWriter.Write(StrWrite);
			
			SrWriter.Flush();
			SrWriter.Close();
			
		}
		
		private static bool f_CPFCGC(string CPFCNPJ, bool NoMsg)
		{
			bool returnValue;
			
			string CPF1 = "";
			string CPF2 = "";
			int Soma;
			int Digito;
			int I;
			int J;
			int ContIni;
			int ContFim;
			string Controle = "";
			string CGC1 = "";
			string CGC2 = "";
			//Dim TELAS As Form
			int Add;
			int VERIFICADOR;
			int H;
			int M;
			string CO = "";
			string mult = "";
			//TELAS = Screen.ActiveForm
			
			string Nl;
			
			if (CPFCNPJ.Length == 11 || CPFCNPJ.Length == 14)
			{
				CPFCNPJ = Strings.Format(soNumero(CPFCNPJ).ToString(), "00000000000");
				CPF1 = Strings.Left(CPFCNPJ, 9);
				CPF2 = Strings.Right(CPFCNPJ, 2);
				ContIni = 2;
				ContFim = 10;
				for (J = 1; J <= 2; J++)
				{
					Soma = 0;
					for (I = ContIni; I <= ContFim; I++)
					{
						Soma = Soma + (Conversion.Val(Strings.Mid(CPF1, I - J, 1)) * (ContFim + 1 + J - I));
					}
					
					if (J == 2)
					{
						Soma = Soma + (2 * Digito);
					}
					Digito = (Soma * 10) % 11;
					if (Digito == 10)
					{
						Digito = 0;
					}
					Controle = Controle + Strings.Trim(str[Digito]);
					//Valores limite para I para o cálculo do segundo dígito
					ContIni = 3;
					ContFim = 11;
				}
				
				Nl = '\r' + '\n'; //Nova Linha
				
				if (Controle != CPF2)
				{
					if (NoMsg == false)
					{
						Interaction.Beep();
						Interaction.MsgBox("CPF inválido", 48, "             Mensagem do Sistema             ");
					}
					returnValue = false;
					return returnValue;
				}
				
			}
			else if (14 <= CPFCNPJ.Length || CPFCNPJ.Length <= 19)
			{
				CPFCNPJ = Strings.Format(soNumero(CPFCNPJ).ToString(), "000000000000000");
				CGC1 = Strings.Left(CPFCNPJ, 13);
				CGC2 = Strings.Right(CPFCNPJ, 2);
				mult = "6543298765432";
				CO = "";
				for (M = 1; M <= 2; M++)
				{
					Add = 0;
					for (H = 1; H <= 13; H++)
					{
						Add = Add + (Conversion.Val(Strings.Mid(CGC1, H, 1)) * Conversion.Val(Strings.Mid(mult, H, 1)));
					}
					if (M == 2)
					{
						Add = Add + (2 * VERIFICADOR);
					}
					VERIFICADOR = (Add * 10) % 11;
					if (VERIFICADOR == 10)
					{
						VERIFICADOR = 0;
					}
					CO = CO + Strings.Trim(str[VERIFICADOR]);
					//Sequência de multplicadores para
					//o cálculo do segundo dígito
					mult = "76543298765432";
				}
				Nl = '\r' + '\n'; //Nova linha
				if (CO != CGC2)
				{
					if (NoMsg == false)
					{
						Interaction.Beep();
						Interaction.MsgBox("CNPJ inválido", 48, "             Mensagem do Sistema             ");
					}
					returnValue = false;
					return returnValue;
				}
				
				//txt1.Mask = "00\.000\.000\/0000\-00;;"
				//DoCmd CalcelEvent
				returnValue = true;
				return returnValue;
			}
			else
			{
				
				if (NoMsg == false)
				{
					Interaction.Beep();
					Interaction.MsgBox("Número de Dígitos inválido.", 48, null);
				}
				
				returnValue = false;
				return returnValue;
			}
			
			returnValue = true;
			
			return returnValue;
		}
		
		internal static string decripto(string string_)
		{
			//Descriptografa uma string contida na variável "string_"
			
			string Key = "";
			long cont;
			long cont2;
			string a = "";
			string b = "";
			string StrCript = "";
			
			Key = "d¡·fRDk£ÈTÍ ²ÂJ ¢¯WÞê¸&zªk(N§FaÙìß|n4¦O(=1æ«ñüFWMH¬#ûýbÐ66Å+c|¾/Ð[^áÇ:ÁõðÎ³§¦JË¥¥¨´éx@-&ÒÆ+ïL²¤¾9PÕðfîò\\»íjg l>4ÉØ~Ëh©n=;3£EîÓIùõ7ï5èäß¿Â?UÛ#{,UÖÜwà:pµ¼ç.KûÎapI<½SÍºY.×ói¹ñÑr}³!Òíçáøüú$A`ºX8þþú%5æ2)uqã2ÆòvC)S±_ }Ø­*~¯h\\9ÃÖÚys·¿xÀÌ×Õ/GÔvö<­Ñ_¶TâEe3®AKrZÜVâã¶RtM°ÉÛèCµÓu¨Úz¤Ý\'÷ÅeÝ¼Á½ëZôéÃHXÄól¹i*ë¬«oÏÈ8QôìÊ]NO0`ý^Ybä0]dq-%±Þ;å´¡j®êm>[ÀD,\'LàÔù»oÏysÌt@ÊGÇ{åQmPBÙög÷7$1ªVwB°©Ä¸c?!ø¢";
			
			for (cont = 1; cont <= string_.Length; cont++)
			{
				a = string_.Substring(cont - 1, 1);
				
				for (cont2 = 2; cont2 <= Key.Length; cont2 += 2)
				{
					if (Key.Substring(cont2 - 1, 1) == a)
					{
						b = Key.Substring(cont2 - 1 - 1, 1);
						break;
					}
					
				}
				
				StrCript = StrCript + b;
			}
			
			return StrCript;
			
		}
		
		internal static string cripto(string string_)
		{
			//Criptografa uma string contida na variável "string_"
			
			string Key = "";
			long cont;
			long cont2;
			string a = "";
			string b = "";
			string StrCript = "";
			
			Key = "d¡·fRDk£ÈTÍ ²ÂJ ¢¯WÞê¸&zªk(N§FaÙìß|n4¦O(=1æ«ñüFWMH¬#ûýbÐ66Å+c|¾/Ð[^áÇ:ÁõðÎ³§¦JË¥¥¨´éx@-&ÒÆ+ïL²¤¾9PÕðfîò\\»íjg l>4ÉØ~Ëh©n=;3£EîÓIùõ7ï5èäß¿Â?UÛ#{,UÖÜwà:pµ¼ç.KûÎapI<½SÍºY.×ói¹ñÑr}³!Òíçáøüú$A`ºX8þþú%5æ2)uqã2ÆòvC)S±_ }Ø­*~¯h\\9ÃÖÚys·¿xÀÌ×Õ/GÔvö<­Ñ_¶TâEe3®AKrZÜVâã¶RtM°ÉÛèCµÓu¨Úz¤Ý\'÷ÅeÝ¼Á½ëZôéÃHXÄól¹i*ë¬«oÏÈ8QôìÊ]NO0`ý^Ybä0]dq-%±Þ;å´¡j®êm>[ÀD,\'LàÔù»oÏysÌt@ÊGÇ{åQmPBÙög÷7$1ªVwB°©Ä¸c?!ø¢";
			
			for (cont = 1; cont <= string_.Length; cont++)
			{
				a = string_.Substring(cont - 1, 1);
				
				for (cont2 = 1; cont2 <= Key.Length; cont2 += 2)
				{
					if (Key.Substring(cont2 - 1, 1) == a)
					{
						b = Key.Substring(cont2 + 1 - 1, 1);
						break;
					}
					
				}
				
				StrCript = StrCript + b;
			}
			
			return StrCript;
			
		}
		
		
		
	}
	
	
}
