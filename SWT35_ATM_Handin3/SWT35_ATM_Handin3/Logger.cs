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
		private StreamWriter _streamWriter;
		
		public Logger(ITracker tracker,string filePathAndName)
		{
			_streamWriter = new StreamWriter(filePathAndName,true);
			tracker.TracksUpdated += WriteToLog;
		}

		public void WriteToLog(object o, UpdateEventArgs args)
		{
			foreach (var separation in args.SeparationEvents)
			{
				var logstring = separation.Tag1 + ";" + separation.Tag2 + ";" + separation.Time.ToString("yyyyMMddHHmmssfff");
				_streamWriter.WriteLine(logstring);
			}
		}
	}
}
