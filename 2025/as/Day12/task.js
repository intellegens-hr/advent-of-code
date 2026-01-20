const fs = require("fs");

/* Link to task description: https://adventofcode.com/2025/day/12 */

/**
 * Part 1
 */
function part1() {
  fs.readFile("input.txt", "utf-8", function (err, data) {
    if (err) throw err;

    const shapesDict = {};
    const areaList = [];



    // Split data
    let currentShape = [];
    let currentShapeIndex;
    const numberOfGifts = 5;
    let giftsMapped = 0;

    data.split("\n").forEach((row) => {
      // Handle shapes
      if (giftsMapped <= numberOfGifts) {
        // New shape start
        if (row[1] === ":") {
          currentShapeIndex = parseInt(row[0]);
        }

        // Shape row
        else if (row.trim() !== "") {
          var marks = row.split("");
          var shapeRowArray = [];

          marks.forEach((mark, idx) => {
            if (mark === "#") {
              shapeRowArray.push(idx);
            }
          });
          currentShape.push(shapeRowArray);
        } else {
          shapesDict[currentShapeIndex] = currentShape;
          currentShapeIndex = undefined;
          currentShape = [];
          giftsMapped++;
        }
      }

      // Handle areas
      else {
        var areaEntry = {};
        var [area, reqs] = row.split(":");
        var areaMarks = area.split("x");
        areaEntry.SpaceAvailable = [
          parseInt(areaMarks[0]),
          parseInt(areaMarks[1]),
        ];

        var reqItems = reqs.split(" ").filter((x) => x.trim() !== "");
        reqItems.forEach((reqItem, idx) => {
          areaEntry[idx] = parseInt(reqItem);
        });

        areaList.push(areaEntry);
      }
    });

    var eligibleAreaList = [];

    // Filter out gifts that can't fit in any shape
    for (let i = 0; i < areaList.length; i++) {
      const areaSpace =
        areaList[i].SpaceAvailable[0] * areaList[i].SpaceAvailable[1];
      let filledCount = 0;
      let outOfBound = false;

      for (let j = 0; j <= numberOfGifts; j++) {
        var shapeCount = shapesDict[j].reduce(
          (acc, curr) => acc + curr.length,
          0
        );
      filledCount += (areaList[i][j] || 0) * shapeCount;
        if (filledCount > areaSpace) {
          outOfBound = true;
          break;
        }
      }

      if (!outOfBound) {
        eligibleAreaList.push(areaList[i]);
      }
    }

    var giftsThatCanFitInAnyShape =  [];

    // Gifts that can fit in any shape
    for (let i = 0; i < eligibleAreaList.length; i++) {
      const areaSpace =
        eligibleAreaList[i].SpaceAvailable[0] *
        eligibleAreaList[i].SpaceAvailable[1];
      let filledCount = 0;
      let outOfBound = false;

      for (let j = 0; j <= numberOfGifts; j++) {
        filledCount += eligibleAreaList[i][j] * 9;

        if (filledCount > areaSpace) {
          outOfBound = true;
          break;
        }
      }

      if (!outOfBound) {
        giftsThatCanFitInAnyShape.push(areaList[i]);
      }
    }

    if(eligibleAreaList.length == giftsThatCanFitInAnyShape.length)
      console.log(`Total number of areas that can fit their gifts is ${giftsThatCanFitInAnyShape.length}`)
    else
      throw new Error("Get a pen & pencil then");
  });
}

part1();
