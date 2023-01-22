namespace Flux.DataStructures.UnionFind
{
  public sealed class QuickUnion<T>
    : IUnionFind<T>
    where T : notnull
  {
    private readonly System.Collections.Generic.Dictionary<T, int> m_map = new();
    private readonly int[] m_parents;
    private readonly System.Collections.Generic.List<T> m_values;

    public QuickUnion(System.Collections.Generic.IEnumerable<T> values)
    {
      m_values = new(values);

      m_parents = new int[Values.Count];

      for (var index = 0; index < Values.Count; index++)
      {
        m_map.Add(Values[index], index);
        m_parents[index] = index;
      }
    }

    public System.Collections.Generic.IReadOnlyList<T> Values => m_values;

    public bool AreConnected(T value, T otherValue) => Find(value) == Find(otherValue);

    public int Find(T value)
    {
      int currentValue = m_map[value];

      while (currentValue != m_parents[currentValue])
        currentValue = m_parents[currentValue];

      return currentValue;
    }

    public bool Union(T value, T oldValue)
    {
      if (AreConnected(value, oldValue))
        return false;

      m_parents[m_map[oldValue]] = m_map[value];

      return true;
    }

    public override string ToString() => GetType().Name;
  }
}
