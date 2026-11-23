namespace Flux
{
  public static class PositionalNotation
  {
    /// <summary>
    /// <para>Converts a <paramref name="value"/> to <paramref name="positionalNotationIndices"/> based on <paramref name="radix"/>.</para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <typeparam name="TRadix"></typeparam>
    /// <param name="value"></param>
    /// <param name="radix"></param>
    /// <param name="positionalNotationIndices"></param>
    /// <returns></returns>
    public static void ConvertNumberToPositionalNotationIndices<TInteger, TRadix>(this TInteger value, TRadix radix, out System.Collections.Generic.List<int> positionalNotationIndices)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      BinaryInteger.AssertRadix(radix, out TInteger rdx);

      positionalNotationIndices = [with(BinaryInteger.MaxRepresentableDigitCount(TInteger.Abs(value).GetShortestBitLength(), radix, false))];

      while (!TInteger.IsZero(value))
      {
        (value, var remainder) = TInteger.DivRem(value, rdx);

        positionalNotationIndices.Insert(0, int.CreateChecked(remainder));
      }
    }

    /// <summary>
    /// <para>Converts <paramref name="positionalNotationIndices"/> to a <paramref name="value"/> based on <paramref name="radix"/>.</para>
    /// </summary>
    /// <typeparam name="TRadix"></typeparam>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="positionalNotationIndices"></param>
    /// <param name="radix"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public static void ConvertPositionalNotationIndicesToNumber<TRadix, TInteger>(this System.Collections.Generic.List<int> positionalNotationIndices, TRadix radix, out TInteger value)
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      BinaryInteger.AssertRadix(radix, out TInteger rdx);

      value = TInteger.Zero;

      for (var index = 0; index < positionalNotationIndices.Count; index++)
      {
        value *= rdx;

        value += TInteger.CreateChecked(positionalNotationIndices[index]);
      }
    }

    /// <summary>
    /// <para>Converts <paramref name="positionalNotationIndices"/> to <paramref name="symbols"/> using the specified <paramref name="alphabet"/>.</para>
    /// </summary>
    /// <typeparam name="TSymbol"></typeparam>
    /// <param name="positionalNotationIndices"></param>
    /// <param name="alphabet"></param>
    /// <param name="symbols"></param>
    /// <returns></returns>
    public static void TransposePositionalNotationIndicesToSymbols<TSymbol>(this System.Collections.Generic.List<int> positionalNotationIndices, System.ReadOnlySpan<TSymbol> alphabet, out System.Collections.Generic.List<TSymbol> symbols)
    {
      symbols = [];

      for (var i = 0; i < positionalNotationIndices.Count; i++)
        symbols.Add(alphabet[positionalNotationIndices[i]]);
    }

    /// <summary>
    /// <para>Converts <paramref name="symbols"/> to <paramref name="positionalNotationIndices"/> using the specified <paramref name="alphabet"/>.</para>
    /// </summary>
    /// <typeparam name="TSymbol"></typeparam>
    /// <param name="symbols"></param>
    /// <param name="alphabet"></param>
    /// <param name="positionalNotationIndices"></param>
    /// <returns></returns>
    public static void TransposeSymbolsToPositionalNotationIndices<TSymbol>(this System.Collections.Generic.List<TSymbol> symbols, System.ReadOnlySpan<TSymbol> alphabet, out System.Collections.Generic.List<int> positionalNotationIndices)
    {
      positionalNotationIndices = [with(symbols.Count)];

      for (var i = 0; i < symbols.Count; i++)
        positionalNotationIndices[i] = alphabet.IndexOf(symbols[i]);
    }
  }
}
