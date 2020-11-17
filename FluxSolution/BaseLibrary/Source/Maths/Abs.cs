namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Returns the absolute (positive) value of the signed value.</summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    internal static System.Numerics.BigInteger Abs(System.Numerics.BigInteger value)
      => System.Numerics.BigInteger.Abs(value);

    /// <summary>Returns the absolute (positive) value of the signed value.</summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    internal static decimal Abs(decimal value)
      => System.Math.Abs(value);

    /// <summary>Returns the absolute (positive) value of the signed value.</summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    internal static double Abs(double value)
      => System.Math.Abs(value);

    /// <summary>Returns the absolute (positive) value of the signed value.</summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    internal static float Abs(float value)
      => System.Math.Abs(value);

    /// <summary>Returns the absolute (positive) value of the signed value.</summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    internal static int Abs(int value)
      => System.Math.Abs(value);

    /// <summary>Returns the absolute (positive) value of the signed value.</summary>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    internal static long Abs(long value)
      => System.Math.Abs(value);
  }
}
