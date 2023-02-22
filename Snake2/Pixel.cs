using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake2
{
    public readonly struct Pixel
    {
        public int X { get;}
        public int Y { get;}
        public ConsoleColor Color { get; }

        public int PixelSize { get;}

        public Pixel (int x, int y, ConsoleColor color)
        {
            X = x;
            Y = y;
            Color = color;
            PixelSize = Program.PixelSize;
        }

        private const char PixelChar = '█';

        public void Draw()
        {
            Console.ForegroundColor = Color;

            for (int x = 0; x < PixelSize; x++)
            {

                for (int y = 0; y < PixelSize; y++)
                {
                    Console.SetCursorPosition(left: X * PixelSize + x, top: Y * PixelSize + y);
                    Console.Write(PixelChar);
                }
            }
        }

        public void Clear()
        {
            for (int x = 0; x < PixelSize; x++)
            {

                for (int y = 0; y < PixelSize; y++)
                {
                    Console.SetCursorPosition(left: X * PixelSize + x, top: Y * PixelSize + y);
                    Console.Write(' ');
                }
            }
        }
    }
}
