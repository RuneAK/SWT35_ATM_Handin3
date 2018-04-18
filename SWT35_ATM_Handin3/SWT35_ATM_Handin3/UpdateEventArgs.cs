using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT35_ATM_Handin3
{
	public class UpdateEventArgs : EventArgs
	{
		public List<Track> Tracks { get; set; }
		public List<Separation> SeparationEvents { get; set; }
	}
}
