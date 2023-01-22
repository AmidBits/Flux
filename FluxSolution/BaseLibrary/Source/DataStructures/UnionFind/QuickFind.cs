namespace Flux.DataStructures.UnionFind
{
  public sealed class QuickFind<T>
    : IUnionFind<T>
    where T : notnull
  {
    private int m_friendGroupCount;

    private readonly System.Collections.Generic.Dictionary<T, int> m_map = new();
    private readonly int[] m_sets;
    private readonly int[] m_sizes;
    private readonly System.Collections.Generic.List<T> m_values;

    public QuickFind(System.Collections.Generic.IEnumerable<T> values)
    {
      m_values = new(values);

      m_sets = new int[m_values.Count];
      m_sizes = new int[m_values.Count];

      for (var index = 0; index < m_values.Count; index++)
      {
        m_map.Add(m_values[index], index);
        m_sets[index] = index;
        m_sizes[index] = 1;
      }

      m_friendGroupCount = m_values.Count;
    }

    public int FriendGroupCount => m_friendGroupCount;
    public System.Collections.Generic.IReadOnlyList<T> Values => m_values;

    public bool AreConnected(T value, T otherValue) => m_sets[m_map[value]] == m_sets[m_map[otherValue]];

    public int Find(T value) => m_sets[m_map[value]];

    public bool Union(T value, T unionValue)
    {
      int newSet = m_sets[m_map[value]];
      int oldSet = m_sets[m_map[unionValue]];

      if (oldSet == newSet)
        return false;

      //if (AreConnected(value, unionValue))
      //  return false;

      foreach (T Value in m_values)
        if (m_sets[m_map[Value]] == oldSet)
          m_sets[m_map[Value]] = newSet;

      m_friendGroupCount--;

      m_sizes[newSet] += m_sizes[oldSet];
      m_sizes[oldSet] = 0;

      return true;
    }

    public override string ToString() => GetType().Name;
  }
}
