const fs = require("fs");

/* Link to task description: https://adventofcode.com/2021/day/7 */

/**
 * Part 1
 */
function part1() {
  fs.readFile("input.txt", "utf-8", function (err, data) {
    if (err) throw err;
    const arrayOfInputs = data
      .split(",")
      .map((s) => parseInt(s))
      .sort((a, b) => a - b); //?

    let minCount;

    for (let i = 0; i < arrayOfInputs.length; i++) {
      let tempCount = 0;
      if (arrayOfInputs[i] === arrayOfInputs[i - 1]) {
        continue;
      }

      for (let j = 0; j < arrayOfInputs.length; j++) {
        let diff = Math.abs(arrayOfInputs[i] - arrayOfInputs[j]);
        tempCount += diff;

        if (minCount && tempCount > minCount) {
          break;
        }
      }

      if (!minCount) {
        minCount = tempCount;
        continue;
      }

      if (tempCount < minCount) {
        minCount = tempCount;
      }
    }

    console.log(`Minimal fuel consumption is ${minCount}`);
  });
}

part1();

/**
 * Part 2
 */
function part2() {
  function count(num1, num2) {
    let count = 0;
    for (let i = num1; i <= num2; i++) {
      count += i;
    }
    return count;
  }

  fs.readFile("input.txt", "utf-8", function (err, data) {
    if (err) throw err;
    const arrayOfInputs = data
      .split(",")
      .map((s) => parseInt(s))
      .sort((a, b) => a - b);

    let minCount;

    for (let i = 0; i < arrayOfInputs.length; i++) {
      let tempCount = 0;
      if (arrayOfInputs[i] === arrayOfInputs[i - 1]) {
        continue;
      }

      for (let j = 0; j < arrayOfInputs.length; j++) {
        let diff = Math.abs(arrayOfInputs[i] - arrayOfInputs[j]);
        tempCount += count(0, diff);

        if (minCount && tempCount > minCount) {
          break;
        }
      }

      if (!minCount) {
        minCount = tempCount;
        continue;
      }

      if (tempCount < minCount) {
        minCount = tempCount;
      }
    }

    console.log(`Minimal fuel consumption is ${minCount}`);
  });
}

part2();
