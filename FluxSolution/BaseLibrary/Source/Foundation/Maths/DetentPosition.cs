namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Determines whether a value is within a specified distance of the position, and if so, snaps the value to the position.</summary>
    public static System.Numerics.BigInteger DetentPosition(System.Numerics.BigInteger value, System.Numerics.BigInteger position, System.Numerics.BigInteger distance)
      => System.Numerics.BigInteger.Abs(position - value) > System.Numerics.BigInteger.Abs(distance)
      ? value
      : position;

    /// <summary>Snaps the value to the position if it's within the specified distance of the position.</summary>
    public static decimal DetentPosition(decimal value, decimal position, decimal distance)
      => System.Math.Abs(position - value) > System.Math.Abs(distance)
      ? value
      : position;

    /// <summary>Snaps the value to the position if it's within the specified distance of the position.</summary>
    public static float DetentPosition(float value, float position, float distance)
      => System.Math.Abs(position - value) > System.Math.Abs(distance)
      ? value
      : position;
    /// <summary>Snaps the value to the position if it's within the specified distance of the position.</summary>
    public static double DetentPosition(double value, double position, double distance)
      => System.Math.Abs(position - value) > System.Math.Abs(distance)
      ? value
      : position;

    /// <summary>Determines whether a value is within a specified positive distance of the position, and if so, snaps the value to the position.</summary>
    public static int DetentPosition(int value, int position, int distance)
      => System.Math.Abs(position - value) > System.Math.Abs(distance)
      ? value
      : position;
    /// <summary>Determines whether a value is within a specified distance of the position, and if so, snaps the value to the position.</summary>
    public static long DetentPosition(long value, long position, long distance)
      => System.Math.Abs(position - value) > System.Math.Abs(distance)
      ? value
      : position;

    /// <summary>Determines whether a value is within a specified distance of the position, and if so, snaps the value to the position.</summary>
    [System.CLSCompliant(false)]
    public static uint DetentPosition(uint value, uint position, uint distance)
      => value >= position - distance && value <= position + distance
      ? position
      : value;
    /// <summary>Determines whether a value is within a specified distance of the position, and if so, snaps the value to the position.</summary>
    [System.CLSCompliant(false)]
    public static ulong DetentPosition(ulong value, ulong position, ulong distance)
      => value >= position - distance && value <= position + distance
      ? position
      : value;
  }
}
