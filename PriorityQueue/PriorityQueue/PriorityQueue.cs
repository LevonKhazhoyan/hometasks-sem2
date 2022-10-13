namespace PriorityQueue;

/// <summary>
/// Priority Queue realization on list with numeric priority and generic value
/// </summary>
public class PriorityQueue<T>
{
    /// <summary>
    /// Pointer to the head of the queue
    /// </summary>
    private QueueElement? head;
    private int count;

    /// <summary>
    /// Adds a value to the queue by priority
    /// </summary>
    /// <param name="value">Value to add</param>
    /// <param name="priority">Priority value</param>
    public void Enqueue(T value, int priority)
    {
        var currentElement = head;
        var newElement = new QueueElement(value, priority);

        if (head == null || head.Priority < newElement.Priority)
        {
            newElement.Next = head;
            head = newElement;
            count++;
            return;
        }

        while (currentElement?.Next != null && currentElement.Next.Priority >= newElement.Priority)
        {
            currentElement = currentElement.Next;
        }
        
        count++;
        newElement.Next = currentElement?.Next;
        currentElement!.Next = newElement;
    }
 
    /// <summary>
    /// Returns the value of the queue head and moves the head to the next element
    /// </summary>
    public T Dequeue()
    {
        if (head == null)
        {
            throw new InvalidOperationException();
        }

        var value = head.Value;
        head = head.Next;
        count--;
        return value;
    }

    
    /// <summary>
    /// IsEmpty method
    /// </summary>
    public bool IsEmpty()
    {
        return count <= 0;
    }
    
    /// <summary>
    /// Priority queue element that has a value, a priority and a reference to the next element
    /// </summary>
    private class QueueElement
    {
        public QueueElement? Next { get; set; }

        public T Value { get; }

        public int Priority { get; }

        public QueueElement(T value, int priority)
        {
            Value = value;
            Priority = priority;
        }
    }
}
