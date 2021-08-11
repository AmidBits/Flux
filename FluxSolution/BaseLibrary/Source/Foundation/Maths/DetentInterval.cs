namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Snaps the value to the nearest interval if it's within the specified distance of an interval.</summary>
    public static System.Numerics.BigInteger DetentInterval(in System.Numerics.BigInteger value, in System.Numerics.BigInteger interval, in System.Numerics.BigInteger distance)
      => (value / interval) * interval is var nearestInterval && System.Numerics.BigInteger.Abs(nearestInterval - value) < distance
      ? nearestInterval
      : value;

    /// <summary>Snaps the value to the nearest interval if it's within the specified distance of an interval.</summary>
    public static decimal DetentInterval(in decimal value, in decimal interval, in decimal distance)
      => System.Math.Round(value / interval) * interval is var nearestInterval && System.Math.Abs(nearestInterval - value) < distance
      ? nearestInterval
      : value;

    /// <summary>Snaps the value to the nearest interval if it's within the specified distance of an interval.</summary>
    public static float DetentInterval(in float value, in float interval, in float distance)
      => (float)System.Math.Round(value / interval) * interval is var nearestInterval && (float)System.Math.Abs(nearestInterval - value) < distance
      ? nearestInterval
      : value;
    /// <summary>Snaps the value to the nearest interval if it's within the specified distance of an interval.</summary>
    public static double DetentInterval(in double value, in double interval, in double distance)
      => System.Math.Round(value / interval) * interval is var nearestInterval && System.Math.Abs(nearestInterval - value) < distance
      ? nearestInterval
      : value;

    /// <summary>Snaps the value to the nearest interval if it's within the specified distance of an interval.</summary>
    public static int DetentInterval(int value, int interval, int distance)
      => value / interval * interval is var nearestInterval && System.Math.Abs(nearestInterval - value) < distance
      ? nearestInterval
      : value;
    /// <summary>Snaps the value to the nearest interval if it's within the specified distance of an interval.</summary>
    public static long DetentInterval(long value, long interval, long distance)
      => value / interval * interval is var nearestInterval && System.Math.Abs(nearestInterval - value) < distance
      ? nearestInterval
      : value;

    /// <summary>Snaps the value to the nearest interval if it's within the specified distance of an interval.</summary>
    [System.CLSCompliant(false)]
    public static uint DetentInterval(uint value, uint interval, uint distance)
      => (value / interval) * interval is var nearestInterval && System.Math.Abs(nearestInterval - value) < distance
      ? nearestInterval
      : value;
    /// <summary>Snaps the value to the nearest interval if it's within the specified distance of an ixxnterval.</summary>
    [System.CLSCompliant(false)]
    public static ulong DetentInterval(ulong value, ulong interval, ulong distance)
      => (value / interval) * interval is var nearestInterval && (ulong)System.Math.Abs((long)(nearestInterval - value)) < distance
      ? nearestInterval
      : value;
  }
}
