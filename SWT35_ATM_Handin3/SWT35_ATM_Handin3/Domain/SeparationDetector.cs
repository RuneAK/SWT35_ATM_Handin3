using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWT35_ATM_Handin3.Boundary;
using SWT35_ATM_Handin3.Interfaces;

namespace SWT35_ATM_Handin3.Domain
{
	public class SeparationDetector : ISeparationDetector
	{

		public SeparationDetector(IUpdate update)
		{
			update.TracksUpdated += DetectSeparations;
		}

		private void DetectSeparations(object sender, EventTracks eventTracks)
		{
			throw new NotImplementedException();
		}

		private bool CalculateSeparation(Point trackOne, Point trackTwo)
		{
			if (Math.Abs(trackOne.Altitude - trackTwo.Altitude) <= 300)
			{
				var xsqr = Math.Pow(trackOne.X - trackTwo.X, 2);
				var ysqr = Math.Pow(trackOne.Y - trackTwo.Y, 2);
				if (Math.Sqrt(xsqr + ysqr) <= 5000)
					return true;
				return false;
			}
			return false;
		}
	}
}
