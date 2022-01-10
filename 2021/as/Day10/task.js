const fs = require("fs");

/* Link to task description: https://adventofcode.com/2021/day/10 */

/**
 * Part 1
 */
function part1() {
  function isOpening(char) {
    switch (char) {
      case "(":
      case "[":
      case "{":
      case "<":
        return true;

      default:
        return false;
    }
  }

  function checkMatch(characters, opening, closing) {
    for (const character of characters) {
      if (character.opening === opening) {
        return character.closing === closing;
      }
    }
  }

  function getScore(characters, closing) {
    for (const character of characters) {
      if (character.closing === closing) {
        return character.score;
      }
    }
  }

  function deleteLowestChunk(currentChunk) {
    if (currentChunk.child && currentChunk.child.child) {
      return deleteLowestChunk(currentChunk.child);
    } else {
      currentChunk.child = undefined;
    }
  }

  function reachDeepestChunk(currentChunk, callback) {
    if (currentChunk.child) {
      return reachDeepestChunk(currentChunk.child, callback);
    } else {
      return callback(currentChunk);
    }
  }

  class Chunk {
    constructor(opening) {
      this.opening = opening;
      this.closing = undefined;
      this.child = undefined;
    }
  }

  fs.readFile("input.txt", "utf-8", function (err, data) {
    if (err) throw err;
    const arrayOfInputs = data.split("\n");
    const characters = [
      { opening: "(", closing: ")", score: 3 },
      { opening: "[", closing: "]", score: 57 },
      { opening: "{", closing: "}", score: 1197 },
      { opening: "<", closing: ">", score: 25137 },
    ];
    let totalScoreCount = 0;

    for (const line of arrayOfInputs) {
      const letters = line.split("");
      let mostOuterChunk;

      for (const letter of letters) {
        let breaker = false;
        if (isOpening(letter)) {
          let newChunk = new Chunk(letter);
          if (!mostOuterChunk) {
            mostOuterChunk = newChunk;
          } else {
            reachDeepestChunk(mostOuterChunk, (deepestChunk) => {
              deepestChunk.child = newChunk;
            });
          }
        } else {
          let deleteTrigger = true;
          reachDeepestChunk(mostOuterChunk, (deepestChunk) => {
            if (!checkMatch(characters, deepestChunk.opening, letter)) {
              totalScoreCount += getScore(characters, letter);
              breaker = true;
              deleteTrigger = false;
            }
          });

          if (deleteTrigger) {
            deleteLowestChunk(mostOuterChunk);
          }
        }

        if (breaker) {
          break;
        }
      }
    }

    console.log(`Total sum of invalid syntaxes is ${totalScoreCount}`);
  });
}

part1();

/**
 * Part 2
 */
function part2() {
  function addMissingCharacters(
    characters,
    mostOuterChunk,
    missingCharacterArray = []
  ) {
    let match = getClosingMatch(characters, mostOuterChunk.opening);
    missingCharacterArray.unshift(match);

    if (mostOuterChunk.child) {
      return addMissingCharacters(
        characters,
        mostOuterChunk.child,
        missingCharacterArray
      );
    } else {
      return missingCharacterArray;
    }
  }

  function calculateScore(characters, matchArray) {
    let score = 0;
    for (const match of matchArray) {
      score = score * 5 + getScore(characters, match);
    }
    return score;
  }

  function isOpening(char) {
    switch (char) {
      case "(":
      case "[":
      case "{":
      case "<":
        return true;

      default:
        return false;
    }
  }

  function checkMatch(characters, opening, closing) {
    for (const character of characters) {
      if (character.opening === opening) {
        return character.closing === closing;
      }
    }
  }

  function getClosingMatch(characters, opening) {
    for (const character of characters) {
      if (character.opening === opening) {
        return character.closing;
      }
    }
  }

  function getScore(characters, closing) {
    for (const character of characters) {
      if (character.closing === closing) {
        return character.score;
      }
    }
  }

  function deleteLowestChunk(currentChunk) {
    if (currentChunk.child && currentChunk.child.child) {
      return deleteLowestChunk(currentChunk.child);
    } else {
      currentChunk.child = undefined;
    }
  }

  function reachDeepestChunk(currentChunk, callback) {
    if (currentChunk.child) {
      return reachDeepestChunk(currentChunk.child, callback);
    } else {
      return callback(currentChunk);
    }
  }

  class Chunk {
    constructor(opening) {
      this.opening = opening;
      this.closing = undefined;
      this.child = undefined;
    }
  }

  fs.readFile("input.txt", "utf-8", function (err, data) {
    if (err) throw err;
    const arrayOfInputs = data.split("\n");
    const characters = [
      { opening: "(", closing: ")", score: 1 },
      { opening: "[", closing: "]", score: 2 },
      { opening: "{", closing: "}", score: 3 },
      { opening: "<", closing: ">", score: 4 },
    ];
    let scoreArray = [];

    for (const line of arrayOfInputs) {
      const letters = line.split("");
      let mostOuterChunk;
      let letterIndex = 0;

      for (const letter of letters) {
        let breaker = false;

        // Check if characets is opening or closing
        // Add chunk as child or outer
        if (isOpening(letter)) {
          let newChunk = new Chunk(letter);

          if (!mostOuterChunk) {
            mostOuterChunk = newChunk;
          } else {
            reachDeepestChunk(mostOuterChunk, (deepestChunk) => {
              deepestChunk.child = newChunk;
            });
          }
        } else {
          let deleteTrigger = true;

          reachDeepestChunk(mostOuterChunk, (deepestChunk) => {
            if (!checkMatch(characters, deepestChunk.opening, letter)) {
              breaker = true;
              deleteTrigger = false;
            }
          });

          // If outer chunk has child, delete deepest child.
          // Otherwise delete outer chunk
          if (deleteTrigger) {
            if (mostOuterChunk.child) {
              deleteLowestChunk(mostOuterChunk);
            } else {
              mostOuterChunk = undefined;
            }
          }
        }

        letterIndex++;
        if (breaker) {
          break;
        }

        if (letterIndex === letters.length) {
          const missingCharacters = addMissingCharacters(
            characters,
            mostOuterChunk
          );
          const score = calculateScore(characters, missingCharacters);
          scoreArray.push(score);
        }
      }
    }

    scoreArray.sort((a, b) => a - b);
    const middleScore = scoreArray[Math.floor(scoreArray.length / 2)];
    console.log(`Middle score is ${middleScore}`);
  });
}

part2();
