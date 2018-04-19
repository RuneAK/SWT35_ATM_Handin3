using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using SWT35_ATM_Handin3;

namespace SWT35_ATM_Handin3.Test.Unit
{
	[TestFixture]
	public class TrackFactoryTest
	{
		private TrackFactory _uut;
		private string transsponderTestString;
		[SetUp]
	    public void SetUp()
	    {
			_uut = new TrackFactory();
		    transsponderTestString = "TAG12;43210;54321;12345;20000101235959999";

	    }

		[Test]
		public void CreateTrack_TagCorrect()
		{
			Assert.That(_uut.CreateTrack(transsponderTestString).Tag, Is.EqualTo("TAG12"));
		}

		[Test]
		public void CreateTrack_XCoordinateCorrect()
		{
			Assert.That(_uut.CreateTrack(transsponderTestString).Position.XCoordinate, Is.EqualTo(43210));
		}

        [Test]
		public void CreateTrack_YCoordinateCorrect()
		{
			Assert.That(_uut.CreateTrack(transsponderTestString).Position.YCoordinate, Is.EqualTo(54321));
		}

        [Test]
		public void CreateTrack_AltitudeCorrect()
		{
			Assert.That(_uut.CreateTrack(transsponderTestString).Altitude, Is.EqualTo(12345));
		}

        [Test]
		public void CreateTrack_TimestampCorrect()
		{
			Assert.That(_uut.CreateTrack(transsponderTestString).Timestamp, Is.EqualTo(new DateTime(2000, 01, 01, 23, 59,59,999)));
		}
	}
}
