using System;

namespace SWT35_ATM_Handin3.Interfaces
{
	public interface ISeparationDetector
	{
		event EventHandler<EventSeparation> SeparationsUpdated;
	}
}