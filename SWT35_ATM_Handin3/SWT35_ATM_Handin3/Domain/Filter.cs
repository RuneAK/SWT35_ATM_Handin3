using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWT35_ATM_Handin3.Boundary;
using SWT35_ATM_Handin3.Interfaces;

namespace SWT35_ATM_Handin3.Domain
{
	public class Filter : IFilter
	{
		private IAirspace _airspace;
		public event EventHandler<EventTracks> TracksFiltered;
		public Filter(IAirspace airspace, IFactory factory)
		{
			_airspace = airspace;
			factory.TracksReady += FilterTracks;
		}

		private void FilterTracks(object sender, EventTracks e)
		{
			var filteredTracks = new List<ITrack>();

			foreach (var track in e.TrackData)
			{
				if (_airspace.CalculateWithinAirspace(track.Position))
					filteredTracks.Add(track);
			}

			FilteredTrackEvent(new EventTracks(filteredTracks));
		}

		protected virtual void FilteredTrackEvent(EventTracks e)
		{
			TracksFiltered?.Invoke(this, e);
		}
	}
}
