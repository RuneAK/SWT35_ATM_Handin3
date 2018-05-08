using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using SWT35_ATM_Handin3.Boundary;
using SWT35_ATM_Handin3.Domain;
using SWT35_ATM_Handin3.Interfaces;

namespace SWT35_ATM_Handin3.Test.Unit
{
    [TestFixture()]
    public class FilterTest
    {
        //Unit under test
        private Filter _uut;
        //Stubs
        private IAirspace _airspace;
        private IFactory _factory;
        private List<ITrack> _trackList;
        //Fields
        private int _nEventsReceived;
        private Point _within;
        private Point _outside;

        [SetUp]
        public void SetUp()
        {
            //Fields
            _within = new Point(15000, 15000, 10000);
            _outside = new Point(1, 1, 1);

            //Stubs
            _airspace = Substitute.For<IAirspace>();
            _airspace.CalculateWithinAirspace(_within).Returns(true);
            _airspace.CalculateWithinAirspace(_outside).Returns(false);
            _factory = Substitute.For<IFactory>();

            //Unit under test
            _uut = new Filter(_airspace, _factory);
            _uut.TracksFiltered += (o, args) =>
            {
                _trackList = args.TrackData;
                ++_nEventsReceived;
            };
        }

        [Test]
        public void FilterTracks_ListWithTracksBothWithinAirspaceAndOutsideAirspace_ListOfTracksOnlyWithinAirspace()
        {
            //Arrange
            var trackWithinAirspace = new Track("one", _within, DateTime.Now);
            var trackOutsideAirspace = new Track("two", _outside, DateTime.Now);
            var trackList = new List<ITrack>();
            trackList.Add(trackWithinAirspace);
            trackList.Add(trackOutsideAirspace);
            var args = new EventTracks(trackList);

            //Act
            _factory.TracksReady += Raise.EventWith(args);

            //Assert
            Assert.That(_trackList.Contains(trackWithinAirspace), Is.True);
            Assert.That(_trackList.Contains(trackOutsideAirspace), Is.False);
        }
    }
}
