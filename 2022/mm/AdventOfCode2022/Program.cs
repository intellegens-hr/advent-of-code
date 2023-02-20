using AdventOfCode2022.Tasks._01;
using AdventOfCode2022.Tasks._02;
using AdventOfCode2022.Tasks._03;
using AdventOfCode2022.Tasks._04;
using AdventOfCode2022.Tasks._05;
using AdventOfCode2022.Tasks._06;
using AdventOfCode2022.Tasks._07;
using AdventOfCode2022.Tasks._08;
using AdventOfCode2022.Tasks._09;
using AdventOfCode2022.Tasks._10;

var startDateTime = DateTime.Now;
double span = 0.0;
long finishedAtTicks = DateTime.Now.Ticks;
long startedAtTicks = finishedAtTicks;

//Console.WriteLine("Task 1 - Part 1: " + Task_01.PartOne());
//span = (double)(DateTime.Now.Ticks - finishedAtTicks) / TimeSpan.TicksPerMillisecond;
//Console.WriteLine($"Finshed in {span}ms\n");
//finishedAtTicks = DateTime.Now.Ticks;

//Console.WriteLine("Task 1 - Part 2: " + Task_01.PartTwo());
//span = (double)(DateTime.Now.Ticks - finishedAtTicks) / TimeSpan.TicksPerMillisecond;
//Console.WriteLine($"Finshed in {span}ms\n");
//finishedAtTicks = DateTime.Now.Ticks;

//Console.WriteLine("Task 2 - Part 1: " + Task_02.PartOne());
//span = (double)(DateTime.Now.Ticks - finishedAtTicks) / TimeSpan.TicksPerMillisecond;
//Console.WriteLine($"Finshed in {span}ms\n");
//finishedAtTicks = DateTime.Now.Ticks;

//Console.WriteLine("Task 2 - Part 2: " + Task_02.PartTwo());
//span = (double)(DateTime.Now.Ticks - finishedAtTicks) / TimeSpan.TicksPerMillisecond;
//Console.WriteLine($"Finshed in {span}ms\n");
//finishedAtTicks = DateTime.Now.Ticks;

//Console.WriteLine("Task 3 - Part 1: " + Task_03.PartOne());
//span = (double)(DateTime.Now.Ticks - finishedAtTicks) / TimeSpan.TicksPerMillisecond;
//Console.WriteLine($"Finshed in {span}ms\n");
//finishedAtTicks = DateTime.Now.Ticks;

//Console.WriteLine("Task 3 - Part 2: " + Task_03.PartTwo());
//span = (double)(DateTime.Now.Ticks - finishedAtTicks) / TimeSpan.TicksPerMillisecond;
//Console.WriteLine($"Finshed in {span}ms\n");
//finishedAtTicks = DateTime.Now.Ticks;

//Console.WriteLine("Task 4 - Part 1: " + Task_04.PartOne());
//span = (double)(DateTime.Now.Ticks - finishedAtTicks) / TimeSpan.TicksPerMillisecond;
//Console.WriteLine($"Finshed in {span}ms\n");
//finishedAtTicks = DateTime.Now.Ticks;

//Console.WriteLine("Task 4 - Part 2: " + Task_04.PartTwo());
//span = (double)(DateTime.Now.Ticks - finishedAtTicks) / TimeSpan.TicksPerMillisecond;
//Console.WriteLine($"Finshed in {span}ms\n");
//finishedAtTicks = DateTime.Now.Ticks;

//Console.WriteLine("Task 5 - Part 1: " + Task_05.PartOne());
//span = (double)(DateTime.Now.Ticks - finishedAtTicks) / TimeSpan.TicksPerMillisecond;
//Console.WriteLine($"Finshed in {span}ms\n");
//finishedAtTicks = DateTime.Now.Ticks;

//Console.WriteLine("Task 5 - Part 2: " + Task_05.PartTwo());
//span = (double)(DateTime.Now.Ticks - finishedAtTicks) / TimeSpan.TicksPerMillisecond;
//Console.WriteLine($"Finshed in {span}ms\n");
//finishedAtTicks = DateTime.Now.Ticks;

//Console.WriteLine("Task 6 - Part 1: " + Task_06.PartOne(4));
//span = (double)(DateTime.Now.Ticks - finishedAtTicks) / TimeSpan.TicksPerMillisecond;
//Console.WriteLine($"Finshed in {span}ms\n");
//finishedAtTicks = DateTime.Now.Ticks;

//Console.WriteLine("Task 6 - Part 2: " + Task_06.PartOne(14));
//span = (double)(DateTime.Now.Ticks - finishedAtTicks) / TimeSpan.TicksPerMillisecond;
//Console.WriteLine($"Finshed in {span}ms\n");
//finishedAtTicks = DateTime.Now.Ticks;

//Console.WriteLine("Task 7 - Part 1: " + Task_07.PartOne());
//span = (double)(DateTime.Now.Ticks - finishedAtTicks) / TimeSpan.TicksPerMillisecond;
//Console.WriteLine($"Finshed in {span}ms\n");
//finishedAtTicks = DateTime.Now.Ticks;

//Console.WriteLine("Task 7 - Part 2: " + Task_07.PartTwo());
//span = (double)(DateTime.Now.Ticks - finishedAtTicks) / TimeSpan.TicksPerMillisecond;
//Console.WriteLine($"Finshed in {span}ms\n");
//finishedAtTicks = DateTime.Now.Ticks;

//Console.WriteLine("Task 8 - Part 1: " + Task_08.PartOne());
//span = (double)(DateTime.Now.Ticks - finishedAtTicks) / TimeSpan.TicksPerMillisecond;
//Console.WriteLine($"Finshed in {span}ms\n");
//finishedAtTicks = DateTime.Now.Ticks;

//Console.WriteLine("Task 8 - Part 2: " + Task_08.PartTwo());
//span = (double)(DateTime.Now.Ticks - finishedAtTicks) / TimeSpan.TicksPerMillisecond;
//Console.WriteLine($"Finshed in {span}ms\n");
//finishedAtTicks = DateTime.Now.Ticks;

//Console.WriteLine("Task 9 - Part 1: " + Task_09.PartOne());
//span = (double)(DateTime.Now.Ticks - finishedAtTicks) / TimeSpan.TicksPerMillisecond;
//Console.WriteLine($"Finshed in {span}ms\n");
//finishedAtTicks = DateTime.Now.Ticks;

//Console.WriteLine("Task 9 - Part 2: " + Task_09.PartTwo());
//span = (double)(DateTime.Now.Ticks - finishedAtTicks) / TimeSpan.TicksPerMillisecond;
//Console.WriteLine($"Finshed in {span}ms\n");
//finishedAtTicks = DateTime.Now.Ticks;

Console.WriteLine("Task 10 - Part 1: " + Task_10.PartOne());
span = (double)(DateTime.Now.Ticks - finishedAtTicks) / TimeSpan.TicksPerMillisecond;
Console.WriteLine($"Finshed in {span}ms\n");
finishedAtTicks = DateTime.Now.Ticks;

span = (double)(finishedAtTicks - startedAtTicks) / TimeSpan.TicksPerMillisecond;
Console.WriteLine($"Total time: {span}ms\n");

Console.ReadKey();