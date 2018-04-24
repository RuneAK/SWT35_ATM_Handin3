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
		private ILogger _logger;
		private ITracks _tracks;
		private Tracker _uut;
		private ITrackFactory _trackfactory;
		private IDisplay _display;
		private ICalculator _calculator;
		private int _nEventsReceived;
		private UpdateEventArgs _testTrackEvent;
		
		[SetUp]
		public void SetUp()
		{
			_display = Substitute.For<IDisplay>();
			_trackfactory = Substitute.For<ITrackFactory>();
			_calculator = Substitute.For<ICalculator>();
			_logger = Substitute.For<ILogger>();

		    _testTrackEvent = new UpdateEventArgs();
            _testTrackEvent.Tracks = new Tracks();
            
			_uut = new Tracker(_trackfactory, _calculator, _display, _logger);			
		}
		
		[Test]
		public void Test_TrackFactoryRaisesEvent_TrackingIsCalled()
		{
			_trackfactory.TracksUpdated += Raise.EventWith(_testTrackEvent);           
            _display.Received(1).Clear();
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
