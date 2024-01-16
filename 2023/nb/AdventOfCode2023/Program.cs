// See https://aka.ms/new-console-template for more information

using AdventOfCode2023.Day1;
using AdventOfCode2023.Day2;

//var day1 = new Day1();
//var calibrations = day1.SumCalibrations();

//Console.WriteLine($"Calibrations sum = {calibrations}");

var day2 = new Day2();
var validGamesCount = day2.CountValidGames();

Console.WriteLine($"Valid games count = {validGamesCount}");
