using System;

namespace FacetedBuilder
{
    public class Person
    {
        //address
        public string Street, Postcode, City;

        //employment
        public string CompanyName, Position;
        public int AnnualIncome;

        public override string ToString()
        {
            return $"{nameof(Street)}: {Street}, {nameof(Postcode)}: {Postcode}, {nameof(City)}: {City}, {nameof(CompanyName)}: {CompanyName}, {nameof(Position)}: {Position}, {nameof(AnnualIncome)}: {AnnualIncome}";
        }
    }

    public class PersonBuilder //facade
    {
        protected Person person = new Person();

        public PersonJobBuilder Works => new PersonJobBuilder(person);
        public PersonAddressBuilder Lives => new PersonAddressBuilder(person);

        public static implicit operator Person(PersonBuilder pb)
        {
            return pb.person;
        }
    }

    public class PersonJobBuilder : PersonBuilder
    {
        public PersonJobBuilder(Person person)
        {
            this.person = person;
        }

        public PersonJobBuilder At(string companyName)
        {
            person.CompanyName = companyName;
            return this;
        }

        public PersonJobBuilder AsA(string position)
        {
            person.Position = position;
            return this;
        }

        public PersonJobBuilder Makes(int amount)
        {
            person.AnnualIncome = amount;
            return this;
        }
    }

    public class PersonAddressBuilder : PersonBuilder
    {
        public PersonAddressBuilder(Person person)
        {
            this.person = person;
        }
        public PersonAddressBuilder Street(string street)
        {
            person.Street = street;
            return this;
        }

        public PersonAddressBuilder City(string city)
        {
            person.City = city;
            return this;
        }

        public PersonAddressBuilder Postcode(string postcode)
        {
            person.Postcode = postcode;
            return this;
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            var pb = new PersonBuilder();
            Person person = pb
                .Works.At("Logisense").AsA("Developer").Makes(10000000)
                .Lives.Street("111 Victoria").City("Kitchener").Postcode("N2A2h5");
            Console.WriteLine(person);
        }
    }
}
