namespace Flux
{
  public static partial class Maths
  {
    #region Gudermannian with inverse trigonometric functionality.

#if NET7_0_OR_GREATER

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

#else

    /// <summary>Returns the Gudermannian of the specified value.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Gudermannian_function"/>
    public static double Gd(this double x)
      => System.Math.Atan(System.Math.Sinh(x));

    /// <summary>Returns the inverse Gudermannian of the specified value.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Gudermannian_function#Inverse"/>
    /// <remarks>The integral of the secant function defines the inverse of the Gudermannian function.</remarks>
    /// <remarks>The lambertian function (lam) is a notation for the inverse of the gudermannian which is encountered in the theory of map projections.</remarks>
    public static double Agd(this double y)
      => System.Math.Atanh(System.Math.Sin(y));

#endif

    #endregion // Gudermannian with inverse trigonometric functionality.
  }
}
