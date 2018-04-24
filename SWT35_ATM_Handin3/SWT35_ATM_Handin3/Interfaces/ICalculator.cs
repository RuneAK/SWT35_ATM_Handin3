using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT35_ATM_Handin3.Interfaces
{
	public interface ICalculator
	{
		double CalculateHorizontalVelocity(Point oldPoint, Point newPoint, DateTime oldTimeStamp, DateTime newTimeStamp);
		double CalculateCompassCourse(Point oldPoint, Point newPoint);
		bool CalculateSeperation(Point point1, Point point2);
		bool CalculateWithinAirspace(Point point, Point boundaryPoint1, Point boundaryPoint2);
	}
}
