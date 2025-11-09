namespace Flux
{
  public static class GudermannianFunctions
  {
    extension<TFloat>(TFloat x)
      where TFloat : System.Numerics.IHyperbolicFunctions<TFloat>, System.Numerics.ITrigonometricFunctions<TFloat>
    {
      /// <summary>Returns the Gudermannian of the specified x.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Gudermannian_function"/>
      public TFloat Gd()
        => TFloat.Atan(TFloat.Sinh(x));
    }

    extension<TFloat>(TFloat y)
      where TFloat : System.Numerics.IHyperbolicFunctions<TFloat>, System.Numerics.ITrigonometricFunctions<TFloat>
    {
      /// <summary>Returns the inverse Gudermannian of the specified x.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Gudermannian_function#Inverse"/>
      /// <remarks>The integral of the secant function defines the inverse of the Gudermannian function.</remarks>
      /// <remarks>The lambertian function (lam) is a notation for the inverse of the gudermannian which is encountered in the theory of map projections.</remarks>
      public TFloat Agd()
        => TFloat.Atanh(TFloat.Sin(y));
    }

    ///// <summary>Returns the complex Gudermannian of the specified x.</summary>
    ///// <see href="https://en.wikipedia.org/wiki/Gudermannian_function"/>
    //public static System.Numerics.Complex Gd(System.Numerics.Complex x)
    //  => 2 * System.Numerics.Complex.Atan(System.Numerics.Complex.Tanh(0.5 * x));

    ///// <summary>Returns the inverse Gudermannian of the specified x.</summary>
    ///// <see href="https://en.wikipedia.org/wiki/Gudermannian_function#Inverse"/>
    ///// <remarks>The integral of the secant function defines the inverse of the Gudermannian function.</remarks>
    ///// <remarks>The lambertian function (lam) is a notation for the inverse of the gudermannian which is encountered in the theory of map projections.</remarks>
    //public static System.Numerics.Complex Agd(System.Numerics.Complex y)
    //  => 2 * System.Numerics.Complex.Atanh(System.Numerics.Complex.Tan(0.5 * y));
  }
}
