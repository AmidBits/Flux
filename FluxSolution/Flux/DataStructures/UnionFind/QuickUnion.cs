namespace Flux.DataStructures.UnionFind
{
  public sealed class QuickUnion<TKey>
    : IUnionFind<TKey>
    where TKey : notnull
  {
    private readonly System.Collections.Generic.Dictionary<TKey, int> m_map = [];
    private readonly int[] m_parents;
    private readonly System.Collections.Generic.List<TKey> m_values;

    public QuickUnion(System.Collections.Generic.IEnumerable<TKey> values)
    {
      m_values = new(values);

      m_parents = new int[Values.Count];

      for (var index = 0; index < Values.Count; index++)
      {
        m_map.Add(Values[index], index);
        m_parents[index] = index;
      }
    }

    public System.Collections.Generic.IReadOnlyList<TKey> Values => m_values;

    public bool AreConnected(TKey value, TKey otherValue) => Find(value) == Find(otherValue);

    public int Find(TKey value)
    {
      int currentValue = m_map[value];

      while (currentValue != m_parents[currentValue])
        currentValue = m_parents[currentValue];

      return currentValue;
    }

    public bool Union(TKey value, TKey oldValue)
    {
      if (AreConnected(value, oldValue))
        return false;

      m_parents[m_map[oldValue]] = m_map[value];

      return true;
    }

    public override string ToString() => GetType().Name;
  }
}
