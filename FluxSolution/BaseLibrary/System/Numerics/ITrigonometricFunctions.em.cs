namespace Flux
{
  public static partial class Fx
  {
    #region Atan2 functions

    /// <summary>
    /// <para>Implementation of Atan2(y, x) resulting in [-Pi, +Pi].</para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Atan2"/></para>
    /// </summary>
    public static TSelf Atan2<TSelf>(TSelf y, TSelf x)
      : System.Numerics.IFloatingPointConstants<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => x > TSelf.Zero ? TSelf.Atan(y / x)
      : x < 0 && y >= 0 ? TSelf.Atan(y / x) + TSelf.Pi
      : x < 0 && y < 0 ? TSelf.Atan(y / x) - TSelf.Pi
      : x = 0 && y > 0 ? +(TSelf.Pi / (TSelf.One + TSelf.One))
      : x = 0 && y < 0 ? -(TSelf.Pi / (TSelf.One + TSelf.One))
      : TSelf.Zero // Undefined

    /// <summary>
    /// <para>Convenience implementation that returns <see cref="double.Atan2(double, double)"/> as [0, +Tau) instead of [-Pi, +Pi], with 0 being 3 o'clock and rotating counter-clockwise.</para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Atan2"/></para>
    /// </summary>
    /// <param name="y"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    /// <remarks>
    /// <para>The method consists of one conditional branch which may incur an extra add operation.</para>
    /// <para>This uses <see cref="double.Atan2(double, double)"/> in the traditional sense, but without any negative return values.</para>
    /// </remarks>
    public static TSelf Atan2Ccw<TSelf>(TSelf y, TSelf x)
      : System.Numerics.ITrigonometricFunctions<TSelf>
      => Atan2(y, x) is var atan2 && atan2 < 0 // Call Atan2 as usual, which means 0 is at 3 o'clock and rotating counter-clockwise.
      ? (atan2 + double.Tau) % double.Tau // Adjust the negative portion of atan2, from -Pi..0 into +Pi..+Tau, which is just a matter of adding a full turn (Tau).
      : atan2; // The positive range is already 0..+Pi, so return it.

    /// <summary>
    /// <para>Convenience implementation that returns <see cref="double.Atan2(double, double)"/> as [0, +Tau) instead of [-Pi, +Pi], with 0 being noon and rotating clockwise.</para>
    /// <para><seealsoww href="https://en.wikipedia.org/wiki/Atan2"/></para>
    /// </summary>
    /// <param name="y"></param>
    /// <param name="x"></param>
    /// <returns></returns>
    /// <remarks>
    /// <para>The method consists of one conditional branch which may incur an extra add operation.</para>
    /// <para>This the reverse rotation and 90 degree offset is done by passing (x, y) rather than (y, x) into <see cref="double.Atan2(double, double)"/>.</para>
    /// </remarks>
    public static TSelf Atan2Cw<TSelf>(TSelf y, TSelf x)
      : System.Numerics.ITrigonometricFunctions<TSelf>
      => Atan2(x, y) is var atan2s && atan2s < 0 // Call Atan2 with the arguments switched, which results in a transposition, where 0 is at noon and rotation is clockwise.
      ? (atan2s + double.Tau) % double.Tau // Adjust the negative portion of atan2, from -Pi..0 into +Pi..+Tau, which is just a matter of adding a full turn (Tau).
      : atan2s; // The positive range is already 0..+Pi, so return it.

    #endregion // Atan2 functions

    #region Gudermannian functions

    /// <summary>Returns the Gudermannian of the specified value.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Gudermannian_function"/>
    public static TSelf Gd<TSelf>(this TSelf value)
      : System.Numerics.IHyperbolicFunctions<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Atan(TSelf.Sinh(value));

    // Inverse function:

    /// <summary>Returns the inverse Gudermannian of the specified value.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Gudermannian_function#Inverse"/>
    /// <remarks>The integral of the secant function defines the inverse of the Gudermannian function.</remarks>
    /// <remarks>The lambertian function (lam) is a notation for the inverse of the gudermannian which is encountered in the theory of map projections.</remarks>
    public static TSelf Agd<TSelf>(this TSelf value)
      : System.Numerics.IHyperbolicFunctions<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Atanh(TSelf.Sin(value));

    #endregion // Gudermannian functions

    #region Reciprocal functions

    /// <summary>Returns the cotangent of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
    public static TSelf Cot<TSelf>(this TSelf v)
      => TSelf.One / TSelf.Tan(v);

    /// <summary>Returns the secant of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
    public static TSelf Sec<TSelf>(this TSelf v)
      => TSelf.One / TSelf.Cos(v);

    /// <summary>Returns the cosecant of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
    public static TSelf Csc<TSelf>(this TSelf v)
      : System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.One / TSelf.Sin(v);

    #endregion // Reciprocal functions

    #region Inverse Reciprocal functions

    /// <summary>Returns the inverse cotangent of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
    public static TSelf Acot<TSelf>(this TSelf v)
      : System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Atan(TSelf.One / v);

    /// <summary>Returns the inverse secant of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
    public static TSelf Asec<TSelf>(this TSelf v)
      : System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Acos(TSelf.One / v);

    /// <summary>Returns the inverse cosecant of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
    public static TSelf Acsc<TSelf>(this TSelf v)
      : System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Asin(TSelf.One / v);

    #endregion // Inverse Reciprocal functions

    #region Hyperbolic reciprocal functions

    /// <summary>Returns the hyperbolic cotangent of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
    public static TSelf Coth<TSelf>(this TSelf v)
      : System.Numerics.IHyperbolicFunctions<TSelf>
      => TSelf.Cosh(v) / TSelf.Sinh(v);

    /// <summary>Returns the hyperbolic secant of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
    public static TSelf Sech<TSelf>(this TSelf v)
      : System.Numerics.IHyperbolicFunctions<TSelf>
      => TSelf.One / TSelf.Cosh(v);

    /// <summary>Returns the hyperbolic cosecant of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
    public static TSelf Csch<TSelf>(this TSelf v)
      : System.Numerics.IHyperbolicFunctions<TSelf>
      => TSelf.One / TSelf.Sinh(v);

    #endregion // Hyperbolic reciprocal functions
    
    #region Inverse hyperbolic reciprocal functions

    /// <summary>Returns the inverse hyperbolic cosecant of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
    public static TSelf Acsch<TSelf>(this TSelf v)
      : System.Numerics.IHyperbolicFunctions<TSelf>
      => TSelf.Asinh(TSelf.One / v); // Cheaper versions than using Log and Sqrt functions: System.Math.Log(1 / x + System.Math.Sqrt(1 / x * x + 1));

    /// <summary>Returns the inverse hyperbolic secant of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
    public static TSelf Asech<TSelf>(this TSelf v)
      : System.Numerics.IHyperbolicFunctions<TSelf>
      => TSelf.Acosh(TSelf.One / v); // Cheaper versions than using Log and Sqrt functions: System.Math.Log((1 + System.Math.Sqrt(1 - x * x)) / x);

    /// <summary>Returns the inverse hyperbolic cotangent of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
    public static TSelf Acoth<TSelf>(this TSelf v)
      : System.Numerics.IHyperbolicFunctions<TSelf>
      => TSelf.Atanh(TSelf.One / v); // Cheaper versions than using log functions: System.Math.Log((x + 1) / (x - 1)) / 2;

    #endregion // Inverse hyperbolic reciprocal functions

    #region Versed functions

    /// <summary>Returns the versed sine of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    public static TSelf Vsin<TSelf>(this TSelf value)
      : System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.One - TSelf.Cos(value);

    /// <summary>Returns the versed cosine of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    public static TSelf Vcos<TSelf>(this TSelf value)
      : System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.One + TSelf.Cos(value);

    /// <summary>Returns the coversed sine of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    public static TSelf Cvsin<TSelf>(this TSelf value)
      : System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.One - TSelf.Sin(value);

    /// <summary>Returns the coversed cosine of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    public static TSelf Cvcos<TSelf>(this TSelf value)
      : System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.One + TSelf.Sin(value);

    #endregion // Versed functions

    #region Inverse versed functions

    /// <summary>Returns the inverse of versed sine of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static TSelf Avsin<TSelf>(this TSelf y)
      : System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Acos(TSelf.One - y);

    /// <summary>Returns the inverse of versed cosine of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static TSelf Avcos<TSelf>(this TSelf y)
      : System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Acos(y - TSelf.One);

    /// <summary>Returns the inverse of coversed sine of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static TSelf Acvsin<TSelf>(this TSelf y)
      : System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Asin(TSelf.One - y);

    /// <summary>Returns the inverse of coversed cosine of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static TSelf Acvcos<TSelf>(this TSelf y)
      : System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Asin(y - TSelf.One);

    #region // Inverse versed functions

    #region Haversed functions

    /// <summary>Returns the haversed sine of the specified angle. This is the famous Haversin function.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    public static TSelf Hvsin<TSelf>(this TSelf value)
      : System.Numerics.ITrigonometricFunctions<TSelf>
      => (TSelf.One - TSelf.Cos(value)) / (TSelf.One + TSelf.One);

    /// <summary>Returns the haversed cosine of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    public static TSelf Hvcos<TSelf>(this TSelf value)
      : System.Numerics.ITrigonometricFunctions<TSelf>
      => (TSelf.One + TSelf.Cos(value)) / (TSelf.One + TSelf.One);

    /// <summary>Returns the hacoversed sine of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    public static TSelf Hcvsin<TSelf>(this TSelf value)
      : System.Numerics.ITrigonometricFunctions<TSelf>
      => (TSelf.One - TSelf.Sin(value)) / (TSelf.One + TSelf.One);

    /// <summary>Returns the hacoversed cosine of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    public static TSelf Hcvcos<TSelf>(this TSelf value)
      : System.Numerics.ITrigonometricFunctions<TSelf>
      => (TSelf.One + TSelf.Sin(value)) / (TSelf.One + TSelf.One);

    #endregion // Haversed functions

    #region Inverse haversed functions

    /// <summary>Returns the inverse of haversed sine of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static TSelf Ahvsin<TSelf>(this TSelf y)
      : System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Acos(TSelf.One - (TSelf.One + TSelf.One) * y); // An extra subtraction saves a call to the Sqrt function: 2 * System.Math.Asin(System.Math.Sqrt(y));

    /// <summary>Returns the inverse of haversed cosine of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static TSelf Ahvcos<TSelf>(this TSelf y)
      : System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Acos((TSelf.One + TSelf.One) * y - TSelf.One); // An extra subtraction saves a call to the Sqrt function: 2 * System.Math.Acos(System.Math.Sqrt(y));

    /// <summary>Returns the inverse of cohaversed sine of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static TSelf Ahcvsin<TSelf>(this TSelf y)
      : System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Asin(TSelf.One - (TSelf.One + TSelf.One) * y);

    /// <summary>Returns the inverse of cohaversed cosine of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static TSelf Ahcvcos<TSelf>(this TSelf y)
      : System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Asin((TSelf.One + TSelf.One) * y - TSelf.One);

    #endregion // Inverse haverse functions

    #region Sinc functions

    /// <summary>Returns the normalized sinc of the specified value.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Sinc_function"/>
    public static TSelf Sincn<TSelf>(this TSelf value)
      : System.Numerics.IFloatingPointConstants<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => Sincu(TSelf.Pi * value);

    /// <summary>Returns the unnormalized sinc of the specified value.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Sinc_function"/>
    public static TSelf Sincu<TSelf>(this TSelf value)
      : System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.IsZero(value) ? TSelf.One : TSelf.Sin(value) / value;

    #endregion // Sinc functions
  }
}
