using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWT35_ATM_Handin3.Interfaces;
using TransponderReceiver;

namespace SWT35_ATM_Handin3
{
	public class TrackFactory : ITrackFactory
	{
		
		public event EventHandler<UpdateEventArgs> TracksUpdated;

		public TrackFactory(ITransponderReceiver transponderReceiver)
		{
			transponderReceiver.TransponderDataReady += NewTracks;
		}

		private void NewTracks(object o, RawTransponderDataEventArgs args)
		{
			ITracks newTracks = new Tracks();

			foreach (var str in args.TransponderData)
			{
				newTracks.Add(CreateTrack(str));
			}

			TransponderUpdated(new UpdateEventArgs { Tracks = newTracks});
		}

		public Track CreateTrack(string info)
		{
			string[] splitInfo = info.Split(';');
			var track = new Track();

			track.Tag = splitInfo[0];
			track.Position = new Point(Int32.Parse(splitInfo[1]), Int32.Parse(splitInfo[2]), Int32.Parse(splitInfo[3]));
			track.Timestamp = DateTime.ParseExact(splitInfo[4], "yyyyMMddHHmmssfff", System.Globalization.CultureInfo.InvariantCulture);

			return track;
		}

		public void TransponderUpdated(UpdateEventArgs args)
		{
			var handler = TracksUpdated;
			handler?.Invoke(this, args);
		}
	}
}
