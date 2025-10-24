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
  }
}
