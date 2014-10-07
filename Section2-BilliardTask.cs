using System;

namespace Billiards
{
	public class BilliardTasks
	{
		public double BounceVerticalWall(double directionRadians)
		{
		    var BounceVerticalWall = Math.PI - directionRadians;
			return BounceVerticalWall;
		}
		public double BounceHorizontalWall(double directionRadians)
		{
		    var BounceHorizontalWall = directionRadians*(-1);
			return BounceHorizontalWall;
		}

		public double BounceWall(double directionRadians, double wallInclanationRadians)
		{
		    var BounceWall = 2*wallInclanationRadians - directionRadians;
			return BounceWall;
		}

	}
}