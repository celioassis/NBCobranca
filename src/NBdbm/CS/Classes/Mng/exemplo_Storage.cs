using System.Data;
using System.Collections;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System;

//Public Interface IStorage
//    '    ReadOnly Property Alteracoes() As Alteracoes
//    '    ReadOnly Property Parent() As Arvore
//    Overloads Function HashMe(ByRef No As No) As No
//    Overloads Function HashMe(ByRef Nos As Nos) As Nos
//    Overloads Function FillMe(ByRef No As No) As No
//    Overloads Function FillMe(ByRef Nos As Nos) As Nos
//    Function FillMeRoots(ByRef Nos As Nos)
//    Sub SalvarArvore()
//    Sub Dispose()
//End Interface


//Imports System
//Imports System.Data
//Imports System.Data.SqlClient
//Imports Microsoft.VisualBasic

//Public Class StorageSQLServer
//    Implements nbcoretree.IImplementacao.IStorage

//    Friend mConn As SqlConnection
//    Friend mParent As Arvore
//    Friend mAlteracoes As Alteracoes

//    Public Sub New(ByVal Parent As Arvore)
//        mConn = New SqlConnection("Data Source=localhost;Integrated Security=SSPI;Initial Catalog=NEOBRIDGE")
//        'mConn = New SqlConnection("DSN=dsnPostgreSQL;UID=marcos;PWD=tonetto;")
//        mConn.Open()
//        mParent = Parent
//        mAlteracoes = New Alteracoes(Me)
//    End Sub

//    Public Sub Dispose() Implements nbcoretree.IImplementacao.IStorage.Dispose
//        mConn.Close()
//        mConn.Dispose()
//        mConn = Nothing
//    End Sub

//    Public Overloads Function FillMe(ByRef No As No) As No Implements nbcoretree.IImplementacao.IStorage.FillMe
//        Dim cmm As SqlCommand
//        Dim dr As SqlDataReader
//        Dim SQL As String
//        Dim IdRoot As Long
//        Dim newNo As No
//        Dim Filhos As Nos

//        SQL = "SELECT * FROM CTRL_Nos WHERE Pai = " & Trim(No.Id)

//        cmm = New SqlCommand(SQL, Me.mConn)
//        dr = cmm.ExecuteReader()

//        Filhos = No.mFilhos

//        Do While dr.Read()
//            newNo = New No(Me)
//            newNo.Nome = dr.Item("Nome")
//            newNo.Id = dr.Item("IdNo")
//            newNo.HerdaDoPai = IIf(dr.Item("HerdaDoPai") = 1, True, False)
//            newNo.Perene = IIf(dr.Item("Perene") = 1, True, False)
//            newNo.PermiteItens = IIf(dr.Item("PermiteItens") = 1, True, False)
//            newNo.PermiteNos = IIf(dr.Item("PermiteNos") = 1, True, False)
//            newNo.Path = dr.Item("Path")
//            newNo.XmPath = dr.Item("XmPath")
//            newNo.Status.NoBanco = nbcoretree.IImplementacao.IStatus.eTristate.Verdadeiro
//            Filhos.Add(newNo)
//        Loop
//        dr.Close()
//        cmm.Dispose()

//    End Function

//    Public Overloads Function FillMe(ByRef Nos As Nos) As Nos Implements nbcoretree.IImplementacao.IStorage.FillMe
//        'implementar na arvore binária
//    End Function

//    Public Overloads Function HashMe(ByRef Nos As Nos) As Nos Implements nbcoretree.IImplementacao.IStorage.HashMe
//        'implementar na arvore binária
//    End Function

//    Public Overloads Function HashMe(ByRef No As No) As No Implements nbcoretree.IImplementacao.IStorage.HashMe
//        'implementar na arvore binária
//    End Function

//    Public ReadOnly Property Parent() As Arvore Implements nbcoretree.IImplementacao.IStorage.Parent
//        Get
//            Return mParent
//        End Get
//    End Property

//    Public Sub SalvarArvore() Implements nbcoretree.IImplementacao.IStorage.SalvarArvore
//        'implementar
//    End Sub

//    Public Function FillMeRoots(ByRef Nos As Nos) As Object Implements nbcoretree.IImplementacao.IStorage.FillMeRoots
//        Dim cmm As SqlCommand
//        Dim dr As SqlDataReader
//        Dim SQL As String
//        Dim IdRoot As Long
//        Dim newNo As No

//        SQL = "SELECT * FROM CTRL_Nos WHERE Indice = 0"

//        cmm = New SqlCommand(SQL, Me.mConn)
//        dr = cmm.ExecuteReader()
//        dr.Read()
//        IdRoot = dr.Item("IdNo")

//        dr.Close()
//        cmm.Dispose()

//        SQL = "SELECT * FROM CTRL_Nos WHERE Pai = " & Trim(IdRoot)

//        cmm = New SqlCommand(SQL, Me.mConn)
//        dr = cmm.ExecuteReader()

//        Do While dr.Read()
//            newNo = New No(Me)
//            newNo.Nome = dr.Item("Nome")
//            newNo.Id = dr.Item("IdNo")
//            newNo.HerdaDoPai = IIf(dr.Item("herdadopai") = 1, True, False)
//            newNo.Perene = IIf(dr.Item("Perene") = 1, True, False)
//            newNo.PermiteItens = IIf(dr.Item("PermiteItens") = 1, True, False)
//            newNo.PermiteNos = IIf(dr.Item("PermiteNos") = 1, True, False)
//            newNo.Path = dr.Item("Path")
//            newNo.XmPath = dr.Item("XmPath")
//            newNo.Status.NoBanco = nbcoretree.IImplementacao.IStatus.eTristate.Verdadeiro
//            Nos.Add(newNo)
//        Loop
//        dr.Close()
//        cmm.Dispose()
//    End Function

//    Public ReadOnly Property Alteracoes() As Alteracoes Implements nbcoretree.IImplementacao.IStorage.Alteracoes
//        Get
//            Return mAlteracoes
//        End Get
//    End Property

//End Class


//Imports System
//Imports System.Data
//Imports Microsoft.Data.Odbc
//Imports Microsoft.VisualBasic

//Public Class StoragePostgreSQL
//    Implements nbcoretree.IImplementacao.IStorage

//    Friend mConn As OdbcConnection
//    Friend mParent As Arvore
//    Friend mAlteracoes As Alteracoes

//    Public Sub New(ByVal Parent As Arvore)
//        Dim cnString As String
//        'cnString = "DSN=dsnPostgreSQL;UID=marcos;PWD=tonetto;"
//        cnString = "DSN=PostgreSQL30;UID=marcos;PWD=tonetto;"

//        mConn = New OdbcConnection(cnString)
//        mConn.Open()
//        mParent = Parent
//        mAlteracoes = New Alteracoes(Me)
//    End Sub

//    Public Sub Dispose() Implements nbcoretree.IImplementacao.IStorage.Dispose
//        mConn.Close()
//        mConn.Dispose()
//        mConn = Nothing
//    End Sub

//    Public Overloads Function FillMe(ByRef No As No) As No Implements nbcoretree.IImplementacao.IStorage.FillMe
//        Dim cmm As OdbcCommand
//        Dim dr As OdbcDataReader
//        Dim SQL As String
//        Dim IdRoot As Long
//        Dim newNo As No
//        Dim Filhos As Nos

//        SQL = "SELECT * FROM CTRL_Nos WHERE Pai = " & Trim(No.Id)

//        cmm = New OdbcCommand(SQL, Me.mConn)
//        dr = cmm.ExecuteReader


//        Filhos = No.mFilhos

//        Do While dr.Read()
//            newNo = New No(Me)
//            newNo.Nome = dr.Item("Nome")
//            newNo.Id = dr.Item("IdNo")
//            newNo.HerdaDoPai = IIf(dr.Item("HerdaDoPai") = "T", True, False)
//            newNo.Perene = IIf(dr.Item("Perene") = "T", True, False)
//            newNo.PermiteItens = IIf(dr.Item("PermiteItens") = "T", True, False)
//            newNo.PermiteNos = IIf(dr.Item("PermiteNos") = "T", True, False)
//            newNo.Path = dr.Item("Path")
//            newNo.XmPath = dr.Item("XmPath")
//            newNo.Status.NoBanco = nbcoretree.IImplementacao.IStatus.eTristate.Verdadeiro
//            Filhos.Add(newNo)
//        Loop
//        dr.Close()
//        cmm.Dispose()

//    End Function

//    Public Overloads Function FillMe(ByRef Nos As Nos) As Nos Implements nbcoretree.IImplementacao.IStorage.FillMe
//        'implementar na arvore binária
//    End Function

//    Public Overloads Function HashMe(ByRef Nos As Nos) As Nos Implements nbcoretree.IImplementacao.IStorage.HashMe
//        'implementar na arvore binária
//    End Function

//    Public Overloads Function HashMe(ByRef No As No) As No Implements nbcoretree.IImplementacao.IStorage.HashMe
//        'implementar na arvore binária
//    End Function

//    Public ReadOnly Property Parent() As Arvore Implements nbcoretree.IImplementacao.IStorage.Parent
//        Get
//            Return mParent
//        End Get
//    End Property

//    Public Sub SalvarArvore() Implements nbcoretree.IImplementacao.IStorage.SalvarArvore
//        'implementar
//    End Sub

//    Public Function FillMeRoots(ByRef Nos As Nos) As Object Implements nbcoretree.IImplementacao.IStorage.FillMeRoots
//        Dim cmm As OdbcCommand
//        Dim dr As OdbcDataReader
//        Dim SQL As String
//        Dim IdRoot As Long
//        Dim newNo As No

//        SQL = "SELECT * FROM CTRL_Nos WHERE Indice = 0"

//        cmm = New OdbcCommand(SQL, Me.mConn)
//        dr = cmm.ExecuteReader()
//        dr.Read()
//        IdRoot = dr.Item("IdNo")

//        dr.Close()
//        cmm.Dispose()

//        SQL = "SELECT * FROM CTRL_Nos WHERE Pai = " & Trim(IdRoot)

//        cmm = New OdbcCommand(SQL, Me.mConn)
//        dr = cmm.ExecuteReader()

//        Do While dr.Read()
//            newNo = New No(Me)
//            newNo.Nome = dr.Item("Nome")
//            newNo.Id = dr.Item("IdNo")
//            newNo.HerdaDoPai = IIf(dr.Item("herdadopai") = "T", True, False)
//            newNo.Perene = IIf(dr.Item("Perene") = "T", True, False)
//            newNo.PermiteItens = IIf(dr.Item("PermiteItens") = "T", True, False)
//            newNo.PermiteNos = IIf(dr.Item("PermiteNos") = "T", True, False)
//            newNo.Path = dr.Item("Path")
//            newNo.XmPath = dr.Item("XmPath")
//            newNo.Status.NoBanco = nbcoretree.IImplementacao.IStatus.eTristate.Verdadeiro
//            Nos.Add(newNo)
//        Loop
//        dr.Close()
//        cmm.Dispose()
//    End Function

//    Public ReadOnly Property Alteracoes() As Alteracoes Implements nbcoretree.IImplementacao.IStorage.Alteracoes
//        Get
//            Return mAlteracoes
//        End Get
//    End Property

//End Class

//Public Class Storage
//    Inherits StoragePostgreSQL
//    'Inherits StorageSQLServer

//    Public Sub New(ByVal Parent As Arvore)
//        MyBase.New(Parent)
//    End Sub

//End Class
