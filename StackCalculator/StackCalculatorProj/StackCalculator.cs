using StackCalculatorProj.Stacks;

namespace StackCalculatorProj;

/// <summary>
/// Calculator of Reverse Polish Notation type expressions
/// </summary>
public class StackCalculator
{

    /// <summary>
    /// Calls method "EvalRpn" for calculating RPN expression and handling exceptions from it
    /// </summary>
    public void Calculate(IStack<string> stack)
    {
        try
        {
            var r = EvalRpn(stack);
            if (!stack.IsEmpty()) throw new ArgumentException("Incorrect postfix form");
            Console.WriteLine(r);
        } catch (DivideByZeroException e) {
            Console.WriteLine("Attempted to divide by zero");
        } catch (ArgumentException e) {
            Console.WriteLine(e.Message);
        }
    }

    /// <summary>
    /// Calculating RPN expression
    /// </summary>
    private static double EvalRpn(IStack<string> stack)
    {
        var tk = stack.Pop();
        double x, y;
        if (double.TryParse(tk, out x))
            return x;
        y = EvalRpn(stack);
        x = EvalRpn(stack);
        switch (tk)
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
                throw new ArgumentException("Attempted to divide by zero");
        }
        return x;
    }
}
