namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Spreads an in-interval <paramref name="number"/> around the outside edges of the closed interval [<paramref name="lowValue"/>, <paramref name="highValue"/>].</para>
    /// <para>This is the opposite of a Clamp( [<paramref name="minValue"/>, <paramref name="maxValue"/>] ) which is inclusive, making the Spread( either [NegativeInfinity, <paramref name="lowValue"/>) or (<paramref name="highValue"/>, PositiveInfinity] ), i.e. exclusive of <paramref name="lowValue"/> and <paramref name="highValue"/>.</para>
    /// </summary>
    public static TNumber Spread<TNumber>(this TNumber number, TNumber lowValue, TNumber highValue, HalfRounding mode)
      where TNumber : System.Numerics.INumber<TNumber>
      => number < lowValue || number > highValue // If number is already spread, nothing to do.
      ? number
      : number.RoundToNearest((UniversalRounding)(int)mode, lowValue, highValue) is var nearestValue && nearestValue == lowValue
      ? lowValue.GetInfimum()
      : nearestValue == highValue
      ? lowValue.GetSupremum()
      : nearestValue;

  }
}
