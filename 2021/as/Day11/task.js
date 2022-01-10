const fs = require("fs");

/* Link to task description: https://adventofcode.com/2021/day/11 */

/**
 * Part 1
 */
function part1() {
  function addEnergyToAdjacentOctopuses(
    arrayOfInputs,
    rowIndex,
    locationIndex
  ) {
    if (
      arrayOfInputs[rowIndex][locationIndex - 1] ||
      arrayOfInputs[rowIndex][locationIndex - 1] === 0
    ) {
      arrayOfInputs[rowIndex][locationIndex - 1]++;
    }

    if (
      arrayOfInputs[rowIndex][locationIndex + 1] ||
      arrayOfInputs[rowIndex][locationIndex + 1] === 0
    ) {
      arrayOfInputs[rowIndex][locationIndex + 1]++;
    }

    if (
      arrayOfInputs[rowIndex - 1] &&
      (arrayOfInputs[rowIndex - 1][locationIndex] ||
        arrayOfInputs[rowIndex - 1][locationIndex] === 0)
    ) {
      arrayOfInputs[rowIndex - 1][locationIndex]++;
    }

    if (
      arrayOfInputs[rowIndex + 1] &&
      (arrayOfInputs[rowIndex + 1][locationIndex] ||
        arrayOfInputs[rowIndex + 1][locationIndex] === 0)
    ) {
      arrayOfInputs[rowIndex + 1][locationIndex]++;
    }

    if (
      arrayOfInputs[rowIndex - 1] &&
      (arrayOfInputs[rowIndex - 1][locationIndex - 1] ||
        arrayOfInputs[rowIndex - 1][locationIndex - 1] === 0)
    ) {
      arrayOfInputs[rowIndex - 1][locationIndex - 1]++;
    }

    if (
      arrayOfInputs[rowIndex - 1] &&
      (arrayOfInputs[rowIndex - 1][locationIndex + 1] ||
        arrayOfInputs[rowIndex - 1][locationIndex + 1] === 0)
    ) {
      arrayOfInputs[rowIndex - 1][locationIndex + 1]++;
    }

    if (
      arrayOfInputs[rowIndex + 1] &&
      (arrayOfInputs[rowIndex + 1][locationIndex - 1] ||
        arrayOfInputs[rowIndex + 1][locationIndex - 1] === 0)
    ) {
      arrayOfInputs[rowIndex + 1][locationIndex - 1]++;
    }

    if (
      arrayOfInputs[rowIndex + 1] &&
      (arrayOfInputs[rowIndex + 1][locationIndex + 1] ||
        arrayOfInputs[rowIndex + 1][locationIndex + 1] === 0)
    ) {
      arrayOfInputs[rowIndex + 1][locationIndex + 1]++;
    }
  }

  function addOneEnergyLevelToEach(arrayOfInputs) {
    let rowIndex = 0;
    for (let row of arrayOfInputs) {
      let octopusIndex = 0;
      for (let octopus of row) {
        arrayOfInputs[rowIndex][octopusIndex]++;
        octopusIndex++;
      }
      rowIndex++;
    }
  }

  function revertAllFlashedOctopusesToZero(arrayOfInputs) {
    let rowIndex = 0;

    for (let row of arrayOfInputs) {
      let octopusIndex = 0;
      for (let octopus of row) {
        if (octopus > 9) {
          arrayOfInputs[rowIndex][octopusIndex] = 0;
        }
        octopusIndex++;
      }
      rowIndex++;
    }
  }

  function simulateStep(
    arrayOfInputs,
    totalNumOfFlashes = 0,
    arrayOfFlashedAlready = []
  ) {
    let rowIndex = 0;
    let tempNumOfFlashes = 0;

    for (const row of arrayOfInputs) {
      let octopusIndex = 0;

      for (const octopus of row) {
        if (octopus > 9) {
          let octopusID = `${rowIndex},${octopusIndex}`;
          if (!arrayOfFlashedAlready.includes(octopusID)) {
            tempNumOfFlashes++;
            arrayOfFlashedAlready.push(octopusID);
            addEnergyToAdjacentOctopuses(arrayOfInputs, rowIndex, octopusIndex);
          }
        }

        octopusIndex++;
      }
      rowIndex++;
    }

    totalNumOfFlashes += tempNumOfFlashes;

    if (tempNumOfFlashes > 0) {
      return simulateStep(
        arrayOfInputs,
        totalNumOfFlashes,
        arrayOfFlashedAlready
      );
    } else {
      revertAllFlashedOctopusesToZero(arrayOfInputs);
      return totalNumOfFlashes;
    }
  }

  fs.readFile("input.txt", "utf-8", function (err, data) {
    if (err) throw err;
    const arrayOfInputs = data
      .split("\n")
      .map((s) => s.split("").map((s) => parseInt(s)));
    let sumOfFlashes = 0;

    // Simulate 100 steps
    for (let i = 0; i < 100; i++) {
      addOneEnergyLevelToEach(arrayOfInputs);

      sumOfFlashes += simulateStep(arrayOfInputs);
    }

    console.log(`Total number of flashes for 100 steps is: ${sumOfFlashes}`);
  });
}

part1();

function part2() {
  function addEnergyToAdjacentOctopuses(
    arrayOfInputs,
    rowIndex,
    locationIndex
  ) {
    if (
      arrayOfInputs[rowIndex][locationIndex - 1] ||
      arrayOfInputs[rowIndex][locationIndex - 1] === 0
    ) {
      arrayOfInputs[rowIndex][locationIndex - 1]++;
    }

    if (
      arrayOfInputs[rowIndex][locationIndex + 1] ||
      arrayOfInputs[rowIndex][locationIndex + 1] === 0
    ) {
      arrayOfInputs[rowIndex][locationIndex + 1]++;
    }

    if (
      arrayOfInputs[rowIndex - 1] &&
      (arrayOfInputs[rowIndex - 1][locationIndex] ||
        arrayOfInputs[rowIndex - 1][locationIndex] === 0)
    ) {
      arrayOfInputs[rowIndex - 1][locationIndex]++;
    }

    if (
      arrayOfInputs[rowIndex + 1] &&
      (arrayOfInputs[rowIndex + 1][locationIndex] ||
        arrayOfInputs[rowIndex + 1][locationIndex] === 0)
    ) {
      arrayOfInputs[rowIndex + 1][locationIndex]++;
    }

    if (
      arrayOfInputs[rowIndex - 1] &&
      (arrayOfInputs[rowIndex - 1][locationIndex - 1] ||
        arrayOfInputs[rowIndex - 1][locationIndex - 1] === 0)
    ) {
      arrayOfInputs[rowIndex - 1][locationIndex - 1]++;
    }

    if (
      arrayOfInputs[rowIndex - 1] &&
      (arrayOfInputs[rowIndex - 1][locationIndex + 1] ||
        arrayOfInputs[rowIndex - 1][locationIndex + 1] === 0)
    ) {
      arrayOfInputs[rowIndex - 1][locationIndex + 1]++;
    }

    if (
      arrayOfInputs[rowIndex + 1] &&
      (arrayOfInputs[rowIndex + 1][locationIndex - 1] ||
        arrayOfInputs[rowIndex + 1][locationIndex - 1] === 0)
    ) {
      arrayOfInputs[rowIndex + 1][locationIndex - 1]++;
    }

    if (
      arrayOfInputs[rowIndex + 1] &&
      (arrayOfInputs[rowIndex + 1][locationIndex + 1] ||
        arrayOfInputs[rowIndex + 1][locationIndex + 1] === 0)
    ) {
      arrayOfInputs[rowIndex + 1][locationIndex + 1]++;
    }
  }

  function addOneEnergyLevelToEach(arrayOfInputs) {
    let rowIndex = 0;
    for (let row of arrayOfInputs) {
      let octopusIndex = 0;
      for (let octopus of row) {
        arrayOfInputs[rowIndex][octopusIndex]++;
        octopusIndex++;
      }
      rowIndex++;
    }
  }

  function revertAllFlashedOctopusesToZero(arrayOfInputs) {
    let rowIndex = 0;

    for (let row of arrayOfInputs) {
      let octopusIndex = 0;
      for (let octopus of row) {
        if (octopus > 9) {
          arrayOfInputs[rowIndex][octopusIndex] = 0;
        }
        octopusIndex++;
      }
      rowIndex++;
    }
  }

  function simulateStep(
    arrayOfInputs,
    totalNumOfFlashes = 0,
    arrayOfFlashedAlready = []
  ) {
    let rowIndex = 0;
    let tempNumOfFlashes = 0;
    let totalNumberOfOctopuses = 0;
    for (const row of arrayOfInputs) {
      let octopusIndex = 0;

      for (const octopus of row) {
        if (octopus > 9) {
          let octopusID = `${rowIndex},${octopusIndex}`;
          if (!arrayOfFlashedAlready.includes(octopusID)) {
            tempNumOfFlashes++;
            arrayOfFlashedAlready.push(octopusID);
            addEnergyToAdjacentOctopuses(arrayOfInputs, rowIndex, octopusIndex);
          }
        }
        totalNumberOfOctopuses++;
        octopusIndex++;
      }
      rowIndex++;
    }

    totalNumOfFlashes += tempNumOfFlashes;

    if (tempNumOfFlashes > 0) {
      return simulateStep(
        arrayOfInputs,
        totalNumOfFlashes,
        arrayOfFlashedAlready
      );
    } else {
      revertAllFlashedOctopusesToZero(arrayOfInputs);

      if (totalNumberOfOctopuses === totalNumOfFlashes) {
        return true;
      } else {
        return false;
      }
    }
  }

  fs.readFile("input.txt", "utf-8", function (err, data) {
    if (err) throw err;
    const arrayOfInputs = data
      .split("\n")
      .map((s) => s.split("").map((s) => parseInt(s)));
    let flashFoundIndex = 0;
    let stepFound = false;

    // Simulate 100 steps
    for (let i = 0; !stepFound; i++) {
      addOneEnergyLevelToEach(arrayOfInputs);
      stepFound = simulateStep(arrayOfInputs);

      if (stepFound) {
        flashFoundIndex = i;
        break;
      }
    }
    console.log(
      `First step at which all octopuses flash is: ${flashFoundIndex + 1}`
    );
  });
}

part2();
