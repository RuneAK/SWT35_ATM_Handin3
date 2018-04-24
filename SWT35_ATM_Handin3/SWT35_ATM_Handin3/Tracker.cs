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
		private IAirspace _airspace;
		private readonly ITracks _flightTracks;
		private List<SeparationEvent> _separations = new List<SeparationEvent>();
		private readonly ITrackFactory _trackFactory;
		private readonly ICalculator _calculator;
	    private ISeperationRepository _seperationRepository;
		
		public event EventHandler<UpdateEventArgs> TracksUpdated;
		public event EventHandler<SeparationEvent>SeparationsUpdated;

		public Tracker(ITrackFactory trackFactory, ICalculator calculator, IAirspace airspace)
		{
			_airspace = airspace;
            _seperationRepository = new SeparationRepository();
			_calculator = calculator;
			_trackFactory = trackFactory;
			_flightTracks = new Tracks(_calculator);
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
			

			/* !!!Test!!!
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
			*/
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
                            _seperationRepository.AddSeperationEvent(newSeperationEvent);						
						}
						else
						{
						    _seperationRepository.DeleteSeperationEvent(_seperationRepository.Get(trackOne.Tag, trackTwo.Tag));
						}
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
