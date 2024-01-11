// See https://aka.ms/new-console-template for more information

using Day8;

var read=new ReadFile("../../../Day8.txt");
var grove=new TreeGrove(read.lines);
Console.WriteLine(grove.GetBestScore());
