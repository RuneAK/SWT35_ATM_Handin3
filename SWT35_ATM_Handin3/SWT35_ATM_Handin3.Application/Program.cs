using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWT35_ATM_Handin3.Boundary;
using SWT35_ATM_Handin3.Domain;
using SWT35_ATM_Handin3.Interfaces;
using TransponderReceiver;

namespace SWT35_ATM_Handin3.Application
{
	class Program
	{
		static void Main(string[] args)
		{
			ITransponderReceiver transponderReceiver = TransponderReceiverFactory.CreateTransponderDataReceiver();
			IFactory factory = new Factory(transponderReceiver);
			IAirspace airspace = new Airspace();
			IFilter filter = new Filter(airspace, factory);
			IUpdate update = new Update(filter);
			ILogger logger = new Logger();
			ISeparationDetector separationDetector = new SeparationDetector(update, logger);
			IDisplay display = new Display();
			ITrackRender trackRender = new TrackRender(update, display);
			ISeparationRender separationRender = new SeparationRender(separationDetector, display);

			Console.ReadKey();
		}
	}
}
