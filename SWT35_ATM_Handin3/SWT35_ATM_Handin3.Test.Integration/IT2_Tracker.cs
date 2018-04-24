using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using SWT35_ATM_Handin3;
using SWT35_ATM_Handin3.Interfaces;
using TransponderReceiver;

namespace SWT35_ATM_Handin3.Test.Integration
{
    [TestFixture]
    public class IT2_Tracker
    {
        //Real
        private ICalculator _calculator;
        private IAirspace _airspace;
        private ITrack _testTrack1;
        private ITrack _testTrack2;
        private ITracks _tracks;

        //Stubs
        private ITransponderReceiver _transponderReceiver;
        private ITracker _tracker;
        private ISeperationRepository _seperationRepository;
        private IDisplay _display;
        private ILogger _logger;

        //Units under test
        private ITrackFactory _uut;

        [SetUp]
        public void SetUp()
        {
            //Real
            _testTrack1 = new Track
            {
                Tag = "TestTag1",
                Position = new Point(20000, 20000, 5000),
                Timestamp = new DateTime(2000, 1, 1, 12, 00, 00, 000)
            };

            _testTrack2 = new Track
            {
                Tag = "TestTag1",
                Position = new Point(20300, 20400, 5100),
                Timestamp = new DateTime(2000, 1, 1, 12, 00, 01, 000)
            };
            _calculator = new Calculator(300, 5000);
            _airspace = new Airspace();
            _tracks = new Tracks(_calculator, _airspace);

            //Stubs
            _display = Substitute.For<IDisplay>();
            _logger = Substitute.For<ILogger>();           
            _transponderReceiver = Substitute.For<ITransponderReceiver>();
            _tracker = Substitute.For<ITracker>();
            //UUT
            _uut = new TrackFactory(_transponderReceiver);            
        }

        [Test]
        public void TrackFactory_NewTrack_AddCalled()
        {
            var transponderdata = new List<string>
            {
                "TAG12;43210;54321;12345;20000101235959999"
            };
            var args = new RawTransponderDataEventArgs(transponderdata);

            //Act
            _transponderReceiver.TransponderDataReady += Raise.EventWith(args);

            //Assert
            
        }
    }
}
