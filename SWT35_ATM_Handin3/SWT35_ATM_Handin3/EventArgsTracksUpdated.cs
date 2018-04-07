using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT35_ATM_Handin3
{
	public class EventArgsTracksUpdated : EventArgs
	{
		public List<Track> Tracks { get; set; }
	}
}
