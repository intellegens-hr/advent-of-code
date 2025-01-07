const fs = require("fs/promises");

async function main() {
  try {
    const data = await fs.readFile("input.txt", "utf8");
    const [rulesSection, updatesSection] = data.split("\n\n");

    const rules = rulesSection
      .split("\n")
      .map((line) => line.split("|").map(Number));
    const updates = updatesSection
      .split("\n")
      .map((line) => line.split(",").map(Number));

    const sums = sumMiddlePageNumbers(rules, updates);
    console.log("Sum of correctly-ordered updates:", sums[0]);
    console.log("Sum of incorrectly-ordered updates:", sums[1]);
  } catch (err) {
    console.error("Error reading the file:", err);
  }
}

function sumMiddlePageNumbers(rules, updates) {
  let correctlyOrderedTotal = 0;
  let incorrectlyOrderedTotal = 0;

  for (const update of updates) {
    // Part 1.
    if (isUpdateOrdered(update, rules)) {
      correctlyOrderedTotal += findMiddlePageNumber(update);
    } else {
      incorrectlyOrderedTotal += findMiddlePageNumber(
        // Part 2.
        reorderUpdate(update, rules)
      );
    }
  }

  return [correctlyOrderedTotal, incorrectlyOrderedTotal];
}

function isUpdateOrdered(update, rules) {
  const indexMap = new Map(update.map((page, index) => [page, index]));
  for (const [x, y] of rules) {
    if (indexMap.has(x) && indexMap.has(y)) {
      if (indexMap.get(x) > indexMap.get(y)) return false;
    }
  }

  return true;
}

function reorderUpdate(update, rules) {
  const ruleMap = new Map();

  for (const [x, y] of rules) {
    if (!ruleMap.has(x)) ruleMap.set(x, new Set());

    ruleMap.get(x).add(y);
  }

  return update.slice().sort((a, b) => {
    if (ruleMap.has(a) && ruleMap.get(a).has(b)) return -1;
    if (ruleMap.has(b) && ruleMap.get(b).has(a)) return 1;

    return 0;
  });
}

function findMiddlePageNumber(update) {
  const middlePageIndex = Math.floor(update.length / 2);
  return update[middlePageIndex];
}

main();
