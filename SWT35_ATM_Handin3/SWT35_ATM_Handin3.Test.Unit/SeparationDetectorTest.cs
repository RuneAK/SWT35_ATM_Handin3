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
using ILogger = SWT35_ATM_Handin3.Interfaces.ILogger;

namespace SWT35_ATM_Handin3.Test.Unit
{
    [TestFixture()]
    public class SeparationDetectorTest
    {
        //Stubs
        private ILogger _logger;
        private IUpdate _update;
        //Fields
        private List<ISeparation> _separationData;
        private int _nEventsReceived;
	    private Point _point;

		//Unit under test
		private SeparationDetector _uut;

        [SetUp]
        public void SetUp()
        {
            //Stubs
            _logger = Substitute.For<ILogger>();
            _update = Substitute.For<IUpdate>();
			//Fields
			_point = new Point(15000, 15000, 10000);

			//Unit under test
			_uut = new SeparationDetector(_update, _logger);
            _uut.SeparationsUpdated += (o, args) =>
            {
                _separationData = args.SeparationData;
                ++_nEventsReceived;
            };
        }

        [Test]
		public void DetectSeparation_TwoTracksCausingSeparation_LoggerUsed()
        {
	        //Arrange
	        var trackOne = new Track("one", _point, new DateTime(2000, 12, 31, 23, 59, 59));
	        var trackTwo = new Track("two", _point, new DateTime(2000, 12, 31, 23, 59, 59));
	        var trackList = new List<ITrack>();
	        trackList.Add(trackOne);
	        trackList.Add(trackTwo);
	        var args = new EventTracks(trackList);

	        //Act
	        _update.TracksUpdated += Raise.EventWith(args);

	        //Assert
	        _logger.Received().Log("one;two;" + new DateTime(2000, 12, 31, 23, 59, 59));

		}

		[TestCase(0, 0, 301, false)]
        [TestCase(0, 5001, 0, false)]
        [TestCase(5001, 0, 0, false)]
		public void DetectSeparation_TwoTracksCausingNoSeparation_CalculateSeparationFalse( int x, int y, int alt, bool separation)
        {

	        //Arrange
	        var trackOne = new Track("one", _point, new DateTime(2000,12,31,23,59,59));
	        var trackTwo = new Track("two", new Point(_point.X+x, _point.Y+y, _point.Altitude+alt), new DateTime(2000, 12, 31, 23, 59, 59));
	        var trackList = new List<ITrack>();
	        trackList.Add(trackOne);
	        trackList.Add(trackTwo);
	        var args = new EventTracks(trackList);

	        //Act
	        _update.TracksUpdated += Raise.EventWith(args);

	        //Assert
	        Assert.That(_separationData.Any(), Is.EqualTo(separation));
	        Assert.That(_separationData.Count(), Is.EqualTo(0));
		}

	    [TestCase(0, 0, 300, true)]
	    [TestCase(0, 5000, 0, true)]
	    [TestCase(5000, 0, 0, true)]
        public void DetectSeparation_TwoTracksCausingSeparation_CalculateSeparationTrue(int x, int y, int alt, bool separation)
        {
	        //Arrange
	        var trackOne = new Track("one", _point, new DateTime(2000, 12, 31, 23, 59, 59));
	        var trackTwo = new Track("two", new Point(_point.X + x, _point.Y + y, _point.Altitude + alt), new DateTime(2000, 12, 31, 23, 59, 59));
	        var trackList = new List<ITrack>();
	        trackList.Add(trackOne);
	        trackList.Add(trackTwo);
	        var args = new EventTracks(trackList);

	        //Act
	        _update.TracksUpdated += Raise.EventWith(args);

	        //Assert
	        Assert.That(_separationData.Any(), Is.EqualTo(separation));
	        Assert.That(_separationData.Count(), Is.EqualTo(1));
		}
    }
}
