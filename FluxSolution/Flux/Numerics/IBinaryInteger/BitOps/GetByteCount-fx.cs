namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>
    /// <para>Using the built-in <see cref="System.Numerics.IBinaryInteger{TInteger}.GetByteCount(TInteger)"/>.</para>
    /// </summary>
    /// <remarks>
    /// <para>Note that some datatypes, e.g. <see cref="System.Numerics.BigInteger"/>, use dynamic storage strategies, making <see cref="GetByteCount{TValue}"/> dynamic also.</para>
    /// </remarks>
    public static int GetByteCount<TInteger>(this TInteger value)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => value.GetByteCount();
  }
}
