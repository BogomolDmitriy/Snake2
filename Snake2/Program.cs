using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using static System.Console;

namespace Snake2
{
    class Program
    {
        private const int MapWidth = 30;
        private const int MapHeight = 20;
        public const int PixelSize = 2;

        private const ConsoleColor BorderColor = ConsoleColor.Gray;
        private const ConsoleColor HeadColor = ConsoleColor.Blue;
        private const ConsoleColor BodiColor = ConsoleColor.Green;
        private const ConsoleColor FoodColor = ConsoleColor.Red;

        private static readonly Random Rand = new Random();

        private const int FrameMs = 200;
        static void Main()
        {
            SetWindowSize(MapWidth* PixelSize, MapHeight* PixelSize);
            SetBufferSize(MapWidth* PixelSize, MapHeight* PixelSize);
            CursorVisible = false;

            while (true)
            {
                startGeme();
                Thread.Sleep(700);
            }
        }

        static void startGeme()
        {
            int levl = 0;
            int count = 0;
            int lag = 0;
            Clear();
            DrowBorder();

            Direction currentMov = Direction.Right;

            Sneke sneke = new Sneke(initialX: (int)(MapWidth / 2), initialY: (int)(MapHeight / 2 - 1), headColor: HeadColor, bodiColor: BodiColor);

            Pixel food = Food(sneke);
            food.Draw();

            Stopwatch sw = new Stopwatch();

            while (true)
            {
                sw.Restart();
                Direction oldMov = currentMov;

                while (sw.ElapsedMilliseconds <= FrameMs - levl - lag)
                {
                    if (currentMov == oldMov)
                    {
                        currentMov = ReadMovment(currentMov);
                    }
                }



                if (sneke.Head.X ==  food.X && sneke.Head.Y == food.Y)
                {
                    sneke.Move(currentMov, true);

                    food = Food(sneke);
                    food.Draw();
                    count++;
                    levl = levl + 5;
                }

                else
                {
                    sneke.Move(currentMov);
                }


                if (sneke.Head.X == MapWidth - 1
                    || sneke.Head.X == 0
                    || sneke.Head.Y == MapHeight - 1
                    || sneke.Head.Y == 0
                    || sneke.Bodi.Any(b => b.X == sneke.Head.X && b.Y == sneke.Head.Y))
                {
                    break;
                }

                lag = (int)sw.ElapsedMilliseconds;
            }
            sneke.Clear();
            SetCursorPosition(left: (MapWidth * PixelSize) / 3, top: (MapHeight * PixelSize) / 2);
            WriteLine($"Game over - count = {count}");
        }

        static Pixel Food(Sneke sneke)
        {
            Pixel food;

            do
            {
                food = new Pixel(x: Rand.Next(1, MapWidth - 2), y: Rand.Next(1, MapHeight - 2), color: FoodColor);
            } while (sneke.Head.X == food.X && sneke.Head.Y == food.Y
            || sneke.Bodi.Any(b => b.X == food.X && b.Y == food.Y));
            return food;
        }

        static Direction ReadMovment(Direction curentDir)
        {
            if(!KeyAvailable)
                return curentDir;

            ConsoleKey key = ReadKey(intercept: true).Key;

            curentDir = key switch
            {
                ConsoleKey.UpArrow when curentDir != Direction.Down => Direction.Up,
                ConsoleKey.DownArrow when curentDir != Direction.Up => Direction.Down,
                ConsoleKey.LeftArrow when curentDir != Direction.Right => Direction.Left,
                ConsoleKey.RightArrow when curentDir != Direction.Left => Direction.Right,
                _ => curentDir
            };

            return curentDir;
        }
        static void DrowBorder()
        {
            for (int i = 0; i < MapWidth; i++)
            {
                new Pixel(x:i, y:0, BorderColor).Draw();
                new Pixel(x:i, y:MapHeight-1, BorderColor).Draw();
            }

            for (int i = 1; i < MapHeight-1; i++)
            {
                new Pixel(x: 0, y: i, BorderColor).Draw();
                new Pixel(x: MapWidth-1, y: i, BorderColor).Draw();
            }
        }
    }
}
