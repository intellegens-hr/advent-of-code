namespace AdventOfCode2025.Days;

public class Day01 : Puzzle<int>
{
    public override int First(string input)
    {
        var lines = input.ToLines();
        var position = 50;
        var zeros = 0;

        foreach (var line in lines)
        {
            var amount = int.Parse(line[1..]);

            if (line[0] == 'L') 
                amount *= -1;

            position = (position + amount + 100) % 100;

            if (position == 0)
                zeros++;
        }

        return zeros;
    }

    public override int Second(string input)
    {
        var lines = input.ToLines();
        var position = 50;
        var zeros = 0;

        foreach (var line in lines)
        {
            var direction = line[0];
            var amount = int.Parse(line.Substring(1));


            var oldPosition = position;

            if (direction == 'R')
                position += amount;
            else
                position -= amount;

            if (position >= 100)
                zeros += position / 100;

            if (oldPosition != 0 && position < 0)
                zeros += (-position / 100) + 1;

            if (oldPosition == 0 && position <= -100)
                zeros += (-position / 100);

            if (position == 0)
                zeros++;


            position = ((position) % 100 + 100) % 100;
        }

        return zeros;
    }
}
