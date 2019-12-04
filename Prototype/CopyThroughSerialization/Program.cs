using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace CopyThroughSerialization
{
    public static class ExtensionMethods
    {
        public static T DeepCopy<T>(this T self)
        {
            var stream = new MemoryStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, self);
            stream.Seek(0, SeekOrigin.Begin);
            object copy = formatter.Deserialize(stream);
            stream.Close();
            return (T) copy;
        }

        public static T DeepCopyXml<T>(this T self)
        {
            var ms = new MemoryStream();
            var serializer = new XmlSerializer(typeof(T));
            serializer.Serialize(ms, self);
            ms.Position = 0;
            object copy = serializer.Deserialize(ms);
            ms.Close();
            return (T) copy;
        }
    }

    [Serializable]
    public class Person
    {
        public string[] Names { get; set; }
        private Address Address;

        public Person()
        {
            
        }

        public Person(Address address, string[] names)
        {
            Address = address;
            Names = names;
        }

        public override string ToString()
        {
            return $"Name: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
        }
    }

    [Serializable]
    public class Address
    {
        public string StreetName { get; set; }
        public int HouseNumber { get; set; }

        public Address()
        {
            
        }

        public Address(string streetName, int houseNumber)
        {
            StreetName = streetName;
            HouseNumber = houseNumber;
        }

        public override string ToString()
        {
            return $"{HouseNumber} {StreetName} ave.";
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var john = new Person(new Address("Liberty", 123), new[] { "John", "Smith" });
            var jane = john.DeepCopy();
            jane.Names[0] = "Jane";
            var alex = jane.DeepCopyXml();
            alex.Names[0] = "Alex";
            alex.Names[1] = "Read";
            Console.WriteLine(john);
            Console.WriteLine(jane);
            Console.WriteLine(alex);
        }
    }
}
