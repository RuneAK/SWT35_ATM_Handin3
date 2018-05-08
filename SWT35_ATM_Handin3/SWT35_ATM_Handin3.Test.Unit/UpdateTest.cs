using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using SWT35_ATM_Handin3.Boundary;
using SWT35_ATM_Handin3.Domain;
using SWT35_ATM_Handin3.Interfaces;

namespace SWT35_ATM_Handin3.Test.Unit
{
    [TestFixture()]
    public class UpdateTest
    {
        //Stubs
        private IFilter _filter;

        //Unit under test
        private Update _uut;

        //Fields
        private List<ITrack> _trackList;
        private int _nEventsReceived;
        private Point _withinOne;
        private Point _withinTwo;

        [SetUp]
        public void SetUp()
        {
            //Stubs
            _filter = Substitute.For<IFilter>();

            //Fields
            _withinOne = new Point(15000, 15000, 10000);
            _withinTwo = new Point(20000, 20000, 12000);

            //Unit under test
            _uut = new Update(_filter);
            _uut.TracksUpdated += (o, args) =>
            {
                _trackList = args.TrackData;
                ++_nEventsReceived;
            };
        }

        [Test]
        public void UpdateTracks_ListOfTracksNotOnOldTracks_ListAdded()
        {
            //Arrange
            var trackOne = new Track("one", _withinOne, DateTime.Now);
            var trackTwo = new Track("two", _withinTwo, DateTime.Now);
            var trackList = new List<ITrack>();
            trackList.Add(trackOne);
            trackList.Add(trackTwo);
            var args = new EventTracks(trackList);

            //Act
            _filter.TracksFiltered += Raise.EventWith(args);

            //Assert
            Assert.That(_trackList.Count(), Is.EqualTo(2));
            Assert.That(_trackList.Contains(trackOne));
            Assert.That(_trackList.Contains(trackTwo));
        }

		
        [TestCase(0, 0, 0, double.NaN)]
        [TestCase(0, 1, 0, 0)]
        [TestCase(1, 0, 0, 90)]
        [TestCase(0, -1, 0, 180)]
        [TestCase(-1, 0, 0, 270)]
        [TestCase(1, 1, 0, 45)]
        [TestCase(1, -1, 0, 135)]
        [TestCase(-1, -1, 0, 225)]
        [TestCase(-1, 1, 0, 315)]
		public void CalculateCourse_ListOfTracksAlreadyOnOldTracks_CourseIsCorrect(int x, int y, int alt, double course)
        {
            //Arrange
            var trackOne = new Track("one", _withinOne,  DateTime.Now);
            var trackList = new List<ITrack>();
            trackList.Add(trackOne);
            var args = new EventTracks(trackList);

            //Act
            _filter.TracksFiltered += Raise.EventWith(args);

            //Arrange again
			trackOne = new Track("one", new Point(_withinOne.X+x, _withinOne.Y+y, _withinOne.Altitude+alt), DateTime.Now);
			trackList = new List<ITrack>();
            trackList.Add(trackOne);
            args = new EventTracks(trackList);

            //Act again
            _filter.TracksFiltered += Raise.EventWith(args);

            //Assert
            Assert.That(_trackList.Count(), Is.EqualTo(1));
            Assert.That(_trackList.Contains(trackOne), Is.True);
            Assert.That(_trackList[0].Course, Is.EqualTo(course));
		}

		[TestCase(0, 1, 0, 1, 2, 1)]
        [TestCase(1, 0, 0, 1, 2, 1)]
        [TestCase(0, 2, 0, 1, 2, 2)]
        [TestCase(2, 0, 0, 1, 2, 2)]
        [TestCase(0, 4, 0, 1, 5, 1)]
        [TestCase(4, 0, 0, 1, 5, 1)]
        public void CalculateVelocity_ListOfTracksAlreadyOnOldTracks_VelocityIsCorrect(int x, int y, int alt, int time1, int time2, double velocity)
        {
            //Arrange
            var trackOne = new Track("one", _withinOne, new DateTime(2000, 1, 1, 1, 1, time1));
            var trackList = new List<ITrack>();
            trackList.Add(trackOne);
            var args = new EventTracks(trackList);

            //Act
            _filter.TracksFiltered += Raise.EventWith(args);

            //Arrange again
            trackOne = new Track("one", new Point(_withinOne.X+x, _withinOne.Y+y, _withinOne.Altitude+alt), new DateTime(2000, 1, 1, 1, 1, time2));
			trackList = new List<ITrack>();
            trackList.Add(trackOne);
            args = new EventTracks(trackList);

            //Act again
            _filter.TracksFiltered += Raise.EventWith(args);

            //Assert
            Assert.That(_trackList.Count(), Is.EqualTo(1));
            Assert.That(_trackList.Contains(trackOne), Is.True);
            Assert.That(_trackList[0].Velocity, Is.EqualTo(velocity));
        }
	}
}
