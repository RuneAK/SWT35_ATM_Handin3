using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT35_ATM_Handin3.Interfaces
{
	public interface ITracks
	{
		List<ITrack> FlightTracks { get; }
		void Add(ITrack track);
		void Update(ITracks flightTracks);
	}
}
