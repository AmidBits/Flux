namespace Flux
{
  public static partial class Fx
  {
    /// <summary>The Z-function for this sequence is an array of length n where the i-th element is equal to the greatest number of elements starting from the position i that coincide with the first elements of source. I.e., z[i] is the length of the longest common prefix between source and the suffix of source starting at i.</summary>
    // https://cp-algorithms.com/string/z-function.html
    public static int[] ZFunction(this System.Text.StringBuilder source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var z = new int[source.Length];

      for (int i = 1, l = 0, r = 0; i < source.Length; i++)
      {
        if (i <= r)
          z[i] = System.Math.Min(r - i + 1, z[i - l]);

        while (i + z[i] < source.Length && source[z[i]] == source[i + z[i]])
          z[i]++;

        if (i + z[i] - 1 > r)
        {
          l = i;
          r = i + z[i] - 1;
        }
      }

      return z;
    }
  }
}
