// See https://aka.ms/new-console-template for more information

using Day11;

Console.WriteLine("Hello, World!");

var read=new ReadFile("../../../Day11.txt");
var group=new Group(read.lines);
group.DoRounds(10000);
Console.WriteLine(group.GetMonkeyActivity());