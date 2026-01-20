const fs = require("fs");

/* Link to task description: https://adventofcode.com/2025/day/7 */

/**
 * Part 1
 */
function part1() {
  fs.readFile("input.txt", "utf-8", function (err, data) {
    if (err) throw err;

    var rows = data.split("\n");
    var adaptedRows = rows.map((x) => x.split(""));
    const splitMark = "^";
    let beamLocations = [adaptedRows[0].indexOf("S")];
    console.log(beamLocations[0], adaptedRows.length);
    let beamSplitCount = 0;
    for (var i = 0; i < adaptedRows.length; i++) {
      // Return if last
      if (i === adaptedRows.length - 1) break;
      let newBeamLocations = [];

      // Loop current beam locations
      for (let j = 0; j < beamLocations.length; j++) {
        const beamIndex = beamLocations[j];

        if (adaptedRows[i + 1][beamIndex] === splitMark) {
          if (newBeamLocations.find((x) => x === beamIndex - 1) === undefined)
            newBeamLocations.push(beamIndex - 1);
          if (newBeamLocations.find((x) => x === beamIndex + 1) === undefined)
            newBeamLocations.push(beamIndex + 1);

          beamSplitCount++;
        } else {
          if (newBeamLocations.find((x) => x === beamIndex) === undefined)
            newBeamLocations.push(beamIndex);
        }
      }

      beamLocations = newBeamLocations;
    }

    console.log(`Total beam split count is: ${beamSplitCount}.`);
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
    var adaptedRows = rows.map((x) => x.split(""));
    let beamLocation = adaptedRows[0].indexOf("S");

    let currentRow = 0;
    let nextRowIndexes = {};
    let currentRowIndexes = {
      [beamLocation]: 1,
    };
    let beamSplitCount = 0;
    while (currentRow < adaptedRows.length - 1) {
      Object.entries(currentRowIndexes).forEach(([key, value]) => {
        if (adaptedRows[currentRow + 1] !== undefined) {
          var intKey = parseInt(key);

          if (adaptedRows[currentRow + 1][intKey] === ".") {
            if (nextRowIndexes[key] === undefined) nextRowIndexes[key] = 0;

            nextRowIndexes[key] += value;
          } else if (adaptedRows[currentRow + 1][intKey] === "^") {
            beamSplitCount += value;

            var minusKey = (intKey - 1).toString();
            var plusKey = (intKey + 1).toString();
            if (nextRowIndexes[minusKey] === undefined)
              nextRowIndexes[minusKey] = 0;

            if (nextRowIndexes[plusKey] === undefined)
              nextRowIndexes[plusKey] = 0;

            nextRowIndexes[minusKey] += value;
            nextRowIndexes[plusKey] += value;
          }
        }
      });

      currentRow++;

      currentRowIndexes = structuredClone(nextRowIndexes);
      nextRowIndexes = {};
    }

    console.log(`Total beam split count is: ${beamSplitCount + 1}.`);
  });
}

// function getTimelineCount(
//   adaptedRows,
//   currentRowIndex,
//   beamIndex,
//   totalCount = 0
// ) {
//   if (currentRowIndex === adaptedRows.length - 1) {
//     //console.log(`Ending with total count ${totalCount}`);
//     return totalCount;
//   }

//   if (adaptedRows[currentRowIndex + 1][beamIndex] === ".") {
//     // console.log(
//     //   `Encountered empty space, moving to row ${
//     //     currentRowIndex + 2
//     //   } with beam index ${beamIndex} and current count of ${totalCount}`
//     // );
//     return getTimelineCount(
//       adaptedRows,
//       currentRowIndex + 1,
//       beamIndex,
//       totalCount
//     );
//   }

//   if (adaptedRows[currentRowIndex + 1][beamIndex] === "^") {
//     // console.log(
//     //   `*** Ęncountered SPLIT at row ${
//     //     currentRowIndex + 1
//     //   }. Splitting timelines into two, with current count of ${totalCount}`
//     // );
//     totalCount++;
//     return (
//       totalCount +
//       getTimelineCount(adaptedRows, currentRowIndex + 1, beamIndex - 1, 0) +
//       getTimelineCount(adaptedRows, currentRowIndex + 1, beamIndex + 1, 0)
//     );
//   }

//   console.log("Ended before hitting last?");
//   // for (let i = currentRowIndex; i < adaptedRows.length; i++) {
//   //   if (i === adaptedRows.length - 1) return totalCount;

//   //   if (adaptedRows[i + 1][beamIndex] === ".") continue;

//   //   if (adaptedRows[i + 1][beamIndex] === "^") {
//   //     totalCount++;

//   //     totalCount +=
//   //       getTimelineCount(adaptedRows, i + 1, beamIndex - 1, totalCount) +
//   //       getTimelineCount(adaptedRows, i + 1, beamIndex + 1, totalCount);
//   //   }
//   // }
// }

part2();
