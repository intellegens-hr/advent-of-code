namespace AoC2021.Solutions.Models.Day06
{
    public class Fish
    {
        public long Count { get; set; }

        public Fish(int state, long count = 0)
        {
            Count = count;
            State = state;
        }

        public int State { get; set; }

        /// <summary>
        /// Returns number of newly created fish
        /// </summary>
        /// <returns></returns>
        public long NextDay()
        {
            State--;
            if (State == -1)
            {
                State = 6;
                return Count;
            }
            return 0;
        }
    }
}