const fs = require("fs");

/* Link to task description: https://adventofcode.com/2025/day/8 */

/**
 * Part 1
 */
function part1() {
  fs.readFile("input.txt", "utf-8", function (err, data) {
    if (err) throw err;

    let connectingRequirement = 1000;
    var coordinates = data.split("\n").map((x) => x.split(","));
    var coordinateDistance = {};

    // Calc distances for each possible connection
    for (let i = 0; i < coordinates.length; i++) {
      // Move only downwards + skip itself
      for (let j = i + 1; j < coordinates.length; j++) {
        var coord1 = coordinates[i].map((x) => parseInt(x));
        var coord2 = coordinates[j].map((x) => parseInt(x));
        var distance = calcDistance(coord1, coord2);

        coordinateDistance[`${coord1}-${coord2}`] = distance;
      }
    }

    // Sort distances
    var sortedDistances = Object.entries(coordinateDistance)
      .sort((a, b) => {
        return a[1] - b[1];
      })
      .map((x) => x[0]);

    // Loop thru shortest distances and make connections
    let groupsOfConnections = [];

    // Merge connections
    let i = 0;
    while (i < connectingRequirement) {
      let matchingGroupIndexes = [];
      var [firstCoord, secondCoord] = sortedDistances[i].split("-");

      // Izbrojati s koliko trenutnih grupa se podudara
      for (let j = 0; j < groupsOfConnections.length; j++) {
        for (let k = 0; k < groupsOfConnections[j].length; k++) {
          if (
            firstCoord === groupsOfConnections[j][k] ||
            secondCoord === groupsOfConnections[j][k]
          ) {
            if (!matchingGroupIndexes.includes(j)) {
              matchingGroupIndexes.push(j);
              break;
            }
          }
        }
      }

      // Ako je 0 => kreirati novu grupu
      if (matchingGroupIndexes.length === 0) {
        groupsOfConnections.push([firstCoord, secondCoord]);
      }

      // Ako je 1 => dodati u tu grupu ako već ne postoji
      if (matchingGroupIndexes.length === 1) {
        if (!groupsOfConnections[matchingGroupIndexes[0]].includes(firstCoord))
          groupsOfConnections[matchingGroupIndexes[0]].push(firstCoord);

        if (!groupsOfConnections[matchingGroupIndexes[0]].includes(secondCoord))
          groupsOfConnections[matchingGroupIndexes[0]].push(secondCoord);
      }

      // Ako je 2 => mergati grupe i dodati u grupu ako već ne postoji
      if (matchingGroupIndexes.length > 1) {
        const mergedGroupConnections = [];
        var updatedGroupsOfConnections = [];

        for (let i = 0; i < groupsOfConnections.length; i++) {
          if (matchingGroupIndexes.includes(i)) {
            mergedGroupConnections.push(...groupsOfConnections[i]);
          } else {
            updatedGroupsOfConnections.push(groupsOfConnections[i]);
          }
        }
        var uniqueMerged = new Set(mergedGroupConnections);
        updatedGroupsOfConnections.push(Array.from(uniqueMerged));
        groupsOfConnections = updatedGroupsOfConnections;
      }

      i++;
    }

    var sortedGroupsOfConnections = groupsOfConnections.sort(
      (a, b) => b.length - a.length
    );

    var sizeOfThreeLargestCircuits =
      sortedGroupsOfConnections[0].length *
      sortedGroupsOfConnections[1].length *
      sortedGroupsOfConnections[2].length;

    console.log(
      `Multiplied sizes of the three largest circuits is ${sizeOfThreeLargestCircuits}`
    );
  });
}

function calcDistance(coord1, coord2) {
  var dx = coord1[0] - coord2[0];
  var dy = coord1[1] - coord2[1];
  var dz = coord1[2] - coord2[2];

  return Math.sqrt(dx * dx + dy * dy + dz * dz);
}

part1();

/**
 * Part 2
 */
function part2() {
  fs.readFile("input.txt", "utf-8", function (err, data) {
    if (err) throw err;

    var coordinates = data.split("\n").map((x) => x.split(","));
    var coordinateDistance = {};

    // Calc distances for each possible connection
    for (let i = 0; i < coordinates.length; i++) {
      // Move only downwards + skip itself
      for (let j = i + 1; j < coordinates.length; j++) {
        var coord1 = coordinates[i].map((x) => parseInt(x));
        var coord2 = coordinates[j].map((x) => parseInt(x));
        var distance = calcDistance(coord1, coord2);

        coordinateDistance[`${coord1}-${coord2}`] = distance;
      }
    }

    // Sort distances
    var sortedDistances = Object.entries(coordinateDistance)
      .sort((a, b) => {
        return a[1] - b[1];
      })
      .map((x) => x[0]);

    let connectingRequirement = sortedDistances.length;

    // Loop thru shortest distances and make connections
    let groupsOfConnections = [];

    // Merge connections
    let i = 0;

    let lastIndexWhenConnectionWasAdded = 0;
    while (i < connectingRequirement) {
      let matchingGroupIndexes = [];
      var [firstCoord, secondCoord] = sortedDistances[i].split("-");

      // Izbrojati s koliko trenutnih grupa se podudara
      for (let j = 0; j < groupsOfConnections.length; j++) {
        for (let k = 0; k < groupsOfConnections[j].length; k++) {
          if (
            firstCoord === groupsOfConnections[j][k] ||
            secondCoord === groupsOfConnections[j][k]
          ) {
            if (!matchingGroupIndexes.includes(j)) {
              matchingGroupIndexes.push(j);
              break;
            }
          }
        }
      }

      // Ako je 0 => kreirati novu grupu
      if (matchingGroupIndexes.length === 0) {
        lastIndexWhenConnectionWasAdded = i;
        groupsOfConnections.push([firstCoord, secondCoord]);
      }

      // Ako je 1 => dodati u tu grupu ako već ne postoji
      if (matchingGroupIndexes.length === 1) {
        if (
          !groupsOfConnections[matchingGroupIndexes[0]].includes(firstCoord)
        ) {
          lastIndexWhenConnectionWasAdded = i;
          groupsOfConnections[matchingGroupIndexes[0]].push(firstCoord);
        }

        if (
          !groupsOfConnections[matchingGroupIndexes[0]].includes(secondCoord)
        ) {
          lastIndexWhenConnectionWasAdded = i;
          groupsOfConnections[matchingGroupIndexes[0]].push(secondCoord);
        }
      }

      // Ako je 2 => mergati grupe i dodati u grupu ako već ne postoji
      if (matchingGroupIndexes.length > 1) {
        const mergedGroupConnections = [];
        var updatedGroupsOfConnections = [];

        for (let i = 0; i < groupsOfConnections.length; i++) {
          if (matchingGroupIndexes.includes(i)) {
            mergedGroupConnections.push(...groupsOfConnections[i]);
          } else {
            updatedGroupsOfConnections.push(groupsOfConnections[i]);
          }
        }

        var uniqueMerged = new Set(mergedGroupConnections);
        updatedGroupsOfConnections.push(Array.from(uniqueMerged));
        groupsOfConnections = updatedGroupsOfConnections;
      }

      i++;
    }

    var [firstCoord, secondCoord] =
      sortedDistances[lastIndexWhenConnectionWasAdded].split("-");
    var x1 = parseInt(firstCoord.split(",")[0]);
    var x2 = parseInt(secondCoord.split(",")[0]);

    console.log(
      `Multiplied X coordinates for first joint connection is ${x1 * x2}`
    );
  });
}

part2();
