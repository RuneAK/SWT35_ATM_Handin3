using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;

namespace SWT35_ATM_Handin3.Test.Unit
{
    [TestFixture()]
    public class SeparationRepositoryTest
    {
        private SeparationRepository _uut;
        private SeparationEvent _separationEvent;

	    [SetUp]
	    public void SetUp()
	    {
            _uut = new SeparationRepository();
            _separationEvent = new SeparationEvent("Test1", "Test2", new DateTime(1999, 12, 31, 12, 00, 00));
	    }

        [Test]
        public void Test_GetSepEvent_ReturnsSeperation()
        {
            _uut.AddSeperationEvent(_separationEvent);

            Assert.That(_uut.Get(_separationEvent), Is.EqualTo(_separationEvent));
        }

        [Test]
        public void Test_GetString1String2_ReturnsSeperation()
        {
            _uut.AddSeperationEvent(_separationEvent);

            Assert.That(_uut.Get(_separationEvent.Tag1, _separationEvent.Tag2), Is.EqualTo(_separationEvent));
        }

        [Test]
        public void Test_GetAll_ReturnsSeperationList()
        {
            _uut.AddSeperationEvent(_separationEvent);

            Assert.That(_uut.GetAll().Contains(_separationEvent) && _uut.GetAll().Count == 1);
        }

        [Test]
        public void Test_Delete_SeparationListDoesntContainAndEmpty()
        {
            _uut.AddSeperationEvent(_separationEvent);
            _uut.DeleteSeperationEvent(_separationEvent);
            Assert.That(!_uut.GetAll().Contains(_separationEvent) && _uut.GetAll().Count == 0);
        }
    }
}
