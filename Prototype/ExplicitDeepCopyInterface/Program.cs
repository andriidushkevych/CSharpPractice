using System;

namespace ExplicitDeepCopyInterface
{
    public interface IPrototype<T>
    {
        T DeepCopy();
    }
    public class Person : IPrototype<Person>
    {
        public string[] Names { get; set; }
        private Address Address;

        public Person(Address address, string[] names)
        {
            Address = address;
            Names = names;
        }

        public Person DeepCopy()
        {
            return new Person(Address.DeepCopy(), Names);
        }

        public override string ToString()
        {
            return $"Name: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
        }
    }

    public class Address:IPrototype<Address>
    {
        public string StreetName { get; set; }
        public int HouseNumber { get; set; }

        public Address(string streetName, int houseNumber)
        {
            StreetName = streetName;
            HouseNumber = houseNumber;
        }

        public Address DeepCopy()
        {
            return new Address(StreetName, HouseNumber);
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
            Console.WriteLine(john);
            Console.WriteLine(jane);
        }
    }
}
