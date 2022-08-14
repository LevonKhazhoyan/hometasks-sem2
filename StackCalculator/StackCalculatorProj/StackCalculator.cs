using StackCalculatorProj.Stacks;

namespace StackCalculatorProj;

/// <summary>
/// Calculator of Reverse Polish Notation type expressions
/// </summary>
public static class StackCalculator
{
    /// <summary>
    /// Calculating RPN expression
    /// </summary>
    public static double EvalRpn(IStack<string> stack)
    {
        var element = stack.Pop();
        if (double.TryParse(element, out var x))
        {
            return x;
        }
        var y = EvalRpn(stack);
        x = EvalRpn(stack);
        switch (element)
        {
            case "+":
                x += y;
                break;
            case "-":
                x -= y;
                break;
            case "*":
                x *= y;
                break;
            case "/":
                if (Math.Abs(y) < double.Epsilon)
                    throw new DivideByZeroException();
                x /= y;
                break;
            default:
                throw new ArgumentException();
        }
        return x;
    }
}
