namespace Flux
{
  public static partial class Fx
  {
    /// <summary>The unit sign step function.</summary>
    /// <remarks>LT 0 = -1, GTE 0 = +1.</remarks>
    /// <see href="https://en.wikipedia.org/wiki/Step_function"/>
    /// <seealso href="https://en.wikipedia.org/wiki/Sign_function"/>
    public static TNumber UnitSign<TNumber>(this TNumber number)
      where TNumber : System.Numerics.INumber<TNumber>
      => TNumber.IsNegative(number) ? -TNumber.One : TNumber.One;
  }
}
