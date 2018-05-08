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
using ILogger = SWT35_ATM_Handin3.Interfaces.ILogger;

namespace SWT35_ATM_Handin3.Test.Unit
{
    [TestFixture()]
    public class SeparationDetectorTest
    {
        //Stubs
        private ILogger _logger;
        private IUpdate _update;
        //Fields
        private List<ISeparation> _separationData;
        private int _nEventsReceived;

        //Unit under test
        private SeparationDetector _uut;

        [SetUp]
        public void SetUp()
        {
            //Stubs
            _logger = Substitute.For<ILogger>();
            _update = Substitute.For<IUpdate>();
            //Fields

            //Unit under test
            _uut = new SeparationDetector(_update, _logger);
            _uut.SeparationsUpdated += (o, args) =>
            {
                _separationData = args.SeparationData;
                ++_nEventsReceived;
            };
        }

        [TestCase()]
        public void DetectSeparation_TwoTracksCausingSeparation_LoggerUsed()
        {

        }

        [TestCase()]
        public void DetectSeparation_TwoTracksCausingNoSeparation_CalculateSeparationFalse()
        {

        }

        [TestCase()]
        public void DetectSeparation_TwoTracksCausingSeparation_CalculateSeparationTrue()
        {

        }
    }
}
