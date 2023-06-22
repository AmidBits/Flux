namespace Flux
{
  public static partial class Maths
  {
#if NET7_0_OR_GREATER

    /// <summary>Rounds <paramref name="x"/> by truncating to the specified number of <paramref name="significantDigits"/> and then round using the <paramref name="mode"/>. The reason for doing this is because unless a value is EXACTLY between two numbers, to the decimal, it will be rounded based on the next least significant decimal digit and so on.</summary>
    /// <seealso cref="https://stackoverflow.com/questions/1423074/rounding-to-even-in-c-sharp"/>
    /// <remarks>var r = GenericMath.RoundToTruncatedPrecision(99.96535789, 2, HalfwayRounding.ToEven); // = 99.96 (compare with the corresponding <see cref="RoundToPrecision{TValue}(TValue, RoundingMode, int, int)"/> method)</remarks>
    public static TSelf RoundToTruncatedPrecision<TSelf>(this TSelf x, RoundingMode mode, int significantDigits, int radix = 10)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
    {
      if (significantDigits < 0) throw new System.ArgumentOutOfRangeException(nameof(significantDigits));

      var scalar = TSelf.Pow(TSelf.CreateChecked(radix), TSelf.CreateChecked(significantDigits + 1));

      return RoundToPrecision(TSelf.Truncate(x * scalar) / scalar, mode, significantDigits, radix);
    }

#else

    /// <summary>Rounds <paramref name="x"/> by truncating to the specified number of <paramref name="significantDigits"/> and then round using the <paramref name="mode"/>. The reason for doing this is because unless a value is EXACTLY between two numbers, to the decimal, it will be rounded based on the next least significant decimal digit and so on.</summary>
    /// <seealso cref="https://stackoverflow.com/questions/1423074/rounding-to-even-in-c-sharp"/>
    /// <remarks>var r = GenericMath.RoundToTruncatedPrecision(99.96535789, 2, HalfwayRounding.ToEven); // = 99.96 (compare with the corresponding <see cref="RoundToPrecision{TValue}(TValue, RoundingMode, int, int)"/> method)</remarks>
    public static double RoundToTruncatedPrecision(this double x, RoundingMode mode, int significantDigits, int radix = 10)
    {
      if (significantDigits < 0) throw new System.ArgumentOutOfRangeException(nameof(significantDigits));

      var scalar = System.Math.Pow(10, significantDigits + 1);

      return RoundToPrecision(System.Math.Truncate(x * scalar) / scalar, mode, significantDigits, radix);
    }

#endif
  }
}
