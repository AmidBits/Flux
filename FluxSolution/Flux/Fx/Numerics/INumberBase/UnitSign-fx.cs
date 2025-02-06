namespace Flux
{
  public static partial class Fx
  {
    /// <summary>The unit sign step function, i.e. zero is treated as a positive unit value of one.</summary>
    /// <remarks>LT 0 = -1, GTE 0 = +1.</remarks>
    /// <see href="https://en.wikipedia.org/wiki/Step_function"/>
    /// <seealso href="https://en.wikipedia.org/wiki/Sign_function"/>
    public static TNumber UnitSign<TNumber>(this TNumber number)
      where TNumber : System.Numerics.INumberBase<TNumber>
      => TNumber.IsNegative(number) ? -TNumber.One : TNumber.One;
  }
}
