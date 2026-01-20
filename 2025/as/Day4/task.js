const fs = require("fs");

/* Link to task description: https://adventofcode.com/2025/day/4 */

/**
 * Part 1
 */
const match = "@";
const empty = ".";
function part1() {
  fs.readFile("input.txt", "utf-8", function (err, data) {
    if (err) throw err;

    var rows = data.split("\n");
    let eligibleCount = 0;

    rows.forEach((row, idx) => {
      var items = row.split("").filter((x) => x === match || x === empty);
      items.forEach((item, itemIdx) => {
        if (item === match) {
          const adjacentCount = getAdjacentCount(
            rows,
            idx,
            itemIdx,
            items.length
          );

          if (adjacentCount < 4) eligibleCount++;
        }
      });
    });

    console.log(`Total count is ${eligibleCount}.`);
  });
}

part1();

function getAdjacentCount(rows, rowNumber, index, rowLength) {
  let count = 0;
  const rowTotalCount = rows.length;

  // Diagonal top left
  if (rowNumber > 0 && index > 0 && rows[rowNumber - 1][index - 1] === match)
    count++;

  // Top
  if (rowNumber > 0 && rows[rowNumber - 1][index] === match) count++;

  // Diagonal top right
  if (
    rowNumber > 0 &&
    index + 1 < rowLength &&
    rows[rowNumber - 1][index + 1] === match
  )
    count++;

  // Left
  if (index > 0 && rows[rowNumber][index - 1] === match) count++;

  // Right
  if (index + 1 < rowLength && rows[rowNumber][index + 1] === match) count++;

  // Diagonal bottom left
  if (
    rowNumber + 1 < rowTotalCount &&
    index > 0 &&
    rows[rowNumber + 1][index - 1] === match
  )
    count++;

  // Bottom
  if (rowNumber + 1 < rowTotalCount && rows[rowNumber + 1][index] === match)
    count++;

  // Diagonal bottom right
  if (
    rowNumber + 1 < rowTotalCount &&
    index + 1 < rowLength &&
    rows[rowNumber + 1][index + 1] === match
  )
    count++;

  return count;
}

/**
 * Part 2
 */
function part2() {
  fs.readFile("input.txt", "utf-8", function (err, data) {
    if (err) throw err;

    var rows = data.split("\n");
    var rows = rows.map((x) =>
      x.split("").filter((x) => x === match || x === empty)
    );

    const rowsCleared = {};

    rows.forEach((_, idx) => {
      rowsCleared[idx] = { FirstRun: false, FinalRun: false };
    });

    let calculationsDone = false;
    let eligibleCount = 0;
    let eligibleCountChanged = false;
    let i = 0;

    while (!calculationsDone) {
      const row = rows[i];

      let rowDone = true;
      for (let j = 0; j < row.length; j++) {
        const item = row[j];

        if (item === match) {
          const adjacentCount = getAdjacentCount(rows, i, j, row.length);

          if (adjacentCount < 4) {
            eligibleCount++;
            eligibleCountChanged = true;
            rows[i][j] = empty;
            rowDone = false;
            break;
          }
        }
      }

      if (rowDone) {
        if (i + 1 < rows.length) {
          i++;
        } else {
          if (eligibleCountChanged) {
            i = 0;
            eligibleCountChanged = false;
          } else {
            calculationsDone = true;
          }
        }
      }
    }

    console.log(`Total count is ${eligibleCount}.`);
  });
}

part2();
