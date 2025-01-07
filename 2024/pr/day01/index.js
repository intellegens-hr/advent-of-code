const fs = require("fs");

fs.readFile("input.txt", "utf8", (err, data) => {
  if (err) {
    console.error("Error reading the file:", err);
    return;
  }

  // PART 1.

  const lines = data.trim().split("\n");
  let leftList = [];
  let rightList = [];

  // Parse data into two arrays
  lines.forEach((line) => {
    const [leftNumber, rightNumber] = line.trim().split(/\s+/).map(Number);
    leftList.push(leftNumber);
    rightList.push(rightNumber);
  });

  // Sort each array in ascending order and calculate the distance
  leftList.sort((a, b) => a - b);
  rightList.sort((a, b) => a - b);

  const distances = leftList.map((leftNumber, index) => {
    const rightNumber = rightList[index];
    return Math.abs(leftNumber - rightNumber);
  });

  const totalDistance = distances.reduce((sum, distance) => sum + distance, 0);

  console.log("Total distance:", totalDistance);

  // PART 2.

  const rightCount = rightList.reduce((countMap, num) => {
    countMap[num] = (countMap[num] || 0) + 1;
    return countMap;
  }, {});

  const similarityScore = leftList.reduce((score, num) => {
    const count = rightCount[num] || 0;
    return score + num * count;
  }, 0);

  console.log("Total similarity score:", similarityScore);
});
