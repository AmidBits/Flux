namespace Flux
{
  public static partial class DoubleEm
  {
    /// <summary>Deconstructs the decimal number into its basic components as a named ValueType for use.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Double-precision_floating-point_format"/>
    /// <seealso cref="http://www.yoda.arachsys.com/csharp/DoubleConverter.cs"/>
    /// <seealso cref="https://blogs.msdn.microsoft.com/dwayneneed/2010/05/06/fun-with-floating-point/"/>
    public static double GetParts(this double source, out long bits, out int exponent, out bool isNegative, out long mantissa)
    {
      //if (double.IsNaN(source)) throw new System.ArithmeticException(double.NaN.ToString());
      //else if (double.IsNegativeInfinity(source)) throw new System.ArithmeticException(double.NegativeInfinity.ToString());
      //else if (double.IsPositiveInfinity(source)) throw new System.ArithmeticException(double.PositiveInfinity.ToString());

      bits = System.BitConverter.DoubleToInt64Bits(source);

      exponent = (int)((bits >> 52) & 0x7ffL);

      isNegative = (bits < 0);

      mantissa = bits & 0xfffffffffffffL; // a.k.a. significand

      return source;
    }
  }
}
