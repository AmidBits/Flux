namespace Flux
{
  public static partial class NumberSequence
  {
    extension<TInteger>(TInteger)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      #region (Highly, Super) Abundant numbers

      /// <summary>
      /// <para>Creates a new sequence of abundant numbers.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Abundant_number"/></para>
      /// </summary>
      /// <typeparam name="TSelf"></typeparam>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<(TInteger Number, TInteger Sum)> GetAbundantNumbers()
        => TInteger.CreateChecked(3).LoopVerge(TInteger.One).AsParallel().AsOrdered().Select(n => (n, sum: n.SumDivisors().Sum - n)).Where(x => x.sum > x.n);
      //=> Enumerable.Loop(() => (System.Numerics.BigInteger)3, e => true, e => e + 1, e => e).AsParallel().AsOrdered().Select(n => (n, sum: NumberSequences.Factors.GetSumOfDivisors(n) - n)).Where(x => x.sum > x.n);

      /// <summary>
      /// <para>Creates a new sequence of highly abundant numbers.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Highly_abundant_number"/></para>
      /// </summary>
      /// <typeparam name="TSelf"></typeparam>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<(TInteger Number, TInteger Sum)> GetHighlyAbundantNumbers()
      {
        var largestSumOfDivisors = TInteger.Zero;

        for (var index = TInteger.One; ; index++)
          if (index.SumDivisors().Sum is var sumOfDivisors && sumOfDivisors > largestSumOfDivisors)
          {
            yield return (index, sumOfDivisors);

            largestSumOfDivisors = sumOfDivisors;
          }
      }

      /// <summary>
      /// <para>Creates a new sequence of super-abundant numbers.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Superabundant_number"/></para>
      /// </summary>
      /// <typeparam name="TSelf"></typeparam>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<(TInteger Number, TInteger Sum)> GetSuperAbundantNumbers()
      {
        var largestValue = 0.0;

        foreach (var tuple in GetHighlyAbundantNumbers<TInteger>())
          if ((double.CreateChecked(tuple.Sum) / double.CreateChecked(tuple.Number)) is var value && value > largestValue)
          {
            yield return tuple;

            largestValue = value;
          }
      }

      #endregion

      #region Bell numbers

      /// <summary>
      /// <para>Creates a new sequence of Bell numbers.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Bell_number"/></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<TInteger> GetBellNumbers()
      {
        var current = new TInteger[1] { TInteger.One };

        while (true)
        {
          yield return current[0];

          try
          {
            checked
            {
              var previous = current;
              current = new TInteger[previous.Length + 1];
              current[0] = previous[^1];
              for (var i = 1; i <= previous.Length; i++)
                current[i] = previous[i - 1] + current[i - 1];
            }
          }
          catch { break; }
        }
      }

      /// <summary>
      /// <para>Creates a new sequence with arrays (i.e. row) of Bell numbers in a Bell triangle.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Bell_triangle"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Bell_number"/></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<TInteger[]> GetBellTriangle()
      {
        var current = new TInteger[] { TInteger.One };

        while (true)
        {
          yield return current;

          try
          {
            checked
            {
              var previous = current;
              current = new TInteger[previous.Length + 1];
              current[0] = previous[^1];
              for (var i = 1; i <= previous.Length; i++)
                current[i] = previous[i - 1] + current[i - 1];
            }
          }
          catch { break; }
        }
      }

      /// <summary>
      /// <para>Creates a new sequence with arrays (i.e. row) of Bell numbers in an augmented Bell triangle.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Bell_triangle"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Bell_number"/></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<TInteger[]> GetBellTriangleAugmented()
      {
        var current = new TInteger[] { TInteger.One };

        while (true)
        {
          yield return current;

          try
          {
            checked
            {
              var previous = current;
              current = new TInteger[previous.Length + 1];
              current[0] = (current[1] = previous[^1]) - previous[0];
              for (var i = 2; i <= previous.Length; i++)
                current[i] = previous[i - 1] + current[i - 1];
            }
          }
          catch { break; }
        }
      }

      #endregion

      ///// <summary>
      ///// <para>Creates a new sequence with Catalan numbers.</para>
      ///// <para><see href="https://en.wikipedia.org/wiki/Catalan_number"/></para>
      ///// </summary>
      ///// <remarks>This function runs indefinitely, if allowed.</remarks>
      ///// <typeparam name="TInteger"></typeparam>
      ///// <returns></returns>
      //public static System.Collections.Generic.IEnumerable<TInteger> GetCatalanSequence()
      //  => TInteger.Zero.LoopVerge(TInteger.One).AsParallel().AsOrdered().Select(GetCatalanNumber);

      #region (Highly) Composite numbers

      /// <summary>
      /// <para>Generates a new sequence of composite numbers.</para>
      /// </summary>
      /// <typeparam name="TSelf"></typeparam>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<TInteger> GetCompositeNumbers()
        => TInteger.One.LoopVerge(TInteger.One).AsParallel().AsOrdered().Where(IsComposite);

      /// <summary>
      /// <para>Creates a new sequence of highly composite numbers.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Highly_composite_number"/></para>
      /// </summary>
      /// <typeparam name="TSelf"></typeparam>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<(TInteger Number, TInteger Count)> GetHighlyCompositeNumbers()
      {
        var largestCount = TInteger.Zero;

        foreach (var tuple in TInteger.One.LoopVerge(TInteger.One).AsParallel().AsOrdered().Select(n => (Number: n, Count: n.CountDivisors())))
          if (tuple.Count > largestCount)
          {
            yield return tuple;

            largestCount = tuple.Count;
          }

        //var largestCountOfDivisors = TInteger.Zero;

        //var index = TInteger.One;

        //while (true)
        //{
        //  if (TInteger.CreateChecked(index.CountDivisors()) is var countOfDivisors && countOfDivisors > largestCountOfDivisors)
        //  {
        //    yield return (index, countOfDivisors);

        //    largestCountOfDivisors = countOfDivisors;
        //  }

        //  checked { index++; }
        //}
      }

      #endregion

      #region Fibonacci sequence

      /// <summary>
      /// <para>Creates a new sequence of <typeparamref name="TInteger"/> with Fibonacci numbers.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Fibonacci_number"/></para>
      /// </summary>
      /// <remarks>This function runs indefinitely, if allowed.</remarks>
      /// <typeparam name="TInteger"></typeparam>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<TInteger> GetFibonacciSequence()
      {
        var n1 = TInteger.Zero;
        var n2 = TInteger.One;

        while (true)
        {
          yield return n1;

          try
          {
            checked { n1 += n2; }
          }
          catch { break; }

          yield return n2;

          try
          {
            checked { n2 += n1; }
          }
          catch { break; }
        }
      }

      #endregion

      #region Mersenne sequences

      /// <summary>`
      /// <para>Creates a new sequence of Mersenne numbers.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Mersenne_number"/></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<TInteger> GetMersenneNumberSequence()
        => TInteger.One.LoopVerge(TInteger.One).Select(GetMersenneNumber);

      /// <summary>
      /// <para>Creates a new sequence of Mersenne primes.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Mersenne_number"/></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<TInteger> GetMersennePrimeSequence()
        => GetMersenneNumberSequence<TInteger>().Where(IsPrimeNumber);

      #endregion

      #region Padovan sequence

      /// <summary>
      /// <para>Creates a new sequence with Padovan numbers.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Padovan_sequence"/></para>
      /// </summary>
      /// <remarks>This function runs indefinitely, if allowed.</remarks>
      /// <typeparam name="TSelf"></typeparam>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<TInteger> GetPadovanSequence()
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

      #region Perrin numbers

      /// <summary>
      /// <para>Creates an indefinite sequence of Perrin numbers.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Perrin_number"/></para>
      /// </summary>
      /// <typeparam name="TSelf"></typeparam>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<TInteger> GetPerrinNumbers()
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
    }

    #region Centered Polygonal Numbers

    extension<TInteger>(TInteger k)
        where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      /// <summary>
      /// <para>Creates a new sequence of </para>
      /// <para><see href="https://en.wikipedia.org/wiki/Centered_polygonal_number"/></para>
      /// </summary>
      /// <remarks>This function runs indefinitely, if allowed.</remarks>
      /// <typeparam name="TSelf"></typeparam>
      /// <param name="numberOfSides"></param>
      /// <returns></returns>
      public System.Collections.Generic.IEnumerable<(TInteger LayerCount, TInteger CenterPolygonalNumber)> GetCenteredPolygonalLayers()
      {
        yield return (TInteger.One, TInteger.One);

        foreach (var v in GetCenteredPolygonalNumberSequence(k).PartitionTuple2(false, (previous, current, index) => (previous, current, index)))
          yield return (TInteger.CreateChecked(v.index + 2), v.current);
      }

      /// <summary></summary>
      /// <see href="https://en.wikipedia.org/wiki/Centered_polygonal_number"/>
      public TInteger GetCenteredPolygonalNumber(TInteger n)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(n);
        System.ArgumentOutOfRangeException.ThrowIfLessThan(k, TInteger.CreateChecked(3));

        return checked(k * n * (n + TInteger.One) / TInteger.CreateChecked(2) + TInteger.One);
      }

      /// <summary></summary>
      /// <see href="https://en.wikipedia.org/wiki/Centered_polygonal_number"/>
      /// <remarks>This function runs indefinitely, if allowed.</remarks>
      public System.Collections.Generic.IEnumerable<TInteger> GetCenteredPolygonalNumberSequence()
        => TInteger.Zero.LoopVerge(TInteger.One).Select(n => GetCenteredPolygonalNumber(k, n));
    }

    #endregion

    extension<TInteger>(TInteger n)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      #region Prime Numbers

      /// <summary>
      /// 
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="n"></param>
      /// <returns></returns>
      public (TInteger TowardZero, TInteger AwayFromZero) GetPrimeCandidates()
      {
        var n3 = TInteger.CreateChecked(3);
        var n5 = TInteger.CreateChecked(5);

        if (n >= n5)
        {
          var r = n % TInteger.CreateChecked(6);

          if (r == TInteger.One || r == n5) // E.g. 11 or 13 (12 = mod 6)
            return (n, n);

          if (TInteger.IsZero(r)) // E.g. 12 (mod 6)
            return (n - TInteger.One, n + TInteger.One);

          return (n - r + TInteger.One, n + n5 - r); // For all others we can use a formula.
        }
        else if (n == TInteger.CreateChecked(4))
          return (n3, n5);
        else if (n == n3)
          return (n3, n3);
        else // Less-than-or-equal-to 2:
        {
          var n2 = TInteger.CreateChecked(2);

          return (n2, n2);
        }
      }

      /// <summary>
      /// <para>A prime candidate is a number that is either -1 or +1 of a prime multiple, which is a multiple of 6. Obviously all prime candidates are not prime numbers, hence the name, but all prime numbers are prime candidates.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="value"></param>
      /// <param name="mode"></param>
      /// <param name="towardZero"></param>
      /// <param name="awayFromZero"></param>
      /// <returns></returns>
      public TInteger NearestPrimeCandidate(HalfRounding mode, out TInteger towardZero, out TInteger awayFromZero)
      {
        (towardZero, awayFromZero) = GetPrimeCandidates(n);

        return n.RoundToNearest(mode, towardZero, awayFromZero);
      }

      /// <summary>
      /// <para>Creates a new sequence of ascending potential primes, greater-than-or-equal-to the specified <paramref name="value"/>.</para>
      /// </summary>
      public System.Collections.Generic.IEnumerable<TInteger> GetAscendingPrimeCandidates()
      {
        //NearestPrimeCandidate<TInteger>(n, HalfRounding.TowardZero, out var _, out var _);

        if (TInteger.CreateChecked(2) is var two && n <= two)
          yield return two;

        if (TInteger.CreateChecked(3) is var three && n <= three)
          yield return three;

        if (TInteger.CreateChecked(5) is var five && n <= five)
          n = five;

        var six = TInteger.CreateChecked(6);

        var (quotient, remainder) = TInteger.DivRem(n, six);

        checked
        {
          var multiple = quotient * six;

          if (remainder <= TInteger.One) // Or, either between two potential primes or on right of a % 6 value. E.g. 12 or 13.
          {
            yield return multiple + TInteger.One;

            try { multiple += six; } catch { yield break; }
          }

          while (true)
          {
            yield return multiple - TInteger.One;
            yield return multiple + TInteger.One;

            try { multiple += six; } catch { yield break; }
          }
        }
      }

      /// <summary>
      /// <para>Creates a new sequence ascending prime numbers, greater-than-or-equal-to the specified <paramref name="value"/>.</para>
      /// </summary>
      public System.Collections.Generic.IEnumerable<TInteger> GetAscendingPrimes()
        => GetAscendingPrimeCandidates(n).AsParallel().AsOrdered().Where(IsPrimeNumber);

      /// <summary>Creates a new sequence of descending potential primes, less than the specified <paramref name="value"/>.</summary>
      public System.Collections.Generic.IEnumerable<TInteger> GetDescendingPrimeCandidates()
      {
        if (TInteger.CreateChecked(5) is var five && n >= five)
        {
          var six = TInteger.CreateChecked(6);

          var (quotient, remainder) = TInteger.DivRem(n, six);

          var multiple = (quotient + (remainder == five ? TInteger.One : TInteger.Zero)) * six;

          if (remainder == TInteger.Zero || remainder == five) // Or, either between two potential primes or on left of (startAt % 6). E.g. 11 or 12.
          {
            yield return multiple - TInteger.One;

            multiple -= six;
          }

          while (multiple >= six)
          {
            yield return multiple + TInteger.One;
            yield return multiple - TInteger.One;

            multiple -= six;
          }
        }

        if (TInteger.CreateChecked(3) is var three && n >= three)
          yield return three;

        if (TInteger.CreateChecked(2) is var two && n >= two)
          yield return two;
      }

      /// <summary>
      /// <para>Creates a new sequence descending prime numbers, less-than-or-equal-to the specified <paramref name="value"/>.</para>
      /// </summary>
      public System.Collections.Generic.IEnumerable<TInteger> GetDescendingPrimes()
        => GetDescendingPrimeCandidates(n).AsParallel().AsOrdered().Where(IsPrimeNumber);

      /// <summary>
      /// <para>Determines if the number is a prime candidate. If not, it's definitely a composite.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="n"></param>
      /// <returns></returns>
      public bool IsPrimeCandidate()
        => TInteger.CreateChecked(6) is var six && n < six ? int.CreateChecked(n) is 2 or 3 or 5 : int.CreateChecked(n % six) is 1 or 5;

      #endregion

      /// <summary>
      /// <para>Returns the Catalan number for the specified <paramref name="number"/>.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Catalan_number"/></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="number"></param>
      /// <returns></returns>
      public TInteger GetCatalanNumber()
        => (n + n).Factorial() / ((n + TInteger.One).Factorial() * n.Factorial());

      //#region Centered Polygonal Numbers

      ///// <summary>
      ///// <para>Creates a new sequence of </para>
      ///// <para><see href="https://en.wikipedia.org/wiki/Centered_polygonal_number"/></para>
      ///// </summary>
      ///// <remarks>This function runs indefinitely, if allowed.</remarks>
      ///// <typeparam name="TSelf"></typeparam>
      ///// <param name="numberOfSides"></param>
      ///// <returns></returns>
      //public System.Collections.Generic.IEnumerable<(TInteger minCenteredNumber, TInteger maxCenteredNumber, TInteger count)> GetCenteredPolygonalLayers()
      //{
      //  yield return (TInteger.One, TInteger.One, TInteger.One);

      //  foreach (var v in GetCenteredPolygonalNumberSequence(n).PartitionTuple2(false, (min, max, index) => (min, max)))
      //    yield return (v.min + TInteger.One, v.max, v.max - v.min);
      //}

      ///// <summary></summary>
      ///// <see href="https://en.wikipedia.org/wiki/Centered_polygonal_number"/>
      //public TInteger GetCenteredPolygonalNumber(TInteger index)
      //{
      //  System.ArgumentOutOfRangeException.ThrowIfNegative(index);
      //  System.ArgumentOutOfRangeException.ThrowIfLessThan(n, TInteger.CreateChecked(3));

      //  var two = TInteger.CreateChecked(2);

      //  return (n * index * index + n * index + two) / two;
      //}

      ///// <summary></summary>
      ///// <see href="https://en.wikipedia.org/wiki/Centered_polygonal_number"/>
      ///// <remarks>This function runs indefinitely, if allowed.</remarks>
      //public System.Collections.Generic.IEnumerable<TInteger> GetCenteredPolygonalNumberSequence()
      //  => TInteger.Zero.LoopVerge(TInteger.One).Select(i => GetCenteredPolygonalNumber(n, i));

      //#endregion

      /// <summary>
      /// <para>Computes the Mersenne number for the specified <paramref name="value"/>.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Mersenne_number"/></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="value"></param>
      /// <returns></returns>
      public TInteger GetMersenneNumber()
        => checked((TInteger.One << int.CreateChecked(n)) - TInteger.One);

      /// <summary>Creates a sequence of Moser/DeBruijn numbers.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Moser%E2%80%93De_Bruijn_sequence"/>
      /// <seealso cref="https://www.geeksforgeeks.org/moser-de-bruijn-sequence/"/>
      public System.Collections.Generic.List<TInteger> GetMoserDeBruijnSequence()
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

      /// <summary>
      /// <para>Creates a sequence of powers-of-radix values.</para>
      /// </summary>
      /// <typeparam name="TSelf"></typeparam>
      /// <param name="radix"></param>
      /// <returns></returns>
      public System.Collections.Generic.IEnumerable<TInteger> GetPowerSequence()
      {
        var power = TInteger.One;

        while (true)
        {
          yield return power;

          try { checked { power *= n; } }
          catch { break; }
        }
      }

      /// <summary>
      /// <para>Creates a sequence of powers-of-radix values.</para>
      /// </summary>
      /// <typeparam name="TMinMaxInteger"></typeparam>
      /// <param name="nth"></param>
      /// <returns></returns>
      public System.Collections.Generic.IEnumerable<(TInteger Root, TInteger Number)> GetSequenceOfRootN()
      {
        System.ArgumentOutOfRangeException.ThrowIfLessThan(n, TInteger.CreateChecked(2));

        checked
        {
          for (var root = System.Numerics.BigInteger.Zero; ; root++)
            yield return (TInteger.CreateChecked(root), TInteger.CreateChecked(System.Numerics.BigInteger.Pow(System.Numerics.BigInteger.CreateChecked(root), int.CreateChecked(n))));
        }
      }

      /// <summary>
      /// <para>This is a fast building sieve of Eratosthenes.</para>
      /// </summary>
      /// <param name="limit">The max number of the sieve.</param>
      /// <returns></returns>
      /// <remarks>In .NET there is currently a maximum index limit for an array: 2,146,435,071 (0X7FEFFFFF). That number times 64 (137,371,844,544) is the practical limit of <paramref name="limit"/>.</remarks>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public DataStructures.BitArray64 GetSieveOfEratosthenes()
      {
        var limit = long.CreateChecked(n);

        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(limit);
        System.ArgumentOutOfRangeException.ThrowIfGreaterThan(limit, 64L * System.Array.MaxIndexArrayOfMultiByteStructures);

        var ba = new Flux.DataStructures.BitArray64(limit + 1, unchecked((long)0xAAAAAAAAAAAAAAAAUL)); // Bits represents the number line, so we start with all odd numbers being set to 1 and all even numbers set to 0.

        ba.Set(1, false); // One is not a prime so we set it to 0.
        ba.Set(2, true); // Two is the only even and the oddest prime so we set it to 1.

        var factor = 3L;

        while (factor * factor <= limit)
        {
          for (var i = factor; i <= limit; i += 2)
            if (ba.Get(i))
            {
              factor = i;
              break;
            }

          for (var i = factor * factor; i <= limit; i += factor * 2)
            ba.Set(i, false);

          factor += 2;
        }

        return ba;
      }

      /// <summary>
      /// <para>Yields a sequence of all sphenic numbers less than <paramref name="maxValue"/>.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Sphenic_number"/></para>
      /// </summary>
      /// <typeparam name="TSelf"></typeparam>
      /// <param name="maxValue"></param>
      /// <returns></returns>
      public System.Collections.Generic.IEnumerable<TInteger> GetSphenicNumbers(TInteger maxValue)
      {
        checked
        {
          for (var i = TInteger.CreateChecked(30); i < maxValue; i++)
          {
            var count = 0;

            var k = i;

            for (var j = TInteger.CreateChecked(2); k > TInteger.One && count <= 2; j++)
            {
              if (TInteger.IsZero(k % j))
              {
                k /= j;

                if (TInteger.IsZero(k % j))
                  break;

                count++;
              }

              if (count == 0 && j > maxValue / (j * j))
                break;

              if (count == 1 && j > (k / j))
                break;
            }

            if (count == 3 && k == TInteger.One)
              yield return i;
          }
        }
      }

      /// <summary>
      /// <para>Creates a new Van Eck's sequence, starting with the specified <paramref name="number"/> (where 0 yields the original sequence).</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Van_Eck%27s_sequence"/></para>
      /// </summary>
      /// <remarks>This function runs indefinitely, if allowed.</remarks>
      /// <typeparam name="TSelf"></typeparam>
      /// <param name="number"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public System.Collections.Generic.IEnumerable<TInteger> GetVanEckSequence()
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(n);

        var lasts = new System.Collections.Generic.Dictionary<TInteger, TInteger>();
        var last = n;

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

      /// <summary>
      /// <para>Determines whether the <paramref name="number"/> is an abundant number.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Abundant_number"/></para>
      /// </summary>
      /// <typeparam name="TSelf"></typeparam>
      /// <param name="number"></param>
      /// <returns></returns>
      public bool IsAbundantNumber()
        => n.SumDivisors().AliquotSum > n;

      /// <summary>
      /// <para>Determines if the <paramref name="number"/> is a composite number.</para>
      /// </summary>
      /// <typeparam name="TSelf"></typeparam>
      /// <param name="number"></param>
      /// <returns></returns>
      public bool IsComposite()
      {
        var three = TInteger.CreateChecked(3); // Used twice.

        if (n <= three)
          return false;

        var two = TInteger.CreateChecked(2); // Used twice.

        if (TInteger.IsZero(n % two) || TInteger.IsZero(n % three))
          return true;

        var six = TInteger.CreateChecked(6); // Used in the for loop below.

        for (var k = TInteger.CreateChecked(5); k * k <= n; k += six)
          if (TInteger.IsZero(n % k) || TInteger.IsZero(n % (k + two)))
            return true;

        return false;
      }

      /// <summary>
      /// <para>Determines whether the <paramref name="number"/> is a Fibonacci number.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Fibonacci_number"/></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="number"></param>
      /// <returns></returns>
      public bool IsFibonacciNumber()
      {
        var four = TInteger.CreateChecked(4);

        var fivens = TInteger.CreateChecked(5) * n * n;
        var fp4 = fivens + four;
        var fp4sr = fp4.IntegerSqrt();
        var fm4 = fivens - four;
        var fm4sr = fm4.IntegerSqrt();

        return fp4sr * fp4sr == fp4 || fm4sr * fm4sr == fm4;
      }

      /// <summary>
      /// <para>Indicates whether a <paramref name="value"/> is a prime.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Primality_test"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Prime_number"/></para>
      /// </summary>
      public bool IsPrimeNumber()
      {
        var two = TInteger.CreateChecked(2);
        var three = TInteger.CreateChecked(3);

        if (n <= three)
          return n >= two;

        if (TInteger.IsZero(n % two) || TInteger.IsZero(n % three))
          return false;

        var six = TInteger.CreateChecked(6);

        for (var k = TInteger.CreateChecked(5); k * k <= n; k += six)
          if (TInteger.IsZero(n % k) || TInteger.IsZero(n % (k + two)))
            return false;

        return true;
      }
    }

    #region DeBruijn sequence

    /// <summary>
    /// <para>Returns the total length of the DeBruijn sequence.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/De_Bruijn_sequence"/></para>
    /// <para><seealso href="https://www.rosettacode.org/wiki/De_Bruijn_sequences"/></para>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="k"></param>
    /// <param name="n"></param>
    /// <returns></returns>
    /// <remarks>The formula for the length is <c>(<paramref name="k"/> * <paramref name="n"/> + <paramref name="n"/> - 1)</c>.</remarks>
    public static int GetDeBruijnSequenceLength(int k, int n)
    {
      System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(k);
      System.ArgumentOutOfRangeException.ThrowIfNegative(n);

      return (int)System.Numerics.BigInteger.Pow(k, n) + n - 1;
    }

    /// <summary>
    /// <para>Creates a new DeBruijn sequence with DeBruijn numbers, which are the indices in a <paramref name="k"/> alphabet (e.g. 10 digit number pad) of <paramref name="n"/> size (e.g. 4 digit codes).</para>
    /// <para>The indices can be translated into symbols using an "alphabet".</para>
    /// <para><see href="https://en.wikipedia.org/wiki/De_Bruijn_sequence"/></para>
    /// <para><seealso href="https://www.rosettacode.org/wiki/De_Bruijn_sequences"/></para>
    /// </summary>
    /// <param name="k"></param>
    /// <param name="n"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static System.Collections.Generic.List<int> GetDeBruijnSequence(int k, int n)
    {
      var sequence = new System.Collections.Generic.List<int>(GetDeBruijnSequenceLength(k, n));

      var a = new int[k * n];

      DeBruijn(1, 1);

      sequence.AddRange(sequence.GetRange(0, n - 1));

      return sequence;

      void DeBruijn(int t, int p)
      {
        if (t > n)
        {
          if ((n % p) == 0)
            sequence.AddRange(new System.ArraySegment<int>(a, 1, p));
        }
        else
        {
          a[t] = a[t - p];
          DeBruijn(t + 1, p);
          var j = a[t - p] + 1;

          while (j < k)
          {
            a[t] = j;
            DeBruijn(t + 1, t);
            j++;
          }
        }
      }
    }

    /// <summary>
    /// <para>Creates a new expanded DeBruijn sequence of indices based on <paramref name="k"/> and <paramref name="n"/>.</para>
    /// </summary>
    /// <param name="k"></param>
    /// <param name="n"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<int[]> GetDeBruijnSequenceExpanded(int k, int n)
      => GetDeBruijnSequence(k, n).PartitionNgram(n, (e, i) => e.ToArray());

    /// <summary>
    /// <para>Creates a new expanded DeBruijn sequence of symbols based on <paramref name="k"/>, <paramref name="n"/> and <paramref name="alphabet"/>.</para>
    /// </summary>
    /// <typeparam name="TSymbol"></typeparam>
    /// <param name="k"></param>
    /// <param name="n"></param>
    /// <param name="alphabet"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<TSymbol>> GetDeBruijnSequenceExpandedSymbols<TSymbol>(int k, int n, params TSymbol[] alphabet)
      => GetDeBruijnSequence(k, n).PartitionNgram(n, (e, i) => e.Select(i => alphabet[i]).ToList());

    #endregion

    #region Leonardo sequence

    /// <summary>
    /// <para>Creates a new sequence with Leonardo numbers.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Leonardo_number"/></para>
    /// </summary>
    /// <remarks>This function runs indefinitely, if allowed.</remarks>
    /// <typeparam name="TSelf"></typeparam>
    /// <param name="first"></param>
    /// <param name="second"></param>
    /// <param name="step"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<TSelf> GetLeonardoSequence<TSelf>(TSelf first, TSelf second, TSelf step)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      while (true)
      {
        yield return first;

        checked { (first, second) = (second, first + second + step); }
      }
    }

    #endregion

    #region Arithmetic progression

    /// <summary>
    /// <para>Creates a new sequence of non-zero numbers where each term after the first <paramref name="firstTerm"/> is found by multiplying the previous one by a fixed, non-zero number called the <paramref name="d"/>.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Geometric_progression"/></para>
    /// </summary>
    /// <remarks>This function runs indefinitely, if allowed.</remarks>
    /// <typeparam name="TFloat"></typeparam>
    /// <param name="a"></param>
    /// <param name="d"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static System.Collections.Generic.IEnumerable<TNumber> GetArithmeticSequence<TNumber>(this TNumber a, TNumber d)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      System.ArgumentOutOfRangeException.ThrowIfZero(a);
      System.ArgumentOutOfRangeException.ThrowIfZero(d);

      for (var n = 0; true; n++) // We can start at zero..
        yield return checked(a + TNumber.CreateChecked(n) * d); // ..and get away with NOT subtracting one from n: (a + n * d)
    }

    public static double ArithmeticMeanOfTwoTerms<TInteger>(this TInteger a, TInteger b)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => double.CreateChecked(a + b) / 2;

    /// <summary>
    /// <para>Get the <paramref name="nth"/> term of a geometric sequence with the specified <paramref name="commonRatio"/>.</para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="a"></param>
    /// <param name="d"></param>
    /// <param name="n"></param>
    /// <returns></returns>
    public static TNumber ArithmeticSequenceNthTerm<TNumber, TInteger>(this TNumber a, TNumber d, TInteger n)
      where TNumber : System.Numerics.INumber<TNumber>
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => a + TNumber.CreateChecked(n - TInteger.One) * d; // (a + (n - 1) * d)

    /// <summary>
    /// <para>Gets the geometric series (sum) of a geometric sequence with infinite terms and the specified <paramref name="d"/>.</para>
    /// </summary>
    /// <param name="d">The common ratio of the geometric sequence.</param>
    /// <returns></returns>
    public static TNumber ArithmeticSeriesMeanOfNthTerms<TNumber, TInteger>(this TNumber a, TNumber d, TInteger n)
      where TNumber : System.Numerics.INumber<TNumber>
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => (a + ArithmeticSequenceNthTerm(a, d, n)) / TNumber.CreateChecked(2);

    ///// <summary>
    ///// <para>Gets the geometric series (sum) of a geometric sequence with infinite terms and the specified <paramref name="d"/>.</para>
    ///// </summary>
    ///// <param name="d">The common ratio of the geometric sequence.</param>
    ///// <returns></returns>
    //public static TNumber ArithmeticSeriesOfInfiniteTerms<TNumber>(this TNumber a, TNumber d)
    //  where TNumber : System.Numerics.INumber<TNumber>
    //  => ()

    /// <summary>
    /// <para>Gets the geometric series (sum) of a geometric sequence with <paramref name="n"/> terms and the specified <paramref name="d"/>.</para>
    /// </summary>
    /// <param name="d">The common ratio of the geometric sequence.</param>
    /// <param name="n">The term of which to find the sum up until.</param>
    /// <returns></returns>
    public static TNumber ArithmeticSeriesOfNthTerms<TNumber, TInteger>(this TNumber a, TNumber d, TInteger n)
      where TNumber : System.Numerics.INumber<TNumber>
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => TNumber.CreateChecked(n) * (a + ArithmeticSequenceNthTerm(a, d, n)) / TNumber.CreateChecked(2);

    #endregion

    #region Geometric progression

    /// <summary>
    /// <para>Creates a new sequence of non-zero numbers where each term after the first <paramref name="firstTerm"/> is found by multiplying the previous one by a fixed, non-zero number called the <paramref name="commonRatio"/>.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Geometric_progression"/></para>
    /// </summary>
    /// <remarks>This function runs indefinitely, if allowed.</remarks>
    /// <typeparam name="TFloat"></typeparam>
    /// <param name="firstTerm"></param>
    /// <param name="commonRatio"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static System.Collections.Generic.IEnumerable<TNumber> GetGeometricSequence<TNumber>(this TNumber a1, TNumber commonRatio)
      where TNumber : System.Numerics.INumber<TNumber>
    {
      System.ArgumentOutOfRangeException.ThrowIfZero(a1);
      System.ArgumentOutOfRangeException.ThrowIfZero(commonRatio);

      while (true)
      {
        yield return a1;

        try
        {
          checked { a1 *= commonRatio; }
        }
        catch { break; }
      }
    }

    public static double GeometricMeanOfTwoTerms<TInteger>(this TInteger a, TInteger b)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => double.Sqrt(double.CreateChecked(a * b));

    /// <summary>
    /// <para>Get the <paramref name="nth"/> term of a geometric sequence with the specified <paramref name="commonRatio"/>.</para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    /// <param name="commonRatio"></param>
    /// <param name="nth"></param>
    /// <returns></returns>
    public static TNumber GeometricSequenceNthTerm<TNumber, TInteger>(this TNumber a1, TNumber commonRatio, TInteger nth)
      where TNumber : System.Numerics.INumber<TNumber>, System.Numerics.IPowerFunctions<TNumber>
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => a1 * TNumber.Pow(commonRatio, TNumber.CreateChecked(nth - TInteger.One));

    /// <summary>
    /// <para>Gets the geometric series (sum) of a geometric sequence with infinite terms and the specified <paramref name="commonRatio"/>.</para>
    /// </summary>
    /// <param name="commonRatio">The common ratio of the geometric sequence.</param>
    /// <returns></returns>
    public static TNumber GeometricSeriesOfInfiniteTerms<TNumber>(this TNumber a1, TNumber commonRatio)
      where TNumber : System.Numerics.INumber<TNumber>
      => commonRatio < TNumber.One
      ? a1 / (TNumber.One - commonRatio)
      : throw new System.ArithmeticException();

    /// <summary>
    /// <para>Gets the geometric series (sum) of a geometric sequence with <paramref name="nth"/> terms and the specified <paramref name="commonRatio"/>.</para>
    /// </summary>
    /// <param name="commonRatio">The common ratio of the geometric sequence.</param>
    /// <param name="nth">The term of which to find the sum up until.</param>
    /// <returns></returns>
    public static TNumber GeometricSeriesOfNthTerms<TNumber, TInteger>(this TNumber a1, TNumber commonRatio, TInteger nth)
      where TNumber : System.Numerics.INumber<TNumber>, System.Numerics.IPowerFunctions<TNumber>
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => a1 * (TNumber.One - TNumber.Pow(commonRatio, TNumber.CreateChecked(nth))) / (TNumber.One - commonRatio);

    #endregion

    #region Harmonic progression

    public static System.Collections.Generic.IEnumerable<TFloat> GetHarmonicSequence<TFloat>(this TFloat a, TFloat d)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
      => GetArithmeticSequence(a, d).Select(an => TFloat.One / an);

    public static double HarmonicMeanOfTwoTerms<TInteger>(this TInteger a, TInteger b)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => double.CreateChecked(TInteger.CreateChecked(2) * a * b) / double.CreateChecked(a + b);

    public static double HarmonicMeanOfThreeTerms<TInteger>(this TInteger a, TInteger b, TInteger c)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => double.CreateChecked(TInteger.CreateChecked(3) * a * b * c) / double.CreateChecked(a * b + b * c + c * a);

    public static TFloat HarmonicSequenceNthTerm<TFloat, TInteger>(this TFloat a, TFloat d, TInteger n)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
      => TFloat.One / (a + (TFloat.CreateChecked(n) - TFloat.One) * d);

    /// <summary>
    /// <para>Gets the harmonic series (sum) of a geometric sequence with <paramref name="nth"/> terms and the specified <paramref name="commonRatio"/>.</para>
    /// </summary>
    /// <param name="commonRatio">The common ratio of the geometric sequence.</param>
    /// <param name="nth">The term of which to find the sum up until.</param>
    /// <returns></returns>
    public static TFloat HarmonicSeriesOfNthTerms<TFloat, TInteger>(this TFloat a, TFloat d, TInteger n)
      where TFloat : System.Numerics.IFloatingPoint<TFloat>, System.Numerics.ILogarithmicFunctions<TFloat>
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      var two = TFloat.CreateChecked(2);

      return TFloat.One / d * TFloat.Log((two * a + (two * TFloat.CreateChecked(n) - TFloat.One) * d) / (two * a - d));
    }

    #endregion

    #region Gaps in sequence

    public static System.Collections.Generic.IEnumerable<TNumber> GetGapsInSequence<TNumber>(this System.Collections.Generic.IEnumerable<TNumber> source, bool includeLastFirstGap)
      where TNumber : System.Numerics.INumber<TNumber>
      => source.PartitionTuple2(includeLastFirstGap, (leading, trailing, index) => trailing - leading);

    #endregion
  }
}
