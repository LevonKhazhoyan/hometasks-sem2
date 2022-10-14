namespace StackCalculatorProj.Stacks.Impl;

/// <summary>
/// Stack based on Array
/// </summary>
public class StackArray<T> : IStack<T>
{
    private int _top = 0;
    private readonly int _size;
    private T[] _stack;
    
    /// <summary>
    /// Initializes a new instance of the <see cref="StackArray{T}"/> class
    /// </summary>
    public StackArray(int size = 10)
    {
        _size = size;
        _stack = new T[size];
    }
    
    /// <summary>
    /// Checks if current <see cref="StackArray{T}"/> instance is empty
    /// </summary>
    public bool IsEmpty()
        => _top == 0;
    
    /// <summary>
    /// Adds an element in current <see cref="StackArray{T}"/> instance
    /// </summary>
    public void Push(T element)
    {
        if (_top > _size)
        {
            Array.Resize(ref _stack, _size * 2);
        }
        _stack[_top] = element;
        _top++;
    }

    /// <summary>
    /// Pops an element from current <see cref="StackArray{T}"/> instance
    /// </summary>
    public T Pop()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException();
        }
        var result = _stack[_top];
        _stack = _stack.SkipLast(1).ToArray();
        _top--;
        return result;
    }
}
