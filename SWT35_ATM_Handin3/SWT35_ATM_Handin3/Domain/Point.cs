using SWT35_ATM_Handin3.Interfaces;

namespace SWT35_ATM_Handin3.Domain
{
	public class Point : IPoint
	{
		public int X { get; set; }
		public int Y { get; set; }
		public int Altitude { get; set; }
		public Point(int x = 0, int y = 0, int alt = 0)
		{
			X = x;
			Y = y;
			Altitude = alt;
		}
	}
}