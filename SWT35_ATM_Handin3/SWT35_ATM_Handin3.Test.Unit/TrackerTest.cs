using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using SWT35_ATM_Handin3.Interfaces;
using TransponderReceiver;

namespace SWT35_ATM_Handin3.Test.Unit
{
	[TestFixture]
	public class TrackerTest
	{
		private Tracker _uut;
		private ITrackFactory _trackfactory;
		private ITransponderReceiver _transponderReceiver;
		private int _nEventsReceived;
		private List<string> _rawTranssponderData;
		[SetUp]
		public void SetUp()
		{
			_nEventsReceived = 0;
			_rawTranssponderData = new List<string>();
			_rawTranssponderData.Add("test");

			_trackfactory = Substitute.For<ITrackFactory>();
			_transponderReceiver = Substitute.For<ITransponderReceiver>();
			_uut = new Tracker(_transponderReceiver, _trackfactory);
			_uut.TracksUpdated += (o, args) =>
			{
				++_nEventsReceived;
			};
		}

		[Test]
		public void Initial_TransponderStringsChangedOnce_TrackFactoryCalledWithCorrectString()
		{
			var args = new RawTransponderDataEventArgs(_rawTranssponderData);

			_transponderReceiver.TransponderDataReady += Raise.EventWith(args);

			_trackfactory.Received(1).CreateTrack("test");
		}

		[Test]
		public void Initial_TransponderStringsChangedTwice_TrackFactoryCalledWithCorrectString()
		{
			var args = new RawTransponderDataEventArgs(_rawTranssponderData);
			_transponderReceiver.TransponderDataReady += Raise.EventWith(args);
			_transponderReceiver.TransponderDataReady += Raise.EventWith(args);

			_trackfactory.Received(2).CreateTrack("test");
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

	}
}
