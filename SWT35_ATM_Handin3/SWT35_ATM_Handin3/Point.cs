using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWT35_ATM_Handin3
{
	public class Point
	{
		public Point(int xCoordinate, int yCoordinate)
		{
			XCoordinate = xCoordinate;
			YCoordinate = yCoordinate;
		}
		public int XCoordinate { get; private set; }
		public int YCoordinate { get; private set; }
	}
}
