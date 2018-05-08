using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWT35_ATM_Handin3.Interfaces;

namespace SWT35_ATM_Handin3.Domain
{
    public class Airspace : IAirspace
    {
        public Point LowerBound { get; set; }
        public Point UpperBound { get; set; }
        public Airspace(Point lowerBound = null, Point upperBound = null)
        {
            // Check if lower bounds exceeds south-west corner
            LowerBound = lowerBound ?? new Point(10000, 10000, 500);

            // Check if upper bounds exceeds north-east corner
            UpperBound = upperBound ?? new Point(90000, 90000, 20000);

            // Check that lower bound isn't greater than upper bound
            if ((UpperBound.X <= LowerBound.X) || (UpperBound.Y <= LowerBound.Y) ||
                (UpperBound.Altitude <= LowerBound.Altitude))
                throw new ArgumentException();
        }

        public bool CalculateWithinAirspace(Point trackPoint)
        {
            if (trackPoint.X < LowerBound.X || trackPoint.Y < LowerBound.Y)
                return false;
            if (trackPoint.X > UpperBound.X || trackPoint.Y > UpperBound.Y)
                return false;
            if (trackPoint.Altitude < LowerBound.Altitude || trackPoint.Altitude > UpperBound.Altitude)
                return false;
            return true;
        }
    }
}
