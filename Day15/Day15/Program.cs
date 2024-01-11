// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using Day15;

Console.WriteLine("Hello, World!");

var readTest = new ReadFile("../../../Test.txt");
var y1 = 10;
var cave = new CaveMap(readTest.lines);
Console.WriteLine(cave.CountRangeNotAllowed(y1));
// rep =26;
//0 & 20

Console.WriteLine(cave.FindTuningFrequency(20));
var y = 2000000;


var read=new ReadFile("../../../Day15.txt");
var cave2 = new CaveMap(read.lines);
Console.WriteLine(cave2.CountRangeNotAllowed(y));
Stopwatch stopwatch = new Stopwatch();
stopwatch.Start();
Console.WriteLine(cave2.FindTuningFrequency(4000000));
stopwatch.Stop();
Console.WriteLine(stopwatch.ElapsedMilliseconds);

//0 & 4000000

// 1790297260621 too low

