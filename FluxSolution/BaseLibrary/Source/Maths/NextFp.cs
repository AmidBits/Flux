namespace Flux
{
  public static partial class Maths
  {
    /// <summary>
    /// <para>Computes the next real number from <paramref name="value"/> towards-zero and away-from-zero.</para>
    /// </summary>
    /// <remarks>
    /// <para>If <paramref name="value"/> is positive the next values are (less-than-<paramref name="value"/>, greater-than-<paramref name="value"/>).</para>
    /// <para>If <paramref name="value"/> is negative the next values are (greater-than-<paramref name="value"/>, less-than-<paramref name="value"/>).</para>
    /// </remarks>
    public static (TSelf nextFpTowardsZero, TSelf nextFpAwayFromZero) NextFp<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => TSelf.IsNegative(value)
      ? (NextFpGreaterThan(value), NextFpLessThan(value))
      : (NextFpLessThan(value), NextFpGreaterThan(value));

    /// <summary>
    /// <para>Computes the next real value greater-than <paramref name="value"/>.</para>
    /// </summary>
    public static TSelf NextFpGreaterThan<TSelf>(TSelf value)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => TSelf.BitIncrement(value);

    /// <summary>
    /// <para>Computes the next floating-point value less-than <paramref name="value"/>.</para>
    /// </summary>
    public static TSelf NextFpLessThan<TSelf>(TSelf value)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => TSelf.BitDecrement(value);
  }
}
