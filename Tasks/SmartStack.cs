using System.Collections;

public class SmartStack<T> : IEnumerable<T>
{
    private T[] _stack;
    public const int DefaultCapacity = 4;
    private int _capacity = 0;
    private int _top = -1;
    public SmartStack()
    {
        _stack = new T[DefaultCapacity];
        _capacity = DefaultCapacity;
    }

    public SmartStack(int n)
    {
        _stack = new T[n];
        _capacity = n;
    }

    public SmartStack(IEnumerable<T> collection)
    {
        foreach (T item in collection)
        {
            _capacity++;
        }

        if (_capacity == 0) { _capacity = DefaultCapacity; }

        _stack = new T[_capacity];

        PushRange(collection);
    }

    public void Push(T element)
    {
        if (_top + 1 == _capacity)
        {
            Resize();
        }

        _top++;
        _stack[_top] = element;
         
    }

    public void PushRange(IEnumerable<T> collection)
    {
        foreach (T item in collection)
        {
            Push(item);
        }
    }

    public T Pop()
    {
        if (IsEmpty()) {
            throw new InvalidOperationException();
        }
        T item = _stack[_top];
        //csharp_prefer_simple_default_expression = true
        _stack[_top] = default;
        _top--;

        return item;

    }

    public T Peek()
    {
        if (IsEmpty())
        {
            throw new InvalidOperationException();
        }
        T item = _stack[_top];

        return item;
    }

    public bool Contains(T el)
    {
        for (int i = 0; i <= _top; i++)
        {
            if (Equals(el, _stack[i]))
            {
                return true;
            }
        }

        return false;
    }

    public int Count
    {
        get { return _top + 1; }
    }

    public int Capacity
    {
        get { return _capacity; }
    }

    public void Resize()
    {
        _capacity *= 2;

        T[] newArray = new T[_capacity];
        Array.Copy(_stack, 0, newArray, 0, Count);

        _stack = newArray;
    }

    public bool IsEmpty()
    {
        return _top == -1;
    }

    public T this[int index]
    {
        get
        {
            if (index < 0 || index > _top)
            {
                throw new ArgumentOutOfRangeException();
            }

            return _stack[_top - index];
        }

        set
        {
            if (index < 0 || index > _top)
            {
                throw new ArgumentOutOfRangeException();
            }
            _stack[_top - index] = value;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (int i = _top; i >= 0; i--)
        {
            yield return _stack[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

}
