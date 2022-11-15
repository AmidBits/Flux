//namespace Flux
//{
//  public static partial class Maths
//  {
//    /// <summary>Proportionally re-scale the <paramref name="value"/> from within one closed interval [<paramref name="minSource"/>, <paramref name="maxSource"/>] to within another closed interval [<paramref name="minTarget"/>, <paramref name="maxTarget"/>]. The value retains its interval ratio. E.g. a 5 in the range [0, 10] becomes 50 when rescaled to the range [0, 100].</summary>
//    public static System.Numerics.BigInteger Rescale(System.Numerics.BigInteger value, System.Numerics.BigInteger minSource, System.Numerics.BigInteger maxSource, System.Numerics.BigInteger minTarget, System.Numerics.BigInteger maxTarget)
//      => (maxTarget - minTarget) * (value - minSource) / (maxSource - minSource) + minTarget;

//    /// <summary>Proportionally re-scale the <paramref name="value"/> from within one closed interval [<paramref name="minSource"/>, <paramref name="maxSource"/>] to within another closed interval [<paramref name="minTarget"/>, <paramref name="maxTarget"/>]. The value retains its interval ratio. E.g. a 5 in the range [0, 10] becomes 50 when rescaled to the range [0, 100].</summary>
//    public static decimal Rescale(decimal value, decimal minSource, decimal maxSource, decimal minTarget, decimal maxTarget)
//      => (maxTarget - minTarget) * (value - minSource) / (maxSource - minSource) + minTarget;

//    /// <summary>Proportionally re-scale the <paramref name="value"/> from within one closed interval [<paramref name="minSource"/>, <paramref name="maxSource"/>] to within another closed interval [<paramref name="minTarget"/>, <paramref name="maxTarget"/>]. The value retains its interval ratio. E.g. a 5 in the range [0, 10] becomes 50 when rescaled to the range [0, 100].</summary>
//    public static float Rescale(float value, float minSource, float maxSource, float minTarget, float maxTarget)
//      => (maxTarget - minTarget) * (value - minSource) / (maxSource - minSource) + minTarget;
//    /// <summary>Proportionally re-scale the <paramref name="value"/> from within one closed interval [<paramref name="minSource"/>, <paramref name="maxSource"/>] to within another closed interval [<paramref name="minTarget"/>, <paramref name="maxTarget"/>]. The value retains its interval ratio. E.g. a 5 in the range [0, 10] becomes 50 when rescaled to the range [0, 100].</summary>
//    public static double Rescale(double value, double minSource, double maxSource, double minTarget, double maxTarget)
//      => (maxTarget - minTarget) * (value - minSource) / (maxSource - minSource) + minTarget;

//    /// <summary>Proportionally re-scale the <paramref name="value"/> from within one closed interval [<paramref name="minSource"/>, <paramref name="maxSource"/>] to within another closed interval [<paramref name="minTarget"/>, <paramref name="maxTarget"/>]. The value retains its interval ratio. E.g. a 5 in the range [0, 10] becomes 50 when rescaled to the range [0, 100].</summary>
//    public static int Rescale(int value, int minSource, int maxSource, int minTarget, int maxTarget)
//      => (maxTarget - minTarget) * (value - minSource) / (maxSource - minSource) + minTarget;
//    /// <summary>Proportionally re-scale the <paramref name="value"/> from within one closed interval [<paramref name="minSource"/>, <paramref name="maxSource"/>] to within another closed interval [<paramref name="minTarget"/>, <paramref name="maxTarget"/>]. The value retains its interval ratio. E.g. a 5 in the range [0, 10] becomes 50 when rescaled to the range [0, 100].</summary>
//    public static long Rescale(long value, long minSource, long maxSource, long minTarget, long maxTarget)
//      => (maxTarget - minTarget) * (value - minSource) / (maxSource - minSource) + minTarget;

//    /// <summary>Proportionally re-scale the <paramref name="value"/> from within one closed interval [<paramref name="minSource"/>, <paramref name="maxSource"/>] to within another closed interval [<paramref name="minTarget"/>, <paramref name="maxTarget"/>]. The value retains its interval ratio. E.g. a 5 in the range [0, 10] becomes 50 when rescaled to the range [0, 100].</summary>
//    [System.CLSCompliant(false)]
//    public static uint Rescale(uint value, uint minSource, uint maxSource, uint minTarget, uint maxTarget)
//      => (maxTarget - minTarget) * (value - minSource) / (maxSource - minSource) + minTarget;
//    /// <summary>Proportionally re-scale the <paramref name="value"/> from within one closed interval [<paramref name="minSource"/>, <paramref name="maxSource"/>] to within another closed interval [<paramref name="minTarget"/>, <paramref name="maxTarget"/>]. The value retains its interval ratio. E.g. a 5 in the range [0, 10] becomes 50 when rescaled to the range [0, 100].</summary>
//    [System.CLSCompliant(false)]
//    public static ulong Rescale(ulong value, ulong minSource, ulong maxSource, ulong minTarget, ulong maxTarget)
//      => (maxTarget - minTarget) * (value - minSource) / (maxSource - minSource) + minTarget;
//  }
//}
