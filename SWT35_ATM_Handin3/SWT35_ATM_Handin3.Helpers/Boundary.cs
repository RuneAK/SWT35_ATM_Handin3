using System;

namespace SWT35_ATM_Handin3.Helpers
{
    public class Boundary
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Alt { get; set; }

        public Boundary(int x = 0, int y = 0, int alt = 0)
        {
            X = x;
            Y = y;
            Alt = alt;
        }
    }
}