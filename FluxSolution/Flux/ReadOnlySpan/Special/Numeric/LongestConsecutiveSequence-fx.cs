namespace Flux
{
  public static partial class LongestConsecutiveSequence
  {
    extension<TInteger>(System.ReadOnlySpan<TInteger> source)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      /// <summary>
      /// <para>Finds the longest consecutive sequence.</para>
      /// </summary>
      /// <param name="equalityComparer"></param>
      /// <returns></returns>
      public int LongestConsecutiveSequenceLength(System.Collections.Generic.IEqualityComparer<TInteger>? equalityComparer = null)
        => source.ToHashSet(equalityComparer).LongestConsecutiveSequenceLength();
    }
  }
}
