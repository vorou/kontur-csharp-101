using System;

namespace Mazes
{

    public static class MazeTasks
	{
		public static void MoveOutFromEmptyMaze(Robot robot, int width, int height)
		{
		    int i = 0;
		    while (i < (height + width - 6))
		    {
		        while (i < height - 3)
		        {
		            robot.MoveTo(Direction.Down);
		            i++;
		        }
		        robot.MoveTo(Direction.Right);
		        i++;
		    }
		}

		public static void MoveOutFromSnakeMaze(Robot robot, int width, int height)
		{
		    var direction = Direction.Right;
		    var verticalPosition = height - 2;
		    while (verticalPosition > 0)
		    {
                var stepsAmount = 0;
                while (stepsAmount != width - 3)
                {
                    robot.MoveTo(direction);
                    stepsAmount++;
                }
		        if (verticalPosition != 1)
		        {
                    robot.MoveTo(Direction.Down);
                    robot.MoveTo(Direction.Down);
		        }
                if (direction == Direction.Right)
                    direction = Direction.Left;
                else direction = Direction.Right;
		        verticalPosition = verticalPosition - 2;
		    }
		}

		public static void MoveOutFromPyramidMaze(Robot robot, int width, int height)
		{
            var direction = Direction.Right;
            var verticalPosition = height - 2;

            while (verticalPosition > 0)
            {
                var stepsAmount = 0;
                while (stepsAmount != width - 3)
                {
                    robot.MoveTo(direction);
                    stepsAmount++;
                }
                if (verticalPosition != 1)
                {
                    robot.MoveTo(Direction.Up);
                    robot.MoveTo(Direction.Up);
                }
                if (direction == Direction.Right)
                    direction = Direction.Left;
                else direction = Direction.Right;
                verticalPosition = verticalPosition - 2;
                width = width - 2;
            }
		}
	}
}