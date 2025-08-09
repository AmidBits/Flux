namespace Flux
{
  public static partial class Number
  {
    /// <summary>
    /// <para>Creates a new sequence with as many numbers as possible using <typeparamref name="TNumber"/> starting at <paramref name="source"/> and spaced by <paramref name="stepSize"/>.</para>
    /// <para>When <typeparamref name="TNumber"/> overflows/underflows, the enumeration is terminated.</para>
    /// </summary>
    /// <remarks>Please note! If <typeparamref name="TNumber"/> is unlimited in nature (e.g. <see cref="System.Numerics.BigInteger"/>) enumeration is indefinite to the extent of system resources.</remarks>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="source"></param>
    /// <param name="stepSize"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<TNumber> LoopVerge<TNumber>(this TNumber source, TNumber stepSize)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      System.ArgumentOutOfRangeException.ThrowIfZero(stepSize);

      checked
      {
        TNumber number;

        for (var index = TNumber.Zero; ; index++)
        {
          try { number = source + index * stepSize; }
          catch { break; }

          yield return number;
        }
      }
    }
  }
}
