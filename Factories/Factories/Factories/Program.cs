using System;

namespace FactoryMethod
{
    public class Point
    {
        public static Point CartesianPoint(double x, double y)
        {
            return new Point(x, y);
        }

        public static Point PolarPoint(double rho, double theta)
        {
            return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
        }

        private double x, y;

        private Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var point = Point.PolarPoint(1.0, Math.PI / 2);
            var point2 = Point.CartesianPoint(3, 15.2);
            Console.WriteLine(point);
            Console.WriteLine(point2);
        }
    }
}
