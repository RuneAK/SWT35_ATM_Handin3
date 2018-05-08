using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWT35_ATM_Handin3.Domain;
using SWT35_ATM_Handin3.Interfaces;
using TransponderReceiver;

namespace SWT35_ATM_Handin3.Boundary
{
	public class Factory : IFactory
	{
		public event EventHandler<EventTracks> TracksReady;
		public Factory(ITransponderReceiver transponderReceiver)
		{
			transponderReceiver.TransponderDataReady += CreateTracks;
		}

		private void CreateTracks(object sender, RawTransponderDataEventArgs e)
		{
			var trackList = new List<ITrack>();
			foreach (var str in e.TransponderData)
			{
				var split = str.Split(';');
				var newTrack = new Track(split[0], new Point(Int32.Parse(split[1]), Int32.Parse(split[2]), Int32.Parse(split[3])),
					DateTime.ParseExact(split[4], "yyyyMMddHHmmssfff", null));
				trackList.Add(newTrack);
			}

			NewTrackEvent(new EventTracks(trackList));
		}

		protected virtual void NewTrackEvent(EventTracks e)
		{
			TracksReady?.Invoke(this, e);
		}
	}
}
