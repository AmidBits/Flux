namespace Flux
{
  public static partial class FloatingPointConstants<TFloat>
    where TFloat : System.Numerics.IFloatingPoint<TFloat>
  {
    /// <summary>Yields the value cbrt(2), Delian constant</summary>
    public static readonly TFloat Cbrt2 = TFloat.CreateChecked(1.2599210498948731647672106072782283505702514647015m);

    /// <summary>Yields the value  cbrt(3)</summary>
    public static readonly TFloat Cbrt3 = TFloat.CreateChecked(1.44224957030740838232163831078010958839186925349935m);

    /// <summary>Yields the value 0.577.., Euler's constant</summary>
    public static readonly TFloat EulersConstant = TFloat.CreateChecked(0.57721566490153286060651209008240243m);

    /// <summary>Yields the 1.618.., Golden ratio</summary>
    public static readonly TFloat GoldenRatio = TFloat.CreateChecked(1.6180339887498948482045868343656381177203091798057628621354486227052604628189024497072072041893911374m); // (TFloat.One + Sqrt5) / Two

    /// <summary>Yields the value 0.5</summary>
    public static readonly TFloat OneHalf = TFloat.One / NumberBaseConstants<TFloat>.Two;

    /// <summary>Yields the value Pi/2</summary>
    public static readonly TFloat PiOver2 = TFloat.Pi / NumberBaseConstants<TFloat>.Two;

    /// <summary>Yields the value Pi/4</summary>
    public static readonly TFloat PiOver4 = TFloat.Pi / NumberBaseConstants<TFloat>.Four;

    /// <summary>Yields the value sqrt(2), Pythagoras constant</summary>
    public static readonly TFloat Sqrt2 = TFloat.CreateChecked(1.414213562373095048801688724209698078569671875376948073176679737990732478462m);

    /// <summary>Yields the value sqrt(3), Theodorus' constant</summary>
    public static readonly TFloat Sqrt3 = TFloat.CreateChecked(1.732050807568877293527446341505872366942805253810380628055806979451933016909m);

    /// <summary>Yields the value sqrt(5)</summary>
    public static readonly TFloat Sqrt5 = TFloat.CreateChecked(2.236067977499789696409173668731276235440618359611525724270897m);

    /// <summary>Yields the value of rootn(2, 12), Twelfth root of 2</summary>
    public static readonly TFloat TwelfthRootOf2 = TFloat.CreateChecked(1.05946309435929526456182529494634170077920431749418562855921m);
  }

  //public static partial class Number
  //{
  //  extension<TNumber>(TNumber)
  //    where TNumber : System.Numerics.INumber<TNumber>
  //  {
  //  }
  //}

  public static partial class NumberBaseConstants<TNumberBase>
    where TNumberBase : System.Numerics.INumberBase<TNumberBase>
  {
    #region Integers

    /// <summary>Yields the value 2</summary>
    public static readonly TNumberBase Two = TNumberBase.CreateChecked(2);

    /// <summary>Yields the value 3</summary>
    public static readonly TNumberBase Three = TNumberBase.CreateChecked(3);

    /// <summary>Yields the value 4</summary>
    public static readonly TNumberBase Four = TNumberBase.CreateChecked(4);

    /// <summary>Yields the value 5</summary>
    public static readonly TNumberBase Five = TNumberBase.CreateChecked(5);

    /// <summary>Yields the value 6</summary>
    public static readonly TNumberBase Six = TNumberBase.CreateChecked(6);

    /// <summary>Yields the value 8</summary>
    public static readonly TNumberBase Eight = TNumberBase.CreateChecked(8);

    /// <summary>Yields the value 9</summary>
    public static readonly TNumberBase Nine = TNumberBase.CreateChecked(9);

    /// <summary>Yields the value 10</summary>
    public static readonly TNumberBase Ten = TNumberBase.CreateChecked(10);

    /// <summary>Yields the value 63</summary>
    public static readonly TNumberBase SixtyThree = TNumberBase.CreateChecked(63);

    #endregion
  }
}
