namespace AdventOfCode2022.Days;

public class Day2 : Puzzle<int>
{
    public override int Day => 2;

    enum Shape
    {
        Rock = 1,
        Paper = 2,
        Scissors = 3
    }

    static Shape Lose(Shape you)
    {
        var next = (int)you + 1;

        if (next > 3)
            next = 1;

        return (Shape)next;
    }

    static Shape Win(Shape you)
    {
        var next = (int)you - 1;

        if (next < 1)
            next = 3;

        return (Shape)next;
    }

    int Score(Shape opponent, Shape you)
    {
        var outcome = 0;
        var shape = (int)you;

        if (opponent == you)
        {
            outcome = 3;
        }
        else if (opponent == Win(you))
        {
            outcome = 6;
        }
        else if (opponent == Lose(you))
        {
            outcome = 0;
        }

        return shape + outcome;
    }

    public override int First()
    {
        var lines = GetInputLines();

        var score = 0;

        foreach (var line in lines)
        {
            Shape opponent = line[0] switch
            {
                'A' => Shape.Rock,
                'B' => Shape.Paper,
                'C' => Shape.Scissors,
                _ => throw new NotImplementedException(),
            };
            Shape you = line[2] switch
            {
                'X' => Shape.Rock,
                'Y' => Shape.Paper,
                'Z' => Shape.Scissors,
                _ => throw new NotImplementedException(),
            };

            score += Score(opponent, you);
        }

        return score;
    }

    public override int Second()
    {
        var lines = GetInputLines();

        var score = 0;

        foreach (var line in lines)
        {
            Shape opponent = line[0] switch
            {
                'A' => Shape.Rock,
                'B' => Shape.Paper,
                'C' => Shape.Scissors,
                _ => throw new NotImplementedException(),
            };
            Shape you = line[2] switch
            {
                'X' => Win(opponent),
                'Y' => opponent,
                'Z' => Lose(opponent),
                _ => throw new NotImplementedException(),
            };

            score += Score(opponent, you);
        }

        return score;
    }
}
