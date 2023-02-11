namespace Flux
{
  public static partial class GenericMath
  {
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
  }
}
