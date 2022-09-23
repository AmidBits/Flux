#if NET7_0_OR_GREATER
namespace Flux
{
    public static partial class Number
  {
    /// <summary>PREVIEW! Rounds a value to the nearest specified interval. The mode specifies how to round when between two intervals.</summary>
    public static TSelf RoundToMultiple<TSelf>(this TSelf value, TSelf multiple, HalfwayRounding mode)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      if (value % multiple is var remainder && remainder == TSelf.Zero)
        return value; // The number is already a multiple.

      var towardsZero = value - remainder;
      var awayFromZero = value < TSelf.Zero ? towardsZero - multiple : towardsZero + multiple;

      var intervalValue = awayFromZero - remainder;

      if (intervalValue > value)
        return value < TSelf.Zero ? awayFromZero : towardsZero;
      else if (intervalValue < value)
        return value < TSelf.Zero ? towardsZero : awayFromZero;
      else // Halfway rounding applies.
        return mode switch
        {
          HalfwayRounding.ToEven => TSelf.IsEvenInteger(towardsZero) ? towardsZero : awayFromZero,
          HalfwayRounding.AwayFromZero => awayFromZero,
          HalfwayRounding.TowardZero => towardsZero,
          HalfwayRounding.ToNegativeInfinity => value < TSelf.Zero ? awayFromZero : towardsZero,
          HalfwayRounding.ToPositiveInfinity => value < TSelf.Zero ? towardsZero : awayFromZero,
          HalfwayRounding.ToOdd => TSelf.IsOddInteger(awayFromZero) ? awayFromZero : towardsZero,
          _ => throw new System.ArgumentOutOfRangeException(nameof(mode)),
        };
    }

    /// <summary>PREVIEW! Rounds a value to the nearest specified interval. The mode specifies how to round when between two intervals.</summary>
    public static TSelf RoundToMultiple<TSelf>(this TSelf value, TSelf multiple, IntegerRounding mode)
      where TSelf : System.Numerics.INumber<TSelf>
    {
      if (value % multiple is var remainder && remainder == TSelf.Zero)
        return value; // The number is already a multiple.

      var towardsZero = value - remainder;
      var awayFromZero = value < TSelf.Zero ? towardsZero - multiple : towardsZero + multiple;

      return mode switch
      {
        IntegerRounding.AwayFromZero => awayFromZero,
        IntegerRounding.Ceiling => value < TSelf.Zero ? towardsZero : awayFromZero,
        IntegerRounding.Floor => value < TSelf.Zero ? awayFromZero : towardsZero,
        IntegerRounding.TowardZero => towardsZero,
        _ => throw new System.ArgumentOutOfRangeException(nameof(mode)),
      };
    }
  }
}
#endif
