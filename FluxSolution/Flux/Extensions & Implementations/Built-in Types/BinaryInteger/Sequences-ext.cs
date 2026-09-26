namespace Flux
{
  public static partial class BinaryInteger
  {
    extension<TInteger>(TInteger)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      #region FibonacciSequence

      /// <summary>
      /// <para>Creates a new sequence of <typeparamref name="TInteger"/> with Fibonacci numbers.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Fibonacci_number"/></para>
      /// </summary>
      /// <remarks>This function generate results until the type <typeparamref name="TInteger"/> under/overflows in any calculation. No exception is thrown.</remarks>
      /// <typeparam name="TInteger"></typeparam>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<TInteger> FibonacciSequence()
      {
        var n1 = TInteger.Zero;
        var n2 = TInteger.One;

        while (true)
        {
          yield return n1;

          try { checked { n1 += n2; } } catch { break; }

          yield return n2;

          try { checked { n2 += n1; } } catch { break; }
        }
      }

      #endregion

      #region IsFibonacciNumber

      /// <summary>
      /// <para>Determines whether the <paramref name="number"/> is a Fibonacci number.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Fibonacci_number"/></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="number"></param>
      /// <returns></returns>
      public static bool IsFibonacciNumber(TInteger n)
      {
        checked
        {
          var four = TInteger.CreateChecked(4);

          var fivens = TInteger.CreateChecked(5) * n * n;
          var fp4 = fivens + four;
          var fp4sr = SquareRoot(fp4);
          var fm4 = fivens - four;
          var fm4sr = SquareRoot(fm4);

          return fp4sr * fp4sr == fp4 || fm4sr * fm4sr == fm4;
        }
      }

      #endregion

      #region LeonardoSequence

      /// <summary>
      /// <para>Creates a new sequence with Leonardo numbers.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Leonardo_number"/></para>
      /// </summary>
      /// <param name="first"></param>
      /// <param name="second"></param>
      /// <param name="step"></param>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<TInteger> LeonardoSequence(TInteger first, TInteger second, TInteger step)
      {
        while (true)
        {
          yield return first;

          checked { (first, second) = (second, first + second + step); }
        }
      }

      #endregion

      #region MoserDeBruijnSequence

      /// <summary>Creates a sequence of Moser/DeBruijn numbers.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Moser%E2%80%93De_Bruijn_sequence"/>
      /// <seealso cref="https://www.geeksforgeeks.org/moser-de-bruijn-sequence/"/>
      public static System.Collections.Generic.List<TInteger> MoserDeBruijnSequence(TInteger n)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(n);

        var sequence = new System.Collections.Generic.List<TInteger>(int.CreateChecked(n)) { TInteger.Zero };

        if (n > TInteger.Zero)
          sequence.Add(TInteger.One);

        for (var i = TInteger.CreateChecked(2); i < n; i++)
        {
          var (q, r) = TInteger.DivRem(i, TInteger.CreateChecked(2));

          var next = TInteger.CreateChecked(4) * sequence[int.CreateChecked(q)];

          if (!TInteger.IsZero(r)) // Zero remainder: S(2 * n) = 4 * S(n)
            next++; // Non-zero remainder: S(2 * n + 1) = 4 * S(n) + 1

          sequence.Add(next);
        }

        return sequence;
      }

      #endregion

      #region PadovanSequence

      /// <summary>
      /// <para>Creates a new sequence with Padovan numbers.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Padovan_sequence"/></para>
      /// </summary>
      /// <remarks>This function generate results until the type <typeparamref name="TInteger"/> under/overflows in any calculation. No exception is thrown.</remarks>
      /// <typeparam name="TSelf"></typeparam>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<TInteger> PadovanSequence()
      {
        TInteger p1 = TInteger.One, p2 = TInteger.One, p3 = TInteger.One;

        yield return p1;
        yield return p2;
        yield return p3;

        TInteger pn;

        while (true)
        {
          try
          {
            pn = checked(p2 + p3);
          }
          catch { break; }

          yield return pn;

          p3 = p2;
          p2 = p1;
          p1 = pn;
        }
      }

      #endregion

      #region PerrinNumbers

      /// <summary>
      /// <para>Creates an indefinite sequence of Perrin numbers.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Perrin_number"/></para>
      /// </summary>
      /// <remarks>This function generate results until the type <typeparamref name="TInteger"/> under/overflows in any calculation. No exception is thrown.</remarks>
      /// <typeparam name="TSelf"></typeparam>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<TInteger> PerrinNumbers()
      {
        TInteger a = TInteger.CreateChecked(3), b = TInteger.Zero, c = TInteger.CreateChecked(2);

        yield return a;
        yield return b;
        yield return c;

        TInteger p;

        while (true)
        {
          try
          {
            p = checked(a + b);
          }
          catch { break; }

          a = b;
          b = c;
          c = p;

          yield return p;
        }
      }

      #endregion

      #region VanEcksSequence

      /// <summary>
      /// <para>Creates a new Van Eck's sequence, starting with the specified <paramref name="minNumber"/> (where 0 yields the original sequence).</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Van_Eck%27s_sequence"/></para>
      /// </summary>
      /// <param name="minNumber"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public static System.Collections.Generic.IEnumerable<TInteger> VanEcksSequence(TInteger minNumber)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(minNumber);

        var lasts = new System.Collections.Generic.Dictionary<TInteger, TInteger>();
        var last = minNumber;

        checked
        {
          for (var index = TInteger.Zero; ; index++)
          {
            yield return last;

            TInteger next = TInteger.Zero;

            if (!lasts.TryAdd(last, index))
            {
              next = index - lasts[last];

              lasts[last] = index;
            }

            last = next;
          }
        }
      }

      #endregion
    }
  }
}
