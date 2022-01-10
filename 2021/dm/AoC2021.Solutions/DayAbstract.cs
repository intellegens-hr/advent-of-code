namespace AoC2021.Solutions
{
    public abstract class DayAbstract<T> where T : struct
    {
        public DayAbstract()
        {
        }

        public abstract T CalculatePart1(string input);
        public abstract T CalculatePart2(string input);
    }
}
