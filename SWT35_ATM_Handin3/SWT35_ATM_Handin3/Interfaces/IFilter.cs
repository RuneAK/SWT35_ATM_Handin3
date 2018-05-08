using System;
using SWT35_ATM_Handin3.Boundary;

namespace SWT35_ATM_Handin3.Domain
{
	public interface IFilter
	{
		event EventHandler<EventTracks> TracksFiltered;
	}
}