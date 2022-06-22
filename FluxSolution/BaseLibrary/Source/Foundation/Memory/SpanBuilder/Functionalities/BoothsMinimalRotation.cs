namespace Flux
{
  public ref partial struct SpanBuilder<T>
  {
    /// <summary>Find the rotation of a sequence possessing the lowest lexicographical order of all such rotation. Uses the specified comparer.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Lexicographically_minimal_string_rotation#Booth's_Algorithm"/>
    public int BoothsMinimalRotation(System.Collections.Generic.IComparer<T> comparer)
    {
      var sourceLength = m_bufferPosition;

      var s = new T[sourceLength * 2]; // Double up the string, to avoid using index remainder.
      CopyTo(s);
      CopyTo(s, sourceLength);

      var sLength = s.Length;

      var f = new int[sLength]; // Failure function.
      System.Array.Fill(f, -1);

      var k = 0; // Minimal rotation of string found so far.

      for (var j = 1; j < sLength; j++)
      {
        var sj = s[j];

        var i = f[j - k - 1];

        while (i != -1 && comparer.Compare(sj, s[k + i + 1]) != 0)
        {
          if (comparer.Compare(sj, s[k + i + 1]) < 0)
            k = j - i - 1;

          i = f[i];
        }

        if (comparer.Compare(sj, s[k + i + 1]) != 0) // if sj != S[k+i+1], then i == -1
        {
          if (comparer.Compare(sj, s[k]) < 0) // k+i+1 = k
            k = j;

          f[j - k] = -1;
        }
        else
          f[j - k] = i + 1;
      }

      return k;
    }
    /// <summary>Find the rotation of a sequence possessing the lowest lexicographical order of all such rotation. Uses the default comparer.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Lexicographically_minimal_string_rotation#Booth's_Algorithm"/>
    public int BoothsMinimalRotation()
      => BoothsMinimalRotation(System.Collections.Generic.Comparer<T>.Default);
  }
}
