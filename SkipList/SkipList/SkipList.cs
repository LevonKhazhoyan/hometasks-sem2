namespace SkipList;

using System.Collections;

/// <summary>
/// SkipList realization of IList interface
/// </summary> 
public class SkipList<T> : IList<T> where T : IComparable <T>
{
    private SkipNode head;
    private int level { get; set; }
    private int maxLevel;
    public int Count { get; private set; } 
    public bool IsReadOnly { get; }
    private readonly double probability;
    private static readonly Random random = new();

    /// <summary>
    /// SkipList constructor
    /// <param name="maxLevel">Maximum level of list</param>
    /// </summary> 
    public SkipList(int maxLevel) {
        head = new SkipNode(default!, 0);
        level = -1;
        this.maxLevel = maxLevel;
        Count = 0;
        probability = 0.5;
    }
    
    /// <summary>
    /// SkipList constructor
    /// <param name="maxLevel">Maximum level of list</param>
    /// <param name="probability">Probability of list</param>
    /// </summary> 
    public SkipList(int maxLevel, double probability) {
        head = new SkipNode(default!, 0);
        level = -1;
        this.maxLevel = maxLevel;
        Count = 0;
        this.probability = probability;
    }
    
    /// <summary>
    /// Getter of random number from 0 to maxLevel
    /// <param name="levelProbability">Maximum level of list</param>
    /// </summary> 
    private int RandomLevel(double levelProbability) {
        int randomLevel;
        for (randomLevel = 0; random.NextDouble() < levelProbability && randomLevel < maxLevel; randomLevel++) {
        }
        return randomLevel;
    }

    /// <summary>
    /// Clears head of list and resets Count
    /// </summary> 
    public void Clear()
    {
        head = new SkipNode(default!, -1);
        Count = 0;
    }

    /// <summary>
    /// Checks if list contains item
    /// </summary> 
    public bool Contains(T item)
    {
        if (Count == 0)
        {
            return false;
        }
        
        var x = head;
        for (var i = level; i >= 0; i--) { 
            while (x.Forward?[i] != null && x.Forward[i].Value?.CompareTo(item) < 0)
            {
                x = x.Forward[i];
            }
        }
        x = x.Forward?[0];
        if (x != null && x.Value?.CompareTo(item) == 0) { return true; }
        { return false; }
    }

    /// <summary>
    /// Value by index
    /// </summary> 
    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= Count)
            {
                throw new ArgumentOutOfRangeException(nameof(index));
            }

            var counter = 0;
            var x = head.Forward?[0];
            while (counter != index)
            {
                x = x?.Forward![0];
                counter++;
            }

            return x!.Value!;
        }
        set => throw new NotImplementedException();
    }

    /// <summary>
    /// Adds item to list
    /// </summary> 
    public void Add(T item) {
        var newLevel = RandomLevel(probability);
        if (newLevel > level) { 
            AdjustHead(newLevel);
        }
        var update = new SkipNode[level + 1];
        var x = head;
        for (var i = level; i >= 0; i--) {
            while (x.Forward?[i] != null && x.Forward[i].Value?.CompareTo(item) < 0) {
                x = x.Forward[i];
            }
            update[i] = x;
        }
        x = new SkipNode(item, newLevel);
        for (var i = 0; i <= newLevel; i++) { 
            x.Forward![i] = update[i].Forward![i];
            update[i].Forward![i] = x;
        }
        Count++;
    }

    /// <summary>
    /// Adds new levels of elements
    /// </summary> 
    private void AdjustHead(int newLevel) {
        var temp = head;
        head = new SkipNode(default!, newLevel);
        for (var i = 0; i <= level; i++) {
            head.Forward[i] = temp.Forward[i];
        }
        level = newLevel;
    }
    
    /// <summary>
    /// Copy values of skipList in array starting by arrayIndex
    /// </summary> 
    public void CopyTo(T[] array, int arrayIndex)
    {
        if (array == null)
        {
            throw new ArgumentNullException();
        }

        if (arrayIndex < 0)
        {
            throw new ArgumentOutOfRangeException();
        }

        if (array.Length - arrayIndex < Count)
        {
            throw new ArgumentException();
        }

        var element = head.Forward?[0];
        for (var counter = arrayIndex; counter < array.Length; counter++)
        {
            array[counter] = element.Value;
            element = element.Forward?[0];
        }
    }

    /// <summary>
    /// Insert by index is not implemented in skipList
    /// </summary> 
    public void Insert(int index, T item)
    {
        throw new NotImplementedException();
    }

    
    /// <summary>
    /// Index of item in list
    /// </summary> 
    public int IndexOf(T item)
    {
        var counter = 0;
        for (var i = level; i >= 0; i--)
        {
            var x = head.Forward[0];
            while (x.Forward?[0] != null && x.Forward[0].Value.CompareTo(item) < 0)
            {
                x = x.Forward[0];
                counter++;
            }

            if (x.Forward[0].Value.CompareTo(item) == 0)
            {
                return counter;
            }
        }

        return -1;
    }

    /// <summary>
    /// Remove item in list
    /// </summary> 
    public bool Remove(T item)
    {
        var contains = Contains(item);
        var current = head;
        
        for (var i = level; i > -1; i--)
        {
            while (current.Forward[i] != null && current.Forward[i].Value.CompareTo(item) < 0) 
            {
                current = current.Forward[i];
            }

            if (current.Forward[i] != null && current.Forward[i].Value.CompareTo(item) == 0 && current.Forward[i].Forward != null)
            {
                current.Forward[i] = current.Forward[i].Forward?[i];
            }
        }

        while (level > 0 && head.Forward[level] == null)
        {
            level--;
        }

        Count--;
        
        if (contains)
        {
            return !Contains(item);
        }
        
        return false;
    }
    
    /// <summary>
    /// Remove item by index 
    /// </summary> 
    public void RemoveAt(int index)
    {
        var removeElement = this[index];
        
        Remove(removeElement);
    }
    
    /// <summary>
    /// Node class with list of forward nodes and value
    /// </summary> 
    private class SkipNode {
        private T? value;
        public T? Value { get; }
        private SkipNode[]? forward;
        public SkipNode[]? Forward
        {
            get => forward;
            set => forward = value ?? throw new ArgumentNullException(nameof(value));
        }

        public SkipNode(T value, int level) {
            Value = value;
            forward = new SkipNode[level + 1];
            for (var i = 0; i < level; i++) {
                forward[i] = null;
            }
        }
    }
    
    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    IEnumerator<T> IEnumerable<T>.GetEnumerator() => GetEnumerator();

    private SkipListEnum GetEnumerator()
    {
        return new SkipListEnum(this);
    }

    /// <summary>
    /// Class for implementing the IEnumerator<T> interface
    /// </summary>
    private class SkipListEnum : IEnumerator<T>
    {
        private readonly SkipList<T> list;
        private SkipNode? head;

        public SkipListEnum(SkipList<T> inputList)
        {
            list = inputList;
            head = list.head.Forward[0];
        }

        public bool MoveNext()
        {
            if (head != head.Forward[0] != null)
            {
                head = head.Forward[0];
                return true;
            }
            head = null;
            return false;
        }

        public void Reset() => head = list.head;

        object IEnumerator.Current { get; }

        public T Current => head.Value;

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}