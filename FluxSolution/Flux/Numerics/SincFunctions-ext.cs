namespace Flux
{
  public static class SincFunctions
  {
    extension<TFloat>(TFloat x)
      where TFloat : System.Numerics.ITrigonometricFunctions<TFloat>
    {

      /// <summary>
      /// <para>Returns the normalized (using PI) sinc function of the specified x.</para>
      /// <para>The normalization causes the definite integral of the function over the real numbers to equal 1 (whereas the same integral of the unnormalized sinc function has a value of π).</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Sinc_function"/></para>
      /// </summary>
      public TFloat Sincn()
        => Sincu(TFloat.Pi * x);

      /// <summary>
      /// <para>Returns the unnormalized sinc of the specified x.</para>
      /// <para>The definite integral of the unnormalized sinc function has a value of π.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Sinc_function"/></para>
      /// </summary>
      public TFloat Sincu()
        => TFloat.IsZero(x)
        ? TFloat.One
        : TFloat.Sin(x) / x;
    }
  }
}
