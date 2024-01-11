// See https://aka.ms/new-console-template for more information

using Day4;

var read = new ReadFile("../../../Day4.txt");
Troupe troupe = new Troupe(read);
Console.WriteLine(troupe.GetOverLapNumber());
