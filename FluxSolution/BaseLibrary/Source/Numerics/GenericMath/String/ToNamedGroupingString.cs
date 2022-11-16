namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>Converts <paramref name="x"/> to text using cardinal numerals, e.g. 145,000 would become "one hundred fourty five thousand".</summary>
    public static System.ReadOnlySpan<char> ToCompoundStringCardinalNumerals<TSelf>(this TSelf value)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
      => NumeralComposition.ToCardinalNumeralCompoundString(System.Numerics.BigInteger.CreateChecked(value));
  }
}
