#if INumber
namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Returns the 1-dimensional distance between the two specified values.</summary>
    public static TSelf Distance<TSelf>(TSelf a, TSelf b)
      where TSelf : INumber<TSelf>
      => TSelf.Abs(b - a);

    /// <summary>Folds an out-of-bound <paramref name="value"/> over across the interval, back and forth, between the closed interval [<paramref name="min"/>, <paramref name="max"/>], until the value is back within range.</summary>
    //public static TSelf Fold<TSelf>(TSelf value, TSelf min, TSelf max)
    //  where TSelf : INumber<TSelf>
    //{
    //  if (value > max)
    //    return (TSelf.DivRem(value - max, max - min) is var (quotient, remainder) && quotient & 1) == 0 ? max - remainder : min + remainder;
    //  else if (value < min)
    //    return (TSelf.DivRem(min - value, max - min) is var (quotient, remainder) && quotient & 1) == 0 ? min + remainder : max - remainder;
    //  return value;
    //}

    /// <summary>Proportionally re-scale the <paramref name="value"/> from within one closed interval [<paramref name="minSource"/>, <paramref name="maxSource"/>] to within another closed interval [<paramref name="minTarget"/>, <paramref name="maxTarget"/>]. The value retains its interval ratio. E.g. a 5 in the range [0, 10] becomes 50 when rescaled to the range [0, 100].</summary>
    public static TSelf Rescale<TSelf>(this TSelf value, TSelf minSource, TSelf maxSource, TSelf minTarget, TSelf maxTarget)
      where TSelf : INumber<TSelf>
      => (maxTarget - minTarget) * (value - minSource) / (maxSource - minSource) + minTarget;

    /// <summary>Returns the <paramref name="value"/> indefinitely wrapped (overflowed) around the boundaries of the closed interval [<paramref name="min"/>, <paramref name="max"/>].</summary>
    public static TSelf Wrap<TSelf>(this TSelf value, TSelf min, TSelf max)
      where TSelf : INumber<TSelf>
      => value < min ? max - (min - value) % (max - min) : value > max ? min + (value - min) % (max - min) : value;

  }
}
#endif
