#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Returns the sin of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
    public static TSelf Sin<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Sin(x);

    /// <summary>Returns the cos of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
    public static TSelf Cos<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Cos(x);

    /// <summary>Returns the tan of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
    public static TSelf Tan<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Tan(x);

    /// <summary>Returns the cosecant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
    public static TSelf Csc<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.One / TSelf.Sin(x);

    /// <summary>Returns the secant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
    public static TSelf Sec<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.One / TSelf.Cos(x);

    /// <summary>Returns the cotangent of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
    public static TSelf Cot<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.One / TSelf.Tan(x);

    // Inverse:

    /// <summary>Returns the sin of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
    public static TSelf Asin<TSelf>(this TSelf y)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Asin(y);

    /// <summary>Returns the cos of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
    public static TSelf Acos<TSelf>(this TSelf y)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Acos(y);

    /// <summary>Returns the tan of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
    public static TSelf Atan<TSelf>(this TSelf y)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Atan(y);

    /// <summary>Returns the tan of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Trigonometric_functions"/>
    public static TSelf Atan2<TSelf>(this TSelf y, TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Atan2(y, x);

    /// <summary>Returns the inverse cosecant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
    public static TSelf Acsc<TSelf>(this TSelf y)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Asin(TSelf.One / y);

    /// <summary>Returns the inverse secant of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
    public static TSelf Asec<TSelf>(this TSelf y)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Acos(TSelf.One / y);

    /// <summary>Returns the inverse cotangent of the specified angle.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Inverse_trigonometric_functions"/>
    public static TSelf Acot<TSelf>(this TSelf y)
      where TSelf : System.Numerics.INumber<TSelf>, System.Numerics.ITrigonometricFunctions<TSelf>
      => TSelf.Atan(TSelf.One / y);
  }
}
#endif
