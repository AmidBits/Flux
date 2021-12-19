namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Find the rotation of a sequence possessing the lowest lexicographical order of all such rotation. Uses the specified comparer.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Lexicographically_minimal_string_rotation#Booth's_Algorithm"/>
    public static int BoothsMinimalRotation<T>(this System.ReadOnlySpan<T> source, System.Collections.Generic.IComparer<T> comparer)
    {
      var sourceLength = source.Length;

      var s = new T[sourceLength * 2]; // Double up the string, to avoid using index remainder.
      source.CopyTo(s);
      source.CopyTo(s, sourceLength);

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
    public static int BoothsMinimalRotation<T>(this System.ReadOnlySpan<T> source)
      => BoothsMinimalRotation(source, System.Collections.Generic.Comparer<T>.Default);
  }
}
