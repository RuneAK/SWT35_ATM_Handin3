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
		private List<Separation> _sperations = new List<Separation>();
		
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


		/* Obsolete!
		private Track CalculateVelocityAndCompassCourse(Track oldTrack, Track newTrack)
		{

			DONE!
			TimeSpan diff = newTrack.Timestamp.Subtract(oldTrack.Timestamp);
			var timeDifference = diff.TotalSeconds;
			var distance = Math.Sqrt(Math.Pow((newTrack.XCoordinate - oldTrack.XCoordinate), 2) + Math.Pow((newTrack.YCoordinate - oldTrack.YCoordinate), 2));
			var velocity = distance / timeDifference;
			newTrack.HorizontalVelocity = velocity;

			DONE!
			var Y = newTrack.YCoordinate - oldTrack.YCoordinate;
			var X = newTrack.XCoordinate - oldTrack.XCoordinate;
			if (X != 0 && Y != 0)
			{
				var divide = (newTrack.YCoordinate - oldTrack.YCoordinate) / (newTrack.XCoordinate - oldTrack.XCoordinate);
				var compassCourse = Math.Atan(divide) / Math.PI * 180;

				if (compassCourse < 0)
				{
					compassCourse += 360;
				}

				newTrack.CompassCourse = compassCourse;
			}
			else if(X==0)
			{
				newTrack.CompassCourse = 90;
			}
			else if(Y==0)
			{
				newTrack.CompassCourse = 0;
			}
			else
			{
				newTrack.CompassCourse = Double.NaN;

			}
			

			return newTrack;
		}
		*/

		/* Obselete
		private bool CheckSeperation(Track trackOne, Track trackTwo)
		{
			if (Math.Abs(trackOne.Altitude - trackTwo.Altitude) < 300)
			{
				var xsqr = Math.Pow(trackOne.XCoordinate - trackTwo.XCoordinate, 2);
				var ysqr = Math.Pow(trackOne.YCoordinate - trackTwo.YCoordinate,2);
				if (Math.Sqrt(xsqr + ysqr)<5000)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}
		*/
		
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
