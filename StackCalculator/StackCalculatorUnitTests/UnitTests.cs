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
        var error = _stringWriter.ToString().Replace("\n", String.Empty).Replace("\r", String.Empty);
        Assert.That(error, Is.EqualTo("Attempted to divide by zero"));
    }
    
    [Test]
    public void DivisionByZeroUsingLinkedListStack()
    {
        _calculator.Calculate( InsertStringInLinkedListStack("1 0 /"));
        var error = _stringWriter.ToString().Replace("\n", String.Empty).Replace("\r", String.Empty);
        Assert.That(error, Is.EqualTo("Attempted to divide by zero"));
    }
    
    [Test]
    public void SimpleRpnCalculatorTestUsingArrayStack()
    {
        _calculator.Calculate(InsertStringInLinkedListStack("1 1 1 1 1 1 1 1 1 + + + + + + + +")); 
        var error = _stringWriter.ToString().Replace("\n", String.Empty).Replace("\r", String.Empty);
        Assert.That(error, Is.EqualTo("9"));
    }
    
    [Test]
    public void SimpleRpnCalculatorTestUsingLinkedListStack()
    {
        _calculator.Calculate(InsertStringInLinkedListStack("1 2 3 * +")); 
        var response = _stringWriter.ToString().Replace("\n", String.Empty).Replace("\r", String.Empty);;
        Assert.That(response, Is.EqualTo("7"));
    }
    
    
    [Test]
    public void AttemptToCalculateIncorrectRpnExpressionUsingArrayStack()
    {
        _calculator.Calculate(InsertStringInLinkedListStack("1 2 3 * + "));
        var error = _stringWriter.ToString().Replace("\n", String.Empty).Replace("\r", String.Empty);
        Assert.That(error, Is.EqualTo("Specified argument was out of the range of valid values."));
    }
    
    [Test]
    public void AttemptToCalculateIncorrectRpnExpressionUsingLinkedListStack()
    {
        _calculator.Calculate(InsertStringInLinkedListStack("1 2 3 *"));
        var error = _stringWriter.ToString().Replace("\n", String.Empty).Replace("\r", String.Empty);
        Assert.That(error, Is.EqualTo("Incorrect postfix form"));
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