namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Compare the fraction part of <paramref name="value"/> to it's midpoint (i.e. 0.5).</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value">The value to be compared.</param>
    /// <returns>
    /// <para>-1 if <paramref name="value"/> is less than 0.5.</para>
    /// </returns>
    public static int CompareToFractionMidpoint<TValue>(this TValue value)
      where TValue : System.Numerics.IFloatingPoint<TValue>
      => value.CompareToFractionPercent(TValue.CreateChecked(0.5));

    /// <summary>
    /// <para>Compares the fraction part of <paramref name="value"/> to the specified <paramref name="percent"/> and returns the sign of the result (i.e. -1 means less-than, 0 means equal-to, and 1 means greater-than).</para>
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <param name="value">The value to be compared.</param>
    /// <param name="percent">Percent in the range [0, 1].</param>
    /// <returns>
    /// <para></para>
    /// <para>-1 when <paramref name="value"/> is less than <paramref name="percent"/>.</para>
    /// <para>0 when <paramref name="value"/> is equal to <paramref name="percent"/>.</para>
    /// <para>1 when <paramref name="value"/> is greater than <paramref name="percent"/>.</para>
    /// </returns>
    public static int CompareToFractionPercent<TValue>(this TValue value, TValue percent)
      where TValue : System.Numerics.IFloatingPoint<TValue>
      => (value - TValue.Floor(value)).CompareTo(percent);
  }
}
