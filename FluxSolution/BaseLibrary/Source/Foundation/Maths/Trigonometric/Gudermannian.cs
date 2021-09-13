namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Returns the Gudermannian of the specified value.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Gudermannian_function"/>
    public static double Gd(double value)
      => System.Math.Atan(System.Math.Sinh(value));

    // Inverse function:

    /// <summary>Returns the inverse Gudermannian of the specified value.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Gudermannian_function#Inverse"/>
    /// <remarks>The integral of the secant function defines the inverse of the Gudermannian function.</remarks>
    /// <remarks>The lambertian function (lam) is a notation for the inverse of the gudermannian which is encountered in the theory of map projections.</remarks>
    public static double Agd(double value)
      => System.Math.Atanh(System.Math.Sin(value));
  }
}
