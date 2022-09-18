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
    public static TSelf IntegerRoundAwayFromZero<TSelf>(this TSelf number)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.Sign(number) < 0 ? TSelf.Floor(number) : TSelf.Ceiling(number);

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

    /// <summary>PREVIEW! Rounds the <paramref name="value"/> to the nearest integer. The <paramref name="mode"/> specifies how to round if it is midway between two numbers.</summary>
    public static TSelf Round<TSelf>(this TSelf value, HalfwayRounding mode)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
    {
      var two = TSelf.One + TSelf.One;
      var halfOfOne = TSelf.One / two;

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

    /// <summary>PREVIEW! Rounds a <paramref name="number"/> to an integer boundary. The <paramref name="mode"/> specifies what method to use.</summary>
    public static TSelf Round<TSelf>(this TSelf number, IntegerRounding mode)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => mode switch
      {
        IntegerRounding.AwayFromZero => IntegerRoundAwayFromZero(number),
        IntegerRounding.Ceiling => IntegerRoundCeiling(number),
        IntegerRounding.Floor => IntegerRoundFloor(number),
        IntegerRounding.TowardZero => IntegerRoundTowardZero(number),
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

    /// <summary>PREVIEW! Rounds a value to the nearest specified interval. The mode specifies how to round when between two intervals.</summary>
    public static TSelf RoundToMultiple<TSelf>(this TSelf number, TSelf interval, System.MidpointRounding mode)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.IModulusOperators<TSelf, TSelf, TSelf>
    {
      if (number % interval is var remainder && remainder == TSelf.Zero)
        return number; // The number is already a multiple.

      var roundedToZero = number - remainder;

      return mode switch
      {
        System.MidpointRounding.AwayFromZero => number < TSelf.Zero ? roundedToZero - interval : roundedToZero + interval,
        System.MidpointRounding.ToPositiveInfinity => number < TSelf.Zero ? roundedToZero : roundedToZero + interval,
        System.MidpointRounding.ToNegativeInfinity => number < TSelf.Zero ? roundedToZero - interval : roundedToZero,
        System.MidpointRounding.ToZero => roundedToZero,
        System.MidpointRounding.ToEven => TSelf.IsEvenInteger(roundedToZero) ? roundedToZero : roundedToZero < TSelf.Zero && roundedToZero - interval is var n && TSelf.IsEvenInteger(n) ? n : roundedToZero > TSelf.Zero && roundedToZero + interval is var p && TSelf.IsEvenInteger(p) ? p : throw new System.ArgumentException("The number and interval cannot evaluate to even."),
        _ => throw new System.ArgumentOutOfRangeException(nameof(mode)),
      };
    }
  }
}
#endif
