using StackCalculatorProj;
using StackCalculatorProj.Stacks.Impl;

Console.WriteLine("Insert Reverse Polish Notation type string to calculate");
var s = Console.ReadLine();

if (s != null)
{
    var tks = new StackArray<string>(s.Length);
    foreach (var el in s.Split())
    {
        tks.Push(el);
    }
    var stackCalculator = new StackCalculator();
    stackCalculator.Calculate(tks);
}
