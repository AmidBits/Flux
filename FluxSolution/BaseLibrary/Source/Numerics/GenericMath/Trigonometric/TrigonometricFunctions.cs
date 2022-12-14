namespace Flux
{
  public static partial class GenericMath
  {
    #region Trigonometric/inverse functionality.

    /// <summary>Returns the sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
    public static TSelf Sin<TSelf>(this TSelf x)
      where TSelf : System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Sin(x);

    /// <summary>Returns the cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
    public static TSelf Cos<TSelf>(this TSelf x)
      where TSelf : System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Cos(x);

    /// <summary>Returns the tangent of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
    public static TSelf Tan<TSelf>(this TSelf x)
      where TSelf : System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Tan(x);

    /// <summary>Returns the cosecant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
    public static TSelf Csc<TSelf>(this TSelf x)
      where TSelf : System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.One / TSelf.Sin(x);

    /// <summary>Returns the secant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
    public static TSelf Sec<TSelf>(this TSelf x)
      where TSelf : System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.One / TSelf.Cos(x);

    /// <summary>Returns the cotangent of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
    public static TSelf Cot<TSelf>(this TSelf x)
      where TSelf : System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.One / TSelf.Tan(x);

    /// <summary>Returns the inverse sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
    public static TSelf Asin<TSelf>(this TSelf y)
      where TSelf : System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Asin(y);

    /// <summary>Returns the inverse cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
    public static TSelf Acos<TSelf>(this TSelf y)
      where TSelf : System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Acos(y);

    /// <summary>Returns the inverse tangent (arctangent) of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
    public static TSelf Atan<TSelf>(this TSelf y)
      where TSelf : System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Atan(y);

    /// <summary>Returns the inverse tangent (arctangent) of the specified angle. I.e. the two-argument variant of arctangent.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
    public static TSelf Atan2<TSelf>(this TSelf y, TSelf x)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => TSelf.Atan2(y, x);

    /// <summary>Returns the inverse cosecant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
    public static TSelf Acsc<TSelf>(this TSelf y)
      where TSelf : System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Asin(TSelf.One / y);

    /// <summary>Returns the inverse secant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
    public static TSelf Asec<TSelf>(this TSelf y)
      where TSelf : System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Acos(TSelf.One / y);

    /// <summary>Returns the inverse cotangent of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
    public static TSelf Acot<TSelf>(this TSelf y)
      where TSelf : System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Atan(TSelf.One / y);

    #endregion

    #region Hyperbolic/inverse functionality.

    /// <summary>Returns the hyperbolic sin of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
    public static TSelf Sinh<TSelf>(this TSelf v)
      where TSelf : System.Numerics.IHyperbolicFunctions<TSelf>
      => TSelf.Sinh(v);
    //where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IExponentialFunctions<TSelf>
    //=> (TSelf.Exp(v) - TSelf.Exp(-v)) / (TSelf.One + TSelf.One);

    /// <summary>Returns the hyperbolic cos of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
    public static TSelf Cosh<TSelf>(this TSelf v)
      where TSelf : System.Numerics.IHyperbolicFunctions<TSelf>
      => TSelf.Cosh(v);
    //where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IExponentialFunctions<TSelf>
    //=> (TSelf.Exp(v) + TSelf.Exp(-v)) / (TSelf.One + TSelf.One);

    /// <summary>Returns the hyperbolic tan of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
    public static TSelf Tanh<TSelf>(this TSelf v)
      where TSelf : System.Numerics.IHyperbolicFunctions<TSelf>
      => TSelf.Tanh(v);
    //where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IExponentialFunctions<TSelf>
    //=> Cosh(v) / Sinh(v);

    /// <summary>Returns the hyperbolic cosecant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
    public static TSelf Csch<TSelf>(this TSelf v)
      where TSelf : System.Numerics.IHyperbolicFunctions<TSelf>
      => TSelf.One / TSelf.Sinh(v);
    //where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IExponentialFunctions<TSelf>
    //=> TSelf.One / Sinh(v);

    /// <summary>Returns the hyperbolic secant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
    public static TSelf Sech<TSelf>(this TSelf v)
      where TSelf : System.Numerics.IHyperbolicFunctions<TSelf>
      => TSelf.One / TSelf.Cosh(v);
    //where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IExponentialFunctions<TSelf>
    //=> TSelf.One / Cosh(v);

    /// <summary>Returns the hyperbolic cotangent of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
    public static TSelf Coth<TSelf>(this TSelf v)
      where TSelf : System.Numerics.IHyperbolicFunctions<TSelf>
      => TSelf.Cosh(v) / TSelf.Sinh(v);
    //where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.IExponentialFunctions<TSelf>
    //=> Cosh(v) / Sinh(v);

    /// <summary>Returns the inverse hyperbolic sin of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
    static public TSelf Asinh<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IHyperbolicFunctions<TSelf>
      => TSelf.Asinh(x);
    //where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.ILogarithmicFunctions<TSelf>, System.Numerics.IRootFunctions<TSelf>
    //=> TSelf.Log(x + TSelf.Sqrt((x * x) + TSelf.One));

    /// <summary>Returns the inverse hyperbolic cos of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
    static public TSelf Acosh<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IHyperbolicFunctions<TSelf>
      => TSelf.Acosh(x);
    //where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.ILogarithmicFunctions<TSelf>, System.Numerics.IRootFunctions<TSelf>
    //=> TSelf.Log(x + TSelf.Sqrt((x * x) - TSelf.One));

    /// <summary>Returns the inverse hyperbolic tan of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
    static public TSelf Atanh<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IHyperbolicFunctions<TSelf>
      => TSelf.Atanh(x);
    //where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.ILogarithmicFunctions<TSelf>
    //=> TSelf.Log((TSelf.One + x) / (TSelf.One - x)) / (TSelf.One + TSelf.One);

    /// <summary>Returns the inverse hyperbolic cosecant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
    static public TSelf Acsch<TSelf>(this TSelf x)
      where TSelf : System.Numerics.ILogarithmicFunctions<TSelf>, System.Numerics.IRootFunctions<TSelf>
      => TSelf.Log(TSelf.One / x + TSelf.Sqrt(TSelf.One / (x * x) + TSelf.One));

    /// <summary>Returns the inverse hyperbolic secant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
    static public TSelf Asech<TSelf>(this TSelf x)
      where TSelf : System.Numerics.ILogarithmicFunctions<TSelf>, System.Numerics.IRootFunctions<TSelf>
      => TSelf.Log((TSelf.One + TSelf.Sqrt(TSelf.One - (x * x))) / x);

    /// <summary>Returns the inverse hyperbolic cotangent of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
    static public TSelf Acoth<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ILogarithmicFunctions<TSelf>
      => TSelf.Log((x + TSelf.One) / (x - TSelf.One)).Divide(2);

    #endregion

    #region Versed/inverse trigonometric functionality.

    /// <summary>Returns the versed sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine"/>
    public static TSelf Vsin<TSelf>(this TSelf x)
      where TSelf : System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.One - TSelf.Cos(x);

    /// <summary>Returns the versed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine"/>
    public static TSelf Vcos<TSelf>(this TSelf x)
      where TSelf : System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.One + TSelf.Cos(x);

    /// <summary>Returns the inverse of versed sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static TSelf Avsin<TSelf>(this TSelf y)
      where TSelf : System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Acos(TSelf.One - y);

    /// <summary>Returns the inverse of versed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static TSelf Avcos<TSelf>(this TSelf y)
      where TSelf : System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Acos(y - TSelf.One);

    #endregion

    #region Coversed/inverse trigonometric functionality.

    /// <summary>Returns the coversed sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    public static TSelf Cvsin<TSelf>(this TSelf x)
      where TSelf : System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.One - TSelf.Sin(x);

    /// <summary>Returns the coversed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    public static TSelf Cvcos<TSelf>(this TSelf x)
      where TSelf : System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.One + TSelf.Sin(x);

    /// <summary>Returns the inverse of coversed sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static TSelf Acvsin<TSelf>(this TSelf y)
      where TSelf : System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Asin(TSelf.One - y);

    /// <summary>Returns the inverse of coversed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static TSelf Acvcos<TSelf>(this TSelf y)
      where TSelf : System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Asin(y - TSelf.One);

    #endregion

    #region Haversed/inverse trigonometric functionality.

    /// <summary>Returns the haversed sine of the specified angle. This is the famous Haversin function.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine"/>
    public static TSelf Hvsin<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => (TSelf.One - TSelf.Cos(x)).Divide(2);

    /// <summary>Returns the haversed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine"/>
    public static TSelf Hvcos<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => (TSelf.One + TSelf.Cos(x)).Divide(2);

    /// <summary>Returns the inverse of haversed sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static TSelf Ahvsin<TSelf>(this TSelf y)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Acos(TSelf.One - y.Multiply(2)); // An extra subtraction saves a call to the Sqrt function: 2 * System.Math.Asin(System.Math.Sqrt(y));

    /// <summary>Returns the inverse of haversed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static TSelf Ahvcos<TSelf>(this TSelf y)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Acos(y.Multiply(2) - TSelf.One); // An extra subtraction saves a call to the Sqrt function: 2 * System.Math.Acos(System.Math.Sqrt(y));

    #endregion

    #region Hacoversed/inverse trigonometric functionality.

    /// <summary>Returns the hacoversed sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine"/>
    public static TSelf Hcvsin<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => (TSelf.One - TSelf.Sin(x)).Divide(2);

    /// <summary>Returns the hacoversed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine"/>
    public static TSelf Hcvcos<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => (TSelf.One + TSelf.Sin(x)).Divide(2);

    /// <summary>Returns the inverse of cohaversed sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static TSelf Ahcvsin<TSelf>(this TSelf y)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Asin(TSelf.One - y.Multiply(2));

    /// <summary>Returns the inverse of cohaversed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static TSelf Ahcvcos<TSelf>(this TSelf y)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Asin(y.Multiply(2) - TSelf.One);

    #endregion

    #region Gudermannian/inverse trigonometric functionality.

    /// <summary>Returns the Gudermannian of the specified value.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Gudermannian_function"/>
    public static TSelf Gd<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IHyperbolicFunctions<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Atan(TSelf.Sinh(x));

    /// <summary>Returns the inverse Gudermannian of the specified value.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Gudermannian_function#Inverse"/>
    /// <remarks>The integral of the secant function defines the inverse of the Gudermannian function.</remarks>
    /// <remarks>The lambertian function (lam) is a notation for the inverse of the gudermannian which is encountered in the theory of map projections.</remarks>
    public static TSelf Agd<TSelf>(this TSelf y)
      where TSelf : System.Numerics.IHyperbolicFunctions<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Atanh(TSelf.Sin(y));

    #endregion

    #region Sinc normalized and unnormalized.
    /// <summary>Returns the normalized form sinc of the specified value.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Sinc_function"/>
    public static TSelf Sincn<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPointIeee754<TSelf>
      => Sincu(TSelf.Pi * x);

    /// <summary>Returns the unnormalized form sinc of the specified value.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Sinc_function"/>
    public static TSelf Sincu<TSelf>(this TSelf x)
      where TSelf : System.Numerics.IFloatingPoint<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => x != TSelf.Zero ? TSelf.Sin(x) / x : TSelf.One;
    #endregion Sinc normalized and unnormalized.
  }
}
