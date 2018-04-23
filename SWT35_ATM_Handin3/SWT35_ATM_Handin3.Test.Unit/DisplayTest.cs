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
	public class DisplayTest
	{
		private UpdateEventArgs _args;
		private ITracker _tracker;
		private Display _uut;

		[SetUp]
		public void Setup()
		{
			_args = Substitute.For<UpdateEventArgs>();
			_tracker = Substitute.For<ITracker>();
			_uut = new Display(_tracker);
		}

		[Test]
		public void TestOutPut()
		{
			//Act
			//_tracker.TracksUpdated += Raise.EventWith(_args);

			//Assert
			//Can console output be tested?
		}

	}
}
