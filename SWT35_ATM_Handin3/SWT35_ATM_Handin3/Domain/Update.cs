using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWT35_ATM_Handin3.Boundary;
using SWT35_ATM_Handin3.Interfaces;

namespace SWT35_ATM_Handin3.Domain
{
	public class Update : IUpdate
	{
		public event EventHandler<EventTracks> TracksUpdated;
		private List<ITrack> _oldTracks;
		public Update(IFilter filter)
		{
            _oldTracks = new List<ITrack>();
			filter.TracksFiltered += UpdateTracks;
		}

		private void UpdateTracks(object sender, EventTracks e)
		{
			var updatedTracks = new List<ITrack>();

				foreach (var track in e.TrackData)
				{
					var oldTrack = _oldTracks.FirstOrDefault(t => t.Tag == track.Tag);
					if (oldTrack != null)
					{
						track.Course = CalculateCourse(oldTrack.Position, track.Position);
						track.Velocity = CalculateVelocity(oldTrack.Position, track.Position, oldTrack.Timestamp, track.Timestamp);
					}
				    updatedTracks.Add(track);
                }

			_oldTracks = updatedTracks;
			UpdatedTrackEvent(new EventTracks(updatedTracks));
		}

		protected virtual void UpdatedTrackEvent(EventTracks e)
		{
			TracksUpdated?.Invoke(this, e);
		}

		private double CalculateCourse(Point oldPoint, Point newPoint)
		{
			double Y = Math.Abs(newPoint.Y - oldPoint.Y);
			double X = Math.Abs(newPoint.X - oldPoint.X);
			double compassCourse = Math.Atan2(Y, X) * (180 / Math.PI);

			//No direction
			if (X == 0 && Y == 0)
			{
				return Double.NaN;
			}
			//NorthSouth-Direction
			else if (X == 0)
			{
				//South
				if (newPoint.Y < oldPoint.Y)
					compassCourse = 180;
				//North
				else
					compassCourse = 0;
			}
			//EastWest-Direction
			else if (Y == 0)
			{
				//West
				if (newPoint.X < oldPoint.X)
					compassCourse = 270;
				//East
				else
					compassCourse = 90;
			}
			//West
			else if (newPoint.X < oldPoint.X)
			{
				//South
				if (newPoint.Y < oldPoint.Y)
					compassCourse += 180;
				//North
				else
					compassCourse += 270;
			}
			//East
			else if (newPoint.X > oldPoint.X)
			{
				//South
				if (newPoint.Y < oldPoint.Y)
					compassCourse += 90;
			}
			return compassCourse;
		}

		private double CalculateVelocity(Point oldPoint, Point newPoint, DateTime oldTimestamp, DateTime newTimestamp)
		{
			TimeSpan diff = newTimestamp.Subtract(oldTimestamp);
			var timeDifference = diff.TotalSeconds;
			var distance = Math.Sqrt(Math.Pow((newPoint.X - oldPoint.X), 2) + Math.Pow((newPoint.Y - oldPoint.Y), 2));
			var velocity = distance / timeDifference;

			return velocity;
		}
	}
}
