namespace AdventOfCode2024;

public static class Utils
{
    public static T[,] To2DArray<T>(this IEnumerable<IEnumerable<T>> source)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        var rowValues = source.ToArray();
        var rows = rowValues.Length;
        var columns = rowValues[0].Count();

        var result = new T[rows, columns];

        for (int i = 0; i < rows; i++)
        {
            var columnValues = rowValues[i].ToArray();
            for (int j = 0; j < columns; j++)
            {
                result[i, j] = columnValues[j];
            }
        }

        return result;
    }

    public static T[,] ToArray<T>(this T[,] source)
    {
        if (source == null)
            throw new ArgumentNullException(nameof(source));

        var rows = source.GetLength(0);
        var columns = source.GetLength(1);

        var result = new T[rows, columns];
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                result[i, j] = source[i,j];
            }
        }

        return result;
    }


    public static T[,] Transpose<T>(this T[,] matrix)
    {
        var rows = matrix.GetLength(0);
        var columns = matrix.GetLength(1);

        var result = new T[columns, rows];

        for (var c = 0; c < columns; c++)
        {
            for (var r = 0; r < rows; r++)
            {
                result[c, r] = matrix[r, c];
            }
        }
        return result;
    }

    public static T[] GetColumn<T>(this T[,] matrix, int c)
    {
        var rows = matrix.GetLength(0);

        var result = new T[rows];
        for (var r = 0; r < rows; r++)
        {
            result[r] = matrix[r, c];
        }
        return result;
    }

    public static T[] GetRow<T>(this T[,] matrix, int r)
    {
        var columns = matrix.GetLength(1);

        var result = new T[columns];
        for (var c = 0; c < columns; c++)
        {
            result[c] = matrix[r, c];
        }
        return result;
    }


    public static string[] ToLines(this string input)
    {
        return input.Split(Environment.NewLine);
    }

    public static double DistanceTo(this (int X, int Y) from, (int X, int Y) to)
    {
        return Math.Sqrt(Math.Pow((from.X - to.X), 2) + Math.Pow((from.Y - to.Y), 2));
    }

    public static string Reverse(this string s)
    {
        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }
}
