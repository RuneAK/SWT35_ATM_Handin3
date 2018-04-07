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

		private void WriteToConsole(object o, EventArgsTracksUpdated args)
		{
			foreach (var track in args.Tracks)
			{
				Console.WriteLine("Tag: " + track.Tag + " Xcoordinate: " + track.XCoordinate + " Ycoordinate: " + track.YCoordinate + " Altitude: " + track.Altitude + " Time: " + track.Timestamp.ToString("yyyy-M-d HH:mm:ss:fff"));
			}
		}
	}
}
