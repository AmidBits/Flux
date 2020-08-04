
namespace Flux
{
  public static partial class Math
  {
    /// <summary>Folds out-of-bound values over across the range, back and forth, until the value is in range.</summary>
    public static System.Numerics.BigInteger Fold(System.Numerics.BigInteger value, System.Numerics.BigInteger minimum, System.Numerics.BigInteger maximum)
    {
      if (value > maximum)
      {
        return (System.Numerics.BigInteger.DivRem(value - maximum, maximum - minimum, out var remainder) & 1) == 0 ? maximum - remainder : minimum + remainder;
      }
      else if (value < minimum)
      {
        return (System.Numerics.BigInteger.DivRem(minimum - value, maximum - minimum, out var remainder) & 1) == 0 ? minimum + remainder : maximum - remainder;
      }

      return value;
    }

    /// <summary>Folds out-of-bound values over across the range, back and forth, until the value is in range.</summary>
    public static decimal Fold(decimal value, decimal minimum, decimal maximum)
    {
      if (value > maximum)
      {
        var magnitude = value - maximum;
        var range = maximum - minimum;

        return ((int)(magnitude / range) & 1) == 0 ? maximum - (magnitude % range) : minimum + (magnitude % range);
      }
      else if (value < minimum)
      {
        var magnitude = minimum - value;
        var range = maximum - minimum;

        return ((int)(magnitude / range) & 1) == 0 ? minimum + (magnitude % range) : maximum - (magnitude % range);
      }

      return value;
    }

    /// <summary>Folds out-of-bound values over across the range, back and forth, until the value is in range.</summary>
    public static float Fold(float value, float minimum, float maximum)
    {
      if (value > maximum)
      {
        var magnitude = value - maximum;
        var range = maximum - minimum;

        return ((int)(magnitude / range) & 1) == 0 ? maximum - (magnitude % range) : minimum + (magnitude % range);
      }
      else if (value < minimum)
      {
        var magnitude = minimum - value;
        var range = maximum - minimum;

        return ((int)(magnitude / range) & 1) == 0 ? minimum + (magnitude % range) : maximum - (magnitude % range);
      }

      return value;
    }
    /// <summary>Folds out-of-bound values over across the range, back and forth, until the value is in range.</summary>
    public static double Fold(double value, double minimum, double maximum)
    {
      if (value > maximum)
      {
        var magnitude = value - maximum;
        var range = maximum - minimum;

        return ((int)(magnitude / range) & 1) == 0 ? maximum - (magnitude % range) : minimum + (magnitude % range);
      }
      else if (value < minimum)
      {
        var magnitude = minimum - value;
        var range = maximum - minimum;

        return ((int)(magnitude / range) & 1) == 0 ? minimum + (magnitude % range) : maximum - (magnitude % range);
      }

      return value;
    }
    
    /// <summary>Folds out-of-bound values over across the range, back and forth, until the value is in range.</summary>
    public static int Fold(int value, int minimum, int maximum)
    {
      if (value > maximum)
      {
        return (System.Math.DivRem(value - maximum, maximum - minimum, out var remainder) & 1) == 0 ? maximum - remainder : minimum + remainder;
      }
      else if (value < minimum)
      {
        return (System.Math.DivRem(minimum - value, maximum - minimum, out var remainder) & 1) == 0 ? minimum + remainder : maximum - remainder;
      }

      return value;
    }
    /// <summary>Folds out-of-bound values over across the range, back and forth, until the value is in range.</summary>
    public static long Fold(long value, long minimum, long maximum)
    {
      if (value > maximum)
      {
        return (System.Math.DivRem(value - maximum, maximum - minimum, out var remainder) & 1L) == 0L ? maximum - remainder : minimum + remainder;
      }
      else if (value < minimum)
      {
        return (System.Math.DivRem(minimum - value, maximum - minimum, out var remainder) & 1L) == 0L ? minimum + remainder : maximum - remainder;
      }

      return value;
    }
    
    /// <summary>Folds out-of-bound values over across the range, back and forth, until the value is in range.</summary>
    [System.CLSCompliant(false)]
    public static uint Fold(uint value, uint minimum, uint maximum)
    {
      if (value > maximum)
      {
        var magnitude = value - maximum;
        var range = maximum - minimum;

        return ((int)(magnitude / range) & 1) == 0 ? maximum - (magnitude % range) : minimum + (magnitude % range);
      }
      else if (value < minimum)
      {
        var magnitude = value - maximum;
        var range = maximum - minimum;

        return ((int)(magnitude / range) & 1) == 0 ? minimum + (magnitude % range) : maximum - (magnitude % range);
      }

      return value;
    }
    /// <summary>Folds out-of-bound values over across the range, back and forth, until the value is in range.</summary>
    [System.CLSCompliant(false)]
    public static ulong Fold(ulong value, ulong minimum, ulong maximum)
    {
      if (value > maximum)
      {
        var magnitude = value - maximum;
        var range = maximum - minimum;

        return ((int)(magnitude / range) & 1) == 0 ? maximum - (magnitude % range) : minimum + (magnitude % range);
      }
      else if (value < minimum)
      {
        var magnitude = value - maximum;
        var range = maximum - minimum;

        return ((int)(magnitude / range) & 1) == 0 ? minimum + (magnitude % range) : maximum - (magnitude % range);
      }

      return value;
    }
  }
}
