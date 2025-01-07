const fs = require("fs/promises");

async function main() {
  try {
    const data = await fs.readFile("input.txt", "utf8");
    const grid = data.split("\n").map((row) => row.trim().split(""));

    // Part 1.
    const occurrencesInGrid = countXMASInGrid(grid);
    console.log(`XMAS appears ${occurrencesInGrid} times in grid`);

    // Part 2.
    const occurrencesInX = countMASInX(grid);
    console.log(`MAS appears ${occurrencesInX} times in X`);
  } catch (err) {
    console.error("Error reading the file:", err);
  }
}

function countXMASInGrid(grid) {
  const directions = [
    [0, 1],
    [1, 0],
    [1, 1],
    [1, -1],
    [0, -1],
    [-1, 0],
    [-1, -1],
    [-1, 1],
  ];
  const rows = grid.length;
  const cols = grid[0].length;

  let count = 0;

  grid.forEach((row, x) =>
    row.forEach((_, y) => {
      directions.forEach(([dx, dy]) => {
        let found = true;
        let word = "XMAS";
        for (let i = 0; i < word.length; i++) {
          const nx = x + i * dx,
            ny = y + i * dy;
          if (
            nx < 0 ||
            ny < 0 ||
            nx >= rows ||
            ny >= cols ||
            grid[nx][ny] !== word[i]
          ) {
            found = false;
            break;
          }
        }
        if (found) count++;
      });
    })
  );

  return count;
}

function countMASInX(grid) {
  const rows = grid.length;
  const cols = grid[0].length;

  let count = 0;

  for (let x = 1; x < rows - 1; x++) {
    for (let y = 1; y < cols - 1; y++) {
      if (grid[x][y] === "A") {
        // Top-left to bottom-right
        const diag1First = grid[x - 1][y - 1];
        const diag1Second = grid[x + 1][y + 1];

        // Top-right to bottom-left
        const diag2First = grid[x - 1][y + 1];
        const diag2Second = grid[x + 1][y - 1];

        // Check if two characters are 'M' and 'S' in any order
        const isMAS = (char1, char2) => {
          return (
            (char1 === "M" && char2 === "S") || (char1 === "S" && char2 === "M")
          );
        };

        // Verify both diagonals
        if (isMAS(diag1First, diag1Second) && isMAS(diag2First, diag2Second))
          count++;
      }
    }
  }

  return count;
}

main();
