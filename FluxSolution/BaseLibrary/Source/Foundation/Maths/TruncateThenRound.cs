namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Wrapper for System.Math.Round that truncates the number at the specified number of fractional digits and then performs the rounding of the number. The reason for doing this is because unless a value is EXACTLY between two numbers, to the decimal, it will be rounded based on the next lower decimal digit and so on.</summary>
    /// <seealso cref="https://stackoverflow.com/questions/1423074/rounding-to-even-in-c-sharp"/>
    public static decimal RoundToSignificantDigits(decimal value, int significantDigits, System.MidpointRounding mode)
      => significantDigits > 0 && (decimal)System.Math.Pow(10, significantDigits + 1) is var scalar
      ? System.Math.Round(System.Math.Truncate(value * scalar) / scalar, significantDigits, mode)
      : throw new System.ArgumentOutOfRangeException(nameof(significantDigits));

    /// <summary>Wrapper for System.Math.Round that truncates the number at the specified number of fractional digits and then performs the rounding of the number. The reason for doing this is because unless a value is EXACTLY between two numbers, to the decimal, it will be rounded based on the next lower decimal digit and so on.</summary>
    /// <seealso cref="https://stackoverflow.com/questions/1423074/rounding-to-even-in-c-sharp"/>
    public static double RoundToSignificantDigits(double value, int significantDigits, System.MidpointRounding mode)
      => significantDigits >= 0 && System.Math.Pow(10, significantDigits + 1) is var scalar
      ? System.Math.Round(System.Math.Truncate(value * scalar) / scalar, significantDigits, mode)
      : throw new System.ArgumentOutOfRangeException(nameof(significantDigits));
  }
}
