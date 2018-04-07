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
		[SetUp]
	    public void SetUp()
	    {
			_uut = new TrackFactory();
	    }

		[Test]
		public void CreateTrack_TagCorrect()
		{
			string trackinfo = "TAG12;43210;43210;12345;20000101235959999";
			Assert.That(_uut.CreateTrack(trackinfo).Tag, Is.EqualTo("TAG12"));
		}
    }
}
