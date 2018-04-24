using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWT35_ATM_Handin3.Interfaces;

namespace SWT35_ATM_Handin3
{
	public class Logger : ILogger
	{
		private readonly string _filePathAndName;

		public Logger(string filePathAndName = null)
		{
			_filePathAndName = filePathAndName ?? "Log.txt";
		}

		public void WriteToFile(string output)
		{
			using (System.IO.StreamWriter file = new System.IO.StreamWriter(@_filePathAndName, true))
			{
				file.WriteLine(output);
			}
		}
	}
}
