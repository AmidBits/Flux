namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Optimized routines for 2 values of T (where T : System.IComparable<T>).</summary>
    public static T Max<T>(T a, T b)
      where T : System.IComparable<T>
      => a.CompareTo(b) >= 0 ? a : b;

    /// <summary>Optimized routines for 3 values of T (where T : System.IComparable<T>).</summary>
    public static T Max<T>(T a, T b, T c)
      where T : System.IComparable<T>
      => a.CompareTo(b) >= 0 ? (a.CompareTo(c) >= 0 ? a : c) : (b.CompareTo(c) >= 0 ? b : c);

    /// <summary>Optimized routines for 4 values of T (where T : System.IComparable<T>).</summary>
    public static T Max<T>(T a, T b, T c, T d)
      where T : System.IComparable<T>
      => a.CompareTo(b) >= 0 ? (a.CompareTo(c) >= 0 ? (a.CompareTo(d) >= 0 ? a : d) : (c.CompareTo(d) >= 0 ? c : d)) : (b.CompareTo(c) >= 0 ? (b.CompareTo(d) >= 0 ? b : d) : (c.CompareTo(d) >= 0 ? c : d));
  }
}
