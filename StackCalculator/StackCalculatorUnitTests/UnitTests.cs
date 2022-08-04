using NUnit.Framework;
using StackCalculatorProj;
using StackCalculatorProj.Stacks;
using StackCalculatorProj.Stacks.Impl;

namespace StackCalculatorUnitTests;

public class Tests
{
    private readonly StackCalculator _calculator = new();
    private StringWriter _stringWriter = new();
    
    [SetUp]
    public void SetUp()
    {
        _stringWriter = new StringWriter();
        Console.SetOut(_stringWriter);    
    }
    
    [Test]
    public void DivisionByZeroUsingArrayStack()
    {
        _calculator.Calculate(InsertStringInArrayStack("1 0 /"));
        Assert.That(_stringWriter.ToString(), Is.EqualTo("Attempted to divide by zero\r\n"));
    }
    
    [Test]
    public void DivisionByZeroUsingLinkedListStack()
    {
        _calculator.Calculate( InsertStringInLinkedListStack("1 0 /"));
        Assert.That(_stringWriter.ToString(), Is.EqualTo("Attempted to divide by zero\r\n"));
    }
    
    [Test]
    public void SimpleRpnCalculatorTestUsingArrayStack()
    {
        _calculator.Calculate(InsertStringInLinkedListStack("1 1 1 1 1 1 1 1 1 + + + + + + + +"));
        Assert.That(_stringWriter.ToString(), Is.EqualTo("9\r\n"));
    }
    
    [Test]
    public void SimpleRpnCalculatorTestUsingLinkedListStack()
    {
        _calculator.Calculate( InsertStringInLinkedListStack("1 2 3 * +"));
        Assert.That(_stringWriter.ToString(), Is.EqualTo("7\r\n"));
    }
    
    
    [Test]
    public void AttemptToCalculateIncorrectRpnExpressionUsingArrayStack()
    {
        _calculator.Calculate(InsertStringInLinkedListStack("1 2 3 * + "));
        Assert.That(_stringWriter.ToString(), Is.EqualTo("Specified argument was out of the range of valid values.\r\n"));
    }
    
    [Test]
    public void AttemptToCalculateIncorrectRpnExpressionUsingLinkedListStack()
    {
        _calculator.Calculate(InsertStringInLinkedListStack("1 2 3 *"));
        Assert.That(_stringWriter.ToString(), Is.EqualTo("Incorrect postfix form\r\n"));
    }
    
    private static IStack<string> InsertStringInArrayStack(string word)
    {
        var tks = new StackArray<string>(word.Length);
        foreach (var el in word.Split())
            tks.Push(el);
        return tks;
    }
    
    private static IStack<string> InsertStringInLinkedListStack(string word)
    {
        var tks = new StackArray<string>(word.Length);
        foreach (var el in word.Split())
            tks.Push(el);
        return tks;
    }
}