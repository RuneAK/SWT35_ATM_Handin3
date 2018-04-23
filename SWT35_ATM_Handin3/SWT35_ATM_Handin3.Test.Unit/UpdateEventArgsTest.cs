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
    public class UpdateEventArgsTest
    {
		private ITracks _tracks;
	    private SeparationEvent _separationEvent;
		private UpdateEventArgs _uut;

	    [SetUp]
	    public void SetUp()
	    {
		    _separationEvent = Substitute.For<SeparationEvent>("TAG1", "TAG2", new DateTime(1999,12,31,23,59,59,999));
		    _tracks = Substitute.For<ITracks>();
			_uut = new UpdateEventArgs();
	    }

	    [Test]
	    public void Test_UpdateEventArgs_CanBeSet()
	    {
			//Act
		    _uut.Tracks = _tracks;
		    _uut.SeparationEvents = new List<SeparationEvent>();
			_uut.SeparationEvents.Add(_separationEvent);

			//Assert
			Assert.That(_uut.Tracks, Is.EqualTo(_tracks));
			Assert.That(_uut.SeparationEvents[0], Is.EqualTo(_separationEvent));

	    }
    }
}
