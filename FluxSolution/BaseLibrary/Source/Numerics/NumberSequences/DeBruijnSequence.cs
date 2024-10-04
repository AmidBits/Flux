namespace Flux
{
  public static partial class NumberSequence
  {
    /// <summary>
    /// <para>Returns the total length of the DeBruijn sequence.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/De_Bruijn_sequence"/></para>
    /// <para><seealso href="https://www.rosettacode.org/wiki/De_Bruijn_sequences"/></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sizeK"></param>
    /// <param name="orderN"></param>
    /// <returns></returns>
    /// <remarks>The formula for the length is <c>(<paramref name="sizeK"/> * <paramref name="orderN"/> + <paramref name="orderN"/> - 1)</c>.</remarks>
    public static int GetDeBruijnSequenceLength(int sizeK, int orderN)
    {
      if (sizeK < 1) throw new System.ArgumentOutOfRangeException(nameof(sizeK));
      if (orderN < 0) throw new System.ArgumentOutOfRangeException(nameof(orderN));

      return (int)System.Numerics.BigInteger.Pow(sizeK, orderN) + orderN - 1;
    }

    /// <summary>
    /// <para>Creates a new DeBruijn sequence with DeBruijn numbers, which are the indices in a <paramref name="sizeK"/> alphabet (e.g. 10 digit number pad) of <paramref name="orderN"/> size (e.g. 4 digit codes).</para>
    /// <para>The indices can be translated into symbols using an "alphabet".</para>
    /// <para><see href="https://en.wikipedia.org/wiki/De_Bruijn_sequence"/></para>
    /// <para><seealso href="https://www.rosettacode.org/wiki/De_Bruijn_sequences"/></para>
    /// </summary>
    /// <param name="sizeK"></param>
    /// <param name="orderN"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static System.Collections.Generic.List<int> GetDeBruijnSequence(int sizeK, int orderN)
    {
      if (sizeK < 1) throw new System.ArgumentOutOfRangeException(nameof(sizeK));
      if (orderN < 0) throw new System.ArgumentOutOfRangeException(nameof(orderN));

      var sequence = new System.Collections.Generic.List<int>(GetDeBruijnSequenceLength(sizeK, orderN));

      var a = new int[sizeK * orderN];

      DeBruijn(1, 1);

      sequence.AddRange(sequence.GetRange(0, orderN - 1));

      return sequence;

      void DeBruijn(int t, int p)
      {
        if (t > orderN)
        {
          if ((orderN % p) == 0)
            sequence.AddRange(new System.ArraySegment<int>(a, 1, p));
        }
        else
        {
          a[t] = a[t - p];
          DeBruijn(t + 1, p);
          var j = a[t - p] + 1;

          while (j < sizeK)
          {
            a[t] = j;
            DeBruijn(t + 1, t);
            j++;
          }
        }
      }
    }

    /// <summary>
    /// <para>Creates a new expanded DeBruijn sequence of indices based on <paramref name="sizeK"/> and <paramref name="orderN"/>.</para>
    /// </summary>
    /// <param name="sizeK"></param>
    /// <param name="orderN"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<int>> GetDeBruijnSequenceExpanded(int sizeK, int orderN)
      => GetDeBruijnSequence(sizeK, orderN).PartitionNgram(orderN, (e, i) => e.ToList());

    /// <summary>
    /// <para>Creates a new expanded DeBruijn sequence of symbols based on <paramref name="sizeK"/>, <paramref name="orderN"/> and <paramref name="alphabet"/>.</para>
    /// </summary>
    /// <typeparam name="TSymbol"></typeparam>
    /// <param name="sizeK"></param>
    /// <param name="orderN"></param>
    /// <param name="alphabet"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<TSymbol>> GetDeBruijnSequenceExpanded<TSymbol>(int sizeK, int orderN, System.Collections.Generic.IList<TSymbol> alphabet)
      => GetDeBruijnSequence(sizeK, orderN).PartitionNgram(orderN, (e, i) => e.Select(i => alphabet[i]).ToList());
  }
}
