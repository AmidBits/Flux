namespace Flux
{
  public static partial class ExtensionMethodsReadOnlySpan
  {
    /// <summary>The Prefix function for this sequence is an array of length n where the i-th element is equal to the greatest number of elements starting from the position i that coincide with the first elements of source. I.e., z[i] is the length of the longest common prefix between source and the suffix of source starting at i.</summary>
    // https://cp-algorithms.com/string/prefix-function.html
    public static int[] PrefixFunction<T>(this System.ReadOnlySpan<T> source, System.Collections.Generic.IEqualityComparer<T>? equalityComparer = null)
    {
      equalityComparer ??= System.Collections.Generic.EqualityComparer<T>.Default;

      var sourceLength = source.Length;

      var p = new int[sourceLength];

      for (int i = 1; i < sourceLength; i++)
      {
        var j = p[i - 1];

        while (j > 0 && !equalityComparer.Equals(source[i], source[j]))
          j = p[j - 1];

        if (equalityComparer.Equals(source[i], source[j]))
          j++;

        p[i] = j;
      }

      return p;
    }
  }
}