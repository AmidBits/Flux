namespace Flux
{
  public static partial class Em
  {
    /// <summary>
    /// <para>Gets the count of all single digits in <paramref name="number"/> using base <paramref name="radix"/>.</para>
    /// </summary>
    public static (TInteger DigitCount, TInteger DigitSum, bool IsJumbled, bool IsPowOf, TInteger NumberReversed, System.Collections.Generic.List<TInteger> PlaceValues, System.Collections.Generic.List<TInteger> ReverseDigits) Digits<TInteger>(this Units.Radix radix, TInteger number)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => Units.Radix.Digits(number, radix.Value);
  }
}
