using System.Data;
using System.Collections;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System;

namespace NBdbm
{
	namespace tipos
	{
		
		public enum tiposConection
		{
			Default_ = 0,
			SQLSERVER = 1,
			OLEDB = 2,
			OUTDB = 3,
			SQLSVR_LUG = 4
		}
		
		public enum BoolYes
		{
			vbTrue = - 1, //Microsoft.VisualBasic.TriState = -1
			vbFalse = 0,
			vbYes = 6,
			vbNo = 7
		}
		
		public enum TipoEntidade
		{
			Funcionários = 1,
			Clientes = 2,
			Fornecedores = 3,
			Devedores = 4,
			Localidades = 5,
			Todas = 6
		}
		public class Versao : IDisposable
		{
			
			
			
			public int major;
			public int minor;
			public int revision;
			
			public override string ToString()
			{
				string returnValue;
				returnValue = Strings.Format(major, "00") + "." + Strings.Format(minor, "00") + "." + Strings.Format(revision, "0000");
				returnValue = returnValue.Trim();
				return returnValue;
			}
			
			public void Dispose()
			{
				major = null;
				minor = null;
				revision = null;
			}
		}
		
		public class Retorno : IDisposable
		{
			
			
			
			private bool vSucesso;
			private string vTag;
			private object vObjeto;
			private NBdbm.NBexception vException;
			
			public bool Sucesso
			{
				get
				{
					return vSucesso;
				}
				set
				{
					vSucesso = value;
				}
			}
			
			public string Tag
			{
				get
				{
					return vTag;
				}
				set
				{
					vTag = value;
				}
			}
			
			public object Objeto
			{
				get
				{
					return vObjeto;
				}
				set
				{
					vObjeto = value;
				}
			}
			
			public new string ToString
			{
				get
				{
					return vObjeto.ToString();
				}
			}
			
			public NBdbm.NBexception Exception
			{
				get
				{
					return vException;
				}
				set
				{
					vException = value;
				}
			}
			
			public string MensagemErro
			{
				get
				{
					return vException.Source + vException.Message + "**** Mensagem Original ****\\r\\r" + vException.InnerException.Message + "\\r\\r*** Entre em Contato com o Desenvolvedor do Sistema ***";
				}
			}
			public void Dispose()
			{
				vSucesso = null;
				vTag = null;
				vObjeto = null;
				vException = null;
			}
		}
		
	}
}
