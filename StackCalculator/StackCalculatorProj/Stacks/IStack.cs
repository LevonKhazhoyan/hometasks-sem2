namespace StackCalculatorProj.Stacks;

/// <summary>
/// Stack Interface
/// </summary>
public interface IStack<T>
{
    /// <summary>
    /// Adds an element in instance of realization <see cref="IStack{T}"/> class
    /// </summary>
    void Push(T element);
    /// <summary>
    /// Pops an element from instance of realization <see cref="IStack{T}"/> class
    /// </summary>
    T Pop();
    /// <summary>
    /// Checks if an instance of realization <see cref="IStack{T}"/> class is empty
    /// </summary>
    bool IsEmpty();
}
