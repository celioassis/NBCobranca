using System.Data;
using System.Collections;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System;


namespace NBdbm
{
	namespace DotNetDataProviderTemplate
	{
		public class TemplateParameter : IDataParameter
		{
			
			
			DbType m_dbType = DbType.Object;
			ParameterDirection m_direction = ParameterDirection.Input;
			bool m_fNullable = false;
			string m_sParamName;
			string m_sSourceColumn;
			DataRowVersion m_sourceVersion = DataRowVersion.Current;
			object m_value;
			
			public TemplateParameter()
			{
			}
			
			public TemplateParameter(string parameterName, DbType type)
			{
				
				m_sParamName = parameterName;
				m_dbType = type;
			}
			
			public TemplateParameter(string parameterName, object value)
			{
				
				m_sParamName = parameterName;
				this.Value = value;
				// Setting the value also infers the type.
			}
			
			public TemplateParameter(string parameterName, DbType type, string sourceColumn)
			{
				
				m_sParamName = parameterName;
				m_dbType = DbType;
				m_sSourceColumn = sourceColumn;
			}
			
			public DbType DbType
			{
				get
				{
					return m_dbType;
				}
				set
				{
					m_dbType = value;
				}
			}
			
			public ParameterDirection Direction
			{
				get
				{
					return m_direction;
				}
				set
				{
					m_direction = value;
				}
			}
			
			public bool IsNullable
			{
				get
				{
					return m_fNullable;
				}
			}
			
			public string ParameterName
			{
				get
				{
					return m_sParamName;
				}
				set
				{
					m_sParamName = value;
				}
			}
			
			public string SourceColumn
			{
				get
				{
					return m_sSourceColumn;
				}
				set
				{
					m_sSourceColumn = value;
				}
			}
			
			public DataRowVersion SourceVersion
			{
				get
				{
					return m_sourceVersion;
				}
				set
				{
					m_sourceVersion = value;
				}
			}
			
			public object Value
			{
				get
				{
					return m_value;
				}
				set
				{
					m_value = value;
					m_dbType = _inferType(value);
				}
			}
			
			private DbType _inferType(object value)
			{
				if ((Type.GetTypeCode(value.GetType())) == TypeCode.Empty)
				{
					throw (new SystemException("Invalid data type"));
				}
				else if ((Type.GetTypeCode(value.GetType())) == TypeCode.Object)
				{
					return DbType.Object;
				}
				else if ((Type.GetTypeCode(value.GetType())) == TypeCode.DBNull)
				{
				}
				else if ((Type.GetTypeCode(value.GetType())) == TypeCode.Char)
				{
				}
				else if ((Type.GetTypeCode(value.GetType())) == @TypeCode.SByte)
				{
				}
				else if ((Type.GetTypeCode(value.GetType())) == TypeCode.UInt16)
				{
				}
				else if ((Type.GetTypeCode(value.GetType())) == TypeCode.UInt32)
				{
				}
				else if ((Type.GetTypeCode(value.GetType())) == TypeCode.UInt64)
				{
					// Throw a SystemException for unsupported data types.
					throw (new SystemException("Invalid data type"));
				}
				else if ((Type.GetTypeCode(value.GetType())) == TypeCode.Boolean)
				{
					return DbType.Boolean;
				}
				else if ((Type.GetTypeCode(value.GetType())) == TypeCode.Byte)
				{
					return DbType.Byte;
				}
				else if ((Type.GetTypeCode(value.GetType())) == TypeCode.Int16)
				{
					return DbType.Int16;
				}
				else if ((Type.GetTypeCode(value.GetType())) == TypeCode.Int32)
				{
					return DbType.Int32;
				}
				else if ((Type.GetTypeCode(value.GetType())) == TypeCode.Int64)
				{
					return DbType.Int64;
				}
				else if ((Type.GetTypeCode(value.GetType())) == TypeCode.Single)
				{
					return DbType.Single;
				}
				else if ((Type.GetTypeCode(value.GetType())) == TypeCode.Double)
				{
					return DbType.Double;
				}
				else if ((Type.GetTypeCode(value.GetType())) == TypeCode.Decimal)
				{
					return DbType.Decimal;
				}
				else if ((Type.GetTypeCode(value.GetType())) == TypeCode.DateTime)
				{
					return DbType.DateTime;
				}
				else if ((Type.GetTypeCode(value.GetType())) == TypeCode.String)
				{
					return DbType.String;
				}
				else
				{
					throw (new SystemException("Value is of unknown data type"));
				}
			}
		}
	}
	
}
