namespace Flux
{
  public static partial class Maths
  {
#if NET7_0_OR_GREATER

    /// <summary>
    /// <para>Snaps the <paramref name="value"/> to the nearest <paramref name="interval"/> if it's within the specified <paramref name="proximity"/> of an <paramref name="interval"/> position, otherwise unaltered.</para>
    /// </summary>
    /// <remarks>This is similar to a knob that has notches which latches the knob at certain positions.</remarks>
    public static TSelf DetentInterval<TSelf>(this TSelf value, TSelf interval, TSelf proximity)
      where TSelf : System.Numerics.INumber<TSelf>
      => TSelf.CreateChecked(int.CreateChecked(value / interval)) * interval is var tzInterval && TSelf.Abs(tzInterval - value) <= proximity
      ? tzInterval
      : tzInterval + interval is var afzInterval && TSelf.Abs(afzInterval - value) <= proximity
      ? afzInterval
      : value;

#else

    /// <summary>Snaps the value to the nearest interval if it's within the specified distance of an interval.</summary>
    public static System.Numerics.BigInteger DetentInterval(this System.Numerics.BigInteger value, in System.Numerics.BigInteger interval, in System.Numerics.BigInteger distance)
      => (value / interval) * interval is var tzInterval && System.Numerics.BigInteger.Abs(tzInterval - value) <= distance
      ? tzInterval
      : tzInterval + interval is var afzInterval && System.Numerics.BigInteger.Abs(afzInterval - value) <= distance
      ? afzInterval
      : value;

    /// <summary>Snaps the value to the nearest interval if it's within the specified distance of an interval.</summary>
    public static decimal DetentInterval(this decimal value, in decimal interval, in decimal distance)
      => (value / interval) * interval is var tzInterval && System.Math.Abs(tzInterval - value) <= distance
      ? tzInterval
      : tzInterval + interval is var afzInterval && System.Math.Abs(afzInterval - value) <= distance
      ? afzInterval
      : value;

    /// <summary>Snaps the value to the nearest interval if it's within the specified distance of an interval.</summary>
    public static float DetentInterval(this float value, in float interval, in float distance)
      => (value / interval) * interval is var tzInterval && System.Math.Abs(tzInterval - value) <= distance
      ? tzInterval
      : tzInterval + interval is var afzInterval && System.Math.Abs(afzInterval - value) <= distance
      ? afzInterval
      : value;

    /// <summary>Snaps the value to the nearest interval if it's within the specified distance of an interval.</summary>
    public static double DetentInterval(this double value, in double interval, in double distance)
      => (value / interval) * interval is var tzInterval && System.Math.Abs(tzInterval - value) <= distance
      ? tzInterval
      : tzInterval + interval is var afzInterval && System.Math.Abs(afzInterval - value) <= distance
      ? afzInterval
      : value;

    /// <summary>Snaps the value to the nearest interval if it's within the specified distance of an interval.</summary>
    public static int DetentInterval(this int value, int interval, int distance)
      => (value / interval) * interval is var tzInterval && System.Math.Abs(tzInterval - value) <= distance
      ? tzInterval
      : tzInterval + interval is var afzInterval && System.Math.Abs(afzInterval - value) <= distance
      ? afzInterval
      : value;

    /// <summary>Snaps the value to the nearest interval if it's within the specified distance of an interval.</summary>
    public static long DetentInterval(this long value, long interval, long distance)
      => (value / interval) * interval is var tzInterval && System.Math.Abs(tzInterval - value) <= distance
      ? tzInterval
      : tzInterval + interval is var afzInterval && System.Math.Abs(afzInterval - value) <= distance
      ? afzInterval
      : value;

#endif
  }
}
