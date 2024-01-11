// See https://aka.ms/new-console-template for more information

using Day3;

var read = new ReadFile("../../../Day3.txt");

var elfe=new Elfe(read);
Console.WriteLine(elfe.value);
var troupe=new Troupe(read);
Console.WriteLine(troupe.value);
