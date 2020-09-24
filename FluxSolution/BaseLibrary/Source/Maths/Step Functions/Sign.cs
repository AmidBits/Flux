namespace Flux
{
  public static partial class Maths
  {
    /// <summary>In difference from the System.Math.Sign(), this one returns only one of two values, -1 when less than zero, otherwise it 1. Zero is never returned.</summary>
    public static int Sign<T>(T value, T zeroReference)
      where T : System.IComparable<T>
      => value.CompareTo(zeroReference) < 0 ? -1 : 1;

    /// <summary>In difference from the System.Math.Sign(), this one returns only one of two values, -1 when less than zero, otherwise it 1. Zero is never returned.</summary>
    public static System.Numerics.BigInteger Sign(System.Numerics.BigInteger value)
      => value < 0 ? -1 : 1;

    //    /// <summary>In difference from the System.Math.Sign(), this one returns only one of two values, -1 when less than zero, otherwise it 1. Zero is never returned.</summary>
    //#if NETCOREAPP
    //    [System.Obsolete("Use from System.Math instead.", true)]
    //#endif
    //    public static decimal Sign(decimal value)
    //      => value < 0 ? -1 : 1;

    //    /// <summary>In difference from the System.Math.Sign(), this one returns only one of two values, -1 when less than zero, otherwise it 1. Zero is never returned.</summary>
    //#if NETCOREAPP
    //    [System.Obsolete("Use from System.Math instead.", true)]
    //#endif
    //    public static float Sign(float value)
    //      => value < 0 ? -1 : 1;
    //    /// <summary>In difference from the System.Math.Sign(), this one returns only one of two values, -1 when less than zero, otherwise it 1. Zero is never returned.</summary>
    //#if NETCOREAPP
    //    [System.Obsolete("Use from System.Math instead.", true)]
    //#endif
    //    public static double Sign(double value)
    //      => value < 0 ? -1 : 1;

    //    /// <summary>In difference from the System.Math.Sign(), this one returns only one of two values, -1 when less than zero, otherwise it 1. Zero is never returned.</summary>
    //#if NETCOREAPP
    //    [System.Obsolete("Use from System.Math instead.", true)]
    //#endif
    //    public static int Sign(int value)
    //      => value < 0 ? -1 : 1;
    //    /// <summary>In difference from the System.Math.Sign(), this one returns only one of two values, -1 when less than zero, otherwise it 1. Zero is never returned.</summary>
    //#if NETCOREAPP
    //    [System.Obsolete("Use from System.Math instead.", true)]
    //#endif
    //    public static long Sign(long value)
    //      => value < 0 ? -1 : 1;
  }
}
