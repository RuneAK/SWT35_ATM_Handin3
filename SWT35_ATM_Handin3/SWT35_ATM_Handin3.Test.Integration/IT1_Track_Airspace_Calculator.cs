 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWT35_ATM_Handin3;
using NSubstitute;
using NUnit.Framework;
 using SWT35_ATM_Handin3.Interfaces;
 using TransponderReceiver;

namespace SWT35_ATM_Handin3.Test.Integration
{
    [TestFixture]
    public class IT1_Track_Airspace_Calculator
    {
        //Stubs
        private ITrackFactory _trackFactory;
        private ITracks _tracks;
        private ISeperationRepository _seperationRepository;
        private ITracker _tracker;

        //Real
        private ITransponderReceiver _transponderReceiver;

        //Units under test
        private ITrack _uut_track;
        private IAirspace _uut_airspace;
        private ICalculator _uut_calculator;

        [SetUp]
        public void SetUp()
        {
            //Stubs
            _trackFactory = Substitute.For<ITrackFactory>();
            _tracks = Substitute.For<ITracks>();
            _seperationRepository = Substitute.For<ISeperationRepository>();
            _tracker = Substitute.For<ITracker>();

            //Real
            _transponderReceiver = TransponderReceiverFactory.CreateTransponderDataReceiver();

            //Units under test
            _uut_track = new Track();
            _uut_airspace = new Airspace();
            _uut_calculator = new Calculator(300, 5000);
        }

        [Test]
        public void FirstFunctionToTest()
        {

        }
    }
}
