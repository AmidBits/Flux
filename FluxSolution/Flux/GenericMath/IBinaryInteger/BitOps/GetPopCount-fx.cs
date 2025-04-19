namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>
    /// <para>Using the built-in <see cref="System.Numerics.IBinaryInteger{TValue}.PopCount(TValue)"/>.</para>
    /// </summary>
    /// <returns>The population count of <paramref name="value"/>, i.e. the number of bits set to 1 in <paramref name="value"/>.</returns>
    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
    public static int GetPopCount<TNumber>(this TNumber value)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => int.CreateChecked(TNumber.PopCount(value));
  }
}
