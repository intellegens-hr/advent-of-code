const fs = require("fs");

/* Link to task description: https://adventofcode.com/2025/day/1 */

/**
 * Part 1
 */
const minDial = 0;
const maxDial = 99;
const numberOfDials = 100;
const numberToSeek = 0;

function part1() {
  fs.readFile("input.txt", "utf-8", function (err, data) {
    if (err) throw err;

    let currentLocation = 50;
    let zeroOccurrences = 0;

    data.split("\n").forEach((stringInput) => {
      var inputs = stringInput.split("\n");

      inputs.forEach((rotation) => {
        currentLocation = rotateDialPart1(currentLocation, rotation);

        if (currentLocation === numberToSeek) {
          zeroOccurrences++;
        }
      });
    });

    console.log(`Total number of 0 occurrences is ${zeroOccurrences}`);
  });
}

function rotateDialPart1(currentLocation, rotation) {
  const id = rotation[0];
  const numberOfRotations = normalizeNumberPart1(parseInt(rotation.slice(1)));

  if (id === "L" && currentLocation - numberOfRotations < minDial) {
    return Math.abs(100 - Math.abs(numberOfRotations - currentLocation));
  } else if (id === "R" && currentLocation + numberOfRotations >= maxDial) {
    return currentLocation + numberOfRotations - 100;
  }

  return id === "L"
    ? currentLocation - numberOfRotations
    : currentLocation + numberOfRotations;
}

function normalizeNumberPart1(rotation) {
  if (rotation < numberOfDials) return rotation;

  return normalizeNumberPart1(rotation - numberOfDials);
}

part1();

/**
 * Part 2
 */
function part2() {
  fs.readFile("input.txt", "utf-8", function (err, data) {
    if (err) throw err;

    let currentLocation = 50;
    let zeroOccurrences = 0;

    data.split("\n").forEach((stringInput) => {
      var inputs = stringInput.split("\n");

      inputs.forEach((rotation) => {
        const [newLocation, newZeroOccurrences] = rotateDialPart2(
          currentLocation,
          rotation
        );
        currentLocation = newLocation;
        zeroOccurrences += newZeroOccurrences;
      });
    });

    console.log(`Total number of 0 occurrences is ${zeroOccurrences}`);
  });
}

function rotateDialPart2(currentLocation, rotationCombination) {
  const id = rotationCombination[0];
  const inputRotation = parseInt(rotationCombination.slice(1));

  let newLocation = currentLocation;
  let zeroOccurrences = 0;

  for (let i = 0; i < inputRotation; i++) {
    // Handle left dial
    if (id === "L") {
      if (newLocation - 1 < minDial) {
        newLocation = maxDial;
      } else {
        newLocation--;
      }
      if (newLocation === 0) zeroOccurrences++;

      // Handle right dial
    } else {
      if (newLocation + 1 > maxDial) {
        newLocation = minDial;
      } else {
        newLocation++;
      }
      if (newLocation === 0) zeroOccurrences++;
    }
  }

  return [newLocation, zeroOccurrences];
}

part2();
