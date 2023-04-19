namespace Flux
{
  public static partial class GenericMath
  {
#if NET7_0_OR_GREATER

    /// <summary>Folds an out-of-bound <paramref name="x"/> over across an interval, back and forth, between the closed interval bounds [<paramref name="min"/>, <paramref name="max"/>], until the value is back within range.</summary>
    public static TSelf Fold<TSelf>(this TSelf x, TSelf min, TSelf max)
      where TSelf : System.Numerics.INumber<TSelf>
      => (x > max)
      ? TSelf.IsEvenInteger(TruncMod(x - max, max - min, out var remHi)) ? max - remHi : min + remHi
      : (x < min)
      ? TSelf.IsEvenInteger(TruncMod(min - x, max - min, out var remLo)) ? min + remLo : max - remLo
      : x;

#else

    /// <summary>Folds out-of-bound values over across the range, back and forth, until the value is in range.</summary>
    public static System.Numerics.BigInteger Fold(this System.Numerics.BigInteger value, System.Numerics.BigInteger minimum, System.Numerics.BigInteger maximum)
      => (value > maximum)
      ? (System.Numerics.BigInteger.DivRem(value - maximum, maximum - minimum, out var remHi) & 1) == 0 ? maximum - remHi : minimum + remHi
      : (value < minimum)
      ? (System.Numerics.BigInteger.DivRem(minimum - value, maximum - minimum, out var remLo) & 1) == 0 ? minimum + remLo : maximum - remLo
      : value;

    /// <summary>Folds an out-of-bound <paramref name="value"/> over across the interval, back and forth, between the closed interval [<paramref name="min"/>, <paramref name="max"/>], until the value is back within range.</summary>
    public static decimal Fold(this decimal value, decimal min, decimal max)
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
    public static float Fold(this float value, float min, float max)
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
    public static double Fold(this double value, double min, double max)
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

    /// <summary>Folds out-of-bound values over across the range, back and forth, until the value is in range.</summary>
    public static int Fold(this int value, int minimum, int maximum)
      => (value > maximum)
      ? (System.Math.DivRem(value - maximum, maximum - minimum, out var remHi) & 1) == 0 ? maximum - remHi : minimum + remHi
      : (value < minimum)
      ? (System.Math.DivRem(minimum - value, maximum - minimum, out var remLo) & 1) == 0 ? minimum + remLo : maximum - remLo
      : value;

    /// <summary>Folds out-of-bound values over across the range, back and forth, until the value is in range.</summary>
    public static long Fold(this long value, long minimum, long maximum)
      => (value > maximum)
      ? (System.Math.DivRem(value - maximum, maximum - minimum, out var remHi) & 1) == 0 ? maximum - remHi : minimum + remHi
      : (value < minimum)
      ? (System.Math.DivRem(minimum - value, maximum - minimum, out var remLo) & 1) == 0 ? minimum + remLo : maximum - remLo
      : value;

    /// <summary>Folds an out-of-bound <paramref name="value"/> over across the interval, back and forth, between the closed interval [<paramref name="min"/>, <paramref name="max"/>], until the value is back within range.</summary>
    [System.CLSCompliant(false)]
    public static uint Fold(this uint value, uint min, uint max)
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
    public static ulong Fold(this ulong value, ulong min, ulong max)
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

#endif
  }
}
