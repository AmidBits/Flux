namespace Flux
{
  public static partial class BinaryInteger
  {
    extension<TInteger>(TInteger)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      #region Abundant number functions

      #region AbundantNumbers

      /// <summary>
      /// <para>Creates a new sequence of 2-tuples, each with an abundant number and its aliquot sum.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Abundant_number"/></para>
      /// </summary>
      /// <remarks>This function generate results until the type <typeparamref name="TInteger"/> under/overflows in any calculation. No exception is thrown.</remarks>
      /// <typeparam name="TSelf"></typeparam>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<(TInteger Number, TInteger AliqoutSum)> AbundantNumbers()
        => Number.ArithmeticSequence(TInteger.CreateChecked(3), TInteger.One).AsParallel().AsOrdered().Select(n => (Number: n, DivisorSum(n).AliquotSum)).Where(x => x.AliquotSum > x.Number);

      #endregion

      #region HighlyAbundantNumbers

      /// <summary>
      /// <para>Creates a new sequence of highly abundant numbers.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Highly_abundant_number"/></para>
      /// </summary>
      /// <remarks>
      /// <para>Not all highly abundant numbers are abundant numbers.</para>
      /// <para>This function generate results until the type <typeparamref name="TInteger"/> under/overflows in any calculation. No exception is thrown.</para>
      /// </remarks>
      /// <typeparam name="TSelf"></typeparam>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<(TInteger Number, TInteger Sum)> HighlyAbundantNumbers()
      {
        var largestSumOfDivisors = TInteger.Zero;

        foreach (var index in Number.ArithmeticSequence(TInteger.One, TInteger.One))
          if (DivisorSum(index).Sum is var sumOfDivisors && sumOfDivisors > largestSumOfDivisors)
          {
            yield return (index, sumOfDivisors);

            largestSumOfDivisors = sumOfDivisors;
          }
      }

      #endregion

      #region IsAbundantNumber

      /// <summary>
      /// <para>Determines whether the <paramref name="number"/> is an abundant number.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Abundant_number"/></para>
      /// </summary>
      /// <typeparam name="TSelf"></typeparam>
      /// <param name="number"></param>
      /// <returns></returns>
      public static bool IsAbundantNumber(TInteger n)
        => DivisorSum(n).AliquotSum > n;

      #endregion

      #region SuperAbundantNumbers

      /// <summary>
      /// <para>Creates a new sequence of super-abundant numbers.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Superabundant_number"/></para>
      /// </summary>
      /// <remarks>
      /// <para>All superabundant numbers are highly abundant.</para>
      /// <para>This function generate results until the type <typeparamref name="TInteger"/> under/overflows in any calculation. No exception is thrown.</para>
      /// </remarks>
      /// <typeparam name="TSelf"></typeparam>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<(TInteger Number, TInteger Sum)> SuperAbundantNumbers()
      {
        var largestValue = 0.0;

        foreach (var tuple in HighlyAbundantNumbers<TInteger>())
          if ((double.CreateChecked(tuple.Sum) / double.CreateChecked(tuple.Number)) is var value && value > largestValue)
          {
            yield return tuple;

            largestValue = value;
          }
      }

      #endregion

      #endregion

      #region Centered polygonal number functions

      /// <summary>
      /// <para>Creates a new sequence of </para>
      /// <para><see href="https://en.wikipedia.org/wiki/Centered_polygonal_number"/></para>
      /// </summary>
      /// <remarks>This function runs indefinitely, if allowed.</remarks>
      /// <typeparam name="TSelf"></typeparam>
      /// <param name="numberOfSides"></param>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<(TInteger LayerCount, TInteger CenterPolygonalNumber)> GenerateCenteredPolygonalLayers(TInteger k)
        => GenerateCenteredPolygonalNumberSequence(k).PartitionTuple2(false, (previous, current, index) => (TInteger.CreateChecked(index + 2), current)).Prepend((TInteger.One, TInteger.One));
      //{
      //  yield return (TInteger.One, TInteger.One);

      //  foreach (var v in GetCenteredPolygonalNumberSequence(k).PartitionTuple2(false, (previous, current, index) => (previous, current, index)))
      //    yield return (TInteger.CreateChecked(v.index + 2), v.current);
      //}

      /// <summary></summary>
      /// <see href="https://en.wikipedia.org/wiki/Centered_polygonal_number"/>
      public static TInteger CenteredPolygonalNumber(TInteger k, TInteger n)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(n);
        System.ArgumentOutOfRangeException.ThrowIfLessThan(k, TInteger.CreateChecked(3));

        return checked(k * n * (n + TInteger.One) / TInteger.CreateChecked(2) + TInteger.One);
      }

      /// <summary></summary>
      /// <see href="https://en.wikipedia.org/wiki/Centered_polygonal_number"/>
      /// <remarks>This function runs indefinitely, if allowed.</remarks>
      public static System.Collections.Generic.IEnumerable<TInteger> GenerateCenteredPolygonalNumberSequence(TInteger k)
        => Number.ArithmeticSequence(TInteger.Zero, TInteger.One).Select(n => CenteredPolygonalNumber(k, n));

      #endregion

      #region Composite number functions

      #region CompositeNumbers

      /// <summary>
      /// <para>Generates a new sequence of composite numbers.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Composite_number"/></para>
      /// </summary>
      /// <remarks>This function generate results until the type <typeparamref name="TInteger"/> under/overflows in any calculation. No exception is thrown.</remarks>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<TInteger> CompositeNumbers()
        => Number.ArithmeticSequence(TInteger.One, TInteger.One).AsParallel().AsOrdered().Where(IsCompositeNumber);

      #endregion

      #region HighlyCompositeNumbers

      /// <summary>
      /// <para>Creates a new sequence of highly composite numbers.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Highly_composite_number"/></para>
      /// </summary>
      /// <remarks>This function generate results until the type <typeparamref name="TInteger"/> under/overflows in any calculation. No exception is thrown.</remarks>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<(TInteger Number, TInteger Count)> HighlyCompositeNumbers()
      {
        var largestCount = TInteger.Zero;

        foreach (var tuple in Number.ArithmeticSequence(TInteger.One, TInteger.One).AsParallel().AsOrdered().Select(n => (Number: n, Count: DivisorCount(n))))
          if (tuple.Count > largestCount)
          {
            yield return tuple;

            largestCount = tuple.Count;
          }
      }

      #endregion

      #region IsCompositeNumber

      /// <summary>
      /// <para>Determines if the <paramref name="number"/> is a composite number, i.e. not a prime and not a unit (1).</para>
      /// </summary>
      /// <param name="n"></param>
      /// <returns></returns>
      public static bool IsCompositeNumber(TInteger n)
        => !IsPrimeNumber(n) // If it's not a prime..
        && n > TInteger.One; // ..and not a unit (1).

      #endregion

      #region IsHighlyCompositeNumber

      /// <summary>
      /// <para>Determines if the <paramref name="number"/> is a highly composite number.</para>
      /// </summary>
      /// <param name="n"></param>
      /// <returns></returns>
      public static bool IsHighlyCompositeNumber(TInteger n)
      {
        var ncd = DivisorCount(n); // Count number of divisors of N

        for (var i = n - TInteger.One; i >= TInteger.One; i--) // Loop to count number of factors of every number less than n.
        {
          var icd = DivisorCount(i);

          if (icd >= ncd) // If any number less than n has more factors than n, then return false.
            return false;
        }

        return true;
      }

      #endregion

      #endregion

      #region Divisor functions

      #region DivisorCount

      /// <summary>
      /// <para>σ0()</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Divisor"/></para>
      /// <para><see href="https://cp-algorithms.com/algebra/divisors.html"/></para>
      /// </summary>
      public static TInteger DivisorCount(TInteger number)
      {
        var count = TInteger.One;

        for (var i = TInteger.CreateChecked(2); (i * i) <= number; i++)
        {
          if (TInteger.IsZero(number % i))
          {
            var e = TInteger.Zero;

            do
            {
              e++;

              number /= i;
            }
            while (TInteger.IsZero(number % i));

            count *= e + TInteger.One;
          }
        }

        if (number > TInteger.One)
          count <<= 1;

        return count;
      }

      #endregion

      #region DivisorSum

      /// <summary>
      /// <para>σ1()</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Divisor"/></para>
      /// <para><see href="https://cp-algorithms.com/algebra/divisors.html"/></para>
      /// </summary>
      public static (TInteger Sum, TInteger AliquotSum) DivisorSum(TInteger number)
      {
        var sum = TInteger.One;

        var aliquot = number; // Need to remember the original number for calculating the aliquot sum, which is the sum of proper divisors (excluding itself).

        for (var i = TInteger.CreateChecked(2); i * i <= number; i++)
        {
          if (TInteger.IsZero(number % i))
          {
            var e = 0;

            do
            {
              e++;

              number /= i;
            }
            while (TInteger.IsZero(number % i));

            var add = TInteger.Zero;
            var pow = TInteger.One;

            do
            {
              add += pow;
              pow *= i;
            }
            while (e-- > 0);

            sum *= add;
          }
        }

        if (number > TInteger.One)
          sum *= TInteger.One + number;

        return (sum, sum - aliquot);
      }

      #endregion

      #region GetDivisors

      /// <summary>
      /// <para>Creates a new list of divisors of a <paramref name="number"/>.</para>
      /// </summary>
      /// <param name="number"></param>
      /// <param name="sort"></param>
      /// <returns></returns>
      public static System.Collections.Generic.List<TInteger> GetDivisors(TInteger number)
      {
        var divisors = new System.Collections.Generic.List<TInteger>();
        GetDivisors(number, divisors);
        return divisors;
      }

      /// <summary>
      /// <para>Adds the divisors of a <paramref name="number"/> to a <paramref name="collectionOfDivisors"/>.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Divisor"/></para>
      /// </summary>
      /// <remarks>This implementaion does not order the result.</remarks>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="value"></param>
      /// <param name="proper"></param>
      /// <returns></returns>
      public static void GetDivisors(TInteger number, System.Collections.Generic.ICollection<TInteger> collectionOfDivisors)
      {
        if (number > TInteger.Zero)
          for (var i = TInteger.One; (i * i) <= number; i++)
          {
            var (q, r) = TInteger.DivRem(number, i);

            if (TInteger.IsZero(r))
            {
              collectionOfDivisors.Add(i);

              if (q != i)
                collectionOfDivisors.Add(q);
            }
          }
      }

      #endregion

      #region IsDeficientNumber

      /// <summary>Determines whether the <paramref name="number"/> is a deficient number.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Deficient_number"/>
      /// <seealso cref="https://en.wikipedia.org/wiki/Divisor#Further_notions_and_facts"/>
      public static bool IsDeficientNumber(TInteger number)
        => DivisorSum(number).AliquotSum < number;

      #endregion

      #region IsPerfectNumber

      /// <summary>Determines whether the <paramref name="number"/> is a perfect number.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Perfect_number"/>
      public static bool IsPerfectNumber(TInteger number)
        => DivisorSum(number).AliquotSum == number;

      #endregion

      #endregion

      #region EulerTotient

      /// <summary>
      /// <para>In number theory, Euler's totient function counts the positive integers up to a given integer n that are relatively prime to n.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Euler%27s_totient_function"/></para>
      /// </summary>
      /// <param name="n"></param>
      /// <returns></returns>
      public static TInteger EulerTotient(TInteger n)
      {
        var result = n;

        for (var p = TInteger.CreateChecked(2); p * p <= n; p++)
          if (TInteger.IsZero(n % p)) // Check if p is a prime factor.
          {
            while (TInteger.IsZero(n % p)) // If yes, then update n and result
              n /= p;

            result -= result / p;
          }

        if (n > TInteger.One) // If n has a prime factor greater than sqrt(n). (There can be at-most one such prime factor.)
          result -= result / n;

        return result;
      }

      #endregion

      #region Greatest Common Divisor (GCD) functions

      /// <summary>
      /// <para>The greatest common divisor (GCD) of two or more integers, which are not all zero, is the largest positive integer that divides each of the integers. This implementation is the binary GCD algorithm.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Greatest_common_divisor"/></para>
      /// <para><see href="https://en.wikipedia.org/wiki/Binary_GCD_algorithm"/></para>
      /// </summary>
      /// <param name="a"></param>
      /// <param name="b"></param>
      /// <returns></returns>
      public static TInteger Gcd(TInteger a, TInteger b)
      {
        var bl = int.Max(a.GetBitLength(), b.GetBitLength());

        if (bl <= 128)
          return GcdBinary(a, b);
        else if (bl <= 512)
          return GcdEuclid(a, b);
        else
          return GcdLehmer(a, b);
      }

      private static TInteger GcdBinary(TInteger a, TInteger b)
      {
        a = TInteger.Abs(a);
        b = TInteger.Abs(b);

        if (TInteger.IsZero(a))
          return b;

        if (TInteger.IsZero(b))
          return a;

        var i = int.CreateChecked(TInteger.TrailingZeroCount(a));
        a >>= i;

        var j = int.CreateChecked(TInteger.TrailingZeroCount(b));
        b >>= j;

        var k = int.Min(i, j);

        while (true)
        {
          if (a > b)
            (a, b) = (b, a);

          b -= a;

          if (TInteger.IsZero(b))
            return a << k;

          b >>>= int.CreateChecked(TInteger.TrailingZeroCount(b));
        }
      }

      private static TInteger GcdEuclid(TInteger a, TInteger b)
      {
        while (!TInteger.IsZero(b))
          (a, b) = (b, a % b);

        return TInteger.Abs(a);
      }

      private static TInteger GcdLehmer(TInteger a, TInteger b)
      {
        a = TInteger.Abs(a);
        b = TInteger.Abs(b);

        if (TInteger.IsZero(a)) return b;
        if (TInteger.IsZero(b)) return a;

        if (b > a)
          (a, b) = (b, a);

        while (b > TInteger.Zero)
        {
          var shift = int.Max(a.GetBitLength(), b.GetBitLength()) - 64;

          var aHigh = a >> shift;
          var bHigh = b >> shift;

          TInteger A = TInteger.One, B = TInteger.Zero, C = TInteger.Zero, D = TInteger.One;

          while (true)
          {
            if (TInteger.IsZero(bHigh + C) || TInteger.IsZero(bHigh + D))
              break;

            var q = ((aHigh + A) / (bHigh + C));
            var q2 = ((aHigh + B) / (bHigh + D));

            if (q != q2)
              break;

            (A, B, C, D) = (C, D, A - q * C, B - q * D);

            (aHigh, bHigh) = (bHigh, aHigh - q * bHigh);
          }

          (a, b) = TInteger.IsZero(B)
            ? (b, a % b) // Single Euclid step.
            : (A * a + B * b, C * a + D * b); // Apply transform.

          if (TInteger.IsNegative(b))
            b = -b; // Ensure positive
        }

        return a;
      }

      /// <summary>
      /// <para>The extended Euclidean algorithm is an extension to the Euclidean algorithm, and computes, in addition to the greatest common divisor (gcd) of integers a and b, also the coefficients of Bézout's identity, which are integers x and y such that "<c>ax + by = gcd(a, b)</c>".</para>
      /// </summary>
      /// <param name="a"></param>
      /// <param name="b"></param>
      /// <param name="x"></param>
      /// <param name="y"></param>
      /// <returns></returns>
      public static TInteger GcdExt(TInteger a, TInteger b, out TInteger x, out TInteger y)
      {
        if (int.Max(a.GetBitLength(), b.GetBitLength()) < 512)
          return GcdExtEuclid(a, b, out x, out y);

        return GcdExtLehmer(a, b, out x, out y);
      }

      /// <summary>
      /// <para>The extended Euclidean algorithm is an extension to the Euclidean algorithm, and computes, in addition to the greatest common divisor (gcd) of integers a and b, also the coefficients of Bézout's identity, which are integers x and y such that "<c>ax + by = gcd(a, b)</c>".</para>
      /// <para>This implementation is for small integers (≤ 512 bits) using the classical extended Euclidean algorithm.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Extended_Euclidean_algorithm"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/B%C3%A9zout%27s_identity"/></para>
      /// </summary>
      /// <remarks>When a and b are coprime (i.e. GCD equals 1), x is the modular multiplicative inverse of a modulo b, and y is the modular multiplicative inverse of b modulo a.</remarks>
      /// <param name="a"></param>
      /// <param name="b"></param>
      /// <param name="x"></param>
      /// <param name="y"></param>
      /// <returns></returns>
      private static TInteger GcdExtEuclid(TInteger a, TInteger b, out TInteger x, out TInteger y)
      {
        var (x0, y0, x1, y1) = (TInteger.One, TInteger.Zero, TInteger.Zero, TInteger.One); // Bézout coefficients.

        while (!TInteger.IsZero(b))
        {
          var q = a / b;

          (a, b) = (b, a - q * b);

          (x0, y0, x1, y1) = (x1, y1, x0 - q * x1, y0 - q * y1);
        }

        (x, y) = (x0, y0);

        return a;
      }

      /// <summary>
      /// <para>The extended Euclidean algorithm is an extension to the Euclidean algorithm, and computes, in addition to the greatest common divisor (gcd) of integers a and b, also the coefficients of Bézout's identity, which are integers x and y such that "<c>ax + by = gcd(a, b)</c>".</para>
      /// <para>This implementation is for large integers (> 512 bits) using the Lehmer's algorithm.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Extended_Euclidean_algorithm"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/B%C3%A9zout%27s_identity"/></para>
      /// </summary>
      /// <param name="a"></param>
      /// <param name="b"></param>
      /// <param name="x"></param>
      /// <param name="y"></param>
      /// <returns></returns>
      private static TInteger GcdExtLehmer(TInteger a, TInteger b, out TInteger x, out TInteger y)
      {
        if (TInteger.IsNegative(a)) a = -a;
        if (TInteger.IsNegative(b)) b = -b;

        var (x0, y0, x1, y1) = (TInteger.One, TInteger.Zero, TInteger.Zero, TInteger.One); // Bézout coefficients.

        while (!TInteger.IsZero(b))
        {
          if (a.GetBitLength() < 64 || b.GetBitLength() < 64) // If numbers are small enough, fall back to classical extended Euclid.
          {
            var q = a / b;

            (a, b) = (b, a - q * b);

            (x0, y0, x1, y1) = (x1, y1, x0 - q * x1, y0 - q * y1);

            continue;
          }

          var shift = int.Max(a.GetBitLength(), b.GetBitLength()) - 64; // Lehmer step: use high words only.

          var (aHigh, bHigh) = (a >> shift, b >> shift);

          var (A, B, C, D) = (TInteger.One, TInteger.Zero, TInteger.Zero, TInteger.One); // Transformation matrix.

          while (true)
          {
            if (TInteger.IsZero(bHigh + C) || TInteger.IsZero(bHigh + D))
              break;

            var q = (aHigh + A) / (bHigh + C);
            var Q = (aHigh + B) / (bHigh + D);

            if (q != Q)
              break;

            (A, C, B, D) = (C, A - q * C, D, B - q * D); // Update transformation matrix.

            (aHigh, bHigh) = (bHigh, aHigh - q * bHigh); // Update high words.
          }

          if (TInteger.IsZero(B)) // Single Euclid step.
          {
            var q = a / b;

            (a, b) = (b, a - q * b);

            (x0, y0, x1, y1) = (x1, y1, x0 - q * x1, y0 - q * y1);
          }
          else
          {
            (a, b) = (A * a + B * b, C * a + D * b); // Apply Lehmer transform.

            (x0, x1, y0, y1) = (A * x0 + B * x1, C * x0 + D * x1, A * y0 + B * y1, C * y0 + D * y1);

            if (TInteger.IsNegative(b))
              b = -b;
          }
        }

        if (TInteger.IsNegative(a)) // Normalize gcd sign.
          (a, x0, y0) = (-a, -x0, -y0);

        (x, y) = (x0, y0);

        return a;
      }

      /// <summary>The same as <see cref="Gcd{TInteger}(TInteger, TInteger)"/> but accepts two or more integers.</summary>
      /// <para><see href="https://en.wikipedia.org/wiki/Greatest_common_divisor"/></para>
      /// <para><see href="https://en.wikipedia.org/wiki/Binary_GCD_algorithm"/></para>
      public static TInteger GreatestCommonDivisor(TInteger a, params TInteger[] other)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(other.Length);

        return Gcd(a, other.Aggregate(Gcd));
      }

      #endregion

      #region IsCoprime

      /// <summary>
      /// <para>In number theory, two integers a and b are coprime, relatively prime or mutually prime if the only positive integer that is a divisor of both of them is 1. Consequently, any prime number that divides a does not divide b, and vice versa. This is equivalent to their greatest common divisor (GCD) being 1. One says also a is prime to b or a is coprime with b.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Coprime_integers"/></para>
      /// </summary>
      /// <param name="a"></param>
      /// <param name="b"></param>
      /// <returns></returns>
      public static bool IsCoprime(TInteger a, TInteger b)
        => Gcd(a, b) == TInteger.One;

      #endregion

      #region Least Common Multiple (LCM) functions

      /// <summary>
      /// <para>In arithmetic and number theory, the least common multiple (LCM) of two integers a and b, usually denoted by lcm(a, b), is the smallest positive integer that is divisible by both a and b. Since division of integers by zero is undefined, this definition has meaning only if a and b are both different from zero. However, some authors define lcm(a, 0) as 0 for all a, since 0 is the only common multiple of a and 0.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Least_common_multiple"/></para>
      /// </summary>
      /// <param name="a"></param>
      /// <param name="b"></param>
      /// <returns></returns>
      public static TInteger Lcm(TInteger a, TInteger b)
        => TInteger.Abs(a / Gcd(a, b) * b);

      /// <summary>The same as <see cref="Lcm{TInteger}(TInteger, TInteger)"/> but accepts two or more integers.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Least_common_multiple"/>
      public static TInteger LeastCommonMultiple(TInteger a, params TInteger[] other)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(other.Length);

        return Lcm(a, other.Aggregate(Lcm));
      }

      #endregion

      #region Mersenne numbers

      #region MersenneNumber

      /// <summary>
      /// <para>Computes the Mersenne number for the specified <paramref name="value"/>.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Mersenne_number"/></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="value"></param>
      /// <returns></returns>
      public static TInteger MersenneNumber(TInteger number)
        => checked((TInteger.One << int.CreateChecked(number)) - TInteger.One);

      #endregion

      #region MersenneNumberSequence

      /// <summary>`
      /// <para>Creates a new sequence of Mersenne numbers.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Mersenne_number"/></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<TInteger> MersenneNumberSequence()
        => Number.ArithmeticSequence(TInteger.One, TInteger.One).Select(MersenneNumber);

      #endregion

      #region MersennePrimeSequence

      /// <summary>
      /// <para>Creates a new sequence of Mersenne primes.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Mersenne_number"/></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<TInteger> MersennePrimeSequence()
        => MersenneNumberSequence<TInteger>().Where(IsPrimeNumber);

      #endregion

      #endregion

      #region MöbiusFunction

      /// <summary>
      /// <para>The Möbius function μ(n) is a multiplicative function in number theory.</para>
      /// </summary>
      /// <param name="n"></param>
      /// <returns></returns>
      public static int MöbiusFunction(TInteger n)
      {
        if (n == TInteger.One)
          return 1;

        var p = TInteger.Zero; // For a prime factor i check if i^2 is also a factor.

        for (var i = TInteger.One; i <= n; i++)
          if (TInteger.IsZero(n % i) && IsPrimeNumber(i))
          {
            if (TInteger.IsZero(n % (i * i))) // Check if n is divisible by i^2
              return 0;
            else // i occurs only once, increase p
              p++;
          }

        return TInteger.IsEvenInteger(p) ? 1 : -1;
      }

      #endregion

      #region Prime number functions

      #region AscendingPrimes

      /// <summary>
      /// <para>Creates a new sequence of ascending possible primes, greater-than-or-equal-to a specified number.</para>
      /// </summary>
      /// <param name="n"></param>
      /// <returns></returns>
      private static System.Collections.Generic.IEnumerable<TInteger> AscendingPrimeCandidates(TInteger n)
      {
        var two = TInteger.CreateChecked(2);
        if (n <= two)
          yield return two;

        var three = TInteger.CreateChecked(3);
        if (n <= three)
          yield return three;

        var six = TInteger.CreateChecked(6);

        foreach (var m in Number.ArithmeticSequence(TInteger.Max(n / six * six, six), six))
        {
          if (checked(m - TInteger.One) is var m1 && m1 >= n)
            yield return m1;
          if (checked(m + TInteger.One) is var p1 && p1 >= n)
            yield return p1;
        }
      }

      /// <summary>
      /// <para>Creates a new sequence ascending prime numbers, greater-than-or-equal-to a specified number.</para>
      /// </summary>
      /// <remarks>This function generate results until the type <typeparamref name="TInteger"/> under/overflows in any calculation. No exception is thrown.</remarks>
      /// <param name="n"></param>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<TInteger> AscendingPrimes(TInteger n)
        => AscendingPrimeCandidates(n).AsParallel().AsOrdered().Where(IsPrimeNumber);

      #endregion

      #region DescendingPrimes

      /// <summary>Creates a new sequence of descending possible primes, less-than-or-equal-to a specified number.</summary>
      private static System.Collections.Generic.IEnumerable<TInteger> DescendingPrimeCandidates(TInteger n)
      {
        var maxPrimeMultiple = BitFoldLeft(TInteger.One);

        if (TypeExtensions.IsNumericsISignedNumber(typeof(TInteger)))
          maxPrimeMultiple >>>= 1;

        var six = TInteger.CreateChecked(6);

        maxPrimeMultiple = maxPrimeMultiple / six * six;

        var primeMultiple = n / six * six;

        if (maxPrimeMultiple - primeMultiple >= six)
          primeMultiple += six;

        for (var pm = primeMultiple; pm >= six; pm -= six)
        {
          if (checked(pm + TInteger.One) is var p1 && p1 <= n)
            yield return p1;
          if (checked(pm - TInteger.One) is var m1 && m1 <= n)
            yield return m1;
        }

        var three = TInteger.CreateChecked(3);
        if (n >= three)
          yield return three;

        var two = TInteger.CreateChecked(2);
        if (n >= two)
          yield return two;
      }

      /// <summary>
      /// <para>Creates a new sequence descending prime numbers, less-than-or-equal-to a specified number.</para>
      /// </summary>
      /// <remarks>This function generate results until the type <typeparamref name="TInteger"/> under/overflows in any calculation. No exception is thrown.</remarks>
      /// <param name="n"></param>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<TInteger> DescendingPrimes(TInteger n)
        => DescendingPrimeCandidates(n).AsParallel().AsOrdered().Where(IsPrimeNumber);

      #endregion

      #region IsPrimeCandidate

      /// <summary>
      /// <para>Determines if the number is a prime candidate. If so, it's possible a prime, and if not, it's definitely a composite.</para>
      /// </summary>
      /// <param name="n"></param>
      /// <returns></returns>
      public static bool IsPrimeCandidate(TInteger n)
      {
        TInteger three = TInteger.CreateChecked(3);

        if (n <= TInteger.One) return false; // 0,1 (and negative numbers, if the type is signed)
        if (n <= three) return true;         // 2,3

        if ((n & TInteger.One) == TInteger.Zero) return false; // even numbers
        if (TInteger.IsZero(n % three)) return false;

        // If already checked % 2 and % 3, then % 6 adds no new eliminations, because:
        // If n % 6 == 0, it’s divisible by 2 or 3
        // If n % 6 == 2, divisible by 2
        // If n % 6 == 3, divisible by 3
        // If n % 6 == 4, divisible by 2
        // Leaving only 1 and 5 as possible remainders for primes greater than 3, which correspond to the 6k ± 1 form.
        // So a % 6 test is logically redundant once you’ve checked % 2 and % 3.

        return true; // candidate for 6k ± 1
      }
      //=> n >= TInteger.CreateChecked(2) && (n <= TInteger.CreateChecked(3) || ());

      #endregion

      #region IsPrimeNumber

      /// <summary>
      /// <para>Indicates whether a number is a prime.</para>
      /// <para>This implementation uses a strategy of different algorithms based on the size of the number.</para>
      /// <list type="number">
      /// <item>A straightforward 6k ± 1 deterministic algorithm for 32-bit numbers.</item>
      /// <item>A Miller-Rabin deterministic algorithm for 64-bit numbers.</item>
      /// <item>A Miller-Rabin probabilistic algorithm for larger numbers.</item>
      /// </list>
      /// <para><see href="https://en.wikipedia.org/wiki/Primality_test"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Prime_number"/></para>
      /// </summary>
      /// <param name="n"></param>
      /// <returns></returns>
      public static bool IsPrimeNumber(TInteger n)
      {
        if (n <= TInteger.One)
          return false; // Numbers smaller than 2 are not prime since 2 is the smallest prime.

        var bi = System.Numerics.BigInteger.CreateChecked(n); // Make it a BigInteger, since it could be huge.

        if (bi <= uint.MaxValue) // If less-or-equal-to an unsigned 32-bit value, use straight 6k ± 1 prime test algorithm.
          return UInt32Extensions.IsPrimeNumberDeterministic(uint.CreateChecked(n));

        if (bi <= ulong.MaxValue) // If less-or-equal-to an unsigned 64-bit value, use Miller-Rabin 64-bit deterministic algorithm.
          return UInt64Extensions.IsPrimeNumberDeterministic(ulong.CreateChecked(n));

        // Otherwise use Miller-Rabin probabilistic algorithm.

        // Log(bit-length, 1.17) yields an approximately 15 iterations @ 10 bits, 30 @ 100, 44 @ 1000, 59 @ 10000, and can be lowered for a higher iteration (k) count.
        // The lower bit-length, the higher count.

        var log = System.Numerics.BigInteger.Log(bi.GetBitLength(), 1.15);

        return BigIntegerExtensions.IsPrimeNumberProbabilistic(bi, int.CreateChecked(log)); // Pass the log value as k parameter.
      }

      #endregion

      #region SieveOfEratosthenes

      /// <summary>
      /// <para>This is a fast building sieve of Eratosthenes.</para>
      /// </summary>
      /// <param name="limit">The max number of the sieve.</param>
      /// <returns></returns>
      /// <remarks>In .NET there is currently a maximum index limit for an array: 2,146,435,071 (0X7FEFFFFF). That number times 64 (137,371,844,544) is the practical limit of <paramref name="limit"/>.</remarks>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public static DataStructures.BitArray64 SieveOfEratosthenes(TInteger n)
      {
        var limit = long.CreateChecked(n);

        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(limit);
        System.ArgumentOutOfRangeException.ThrowIfGreaterThan(limit, 64L * System.Array.MaxArrayIndexOfMultiByteStructures);

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

      #endregion

      #endregion

      #region Prime Omega functions

      #region CountPrimeFactors

      /// <summary>
      /// <para>The number of prime factors that make up a number.</para>
      /// </summary>
      /// <param name="number"></param>
      /// <returns></returns>
      public static (int TotalCount, int DistinctCount) CountPrimeFactors(TInteger number)
      {
        var pf = GetPrimeFactors(number);

        return (pf.Count, pf.Distinct().Count());
      }

      #endregion

      #region GetPrimeFactors

      /// <summary>
      /// <para>Creates a new list of prime factors for a <paramref name="number"/>.</para>
      /// </summary>
      /// <param name="number"></param>
      /// <param name="sort"></param>
      /// <returns></returns>
      public static System.Collections.Generic.List<TInteger> GetPrimeFactors(TInteger number)
      {
        var primeFactors = new System.Collections.Generic.List<TInteger>();
        GetPrimeFactors(number, primeFactors);
        return (primeFactors);
      }

      /// <summary>
      /// <para>Adds the prime factors (a.k.a. divisors) for a <paramref name="number"/> to a <paramref name="collectionOfPrimeFactors"/>.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Factorization"/></para>
      /// <para><see href="https://en.wikipedia.org/wiki/Wheel_factorization"/></para>
      /// <para><see href="https://en.wikipedia.org/wiki/Integer_factorization"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Divisor"/></para>
      /// </summary>
      /// <param name="number"></param>
      /// <param name="collectionOfPrimeFactors"></param>
      public static void GetPrimeFactors(TInteger number, System.Collections.Generic.ICollection<TInteger> collectionOfPrimeFactors)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(number);

        var two = TInteger.CreateChecked(2);
        var four = TInteger.CreateChecked(4);
        var six = TInteger.CreateChecked(6);

        var m_primeFactorWheelIncrements = new TInteger[] { four, two, four, two, four, six, two, six };

        while (TInteger.IsZero(number % two))
        {
          collectionOfPrimeFactors.Add(two);
          number /= two;
        }

        var three = TInteger.CreateChecked(3);

        while (TInteger.IsZero(number % three))
        {
          collectionOfPrimeFactors.Add(three);
          number /= three;
        }

        var five = TInteger.CreateChecked(5);

        while (TInteger.IsZero(number % five))
        {
          collectionOfPrimeFactors.Add(five);
          number /= five;
        }

        TInteger k = TInteger.CreateChecked(7), k2 = k * k;

        var index = 0;

        while (k2 <= number)
        {
          if (TInteger.IsZero(number % k))
          {
            collectionOfPrimeFactors.Add(k);
            number /= k;
          }
          else
          {
            k += m_primeFactorWheelIncrements[index++];
            k2 = k * k;

            if (index >= m_primeFactorWheelIncrements.Length)
              index = 0;
          }
        }

        if (number > TInteger.One)
          collectionOfPrimeFactors.Add(number);
      }

      #endregion

      #region SumAllPrimeFactors

      /// <summary>
      /// <para>The sum of all prime factors that make up a number.</para>
      /// </summary>
      /// <param name="number"></param>
      /// <returns></returns>
      public static TInteger SumAllPrimeFactors(TInteger number)
      {
        var two = TInteger.CreateChecked(2);

        if (number < two)
          return TInteger.Zero; // No prime factors for numbers < 2

        var sum = TInteger.Zero;

        while (TInteger.IsZero(number % two)) // Handle factor 2
        {
          sum += two;
          number /= two;
        }

        for (var i = TInteger.CreateChecked(3); i * i <= number; i += two) // Handle odd factors
          while (TInteger.IsZero(number % i))
          {
            sum += i;
            number /= i;
          }

        if (number > TInteger.One) // If number > 1, it's prime
          sum += number;

        return sum;
      }

      #endregion

      #region SumDistinctPrimeFactors

      /// <summary>
      /// <para>The sum of distinct prime factors that make up a number.</para>
      /// </summary>
      /// <param name="number"></param>
      /// <returns></returns>
      public static TInteger SumDistinctPrimeFactors(TInteger number)
      {
        var two = TInteger.CreateChecked(2);

        if (number < two)
          return TInteger.Zero; // No prime factors for numbers < 2

        var sum = TInteger.Zero;

        if (TInteger.IsZero(number % two)) // Handle factor 2
        {
          sum += two;
          while (TInteger.IsZero(number % two))
            number /= two;
        }

        for (var i = TInteger.CreateChecked(3); i * i <= number; i += two) // Handle odd factors
        {
          if (TInteger.IsZero(number % i))
          {
            sum += i;
            while (TInteger.IsZero(number % i))
              number /= i;
          }
        }

        if (number > TInteger.One) // If number > 1, it's prime
          sum += number;

        return sum;
      }

      #endregion

      #endregion

      #region SequenceOfNthRoot

      /// <summary>
      /// <para>Creates a sequence of powers-of-radix values.</para>
      /// </summary>
      /// <typeparam name="TMinMaxInteger"></typeparam>
      /// <param name="nth"></param>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<(TInteger Root, TInteger Number)> SequenceOfNthRoot(TInteger nth)
      {
        System.ArgumentOutOfRangeException.ThrowIfLessThan(nth, TInteger.CreateChecked(2));

        checked
        {
          TInteger result;

          foreach (var root in Number.ArithmeticSequence(TInteger.One, TInteger.One))
          {
            try { result = Pow(root, nth); } catch { break; }

            yield return (root, result);
          }
        }
      }

      #endregion

      #region SphenicNumbers

      /// <summary>
      /// <para>Yields a sequence of all sphenic numbers less than <paramref name="cutoffNumber"/>.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Sphenic_number"/></para>
      /// </summary>
      /// <param name="cutoffNumber"></param>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<TInteger> SphenicNumbers(TInteger cutoffNumber)
      {
        checked
        {
          for (var i = TInteger.CreateChecked(30); i < cutoffNumber; i++)
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

              if (count == 0 && j > cutoffNumber / (j * j))
                break;

              if (count == 1 && j > (k / j))
                break;
            }

            if (count == 3 && k == TInteger.One)
              yield return i;
          }
        }
      }

      #endregion
    }
  }
}
