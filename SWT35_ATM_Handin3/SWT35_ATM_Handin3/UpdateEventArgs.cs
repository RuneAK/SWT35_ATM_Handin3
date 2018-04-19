using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWT35_ATM_Handin3.Interfaces;

namespace SWT35_ATM_Handin3
{
	public class UpdateEventArgs : EventArgs
	{
		public ITracks Tracks { get; set; }
		public List<SeparationEvent> SeparationEvents { get; set; }
	}
}
