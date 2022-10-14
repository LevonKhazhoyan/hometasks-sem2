using StackCalculatorProj;
using StackCalculatorProj.Stacks.Impl;

Console.WriteLine("Input Reverse Polish Notation type string to calculate");
var str = Console.ReadLine();

if (str == null)
{
    return;
}
var stack = new StackLinkedList<double>();
var result = StackCalculator.EvalRpn(stack, str);

if (!stack.IsEmpty())
{
    Console.WriteLine("Incorrect postfix form. Try again");
}
else
{
    Console.WriteLine(result);
}
