using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWT35_ATM_Handin3.Interfaces;

namespace SWT35_ATM_Handin3
{
	public class Tracks : ITracks
	{
		public List<ITrack> FlightTracks { get; }
		private ICalculator _calculator;
		private IAirspace _airspace;

		public Tracks()
		{
			FlightTracks = new List<ITrack>();
		}
		public Tracks(ICalculator calculator, IAirspace airspace)
		{
			_airspace = airspace;
			_calculator = calculator;
			FlightTracks = new List<ITrack>();
		}

		public void Add(ITrack track)
		{
			FlightTracks.Add(track);
		}

		public void Update(ITracks flightTracks)
		{
			foreach (var newTrack in flightTracks.FlightTracks)
			{
				var oldTrack = FlightTracks.Find(i => i.Tag == newTrack.Tag);

				if (_calculator.CalculateWithinAirspace(newTrack.Position, _airspace.LowerBound, _airspace.UpperBound))
				{
					if (oldTrack == null)
					{
						FlightTracks.Add(newTrack);
					}
					else
					{
						FlightTracks.Remove(oldTrack);
						newTrack.HorizontalVelocity = _calculator.CalculateHorizontalVelocity(oldTrack.Position, newTrack.Position,
							oldTrack.Timestamp, newTrack.Timestamp);
						newTrack.CompassCourse = _calculator.CalculateCompassCourse(oldTrack.Position, newTrack.Position);
						FlightTracks.Add(newTrack);
					}
				}
				else
				{
					FlightTracks.Remove(oldTrack);
				}
				
			}
		}
	}
}
