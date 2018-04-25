using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using SWT35_ATM_Handin3.Interfaces;
using TransponderReceiver;

namespace SWT35_ATM_Handin3.Test.Unit
{
    [TestFixture()]
    public class TracksTest
    {
		private ICalculator _calculator;
	    private IAirspace _airspace;
        private Tracks _uut;
        private ITrack _track;
        private ITrack _track2;

        [SetUp]
        public void SetUp()
        {
	        _track = Substitute.For<ITrack>();
            _track2 = Substitute.For<ITrack>();
            _calculator = Substitute.For<ICalculator>();
	        _airspace = Substitute.For<IAirspace>();
            _uut = new Tracks(_calculator,_airspace);
        }

        [Test]
        public void Tracks_CanAdd_Track()
        {
            // Act
            _uut.Add(_track);

            // Assert
            Assert.That(_uut.FlightTracks.Contains(_track), Is.True);
        }

		
        [Test]
        public void Tracks_CanUpdate_Tracks()
        {

			//Setup additional Tracks object
			var testTracks = new Tracks();
			testTracks.FlightTracks.Add(_track);

			//Act
            _uut.Update(testTracks);

            // Assert
            _calculator.Received(1).CalculateWithinAirspace(_track.Position,null,null);
        }

        /*
        [Test]
        public void Tracks_CanUpdate_FlightTracksAddCalled()
        {

            //Setup additional Tracks object
            var testTracks = new Tracks();
            testTracks.FlightTracks.Add(_track);
            testTracks.FlightTracks.Add(_track2);

            //Act
            _uut.Update(testTracks);

            // Assert
            _calculator.Received(2).CalculateHorizontalVelocity();
            */
        }

    }
}
