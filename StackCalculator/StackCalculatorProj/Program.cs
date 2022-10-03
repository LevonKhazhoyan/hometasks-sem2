using StackCalculatorProj;
using StackCalculatorProj.Stacks.Impl;


Console.WriteLine("Input Reverse Polish Notation type string to calculate");
var str = Console.ReadLine();

if (str != null)
{
    var stack = new StackLinkedList<double>();

    var result = StackCalculator.EvalRpn(stack, str);
    if (!stack.IsEmpty())
    {
        throw new ArgumentException("Incorrect postfix form");
    }
    Console.WriteLine(result);
}

