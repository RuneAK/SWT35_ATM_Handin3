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

namespace SWT35_ATM_Handin3.Test.Unit
{
    [TestFixture()]
    public class TrackRenderTest
    {
        //Stubs
        private IDisplay _display;
        private IUpdate _update;
        //Fields
        private Point _pointOne;
        private Point _pointTwo;

        //Unit under test
        private TrackRender _uut;

        [SetUp]
        public void SetUp()
        {
            //Stubs
            _display = Substitute.For<IDisplay>();
            _update = Substitute.For<IUpdate>();
            //Fields
            _pointOne = new Point(12000, 12000, 12000);
            _pointTwo = new Point(15000, 15000, 15000);

            //Unit under test
            _uut = new TrackRender(_update, _display);
        }

        [Test]
        public void RenderTracks_TwoTracks_DisplayFunctionsCalled()
        {
            //Arrange
            var trackOne = new Track("one", _pointOne, new DateTime(2000, 1, 1, 1, 1, 5));
            var trackTwo = new Track("two", _pointTwo, new DateTime(2000, 1, 1, 1, 1, 8));
            var trackList = new List<ITrack>();
            trackList.Add(trackOne);
            trackList.Add(trackTwo);
            var args = new EventTracks(trackList);

            //Act
            _update.TracksUpdated += Raise.EventWith(args);

            //Assert
            _display.Received(1).Clear();
            _display.Received(1).Write("***Tracks***");
            _display.Received(1).Write($"Tag: " + trackOne.Tag + " CurrentPosition: " + trackOne.Position.X + "mE," +
                                       trackOne.Position.Y +
                                       "mN Altitude: " + trackOne.Position.Altitude + "m Velocity: " +
                                       Math.Round(trackOne.Velocity, 2) + "m/s Course: " +
                                       Math.Round(trackOne.Course, 2) + "°");
            _display.Received(1).Write($"Tag: " + trackTwo.Tag + " CurrentPosition: " + trackTwo.Position.X + "mE," +
                                       trackTwo.Position.Y +
                                       "mN Altitude: " + trackTwo.Position.Altitude + "m Velocity: " +
                                       Math.Round(trackTwo.Velocity, 2) + "m/s Course: " +
                                       Math.Round(trackTwo.Course, 2) + "°");
        }
    }
}
