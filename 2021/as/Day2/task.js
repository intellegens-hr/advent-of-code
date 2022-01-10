const fs = require("fs");

/* Link to task description: https://adventofcode.com/2021/day/2 */

/**
 * Part 1
 */
function part1() {
  fs.readFile("input.txt", "utf-8", function (err, data) {
    if (err) throw err;
    const arrayOfInputs = data.split("\n");
    let trackingObject = {
      horizontalPosition: 0,
      depth: 0,
    };

    arrayOfInputs.forEach((input) => {
      const commandArray = input.split(" ");
      switch (commandArray[0]) {
        case "forward":
          trackingObject.horizontalPosition += parseInt(commandArray[1]);
          break;

        case "down":
          trackingObject.depth += parseInt(commandArray[1]);
          break;

        case "up":
          trackingObject.depth -= parseInt(commandArray[1]);
          break;

        default:
          break;
      }
    });
    console.log(
      "Final horizontal position is: ",
      trackingObject.horizontalPosition
    );
    console.log("Final depth is: ", trackingObject.depth);
    console.log(
      "Values multiplied equal to: ",
      trackingObject.horizontalPosition * trackingObject.depth
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
    const arrayOfInputs = data.split("\n");
    let trackingObject = {
      aim: 0,
      horizontalPosition: 0,
      depth: 0,
    };

    arrayOfInputs.forEach((input) => {
      const commandArray = input.split(" ");
      switch (commandArray[0]) {
        case "forward":
          trackingObject.horizontalPosition += parseInt(commandArray[1]);
          trackingObject.depth +=
            trackingObject.aim * parseInt(commandArray[1]);
          break;

        case "down":
          trackingObject.aim += parseInt(commandArray[1]);
          break;

        case "up":
          trackingObject.aim -= parseInt(commandArray[1]);
          break;

        default:
          break;
      }
    });
    console.log(
      "Final horizontal position is: ",
      trackingObject.horizontalPosition
    );
    console.log("Final depth is: ", trackingObject.depth);
    console.log("Final aim is: ", trackingObject.aim);
    console.log(
      "Values multiplied equal to: ",
      trackingObject.horizontalPosition * trackingObject.depth
    );
  });
}

part2();
