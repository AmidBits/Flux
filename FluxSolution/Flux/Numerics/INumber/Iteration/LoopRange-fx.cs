namespace Flux
{
  public static partial class Number
  {
    /// <summary>
    /// <para>Creates a new sequence of <paramref name="count"/> numbers (or as many as possible) using <typeparamref name="TNumber"/> starting at <paramref name="source"/> and spaced by <paramref name="stepSize"/>.</para>
    /// <para>If <typeparamref name="TNumber"/> overflows/underflows during enumeration, there may be less than <paramref name="count"/> numbers returned.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <typeparam name="TCount"></typeparam>
    /// <param name="source"></param>
    /// <param name="stepSize"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<TNumber> LoopRange<TNumber, TCount>(this TNumber source, TNumber stepSize, TCount count)
      where TNumber : System.Numerics.INumber<TNumber>
      where TCount : System.Numerics.IBinaryInteger<TCount>
    {
      System.ArgumentOutOfRangeException.ThrowIfZero(stepSize);
      System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);

      TNumber number;

      for (var index = TCount.Zero; index < count; index++)
      {
        try { number = checked(source + TNumber.CreateChecked(index) * stepSize); }
        catch { break; }

        yield return number;
      }
    }
  }
}
