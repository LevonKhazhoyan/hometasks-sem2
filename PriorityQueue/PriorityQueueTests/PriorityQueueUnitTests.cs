using PriorityQueue;
using NUnit.Framework;

namespace PriorityQueueTests;

public class PriorityQueueTests
{ 
    PriorityQueue<string> priorityQueue = new();
    
    [SetUp]
    public void Setup()
    {
        priorityQueue = new PriorityQueue<string>();
        priorityQueue.Enqueue("aaa", 1);
        priorityQueue.Enqueue("abc", 3);
        priorityQueue.Enqueue("abb", 2);
        priorityQueue.Enqueue("abd", 3);
    }

    [Test]
    public void FifoTest()
    {
        Assert.That(priorityQueue.Dequeue(), Is.EqualTo("abc"));
        Assert.That(priorityQueue.Dequeue(), Is.EqualTo("abd"));
    }
    
    [Test]
    public void IsEmptyTest()
    {
        Assert.That(priorityQueue.IsEmpty, Is.EqualTo(false));
        priorityQueue.Dequeue();
        priorityQueue.Dequeue();
        priorityQueue.Dequeue();
        Assert.That(priorityQueue.IsEmpty, Is.EqualTo(false));
        priorityQueue.Dequeue();
        Assert.That(priorityQueue.IsEmpty, Is.EqualTo(true));
    }
}