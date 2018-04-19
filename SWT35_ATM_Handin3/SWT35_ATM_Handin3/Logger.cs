﻿using System;
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

		public Logger(ITracker tracker,string filePathAndName)
		{
			_filePathAndName = filePathAndName;
			tracker.SeparationsUpdated += WriteToLog;
		}

		public void WriteToLog(object o, SeparationEvent args)
		{
			var logstring = args.Tag1 + ";" + args.Tag2 + ";" + args.Time.ToString("yyyyMMddHHmmssfff");
			using (System.IO.StreamWriter file = new System.IO.StreamWriter(@_filePathAndName, true))
			{
				file.WriteLine(logstring);
			}
		}
	}
}
