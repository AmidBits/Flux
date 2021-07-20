namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Proportionally re-scale the <paramref name="value"/> from within one closed interval [<paramref name="sourceMinimum"/>, <paramref name="sourceMaximum"/>] to within another closed interval [<paramref name="targetMinimum"/>, <paramref name="targetMaximum"/>]. The value retains its interval ratio. E.g. a 5 in the range [0, 10] becomes 50 when rescaled to the range [0, 100].</summary>
    public static System.Numerics.BigInteger Rescale(System.Numerics.BigInteger value, System.Numerics.BigInteger sourceMinimum, System.Numerics.BigInteger sourceMaximum, System.Numerics.BigInteger targetMinimum, System.Numerics.BigInteger targetMaximum)
      => (targetMaximum - targetMinimum) * (value - sourceMinimum) / (sourceMaximum - sourceMinimum) + targetMinimum;

    /// <summary>Proportionally re-scale the <paramref name="value"/> from within one closed interval [<paramref name="sourceMinimum"/>, <paramref name="sourceMaximum"/>] to within another closed interval [<paramref name="targetMinimum"/>, <paramref name="targetMaximum"/>]. The value retains its interval ratio. E.g. a 5 in the range [0, 10] becomes 50 when rescaled to the range [0, 100].</summary>
    public static decimal Rescale(decimal value, decimal sourceMinimum, decimal sourceMaximum, decimal targetMinimum, decimal targetMaximum)
      => (targetMaximum - targetMinimum) * (value - sourceMinimum) / (sourceMaximum - sourceMinimum) + targetMinimum;

    /// <summary>Proportionally re-scale the <paramref name="value"/> from within one closed interval [<paramref name="sourceMinimum"/>, <paramref name="sourceMaximum"/>] to within another closed interval [<paramref name="targetMinimum"/>, <paramref name="targetMaximum"/>]. The value retains its interval ratio. E.g. a 5 in the range [0, 10] becomes 50 when rescaled to the range [0, 100].</summary>
    public static float Rescale(float value, float sourceMinimum, float sourceMaximum, float targetMinimum, float targetMaximum)
      => (targetMaximum - targetMinimum) * (value - sourceMinimum) / (sourceMaximum - sourceMinimum) + targetMinimum;
    /// <summary>Proportionally re-scale the <paramref name="value"/> from within one closed interval [<paramref name="sourceMinimum"/>, <paramref name="sourceMaximum"/>] to within another closed interval [<paramref name="targetMinimum"/>, <paramref name="targetMaximum"/>]. The value retains its interval ratio. E.g. a 5 in the range [0, 10] becomes 50 when rescaled to the range [0, 100].</summary>
    public static double Rescale(double value, double sourceMinimum, double sourceMaximum, double targetMinimum, double targetMaximum)
      => (targetMaximum - targetMinimum) * (value - sourceMinimum) / (sourceMaximum - sourceMinimum) + targetMinimum;

    /// <summary>Proportionally re-scale the <paramref name="value"/> from within one closed interval [<paramref name="sourceMinimum"/>, <paramref name="sourceMaximum"/>] to within another closed interval [<paramref name="targetMinimum"/>, <paramref name="targetMaximum"/>]. The value retains its interval ratio. E.g. a 5 in the range [0, 10] becomes 50 when rescaled to the range [0, 100].</summary>
    public static int Rescale(int value, int sourceMinimum, int sourceMaximum, int targetMinimum, int targetMaximum)
      => (targetMaximum - targetMinimum) * (value - sourceMinimum) / (sourceMaximum - sourceMinimum) + targetMinimum;
    /// <summary>Proportionally re-scale the <paramref name="value"/> from within one closed interval [<paramref name="sourceMinimum"/>, <paramref name="sourceMaximum"/>] to within another closed interval [<paramref name="targetMinimum"/>, <paramref name="targetMaximum"/>]. The value retains its interval ratio. E.g. a 5 in the range [0, 10] becomes 50 when rescaled to the range [0, 100].</summary>
    public static long Rescale(long value, long sourceMinimum, long sourceMaximum, long targetMinimum, long targetMaximum)
      => (targetMaximum - targetMinimum) * (value - sourceMinimum) / (sourceMaximum - sourceMinimum) + targetMinimum;

    /// <summary>Proportionally re-scale the <paramref name="value"/> from within one closed interval [<paramref name="sourceMinimum"/>, <paramref name="sourceMaximum"/>] to within another closed interval [<paramref name="targetMinimum"/>, <paramref name="targetMaximum"/>]. The value retains its interval ratio. E.g. a 5 in the range [0, 10] becomes 50 when rescaled to the range [0, 100].</summary>
    [System.CLSCompliant(false)]
    public static uint Rescale(uint value, uint sourceMinimum, uint sourceMaximum, uint targetMinimum, uint targetMaximum)
      => (targetMaximum - targetMinimum) * (value - sourceMinimum) / (sourceMaximum - sourceMinimum) + targetMinimum;
    /// <summary>Proportionally re-scale the <paramref name="value"/> from within one closed interval [<paramref name="sourceMinimum"/>, <paramref name="sourceMaximum"/>] to within another closed interval [<paramref name="targetMinimum"/>, <paramref name="targetMaximum"/>]. The value retains its interval ratio. E.g. a 5 in the range [0, 10] becomes 50 when rescaled to the range [0, 100].</summary>
    [System.CLSCompliant(false)]
    public static ulong Rescale(ulong value, ulong sourceMinimum, ulong sourceMaximum, ulong targetMinimum, ulong targetMaximum)
      => (targetMaximum - targetMinimum) * (value - sourceMinimum) / (sourceMaximum - sourceMinimum) + targetMinimum;
  }
}
