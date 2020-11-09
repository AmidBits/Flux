
namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Return one value (x) with the sign of another (y).</summary>
    public static System.Numerics.BigInteger CopySign(System.Numerics.BigInteger value, System.Numerics.BigInteger sign)
      => System.Numerics.BigInteger.Abs(value) * Sign(sign);

    /// <summary>Return one value (x) with the sign of another (y).</summary>
    public static decimal CopySign(decimal value, decimal sign)
      => System.Math.Abs(value) * Sign(sign);

    /// <summary>Return one value (x) with the sign of another (y).</summary>
    public static float CopySign(float value, float sign)
      => System.Math.Abs(value) * Sign(sign);
    /// <summary>Return one value (x) with the sign of another (y).</summary>
    public static double CopySign(double value, double sign)
      => System.Math.Abs(value) * Sign(sign);

    /// <summary>Return one value (x) with the sign of another (y).</summary>
    public static int CopySign(int value, int sign)
      => System.Math.Abs(value) * Sign(sign);
    /// <summary>Return one value (x) with the sign of another (y).</summary>
    public static long CopySign(long value, long sign)
      => System.Math.Abs(value) * Sign(sign);
  }
}
