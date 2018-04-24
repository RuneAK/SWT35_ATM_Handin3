using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWT35_ATM_Handin3;
using NSubstitute;
using NUnit.Framework;
using SWT35_ATM_Handin3.Interfaces;


namespace SWT35_ATM_Handin3.Test.Integration
{
	[TestFixture]
		public class IT1_Tracks
		{
			//Real
			private ICalculator _calculator;
			private IAirspace _airspace;
			private ITrack _testTrack1;
			private ITrack _testTrack2;

			//Stubs
			
			//Units under test
			private ITracks _uut;
		

			[SetUp]
			public void SetUp()
			{
				_calculator = new Calculator(300,5000);
				_airspace = new Airspace();
				_uut = new Tracks(_calculator,_airspace);

			}

			[Test]
			public void Update_TracksUpdatedCorrectly()
			{
				_testTrack1 = new Track
				{
					Tag = "TestTag1",
					Position = new Point(20000, 20000, 5000),
					Timestamp = new DateTime(2000, 1, 1, 12, 00, 00, 000)
				};

				_testTrack2 = new Track
				{
					Tag = "TestTag1",
					Position = new Point(20300, 20400, 5100),
					Timestamp = new DateTime(2000, 1, 1, 12, 00, 01, 000)
				};

				var testTracks = new Tracks();
				testTracks.Add(_testTrack1);
				_uut.Update(testTracks);
				testTracks = new Tracks();
				testTracks.Add(_testTrack2);
				_uut.Update(testTracks);

				Assert.That(_uut.FlightTracks[0].HorizontalVelocity, Is.EqualTo(500));
				Assert.That(_uut.FlightTracks[0].CompassCourse, Is.EqualTo(Math.Atan2(4, 3) * (180 / Math.PI)));
				Assert.That(_uut.FlightTracks[0], Is.EqualTo(_testTrack2));
				Assert.That(_uut.FlightTracks.Count, Is.EqualTo(1));
			}
			
			[Test]
			public void Update_TwoTracksUpdatedCorrectly()
			{
				_testTrack1 = new Track
				{
					Tag = "TestTag1",
					Position = new Point(20000, 20000, 5000),
					Timestamp = new DateTime(2000, 1, 1, 12, 00, 00, 000)
				};

				_testTrack2 = new Track
				{
					Tag = "TestTag2",
					Position = new Point(20300, 20400, 5100),
					Timestamp = new DateTime(2000, 1, 1, 12, 00, 01, 000)
				};

				var testTracks = new Tracks();
				testTracks.Add(_testTrack1);
				_uut.Update(testTracks);
				testTracks = new Tracks();
				testTracks.Add(_testTrack2);
				_uut.Update(testTracks);

				Assert.That(_uut.FlightTracks[0], Is.EqualTo(_testTrack1));
				Assert.That(_uut.FlightTracks[1], Is.EqualTo(_testTrack2));
			Assert.That(_uut.FlightTracks.Count, Is.EqualTo(2));
			}
	}
}
