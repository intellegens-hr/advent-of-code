using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Tasks._02
{
    public class Task_02
    {
        public static List<(char, char)> Loses = new List<(char, char)>()
        {
            ('A', 'Z'),
            ('B', 'X'),
            ('C', 'Y')
        };
        public static List<(char, char)> Draws = new List<(char, char)>()
        {
            ('A', 'X'),
            ('B', 'Y'),
            ('C', 'Z')
        };
        public static List<(char, char)> Wins = new List<(char, char)>()
        {
            ('A', 'Y'),
            ('B', 'Z'),
            ('C', 'X')
        };
        public static Dictionary<char, int> Points = new Dictionary<char, int>()
        {
            {'X', 1 },
            {'Y', 2 },
            {'Z', 3 }
        };
        public static int WinPoints = 6;
        public static int DrawPoints = 3;


        public static int PartOne()
        {
            using var reader = new StreamReader(@"..\..\..\Tasks\02\PartOne.txt");
            string line = String.Empty;
            int s = 0;
            while ((line = reader.ReadLine()) != null)
            {
                var parts = line.Split(' ');
                char one = parts[0][0];
                char two = parts[1][0];

                s += Points[two];


                if (Loses.Contains((one, two)))
                {
                    // We lost
                }
                else if (Draws.Contains((one, two)))
                {
                    // We drew
                    s += DrawPoints;
                }
                else
                {
                    // We won
                    s += WinPoints;
                }
            }

            return s;
        }

        public static int PartTwo()
        {
            using var reader = new StreamReader(@"..\..\..\Tasks\02\PartOne.txt");
            string line = String.Empty;
            int s = 0;
            int[] max = { 0, 0, 0, 0 };
            while ((line = reader.ReadLine()) != null)
            {
                var parts = line.Split(' ');
                char one = parts[0][0];
                char two = parts[1][0];

                if (two == 'X')
                {
                    // We need to lose
                    s += Points[Loses.First(a => a.Item1 == one).Item2];
                }
                else if (two == 'Y')
                {
                    // We need to draw
                    s += DrawPoints;
                    s += Points[Draws.First(a => a.Item1 == one).Item2];

                }
                else
                {
                    // We need to win
                    s += WinPoints;
                    s += Points[Wins.First(a => a.Item1 == one).Item2];

                }
            }

            return s;
        }
    }
}
