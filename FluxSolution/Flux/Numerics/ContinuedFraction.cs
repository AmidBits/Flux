namespace Flux
{
  /// <summary>
  /// <para></para>
  /// <para><see href=""/></para>
  /// <para><see href="https://rosettacode.org/wiki/Continued_fraction/Arithmetic/G(matrix_ng,_continued_fraction_n1,_continued_fraction_n2)"/></para>
  /// </summary>
  public readonly struct ContinuedFraction
    : System.Collections.Generic.IEnumerable<System.Numerics.BigInteger>
  {
    public static ContinuedFraction E { get; } = (ContinuedFraction)Simplify(Cons(2, NaturalNumbers()), Cons(1, NaturalNumbers()));

    public static ContinuedFraction One { get; } = (ContinuedFraction)1;

    public static ContinuedFraction Pi { get; } = (ContinuedFraction)Simplify(Cons(3, Repeat(6)), Squared(Odds()));

    public static ContinuedFraction Sqrt2 { get; } = (ContinuedFraction)Cons(1, Repeat(2));

    private readonly System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> m_seq;

    public ContinuedFraction(System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> s)
    {
      var arr = s.Take(2).ToArray();

      if (arr.Length > 1 && arr[1] < 0)
        m_seq = Negate(s);
      else
        m_seq = s;
    }

    public ContinuedFraction(System.Func<System.Collections.Generic.IEnumerable<System.Numerics.BigInteger>> f) : this(f()) { }

    #region Static methods

    static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> Repeat(System.Numerics.BigInteger n) { while (true) yield return n; }

    static ContinuedFraction Cons(System.Numerics.BigInteger n, System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> s) => (ContinuedFraction)new[] { n }.Concat(s);

    static ContinuedFraction Reciprocal(System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> s) => s.First() == 0 ? (ContinuedFraction)s.Skip(1) : (ContinuedFraction)Cons(0, s);

    static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> NaturalNumbers() { System.Numerics.BigInteger n = 1; while (true) { yield return n; n++; } }

    static ContinuedFraction Squared(System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> s) => (ContinuedFraction)s.Select(n => n * n);

    static ContinuedFraction Odds() => (ContinuedFraction)NaturalNumbers().Select(n => 2 * n - 1);

    static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> Simplify(System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> aseq, System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> bseq)
    {
      System.Numerics.BigInteger a = 0, b = 1, c = 1, d = 0;

      using var e = bseq.GetEnumerator();

      foreach (var t in aseq)
      {
        var u = e.MoveNext() ? e.Current : 1;

        (a, b) = (u * b, a + t * b);
        (c, d) = (u * d, c + t * d);

        while (!c.IsZero && !d.IsZero)
        {
          var m = System.Numerics.BigInteger.DivRem(a, c, out var r);
          var n = System.Numerics.BigInteger.DivRem(b, d, out var s);

          if (m != n)
            break;

          yield return n;

          (a, c) = (c, r);
          (b, d) = (d, s);
        }
      }

      while (!b.IsZero && !d.IsZero)
      {
        var n = System.Numerics.BigInteger.DivRem(b, d, out var s);

        yield return n;

        (b, d) = (d, s);
      }
    }

    #endregion

    #region Implemented operators

    private static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> Just(System.Numerics.BigInteger n) { yield return n; }

    public static explicit operator ContinuedFraction(int n) => new(Just(n));

    public static explicit operator ContinuedFraction(System.Numerics.BigInteger n) => new(Just(n));

    public static ContinuedFraction Ratio(System.Numerics.BigInteger n, System.Numerics.BigInteger d)
      => n.Sign * d.Sign >= 0 ? new(One.Transform(n, 0, d, 0)) : -new ContinuedFraction(One.Transform(-n, 0, d, 0));

    static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GenerateSequence(System.Func<System.Numerics.BigInteger> generator)
    {
      while (true)
        yield return generator();
    }

    public static ContinuedFraction Generate(System.Func<System.Numerics.BigInteger> generator)
      => new(GenerateSequence(generator));

    private System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> Transform(System.Numerics.BigInteger a1, System.Numerics.BigInteger a, System.Numerics.BigInteger b1, System.Numerics.BigInteger b)
    {
      foreach (var term in m_seq)
      {
        (a, a1) = (a1, a + term * a1);
        (b, b1) = (b1, b + term * b1);

        while (!b1.IsZero && !b.IsZero)
        {
          var m = System.Numerics.BigInteger.DivRem(a1, b1, out var r);
          var n = System.Numerics.BigInteger.DivRem(a, b, out var s);

          if (m != n)
            break;

          yield return n;
          (a1, b1) = (b1, r);
          (a, b) = (b, s);
        }
      }

      while (!a1.IsZero && !b1.IsZero)
      {
        var n = System.Numerics.BigInteger.DivRem(a1, b1, out var s);
        yield return n;
        (a1, b1) = (b1, s);
      }
    }

    private static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> AddInt(System.Numerics.BigInteger n, ContinuedFraction cf)
    {
      yield return n + cf.First();

      foreach (var term in cf.Skip(1))
        yield return term;
    }

    private static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> Negate(System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> seq)
    {
      var arr = seq.Take(3).ToArray();

      if (arr.Length == 0)
        return [];

      if (arr.Length == 1)
        return [-arr[0]];

      if (arr[1] == System.Numerics.BigInteger.One)
        return new[] { -arr[0] - System.Numerics.BigInteger.One, arr[2] + System.Numerics.BigInteger.One }.Concat(seq.Skip(3));

      return new[] { -arr[0] - System.Numerics.BigInteger.One, System.Numerics.BigInteger.One, arr[1] - System.Numerics.BigInteger.One }.Concat(seq.Skip(2));
    }

    public static ContinuedFraction operator -(ContinuedFraction cf) => new(Negate(cf.m_seq));
    public static ContinuedFraction operator +(System.Numerics.BigInteger a, ContinuedFraction b) => new(AddInt(a, b));
    public static ContinuedFraction operator +(ContinuedFraction a, System.Numerics.BigInteger b) => new(AddInt(b, a));
    public static ContinuedFraction operator -(System.Numerics.BigInteger a, ContinuedFraction b) => new(AddInt(-a, b));
    public static ContinuedFraction operator -(ContinuedFraction a, System.Numerics.BigInteger b) => new(AddInt(-b, a));
    public static ContinuedFraction operator *(System.Numerics.BigInteger n, ContinuedFraction d) => new(d.Transform(n, 0, 0, 1));
    public static ContinuedFraction operator *(ContinuedFraction n, System.Numerics.BigInteger d) => new(n.Transform(d, 0, 0, 1));
    public static ContinuedFraction operator /(System.Numerics.BigInteger n, ContinuedFraction d) => new(d.Transform(0, n, 1, 0));
    public static ContinuedFraction operator /(ContinuedFraction n, System.Numerics.BigInteger d) => new(n.Transform(1, 0, 0, d));

    private static System.Numerics.BigInteger FloorSqrt(System.Numerics.BigInteger n)
    {
      var guess = System.Numerics.BigInteger.One;

      while (true)
      {
        var newGuess = (n / guess + guess) >> 1;

        if (guess == newGuess)
          return guess;

        if (System.Numerics.BigInteger.Abs(guess - newGuess) < 2)
        {
          var sign1 = (guess * guess - n).Sign;
          var sign2 = (newGuess * newGuess - n).Sign;

          if (sign1 * sign2 == -1)
            return System.Numerics.BigInteger.Min(guess, newGuess);
        }

        guess = newGuess;
      }
    }

    static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> SqrtSeq(System.Numerics.BigInteger S)
    {
      ArgumentOutOfRangeException.ThrowIfLessThan(S, System.Numerics.BigInteger.Zero, nameof(S));

      System.Numerics.BigInteger a0 = FloorSqrt(S);

      yield return a0;

      if (a0 * a0 == S)
        yield break;

      System.Numerics.BigInteger m = 0;
      System.Numerics.BigInteger d = 1;
      System.Numerics.BigInteger a = a0;

      while (true)
      {
        m = d * a - m;
        d = (S - m * m) / d;
        a = (a0 + m) / d;

        yield return a;
      }
    }

    public static ContinuedFraction Sqrt(System.Numerics.BigInteger n) => new(SqrtSeq(n));

    private static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> Transform(
             System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> x,
             System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> y,
             System.Numerics.BigInteger a12, System.Numerics.BigInteger a1, System.Numerics.BigInteger a2, System.Numerics.BigInteger a,
             System.Numerics.BigInteger b12, System.Numerics.BigInteger b1, System.Numerics.BigInteger b2, System.Numerics.BigInteger b
    )
    {
      using var ea = x.GetEnumerator();
      var ha = ea.MoveNext();
      using var eb = y.GetEnumerator();
      var hb = eb.MoveNext();

      var count = 0;

      while (ha && hb)
      {
        var p = ea.Current;
        (a, a1) = (a1, a + a1 * p);
        (a2, a12) = (a12, a2 + a12 * p);
        (b, b1) = (b1, b + b1 * p);
        (b2, b12) = (b12, b2 + b12 * p);
        ha = ea.MoveNext();

        var q = eb.Current;
        (a, a2) = (a2, a + a2 * q);
        (a1, a12) = (a12, a1 + a12 * q);
        (b, b2) = (b2, b + b2 * q);
        (b1, b12) = (b12, b1 + b12 * q);
        hb = eb.MoveNext();

        if (b.IsZero && b1.IsZero && b2.IsZero && b12.IsZero)
          yield break;

        while (!b.IsZero && !b1.IsZero && !b2.IsZero && !b12.IsZero)
        {
          var q0 = System.Numerics.BigInteger.DivRem(a, b, out var r0);
          var q1 = System.Numerics.BigInteger.DivRem(a1, b1, out var r1);
          var q2 = System.Numerics.BigInteger.DivRem(a2, b2, out var r2);
          var q3 = System.Numerics.BigInteger.DivRem(a12, b12, out var r3);

          if (q0 != q1 || q0 != q2 || q0 != q3)
            break;

          yield return q0;

          count = 0;

          (a, b) = (b, r0);
          (a1, b1) = (b1, r1);
          (a2, b2) = (b2, r2);
          (a12, b12) = (b12, r3);
        }

        if (++count >= 1000)
          goto abort;
      }

      while (ha)  // the rest of y is infinite, a, b, e, f don't matter, calculate (c + dx) / (g + hx)
      {
        var p = ea.Current;

        (a2, a12) = (a12, a2 + a12 * p);
        (b2, b12) = (b12, b2 + b12 * p);

        ha = ea.MoveNext();

        if (b2.IsZero && b12.IsZero)
          yield break;

        while (!b2.IsZero && !b12.IsZero)
        {
          var q2 = System.Numerics.BigInteger.DivRem(a2, b2, out var r2);
          var q3 = System.Numerics.BigInteger.DivRem(a12, b12, out var r3);

          if (q2 != q3)
            break;

          yield return q2;

          (a2, b2) = (b2, r2);
          (a12, b12) = (b12, r3);
        }
      }

      while (hb)  // rest of x is infinite, a, c, e, g don't matter, calculate (b + dy) / (f + hy)
      {
        var q = eb.Current;

        (a1, a12) = (a12, a1 + a12 * q);
        (b1, b12) = (b12, b1 + b12 * q);

        hb = eb.MoveNext();

        if (b1.IsZero && b12.IsZero)
          yield break;

        while (!b1.IsZero && !b12.IsZero)
        {
          var q1 = System.Numerics.BigInteger.DivRem(a1, b1, out var r1);
          var q3 = System.Numerics.BigInteger.DivRem(a12, b12, out var r3);

          if (q1 != q3)
            break;

          yield return q1;

          (a1, b1) = (b1, r1);
          (a12, b12) = (b12, r3);
        }
      }

    abort:

      if (b12.IsZero)
        yield break;

      System.Numerics.BigInteger? lastTerm = null;

      foreach (var term in Ratio(a12, b12))
      {
        if (lastTerm.HasValue)
          yield return lastTerm.Value;

        lastTerm = term;
      }

      if (lastTerm.HasValue && System.Numerics.BigInteger.Log2(lastTerm.Value) < 100)
        yield return lastTerm.Value;
    }

    public static ContinuedFraction operator +(ContinuedFraction x, ContinuedFraction y) => new(Transform(x, y, 0, 1, 1, 0, 0, 0, 0, 1));
    public static ContinuedFraction operator -(ContinuedFraction x, ContinuedFraction y) => new(Transform(x, y, 0, 1, -1, 0, 0, 0, 0, 1));
    public static ContinuedFraction operator *(ContinuedFraction x, ContinuedFraction y) => new(Transform(x, y, 1, 0, 0, 0, 0, 0, 0, 1));
    public static ContinuedFraction operator /(ContinuedFraction x, ContinuedFraction y) => new(Transform(x, y, 0, 1, 0, 0, 0, 0, 1, 0));

    #endregion

    #region Implemented interfaces

    public IEnumerator<System.Numerics.BigInteger> GetEnumerator() => m_seq.GetEnumerator();

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

    #endregion

    public override string ToString()
    {
      var s = new System.Text.StringBuilder();

      s.Append('[');

      var n = 0;

      foreach (var t in m_seq.Take(21))
      {
        if (n == 20)
        {
          s.Append(", ...");
          break;
        }

        if (n == 1)
          s.Append("; ");
        else if (n > 1)
          s.Append(", ");

        s.Append(t);

        n++;
      }

      s.Append(']');

      return s.ToString();
    }
  }
}
