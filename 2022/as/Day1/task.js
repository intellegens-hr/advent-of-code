const fs = require("fs");

/* Link to task description: https://adventofcode.com/2022/day/1 */

/**
 * Part 1
 */
function part1() {
  fs.readFile("input.txt", "utf-8", function (err, data) {
    if (err) throw err;

    const finalArray = [];
    let tempArray = [];

    data.split("\n").forEach((stringInput) => {
      if (stringInput !== "") {
        tempArray.push(parseInt(stringInput));
      } else {
        finalArray.push(tempArray.reduce((acc, curr) => acc + curr));
        tempArray = [];
      }
    });

    console.log(
      `The most calories carried by a single elf is ${Math.max(...finalArray)}`
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

    const finalArray = [];
    let tempArray = [];

    data.split("\n").forEach((stringInput) => {
      if (stringInput !== "") {
        tempArray.push(parseInt(stringInput));
      } else {
        finalArray.push(tempArray.reduce((acc, curr) => acc + curr));
        tempArray = [];
      }
    });

    const maxArray = [
      Number.MIN_SAFE_INTEGER,
      Number.MIN_SAFE_INTEGER,
      Number.MIN_SAFE_INTEGER,
    ];

    finalArray.forEach((value) => {
      if (Math.min(value, ...maxArray) !== value) {
        maxArray[maxArray.length - 1] = value;
      }
      maxArray.sort((a, b) => b - a);
    });

    console.log(
      `The most calories carried by top 3 elves is ${maxArray.reduce(
        (acc, curr) => acc + curr
      )}`
    );
  });
}

part2();
