Option Explicit On 
' General Information about an assembly is controlled through the following 
' set of attributes. Change these attribute values to modify the information
' associated with an assembly.

' Review the values of the assembly attributes

Imports System.Reflection
Imports System.Runtime.InteropServices

<Assembly: AssemblyTitle("NBDBM: Manutenção de Banco de Dados")> 
<Assembly: AssemblyDescription("Administrador do Banco de Dados Neobridge")> 
<Assembly: AssemblyCompany("Neobridge Sistemas Ltda.")> 
<Assembly: AssemblyProduct("Administrador do Banco de Dados Neobridge")> 
<Assembly: AssemblyCopyright("Copyright © 2004 Neobridge Systemas Ltda.  All rights reserved.")> 
<Assembly: AssemblyTrademark("Neobridge")> 
<Assembly: CLSCompliant(True)> 

'The following GUID is for the ID of the typelib if this project is exposed to COM
<Assembly: Guid("61A00D64-8C24-4967-BEA5-F8906642248C")> 

' Version information for an assembly consists of the following four values:
'
'      Major Version
'      Minor Version 
'      Build Number
'      Revision
'
' You can specify all the values or you can default the Build and Revision Numbers 
' by using the '*' as shown below:

<Assembly: AssemblyVersion("1.0.0001.*")> 

#Region " Helper Class to Get Information for the About form. "
' This class uses the System.Reflection.Assembly class to
' access assembly meta-data
' This class is not a normal feature of AssemblyInfo.vb
Public Class AssemblyInfo
    ' Used by Helper Functions to access information from Assembly Attributes
    Private myType As Type

    Public Sub New()
        myType = GetType(NBdbm.forms.frmConfig)
    End Sub

    Public ReadOnly Property AsmName() As String
        Get
            'Return myType.Assembly.GetName.Name.ToString()
            Return System.IO.Path.GetFileNameWithoutExtension(myType.Assembly.Location)
        End Get
    End Property
    Public ReadOnly Property AsmFQName() As String
        Get
            'Return myType.Assembly.GetName.FullName.ToString()
      'Return myType.Assembly.Location
      Return myType.Assembly.CodeBase.Substring(8, Len(myType.Assembly.CodeBase) - 8)
        End Get
    End Property
    Public ReadOnly Property Config_Arquivo() As String
        Get
            Return Me.AsmFQName & ".config"
        End Get
    End Property
    Public ReadOnly Property BaseDirectory() As String
        Get
            'Dim txt As String
            'txt = myType.Assembly.CodeBase
            'txt = myType.Assembly.Location
            'txt = myType.AssemblyQualifiedName()
            'txt = myType.Assembly.GetName.FullName.ToString
            'txt = myType.Assembly.GetName.Name.ToString
            'txt = myType.Assembly.Location.PadLeft(1, "\")
            'txt = System.IO.Path.GetDirectoryName(myType.Assembly.Location)
            'Return txt
            Return System.IO.Path.GetDirectoryName(myType.Assembly.Location)
        End Get
    End Property
    Public ReadOnly Property Copyright() As String
        Get
            Dim at As Type = GetType(AssemblyCopyrightAttribute)
            Dim r() As Object = myType.Assembly.GetCustomAttributes(at, False)
            Dim ct As AssemblyCopyrightAttribute = CType(r(0), AssemblyCopyrightAttribute)
            Return ct.Copyright
        End Get
    End Property
    Public ReadOnly Property Company() As String
        Get
            Dim at As Type = GetType(AssemblyCompanyAttribute)
            Dim r() As Object = myType.Assembly.GetCustomAttributes(at, False)
            Dim ct As AssemblyCompanyAttribute = CType(r(0), AssemblyCompanyAttribute)
            Return ct.Company
        End Get
    End Property
    Public ReadOnly Property Description() As String
        Get
            Dim at As Type = GetType(AssemblyDescriptionAttribute)
            Dim r() As Object = myType.Assembly.GetCustomAttributes(at, False)
            Dim da As AssemblyDescriptionAttribute = CType(r(0), AssemblyDescriptionAttribute)
            Return da.Description
        End Get
    End Property
    Public ReadOnly Property Product() As String
        Get
            Dim at As Type = GetType(AssemblyProductAttribute)
            Dim r() As Object = myType.Assembly.GetCustomAttributes(at, False)
            Dim pt As AssemblyProductAttribute = CType(r(0), AssemblyProductAttribute)
            Return pt.Product
        End Get
    End Property
    Public ReadOnly Property Title() As String
        Get
            Dim at As Type = GetType(AssemblyTitleAttribute)
            Dim r() As Object = myType.Assembly.GetCustomAttributes(at, False)
            Dim ta As AssemblyTitleAttribute = CType(r(0), AssemblyTitleAttribute)
            Return ta.Title
        End Get
    End Property
    Public ReadOnly Property Version() As Version
        Get
            Return myType.Assembly.GetName.Version
        End Get
    End Property
End Class

#End Region