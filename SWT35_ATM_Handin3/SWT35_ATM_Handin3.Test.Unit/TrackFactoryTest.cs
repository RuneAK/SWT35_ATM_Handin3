using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;
using SWT35_ATM_Handin3;
using SWT35_ATM_Handin3.Interfaces;
using TransponderReceiver;

namespace SWT35_ATM_Handin3.Test.Unit
{
	[TestFixture]
	public class TrackFactoryTest
	{
		private ITracks _tracks;
		private int _nEventsReceived;
		private TrackFactory _uut;
		private ITransponderReceiver _transponderReceiver;

		[SetUp]
		public void SetUp()
		{
			_transponderReceiver = Substitute.For<ITransponderReceiver>();
			_uut = new TrackFactory(_transponderReceiver);

			_uut.TracksUpdated += (o, args) =>
			{
				_tracks = args.Tracks;
				++_nEventsReceived;
			};
		}

		[TestCase("TAG12;43210;54321;12345;20000101235959999")]
		[TestCase("0;88888;22222;400;19991224120500122")]
		public void Initial_TransponderStringsChangedOnce_TrackDataCorrect(string info)
		{
			//Setup transponderdata
			var transpondersdata = new List<string>();
			transpondersdata.Add(info);
			var args = new RawTransponderDataEventArgs(transpondersdata);

			//Act
			_transponderReceiver.TransponderDataReady += Raise.EventWith(args);

			//Assert
			Assert.That(_tracks.FlightTracks[0].Tag, Is.EqualTo(info.Split(';')[0]));
			Assert.That(_tracks.FlightTracks[0].Position.X, Is.EqualTo(Int32.Parse(info.Split(';')[1])));
			Assert.That(_tracks.FlightTracks[0].Position.Y, Is.EqualTo(Int32.Parse(info.Split(';')[2])));
			Assert.That(_tracks.FlightTracks[0].Position.Alt, Is.EqualTo(Int32.Parse(info.Split(';')[3])));
			Assert.That(_tracks.FlightTracks[0].Timestamp,
				Is.EqualTo(DateTime.ParseExact(info.Split(';')[4], "yyyyMMddHHmmssfff",
					System.Globalization.CultureInfo.InvariantCulture)));

		}

		[Test]
		public void Initial_TransponderStringsChangedTwoTimes_CalledTwoTimes()
		{
			_nEventsReceived = 0;

			//Setup transponderdata
			var transpondersdata = new List<string>();
			transpondersdata.Add("0;88888;22222;400;19991224120500122");
			var args = new RawTransponderDataEventArgs(transpondersdata);

			//Act
			_transponderReceiver.TransponderDataReady += Raise.EventWith(args);
			_transponderReceiver.TransponderDataReady += Raise.EventWith(args);

			//Assert
			Assert.That(_nEventsReceived, Is.EqualTo(2));

		}

		[Test]
		public void Initial_TwoTransponderStringsChangedOnce_NumberOfTracksCorrect()
		{
			//Setup transponderdata
			var transpondersdata = new List<string>();
			transpondersdata.Add("0;88888;22222;400;19991224120500122");
			transpondersdata.Add("0;88888;22222;400;19991224120500122");
			var args = new RawTransponderDataEventArgs(transpondersdata);

			//Act
			_transponderReceiver.TransponderDataReady += Raise.EventWith(args);

			//Assert
			Assert.That(_tracks.FlightTracks.Count, Is.EqualTo(2));

		}
	}
}
