using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;

namespace SWT35_ATM_Handin3.Test.Unit
{
	[TestFixture()]
	public class TrackTest
	{
		private DateTime _testTime;
		private Point _testPoint;
		private Track _uut;

		[SetUp]
		public void SetUp()
		{
			_testTime = new DateTime(1999, 12, 31, 23, 59, 59, 999);
			_testPoint = new Point(1,2,3);
			_uut = new Track();
		}

		[Test]
		public void Test_Track_CanBeSet()
		{
			//Act
			_uut.Tag = "TestTag";
			_uut.Position = _testPoint;
			_uut.Timestamp = _testTime;
			_uut.CompassCourse = 1.1;
			_uut.HorizontalVelocity = 1.1;

			//Assert
			Assert.That(_uut.Tag, Is.EqualTo("TestTag"));
			Assert.That(_uut.Position, Is.EqualTo(_testPoint));
			Assert.That(_uut.Timestamp, Is.EqualTo(_testTime));
			Assert.That(_uut.CompassCourse, Is.EqualTo(1.1));
			Assert.That(_uut.HorizontalVelocity, Is.EqualTo(1.1));
		}
	}
}
