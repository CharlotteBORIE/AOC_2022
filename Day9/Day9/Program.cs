// See https://aka.ms/new-console-template for more information

using Day9;

var read=new ReadFile("../../../Day9.txt");
var grid = new Grid(read.lines);
grid.GetMovesRope();
Console.WriteLine(grid.CountTailPositions());
Console.WriteLine(grid.CountHeadPositions());
