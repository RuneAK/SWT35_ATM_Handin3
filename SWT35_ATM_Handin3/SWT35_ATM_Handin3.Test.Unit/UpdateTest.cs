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
    }
}
