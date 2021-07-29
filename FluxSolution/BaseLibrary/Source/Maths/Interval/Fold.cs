
namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Folds an out-of-bound <paramref name="value"/> over across the interval, back and forth, between the closed interval [<paramref name="min"/>, <paramref name="max"/>], until the value is back within range.</summary>
    public static System.Numerics.BigInteger Fold(System.Numerics.BigInteger value, System.Numerics.BigInteger min, System.Numerics.BigInteger max)
    {
      if (value > max)
        return (System.Numerics.BigInteger.DivRem(value - max, max - min, out var remainder) & 1) == 0 ? max - remainder : min + remainder;
      else if (value < min)
        return (System.Numerics.BigInteger.DivRem(min - value, max - min, out var remainder) & 1) == 0 ? min + remainder : max - remainder;
      return value;
    }

    /// <summary>Folds an out-of-bound <paramref name="value"/> over across the interval, back and forth, between the closed interval [<paramref name="min"/>, <paramref name="max"/>], until the value is back within range.</summary>
    public static decimal Fold(decimal value, decimal min, decimal max)
    {
      decimal magnitude, range;

      if (value > max)
      {
        magnitude = value - max;
        range = max - min;

        return ((int)(magnitude / range) & 1) == 0 ? max - (magnitude % range) : min + (magnitude % range);
      }
      else if (value < min)
      {
        magnitude = min - value;
        range = max - min;

        return ((int)(magnitude / range) & 1) == 0 ? min + (magnitude % range) : max - (magnitude % range);
      }

      return value;
    }

    /// <summary>Folds an out-of-bound <paramref name="value"/> over across the interval, back and forth, between the closed interval [<paramref name="min"/>, <paramref name="max"/>], until the value is back within range.</summary>
    public static float Fold(float value, float min, float max)
    {
      float magnitude, range;

      if (value > max)
      {
        magnitude = value - max;
        range = max - min;

        return ((int)(magnitude / range) & 1) == 0 ? max - (magnitude % range) : min + (magnitude % range);
      }
      else if (value < min)
      {
        magnitude = min - value;
        range = max - min;

        return ((int)(magnitude / range) & 1) == 0 ? min + (magnitude % range) : max - (magnitude % range);
      }

      return value;
    }
    /// <summary>Folds an out-of-bound <paramref name="value"/> over across the interval, back and forth, between the closed interval [<paramref name="min"/>, <paramref name="max"/>], until the value is back within range.</summary>
    public static double Fold(double value, double min, double max)
    {
      double magnitude, range;

      if (value > max)
      {
        magnitude = value - max;
        range = max - min;

        return ((int)(magnitude / range) & 1) == 0 ? max - (magnitude % range) : min + (magnitude % range);
      }
      else if (value < min)
      {
        magnitude = min - value;
        range = max - min;

        return ((int)(magnitude / range) & 1) == 0 ? min + (magnitude % range) : max - (magnitude % range);
      }

      return value;
    }

    /// <summary>Folds an out-of-bound <paramref name="value"/> over across the interval, back and forth, between the closed interval [<paramref name="min"/>, <paramref name="max"/>], until the value is back within range.</summary>
    public static int Fold(int value, int min, int max)
    {
      if (value > max)
        return (System.Math.DivRem(value - max, max - min, out var remainder) & 1) == 0 ? max - remainder : min + remainder;
      else if (value < min)
        return (System.Math.DivRem(min - value, max - min, out var remainder) & 1) == 0 ? min + remainder : max - remainder;

      return value;
    }
    /// <summary>Folds an out-of-bound <paramref name="value"/> over across the interval, back and forth, between the closed interval [<paramref name="min"/>, <paramref name="max"/>], until the value is back within range.</summary>
    public static long Fold(long value, long min, long max)
    {
      if (value > max)
        return (System.Math.DivRem(value - max, max - min, out var remainder) & 1L) == 0L ? max - remainder : min + remainder;
      else if (value < min)
        return (System.Math.DivRem(min - value, max - min, out var remainder) & 1L) == 0L ? min + remainder : max - remainder;

      return value;
    }

    /// <summary>Folds an out-of-bound <paramref name="value"/> over across the interval, back and forth, between the closed interval [<paramref name="min"/>, <paramref name="max"/>], until the value is back within range.</summary>
    [System.CLSCompliant(false)]
    public static uint Fold(uint value, uint min, uint max)
    {
      uint magnitude, range;

      if (value > max)
      {
        magnitude = value - max;
        range = max - min;

        return ((int)(magnitude / range) & 1) == 0 ? max - (magnitude % range) : min + (magnitude % range);
      }
      else if (value < min)
      {
        magnitude = value - max;
        range = max - min;

        return ((int)(magnitude / range) & 1) == 0 ? min + (magnitude % range) : max - (magnitude % range);
      }

      return value;
    }
    /// <summary>Folds an out-of-bound <paramref name="value"/> over across the interval, back and forth, between the closed interval [<paramref name="min"/>, <paramref name="max"/>], until the value is back within range.</summary>
    [System.CLSCompliant(false)]
    public static ulong Fold(ulong value, ulong min, ulong max)
    {
      ulong magnitude, range;

      if (value > max)
      {
        magnitude = value - max;
        range = max - min;

        return ((int)(magnitude / range) & 1) == 0 ? max - (magnitude % range) : min + (magnitude % range);
      }
      else if (value < min)
      {
        magnitude = value - max;
        range = max - min;

        return ((int)(magnitude / range) & 1) == 0 ? min + (magnitude % range) : max - (magnitude % range);
      }

      return value;
    }
  }
}
