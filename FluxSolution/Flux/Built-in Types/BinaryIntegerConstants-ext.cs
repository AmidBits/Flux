namespace Flux
{
  public static class BinaryIntegerConstants
  {
    /// <summary>
    /// <para>The smallest prime number.</para>
    /// </summary>
    public const int MinPrimeNumber = 2;

    /// <summary>
    /// <para>Main classification number system: "DOUBLE-STRUCK CAPITAL Z" = U+2124 = 'ℤ'</para>
    /// </summary>
    public const char NumberClassificationSymbol = '\u2124';

    /// <summary>Represents the square root of 3.</summary>
    public const double TheodorusConstant = 1.732050807568877293527446341505872366942805253810380628055806979451933016909;

    extension<TInteger>(TInteger)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      /// <summary>
      /// <para>The smallest prime number.</para>
      /// </summary>
      public static TInteger MinPrimeNumber => TInteger.CreateChecked(MinPrimeNumber);

      /// <summary>
      /// <para>Main classification number system: "DOUBLE-STRUCK CAPITAL Z" = U+2124 = 'ℤ'</para>
      /// </summary>
      public static char NumberClassificationSymbol => NumberClassificationSymbol;
    }
  }
}
