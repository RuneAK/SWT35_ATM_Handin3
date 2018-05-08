using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWT35_ATM_Handin3.Interfaces;

namespace SWT35_ATM_Handin3.Boundary
{
	public class Display : IDisplay
	{
		public void Write(string str)
		{
			Console.WriteLine(str);
		}

		public void Clear()
		{
			Console.Clear();
		}
	}
}
