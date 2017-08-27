using System.Data;
using System.Collections;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System;

namespace NBdbm
{
	internal class self
	{
		
		
		//Private Shared myFunctions As NBdbm.NBFuncoes
		private static settings mySettings;
		private static ADM.admDB myAdmDB;
		private static Tarefas myTarefas;
		
		#region   Start End
		
		public void dispose()
		{
			if (mySettings != null)
			{
				mySettings.Dispose();
			}
			if (myAdmDB != null)
			{
				myAdmDB.Dispose();
			}
			if (myTarefas != null)
			{
				myTarefas.Dispose();
			}
			
			mySettings = null;
			myAdmDB = null;
			myTarefas = null;
		}
		
		#endregion
		
		internal static settings Settings
		{
			get
			{
				if (mySettings == null)
				{
					mySettings = new settings();
				}
				return mySettings;
			}
			//Set(ByVal Value As Settings)
			//  If Not mySettings Is Nothing Then
			//    mySettings.Dispose()
			//  End If
			//  mySettings = Nothing
			//End Set
		}
		
		//Esta função é Shared e pode ser chamada diretamente.
		//Friend Shared ReadOnly Property NBFuncoes() As NBFuncoes
		//  Get
		//    If myFunctions Is Nothing Then
		//      myFunctions = New NBFuncoes
		//    End If
		//    Return myFunctions
		//  End Get
		//End Property
		
		internal static ADM.admDB AdmDB
		{
			get
			{
				if (myAdmDB == null)
				{
					myAdmDB = new ADM.admDB();
				}
				return myAdmDB;
			}
			//Set(ByVal Value As ADM.admDB)
			//  myAdmDB = Nothing
			//End Set
		}
		
		internal static Tarefas Tarefa
		{
			get
			{
				if (myTarefas == null)
				{
					myTarefas = new Tarefas();
				}
				return myTarefas;
			}
		}
		
	}
	
}
