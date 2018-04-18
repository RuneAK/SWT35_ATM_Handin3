using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT35_ATM_Handin3
{
    public class Airspace
    {
        public Boundary CoordinateX { get; set; }
        public Boundary CoordinateY { get; set; }
        public Boundary Altitude { get; set; }
    }

    public class Boundary
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Alt { get; set; }

        public Boundary()
        {
        }
    }
}
