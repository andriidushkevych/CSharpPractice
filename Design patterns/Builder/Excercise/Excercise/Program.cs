using System;
using System.Collections.Generic;
using System.Text;

namespace Excercise
{
    public class CodeBuilder
    {
        public string ClassName;
        public List<ClassField> Fields = new List<ClassField>();

        public CodeBuilder(string className)
        {
            ClassName = className;
        }

        public CodeBuilder AddField(string name, string type)
        {
            Fields.Add(new ClassField(name, type));
            return this;
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"public class {ClassName}");
            sb.AppendLine("{");
            foreach (var classField in Fields)
            {
                sb.AppendLine(classField.ToString());
            }
            sb.AppendLine("}");
            return sb.ToString();
        }
    }

    public class ClassField
    {
        public string Name, Type;

        public ClassField(string name, string type)
        {
            Name = name;
            Type = type;
        }

        public override string ToString()
        {
            return $"  public {Type} {Name};";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            CodeBuilder cb = new CodeBuilder("Person").AddField("Name", "string").AddField("Age", "int");
            Console.WriteLine(cb);
            Console.ReadKey();
        }
    }
}
