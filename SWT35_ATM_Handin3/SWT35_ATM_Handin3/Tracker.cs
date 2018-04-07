using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWT35_ATM_Handin3.Interfaces;
using TransponderReceiver;

namespace SWT35_ATM_Handin3
{
	public class Tracker : ITracker
	{
		private List<Track> _tracks = new List<Track>();

		private readonly ITrackFactory _trackFactory;
		public event EventHandler<EventArgsTracksUpdated> TracksUpdated;

		public Tracker(ITransponderReceiver transponderReceiver, ITrackFactory trackFactory)
		{
			_trackFactory = trackFactory;
			transponderReceiver.TransponderDataReady += AddNewTracks;
		}

		private void AddNewTracks(object o, RawTransponderDataEventArgs args)
		{
			var newTracks = new List<Track>();

			foreach (var info in args.TransponderData)
			{
				var newTrack = _trackFactory.CreateTrack(info);
				newTracks.Add(newTrack);
			}

			_tracks.AddRange(newTracks);

			OnTracksUpdated(new EventArgsTracksUpdated{Tracks = newTracks});
		}

		private void OnTracksUpdated(EventArgsTracksUpdated args)
		{
			var handler = TracksUpdated;
			handler?.Invoke(this, args);
		}
	}
}
