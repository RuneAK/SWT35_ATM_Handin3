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
	    private ITransponderReceiver _transponderReceiver;
        private ICalculator _calculator;
        private Tracks _uut;
        private ITrack _track;

        [SetUp]
        public void SetUp()
        {
	        _transponderReceiver = Substitute.For<ITransponderReceiver>();
            _track = Substitute.For<ITrack>();
            _calculator = Substitute.For<ICalculator>();
            _uut = new Tracks(_calculator);
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
            Assert.That(_uut.FlightTracks.Contains(_track), Is.True);
		}
		
    }
}
