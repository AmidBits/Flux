namespace Flux
{
  public static partial class Maths
  {
#if NET7_0_OR_GREATER

    /// <summary>
    /// <para>Snaps the <paramref name="value"/> to zero if it's within the specified <paramref name="proximity"/> of zero, otherwise unaltered.</para>
    /// </summary>
    /// <remarks>This is similar to a knob that has a notch which latches the knob at the zero position.</remarks>
    public static TSelf DetentZero<TSelf>(this TSelf value, TSelf proximity)
      where TSelf : System.Numerics.INumber<TSelf>
      => TSelf.Zero.EqualsWithinAbsoluteTolerance(value, proximity)
      ? TSelf.Zero // Detent to zero.
      : value;

#else

    /// <summary>Snaps the value to zero if it's within the specified distance of zero.</summary>
    public static System.Numerics.BigInteger DetentZero(this System.Numerics.BigInteger value, System.Numerics.BigInteger distance)
      => value < -distance || value > distance
      ? value
      : 0;

    /// <summary>Snaps the value to zero if it's within the specified distance of zero.</summary>
    public static decimal DetentZero(this decimal value, decimal distance)
      => value < -distance || value > distance
      ? value
      : 0;

    /// <summary>Snaps the value to zero if it's within the specified distance of zero.</summary>
    public static float DetentZero(this float value, float distance)
      => value < -distance || value > distance
      ? value
      : 0F;

    /// <summary>Snaps the value to zero if it's within the specified distance of zero.</summary>
    public static double DetentZero(this double value, double distance)
      => value < -distance || value > distance
      ? value
      : 0;

    /// <summary>Snaps the value to zero if it's within the specified distance of zero.</summary>
    public static int DetentZero(this int value, int distance)
      => value < -distance || value > distance
      ? value
      : 0;

    /// <summary>Snaps the value to zero if it's within the specified distance of zero.</summary>
    public static long DetentZero(this long value, long distance)
      => value < -distance || value > distance
      ? value
      : 0;

    /// <summary>Snaps the value to zero if it's within the specified distance of zero.</summary>
    [System.CLSCompliant(false)]
    public static uint DetentZero(this uint value, uint distance)
      => value > distance
      ? value
      : 0;

    /// <summary>Snaps the value to zero if it's within the specified distance of zero.</summary>
    [System.CLSCompliant(false)]
    public static ulong DetentZero(this ulong value, ulong distance)
      => value > distance
      ? value
      : 0;

#endif
  }
}
