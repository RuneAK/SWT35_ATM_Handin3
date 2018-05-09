using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using NUnit.Framework.Internal;
using SWT35_ATM_Handin3.Boundary;
using SWT35_ATM_Handin3.Domain;
using SWT35_ATM_Handin3.Interfaces;
using TransponderReceiver;

namespace SWT35_ATM_Handin3.Test.Unit
{
    [TestFixture()]
    public class FactoryTest
    {
        //Stubs
        private ITransponderReceiver _transponderReceiver;
        //Unit under test
        private Factory _uut;
        //Fields
        private List<ITrack> _trackList;
        private int _nEventsReceived;

        [SetUp]
        public void SetUp()
        {
            _transponderReceiver = Substitute.For<ITransponderReceiver>();
            _uut = new Factory(_transponderReceiver);

            _uut.TracksReady += (o, args) =>
            {
                _trackList = args.TrackData;
                ++_nEventsReceived;
            };
        }

        [TestCase("TAG12;43210;54321;12345;20000101235959999")]
        public void CreateTracks_NewTrackAdded_ListContainsTrack(string info)
        {
            //Arrange
            var transponderData = new List<string>();
            transponderData.Add(info);
            var args = new RawTransponderDataEventArgs(transponderData);

            //Act
            _transponderReceiver.TransponderDataReady += Raise.EventWith(args);

            //Assert
            Assert.That(_trackList[0].Tag, Is.EqualTo(info.Split(';')[0]));
            Assert.That(_trackList[0].Position.X, Is.EqualTo(Int32.Parse(info.Split(';')[1])));
            Assert.That(_trackList[0].Position.Y, Is.EqualTo(Int32.Parse(info.Split(';')[2])));
            Assert.That(_trackList[0].Position.Altitude, Is.EqualTo(Int32.Parse(info.Split(';')[3])));
            Assert.That(_trackList[0].Timestamp,
                Is.EqualTo(DateTime.ParseExact(info.Split(';')[4], "yyyyMMddHHmmssfff",
                    null)));
            Assert.That(_nEventsReceived, Is.EqualTo(1));
        }
    }
}
