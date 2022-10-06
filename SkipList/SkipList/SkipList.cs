using System.Collections;

namespace SkipList;

public class SkipList<T> : IList<T> where T : IComparable <T>
{
    private SkipNode head;
    private int level;
    private int maxLevel;
    private int count;
    public int Count
    {
        get => count;
        private set => count = value;
    }
    public bool IsReadOnly { get; }
    private readonly double probability;
    private static readonly Random random = new();

    public SkipList(int maxLevel) {
        head = new SkipNode(default, 0);
        level = -1;
        this.maxLevel = maxLevel;
        count = 0;
        probability = 0.5;
    }
    
    public SkipList(int maxLevel, double probability) {
        head = new SkipNode(default, 0);
        level = -1;
        this.maxLevel = maxLevel;
        count = 0;
        this.probability = probability;
    }
    
    private int RandomLevel(double p) {
        int randomLevel;
        for (randomLevel = 0; random.NextDouble() < p && randomLevel < maxLevel; randomLevel++) {
        }
        return randomLevel;
    }

    public void Clear()
    {
        head = new SkipNode(default, -1);
        count = 0;
    }

    public bool Contains(T item)
    {
        if (count == 0)
        {
            return false;
        }
        
        var x = head;
        for (var i = level; i >= 0; i--) { 
            while (x.Forward[i] != null && x.Forward[i].Value.CompareTo(item) < 0)
            {
                x = x.Forward[i];
            }
        }
        x = x.Forward?[0];
        if (x != null && x.Value.CompareTo(item) == 0) { return true; }
        { return false; }
    }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index >= count)
            {
                throw new ArgumentOutOfRangeException();
            }

            var counter = 0;
            var x = head.Forward[0];
            while (counter != index)
            {
                x = x.Forward[0];
                counter++;
            }

            return x.Value!;
        }
        set => throw new NotImplementedException();
    }

    public void Add(T key) {
        var newLevel = RandomLevel(probability);
        if (newLevel > level) { 
            AdjustHead(newLevel);
        }
        var update = new SkipNode[level + 1];
        var x = head;
        for (var i = level; i >= 0; i--) {
            while (x.Forward?[i] != null && x.Forward[i].Value.CompareTo(key) < 0) {
                x = x.Forward[i];
            }
            update[i] = x;
        }
        x = new SkipNode(key, newLevel);
        for (var i = 0; i <= newLevel; i++) { 
            x.Forward[i] = update[i].Forward[i];
            update[i].Forward[i] = x;
        }
        count++;
    }

    private void AdjustHead(int newLevel) {
        var temp = head;
        head = new SkipNode(default, newLevel);
        for (var i = 0; i <= level; i++) {
            head.Forward[i] = temp.Forward[i];
        }
        level = newLevel;
    }
    
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

        if (array.Length - arrayIndex < count)
        {
            throw new ArgumentException();
        }

        var element = head.Forward[0];
        for (var counter = arrayIndex; counter < array.Length; counter++)
        {
            array[counter] = element.Value!;
            element = element.Forward[0];
        }
    }

    public void Insert(int index, T item)
    {
        throw new NotImplementedException();
    }

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

        count--;
        
        if (contains)
        {
            return !Contains(item);
        }
        
        return false;
    }
    
    public void RemoveAt(int index)
    {
        var removeElement = this[index];
        
        Remove(removeElement);
    }
    
    private class SkipNode {
        private T? value;
        public T? Value
        {
            get => value;
            set => this.value = value;
        }
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

    public SkipListEnum GetEnumerator()
    {
        return new SkipListEnum(this);
    }

    /// <summary>
    /// Class for implementing the IEnumerator<T> interface
    /// </summary>
    public class SkipListEnum : IEnumerator<T>
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