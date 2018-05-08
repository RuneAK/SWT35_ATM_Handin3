using System.Collections.Generic;
using SWT35_ATM_Handin3.Interfaces;

namespace SWT35_ATM_Handin3.Boundary
{
	public class EventTracks
	{
		public EventTracks(List<ITrack> trackData)
		{
			TrackData = trackData;
		}
		public List<ITrack> TrackData;
	}
}