namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>
    /// <para>Compare the fraction part of <paramref name="value"/> to it's midpoint (i.e. its .5).</para>
    /// </summary>
    /// <typeparam name="TNumber"></typeparam>
    /// <param name="value">The value to be compared.</param>
    /// <returns>
    /// <para>The result is similar to that of the Compare/CompareTo functionality, but exactly -1, 0, or 1 is always returned.</para>
    /// <para>-1 if <paramref name="value"/> is less-than 0.5.</para>
    /// <para>0 if <paramref name="value"/> is equal-to 0.5.</para>
    /// <para>1 if <paramref name="value"/> is greater-than 0.5.</para>
    /// </returns>
    public static int CompareToFractionMidpoint<TNumber>(this TNumber value)
      where TNumber : System.Numerics.IFloatingPoint<TNumber>
      => value.CompareToFractionPercent(TNumber.CreateChecked(0.5));

    /// <summary>
    /// <para>Compares the fraction part of <paramref name="value"/> to the specified <paramref name="percent"/> and returns the sign of the result (i.e. -1 means less-than, 0 means equal-to, and 1 means greater-than).</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value">The value to be compared.</param>
    /// <param name="percent">Percent in the range [0, 1].</param>
    /// <returns>
    /// <para>The result is similar to that of the Compare/CompareTo functionality, but exactly -1, 0, or 1 is always returned.</para>
    /// <para>-1 when <paramref name="value"/> is less than <paramref name="percent"/>.</para>
    /// <para>0 when <paramref name="value"/> is equal to <paramref name="percent"/>.</para>
    /// <para>1 when <paramref name="value"/> is greater than <paramref name="percent"/>.</para>
    /// </returns>
    public static int CompareToFractionPercent<TValue>(this TValue value, TValue percent)
      where TValue : System.Numerics.IFloatingPoint<TValue>
      => (value - TValue.Floor(value)).CompareTo(percent);
  }
}
