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
    public class IT2_TrackFactory
    {
        //Real
        private ITrack _track;
        private ITracks _tracks;

        //Stubs
        private ITransponderReceiver _transponderReceiver;

        //Units under test
        private ITrackFactory _uut;

        [SetUp]
        public void SetUp()
        {
            //Stubs
            _transponderReceiver = Substitute.For<ITransponderReceiver>();
            //UUT
            _uut = new TrackFactory(_transponderReceiver);
        }

        [Test]
        public void TrackFactory_NewTrack_AddCalled()
        {
            List<string> testList = new List<string>();
            testList.Add("TAG12;43210;54321;12345;20000101235959999");
            _transponderReceiver.TransponderDataReady += (sender, args) => 
        }
    }
}
