using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Singleton
{
    public interface IDatabase
    {
        int GetPopulation(string name);
    }

    public class SingletonDatabase : IDatabase
    {
        private Dictionary<string, int> cities;

        private SingletonDatabase()
        {
            cities = new Dictionary<string, int>()
            {
                {"Kitchener", 200000},
                {"Waterloo", 300000}
            };
        }
        private static Lazy<SingletonDatabase> instance = new Lazy<SingletonDatabase>(() => new SingletonDatabase());
        public static SingletonDatabase Instance => instance.Value;

        public int GetPopulation(string name)
        {
            return cities[name];
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var db = SingletonDatabase.Instance;
            Console.WriteLine(db.GetPopulation("Kitchener"));
        }
    }
}
