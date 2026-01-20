const fs = require("fs");

/* Link to task description: https://adventofcode.com/2025/day/6 */

/**
 * Part 1
 */
function part1() {
  fs.readFile("input.txt", "utf-8", function (err, data) {
    if (err) throw err;

    var rows = data.split("\n");
    var commands = rows[rows.length - 1]
      .split(" ")
      .filter((x) => x.trim() !== "");

    var allNumsPerColumn = {};

    rows.forEach((row, idx) => {
      if (idx === rows.length - 1) return;

      row
        .split(" ")
        .filter((x) => x.trim() !== "")
        .forEach((num, idx) => {
          if (allNumsPerColumn[idx] === undefined) allNumsPerColumn[idx] = [];

          allNumsPerColumn[idx].push(parseInt(num));
        });
    });

    let totalCount = 0;

    Object.entries(allNumsPerColumn).forEach(([key, value]) => {
      var command = commands[key];
      const columnCount = value.reduce(
        (acc, val) => {
          return calc(command, acc, val);
        },
        command === "*" ? 1 : 0
      );

      totalCount += columnCount;
    });

    console.log(`Total count of all column operations is: ${totalCount}.`);
  });
}

function calc(operator, value1, value2) {
  if (operator === "+") return value1 + value2;
  else if (operator === "-") return value1 - value2;
  else if (operator === "*") return value1 * value2;
  else {
    console.log("---- Unknown operator ----" + operator);
  }
}

part1();

/**
 * Part 2
 */
function part2() {
  fs.readFile("input.txt", "utf-8", function (err, data) {
    if (err) throw err;

    var rows = data.split("\n");
    var commands = rows[rows.length - 1]
      .split(" ")
      .filter((x) => x.trim() !== "")
      .reverse();

    var allCharactersPerColumn = {};
    const zeroMark = "X";

    // 1. Odrediti max length za svaki column (- 1 jer ignoriramo komande)
    for (let i = 0; i < rows.length - 1; i++) {
      const row = rows[i];
      var characters = row.split(" ").filter((x) => x !== "");
      for (let j = 0; j < characters.length; j++) {
        if (allCharactersPerColumn[j] === undefined)
          allCharactersPerColumn[j] = 0;

        if (characters[j].length > allCharactersPerColumn[j])
          allCharactersPerColumn[j] = characters[j].length;
      }
    }
    var numsPerColumn = {};

    // 2. Ispuniti praznine u ostalim brojevima => praznina moze biti s obje strane
    for (let i = 0; i < rows.length - 1; i++) {
      const characters = rows[i].split("");
      const nums = [];
      let currentNum = "";
      let currentColumn = Object.keys(allCharactersPerColumn).length - 1;

      let skipNext = false;
      for (let j = characters.length - 1; j >= 0; j--) {
        if (skipNext) {
          skipNext = false;
          continue;
        }

        let numToPush = characters[j] === " " ? zeroMark : characters[j];
        currentNum = numToPush + currentNum;

        if (currentNum.length === allCharactersPerColumn[currentColumn]) {
          nums.push(currentNum);
          currentColumn--;
          currentNum = "";
          skipNext = true;
        }
      }

      nums.forEach((num, idx) => {
        if (numsPerColumn[idx] === undefined) numsPerColumn[idx] = [];

        numsPerColumn[idx].push(num);
      });
    }

    // 3. Izvršiti operacije, i gledati da se X ignorira
    let totalCount = 0;
    var keyCount = Object.keys(numsPerColumn).length;
    Object.entries(numsPerColumn).forEach(([key, value]) => {
      var adaptedKey = keyCount - 1 - key;

      var command = commands[key];
      const columnNums = [];
      for (let i = 0; i < allCharactersPerColumn[adaptedKey]; i++) {
        let currentNums = [];
        for (let j = 0; j < value.length; j++) {
          var char = value[j][i];

          if (char !== zeroMark) currentNums.push(char);
        }

        columnNums.push(parseInt(currentNums.join("")));
      }

      const columnCount = columnNums.reduce(
        (acc, curr) => {
          return calc(command, acc, curr);
        },
        command === "*" ? 1 : 0
      );
      totalCount += columnCount;
    });

    console.log(`Total count of all column operations is: ${totalCount}.`);
  });
}

part2();
