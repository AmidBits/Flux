namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Find the rotation of a sequence possessing the lowest lexicographical order of all such rotation. Uses the specified comparer.</para>
    /// <see href="https://en.wikipedia.org/wiki/Lexicographically_minimal_string_rotation#Booth's_Algorithm"/>
    /// </summary>
    public static int BoothsFindMinimalRotation<T>(this System.ReadOnlySpan<T> source, System.Collections.Generic.IComparer<T>? comparer = null)
    {
      comparer ??= System.Collections.Generic.Comparer<T>.Default;

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
  }
}
