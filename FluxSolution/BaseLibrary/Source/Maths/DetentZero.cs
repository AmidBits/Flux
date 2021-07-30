namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Snaps the value to zero if it's within the specified distance of zero.</summary>
    public static System.Numerics.BigInteger DetentZero(System.Numerics.BigInteger value, System.Numerics.BigInteger distance)
      => value < -distance || value > distance
      ? value
      : 0;

    /// <summary>Snaps the value to zero if it's within the specified distance of zero.</summary>
    public static decimal DetentZero(decimal value, decimal distance)
      => value < -distance || value > distance
      ? value
      : 0;

    /// <summary>Snaps the value to zero if it's within the specified distance of zero.</summary>
    public static float DetentZero(float value, float distance)
      => value < -distance || value > distance
      ? value
      : 0F;
    /// <summary>Snaps the value to zero if it's within the specified distance of zero.</summary>
    public static double DetentZero(double value, double distance)
      => value < -distance || value > distance
      ? value
      : 0;

    /// <summary>Snaps the value to zero if it's within the specified distance of zero.</summary>
    public static int DetentZero(int value, int distance)
      => value < -distance || value > distance
      ? value
      : 0;
    /// <summary>Snaps the value to zero if it's within the specified distance of zero.</summary>
    public static long DetentZero(long value, long distance)
      => value < -distance || value > distance
      ? value
      : 0;

    /// <summary>Snaps the value to zero if it's within the specified distance of zero.</summary>
    [System.CLSCompliant(false)]
    public static uint DetentZero(uint value, uint distance)
      => value > distance
      ? value
      : 0;
    /// <summary>Snaps the value to zero if it's within the specified distance of zero.</summary>
    [System.CLSCompliant(false)]
    public static ulong DetentZero(ulong value, ulong distance)
      => value > distance
      ? value
      : 0;
  }
}
