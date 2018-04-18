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
			ICalculator calculator = new Calculator(300,5000);
			ITracker tracker = new Tracker(transponderReceiver,trackFactory, calculator);
			IDisplay display = new Display(tracker);
			ILogger logger = new Logger(tracker,"Log.txt");

			Console.ReadLine();
		}
	}
}
