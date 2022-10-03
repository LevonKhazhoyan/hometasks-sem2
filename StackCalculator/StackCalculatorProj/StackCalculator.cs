namespace StackCalculatorProj;

using Stacks;

/// <summary>
/// Calculator of Reverse Polish Notation type expressions
/// </summary>
public static class StackCalculator
{
    /// <summary>
    /// Calculating RPN expression
    /// </summary>
    public static double EvalRpn(IStack<double> stack, string expression)
    {
        var operationsAndOperands = expression.Split(' ');
        
        foreach (var element in operationsAndOperands)
        {
            if (double.TryParse(element, out var number))
            {
                stack.Push(number);
                continue;
            }
            if (element is "+" or "-" or "*" or "/")
            {
                var firstNumber = stack.Pop();
                var secondNumber = stack.Pop();
                switch (element)
                {
                    case "+":
                        stack.Push(firstNumber + secondNumber);
                        break;
                    case "-":
                        stack.Push(secondNumber - firstNumber);
                        break;
                    case "*":
                        stack.Push(firstNumber * secondNumber);
                        break;
                    case "/":
                        if (Math.Abs(firstNumber) < double.Epsilon)
                        {
                            throw new DivideByZeroException();
                        }
                        stack.Push(secondNumber / firstNumber);
                        break;
                }
            }
            else
            {
                throw new ArgumentException();
            }
        }

        var result = stack.Pop();
        if (!stack.IsEmpty())
        {
            throw new ArithmeticException("");
        }
        return result;
    }
}
