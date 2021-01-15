using AdventOfCode2020.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode2020.Solutions.Task4
{
    

    public class Passport
    {
        public int? BirthYear { get; set; }
        public int? IssueYear { get; set; }
        public int? ExpirationYear { get; set; }
        public string? Height { get; set; }
        public string? HairColor { get; set; }
        public string? EyeColor { get; set; }
        public string? PassportId { get; set; }
        public string? CountryId { get; set; }

        public Passport(string input, string lineSeparator = "\r\n")
        {

            // Remove empty lines
            input = input.Replace(lineSeparator, " ");

            // Get Pairs
            var pairs = input.Split(" ").Select(i => i.Trim()).ToList();

            foreach (var pair in pairs)
            {
                if (!string.IsNullOrWhiteSpace(pair))
                {
                    var keyValue = pair.Split(":").Select(p => p.Trim()).ToList();
                    switch (keyValue[0])
                    {
                        case "byr":
                            this.BirthYear = Convert.ToInt32(keyValue[1]);
                            break;
                        case "iyr":
                            this.IssueYear = Convert.ToInt32(keyValue[1]);
                            break;
                        case "eyr":
                            this.ExpirationYear = Convert.ToInt32(keyValue[1]);
                            break;
                        case "hgt":
                            this.Height = keyValue[1];
                            break;
                        case "hcl":
                            this.HairColor = keyValue[1];
                            break;
                        case "ecl":
                            this.EyeColor = keyValue[1];
                            break;
                        case "pid":
                            this.PassportId = keyValue[1];
                            break;
                        case "cid":
                            this.CountryId = keyValue[1];
                            break;
                        default:
                            throw new Exception("Unknown prefix " + keyValue[1]);
                            break;
                    }
                }
            }
        }

        public bool IsValid()
        {
            // check if all data present
            if (!(
                BirthYear != null
                && IssueYear != null
                && ExpirationYear != null
                && Height != null
                && HairColor != null
                && EyeColor != null
                && PassportId != null)) return false;

            // validate birth year
            if (BirthYear < 1920 || BirthYear > 2002) return false;

            // validate issue year
            if (IssueYear < 2010 || IssueYear > 2020) return false;

            // validate expiration year
            if (ExpirationYear < 2020 || ExpirationYear > 2030) return false;

            // validate height
            if (!Height.EndsWith("cm") && !Height.EndsWith("in")) return false;
            
            try
            {
                int heightNumber = Convert.ToInt32(Height.Substring(0, Height.Length - 2));
                string heightType = Height.Substring(Height.Length - 2);
                if (heightType == "cm" && (heightNumber < 150 || heightNumber > 193)) return false;
                if (heightType == "in" && (heightNumber < 59 || heightNumber > 76)) return false;
            } catch
            {
                // Cannot parse
                throw new Exception("Cannot parse " + Height);
            }

            // validate hair color
            if (HairColor.Length != 7 || !HairColor.StartsWith("#")) return false;
            var allowedChars = new List<char> { '#', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'a', 'b', 'c', 'd', 'e', 'f' };
            for (var i = 0; i< HairColor.Length; i++)
            {
                var currentChar = Convert.ToChar(HairColor.Substring(i, 1));
                if (!allowedChars.Contains(currentChar)) return false;
            }
            
            // validate eye color
            var allowedValues = new List<string> { "amb", "blu", "brn", "gry", "grn", "hzl", "oth" };
            if (!allowedValues.Contains(EyeColor)) return false;

            // validate passport number
            if (PassportId.Length != 9) return false;
            allowedChars = new List<char> { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            for (var i = 0; i < PassportId.Length; i++)
            {
                var currentChar = Convert.ToChar(PassportId.Substring(i, 1));
                if (!allowedChars.Contains(currentChar)) return false;
            }
            
            // Passed all validations - return true;
            return true;
        }

        public override string ToString()
        {
            return BirthYear + " "
                + IssueYear + " "
                + ExpirationYear + " "
                + Height + " "
                + HairColor + " "
                + EyeColor + " "
                + PassportId + " "
                + CountryId + " ";
        }
    }


    /// <summary>
    /// Task 4 - PAssport validation
    /// </summary>
    public class Task4
    {
        const string LINESEPARATOR = "\r\n";
        public static int FirstPart(string filename)
        {
            List<Passport> validPassports = new List<Passport>();

            var lines = ReadInputs.ReadWholeFileAsString(filename).Split(LINESEPARATOR + LINESEPARATOR).Select(l => l.Trim()).ToList();
            int count = 0;

            foreach (var line in lines)
            {
                var passport = new Passport(line, LINESEPARATOR);

                if (passport.IsValid())
                {
                    validPassports.Add(passport);
                    count++;
                }
            }

            //var birthYears = validPassports.Select(p => p.BirthYear).ToList();
            //var issueYears = validPassports.Select(p => p.IssueYear).ToList();
            //var expirationYears = validPassports.Select(p => p.ExpirationYear).ToList();
            //var heights = validPassports.Select(p => p.Height).ToList();
            //var haircolors = validPassports.Select(p => p.HairColor).ToList();
            //var eyecolors = validPassports.Select(p => p.EyeColor).ToList();
            //var passportIds = validPassports.Select(p => p.PassportId).ToList();

            return count;
        }
    }
}
