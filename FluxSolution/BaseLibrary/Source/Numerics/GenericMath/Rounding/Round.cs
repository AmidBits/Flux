#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    #region Halfway Rounding Extension Methods
    /// <summary>PREVIEW! Common rounding: round half down, bias: negative infinity.</summary>
    public static TSelf HalfwayRoundDown<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.Ceiling(x - (TSelf.One / (TSelf.One + TSelf.One)));

    /// <summary>PREVIEW! Symmetric rounding: round half down, bias: towards zero.</summary>
    public static TSelf HalfwayRoundDownZero<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.CopySign(HalfwayRoundDown(TSelf.Abs(x)), x);

    /// <summary>PREVIEW! Common rounding: round half up, bias: positive infinity.</summary>
    public static TSelf HalfwayRoundUp<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.Floor(x + (TSelf.One / (TSelf.One + TSelf.One)));

    /// <summary>PREVIEW! Symmetric rounding: round half up, bias: away from zero.</summary>
    public static TSelf HalfwayRoundUpZero<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.CopySign(HalfwayRoundUp(TSelf.Abs(x)), x);
    #endregion Halfway Rounding Extension Methods

    #region Integer Rounding Extension Methods
    /// <summary>PREVIEW! Symmetric rounding: round up, bias: away from zero.</summary>
    public static TSelf IntegerRoundAwayFromZero<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.Sign(x) < 0 ? TSelf.Floor(x) : TSelf.Ceiling(x);

    /// <summary>PREVIEW! Common rounding: round up, bias: positive infinity.</summary>
    public static TSelf IntegerRoundCeiling<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.Ceiling(x);

    /// <summary>PREVIEW! Common rounding: round down, bias: negative infinity.</summary>
    public static TSelf IntegerRoundFloor<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.Floor(x);

    /// <summary>PREVIEW! Symmetric rounding: round down, bias: towards zero.</summary>
    public static TSelf IntegerRoundTowardZero<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.Truncate(x);
    #endregion Integer Rounding Extension Methods

    /// <summary>PREVIEW! Rounds the <paramref name="x"/> to the nearest integer. The <paramref name="mode"/> specifies the halfway rounding strategy to use if the value is halfway between two integers (e.g. 11.5).</summary>
    public static TSelf Round<TSelf>(this TSelf x, HalfwayRounding mode)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      var two = TSelf.CreateChecked(2);
      var halfOfOne = TSelf.CreateChecked(0.5);

      return mode switch
      {
        HalfwayRounding.ToEven => TSelf.Floor(x + halfOfOne) is var pi && !TSelf.IsZero(pi % two) && x - TSelf.Floor(x) == halfOfOne ? pi - TSelf.One : pi,
        HalfwayRounding.AwayFromZero => HalfwayRoundUpZero(x),
        HalfwayRounding.TowardZero => HalfwayRoundDownZero(x),
        HalfwayRounding.ToNegativeInfinity => HalfwayRoundDown(x),
        HalfwayRounding.ToPositiveInfinity => HalfwayRoundUp(x),
        HalfwayRounding.ToOdd => TSelf.Floor(x + halfOfOne) is var pi && TSelf.IsZero(pi % two) && x - TSelf.Floor(x) == halfOfOne ? pi - TSelf.One : pi,
        _ => throw new System.ArgumentOutOfRangeException(nameof(mode)),
      };
    }

    /// <summary>PREVIEW! Rounds the <paramref name="x"/> to the nearest integer. The <paramref name="mode"/> specifies the halfway rounding strategy to use if the value is halfway between two integers (e.g. 11.5).</summary>
    public static TSelf Round<TSelf>(this TSelf x, int significantDigits, HalfwayRounding mode)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IPowerFunctions<TSelf>
      => significantDigits >= 0 && TSelf.Pow(TSelf.CreateChecked(10), TSelf.CreateChecked(significantDigits)) is var scalar
      ? Round(x * scalar, mode) / scalar
      : throw new System.ArgumentOutOfRangeException(nameof(significantDigits));

    /// <summary>PREVIEW! Rounds <paramref name="x"/> to an integer boundary. The <paramref name="mode"/> specifies which full rounding strategy to use. Full rounding in this context means to directly force an integer rounding, i.e. like the traditional Truncate, Ceiling, etc.</summary>
    public static TSelf Round<TSelf>(this TSelf x, FullRounding mode)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => mode switch
      {
        FullRounding.AwayFromZero => IntegerRoundAwayFromZero(x),
        FullRounding.Ceiling => IntegerRoundCeiling(x),
        FullRounding.Floor => IntegerRoundFloor(x),
        FullRounding.TowardZero => IntegerRoundTowardZero(x),
        _ => throw new System.ArgumentOutOfRangeException(nameof(mode)),
      };

    /// <summary>PREVIEW! Rounds the <paramref name="x"/> to the nearest integer. The <paramref name="mode"/> specifies the halfway rounding strategy to use if the value is halfway between two integers (e.g. 11.5).</summary>
    public static TSelf Round<TSelf>(this TSelf x, int significantDigits, FullRounding mode)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IPowerFunctions<TSelf>
      => significantDigits >= 0 && TSelf.Pow(TSelf.CreateChecked(10), TSelf.CreateChecked(significantDigits)) is var scalar
      ? Round(x * scalar, mode) / scalar
      : throw new System.ArgumentOutOfRangeException(nameof(significantDigits));

    ///// <summary>PREVIEW! Symmetric rounding: round up, bias: away from zero.</summary>
    //public static TSelf IntegerRoundAwayFromZero<TSelf>(this TSelf value)
    //  where TSelf : System.Numerics.IFloatingPoint<TSelf>
    //  => TSelf.CopySign(TSelf.Ceiling(TSelf.Abs(value)), value);

    ///// <summary>PREVIEW! Symmetric rounding: round down, bias: towards zero.</summary>
    //public static TSelf IntegerRoundTowardZero<TSelf>(this TSelf value)
    //  where TSelf : System.Numerics.IFloatingPoint<TSelf>
    //  => TSelf.CopySign(TSelf.Floor(TSelf.Abs(value)), value);
  }
}
#endif
