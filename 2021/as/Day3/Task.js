const fs = require("fs");

/* Link to task description: https://adventofcode.com/2021/day/3 */

/**
 * Part 1
 */
function part1() {
  fs.readFile("input.txt", "utf-8", function (err, data) {
    if (err) throw err;
    const arrayOfInputs = data.split("\n");
    const countObj = {};

    arrayOfInputs.forEach((input) => {
      input.split("").forEach((letter, index) => {
        if (!countObj.hasOwnProperty(index)) {
          countObj[index] = {
            0: 0,
            1: 0,
          };
        }

        countObj[index][letter]++;
      });
    });

    let gamaNums = "";
    let epsilonNums = "";

    for (const item of Object.values(countObj)) {
      if (item["0"] > item["1"]) {
        gamaNums += 0;
        epsilonNums += 1;
      } else {
        gamaNums += 1;
        epsilonNums += 0;
      }
    }

    console.log(
      `Power consumption is: ${
        parseInt(gamaNums, 2) * parseInt(epsilonNums, 2)
      }`
    );
  });
}

part1();

/**
 * Part 2
 */
function part2() {
  function calcPositions(allInputs, countObject, index) {
    allInputs.forEach((input) => {
      const inputSplit = input.split("");
      countObject[inputSplit[index]]++;
    });
    return countObject;
  }

  fs.readFile("input.txt", "utf-8", function (err, data) {
    if (err) throw err;
    const arrayOfInputs = data.split("\n");
    let remainingOG = arrayOfInputs.map((i) => i);
    let remainingCO2 = arrayOfInputs.map((i) => i);

    for (let i = 0; i < arrayOfInputs[0].length; i++) {
      if (remainingOG.length === 1 && remainingCO2.length === 1) {
        break;
      }

      if (remainingOG.length > 1) {
        const countObjectOG = calcPositions(remainingOG, { 0: 0, 1: 0 }, i);
        if (countObjectOG["0"] > countObjectOG["1"]) {
          remainingOG = remainingOG.filter((num) => num[i] === "0");
        } else if (countObjectOG["0"] <= countObjectOG[1]) {
          remainingOG = remainingOG.filter((num) => num[i] === "1");
        }
      }

      if (remainingCO2.length > 1) {
        const countObjectCO2 = calcPositions(remainingCO2, { 0: 0, 1: 0 }, i);

        if (countObjectCO2["0"] > countObjectCO2["1"]) {
          remainingCO2 = remainingCO2.filter((num) => num[i] === "1");
        } else if (countObjectCO2["0"] <= countObjectCO2["1"]) {
          remainingCO2 = remainingCO2.filter((num) => num[i] === "0");
        }
      }
    }

    console.log(
      `Life supporting rating is: ${
        parseInt(remainingOG[0], 2) * parseInt(remainingCO2[0], 2)
      }`
    );
  });
}

part2();
