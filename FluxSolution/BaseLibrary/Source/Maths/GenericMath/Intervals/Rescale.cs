namespace Flux
{
  public static partial class Maths
  {
#if NET7_0_OR_GREATER

    /// <summary>Proportionally rescale the <paramref name="value"/> from the closed interval [<paramref name="minSource"/>, <paramref name="maxSource"/>] to the closed interval [<paramref name="minTarget"/>, <paramref name="maxTarget"/>]. The <paramref name="value"/> retains its proportional interval ratio, e.g. a 5 from the closed interval [0, 10] becomes 50 when rescaled to the closed interval [0, 100].</summary>
    public static TSelf Rescale<TSelf>(this TSelf value, TSelf minSource, TSelf maxSource, TSelf minTarget, TSelf maxTarget)
      where TSelf : System.Numerics.INumber<TSelf>
      => minTarget + (maxTarget - minTarget) * (value - minSource) / (maxSource - minSource);

#else

    /// <summary>Proportionally re-scale the <paramref name="value"/> from within one closed interval [<paramref name="minSource"/>, <paramref name="maxSource"/>] to within another closed interval [<paramref name="minTarget"/>, <paramref name="maxTarget"/>]. The value retains its interval ratio. E.g. a 5 in the range [0, 10] becomes 50 when rescaled to the range [0, 100].</summary>
    public static System.Numerics.BigInteger Rescale(this System.Numerics.BigInteger value, System.Numerics.BigInteger minSource, System.Numerics.BigInteger maxSource, System.Numerics.BigInteger minTarget, System.Numerics.BigInteger maxTarget)
      => (maxTarget - minTarget) * (value - minSource) / (maxSource - minSource) + minTarget;

    /// <summary>Proportionally re-scale the <paramref name="value"/> from within one closed interval [<paramref name="minSource"/>, <paramref name="maxSource"/>] to within another closed interval [<paramref name="minTarget"/>, <paramref name="maxTarget"/>]. The value retains its interval ratio. E.g. a 5 in the range [0, 10] becomes 50 when rescaled to the range [0, 100].</summary>
    public static decimal Rescale(this decimal value, decimal minSource, decimal maxSource, decimal minTarget, decimal maxTarget)
      => (maxTarget - minTarget) * (value - minSource) / (maxSource - minSource) + minTarget;

    /// <summary>Proportionally re-scale the <paramref name="value"/> from within one closed interval [<paramref name="minSource"/>, <paramref name="maxSource"/>] to within another closed interval [<paramref name="minTarget"/>, <paramref name="maxTarget"/>]. The value retains its interval ratio. E.g. a 5 in the range [0, 10] becomes 50 when rescaled to the range [0, 100].</summary>
    public static float Rescale(this float value, float minSource, float maxSource, float minTarget, float maxTarget)
      => (maxTarget - minTarget) * (value - minSource) / (maxSource - minSource) + minTarget;

    /// <summary>Proportionally re-scale the <paramref name="value"/> from within one closed interval [<paramref name="minSource"/>, <paramref name="maxSource"/>] to within another closed interval [<paramref name="minTarget"/>, <paramref name="maxTarget"/>]. The value retains its interval ratio. E.g. a 5 in the range [0, 10] becomes 50 when rescaled to the range [0, 100].</summary>
    public static double Rescale(this double value, double minSource, double maxSource, double minTarget, double maxTarget)
      => (maxTarget - minTarget) * (value - minSource) / (maxSource - minSource) + minTarget;

    /// <summary>Proportionally re-scale the <paramref name="value"/> from within one closed interval [<paramref name="minSource"/>, <paramref name="maxSource"/>] to within another closed interval [<paramref name="minTarget"/>, <paramref name="maxTarget"/>]. The value retains its interval ratio. E.g. a 5 in the range [0, 10] becomes 50 when rescaled to the range [0, 100].</summary>
    public static int Rescale(this int value, int minSource, int maxSource, int minTarget, int maxTarget)
      => (maxTarget - minTarget) * (value - minSource) / (maxSource - minSource) + minTarget;

    /// <summary>Proportionally re-scale the <paramref name="value"/> from within one closed interval [<paramref name="minSource"/>, <paramref name="maxSource"/>] to within another closed interval [<paramref name="minTarget"/>, <paramref name="maxTarget"/>]. The value retains its interval ratio. E.g. a 5 in the range [0, 10] becomes 50 when rescaled to the range [0, 100].</summary>
    public static long Rescale(this long value, long minSource, long maxSource, long minTarget, long maxTarget)
      => (maxTarget - minTarget) * (value - minSource) / (maxSource - minSource) + minTarget;

    /// <summary>Proportionally re-scale the <paramref name="value"/> from within one closed interval [<paramref name="minSource"/>, <paramref name="maxSource"/>] to within another closed interval [<paramref name="minTarget"/>, <paramref name="maxTarget"/>]. The value retains its interval ratio. E.g. a 5 in the range [0, 10] becomes 50 when rescaled to the range [0, 100].</summary>
    [System.CLSCompliant(false)]
    public static uint Rescale(this uint value, uint minSource, uint maxSource, uint minTarget, uint maxTarget)
      => (maxTarget - minTarget) * (value - minSource) / (maxSource - minSource) + minTarget;

    /// <summary>Proportionally re-scale the <paramref name="value"/> from within one closed interval [<paramref name="minSource"/>, <paramref name="maxSource"/>] to within another closed interval [<paramref name="minTarget"/>, <paramref name="maxTarget"/>]. The value retains its interval ratio. E.g. a 5 in the range [0, 10] becomes 50 when rescaled to the range [0, 100].</summary>
    [System.CLSCompliant(false)]
    public static ulong Rescale(this ulong value, ulong minSource, ulong maxSource, ulong minTarget, ulong maxTarget)
      => (maxTarget - minTarget) * (value - minSource) / (maxSource - minSource) + minTarget;

#endif
  }
}
