namespace Flux
{
  public static partial class Maths
  {
#if NET7_0_OR_GREATER

    /// <summary>Snaps the value to zero if it's within the specified distance of zero, otherwise unaltered.</summary>
    public static TSelf DetentZero<TSelf>(this TSelf value, TSelf distance)
      where TSelf : System.Numerics.INumber<TSelf>
      => TSelf.Zero.EqualsWithinAbsoluteTolerance(value, distance)
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
