using System;

namespace SWT35_ATM_Handin3.Interfaces
{
	public interface ISeparation
	{
		string Tag1 { get; }
		string Tag2 { get; }
		DateTime TimeStamp { get; }
	}
}