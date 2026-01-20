const fs = require("fs");

/* Link to task description: https://adventofcode.com/2025/day/10 */

/**
 * Part 1
 */
function part1() {
  fs.readFile("input.txt", "utf-8", function (err, data) {
    if (err) throw err;

    const indicatorData = {};

    // Parse data
    data.split("\n").forEach((row, rowIdx) => {
      row.split(" ").forEach((rowItem) => {
        if (indicatorData[rowIdx] === undefined)
          indicatorData[rowIdx] = {
            Indicators: [],
            Buttons: [],
            Min: Number.MAX_SAFE_INTEGER,
          };
        // Handle indicators
        if (rowItem[0] === "[")
          indicatorData[rowIdx].Indicators = rowItem.slice(
            1,
            rowItem.length - 1
          );

        // Handle buttons
        if (rowItem[0] === "(") {
          indicatorData[rowIdx].Buttons.push(
            rowItem
              .slice(1, rowItem.length - 1)
              .split(",")
              .map((x) => parseInt(x))
          );
        }
      });
    });

    let currentRowIdx = 0;
    let curentRowButtonPress = 1;
    while (currentRowIdx < Object.keys(indicatorData).length) {
      var currentButtons = indicatorData[currentRowIdx].Buttons;
      var desiredConfiguration = indicatorData[currentRowIdx].Indicators;
      var currentMin = Number.MAX_SAFE_INTEGER;

      var combinations = getAllCombinationsForButton(
        currentButtons.length,
        curentRowButtonPress
      );

      for (let i = 0; i < combinations.length; i++) {
        let currentConfiguration = ".".repeat(desiredConfiguration.length);
        let currentPressCount = combinations[i].length;

        if (currentPressCount >= currentMin) continue;

        for (let j = 0; j < combinations[i].length; j++) {
          currentConfiguration = pressButton(
            currentConfiguration,
            currentButtons[combinations[i][j]]
          );
        }

        if (currentConfiguration === desiredConfiguration) {
          currentMin = currentPressCount;
        }
      }

      // If new min is defined => break and start new row
      if (currentMin !== Number.MAX_SAFE_INTEGER) {
        indicatorData[currentRowIdx].Min = currentMin;
        currentRowIdx++;
        curentRowButtonPress = 1;
      }
      // Raise allowed press count
      else {
        curentRowButtonPress++;
      }
    }

    var sum = Object.values(indicatorData).reduce(
      (acc, curr) => acc + curr.Min,
      0
    );
    console.log(`Total sum of fewest possible combinations is ${sum}`);
  });
}

function pressButton(currentConfiguration, buttonIndexes) {
  buttonIndexes.forEach((buttonIdx) => {
    currentConfiguration =
      currentConfiguration.substring(0, buttonIdx) +
      (currentConfiguration[buttonIdx] === "#" ? "." : "#") +
      currentConfiguration.substring(buttonIdx + 1);
  });

  return currentConfiguration;
}

function getAllCombinationsForButton(buttonCount, maxLength) {
  var combinations = [[]];

  for (let i = 0; i < maxLength; i++) {
    var current = [];

    for (let j = 0; j < combinations.length; j++) {
      var currentCombo = combinations[j];

      // remove same items in different order
      var start =
        currentCombo.length === 0
          ? 0
          : currentCombo[currentCombo.length - 1] + 1;

      for (let z = start; z < buttonCount; z++) {
        if (combinations[j].includes(z)) continue;

        current.push([...combinations[j], z]);
      }
    }

    combinations.push(...current);
  }

  return combinations.filter((x) => x.length > 0 && x.length <= maxLength);
}

part1();

/**
 * Part 2
 */
const indicatorData = {};

function part2() {
  fs.readFile("input.txt", "utf-8", function (err, data) {
    if (err) throw err;

    // Parse data
    data.split("\n").forEach((row, rowIdx) => {
      row.split(" ").forEach((rowItem) => {
        if (indicatorData[rowIdx] === undefined)
          indicatorData[rowIdx] = {
            JoltageRequired: [],
            Buttons: [],
            Min: Number.MAX_SAFE_INTEGER,
          };
        // Handle joltage
        if (rowItem[0] === "{")
          indicatorData[rowIdx].JoltageRequired = [
            ...rowItem
              .slice(1, rowItem.length - 1)
              .split(",")
              .map((x) => parseInt(x)),
          ];

        // Handle buttons
        if (rowItem[0] === "(") {
          indicatorData[rowIdx].Buttons.push(
            rowItem
              .slice(1, rowItem.length - 1)
              .split(",")
              .map((x) => parseInt(x))
          );
        }
      });
    });

    var numberOfRows = Object.keys(indicatorData).length;

    let currentRowIdx = 0;
    while (currentRowIdx < numberOfRows) {
      // Sort buttons by size, higher to lower
      let currentButtons = indicatorData[currentRowIdx].Buttons.sort((a, b) => b.length - a.length);;
      let desiredJoltage = indicatorData[currentRowIdx].JoltageRequired;
      indicatorData[currentRowIdx].Min = desiredJoltage.reduce((acc, curr) => acc + curr, 0);

      let maxPressCount = new Array(currentButtons.length).fill(0);

      // Loop each button and calculate how many times it can be pressed before reaching desired joltage for each index
      for (let j = 0; j < currentButtons.length; j++) {
        let min =  indicatorData[currentRowIdx].Min;

        for (let k = 0; k < currentButtons[j].length; k++) {
          let btnIdx = currentButtons[j][k];
          if (desiredJoltage[btnIdx] < min) min = desiredJoltage[btnIdx];
        }

        // Save max press count
        maxPressCount[j] = min;
      }

      // Press counters
      let currentPressArray = new Array(currentButtons.length).fill(0);
      let isComplete = false;

      while (!isComplete) {
        var tempJoltage = new Array(
          indicatorData[currentRowIdx].JoltageRequired.length
        ).fill(0);

        let totalPressCount = 0;
        let valid = true;

        for (let j = 0; j < currentButtons.length; j++) {
          if (currentPressArray[j] === 0) continue;

          // Break early if total press count already above current min
          totalPressCount += currentPressArray[j];
          if (totalPressCount >= indicatorData[currentRowIdx].Min) {
            valid = false;
            break;
          }

          for (let k = 0; k < currentButtons[j].length; k++) {
            let idx = currentButtons[j][k];
            tempJoltage[idx] += currentPressArray[j];

            if (tempJoltage[idx] > desiredJoltage[idx]) {
              valid = false;
              break;
            }
          }

          // Break early if any joltage exceeds desired count
          if (!valid) break;
        }

        // Mark as invalid if any current != desired
        if (valid) {
          for (let j = 0; j < desiredJoltage.length; j++) {
            if (tempJoltage[j] !== desiredJoltage[j]) {
              valid = false;
              break;
            }
          }
        }

        // Update minimum if valid and smaller than previous min
        if (valid && totalPressCount < indicatorData[currentRowIdx].Min) {
          indicatorData[currentRowIdx].Min = totalPressCount;
        }

        // Get next press combination
        let currentPosition = 0;
        while (currentPosition < currentButtons.length) {
          currentPressArray[currentPosition]++;
          if (
            currentPressArray[currentPosition] <= maxPressCount[currentPosition]
          )
            break;
          currentPressArray[currentPosition] = 0;
          currentPosition++;
        }

        if (currentPosition === currentButtons.length) isComplete = true;
      }

      currentRowIdx++;
    }

    var sum = Object.values(indicatorData).reduce(
      (acc, curr) => acc + curr.Min,
      0
    );
    console.log(`Total sum of fewest possible combinations is ${sum}`);
  });
}

part2();
