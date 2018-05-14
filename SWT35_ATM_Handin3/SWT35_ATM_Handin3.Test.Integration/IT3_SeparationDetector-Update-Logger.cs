using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using SWT35_ATM_Handin3.Domain;
using SWT35_ATM_Handin3.Interfaces;
using TransponderReceiver;

namespace SWT35_ATM_Handin3.Test.Integration
{
	[TestFixture()]
	public class IT3_SeparationDetector_Update_Logger
	{
		//Stubs
		private ITransponderReceiver _transponderReceiver;
		private SWT35_ATM_Handin3.Interfaces.ILogger _logger;
		//UUT
		private ISeparationDetector _uut;
		//Real
		private IFilter _filter;
		private IAirspace _airspace;
		private IFactory _factory;
		private IUpdate _update;
		private List<ISeparation> _separationList;
		private int _nEventReceived;

		[SetUp]
		public void SetUp()
		{
			//Stubs
			_transponderReceiver = Substitute.For<ITransponderReceiver>();
			_logger = Substitute.For<SWT35_ATM_Handin3.Interfaces.ILogger>();
			//Real
			_airspace = new Airspace();
			_factory = new Factory(_transponderReceiver);
			_filter = new Filter(_airspace, _factory);
			_update = new Update(_filter);
			//UUT
			_uut = new SeparationDetector(_update, _logger);

			_uut.SeparationsUpdated += (o, args) => {
				_separationList = args.SeparationData;
				++_nEventReceived;
			};

		}

		[TestCase("TAG12;43210;54321;12345;20000101235958999", "TAG21;43211;54322;12345;20000101235958999", "TAG12;43211;54321;12345;20000101235959999", "TAG21;60000;54321;15345;20000101235959999")]
		public void SeparationDetector_TransponderDataTwice_SeparationsCorrect_LoggerCalled(string trackOne, string trackTwo, string trackOneUD, string trackTwoUD)
		{
			//Arrange
			string trackOneTag = trackOne.Split(';')[0];
			string trackTwoTag = trackTwo.Split(';')[0];
			DateTime trackOneTimeStamp = DateTime.ParseExact(trackOne.Split(';')[4], "yyyyMMddHHmmssfff", null);
			var transponderData = new List<string>();
			var track1 = trackOne;
			var track2 = trackTwo;
			transponderData.Add(track1);
			transponderData.Add(track2);
			var transponderDataEventArgs = new RawTransponderDataEventArgs(transponderData);

			//Act
			_transponderReceiver.TransponderDataReady += Raise.EventWith(transponderDataEventArgs);

			//Assert
			Assert.That(_separationList.Count(), Is.EqualTo(1));
			Assert.That(_separationList[0].Tag1, Is.EqualTo(trackOneTag));
			Assert.That(_separationList[0].Tag2, Is.EqualTo(trackTwoTag));
			Assert.That(_nEventReceived, Is.EqualTo(1));
			_logger.Received(1).Log(trackOneTag + ";" + trackTwoTag + ";" + trackOneTimeStamp);

			//Re-Arrange
			transponderData = new List<string>();
			track1 = trackOneUD;
			track2 = trackTwoUD;
			transponderData.Add(track1);
			transponderData.Add(track2);
			transponderDataEventArgs = new RawTransponderDataEventArgs(transponderData);

			//Act Again
			_transponderReceiver.TransponderDataReady += Raise.EventWith(transponderDataEventArgs);

			//Assert Again
			Assert.That(_nEventReceived, Is.EqualTo(2));
			Assert.That(_separationList.Count(), Is.EqualTo(0));
		}
	}
}
