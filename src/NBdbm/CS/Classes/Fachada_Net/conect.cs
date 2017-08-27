using System.Data;
using System.Collections;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System;



namespace NBdbm
{
	namespace DotNetDataProviderTemplate
	{
		
		public class TemplateConnection : IDbConnection
		{
			
			
			private ConnectionState m_state;
			private string m_sConnString;
			
			// Use the "SampleDb" class to simulate a database connection.
			private SampleDb m_sampleDb;
			
			// Always have a default constructor.
			public TemplateConnection()
			{
				this.InitClass();
			}
			
			// Have a constructor that takes a connection string.
			public TemplateConnection(string sConnString)
			{
				this.InitClass();
				this.ConnectionString = sConnString;
			}
			
			private void InitClass()
			{
				m_state = ConnectionState.Closed;
				
				//*
				//* Obtain a connection to the database. In this case,
				//* use the SampleDb class to simulate a connection to
				//* a real database.
				//*
				m_sampleDb = new SampleDb();
			}
			
			//****
			//* IMPLEMENT THE REQUIRED PROPERTIES.
			//****
			public string ConnectionString
			{
				get
				{
					// Always return exactly what the user set.
					// Security-sensitive information may be removed.
					return m_sConnString;
				}
				set
				{
					m_sConnString = value;
				}
			}
			
			public int ConnectionTimeout
			{
				get
				{
					// Returns the connection time-out value set in the connection
					// string. Zero indicates an indefinite time-out period.
					return 0;
				}
			}
			
			public string Database
			{
				get
				{
					// Returns an initial database as set in the connection string.
					// An empty string indicates not set - do not return a null reference.
					return "";
				}
			}
			
			public ConnectionState State
			{
				get
				{
					return m_state;
				}
			}
			
			//****
			//* IMPLEMENT THE REQUIRED METHODS.
			//****
			
			public IDbTransaction BeginTransaction()
			{
				throw (new NotSupportedException());
			}
			
			public IDbTransaction BeginTransaction(IsolationLevel level)
			{
				throw (new NotSupportedException());
			}
			
			public void ChangeDatabase(string dbName)
			{
				// Change the database setting on the back-end. Note that it is a method
				// and not a property because the operation requires an expensive
				// round trip.
			}
			
			public void Open()
			{
				//*
				//* Open the database connection and set the ConnectionState
				//* property. If the underlying connection to the server is
				//* expensive to obtain, the implementation should provide
				//* implicit pooling of that connection.
				//*
				//* If the provider also supports automatic enlistment in
				//* distributed transactions, it should enlist during Open().
				//*
				m_state = ConnectionState.Open;
			}
			
			public void Close()
			{
				//*
				//* Close the database connection and set the ConnectionState
				//* property. If the underlying connection to the server is
				//* being pooled, Close() will release it back to the pool.
				//*
				m_state = ConnectionState.Closed;
			}
			
			public IDbCommand CreateCommand()
			{
				// Return a new instance of a command object.
				return new TemplateCommand();
			}
			
			//*
			//* Implementation specific properties / methods.
			//*
			internal SampleDb SampleDb
			{
				get
				{
					return m_sampleDb;
				}
			}
			
			public void Dispose()
			{
				this.Dispose(true);
				System.GC.SuppressFinalize(this);
			}
			
			private void Dispose(bool disposing)
			{
				//
				// Dispose of the object and perform any cleanup.
				//
				
				if (m_state == ConnectionState.Open)
				{
					this.Close();
				}
			}
			
		}
	}
	
}
