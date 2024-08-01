namespace Flux
{
  public static partial class Fx
  {
    ///// <summary>The half sign step function. Only for numeric types derived from <see cref="System.Numerics.IUnsignedNumber{TSelf}"/>.</summary>
    ///// <remarks>EQ 0 = 0 and GT 0 = +1.</remarks>
    ///// <see href="https://en.wikipedia.org/wiki/Step_function"/>
    ///// <seealso href="https://en.wikipedia.org/wiki/Sign_function"/>
    //public static TSelf HalfSign<TSelf>(this TSelf x)
    //  where TSelf : System.Numerics.IUnsignedNumber<TSelf>
    //  => TSelf.IsZero(x) ? TSelf.Zero : TSelf.One;

    /// <summary>The sign step function.</summary>
    /// <remarks>LT 0 = -1, EQ 0 = 0 and GT 0 = +1.</remarks>
    /// <see href="https://en.wikipedia.org/wiki/Step_function"/>
    /// <seealso href="https://en.wikipedia.org/wiki/Sign_function"/>
    public static TSelf Sign<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>
      => TSelf.Sign(x);

    /// <summary>The unit sign step function.</summary>
    /// <remarks>LT 0 = -1, GTE 0 = +1.</remarks>
    /// <see href="https://en.wikipedia.org/wiki/Step_function"/>
    /// <seealso href="https://en.wikipedia.org/wiki/Sign_function"/>
    public static TSelf UnitSign<TSelf>(this TSelf x)
      where TSelf : System.Numerics.INumber<TSelf>
      => TSelf.IsNegative(x) ? -TSelf.One : TSelf.One;
  }
}
