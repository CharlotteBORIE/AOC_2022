// See https://aka.ms/new-console-template for more information

using Day14;

Console.WriteLine("Hello, World!");

// var read1 = new ReadFile("../../../Test.txt");
// var rock = new RockPath(read1.lines);
// Console.WriteLine(rock.DropSandUntilBlocked());

var watch = new System.Diagnostics.Stopwatch();
watch.Start();
var read=new ReadFile("../../../Day14.txt");

var rock2 = new RockPath(read.lines);
Console.WriteLine(rock2.DropSandUntilBlocked());
watch.Stop();
Console.WriteLine(watch.ElapsedMilliseconds);