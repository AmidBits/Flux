namespace Flux
{
  public static partial class BitArrayExtensions
  {
    /// <summary>
    /// <para>Provides extension methods for the <see cref="System.Collections.BitArray"/> type.</para>
    /// </summary>
    /// <param name="source">The <see cref="System.Collections.BitArray"/> instance to operate on.</param>
    extension(System.Collections.BitArray source)
    {
      /// <summary>
      /// <para>Returns the indices of the bits that are either true (1) or false (0) depending on the <paramref name="eitherTrueOrFalse"/>.</para>
      /// </summary>
      /// <param name="eitherTrueOrFalse"></param>
      /// <returns></returns>
      public System.Collections.Generic.IEnumerable<long> GetIndicesOfBits(bool eitherTrueOrFalse)
      {
        using var e = source.Cast<bool>().GetEnumerator();

        for (var index = 0L; e.MoveNext(); index++)
          if (e.Current == eitherTrueOrFalse)
            yield return index;
      }
    }
  }
}
