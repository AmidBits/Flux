//namespace Flux
//{
//  public class MultiCombinadics
//  {
//    private readonly long[,] C;
//    private readonly int maxN;

//    public MultiCombinadics(int maxN)
//    {
//      this.maxN = maxN;
//      C = new long[maxN + maxN + 1, maxN + 1];
//      BuildPascal();
//    }

//    private void BuildPascal()
//    {
//      for (int n = 0; n < C.GetLength(0); n++)
//      {
//        C[n, 0] = 1;
//        if (n < C.GetLength(1))
//          C[n, n] = 1;

//        for (int k = 1; k < Math.Min(n, C.GetLength(1)); k++)
//          C[n, k] = C[n - 1, k - 1] + C[n - 1, k];
//      }
//    }

//    private long Binom(int n, int k) => C[n, k];

//    public long Rank(int[] combo)
//    {
//      long rank = 0;
//      for (int i = 0; i < combo.Length; i++)
//        rank += Binom(combo[i] + (i + 1), i + 1);
//      return rank;
//    }

//    public int[] Unrank(long rank, int k)
//    {
//      int[] combo = new int[k];

//      for (int i = k; i >= 1; i--)
//      {
//        int low = 0;
//        int high = maxN;

//        while (low <= high)
//        {
//          int mid = (low + high) >> 1;
//          if (Binom(mid + i, i) <= rank)
//            low = mid + 1;
//          else
//            high = mid - 1;
//        }

//        combo[i - 1] = high;
//        rank -= Binom(high + i, i);
//      }

//      return combo;
//    }
//  }

//  public class MultiLehmer
//  {
//    private readonly long[] fact;

//    public MultiLehmer(int maxN)
//    {
//      fact = new long[maxN + 1];
//      fact[0] = 1;
//      for (int i = 1; i <= maxN; i++)
//        fact[i] = fact[i - 1] * i;
//    }

//    private long Fact(int n) => fact[n];

//    internal long CountPerms(int[] counts)
//    {
//      int total = 0;
//      long denom = 1;

//      foreach (int c in counts)
//      {
//        total += c;
//        denom *= Fact(c);
//      }

//      return Fact(total) / denom;
//    }

//    public long Rank(int[] sequence, int alphabetSize)
//    {
//      long rank = 0;

//      int[] counts = new int[alphabetSize];
//      foreach (int x in sequence)
//        counts[x]++;

//      for (int i = 0; i < sequence.Length; i++)
//      {
//        int current = sequence[i];

//        for (int sym = 0; sym < current; sym++)
//        {
//          if (counts[sym] > 0)
//          {
//            counts[sym]--;
//            rank += CountPerms(counts);
//            counts[sym]++;
//          }
//        }

//        counts[current]--;
//      }

//      return rank;
//    }

//    public int[] Unrank(long rank, int length, int[] counts)
//    {
//      int alphabetSize = counts.Length;
//      int[] result = new int[length];

//      for (int pos = 0; pos < length; pos++)
//      {
//        for (int sym = 0; sym < alphabetSize; sym++)
//        {
//          if (counts[sym] == 0)
//            continue;

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

//      return result;
//    }
//  }

//  public class MultiComboPermEncoder
//  {
//    private readonly MultiCombinadics comb;
//    private readonly MultiLehmer perm;

//    public MultiComboPermEncoder(int maxN)
//    {
//      comb = new MultiCombinadics(maxN);
//      perm = new MultiLehmer(maxN);
//    }

//    public long Rank(int[] combo, int[] sequence, int alphabetSize)
//    {
//      long rCombo = comb.Rank(combo);

//      int[] counts = new int[alphabetSize];
//      foreach (int x in sequence)
//        counts[x]++;

//      long totalPerms = perm.CountPerms(counts);
//      long rPerm = perm.Rank(sequence, alphabetSize);

//      return rCombo * totalPerms + rPerm;
//    }

//    public (int[] combo, int[] sequence) Unrank(long rank, int k, int length, int alphabetSize)
//    {
//      int[] dummyCounts = new int[alphabetSize];
//      long totalPerms = perm.CountPerms(dummyCounts);

//      long rCombo = rank / totalPerms;
//      long rPerm = rank % totalPerms;

//      int[] combo = comb.Unrank(rCombo, k);

//      int[] counts = new int[alphabetSize];
//      foreach (int x in combo)
//        counts[x]++;

//      int[] sequence = perm.Unrank(rPerm, length, counts);

//      return (combo, sequence);
//    }
//  }
//}
