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
    public static bool TryConvertNumberToPositionalNotationIndices<TInteger, TRadix>(this TInteger value, TRadix radix, out System.Collections.Generic.List<int> positionalNotationIndices)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
    {
      positionalNotationIndices = new();

      try
      {
        var rdx = TInteger.CreateChecked(Units.Radix.AssertMember(radix));

        while (!TInteger.IsZero(value))
        {
          (value, var remainder) = TInteger.DivRem(value, rdx);

          positionalNotationIndices.Insert(0, int.CreateChecked(remainder));
        }

        return true;
      }
      catch { }

      return false;
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
    public static bool TryConvertPositionalNotationIndicesToNumber<TRadix, TInteger>(this System.Collections.Generic.List<int> positionalNotationIndices, TRadix radix, out TInteger value)
      where TRadix : System.Numerics.IBinaryInteger<TRadix>
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      value = TInteger.Zero;

      try
      {
        var rdx = TInteger.CreateChecked(Units.Radix.AssertMember(radix));

        for (var index = 0; index < positionalNotationIndices.Count; index++)
        {
          value *= rdx;

          value += TInteger.CreateChecked(positionalNotationIndices[index]);
        }

        return true;
      }
      catch { }

      return false;
    }

    /// <summary>
    /// <para>Converts <paramref name="positionalNotationIndices"/> to <paramref name="symbols"/> using the specified <paramref name="alphabet"/>.</para>
    /// </summary>
    /// <typeparam name="TSymbol"></typeparam>
    /// <param name="positionalNotationIndices"></param>
    /// <param name="alphabet"></param>
    /// <param name="symbols"></param>
    /// <returns></returns>
    public static bool TryTransposePositionalNotationIndicesToSymbols<TSymbol>(this System.Collections.Generic.List<int> positionalNotationIndices, System.ReadOnlySpan<TSymbol> alphabet, out System.Collections.Generic.List<TSymbol> symbols)
    {
      try
      {
        symbols = new System.Collections.Generic.List<TSymbol>(positionalNotationIndices.Count);

        for (var i = 0; i < positionalNotationIndices.Count; i++)
          symbols.Add(alphabet[positionalNotationIndices[i]]);

        return true;
      }
      catch { }

      symbols = new();
      return false;
    }

    /// <summary>
    /// <para>Converts <paramref name="symbols"/> to <paramref name="positionalNotationIndices"/> using the specified <paramref name="alphabet"/>.</para>
    /// </summary>
    /// <typeparam name="TSymbol"></typeparam>
    /// <param name="symbols"></param>
    /// <param name="alphabet"></param>
    /// <param name="positionalNotationIndices"></param>
    /// <returns></returns>
    public static bool TryTransposeSymbolsToPositionalNotationIndices<TSymbol>(this System.Collections.Generic.List<TSymbol> symbols, System.ReadOnlySpan<TSymbol> alphabet, out System.Collections.Generic.List<int> positionalNotationIndices)
    {
      try
      {
        positionalNotationIndices = new(symbols.Count);

        for (var i = 0; i < symbols.Count; i++)
          positionalNotationIndices[i] = alphabet.IndexOf(symbols[i]);

        return true;
      }
      catch { }

      positionalNotationIndices = new();
      return false;
    }
  }
}
