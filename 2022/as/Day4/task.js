const fs = require("fs");

/* Link to task description: https://adventofcode.com/2022/day/4 */

/**
 * Part 1
 */
function part1() {
  fs.readFile("input.txt", "utf-8", function (err, data) {
    if (err) throw err;

    const isInRange = (numberToCheck, range) => {
      return numberToCheck >= range[0] && numberToCheck <= range[1];
    }

    const elves = [];

    data.split("\n").forEach((stringInput) => {
      var stringAssignments = stringInput.split(",");
      elves.push(stringAssignments.map(m => m.split("-").map(s => parseInt(s))))
    });

    let count = 0;

    for (const pairOfElves of elves) {
      const elf1 = pairOfElves[0];
      const elf2 = pairOfElves[1];

      if ((isInRange(elf1[0], elf2) && isInRange(elf1[1], elf2)) || (isInRange(elf2[0], elf1) && isInRange(elf2[1], elf1))) {
        count++;
      }
    }
    console.log(`Total count of elves with totally overlapping assignments is ${count}`)
  });
}

part1();

/**
 * Part 2
 */
function part2() {
  fs.readFile("input.txt", "utf-8", function (err, data) {
    if (err) throw err;

    const isInRange = (numberToCheck, range) => {
      return numberToCheck >= range[0] && numberToCheck <= range[1];
    }

    const elves = [];

    data.split("\n").forEach((stringInput) => {
      var stringAssignments = stringInput.split(",");
      elves.push(stringAssignments.map(m => m.split("-").map(s => parseInt(s))))
    });

    let count = 0;

    for (const pairOfElves of elves) {
      const elf1 = pairOfElves[0];
      const elf2 = pairOfElves[1];

      if (isInRange(elf1[0], elf2) || isInRange(elf1[1], elf2) || isInRange(elf2[0], elf1) || isInRange(elf2[1], elf1)) {
        count++;
      }
    }

    console.log(`Total count of elves with any overlapping assignments is ${count}`)
  });
}

part2();
