// See https://aka.ms/new-console-template for more information

using Day12;
using Path = Day12.Path;

Console.WriteLine("Hello, World!");

var read1=new ReadFile("../../../Text.txt");
var graph1=new Path(read1.lines);

var read=new ReadFile("../../../Day12.txt");
var graph=new Path(read.lines);

// entre 400 et 1500 , mon algo bloque à 140 trop de copy