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
		[TestCase(2, 2, 1, 1, 225)]
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
	}
}
