namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Folds an out-of-bound <paramref name="value"/> (back and forth) over the closed interval [<paramref name="min"/>, <paramref name="max"/>], until the <paramref name="value"/> is within the closed interval.</summary>
    public static TSelf Fold<TSelf>(this TSelf value, TSelf min, TSelf max)
      where TSelf : System.Numerics.INumber<TSelf>
      => (value > max)
      ? TSelf.IsEvenInteger(TruncMod(value - max, max - min, out var remHi)) ? max - remHi : min + remHi
      : (value < min)
      ? TSelf.IsEvenInteger(TruncMod(min - value, max - min, out var remLo)) ? min + remLo : max - remLo
      : value;
  }
}
