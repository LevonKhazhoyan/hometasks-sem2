using NUnit.Framework;
using StackCalculatorProj;
using StackCalculatorProj.Stacks;
using StackCalculatorProj.Stacks.Impl;

namespace StackCalculatorUnitTests;

public class Tests
{
    private StringWriter _stringWriter = new();
    
    [SetUp]
    public void SetUp()
    {
        _stringWriter = new StringWriter();
        Console.SetOut(_stringWriter);    
    }
    
    [Test]
    [TestCaseSource(nameof(CorrectDataForCalculator))]
    public void CorrectRpnCalculations(IStack<string> testStack, double response)
    {
        Assert.That(StackCalculator.EvalRpn(testStack), Is.EqualTo(response));
    }

    [Test]
    [TestCaseSource(nameof(DivideByZeroStacks))]
    public void FailedRpnCalculationsDivideByZero(IStack<string> testStack)
    {
        Assert.Throws<DivideByZeroException>(() => StackCalculator.EvalRpn(testStack));
    }
    
    [Test]
    [TestCaseSource(nameof(IncorrectForm))]
    public void FailedRpnCalculationsIncorrectForm(IStack<string> testStack)
    {
        Assert.Throws<InvalidOperationException>(() => StackCalculator.EvalRpn(testStack));
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
    
    public static IEnumerable<TestCaseData> CorrectDataForCalculator
    {
        get
        {
            yield return new TestCaseData(InsertStringInLinkedListStack("1 1 1 1 1 1 1 1 1 + + + + + + + +"), 9);
            yield return new TestCaseData(InsertStringInLinkedListStack("1 1 1 1 20 1 1 1 1 + / + + + + + +"), 25.5d);
            yield return new TestCaseData(InsertStringInArrayStack("1 2 3 * +"), 7);
            yield return new TestCaseData(InsertStringInArrayStack("1 2 300 * +"), 601);
        }
    }
    
    public static IEnumerable<TestCaseData> DivideByZeroStacks
    {
        get
        {
            yield return new TestCaseData(InsertStringInLinkedListStack("1 0 /"));
            yield return new TestCaseData(InsertStringInArrayStack("3 1 0 / +"));
            yield return new TestCaseData(InsertStringInLinkedListStack("3 1 8 0 / + +"));
            yield return new TestCaseData(InsertStringInArrayStack("1 0 /"));
        }
    }
    
    public static IEnumerable<TestCaseData> IncorrectForm
    {
        get
        {
            yield return new TestCaseData(InsertStringInLinkedListStack("1 0 + + +"));
            yield return new TestCaseData(InsertStringInLinkedListStack("3 1 0 + * ["));
            yield return new TestCaseData(InsertStringInArrayStack("1 2 / +"));
        }
    }
}