namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Returns a length (distance) between the two specified values.</summary>
    public static System.Numerics.BigInteger Length(System.Numerics.BigInteger a, System.Numerics.BigInteger b)
      => System.Numerics.BigInteger.Max(a, b) - System.Numerics.BigInteger.Min(a, b);

    /// <summary>Returns a length (distance) between the two specified values.</summary>
    public static decimal Wrap(decimal a, decimal b)
      => System.Math.Max(a, b) - System.Math.Min(a, b);

    /// <summary>Returns a length (distance) between the two specified values.</summary>
    public static float Wrap(float a, float b)
      => System.Math.Max(a, b) - System.Math.Min(a, b);
    /// <summary>Returns a length (distance) between the two specified values.</summary>
    public static double Wrap(double a, double b)
      => System.Math.Max(a, b) - System.Math.Min(a, b);

    /// <summary>Returns a length (distance) between the two specified values.</summary>
    public static int Wrap(int a, int b)
      => System.Math.Max(a, b) - System.Math.Min(a, b);
    /// <summary>Returns a length (distance) between the two specified values.</summary>
    public static long Wrap(long a, long b)
      => System.Math.Max(a, b) - System.Math.Min(a, b);

    /// <summary>Returns a length (distance) between the two specified values.</summary>
    [System.CLSCompliant(false)]
    public static uint Wrap(uint a, uint b)
      => System.Math.Max(a, b) - System.Math.Min(a, b);
    /// <summary>Returns a length (distance) between the two specified values.</summary>
    [System.CLSCompliant(false)]
    public static ulong Wrap(ulong a, ulong b)
      => System.Math.Max(a, b) - System.Math.Min(a, b);
  }
}
