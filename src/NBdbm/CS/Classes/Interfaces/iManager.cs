using System.Data;
using System.Collections;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System;

namespace NBdbm
{
	namespace Interfaces
	{
		
		public interface iManager
		{
			
			object createDB(string commandString);
			
			object createTable(string commandString);
			
			object createView(string commandString);
			
			object createField(string commandString);
			
			System.Data.DataView readDB(string SQL);
			
		}
		
	}
}
