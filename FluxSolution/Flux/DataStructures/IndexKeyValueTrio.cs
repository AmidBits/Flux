namespace Flux.DataStructures
{
  /// <summary>
  /// <para>This is a three part structure (index, key and value) comparable to the two part structure <see cref="System.Collections.Generic.KeyValuePair{TKey, TValue}"/>.</para>
  /// </summary>
  /// <typeparam name="TKey"></typeparam>
  /// <typeparam name="TValue"></typeparam>
  public readonly record struct IndexKeyValueTrio<TKey, TValue>
    where TKey : notnull
  {
    private readonly int m_index;
    private readonly TKey m_key;
    private readonly TValue m_value;

    public IndexKeyValueTrio(int index, TKey key, TValue value)
    {
      m_index = index;
      m_key = key;
      m_value = value;
    }

    public readonly void Deconstruct(out int index, out TKey key, out TValue value)
    {
      index = m_index;
      key = m_key;
      value = m_value;
    }

    /// <summary>
    /// <para>Gets the index in the index/key/value trio.</para>
    /// </summary>
    public int Index => m_index;

    /// <summary>
    /// <para>Gets the key in the index/key/value trio.</para>
    /// </summary>
    public TKey Key => m_key;

    /// <summary>
    /// <para>Gets the value in the index/key/value trio.</para>
    /// </summary>
    public TValue Value => m_value;

    /// <summary>
    /// <para>Creates a new <see cref="System.Collections.Generic.KeyValuePair{TKey, TValue}"/> from the <see cref="IndexKeyValueTrio{TKey, TValue}"/>.</para>
    /// </summary>
    /// <returns></returns>
    public System.Collections.Generic.KeyValuePair<TKey, TValue> ToKeyValuePair() => new(m_key, m_value);
  }
}
