using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using SWT35_ATM_Handin3.Interfaces;

namespace SWT35_ATM_Handin3.Test.Unit
{
    [TestFixture()]
    public class TracksTest
    {
        private ICalculator _calculator;
        private Tracks _uut;
        private ITrack _track;

        [SetUp]
        public void SetUp()
        {
            _track = Substitute.For<ITrack>();
            _calculator = Substitute.For<ICalculator>();
            _uut = new Tracks(_calculator);
        }

        [TestCase()]
        public void Tracks_CanAdd_Track(ITrack track)
        {

        }
    }
}
