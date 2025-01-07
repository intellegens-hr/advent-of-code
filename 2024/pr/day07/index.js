const fs = require("fs/promises");

async function main() {
  try {
    const data = await fs.readFile("input.txt", "utf8");
    const lines = data.replace(/\r/g, "").trim().split("\n");
    const result = sumOfValidTargets(lines);
    console.log("Total calibration result:", result);
  } catch (err) {
    console.error("Error reading the file:", err);
  }
}

// Part 1.
function canMatchTarget(target, numbers) {
  if (numbers.length === 1) {
    return numbers[0] === target;
  }

  const slots = numbers.length - 1;
  // Iterate over all combinations of operators
  for (let mask = 0; mask < 1 << slots; mask++) {
    let result = numbers[0];

    for (let i = 0; i < slots; i++) {
      const op = mask & (1 << i) ? "*" : "+";
      const nextNum = numbers[i + 1];
      if (op === "+") {
        result = result + nextNum;
      } else {
        result = result * nextNum;
      }
    }

    if (result === target) {
      return true;
    }
  }

  return false;
}

// Part 2.
function canMatchTargetWithBacktrack(target, numbers) {
  // Check single number if it's equal to the target
  if (numbers.length === 1) {
    return numbers[0] === target;
  }

  return backtrack(numbers, target);
}

function backtrack(numbers, target) {
  // Check the target if reduced to one number
  if (numbers.length === 1) {
    return numbers[0] === target;
  }

  // Apply +, *, and || between the first two numbers
  const a = numbers[0];
  const b = numbers[1];
  const rest = numbers.slice(2);

  // Addition
  if (backtrack([a + b, ...rest], target)) return true;

  // Multiplication
  if (backtrack([a * b, ...rest], target)) return true;

  // Concatenation: convert a and b to strings, concatenate, then parse back as number
  const concatVal = parseInt(a.toString() + b.toString(), 10);
  if (backtrack([concatVal, ...rest], target)) return true;

  return false;
}

function sumOfValidTargets(lines) {
  let total = 0;
  for (const line of lines) {
    if (!line.trim()) continue;

    const [left, right] = line.split(":").map((part) => part.trim());
    const target = parseInt(left, 10);
    const numbers = right.split(" ").map((n) => parseInt(n, 10));

    if (canMatchTargetWithBacktrack(target, numbers)) {
      total += target;
    }
  }
  return total;
}

main();
