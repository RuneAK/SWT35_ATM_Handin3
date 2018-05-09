using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWT35_ATM_Handin3.Boundary;
using SWT35_ATM_Handin3.Interfaces;

namespace SWT35_ATM_Handin3.Domain
{
	public class SeparationDetector : ISeparationDetector
	{
		private List<ISeparation> _oldSeparations;
		private ILogger _logger;
		public event EventHandler<EventSeparation> SeparationsUpdated;

		public SeparationDetector(IUpdate update, ILogger logger)
		{
			_logger = logger;
			_oldSeparations = new List<ISeparation>();
			update.TracksUpdated += DetectSeparations;
		}

		private void DetectSeparations(object sender, EventTracks e)
		{
			var updatedSeparations = new List<ISeparation>();

			foreach (var trackOne in e.TrackData)
			{
				foreach (var trackTwo in e.TrackData)
				{
					if (trackOne.Tag != trackTwo.Tag)
					{
						if (CalculateSeparation(trackOne.Position, trackTwo.Position))
						{
							var oldSeparations = updatedSeparations.FirstOrDefault(s => s.Tag1 == trackOne.Tag || s.Tag2 == trackOne.Tag);
							if (oldSeparations == null)
								updatedSeparations.Add(new Separation(trackOne.Tag, trackTwo.Tag, trackOne.Timestamp));
						}
					}
				}
			}

			foreach (var sep in updatedSeparations)
			{
				var oldSeparation = _oldSeparations.FirstOrDefault(s => s.Tag1 == sep.Tag1 && s.Tag2 == sep.Tag2);
				if (oldSeparation == null)
					_logger.Log(sep.Tag1 + ";" + sep.Tag2 + ";" + sep.TimeStamp);

			}

			_oldSeparations = updatedSeparations;
			UpdatedSeparationEvent(new EventSeparation(updatedSeparations));
		}

		protected  virtual void UpdatedSeparationEvent(EventSeparation e)
		{
			SeparationsUpdated?.Invoke(this, e);
		}

		private bool CalculateSeparation(Point trackOne, Point trackTwo)
		{
			if (Math.Abs(trackOne.Altitude - trackTwo.Altitude) <= 300)
			{
				var xsqr = Math.Pow(trackOne.X - trackTwo.X, 2);
				var ysqr = Math.Pow(trackOne.Y - trackTwo.Y, 2);
				if (Math.Sqrt(xsqr + ysqr) <= 5000)
					return true;
				return false;
			}
			return false;
		}
	}
}
