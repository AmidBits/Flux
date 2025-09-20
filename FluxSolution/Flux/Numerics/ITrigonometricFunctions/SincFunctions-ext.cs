namespace Flux
{
  public static partial class SincFunctions
  {
    extension<TFloat>(TFloat x)
      where TFloat : System.Numerics.ITrigonometricFunctions<TFloat>
    {
      /// <summary>Returns the normalized sinc of the specified x.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Sinc_function"/>
      public TFloat Sincn() => Sincu(TFloat.Pi * x);

      /// <summary>Returns the unnormalized sinc of the specified x.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Sinc_function"/>
      public TFloat Sincu() => !TFloat.IsZero(x) ? TFloat.Sin(x) / x : TFloat.One;
    }
  }
}
