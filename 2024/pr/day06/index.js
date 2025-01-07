const fs = require("fs/promises");

async function main() {
  try {
    const data = await fs.readFile("input.txt", "utf8");
    const map = data.replace(/\r/g, "").trim().split("\n");
    console.log("Guard visited positions:", simulatePatrol(map));
    console.log("Obstacle position possibilities:", simulateLoopObstacles(map));
  } catch (err) {
    console.error("Error reading the file:", err);
  }
}

const directions = [
  { dx: -1, dy: 0, symbol: "^" },
  { dx: 0, dy: 1, symbol: ">" },
  { dx: 1, dy: 0, symbol: "v" },
  { dx: 0, dy: -1, symbol: "<" },
];

function findInitialGuardState(grid) {
  for (let r = 0; r < grid.length; r++) {
    for (let c = 0; c < grid[r].length; c++) {
      const cell = grid[r][c];
      if (["^", ">", "v", "<"].includes(cell)) {
        const dirIndex = directions.findIndex((d) => d.symbol === cell);
        return { row: r, col: c, dirIndex };
      }
    }
  }
  throw new Error("Guard's starting position not found.");
}

function isInBounds(grid, r, c) {
  return r >= 0 && r < grid.length && c >= 0 && c < grid[0].length;
}

function isObstacle(grid, r, c) {
  return isInBounds(grid, r, c) && grid[r][c] === "#";
}

// Part 1.
function simulatePatrol(grid) {
  let { row, col, dirIndex } = findInitialGuardState(grid);
  const visited = new Set([`${row},${col}`]);

  while (true) {
    const { dx, dy } = directions[dirIndex];
    const nextR = row + dx;
    const nextC = col + dy;

    if (!isInBounds(grid, nextR, nextC)) {
      // Stop since next step is out of area
      break;
    }

    if (isObstacle(grid, nextR, nextC)) {
      // Obstacle ahead, turn right
      dirIndex = (dirIndex + 1) % directions.length;
    } else {
      // Move forward
      row = nextR;
      col = nextC;
      visited.add(`${row},${col}`);
    }
  }

  return visited.size;
}

// Part 2.
function simulateLoopObstacles(grid) {
  const {
    row: startRow,
    col: startCol,
    dirIndex: startDir,
  } = findInitialGuardState(grid);

  // Convert grid to easily modifiable structure
  const baseGrid = grid.map((line) => line.split(""));
  const rows = baseGrid.length;
  const cols = baseGrid[0].length;

  let possibleObstacles = 0;

  // Simulate and check for loops given a grid with one extra obstacle
  function createsLoop(testGrid) {
    let r = startRow;
    let c = startCol;
    let d = startDir;

    const visitedStates = new Set();
    visitedStates.add(`${r},${c},${d}`);

    while (true) {
      const { dx, dy } = directions[d];
      const nr = r + dx;
      const nc = c + dy;

      if (!isInBounds(testGrid, nr, nc)) {
        // Guard leaves the area, no loop
        return false;
      }

      if (testGrid[nr][nc] === "#") {
        // Obstacle ahead, turn right
        d = (d + 1) % directions.length;
      } else {
        // Move forward
        r = nr;
        c = nc;
      }

      const stateKey = `${r},${c},${d}`;
      if (visitedStates.has(stateKey)) {
        // Returned to a previous state => loop detected
        return true;
      } else {
        visitedStates.add(stateKey);
      }
    }
  }

  for (let r = 0; r < rows; r++) {
    for (let c = 0; c < cols; c++) {
      // Skip guard starting position
      if (r === startRow && c === startCol) continue;
      // Skip if already obstacle
      if (baseGrid[r][c] === "#") continue;

      // Make a copy of the grid and place an obstacle
      const testGrid = baseGrid.map((rowArr) => [...rowArr]);
      testGrid[r][c] = "#";

      if (createsLoop(testGrid)) {
        possibleObstacles++;
      }
    }
  }

  return possibleObstacles;
}

main();
