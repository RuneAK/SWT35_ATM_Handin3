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
		private readonly ITrackFactory _trackFactory;
		private readonly ICalculator _calculator;
	    private ISeperationRepository _seperationRepository;
		
		public event EventHandler<UpdateEventArgs> TracksUpdated;
		public event EventHandler<SeparationEvent>SeparationsUpdated;

		public Tracker(ITrackFactory trackFactory, ICalculator calculator)
		{
			_seperationRepository = new SeparationRepository();
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

			SeperationEvents();
			
			OnTracksUpdated(new UpdateEventArgs { Tracks = _flightTracks });
			
		}

		private void SeperationEvents()
		{
			if (_flightTracks.FlightTracks.Count != 0)
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
