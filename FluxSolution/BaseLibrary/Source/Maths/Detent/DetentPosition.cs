namespace Flux
{
  public static partial class Maths
  {
#if NET7_0_OR_GREATER

    /// <summary>
    /// <para>Snaps a <paramref name="value"/> to a <paramref name="position"/> if it's within the specified <paramref name="proximity"/> of the <paramref name="position"/>, otherwise unaltered.</para>
    /// </summary>
    /// <remarks>This is similar to a knob that has a notch which latches the knob at a certain position.</remarks>
    public static TSelf DetentPosition<TSelf>(this TSelf value, TSelf position, TSelf proximity)
      where TSelf : System.Numerics.INumber<TSelf>
      => position.EqualsWithinAbsoluteTolerance(value, proximity)
      ? position // Detent to the position.
      : value;

#else

    /// <summary>Determines whether a value is within a specified distance of the position, and if so, snaps the value to the position.</summary>
    public static System.Numerics.BigInteger DetentPosition(this System.Numerics.BigInteger value, System.Numerics.BigInteger position, System.Numerics.BigInteger distance)
      => System.Numerics.BigInteger.Abs(position - value) > System.Numerics.BigInteger.Abs(distance)
      ? value
      : position;

    /// <summary>Snaps the value to the position if it's within the specified distance of the position.</summary>
    public static decimal DetentPosition(this decimal value, decimal position, decimal distance)
      => System.Math.Abs(position - value) > System.Math.Abs(distance)
      ? value
      : position;

    /// <summary>Snaps the value to the position if it's within the specified distance of the position.</summary>
    public static float DetentPosition(this float value, float position, float distance)
      => System.Math.Abs(position - value) > System.Math.Abs(distance)
      ? value
      : position;

    /// <summary>Snaps the value to the position if it's within the specified distance of the position.</summary>
    public static double DetentPosition(this double value, double position, double distance)
      => System.Math.Abs(position - value) > System.Math.Abs(distance)
      ? value
      : position;

    /// <summary>Determines whether a value is within a specified positive distance of the position, and if so, snaps the value to the position.</summary>
    public static int DetentPosition(this int value, int position, int distance)
      => System.Math.Abs(position - value) > System.Math.Abs(distance)
      ? value
      : position;

    /// <summary>Determines whether a value is within a specified distance of the position, and if so, snaps the value to the position.</summary>
    public static long DetentPosition(this long value, long position, long distance)
      => System.Math.Abs(position - value) > System.Math.Abs(distance)
      ? value
      : position;

    /// <summary>Determines whether a value is within a specified distance of the position, and if so, snaps the value to the position.</summary>
    [System.CLSCompliant(false)]
    public static uint DetentPosition(this uint value, uint position, uint distance)
      => value >= position - distance && value <= position + distance
      ? position
      : value;

    /// <summary>Determines whether a value is within a specified distance of the position, and if so, snaps the value to the position.</summary>
    [System.CLSCompliant(false)]
    public static ulong DetentPosition(this ulong value, ulong position, ulong distance)
      => value >= position - distance && value <= position + distance
      ? position
      : value;

#endif
  }
}
