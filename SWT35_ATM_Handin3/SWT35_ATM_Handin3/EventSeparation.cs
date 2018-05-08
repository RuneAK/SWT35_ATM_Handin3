using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWT35_ATM_Handin3.Interfaces;

namespace SWT35_ATM_Handin3
{
	public class EventSeparation : EventArgs
	{
		public List<ISeparation> SeparationData;
		public EventSeparation(List<ISeparation> separations)
		{
			SeparationData = separations;
		}
	}
}
