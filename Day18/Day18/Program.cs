// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using Day18;

Console.WriteLine("Hello, World!");

var readTest = new ReadFile("../../../Test.txt");
var lavaTest = new Droplets(readTest.lines);
Console.WriteLine(lavaTest.AppearingSidesExterior());

//should find 58


var read=new ReadFile("../../../Day18.txt");

var lava = new Droplets(read.lines);
Console.WriteLine(lava.AppearingSidesExterior());

//should find 2058



