using System;

namespace InnerFactory
{
    public class Point
    {
        private double x, y;

        private Point(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public static class Factory
        {
            public static Point CartesianPoint(double x, double y)
            {
                return new Point(x, y);
            }

            public static Point PolarPoint(double rho, double theta)
            {
                return new Point(rho * Math.Cos(theta), rho * Math.Sin(theta));
            }
        }

        public static Point Origin => new Point(0, 0);

        public static Point OriginSingleInstance = new Point(0, 0);

        public override string ToString()
        {
            return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var point = Point.Factory.PolarPoint(1, 1);
            Console.WriteLine(point);
        }
    }
}
