namespace Flux
{
  public static partial class Math
  {
    /// <summary>Returns the Gudermannian of the specified value.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Gudermannian_function"/>
    public static double Gd(double value)
      => System.Math.Atan(System.Math.Sinh(value));
    /// <summary>Returns the Gudermannian of the specified value. Bonus result from sinh(value) in out parameter.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Gudermannian_function"/>
    public static double Gd(double value, out double sinh)
      => System.Math.Atan(sinh = System.Math.Sinh(value));

    /// <summary>Returns the inverse Gudermannian of the specified value.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Gudermannian_function#Inverse"/>
    /// <remarks>The integral of the secant function defines the inverse of the Gudermannian function.</remarks>
    /// <remarks>The lambertian function (lam) is a notation for the inverse of the gudermannian which is encountered in the theory of map projections.</remarks>
    public static double Agd(double value)
      => Atanh(System.Math.Sin(value));
    /// <summary>Returns the inverse Gudermannian of the specified value. Bonus result from sin(value) in out parameter.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Gudermannian_function#Inverse"/>
    /// <remarks>The integral of the secant function defines the inverse of the Gudermannian function.</remarks>
    /// <remarks>The lambertian function (lam) is a notation for the inverse of the gudermannian which is encountered in the theory of map projections.</remarks>
    public static double Agd(double value, out double sin)
      => Atanh(sin = System.Math.Sin(value));
  }
}
