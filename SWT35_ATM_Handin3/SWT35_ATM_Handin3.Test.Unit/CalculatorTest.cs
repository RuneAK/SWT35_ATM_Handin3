using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using SWT35_ATM_Handin3.Interfaces;

namespace SWT35_ATM_Handin3.Test.Unit
{
	public class CalculatorTest
	{
		private Calculator _uut;

		[SetUp]
		public void SetUp()
		{
			_uut = new Calculator(300, 5000);
		}

		[TestCase(1,1,1,2,1,2,1)]
		public void Test_CalculateHorizontalVelocity_CalculationCorrect(int x1, int y1, int x2, int y2, int time1, int time2, double velocity)
		{
			Assert.That(_uut.CalculateHorizontalVelocity(new Point(x1, y1, 0), new Point(x2, y2, 0), new DateTime(2000, 1, 1, 1, 1, time1),
				new DateTime(2000, 1, 1, 1, 1, time2)), Is.EqualTo(velocity));
		}

		[TestCase(1, 1, 1, 1, Double.NaN)]
		[TestCase(1, 1, 1, 2, 0)]
		[TestCase(1, 1, 2, 1, 90)]
		[TestCase(2, 1, 1, 1, 270)]
		[TestCase(1, 2, 1, 1, 180)]
		[TestCase(1, 1, 2, 2, 45)]
		[TestCase(1, 2, 2, 1, 135)]
		[TestCase(2, 2, 1, 1, 225)]
		[TestCase(2, 1, 1, 2, 315)]
		public void Test_CalculateCompassCourse_CalculationCorrect(int x1, int y1, int x2, int y2, double compasscourse)
		{
			Assert.That(_uut.CalculateCompassCourse(new Point(x1, y1, 0), new Point(x2, y2, 0)), Is.EqualTo(compasscourse));
		}

		[TestCase(1, 1, 0, 1, 1, 300, true)]
		[TestCase(1, 1, 0, 1, 1, 301, false)]
		[TestCase(0, 0, 0, 0, 5000, 0, true)]
		[TestCase(0, 0, 0, 0, 5001, 0, false)]
		[TestCase(0, 0, 0, 5000, 0, 0, true)]
		[TestCase(0, 0, 0, 5001, 0, 0, false)]
		public void Test_CalculateSeperation_CorrectTrueOrFalse(int x1, int y1, int alt1, int x2, int y2, int alt2, bool seperation)
		{
			Assert.That(_uut.CalculateSeperation(new Point(x1, y1, alt1), new Point(x2, y2, alt2)), Is.EqualTo(seperation));
		}

		[TestCase(10000, 10000, 500, 10000, 10000, 500, 90000, 90000, 20000, true)]
		[TestCase(90000, 90000, 20000, 10000, 10000, 500, 90000, 90000, 20000, true)]
		[TestCase(9999, 9999, 499, 10000, 10000, 500, 90000, 90000, 20000, false)]
		[TestCase(90001, 90001, 20001, 10000, 10000, 500, 90000, 90000, 20000, false)]
		[TestCase(50000, 90001, 10000, 10000, 10000, 500, 90000, 90000, 20000, false)]
		[TestCase(90001, 50000, 10000, 10000, 10000, 500, 90000, 90000, 20000, false)]
		[TestCase(50000, 50000, 200001, 10000, 10000, 500, 90000, 90000, 20000, false)]
		[TestCase(50000, 9999, 10000, 10000, 10000, 500, 90000, 90000, 20000, false)]
		[TestCase(9999, 50000, 10000, 10000, 10000, 500, 90000, 90000, 20000, false)]
		[TestCase(50000, 50000, 499, 10000, 10000, 500, 90000, 90000, 20000, false)]
		public void Test_CalculateWithInAirspace_CorrectTrueOrFalse(int x0, int y0, int alt0, int x1, int y1, int alt1, int x2, int y2, int alt2, bool withinairspace)
		{
			Assert.That(_uut.CalculateWithinAirspace(new Point(x0, y0, alt0), new Point(x1, y1, alt1), new Point(x2, y2, alt2)), Is.EqualTo(withinairspace));
		}
	}
}
