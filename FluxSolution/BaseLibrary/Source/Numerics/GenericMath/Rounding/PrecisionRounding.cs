namespace Flux
{
  /// <summary>Floating point rounding, rounds the <paramref name="x"/> to the nearest <paramref name="significantDigits"/>. The <paramref name="mode"/> specifies the halfway rounding strategy to use.</summary>
  /// <remarks>var r = Flux.GenericMath.TruncatingRound(99.96535789, 2, HalfwayRounding.ToEven); // = 99.97 (compare with the corresponding TruncatingRound method)</remarks>
  public class PrecisionRounding<TSelf>
    : INumberRoundable<TSelf>
    where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IPowerFunctions<TSelf>
  {
    private readonly int m_significantDigits;

    public PrecisionRounding(int significantDigits) => m_significantDigits = significantDigits;

    public TruncatedPrecisionRounding<TSelf> ToPrecisionTruncatedRounding() => new(m_significantDigits);

    #region Static methods
    /// <summary>Rounds the <paramref name="x"/> to the nearest <paramref name="significantDigits"/>. The <paramref name="mode"/> specifies the halfway rounding strategy to use.</summary>
    /// <remarks>var r = Flux.GenericMath.TruncatingRound(99.96535789, 2, HalfwayRounding.ToEven); // = 99.97 (compare with the corresponding TruncatingRound method)</remarks>
    public static TSelf Round(TSelf x, RoundingMode mode, int significantDigits)
      => significantDigits >= 0 && TSelf.Pow(TSelf.CreateChecked(10), TSelf.CreateChecked(significantDigits)) is var scalar
      ? Rounding<TSelf>.Round(x * scalar, mode) / scalar
      : throw new System.ArgumentOutOfRangeException(nameof(significantDigits));
    #endregion Static methods

    #region Implemented interfaces
    public TSelf RoundNumber(TSelf x, RoundingMode mode) => Round(x, mode, m_significantDigits);
    #endregion Implemented interfaces
  }
}
