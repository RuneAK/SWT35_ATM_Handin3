﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWT35_ATM_Handin3.Boundary;
using TransponderReceiver;

namespace SWT35_ATM_Handin3.Interfaces
{
	public interface IFactory
	{
		event EventHandler<EventTracks> TracksReady;
    }
}
