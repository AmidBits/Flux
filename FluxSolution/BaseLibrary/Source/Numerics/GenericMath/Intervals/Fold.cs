#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Folds an out-of-bound <paramref name="self"/> over across the interval, back and forth, between the closed interval [<paramref name="min"/>, <paramref name="max"/>], until the value is back within range.</summary>
    public static TSelf Fold<TSelf>(this TSelf self, TSelf min, TSelf max)
      where TSelf : System.Numerics.INumber<TSelf>
      => (self > max)
      ? TSelf.IsEvenInteger(TruncDivRem(self - max, max - min, out var remainderHi)) ? max - remainderHi : min + remainderHi
      : (self < min)
      ? TSelf.IsEvenInteger(TruncDivRem(min - self, max - min, out var remainderLo)) ? min + remainderLo : max - remainderLo
      : self;
  }
}
#endif
