namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Like System.Math.Round that truncates the number at the specified number of fractional digits and then performs the rounding of the number.</summary>
    /// <seealso cref="https://stackoverflow.com/questions/1423074/rounding-to-even-in-c-sharp"/>
    public static decimal RoundViaTruncation(decimal value, int digits, System.MidpointRounding mode)
      => digits > 0 && (decimal)System.Math.Pow(10, digits + 1) is var scalar
      ? System.Math.Round(System.Math.Truncate(scalar * value) / scalar, digits, mode)
      : throw new System.ArgumentOutOfRangeException(nameof(digits));

    /// <summary>Wrapper for System.Math.Round that truncates the number at the specified number of fractional digits and then performs the rounding of the number.</summary>
    /// <seealso cref="https://stackoverflow.com/questions/1423074/rounding-to-even-in-c-sharp"/>
    public static double RoundViaTruncation(double value, int digits, System.MidpointRounding mode)
      => digits >= 0 && System.Math.Pow(10, digits + 1) is var scalar
      ? System.Math.Round(System.Math.Truncate(scalar * value) / scalar, digits, mode)
      : throw new System.ArgumentOutOfRangeException(nameof(digits));
    /// <summary>Wrapper for System.Math.Round that truncates the number at the specified number of fractional digits and then performs the rounding of the number.</summary>
    /// <seealso cref="https://stackoverflow.com/questions/1423074/rounding-to-even-in-c-sharp"/>
    public static float RoundViaTruncation(float value, int digits, System.MidpointRounding mode)
      => (float)RoundViaTruncation((double)value, digits, mode);
  }
}
