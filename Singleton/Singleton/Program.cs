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
        private static int instanceCount;
        public static int Count => instanceCount;
        private SingletonDatabase()
        {
            instanceCount++;
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

    public class SingletonRecordFinder
    {
        public int GetTotalPopulation(IEnumerable<string> names)
        {
            int result = 0;
            foreach (var name in names)
            {
                result += SingletonDatabase.Instance.GetPopulation(name);
            }

            return result;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var db = SingletonDatabase.Instance;
            var db2 = SingletonDatabase.Instance;
            Console.WriteLine(db.GetPopulation("Kitchener"));
            Console.WriteLine($"db.Equals(db2): {db.Equals(db2)}");
            Console.WriteLine($"SingletonDatabase.Count: {SingletonDatabase.Count}");
            var rf = new SingletonRecordFinder();
            Console.WriteLine($"Total population of Kitchener and Waterloo: {rf.GetTotalPopulation(new [] { "Kitchener", "Waterloo" })}");
        }
    }
}
