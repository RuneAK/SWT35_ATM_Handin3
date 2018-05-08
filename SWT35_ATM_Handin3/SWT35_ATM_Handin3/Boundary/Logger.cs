using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWT35_ATM_Handin3.Interfaces;

namespace SWT35_ATM_Handin3.Boundary
{
	public class Logger : ILogger
	{

		private readonly string _filePathAndName;

		public Logger(string filePathAndName = null)
		{
			_filePathAndName = filePathAndName ?? "Log.txt";
		}
		public void Log(string str)
		{
			using (StreamWriter file = new StreamWriter(_filePathAndName, true))
				file.WriteLine(str);
		}
	}
}
