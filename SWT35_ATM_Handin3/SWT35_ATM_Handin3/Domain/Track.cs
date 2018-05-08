using System;
using SWT35_ATM_Handin3.Interfaces;

namespace SWT35_ATM_Handin3.Domain
{
	public class Track : ITrack
	{
		public string Tag { get; set; }
		public Point Position { get; set; }
		public DateTime Timestamp { get; set; }
		public double Velocity { get; set; }
		public double Course { get; set; }

		public Track(string tag, Point position, DateTime timestamp)
		{
			Tag = tag;
			Position = position;
			Timestamp = timestamp;
		}
	}
}