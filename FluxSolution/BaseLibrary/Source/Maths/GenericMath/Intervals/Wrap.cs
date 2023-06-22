namespace Flux
{
  public static partial class Maths
  {
#if NET7_0_OR_GREATER

    /// <summary>Wraps an out-of-bound <paramref name="x"/> around the closed interval [<paramref name="min"/>, <paramref name="max"/>], until the value is back within range.</summary>
    public static TSelf Wrap<TSelf>(this TSelf x, TSelf min, TSelf max)
      where TSelf : System.Numerics.INumber<TSelf>
      => x < min
      ? max - (min - x) % (max - min)
      : x > max
      ? min + (x - min) % (max - min)
      : x;

#else

    /// <summary>Returns the <paramref name="value"/> indefinitely wrapped (overflowed) around the boundaries of the closed interval [<paramref name="min"/>, <paramref name="max"/>].</summary>
    public static System.Numerics.BigInteger Wrap(this System.Numerics.BigInteger value, System.Numerics.BigInteger min, System.Numerics.BigInteger max)
      => value < min ? max - (min - value) % (max - min) : value > max ? min + (value - min) % (max - min) : value;

    /// <summary>Returns the <paramref name="value"/> indefinitely wrapped (overflowed) around the boundaries of the closed interval [<paramref name="min"/>, <paramref name="max"/>].</summary>
    public static decimal Wrap(this decimal value, decimal min, decimal max)
      => value < min ? max - (min - value) % (max - min) : value > max ? min + (value - min) % (max - min) : value;

    /// <summary>Returns the <paramref name="value"/> indefinitely wrapped (overflowed) around the boundaries of the closed interval [<paramref name="min"/>, <paramref name="max"/>].</summary>
    public static float Wrap(this float value, float min, float max)
      => value < min ? max - (min - value) % (max - min) : value > max ? min + (value - min) % (max - min) : value;

    /// <summary>Returns the <paramref name="value"/> indefinitely wrapped (overflowed) around the boundaries of the closed interval [<paramref name="min"/>, <paramref name="max"/>].</summary>
    public static double Wrap(this double value, double min, double max)
      => value < min ? max - (min - value) % (max - min) : value > max ? min + (value - min) % (max - min) : value;

    /// <summary>Returns the <paramref name="value"/> indefinitely wrapped (overflowed) around the boundaries of the closed interval [<paramref name="min"/>, <paramref name="max"/>].</summary>
    public static int Wrap(this int value, int min, int max)
      => value < min ? max - (min - value) % (max - min) : value > max ? min + (value - min) % (max - min) : value;

    /// <summary>Returns the <paramref name="value"/> indefinitely wrapped (overflowed) around the boundaries of the closed interval [<paramref name="min"/>, <paramref name="max"/>].</summary>
    public static long Wrap(this long value, long min, long max)
      => value < min ? max - (min - value) % (max - min) : value > max ? min + (value - min) % (max - min) : value;

    /// <summary>Returns the <paramref name="value"/> indefinitely wrapped (overflowed) around the boundaries of the closed interval [<paramref name="min"/>, <paramref name="max"/>].</summary>
    [System.CLSCompliant(false)]
    public static uint Wrap(this uint value, uint min, uint max)
      => value < min ? max - (min - value) % (max - min) : value > max ? min + (value - min) % (max - min) : value;

    /// <summary>Returns the <paramref name="value"/> indefinitely wrapped (overflowed) around the boundaries of the closed interval [<paramref name="min"/>, <paramref name="max"/>].</summary>
    [System.CLSCompliant(false)]
    public static ulong Wrap(this ulong value, ulong min, ulong max)
      => value < min ? max - (min - value) % (max - min) : value > max ? min + (value - min) % (max - min) : value;

#endif
  }
}
