using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NSubstitute.Core;
using NUnit.Framework;
using SWT35_ATM_Handin3.Interfaces;
using TransponderReceiver;

namespace SWT35_ATM_Handin3.Test.Unit
{
	[TestFixture]
	public class TrackerTest
	{
		private ITracks _tracks;
		private Tracker _uut;
		private ITrackFactory _trackfactory;
		private IDisplay _display;
		private ICalculator _calculator;
		private int _nEventsReceived;
		
		[SetUp]
		public void SetUp()
		{
			_nEventsReceived = 0;
			_display = Substitute.For<IDisplay>();
			_trackfactory = Substitute.For<ITrackFactory>();
			_calculator = Substitute.For<ICalculator>();
			_uut = new Tracker(_trackfactory, _calculator);
			_uut.TracksUpdated += (o, args) =>
			{
				_tracks = args.Tracks;
				++_nEventsReceived;
			};
		}
		
		[Test]
		public void Initial_TrackFactoryTracksUpdated_TracksUpdatedCorrectly()
		{
			var testTrack = new Track();
			testTrack.Tag = "´TAG12";
			testTrack.Position = new Point(12345,12345,12345);
			testTrack.Timestamp = new DateTime(2000,1,1,23,59,59,999);

			var args = new UpdateEventArgs();
			args.Tracks = new Tracks();
			args.Tracks.FlightTracks.Add(testTrack);
			_trackfactory.TracksUpdated += Raise.EventWith(args);

			Assert.That(_nEventsReceived, Is.EqualTo(1));
		}

		/*
		[Test]
		public void Initial_TransponderStringsChangedTwice_TrackFactoryCalledWithCorrectString()
		{
			var args = new RawTransponderDataEventArgs(_rawTranssponderData);
			_transponderReceiver.TransponderDataReady += Raise.EventWith(args);
			_transponderReceiver.TransponderDataReady += Raise.EventWith(args);

			//_trackfactory.Received(2).CreateTrack("test");
		}

		[Test]
		public void Initial_TransponderStringsChangedOnce_NumberOfEventsCorrect()
		{
			var args = new RawTransponderDataEventArgs(_rawTranssponderData);

			_transponderReceiver.TransponderDataReady += Raise.EventWith(args);

			Assert.That(_nEventsReceived, Is.EqualTo(1));
		}

		[Test]
		public void Initial_TransponderStringsChangedTwice_NumberOfEventsCorrect()
		{
			var args = new RawTransponderDataEventArgs(_rawTranssponderData);

			_transponderReceiver.TransponderDataReady += Raise.EventWith(args);
			_transponderReceiver.TransponderDataReady += Raise.EventWith(args);

			Assert.That(_nEventsReceived, Is.EqualTo(2));
		}
		*/
	}
}
