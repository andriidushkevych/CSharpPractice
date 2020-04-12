using System;

namespace GenericValueAdapter
{
    public interface IInteger
    {
        int Value { get; }
    }

    public static class Dimensions
    {
        public class Two : IInteger
        {
            public int Value => 2;
        }

        public class Three : IInteger
        {
            public int Value => 3;
        }
    }

    public class Vector<T, D> where D : IInteger, new()
    {
        protected T[] data;

        public Vector()
        {
            data = new T[new D().Value];
        }

        public Vector(params T[] values)
        {
            var requiredSize = new D().Value;
            data = new T[requiredSize];
            for (var i = 0; i < Math.Min(requiredSize, values.Length); i++)
            {
                data[i] = values[i];
            }
        }

        public T this[int index]
        {
            get => data[index];
            set => data[index] = value;
        }
    }

    public class Vector2i : Vector<int, Dimensions.Two>
    {
        public Vector2i()
        {
        }

        public Vector2i(params int[] values) : base(values)
        {
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var v = new Vector2i(1,3);
            var vv = new Vector2i(2,3);
        }
    }
}
