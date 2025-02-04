namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>
    /// <para>Using the built-in <see cref="System.Numerics.IBinaryInteger{TValue}.PopCount(TValue)"/>.</para>
    /// </summary>
    /// <returns>The population count of <paramref name="source"/>, i.e. the number of bits set to 1 in <paramref name="source"/>.</returns>
    public static int GetPopCount<TNumber>(this TNumber source)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => int.CreateChecked(TNumber.PopCount(source));
  }
}
