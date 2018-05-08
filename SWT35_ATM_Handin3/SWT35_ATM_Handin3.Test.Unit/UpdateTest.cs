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
        public void CalculateCourse_ListOfTracksAlreadyOnOldTracks_CourseIsCorrect(int x1, int y1, int alt1, double course)
        {
            //Arrange
            var trackOne = new Track("one", _withinOne,  DateTime.Now);
            var trackList = new List<ITrack>();
            trackList.Add(trackOne);
            var args = new EventTracks(trackList);

            //Act
            _filter.TracksFiltered += Raise.EventWith(args);

            //Arrange again
            trackList.Remove(trackOne);
            _withinOne.X = _withinOne.X + x1;
            _withinOne.Y = _withinOne.Y + y1;
            _withinOne.Altitude = _withinOne.Altitude + alt1;
            trackOne = new Track("one", _withinOne, DateTime.Now);
            trackList.Add(trackOne);
            args = new EventTracks(trackList);

            //Act again
            _filter.TracksFiltered += Raise.EventWith(args);

            //Assert
            Assert.That(_trackList.Count(), Is.EqualTo(1));
            Assert.That(_trackList.Contains(trackOne), Is.True);
            Assert.That(_trackList[0].Course, Is.EqualTo(course));
        }
    }
}
