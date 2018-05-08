using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWT35_ATM_Handin3.Interfaces;

namespace SWT35_ATM_Handin3.Domain
{
	class SeparationRender : ISeparationRender
	{
		private IDisplay _display;
		public SeparationRender(ISeparationDetector detector, IDisplay display)
		{
			detector.SeparationsUpdated += RenderSeparations;
		}

		private void RenderSeparations(object sender, EventSeparation e)
		{
			_display.Write("***Separations***");
			foreach (var sep in e.SeparationData)
			{
				var str = "Tag1: " + sep.Tag1 + " Tag2: " + sep.Tag2 + sep.TimeStamp;
				_display.Write(str);
			}
		}
	}
}
