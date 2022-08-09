#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Returns the hyperbolic sin of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
    public static TSelf Sinh<TSelf>(this TSelf v)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.IExponentialFunctions<TSelf>
      => (TSelf.Exp(v) - TSelf.Exp(-v)) / (TSelf.One + TSelf.One);

    /// <summary>Returns the hyperbolic cos of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
    public static TSelf Cosh<TSelf>(this TSelf v)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.IExponentialFunctions<TSelf>
      => (TSelf.Exp(v) + TSelf.Exp(-v)) / (TSelf.One + TSelf.One);

    /// <summary>Returns the hyperbolic tan of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
    public static TSelf Tanh<TSelf>(this TSelf v)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.IExponentialFunctions<TSelf>
      => Cosh(v) / Sinh(v);

    /// <summary>Returns the hyperbolic cosecant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
    public static TSelf Csch<TSelf>(this TSelf v)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.IExponentialFunctions<TSelf>
      => TSelf.One / Sinh(v);

    /// <summary>Returns the hyperbolic secant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
    public static TSelf Sech<TSelf>(this TSelf v)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.IExponentialFunctions<TSelf>
      => TSelf.One / Cosh(v);

    /// <summary>Returns the hyperbolic cotangent of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
    public static TSelf Coth<TSelf>(this TSelf v)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.IExponentialFunctions<TSelf>
      => Cosh(v) / Sinh(v);

    /// <summary>Returns the inverse hyperbolic sin of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
    static public TSelf Asinh<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ILogarithmicFunctions<TSelf>, System.Numerics.IPowerFunctions<TSelf>, System.Numerics.IRootFunctions<TSelf>
      => TSelf.Log(x + TSelf.Sqrt((x * x) + TSelf.One));

    /// <summary>Returns the inverse hyperbolic cos of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
    static public TSelf Acosh<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ILogarithmicFunctions<TSelf>, System.Numerics.IPowerFunctions<TSelf>, System.Numerics.IRootFunctions<TSelf>
      => TSelf.Log(x + TSelf.Sqrt((x * x) - TSelf.One));

    /// <summary>Returns the inverse hyperbolic tan of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
    static public TSelf Atanh<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ILogarithmicFunctions<TSelf>, System.Numerics.IRootFunctions<TSelf>
      => TSelf.Log((TSelf.One + x) / (TSelf.One - x)) / x.Two();

    /// <summary>Returns the inverse hyperbolic cosecant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
    static public TSelf Acsch<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ILogarithmicFunctions<TSelf>, System.Numerics.IRootFunctions<TSelf>
      => TSelf.Log(TSelf.One / x + TSelf.Sqrt(TSelf.One / (x * x) + TSelf.One));

    /// <summary>Returns the inverse hyperbolic secant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
    static public TSelf Asech<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ILogarithmicFunctions<TSelf>, System.Numerics.IRootFunctions<TSelf>
      => TSelf.Log((TSelf.One + TSelf.Sqrt(TSelf.One - (x * x))) / x);

    /// <summary>Returns the inverse hyperbolic cotangent of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
    static public TSelf Acoth<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ILogarithmicFunctions<TSelf>
      => TSelf.Log((x + TSelf.One) / (x - TSelf.One)) / x.Two();
  }
}
#endif
