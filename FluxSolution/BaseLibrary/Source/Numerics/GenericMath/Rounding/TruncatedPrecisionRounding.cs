namespace Flux
{
#if NET7_0_OR_GREATER

  /// <summary>Rounds <paramref name="x"/> by truncating to the specified number of <paramref name="significantDigits"/> and then round using the <paramref name="mode"/>. The reason for doing this is because unless a value is EXACTLY between two numbers, to the decimal, it will be rounded based on the next least significant decimal digit and so on.</summary>
  /// <seealso cref="https://stackoverflow.com/questions/1423074/rounding-to-even-in-c-sharp"/>
  /// <remarks>var r = Flux.GenericMath.TruncatingRound(99.96535789, 2, HalfwayRounding.ToEven); // = 99.96 (compare with the corresponding Round method)</remarks>
  public class TruncatedPrecisionRounding<TSelf>
    : INumberRoundable<TSelf>
    where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IPowerFunctions<TSelf>
  {
    private readonly int m_significantDigits;

    public TruncatedPrecisionRounding(int significantDigits) => m_significantDigits = significantDigits;

    public PrecisionRounding<TSelf> ToPrecisionRounding() => new(m_significantDigits);

    #region Static methods
    /// <summary>Rounds <paramref name="x"/> by truncating to the specified number of <paramref name="significantDigits"/> and then round using the <paramref name="mode"/>. The reason for doing this is because unless a value is EXACTLY between two numbers, to the decimal, it will be rounded based on the next least significant decimal digit and so on.</summary>
    /// <seealso cref="https://stackoverflow.com/questions/1423074/rounding-to-even-in-c-sharp"/>
    /// <remarks>var r = Flux.PrecisionTruncatedRounding(99.96535789, 2, HalfwayRounding.ToEven); // = 99.96 (compare with the corresponding Round method)</remarks>
    public static TSelf Round(TSelf x, RoundingMode mode, int significantDigits)
      => significantDigits >= 0 && TSelf.Pow(TSelf.CreateChecked(10), TSelf.CreateChecked(significantDigits + 1)) is var scalar
      ? PrecisionRounding<TSelf>.Round(TSelf.Truncate(x * scalar) / scalar, mode, significantDigits)
      : throw new System.ArgumentOutOfRangeException(nameof(significantDigits));
    #endregion Static methods

    #region Implemented interfaces
    public TSelf RoundNumber(TSelf value, RoundingMode mode) => Round(value, mode, m_significantDigits);
    #endregion Implemented interfaces
  }

#else

  /// <summary>Rounds <paramref name="x"/> by truncating to the specified number of <paramref name="significantDigits"/> and then round using the <paramref name="mode"/>. The reason for doing this is because unless a value is EXACTLY between two numbers, to the decimal, it will be rounded based on the next least significant decimal digit and so on.</summary>
  /// <seealso cref="https://stackoverflow.com/questions/1423074/rounding-to-even-in-c-sharp"/>
  /// <remarks>var r = Flux.GenericMath.TruncatingRound(99.96535789, 2, HalfwayRounding.ToEven); // = 99.96 (compare with the corresponding Round method)</remarks>
  public class TruncatedPrecisionRounding
    : INumberRoundable
  {
    private readonly int m_significantDigits;

    public TruncatedPrecisionRounding(int significantDigits) => m_significantDigits = significantDigits;

    public PrecisionRounding ToPrecisionRounding() => new(m_significantDigits);

    #region Static methods
    /// <summary>Rounds <paramref name="x"/> by truncating to the specified number of <paramref name="significantDigits"/> and then round using the <paramref name="mode"/>. The reason for doing this is because unless a value is EXACTLY between two numbers, to the decimal, it will be rounded based on the next least significant decimal digit and so on.</summary>
    /// <seealso cref="https://stackoverflow.com/questions/1423074/rounding-to-even-in-c-sharp"/>
    /// <remarks>var r = Flux.PrecisionTruncatedRounding(99.96535789, 2, HalfwayRounding.ToEven); // = 99.96 (compare with the corresponding Round method)</remarks>
    public static double Round(double x, RoundingMode mode, int significantDigits)
      => significantDigits >= 0 && System.Math.Pow(10, (significantDigits + 1)) is var scalar
      ? PrecisionRounding.Round(System.Math.Truncate(x * scalar) / scalar, mode, significantDigits)
      : throw new System.ArgumentOutOfRangeException(nameof(significantDigits));
    #endregion Static methods

    #region Implemented interfaces
    public double RoundNumber(double value, RoundingMode mode) => Round(value, mode, m_significantDigits);
    #endregion Implemented interfaces
  }

#endif
}
