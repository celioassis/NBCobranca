using System.Data;
using System.Collections;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System;



namespace NBdbm
{
	internal class Tarefa
	{
		
		
		private int vIndex;
		private string vStatus;
		private byte vProgress;
		private string vDescricao;
		private string vBuffer;
		private object vObjectStatus; //Deve ser compatível com o Label ou caixa de texto
		private object vObjectProgress; //Deve ser compatível com o Painel ou progressBar
		
		public int Index
		{
			get
			{
				return vIndex;
			}
			set
			{
				vIndex = value;
			}
		}
		
		public string Status
		{
			get
			{
				return vStatus;
			}
			set
			{
				vBuffer = vBuffer + "\r\n" + vStatus;
				vStatus = value;
				if (vObjectStatus != null)
				{
					vObjectStatus.text = vStatus.Trim();
					vObjectStatus.Refresh();
				}
			}
		}
		
		public byte progress
		{
			get
			{
				return vProgress;
			}
			set
			{
				vProgress = value;
				if (vObjectProgress != null)
				{
					if (vProgress > 100)
					{
						vProgress = 100;
					}
					vObjectProgress.FloodPercent = vProgress;
					vObjectProgress.Caption = Strings.Trim(vProgress + " %");
					vObjectProgress.Refresh();
				}
			}
		}
		
		public string Descricao
		{
			get
			{
				return vDescricao;
			}
			set
			{
				vDescricao = value;
			}
		}
		
		public object Buffer(string vNewValue)
		{
			object returnValue;
			returnValue = vBuffer;
			vBuffer = "";
			return returnValue;
		}
		
		public object objectProgress
		{
			get
			{
				return vObjectProgress;
			}
			set
			{
				vObjectProgress = value;
			}
		}
		
		public object objectStatus
		{
			get
			{
				return vObjectStatus;
			}
			set
			{
				vObjectStatus = value;
			}
		}
		
	}
	
	internal class Tarefas : IDisposable, IEnumerable
	{
		
		
		
		//local variable to hold collection
		private Collection CollDesc;
		private Collection CollIndex;
		//
		public Tarefas()
		{
			CollDesc = new Collection();
			CollIndex = new Collection();
		}
		
		public void Dispose()
		{
			CollDesc = null;
			CollIndex = null;
		}
		
		~Tarefas()
		{
			base.Finalize();
			this.Dispose();
		}
		
		public Tarefa Add(string InicialStatus, double InicialProgress, string Descricao)
		{
			this.Add(InicialStatus, InicialProgress, Descricao, System.String.Empty, System.String.Empty);
			return null;
		}
		
		public Tarefa Add(string InicialStatus, double InicialProgress, string Descricao, object objProgress, object objStatus)
		{
			Tarefa returnValue;
			
			//On Error GoTo errAdd
			//create a new object
			
			Tarefa objNewMember;
			objNewMember = new Tarefa();
			
			//set the properties passed into the method
			if (objProgress != null)
			{
				objNewMember.objectProgress = objProgress;
			}
			
			if (objStatus != null)
			{
				objNewMember.objectStatus = objStatus;
			}
			
			objNewMember.Index = CollDesc.Count + 1;
			objNewMember.Status = InicialStatus;
			objNewMember.progress = InicialProgress;
			objNewMember.Descricao = Descricao;
			
			//set the properties passed into the method
			//    If Len(NomeCampo) = 0 Then
			//        Collection.Add objNewMember
			//    Else
			CollDesc.Add(objNewMember, Descricao, null, null);
			CollIndex.Add(objNewMember, "k=" + Strings.Trim(objNewMember.Index), null, null);
			
			//    End If
			
			//return the object created
			returnValue = objNewMember;
			objNewMember = null;
			//fimAdd:
			//    Exit Function
			//    Resume
			//errAdd:
			//    If Err.Number = 457 Then
			//      Resume fimAdd
			//    End If
			return returnValue;
		}
		
		public long Count
		{
			get
			{
				//used when retrieving the number of elements in the
				//collection. Syntax: Debug.Print x.Count
				return CollDesc.Count;
			}
		}
		
		
		public Tarefa ItemDescricao(object vntIndexKey)
		{
			return CollDesc[vntIndexKey];
			//On Error GoTo errItemDescricao
			//ItemDescricao = CollDesc(vntIndexKey)
			//fimItemDescricao:
			//      Exit Property
			//errItemDescricao:
			//      If err.Number = 5 Then
			//        Dim Vazio As New Tarefa
			//        ItemDescricao = Vazio
			//      End If
		}
		
		//used when referencing an element in the collection
		//vntIndexKey contains either the Index or Key to the collection,
		//this is why it is declared as a Variant
		//Syntax: Set foo = x.Item(xyz) or Set foo = x.Item(5)
		public Tarefa ItemIndex(object vntIndexKey)
		{
			//ItemIndex = CollIndex("K=" & Trim(vntIndexKey))
			return CollIndex["K=" + Strings.Trim(vntIndexKey)];
		}
		
		public System.Collections.IEnumerator GetEnumerator()
		{
			return CollIndex.GetEnumerator();
		}
		
		public void Remove(object vntIndexKey)
		{
			//On Error GoTo fimRemove
			Tarefa vTarefa;
			object tmp;
			
			if (CollDesc.Count > 0)
			{
				
				if (Information.IsNumeric(vntIndexKey) == true)
				{
					vTarefa = CollIndex[vntIndexKey];
					tmp = vTarefa.Descricao;
					vTarefa = null;
					RemoveIndex(vntIndexKey);
					RemoveDesc(tmp);
				}
				else
				{
					vTarefa = CollDesc[vntIndexKey];
					tmp = vTarefa.Index;
					vTarefa = null;
					RemoveIndex(tmp);
					RemoveDesc(vntIndexKey);
				}
				
			}
			//fimRemove:
		}
		
		private void RemoveDesc(object vntIndexKey)
		{
			CollDesc.Remove(vntIndexKey);
		}
		
		private void RemoveIndex(object vntIndexKey)
		{
			CollIndex.Remove("K=" + vntIndexKey);
		}
		
	}
	
	public class TarefaExemplo
	{
		
		
		public void exemplo()
		{
			
			Tarefas Ts = new Tarefas();
			ob ob1 = new ob();
			ob ob2 = new ob();
			
			ob1.text = "11";
			ob2.text = "12";
			Ts.Add("Item1", 1, "Processo1", ob1, ob2);
			ob1.text = "21";
			ob2.text = "22";
			Ts.Add("Item2", 2, "Processo2", ob1, ob2);
			ob1.text = "31";
			ob2.text = "32";
			Ts.Add("Item3", 3, "Processo3", ob1, ob2);
			ob1.text = "41";
			ob2.text = "42";
			Ts.Add("Item4", 4, "Processo4", ob1, ob2);
			ob1.text = "51";
			ob2.text = "52";
			Ts.Add("Item5", 5, "Processo5", ob1, ob2);
			ob1.text = "61";
			ob2.text = "62";
			Ts.Add("Item6", 6, "Processo6", ob1, ob2);
			
			//Vamos testar for next
			
			string txt;
			foreach (Tarefa t in Ts)
			{
				
				txt = t.Status;
				txt = t.progress.ToString();
				txt = t.Descricao;
				txt = t.Index.ToString();
				txt = t.objectProgress.text;
				txt = t.objectStatus.text;
				
			}
			
			
		}
		
		private class ob
		{
			
			
			public string text;
			
			public void Refresh()
			{
				Interaction.Beep();
			}
			
			
		}
		
	}
}
