using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SWT35_ATM_Handin3
{
	public class Track
	{
		public string Tag { get; set; }
		public Point Position { get; set; }
		public uint Altitude { get; set; }
		public DateTime Timestamp { get; set; }
		public double HorizontalVelocity { get; set; }
		public double CompassCourse { get; set; }
	}
}
