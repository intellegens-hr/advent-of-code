const fs = require("fs");

/* Link to task description: https://adventofcode.com/2021/day/4 */

/**
 * Part 1
 */
function part1() {
  function checkWinner(board) {
    let winningTrigger = false;
    let rowCount = 0;

    for (const row of board.counter) {
      const rowTest = row.reduce((prevVal, currVal) => {
        return prevVal + currVal;
      }, 0);

      if (rowTest > 0) {
        rowCount++;
      }

      if (rowTest === 5) {
        winningTrigger = true;
        break;
      }
    }

    if (!winningTrigger) {
      for (let i = 0; i < 5; i++) {
        if (
          board.counter[0][i] +
            board.counter[1][i] +
            board.counter[2][i] +
            board.counter[3][i] +
            board.counter[4][i] ===
          5
        ) {
          winningTrigger = true;
          break;
        }
      }
    }

    return winningTrigger;
  }

  function calcUnmarkedNumbers(board) {
    let sum = 0;

    board.counter.forEach((row, rowIndex) => {
      row.forEach((num, numIndex) => {
        if (num === 0) {
          sum += board.numbers[rowIndex][numIndex];
        }
      });
    });
    return sum;
  }

  fs.readFile("input.txt", "utf-8", function (err, data) {
    if (err) throw err;
    const arrayOfInputs = data.split("\n");
    const bingoNumbers = [];
    const boards = [];
    let tempBoard = [];
    let index = 0;
    let winningBoard;
    let winningNumber = 0;

    for (const input of arrayOfInputs) {
      if (index === 0) {
        bingoNumbers.push(...input.split(",").map((s) => parseInt(s)));
        index++;
        continue;
      }

      if (input === "" && !tempBoard.length) {
        index++;
        continue;
      }

      if (input === "") {
        boards.push({
          counter: [
            Array(5).fill(0),
            Array(5).fill(0),
            Array(5).fill(0),
            Array(5).fill(0),
            Array(5).fill(0),
          ],
          numbers: tempBoard,
        });
        tempBoard = [];
      } else {
        tempBoard.push(
          input
            .split(" ")
            .map((s) => parseInt(s))
            .filter((n) => n || n === 0)
        );
      }
    }

    for (const bingoNumber of bingoNumbers) {
      let boardIndex = 0;
      for (const board of boards) {
        let rowIndex = 0;
        let bingoNumFound = false;
        for (const row of board.numbers) {
          let rowNumIndex = 0;

          for (const rowNum of row) {
            if (rowNum === bingoNumber) {
              bingoNumFound = true;
              boards[boardIndex].counter[rowIndex][rowNumIndex]++;
              break;
            }

            if (bingoNumFound) {
              break;
            }

            rowNumIndex++;
          }

          if (bingoNumFound) {
            break;
          }

          rowIndex++;
        }

        if (bingoNumFound) {
          if (checkWinner(board)) {
            winningBoard = board;
            winningNumber = bingoNumber;
            break;
          }
        }

        boardIndex++;
      }

      if (winningBoard) {
        break;
      }
    }

    console.log(
      `Unmarked numbers sum times winning number is ${
        winningNumber * calcUnmarkedNumbers(winningBoard)
      }`
    );
  });
}

part1();

/**
 * Part 2
 */
function part2() {
  function checkWinner(board) {
    let winningTrigger = false;

    for (const row of board.counter) {
      const rowTest = row.reduce((prevVal, currVal) => {
        return prevVal + currVal;
      }, 0);

      if (rowTest === 5) {
        winningTrigger = true;
        break;
      }
    }

    if (!winningTrigger) {
      for (let i = 0; i < 5; i++) {
        if (
          board.counter[0][i] +
            board.counter[1][i] +
            board.counter[2][i] +
            board.counter[3][i] +
            board.counter[4][i] ===
          5
        ) {
          winningTrigger = true;
          break;
        }
      }
    }

    return winningTrigger;
  }

  function calcUnmarkedNumbers(board) {
    let sum = 0;

    board.counter.forEach((row, rowIndex) => {
      row.forEach((num, numIndex) => {
        if (num === 0) {
          sum += board.numbers[rowIndex][numIndex];
        }
      });
    });
    return sum;
  }

  fs.readFile("input.txt", "utf-8", function (err, data) {
    if (err) throw err;
    const arrayOfInputs = data.split("\n");
    const bingoNumbers = [];
    const boards = [];
    let tempBoard = [];
    let index = 0;
    let winningNumber = 0;
    let winningIndexArray = [];

    for (const input of arrayOfInputs) {
      if (index === 0) {
        bingoNumbers.push(...input.split(",").map((s) => parseInt(s)));
        index++;
        continue;
      }

      if (input === "" && !tempBoard.length) {
        index++;
        continue;
      }

      if (input === "") {
        boards.push({
          counter: [
            Array(5).fill(0),
            Array(5).fill(0),
            Array(5).fill(0),
            Array(5).fill(0),
            Array(5).fill(0),
          ],
          numbers: tempBoard,
        });
        tempBoard = [];
      } else {
        tempBoard.push(
          input
            .split(" ")
            .map((s) => parseInt(s))
            .filter((n) => n || n === 0)
        );
      }
    }

    for (const bingoNumber of bingoNumbers) {
      let boardIndex = 0;

      for (const board of boards) {
        if (winningIndexArray.includes(boardIndex)) {
          boardIndex++;
          continue;
        }

        let rowIndex = 0;
        let bingoNumFound = false;
        for (const row of board.numbers) {
          let rowNumIndex = 0;

          for (const rowNum of row) {
            if (rowNum === bingoNumber) {
              bingoNumFound = true;
              boards[boardIndex].counter[rowIndex][rowNumIndex]++;
              break;
            }

            if (bingoNumFound) {
              break;
            }

            rowNumIndex++;
          }

          if (bingoNumFound) {
            break;
          }

          rowIndex++;
        }

        if (bingoNumFound) {
          if (checkWinner(board)) {
            if (!winningIndexArray.includes(boardIndex)) {
              winningIndexArray.push(boardIndex);
              winningNumber = bingoNumber;
            }
          }
        }

        boardIndex++;
      }
    }

    console.log(
      `Unmarked numbers sum times winning number is ${
        winningNumber *
        calcUnmarkedNumbers(
          boards[winningIndexArray[winningIndexArray.length - 1]]
        )
      }`
    );
  });
}

part2();
