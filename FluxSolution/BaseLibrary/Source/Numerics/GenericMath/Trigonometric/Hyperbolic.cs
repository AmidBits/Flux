namespace Flux
{
  public static partial class GenericMath
  {
    #region Hyperbolic with inverse trigonometric functionality.

    /// <summary>Returns the hyperbolic cotangent of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
    public static TSelf Coth<TSelf>(this TSelf v)
      where TSelf : System.Numerics.IHyperbolicFunctions<TSelf>
      => TSelf.Cosh(v) / TSelf.Sinh(v);

    /// <summary>Returns the hyperbolic secant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
    public static TSelf Sech<TSelf>(this TSelf v)
      where TSelf : System.Numerics.IHyperbolicFunctions<TSelf>
      => TSelf.One / TSelf.Cosh(v);

    /// <summary>Returns the hyperbolic cosecant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
    public static TSelf Csch<TSelf>(this TSelf v)
      where TSelf : System.Numerics.IHyperbolicFunctions<TSelf>
      => TSelf.One / TSelf.Sinh(v);

    /// <summary>Returns the inverse hyperbolic cotangent of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
    static public TSelf Acoth<TSelf>(this TSelf x)
      where TSelf : System.Numerics.ILogarithmicFunctions<TSelf>
      => TSelf.Log((x + TSelf.One) / (x - TSelf.One)).Divide(2);

    /// <summary>Returns the inverse hyperbolic secant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
    static public TSelf Asech<TSelf>(this TSelf x)
      where TSelf : System.Numerics.ILogarithmicFunctions<TSelf>, System.Numerics.IRootFunctions<TSelf>
      => TSelf.Log((TSelf.One + TSelf.Sqrt(TSelf.One - (x * x))) / x);

    /// <summary>Returns the inverse hyperbolic cosecant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
    static public TSelf Acsch<TSelf>(this TSelf x)
      where TSelf : System.Numerics.ILogarithmicFunctions<TSelf>, System.Numerics.IRootFunctions<TSelf>
      => TSelf.Log(TSelf.One / x + TSelf.Sqrt(TSelf.One / (x * x) + TSelf.One));

    #endregion // Hyperbolic with inverse trigonometric functionality.
  }
}
