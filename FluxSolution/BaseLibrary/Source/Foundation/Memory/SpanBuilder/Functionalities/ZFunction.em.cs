namespace Flux
{
  public ref partial struct SpanBuilder<T>
  {
    /// <summary>The Z-function for this sequence is an array of length n where the i-th element is equal to the greatest number of elements starting from the position i that coincide with the first elements of source. I.e., z[i] is the length of the longest common prefix between source and the suffix of source starting at i.</summary>
    // https://cp-algorithms.com/string/z-function.html
    public int[] ZFunction(System.Collections.Generic.IEqualityComparer<T> equalityComparer)
    {
      var sourceLength = m_bufferPosition;

      var z = new int[sourceLength];

      for (int i = 1, l = 0, r = 0; i < sourceLength; i++)
      {
        if (i <= r)
          z[i] = System.Math.Min(r - i + 1, z[i - l]);

        while (i + z[i] < sourceLength && equalityComparer.Equals(m_buffer[z[i]], m_buffer[i + z[i]]))
          z[i]++;

        if (i + z[i] - 1 > r)
        {
          l = i;
          r = i + z[i] - 1;
        }
      }

      return z;
    }
    /// <summary>The Z-function for this string is an array of length n where the i-th element is equal to the greatest number of characters starting from the position i that coincide with the first characters of s. I.e., z[i] is the length of the longest common prefix between s and the suffix of s starting at i.</summary>
    public int[] ZFunction()
      => ZFunction(System.Collections.Generic.EqualityComparer<T>.Default);
  }
}
