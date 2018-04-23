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
	public class SeparationEventTest
	{
		private DateTime _testTime;
		private SeparationEvent _uut;

		[SetUp]
		public void SetUp()
		{
			_testTime = new DateTime(1999, 12, 31, 23, 59, 59, 999);
		}

		[Test]
		public void Test_SeperationEvent_CanBeSet()
		{
			//Act
			_uut = new SeparationEvent("Tag1", "Tag2", _testTime);

			//Assert
			Assert.That(_uut.Tag1, Is.EqualTo("Tag1"));
			Assert.That(_uut.Tag2, Is.EqualTo("Tag2"));
			Assert.That(_uut.Time, Is.EqualTo(_testTime));
		}
	}
}
