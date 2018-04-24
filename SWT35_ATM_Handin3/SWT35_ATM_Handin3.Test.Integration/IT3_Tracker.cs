using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWT35_ATM_Handin3;
using NSubstitute;
using NUnit.Framework;
using SWT35_ATM_Handin3.Interfaces;
using TransponderReceiver;

namespace SWT35_ATM_Handin3.Test.Integration
{
	public class IT3_Tracker
	{
		//Real
		private ICalculator _calculator;
		private ITrackFactory _trackFactory;
		
		//Stubs
		private ITransponderReceiver _transponderReceiver;
		private IDisplay _display;
		private ILogger _logger;

		//Units under test
		private ITracker _uut;


		[SetUp]
		public void SetUp()
		{
			_transponderReceiver = Substitute.For<ITransponderReceiver>();
			_display = Substitute.For<IDisplay>();
			_logger = Substitute.For<ILogger>();
			_calculator = new Calculator(300, 5000);
			_trackFactory = new TrackFactory(_transponderReceiver);
			_uut = new Tracker(_trackFactory, _calculator, _display, _logger);

		}

		[Test]
		public void NewTransponderData_OneTrackInfoWritten()
		{
			var testInfo = "TestTag1;20000;20000;5000;20001231235959000";
			var transpondersdata = new List<string>();
			transpondersdata.Add(testInfo);
			var args = new RawTransponderDataEventArgs(transpondersdata);

			_transponderReceiver.TransponderDataReady += Raise.EventWith(args);

			_display.Received(1).Write("Tag: TestTag1 CurrentPosition: 20000mE,20000mN Altitude: 5000m HorizontalVelocity: 0m/s CompassCourse: 0°");
		}

		[Test]
		public void NewTransponderData_SeparationInfoWritten()
		{
			var testInfo1 = "TestTag1;20000;20000;5000;20001231235959000";
			var testInfo2 = "TestTag2;20000;20000;5000;20001231235959000";
			var transpondersdata = new List<string>();
			transpondersdata.Add(testInfo1);
			transpondersdata.Add(testInfo2);
			var args = new RawTransponderDataEventArgs(transpondersdata);

			_transponderReceiver.TransponderDataReady += Raise.EventWith(args);

			_logger.Received(1).WriteToFile("TestTag1;TestTag2;31-Dec-00 23:59:59");
			_display.Received(1).WriteRed("TestTag1/TestTag2 Time: 31-Dec-00 23:59:59");
		}
	}
}
