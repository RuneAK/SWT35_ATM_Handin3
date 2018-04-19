using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT35_ATM_Handin3.Interfaces
{
	public interface ITrack
	{
		string Tag { get; set; }
		Point Position { get; set; }
		DateTime Timestamp { get; set; }
		double HorizontalVelocity { get; set; }
		double CompassCourse { get; set; }
	}
}
