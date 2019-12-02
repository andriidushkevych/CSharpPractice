using System;

namespace ICloneable
{
    public class Person : System.ICloneable
    {
        public string[] Names { get; set; }
        private Address Address;

        public Person(Address address, string[] names)
        {
            Address = address;
            Names = names;
        }

        public override string ToString()
        {
            return $"Name: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
        }

        public object Clone()
        {
            return new Person((Address) Address.Clone(), Names);
        }
    }

    public class Address:System.ICloneable
    {
        public string StreetName { get; set; }
        public int HouseNumber { get; set; }

        public Address(string streetName, int houseNumber)
        {
            StreetName = streetName;
            HouseNumber = houseNumber;
        }

        public override string ToString()
        {
            return $"{HouseNumber} {StreetName} ave.";
        }

        public object Clone()
        {
            return new Address(StreetName, HouseNumber);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var john = new Person(new Address("Liberty", 123), new []{"John", "Smith"} );
            var jane = (Person)john.Clone();
            jane.Names[0] = "Jane";
            Console.WriteLine(john);
            Console.WriteLine(jane);
        }
    }
}
