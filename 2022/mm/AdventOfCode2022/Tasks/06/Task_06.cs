using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Tasks._06
{
    public class Task_06
    {
        public static int PartOne(int n)
        {
            using var reader = new StreamReader(@"..\..\..\Tasks\06\PartOne.txt");
            //using var reader = new StreamReader(@"..\..\..\Tasks\06\Example.txt");
            string line = String.Empty;
            int s = 0;
            Queue<char> queue = new Queue<char>();

            while ((line = reader.ReadLine()) != null)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    if (queue.Contains(line[i]))
                    {
                        char toRemove = queue.Dequeue();
                        while (toRemove != line[i]) toRemove = queue.Dequeue();
                    }
                    queue.Enqueue(line[i]);
                    if (queue.Count == n) return i + 1;

                }
            }
            return s;
        }

    }
}
