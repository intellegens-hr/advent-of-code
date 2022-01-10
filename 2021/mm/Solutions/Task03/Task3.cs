using AdventOfCode2021.Helpers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace AdventOfCode2021.Solutions.Task3
{
    public static class Task3
    {
        public static int Solve()
        {
            StreamReader reader = new StreamReader(Constants.PREPATH + "Task3\\Input.txt");
            string line = string.Empty;

            int x = 0;
            long depth = 0;
            int aim = 0;

            // Read first line
            line = reader.ReadLine();
            var chars = line.Trim().ToCharArray();
            var countOnes = new int[line.Length];
            var countLines = 1;

            while (line != null)
            {
                for (int i = 0; i < chars.Length; i++)
                {
                    countOnes[i] += Convert.ToInt32(chars[i].ToString());
                }

                line = reader.ReadLine();
                countLines++;
                if (line != null) chars = line.Trim().ToCharArray();
            }

            countLines--;

            // calculate gamma and epsilon
            var gamma = 0;
            var epsilon = 0;
            for (int i = 0; i < countOnes.Length; i++)
            {
                if (countOnes[countOnes.Length - 1 - i] > countLines / 2)
                {
                    // Gamma weight = 1, Epsilon weight = 0;
                    gamma += Convert.ToInt32(Math.Pow(2, i));
                }
                else
                {
                    epsilon += Convert.ToInt32(Math.Pow(2, i));
                }
            }

            reader.Close();
            return gamma * epsilon;
        }

        public static int Solve2()
        {
            StreamReader reader = new StreamReader(Constants.PREPATH + "Task3\\Input.txt");
            string line = string.Empty;

            int x = 0;
            long depth = 0;
            int aim = 0;
            List<char[]> binaryNumbers = new List<char[]>();

            // Read first line
            line = reader.ReadLine();
            var chars = line.Trim().ToCharArray();
            binaryNumbers.Add(chars);
            var countOnes = new int[line.Length];

            while (line != null)
            {
                for (int i = 0; i < chars.Length; i++)
                {
                    countOnes[i] += Convert.ToInt32(chars[i].ToString());
                }

                line = reader.ReadLine();
                if (line != null)
                {
                    chars = line.Trim().ToCharArray();
                    binaryNumbers.Add(chars);
                }
            }

            List<char[]> oxygen = new List<char[]>();
            List<char[]> co2 = new List<char[]>();
            oxygen.AddRange(binaryNumbers);
            co2.AddRange(binaryNumbers);

            for (int i = 0; i < countOnes.Length; i++)
            {
                if (oxygen.Count > 1)
                {
                    var sum1oxy = oxygen.Sum(a => Convert.ToInt32(a[i].ToString()));

                    if (sum1oxy >= oxygen.Count / 2.0)
                    {
                        oxygen = oxygen.Where(a => a[i] == '1').ToList();
                    }
                    else
                    {
                        oxygen = oxygen.Where(a => a[i] == '0').ToList();
                    }
                }

                if (co2.Count > 1)
                {
                    var sum1co2 = co2.Sum(a => Convert.ToInt32(a[i].ToString()));

                    if (sum1co2 >= co2.Count / 2.0)
                    {
                        co2 = co2.Where(a => a[i] == '0').ToList();
                    }
                    else
                    {
                        co2 = co2.Where(a => a[i] == '1').ToList();
                    }
                }
            }

            var oxyArray = oxygen[0];
            var co2Array = co2[0];
            int o = 0;
            int c = 0;
            for (int i = 0; i < countOnes.Length; i++)
            {
                if (oxyArray[i] == '1') o += Convert.ToInt32(Math.Pow(2, oxyArray.Length - 1 - i));
                if (co2Array[i] == '1') c += Convert.ToInt32(Math.Pow(2, co2Array.Length - 1 - i));

            }

            reader.Close();
            return o * c;
        }
    }
}
