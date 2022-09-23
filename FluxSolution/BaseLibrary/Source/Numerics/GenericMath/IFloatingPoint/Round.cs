#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class FloatingPoint
  {
    #region Halfway Rounding Extension Methods
    /// <summary>PREVIEW! Common rounding: round half down, bias: negative infinity.</summary>
    public static TSelf HalfwayRoundDown<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.Ceiling(value - (TSelf.One / (TSelf.One + TSelf.One)));

    /// <summary>PREVIEW! Symmetric rounding: round half down, bias: towards zero.</summary>
    public static TSelf HalfwayRoundDownZero<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.CopySign(HalfwayRoundDown(TSelf.Abs(value)), value);

    /// <summary>PREVIEW! Common rounding: round half up, bias: positive infinity.</summary>
    public static TSelf HalfwayRoundUp<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.Floor(value + (TSelf.One / (TSelf.One + TSelf.One)));

    /// <summary>PREVIEW! Symmetric rounding: round half up, bias: away from zero.</summary>
    public static TSelf HalfwayRoundUpZero<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.CopySign(HalfwayRoundUp(TSelf.Abs(value)), value);
    #endregion Halfway Rounding Extension Methods

    #region Integer Rounding Extension Methods
    /// <summary>PREVIEW! Symmetric rounding: round up, bias: away from zero.</summary>
    public static TSelf IntegerRoundAwayFromZero<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.Sign(value) < 0 ? TSelf.Floor(value) : TSelf.Ceiling(value);

    /// <summary>PREVIEW! Common rounding: round up, bias: positive infinity.</summary>
    public static TSelf IntegerRoundCeiling<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.Ceiling(value);

    /// <summary>PREVIEW! Common rounding: round down, bias: negative infinity.</summary>
    public static TSelf IntegerRoundFloor<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.Floor(value);

    /// <summary>PREVIEW! Symmetric rounding: round down, bias: towards zero.</summary>
    public static TSelf IntegerRoundTowardZero<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.Truncate(value);
    #endregion Integer Rounding Extension Methods

    /// <summary>PREVIEW! Rounds the <paramref name="value"/> to the nearest integer. The <paramref name="mode"/> specifies which .NET built-in midpoint strategy to use when .</summary>
    public static TSelf Round<TSelf>(this TSelf value, System.MidpointRounding mode)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.Round(value, mode);

    /// <summary>PREVIEW! Rounds the <paramref name="value"/> to the nearest integer. The <paramref name="mode"/> specifies the strategy to use if the value is midway between two integers (e.g. 11.5).</summary>
    public static TSelf Round<TSelf>(this TSelf value, HalfwayRounding mode)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      var two = TSelf.CreateChecked(2);
      var halfOfOne = TSelf.CreateChecked(0.5);

      return mode switch
      {
        HalfwayRounding.ToEven => TSelf.Floor(value + halfOfOne) is var pi && pi % two != TSelf.Zero && value - TSelf.Floor(value) == halfOfOne ? pi - TSelf.One : pi,
        HalfwayRounding.AwayFromZero => HalfwayRoundUpZero(value),
        HalfwayRounding.TowardZero => HalfwayRoundDownZero(value),
        HalfwayRounding.ToNegativeInfinity => HalfwayRoundDown(value),
        HalfwayRounding.ToPositiveInfinity => HalfwayRoundUp(value),
        HalfwayRounding.ToOdd => TSelf.Floor(value + halfOfOne) is var pi && pi % two == TSelf.Zero && value - TSelf.Floor(value) == halfOfOne ? pi - TSelf.One : pi,
        _ => throw new System.ArgumentOutOfRangeException(nameof(mode)),
      };
    }

    /// <summary>PREVIEW! Rounds a <paramref name="value"/> to an integer boundary. The <paramref name="mode"/> specifies which strategy to use.</summary>
    public static TSelf Round<TSelf>(this TSelf value, IntegerRounding mode)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => mode switch
      {
        IntegerRounding.AwayFromZero => IntegerRoundAwayFromZero(value),
        IntegerRounding.Ceiling => IntegerRoundCeiling(value),
        IntegerRounding.Floor => IntegerRoundFloor(value),
        IntegerRounding.TowardZero => IntegerRoundTowardZero(value),
        _ => throw new System.ArgumentOutOfRangeException(nameof(mode)),
      };

    ///// <summary>PREVIEW! Symmetric rounding: round up, bias: away from zero.</summary>
    //public static TSelf IntegerRoundAwayFromZero<TSelf>(this TSelf value)
    //  where TSelf : System.Numerics.IFloatingPoint<TSelf>
    //  => TSelf.CopySign(TSelf.Ceiling(TSelf.Abs(value)), value);

    ///// <summary>PREVIEW! Symmetric rounding: round down, bias: towards zero.</summary>
    //public static TSelf IntegerRoundTowardZero<TSelf>(this TSelf value)
    //  where TSelf : System.Numerics.IFloatingPoint<TSelf>
    //  => TSelf.CopySign(TSelf.Floor(TSelf.Abs(value)), value);

    /// <summary>Wrapper for System.Math.Round that truncates the number at the specified number of fractional digits and then performs the rounding of the number. The reason for doing this is because unless a value is EXACTLY between two numbers, to the decimal, it will be rounded based on the next lower decimal digit and so on.</summary>
    /// <seealso cref="https://stackoverflow.com/questions/1423074/rounding-to-even-in-c-sharp"/>
    public static TSelf RoundToSignificantDigits<TSelf>(this TSelf value, int significantDigits, System.MidpointRounding mode)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IPowerFunctions<TSelf>
      => significantDigits >= 0 && TSelf.Pow(TSelf.CreateChecked(10), TSelf.CreateChecked(significantDigits + 1)) is var scalar
      ? TSelf.Round(TSelf.Truncate(value * scalar) / scalar, significantDigits, mode)
      : throw new System.ArgumentOutOfRangeException(nameof(significantDigits));
  }
}
#endif
