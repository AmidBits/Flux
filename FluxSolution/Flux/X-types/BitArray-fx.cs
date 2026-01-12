namespace Flux
{
  public static partial class BitArrayExtensions
  {
    /// <summary>
    /// <para>Returns the indices of the bits that are either true (1) or false (0) depending on the <paramref name="isOne"/>.</para>
    /// </summary>
    public static System.Collections.Generic.IEnumerable<long> GetIndicesOfBits(this System.Collections.BitArray source, bool isOne)
    {
      using var e = source.Cast<bool>().GetEnumerator();

      for (var index = 0L; e.MoveNext(); index++)
        if (e.Current == isOne)
          yield return index;
    }
  }
}
