//namespace Flux
//{
//  public class ComboPermEncoder
//  {
//    private readonly FastCombinadics m_fastCombinadics;
//    private readonly FastLehmer m_fastLehmer;

//    public ComboPermEncoder(int maxN)
//    {
//      m_fastCombinadics = new FastCombinadics(maxN);
//      m_fastLehmer = new FastLehmer(maxN); // factorials up to maxN
//    }

//    // Rank a combination + permutation
//    // combo: sorted array of chosen elements
//    // perm: permutation of indices 0..k-1
//    public long Rank(int[] combo, int[] perm)
//    {
//      int k = combo.Length;

//      long rCombo = m_fastCombinadics.Rank(combo);
//      long rPerm = m_fastLehmer.Rank(perm);

//      return rCombo * m_fastLehmer.Fact(k) + rPerm;
//    }

//    // Unrank into (combination, permutation)
//    public (int[] combo, int[] perm) Unrank(long rank, int k)
//    {
//      long blockSize = m_fastLehmer.Fact(k);

//      long rCombo = rank / blockSize;
//      long rPerm = rank % blockSize;

//      int[] combo = m_fastCombinadics.Unrank(rCombo, k);
//      int[] perm = m_fastLehmer.Unrank(rPerm);

//      return (combo, perm);
//    }
//  }

//  public class FastCombinadics
//  {
//    private readonly long[,] C;   // Pascal table
//    private readonly int maxN;

//    public FastCombinadics(int maxN)
//    {
//      this.maxN = maxN;
//      C = new long[maxN + 1, maxN + 1];
//      BuildPascal();
//    }

//    private void BuildPascal()
//    {
//      for (int n = 0; n <= maxN; n++)
//      {
//        C[n, 0] = 1;
//        C[n, n] = 1;
//        for (int k = 1; k < n; k++)
//        {
//          C[n, k] = C[n - 1, k - 1] + C[n - 1, k];
//        }
//      }
//    }

//    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
//    public long Binom(int n, int k) => C[n, k];

//    // Rank a sorted combination
//    public long Rank(int[] combo)
//    {
//      long rank = 0;
//      for (int i = 0; i < combo.Length; i++)
//      {
//        rank += Binom(combo[i], i + 1);
//      }
//      return rank;
//    }

//    // Unrank using binary search
//    public int[] Unrank(long rank, int k)
//    {
//      int[] combo = new int[k];

//      for (int i = k; i >= 1; i--)
//      {
//        int low = i;
//        int high = maxN;

//        while (low <= high)
//        {
//          int mid = (low + high) >> 1;
//          if (Binom(mid, i) <= rank)
//            low = mid + 1;
//          else
//            high = mid - 1;
//        }

//        combo[i - 1] = high;
//        rank -= Binom(high, i);
//      }

//      return combo;
//    }
//  }

//  public class FastLehmer
//  {
//    private readonly long[] fact;
//    private readonly int n;

//    public FastLehmer(int n)
//    {
//      this.n = n;
//      fact = new long[n + 1];
//      BuildFactorials();
//    }

//    private void BuildFactorials()
//    {
//      fact[0] = 1;
//      for (int i = 1; i <= n; i++)
//        fact[i] = fact[i - 1] * i;
//    }

//    [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
//    public long Fact(int k) => fact[k];

//    // Rank a permutation of 0..n-1
//    public long Rank(int[] perm)
//    {
//      long rank = 0;
//      bool[] used = new bool[n];

//      for (int i = 0; i < n; i++)
//      {
//        int x = perm[i];
//        int smallerUnused = 0;

//        for (int v = 0; v < x; v++)
//          if (!used[v]) smallerUnused++;

//        rank += smallerUnused * Fact(n - 1 - i);
//        used[x] = true;
//      }

//      return rank;
//    }

//    // Unrank into a permutation of 0..n-1
//    public int[] Unrank(long rank)
//    {
//      int[] perm = new int[n];
//      bool[] used = new bool[n];

//      for (int i = 0; i < n; i++)
//      {
//        long f = Fact(n - 1 - i);
//        int block = (int)(rank / f);
//        rank %= f;

//        int count = 0;
//        for (int v = 0; v < n; v++)
//        {
//          if (!used[v])
//          {
//            if (count == block)
//            {
//              perm[i] = v;
//              used[v] = true;
//              break;
//            }
//            count++;
//          }
//        }
//      }

//      return perm;
//    }
//  }

//}
