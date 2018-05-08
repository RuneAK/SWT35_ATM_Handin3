using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWT35_ATM_Handin3.Interfaces;

namespace SWT35_ATM_Handin3.Domain
{
	public class Separation : ISeparation
	{
		public string Tag1 { get; }
		public string Tag2 { get; }
		public DateTime TimeStamp { get; }
		public Separation(string tag1, string tag2, DateTime timeStamp)
		{
			Tag1 = tag1;
			Tag2 = tag2;
			TimeStamp = timeStamp;
		}
	}
}
