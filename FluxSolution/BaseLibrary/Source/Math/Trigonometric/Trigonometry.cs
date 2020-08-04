//namespace Flux
//{
//  public static partial class Math
//  {
//    #region Gudermannian functions
//    /// <summary>Returns the Gudermannian of the specified value.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Gudermannian_function"/>
//    public static double Gd(double value)
//      => System.Math.Atan(System.Math.Sinh(value));
//    /// <summary>Returns the Gudermannian of the specified value. Bonus result from sinh(value) in out parameter.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Gudermannian_function"/>
//    public static double Gd(double value, out double sinh)
//      => System.Math.Atan(sinh = System.Math.Sinh(value));
//    #endregion Gudermannian functions

//    #region Inverse gudermannian functions
//    /// <summary>Returns the inverse Gudermannian of the specified value.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Gudermannian_function#Inverse"/>
//    /// <remarks>The integral of the secant function defines the inverse of the Gudermannian function.</remarks>
//    /// <remarks>The lambertian function (lam) is a notation for the inverse of the gudermannian which is encountered in the theory of map projections.</remarks>
//    public static double Agd(double value)
//      => Atanh(System.Math.Sin(value));
//    /// <summary>Returns the inverse Gudermannian of the specified value. Bonus result from sin(value) in out parameter.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Gudermannian_function#Inverse"/>
//    /// <remarks>The integral of the secant function defines the inverse of the Gudermannian function.</remarks>
//    /// <remarks>The lambertian function (lam) is a notation for the inverse of the gudermannian which is encountered in the theory of map projections.</remarks>
//    public static double Agd(double value, out double sin)
//      => Atanh(sin = System.Math.Sin(value));
//    #endregion Inverse gudermannian functions

//    ///// <summary>The haversine formula determines the great-circle distance between two points on a sphere given their longitudes and latitudes.</summary>
//    //public static double Hav(double value) => System.Math.Pow(System.Math.Sin(value / 2), 2);
//    ///// <summary>The inverse of the haversine formula.</summary>
//    //public static double Ahav(double value) => 2 * System.Math.Asin(System.Math.Sqrt(value));

//    #region Sinc functions
//    /// <summary>Returns the normalized sinc of the specified value.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Sinc_function"/>
//    public static double Sincn(double value)
//      => Sincu(System.Math.PI * value);
//    /// <summary>Returns the unnormalized sinc of the specified value.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Sinc_function"/>
//    public static double Sincu(double value)
//      => value != 0 ? System.Math.Sin(value) / value : 1;
//    #endregion Sinc functions

//    #region Trigonometric functions
//    //[System.Obsolete("Use System.Math.Sin", true)]
//    //public static double Sin(double value) => System.Math.Sin(value);
//    //[System.Obsolete("Use System.Math.Cos", true)]
//    //public static double Cos(double value) => System.Math.Cos(value);
//    //[System.Obsolete("Use System.Math.Tan", true)]
//    //public static double Tan(double value) => System.Math.Tan(value);
//    /// <summary>Returns the cotangent of the specified angle.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
//    public static double Cot(double value)
//      => 1 / System.Math.Tan(value);
//    /// <summary>Returns the cotangent of the specified angle. Bonus result from tan(value) in out parameter.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
//    public static double Cot(double value, out double tan)
//      => 1 / (tan = System.Math.Tan(value));
//    /// <summary>Returns the secant of the specified angle.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
//    public static double Sec(double value)
//      => 1 / System.Math.Cos(value);
//    /// <summary>Returns the secant of the specified angle. Bonus result from cos(value) in out parameter.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
//    public static double Sec(double value, out double cos)
//      => 1 / (cos = System.Math.Cos(value));
//    /// <summary>Returns the cosecant of the specified angle.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
//    public static double Csc(double value)
//      => 1 / System.Math.Sin(value);
//    /// <summary>Returns the cosecant of the specified angle. Bonus result from sin(value) in out parameter.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
//    public static double Csc(double value, out double sin)
//      => 1 / (sin = System.Math.Sin(value));
//    #endregion Trigonometric functions

//    #region Inverse trigonometric functions
//    //[System.Obsolete("Use System.Math.Asin", true)]
//    //public static double Asin(double value) => System.Math.Asin(value);
//    //[System.Obsolete("Use System.Math.Acos", true)]
//    //public static double Acos(double value) => System.Math.Acos(value);
//    //[System.Obsolete("Use System.Math.Atan", true)]
//    //public static double Atan(double value) => System.Math.Atan(value);
//    /// <summary>Returns the inverse cotangent of the specified angle.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
//    public static double Acot(double value)
//      => System.Math.Atan(1 / value);
//    /// <summary>Returns the inverse cotangent of the specified angle.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
//    public static double Acot(double value, out double inverseOfValue)
//      => System.Math.Atan(inverseOfValue = (1 / value));
//    /// <summary>Returns the inverse secant of the specified angle.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
//    public static double Asec(double value)
//      => System.Math.Acos(1 / value);
//    /// <summary>Returns the inverse secant of the specified angle.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
//    public static double Asec(double value, out double inverseOfValue)
//      => System.Math.Acos(inverseOfValue = (1 / value));
//    /// <summary>Returns the inverse cosecant of the specified angle.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
//    public static double Acsc(double value)
//      => System.Math.Asin(1 / value);
//    /// <summary>Returns the inverse cosecant of the specified angle.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
//    public static double Acsc(double value, out double inverseValue)
//      => System.Math.Asin(inverseValue = (1 / value));
//    #endregion Inverse trigonometric functions

//    #region Hyperbolic functions
//    //[System.Obsolete("Use System.Math.Sinh", true)]
//    //public static double Sinh(double value) => System.Math.Sinh(value);
//    //[System.Obsolete("Use System.Math.Cosh", true)]
//    //public static double Cosh(double value) => System.Math.Cosh(value);
//    //[System.Obsolete("Use System.Math.Tanh", true)]
//    //public static double Tanh(double value) => System.Math.Tanh(value);
//    /// <summary>Returns the hyperbolic cotangent of the specified angle.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
//    public static double Coth(double value)
//      => System.Math.Exp(value) is var evp && System.Math.Exp(-value) is var evn ? (evp + evn) / (evp - evn) : throw new System.ArithmeticException();
//    /// <summary>Returns the hyperbolic cotangent of the specified angle. Bonus result from exp(value) and exp(-value) in out parameters.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
//    public static double Coth(double value, out double exp, out double expn)
//      => ((exp = System.Math.Exp(value)) + (expn = System.Math.Exp(-value))) / (exp - expn);
//    /// <summary>Returns the hyperbolic secant of the specified angle.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
//    public static double Sech(double value)
//      => 2 / (System.Math.Exp(value) + System.Math.Exp(-value));
//    /// <summary>Returns the hyperbolic secant of the specified angle. Bonus result from exp(value) and exp(-value) in out parameters.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
//    public static double Sech(double value, out double exp, out double expn)
//      => 2 / ((exp = System.Math.Exp(value)) + (expn = System.Math.Exp(-value)));
//    /// <summary>Returns the hyperbolic cosecant of the specified angle.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
//    public static double Csch(double value)
//      => 2 / (System.Math.Exp(value) - System.Math.Exp(-value));
//    /// <summary>Returns the hyperbolic cosecant of the specified angle. Bonus result from exp(value) and exp(-value) in out parameters.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Hyperbolic_function"/>
//    public static double Csch(double value, out double exp, out double expn)
//      => 2 / ((exp = System.Math.Exp(value)) - (expn = System.Math.Exp(-value)));
//    #endregion Hyperbolic functions

//    #region Inverse hyperbolic functions
//    /// <summary>Returns the inverse hyperbolic sine of the specified angle.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
//    static public double Asinh(double value)
//      => System.Math.Log(value + System.Math.Sqrt(value * value + 1));
//    /// <summary>Returns the inverse hyperbolic cosine of the specified angle.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
//    static public double Acosh(double value)
//      => System.Math.Log(value + System.Math.Sqrt(value * value - 1));
//    /// <summary>Returns the inverse hyperbolic tangent of the specified angle.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
//    static public double Atanh(double value)
//      => System.Math.Log((1 + value) / (1 - value)) / 2;
//    /// <summary>Returns the inverse hyperbolic cotangent of the specified angle.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
//    static public double Acoth(double value)
//      => System.Math.Log((value + 1) / (value - 1)) / 2;
//    /// <summary>Returns the inverse hyperbolic secant of the specified angle.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
//    static public double Asech(double value)
//      => System.Math.Log((System.Math.Sqrt(-value * value + 1) + 1) / value);
//    /// <summary>Returns the inverse hyperbolic cosecant of the specified angle.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Inverse_hyperbolic_function"/>
//    static public double Acsch(double value)
//      => System.Math.Log((System.Math.Sign(value) * System.Math.Sqrt(value * value + 1) + 1) / value);
//    #endregion Inverse hyperbolic functions

//    #region Versed functions
//    /// <summary>Returns the versed sine of the specified angle.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
//    public static double Vsin(double value)
//      => 1 - System.Math.Cos(value);
//    /// <summary>Returns the versed sine of the specified angle. Bonus result from cos(value) in out parameter.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
//    public static double Vsin(double value, out double cos)
//      => 1 - (cos = System.Math.Cos(value));
//    /// <summary>Returns the versed cosine of the specified angle.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
//    public static double Vcos(double value)
//      => 1 + System.Math.Cos(value);
//    /// <summary>Returns the versed cosine of the specified angle. Bonus result from cos(value) in out parameter.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
//    public static double Vcos(double value, out double cos)
//      => 1 + (cos = System.Math.Cos(value));
//    #endregion Versed functions

//    #region Inverse versed functions
//    /// <summary>Returns the inverse of versed sine of the specified angle.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
//    public static double Avsin(double value)
//      => System.Math.Acos(1 - value);
//    /// <summary>Returns the inverse of versed cosine of the specified angle.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
//    public static double Avcos(double value)
//      => System.Math.Acos(1 + value);
//    #endregion Inverse versed functions

//    #region Coversed functions
//    /// <summary>Returns the coversed sine of the specified angle.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
//    public static double Cvsin(double value)
//      => 1 - System.Math.Sin(value);
//    /// <summary>Returns the coversed sine of the specified angle. Bonus result from sin(value) in out parameter.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
//    public static double Cvsin(double value, out double sin)
//      => 1 - (sin = System.Math.Sin(value));
//    /// <summary>Returns the coversed cosine of the specified angle.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
//    public static double Cvcos(double value)
//      => 1 + System.Math.Sin(value);
//    /// <summary>Returns the coversed cosine of the specified angle. Bonus result from sin(value) in out parameter.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
//    public static double Cvcos(double value, out double sin)
//      => 1 + (sin = System.Math.Sin(value));
//    #endregion Coversed functions

//    #region Inverse coversed functions
//    /// <summary>Returns the inverse of coversed sine of the specified angle.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
//    public static double Acvsin(double value)
//      => System.Math.Asin(1 - value);
//    /// <summary>Returns the inverse of coversed cosine of the specified angle.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
//    public static double Acvcos(double value)
//      => System.Math.Acos(1 + value);
//    #endregion Inverse coversed functions

//    #region Haversed functions
//    /// <summary>Returns the haversed sine of the specified angle.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
//    public static double Hvsin(double value)
//      => (1 - System.Math.Cos(value)) / 2;
//    /// <summary>Returns the haversed sine of the specified angle. Bonus result from cos(value) in out parameter.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
//    public static double Hvsin(double value, out double cos)
//      => (1 - (cos = System.Math.Cos(value))) / 2;
//    /// <summary>Returns the haversed cosine of the specified angle.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
//    public static double Hvcos(double value)
//      => (1 + System.Math.Cos(value)) / 2;
//    /// <summary>Returns the haversed cosine of the specified angle. Bonus result from cos(value) in out parameter.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
//    public static double Hvcos(double value, out double cos)
//      => (1 + (cos = System.Math.Cos(value))) / 2;
//    #endregion Haversed functions

//    #region Inverse haversed functions
//    /// <summary>Returns the inverse of haversed sine of the specified angle.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
//    public static double Ahvsin(double value)
//      => 2 * System.Math.Asin(System.Math.Sqrt(value));
//    /// <summary>Returns the inverse of haversed sine of the specified angle. Bonus result from sqrt(value) in out parameter.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
//    public static double Ahvsin(double value, out double sqrt)
//      => 2 * System.Math.Asin(sqrt = System.Math.Sqrt(value));
//    /// <summary>Returns the inverse of haversed cosine of the specified angle.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
//    public static double Ahvcos(double value)
//      => 2 * System.Math.Acos(System.Math.Sqrt(value));
//    /// <summary>Returns the inverse of haversed cosine of the specified angle. Bonus result from sqrt(value) in out parameter.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
//    public static double Ahvcos(double value, out double sqrt)
//      => 2 * System.Math.Acos(sqrt = System.Math.Sqrt(value));
//    #endregion Inverse haversed functions

//    #region Hacoversed functions
//    /// <summary>Returns the hacoversed sine of the specified angle.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
//    public static double Hcvsin(double value)
//      => (1 - System.Math.Sin(value)) / 2;
//    /// <summary>Returns the hacoversed sine of the specified angle. Bonus result from sin(value) in out parameter.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
//    public static double Hcvsin(double value, out double sin)
//      => (1 - (sin = System.Math.Sin(value))) / 2;
//    /// <summary>Returns the hacoversed cosine of the specified angle.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
//    public static double Hcvcos(double value)
//      => (1 + System.Math.Sin(value)) / 2;
//    /// <summary>Returns the hacoversed cosine of the specified angle. Bonus result from sin(value) in out parameter.</summary>
//    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
//    public static double Hcvcos(double value, out double sin)
//      => (1 + (sin = System.Math.Sin(value))) / 2;
//    #endregion Hacoversed functions

//    #region Inverse hacoversed functions are missing.
//    #endregion
//  }
//}
