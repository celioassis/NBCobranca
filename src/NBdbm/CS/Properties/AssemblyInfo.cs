using System.Data;
using System.Collections;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System;
using System.Reflection;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.

// Review the values of the assembly attributes


[assembly:AssemblyTitle("NBDBM: Manutenção de Banco de Dados")]
[assembly:AssemblyDescription("Administrador do Banco de Dados Neobridge")]
[assembly:AssemblyCompany("Neobridge Sistemas Ltda.")]
[assembly:AssemblyProduct("Administrador do Banco de Dados Neobridge")]
[assembly:AssemblyCopyright("Copyright © 2004 Neobridge Systemas Ltda.  All rights reserved.")]
[assembly:AssemblyTrademark("Neobridge")]
[assembly:CLSCompliant(true)]

//The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly:Guid("61A00D64-8C24-4967-BEA5-F8906642248C")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers
// by using the '*' as shown below:

[assembly:AssemblyVersion("1.0.0001.*")]

namespace NBdbm
{
	#region  Helper Class to Get Information for the About form.
	// This class uses the System.Reflection.Assembly class to
	// access assembly meta-data
	// This class is not a normal feature of AssemblyInfo.vb
	public class AssemblyInfo
	{
		
		// Used by Helper Functions to access information from Assembly Attributes
		private Type myType;
		
		public AssemblyInfo()
		{
			myType = typeof(NBdbm.forms.frmConfig);
		}
		
		public string AsmName
		{
			get
			{
				//Return myType.Assembly.GetName.Name.ToString()
				return System.IO.Path.GetFileNameWithoutExtension(myType.Assembly.Location);
			}
		}
		public string AsmFQName
		{
			get
			{
				//Return myType.Assembly.GetName.FullName.ToString()
				//Return myType.Assembly.Location
				return myType.Assembly.CodeBase.Substring(8, myType.Assembly.CodeBase.Length - 8);
			}
		}
		public string Config_Arquivo
		{
			get
			{
				return this.AsmFQName + ".config";
			}
		}
		public string BaseDirectory
		{
			get
			{
				//Dim txt As String
				//txt = myType.Assembly.CodeBase
				//txt = myType.Assembly.Location
				//txt = myType.AssemblyQualifiedName()
				//txt = myType.Assembly.GetName.FullName.ToString
				//txt = myType.Assembly.GetName.Name.ToString
				//txt = myType.Assembly.Location.PadLeft(1, "\")
				//txt = System.IO.Path.GetDirectoryName(myType.Assembly.Location)
				//Return txt
				return System.IO.Path.GetDirectoryName(myType.Assembly.Location);
			}
		}
		public string Copyright
		{
			get
			{
				Type at = typeof(AssemblyCopyrightAttribute);
				object[] r = myType.Assembly.GetCustomAttributes(at, false);
				AssemblyCopyrightAttribute ct = (AssemblyCopyrightAttribute) (r[0]);
				return ct.Copyright;
			}
		}
		public string Company
		{
			get
			{
				Type at = typeof(AssemblyCompanyAttribute);
				object[] r = myType.Assembly.GetCustomAttributes(at, false);
				AssemblyCompanyAttribute ct = (AssemblyCompanyAttribute) (r[0]);
				return ct.Company;
			}
		}
		public string Description
		{
			get
			{
				Type at = typeof(AssemblyDescriptionAttribute);
				object[] r = myType.Assembly.GetCustomAttributes(at, false);
				AssemblyDescriptionAttribute da = (AssemblyDescriptionAttribute) (r[0]);
				return da.Description;
			}
		}
		public string Product
		{
			get
			{
				Type at = typeof(AssemblyProductAttribute);
				object[] r = myType.Assembly.GetCustomAttributes(at, false);
				AssemblyProductAttribute pt = (AssemblyProductAttribute) (r[0]);
				return pt.Product;
			}
		}
		public string Title
		{
			get
			{
				Type at = typeof(AssemblyTitleAttribute);
				object[] r = myType.Assembly.GetCustomAttributes(at, false);
				AssemblyTitleAttribute ta = (AssemblyTitleAttribute) (r[0]);
				return ta.Title;
			}
		}
		public Version Version
		{
			get
			{
				return myType.Assembly.GetName().Version;
			}
		}
	}
	
	#endregion
}
