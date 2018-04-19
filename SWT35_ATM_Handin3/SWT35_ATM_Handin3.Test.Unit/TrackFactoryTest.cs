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

	    [TestCase("TAG12;43210;54321;12345;20000101235959999")]
	    [TestCase("0;88888;22222;400;19991224120500122")]
        public void CreateTrack_CanCreate(string info)
	    {
	        Assert.That(_uut.CreateTrack(info).Tag, Is.EqualTo(info.Split(';')[0]));
	        Assert.That(_uut.CreateTrack(info).Position.X, Is.EqualTo(Int32.Parse(info.Split(';')[1])));
	        Assert.That(_uut.CreateTrack(info).Position.Y, Is.EqualTo(Int32.Parse(info.Split(';')[2])));
	        Assert.That(_uut.CreateTrack(info).Position.Alt, Is.EqualTo(Int32.Parse(info.Split(';')[3])));
	        Assert.That(_uut.CreateTrack(info).Timestamp, Is.EqualTo(DateTime.ParseExact(info.Split(';')[4], "yyyyMMddHHmmssfff", System.Globalization.CultureInfo.InvariantCulture)));
        }

		[Test]
		public void CreateTrack_TagCorrect()
		{
			Assert.That(_uut.CreateTrack(transsponderTestString).Tag, Is.EqualTo("TAG12"));
		}

		[Test]
		public void CreateTrack_XCoordinateCorrect()
		{
			Assert.That(_uut.CreateTrack(transsponderTestString).Position.X, Is.EqualTo(43210));
		}

        [Test]
		public void CreateTrack_YCoordinateCorrect()
		{
			Assert.That(_uut.CreateTrack(transsponderTestString).Position.Y, Is.EqualTo(54321));
		}

        [Test]
		public void CreateTrack_AltitudeCorrect()
		{
			Assert.That(_uut.CreateTrack(transsponderTestString).Position.Alt, Is.EqualTo(12345));
		}

        [Test]
		public void CreateTrack_TimestampCorrect()
		{
			Assert.That(_uut.CreateTrack(transsponderTestString).Timestamp, Is.EqualTo(new DateTime(2000, 01, 01, 23, 59,59,999)));
		}
	}
}
