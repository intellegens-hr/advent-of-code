namespace AdventOfCode2025.Days;

public class Day12 : Puzzle<long>
{
    public override long First(string input)
    {
        var positions = input.ToLines()[30..];

        var total = 0;

        //this calculates how many positions are valid without even checking them(all tiles can be places side by side without optimisation)
        foreach (var position in positions)
        {
            var parts = position.Split(": ");
            var ln = int.Parse(parts[0].Split("x")[0]);
            var rn = int.Parse(parts[0].Split("x")[1]);
            var sm = parts[1].Split(" ").Select(int.Parse).Sum() * 9;

            ln -= ln % 3;
            rn -= rn % 3;

            if (ln * rn >= sm)
                total++;
        }

        //// this calculates how many positions are not possible even with optimisation
        //foreach (var position in positions)
        //{
        //    var parts = position.Split(": ");
        //    var ln = int.Parse(parts[0].Split("x")[0]);
        //    var rn = int.Parse(parts[0].Split("x")[1]);
        //    var sms = parts[1].Split(" ").Select(int.Parse).ToArray(); //.Sum() * 9;
        //    var sm = sms[0] * 7 + sms[1] * 7 + sms[2] * 6 + sms[3] * 7 + sms[4] * 5 + sms[5] * 7;

        //    if (ln * rn < sm)
        //        total++;
        //}

        return total;
    }

    public override long Second(string input)
    {
        return 0;
    }
}