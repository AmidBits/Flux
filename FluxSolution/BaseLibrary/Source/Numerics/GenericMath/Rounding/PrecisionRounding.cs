﻿#if NET7_0_OR_GREATER
namespace Flux
{
  public class PrecisionRounding<TSelf>
    : INumberRoundable<TSelf>
    where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IPowerFunctions<TSelf>
  {
    private readonly RoundingMode m_mode;
    private readonly int m_significantDigits;

    public PrecisionRounding(RoundingMode mode, int significantDigits)
    {
      m_mode = mode;
      m_significantDigits = significantDigits;
    }

    public PrecisionTruncatedRounding<TSelf> ToPrecisionTruncatedRounding()
      => new(m_mode, m_significantDigits);

    /// <summary>PREVIEW! Rounds the <paramref name="x"/> to the nearest integer. The <paramref name="mode"/> specifies the halfway rounding strategy to use if the value is halfway between two integers (e.g. 11.5).</summary>
    /// <remarks>var r = Flux.GenericMath.TruncatingRound(99.96535789, 2, HalfwayRounding.ToEven); // = 99.97 (compare with the corresponding TruncatingRound method)</remarks>
    public static TSelf Round(TSelf x, int significantDigits, RoundingMode mode)
      => significantDigits >= 0 && TSelf.Pow(TSelf.CreateChecked(10), TSelf.CreateChecked(significantDigits)) is var scalar
      ? Rounding<TSelf>.Round(x * scalar, mode) / scalar
      : throw new System.ArgumentOutOfRangeException(nameof(significantDigits));

    #region Implemented interfaces
    public TSelf RoundNumber(TSelf value)
      => Round(value, m_significantDigits, m_mode);
    #endregion Implemented interfaces
  }
}
#endif