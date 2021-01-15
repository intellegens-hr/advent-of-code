using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode2020.Solutions.Task2
{
    public class PasswordRule
    {
        public int MinimumOccurences { get; set; }
        public int MaximumOccurences { get; set; }
        public string Character { get; set; }
        public string Password { get; set; }
        public int NumberOfOccurences { get; set; }


        public PasswordRule()
        {

        }

        /// <summary>
        /// Fills the properties, parses the file row.
        /// </summary>
        /// <param name="fileRow"></param>
        public PasswordRule(string fileRow)
        {
            var temp = fileRow.Split(':');
            Password = temp[1].Trim();

            var temp2 = temp[0].Split(' ');
            Character = temp2[1].Trim();

            var temp3 = temp2[0].Split('-');
            MinimumOccurences = Convert.ToInt32(temp3[0].Trim());
            MaximumOccurences = Convert.ToInt32(temp3[1].Trim());
        }


        /// <summary>
        /// Returns if password is valid based on conditions from 1st star task.
        /// </summary>
        /// <returns></returns>
        public bool IsValidOldPolicy()
        {
            NumberOfOccurences = 0;
            for (var i = 0; i < Password.Length - Character.Length + 1; i++)
            {
                if (Password.Substring(i, Character.Length) == Character) NumberOfOccurences++;
            }

            if (MinimumOccurences <= NumberOfOccurences && MaximumOccurences >= NumberOfOccurences)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public bool IsValidNewPolicy()
        {
            if ((Password.Substring(MinimumOccurences - 1, 1) == Character && Password.Substring(MaximumOccurences - 1, 1) != Character)
                || (Password.Substring(MinimumOccurences - 1, 1) != Character && Password.Substring(MaximumOccurences - 1, 1) == Character))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override string ToString()
        {
            return String.Format("Min: {0}, Max: {1}, Character: {2}, Pass: {3}, Number of occurences: {4}", MinimumOccurences, MaximumOccurences, Character, Password, NumberOfOccurences);
        }


    }
}
