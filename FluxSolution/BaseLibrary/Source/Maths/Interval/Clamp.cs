namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Returns a number that is clamped between a minimum and a maximum number.</summary>
    public static System.Numerics.BigInteger Clamp(System.Numerics.BigInteger value, System.Numerics.BigInteger minimum, System.Numerics.BigInteger maximum)
      => value < minimum ? minimum : value > maximum ? maximum : value;

    //    /// <summary>Returns a number that is clamped between a minimum and a maximum number.</summary>
    //#if NETCOREAPP
    //    [System.Obsolete("Use from System.Math instead.", true)]
    //#endif
    //    public static decimal Clamp(decimal value, decimal minimum, decimal maximum)
    //      => value < minimum ? minimum : value > maximum ? maximum : value;

    //    /// <summary>Returns a number that is clamped between a minimum and a maximum number.</summary>
    //#if NETCOREAPP
    //    [System.Obsolete("Use from System.Math instead.", true)]
    //#endif
    //    public static float Clamp(float value, float minimum, float maximum)
    //      => value < minimum ? minimum : value > maximum ? maximum : value;
    //    /// <summary>Returns a number that is clamped between a minimum and a maximum number.</summary>
    //#if NETCOREAPP
    //    [System.Obsolete("Use from System.Math instead.", true)]
    //#endif
    //    public static double Clamp(double value, double minimum, double maximum)
    //      => value < minimum ? minimum : value > maximum ? maximum : value;

    //    /// <summary>Returns a number that is clamped between a minimum and a maximum number.</summary>
    //#if NETCOREAPP
    //    [System.Obsolete("Use from System.Math instead.", true)]
    //#endif
    //    public static int Clamp(int value, int minimum, int maximum)
    //      => value < minimum ? minimum : value > maximum ? maximum : value;
    //    /// <summary>Returns a number that is clamped between a minimum and a maximum number.</summary>
    //#if NETCOREAPP
    //    [System.Obsolete("Use from System.Math instead.", true)]
    //#endif
    //    public static long Clamp(long value, long minimum, long maximum)
    //      => value < minimum ? minimum : value > maximum ? maximum : value;

    /// <summary>Returns a number that is clamped between a minimum and a maximum number.</summary>
    [System.CLSCompliant(false)]
    public static uint Clamp(uint value, uint minimum, uint maximum)
      => value < minimum ? minimum : value > maximum ? maximum : value;
    /// <summary>Returns a number that is clamped between a minimum and a maximum number.</summary>
    [System.CLSCompliant(false)]
    public static ulong Clamp(ulong value, ulong minimum, ulong maximum)
      => value < minimum ? minimum : value > maximum ? maximum : value;
  }
}
