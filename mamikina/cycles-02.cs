using System;

namespace Mazes
{
	public static class MazeTasks
	{
		/*
		* Не нужно писать код выхода из произвольного лабиринта. Напишите, решение для конкретных лабиринтов.
		* Используйте циклы. Ваш К.О.
		* Постарайтесь решить задачу с ограничение "не более одного цикла на метод".
		* Подумайте, какие вспомогательные методы помогут сделать ваше решение более лаконичным, понятным и красивым.
		*/



		public static void MoveOutFromEmptyMaze(Robot robot, int width, int height)
		{
            while (robot.Pos.X < (width - 2) || robot.Pos.Y < (height - 2))
		    {
                if (robot.Pos.Y < (height - 2)) robot.MoveTo(Direction.Down);
                if (robot.Pos.X < (width - 2)) robot.MoveTo(Direction.Right);
		    }
           
		}

		public static void MoveOutFromSnakeMaze(Robot robot, int width, int height)
		{
		    var extRight = width - 2;
		    var extLeft = 1;
		    var extUp = 1;
		    var extDown = height - 2;
            while (robot.Pos.X != extLeft || robot.Pos.Y != extDown)
            {

                if (robot.Pos.X < extRight && (robot.Pos.Y%4 == 1)) robot.MoveTo(Direction.Right);
                if ((robot.Pos.X == extRight || robot.Pos.X == extLeft) && (robot.Pos.Y != extDown))
                {
                    robot.MoveTo(Direction.Down);
                    robot.MoveTo(Direction.Down);
                }
                if (robot.Pos.X > extLeft && robot.Pos.Y%4 != 1) robot.MoveTo(Direction.Left);
            }

		}

		public static void MoveOutFromPyramidMaze(Robot robot, int width, int height)
		{
            var extRight = width - 2;
            var extLeft = 1;
            var extUp = 1;
            var extDown = height - 2;

            while (robot.Pos.X != 7 || robot.Pos.Y != 1)
		    {
                if (robot.Pos.X < extRight && robot.Pos.Y % 4 == 3)
                robot.MoveTo(Direction.Right);
                if (robot.Pos.X == extRight || 
                   (robot.Pos.X == (extLeft + 2) && robot.Pos.Y == 9) ||
                   (robot.Pos.X == (extRight - 2) && robot.Pos.Y == 7) ||
                   (robot.Pos.X == (extLeft + 4) && robot.Pos.Y ==5 ) ||
                   (robot.Pos.X == (extRight - 4) && robot.Pos.Y ==3 ))
                {
                    robot.MoveTo(Direction.Up);
                    robot.MoveTo(Direction.Up);
                }
                if (robot.Pos.X > extLeft && robot.Pos.Y % 4 != 3) robot.MoveTo(Direction.Left);
		    }
		}

	}
}