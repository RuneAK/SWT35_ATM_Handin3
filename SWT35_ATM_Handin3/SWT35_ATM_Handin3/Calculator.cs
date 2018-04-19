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
			var distance = Math.Sqrt(Math.Pow((newPoint.X - oldPoint.X), 2) + Math.Pow((newPoint.Y - oldPoint.Y), 2));
			var velocity = distance / timeDifference;
			
			return velocity;
		}

		public double CalculateCompassCourse(Point oldPoint, Point newPoint)
		{
			double Y = Math.Abs(newPoint.Y - oldPoint.Y);
			double X = Math.Abs(newPoint.X - oldPoint.X);
			double compassCourse  = Math.Atan2(Y, X) * (180 / Math.PI);
			
			//West
			if (newPoint.X < oldPoint.X)
			{
				//South
				if (newPoint.Y < oldPoint.Y)				
					compassCourse += 180;
				//North
				else
					compassCourse += 270;
			}
			//East
			else if (newPoint.X > oldPoint.X)
			{
				//South
				if (newPoint.Y < oldPoint.Y)
					compassCourse += 90;
			}
			//NorthSouth-Direction
			else if(X==0)
			{
				//South
				if (newPoint.Y < oldPoint.Y)
					compassCourse = 180;
				//North
				else
					compassCourse = 0;
			}
			//EastWest-Direction
			else if(Y==0)
			{
				//West
				if (newPoint.X < oldPoint.X)
					compassCourse = 270;
				//East
				else
					compassCourse = 90;
			}
			else
			{
				return Double.NaN;
			}

			return compassCourse;
		}

		public bool CalculateSeperation(Point point1, Point point2)
		{
			if (Math.Abs(point1.Alt - point2.Alt) < _verticalMin)
			{
				var xsqr = Math.Pow(point1.X - point2.X, 2);
				var ysqr = Math.Pow(point1.Y - point2.Y, 2);
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
