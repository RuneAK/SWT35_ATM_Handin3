using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework.Constraints;
using SWT35_ATM_Handin3.Interfaces;
using TransponderReceiver;

namespace SWT35_ATM_Handin3
{
	public class Tracker : ITracker
	{
		private readonly ITracks _flightTracks;
		private List<SeparationEvent> _separations = new List<SeparationEvent>();
		private readonly ITrackFactory _trackFactory;
		private readonly ICalculator _calculator;
		
		public event EventHandler<UpdateEventArgs> TracksUpdated;
		public event EventHandler<SeparationEvent>SeparationsUpdated;

		public Tracker(ITrackFactory trackFactory, ICalculator calculator)
		{
			_calculator = calculator;
			_trackFactory = trackFactory;
			_flightTracks = new Tracks(_calculator, new Airspace());
			_trackFactory.TracksUpdated += Tracking;
		}

		private void Tracking(object o, UpdateEventArgs args)
		{
			if (args.Tracks.FlightTracks.Count != 0)
			{
				_flightTracks.Update(args.Tracks);
			}

			if (_flightTracks.FlightTracks.Count != 0)
			{
				SeperationEvents();
			}
			
			if (_separations.Count != 0 || _flightTracks.FlightTracks.Count != 0)
			{
				OnTracksUpdated(new UpdateEventArgs { Tracks = _flightTracks, SeparationEvents = _separations });
			}
		}

		private void SeperationEvents()
		{
			foreach (var trackOne in _flightTracks.FlightTracks)
			{
				foreach (var trackTwo in _flightTracks.FlightTracks)
				{
					if (trackOne.Tag != trackTwo.Tag)
					{
						if (_calculator.CalculateSeperation(trackOne.Position, trackTwo.Position))
						{
							var newSeperationEvent = new SeparationEvent(trackOne.Tag, trackTwo.Tag, DateTime.Now);

							if (_separations.Find(i => i.Tag1 == trackOne.Tag && i.Tag2 == trackTwo.Tag) == null && _separations.Find(i => i.Tag2 == trackOne.Tag && i.Tag1 == trackTwo.Tag) == null)
							{
								OnSeparationsUpdated(newSeperationEvent);
								_separations.Add(newSeperationEvent);
							}
						}
					}
				}
			}

			if (_separations.Any())
			{
				foreach (var se in _separations)
				{
					var track1 = _flightTracks.FlightTracks.Find(i => i.Tag == se.Tag1);
					var track2 = _flightTracks.FlightTracks.Find(i => i.Tag == se.Tag2);
					if (!_calculator.CalculateSeperation(track1.Position, track2.Position))
					{
						//remove me from list PLEASE!
					}
				}
			}
		}

		private void OnTracksUpdated(UpdateEventArgs args)
		{
			var handler = TracksUpdated;
			handler?.Invoke(this, args);
		}

		private void OnSeparationsUpdated(SeparationEvent args)
		{
			var handler = SeparationsUpdated;
			handler?.Invoke(this, args);
		}

	}
}
