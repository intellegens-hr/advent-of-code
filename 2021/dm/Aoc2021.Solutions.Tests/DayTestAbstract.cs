using AoC2021.Solutions;
using Xunit;

namespace Aoc2021.Solutions.Tests
{
    public abstract class DayTests<T, K> 
        where T : DayAbstract<K>, new()
        where K : struct
    {
        protected abstract string Input { get; }
        protected DayAbstract<K> day;

        protected DayTests()
        {
            day = new T();
        }

        [Fact]
        public abstract void Part1();

        [Fact]
        public abstract void Part1_FullInput();

        [Fact]
        public abstract void Part2();

        [Fact]
        public abstract void Part2_FullInput();
    }
}