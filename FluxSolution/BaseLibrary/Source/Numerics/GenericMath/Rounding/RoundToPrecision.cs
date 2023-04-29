namespace Flux
{
  public static partial class GenericMath
  {
#if NET7_0_OR_GREATER

    /// <summary>Rounds the <paramref name="value"/> to the nearest <paramref name="significantDigits"/>. The <paramref name="mode"/> specifies the halfway rounding strategy to use.</summary>
    /// <remarks>var r = Flux.GenericMath.RoundToPrecision(99.96535789, 2, HalfwayRounding.ToEven); // = 99.97 (compare with the corresponding <see cref="RoundToTruncatedPrecision{TSelf}(TSelf, RoundingMode, int, int)"/> method)</remarks>
    public static TValue RoundToPrecision<TValue>(this TValue value, RoundingMode mode, int significantDigits, int radix = 10)
      where TValue : System.Numerics.IFloatingPointIeee754<TValue>
    {
      var scalar = TValue.Pow(TValue.CreateChecked(radix), TValue.CreateChecked(significantDigits));

      return Round(value * scalar, mode) / scalar;
    }

#else

    /// <summary>Rounds the <paramref name="value"/> to the nearest <paramref name="significantDigits"/>. The <paramref name="mode"/> specifies the halfway rounding strategy to use.</summary>
    /// <remarks>var r = Flux.GenericMath.RoundToPrecision(99.96535789, 2, HalfwayRounding.ToEven); // = 99.97 (compare with the corresponding <see cref="RoundToTruncatedPrecision{TSelf}(TSelf, RoundingMode, int, int)"/> method)</remarks>
    public static double RoundToPrecision(this double x, RoundingMode mode, int significantDigits, int radix = 10)
    {
      var scalar = System.Math.Pow(10, significantDigits);

      return Round(x * scalar, mode) / scalar;
    }

#endif
  }
}
