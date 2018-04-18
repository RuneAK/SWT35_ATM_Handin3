using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Constraints;
using SWT35_ATM_Handin3.Interfaces;
using TransponderReceiver;

namespace SWT35_ATM_Handin3
{
	public class Tracker : ITracker
	{
		private List<Track> _tracks = new List<Track>();
		
		private readonly ITrackFactory _trackFactory;
		private readonly ICalculator _calculator;
		
		public event EventHandler<UpdateEventArgs> TracksUpdated;
		
		public Tracker(ITransponderReceiver transponderReceiver, ITrackFactory trackFactory, ICalculator calculator)
		{
			_calculator = calculator;
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

			//Test
			var test1 = new Track();
			test1.Tag = "Test1";
			test1.Altitude = 100;
			test1.Position = new Point(100,100);
			var test2 = new Track();
			test2.Tag = "Test2";
			test2.Altitude = 200;
			test2.Position = new Point(200,200);
			newTracks.Add(test1);
			newTracks.Add(test2);
			//

			_tracks = Update(newTracks);
			var separationEvents = new List<Separation>();

			foreach (var trackOne in _tracks)
			{
				foreach (var trackTwo in _tracks)
				{
					if (trackOne.Tag != trackTwo.Tag)
					{
						if(_calculator.CalculateSeperation(trackOne.Altitude, trackTwo.Altitude,trackOne.Position,trackTwo.Position))
						{
							var newSeperationEvent = new Separation();
							newSeperationEvent.Tag1 = trackOne.Tag;
							newSeperationEvent.Tag2 = trackTwo.Tag;
							newSeperationEvent.Time = DateTime.Now;
							separationEvents.Add(newSeperationEvent);
						}
					}	
				}
			}

			OnTracksUpdated(new UpdateEventArgs{Tracks = _tracks, SeparationEvents = separationEvents});
		}

		private void OnTracksUpdated(UpdateEventArgs args)
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
					newTrack.CompassCourse = Double.NaN;
					newTrack.HorizontalVelocity = Double.NaN;
					updatedTracks.Add(newTrack);
				}
				else
				{
					newTrack.HorizontalVelocity = _calculator.CalculateHorizontalVelocity(oldTrack.Position, newTrack.Position,
						oldTrack.Timestamp, newTrack.Timestamp);
					newTrack.CompassCourse = _calculator.CalculateCompassCourse(oldTrack.Position, newTrack.Position);
					updatedTracks.Add(newTrack);
				}
			}

			return updatedTracks;

		}
	}
}
