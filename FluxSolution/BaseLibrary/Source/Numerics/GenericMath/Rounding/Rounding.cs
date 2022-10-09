namespace Flux
{
  public interface INumberRoundable<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    TSelf RoundNumber(TSelf value);
  }

  public class Rounding<TSelf>
    : INumberRoundable<TSelf>
    where TSelf : System.Numerics.IFloatingPoint<TSelf>
  {
    public static Rounding<TSelf> ToEven() => new(Rounding.HalfwayToEven);
    public static Rounding<TSelf> ToAwayFromZero() => new(Rounding.HalfwayAwayFromZero);
    public static Rounding<TSelf> ToTowardZero() => new(Rounding.HalfwayTowardZero);
    public static Rounding<TSelf> ToNegativeInfinity() => new(Rounding.HalfwayToNegativeInfinity);
    public static Rounding<TSelf> ToPositiveInfinity() => new(Rounding.HalfwayToPositiveInfinity);
    public static Rounding<TSelf> ToOdd() => new(Rounding.HalfwayToOdd);

    public static Rounding<TSelf> Envelop() => new(Rounding.Envelop);
    public static Rounding<TSelf> Truncate() => new(Rounding.Truncate);
    public static Rounding<TSelf> Ceiling() => new(Rounding.Ceiling);
    public static Rounding<TSelf> Floor() => new(Rounding.Floor);

    private readonly Rounding m_mode;

    public Rounding(Rounding mode)
      => m_mode = mode;

    public TSelf RoundNumber(TSelf x)
    {
      var two = TSelf.CreateChecked(2);
      var halfOfOne = TSelf.CreateChecked(0.5);

      return m_mode switch
      {
        Rounding.HalfwayToEven => TSelf.Floor(x + halfOfOne) is var pi && !TSelf.IsZero(pi % two) && x - TSelf.Floor(x) == halfOfOne ? pi - TSelf.One : pi,
        Rounding.HalfwayAwayFromZero => HalfwayRoundUpZero(x),
        Rounding.HalfwayTowardZero => HalfwayRoundDownZero(x),
        Rounding.HalfwayToNegativeInfinity => HalfwayRoundDown(x),
        Rounding.HalfwayToPositiveInfinity => HalfwayRoundUp(x),
        Rounding.HalfwayToOdd => TSelf.Floor(x + halfOfOne) is var pi && TSelf.IsZero(pi % two) && x - TSelf.Floor(x) == halfOfOne ? pi - TSelf.One : pi,
        Rounding.Envelop => IntegerRoundEnvelop(x),
        Rounding.Ceiling => IntegerRoundCeiling(x),
        Rounding.Floor => IntegerRoundFloor(x),
        Rounding.Truncate => IntegerRoundTruncate(x),
        _ => throw new System.ArgumentOutOfRangeException(m_mode.ToString()),
      };
    }

    #region Halfway Rounding Extension Methods
    /// <summary>PREVIEW! Common rounding: round half down, bias: negative infinity.</summary>
    public static TSelf HalfwayRoundDown(TSelf x)
      => TSelf.Ceiling(x - (TSelf.One.Div2()));

    /// <summary>PREVIEW! Symmetric rounding: round half down, bias: towards zero.</summary>
    public static TSelf HalfwayRoundDownZero(TSelf x)
      => TSelf.CopySign(HalfwayRoundDown(TSelf.Abs(x)), x);

    /// <summary>PREVIEW! Common rounding: round half up, bias: positive infinity.</summary>
    public static TSelf HalfwayRoundUp(TSelf x)
      => TSelf.Floor(x + (TSelf.One.Div2()));

    /// <summary>PREVIEW! Symmetric rounding: round half up, bias: away from zero.</summary>
    public static TSelf HalfwayRoundUpZero(TSelf x)
      => TSelf.CopySign(HalfwayRoundUp(TSelf.Abs(x)), x);
    #endregion Halfway Rounding Extension Methods

    #region Integer Rounding Extension Methods
    /// <summary>PREVIEW! Symmetric rounding: round up, bias: away from zero.</summary>
    public static TSelf IntegerRoundEnvelop(TSelf x)
      => TSelf.Sign(x) < 0 ? TSelf.Floor(x) : TSelf.Ceiling(x);

    /// <summary>PREVIEW! Common rounding: round up, bias: positive infinity.</summary>
    public static TSelf IntegerRoundCeiling(TSelf x)
     => TSelf.Ceiling(x);

    /// <summary>PREVIEW! Common rounding: round down, bias: negative infinity.</summary>
    public static TSelf IntegerRoundFloor(TSelf x)
      => TSelf.Floor(x);

    /// <summary>PREVIEW! Symmetric rounding: round down, bias: towards zero.</summary>
    public static TSelf IntegerRoundTruncate(TSelf x)
      => TSelf.Truncate(x);
    #endregion Integer Rounding Extension Methods
  }

  public class BoundaryRounding<TSelf>
    : INumberRoundable<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    private readonly Rounding m_mode;
    private readonly TSelf m_boundaryTowardsZero;
    private readonly TSelf m_boundaryAwayFromZero;

    public BoundaryRounding(Rounding mode, TSelf boundaryTowardsZero, TSelf boundaryAwayFromZero)
    {
      m_mode = mode;
      m_boundaryTowardsZero = boundaryTowardsZero;
      m_boundaryAwayFromZero = boundaryAwayFromZero;
    }

    /// <summary>PREVIEW! Rounds a value to the nearest boundary. The distance computation is a slight optimization for special cases, e.g. when rounding to multiple of. The mode specifies how to round when between two intervals.</summary>
    public static TSelf Round(TSelf x, TSelf boundaryTowardsZero, TSelf boundaryAwayFromZero, Rounding mode, TSelf distanceTowardsZero, TSelf distanceAwayFromZero)
    {
      if (distanceTowardsZero < distanceAwayFromZero) // It's a clear win for towardsZero.
        return boundaryTowardsZero;
      if (distanceAwayFromZero < distanceTowardsZero) // It's a clear win for awayFromZero.
        return boundaryAwayFromZero;

      return mode switch // It's exactly halfway, use appropriate rounding to resolve winner.
      {
        Rounding.HalfwayToEven => TSelf.IsEvenInteger(boundaryTowardsZero) ? boundaryTowardsZero : boundaryAwayFromZero,
        Rounding.HalfwayAwayFromZero => boundaryAwayFromZero,
        Rounding.HalfwayTowardZero => boundaryTowardsZero,
        Rounding.HalfwayToNegativeInfinity => x < TSelf.Zero ? boundaryAwayFromZero : boundaryTowardsZero,
        Rounding.HalfwayToPositiveInfinity => x < TSelf.Zero ? boundaryTowardsZero : boundaryAwayFromZero,
        Rounding.HalfwayToOdd => TSelf.IsOddInteger(boundaryAwayFromZero) ? boundaryAwayFromZero : boundaryTowardsZero,
        Rounding.Envelop => boundaryAwayFromZero,
        Rounding.Truncate => boundaryTowardsZero,
        Rounding.Floor => x < TSelf.Zero ? boundaryAwayFromZero : boundaryTowardsZero,
        Rounding.Ceiling => x < TSelf.Zero ? boundaryTowardsZero : boundaryAwayFromZero,
        _ => throw new System.ArgumentOutOfRangeException(mode.ToString()),
      };
    }

    public static TSelf Round(TSelf x, TSelf boundaryTowardsZero, TSelf boundaryAwayFromZero, Rounding mode)
    {
      var distanceTowardsZero = TSelf.Abs(x - TSelf.CreateChecked(boundaryTowardsZero)); // Distance from value to the boundary towardsZero.
      var distanceAwayFromZero = TSelf.Abs(TSelf.CreateChecked(boundaryAwayFromZero) - x); // Distance from value to the boundary awayFromZero;

      return Round(x, boundaryTowardsZero, boundaryAwayFromZero, mode, distanceTowardsZero, distanceAwayFromZero);
    }

    /// <summary>PREVIEW! Rounds a value to the nearest boundary. The mode specifies how to round when between two intervals.</summary>
    public TSelf RoundNumber(TSelf x)
    {
      var distanceTowardsZero = TSelf.Abs(x - TSelf.CreateChecked(m_boundaryTowardsZero)); // Distance from value to the boundary towardsZero.
      var distanceAwayFromZero = TSelf.Abs(TSelf.CreateChecked(m_boundaryAwayFromZero) - x); // Distance from value to the boundary awayFromZero;

      return Round(x, m_boundaryTowardsZero, m_boundaryAwayFromZero, m_mode, distanceTowardsZero, distanceAwayFromZero);
    }
  }

  public class RoundToMultiple<TSelf>
    : INumberRoundable<TSelf>
    where TSelf : System.Numerics.INumber<TSelf>
  {
    private readonly Rounding m_mode;
    private readonly TSelf m_multiple;
    private readonly bool m_proper;

    public RoundToMultiple(Rounding mode, TSelf multiple, bool proper)
    {
      m_mode = mode;
      m_multiple = multiple;
      m_proper = proper;
    }

    /// <summary>PREVIEW! Get the two multiples nearest to value.</summary>
    /// <param name="x">The value for which the nearest multiples of will be found.</param>
    /// <param name="multiple">The multiple to which the results will align.</param>
    /// <param name="proper">Proper means nearest but do not include x if it's a multiple-of, i.e. the two multiple-of will be properly nearest (but not the same), or LT/GT rather than LTE/GTE.</param>
    /// <param name="nearestTowardsZero">Outputs the multiple of that is closer to zero.</param>
    /// <param name="nearestAwayFromZero">Outputs the multiple of that is farther from zero.</param>
    /// <returns>The nearest two multiples to value as out parameters.</returns>
    public static void FindNearestMultiples(TSelf x, TSelf multiple, bool proper, out TSelf nearestTowardsZero, out TSelf nearestAwayFromZero)
    {
      var offsetTowardsZero = x % multiple;

      nearestTowardsZero = x - offsetTowardsZero;
      nearestAwayFromZero = TSelf.IsNegative(x) ? nearestTowardsZero - multiple : nearestTowardsZero + multiple;

      if (proper)
      {
        if (nearestTowardsZero == x)
          nearestTowardsZero -= multiple;
        if (nearestAwayFromZero == x)
          nearestAwayFromZero -= multiple;
      }
    }

    /// <summary>PREVIEW! Find the nearest (to <paramref name="x"/>) of two multiples, using the specified <see cref="HalfRounding"/> <paramref name="mode"/> to resolve any halfway conflict, and also return both multiples as out parameters.</summary>
    /// <param name="x">The value for which the nearest multiples of will be found.</param>
    /// <param name="multiple">The multiple to which the results will align.</param>
    /// <param name="proper">Proper means nearest but do not include x if it's a multiple-of, i.e. the two multiple-of will be properly nearest (but not the same), or LT/GT rather than LTE/GTE.</param>
    /// <param name="mode"></param>
    /// <param name="nearestTowardsZero">Outputs the multiple of that is closer to zero.</param>
    /// <param name="nearestAwayFromZero">Outputs the multiple of that is farther from zero.</param>
    /// <returns>The nearest two multiples to value.</returns>
    public static TSelf GetNearestMultiple(TSelf x, TSelf multiple, bool proper, Rounding mode, out TSelf nearestTowardsZero, out TSelf nearestAwayFromZero)
    {
      FindNearestMultiples(x, multiple, proper, out nearestTowardsZero, out nearestAwayFromZero);

      return new BoundaryRounding<TSelf>(mode, nearestTowardsZero, nearestAwayFromZero).RoundNumber(x);
    }

    /// <summary>PREVIEW! Rounds a value to the nearest boundary. The mode specifies how to round when between two intervals.</summary>
    public TSelf RoundNumber(TSelf x)
      => GetNearestMultiple(x, m_multiple, m_proper, m_mode, out var _, out var _);
  }

  public class RoundToPrecision<TSelf>
    : INumberRoundable<TSelf>
    where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IPowerFunctions<TSelf>
  {
    private readonly Rounding m_mode;
    private readonly int m_significantDigits;

    public RoundToPrecision(Rounding mode, int significantDigits)
    {
      m_mode = mode;
      m_significantDigits = significantDigits;
    }

    /// <summary>PREVIEW! Rounds the <paramref name="x"/> to the nearest integer. The <paramref name="mode"/> specifies the halfway rounding strategy to use if the value is halfway between two integers (e.g. 11.5).</summary>
    /// <remarks>var r = Flux.GenericMath.TruncatingRound(99.96535789, 2, HalfwayRounding.ToEven); // = 99.97 (compare with the corresponding TruncatingRound method)</remarks>
    public static TSelf PrecisionRound(TSelf x, int significantDigits, Rounding mode)
      => significantDigits >= 0 && TSelf.Pow(TSelf.CreateChecked(10), TSelf.CreateChecked(significantDigits)) is var scalar
      ? new Rounding<TSelf>(mode).RoundNumber(x * scalar) / scalar
      : throw new System.ArgumentOutOfRangeException(nameof(significantDigits));

    public TSelf RoundNumber(TSelf value)
      => PrecisionRound(value, m_significantDigits, m_mode);
  }

  public class TruncatingRoundToPrecision<TSelf>
  : INumberRoundable<TSelf>
  where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IPowerFunctions<TSelf>
  {
    private readonly Rounding m_mode;
    private readonly int m_significantDigits;

    public TruncatingRoundToPrecision(Rounding mode, int significantDigits)
    {
      m_mode = mode;
      m_significantDigits = significantDigits;
    }
    /// <summary>Rounds <paramref name="x"/> by truncating to the specified number of <paramref name="significantDigits"> decimal digits</paramref> and then round using the <paramref name="mode"/>. The reason for doing this is because unless a value is EXACTLY between two numbers, to the decimal, it will be rounded based on the next least significant decimal digit and so on.</summary>
    /// <seealso cref="https://stackoverflow.com/questions/1423074/rounding-to-even-in-c-sharp"/>
    /// <remarks>var r = Flux.GenericMath.TruncatingRound(99.96535789, 2, HalfwayRounding.ToEven); // = 99.96 (compare with the corresponding Round method)</remarks>
    public static TSelf TruncatingRound(TSelf x, int significantDigits, Rounding mode)
      => significantDigits >= 0 && TSelf.Pow(TSelf.CreateChecked(10), TSelf.CreateChecked(significantDigits + 1)) is var scalar
      ? new RoundToPrecision<TSelf>(mode, significantDigits).RoundNumber(TSelf.Truncate(x * scalar) / scalar)
      : throw new System.ArgumentOutOfRangeException(nameof(significantDigits));

    public TSelf RoundNumber(TSelf value)
      => TruncatingRound(value, m_significantDigits, m_mode);
  }

}
