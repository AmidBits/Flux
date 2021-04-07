namespace Flux.Collections.Generic.UnionFind
{
  public class QuickFind<T>
    : IUnionFind<T>
    where T : notnull
  {
    private readonly System.Collections.Generic.Dictionary<T, int> m_map = new System.Collections.Generic.Dictionary<T, int>();
    private readonly int[] m_sets;
    private readonly int[] m_sizes;

    public int FriendGroupCount { get; private set; }
    public System.Collections.Generic.List<T> Values { get; } = new System.Collections.Generic.List<T>();

    public QuickFind(System.Collections.Generic.List<T> values)
    {
      Values.AddRange(values);

      m_sets = new int[Values.Count];
      m_sizes = new int[Values.Count];

      for (var index = 0; index < Values.Count; index++)
      {
        m_map.Add(Values[index], index);
        m_sets[index] = index;
        m_sizes[index] = 1;
      }

      FriendGroupCount = Values.Count;
    }

    public bool AreConnected(T value, T otherValue)
      => m_sets[m_map[value]] == m_sets[m_map[otherValue]];

    public int Find(T value)
      => m_sets[m_map[value]];

    public bool Union(T value, T unionValue)
    {
      int newSet = m_sets[m_map[value]];
      int oldSet = m_sets[m_map[unionValue]];

      if (oldSet == newSet)
        return false;

      //if (AreConnected(value, unionValue))
      //  return false;

      foreach (T Value in Values)
        if (m_sets[m_map[Value]] == oldSet)
          m_sets[m_map[Value]] = newSet;

      FriendGroupCount--;

      m_sizes[newSet] += m_sizes[oldSet];
      m_sizes[oldSet] = 0;

      return true;
    }
  }
}
