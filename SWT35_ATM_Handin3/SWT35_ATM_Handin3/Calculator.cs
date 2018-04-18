using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWT35_ATM_Handin3.Interfaces;

namespace SWT35_ATM_Handin3
{
	public class Calculator : ICalculator
	{
		private readonly uint _verticalMin;
		private readonly uint _horizontalMin;

		public Calculator(uint verticalMin, uint horizontalMin)
		{
			_verticalMin = verticalMin;
			_horizontalMin = horizontalMin;
		}
		public double CalculateHorizontalVelocity(Point oldPoint, Point newPoint, DateTime oldTimeStamp, DateTime newTimeStamp)
		{

			TimeSpan diff = newTimeStamp.Subtract(oldTimeStamp);
			var timeDifference = diff.TotalSeconds;
			var distance = Math.Sqrt(Math.Pow((newPoint.XCoordinate - oldPoint.XCoordinate), 2) + Math.Pow((newPoint.YCoordinate - oldPoint.YCoordinate), 2));
			var velocity = distance / timeDifference;
			
			return velocity;
		}

		public double CalculateCompassCourse(Point oldPoint, Point newPoint)
		{
			var Y = newPoint.YCoordinate - oldPoint.YCoordinate;
			var X = newPoint.XCoordinate - oldPoint.XCoordinate;
			if (X != 0 && Y != 0)
			{
				var divide = (newPoint.YCoordinate - oldPoint.YCoordinate) / (newPoint.XCoordinate - oldPoint.XCoordinate);
				var compassCourse = Math.Atan(divide) / Math.PI * 180;

				if (compassCourse < 0)
				{
					compassCourse += 360;
				}

				return compassCourse;
			}
			else if (X == 0)
			{
				return 90;
			}
			else if (Y == 0)
			{
				return 0;
			}
			
			return  Double.NaN;
		}

		public bool CalculateSeperation(uint altitude1, uint altitude2, Point point1, Point point2)
		{
			if (Math.Abs(altitude1 - altitude2) < _verticalMin)
			{
				var xsqr = Math.Pow(point1.XCoordinate - point2.XCoordinate, 2);
				var ysqr = Math.Pow(point1.YCoordinate - point2.YCoordinate, 2);
				if (Math.Sqrt(xsqr + ysqr) < _horizontalMin)
				{
					return true;
				}
				else
				{
					return false;
				}
			}
			else
			{
				return false;
			}
		}
	}
}
