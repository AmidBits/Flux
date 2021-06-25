namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Returns a value that is wrapped (overflowed) around a minimum and a maximum number.</summary>
    public static System.Numerics.BigInteger Wrap(System.Numerics.BigInteger value, System.Numerics.BigInteger minimum, System.Numerics.BigInteger maximum)
      => value < minimum ? maximum - (minimum - value) % (maximum - minimum) : value > maximum ? minimum + (value - minimum) % (maximum - minimum) : value;

    /// <summary>Returns a value that is wrapped (overflowed) around a minimum and a maximum number.</summary>
    public static decimal Wrap(decimal value, decimal minimum, decimal maximum)
      => value < minimum ? maximum - (minimum - value) % (maximum - minimum) : value > maximum ? minimum + (value - minimum) % (maximum - minimum) : value;

    /// <summary>Returns a value that is wrapped (overflowed) around a minimum and a maximum number.</summary>
    public static float Wrap(float value, float minimum, float maximum)
      => value < minimum ? maximum - (minimum - value) % (maximum - minimum) : value > maximum ? minimum + (value - minimum) % (maximum - minimum) : value;
    /// <summary>Returns a value that is wrapped (overflowed) around a minimum and a maximum number.</summary>
    public static double Wrap(double value, double minimum, double maximum)
      => value < minimum ? maximum - (minimum - value) % (maximum - minimum) : value > maximum ? minimum + (value - minimum) % (maximum - minimum) : value;

    /// <summary>Returns a value that is wrapped (overflowed) around a minimum and a maximum number.</summary>
    public static int Wrap(int value, int minimum, int maximum)
      => value < minimum ? maximum - (minimum - value) % (maximum - minimum) : value > maximum ? minimum + (value - minimum) % (maximum - minimum) : value;
    /// <summary>Returns a value that is wrapped (overflowed) around a minimum and a maximum number.</summary>
    public static long Wrap(long value, long minimum, long maximum)
      => value < minimum ? maximum - (minimum - value) % (maximum - minimum) : value > maximum ? minimum + (value - minimum) % (maximum - minimum) : value;

    /// <summary>Returns a value that is wrapped (overflowed) around a minimum and a maximum number.</summary>
    [System.CLSCompliant(false)]
    public static uint Wrap(uint value, uint minimum, uint maximum)
      => value < minimum ? maximum - (minimum - value) % (maximum - minimum) : value > maximum ? minimum + (value - minimum) % (maximum - minimum) : value;
    /// <summary>Returns a value that is wrapped (overflowed) around a minimum and a maximum number.</summary>
    [System.CLSCompliant(false)]
    public static ulong Wrap(ulong value, ulong minimum, ulong maximum)
      => value < minimum ? maximum - (minimum - value) % (maximum - minimum) : value > maximum ? minimum + (value - minimum) % (maximum - minimum) : value;
  }
}
