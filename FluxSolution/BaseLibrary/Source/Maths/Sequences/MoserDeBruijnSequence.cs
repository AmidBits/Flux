namespace Flux
{
  public class MoserDeBruijnSequence
  : ISequenceInfinite<System.Numerics.BigInteger>
  {
    public System.Collections.Generic.IEnumerator<System.Numerics.BigInteger> GetEnumerator()
      => GetSequence().GetEnumerator();
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
      => GetEnumerator();

    public int MaxNumber { get; set; }

    /// <summary>Creates a new sequence with Moser/DeBruijn numbers.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Moser%E2%80%93De_Bruijn_sequence"/>
    public System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetSequence()
      => GenerateSequence(MaxNumber);

    // https://en.wikipedia.org/wiki/Moser%E2%80%93De_Bruijn_sequence
    // https://www.geeksforgeeks.org/moser-de-bruijn-sequence/
    public static System.Collections.Generic.IList<System.Numerics.BigInteger> GenerateSequence(System.Numerics.BigInteger maxNumber)
    {
      if (maxNumber < 0) throw new System.ArgumentOutOfRangeException(nameof(maxNumber));

      var sequence = new System.Collections.Generic.List<System.Numerics.BigInteger>();

      sequence.Add(0);

      if (maxNumber > 0)
        sequence.Add(1);

      for (var i = 2; i <= maxNumber; i++)
      {
        if (i % 2 == 0) // S(2 * n) = 4 * S(n)
          sequence.Add(4 * sequence[i / 2]);
        else // S(2 * n + 1) = 4 * S(n) + 1
          sequence.Add(4 * sequence[i / 2] + 1);
      }

      return sequence;
    }
  }

  //public static partial class Maths
  //{
  //  // https://en.wikipedia.org/wiki/Moser%E2%80%93De_Bruijn_sequence
  //  // https://www.geeksforgeeks.org/moser-de-bruijn-sequence/
  //  public static System.Collections.Generic.IList<int> GenerateMoserDeBruijnSequence(int number)
  //  {
  //    if (number < 0) throw new System.ArgumentOutOfRangeException(nameof(number));

  //    var sequence = new System.Collections.Generic.List<int>();

  //    sequence.Add(0);

  //    if (number > 0)
  //      sequence.Add(1);

  //    for (var i = 2; i <= number; i++)
  //    {
  //      if (i % 2 == 0) // S(2 * n) = 4 * S(n)
  //        sequence.Add(4 * sequence[i / 2]);
  //      else // S(2 * n + 1) = 4 * S(n) + 1
  //        sequence.Add(4 * sequence[i / 2] + 1);
  //    }

  //    return sequence;
  //  }
  //}
}
