namespace Flux
{
  public static partial class Maths
  {
    #region DetentInterval(value, interval, distance)

    /// <summary>Snaps the value to the nearest interval if it's within the specified distance of an interval.</summary>
    public static System.Numerics.BigInteger DetentInterval(in System.Numerics.BigInteger value, in System.Numerics.BigInteger interval, in System.Numerics.BigInteger distance)
      => (value / interval) * interval is var nearestInterval && System.Numerics.BigInteger.Abs(nearestInterval - value) < distance ? nearestInterval : value;

    /// <summary>Snaps the value to the nearest interval if it's within the specified distance of an interval.</summary>
    public static decimal DetentInterval(in decimal value, in decimal interval, in decimal distance)
      => System.Math.Round(value / interval) * interval is var nearestInterval && System.Math.Abs(nearestInterval - value) < distance ? nearestInterval : value;

    /// <summary>Snaps the value to the nearest interval if it's within the specified distance of an interval.</summary>
    public static float DetentInterval(in float value, in float interval, in float distance)
      => (float)System.Math.Round(value / interval) * interval is var nearestInterval && (float)System.Math.Abs(nearestInterval - value) < distance ? nearestInterval : value;
    /// <summary>Snaps the value to the nearest interval if it's within the specified distance of an interval.</summary>
    public static double DetentInterval(in double value, in double interval, in double distance)
      => System.Math.Round(value / interval) * interval is var nearestInterval && System.Math.Abs(nearestInterval - value) < distance ? nearestInterval : value;

    /// <summary>Snaps the value to the nearest interval if it's within the specified distance of an interval.</summary>
    public static int DetentInterval(int value, int interval, int distance)
      => value / interval * interval is var nearestInterval && System.Math.Abs(nearestInterval - value) < distance ? nearestInterval : value;
    /// <summary>Snaps the value to the nearest interval if it's within the specified distance of an interval.</summary>
    public static long DetentInterval(long value, long interval, long distance)
      => value / interval * interval is var nearestInterval && System.Math.Abs(nearestInterval - value) < distance ? nearestInterval : value;

    /// <summary>Snaps the value to the nearest interval if it's within the specified distance of an interval.</summary>
    [System.CLSCompliant(false)]
    public static uint DetentInterval(uint value, uint interval, uint distance)
      => (value / interval) * interval is var nearestInterval && System.Math.Abs(nearestInterval - value) < distance ? nearestInterval : value;
    /// <summary>Snaps the value to the nearest interval if it's within the specified distance of an ixxnterval.</summary>
    [System.CLSCompliant(false)]
    public static ulong DetentInterval(ulong value, ulong interval, ulong distance)
      => (value / interval) * interval is var nearestInterval && (ulong)System.Math.Abs((long)(nearestInterval - value)) < distance ? nearestInterval : value;

    #endregion DetentInterval(value, interval, distance)

    #region DetentPosition(value, position, distance)

    /// <summary>Determines whether a value is within a specified distance of the position, and if so, snaps the value to the position.</summary>
    public static System.Numerics.BigInteger DetentPosition(System.Numerics.BigInteger value, System.Numerics.BigInteger position, System.Numerics.BigInteger distance)
      => System.Numerics.BigInteger.Abs(position - value) > System.Numerics.BigInteger.Abs(distance) ? value : position;

    /// <summary>Snaps the value to the position if it's within the specified distance of the position.</summary>
    public static decimal DetentPosition(decimal value, decimal position, decimal distance)
      => System.Math.Abs(position - value) > System.Math.Abs(distance) ? value : position;

    /// <summary>Snaps the value to the position if it's within the specified distance of the position.</summary>
    public static float DetentPosition(float value, float position, float distance)
      => System.Math.Abs(position - value) > System.Math.Abs(distance) ? value : position;
    /// <summary>Snaps the value to the position if it's within the specified distance of the position.</summary>
    public static double DetentPosition(double value, double position, double distance)
      => System.Math.Abs(position - value) > System.Math.Abs(distance) ? value : position;

    /// <summary>Determines whether a value is within a specified positive distance of the position, and if so, snaps the value to the position.</summary>
    public static int DetentPosition(int value, int position, int distance)
      => System.Math.Abs(position - value) > System.Math.Abs(distance) ? value : position;
    /// <summary>Determines whether a value is within a specified distance of the position, and if so, snaps the value to the position.</summary>
    public static long DetentPosition(long value, long position, long distance)
      => System.Math.Abs(position - value) > System.Math.Abs(distance) ? value : position;

    /// <summary>Determines whether a value is within a specified distance of the position, and if so, snaps the value to the position.</summary>
    [System.CLSCompliant(false)]
    public static uint DetentPosition(uint value, uint position, uint distance)
      => value >= position - distance && value <= position + distance ? position : value;
    /// <summary>Determines whether a value is within a specified distance of the position, and if so, snaps the value to the position.</summary>
    [System.CLSCompliant(false)]
    public static ulong DetentPosition(ulong value, ulong position, ulong distance)
      => value >= position - distance && value <= position + distance ? position : value;

    #endregion DetentPosition(value, position, distance)

    #region DetentZero(value, distance)

    /// <summary>Snaps the value to zero if it's within the specified distance of zero.</summary>
    public static System.Numerics.BigInteger DetentZero(System.Numerics.BigInteger value, System.Numerics.BigInteger distance)
      => value < -distance || value > distance ? value : 0;

    /// <summary>Snaps the value to zero if it's within the specified distance of zero.</summary>
    public static decimal DetentZero(decimal value, decimal distance)
      => value < -distance || value > distance ? value : 0;

    /// <summary>Snaps the value to zero if it's within the specified distance of zero.</summary>
    public static float DetentZero(float value, float distance)
      => value < -distance || value > distance ? value : 0F;
    /// <summary>Snaps the value to zero if it's within the specified distance of zero.</summary>
    public static double DetentZero(double value, double distance)
      => value < -distance || value > distance ? value : 0;

    /// <summary>Snaps the value to zero if it's within the specified distance of zero.</summary>
    public static int DetentZero(int value, int distance)
      => value < -distance || value > distance ? value : 0;
    /// <summary>Snaps the value to zero if it's within the specified distance of zero.</summary>
    public static long DetentZero(long value, long distance)
      => value < -distance || value > distance ? value : 0;

    /// <summary>Snaps the value to zero if it's within the specified distance of zero.</summary>
    [System.CLSCompliant(false)]
    public static uint DetentZero(uint value, uint distance)
      => value > distance ? value : 0;
    /// <summary>Snaps the value to zero if it's within the specified distance of zero.</summary>
    [System.CLSCompliant(false)]
    public static ulong DetentZero(ulong value, ulong distance)
      => value > distance ? value : 0;

    #endregion DetentZero(value, distance)
  }
}
