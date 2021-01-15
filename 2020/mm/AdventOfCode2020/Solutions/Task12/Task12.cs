using AdventOfCode2020.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020.Solutions.Task12
{
    public class Task12
    {
        public static int FirstPart(string filename)
        {
            int x = 0, y = 0;
            int direction = 90;

            var lines = ReadInputs.ReadAllStrings(ReadInputs.GetFullPath(filename));
            foreach (var line in lines)
            {
                char command = Convert.ToChar(line.Substring(0, 1));
                int arg = Convert.ToInt32(line.Substring(1));

                Console.Write("After command " + line);

                if (command == 'F')
                {
                    if (direction == 0) command = 'N';
                    else if (direction == 90) command = 'E';
                    else if (direction == 180) command = 'S';
                    else if (direction == 270) command = 'W';
                    else throw new Exception("Jebo te Pitagora da te jebo!");
                }

                switch (command)
                {
                    case 'N':
                        y = y + arg;
                        break;
                    case 'S':
                        y = y - arg;
                        break;
                    case 'E':
                        x = x - arg;
                        break;
                    case 'W':
                        x = x + arg;
                        break;
                    case 'L':
                        direction = (direction - arg) % 360;
                        if (direction < 0) direction += 360;
                        break;
                    case 'R':
                        direction = (direction + arg) % 360;
                        break;

                    default:
                        throw new Exception("We should never hit here!");
                        break;
                }

                Console.WriteLine(" we are at " + x + "," + y + " heading " + direction + ".");
            }


            return Math.Abs(x) + Math.Abs(y);
        }

        public static int SecondPart(string filename)
        {
            int x = 0, y = 0;
            int waypointX = 10, waypointY = 1;

            var lines = ReadInputs.ReadAllStrings(ReadInputs.GetFullPath(filename));
            foreach (var line in lines)
            {
                char command = Convert.ToChar(line.Substring(0, 1));
                int arg = Convert.ToInt32(line.Substring(1));

                if (command == 'L')
                {
                    command = 'R';
                    arg = (360 - arg) % 360;
                }

                Console.Write("After command " + line);

                switch (command)
                {
                    case 'N':
                        waypointY += arg;
                        break;
                    case 'S':
                        waypointY -= arg;
                        break;
                    case 'E':
                        waypointX += arg;
                        break;
                    case 'W':
                        waypointX -= arg;
                        break;
                    case 'R':
                        int temp;
                        switch(arg)
                        {
                            case 0:
                                break;
                            case 90:
                                temp = waypointX;
                                waypointX = waypointY;
                                waypointY = -temp;
                                break;
                            case 180:
                                waypointY = -waypointY;
                                waypointX = -waypointX;
                                break;
                            case 270:
                                temp = waypointX;
                                waypointX = -waypointY;
                                waypointY = temp;
                                break;
                            default:
                                throw new Exception("Jebo te Pitagora da te jebo!");
                                break;
                        }
                        break;
                    case 'F':
                        x += arg * waypointX;
                        y += arg * waypointY;
                        break;

                    default:
                        throw new Exception("We should never hit here!");
                        break;
                }

                Console.WriteLine(" we are at " + x + "," + y + " with waypoint [" + waypointX + "," + waypointY+ "].");
            }


            return Math.Abs(x) + Math.Abs(y);
        }
    }
}
