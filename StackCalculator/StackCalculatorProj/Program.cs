using StackCalculatorProj;
using StackCalculatorProj.Stacks.Impl;


Console.WriteLine("Insert Reverse Polish Notation type string to calculate");
var str = Console.ReadLine();

if (str != null)
{
    var stack = new StackArray<string>(str.Length);
    foreach (var element in str.Split())
    {
        stack.Push(element);
    }
    var result = StackCalculator.EvalRpn(stack);
    if (!stack.IsEmpty())
    {
        throw new ArgumentException("Incorrect postfix form");
    }
    Console.WriteLine(result);
}

