using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace DesignPatterns.SOLID
{
    public class Journal
    {
        private readonly List<string> _entries = new List<string>();

        private static uint _count = 0;

        public uint AddEntry(string text)
        {
            _entries.Add(++_count + ": " + text);
            return _count; // memento
        }

        public void RemoveEntry(int index)
        {
            _entries.RemoveAt(index);
        }

        public override string ToString()
        {
            return string.Join(Environment.NewLine, _entries);
        }
    }

    public class Persistence
    {
        public void SaveToFile(Journal journal, string filename, bool overwrite = false)
        {
            if (overwrite || !File.Exists(filename))
            {
                File.WriteAllText(filename, journal.ToString());
            }
        }
    }

    public class Demo
    {
        static void Main(string[] args)
        {
            var j = new Journal();
            j.AddEntry("2019 is coming.");
            j.AddEntry("Kept myself hydrated");
            Console.WriteLine(j);

            var p = new Persistence();
            var f = @"C:\Temp\journal.txt";
            p.SaveToFile(j, f, true);

            Process.Start(f);
        }
    }
}