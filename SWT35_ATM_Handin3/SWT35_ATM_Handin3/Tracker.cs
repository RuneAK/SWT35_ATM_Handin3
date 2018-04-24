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
		private IDisplay _display;
		private ILogger _logger;
		
		public Tracker(ITrackFactory trackFactory, ICalculator calculator, IDisplay display, ILogger logger)
		{
			_logger = logger;
			_display = display;
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
			
			_display.Clear();
			if (_seperationRepository.GetAll().Count != 0)
			{
				_display.WriteRed("Seperation Event(s):");
				foreach (var var in _seperationRepository.GetAll())
				{
					_display.WriteRed(var.Tag1 + "/" + var.Tag2 + " Time: " + var.Time);
				}
			}

			if (_flightTracks.FlightTracks.Count != 0)
			{
				_display.Write("Tracks:");
				foreach (var track in _flightTracks.FlightTracks)
				{
					_display.Write("Tag: " + track.Tag + " CurrentPosition: " + track.Position.X + "mE," + track.Position.Y +
					               "mN Altitude: " + track.Position.Alt + "m HorizontalVelocity: " +
					               Math.Round(track.HorizontalVelocity, 2) + "m/s CompassCourse: " +
					               Math.Round(track.CompassCourse, 2) + "°");
				}
			}
		
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
								_logger.WriteToFile(newSeperationEvent.Tag1 + ";" + newSeperationEvent.Tag2 + ";" + newSeperationEvent.Time);
							}
							else
							{
								var tempSeparationEvent = _seperationRepository.Get(trackOne.Tag, trackTwo.Tag);
								if(tempSeparationEvent != null)
									_seperationRepository.DeleteSeperationEvent(tempSeparationEvent);
							}
						}
					}
				}
			}
		}
	}
}
