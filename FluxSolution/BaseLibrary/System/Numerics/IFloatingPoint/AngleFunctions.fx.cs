namespace Flux
{
  public static partial class Fx
  {
    #region Atan2 functions

    /// <summary>
    /// <para>Implementation of Atan2(y, x) resulting in [-Pi, +Pi].</para>
    /// <para><seealso href="https://en.wikipedia.org/wiki/Atan2"/></para>
    /// </summary>
    // Rough first draft, needs examination.
    public static TValue Atan2<TValue>(TValue y, TValue x)
      where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.IFloatingPointConstants<TValue>, System.Numerics.ITrigonometricFunctions<TValue>
      => x > TValue.Zero ? TValue.Atan(y / x)
      : x < TValue.Zero && y >= TValue.Zero ? TValue.Atan(y / x) + TValue.Pi
      : x < TValue.Zero && y < TValue.Zero ? TValue.Atan(y / x) - TValue.Pi
      : x == TValue.Zero && y > TValue.Zero ? +(TValue.Pi / (TValue.One + TValue.One))
      : x == TValue.Zero && y < TValue.Zero ? -(TValue.Pi / (TValue.One + TValue.One))
      : TValue.Zero; // Undefined

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
    public static TValue Atan2Ccw<TValue>(TValue y, TValue x)
      where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.IFloatingPointConstants<TValue>, System.Numerics.ITrigonometricFunctions<TValue>
      => Atan2(y, x) is var atan2 && atan2 < TValue.Zero // Call Atan2 as usual, which means 0 is at 3 o'clock and rotating counter-clockwise.
      ? (atan2 + TValue.Tau) % TValue.Tau // Adjust the negative portion of atan2, from -Pi..0 into +Pi..+Tau, which is just a matter of adding a full turn (Tau).
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
    public static TValue Atan2Cw<TValue>(TValue y, TValue x)
      where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.IFloatingPointConstants<TValue>, System.Numerics.ITrigonometricFunctions<TValue>
      => Atan2(x, y) is var atan2s && atan2s < TValue.Zero // Call Atan2 with the arguments switched, which results in a transposition, where 0 is at noon and rotation is clockwise.
      ? (atan2s + TValue.Tau) % TValue.Tau // Adjust the negative portion of atan2, from -Pi..0 into +Pi..+Tau, which is just a matter of adding a full turn (Tau).
      : atan2s; // The positive range is already 0..+Pi, so return it.

    #endregion // Atan2 functions

    #region Gudermannian functions

    /// <summary>Returns the Gudermannian of the specified value.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Gudermannian_function"/>
    public static TValue Gd<TValue>(this TValue value)
      where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.IHyperbolicFunctions<TValue>, System.Numerics.ITrigonometricFunctions<TValue>
      => TValue.Atan(TValue.Sinh(value));

    // Inverse function:

    /// <summary>Returns the inverse Gudermannian of the specified value.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Gudermannian_function#Inverse"/>
    /// <remarks>The integral of the secant function defines the inverse of the Gudermannian function.</remarks>
    /// <remarks>The lambertian function (lam) is a notation for the inverse of the gudermannian which is encountered in the theory of map projections.</remarks>
    public static TValue Agd<TValue>(this TValue value)
      where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.IHyperbolicFunctions<TValue>, System.Numerics.ITrigonometricFunctions<TValue>
      => TValue.Atanh(TValue.Sin(value));

    #endregion // Gudermannian functions

    #region Reciprocal functions

    /// <summary>Returns the cotangent of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
    public static TValue Cot<TValue>(this TValue v)
      where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.ITrigonometricFunctions<TValue>
      => TValue.One / TValue.Tan(v);

    /// <summary>Returns the secant of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
    public static TValue Sec<TValue>(this TValue v)
      where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.ITrigonometricFunctions<TValue>
      => TValue.One / TValue.Cos(v);

    /// <summary>Returns the cosecant of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
    public static TValue Csc<TValue>(this TValue v)
      where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.ITrigonometricFunctions<TValue>
      => TValue.One / TValue.Sin(v);

    #endregion // Reciprocal functions

    #region Inverse Reciprocal functions

    /// <summary>Returns the inverse cotangent of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
    public static TValue Acot<TValue>(this TValue v)
      where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.ITrigonometricFunctions<TValue>
      => TValue.Atan(TValue.One / v);

    /// <summary>Returns the inverse secant of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
    public static TValue Asec<TValue>(this TValue v)
      where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.ITrigonometricFunctions<TValue>
      => TValue.Acos(TValue.One / v);

    /// <summary>Returns the inverse cosecant of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
    public static TValue Acsc<TValue>(this TValue v)
      where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.ITrigonometricFunctions<TValue>
      => TValue.Asin(TValue.One / v);

    #endregion // Inverse Reciprocal functions

    #region Hyperbolic reciprocal functions

    /// <summary>Returns the hyperbolic cotangent of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
    public static TValue Coth<TValue>(this TValue v)
      where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.IHyperbolicFunctions<TValue>
      => TValue.Cosh(v) / TValue.Sinh(v);

    /// <summary>Returns the hyperbolic secant of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
    public static TValue Sech<TValue>(this TValue v)
      where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.IHyperbolicFunctions<TValue>
      => TValue.One / TValue.Cosh(v);

    /// <summary>Returns the hyperbolic cosecant of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
    public static TValue Csch<TValue>(this TValue v)
      where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.IHyperbolicFunctions<TValue>
      => TValue.One / TValue.Sinh(v);

    #endregion // Hyperbolic reciprocal functions

    #region Inverse hyperbolic reciprocal functions

    /// <summary>Returns the inverse hyperbolic cosecant of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
    public static TValue Acsch<TValue>(this TValue v)
      where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.IHyperbolicFunctions<TValue>
      => TValue.Asinh(TValue.One / v); // Cheaper versions than using Log and Sqrt functions: System.Math.Log(1 / x + System.Math.Sqrt(1 / x * x + 1));

    /// <summary>Returns the inverse hyperbolic secant of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
    public static TValue Asech<TValue>(this TValue v)
      where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.IHyperbolicFunctions<TValue>
      => TValue.Acosh(TValue.One / v); // Cheaper versions than using Log and Sqrt functions: System.Math.Log((1 + System.Math.Sqrt(1 - x * x)) / x);

    /// <summary>Returns the inverse hyperbolic cotangent of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
    public static TValue Acoth<TValue>(this TValue v)
      where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.IHyperbolicFunctions<TValue>
      => TValue.Atanh(TValue.One / v); // Cheaper versions than using log functions: System.Math.Log((x + 1) / (x - 1)) / 2;

    #endregion // Inverse hyperbolic reciprocal functions

    #region Versed functions

    /// <summary>Returns the versed sine of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    public static TValue Vsin<TValue>(this TValue value)
      where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.ITrigonometricFunctions<TValue>
      => TValue.One - TValue.Cos(value);

    /// <summary>Returns the versed cosine of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    public static TValue Vcos<TValue>(this TValue value)
      where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.ITrigonometricFunctions<TValue>
      => TValue.One + TValue.Cos(value);

    /// <summary>Returns the coversed sine of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    public static TValue Cvsin<TValue>(this TValue value)
      where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.ITrigonometricFunctions<TValue>
      => TValue.One - TValue.Sin(value);

    /// <summary>Returns the coversed cosine of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    public static TValue Cvcos<TValue>(this TValue value)
      where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.ITrigonometricFunctions<TValue>
      => TValue.One + TValue.Sin(value);

    #endregion // Versed functions

    #region Inverse versed functions

    /// <summary>Returns the inverse of versed sine of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static TValue Avsin<TValue>(this TValue y)
      where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.ITrigonometricFunctions<TValue>
      => TValue.Acos(TValue.One - y);

    /// <summary>Returns the inverse of versed cosine of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static TValue Avcos<TValue>(this TValue y)
      where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.ITrigonometricFunctions<TValue>
      => TValue.Acos(y - TValue.One);

    /// <summary>Returns the inverse of coversed sine of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static TValue Acvsin<TValue>(this TValue y)
      where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.ITrigonometricFunctions<TValue>
      => TValue.Asin(TValue.One - y);

    /// <summary>Returns the inverse of coversed cosine of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static TValue Acvcos<TValue>(this TValue y)
      where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.ITrigonometricFunctions<TValue>
      => TValue.Asin(y - TValue.One);

    #endregion // Inverse versed functions

    #region Haversed functions

    /// <summary>Returns the haversed sine of the specified angle. This is the famous Haversin function.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    public static TValue Hvsin<TValue>(this TValue value)
      where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.ITrigonometricFunctions<TValue>
      => (TValue.One - TValue.Cos(value)) / TValue.CreateChecked(2);

    /// <summary>Returns the haversed cosine of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    public static TValue Hvcos<TValue>(this TValue value)
      where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.ITrigonometricFunctions<TValue>
      => (TValue.One + TValue.Cos(value)) / TValue.CreateChecked(2);

    /// <summary>Returns the hacoversed sine of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    public static TValue Hcvsin<TValue>(this TValue value)
      where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.ITrigonometricFunctions<TValue>
      => (TValue.One - TValue.Sin(value)) / TValue.CreateChecked(2);

    /// <summary>Returns the hacoversed cosine of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    public static TValue Hcvcos<TValue>(this TValue value)
      where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.ITrigonometricFunctions<TValue>
      => (TValue.One + TValue.Sin(value)) / TValue.CreateChecked(2);

    #endregion // Haversed functions

    #region Inverse haversed functions

    /// <summary>Returns the inverse of haversed sine of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static TValue Ahvsin<TValue>(this TValue y)
      where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.ITrigonometricFunctions<TValue>
      => TValue.Acos(TValue.One - TValue.CreateChecked(2) * y); // An extra subtraction saves a call to the Sqrt function: 2 * System.Math.Asin(System.Math.Sqrt(y));

    /// <summary>Returns the inverse of haversed cosine of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static TValue Ahvcos<TValue>(this TValue y)
      where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.ITrigonometricFunctions<TValue>
      => TValue.Acos(TValue.CreateChecked(2) * y - TValue.One); // An extra subtraction saves a call to the Sqrt function: 2 * System.Math.Acos(System.Math.Sqrt(y));

    /// <summary>Returns the inverse of cohaversed sine of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static TValue Ahcvsin<TValue>(this TValue y)
      where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.ITrigonometricFunctions<TValue>
      => TValue.Asin(TValue.One - TValue.CreateChecked(2) * y);

    /// <summary>Returns the inverse of cohaversed cosine of the specified angle.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static TValue Ahcvcos<TValue>(this TValue y)
      where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.ITrigonometricFunctions<TValue>
      => TValue.Asin(TValue.CreateChecked(2) * y - TValue.One);

    #endregion // Inverse haverse functions

    #region Sinc functions

    /// <summary>Returns the normalized sinc of the specified value.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Sinc_function"/>
    public static TValue Sincn<TValue>(this TValue value)
      where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.IFloatingPointConstants<TValue>, System.Numerics.ITrigonometricFunctions<TValue>
      => Sincu(TValue.Pi * value);

    /// <summary>Returns the unnormalized sinc of the specified value.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Sinc_function"/>
    public static TValue Sincu<TValue>(this TValue value)
      where TValue : System.Numerics.IFloatingPoint<TValue>, System.Numerics.ITrigonometricFunctions<TValue>
      => TValue.IsZero(value) ? TValue.One : TValue.Sin(value) / value;

    #endregion // Sinc functions
  }
}
