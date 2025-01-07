const fs = require("fs/promises");

async function main() {
  try {
    const data = await fs.readFile("input.txt", "utf8");
    const reports = parseReports(data);
    const safeReports = findSafeReports(reports);
    console.log("Safe reports:", safeReports.length);
  } catch (err) {
    console.error("Error reading the file:", err);
  }
}

function parseReports(data) {
  return data
    .trim()
    .split("\n")
    .map((line) => line.trim().split(/\s+/).map(Number));
}

// PART 1.
function isReportSafe(levels) {
  const diffs = levels.slice(1).map((curr, i) => curr - levels[i]);

  if (
    diffs.some((diff) => diff === 0 || Math.abs(diff) < 1 || Math.abs(diff) > 3)
  )
    return false;

  const isIncreasing = diffs[0] > 0;
  return diffs.every((diff) => (isIncreasing ? diff > 0 : diff < 0));
}

// PART 2.
function canBeMadeSafe(levels) {
  if (isReportSafe(levels)) {
    return true;
  }

  // Try removing each number to check if report becomes safe
  for (let i = 0; i < levels.length; i++) {
    const newLevels = levels.slice(0, i).concat(levels.slice(i + 1));
    if (newLevels.length < 2) {
      continue;
    }
    if (isReportSafe(newLevels)) {
      return true;
    }
  }
  return false;
}

function findSafeReports(reports) {
  return reports.reduce((safeReports, levels, index) => {
    if (canBeMadeSafe(levels)) safeReports.push(index + 1);
    return safeReports;
  }, []);
}

main();
