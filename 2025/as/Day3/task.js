const fs = require("fs");

/* Link to task description: https://adventofcode.com/2025/day/3 */

/**
 * Part 1
 */
function part1() {
  fs.readFile("input.txt", "utf-8", function (err, data) {
    if (err) throw err;

    let sum = 0;

    data.split("\n").forEach((arrayOfNums) => {
      var nums = arrayOfNums.split("");
      let firstNumber = [];
      let secondNumber = [];

      // Skip last number for first iteration
      for (let i = 0; i < nums.length - 2; i++) {
        var num = parseInt(nums[i]);

        if (firstNumber.length === 0) {
          firstNumber = [num, i];
          continue;
        }

        if (num > firstNumber[0]) firstNumber = [num, i];
      }

      for (let j = firstNumber[1] + 1; j < nums.length - 1; j++) {
        var num = parseInt(nums[j]);

        if (secondNumber.length === 0) {
          secondNumber = [num, j];
          continue;
        }

        if (num > secondNumber[0]) secondNumber = [num, j];
      }

      sum += parseInt(`${firstNumber[0]}${secondNumber[0]}`);
    });

    console.log(`Sum of maximum joltage from each bank is ${sum}.`);
  });
}

part1();

/**
 * Part 2
 */
function part2() {
  fs.readFile("input.txt", "utf-8", function (err, data) {
    if (err) throw err;

    let sum = 0;

    data.split("\n").forEach((arrayOfNums) => {
      let remainingNumbers = arrayOfNums
        .split("")
        .map((x) => parseInt(x))
        .filter((x) => !isNaN(x));
      const currentMaxNumbers = [];

      while (currentMaxNumbers.length < 12) {
        var max = calculateMaxStartInRange(
          remainingNumbers.slice(
            0,
            remainingNumbers.length - (12 - currentMaxNumbers.length - 1)
          )
        );
        currentMaxNumbers.push(max[0]);
        remainingNumbers = remainingNumbers.slice(max[1] + 1);
      }

      sum += parseInt(`${currentMaxNumbers.join("")}`);
    });

    console.log(`Sum of maximum joltage from each bank is ${sum}.`);
  });
}

function calculateMaxStartInRange(remainingNumbers) {
  let max = [0, 0];
  for (let i = 0; i < remainingNumbers.length; i++) {
    var num = remainingNumbers[i];

    if (num > max[0]) {
      max = [num, i];
    }
  }

  return max;
}

part2();
