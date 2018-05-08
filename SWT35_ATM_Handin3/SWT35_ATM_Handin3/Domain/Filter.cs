using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWT35_ATM_Handin3.Boundary;
using SWT35_ATM_Handin3.Interfaces;

namespace SWT35_ATM_Handin3.Domain
{
	public class Filter : IFilter
	{
		public Filter(IAirspace airspace, IFactory factory)
		{
			factory.TracksReady += FilterTracks;
		}

		private void FilterTracks(object sender, EventTracks e)
		{
			foreach (var track in e.TrackData)
			{
				
			}
		}
	}
}
