const fs = require("fs");

/* Link to task description: https://adventofcode.com/2025/day/5 */

/**
 * Part 1
 */
function part1() {
  fs.readFile("input.txt", "utf-8", function (err, data) {
    if (err) throw err;

    var rows = data.split("\n");
    const intervals = [];
    const numsToCheck = [];
    let lastRangeCount = 0;
    let validNums = [];

    for (let i = 0; i < rows.length; i++) {
      const row = rows[i].replaceAll("\r", "");

      if (row.trim() === "") {
        lastRangeCount = i;
        break;
      }

      var nums = row.split("-");
      intervals.push([parseInt(nums[0]), parseInt(nums[1])]);
    }

    for (let i = lastRangeCount + 1; i < rows.length; i++) {
      numsToCheck.push(parseInt(rows[i]));
    }

    for (let i = 0; i < numsToCheck.length; i++) {
      const numToCheck = numsToCheck[i];
      let numValid = false;

      for (let j = 0; j < intervals.length; j++) {
        var currentInterval = intervals[j];

        if (
          numToCheck >= currentInterval[0] &&
          numToCheck <= currentInterval[1]
        ) {
          numValid = true;
          break;
        }
      }

      if (numValid) validNums.push(numToCheck);
    }

    console.log(
      `Total number of fresh available ingredients is: ${validNums.length}.`
    );
  });
}

part1();

/**
 * Part 2
 */
function part2() {
  fs.readFile("input.txt", "utf-8", function (err, data) {
    if (err) throw err;

    var rows = data.split("\n");
    const intervals = [];

    for (let i = 0; i < rows.length; i++) {
      const row = rows[i].replaceAll("\r", "");

      if (row.trim() === "") {
        break;
      }

      var nums = row.split("-");
      intervals.push([parseInt(nums[0]), parseInt(nums[1])]);
    }

    var sortedIntervals = intervals.sort((x, y) => {
      return x[0] - y[0];
    });

    var updatedIntervals = [];

    let currentlyOpenedInterval = [];
    let currentClosingCandidate = 0;
    for (let i = 0; i < sortedIntervals.length; i++) {
      currentClosingCandidate =
        sortedIntervals[i][1] > currentClosingCandidate
          ? sortedIntervals[i][1]
          : currentClosingCandidate;

      // Finish the loop if last
      if (i === sortedIntervals.length - 1) {
        if (currentlyOpenedInterval[0] === undefined)
          currentlyOpenedInterval[0] = sortedIntervals[i][0];

        currentlyOpenedInterval[1] = currentClosingCandidate;
        updatedIntervals.push(currentlyOpenedInterval);
        break;
      }

      // Open if not opened AND updated are empty OR
      if (
        currentlyOpenedInterval.length === 0 &&
        (updatedIntervals.length === 0 ||
          sortedIntervals[i][0] >
            updatedIntervals[updatedIntervals.length - 1][1])
      )
        currentlyOpenedInterval[0] = sortedIntervals[i][0];

      // Close current and add to list if both next are larger
      if (
        sortedIntervals[i][1] < sortedIntervals[i + 1][0] &&
        sortedIntervals[i][1] < sortedIntervals[i + 1][1]
      ) {
        currentlyOpenedInterval[1] = currentClosingCandidate;
        updatedIntervals.push(currentlyOpenedInterval);
        currentlyOpenedInterval = [];
        currentClosingCandidate = 0;
        continue;
      }
    }
    let i = 0;
    const sum = updatedIntervals.reduce((acc, curr) => {
      let addOverlap = 1;

      if (updatedIntervals[i - 1] < curr[0]) addOverlap++;

      i++;
      return acc + (curr[1] - curr[0]) + addOverlap;
    }, 0);

    console.log(`Total number of fresh available ingredients is: ${sum}.`);
  });
}

part2();
