namespace Flux
{
  public static class BinaryIntegerConstants
  {
    /// <summary>
    /// <para>The smallest prime number.</para>
    /// </summary>
    public const int MinPrimeNumber = 2;

    extension<TInteger>(TInteger)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      /// <summary>
      /// <para>The smallest prime number.</para>
      /// </summary>
      public static TInteger MinPrimeNumber => TInteger.CreateChecked(MinPrimeNumber);

      /// <summary>
      /// <para>Main classification number system:</para>
      /// <para>Signed integers: "DOUBLE-STRUCK CAPITAL Z" = U+2124 = '&#x2124;'</para>
      /// <para>Unsigned integers (natural numbers): "DOUBLE-STRUCK CAPITAL N" = U+2115 = '&#x2115;'</para>
      /// </summary>
      public static char NumberClassificationSymbol
        => typeof(TInteger).IsISignedNumber() ? '\u2124'
        : typeof(TInteger).IsIUnsignedNumber() ? '\u2115'
        : throw new System.NotImplementedException();
    }
  }
}
