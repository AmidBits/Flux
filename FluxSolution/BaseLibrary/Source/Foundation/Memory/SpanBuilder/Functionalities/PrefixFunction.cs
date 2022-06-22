namespace Flux
{
  public ref partial struct SpanBuilder<T>
  {
    /// <summary>The Prefix function for this sequence is an array of length n where the i-th element is equal to the greatest number of elements starting from the position i that coincide with the first elements of source. I.e., z[i] is the length of the longest common prefix between source and the suffix of source starting at i.</summary>
    // https://cp-algorithms.com/string/prefix-function.html
    public int[] PrefixFunction(System.Collections.Generic.IEqualityComparer<T> equalityComparer)
    {
      var p = new int[m_bufferPosition];

      for (int i = 1; i < m_bufferPosition; i++)
      {
        var j = p[i - 1];

        while (j > 0 && !equalityComparer.Equals(m_buffer[i], m_buffer[j]))
          j = p[j - 1];

        if (equalityComparer.Equals(m_buffer[i], m_buffer[j]))
          j++;

        p[i] = j;
      }

      return p;
    }
    /// <summary>The Z-function for this string is an array of length n where the i-th element is equal to the greatest number of characters starting from the position i that coincide with the first characters of s. I.e., z[i] is the length of the longest common prefix between s and the suffix of s starting at i.</summary>
    public int[] PrefixFunction()
      => PrefixFunction(System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
