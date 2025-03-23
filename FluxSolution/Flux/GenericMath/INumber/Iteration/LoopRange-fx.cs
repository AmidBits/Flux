namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>
    /// <para>Creates a sequence of <paramref name="count"/> numbers, starting at <paramref name="source"/>, spaced by <paramref name="stepSize"/>.</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <typeparam name="TCount"></typeparam>
    /// <param name="source"></param>
    /// <param name="stepSize"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static System.Collections.Generic.IEnumerable<TNumber> LoopRange<TNumber, TCount>(this TNumber source, TNumber stepSize, TCount count)
      where TNumber : System.Numerics.INumber<TNumber>
      where TCount : System.Numerics.IBinaryInteger<TCount>
    {
      System.ArgumentOutOfRangeException.ThrowIfZero(stepSize);
      System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(count);

      for (var index = TCount.Zero; index < count; index++)
        yield return source + TNumber.CreateChecked(index) * stepSize;
    }
  }
}
