using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NSubstitute;
using NUnit.Framework;
using SWT35_ATM_Handin3.Interfaces;
using TransponderReceiver;

namespace SWT35_ATM_Handin3.Test.Unit
{
	[TestFixture]
	public class DisplayTest
	{
		private Display _uut;
		private ITracker _tracker;
		
		[SetUp]
		public void SetUp()
		{
			_tracker = Substitute.For<ITracker>();
			_uut = new Display(_tracker);
		}
	}
}
