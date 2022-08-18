#if NET7_0_OR_GREATER
using Flux.AmbOps;
using Flux.Hashing;

namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Rounds the <paramref name="value"/> to the nearest integer. The <paramref name="mode"/> specifies how to round if it is midway between two numbers.</summary>
    public static TSelf Round<TSelf>(this TSelf value, HalfRounding mode)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IModulusOperators<TSelf, TSelf, TSelf>
    {
      var two = (TSelf.One + TSelf.One);
      var halfOne = TSelf.One / two;

      return mode switch
      {
        HalfRounding.ToEven => TSelf.Floor(value + halfOne) is var pi && pi % two != TSelf.Zero && value - TSelf.Floor(value) == halfOne ? pi - TSelf.One : pi,
        //HalfRounding.AwayFromZero => TSelf.Truncate(value + halfOne * value.Sign()),
        HalfRounding.AwayFromZero => value < TSelf.Zero ? TSelf.Ceiling(value - halfOne) : TSelf.Floor(value + halfOne),
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
