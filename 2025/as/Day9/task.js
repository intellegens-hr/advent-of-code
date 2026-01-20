const fs = require("fs");

/* Link to task description: https://adventofcode.com/2025/day/9 */

/**
 * Part 1
 */
function part1() {
  fs.readFile("input.txt", "utf-8", function (err, data) {
    if (err) throw err;

    var redTileCoordinates = [];
    data.split("\n").forEach((row) => {
      var coordinates = row.split(",");
      redTileCoordinates.push(coordinates.map((x) => parseInt(x)));
    });

    let maxSquareCount = 0;

    for (let i = 0; i < redTileCoordinates.length; i++) {
      for (let j = i; j < redTileCoordinates.length; j++) {
        if (i === j) continue;

        var squareCountCandidate = calcSquares(
          redTileCoordinates[i],
          redTileCoordinates[j]
        );
        if (squareCountCandidate > maxSquareCount)
          maxSquareCount = squareCountCandidate;
      }
    }

    console.log(`Max square are between two red tiles is: ${maxSquareCount}`);
  });
}

function calcSquares(coords1, coords2) {
  return (
    (Math.abs(coords1[0] - coords2[0]) + 1) *
    (Math.abs(coords1[1] - coords2[1]) + 1)
  );
}

part1();

const LEFT = "LEFT";
const RIGHT = "RIGHT";
const TOP = "TOP";
const BOTTOM = "BOTTOM";

/**
 * Part 2
 */
function part2() {
  fs.readFile("input.txt", "utf-8", function (err, data) {
    if (err) throw err;

    const eligibleRowPositions = {};

    var redTileCoordinates = [];
    var rows = data.split("\n");
    rows.forEach((row) => {
      var coordinates = row.split(",");

      if (eligibleRowPositions[coordinates[1]] === undefined)
        eligibleRowPositions[coordinates[1]] = [];

      eligibleRowPositions[coordinates[1]].push(parseInt(coordinates[0]));
      redTileCoordinates.push(coordinates.map((x) => parseInt(x)));
    });

    // Mark all green tiles by straight line
    for (let i = 0; i < redTileCoordinates.length; i++) {
      var row1, row2;

      if (i === redTileCoordinates.length - 1) {
        row1 = redTileCoordinates[i];
        row2 = redTileCoordinates[0];
      } else {
        row1 = redTileCoordinates[i];
        row2 = redTileCoordinates[i + 1];
      }

      var greenTilesInBetween = getTilesInBetween(row1, row2);
      greenTilesInBetween.forEach((tileCoordinate) => {
        if (eligibleRowPositions[tileCoordinate[1]] === undefined)
          eligibleRowPositions[tileCoordinate[1]] = [];

        eligibleRowPositions[tileCoordinate[1]].push(tileCoordinate[0]);
      });
    }

    const eligibleRowBoundaries = {};

    // Add looped green tiles
    Object.entries(eligibleRowPositions).forEach(([key, value]) => {
      eligibleRowBoundaries[key] = [Math.min(...value), Math.max(...value)];
    });

    // Calculate max square area
    let maxSquareCount = 0;

    var sortedRedLineCoordinatesByRow = redTileCoordinates.sort(
      (a, b) => a[1] - b[1]
    );

    for (let i = 0; i < sortedRedLineCoordinatesByRow.length; i++) {
      for (let j = i; j < sortedRedLineCoordinatesByRow.length; j++) {
        if (
          i === j ||
          !isCoordinateCombinationEligible(
            sortedRedLineCoordinatesByRow[i],
            sortedRedLineCoordinatesByRow[j],
            eligibleRowBoundaries
          )
        )
          continue;

        var squareCountCandidate = calcSquares(
          sortedRedLineCoordinatesByRow[i],
          sortedRedLineCoordinatesByRow[j]
        );
        if (squareCountCandidate > maxSquareCount)
          maxSquareCount = squareCountCandidate;
      }
    }

    console.log(`Max square are between two red tiles is: ${maxSquareCount}`);
  });
}

function isCoordinateCombinationEligible(
  coords1,
  coords2,
  eligibleRowBoundaries
) {
  const minRowRange = coords1[0] < coords2[0] ? coords1[0] : coords2[0];
  const maxRowRange = coords1[0] > coords2[0] ? coords1[0] : coords2[0];

  for (let i = coords1[1]; i < coords2[1]; i++) {
    if (
      minRowRange < eligibleRowBoundaries[i][0] ||
      maxRowRange > eligibleRowBoundaries[i][1]
    )
      return false;
  }

  return true;
}

function getDirection(coords1, coords2) {
  // Move horizontally
  if (coords1[1] === coords2[1]) {
    return coords1[0] > coords2[0] ? LEFT : RIGHT;
  }
  // Move vertically
  else if (coords1[0] === coords2[0]) {
    return coords1[1] > coords2[1] ? TOP : BOTTOM;
  } else {
    console.log("Invalid movement detected");
  }
}

function getTilesInBetween(coords1, coords2) {
  var direction = getDirection(coords1, coords2);
  const tilesInBetween = [];

  // Top scenario: 9, 7 => 9, 5
  if (direction === TOP) {
    for (let i = coords2[1] + 1; i < coords1[1]; i++) {
      tilesInBetween.push([coords1[0], i]);
    }
  }

  // Bottom scenario: 11, 1 => 11, 7
  else if (direction === BOTTOM) {
    for (let i = coords1[1] + 1; i < coords2[1]; i++) {
      tilesInBetween.push([coords1[0], i]);
    }
  }

  // Right scenario: 5, 7 => 9, 7
  else if (direction === RIGHT) {
    for (let i = coords1[0] + 1; i < coords2[0]; i++) {
      tilesInBetween.push([i, coords1[1]]);
    }
  }

  // Left scenario: 7, 7 => 5, 7
  else if (direction === LEFT) {
    for (let i = coords2[0] + 1; i < coords1[0]; i++) {
      tilesInBetween.push([i, coords1[1]]);
    }
  }

  return tilesInBetween;
}

part2();
