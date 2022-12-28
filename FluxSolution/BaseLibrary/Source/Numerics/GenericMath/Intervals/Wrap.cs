namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Returns the <paramref name="x"/> indefinitely wrapped (overflowed) around the closed interval bounds [<paramref name="min"/>, <paramref name="max"/>].</summary>
    public static TSelf Wrap<TSelf>(this TSelf x, TSelf min, TSelf max)
      where TSelf : System.Numerics.INumber<TSelf>
      => x < min
      ? max - (min - x) % (max - min)
      : x > max
      ? min + (x - min) % (max - min)
      : x;
  }
}
