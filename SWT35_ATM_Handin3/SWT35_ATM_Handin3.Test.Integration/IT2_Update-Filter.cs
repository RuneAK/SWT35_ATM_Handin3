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
	public class IT2_Update_Filter
	{
		//Stubs
		private ITransponderReceiver _transponderReceiver;
		//UUT
		private IUpdate _uut;
		//Real
		private IFilter _filter;
		private IAirspace _airspace;
		private IFactory _factory;
		private List<ITrack> _updatedList;
		private int _nEventReceived;

		[SetUp]
		public void SetUp()
		{
			//Stubs
			_transponderReceiver = Substitute.For<ITransponderReceiver>();
			//Real
			_airspace = new Airspace();
			_factory = new Factory(_transponderReceiver);
			_filter = new Filter(_airspace, _factory);
			//UUT
			_uut = new Update(_filter);

			_uut.TracksUpdated += (o, args) => { _updatedList = args.TrackData;
				++_nEventReceived;
			};

		}

		[TestCase("TAG12;43210;54321;12345;20000101235958999", "TAG21;90001;54321;12345;20000101235958999", "TAG12;43211;54321;12345;20000101235959999", "TAG21;90002;54321;12345;20000101235959999", 90, 1)]
		public void Update_TransponderDataTwice_UpdatedTracks(string trackWithin, string trackOutside, string trackWithinUD, string trackOutsideUD, double withinCourse, double withinVelocity)
		{
			//Arrange
			string withinTag = trackWithin.Split(';')[0];
			var transponderData = new List<string>();
			var track1 = trackWithin;
			var track2 = trackOutside;
			transponderData.Add(track1);
			transponderData.Add(track2);
			var transponderDataEventArgs = new RawTransponderDataEventArgs(transponderData);

			//Act
			_transponderReceiver.TransponderDataReady += Raise.EventWith(transponderDataEventArgs);

			//Re-Arrange
			transponderData = new List<string>();
			track1 = trackWithinUD;
			track2 = trackOutsideUD;
			transponderData.Add(track1);
			transponderData.Add(track2);
			transponderDataEventArgs = new RawTransponderDataEventArgs(transponderData);

			//Act Again
			_transponderReceiver.TransponderDataReady += Raise.EventWith(transponderDataEventArgs);

			//Assert
			Assert.That(_updatedList.Count == 1, Is.True);
			Assert.That(_updatedList[0].Tag, Is.EqualTo(withinTag));
			Assert.That(_updatedList[0].Course, Is.EqualTo(withinCourse));
			Assert.That(_updatedList[0].Velocity, Is.EqualTo(withinVelocity));
			Assert.That(_nEventReceived, Is.EqualTo(2));
		}
	}
}
