using System.Data;
using System.Collections;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System;


namespace NBdbm
{
	public class NBexception : System.Exception
	{
		
		
		public NBexception()
		{
		}
		public NBexception(string message) : base(message)
		{
		}
		public NBexception(string message, System.Exception innerException) : base(message, innerException)
		{
		}
	}
	
	public class EVTexception : Exception
	{
		
		public EVTexception()
		{
		}
		
		public EVTexception(string message, string source, System.Exception exception) : base(message, exception)
		{
			this.Source = source;
		}
		
		public EVTexception(string source, Exception exception) : this("", source, exception)
		{
		}
		
		public EVTexception(string message, string source) : base(message)
		{
			this.Source = source;
		}
		public EVTexception(Exception exception) : this(exception.Source, exception)
		{
		}
		public override string Message
		{
			get
			{
				System.Text.StringBuilder msg = new System.Text.StringBuilder();
				msg.Append("*** " + base.Source + " ***\\r");
				msg.Append(base.Message.Replace("\r\n", "\\r") + "\\r\\r");
				msg.Append(this.InnerMessage(this.InnerException));
				msg.Append("*** Qualquer dúvida entre em contato com o suporte técnico ***");
				return msg.ToString().Replace("\'", "#");
			}
		}
		protected string InnerMessage(Exception pException)
		{
			System.Text.StringBuilder msg = new System.Text.StringBuilder();
			if (pException != null)
			{
				msg.Append("*** " + pException.Source + " ***\\r");
				msg.Append(pException.Message.Replace("\r\n", "\\r") + "\\r\\r");
				msg.Append(InnerMessage(pException.InnerException));
			}
			return msg.ToString();
		}
	}
	
	public class COBR_Exception : EVTexception
	{
		
		
		private string aClientID;
		//Novo construtor informando o ClientID, que serve para definir em
		//Qual campo ocorreu a exception.
		
		public COBR_Exception()
		{
		}
		
		public COBR_Exception(string message, string clientID, string source) : base(message, source)
		{
			this.aClientID = clientID;
		}
		
		public COBR_Exception(string message, string source, System.Exception exception) : base(message, source, exception)
		{
		}
		
		public COBR_Exception(string source, Exception exception) : base(source, exception)
		{
		}
		
		public COBR_Exception(string message, string source) : base(message, source)
		{
		}
		
		public COBR_Exception(Exception exception) : base(exception)
		{
		}
		
		public string ClientID
		{
			set
			{
				this.aClientID = value;
			}
			get
			{
				return this.aClientID;
			}
		}
	}
	
	public class testaClasseException
	{
		
		
		public tipos.Retorno doIt()
		{
			try
			{
				tipos.Retorno retorno;
				
				retorno = teste1();
				return retorno;
fimdoIt:
				return null;
			}
			catch
			{
				goto errdoit;
			}
			
errdoit:
			System.Windows.Forms.MessageBox.Show(Information.Err().Number + " - " + Information.Err().Description + " - " + Information.Err().Source);
		}
		
		private tipos.Retorno teste1()
		{
			tipos.Retorno retorno = new tipos.Retorno();
			teste ob = new teste();
			try
			{
				retorno = ob.meuErro();
				if (retorno.Sucesso == false)
				{
					throw (new NBdbm.NBexception("Duplicou a ferração", retorno.Exception));
				}
			}
			catch (Exception ex)
			{
				
				NBdbm.NBexception rEX = new NBdbm.NBexception("triplicou a ferração", ex);
				retorno.Exception = rEX;
				
			}
			
			return retorno;
		}
		
		private class teste
		{
			
			NBdbm.NBexception o;
			
			public tipos.Retorno meuErro()
			{
				tipos.Retorno retorno = new tipos.Retorno();
				try
				{
					throw (new NBdbm.NBexception("ferrou o barraco"));
					//        ob.BeginTransaction(IsolationLevel.Chaos)
				}
				catch (NBdbm.NBexception ex)
				{
					
					Interaction.Beep();
					retorno.Exception = ex;
				}
				
				return retorno;
			}
			
		}
		
	}
}
