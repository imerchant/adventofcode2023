using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode2023;

[ExcludeFromCodeCoverage]
public class DefaultDictionary<TKey, TValue> : IDictionary<TKey, TValue> where TKey : notnull
{
    private readonly Func<TKey, TValue> _defaultFactory;
    private readonly Dictionary<TKey, TValue> _backingDictionary;

    public TValue this[TKey key]
    {
        get
        {
            var containsKey = _backingDictionary.ContainsKey(key);
            if (!containsKey)
            {
                _backingDictionary[key] = _defaultFactory(key);
            }
            return _backingDictionary[key];
        }
        set => _backingDictionary[key] = value;
    }

    public ICollection<TKey> Keys => _backingDictionary.Keys;

    public ICollection<TValue> Values => _backingDictionary.Values;

    public int Count => _backingDictionary.Count;

    public bool IsReadOnly { get; } = false;

    public DefaultDictionary(Func<TValue> defaultFactory) : this(0, _ => defaultFactory())
    {
    }

    public DefaultDictionary(int capacity, Func<TValue> defaultFactory) : this(capacity, _ => defaultFactory())
    {
    }

    public DefaultDictionary(Func<TKey, TValue> defaultFactory) : this(0, defaultFactory)
    {
    }

    public DefaultDictionary(int capacity, Func<TKey, TValue> defaultFactory)
    {
        _defaultFactory = defaultFactory ?? throw new ArgumentNullException(nameof(defaultFactory));
        _backingDictionary = new Dictionary<TKey, TValue>(capacity);
    }

    public void Add(TKey key, TValue value)
    {
        _backingDictionary.Add(key, value);
    }

    public void Add(KeyValuePair<TKey, TValue> item)
    {
        ((IDictionary<TKey, TValue>)_backingDictionary).Add(item);
    }

    public void Clear()
    {
        _backingDictionary.Clear();
    }

    public bool Contains(KeyValuePair<TKey, TValue> item)
    {
        return ((IDictionary<TKey, TValue>)_backingDictionary).Contains(item);
    }

    public bool ContainsKey(TKey key)
    {
        return _backingDictionary.ContainsKey(key);
    }

    public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
    {
        ((IDictionary<TKey, TValue>)_backingDictionary).CopyTo(array, arrayIndex);
    }

    public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
    {
        return _backingDictionary.GetEnumerator();
    }

    public bool Remove(TKey key)
    {
        return _backingDictionary.Remove(key);
    }

    public bool Remove(KeyValuePair<TKey, TValue> item)
    {
        return ((IDictionary<TKey, TValue>)_backingDictionary).Remove(item);
    }

    public bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value)
    {
        var result = _backingDictionary.TryGetValue(key, out value);
        if (!result)
        {
            _backingDictionary[key] = value = _defaultFactory(key);
        }
        return result;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
