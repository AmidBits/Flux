//namespace Flux
//{
//  public static partial class Fx
//  {
//    #region Atan2 functions

//    /// <summary>
//    /// <para>Implementation of Atan2(y, x) resulting in [-Pi, +Pi].</para>
//    /// <para><seealso href="https://en.wikipedia.org/wiki/Atan2"/></para>
//    /// </summary>
//    // Rough first draft, needs examination.
//    public static TNumber Atan2<TNumber>(TNumber y, TNumber x)
//      where TNumber : System.Numerics.IFloatingPoint<TNumber>, System.Numerics.IFloatingPointConstants<TNumber>, System.Numerics.ITrigonometricFunctions<TNumber>
//      => x > TNumber.Zero ? TNumber.Atan(y / x)
//      : x < TNumber.Zero && y >= TNumber.Zero ? TNumber.Atan(y / x) + TNumber.Pi
//      : x < TNumber.Zero && y < TNumber.Zero ? TNumber.Atan(y / x) - TNumber.Pi
//      : x == TNumber.Zero && y > TNumber.Zero ? +(TNumber.Pi / (TNumber.One + TNumber.One))
//      : x == TNumber.Zero && y < TNumber.Zero ? -(TNumber.Pi / (TNumber.One + TNumber.One))
//      : TNumber.Zero; // Undefined

//    /// <summary>
//    /// <para>Convenience implementation that returns <see cref="double.Atan2(double, double)"/> as [0, +Tau) instead of [-Pi, +Pi], with 0 being 3 o'clock and rotating counter-clockwise.</para>
//    /// <para><seealso href="https://en.wikipedia.org/wiki/Atan2"/></para>
//    /// </summary>
//    /// <param name="y"></param>
//    /// <param name="x"></param>
//    /// <returns></returns>
//    /// <remarks>
//    /// <para>The method consists of one conditional branch which may incur an extra add operation.</para>
//    /// <para>This uses <see cref="double.Atan2(double, double)"/> in the traditional sense, but without any negative return values.</para>
//    /// </remarks>
//    public static TNumber Atan2Ccw<TNumber>(TNumber y, TNumber x)
//      where TNumber : System.Numerics.IFloatingPoint<TNumber>, System.Numerics.IFloatingPointConstants<TNumber>, System.Numerics.ITrigonometricFunctions<TNumber>
//      => Atan2(y, x) is var atan2 && atan2 < TNumber.Zero // Call Atan2 as usual, which means 0 is at 3 o'clock and rotating counter-clockwise.
//      ? (atan2 + TNumber.Tau) % TNumber.Tau // Adjust the negative portion of atan2, from -Pi..0 into +Pi..+Tau, which is just a matter of adding a full turn (Tau).
//      : atan2; // The positive range is already 0..+Pi, so return it.

//    /// <summary>
//    /// <para>Convenience implementation that returns <see cref="double.Atan2(double, double)"/> as [0, +Tau) instead of [-Pi, +Pi], with 0 being noon and rotating clockwise.</para>
//    /// <para><seealsoww href="https://en.wikipedia.org/wiki/Atan2"/></para>
//    /// </summary>
//    /// <param name="y"></param>
//    /// <param name="x"></param>
//    /// <returns></returns>
//    /// <remarks>
//    /// <para>The method consists of one conditional branch which may incur an extra add operation.</para>
//    /// <para>This the reverse rotation and 90 degree offset is done by passing (x, y) rather than (y, x) into <see cref="double.Atan2(double, double)"/>.</para>
//    /// </remarks>
//    public static TNumber Atan2Cw<TNumber>(TNumber y, TNumber x)
//      where TNumber : System.Numerics.IFloatingPoint<TNumber>, System.Numerics.IFloatingPointConstants<TNumber>, System.Numerics.ITrigonometricFunctions<TNumber>
//      => Atan2(x, y) is var atan2s && atan2s < TNumber.Zero // Call Atan2 with the arguments switched, which results in a transposition, where 0 is at noon and rotation is clockwise.
//      ? (atan2s + TNumber.Tau) % TNumber.Tau // Adjust the negative portion of atan2, from -Pi..0 into +Pi..+Tau, which is just a matter of adding a full turn (Tau).
//      : atan2s; // The positive range is already 0..+Pi, so return it.

//    #endregion // Atan2 functions

//    #region Gudermannian functions

//    /// <summary>Returns the Gudermannian of the specified value.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Gudermannian_function"/>
//    public static TNumber Gd<TNumber>(this TNumber value)
//      where TNumber : System.Numerics.IFloatingPoint<TNumber>, System.Numerics.IHyperbolicFunctions<TNumber>, System.Numerics.ITrigonometricFunctions<TNumber>
//      => TNumber.Atan(TNumber.Sinh(value));

//    // Inverse function:

//    /// <summary>Returns the inverse Gudermannian of the specified value.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Gudermannian_function#Inverse"/>
//    /// <remarks>The integral of the secant function defines the inverse of the Gudermannian function.</remarks>
//    /// <remarks>The lambertian function (lam) is a notation for the inverse of the gudermannian which is encountered in the theory of map projections.</remarks>
//    public static TNumber Agd<TNumber>(this TNumber value)
//      where TNumber : System.Numerics.IFloatingPoint<TNumber>, System.Numerics.IHyperbolicFunctions<TNumber>, System.Numerics.ITrigonometricFunctions<TNumber>
//      => TNumber.Atanh(TNumber.Sin(value));

//    #endregion // Gudermannian functions

//    #region Reciprocal functions

//    /// <summary>Returns the cotangent of the specified angle.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
//    public static TNumber Cot<TNumber>(this TNumber v)
//      where TNumber : System.Numerics.IFloatingPoint<TNumber>, System.Numerics.ITrigonometricFunctions<TNumber>
//      => TNumber.One / TNumber.Tan(v);

//    /// <summary>Returns the secant of the specified angle.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
//    public static TNumber Sec<TNumber>(this TNumber v)
//      where TNumber : System.Numerics.IFloatingPoint<TNumber>, System.Numerics.ITrigonometricFunctions<TNumber>
//      => TNumber.One / TNumber.Cos(v);

//    /// <summary>Returns the cosecant of the specified angle.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
//    public static TNumber Csc<TNumber>(this TNumber v)
//      where TNumber : System.Numerics.IFloatingPoint<TNumber>, System.Numerics.ITrigonometricFunctions<TNumber>
//      => TNumber.One / TNumber.Sin(v);

//    #endregion // Reciprocal functions

//    #region Inverse Reciprocal functions

//    /// <summary>Returns the inverse cotangent of the specified angle.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
//    public static TNumber Acot<TNumber>(this TNumber v)
//      where TNumber : System.Numerics.IFloatingPoint<TNumber>, System.Numerics.ITrigonometricFunctions<TNumber>
//      => TNumber.Atan(TNumber.One / v);

//    /// <summary>Returns the inverse secant of the specified angle.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
//    public static TNumber Asec<TNumber>(this TNumber v)
//      where TNumber : System.Numerics.IFloatingPoint<TNumber>, System.Numerics.ITrigonometricFunctions<TNumber>
//      => TNumber.Acos(TNumber.One / v);

//    /// <summary>Returns the inverse cosecant of the specified angle.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
//    public static TNumber Acsc<TNumber>(this TNumber v)
//      where TNumber : System.Numerics.IFloatingPoint<TNumber>, System.Numerics.ITrigonometricFunctions<TNumber>
//      => TNumber.Asin(TNumber.One / v);

//    #endregion // Inverse Reciprocal functions

//    #region Hyperbolic reciprocal functions

//    /// <summary>Returns the hyperbolic cotangent of the specified angle.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
//    public static TNumber Coth<TNumber>(this TNumber v)
//      where TNumber : System.Numerics.IFloatingPoint<TNumber>, System.Numerics.IHyperbolicFunctions<TNumber>
//      => TNumber.Cosh(v) / TNumber.Sinh(v);

//    /// <summary>Returns the hyperbolic secant of the specified angle.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
//    public static TNumber Sech<TNumber>(this TNumber v)
//      where TNumber : System.Numerics.IFloatingPoint<TNumber>, System.Numerics.IHyperbolicFunctions<TNumber>
//      => TNumber.One / TNumber.Cosh(v);

//    /// <summary>Returns the hyperbolic cosecant of the specified angle.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
//    public static TNumber Csch<TNumber>(this TNumber v)
//      where TNumber : System.Numerics.IFloatingPoint<TNumber>, System.Numerics.IHyperbolicFunctions<TNumber>
//      => TNumber.One / TNumber.Sinh(v);

//    #endregion // Hyperbolic reciprocal functions

//    #region Inverse hyperbolic reciprocal functions

//    /// <summary>Returns the inverse hyperbolic cosecant of the specified angle.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
//    public static TNumber Acsch<TNumber>(this TNumber v)
//      where TNumber : System.Numerics.IFloatingPoint<TNumber>, System.Numerics.IHyperbolicFunctions<TNumber>
//      => TNumber.Asinh(TNumber.One / v); // Cheaper versions than using Log and Sqrt functions: System.Math.Log(1 / x + System.Math.Sqrt(1 / x * x + 1));

//    /// <summary>Returns the inverse hyperbolic secant of the specified angle.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
//    public static TNumber Asech<TNumber>(this TNumber v)
//      where TNumber : System.Numerics.IFloatingPoint<TNumber>, System.Numerics.IHyperbolicFunctions<TNumber>
//      => TNumber.Acosh(TNumber.One / v); // Cheaper versions than using Log and Sqrt functions: System.Math.Log((1 + System.Math.Sqrt(1 - x * x)) / x);

//    /// <summary>Returns the inverse hyperbolic cotangent of the specified angle.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
//    public static TNumber Acoth<TNumber>(this TNumber v)
//      where TNumber : System.Numerics.IFloatingPoint<TNumber>, System.Numerics.IHyperbolicFunctions<TNumber>
//      => TNumber.Atanh(TNumber.One / v); // Cheaper versions than using log functions: System.Math.Log((x + 1) / (x - 1)) / 2;

//    #endregion // Inverse hyperbolic reciprocal functions

//    #region Versed functions

//    /// <summary>Returns the versed sine of the specified angle.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
//    public static TNumber Vsin<TNumber>(this TNumber value)
//      where TNumber : System.Numerics.IFloatingPoint<TNumber>, System.Numerics.ITrigonometricFunctions<TNumber>
//      => TNumber.One - TNumber.Cos(value);

//    /// <summary>Returns the versed cosine of the specified angle.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
//    public static TNumber Vcos<TNumber>(this TNumber value)
//      where TNumber : System.Numerics.IFloatingPoint<TNumber>, System.Numerics.ITrigonometricFunctions<TNumber>
//      => TNumber.One + TNumber.Cos(value);

//    /// <summary>Returns the coversed sine of the specified angle.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
//    public static TNumber Cvsin<TNumber>(this TNumber value)
//      where TNumber : System.Numerics.IFloatingPoint<TNumber>, System.Numerics.ITrigonometricFunctions<TNumber>
//      => TNumber.One - TNumber.Sin(value);

//    /// <summary>Returns the coversed cosine of the specified angle.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
//    public static TNumber Cvcos<TNumber>(this TNumber value)
//      where TNumber : System.Numerics.IFloatingPoint<TNumber>, System.Numerics.ITrigonometricFunctions<TNumber>
//      => TNumber.One + TNumber.Sin(value);

//    #endregion // Versed functions

//    #region Inverse versed functions

//    /// <summary>Returns the inverse of versed sine of the specified angle.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
//    public static TNumber Avsin<TNumber>(this TNumber y)
//      where TNumber : System.Numerics.IFloatingPoint<TNumber>, System.Numerics.ITrigonometricFunctions<TNumber>
//      => TNumber.Acos(TNumber.One - y);

//    /// <summary>Returns the inverse of versed cosine of the specified angle.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
//    public static TNumber Avcos<TNumber>(this TNumber y)
//      where TNumber : System.Numerics.IFloatingPoint<TNumber>, System.Numerics.ITrigonometricFunctions<TNumber>
//      => TNumber.Acos(y - TNumber.One);

//    /// <summary>Returns the inverse of coversed sine of the specified angle.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
//    public static TNumber Acvsin<TNumber>(this TNumber y)
//      where TNumber : System.Numerics.IFloatingPoint<TNumber>, System.Numerics.ITrigonometricFunctions<TNumber>
//      => TNumber.Asin(TNumber.One - y);

//    /// <summary>Returns the inverse of coversed cosine of the specified angle.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
//    public static TNumber Acvcos<TNumber>(this TNumber y)
//      where TNumber : System.Numerics.IFloatingPoint<TNumber>, System.Numerics.ITrigonometricFunctions<TNumber>
//      => TNumber.Asin(y - TNumber.One);

//    #endregion // Inverse versed functions

//    #region Haversed functions

//    /// <summary>Returns the haversed sine of the specified angle. This is the famous Haversin function.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
//    public static TNumber Hvsin<TNumber>(this TNumber value)
//      where TNumber : System.Numerics.IFloatingPoint<TNumber>, System.Numerics.ITrigonometricFunctions<TNumber>
//      => (TNumber.One - TNumber.Cos(value)) / TNumber.CreateChecked(2);

//    /// <summary>Returns the haversed cosine of the specified angle.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
//    public static TNumber Hvcos<TNumber>(this TNumber value)
//      where TNumber : System.Numerics.IFloatingPoint<TNumber>, System.Numerics.ITrigonometricFunctions<TNumber>
//      => (TNumber.One + TNumber.Cos(value)) / TNumber.CreateChecked(2);

//    /// <summary>Returns the hacoversed sine of the specified angle.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
//    public static TNumber Hcvsin<TNumber>(this TNumber value)
//      where TNumber : System.Numerics.IFloatingPoint<TNumber>, System.Numerics.ITrigonometricFunctions<TNumber>
//      => (TNumber.One - TNumber.Sin(value)) / TNumber.CreateChecked(2);

//    /// <summary>Returns the hacoversed cosine of the specified angle.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Versine#Definitions"/>
//    public static TNumber Hcvcos<TNumber>(this TNumber value)
//      where TNumber : System.Numerics.IFloatingPoint<TNumber>, System.Numerics.ITrigonometricFunctions<TNumber>
//      => (TNumber.One + TNumber.Sin(value)) / TNumber.CreateChecked(2);

//    #endregion // Haversed functions

//    #region Inverse haversed functions

//    /// <summary>Returns the inverse of haversed sine of the specified angle.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
//    public static TNumber Ahvsin<TNumber>(this TNumber y)
//      where TNumber : System.Numerics.IFloatingPoint<TNumber>, System.Numerics.ITrigonometricFunctions<TNumber>
//      => TNumber.Acos(TNumber.One - TNumber.CreateChecked(2) * y); // An extra subtraction saves a call to the Sqrt function: 2 * System.Math.Asin(System.Math.Sqrt(y));

//    /// <summary>Returns the inverse of haversed cosine of the specified angle.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
//    public static TNumber Ahvcos<TNumber>(this TNumber y)
//      where TNumber : System.Numerics.IFloatingPoint<TNumber>, System.Numerics.ITrigonometricFunctions<TNumber>
//      => TNumber.Acos(TNumber.CreateChecked(2) * y - TNumber.One); // An extra subtraction saves a call to the Sqrt function: 2 * System.Math.Acos(System.Math.Sqrt(y));

//    /// <summary>Returns the inverse of cohaversed sine of the specified angle.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
//    public static TNumber Ahcvsin<TNumber>(this TNumber y)
//      where TNumber : System.Numerics.IFloatingPoint<TNumber>, System.Numerics.ITrigonometricFunctions<TNumber>
//      => TNumber.Asin(TNumber.One - TNumber.CreateChecked(2) * y);

//    /// <summary>Returns the inverse of cohaversed cosine of the specified angle.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
//    public static TNumber Ahcvcos<TNumber>(this TNumber y)
//      where TNumber : System.Numerics.IFloatingPoint<TNumber>, System.Numerics.ITrigonometricFunctions<TNumber>
//      => TNumber.Asin(TNumber.CreateChecked(2) * y - TNumber.One);

//    #endregion // Inverse haverse functions

//    #region Sinc functions

//    /// <summary>Returns the normalized sinc of the specified value.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Sinc_function"/>
//    public static TNumber Sincn<TNumber>(this TNumber value)
//      where TNumber : System.Numerics.IFloatingPoint<TNumber>, System.Numerics.IFloatingPointConstants<TNumber>, System.Numerics.ITrigonometricFunctions<TNumber>
//      => Sincu(TNumber.Pi * value);

//    /// <summary>Returns the unnormalized sinc of the specified value.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Sinc_function"/>
//    public static TNumber Sincu<TNumber>(this TNumber value)
//      where TNumber : System.Numerics.IFloatingPoint<TNumber>, System.Numerics.ITrigonometricFunctions<TNumber>
//      => TNumber.IsZero(value) ? TNumber.One : TNumber.Sin(value) / value;

//    #endregion // Sinc functions
//  }
//}
