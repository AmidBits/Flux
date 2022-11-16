namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Folds an out-of-bound <paramref name="x"/> over across the interval, back and forth, between the closed interval [<paramref name="min"/>, <paramref name="max"/>], until the value is back within range.</summary>
    public static TSelf Fold<TSelf>(this TSelf x, TSelf min, TSelf max)
      where TSelf : System.Numerics.INumber<TSelf>
      => (x > max)
      ? TSelf.IsEvenInteger(TruncMod(x - max, max - min, out var remainderHi)) ? max - remainderHi : min + remainderHi
      : (x < min)
      ? TSelf.IsEvenInteger(TruncMod(min - x, max - min, out var remainderLo)) ? min + remainderLo : max - remainderLo
      : x;
  }
}
