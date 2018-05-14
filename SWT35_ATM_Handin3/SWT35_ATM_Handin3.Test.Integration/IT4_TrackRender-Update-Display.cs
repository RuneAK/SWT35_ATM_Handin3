using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
	public class IT4_TrackRender_Update_Display
	{
		//Stubs
		private ITransponderReceiver _transponderReceiver;
		private SWT35_ATM_Handin3.Interfaces.ILogger _logger;
		private IDisplay _display;
		//UUT
		private ITrackRender _uut;
		//Real
		private ISeparationDetector _separationDetector;
		private IFilter _filter;
		private IAirspace _airspace;
		private IFactory _factory;
		private IUpdate _update;

		[SetUp]
		public void SetUp()
		{
			//Stubs
			_transponderReceiver = Substitute.For<ITransponderReceiver>();
			_display = Substitute.For<IDisplay>();
			_logger = Substitute.For<SWT35_ATM_Handin3.Interfaces.ILogger>();
			//Real
			_airspace = new Airspace();
			_factory = new Factory(_transponderReceiver);
			_filter = new Filter(_airspace, _factory);
			_update = new Update(_filter);
			_separationDetector = new SeparationDetector(_update, _logger);
			//UUT
			_uut = new TrackRender(_update, _display);
		}

		[TestCase("TAG12;40500;54321;12345;20000101235958999", "TAG12;90500;54321;12345;20000101235959999")]
		public void TrackRender_TrackInsideMovesOutside_RenderTrackOnce(string trackInside, string trackOutside)
		{
			//Arrange
			var transponderData = new List<string>();
			var track1 = trackInside;
			transponderData.Add(track1);
			var transponderDataEventArgs = new RawTransponderDataEventArgs(transponderData);

			//Act
			_transponderReceiver.TransponderDataReady += Raise.EventWith(transponderDataEventArgs);

			//Assert
			_display.Received(1).Write("***Tracks***");
			_display.Received(1).Write($"Tag: " + trackInside.Split(';')[0] + " CurrentPosition: " + trackInside.Split(';')[1] + "mE," +
			                           trackInside.Split(';')[2] +
			                           "mN Altitude: " + trackInside.Split(';')[3] + "m Velocity: " +
			                           "0" + "m/s Course: " +
			                           "0" + "°");

			//Re-Arrange
			transponderData = new List<string>();
			track1 = trackOutside;
			transponderData.Add(track1);
			transponderDataEventArgs = new RawTransponderDataEventArgs(transponderData);

			//Act Again
			_transponderReceiver.TransponderDataReady += Raise.EventWith(transponderDataEventArgs);

			//Assert again
			_display.Received(2).Write("***Tracks***");
			_display.DidNotReceive().Write($"Tag: " + trackOutside.Split(';')[0] + " CurrentPosition: " + trackOutside.Split(';')[1] + "mE," +
			                           trackOutside.Split(';')[2] +
			                           "mN Altitude: " + trackOutside.Split(';')[3] + "m Velocity: " +
			                           "50000" + "m/s Course: " +
			                           "90" + "°");
		}
	}
}
