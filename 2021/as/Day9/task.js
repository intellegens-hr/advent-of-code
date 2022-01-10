const fs = require("fs");

/* Link to task description: https://adventofcode.com/2021/day/9 */

/**
 * Part 1
 */
function part1() {
  function getAdjacentLocations(arrayOfInputs, row, rowIndex, locationIndex) {
    let adjacentLocations = [];

    if (row[locationIndex - 1] || row[locationIndex - 1] === 0) {
      adjacentLocations.push(row[locationIndex - 1]);
    }

    if (row[locationIndex + 1] || row[locationIndex + 1] === 0) {
      adjacentLocations.push(row[locationIndex + 1]);
    }

    if (
      arrayOfInputs[rowIndex - 1] &&
      (arrayOfInputs[rowIndex - 1][locationIndex] ||
        arrayOfInputs[rowIndex - 1][locationIndex] === 0)
    ) {
      adjacentLocations.push(arrayOfInputs[rowIndex - 1][locationIndex]);
    }

    if (
      arrayOfInputs[rowIndex + 1] &&
      (arrayOfInputs[rowIndex + 1][locationIndex] ||
        arrayOfInputs[rowIndex + 1][locationIndex] === 0)
    ) {
      adjacentLocations.push(arrayOfInputs[rowIndex + 1][locationIndex]);
    }

    return adjacentLocations;
  }

  function checkIfLowest(numberToCheck, numArray) {
    for (let i = 0; i < numArray.length; i++) {
      if (numberToCheck >= numArray[i]) {
        return false;
      }
    }

    return true;
  }

  fs.readFile("input.txt", "utf-8", function (err, data) {
    if (err) throw err;
    const arrayOfInputs = data.split("\n");
    const parsedInputs = arrayOfInputs.map((i) =>
      i.split("").map((s) => parseInt(s))
    );

    const arrayOfLowPoints = [];

    parsedInputs.forEach((row, rowIndex) => {
      row.forEach((location, locationIndex) => {
        const adjacentLocations = getAdjacentLocations(
          parsedInputs,
          row,
          rowIndex,
          locationIndex
        );

        if (checkIfLowest(location, adjacentLocations)) {
          arrayOfLowPoints.push(location);
        }
      });
    });

    const sum = arrayOfLowPoints.reduce((prevVal, curVal) => {
      return prevVal + (curVal + 1);
    }, 0);
    console.log(`Sum of low points is ${sum}`);
  });
}

part1();

/**
 * Part 2
 */
function part2() {
  class Location {
    constructor(value, rowIndex, locationIndex) {
      this.value = value;
      this.rowIndex = rowIndex;
      this.locationIndex = locationIndex;
    }
  }

  function getAdjacentLocations(arrayOfInputs, rowIndex, locationIndex) {
    let adjacentLocations = [];

    if (
      arrayOfInputs[rowIndex][locationIndex - 1] ||
      arrayOfInputs[rowIndex][locationIndex - 1] === 0
    ) {
      adjacentLocations.push(
        new Location(
          arrayOfInputs[rowIndex][locationIndex - 1],
          rowIndex,
          locationIndex - 1
        )
      );
    }

    if (
      arrayOfInputs[rowIndex][locationIndex + 1] ||
      arrayOfInputs[rowIndex][locationIndex + 1] === 0
    ) {
      adjacentLocations.push(
        new Location(
          arrayOfInputs[rowIndex][locationIndex + 1],
          rowIndex,
          locationIndex + 1
        )
      );
    }

    if (
      arrayOfInputs[rowIndex - 1] &&
      (arrayOfInputs[rowIndex - 1][locationIndex] ||
        arrayOfInputs[rowIndex - 1][locationIndex] === 0)
    ) {
      adjacentLocations.push(
        new Location(
          arrayOfInputs[rowIndex - 1][locationIndex],
          rowIndex - 1,
          locationIndex
        )
      );
    }

    if (
      arrayOfInputs[rowIndex + 1] &&
      (arrayOfInputs[rowIndex + 1][locationIndex] ||
        arrayOfInputs[rowIndex + 1][locationIndex] === 0)
    ) {
      adjacentLocations.push(
        new Location(
          arrayOfInputs[rowIndex + 1][locationIndex],
          rowIndex + 1,
          locationIndex
        )
      );
    }

    return adjacentLocations;
  }

  function findLowest(numberToCheck, numArray) {
    let returnObj = {
      smallest: true,
      lowest: JSON.parse(JSON.stringify(numberToCheck)),
    };
    for (let i = 0; i < numArray.length; i++) {
      if (numberToCheck.value >= numArray[i].value) {
        returnObj.smallest = false;
      }
      if (numArray[i].value < returnObj.lowest.value) {
        returnObj.lowest = JSON.parse(JSON.stringify(numArray[i]));
      }
    }

    return returnObj;
  }

  function getNearestBasin(arrayOfInputs, rowIndex, location, locationIndex) {
    const adjacentLocations = getAdjacentLocations(
      arrayOfInputs,
      rowIndex,
      locationIndex
    );
    const currentNumberAsLocation = new Location(
      location,
      rowIndex,
      locationIndex
    );
    const lowestNum = findLowest(currentNumberAsLocation, adjacentLocations);

    if (lowestNum.smallest) {
      return {
        id: `${currentNumberAsLocation.rowIndex},${currentNumberAsLocation.locationIndex}`,
        num: location,
      };
    } else {
      return getNearestBasin(
        arrayOfInputs,
        lowestNum.lowest.rowIndex,
        lowestNum.lowest.value,
        lowestNum.lowest.locationIndex
      );
    }
  }

  fs.readFile("input.txt", "utf-8", function (err, data) {
    if (err) throw err;
    const arrayOfInputs = data.split("\n");
    const parsedInputs = arrayOfInputs.map((i) =>
      i.split("").map((s) => parseInt(s))
    );

    const basinHashmap = {};

    parsedInputs.forEach((row, rowIndex) => {
      row.forEach((location, locationIndex) => {
        /**
         * Za svaki broj pronaći najbližeg najmanjeg susjeda
         * U slućaju da ima manjeg susjeda, nastaviti tražiti najmanjeg susjeda
         * tog susjeda dok ne dođeš do broja koji je najbliži u susjedstvu
         * Provjeriti u HashMapu da li je taj basin registrovan, ako da, dodati broj u array, ako ne, dodati koristeći unique id key (rowIndex + locationIndex)
         */
        if (location !== 9) {
          const nearestBasin = getNearestBasin(
            parsedInputs,
            rowIndex,
            location,
            locationIndex
          );

          if (basinHashmap.hasOwnProperty(nearestBasin.id)) {
            basinHashmap[nearestBasin.id]++;
          } else {
            basinHashmap[nearestBasin.id] = 1;
          }
        }
      });
    });

    const sortedValues = Object.values(basinHashmap).sort((a, b) => b - a);
    console.log(
      `Multiplied value of three largest basins is ${
        sortedValues[0] * sortedValues[1] * sortedValues[2]
      }`
    );
  });
}

part2();
