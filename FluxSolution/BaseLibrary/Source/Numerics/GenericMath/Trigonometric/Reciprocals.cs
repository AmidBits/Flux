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
      if (TSelf.IsZero(x)) // With x being zero, everything depends on y.
      {
        if (TSelf.IsNegative(y))
          return -TSelf.Pi.Divide(2);
        else if (TSelf.IsZero(y)) // With x AND y both being zero, the result is undefined.
          return TSelf.Zero; // This implementation returns zero.
        else // if (y > TSelf.Zero)
          return TSelf.Pi.Divide(2);
      }

      var atan = TSelf.Atan(y / x);

      if (TSelf.IsNegative(x))
        return TSelf.IsNegative(y) ? atan - TSelf.Pi : atan + TSelf.Pi;
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
  }
}
