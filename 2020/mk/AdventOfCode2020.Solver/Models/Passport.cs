using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdventOfCode2020.Solver.Models
{
    public class Passport
    {
        [Required]
        [Range(1920, 2002)]
        public string byr { get; set; } // (Birth Year)

        [Required]
        [Range(2010, 2020)]
        public string iyr { get; set; } // (Issue Year)

        [Required]
        [Range(2020, 2030)]
        public string eyr { get; set; } // (Expiration Year)

        [Required]
        [HeightAtttribute(59, 76, 150, 193)]
        public string hgt { get; set; } // (Height)

        [Required]
        [RegularExpression("^#(\\d|[a-f]){6}$")]
        public string hcl { get; set; } // (Hair Color)

        [Required]
        [RegularExpression("^(amb|blu|brn|gry|grn|hzl|oth)$")]
        public string ecl { get; set; } // (Eye Color)

        [Required]
        [RegularExpression("^(\\d){9}$")]
        public string pid { get; set; } // (Passport ID)

        public string cid { get; set; } // (Country ID)


        public Passport(string input)
        {
            var props = input
                .Split(new[] { "\r\n", " " }, StringSplitOptions.RemoveEmptyEntries)
                .Select(a => a.Split(':'))
                .ToDictionary(a => a[0], a => a[1]);

            if (props.ContainsKey(nameof(byr)))
                byr = props[nameof(byr)];
            if (props.ContainsKey(nameof(iyr)))
                iyr = props[nameof(iyr)];
            if (props.ContainsKey(nameof(eyr)))
                eyr = props[nameof(eyr)];
            if (props.ContainsKey(nameof(eyr)))
                eyr = props[nameof(eyr)];
            if (props.ContainsKey(nameof(hgt)))
                hgt = props[nameof(hgt)];
            if (props.ContainsKey(nameof(hcl)))
                hcl = props[nameof(hcl)];
            if (props.ContainsKey(nameof(ecl)))
                ecl = props[nameof(ecl)];
            if (props.ContainsKey(nameof(pid)))
                pid = props[nameof(pid)];
            if (props.ContainsKey(nameof(cid)))
                cid = props[nameof(cid)];
        }

        public bool HasRequiredFields()
        {
            ValidationContext context = new ValidationContext(this, null, null);
            List<ValidationResult> validationResults = new List<ValidationResult>();
            Validator.TryValidateObject(this, context, validationResults, true);

            if (validationResults.Any(r => r.ErrorMessage.Contains("field is required")))
                return false;

            return true;
        }

        public bool IsValid()
        {
            ValidationContext context = new ValidationContext(this, null, null);
            List<ValidationResult> validationResults = new List<ValidationResult>();
            bool valid = Validator.TryValidateObject(this, context, validationResults, true);

            return valid;
        }
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
    public class HeightAtttribute : ValidationAttribute
    {
        readonly int _inFrom;
        readonly int _inTo;
        readonly int _cmFrom;
        readonly int _cmTo;

        public HeightAtttribute(int inFrom, int inTo, int cmFrom, int cmTo)
        {
            _inFrom = inFrom;
            _inTo = inTo;
            _cmFrom = cmFrom;
            _cmTo = cmTo;
        }

        public override bool IsValid(object value)
        {
            var hgt = (string)value;

            if (hgt.EndsWith("cm") && Int32.TryParse(hgt.Substring(0, hgt.Length - 2), out int cmHeight))
            {
                if ((cmHeight >= _cmFrom && cmHeight <= _cmTo))
                    return true;
            }
            else if (hgt.EndsWith("in") && Int32.TryParse(hgt.Substring(0, hgt.Length - 2), out int inHeight))
            {
                if ((inHeight >= _inFrom && inHeight <= _inTo))
                    return true;
            }

            return false;
        }


        //public override string FormatErrorMessage(string name)
        //{
        //    return String.Format(CultureInfo.CurrentCulture,
        //      ErrorMessageString, name, this.Mask);
        //}

    }
}
