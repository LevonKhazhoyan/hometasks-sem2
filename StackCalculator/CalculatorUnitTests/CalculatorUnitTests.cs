using NUnit.Framework;
using StackCalculatorProj;
using StackCalculatorProj.Stacks;
using StackCalculatorProj.Stacks.Impl;

namespace CalculatorUnitTests;

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
    public void CorrectRpnCalculations(IStack<double> testStack, string expression,double response)
    {
        Assert.That(StackCalculator.EvalRpn(testStack, expression), Is.EqualTo(response));
    }

    [Test]
    [TestCaseSource(nameof(DivideByZeroStacks))]
    public void FailedRpnCalculationsDivideByZero(IStack<double> testStack, string expression)
    {
        Assert.Throws<DivideByZeroException>(() => StackCalculator.EvalRpn(testStack, expression));
    }
    
    [Test]
    [TestCaseSource(nameof(IncorrectForm))]
    public void FailedRpnCalculationsIncorrectForm(IStack<double> testStack, string expression)
    {
        Assert.Throws<InvalidOperationException>(() => StackCalculator.EvalRpn(testStack, expression));
    }
    
    public static IEnumerable<TestCaseData> CorrectDataForCalculator
    {
        get
        {
            yield return new TestCaseData(new StackLinkedList<double>(), "1 1 1 1 1 1 1 1 1 + + + + + + + +", 9);
            yield return new TestCaseData(new StackLinkedList<double>(), "1 1 1 1 20 1 1 1 1 + / + + + + + +", 25.5d);
            yield return new TestCaseData(new StackLinkedList<double>(), "1 2 3 * +", 7);
            yield return new TestCaseData(new StackLinkedList<double>(), "1 2 300 * +", 601);
        }
    }
    
    public static IEnumerable<TestCaseData> DivideByZeroStacks
    {
        get
        {
            yield return new TestCaseData(new StackLinkedList<double>(), "1 0 /");
            yield return new TestCaseData(new StackLinkedList<double>(), "3 1 0 / +");
            yield return new TestCaseData(new StackLinkedList<double>(), "3 1 8 0 / + +");
            yield return new TestCaseData(new StackLinkedList<double>(), "1 0 /");
        }
    }
    
    public static IEnumerable<TestCaseData> IncorrectForm
    {
        get
        {
            yield return new TestCaseData(new StackLinkedList<double>(), "1 0 + + +");
            yield return new TestCaseData(new StackLinkedList<double>(), "1 2 / +");
        }
    }
}