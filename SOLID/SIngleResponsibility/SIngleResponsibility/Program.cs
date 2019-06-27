using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace SIngleResponsibility
{
    class Program
    {
        public class Journal
        {
            private readonly List<string> entries = new List<string>();
            private static int count = 0;

            public int AddEntry(string text)
            {
                entries.Add($"{++count}: {text}");
                return count;
            }

            public void RemoveEntry(int index)
            {
                entries.RemoveAt(index);
            }

            public override string ToString()
            {
                return string.Join(Environment.NewLine, entries);
            }
        }

        public class JournalHelper
        {
            public static void SaveToFile(Journal j, string filename, bool overwrite = false)
            {
                if (overwrite || !File.Exists(filename))
                    File.WriteAllText(filename, j.ToString());
            }
        }

        static void Main(string[] args)
        {
            var filename = "test.txt";
            var j = new Journal();
            j.AddEntry("Entry 1");
            j.AddEntry("Entry 2");
            Console.WriteLine(j);
            JournalHelper.SaveToFile(j, filename, true);
            Console.ReadKey();
        }
    }
}
