// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using Day20;

Console.WriteLine("Hello, World!");

var readTest = new ReadFile("../../../Test.txt");
var messageTest = new EncryptedMessage(readTest.lines);




var read=new ReadFile("../../../Day20.txt");
var message = new EncryptedMessage(read.lines);

//7603 too low
//22274 too high

//index 0 3025
//should be 6420481789383

