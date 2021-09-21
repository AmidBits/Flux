namespace Flux.Model
{
  public class Fifteen
  {
    private readonly int[] Nr = new[] { 3, 0, 0, 0, 0, 1, 1, 1, 1, 2, 2, 2, 2, 3, 3, 3 };
    private readonly int[] Nc = new[] { 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2, 3, 0, 1, 2 };

    private int n, _n;
    private readonly int[] E = new int[100];
    private readonly int[] M = new int[100];
    private readonly int[] C = new int[100];

    private readonly ulong[] B = new ulong[100];

    private bool Ti()
    {
      if (C[n] < _n)
        return Tm();

      if (B[n] == 0x123456789abcdef0)
      {
        System.Console.WriteLine($"Solution found in {n} moves: ");

        for (int g = 1; g <= n; ++g)
          System.Console.WriteLine((char)M[g]);

        System.Console.WriteLine();

        return true;
      };

      if (C[n] == _n)
        return Tm();
      else
        return false;
    }

    private bool Tm()
    {
      if (M[n] != 'u' && E[n] / 4 < 3) { Md(); ++n; if (Ti()) return true; --n; }
      if (M[n] != 'd' && E[n] / 4 > 0) { Mu(); ++n; if (Ti()) return true; --n; }
      if (M[n] != 'l' && E[n] % 4 < 3) { Mr(); ++n; if (Ti()) return true; --n; }
      if (M[n] != 'r' && E[n] % 4 > 0) { Ml(); ++n; if (Ti()) return true; --n; }
      return false;
    }

    private void Md()
    {
      int g = (11 - E[n]) * 4;
      ulong a = B[n] & ((ulong)15 << g);
      E[n + 1] = E[n] + 4;
      B[n + 1] = B[n] - a + (a << 16);
      M[n + 1] = 'd';
      C[n + 1] = C[n] + (Nr[a >> g] <= E[n] / 4 ? 0 : 1);
    }

    private void Mu()
    {
      int g = (19 - E[n]) * 4;
      ulong a = B[n] & ((ulong)15 << g);
      E[n + 1] = E[n] - 4;
      B[n + 1] = B[n] - a + (a >> 16);
      M[n + 1] = 'u';
      C[n + 1] = C[n] + (Nr[a >> g] >= E[n] / 4 ? 0 : 1);
    }

    private void Mr()
    {
      int g = (14 - E[n]) * 4;
      ulong a = B[n] & ((ulong)15 << g);
      E[n + 1] = E[n] + 1;
      B[n + 1] = B[n] - a + (a << 4);
      M[n + 1] = 'r';
      C[n + 1] = C[n] + (Nc[a >> g] <= E[n] % 4 ? 0 : 1);
    }

    private void Ml()
    {
      int g = (16 - E[n]) * 4;
      ulong a = B[n] & ((ulong)15 << g);
      E[n + 1] = E[n] - 1;
      B[n + 1] = B[n] - a + (a >> 4);
      M[n + 1] = 'l';
      C[n + 1] = C[n] + (Nc[a >> g] >= E[n] % 4 ? 0 : 1);
    }

    [System.CLSCompliant(false)]
    public Fifteen(int n, ulong g)
    {
      E[0] = n;
      B[0] = g;
    }
    public Fifteen(int n, long g)
      : this(n, unchecked((ulong)g))
    { }

    public void Solve()
    {
      for (; !Ti(); ++_n) ;
    }
  }
}

/*
  // Easier
  var t = new Flux.Model.Fifteen(8, 0xfe169b4c0a73d852);
  t.Solve();

  // Harder
  var t = new Flux.Model.Fifteen(0, 0x0c9dfbae37254861);
  t.Solve();
*/
