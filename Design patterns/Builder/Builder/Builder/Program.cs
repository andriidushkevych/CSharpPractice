using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Builder
{
    public class HtmlElement
    {
        public string Name, Text;
        public List<HtmlElement> Elements = new List<HtmlElement>();
        private const int indentSize = 2;

        public HtmlElement()
        {
            
        }

        public HtmlElement(string name, string text)
        {
            Name = name;
            Text = text;
        }

        private string ToStringImpl(int indent)
        {
            var sb = new StringBuilder();
            var i = new string(' ', indentSize * indent);
            sb.AppendLine($"{i}<{Name}>");
            if (!string.IsNullOrWhiteSpace(Text))
            {
                sb.Append(new string(' ', indentSize * (indent + 1)));
                sb.AppendLine(Text);
            }

            foreach (var el in Elements)
            {
                sb.Append(el.ToStringImpl(indent + 1));
            }
            sb.AppendLine($"{i}</{Name}>");
            return sb.ToString();
        }

        public class HtmlBuilder
        {
            private readonly string _rootName;
            HtmlElement root = new HtmlElement();

            public HtmlBuilder(string rootName)
            {
                _rootName = rootName;
                root.Name = rootName;
            }

            public void AddChild(string childName, string childText)
            {
                var e = new HtmlElement(childName, childText);
                root.Elements.Add(e);
            }

            public override string ToString()
            {
                return root.ToString();
            }

            public void Clear()
            {
                root = new HtmlElement() {Name = _rootName};
            }
        }

        public override string ToString()
        {
            return ToStringImpl(0);
        }
    }
    public class Program
    {
        static void Main(string[] args)
        {
            var elementsInList = new[] {"hello", "world"};
            var builder = new HtmlElement.HtmlBuilder("ul");
            foreach (var el in elementsInList)
            {
                builder.AddChild("li", el);
            }
            Console.WriteLine(builder);
        }
    }
}
