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
		public class IT2_Tracks
		{
			//Real
			private ICalculator _calculator;
			private IAirspace _airspace;
			private ITrack _testTrack1;
			private ITrack _testTrack2;

			//Stubs
			private IDisplay _display;
			private ILogger _logger;
			private ISeperationRepository _seperationRepository;
			private ITrackFactory _trackFactory;
			private ITracker _tracker;

			//Units under test
			private ITracks _uut;
		

			[SetUp]
			public void SetUp()
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

				_calculator = new Calculator(300,5000);
				_airspace = new Airspace();
				_uut = new Tracks(_calculator,_airspace);

				_display = Substitute.For<IDisplay>();
				_logger = Substitute.For<ILogger>();
				_s

			}

			[Test]
			public void FirstFunctionToTest()
			{
				var testTracks = new Tracks();
				testTracks.Add(_testTrack1);
				_uut.Update(testTracks);
				testTracks = new Tracks();
				testTracks.Add(_testTrack2);
				_uut.Update(testTracks);
			}
		}
}
