using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWT35_ATM_Handin3.Domain;

namespace SWT35_ATM_Handin3.Interfaces
{
    public interface IAirspace
    {
        Point LowerBound { get; }
        Point UpperBound { get; }
        bool CalculateWithinAirspace(Point trackPoint);
    }
}
