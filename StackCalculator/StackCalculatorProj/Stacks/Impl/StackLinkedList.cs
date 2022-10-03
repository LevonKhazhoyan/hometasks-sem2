namespace StackCalculatorProj.Stacks.Impl;

/// <summary>
/// Stack class based on Linked List
/// </summary>
public class StackLinkedList<T> : IStack<T>
{
    private Node? _top;
    private int _size = 0;
    public int Size { get; }

    /// <summary>
    /// Checks if current <see cref="StackArray{T}"/> instance is empty
    /// </summary>
    public bool IsEmpty()
        => _size == 0;

    /// <summary>
    /// Adds an element in current <see cref="StackArray{T}"/> instance
    /// </summary>
    public void Push(T element)
    {
        if (element == null)
        {
            return;
        }
        var node = new Node(element, _top);
        _top = node;
        _size++;
    }
    
    /// <summary>
    /// Pops an element from current <see cref="StackArray{T}"/> instance
    /// </summary>
    public T Pop()
    {
        if (_top == null)
        {
            throw new InvalidOperationException();
        }
        var temp = _top.Element;
        _top = _top.Next;
        _size--;
        return temp;
    }
    
    /// <summary>
    /// Node just like in <see cref="LinkedList{T}"/> class for <see cref="StackLinkedList{T}"/> realization
    /// </summary>
    private class Node
    {
        public Node(T element, Node next)
        {
            Element = element;
            Next = next;
        }
        public T Element { get; }
        public Node Next { get; }
    }
}
