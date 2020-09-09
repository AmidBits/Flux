namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Optimized routines for 2 values of T (where T : System.IComparable<T>).</summary>
    public static T Min<T>(T a, T b)
      where T : System.IComparable<T>
      => a.CompareTo(b) <= 0 ? a : b;

    /// <summary>Optimized routines for 3 values of T (where T : System.IComparable<T>).</summary>
    public static T Min<T>(T a, T b, T c)
      where T : System.IComparable<T>
      => a.CompareTo(b) <= 0 ? (a.CompareTo(c) <= 0 ? a : c) : (b.CompareTo(c) <= 0 ? b : c);

    /// <summary>Optimized routines for 4 values of T (where T : System.IComparable<T>).</summary>
    public static T Min<T>(T a, T b, T c, T d)
      where T : System.IComparable<T>
      => a.CompareTo(b) <= 0 ? (a.CompareTo(c) <= 0 ? (a.CompareTo(d) <= 0 ? a : d) : (c.CompareTo(d) <= 0 ? c : d)) : (b.CompareTo(c) <= 0 ? (b.CompareTo(d) <= 0 ? b : d) : (c.CompareTo(d) <= 0 ? c : d));

    /// <summary>Optimized routines for 3 values.</summary>
    public static System.Numerics.BigInteger Min(System.Numerics.BigInteger a, System.Numerics.BigInteger b, System.Numerics.BigInteger c)
      => (a < b) ? (a < c ? a : c) : (b < c ? b : c);
    /// <summary>Optimized routines for 3 values.</summary>
    public static double Min(double a, double b, double c)
      => System.Math.Min(System.Math.Min(a, b), c);
    /// <summary>Optimized routines for 3 values.</summary>
    public static float Min(float a, float b, float c)
      => System.Math.Min(System.Math.Min(a, b), c);
    /// <summary>Optimized routines for 3 values.</summary>
    public static int Min(int a, int b, int c)
      => (a < b) ? (a < c ? a : c) : (b < c ? b : c);
    /// <summary>Optimized routines for 3 values.</summary>
    public static long Min(long a, long b, long c)
      => (a < b) ? (a < c ? a : c) : (b < c ? b : c);
    /// <summary>Optimized routines for 3 values.</summary>
    [System.CLSCompliant(false)]
    public static uint Min(uint a, uint b, uint c)
      => (a < b) ? (a < c ? a : c) : (b < c ? b : c);
    /// <summary>Optimized routines for 3 values.</summary>
    [System.CLSCompliant(false)]
    public static ulong Min(ulong a, ulong b, ulong c)
      => (a < b) ? (a < c ? a : c) : (b < c ? b : c);

    /// <summary>Optimized routines for 4 values.</summary>
    public static System.Numerics.BigInteger Min(System.Numerics.BigInteger a, System.Numerics.BigInteger b, System.Numerics.BigInteger c, System.Numerics.BigInteger d)
      => (a < b) ? (a < c ? (a < d ? a : d) : (c < d ? c : d)) : (b < c ? (b < d ? b : d) : (c < d ? c : d));
    /// <summary>Optimized routines for 4 values.</summary>
    public static double Min(double a, double b, double c, double d)
      => System.Math.Min(System.Math.Min(a, b), System.Math.Min(c, d));
    /// <summary>Optimized routines for 4 values.</summary>
    public static float Min(float a, float b, float c, float d)
      => System.Math.Min(System.Math.Min(a, b), System.Math.Min(c, d));
    /// <summary>Optimized routines for 4 values.</summary>
    public static int Min(int a, int b, int c, int d)
      => (a < b) ? (a < c ? (a < d ? a : d) : (c < d ? c : d)) : (b < c ? (b < d ? b : d) : (c < d ? c : d));
    /// <summary>Optimized routines for 4 values.</summary>
    public static long Min(long a, long b, long c, long d)
      => (a < b) ? (a < c ? (a < d ? a : d) : (c < d ? c : d)) : (b < c ? (b < d ? b : d) : (c < d ? c : d));
    /// <summary>Optimized routines for 4 values.</summary>
    [System.CLSCompliant(false)]
    public static uint Min(uint a, uint b, uint c, uint d)
      => (a < b) ? (a < c ? (a < d ? a : d) : (c < d ? c : d)) : (b < c ? (b < d ? b : d) : (c < d ? c : d));
    /// <summary>Optimized routines for 4 values.</summary>
    [System.CLSCompliant(false)]
    public static ulong Min(ulong a, ulong b, ulong c, ulong d)
      => (a < b) ? (a < c ? (a < d ? a : d) : (c < d ? c : d)) : (b < c ? (b < d ? b : d) : (c < d ? c : d));
  }
}
