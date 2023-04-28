namespace Flux
{
#if NET7_0_OR_GREATER

  public static partial class GenericMath
  {
    /// <summary>Rounds the <paramref name="value"/> to the nearest <paramref name="significantDigits"/>. The <paramref name="mode"/> specifies the halfway rounding strategy to use.</summary>
    /// <remarks>var r = Flux.GenericMath.RoundToPrecision(99.96535789, 2, HalfwayRounding.ToEven); // = 99.97 (compare with the corresponding <see cref="RoundToTruncatedPrecision{TSelf}(TSelf, RoundingMode, int, int)"/> method)</remarks>
    public static TValue RoundToPrecision<TValue>(this TValue value, RoundingMode mode, int significantDigits, int radix = 10)
      where TValue : System.Numerics.IFloatingPointIeee754<TValue>
    {
      var scalar = TValue.Pow(TValue.CreateChecked(radix), TValue.CreateChecked(significantDigits));

      return Round(value * scalar, mode) / scalar;
    }

  }

#else

  /// <summary>Floating point rounding, rounds the <paramref name="x"/> to the nearest <paramref name="significantDigits"/>. The <paramref name="mode"/> specifies the halfway rounding strategy to use.</summary>
  /// <remarks>var r = Flux.GenericMath.TruncatingRound(99.96535789, 2, HalfwayRounding.ToEven); // = 99.97 (compare with the corresponding TruncatingRound method)</remarks>
  public class PrecisionRounding
    : INumberRoundable
  {
    private readonly int m_significantDigits;

    public PrecisionRounding(int significantDigits) => m_significantDigits = significantDigits;

    public TruncatedPrecisionRounding ToPrecisionTruncatedRounding() => new(m_significantDigits);

    #region Static methods
    /// <summary>Rounds the <paramref name="x"/> to the nearest <paramref name="significantDigits"/>. The <paramref name="mode"/> specifies the halfway rounding strategy to use.</summary>
    /// <remarks>var r = Flux.GenericMath.TruncatingRound(99.96535789, 2, HalfwayRounding.ToEven); // = 99.97 (compare with the corresponding TruncatingRound method)</remarks>
    public static double Round(double x, RoundingMode mode, int significantDigits)
      => significantDigits >= 0 && System.Math.Pow(10, significantDigits) is var scalar
      ? Rounding.Round(x * scalar, mode) / scalar
      : throw new System.ArgumentOutOfRangeException(nameof(significantDigits));
    #endregion Static methods

    #region Implemented interfaces
    public double RoundNumber(double x, RoundingMode mode) => Round(x, mode, m_significantDigits);
    #endregion Implemented interfaces
  }

#endif
}
