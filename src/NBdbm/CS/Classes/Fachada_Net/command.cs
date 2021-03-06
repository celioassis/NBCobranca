using System.Data;
using System.Collections;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System;



namespace NBdbm
{
	namespace DotNetDataProviderTemplate
	{
		public class TemplateCommand : IDbCommand
		{
			
			
			private TemplateConnection m_connection;
			private TemplateTransaction m_txn;
			private string m_sCmdText;
			private UpdateRowSource m_updatedRowSource = UpdateRowSource.None;
			private TemplateParameterCollection m_parameters = new TemplateParameterCollection();
			
			// Implement the default constructor here.
			public TemplateCommand()
			{
			}
			
			// Implement other constructors here.
			public TemplateCommand(string cmdText)
			{
				
				m_sCmdText = cmdText;
			}
			
			public TemplateCommand(string cmdText, TemplateConnection connection)
			{
				
				m_sCmdText = cmdText;
				m_connection = connection;
			}
			
			public TemplateCommand(string cmdText, TemplateConnection connection, TemplateTransaction txn)
			{
				
				m_sCmdText = cmdText;
				m_connection = connection;
				m_txn = txn;
			}
			
			//****
			//* IMPLEMENT THE REQUIRED PROPERTIES.
			//****
			public string CommandText
			{
				get
				{
					return m_sCmdText;
				}
				set
				{
					m_sCmdText = value;
				}
			}
			
			public int CommandTimeout
			{
				//*
				// * The sample does not support a command time-out. As a result,
				// * for the get, zero is returned because zero indicates an indefinite
				// * time-out period. For the set, throw an exception.
				//*
				get
				{
					return 0;
				}
				set
				{
					if (value != 0)
					{
						throw (new NotSupportedException());
					}
				}
			}
			
			public CommandType CommandType
			{
				//*
				//* The sample only supports CommandType.Text.
				//*
				get
				{
					return CommandType.Text;
				}
				set
				{
					if (value != CommandType.Text)
					{
						throw (new NotSupportedException());
					}
				}
			}
			
			public IDbConnection Connection
			{
				//**
				//* The user should be able to set or change the connection at
				//* any time.
				//*
				get
				{
					return m_connection;
				}
				set
				{
					//*
					//* Because the connection is associated with the transaction,
					//* setthe transaction object to return a null reference if the connection
					//* is reset.
					//*
					if (m_connection != value)
					{
						this.Transaction = null;
					}
					
					m_connection = (TemplateConnection) value;
				}
			}
			
			public IDataParameterCollection Parameters
			{
				get
				{
					return m_parameters;
				}
			}
			
			public IDbTransaction Transaction
			{
				//*
				//* Set the transaction. Consider additional steps to ensure that the transaction
				//* is compatible with the connection, because the two are usually linked.
				//*
				get
				{
					return m_txn;
				}
				set
				{
					m_txn = (TemplateTransaction) value;
				}
			}
			
			public UpdateRowSource UpdatedRowSource
			{
				get
				{
					return m_updatedRowSource;
				}
				set
				{
					m_updatedRowSource = value;
				}
			}
			
			//****
			//* IMPLEMENT THE REQUIRED METHODS.
			//****
			public void Cancel()
			{
				// The sample does not support canceling a command
				// once it has been initiated.
				throw (new NotSupportedException());
			}
			
			public IDbDataParameter CreateParameter()
			{
				return ((IDbDataParameter) (new TemplateParameter()));
			}
			
			public int ExecuteNonQuery()
			{
				//*
				//* ExecuteNonQuery is intended for commands that do
				//* not return results, instead returning only the number
				//* of records affected.
				//*
				
				// There must be a valid and open connection.
				if (m_connection == null || m_connection.State != ConnectionState.Open)
				{
					throw (new InvalidOperationException("Connection must valid and open"));
				}
				
				// Execute the command.
				SampleDb.SampleDbResultSet resultSet = new SampleDb.SampleDbResultSet();
				//m_connection.SampleDb.Execute(m_sCmdText, resultSet)
				
				// Return the number of records affected.
				return resultSet.recordsAffected;
			}
			
			public IDataReader ExecuteReader()
			{
				//*
				//* ExecuteReader should retrieve results from the data source
				//* and return a DataReader that allows the user to process
				//* the results.
				//*
				// There must be a valid and open connection.
				if (m_connection == null || m_connection.State != ConnectionState.Open)
				{
					throw (new InvalidOperationException("Connection must valid and open"));
				}
				
				// Execute the command.
				SampleDb.SampleDbResultSet resultSet = new SampleDb.SampleDbResultSet();
				m_connection.SampleDb.Execute(m_sCmdText, ref resultSet);
				
				return new TemplateDataReader(resultSet);
			}
			
			public IDataReader ExecuteReader(CommandBehavior behavior)
			{
				//*
				//* ExecuteReader should retrieve results from the data source
				//* and return a DataReader that allows the user to process
				//* the results.
				//*
				
				// There must be a valid and open connection.
				if (m_connection == null || m_connection.State != ConnectionState.Open)
				{
					throw (new InvalidOperationException("Connection must valid and open"));
				}
				
				// Execute the command.
				SampleDb.SampleDbResultSet resultSet = new SampleDb.SampleDbResultSet();
				m_connection.SampleDb.Execute(m_sCmdText, ref resultSet);
				
				//*
				//* The only CommandBehavior option supported by this
				//* sample is the automatic closing of the connection
				//* when the user is done with the reader.
				//*
				if (behavior == CommandBehavior.CloseConnection)
				{
					return new TemplateDataReader(resultSet, m_connection);
				}
				else
				{
					return new TemplateDataReader(resultSet);
				}
			}
			
			public object ExecuteScalar()
			{
				//*
				//* ExecuteScalar assumes that the command will return a single
				//* row with a single column, or if more rows/columns are returned
				//* it will return the first column of the first row.
				//*
				
				// There must be a valid and open connection.
				if (m_connection == null || m_connection.State != ConnectionState.Open)
				{
					throw (new InvalidOperationException("Connection must valid and open"));
				}
				
				// Execute the command.
				SampleDb.SampleDbResultSet resultSet = new SampleDb.SampleDbResultSet();
				m_connection.SampleDb.Execute(m_sCmdText, ref resultSet);
				
				// Return the first column of the first row.
				// Return a null reference if there is no data.
				if (resultSet.data.Length == 0)
				{
					return null;
				}
				
				return resultSet.data[0, 0];
			}
			
			public void Prepare()
			{
				// The sample Prepare is a no-op.
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
			}
			
		}
	}
	
	
}
