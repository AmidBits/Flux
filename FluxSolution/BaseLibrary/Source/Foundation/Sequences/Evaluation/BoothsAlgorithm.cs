namespace Flux.Text
{
  /// <summary>Find the rotation of a sequence possessing the lowest lexicographical order of all such rotation.</summary>
  /// <see href="https://en.wikipedia.org/wiki/Lexicographically_minimal_string_rotation#Booth's_Algorithm"/>
  public class BoothsAlgorithm<T>
  {
    public System.Collections.Generic.IComparer<T> Comparer { get; }

    public BoothsAlgorithm(System.Collections.Generic.IComparer<T> comparer)
      => Comparer = comparer;
    public BoothsAlgorithm()
      : this(System.Collections.Generic.Comparer<T>.Default)
    { }

    /// <summary></summary>
    public int MinimalRotation(System.ReadOnlySpan<T> sequence)
    {
      var sequenceLength = sequence.Length;

      var s = new T[sequenceLength * 2]; // Double up the string, to avoid using index remainder.
      sequence.CopyTo(s);
      sequence.CopyTo(s, sequenceLength);

      var sLength = s.Length;

      var f = new int[sLength]; // Failure function.
      System.Array.Fill(f, -1);

      var k = 0; // Minimal rotation of string found so far.

      for (var j = 1; j < sLength; j++)
      {
        var sj = s[j];

        var i = f[j - k - 1];

        while (i != -1 && Comparer.Compare(sj, s[k + i + 1]) != 0)
        {
          if (Comparer.Compare(sj, s[k + i + 1]) < 0)
            k = j - i - 1;

          i = f[i];
        }

        if (Comparer.Compare(sj, s[k + i + 1]) != 0) // if sj != S[k+i+1], then i == -1
        {
          if (Comparer.Compare(sj, s[k]) < 0) // k+i+1 = k
            k = j;

          f[j - k] = -1;
        }
        else
          f[j - k] = i + 1;
      }

      return k;
    }
  }
}
