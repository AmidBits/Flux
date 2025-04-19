namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>
    /// <para>Using the built-in <see cref="System.Numerics.IBinaryInteger{TValue}.GetByteCount(TValue)"/>.</para>
    /// </summary>
    /// <remarks>
    /// <para>Note that some datatypes, e.g. <see cref="System.Numerics.BigInteger"/>, use dynamic storage strategies, making <see cref="GetByteCount{TValue}"/> dynamic also.</para>
    /// </remarks>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static int GetByteCount<TNumber>(this TNumber value)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => value.GetByteCount();
  }
}
