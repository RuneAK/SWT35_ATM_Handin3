using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWT35_ATM_Handin3.Interfaces;

namespace SWT35_ATM_Handin3
{
	public class Display : IDisplay
	{
		public Display(ITracker tracker)
		{
			tracker.TracksUpdated += WriteToConsole;
		}

		private void WriteToConsole(object o, TracksUpdatedEventArgs args)
		{
			foreach (var track in args.Tracks)
			{
				var hv = Math.Round(track.HorizontalVelocity, 2);
				Console.WriteLine("Tag: " + track.Tag + " Current position: " + track.XCoordinate + "," + track.YCoordinate +
				                  " Altitude: " + track.Altitude + " Horizontal velocity: " + Math.Round(track.HorizontalVelocity, 2) + "m/s Compass course: " + Math.Round(track.CompassCourse,2) +"°");
			}
		}
	}
}
