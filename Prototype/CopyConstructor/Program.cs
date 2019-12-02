using System;

namespace CopyConstructor
{
    public class Person
    {
        public string[] Names { get; set; }
        private Address Address;

        public Person(Address address, string[] names)
        {
            Address = address;
            Names = names;
        }

        public Person(Person otherPerson)
        {
            Names = otherPerson.Names;
            Address = new Address(otherPerson.Address);
        }

        public override string ToString()
        {
            return $"Name: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
        }
    }

    public class Address
    {
        public string StreetName { get; set; }
        public int HouseNumber { get; set; }

        public Address(string streetName, int houseNumber)
        {
            StreetName = streetName;
            HouseNumber = houseNumber;
        }

        public Address(Address otherAddress)
        {
            StreetName = otherAddress.StreetName;
            HouseNumber = otherAddress.HouseNumber;
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
            var jane = new Person(john);
            jane.Names[0] = "Jane";
            Console.WriteLine(john);
            Console.WriteLine(jane);
        }
    }
}
