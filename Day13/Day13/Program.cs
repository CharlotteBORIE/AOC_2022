// See https://aka.ms/new-console-template for more information

using Day13;

Console.WriteLine("Hello, World!");

var read1 = new ReadFile("../../../Test.txt");
var Full = new OrderedMessage(read1.lines);


var read=new ReadFile("../../../Day13.txt");
var Full2 = new OrderedMessage(read.lines);
// Console.WriteLine(Full2.CountValid());

//5524 (Too low)
//6000 (Too high)