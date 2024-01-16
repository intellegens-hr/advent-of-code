using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2023.Day2
{
    internal class Day2
    {
        const string path = "C:\\Users\\nikol\\source\\VisualStudio\\AdventOfCode\\2023\\AdventOfCode2023\\AdventOfCode2023\\Day2\\games1.txt";

        public int CountValidGames()
        {
            int validGameIDSum = 0;
            int gamePowerSetsSum = 0;

            int validRedCubesCount = 12;
            int validGreenCubesCount = 13;
            int validBlueCubesCount = 14;

            // for each line
            foreach (string fileLine in File.ReadLines(path))
            {
                var lineGame = fileLine.Split(':');

                var gameWithID = lineGame[0].Trim();
                var gameID = int.Parse(gameWithID.Split(' ')[1]);

                var gameSets = lineGame[1].Trim().Split(';');
                var cubeGamesSets = ParseGameSets(gameSets);

                //bool isValidGame = true;
                int minRed = 0, minGreen = 0, minBlue = 0;

                foreach (var cubeGameSet in cubeGamesSets)
                {
                    //if (cubeGameSet.RedCubes > validRedCubesCount ||
                    //    cubeGameSet.GreenCubes > validGreenCubesCount ||
                    //    cubeGameSet.BlueCubes > validBlueCubesCount)
                    //{
                    //    isValidGame = false;
                    //    break;
                    //}

                    if (minRed < cubeGameSet.RedCubes)
                    {
                        minRed = cubeGameSet.RedCubes;
                    }
                    if (minGreen < cubeGameSet.GreenCubes)
                    {
                        minGreen = cubeGameSet.GreenCubes;
                    }
                    if (minBlue < cubeGameSet.BlueCubes)
                    {
                        minBlue = cubeGameSet.BlueCubes;
                    }
                }

                var gamePower = minRed * minGreen * minBlue;

                gamePowerSetsSum += gamePower;

                //if (isValidGame)
                //{
                //    validGameIDSum += gameID;
                //}
            }

            return gamePowerSetsSum;
        }

        private static List<CubeGameSet> ParseGameSets(string[] gameSets)
        {
            var ret = new List<CubeGameSet>();

            foreach (var gameSet in gameSets)
            {
                var cubesCounts = gameSet.Split(",");

                var cubeGameSet = new CubeGameSet();

                foreach (var cubeCount in cubesCounts)
                {
                    var coloredCube = cubeCount.Trim().Split(' ');
                    if (coloredCube[1].ToLowerInvariant().Contains("red"))
                    {
                        cubeGameSet.RedCubes = int.Parse(coloredCube[0]);
                    }
                    else if (coloredCube[1].ToLowerInvariant().Contains("green"))
                    {
                        cubeGameSet.GreenCubes = int.Parse(coloredCube[0]);
                    } 
                    else if (coloredCube[1].ToLowerInvariant().Contains("blue"))
                    {
                        cubeGameSet.BlueCubes = int.Parse(coloredCube[0]);
                    }
                }

                ret.Add(cubeGameSet);
            } 
            
            return ret;
        }
    }
}
