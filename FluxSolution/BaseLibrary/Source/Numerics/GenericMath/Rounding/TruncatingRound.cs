#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Rounds <paramref name="x"/> by truncating at the specified number of <paramref name="significantDigits"> decimal digits</paramref> and then round using the <paramref name="mode"/>. The reason for doing this is because unless a value is EXACTLY between two numbers, to the decimal, it will be rounded based on the next least significant decimal digit and so on.</summary>
    /// <seealso cref="https://stackoverflow.com/questions/1423074/rounding-to-even-in-c-sharp"/>
    public static TSelf TruncatingRound<TSelf, TDigits>(this TSelf x, int significantDigits, System.MidpointRounding mode)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IPowerFunctions<TSelf>
      => significantDigits >= 0 && TSelf.Pow(TSelf.CreateChecked(10), TSelf.CreateChecked(significantDigits + 1)) is var scalar
      ? TSelf.Round(TSelf.Truncate(x * scalar) / scalar, significantDigits, mode)
      : throw new System.ArgumentOutOfRangeException(nameof(significantDigits));

    /// <summary>Rounds <paramref name="x"/> by truncating at the specified number of <paramref name="significantDigits"> decimal digits</paramref> and then round using the <paramref name="mode"/>. The reason for doing this is because unless a value is EXACTLY between two numbers, to the decimal, it will be rounded based on the next least significant decimal digit and so on.</summary>
    /// <seealso cref="https://stackoverflow.com/questions/1423074/rounding-to-even-in-c-sharp"/>
    public static TSelf TruncatingRound<TSelf>(this TSelf x, int significantDigits, HalfwayRounding mode)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IPowerFunctions<TSelf>
      => significantDigits >= 0 && TSelf.Pow(TSelf.CreateChecked(10), TSelf.CreateChecked(significantDigits + 1)) is var scalar
      ? Round(TSelf.Truncate(x * scalar) / scalar, significantDigits, mode)
      : throw new System.ArgumentOutOfRangeException(nameof(significantDigits));

    /// <summary>Rounds <paramref name="x"/> by truncating at the specified number of <paramref name="significantDigits"> decimal digits</paramref> and then round using the <paramref name="mode"/>. The reason for doing this is because unless a value is EXACTLY between two numbers, to the decimal, it will be rounded based on the next least significant decimal digit and so on.</summary>
    /// <seealso cref="https://stackoverflow.com/questions/1423074/rounding-to-even-in-c-sharp"/>
    public static TSelf TruncatingRound<TSelf>(this TSelf x, int significantDigits, FullRounding mode)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IPowerFunctions<TSelf>
      => significantDigits >= 0 && TSelf.Pow(TSelf.CreateChecked(10), TSelf.CreateChecked(significantDigits + 1)) is var scalar
      ? Round(TSelf.Truncate(x * scalar) / scalar, significantDigits, mode)
      : throw new System.ArgumentOutOfRangeException(nameof(significantDigits));
  }
}
#endif
