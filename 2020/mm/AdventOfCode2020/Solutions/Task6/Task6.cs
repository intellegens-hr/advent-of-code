using AdventOfCode2020.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020.Solutions.Task6
{
    public class Task6
    {
        public static int FirstPart(string filename)
        {
            int sum = 0;
            const string LINESEPARATOR = "\r\n";
            var sections = ReadInputs.ReadWholeFileAsString(ReadInputs.GetFullPath(filename)).Split(LINESEPARATOR + LINESEPARATOR);

            List<char> allYes = new List<char>();

            for (int index = 0; index < sections.Length; index++)
            {
                var section = sections[index];


                // PART I
                // List<char> chars = new List<char>();
                //var cleanSection = section.Replace(LINESEPARATOR, "");
                //for (int i = 0; i < cleanSection.Length; i++)
                //{
                //    var currentChar = Convert.ToChar(cleanSection.Substring(i, 1));
                //    if (!chars.Contains(currentChar)) chars.Add(currentChar);
                //}

                //sum += chars.Count;

                // PART II
                // Console.WriteLine("Section: \r\n{0}", section);
                var lines = section.Split(LINESEPARATOR).ToList();
                List<char> answers = new List<char>();

                for (var lineNumber = 0; lineNumber < lines.Count; lineNumber++)
                {
                    string line = lines[lineNumber];
                    var personAnswers = line.ToCharArray().Distinct().OrderBy(c => c).ToList();
                    if (lineNumber == 0)
                    {
                        answers = personAnswers;
                    }
                    else
                    {
                        Console.WriteLine("Comparing\r\n{0}\r\n{1}", string.Join(' ', answers), string.Join(' ', personAnswers));

                        for (var k = answers.Count - 1 ; k >= 0; k--)
                        {
                            if (!personAnswers.Contains(answers[k]))
                            {
                                Console.WriteLine("Removing {0}", answers[k]);
                                answers.RemoveAt(k);
                            }
                        }
                    }
                }

                Console.WriteLine("Answers left: {0}",string.Join(' ', answers));
                //Console.ReadKey();

                sum += answers.Count;
            }

            return sum;
        }
    }
}
