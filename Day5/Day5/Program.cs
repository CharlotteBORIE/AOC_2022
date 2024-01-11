// See https://aka.ms/new-console-template for more information

using Day5;

var read = new ReadFile("../../../Day5.txt");
var Cargo=new CargoConfiguration(read);
Cargo.DoNewMoves();
Cargo.GetFirst();