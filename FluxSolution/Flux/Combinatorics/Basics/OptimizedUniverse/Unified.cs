//namespace Flux.CopilotCombinationPermutation_Session
//{
//  using System.Runtime.CompilerServices;

//  // SECTION 1 — Shared Precomputed Tables

//  public static class CombTablesNR
//  {
//    public static readonly long[] Fact;
//    public static readonly long[,] Binom;

//    public static int MaxN { get; }

//    static CombTablesNR()
//    {
//      MaxN = 64;

//      Fact = new long[MaxN + 1];
//      Fact[0] = 1;
//      for (int i = 1; i <= MaxN; i++)
//        Fact[i] = Fact[i - 1] * i;

//      Binom = new long[MaxN + 1, MaxN + 1];
//      for (int n = 0; n <= MaxN; n++)
//      {
//        Binom[n, 0] = 1;
//        Binom[n, n] = 1;
//        for (int k = 1; k < n; k++)
//          Binom[n, k] = Binom[n - 1, k - 1] + Binom[n - 1, k];
//      }
//    }

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static long GetFact(int n) => Fact[n];

//    [MethodImpl(MethodImplOptions.AggressiveInlining)]
//    public static long GetBinom(int n, int k) => Binom[n, k];
//  }

//  // SECTION 2 — Combinations WITHOUT Repetition (Span‑Optimized)

//  public static class CombinadicsSpanNR
//  {
//    public static long Rank(ReadOnlySpan<int> combo)
//    {
//      long rank = 0;
//      for (int i = 0; i < combo.Length; i++)
//        rank += CombTablesNR.GetBinom(combo[i], i + 1);
//      return rank;
//    }

//    public static void Unrank(long rank, int k, Span<int> result, int maxValue)
//    {
//      for (int i = k; i >= 1; i--)
//      {
//        int low = i - 1;
//        int high = maxValue;

//        while (low <= high)
//        {
//          int mid = (low + high) >> 1;
//          long val = CombTablesNR.GetBinom(mid, i);
//          if (val <= rank)
//            low = mid + 1;
//          else
//            high = mid - 1;
//        }

//        result[i - 1] = high;
//        rank -= CombTablesNR.GetBinom(high, i);
//      }
//    }
//  }

//  // SECTION 3 — Permutations WITHOUT Repetition (Span‑Optimized Lehmer Code)

//  public static class LehmerSpanNR
//  {
//    public static long Rank(ReadOnlySpan<int> perm, Span<bool> scratchUsed)
//    {
//      scratchUsed.Clear();
//      int n = perm.Length;
//      long rank = 0;

//      for (int i = 0; i < n; i++)
//      {
//        int x = perm[i];
//        int smallerUnused = 0;

//        for (int v = 0; v < x; v++)
//          if (!scratchUsed[v]) smallerUnused++;

//        rank += smallerUnused * CombTablesNR.GetFact(n - 1 - i);
//        scratchUsed[x] = true;
//      }

//      return rank;
//    }

//    public static void Unrank(long rank, Span<int> result, Span<bool> scratchUsed)
//    {
//      scratchUsed.Clear();
//      int n = result.Length;

//      for (int i = 0; i < n; i++)
//      {
//        long f = CombTablesNR.GetFact(n - 1 - i);
//        int block = (int)(rank / f);
//        rank %= f;

//        int count = 0;
//        for (int v = 0; v < n; v++)
//        {
//          if (!scratchUsed[v])
//          {
//            if (count == block)
//            {
//              result[i] = v;
//              scratchUsed[v] = true;
//              break;
//            }
//            count++;
//          }
//        }
//      }
//    }
//  }

//  // SECTION 4 — Unified Encoder WITHOUT Repetition

//  public static class ComboPermEncoderSpanNR
//  {
//    public static long Rank(
//        ReadOnlySpan<int> combo,
//        ReadOnlySpan<int> perm,
//        Span<bool> scratchUsed)
//    {
//      long rCombo = CombinadicsSpanNR.Rank(combo);
//      long rPerm = LehmerSpanNR.Rank(perm, scratchUsed);
//      long block = CombTablesNR.GetFact(perm.Length);

//      return rCombo * block + rPerm;
//    }

//    public static void Unrank(
//        long rank,
//        int k,
//        int maxValue,
//        Span<int> comboResult,
//        Span<int> permResult,
//        Span<bool> scratchUsed)
//    {
//      long block = CombTablesNR.GetFact(k);

//      long rCombo = rank / block;
//      long rPerm = rank % block;

//      CombinadicsSpanNR.Unrank(rCombo, k, comboResult, maxValue);
//      LehmerSpanNR.Unrank(rPerm, permResult, scratchUsed);
//    }
//  }

//  // SECTION 5 — Combinations WITH Repetition (Multiset Combinadics, Span‑Optimized)

//  public static class MultiCombinadicsSpan
//  {
//    public static long Rank(ReadOnlySpan<int> combo)
//    {
//      long rank = 0;
//      for (int i = 0; i < combo.Length; i++)
//      {
//        int ci = combo[i];
//        int k = i + 1;
//        rank += CombTablesNR.GetBinom(ci + k, k);
//      }
//      return rank;
//    }

//    public static void Unrank(long rank, int k, Span<int> result, int maxValue)
//    {
//      for (int i = k; i >= 1; i--)
//      {
//        int low = 0;
//        int high = maxValue;

//        while (low <= high)
//        {
//          int mid = (low + high) >> 1;
//          long val = CombTablesNR.GetBinom(mid + i, i);
//          if (val <= rank)
//            low = mid + 1;
//          else
//            high = mid - 1;
//        }

//        int chosen = high;
//        result[i - 1] = chosen;
//        rank -= CombTablesNR.GetBinom(chosen + i, i);
//      }
//    }
//  }

//  // SECTION 6 — Permutations WITH Repetition (Multiset Permutations, Span‑Optimized)

//  public static class MultiLehmerSpan
//  {
//    public static long CountPerms(ReadOnlySpan<int> counts)
//    {
//      int total = 0;
//      long denom = 1;
//      for (int i = 0; i < counts.Length; i++)
//      {
//        int c = counts[i];
//        total += c;
//        denom *= CombTablesNR.GetFact(c);
//      }
//      return CombTablesNR.GetFact(total) / denom;
//    }

//    public static long Rank(ReadOnlySpan<int> sequence, int alphabetSize, Span<int> scratchCounts)
//    {
//      scratchCounts.Clear();
//      for (int i = 0; i < sequence.Length; i++)
//        scratchCounts[sequence[i]]++;

//      long rank = 0;

//      for (int pos = 0; pos < sequence.Length; pos++)
//      {
//        int current = sequence[pos];

//        for (int sym = 0; sym < current; sym++)
//        {
//          if (scratchCounts[sym] == 0) continue;

//          scratchCounts[sym]--;
//          rank += CountPerms(scratchCounts);
//          scratchCounts[sym]++;
//        }

//        scratchCounts[current]--;
//      }

//      return rank;
//    }

//    public static void Unrank(long rank, int length, Span<int> counts, Span<int> result)
//    {
//      int alphabetSize = counts.Length;

//      for (int pos = 0; pos < length; pos++)
//      {
//        for (int sym = 0; sym < alphabetSize; sym++)
//        {
//          if (counts[sym] == 0) continue;

//          counts[sym]--;
//          long block = CountPerms(counts);

//          if (block > rank)
//          {
//            result[pos] = sym;
//            break;
//          }

//          rank -= block;
//          counts[sym]++;
//        }
//      }
//    }
//  }

//  // SECTION 7 — Unified Encoder WITH Repetition

//  public static class MultiComboPermEncoderSpan
//  {
//    public static long Rank(
//        ReadOnlySpan<int> combo,
//        ReadOnlySpan<int> seq,
//        int alphabetSize,
//        Span<int> scratchCounts)
//    {
//      long rCombo = MultiCombinadicsSpan.Rank(combo);

//      scratchCounts.Slice(0, alphabetSize).Clear();
//      for (int i = 0; i < seq.Length; i++)
//        scratchCounts[seq[i]]++;

//      long totalPerms = MultiLehmerSpan.CountPerms(scratchCounts.Slice(0, alphabetSize));
//      long rPerm = MultiLehmerSpan.Rank(seq, alphabetSize, scratchCounts.Slice(0, alphabetSize));

//      return rCombo * totalPerms + rPerm;
//    }

//    public static void Unrank(
//        long rank,
//        int k,
//        int length,
//        int alphabetSize,
//        int maxValue,
//        Span<int> comboResult,
//        Span<int> seqResult,
//        Span<int> scratchCounts)
//    {
//      MultiCombinadicsSpan.Unrank(rank: 0, k, comboResult, maxValue);

//      scratchCounts.Slice(0, alphabetSize).Clear();
//      for (int i = 0; i < k; i++)
//        scratchCounts[comboResult[i]]++;

//      long totalPerms = MultiLehmerSpan.CountPerms(scratchCounts.Slice(0, alphabetSize));

//      long rCombo = rank / totalPerms;
//      long rPerm = rank % totalPerms;

//      MultiCombinadicsSpan.Unrank(rCombo, k, comboResult, maxValue);

//      scratchCounts.Slice(0, alphabetSize).Clear();
//      for (int i = 0; i < k; i++)
//        scratchCounts[comboResult[i]]++;

//      MultiLehmerSpan.Unrank(rPerm, length, scratchCounts.Slice(0, alphabetSize), seqResult);
//    }
//  }
//}
