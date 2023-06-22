namespace Flux
{
  public static partial class Maths
  {
    #region Versine/coversine/haversine with inverse trigonometric functionality.

#if NET7_0_OR_GREATER

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

#else

    /// <summary>Returns the versed sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine"/>
    public static double Versin(this double x)
      => 1 - System.Math.Cos(x);

    /// <summary>Returns the coversed sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    public static double Coversin(this double x)
      => 1 - System.Math.Sin(x);

    /// <summary>Returns the versed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine"/>
    public static double Vercosin(this double x)
      => 1 + System.Math.Cos(x);

    /// <summary>Returns the coversed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Definitions"/>
    public static double Covercosin(this double x)
      => 1 + System.Math.Sin(x);

    /// <summary>Returns the haversed sine of the specified angle. This is the famous Haversin function.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine"/>
    public static double Haversin(this double x)
      => (1 - System.Math.Cos(x)) / 2;

    /// <summary>Returns the hacoversed sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine"/>
    public static double Hacoversin(this double x)
      => (1 - System.Math.Sin(x)) / 2;

    /// <summary>Returns the haversed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine"/>
    public static double Havercosin(this double x)
      => (1 + System.Math.Cos(x)) / 2;

    /// <summary>Returns the hacoversed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine"/>
    public static double Hacovercosin(this double x)
      => (1 + System.Math.Sin(x)) / 2;

    /// <summary>Returns the inverse of versed sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static double Arcversin(this double y)
      => System.Math.Acos(1 - y);

    /// <summary>Returns the inverse of versed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static double Arcvercos(this double y)
      => System.Math.Acos(y - 1);

    /// <summary>Returns the inverse of coversed sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static double Arccoversin(this double y)
      => System.Math.Asin(1 - y);

    /// <summary>Returns the inverse of coversed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static double Arccovercos(this double y)
      => System.Math.Asin(y - 1);

    /// <summary>Returns the inverse of haversed sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static double Archaversin<TSelf>(this double y)
      => System.Math.Acos(1 - y * 2); // An extra subtraction saves a call to the Sqrt function: 2 * System.Math.Asin(System.Math.Sqrt(y));

    /// <summary>Returns the inverse of haversed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static double Archavercos(this double y)
      => System.Math.Acos(y * 2 - 1); // An extra subtraction saves a call to the Sqrt function: 2 * System.Math.Acos(System.Math.Sqrt(y));

    /// <summary>Returns the inverse of cohaversed sine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static double Archacoversin(this double y)
      => System.Math.Asin(1 - y * 2);

    /// <summary>Returns the inverse of cohaversed cosine of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Versine#Inverse_functions"/>
    public static double Archacovercos(this double y)
      => System.Math.Asin(y * 2 - 1);

#endif

    #endregion // Versine/coversine/haversine with inverse trigonometric functionality.
  }
}
