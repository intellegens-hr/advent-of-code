using Xunit;

namespace AdventOfCode.Puzzles.Tests
{
    public class Puzzle10Tests
    {
        [Fact]
        public void Task1_Element()
        {
            var stringInput = @"28
33
18
42
31
14
46
20
48
47
24
23
49
45
19
38
39
11
1
32
25
35
8
17
7
9
4
2
34
10
3";

            var number = Puzzle10.Task1(stringInput.ToPuzzle10Input());
            Assert.Equal(220, number);
        }

        [Fact]
        public void Task2_Sum()
        {
            var stringInput = @"28
33
18
42
31
14
46
20
48
47
24
23
49
45
19
38
39
11
1
32
25
35
8
17
7
9
4
2
34
10
3";

            var sum = Puzzle10.Task2(stringInput.ToPuzzle10Input());
            Assert.Equal(19208, sum);
        }
    }
}