using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake2
{
    class Sneke
    {
        private readonly ConsoleColor _headColor;
        private readonly ConsoleColor _bodiColor;
        public Sneke(int initialX, int initialY, ConsoleColor headColor, ConsoleColor bodiColor, int bodi = 2)
        {
            _headColor = headColor;
            _bodiColor = bodiColor;

            Head = new Pixel(initialX, initialY, _headColor);

            for (int i = bodi; i >= 0; i--)
            {
                Bodi.Enqueue(new Pixel(x:Head.X - i - 1, y:Head.Y, _bodiColor));
            }
            Draw();
        }

        public Pixel Head  { get; private set; }
        public Queue<Pixel> Bodi { get; } = new Queue<Pixel>();

        public void Move(Direction direction, bool eat = false)
        {
            Clear();

            Bodi.Enqueue(new Pixel(Head.X, Head.Y, _bodiColor));
            if (!eat)
            {
                Bodi.Dequeue();
            }

            Head = direction switch
            {
            Direction.Up => new Pixel(x: Head.X, y: Head.Y - 1, _headColor),
            Direction.Down => new Pixel(x: Head.X, y: Head.Y + 1, _headColor),
            Direction.Left => new Pixel(x: Head.X - 1, y: Head.Y, _headColor),
            Direction.Right => new Pixel(x: Head.X + 1, y: Head.Y, _headColor),
            _ => Head
            };
            Draw();
        }

        public void Draw()
        {
            Head.Draw();
            foreach (Pixel item in Bodi)
            {
                item.Draw();
            }
        }

        public void Clear()
        {
            Head.Clear();
            foreach (Pixel item in Bodi)
            {
                item.Clear();
            }
        }
    }
}
