using System.Data;
using System.Collections;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System;

namespace NBdbm
{
	namespace Manager
	{
		namespace CamadaDados
		{
			
			public class NBdbm
			{
				
				
				public tipos.Versao versao
				{
					get
					{
						string txt;
						tipos.Versao mVersao = new tipos.Versao();
						txt = System.Configuration.ConfigurationManager.AppSettings.Get("tipo");
						return mVersao;
					}
				}
			}
			
			public class scriptCTR
			{
				
				
				public tipos.Versao versao
				{
					get
					{
						tipos.Versao returnValue;
						returnValue = new tipos.returnValue();
						returnValue.major = 1;
						returnValue.minor = 0;
						returnValue.revision = 10;
						return returnValue;
					}
				}
				
				public tipos.Retorno checkCTR(tipos.Versao versao)
				{
					tipos.Retorno returnValue;
					
					returnValue = new tipos.Retorno();
					
					//"1.00.010"
					if (self.Settings.Versao.toString() < versao.toString())
					{
						//Atualizar
						Interaction.Beep();
					}
					else if (self.Settings.Versao.toString() > "1.00.010")
					{
						//Banco mais atualizado que eu:(NBdbm)
						Interaction.Beep();
					}
					else
					{
						//Banco ok!
						returnValue.Sucesso = true;
					}
					
					return returnValue;
				}
				
				private tipos.Retorno runScript()
				{
					
					string TXT;
					
					
					TXT = "create hgfghfhg f";
					return null;
				}
				
			}
		}
	}
}
