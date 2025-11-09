namespace Flux
{
  /// <summary>
  /// <para><see href="https://en.wikipedia.org/wiki/Twelvefold_way"/></para>
  /// </summary>
  public static partial class TwelvefoldWay
  {
    public static TInteger AnyDistinct<TInteger>(TInteger x, TInteger n)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => x.IPow(n);

    public static TInteger InjectiveDistinct<TInteger>(TInteger x, TInteger n)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => x.FallingFactorial(n);

    public static TInteger SurjectiveDistinct<TInteger>(TInteger x, TInteger n)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => x.Factorial() * n.StirlingNumber2ndKind(x);

    public static TInteger AnySnOrbits<TInteger>(TInteger x, TInteger n)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => (x + n - TInteger.One).BinomialCoefficient(n);

    public static TInteger InjectiveSnOrbits<TInteger>(TInteger x, TInteger n)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => x.BinomialCoefficient(n);

    public static TInteger SurjectiveSnOrbits<TInteger>(TInteger x, TInteger n)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => (n - TInteger.One).BinomialCoefficient(n - x);

    public static TInteger AnySxOrbits<TInteger>(TInteger x, TInteger n)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => throw new System.NotImplementedException();

    public static TInteger InjectiveSxOrbits<TInteger>(TInteger x, TInteger n)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => throw new System.NotImplementedException();

    public static TInteger SurjectiveSxOrbits<TInteger>(TInteger x, TInteger n)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => n.StirlingNumber2ndKind(x);

    public static TInteger AnySnSxOrbits<TInteger>(TInteger x, TInteger n)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => throw new System.NotImplementedException();

    public static TInteger InjectiveSnSxOrbits<TInteger>(TInteger x, TInteger n)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => throw new System.NotImplementedException();

    public static TInteger SurjectiveSnSxOrbits<TInteger>(TInteger x, TInteger n)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => throw new System.NotImplementedException();
  }
}
