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

		private void WriteToConsole(object o, UpdateEventArgs args)
		{
			Console.Clear();

			if (args.SeparationEvents.Count != 0)
			{
				Console.BackgroundColor = ConsoleColor.Red;
				Console.ForegroundColor = ConsoleColor.Black;
				Console.WriteLine("***SeparationEvent(s)***");
				foreach (var separation in args.SeparationEvents)
				{
					Console.WriteLine("Tags: " + separation.Tag1 + "/" + separation.Tag2 + " TimeRaised: " + separation.Time);
				}
				Console.WriteLine("----------------------");

			}

			Console.ResetColor();

			foreach (var track in args.Tracks)
			{
				var hv = Math.Round(track.HorizontalVelocity, 2);
				Console.WriteLine("Tag: " + track.Tag + " CurrentPosition: " + track.Position.XCoordinate + "mE," + track.Position.YCoordinate +
				                  "mN Altitude: " + track.Altitude + "m HorizontalVelocity: " +
				                  Math.Round(track.HorizontalVelocity, 2) + "m/s CompassCourse: " +
				                  Math.Round(track.CompassCourse, 2) + "°");
			}
		}
	}
}
