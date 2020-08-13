
namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Return one value (x) with the sign of another (y).</summary>
    public static System.Numerics.BigInteger CopySign(System.Numerics.BigInteger value, System.Numerics.BigInteger sign)
      => value < 0 ? (sign < 0 ? value : -value) : (sign < 0 ? -value : value);

    /// <summary>Return one value (x) with the sign of another (y).</summary>
    public static decimal CopySign(decimal value, decimal sign)
      => value < 0 ? (sign < 0 ? value : -value) : (sign < 0 ? -value : value);

    /// <summary>Return one value (x) with the sign of another (y).</summary>
    public static float CopySign(float value, float sign)
      => value < 0 ? (sign < 0 ? value : -value) : (sign < 0 ? -value : value);
    /// <summary>Return one value (x) with the sign of another (y).</summary>
    public static double CopySign(double value, double sign)
      => value < 0 ? (sign < 0 ? value : -value) : (sign < 0 ? -value : value);

    /// <summary>Return one value (x) with the sign of another (y).</summary>
    public static int CopySign(int value, int sign)
      => value < 0 ? (sign < 0 ? value : -value) : (sign < 0 ? -value : value);
    /// <summary>Return one value (x) with the sign of another (y).</summary>
    public static long CopySign(long value, long sign)
      => value < 0 ? (sign < 0 ? value : -value) : (sign < 0 ? -value : value);
  }
}
