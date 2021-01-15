using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using ValidationResult = System.ComponentModel.DataAnnotations.ValidationResult;

namespace AdventOfCode.Puzzles
{
    public static class Puzzle04
    {
        public static int CountValidPassportsTask1(IEnumerable<PassportData> passports)
        => passports
            .Where(x => x.IsValidTask1())
            .Count();

        public static int CountValidPassportsTask2(IEnumerable<PassportData> passports)
        => passports
            .Where(x => x.IsValidTask2())
            .Count();

        public static IEnumerable<PassportData> ToPuzzle4Input(this string input)
        => input
            .Trim()
            .Split("\r\n\r\n")
            .Select(x => x.ProcessPuzzle4InputRow());

        private static int? GetNumericValueOrNull(this Dictionary<string, string> pairs, string key)
        => pairs.TryGetValue(key, out string value) ? (Int32.TryParse(value, out var intResult) ? intResult : null) : null;

        private static string? GetValueOrNull(this Dictionary<string, string> pairs, string key)
        => pairs.TryGetValue(key, out string value) ? value : null;

        private static PassportData ProcessPuzzle4InputRow(this string inputRow)
        => inputRow
            .Trim()
            .Replace("\r\n", " ")
            .Split(" ")
            .Select(x => x.Split(":"))
            .Select(x => new KeyValuePair<string, string>(x[0].Trim(), x[1].Trim()))
            .ToDictionary(x => x.Key, x => x.Value)
            .ToPassportData();

        private static PassportData ToPassportData(this Dictionary<string, string> pairs)
        => new PassportData
        {
            BirthYear = pairs.GetNumericValueOrNull("byr"),
            CountryId = pairs.GetValueOrNull("cid"),
            ExpirationYear = pairs.GetNumericValueOrNull("eyr"),
            EyeColor = pairs.GetValueOrNull("ecl"),
            HairColor = pairs.GetValueOrNull("hcl"),
            PassportId = pairs.GetValueOrNull("pid"),
            Height = pairs.GetValueOrNull("hgt"),
            IssueYear = pairs.GetNumericValueOrNull("iyr")
        };
    }

    public class PassportData
    {
        [Required, Range(1920, 2002)]
        public int? BirthYear { get; set; }

        public string? CountryId { get; set; }

        [Required, Range(2020, 2030)]
        public int? ExpirationYear { get; set; }

        [Required, RegularExpression("^(amb|blu|brn|gry|grn|hzl|oth)$")]
        public string EyeColor { get; set; }

        [Required, RegularExpression("^#[0-9a-f]{6}$")]
        public string HairColor { get; set; }

        [Required]
        public string Height { get; set; }

        [Required, Range(2010, 2020)]
        public int? IssueYear { get; set; }

        [Required, RegularExpression("^[0-9]{9}$")]
        public string PassportId { get; set; }

        private int? HeightNumeric
        => Regex.IsMatch(Height, "^[0-9]{2,3}(cm|in)$") ? Convert.ToInt32(Height.Replace("in", "").Replace("cm", "")) : null;

        private bool HeightValid
        => HeightNumeric != null
           && (
            (Height.EndsWith("cm") && HeightNumeric >= 150 && HeightNumeric <= 193)
            || (Height.EndsWith("in") && HeightNumeric >= 59 && HeightNumeric <= 76)
            );

        public bool IsValidTask1()
        => !(BirthYear == null
            || ExpirationYear == null
            || string.IsNullOrEmpty(EyeColor)
            || string.IsNullOrEmpty(HairColor)
            || string.IsNullOrEmpty(Height)
            || IssueYear == null
            || string.IsNullOrEmpty(PassportId));

        public bool IsValidTask2()
        {
            var results = new List<ValidationResult>();
            Validator.TryValidateObject(this, new ValidationContext(this), results, true);
            return !results.Any() && HeightValid;
        }
    }
}