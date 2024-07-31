namespace Flux
{
  public static partial class Fx
  {
    /// <summary>Wraps an out-of-bound <paramref name="value"/> around the closed interval [<paramref name="min"/>, <paramref name="max"/>], until the <paramref name="value"/> is within the closed interval.</summary>
    public static TSelf Wrap<TSelf>(this TSelf value, TSelf min, TSelf max)
      where TSelf : System.Numerics.INumber<TSelf>
      => value < min
      ? max - (min - value) % (max - min)
      : value > max
      ? min + (value - min) % (max - min)
      : value;
  }
}
