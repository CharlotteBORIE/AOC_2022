// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using Day16;

Console.WriteLine("Hello, World!");

// var readTest = new ReadFile("../../../Test.txt");
// var cave = new Cave(readTest.lines);
// Console.WriteLine(cave.FindMaxReleasedPressureWithElephant(30));


var read=new ReadFile("../../../Day16.txt");
var cave2 = new Cave(read.lines);
Console.WriteLine(cave2.FindMaxReleasedPressureWithElephant(30));

// should find 2752


