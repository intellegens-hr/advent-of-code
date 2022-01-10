const fs = require("fs");

/* Link to task description: https://adventofcode.com/2021/day/5 */

/**
 * Part 1
 */
function part1() {
  function fillHashMap(position1, position2, position3, hashMap, axis) {
    if (position1 > position2) {
      for (let i = position2; i <= position1; i++) {
        let hashMapKey;
        if (axis === "x") {
          hashMapKey = `(${i},${position3})`;
        } else {
          hashMapKey = `(${position3},${i})`;
        }
        if (hashMap.hasOwnProperty(hashMapKey)) {
          hashMap[hashMapKey]++;
        } else {
          hashMap[hashMapKey] = 1;
        }
      }
    } else if (position2 > position1) {
      for (let i = position1; i <= position2; i++) {
        let hashMapKey;

        if (axis === "x") {
          hashMapKey = `(${i},${position3})`;
        } else {
          hashMapKey = `(${position3},${i})`;
        }

        if (hashMap.hasOwnProperty(hashMapKey)) {
          hashMap[hashMapKey]++;
        } else {
          hashMap[hashMapKey] = 1;
        }
      }
    }
  }

  fs.readFile("input.txt", "utf-8", function (err, data) {
    if (err) throw err;
    const arrayOfInputs = data.split("\n").map((i) =>
      i
        .trim()
        .replace("->", ",")
        .split(",")
        .map((s) => parseInt(s))
    );

    let hashMap = {};

    for (const input of arrayOfInputs) {
      if (input[0] === input[2] || input[1] === input[3]) {
        if (input[0] === input[2]) {
          fillHashMap(input[1], input[3], input[0], hashMap, "y");
        }

        if (input[1] === input[3]) {
          fillHashMap(input[0], input[2], input[1], hashMap, "x");
        }
      }
    }

    const count = Object.values(hashMap).filter((v) => v > 1).length;
    console.log(
      `Total count of points that have at least 2 intercepting horizontal or vertical lines is ${count}`
    );
  });
}

part1();

/**
 * Part 2
 */
function part2() {
  function setHorizontalOrVertical(
    position1,
    position2,
    position3,
    hashMap,
    axis
  ) {
    if (position1 > position2) {
      for (let i = position2; i <= position1; i++) {
        let hashMapKey;
        if (axis === "x") {
          hashMapKey = `(${i},${position3})`;
        } else {
          hashMapKey = `(${position3},${i})`;
        }
        if (hashMap.hasOwnProperty(hashMapKey)) {
          hashMap[hashMapKey]++;
        } else {
          hashMap[hashMapKey] = 1;
        }
      }
    } else if (position2 > position1) {
      for (let i = position1; i <= position2; i++) {
        let hashMapKey;

        if (axis === "x") {
          hashMapKey = `(${i},${position3})`;
        } else {
          hashMapKey = `(${position3},${i})`;
        }

        if (hashMap.hasOwnProperty(hashMapKey)) {
          hashMap[hashMapKey]++;
        } else {
          hashMap[hashMapKey] = 1;
        }
      }
    }
  }
  function setHashMapKey(hashMap, i, j, type) {
    let hashMapKey = `(${i},${j})`;
    if (hashMap.hasOwnProperty(hashMapKey)) {
      hashMap[hashMapKey]++;
    } else {
      hashMap[hashMapKey] = 1;
    }
  }

  function setDiagonal(x1, y1, x2, y2, hashMap) {
    if (x1 > x2 && y1 > y2) {
      for (let i = x1, j = y1; i >= x2; i--, j--) {
        setHashMapKey(hashMap, i, j);
      }
    } else if (x2 > x1 && y2 > y1) {
      for (let i = x1, j = y1; i <= x2; i++, j++) {
        setHashMapKey(hashMap, i, j);
      }
    } else if (x1 > x2 && y2 > y1) {
      for (let i = x2, j = y2; i <= x1; i++, j--) {
        setHashMapKey(hashMap, i, j);
      }
    } else if (x2 > x1 && y1 > y2) {
      for (let i = x1, j = y1; i <= x2; i++, j--) {
        setHashMapKey(hashMap, i, j);
      }
    }
  }

  fs.readFile("input.txt", "utf-8", function (err, data) {
    if (err) throw err;
    const arrayOfInputs = data.split("\n").map((i) =>
      i
        .trim()
        .replace("->", ",")
        .split(",")
        .map((s) => parseInt(s))
    );

    let hashMap = {};

    for (const input of arrayOfInputs) {
      if (input[0] === input[2]) {
        setHorizontalOrVertical(input[1], input[3], input[0], hashMap, "y");
      }

      if (input[1] === input[3]) {
        setHorizontalOrVertical(input[0], input[2], input[1], hashMap, "x");
      }

      if (Math.abs(input[0] - input[2]) === Math.abs(input[1] - input[3])) {
        setDiagonal(input[0], input[1], input[2], input[3], hashMap);
      }
    }
    const count = Object.values(hashMap).filter((v) => v > 1).length;
    console.log(
      `Total count of points that have at least 2 intercepting horizontal, vertical or diagonal lines is ${count}`
    );
  });
}

part2();
