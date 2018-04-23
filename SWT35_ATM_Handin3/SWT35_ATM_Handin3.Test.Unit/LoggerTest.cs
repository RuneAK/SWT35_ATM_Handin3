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
	[TestFixture]
	public class LoggerTest
	{

		private Logger _uut;
		private ITracker _tracker;
		private SeparationEvent _separationEvent;
		
		[SetUp]
		public void SetUp()
		{
			_separationEvent = Substitute.For<SeparationEvent>();
				//new SeparationEvent("tag1","tag2",new DateTime(1999,31,12,23,59,59,999));
			_tracker = Substitute.For<ITracker>();
			_uut = new Logger(_tracker, "tets.txt");
		}

		[Test]
		public void WriteToLogTest()
		{
			//Act
			_tracker.SeparationsUpdated += Raise.EventWith(_separationEvent);

			//Assert
			//https://stackoverflow.com/questions/12480563/c-sharp-unit-test-a-streamwriter-parameter
			//Move streamwriter out?
		}


	}
}
