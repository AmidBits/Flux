namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Returns the <paramref name="value"/> indefinitely wrapped (overflowed) around the boundaries of the closed interval [<paramref name="min"/>, <paramref name="max"/>].</summary>
    public static System.Numerics.BigInteger Wrap(System.Numerics.BigInteger value, System.Numerics.BigInteger min, System.Numerics.BigInteger max)
      => value < min ? max - (min - value) % (max - min) : value > max ? min + (value - min) % (max - min) : value;

    /// <summary>Returns the <paramref name="value"/> indefinitely wrapped (overflowed) around the boundaries of the closed interval [<paramref name="min"/>, <paramref name="max"/>].</summary>
    public static decimal Wrap(decimal value, decimal min, decimal max)
      => value < min ? max - (min - value) % (max - min) : value > max ? min + (value - min) % (max - min) : value;

    /// <summary>Returns the <paramref name="value"/> indefinitely wrapped (overflowed) around the boundaries of the closed interval [<paramref name="min"/>, <paramref name="max"/>].</summary>
    public static float Wrap(float value, float min, float max)
      => value < min ? max - (min - value) % (max - min) : value > max ? min + (value - min) % (max - min) : value;
    /// <summary>Returns the <paramref name="value"/> indefinitely wrapped (overflowed) around the boundaries of the closed interval [<paramref name="min"/>, <paramref name="max"/>].</summary>
    public static double Wrap(double value, double min, double max)
      => value < min ? max - (min - value) % (max - min) : value > max ? min + (value - min) % (max - min) : value;

    /// <summary>Returns the <paramref name="value"/> indefinitely wrapped (overflowed) around the boundaries of the closed interval [<paramref name="min"/>, <paramref name="max"/>].</summary>
    public static int Wrap(int value, int min, int max)
      => value < min ? max - (min - value) % (max - min) : value > max ? min + (value - min) % (max - min) : value;
    /// <summary>Returns the <paramref name="value"/> indefinitely wrapped (overflowed) around the boundaries of the closed interval [<paramref name="min"/>, <paramref name="max"/>].</summary>
    public static long Wrap(long value, long min, long max)
      => value < min ? max - (min - value) % (max - min) : value > max ? min + (value - min) % (max - min) : value;

    /// <summary>Returns the <paramref name="value"/> indefinitely wrapped (overflowed) around the boundaries of the closed interval [<paramref name="min"/>, <paramref name="max"/>].</summary>
    [System.CLSCompliant(false)]
    public static uint Wrap(uint value, uint min, uint max)
      => value < min ? max - (min - value) % (max - min) : value > max ? min + (value - min) % (max - min) : value;
    /// <summary>Returns the <paramref name="value"/> indefinitely wrapped (overflowed) around the boundaries of the closed interval [<paramref name="min"/>, <paramref name="max"/>].</summary>
    [System.CLSCompliant(false)]
    public static ulong Wrap(ulong value, ulong min, ulong max)
      => value < min ? max - (min - value) % (max - min) : value > max ? min + (value - min) % (max - min) : value;
  }
}
