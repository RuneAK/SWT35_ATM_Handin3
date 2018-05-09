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

        //Unit under test
        private SeparationDetector _uut;

        [SetUp]
        public void SetUp()
        {
            //Stubs
            _logger = Substitute.For<ILogger>();
            _update = Substitute.For<IUpdate>();
            //Fields

            //Unit under test
            _uut = new SeparationDetector(_update, _logger);
            _uut.SeparationsUpdated += (o, args) =>
            {
                _separationData = args.SeparationData;
                ++_nEventsReceived;
            };
        }

        [TestCase(10000, 10000, 10000, 10005, 10005, 10005, "one", "two")]
        public void DetectSeparation_TwoTracksCausingSeparation_CalculateSeparationTrueSeparationsAdded(int x1, int y1, int alt1, int x2, int y2, int alt2, string tag1, string tag2)
        {
            //Arrange
            Point pointOne = new Point(x1, y1, alt1);
            Point pointTwo = new Point(x2, y2, alt2);
            var trackOne = new Track(tag1, pointOne, new DateTime(2000, 1, 1, 1, 1, 1));
            var trackTwo = new Track(tag2, pointTwo, new DateTime(2000, 1, 1, 1, 1, 1));
            var trackList = new List<ITrack>();
            trackList.Add(trackOne);
            trackList.Add(trackTwo);
            var args = new EventTracks(trackList);

            //Act
            _update.TracksUpdated += Raise.EventWith(args);

            //Assert
            Assert.That(_separationData.Count, Is.EqualTo(2));
            Assert.That(_separationData[0].Tag1, Is.EqualTo(tag1));
            Assert.That(_separationData[0].Tag2, Is.EqualTo(tag2));
            Assert.That(_separationData[1].Tag1, Is.EqualTo(tag2));
            Assert.That(_separationData[1].Tag2, Is.EqualTo(tag1));
        }

        [TestCase(10000, 10000, 10000, 20000, 20000, 20000, "one", "two")]
        [TestCase(10000, 10000, 15000, 20000, 20000, 15000, "one", "two")]
        public void DetectSeparation_TwoTracksCausingNoSeparation_CalculateSeparationFalse(int x1, int y1, int alt1, int x2, int y2, int alt2, string tag1, string tag2)
        {
            //Arrange
            Point pointOne = new Point(x1, y1, alt1);
            Point pointTwo = new Point(x2, y2, alt2);
            var trackOne = new Track(tag1, pointOne, new DateTime(2000, 1, 1, 1, 1, 1));
            var trackTwo = new Track(tag2, pointTwo, new DateTime(2000, 1, 1, 1, 1, 1));
            var trackList = new List<ITrack>();
            trackList.Add(trackOne);
            trackList.Add(trackTwo);
            var args = new EventTracks(trackList);

            //Act
            _update.TracksUpdated += Raise.EventWith(args);

            //Assert
            Assert.That(_separationData.Count, Is.EqualTo(0));
        }
    }
}
