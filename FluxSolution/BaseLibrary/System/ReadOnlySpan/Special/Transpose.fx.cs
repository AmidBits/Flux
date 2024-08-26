//namespace Flux
//{
//  public static partial class Fx
//  {
//    /// <summary>
//    /// <para></para>
//    /// </summary>
//    public static bool TryTransposePositionalNotationIndicesToSymbols<TIndex, TSymbol>(this System.ReadOnlySpan<TIndex> source, System.ReadOnlySpan<TSymbol> alphabet, out TSymbol[] symbols)
//      where TIndex : System.Numerics.IBinaryInteger<TIndex>
//    {
//      try
//      {
//        symbols = new TSymbol[source.Length];

//        for (var i = 0; i < source.Length; i++)
//          symbols[i] = alphabet[int.CreateChecked(source[i])];

//        return true;
//      }
//      catch { }

//      symbols = System.Array.Empty<TSymbol>();
//      return false;
//    }

//    /// <summary>
//    /// <para></para>
//    /// </summary>
//    public static bool TryTransposeSymbolsToPositionalNotationIndices<TSymbol, TIndex>(this System.ReadOnlySpan<TSymbol> source, System.ReadOnlySpan<TSymbol> alphabet, out TIndex[] indices)
//        where TIndex : System.Numerics.IBinaryInteger<TIndex>
//    {
//      try
//      {
//        indices = new TIndex[source.Length];

//        for (var i = 0; i < source.Length; i++)
//          indices[i] = TIndex.CreateChecked(alphabet.IndexOf(source[i]));

//        return true;
//      }
//      catch { }

//      indices = System.Array.Empty<TIndex>();
//      return false;
//    }
//  }
//}
