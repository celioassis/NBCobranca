using System.Data;
using System.Collections;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System;


namespace NBdbm
{
	namespace DotNetDataProviderTemplate
	{
		
		public class TemplateTransaction : IDbTransaction
		{
			
			
			public IsolationLevel IsolationLevel
			{
				//*
				//* Should return the current transaction isolation
				//* level. For the template, assume the default
				//* which is ReadCommitted.
				//*
				get
				{
					return IsolationLevel.ReadCommitted;
				}
			}
			
			public void Commit()
			{
				//*
				//* Implement Commit here. Although the template does
				//* not provide an implementation, it should never be
				//* a no-op because data corruption could result.
				//*
			}
			
			public void Rollback()
			{
				//*
				//* Implement Rollback here. Although the template does
				//* not provide an implementation, it should never be
				//* a no-op because data corruption could result.
				//*
			}
			
			public IDbConnection Connection
			{
				//
				// Set and retrieve the connection for the current transaction.
				//
				
				get
				{
					return null;
				}
			}
			
			public void Dispose()
			{
				this.Dispose(true);
				System.GC.SuppressFinalize(this);
			}
			
			private void Dispose(bool disposing)
			{
				if (disposing)
				{
					if (this.Connection != null)
					{
						// implicitly rollback if transaction still valid
						this.Rollback();
					}
				}
			}
			
		}
	}
	
}
