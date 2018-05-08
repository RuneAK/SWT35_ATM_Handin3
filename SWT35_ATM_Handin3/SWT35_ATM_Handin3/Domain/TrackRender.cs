using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWT35_ATM_Handin3.Boundary;
using SWT35_ATM_Handin3.Interfaces;

namespace SWT35_ATM_Handin3.Domain
{
	public class TrackRender : ITrackRender
	{
		private IDisplay _display;
		public TrackRender(IUpdate update, IDisplay display)
		{
			_display = display;
			update.TracksUpdated += RenderTracks;
		}

		private void RenderTracks(object sender, EventTracks e)
		{
			_display.Clear();
			_display.Write("***Tracks***");
			foreach (var track in e.TrackData)
			{
				var  str = "Tag: " + track.Tag + " CurrentPosition: " + track.Position.X + "mE," +
				           track.Position.Y +
				           "mN Altitude: " + track.Position.Altitude + "m Velocity: " +
				           Math.Round(track.Velocity, 2) + "m/s Course: " +
				           Math.Round(track.Course, 2) + "°";
				_display.Write(str);
			}
		}
	}
}
