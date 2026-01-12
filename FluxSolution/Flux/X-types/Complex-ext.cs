namespace Flux
{
  public static class ComplexExtensions
  {
    extension(System.Numerics.Complex)
    {
      #region Gudermannian

      /// <summary>Returns the inverse Gudermannian of the specified x.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Gudermannian_function#Inverse"/>
      /// <remarks>The integral of the secant function defines the inverse of the Gudermannian function.</remarks>
      /// <remarks>The lambertian function (lam) is a notation for the inverse of the gudermannian which is encountered in the theory of map projections.</remarks>
      public static System.Numerics.Complex Agd(System.Numerics.Complex y)
        => 2 * System.Numerics.Complex.Atanh(System.Numerics.Complex.Tan(0.5 * y));

      /// <summary>Returns the complex Gudermannian of the specified x.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Gudermannian_function"/>
      public static System.Numerics.Complex Gd(System.Numerics.Complex x)
        => 2 * System.Numerics.Complex.Atan(System.Numerics.Complex.Tanh(0.5 * x));

      #endregion

      public static System.Numerics.Complex Asinh(System.Numerics.Complex z)
      => System.Numerics.Complex.Log(z + System.Numerics.Complex.Sqrt(z * z + 1));

      public static System.Numerics.Complex Acosh(System.Numerics.Complex z)
        => System.Numerics.Complex.Log(z + System.Numerics.Complex.Sqrt(z + 1) * System.Numerics.Complex.Sqrt(z - 1));

      public static System.Numerics.Complex Acoth(System.Numerics.Complex z)
        => z == 0
        ? 0.5 * System.Numerics.Complex.Log((z + 1) / (z - 1))
        : 0.5 * System.Numerics.Complex.Log(1 + 1 / z) - 0.5 * System.Numerics.Complex.Log(1 - 1 / z);

      public static System.Numerics.Complex Acsch(System.Numerics.Complex z)
        => System.Numerics.Complex.Log(1 / z + System.Numerics.Complex.Sqrt((1 / z * z) + 1));

      public static System.Numerics.Complex Asech(System.Numerics.Complex z)
        => System.Numerics.Complex.Log((1 / z) + System.Numerics.Complex.Sqrt(1 / z + 1) * System.Numerics.Complex.Sqrt(1 / z - 1));

      public static System.Numerics.Complex Atanh(System.Numerics.Complex z)
        => 0.5 * System.Numerics.Complex.Log(1 + z) - 0.5 * System.Numerics.Complex.Log(1 - z);
    }
  }
}
