namespace Flux.Hashing
{
  // https://en.wikipedia.org/wiki/Zobrist_hashing
  public class ZobristTable
  {
    private readonly ulong[,,] m_table;

    public ZobristTable(int lengthDimension0, int lengthDimension1, int statePermutations)
    {
      var rng = new Randomization.Cryptographic();

      m_table = new ulong[lengthDimension0, lengthDimension1, statePermutations];

      for (int h = lengthDimension0 - 1; h >= 0; h--)
        for (int v = lengthDimension1 - 1; v >= 0; v--)
          for (int i = 0; i < statePermutations; i++)
            m_table[h, v, i] = (ulong)rng.NextInt64();
    }

    public long ComputeHash<TValue>(TValue[,] board, System.Func<TValue, bool> predicate, System.Func<TValue, int> selector)
    {
      if (board is null) throw new System.ArgumentNullException(nameof(board));
      if (predicate is null) throw new System.ArgumentNullException(nameof(predicate));
      if (selector is null) throw new System.ArgumentNullException(nameof(selector));

      var td0l = m_table.GetLength(0);
      var td1l = m_table.GetLength(1);
      var td2l = m_table.GetLength(2);

      var bd0l = board.GetLength(0);
      var bd1l = board.GetLength(1);

      if (td0l != bd0l || td1l != bd1l) throw new System.ArgumentOutOfRangeException(nameof(board), $"The board does not have the same boundaries as the Zobrist table.");

      var hash = 0UL;

      for (var d0i = 0; d0i < td0l; d0i++)
      {
        for (var d1i = 0; d1i < td1l; d1i++)
        {
          if (board[d0i, d1i] is var value && predicate(value))
          {
            var permutation = selector(value);

            if (permutation < 0 || permutation >= td2l) throw new System.InvalidOperationException($"The board value does not evaluate to a valid permutation.");

            hash ^= m_table[d0i, d1i, permutation];
          }
        }
      }

      return (long)hash;
    }

    public override string ToString()
      => $"<{nameof(ZobristTable)}: {m_table.GetLength(0)}, {m_table.GetLength(1)}, {m_table.GetLength(2)}>";
  }
}
