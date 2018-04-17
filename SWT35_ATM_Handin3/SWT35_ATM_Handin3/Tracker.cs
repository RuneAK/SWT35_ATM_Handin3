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
		public event EventHandler<TracksUpdatedEventArgs> TracksUpdated;

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

			_tracks = Update(newTracks);

			OnTracksUpdated(new TracksUpdatedEventArgs{Tracks = _tracks});
		}

		private void OnTracksUpdated(TracksUpdatedEventArgs args)
		{
			var handler = TracksUpdated;
			handler?.Invoke(this, args);
		}

		
		private List<Track> Update(List<Track> newTracks)
		{
			var updatedTracks = new List<Track>();

			foreach (var newTrack in newTracks)
			{
				var oldTrack = _tracks.Find(i => i.Tag == newTrack.Tag);
				if (oldTrack == null)
				{
					updatedTracks.Add(newTrack);
				}
				else
				{
					TimeSpan diff = newTrack.Timestamp.Subtract(oldTrack.Timestamp);
					var timeDifference = diff.TotalSeconds;
					var distance = Math.Sqrt(Math.Pow((newTrack.XCoordinate - oldTrack.XCoordinate),2) + Math.Pow((newTrack.YCoordinate - oldTrack.YCoordinate),2));
					var velocity = distance / timeDifference;
					newTrack.HorizontalVelocity = velocity;
					
					var divide = (newTrack.YCoordinate-oldTrack.YCoordinate) / (newTrack.XCoordinate - oldTrack.XCoordinate);
					var compassCourse = Math.Atan(divide) / Math.PI * 180;
					
					if (compassCourse < 0)
					{
						compassCourse += 360;
					}
					
					newTrack.CompassCourse = compassCourse;
					updatedTracks.Add(newTrack);
				}
			}

			return updatedTracks;

		}
	}
}
