#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Wrapper for System.Math.Round that truncates the number at the specified number of fractional digits and then performs the rounding of the number. The reason for doing this is because unless a value is EXACTLY between two numbers, to the decimal, it will be rounded based on the next lower decimal digit and so on.</summary>
    /// <seealso cref="https://stackoverflow.com/questions/1423074/rounding-to-even-in-c-sharp"/>
    public static TSelf RoundToSignificantDigits<TSelf, TDigits>(this TSelf value, int significantDigits, System.MidpointRounding mode)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IPowerFunctions<TSelf>
      => significantDigits >= 0 && TSelf.Pow(TSelf.CreateChecked(10), TSelf.CreateChecked(significantDigits + 1)) is var scalar
      ? TSelf.Round(TSelf.Truncate(value * scalar) / scalar, significantDigits, mode)
      : throw new System.ArgumentOutOfRangeException(nameof(significantDigits));
  }
}
#endif
