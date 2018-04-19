using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWT35_ATM_Handin3.Helpers;

namespace SWT35_ATM_Handin3
{
    public class Airspace
    {
        public Boundary LowerBound { get; set; }
        public Boundary UpperBound { get; set; }

        public Airspace(Boundary lowerBound, Boundary upperBound)
        {
            // Check if lower bounds exceeds south-west corner
            LowerBound = lowerBound ?? new Boundary(10000, 10000, 500);

            // Error checks for lower bound?
            
            // Check if upper bounds exceeds north-east corner
            UpperBound = upperBound ?? new Boundary(90000, 90000, 20000);

            // Error checks for upper bound?

            // Check that lower bound isn't greater than upper bound
            if((UpperBound.X <= LowerBound.X) || (UpperBound.Y <= LowerBound.Y) ||
               (UpperBound.Alt <= LowerBound.Alt))
                throw new ArgumentException();
        }
    }   
}
