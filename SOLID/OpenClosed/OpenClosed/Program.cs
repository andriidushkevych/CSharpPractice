using System;
using System.Collections.Generic;
using System.Data;
using System.Net.Http.Headers;

namespace OpenClosed
{
    public interface ISpecification<T>
    {
        bool IsSatisfied(T t);
    }

    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
    }

    public class ColorSpecification : ISpecification<Product>
    {
        private Color _color;
        public ColorSpecification(Color color)
        {
            this._color = color;
        }

        public bool IsSatisfied(Product t)
        {
            return t.Color == _color;
        }
    }

    public class SizeSpecification : ISpecification<Product>
    {
        private Size _size;

        public SizeSpecification(Size size)
        {
            _size = size;
        }

        public bool IsSatisfied(Product t)
        {
            return _size == t.Size;
        }
    }

    public class CombinedSpecification<T> : ISpecification<T>
    {
        private ISpecification<T> first, second;

        public CombinedSpecification(ISpecification<T> first, ISpecification<T> second)
        {
            if (first != null) this.first = first;
            if (second != null) this.second = second;
        }

        public bool IsSatisfied(T t)
        {
            return first.IsSatisfied(t) && second.IsSatisfied(t);
        }
    }

    public class EnchancedFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
        {
            foreach (var item in items)
            {
                if (spec.IsSatisfied(item))
                    yield return item;
            }
        }
    }

    public enum Color
    {
        Red,
        Green,
        Blue
    }

    public enum Size
    {
        Small, Medium, Large
    }

    public class Product
    {
        public string Name { get; set; }
        public Color Color { get; set; }
        public Size Size { get; set; }

        public Product(string name, Color color, Size size)
        {
            Name = name;
            Color = color;
            Size = size;
        }
    }

    class ProductFilter
    {
        public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
        {
            foreach (var product in products)
            {
                if (product.Size == size)
                {
                    yield return product;
                }
            }
        }

        public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
        {
            foreach (var product in products)
            {
                if (product.Color == color)
                {
                    yield return product;
                }
            }
        }

        public IEnumerable<Product> FilterBySizeAndColor(IEnumerable<Product> products, Size size, Color color)
        {
            foreach (var product in products)
            {
                if (product.Size == size && product.Color == color)
                {
                    yield return product;
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var apple = new Product("Apple", Color.Green, Size.Small);
            var tree = new Product("Tree", Color.Green, Size.Large);
            var house = new Product("House", Color.Blue, Size.Large);

            Product[] products = {apple, tree, house};
            var pf = new ProductFilter();
            Console.WriteLine("Green products (old): ");

            foreach (var product in pf.FilterByColor(products, Color.Green))
            {
                Console.WriteLine($" - {product.Name} is {product.Color}");
            }

            Console.WriteLine("Green products (new): ");
            var ef = new EnchancedFilter();

            foreach (var product in ef.Filter(products, new ColorSpecification(Color.Green)))
            {
                Console.WriteLine($" - {product.Name} is {product.Color}");
            }

            Console.WriteLine("Large Blue products: ");

            foreach (var product in ef.Filter(products, new CombinedSpecification<Product>(new ColorSpecification(Color.Blue), new SizeSpecification(Size.Large))))
            {
                Console.WriteLine($" - {product.Name} is {product.Color} and {product.Size}");
            }

            Console.ReadKey();
        }
    }
}
