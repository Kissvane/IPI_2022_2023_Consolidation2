using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CorrectionConsolidationExo2
{
    class Program
    {
        static int globalCounter = 0;
        static private readonly object counterLock = new object();

        static async Task Main(string[] args)
        {
            List<Task> tasks = new List<Task>
            {
                Counter((int)DateTime.Now.Ticks),
                Counter(1+(int)DateTime.Now.Ticks),
                Counter(2+(int)DateTime.Now.Ticks),
                Counter(3+(int)DateTime.Now.Ticks),
                Counter(4+(int)DateTime.Now.Ticks)
            };

            await Task.WhenAll(tasks);

            Console.WriteLine(globalCounter);
        }

        static async Task Counter(int seed)
        {
            Random random = new Random(seed);
            int toCount = random.Next(1, 10);
            Console.WriteLine($"Objectif {toCount}");
            for (int i = 0; i < toCount; i++)
            {
                lock (counterLock)
                {
                    globalCounter++;
                }
                await Task.Delay(100);
            }

        }
    }
}
