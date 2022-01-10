using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2021.Days
{
    internal class Day21 : DayBase<long>
    {
        public override int Day => 21;
        class PlayerDef
        {
            public string Name { get; set; }
            public int Position { get; set; }
            public long Score { get; set; }

        }

        public override long First()
        {
            var input = GetInputLines().Select(a => int.Parse(a.Last().ToString())).ToArray();
            (int play1, int play2) startPos = (input[0], input[1]);

            var players = new[] { new PlayerDef { Name = "1", Position = startPos.play1, Score = 0 }, new PlayerDef { Name = "2", Position = startPos.play2, Score = 0 } };
            var diceRoll = 6;
            var rolls = 0;

            //for (int i = 0; i < 150; i += 3)
            //{
            //    con
            //}

            var currentPlayer = players[rolls % 2];
            while (true)
            {
                currentPlayer.Position += diceRoll;

                currentPlayer.Position %= 10;
                if (currentPlayer.Position == 0)
                    currentPlayer.Position = 10;

                currentPlayer.Score += currentPlayer.Position;
                
                rolls++;

                if (currentPlayer.Score >= 1000)
                {
                    break;
                }

                //Console.WriteLine($"Player {currentPlayer.Name} Diceroll {diceRoll} Pos {currentPlayer.Position} Score {currentPlayer.Score}");

                currentPlayer = players[rolls % 2];
                diceRoll = (diceRoll + 10 - 1) % 10;
            }

            return rolls * 3 * players[(rolls) % 2].Score;

        }


        enum Player
        {
            First,
            Second
        }

        Player Opposite(Player player)
        {
            return player == Player.First ? Player.Second : Player.First;
        }

        public override long Second()
        {
            var input = GetInputLines().Select(a => int.Parse(a.Last().ToString())).ToArray();
            (int play1, int play2) startPos = (input[0], input[1]);

            var rolls = new (int roll, int count)[] { (3, 1), (4, 3), (5, 6), (6, 7), (7, 6), (8, 3), (9, 1) };
            var wins = new Dictionary<Player, long> { { Player.First, 0 }, { Player.Second, 0 } };

            var scores = new Dictionary<(long player1Position, long player1Score, long player2Position, long player2Score, Player turn), long>() {{ (startPos.play1 , 0, startPos.play2, 0, Player.First), 1 }};


            while (scores.Any())
            {
                var nextScores = new Dictionary<(long player1Position, long player1Score, long player2Position, long player2Score, Player turn), long>();

                foreach (var score in scores)
                {
                    foreach (var roll in rolls)
                    {
                        var scoreToIncrease = score.Key.turn == Player.First ? score.Key.player1Score : score.Key.player2Score;
                        var positionToIncrease = score.Key.turn == Player.First ? score.Key.player1Position : score.Key.player2Position;

                        var increasedPosition = (positionToIncrease + roll.roll) % 10;
                        if (increasedPosition == 0) increasedPosition = 10;

                        var increasedScore = scoreToIncrease + increasedPosition;

                        var newScore = score.Key.turn == Player.First ? 
                            (increasedPosition, increasedScore, score.Key.player2Position, score.Key.player2Score, Opposite(score.Key.turn)) :
                            (score.Key.player1Position, score.Key.player1Score, increasedPosition, increasedScore, Opposite(score.Key.turn));


                        if (nextScores.ContainsKey(newScore))
                        {
                            nextScores[newScore] += roll.count * score.Value;
                        }
                        else
                        {
                            nextScores[newScore] = roll.count * score.Value;
                        }

                        if (newScore.Item2 >= 21)
                        {
                            wins[Player.First] += nextScores[newScore];
                            nextScores.Remove(newScore);
                        }
                        else if (newScore.Item4 >= 21)
                        {
                            wins[Player.Second] += nextScores[newScore];
                            nextScores.Remove(newScore);
                        }
                    }
                }
                scores = nextScores;
            }

            //Console.WriteLine(wins.Max(a => a.Value).ToString().Length);
            return wins.Max(a => a.Value);

        }

        //1761
        //public override long Second()
        //{

        //    var wins = new Dictionary<bool, long> { { false, 0}, { true, 0} };
        //    var universes = new Dictionary<(long, long, bool), long>() { { (0, 0, true), 1 } };
        //    var even = false;
        //    var i = 0;
        //    while (universes.Any())
        //    {
        //        even = !even;
        //        //var universekeys = universes.Keys.Where(a => a.Item3 == even).ToArray();
        //        var newUniverses = new Dictionary<(long, long, bool), long>();
        //        foreach (var currentUniverseScore in universes.Keys.ToArray())
        //        {
        //            //var universe = universes[currentUniverseScore];
        //            //Console.WriteLine(currentUniverseScore + " " + universes[currentUniverseScore]);
        //            //Console.WriteLine("= >");
        //            //Console.ReadLine();
        //            foreach (var roll in rolls)
        //            {
        //                //var even = currentUniverseScore.Item3;

        //                //var scoreAdd = even ? (roll.Item1, 0) : (0, roll.Item1);
        //                var score = even ? currentUniverseScore.Item1 : currentUniverseScore.Item2;
        //                //var add = score == 0 ? (even ? 4 : 8) : 0;
        //                var add = 0;
        //                //var newnewUniverseScore = (universe.Key.Item1 + scoreAdd.Item1, universe.Key.Item2 + scoreAdd.Item2);
        //                var newScore = (roll.Item1 + score + add) % 10;
        //                if (newScore == 0)
        //                    newScore = 10;

        //                newScore += score;

        //                var newUniverseScore = even ? (newScore, currentUniverseScore.Item2, !even) : (currentUniverseScore.Item1, newScore, !even);

        //                if (newUniverses.ContainsKey(newUniverseScore))
        //                {
        //                    newUniverses[newUniverseScore] += roll.Item2 * universes[currentUniverseScore];
        //                    //Console.WriteLine("         +++++++++");
        //                }
        //                else
        //                {
        //                    newUniverses[newUniverseScore] = roll.Item2 * universes[currentUniverseScore];
        //                }
        //                //Console.WriteLine(newUniverseScore + " " + newUniverses[newUniverseScore]);
        //                //Console.ReadLine();

        //                if (newUniverseScore.Item1 >= 21 || newUniverseScore.Item2 >= 21)
        //                {
        //                    wins[even] += newUniverses[newUniverseScore];
        //                    newUniverses.Remove(newUniverseScore);
        //                }
        //            }

        //            //Console.WriteLine("-------------------");
        //            //universes.Remove(currentUniverseScore);
        //        }
        //        universes = newUniverses;
        //    }

        //    Console.WriteLine(i);
        //    Console.WriteLine(wins.Values.Max().ToString().Length);
        //    return wins.Values.Max();

        //}
    }
}
