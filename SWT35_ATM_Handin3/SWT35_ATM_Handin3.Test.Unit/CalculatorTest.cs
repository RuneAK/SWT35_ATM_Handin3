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

		[TestCase(1,1,1,1,1,2,0)]
		public void Test_CalculateHorizontalVelocity_CalculationCorrect(int x1, int y1, int x2, int y2, int time1, int time2, double velocity)
		{
			Assert.That(_uut.CalculateHorizontalVelocity(new Point(x1, y1, 0), new Point(x2, y2, 0), new DateTime(2000, 1, 1, 1, 1, time1),
				new DateTime(2000, 1, 1, 1, 1, time2)), Is.EqualTo(velocity));
		}

		[TestCase(0,0,0,1,0)]
		public void Test_CalculateCompassCourse_CalculationCorrect(int x1, int y1, int x2, int y2, double compasscourse)
		{
			Assert.That(_uut.CalculateCompassCourse(new Point(x1, y1, 0), new Point(x2, y2, 0)), Is.EqualTo(compasscourse));
		}

		[TestCase(0, 0, 0, 1, true)]
		public void Test_CalculateSeperation_CorrectTrueOrFalse(int x1, int y1, int x2, int y2, bool seperation)
		{
			Assert.That(_uut.CalculateSeperation(new Point(x1, y1, 0), new Point(x2, y2, 0)), Is.EqualTo(seperation));
		}
	}
}
