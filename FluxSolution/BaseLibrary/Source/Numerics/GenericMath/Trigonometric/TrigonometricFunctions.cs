namespace Flux
{
  public static partial class GenericMath
  {
    #region Reciprocals with inverse trigonometric functionality.

    /// <summary>Returns the cotangent of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
    public static TSelf Cot<TSelf>(this TSelf x)
      where TSelf : System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.One / TSelf.Tan(x);

    /// <summary>Returns the secant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
    public static TSelf Sec<TSelf>(this TSelf x)
      where TSelf : System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.One / TSelf.Cos(x);

    /// <summary>Returns the cosecant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
    public static TSelf Csc<TSelf>(this TSelf x)
      where TSelf : System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.One / TSelf.Sin(x);

    /// <summary>Returns the inverse tangent (arctangent) of the specified angle using two parameters instead of one. I.e. the two-argument variant of arctangent.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Atan2"/>
    public static TSelf Atan2<TSelf>(this TSelf y, TSelf x)
      where TSelf : System.Numerics.ITrigonometricFunctions<TSelf>
    {
      var pi = TSelf.Pi; // Referenced 4 times.

      if (TSelf.IsZero(x)) // With x being zero, everything depends on y.
      {
        if (TSelf.IsNegative(y))
          return -pi.Divide(2);
        else if (TSelf.IsZero(y)) // With x AND y both being zero, the result is undefined.
          return TSelf.Zero; // This implementation returns zero.
        else // if (y > TSelf.Zero)
          return pi.Divide(2);
      }

      var atan = TSelf.Atan(y / x); // Called 3 times.

      if (TSelf.IsNegative(x))
        return TSelf.IsNegative(y) ? atan - pi : atan + pi;
      else // if (x > TSelf.Zero)
        return atan;
    }

    /// <summary>Returns the inverse cotangent of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
    public static TSelf Acot<TSelf>(this TSelf y)
      where TSelf : System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Atan(TSelf.One / y);

    /// <summary>Returns the inverse secant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
    public static TSelf Asec<TSelf>(this TSelf y)
      where TSelf : System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Acos(TSelf.One / y);

    /// <summary>Returns the inverse cosecant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
    public static TSelf Acsc<TSelf>(this TSelf y)
      where TSelf : System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Asin(TSelf.One / y);

    #endregion // Reciprocals with inverse trigonometric functionality.

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

    #region Versine/coversine/haversine with inverse trigonometric functionality.

    /// <summary>Returns the versed sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine"/>
    public static TSelf Versin<TSelf>(this TSelf x)
      where TSelf : System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.One - TSelf.Cos(x);

    /// <summary>Returns the coversed sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    public static TSelf Coversin<TSelf>(this TSelf x)
      where TSelf : System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.One - TSelf.Sin(x);

    /// <summary>Returns the versed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine"/>
    public static TSelf Vercosin<TSelf>(this TSelf x)
      where TSelf : System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.One + TSelf.Cos(x);

    /// <summary>Returns the coversed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    public static TSelf Covercosin<TSelf>(this TSelf x)
      where TSelf : System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.One + TSelf.Sin(x);

    /// <summary>Returns the haversed sine of the specified angle. This is the famous Haversin function.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine"/>
    public static TSelf Haversin<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => (TSelf.One - TSelf.Cos(x)).Divide(2);

    /// <summary>Returns the hacoversed sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine"/>
    public static TSelf Hacoversin<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => (TSelf.One - TSelf.Sin(x)).Divide(2);

    /// <summary>Returns the haversed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine"/>
    public static TSelf Havercosin<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => (TSelf.One + TSelf.Cos(x)).Divide(2);

    /// <summary>Returns the hacoversed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine"/>
    public static TSelf Hacovercosin<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => (TSelf.One + TSelf.Sin(x)).Divide(2);

    /// <summary>Returns the inverse of versed sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static TSelf Arcversin<TSelf>(this TSelf y)
      where TSelf : System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Acos(TSelf.One - y);

    /// <summary>Returns the inverse of versed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static TSelf Arcvercos<TSelf>(this TSelf y)
      where TSelf : System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Acos(y - TSelf.One);

    /// <summary>Returns the inverse of coversed sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static TSelf Arccoversin<TSelf>(this TSelf y)
      where TSelf : System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Asin(TSelf.One - y);

    /// <summary>Returns the inverse of coversed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static TSelf Arccovercos<TSelf>(this TSelf y)
      where TSelf : System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Asin(y - TSelf.One);

    /// <summary>Returns the inverse of haversed sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static TSelf Archaversin<TSelf>(this TSelf y)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Acos(TSelf.One - y.Multiply(2)); // An extra subtraction saves a call to the Sqrt function: 2 * System.Math.Asin(System.Math.Sqrt(y));

    /// <summary>Returns the inverse of haversed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static TSelf Archavercos<TSelf>(this TSelf y)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Acos(y.Multiply(2) - TSelf.One); // An extra subtraction saves a call to the Sqrt function: 2 * System.Math.Acos(System.Math.Sqrt(y));

    /// <summary>Returns the inverse of cohaversed sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static TSelf Archacoversin<TSelf>(this TSelf y)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Asin(TSelf.One - y.Multiply(2));

    /// <summary>Returns the inverse of cohaversed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static TSelf Archacovercos<TSelf>(this TSelf y)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Asin(y.Multiply(2) - TSelf.One);

    #endregion // Versine/coversine/haversine with inverse trigonometric functionality.

    #region Gudermannian with inverse trigonometric functionality.

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

    #endregion // Gudermannian with inverse trigonometric functionality.

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
