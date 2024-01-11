// See https://aka.ms/new-console-template for more information

using Day10;

var read=new ReadFile("../../../Day10.txt");
var processeur=new Processsor(read.lines);
Console.WriteLine(processeur.GetSumLevels());
processeur.ShowCRT();


//15000 too high