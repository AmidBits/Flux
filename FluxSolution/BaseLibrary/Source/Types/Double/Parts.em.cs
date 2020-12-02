namespace Flux
{
  public static partial class DoubleEm
  {
    /// <summary>Strips the integer part of the floating point value, resulting in only the fractional part.</summary>
    public static double FractionalPart(this double source)
      => source - System.Math.Truncate(source);

    /// <summary>Deconstructs the decimal number into its basic components as a named ValueType for use.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Double-precision_floating-point_format"/>
    /// <seealso cref="http://www.yoda.arachsys.com/csharp/DoubleConverter.cs"/>
    /// <seealso cref="https://blogs.msdn.microsoft.com/dwayneneed/2010/05/06/fun-with-floating-point/"/>
    public static (long Bits, int Exponent, bool IsNegative, long Mantissa, double OriginalValue) GetParts(this double source)
    {
      //if (double.IsNaN(source)) throw new System.ArithmeticException(double.NaN.ToString());
      //else if (double.IsNegativeInfinity(source)) throw new System.ArithmeticException(double.NegativeInfinity.ToString());
      //else if (double.IsPositiveInfinity(source)) throw new System.ArithmeticException(double.PositiveInfinity.ToString());

      var bits = System.BitConverter.DoubleToInt64Bits(source);

      var exponent = (int)((bits >> 52) & 0x7ffL);

      var isNegative = (bits < 0);

      var mantissa = bits & 0xfffffffffffffL; // a.k.a. significand

      return (bits, exponent, isNegative, mantissa, source);
    }

    /// <summary>Strips the fractional part of the floating point value, resulting in only the integer part.</summary>
    public static double IntegerPart(this double source)
      => System.Math.Truncate(source);
  }
}
