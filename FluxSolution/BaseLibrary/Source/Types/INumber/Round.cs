#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Symmetric rounding: round down, bias: towards zero.</summary>
    public static TSelf RoundFloorZero<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.Floor(TSelf.Abs(value)) is var result && value < TSelf.Zero ? -result : result;

    /// <summary>Symmetric rounding: round up, bias: away from zero.</summary>
    public static TSelf RoundCeilingZero<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.Ceiling(TSelf.Abs(value)) is var result && value < TSelf.Zero ? -result : result;

    /// <summary>Common rounding: round half down, bias: negative infinity.</summary>
    public static TSelf RoundHalfDown<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.Ceiling(value - (TSelf.One / (TSelf.One + TSelf.One)));

    /// <summary>Symmetric rounding: round half down, bias: towards zero.</summary>
    public static TSelf RoundHalfDownZero<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => RoundHalfDown(TSelf.Abs(value)) is var result && value < TSelf.Zero ? -result : result;

    /// <summary>Common rounding: round half up, bias: positive infinity.</summary>
    public static TSelf RoundHalfUp<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => TSelf.Floor(value + (TSelf.One / (TSelf.One + TSelf.One)));

    /// <summary>Symmetric rounding: round half up, bias: away from zero.</summary>
    public static TSelf RoundHalfUpZero<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>
      => RoundHalfUp(TSelf.Abs(value)) is var result && value < TSelf.Zero ? -result : result;

    /// <summary>Rounds the <paramref name="value"/> to the nearest integer. The <paramref name="mode"/> specifies how to round if it is midway between two numbers.</summary>
    public static TSelf Round<TSelf>(this TSelf value, HalfRounding mode)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IModulusOperators<TSelf, TSelf, TSelf>
    {
      var two = (TSelf.One + TSelf.One);
      var halfOne = TSelf.One / two;

      return mode switch
      {
        HalfRounding.ToEven => TSelf.Floor(value + halfOne) is var pi && pi % two != TSelf.Zero && value - TSelf.Floor(value) == halfOne ? pi - TSelf.One : pi,
        HalfRounding.AwayFromZero => TSelf.Truncate(value + halfOne * value.Sign()),
        //HalfRounding.AwayFromZero => value < TSelf.Zero ? TSelf.Ceiling(value - halfOne) : TSelf.Floor(value + halfOne),
        HalfRounding.TowardZero => value < TSelf.Zero ? TSelf.Floor(value + halfOne) : TSelf.Ceiling(value - halfOne),
        HalfRounding.ToNegativeInfinity => TSelf.Ceiling(value - halfOne),
        HalfRounding.ToPositiveInfinity => TSelf.Floor(value + halfOne),
        HalfRounding.ToOdd => TSelf.Floor(value + halfOne) is var pi && pi % two == TSelf.Zero && value - TSelf.Floor(value) == halfOne ? pi - TSelf.One : pi,
        _ => throw new System.ArgumentOutOfRangeException(nameof(mode)),
      };
    }

    /// <summary>Rounds a value to the nearest specified interval. The mode specifies how to round when between two intervals.</summary>
    public static TSelf RoundToMultiple<TSelf>(TSelf number, TSelf interval, System.MidpointRounding mode)
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
