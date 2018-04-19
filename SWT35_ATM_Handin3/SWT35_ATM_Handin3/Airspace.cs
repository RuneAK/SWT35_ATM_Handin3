using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT35_ATM_Handin3
{
    public class Airspace
    {
        public Point LowerBound { get; set; }
        public Point UpperBound { get; set; }

        public Airspace(Point lowerBound = null, Point upperBound = null)
        {
            // Check if lower bounds exceeds south-west corner
            LowerBound = lowerBound ?? new Point(10000, 10000, 500);

            // Error checks for lower bound?
            
            // Check if upper bounds exceeds north-east corner
            UpperBound = upperBound ?? new Point(90000, 90000, 20000);

            // Error checks for upper bound?

            // Check that lower bound isn't greater than upper bound
            if((UpperBound.X <= LowerBound.X) || (UpperBound.Y <= LowerBound.Y) ||
               (UpperBound.Alt <= LowerBound.Alt))
                throw new ArgumentException();
        }
    }   
}
