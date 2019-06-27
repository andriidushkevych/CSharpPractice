using System;
using System.Net.NetworkInformation;

namespace LiskovSubstitution
{
    class Program
    {
        public class Rectangle
        {
            public virtual int Width { get; set; }
            public virtual int Height { get; set; }

            public Rectangle()
            {
                
            }

            public Rectangle(int width, int height)
            {
                Width = width;
                Height = height;
            }

            public override string ToString()
            {
                return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
            }
        }

        public class Square:Rectangle
        {
            public override int Width
            {
                set { base.Width = base.Height = value; }
            }
            public override int Height
            {
                set { base.Height = base.Width = value; }
            }
        }

        public static int Area(Rectangle rectangle) => rectangle.Width * rectangle.Height;

        static void Main(string[] args)
        {
            Rectangle rc = new Rectangle(2,3);
            Console.WriteLine($"{rc} has area of: {Area(rc)}");
            Rectangle square = new Square();
            square.Width = 4;
            Console.WriteLine($"{square} has area of: {Area(square)}");
            Console.ReadKey();
        }
    }
}
