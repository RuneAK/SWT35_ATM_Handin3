using System;
using System.Collections.Generic;
using SWT35_ATM_Handin3.Interfaces;

namespace SWT35_ATM_Handin3
{
	public class EventTracks : EventArgs
	{
		public List<ITrack> TrackData;
		public EventTracks(List<ITrack> trackData)
		{
			TrackData = trackData;
		}
	}
}