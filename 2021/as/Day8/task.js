const fs = require("fs");

/* Link to task description: https://adventofcode.com/2021/day/8 */

/**
 * Part 1
 */
function part1() {
  fs.readFile("input.txt", "utf-8", function (err, data) {
    if (err) throw err;
    const arrayOfInputs = data.split("\n").map((l) => l.split(" | "));
    let counter = 0;

    const segments = [
      ["a", "b", "c", "e", "f", "g"] /* length(6) => 0 */,
      /* =>  */ ["c", "f"] /* length(2)  => 1  */,
      ["a", "c", "d", "e", "g"] /* length(5) => 2 */,
      ["a", "c", "d", "f", "g"] /* length(5) => 3*/,
      /* =>  */ ["b", "c", "d", "f"] /* length(4) => 4 */,
      ["a", "b", "d", "f", "g"] /* length(5) => 5 */,
      ["a", "b", "d", "e", "f", "g"] /* length(6) => 6 */,
      /* =>  */ ["a", "c", "f"] /* length(3) => 7 */,
      /* =>  */ ["a", "b", "c", "d", "e", "f", "g"] /* length(7) => 8 */,
      ["a", "b", "c", "d", "f", "g"] /* length (6) => 9*/,
    ];

    for (const input of arrayOfInputs) {
      const fourDigits = input[1].split(" ");

      for (const digit of fourDigits) {
        if (
          digit.length === segments[1].length ||
          digit.length === segments[4].length ||
          digit.length === segments[7].length ||
          digit.length === segments[8].length
        ) {
          counter++;
        }
      }
    }

    console.log(`Total count of digits 1, 4, 7, 8 is ${counter}`);
  });
}

part1();

/**
 * Part 2
 */
function part2() {
  fs.readFile("input.txt", "utf-8", function (err, data) {
    if (err) throw err;
    const arrayOfInputs = data.split("\n").map((l) => l.split(" | "));

    const segments = [
      ["a", "b", "c", "e", "f", "g"] /* length(6) => 0 */,
      /* =>  */ ["c", "f"] /* length(2)  => 1  */,
      ["a", "c", "d", "e", "g"] /* length(5) => 2 */,
      ["a", "c", "d", "f", "g"] /* length(5) => 3*/,
      /* =>  */ ["b", "c", "d", "f"] /* length(4) => 4 */,
      ["a", "b", "d", "f", "g"] /* length(5) => 5 */,
      ["a", "b", "d", "e", "f", "g"] /* length(6) => 6 */,
      /* =>  */ ["a", "c", "f"] /* length(3) => 7 */,
      /* =>  */ ["a", "b", "c", "d", "e", "f", "g"] /* length(7) => 8 */,
      ["a", "b", "c", "d", "f", "g"] /* length (6) => 9*/,
    ];

    function loopThroughValue(patterns, fn) {
      const breaker = false;
      for (let i = 0; i < patterns.length; i++) {
        fn(i, breaker);

        if (breaker) {
          break;
        }
      }
    }

    function getDiff(bigString, smallString) {
      for (let i = 0; i < bigString.length; i++) {
        if (smallString.indexOf(bigString[i]) === -1) {
          return bigString[i];
        }
      }
    }

    function checkIfSame(pattern1, pattern2) {
      for (let i = 0; i < pattern1.length; i++) {
        if (pattern2.indexOf(pattern1[i]) === -1) {
          return false;
        }
      }

      return true;
    }

    function getDigit(digit, digitPatterns) {
      for (let i = 0; i < digitPatterns.length; i++) {
        if (digit.length !== digitPatterns[i].length) {
          continue;
        } else {
          if (checkIfSame(digit, digitPatterns[i])) {
            return i;
          }
        }
      }
    }
    let sum = 0;

    for (const input of arrayOfInputs) {
      const patterns = input[0].split(" ");
      const fourDigits = input[1].split(" ");

      const segmentMapper = {
        a: "",
        b: "",
        c: "",
        d: "",
        e: "",
        f: "",
        g: "",
      };

      const digitPatterns = Array(10).fill(undefined);

      // Nađi brojeve 1 i 7 => slovo a
      loopThroughValue(patterns, (i, breaker) => {
        if (patterns[i].length === 2) {
          digitPatterns[1] = patterns[i];
        }

        if (patterns[i].length === 3) {
          digitPatterns[7] = patterns[i];
        }

        if (digitPatterns[1] && digitPatterns[7]) {
          segmentMapper.a = getDiff(digitPatterns[7], digitPatterns[1]);
          breaker = true;
        }
      });

      // Nađi pattern za broj 4, i slova b i d (bez mapiranje koje je koje)
      let bd = "";
      loopThroughValue(patterns, (i, breaker) => {
        if (patterns[i].length === 4) {
          digitPatterns[4] = patterns[i];

          for (let j = 0; j < digitPatterns[4].length; j++) {
            if (digitPatterns[1].indexOf(digitPatterns[4][j]) === -1) {
              bd += digitPatterns[4][j];
            }

            if (bd.length === 2) {
              break;
            }
          }
          breaker = true;
        }
      });

      // Nađi brojeve s dužinom 6 : 0, 6 9
      loopThroughValue(patterns, (i, breaker) => {
        if (patterns[i].length === 7) {
          digitPatterns[8] = patterns[i];
        }

        if (patterns[i].length === 6) {
          if (
            patterns[i].indexOf(bd[0]) === -1 ||
            patterns[i].indexOf(bd[1]) === -1
          ) {
            digitPatterns[0] = patterns[i];

            segmentMapper.d = patterns[i].indexOf(bd[0]) === -1 ? bd[0] : bd[1];
            segmentMapper.b = bd[0] === segmentMapper.d ? bd[1] : bd[0];
          } else {
            // Broj 9 ili 6 => u odnosu na c gledamo
            if (
              patterns[i].indexOf(digitPatterns[1][0]) === -1 ||
              patterns[i].indexOf(digitPatterns[1][1]) === -1
            ) {
              digitPatterns[6] = patterns[i];
            } else {
              digitPatterns[9] = patterns[i];
            }
          }
        }

        if (
          digitPatterns[0] &&
          digitPatterns[6] &&
          digitPatterns[8] &&
          digitPatterns[9]
        ) {
          segmentMapper.c = getDiff(digitPatterns[8], digitPatterns[6]);
          segmentMapper.d = getDiff(digitPatterns[6], digitPatterns[0]);
          segmentMapper.b = bd[0] === segmentMapper.d ? bd[1] : bd[0];
          segmentMapper.f =
            digitPatterns[1][0] === segmentMapper.c
              ? digitPatterns[1][1]
              : digitPatterns[1][0];
          breaker = true;
        }
      });

      loopThroughValue(patterns, (i, breaker) => {
        if (patterns[i].length === 5) {
          if (patterns[i].indexOf(segmentMapper.c) === -1) {
            digitPatterns[5] = patterns[i];
          } else {
            if (patterns[i].indexOf(segmentMapper.f) === -1) {
              digitPatterns[2] = patterns[i];
            } else {
              digitPatterns[3] = patterns[i];
            }
          }
        }

        if (digitPatterns[2] && digitPatterns[3] && digitPatterns[5]) {
          breaker = true;
          //segmentMapper.e = getDiff(digitPatterns[2], digitPatterns[3]);
          // segmentMapper.g = getDiff() =>dodat i iza slovo G po potrebi
        }
      });

      let stringDigit = "";
      for (const digit of fourDigits) {
        stringDigit += getDigit(digit, digitPatterns);
      }

      sum += parseInt(stringDigit);
    }

    console.log(`Total sum of all decoded numbers is: ${sum}`);
  });
}

part2();
