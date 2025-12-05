namespace Flux.DataStructures
{
  /// <summary>
  /// <para>This is not fully implemented yet.</para>
  /// <para>A bidirectional dictionary allowing fast lookups in both directions.</para>
  /// </summary>
  [System.Obsolete("Not obsolete, but not fully implemented yet.", true)]
  public class BidirectionalDictionary<T1, T2>
    where T1 : notnull
    where T2 : notnull
  {
    private readonly System.Collections.Generic.Dictionary<T1, System.Collections.Generic.HashSet<T2>> m_forward = [];
    private readonly System.Collections.Generic.Dictionary<T2, System.Collections.Generic.HashSet<T1>> m_reverse = [];

    /// <summary>
    /// <para>Gets the number of forward entries.</para>
    /// </summary>
    public int CountForward => m_forward.Count;

    /// <summary>
    /// <para>Gets the number of reverse entries.</para>
    /// </summary>
    public int CountReverse => m_reverse.Count;

    /// <summary>
    /// <para>Clears the bidirectional dictionary.</para>
    /// </summary>
    public void Clear()
    {
      m_forward.Clear();
      m_reverse.Clear();
    }

    /// <summary>
    /// <para>Adds a new pair to a <see cref="BidirectionalDictionary{T1, T2}"/>.</para>
    /// </summary>
    public void Add(T1 forwardKey, T2 reverseKey)
    {
      AddForward(forwardKey, reverseKey);
      AddReverse(reverseKey, forwardKey);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="forwardKey"></param>
    /// <param name="reverseKey"></param>
    public void AddForward(T1 forwardKey, T2 reverseKey)
    {
      if (m_forward.TryGetValue(forwardKey, out var value1)) value1.Add(reverseKey);
      else value1 = [reverseKey];

      m_forward[forwardKey] = value1;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="reverseKey"></param>
    /// <param name="forwardKey"></param>
    public void AddReverse(T2 reverseKey, T1 forwardKey)
    {
      if (m_reverse.TryGetValue(reverseKey, out var value2)) value2.Add(forwardKey);
      else value2 = [forwardKey];

      m_reverse[reverseKey] = value2;
    }

    /// <summary>
    /// <para>Gets a value set associated with key1 in a <see cref="BidirectionalDictionary{T1, T2}"/>.</para>
    /// </summary>
    public bool TryGetForwardValue(T1 forwardKey, out System.Collections.Generic.ISet<T2> forwardValue)
    {
      var found = m_forward.TryGetValue(forwardKey, out var value1);

      forwardValue = value1!;

      return found;
    }

    /// <summary>
    /// <para>Gets a value set associated with key2 in a <see cref="BidirectionalDictionary{T1, T2}"/>.</para>
    /// </summary>
    public bool TryGetReverseValue(T2 reverseKey, out System.Collections.Generic.ISet<T1> reverseValue)
    {
      var found = m_reverse.TryGetValue(reverseKey, out var value1);

      reverseValue = value1!;

      return found;
    }

    /// <summary>
    /// <para>Removes an entry by the first key.</para>
    /// </summary>
    public bool TryRemoveForward(T1 forwardKey, out System.Collections.Generic.ISet<T2> forwardValue)
    {
      if (m_forward.TryGetValue(forwardKey, out var value1))
      {
        m_forward.Remove(forwardKey);

        forwardValue = value1!;

        return true;
      }

      forwardValue = null!;

      return false;
    }

    /// <summary>
    /// Removes an entry by the second key.
    /// </summary>
    public bool TryRemoveReverse(T2 reverseKey, out System.Collections.Generic.ISet<T1> reverseValue)
    {
      if (m_reverse.TryGetValue(reverseKey, out var value2))
      {
        m_reverse.Remove(reverseKey);

        reverseValue = value2!;

        return true;
      }

      reverseValue = null!;

      return false;
    }
  }
}
