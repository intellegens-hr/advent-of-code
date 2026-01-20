const fs = require("fs");

/* Link to task description: https://adventofcode.com/2025/day/2 */

/**
 * Part 1
 */
function part1() {
  fs.readFile("input.txt", "utf-8", function (err, data) {
    if (err) throw err;

    const invalidIDs = [];

    data.split(",").forEach((stringInput) => {
      const range = stringInput.split("-");

      for (let i = parseInt(range[0]); i <= parseInt(range[1]); i++) {
        const patterns = [];
        const stringI = i.toString();
        let ongoingPattern = "";

        for (let j = 0; j < stringI.length; j++) {
          ongoingPattern += stringI[j];
          patterns.push(ongoingPattern);
        }

        if (patterns.find((pattern) => `${pattern}${pattern}` === stringI)) {
          invalidIDs.push(i);
        }
      }
    });

    const sum = invalidIDs.reduce((acc, curr) => {
      return acc + curr;
    }, 0);

    console.log(`Invalid IDs sum is ${sum}`);
  });
}

part1();

/**
 * Part 2
 */
function part2() {
  fs.readFile("input.txt", "utf-8", function (err, data) {
    if (err) throw err;

    const invalidIDs = [];

    data.split(",").forEach((stringInput) => {
      const range = stringInput.split("-");

      for (let i = parseInt(range[0]); i <= parseInt(range[1]); i++) {
        const patterns = [];
        const stringI = i.toString();
        let ongoingPattern = "";

        for (let j = 0; j < stringI.length; j++) {
          ongoingPattern += stringI[j];
          patterns.push(ongoingPattern);
        }

        for (let j = 0; j < patterns.length; j++) {
          var currentPattern = patterns[j];
          var remainder = stringI.length % currentPattern.length;
          if (remainder !== 0) continue;

          var numberOfPossibleRepeats = stringI.length / patterns[j].length;
          if (
            numberOfPossibleRepeats > 1 &&
            currentPattern.repeat(numberOfPossibleRepeats) === stringI
          ) {
            invalidIDs.push(i);
            break;
          }
        }
      }
    });

    const sum = invalidIDs.reduce((acc, curr) => {
      return acc + curr;
    }, 0);

    console.log(`Invalid IDs sum is a ${sum}`);
  });
}

part2();
