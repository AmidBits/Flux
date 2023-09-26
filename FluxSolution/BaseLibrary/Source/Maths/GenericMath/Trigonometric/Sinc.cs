namespace Flux
{
  public static partial class Maths
  {
    #region Sinc normalized and unnormalized.

#if NET7_0_OR_GREATER

    /// <summary>Returns the normalized form sinc of the specified value.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Sinc_function"/>
    public static TSelf Sincn<TSelf>(this TSelf x)
      where TSelf : System.Numerics.ITrigonometricFunctions<TSelf>
      => Sincu(TSelf.Pi * x);

    /// <summary>Returns the unnormalized form sinc of the specified value.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Sinc_function"/>
    public static TSelf Sincu<TSelf>(this TSelf x)
      where TSelf : System.Numerics.ITrigonometricFunctions<TSelf>
      => x != TSelf.Zero ? TSelf.Sin(x) / x : TSelf.One;

#else

    /// <summary>Returns the normalized form sinc of the specified value.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Sinc_function"/>
    public static double Sincn(this double x)
      => Sincu(System.Math.PI * x);

    /// <summary>Returns the unnormalized form sinc of the specified value.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Sinc_function"/>
    public static double Sincu(this double x)
      => x != 0 ? System.Math.Sin(x) / x : 1;

#endif

    #endregion Sinc normalized and unnormalized.
  }
}
