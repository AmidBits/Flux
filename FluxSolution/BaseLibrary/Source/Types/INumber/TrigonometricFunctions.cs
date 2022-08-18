#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class ExtensionMethods
  {
    #region Trigonometric/inverse functionality.

    /// <summary>PREVIEW! Returns the sin of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
    public static TSelf Sin<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Sin(x);

    /// <summary>PREVIEW! Returns the cos of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
    public static TSelf Cos<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Cos(x);

    /// <summary>PREVIEW! Returns the tan of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
    public static TSelf Tan<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Tan(x);

    /// <summary>PREVIEW! Returns the cosecant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
    public static TSelf Csc<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.One / TSelf.Sin(x);

    /// <summary>PREVIEW! Returns the secant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
    public static TSelf Sec<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.One / TSelf.Cos(x);

    /// <summary>PREVIEW! Returns the cotangent of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
    public static TSelf Cot<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.One / TSelf.Tan(x);

    /// <summary>PREVIEW! Returns the sin of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
    public static TSelf Asin<TSelf>(this TSelf y)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Asin(y);

    /// <summary>PREVIEW! Returns the cos of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
    public static TSelf Acos<TSelf>(this TSelf y)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Acos(y);

    /// <summary>PREVIEW! Returns the tan of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
    public static TSelf Atan<TSelf>(this TSelf y)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Atan(y);

    /// <summary>PREVIEW! Returns the tan of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
    public static TSelf Atan2<TSelf>(this TSelf y, TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Atan2(y, x);

    /// <summary>PREVIEW! Returns the inverse cosecant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
    public static TSelf Acsc<TSelf>(this TSelf y)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Asin(TSelf.One / y);

    /// <summary>PREVIEW! Returns the inverse secant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
    public static TSelf Asec<TSelf>(this TSelf y)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Acos(TSelf.One / y);

    /// <summary>PREVIEW! Returns the inverse cotangent of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
    public static TSelf Acot<TSelf>(this TSelf y)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Atan(TSelf.One / y);

    #endregion

    #region Hyperbolic/inverse functionality.

    /// <summary>PREVIEW! Returns the hyperbolic sin of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
    public static TSelf Sinh<TSelf>(this TSelf v)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.IExponentialFunctions<TSelf>
      => (TSelf.Exp(v) - TSelf.Exp(-v)) / (TSelf.One + TSelf.One);

    /// <summary>PREVIEW! Returns the hyperbolic cos of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
    public static TSelf Cosh<TSelf>(this TSelf v)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.IExponentialFunctions<TSelf>
      => (TSelf.Exp(v) + TSelf.Exp(-v)) / (TSelf.One + TSelf.One);

    /// <summary>PREVIEW! Returns the hyperbolic tan of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
    public static TSelf Tanh<TSelf>(this TSelf v)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.IExponentialFunctions<TSelf>
      => Cosh(v) / Sinh(v);

    /// <summary>PREVIEW! Returns the hyperbolic cosecant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
    public static TSelf Csch<TSelf>(this TSelf v)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.IExponentialFunctions<TSelf>
      => TSelf.One / Sinh(v);

    /// <summary>PREVIEW! Returns the hyperbolic secant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
    public static TSelf Sech<TSelf>(this TSelf v)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.IExponentialFunctions<TSelf>
      => TSelf.One / Cosh(v);

    /// <summary>PREVIEW! Returns the hyperbolic cotangent of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
    public static TSelf Coth<TSelf>(this TSelf v)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.IExponentialFunctions<TSelf>
      => Cosh(v) / Sinh(v);

    /// <summary>PREVIEW! Returns the inverse hyperbolic sin of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
    static public TSelf Asinh<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ILogarithmicFunctions<TSelf>, System.Numerics.IPowerFunctions<TSelf>, System.Numerics.IRootFunctions<TSelf>
      => TSelf.Log(x + TSelf.Sqrt((x * x) + TSelf.One));

    /// <summary>PREVIEW! Returns the inverse hyperbolic cos of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
    static public TSelf Acosh<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ILogarithmicFunctions<TSelf>, System.Numerics.IPowerFunctions<TSelf>, System.Numerics.IRootFunctions<TSelf>
      => TSelf.Log(x + TSelf.Sqrt((x * x) - TSelf.One));

    /// <summary>PREVIEW! Returns the inverse hyperbolic tan of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
    static public TSelf Atanh<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ILogarithmicFunctions<TSelf>, System.Numerics.IRootFunctions<TSelf>
      => TSelf.Log((TSelf.One + x) / (TSelf.One - x)) / (TSelf.One + TSelf.One);

    /// <summary>PREVIEW! Returns the inverse hyperbolic cosecant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
    static public TSelf Acsch<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ILogarithmicFunctions<TSelf>, System.Numerics.IRootFunctions<TSelf>
      => TSelf.Log(TSelf.One / x + TSelf.Sqrt(TSelf.One / (x * x) + TSelf.One));

    /// <summary>PREVIEW! Returns the inverse hyperbolic secant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
    static public TSelf Asech<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ILogarithmicFunctions<TSelf>, System.Numerics.IRootFunctions<TSelf>
      => TSelf.Log((TSelf.One + TSelf.Sqrt(TSelf.One - (x * x))) / x);

    /// <summary>PREVIEW! Returns the inverse hyperbolic cotangent of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
    static public TSelf Acoth<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ILogarithmicFunctions<TSelf>
      => TSelf.Log((x + TSelf.One) / (x - TSelf.One)) / (TSelf.One + TSelf.One);

    #endregion

    #region Versed/inverse trigonometric functionality.

    /// <summary>PREVIEW! Returns the versed sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine"/>
    public static TSelf Vsin<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.One - TSelf.Cos(x);

    /// <summary>PREVIEW! Returns the versed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine"/>
    public static TSelf Vcos<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.One + TSelf.Cos(x);

    /// <summary>PREVIEW! Returns the inverse of versed sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static TSelf Avsin<TSelf>(this TSelf y)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Acos(TSelf.One - y);

    /// <summary>PREVIEW! Returns the inverse of versed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static TSelf Avcos<TSelf>(this TSelf y)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Acos(y - TSelf.One);

    #endregion

    #region Coversed/inverse trigonometric functionality.

    /// <summary>PREVIEW! Returns the coversed sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    public static TSelf Cvsin<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.One - TSelf.Sin(x);

    /// <summary>PREVIEW! Returns the coversed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    public static TSelf Cvcos<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.One + TSelf.Sin(x);

    /// <summary>PREVIEW! Returns the inverse of coversed sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static TSelf Acvsin<TSelf>(this TSelf y)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Asin(TSelf.One - y);

    /// <summary>PREVIEW! Returns the inverse of coversed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static TSelf Acvcos<TSelf>(this TSelf y)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Asin(y - TSelf.One);

    #endregion

    #region Haversed/inverse trigonometric functionality.

    /// <summary>PREVIEW! Returns the haversed sine of the specified angle. This is the famous Haversin function.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine"/>
    public static TSelf Hvsin<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => (TSelf.One - TSelf.Cos(x)) / (TSelf.One + TSelf.One);

    /// <summary>PREVIEW! Returns the haversed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine"/>
    public static TSelf Hvcos<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => (TSelf.One + TSelf.Cos(x)) / (TSelf.One + TSelf.One);

    /// <summary>PREVIEW! Returns the inverse of haversed sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static TSelf Ahvsin<TSelf>(this TSelf y)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Acos(TSelf.One - (TSelf.One + TSelf.One) * y); // An extra subtraction saves a call to the Sqrt function: 2 * System.Math.Asin(System.Math.Sqrt(y));

    /// <summary>PREVIEW! Returns the inverse of haversed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static TSelf Ahvcos<TSelf>(this TSelf y)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Acos((TSelf.One + TSelf.One) * y - TSelf.One); // An extra subtraction saves a call to the Sqrt function: 2 * System.Math.Acos(System.Math.Sqrt(y));

    #endregion

    #region Hacoversed/inverse trigonometric functionality.

    /// <summary>PREVIEW! Returns the hacoversed sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine"/>
    public static TSelf Hcvsin<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => (TSelf.One - TSelf.Sin(x)) / (TSelf.One + TSelf.One);

    /// <summary>PREVIEW! Returns the hacoversed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine"/>
    public static TSelf Hcvcos<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => (TSelf.One + TSelf.Sin(x)) / (TSelf.One + TSelf.One);

    /// <summary>PREVIEW! Returns the inverse of cohaversed sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static TSelf Ahcvsin<TSelf>(this TSelf y)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Asin(TSelf.One - (TSelf.One + TSelf.One) * y);

    /// <summary>PREVIEW! Returns the inverse of cohaversed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static TSelf Ahcvcos<TSelf>(this TSelf y)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Asin((TSelf.One + TSelf.One) * y - TSelf.One);

    #endregion

    #region Gudermannian/inverse trigonometric functionality.

    /// <summary>PREVIEW! Returns the Gudermannian of the specified value.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Gudermannian_function"/>
    public static TSelf Gd<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.IExponentialFunctions<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Atan(x.Sinh());

    /// <summary>PREVIEW! Returns the inverse Gudermannian of the specified value.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Gudermannian_function#Inverse"/>
    /// <remarks>The integral of the secant function defines the inverse of the Gudermannian function.</remarks>
    /// <remarks>The lambertian function (lam) is a notation for the inverse of the gudermannian which is encountered in the theory of map projections.</remarks>
    public static TSelf Agd<TSelf>(this TSelf y)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ILogarithmicFunctions<TSelf>, System.Numerics.IRootFunctions<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Sin(y).Atanh();

    #endregion
  }
}
#endif
