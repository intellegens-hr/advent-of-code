using AdventOfCode2020.Commons;
using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020.Solutions.Task2
{
    public class Task2
    {
        /// <summary>
        /// How many passwords are valid based on a given rule.
        /// Rule is in format 1-3 a: pass where 1 is the minimum number of ocurrences of a, 3 is maximum number of occurences of a, a is a varible and pass is a string
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static int FirstPart(string filename)
        {
            var reader = ReadInputs.OpenInputFile(filename);
            string line;
            int numOfValidPasswords = 0;
            int row = 0;

            while ((line = reader.ReadLine()) != null)
            {
                var passwordRule = new PasswordRule(line);
                
                // Uncomment for first part
                //var isValidOld = passwordRule.IsValidOldPolicy();
                //if (isValidOld)
                //{
                //    numOfValidPasswords++;
                //}

                // Uncomment for 2nd part
                var isValidNew = passwordRule.IsValidNewPolicy();
                if (isValidNew)
                {
                    numOfValidPasswords++;
                }

                Console.WriteLine(String.Format("Row {0}, Rule: {1}, IsValid: {2}", row, passwordRule.ToString(), isValidNew));
            }

            reader.Close();

            return numOfValidPasswords;
        }

        //public static int SecondPart(string filename)
        //{

        //}
    }
}
