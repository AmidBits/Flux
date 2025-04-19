namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>
    /// <para>Spreads an in-interval <paramref name="value"/> around the outside edges of the closed interval [<paramref name="lowValue"/>, <paramref name="highValue"/>].</para>
    /// <para>This is the opposite of a <see cref="System.Numerics.INumber{TSelf}.Clamp(TSelf, TSelf, TSelf)"/> ( [<paramref name="minValue"/>, <paramref name="maxValue"/>] ) which is inclusive, making the Spread( either [NegativeInfinity, <paramref name="lowValue"/>) or (<paramref name="highValue"/>, PositiveInfinity] ), i.e. exclusive of <paramref name="lowValue"/> and <paramref name="highValue"/>.</para>
    /// </summary>
    public static TNumber Spread<TNumber>(this TNumber value, TNumber lowValue, TNumber highValue, HalfRounding mode)
      where TNumber : System.Numerics.INumber<TNumber>
      => (value < lowValue || value > highValue)
      ? value // If number is already spread, nothing to do but return it.
      : value.RoundToNearest((UniversalRounding)(int)mode, lowValue, highValue) is var nearestValue && (nearestValue == lowValue)
      ? lowValue.GetInfimum()
      : (nearestValue == highValue)
      ? highValue.GetSupremum()
      : nearestValue;
  }
}
