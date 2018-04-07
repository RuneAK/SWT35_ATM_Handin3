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
	public class TrackerTest
	{
		private Tracker _uut;
		private ITrackFactory _trackfactory;
		private ITransponderReceiver _transponderReceiver;

		[SetUp]
		public void SetUp()
		{
			_trackfactory = Substitute.For<ITrackFactory>();
			_transponderReceiver = Substitute.For<ITransponderReceiver>();
			_uut = new Tracker(_transponderReceiver, _trackfactory);
		}


	}
}
