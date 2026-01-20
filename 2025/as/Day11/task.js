const fs = require("fs");

/* Link to task description: https://adventofcode.com/2025/day/11 */

/**
 * Part 1
 */
function part1() {
  fs.readFile("input.txt", "utf-8", function (err, data) {
    if (err) throw err;

    var deviceDict = {};

    data.split("\n").forEach((row) => {
      const [start, path] = row.split(":");
      const paths = path.split(" ").filter((x) => x !== "");

      deviceDict[start] = paths;
    });

    let nextPossiblePaths = [];
    let currentPaths = [...deviceDict["you"]];
    let totalCount = 0;

    while (currentPaths.length > 0) {
      for (let i = 0; i < currentPaths.length; i++) {
        const nextPathsFromI = deviceDict[currentPaths[i]];

        // Break if path encounters OUT (servers with OUT don't have alternative paths)
        if (nextPathsFromI.includes("out")) {
          totalCount++;
          continue;
        }

        nextPossiblePaths.push(...nextPathsFromI);
      }

      currentPaths = [...nextPossiblePaths];
      nextPossiblePaths = [];
    }

    console.log(`Total number of paths is: ${totalCount}`);
  });
}
part1();

/**
 * Part 2
 */

const pathsCache = {};
const deviceDict = {};

function part2() {
  fs.readFile("input.txt", "utf-8", function (err, data) {
    if (err) throw err;

    data.split("\n").forEach((row) => {
      const [start, path] = row.split(":");
      const paths = path.split(" ").filter((x) => x !== "");

      deviceDict[start] = paths;
    });

    var hitTracker = getPaths("svr", {
      fftHit: 0,
      dacHit: 0,
      bothHit: 0,
      totalHits: 0,
    });

    console.log(`Total hit counter is ${hitTracker.bothHit}`);
  });
}

function getPaths(
  serverName,
  hitTracker = { fftHit: 0, dacHit: 0, bothHit: 0, totalHits: 0 }
) {
  // Early return if already calculated
  if (pathsCache[serverName] !== undefined) return pathsCache[serverName];

  // Reached end => return empty track
  if (serverName === "out")
    return {
      fftHit: hitTracker.fftHit,
      dacHit: hitTracker.dacHit,
      bothHit: hitTracker.bothHit,
      totalHits: hitTracker.totalHits + 1,
    };

  if (serverName === "fft") hitTracker.fftHit++;

  if (serverName === "dac") hitTracker.dacHit++;

  var childUpdates = [];

  for (let i = 0; i < deviceDict[serverName].length; i++) {
    var childHitTracker = getPaths(deviceDict[serverName][i], {
      fftHit: 0,
      dacHit: 0,
      bothHit: 0,
      totalHits: 0,
    });

    // Create copy for each child
    var currentStateClone = {
      fftHit: hitTracker.fftHit,
      dacHit: hitTracker.dacHit,
      bothHit: hitTracker.bothHit,
      totalHits: hitTracker.totalHits,
    };

    if (childHitTracker.bothHit > 0)
      currentStateClone.bothHit += childHitTracker.bothHit;

    // Handle regular FFT hits first
    if (hitTracker.fftHit > 0)
      currentStateClone.fftHit *= childHitTracker.totalHits;
    else if (childHitTracker.fftHit > 0)
      currentStateClone.fftHit = childHitTracker.fftHit;

    // Handle regular DAC hits first
    if (hitTracker.dacHit > 0)
      currentStateClone.dacHit *= childHitTracker.totalHits;
    else if (childHitTracker.dacHit > 0)
      currentStateClone.dacHit = childHitTracker.dacHit;

    // For current state has FFT, add all dac paths to both
    if (hitTracker.fftHit > 0)
      currentStateClone.bothHit += childHitTracker.dacHit;

    // For every DAC path, add FFT count to BOTH as they are now both
    if (hitTracker.dacHit > 0)
      currentStateClone.bothHit += childHitTracker.fftHit;

    // Combine total at the end
    currentStateClone.totalHits += childHitTracker.totalHits;

    childUpdates.push(currentStateClone);
  }

  var finalResults = {
    fftHit: 0,
    dacHit: 0,
    bothHit: 0,
    totalHits: 0,
  };

  childUpdates.forEach((childUpdate) => {
    finalResults.fftHit += childUpdate.fftHit;
    finalResults.dacHit += childUpdate.dacHit;
    finalResults.bothHit += childUpdate.bothHit;
    finalResults.totalHits += childUpdate.totalHits;
  });

  // Save to cache once server is completed
  if (pathsCache[serverName] === undefined)
    pathsCache[serverName] = finalResults;

  return finalResults;
}

part2();
