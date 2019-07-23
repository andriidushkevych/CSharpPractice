using System;

namespace FluentBuilder
{
    public class Person
    {
        public string Name { get; set; }
        public string Position { get; set; }

        public class Builder : PersonJobBuilder<Builder>
        {
            
        }

        public static Builder New => new Builder();

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
        }
    }

    public abstract class PersonBuilder
    {
        protected Person person = new Person();

        public Person Build()
        {
            return person;
        }
    }

    public class PersonInfoBuilder<Self> : PersonBuilder where Self : PersonInfoBuilder<Self>
    {
        public Self Called(string name)
        {
            person.Name = name;
            return (Self)this;
        }
    }

    public class PersonJobBuilder<Self> : PersonInfoBuilder<PersonJobBuilder<Self>> where Self : PersonJobBuilder<Self>
    {
        public Self WorksAsA(string position)
        {
            person.Position = position;
            return (Self)this;
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            var me = Person.New.Called("Andrii").WorksAsA("Developer").Build();
            Console.WriteLine(me);
        }
    }
}
