namespace Flux
{
  public static partial class FloatingPointConstants<TFloat>
    where TFloat : System.Numerics.IFloatingPoint<TFloat>
  {
    /// <summary>Cube root of 2, Delian constant</summary>
    public static readonly TFloat Cbrt2 = TFloat.CreateChecked(1.2599210498948731647672106072782283505702514647015m);

    /// <summary>Cube root of 3</summary>
    public static readonly TFloat Cbrt3 = TFloat.CreateChecked(1.44224957030740838232163831078010958839186925349935m);

    /// <summary>Euler's constant</summary>
    public static readonly TFloat EulersConstant = TFloat.CreateChecked(0.57721566490153286060651209008240243m);

    /// <summary>Golden ratio</summary>
    public static readonly TFloat GoldenRatio = TFloat.CreateChecked(1.6180339887498948482045868343656381177203091798057628621354486227052604628189024497072072041893911374m); // (TFloat.One + Sqrt5) / Two

    /// <summary>One half</summary>
    public static readonly TFloat OneHalf = TFloat.One / NumberConstants<TFloat>.Two;

    /// <summary>PI / 2</summary>
    public static readonly TFloat PiOver2 = TFloat.Pi / NumberConstants<TFloat>.Two;

    /// <summary>PI / 4</summary>
    public static readonly TFloat PiOver4 = TFloat.Pi / NumberConstants<TFloat>.Four;

    /// <summary>Square root of 2, Pythagoras constant</summary>
    public static readonly TFloat Sqrt2 = TFloat.CreateChecked(1.414213562373095048801688724209698078569671875376948073176679737990732478462m);

    /// <summary>Square root of 3, Theodorus' constant</summary>
    public static readonly TFloat Sqrt3 = TFloat.CreateChecked(1.732050807568877293527446341505872366942805253810380628055806979451933016909m);

    /// <summary>Square root of 5</summary>
    public static readonly TFloat Sqrt5 = TFloat.CreateChecked(2.236067977499789696409173668731276235440618359611525724270897m);

    /// <summary>Twelfth root of 2</summary>
    public static readonly TFloat TwelfthRootOf2 = TFloat.CreateChecked(1.05946309435929526456182529494634170077920431749418562855921m);
  }

  public static partial class NumberConstants<TNumber>
    where TNumber : System.Numerics.INumberBase<TNumber>
  {
    public static readonly TNumber Two = TNumber.CreateChecked(2);
    public static readonly TNumber Three = TNumber.CreateChecked(3);
    public static readonly TNumber Four = TNumber.CreateChecked(4);
    public static readonly TNumber Five = TNumber.CreateChecked(5);
    public static readonly TNumber Six = TNumber.CreateChecked(6);
    public static readonly TNumber Eight = TNumber.CreateChecked(8);
    public static readonly TNumber Ten = TNumber.CreateChecked(10);
    public static readonly TNumber Sixteen = TNumber.CreateChecked(16);
  }

  public static partial class Number
  {
    extension<TNumber>(TNumber)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      /// <summary>
      /// <para>Main classification number system:</para>
      /// <para>Integers (signed integers, i.e. includes negative numbers): "DOUBLE-STRUCK CAPITAL Z" = U+2124 = '&#x2124;'</para>
      /// <para>Natural numbers (unsigned integers, i.e. non-negative or positive integers, depending on convention): "DOUBLE-STRUCK CAPITAL N" = U+2115 = '&#x2115;'</para>
      /// <para>Complex numbers: "DOUBLE-STRUCK CAPITAL C" = U+2102 = '&#x2102;'</para>
      /// <para>Real numbers (floating point): "DOUBLE-STRUCK CAPITAL R" = U+211D = '&#x211D;'</para>
      /// </summary>
      public static char NumberSystemClassificationSymbol()
        => typeof(TNumber).IsSignedInteger() ? '\u2124' // "DOUBLE-STRUCK CAPITAL Z" for integers (includes negative numbers).
        : typeof(TNumber).IsUnsignedInteger() ? '\u2115' // "DOUBLE-STRUCK CAPITAL N" for natural numbers (non-negative or positive integers, depending on convention).
        : typeof(TNumber) == typeof(System.Numerics.Complex) ? '\u2102' // "DOUBLE-STRUCK CAPITAL C" for complex numbers.
        : typeof(TNumber).IsIFloatingPoint() ? '\u211D' // "DOUBLE-STRUCK CAPITAL R" for real numbers.
        : throw new System.NotImplementedException($"{typeof(TNumber)}");
    }
  }
}
