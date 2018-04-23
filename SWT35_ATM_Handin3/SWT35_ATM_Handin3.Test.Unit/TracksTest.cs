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
        private TrackFactory _factory;

        [SetUp]
        public void SetUp()
        {
	        //_transponderReceiver = Substitute.For<ITransponderReceiver>();
            //_factory = new TrackFactory(_transponderReceiver);
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

		/*
        [TestCase("TAG12;43210;54321;12345;20000101235959999")]
        public void Tracks_CanUpdate_Tracks(string tag)
        {
            _track = _factory.CreateTrack(tag);
            _uut.FlightTracks.Add(_track);
            _uut.Update(_uut);

            // Assert
            Assert.That(_uut.FlightTracks.Contains(_track), Is.True);
        }
		*/
    }
}
