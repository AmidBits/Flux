namespace Flux
{
  public static partial class BitOps
  {
    #region GetPopCount

    /// <summary>
    /// <para>Using the built-in <see cref="System.Numerics.IBinaryInteger{TInteger}.PopCount(TInteger)"/>.</para>
    /// </summary>
    /// <returns>The population count of <paramref name="value"/>, i.e. the number of bits set to 1 in <paramref name="value"/>.</returns>
    public static int GetPopCount<TInteger>(this TInteger value)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => int.CreateChecked(TInteger.PopCount(value));

    #endregion

    #region ScratchGetPopCount

#if INCLUDE_SCRATCH

    public static int ScratchGetPopCount<TInteger>(this TInteger value)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      System.ArgumentOutOfRangeException.ThrowIfNegative(value);

      var count = 0;

      while (value > TInteger.Zero)
      {
        count++;

        value &= value - TInteger.One; // Clear the LS1B.
      }

      return count;
    }

#endif

    #endregion
  }
}
