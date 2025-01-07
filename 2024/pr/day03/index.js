const fs = require("fs/promises");

async function main() {
  try {
    const data = await fs.readFile("input.txt", "utf8");
    const sum = sumMulResults(data);
    console.log("mul sum result:", sum);
  } catch (err) {
    console.error("Error reading the file:", err);
  }
}

function sumMulResults(input) {
  const mulRegex = /mul\((\d+),(\d+)\)/;
  let sum = 0;

  // PART 2.
  let mulEnabled = true;
  const operations = input.match(/(mul\(\d+,\d+\)|do\(\)|don't\(\))/g);

  if (operations) {
    for (const operation of operations) {
      if (operation === "do()") {
        mulEnabled = true;
      } else if (operation === "don't()") {
        mulEnabled = false;
      } else if (mulEnabled) {
        const match = mulRegex.exec(operation);
        if (match) {
          const x = parseInt(match[1], 10);
          const y = parseInt(match[2], 10);
          sum += x * y;
        }
      }
    }
  }

  // PART 1.
  // while ((match = regex.exec(input)) !== null) {
  //   const x = parseInt(match[1], 10);
  //   const y = parseInt(match[2], 10);
  //   sum += x * y;
  // }

  return sum;
}

main();
