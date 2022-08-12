#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Returns the 1-dimensional distance between the two specified values.</summary>
    public static TSelf Distance<TSelf>(this TSelf a, TSelf b)
      where TSelf : System.Numerics.IBinaryNumber<TSelf>
      => TSelf.Abs(b - a);

    /// <summary>Folds an out-of-bound <paramref name="value"/> over across the interval, back and forth, between the closed interval [<paramref name="min"/>, <paramref name="max"/>], until the value is back within range.</summary>
    public static TSelf Fold<TSelf>(this TSelf value, TSelf min, TSelf max)
      where TSelf : System.Numerics.IBinaryNumber<TSelf>
    {
      if (value > max)
      {
        var magnitude = value - max;
        var range = max - min;

        return ((magnitude / range) & TSelf.One) == TSelf.Zero ? max - (magnitude % range) : min + (magnitude % range);
      }
      else if (value < min)
      {
        var magnitude = min - value;
        var range = max - min;

        return ((magnitude / range) & TSelf.One) == TSelf.Zero ? min + (magnitude % range) : max - (magnitude % range);
      }

      return value;
    }

    /// <summary>Proportionally re-scale the <paramref name="value"/> from within one closed interval [<paramref name="minSource"/>, <paramref name="maxSource"/>] to within another closed interval [<paramref name="minTarget"/>, <paramref name="maxTarget"/>]. The value retains its interval ratio. E.g. a 5 in the range [0, 10] becomes 50 when rescaled to the range [0, 100].</summary>
    public static TSelf Rescale<TSelf>(this TSelf value, TSelf minSource, TSelf maxSource, TSelf minTarget, TSelf maxTarget)
      where TSelf : System.Numerics.IBinaryNumber<TSelf>
      => (maxTarget - minTarget) * (value - minSource) / (maxSource - minSource) + minTarget;

    /// <summary>Returns the <paramref name="value"/> indefinitely wrapped (overflowed) around the boundaries of the closed interval [<paramref name="min"/>, <paramref name="max"/>].</summary>
    public static TSelf Wrap<TSelf>(this TSelf value, TSelf min, TSelf max)
      where TSelf : System.Numerics.IBinaryNumber<TSelf>
      => value < min ? max - (min - value) % (max - min) : value > max ? min + (value - min) % (max - min) : value;

  }
}
#endif
