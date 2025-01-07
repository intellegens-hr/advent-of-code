const fs = require("fs");

/* Link to task description: https://adventofcode.com/2024/day/2 */

/**
 * Part 1
 */
function part1() {
  fs.readFile("input.txt", "utf-8", function (err, data) {
    if (err) throw err;

    const reports = [];
    let numberOfSafeReports = 0;

    data.split("\n").forEach((stringInput) => {
      var inputs = stringInput.split(" ");
      reports.push(inputs.map(x => parseInt(x)));
    });

    reports.forEach(report => {
      if(IsReportSafe(report))
        numberOfSafeReports++;
    });

    console.log(
      `Number of safe reports is ${numberOfSafeReports}`
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

    const reports = [];
    let numberOfSafeReports = 0;

    data.split("\n").forEach((stringInput) => {
      var inputs = stringInput.split(" ");
      reports.push(inputs.map(x => parseInt(x)));
    });

    reports.forEach(report => {
      if(IsReportSafe(report))
        numberOfSafeReports++;
      else{
        for(let i = 0; i < report.length; i++){
          var updatedReport = RemoveItem(report, i);

          if(IsReportSafe(updatedReport))
          {
            numberOfSafeReports++;
            break;
          }
        }
      }
    });

    console.log(
      `Number of safe reports with problem dampener is ${numberOfSafeReports}`
    );
  });
}

function IsReportSafe(report){
  if(report[0] == report[report.length - 1]){
    return (false);
  }

  const increasing = report[0] < report[report.length - 1];

  for(let i = 0; i < report.length - 1; i++){
    if(increasing && report[i] >= report[i + 1]){
      return false;
    }else if(!increasing && report[i] <= report[i + 1]){
      return false;
    }

    const diff = Math.abs(report[i] - report[i + 1]);
    if(diff < 1 || diff > 3){
      return false;
    }
  }

  return true;
}

function RemoveItem(array, n) {
  return array.filter((_, i) => i !== n);
}

part2();
