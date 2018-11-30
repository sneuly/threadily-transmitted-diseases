using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ThreadilyTransmittedDiseases
{
    class Program
    {
        private static readonly Dictionary<int, string> Items = new Dictionary<int, string>();
        static void Main(string[] args)
        {
            var task = Task.Factory.StartNew(AddItem);
            var task2 = Task.Factory.StartNew(AddItem);
            var task3 = Task.Factory.StartNew(AddItem);
            var task4 = Task.Factory.StartNew(AddItem);
            var task5 = Task.Factory.StartNew(AddItem);
            Task.WaitAll(task, task2, task3, task5, task4);
        }

        private static void AddItem()
        {
            lock (Items)
            {
                Console.WriteLine("Lock acquired by " + Task.CurrentId);
                Items.Add(Items.Count, "BS " + Items.Count);
            }

            lock (Items)
            {
                Console.WriteLine("Lock 2 acquired by " + Task.CurrentId);
                var readOnlyItems = Items;
                foreach (var kvp in readOnlyItems)
                {
                    Console.WriteLine($"{kvp.Key} - {kvp.Value}");
                }
            }

        }
    }
}
