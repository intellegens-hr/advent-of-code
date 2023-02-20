const fs = require("fs");

/* Link to task description: https://adventofcode.com/2022/day/2 */

/**
 * Part 1
 */
function part1() {
  fs.readFile("input.txt", "utf-8", function (err, data) {
    if (err) throw err;

    let moveRatings = {
      // Rock
      X: {
        stronger: "C",
        weaker: "B",
        score: 1
      },
      // Paper
      Y: {
        stronger: "A",
        weaker: "C",
        score: 2
      },
      // Scissors
      Z: {
        stronger: "B",
        weaker: "A",
        score: 3
      }
    }

    const calcScore = (move) => {
      const playerMove = moveRatings[move[1]];
      const opponentMove = move[0];

      return playerMove.score + (playerMove.stronger === opponentMove ? 6 : playerMove.weaker === opponentMove ? 0 : 3)
    }

    let score = 0;

    data.split("\n").forEach((stringInput) => {
      score += calcScore(stringInput.split(" "));
    });

    console.log(`Total score would be ${score}.`)
  });
}

part1();

/**
 * Part 2
 */
function part2() {
  fs.readFile("input.txt", "utf-8", function (err, data) {
    if (err) throw err;

    let moveRatings = [
      {
        value: "A",
        stronger: "C",
        weaker: "B",
        score: 1
      },
      {
        value: "B",
        stronger: "A",
        weaker: "C",
        score: 2
      },
      {
        value: "C",
        stronger: "B",
        weaker: "A",
        score: 3
      }
    ]

    const calcScore = (move) => {
      const opponentMove = move[0];
      let playerMove = {};
      let winBonus = 0;

      // Lose
      if (move[1] === "X") {
        playerMove = moveRatings.find(mr => mr.weaker === opponentMove);
        winBonus = 0;
      }
      // Win
      else if (move[1] === "Z") {
        playerMove = moveRatings.find(mr => mr.stronger === opponentMove);
        winBonus = 6;
      }
      // Draw
      else {
        playerMove = moveRatings.find(mr => mr.value === opponentMove);
        winBonus = 3;
      }

      return playerMove.score + winBonus;
    }

    let score = 0;

    data.split("\n").forEach((stringInput) => {
      score += calcScore(stringInput.split(" "));
    });

    console.log(`Total score would be ${score}.`)
  });
}

part2();
