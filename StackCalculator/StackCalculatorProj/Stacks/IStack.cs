namespace StackCalculatorProj.Stacks;

/// <summary>
/// Stack Interface
/// </summary>
public interface IStack<T>
{
    /// <summary>
    /// Adds an element
    /// </summary>
    void Push(T element);
    
    /// <summary>
    /// Pops an element 
    /// </summary>
    T Pop();
    
    /// <summary>
    /// Checks if it's empty
    /// </summary>
    bool IsEmpty();
}
