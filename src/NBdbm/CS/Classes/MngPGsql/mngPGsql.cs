using System.Data;
using System.Collections;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System;

namespace NBdbm
{
	namespace Manager
	{
		namespace PostGreSQL
		{
			public class mngPGsql
			{
				
				
			}
		}
	}
	
	
	
	//Classe antiga para exemplo
	//Imports System.Data
	
	//Namespace CamadaDados
	
	//    Public Class useADOnet
	//        Implements Interfaces.Manager
	//        Private pvt_Connection As IDbConnection
	//        Private pvt_Command As IDbCommand
	
	//        Friend Property connection() As IDbConnection
	//            Get
	//                connection = pvt_Connection
	//            End Get
	//            Set(ByVal newConnection As IDbConnection)
	//                pvt_Connection = newConnection
	//            End Set
	//        End Property
	
	//        Friend Property command() As IDbCommand
	//            Get
	//                command = pvt_Command
	//            End Get
	//            Set(ByVal newCommand As IDbCommand)
	//                pvt_Command = newCommand
	//            End Set
	//        End Property
	
	//        Public Function testcreateTable(ByVal commandText As String)
	
	//            Dim txt As String
	//            txt = "CREATE TABLE aaaTable " & _
	//                  "( " & _
	//                  "IDProduto int NOT NULL IDENTITY (1, 1), " & _
	//                  "IdOrigem int NULL, " & _
	//                  "Modalidade char(10) NULL, " & _
	//                  "Prod char(10) NULL, " & _
	//                  "Nome char(10) NULL " & _
	//                  ")  ON [PRIMARY] "
	
	//            commandText = txt
	
	//            'command.CommandText = "Create DataBase Edgar"
	//            'command.ExecuteScalar()
	
	//            command.CommandText = commandText
	//            command.ExecuteScalar()
	
	//            'MsgBox(connection.State)
	//            'txt = connection.ToString
	//            'txt = connection.DataSource.ToString
	//            txt = connection.Database.ToString
	
	//            'c.CreateCommand.CommandText = o.CommandText
	//            'c.CreateCommand.ExecuteScalar()
	
	//        End Function
	
	//        Public Function createTable(ByVal commandString As String) As Object Implements Interfaces.Manager.createTable
	
	//        End Function
	
	//        Public Function createView(ByVal commandString As String) As Object Implements Interfaces.Manager.createView
	
	//        End Function
	
	//        Public Function readDB(ByVal SQL As String) As Object Implements Interfaces.Manager.readDB
	
	//        End Function
	
	//        Public Function createField(ByVal commandString As String) As Object Implements NBdbm.Interfaces.Manager.createField
	
	//        End Function
	//    End Class
	//End Namespace
}
