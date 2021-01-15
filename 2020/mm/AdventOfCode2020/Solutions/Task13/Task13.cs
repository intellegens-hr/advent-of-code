using AdventOfCode2020.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020.Solutions.Task13
{
    public class Task13
    {
        public static long FirstPart(string filename)
        {
            var lines = ReadInputs.ReadAllStrings(ReadInputs.GetFullPath(filename));
            int timestamp = Convert.ToInt32(lines[0]);
            var busses = lines[1].Split(",").Select(l => l.Trim() == "x" ? -1 : Convert.ToInt32(l)).ToList();

            int bestBus = -1;
            int shortestTime = busses[busses.Count - 1] + 1;


            // TASK 1:
            //foreach (var bus in busses)
            //{
            //    if (bus != -1)
            //    {
            //        var minutesSinceLastLeft = timestamp % bus;
            //        if (minutesSinceLastLeft == 0)
            //        {
            //            bestBus = bus;
            //            shortestTime = 0;
            //            break;
            //        }
            //        else
            //        {
            //            // last bus of this ID left minutesSinceLastLeft ago.
            //            // Next one is coming in bus - minutessincelastleft
            //            if (bus - minutesSinceLastLeft < shortestTime)
            //            {
            //                bestBus = bus;
            //                shortestTime = bus - minutesSinceLastLeft;
            //            }
            //        }
            //    }
            //}

            //return bestBus * shortestTime;


            // TASK 2:
            long i = 0;
            long step = busses[0];
            List<KeyValuePair<long, int>> onlyBusses = busses.Where(b => b != -1).Select(b => new KeyValuePair<long, int>(b, busses.IndexOf(b))).ToList();

            bool isFinished = false;
            int indexOfBus = 1;
            var nextBusToCompare = onlyBusses[indexOfBus].Key;
            long orderOfMagnitude = 1;

            while (!isFinished)
            {
                if (i / orderOfMagnitude >= 1)
                {
                    Console.WriteLine("New order of magnitude, i = " + i + "(" + Math.Log10(orderOfMagnitude) + "), step = " + step);
                    orderOfMagnitude *= 10;
                }

                // Console.WriteLine("Checking for i=" + i);
                // Check if other busses come "in their place"
                var nextBusIn = WhenNextBusComes(i, nextBusToCompare);
                if (nextBusIn == onlyBusses[indexOfBus].Value % onlyBusses[indexOfBus].Key)
                {
                    // Works for next bus.
                    // Increase step, increase indexOfBus
                    step *= nextBusToCompare;
                    indexOfBus++;
                    if (indexOfBus == onlyBusses.Count())
                    {
                        break;
                    }
                    nextBusToCompare = onlyBusses[indexOfBus].Key;
                    Console.WriteLine("Now comparing to bus " + nextBusToCompare + " with new step being " + step);
                } else
                {
                    i += step;
                }
                

            }

            return i;
        }


        private static long WhenNextBusComes(long timestamp, long bus)
        {
            long minutesSinceLastCame = timestamp % bus;
            return bus - minutesSinceLastCame;
        }
    }
}
