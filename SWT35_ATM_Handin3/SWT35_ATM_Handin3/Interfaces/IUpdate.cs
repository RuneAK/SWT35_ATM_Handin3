using System;
using SWT35_ATM_Handin3.Boundary;

namespace SWT35_ATM_Handin3.Interfaces
{
	public interface IUpdate
	{
		event EventHandler<EventTracks> TracksUpdated;
	}
}