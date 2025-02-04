namespace Flux.Numerics
{
  public static partial class NumberSequence
  {
    /// <summary>Creates a sequence of Moser/DeBruijn numbers.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Moser%E2%80%93De_Bruijn_sequence"/>
    /// <seealso cref="https://www.geeksforgeeks.org/moser-de-bruijn-sequence/"/>
    public static System.Collections.Generic.List<TSelf> GetMoserDeBruijnSequence<TSelf>(TSelf maxNumber)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      if (maxNumber < TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(maxNumber));

      var sequence = new System.Collections.Generic.List<TSelf>() { TSelf.Zero };

      if (maxNumber > TSelf.Zero)
        sequence.Add(TSelf.One);

      var two = TSelf.CreateChecked(2);
      var four = TSelf.CreateChecked(4);

      for (var i = two; i <= maxNumber; i++)
      {
        if (TSelf.IsZero(i % two)) // S(2 * n) = 4 * S(n)
          sequence.Add(four * sequence[int.CreateChecked(i / two)]);
        else // S(2 * n + 1) = 4 * S(n) + 1
          sequence.Add(four * sequence[int.CreateChecked(i / two)] + TSelf.One);
      }

      return sequence;
    }
  }
}

//namespace Flux.NumberSequences
//{
//  /// <summary>Creates a new sequence with Moser/DeBruijn numbers.</summary>
//  /// <see href="https://en.wikipedia.org/wiki/Moser%E2%80%93De_Bruijn_sequence"/>
//  public record class MoserDeBruijnSequence
//    : INumericSequence<System.Numerics.BigInteger>
//  {
//    public System.Numerics.BigInteger MaxNumber { get; set; }

//    public MoserDeBruijnSequence(System.Numerics.BigInteger maxNumber)
//      => MaxNumber = maxNumber;

//    #region Static methods

//    /// <summary>Creates a sequence of Moser/DeBruijn numbers.</summary>
//    /// <see href="https://en.wikipedia.org/wiki/Moser%E2%80%93De_Bruijn_sequence"/>
//    /// <seealso cref="https://www.geeksforgeeks.org/moser-de-bruijn-sequence/"/>
//    public static System.Collections.Generic.List<TSelf> GetMoserDeBruijnSequence<TSelf>(TSelf maxNumber)
//      where TSelf : System.Numerics.IBinaryInteger<TSelf>
//    {
//      if (maxNumber < TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(maxNumber));

//      var sequence = new System.Collections.Generic.List<TSelf>() { TSelf.Zero };

//      if (maxNumber > TSelf.Zero)
//        sequence.Add(TSelf.One);

//      var two = TSelf.CreateChecked(2);
//      var four = TSelf.CreateChecked(4);

//      for (var i = two; i <= maxNumber; i++)
//      {
//        if (TSelf.IsZero(i % two)) // S(2 * n) = 4 * S(n)
//          sequence.Add(four * sequence[int.CreateChecked(i / two)]);
//        else // S(2 * n + 1) = 4 * S(n) + 1
//          sequence.Add(four * sequence[int.CreateChecked(i / two)] + TSelf.One);
//      }

//      return sequence;
//    }

//    #endregion Static methods

//    #region Implemented interfaces
//    // INumberSequence
//    public System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetSequence()
//      => GetMoserDeBruijnSequence(MaxNumber);


//    public System.Collections.Generic.IEnumerator<System.Numerics.BigInteger> GetEnumerator()
//      => GetSequence().GetEnumerator();

//    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
//      => GetEnumerator();
//    #endregion Implemented interfaces
//  }
//}
