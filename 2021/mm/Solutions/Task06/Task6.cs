using AdventOfCode2021.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AdventOfCode2021.Solutions.Task6
{
    public static class Task6
    {
        public static int Solve()
        {
            StreamReader reader = new StreamReader(Constants.PREPATH + "Task6\\Input.txt");
            List<Fish> fish = reader.ReadLine().Split(',').Select(x => new Fish(Convert.ToInt32(x))).ToList();
            reader.Close();

            for (int i = 0; i < 80; i++)
            {
                for (int j = 0; j < fish.Count; j++)
                {
                    var f = fish[j];
                    if (f.Days == 0)
                    {
                        f.Days = 6;
                        fish.Add(new Fish(9));  // 9 because it will reduce to 8 for that fish in step 0;
                    } else
                    {
                        f.Days--;
                    }
                }
            }

            return fish.Count();
        }

        public static long Solve2()
        {
            StreamReader reader = new StreamReader(Constants.PREPATH + "Task6\\Input.txt");
            List<Fish> fish = reader.ReadLine().Split(',').Select(x => new Fish(Convert.ToInt32(x))).ToList();
            reader.Close();

            long[] toSpawn = new long[9];
            long sum = fish.Count;

            foreach (var f in fish)
            {
                toSpawn[f.Days]++;
            }

            for (int i = 0; i < 256; i++)
            {
                // Switch all toSpawns one place down
                var spawning = toSpawn[0];
                for (int j = 0; j < 8; j++)
                {
                    toSpawn[j] = toSpawn[j + 1];
                }
                toSpawn[8] = 0;

                // For each fish that spawns today... add an instance that spawns in 6 days and and an instance that spawns in 8 days
                sum += spawning;
                toSpawn[6] += spawning;
                toSpawn[8] = spawning;
            }

            return sum;
        }


    }
}
