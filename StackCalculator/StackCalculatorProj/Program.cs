using StackCalculatorProj;
using StackCalculatorProj.Stacks.Impl;

Console.WriteLine("Input Reverse Polish Notation type string to calculate");
var str = Console.ReadLine();

if (str == null)
{
    return;
}
var stack = new StackLinkedList<double>();
try
{
    var result = StackCalculator.EvalRpn(stack, str);

    if (!stack.IsEmpty())
    {
        Console.WriteLine("Incorrect number of arguments. Try again");
    }
    else
    {
        Console.WriteLine(result);
    }
}
catch (Exception e) when (e is InvalidOperationException or ArgumentException)
{
    Console.WriteLine("Invalid operation or operand in RPN calculator");
}
