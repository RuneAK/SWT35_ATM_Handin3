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
	public class IT1_Filter_Factory_Airspace
	{
		//Stubs
		private ITransponderReceiver _transponderReceiver;
		//UUT
		private IFilter _uut;
		//Real
		private IAirspace _airspace;
		private IFactory _factory;
		private List<ITrack> _filteredList;

		[SetUp]
		public void SetUp()
		{
			//Stubs
			_transponderReceiver = Substitute.For<ITransponderReceiver>();
			//Real
			_airspace = new Airspace();
			_factory = new Factory(_transponderReceiver);
			//UUT
			_uut = new Filter(_airspace, _factory);

			_uut.TracksFiltered += (o, args) => { _filteredList = args.TrackData; };

		}

		[TestCase("TAG12;43210;54321;12345;20000101235959999", "TAG21;90001;54321;12345;20000101235959999")]
		public void Filter_TransponderData_FilteredTracks(string trackWithin, string trackOutside)
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

			//Assert
			Assert.That(_filteredList.Count == 1, Is.True);
			Assert.That(_filteredList[0].Tag, Is.EqualTo(withinTag));
		}
	}
}
