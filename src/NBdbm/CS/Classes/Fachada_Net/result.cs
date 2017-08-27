using System.Data;
using System.Collections;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System;


namespace NBdbm
{
    namespace DotNetDataProviderTemplate
    {
        //*
        //* This class provides database-like operations to simulate a real
        //* data source. The class generates sample data and uses a
        //* fixed set of commands.
        //*
        public class SampleDb
        {

            private const string m_sSelectCmd = "select ";
            private const string m_sUpdateCmd = "update ";

            public class SampleDbResultSet
            {

                public struct MetaDataStruct
                {
                    public string name;
                    public Type type;
                    public int maxSize;
                }

                public int recordsAffected;
                public MetaDataStruct[] metaData;
                public object[,] data;
            }

            private SampleDbResultSet m_resultset;

            public void Execute(string sCmd, ref SampleDbResultSet resultset)
            {
                //*
                //* The sample code simulates SELECT and UPDATE operations.
                //*
                if (0 == string.Compare(sCmd, 0, m_sSelectCmd, 0, m_sSelectCmd.Length, true))
                {
                    _executeSelect(ref resultset);
                }
                else if (0 == string.Compare(sCmd, 0, m_sUpdateCmd, 0, m_sUpdateCmd.Length, true))
                {
                    _executeUpdate(ref resultset);
                }
                else
                {
                    throw (new NotSupportedException("Command string was not recognized"));
                }
            }

            private void _executeSelect(ref SampleDbResultSet resultset)
            {
                // If no sample data exists, create it.
                if (m_resultset == null)
                {
                    _resultsetCreate();
                }

                // Return the sample results.
                resultset = m_resultset;
            }

            private void _executeUpdate(ref SampleDbResultSet resultset)
            {
                // If no sample data exists, create it.
                if (m_resultset == null)
                {
                    _resultsetCreate();
                }

                // Change a row to simulate an update command.
                m_resultset.data[2, 2] = 4199;

                // Create a result set object that is empty except for the RecordsAffected field.
                resultset = new SampleDbResultSet();
                resultset.recordsAffected = 1;
            }

            private void _resultsetCreate()
            {
                m_resultset = new SampleDbResultSet();

                // RecordsAffected is always zero for a SELECT.
                m_resultset.recordsAffected = 0;

                const int numCols = 3;

                SampleDbResultSet.MetaDataStruct[] metaDataArray;
                metaDataArray = new SampleDbResultSet.MetaDataStruct[numCols + 1];
                m_resultset.metaData = metaDataArray;

                _resultsetFillColumn(0, "id", Type.GetType("System.Int32"), 0);
                _resultsetFillColumn(1, "name", Type.GetType("System.String"), 64);
                _resultsetFillColumn(2, "orderid", Type.GetType("System.Int32"), 0);

                object dataArray;
                dataArray = new object[6, numCols + 1];
                m_resultset.data = dataArray;

                _resultsetFillRow(0, 1, "Biggs", 2001);
                _resultsetFillRow(1, 2, "Brown", 2121);
                _resultsetFillRow(2, 3, "Jones", 2543);
                _resultsetFillRow(3, 4, "Smith", 2772);
                _resultsetFillRow(4, 5, "Tyler", 3521);
            }

            private void _resultsetFillColumn(int nIdx, string name, Type type, int maxSize)
            {
                m_resultset.metaData[nIdx].name = name;
                m_resultset.metaData[nIdx].type = type;
                m_resultset.metaData[nIdx].maxSize = maxSize;
            }

            private void _resultsetFillRow(int nIdx, int id, string name, int orderid)
            {
                m_resultset.data[nIdx, 0] = id;
                m_resultset.data[nIdx, 1] = name;
                m_resultset.data[nIdx, 2] = orderid;
            }
        }
    }

}
