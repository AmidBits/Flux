﻿#if NET7_0_OR_GREATER
namespace Flux
{
  public class PrecisionTruncatedRounding<TSelf>
    : INumberRoundable<TSelf>
    where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IPowerFunctions<TSelf>
  {
    private readonly RoundingMode m_mode;
    private readonly int m_significantDigits;

    public PrecisionTruncatedRounding(RoundingMode mode, int significantDigits)
    {
      m_mode = mode;
      m_significantDigits = significantDigits;
    }

    public PrecisionRounding<TSelf> ToPrecisionRounding()
      => new(m_mode, m_significantDigits);

    /// <summary>Rounds <paramref name="x"/> by truncating to the specified number of <paramref name="significantDigits"> decimal digits</paramref> and then round using the <paramref name="mode"/>. The reason for doing this is because unless a value is EXACTLY between two numbers, to the decimal, it will be rounded based on the next least significant decimal digit and so on.</summary>
    /// <seealso cref="https://stackoverflow.com/questions/1423074/rounding-to-even-in-c-sharp"/>
    /// <remarks>var r = Flux.GenericMath.TruncatingRound(99.96535789, 2, HalfwayRounding.ToEven); // = 99.96 (compare with the corresponding Round method)</remarks>
    public static TSelf Round(TSelf x, int significantDigits, RoundingMode mode)
      => significantDigits >= 0 && TSelf.Pow(TSelf.CreateChecked(10), TSelf.CreateChecked(significantDigits + 1)) is var scalar
      ? new PrecisionRounding<TSelf>(mode, significantDigits).RoundNumber(TSelf.Truncate(x * scalar) / scalar)
      : throw new System.ArgumentOutOfRangeException(nameof(significantDigits));

#region Implemented interfaces
    public TSelf RoundNumber(TSelf value)
      => Round(value, m_significantDigits, m_mode);
#endregion Implemented interfaces
  }
}
#endif