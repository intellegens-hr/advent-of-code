namespace AdventOfCode2020.Solver.Models
{
    internal class TicketDefinition
    {
        public int From1 { get; internal set; }
        public string Name { get; internal set; }
        public int To2 { get; internal set; }
        public int From2 { get; internal set; }
        public int To1 { get; internal set; }

        public bool Validate(int number)
        {
            if ((From1 <= number && number <= To1) || (From2 <= number && number <= To2))
                return true;

            return false;
        }
    }
}