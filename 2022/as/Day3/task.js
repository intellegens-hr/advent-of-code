const fs = require("fs");

/* Link to task description: https://adventofcode.com/2022/day/3 */

/**
 * Part 1
 */
function part1() {
  fs.readFile("input.txt", "utf-8", function (err, data) {
    if (err) throw err;

    let sum = 0;

    data.split("\n").forEach((stringInput) => {
      const firstHalf = stringInput.substring(0, stringInput.length / 2).split("");
      const secondHalf = stringInput.substring(stringInput.length / 2).split("");
      const joinLeft = firstHalf.filter(l => secondHalf.includes(l));
      const joinRight = secondHalf.filter(l => firstHalf.includes(l));
      const charCode = joinLeft.find(l => joinRight.includes(l)).charCodeAt(0);

      if (charCode >= 65 && charCode <= 90) {
        sum += (parseInt(charCode) - 65) + 27
      } else if (charCode >= 97 && charCode <= 122) {
        sum += (parseInt(charCode) - 97) + 1
      }
    });

    console.log(`Total priority sum is ${sum}`)
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
    let group = [];
    let badges = [];

    data.split("\n").forEach((stringInput, idx) => {
      group.push(stringInput.split(""))

      if ((idx + 1) % 3 === 0) {
        var letter = group[0].find(l => group[1].includes(l) && group[2].includes(l))
        badges.push(letter);
        group = [];
      }
    });

    badges.forEach(badge => {
      const charCode = badge.charCodeAt(0);
      if (charCode >= 65 && charCode <= 90) {
        sum += (parseInt(charCode) - 65) + 27
      } else if (charCode >= 97 && charCode <= 122) {
        sum += (parseInt(charCode) - 97) + 1
      }
    })

    console.log(`Total badge priority sum is ${sum}`)
  });
}

part2();
