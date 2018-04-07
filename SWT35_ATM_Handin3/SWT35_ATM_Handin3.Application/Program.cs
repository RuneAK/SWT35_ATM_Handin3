using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWT35_ATM_Handin3.Interfaces;
using TransponderReceiver;

namespace SWT35_ATM_Handin3.Application
{
	class Program
	{
		static void Main(string[] args)
		{
			ITransponderReceiver transponderReceiver = TransponderReceiverFactory.CreateTransponderDataReceiver();
			ITrackFactory trackFactory = new TrackFactory();
			ITracker tracker = new Tracker(transponderReceiver,trackFactory);
			IDisplay display = new Display(tracker);

			Console.ReadLine();
		}
	}
}
