const fs = require("fs");

/* Link to task description: https://adventofcode.com/2024/day/3 */

/**
 * Part 1
 */
function part1() {
  fs.readFile("input.txt", "utf-8", function (err, data) {
    if (err) throw err;

    var multiples = [];
    var splitData = data.split("mul"); 

    for(let i = 0; i < splitData.length; i++){
      var sequenceComplete = true;
      var num1 = "";
      var num2 = "";
      let stage = 1;
      // stage 1 (
      // stage 2 first num
      // stage 3 second num

        for(let j = 0; j < splitData[i].length; j++){
          if(stage == 1){
            if(splitData[i][j] == "(" && j == 0){
                stage++;
                continue;
            }else{
              sequenceComplete = false;
              break;
            }
          } else if(stage == 2){
            var parsedNum = parseInt(splitData[i][j]);
            if(!isNaN(parsedNum)){
              num1 += parsedNum;
              continue;
            } 
            else if(num1.length > 0 && splitData[i][j] == ","){
              stage++;
              continue;
            } else{
              sequenceComplete = false;
              break;
            }
          } else if(stage == 3){
            var parsedNum = parseInt(splitData[i][j]);

            if(!isNaN(parsedNum)){
              num2 += parsedNum;
              continue;
            } else if(num2.length > 0 && splitData[i][j] == ")"){
              break;
            } else{
                sequenceComplete = false;
              break;
            }
          } else{
            sequenceComplete = false;
            break;
          }
        }

      if(sequenceComplete){
        multiples.push([parseInt(num1), parseInt(num2)]);
      }
    }

    var sum = multiples.reduce((prevValue, currValue) => {
        return prevValue + (currValue[0] * currValue[1]);
    }, 0);

    console.log(
      `Total sum of valid inputs is ${sum}`
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

    var multiples = [];
    var splitData = data.split("mul"); 

    var isNextDisabled = false;
    
    for(let i = 0; i < splitData.length; i++){
      var sequenceComplete = true;
      var num1 = "";
      var num2 = "";
      let stage = 1;
      // stage 1 (
      // stage 2 first num
      // stage 3 second num

        for(let j = 0; j < splitData[i].length; j++){
          if(stage == 1){
            if(splitData[i][j] == "(" && j == 0){
                stage++;
                continue;
            }else{
              sequenceComplete = false;
              break;
            }
          } else if(stage == 2){
            var parsedNum = parseInt(splitData[i][j]);
            if(!isNaN(parsedNum)){
              num1 += parsedNum;
              continue;
            } 
            else if(num1.length > 0 && splitData[i][j] == ","){
              stage++;
              continue;
            } else{
              sequenceComplete = false;
              break;
            }
          } else if(stage == 3){
            var parsedNum = parseInt(splitData[i][j]);

            if(!isNaN(parsedNum)){
              num2 += parsedNum;
              continue;
            } else if(num2.length > 0 && splitData[i][j] == ")"){
              break;
            } else{
                sequenceComplete = false;
              break;
            }
          } else{
            sequenceComplete = false;
            break;
          }
        }

      if(sequenceComplete && !isNextDisabled){
        multiples.push([parseInt(num1), parseInt(num2)]);
      }

      if(splitData[i].includes("don't")){
        isNextDisabled = true;
      } else if(splitData[i].includes("do"))
        isNextDisabled = false;
    }

    var sum = multiples.reduce((prevValue, currValue) => {
        return prevValue + (currValue[0] * currValue[1]);
    }, 0);

    console.log(
      `Total sum of valid inputs is ${sum}`
    );
  });
}

part2();
