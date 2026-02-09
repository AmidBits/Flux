namespace Flux
{
  public static class FloatingPointConstants
  {
    /// <summary>
    /// <para>Represents the Champernowne constant. A transcendental real constant whose decimal expansion has important properties.</para>
    /// </summary>
    public const double C10 = 0.123456789101112131415161718192021222324252627282930313233343536373839404142434445464748495051525354555657585960;

    /// <summary>
    /// <para>Represents the cube root of 2.</para>
    /// </summary>
    public const double DeliansConstant = 1.2599210498948731647672106072782;

    /// <summary>
    /// <para>Represents mathematical constants.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Euler%27s_constant"/></para>
    /// </summary>
    public const double EulersConstant = 0.57721566490153286060651209008240243;

    /// <summary>
    /// <para>Represents the ratio of two quantities being the same as the ratio of their sum to their maximum. (~1.618)</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Golden_ratio"/></para>
    /// </summary>
    public const double GoldenRatio = 1.6180339887498948482045868343656381177203091798057628621354486227052604628189024497072072041893911374;

    /// <summary>
    /// <para>Represents the square root of 2.</para>
    /// </summary>
    public const double PythagorasConstant = 1.414213562373095048801688724209698078569671875376948073176679737990732478462;

    /// <summary>
    /// <para>Represents the square root of 3.</para>
    /// </summary>
    public const double TheodorusConstant = 1.732050807568877293527446341505872366942805253810380628055806979451933016909;

    extension<TFloat>(TFloat)
      where TFloat : System.Numerics.IFloatingPointConstants<TFloat>
    {
      /// <summary>Represents the Champernowne constant. A transcendental real constant whose decimal expansion has important properties.</summary>
      public static TFloat C10 => TFloat.CreateChecked(C10);

      /// <summary>Represents the cube root of 2.</summary>
      public static TFloat DeliansConstant => TFloat.CreateChecked(DeliansConstant);

      /// <summary>Represents mathematical constants.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Euler%27s_constant"/>
      public static TFloat EulersConstant => TFloat.CreateChecked(EulersConstant);

      /// <summary>Represents the ratio of two quantities being the same as the ratio of their sum to their maximum. (~1.618)</summary>
      /// <see href="https://en.wikipedia.org/wiki/Golden_ratio"/>
      public static TFloat GoldenRatio => TFloat.CreateChecked(GoldenRatio);

      public static TFloat HalfPi => TFloat.CreateChecked(double.Pi / 2);

      /// <summary>Represents the square root of 2.</summary>
      public static TFloat PythagorasConstant => TFloat.CreateChecked(PythagorasConstant);

      /// <summary>Represents the square root of 3.</summary>
      public static TFloat TheodorusConstant => TFloat.CreateChecked(TheodorusConstant);

      /// <summary>
      /// <para>Main classification number system: "DOUBLE-STRUCK CAPITAL R" = U+211D = '&#x211D;'</para>
      /// </summary>
      public static char NumberClassificationSymbol => '\u211D';
    }
  }
}
