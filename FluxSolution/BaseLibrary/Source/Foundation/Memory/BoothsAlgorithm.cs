namespace Flux
{
  /// <summary>Find the rotation of a sequence possessing the lowest lexicographical order of all such rotation.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Lexicographically_minimal_string_rotation#Booth's_Algorithm"/>
  public static class BoothsAlgorithm
  {
    /// <summary>Find the rotation of a sequence possessing the lowest lexicographical order of all such rotation. Uses the specified comparer.</summary>
    public static int FindMinimalRotationB<T>(ref this SpanBuilder<T> source, System.Collections.Generic.IComparer<T> comparer)
      where T : notnull
      => FindMinimalRotationB(source.AsReadOnlySpan(), comparer);
    /// <summary>Find the rotation of a sequence possessing the lowest lexicographical order of all such rotation. Uses the default comparer.</summary>
    public static int FindMinimalRotationB<T>(ref this SpanBuilder<T> source)
      where T : notnull
      => FindMinimalRotationB(source.AsReadOnlySpan());

    /// <summary>Find the rotation of a sequence possessing the lowest lexicographical order of all such rotation. Uses the specified comparer.</summary>
    public static int FindMinimalRotationB<T>(this System.ReadOnlySpan<T> source, System.Collections.Generic.IComparer<T> comparer)
    {
      var lengthX2 = source.Length * 2;

      var s = new T[lengthX2]; // Double up the string, to avoid modular arithmetic (using index remainder).
      source.CopyTo(s);
      source.CopyTo(s, source.Length);

      var f = new int[lengthX2]; System.Array.Fill(f, -1); // Failure function.

      var k = 0; // Minimal (least) rotation of string found so far.

      for (var j = 1; j < lengthX2; j++)
      {
        var sj = s[j];

        var i = f[j - k - 1];

        while (i != -1 && comparer.Compare(sj, s[k + i + 1]) != 0)
        {
          if (comparer.Compare(sj, s[k + i + 1]) < 0)
            k = j - i - 1;

          i = f[i];
        }

        if (comparer.Compare(sj, s[k + i + 1]) != 0) // if sj != S[k + i + 1], then i == -1
        {
          if (comparer.Compare(sj, s[k]) < 0) // k + i + 1 = k
            k = j;

          f[j - k] = -1;
        }
        else
          f[j - k] = i + 1;
      }

      return k;
    }
    /// <summary>Find the rotation of a sequence possessing the lowest lexicographical order of all such rotation. Uses the default comparer.</summary>
    public static int FindMinimalRotationB<T>(this System.ReadOnlySpan<T> source)
      => FindMinimalRotationB(source, System.Collections.Generic.Comparer<T>.Default);
  }

  //public ref partial struct SpanBuilder<T>
  //{
  //  /// <summary>Find the rotation of a sequence possessing the lowest lexicographical order of all such rotation. Uses the specified comparer.</summary>
  //  /// <see href="https://en.wikipedia.org/wiki/Lexicographically_minimal_string_rotation#Booth's_Algorithm"/>
  //  public int MinimalRotationB(System.Collections.Generic.IComparer<T> comparer)
  //  {
  //    var lengthX2 = m_bufferPosition * 2;

  //    var s = new T[lengthX2]; // Double up the string, to avoid modular arithmetic (using index remainder).
  //    AsReadOnlySpan().CopyTo(s);
  //    AsReadOnlySpan().CopyTo(s, m_bufferPosition);

  //    var f = new int[lengthX2]; System.Array.Fill(f, -1); // Failure function.

  //    var k = 0; // Minimal (least) rotation of string found so far.

  //    for (var j = 1; j < lengthX2; j++)
  //    {
  //      var sj = s[j];

  //      var i = f[j - k - 1];

  //      while (i != -1 && comparer.Compare(sj, s[k + i + 1]) != 0)
  //      {
  //        if (comparer.Compare(sj, s[k + i + 1]) < 0)
  //          k = j - i - 1;

  //        i = f[i];
  //      }

  //      if (comparer.Compare(sj, s[k + i + 1]) != 0) // if sj != S[k + i + 1], then i == -1
  //      {
  //        if (comparer.Compare(sj, s[k]) < 0) // k + i + 1 = k
  //          k = j;

  //        f[j - k] = -1;
  //      }
  //      else
  //        f[j - k] = i + 1;
  //    }

  //    return k;
  //  }
  //  /// <summary>Find the rotation of a sequence possessing the lowest lexicographical order of all such rotation. Uses the default comparer.</summary>
  //  /// <see href="https://en.wikipedia.org/wiki/Lexicographically_minimal_string_rotation#Booth's_Algorithm"/>
  //  public int MinimalRotationB()
  //    => MinimalRotationB(System.Collections.Generic.Comparer<T>.Default);
  //}
}
