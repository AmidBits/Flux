namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Spreads an in-bound <paramref name="number"/> to the edges of the closed interval [<paramref name="lowValue"/>, <paramref name="highValue"/>].</para>
    /// </summary>
    public static TNumber Spread<TNumber>(this TNumber number, TNumber lowValue, TNumber highValue, HalfRounding mode)
      where TNumber : System.Numerics.INumber<TNumber>
      => number <= lowValue || number >= highValue // If number is already spread, nothing to do.
      ? number
      : number.RoundToNearest((UniversalRounding)(int)mode, lowValue, highValue);
  }
}
