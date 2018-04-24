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
		public void Write(string output)
		{
			Console.WriteLine(output);
			/*
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

			foreach (var track in args.Tracks.FlightTracks)
			{
				var hv = Math.Round(track.HorizontalVelocity, 2);
				Console.WriteLine("Tag: " + track.Tag + " CurrentPosition: " + track.Position.X + "mE," + track.Position.Y +
				                  "mN Altitude: " + track.Position.Alt + "m HorizontalVelocity: " +
				                  Math.Round(track.HorizontalVelocity, 2) + "m/s CompassCourse: " +
				                  Math.Round(track.CompassCourse, 2) + "°");
			}
			*/
		}

		public void WriteRed(string output)
		{
			Console.BackgroundColor = ConsoleColor.Red;
			Console.ForegroundColor = ConsoleColor.Black;

			Console.WriteLine(output);

			Console.ResetColor();
		}

		public void Clear()
		{
			Console.Clear();
		}
	}
}
