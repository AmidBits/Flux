

namespace Flux
{
  public static partial class Maths
  {

    
    /// <summary>Snaps the value to zero if it's within the specified distance of zero.</summary>
	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static double SqrtX(System.Numerics.BigInteger value)
        => System.Numerics.BigInteger.Sqrt(value);
    
    
    /// <summary>Snaps the value to zero if it's within the specified distance of zero.</summary>
	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static double SqrtX(System.Decimal value)
        => System.Math.Sqrt((double)value);
    
    
    /// <summary>Snaps the value to zero if it's within the specified distance of zero.</summary>
	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static double SqrtX(System.Double value)
        => System.Math.Sqrt((double)value);
    
    
    /// <summary>Snaps the value to zero if it's within the specified distance of zero.</summary>
	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static double SqrtX(System.Single value)
        => System.Math.Sqrt((double)value);
    
    
    /// <summary>Snaps the value to zero if it's within the specified distance of zero.</summary>
	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static double SqrtX(System.Int32 value)
        => System.Math.Sqrt((double)value);
    
    
    /// <summary>Snaps the value to zero if it's within the specified distance of zero.</summary>
	[System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static double SqrtX(System.Int64 value)
        => System.Math.Sqrt((double)value);
    
    
  }
}
