const fs = require("fs");

/* Link to task description: https://adventofcode.com/2024/day/1 */

/**
 * Part 1
 */
function part1() {
  fs.readFile("input.txt", "utf-8", function (err, data) {
    if (err) throw err;

    const leftList = [];
    const rightList = [];

    let finalDistance = 0;

    data.split("\n").forEach((stringInput) => {
      var inputs = stringInput.split(" ").filter(x => x != "");
      leftList.push(parseInt(inputs[0].trim()));
      rightList.push(parseInt(inputs[1].trim()));
    });

    leftList.sort();
    rightList.sort();

    for(let i = 0; i < leftList.length; i++)
    {
        finalDistance += Math.abs(leftList[i] - rightList[i])
    }

    console.log(
      `Total distance is ${finalDistance}`
    );
  });
}

part1();

/**
 * Part 2
 */
function part2() {
  fs.readFile("input.txt", "utf-8", function (err, data) {
    if (err) throw err;

    const leftList = [];
    const rightList = [];

    let finalDistance = 0;

    data.split("\n").forEach((stringInput) => {
      var inputs = stringInput.split(" ").filter(x => x != "");
      leftList.push(parseInt(inputs[0].trim()));
      rightList.push(parseInt(inputs[1].trim()));
    });

    for(let i = 0; i < leftList.length; i++)
    {
      var leftNumber = leftList[i];
      var countInRightList = rightList.filter(x => x == leftNumber).length;
      finalDistance += leftNumber * countInRightList
    }

    console.log(
      `Similarity score is ${finalDistance}`
    );
  });
}

part2();
