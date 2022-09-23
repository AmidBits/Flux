#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Returns the 1-dimensional distance between the two specified values.</summary>
    public static TSelf Distance<TSelf>(this TSelf self, TSelf other)
      where TSelf : System.Numerics.INumberBase<TSelf>
      => TSelf.Abs(other - self);

    /// <summary>PREVIEW! Folds an out-of-bound <paramref name="self"/> over across the interval, back and forth, between the closed interval [<paramref name="min"/>, <paramref name="max"/>], until the value is back within range.</summary>
    public static TSelf Fold<TSelf>(this TSelf self, TSelf min, TSelf max)
      where TSelf : System.Numerics.INumber<TSelf>
      => (self > max)
      ? TSelf.IsEvenInteger(TruncDivRem(self - max, max - min, out var remainderHi)) ? max - remainderHi : min + remainderHi
      : (self < min)
      ? TSelf.IsEvenInteger(TruncDivRem(min - self, max - min, out var remainderLo)) ? min + remainderLo : max - remainderLo
      : self;

    /// <summary>PREVIEW! Proportionally re-scale the <paramref name="self"/> from within one closed interval [<paramref name="minSource"/>, <paramref name="maxSource"/>] to within another closed interval [<paramref name="minTarget"/>, <paramref name="maxTarget"/>]. The value retains its interval ratio. E.g. a 5 in the range [0, 10] becomes 50 when rescaled to the range [0, 100].</summary>
    public static TSelf Rescale<TSelf>(this TSelf self, TSelf minSource, TSelf maxSource, TSelf minTarget, TSelf maxTarget)
      where TSelf : System.Numerics.INumberBase<TSelf>
      => (maxTarget - minTarget) * (self - minSource) / (maxSource - minSource) + minTarget;

    /// <summary>PREVIEW! Returns the <paramref name="self"/> indefinitely wrapped (overflowed) around the boundaries of the closed interval [<paramref name="min"/>, <paramref name="max"/>].</summary>
    public static TSelf Wrap<TSelf>(this TSelf self, TSelf min, TSelf max)
      where TSelf : System.Numerics.INumber<TSelf>
      => self < min
      ? max - (min - self) % (max - min)
      : self > max
      ? min + (self - min) % (max - min)
      : self;

  }
}
#endif
