namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>
    /// <para>Returns the storage size, in number of bits, needed for the type <typeparamref name="TNumber"/>.</para>
    /// </summary>
    /// <remarks>
    /// <para>Note that some datatypes, e.g. <see cref="System.Numerics.BigInteger"/>, use dynamic storage strategies, making <see cref="GetBitCount{TValue}"/> dynamic also.</para>
    /// </remarks>
    public static int GetBitCount<TNumber>(this TNumber source)
      where TNumber : System.Numerics.IBinaryInteger<TNumber>
      => source.GetByteCount() * 8;
  }
}
