#define ROOTN_NEWTONRAPSON // or ROOTN_BINARYMETHOD
using System.Data;

namespace Flux
{
  public static class BinaryInteger
  {
    extension<TInteger>(TInteger)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      #region Abundant numbers (including highly & super abundant)

      /// <summary>
      /// <para>Creates a new sequence of 2-tuples, each with an abundant number and its aliquot sum.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Abundant_number"/></para>
      /// </summary>
      /// <remarks>This function generate results until the type <typeparamref name="TInteger"/> under/overflows in any calculation. No exception is thrown.</remarks>
      /// <typeparam name="TSelf"></typeparam>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<(TInteger Number, TInteger AliqoutSum)> GetAbundantNumbers()
        => Number.LoopVerge(TInteger.CreateChecked(3), TInteger.One).AsParallel().AsOrdered().Select(n => (Number: n, SumDivisors(n).AliquotSum)).Where(x => x.AliquotSum > x.Number);

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
      public static System.Collections.Generic.IEnumerable<(TInteger Number, TInteger Sum)> GetHighlyAbundantNumbers()
      {
        var largestSumOfDivisors = TInteger.Zero;

        foreach (var index in Number.LoopVerge(TInteger.One, TInteger.One))
          if (SumDivisors(index).Sum is var sumOfDivisors && sumOfDivisors > largestSumOfDivisors)
          {
            yield return (index, sumOfDivisors);

            largestSumOfDivisors = sumOfDivisors;
          }
      }

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

      /// <summary>
      /// <para>Determines whether the <paramref name="number"/> is an abundant number.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Abundant_number"/></para>
      /// </summary>
      /// <typeparam name="TSelf"></typeparam>
      /// <param name="number"></param>
      /// <returns></returns>
      public static bool IsAbundantNumber(TInteger n)
        => SumDivisors(n).AliquotSum > n;

      #endregion

      #region Bell numbers

      /// <summary>
      /// <para>Creates a new sequence of Bell numbers.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Bell_number"/></para>
      /// </summary>
      /// <remarks>This function generate results until the type <typeparamref name="TInteger"/> under/overflows in any calculation. No exception is thrown.</remarks>
      /// <typeparam name="TInteger"></typeparam>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<TInteger> GetBellNumbers()
        => GetBellTriangle<TInteger>().Select(a => a[0]);

      /// <summary>
      /// <para>Creates a new sequence with arrays (i.e. row) of Bell numbers in a Bell triangle.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Bell_triangle"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Bell_number"/></para>
      /// </summary>
      /// <remarks>This function generate results until the type <typeparamref name="TInteger"/> under/overflows in any calculation. No exception is thrown.</remarks>
      /// <typeparam name="TInteger"></typeparam>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<TInteger>> GetBellTriangle()
      {
        foreach (var list in GetBellTriangleAugmented<TInteger>().Skip(1))
        {
          list.RemoveAt(0);

          yield return list;
        }
      }

      /// <summary>
      /// <para>Creates a new sequence with arrays (i.e. row) of Bell numbers in an augmented Bell triangle.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Bell_triangle"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Bell_number"/></para>
      /// </summary>
      /// <remarks>This function generate results until the type <typeparamref name="TInteger"/> under/overflows in any calculation. No exception is thrown.</remarks>
      /// <typeparam name="TInteger"></typeparam>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<TInteger>> GetBellTriangleAugmented()
      {
        var l1 = new System.Collections.Generic.List<TInteger>() { TInteger.One }; // This is the current.
        var l0 = new System.Collections.Generic.List<TInteger>(); // This is the previous.

        while (true)
        {
          yield return l1;

          try
          {
            checked
            {
              (l1, l0) = (l0, l1); // Rotate and reuse the lists is much faster and less resource intensive.

              l1.Clear(); // This is now the current, and l0 became the previous.

              l1.Add(l1[0] - l0[0]);
              l1.Add(l0[^1]);

              for (var i = 2; i <= l0.Count; i++)
                l1.Add(l0[i - 1] + l1[i - 1]);
            }
          }
          catch { break; }
        }
      }

      #endregion

      #region BinomialCoefficient

      /// <summary>
      /// <para>The binomial coefficients are the positive integers that occur as coefficients in the binomial theorem. Commonly, a binomial coefficient is indexed by a pair of integers "n >= k >= 0".</para>
      /// <para>This implementation can easily overflow, use larger storage types when possible.</para>
      /// <para><also href="https://en.wikipedia.org/wiki/Binomial_coefficient"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Binomial_coefficient#In_programming_languages"/></para>
      /// <para><seealso href="https://cp-algorithms.com/combinatorics/binomial-coefficients.html"/></para>
      /// <para><see href="https://dmitrybrant.com/2008/04/29/binomial-coefficients-stirling-numbers-csharp"/></para>
      /// </summary>
      /// <remarks>
      /// <para>Also known as "nCk", i.e. "<paramref name="n"/> choose <paramref name="k"/>", because there are nCk ways to choose an (unordered) subset of <paramref name="k"/> elements from a fixed set of <paramref name="n"/> elements.</para>
      /// <para>(k &lt; 0 or k > n) = 0</para>
      /// <para>(k = 0 or k = n) = 1</para>
      /// </remarks>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="n"></param>
      /// <param name="k"></param>
      /// <returns></returns>
      public static TInteger BinomialCoefficient(TInteger n, TInteger k)
      {
        k = TInteger.Min(k, n - k); // Optimization.

        if (TInteger.IsNegative(k))
          return TInteger.Zero;

        if (TInteger.IsZero(k)) // Because of the optimization above, only half of "(TInteger.IsZero(k) || k == n)" is needed.
          return TInteger.One;

        return FallingFactorial(n, k) / Factorial(k);
      }

      #endregion

      #region BitFold..

      /// <summary>
      /// <para>Recursively "folds" all 1-bits, starting at the least-significant-1-bit, into the left-most or higher-order bits.</para>
      /// <para>Yields a bit vector with the same least-significant-1-bit as <paramref name="value"/>, and with all 1's above it.</para>
      /// </summary>
      /// <returns>The left-most or higher-order bits, to the least-significant-1-bit of <paramref name="value"/>, set to 1. If <paramref name="value"/> is negative, -1 is returned (all bits set to 1). Zero returns 0.</returns>
      public static TInteger BitFoldLeft(TInteger value)
        => TInteger.IsZero(value)
        ? value
        : (value is System.Numerics.BigInteger ? CreateBitMaskRight(TInteger.CreateChecked(value.GetBitCount())) : ~TInteger.Zero) << int.CreateChecked(TInteger.TrailingZeroCount(value));
      //var tzc = value.GetTrailingZeroCount();
      //return BitFoldRight(value << value.GetLeadingZeroCount()) >> tzc << tzc;

      /// <summary>
      /// <para>Recursively "folds" all 1-bits, starting at the most-significant-1-bit, into the right-most or lower-order bits.</para>
      /// <para>Yields a bit vector with the same most-significant-1-bit as <paramref name="value"/>, and with all 1's below it.</para>
      /// </summary>
      /// <returns>The right-most or lower-order bits, to the most-significant-1-bit of <paramref name="value"/>, set to 1. If <paramref name="value"/> is negative, -1 is returned (all bits set to 1). Zero returns 0.</returns>
      public static TInteger BitFoldRight(TInteger value)
        => TInteger.IsZero(value)
        ? value
        : (((MostSignificant1Bit(value) - TInteger.One) << 1) | TInteger.One);

#if INCLUDE_SCRATCH

      /// <summary>
      /// <para>This is the traditional SWAR algorithm that recursively "folds" the lower bits into the upper bits, i.e. folded left or towards the MSB.</para>
      /// </summary>
      /// <typeparam name="TValue"></typeparam>
      /// <param name="value"></param>
      /// <returns></returns>
      public TInteger ScratchBitFoldLeft()
      {
        // Or loop to accomodate dynamic data types, but works like the traditional unrolled SWAR below:
        for (var shift = GetBitCount(value) >> 1; shift > 0; shift >>= 1)
          value |= value << shift;

        // value |= (value << 64); // For a 128-bit type.
        // value |= (value << 32); // For a 64-bit type.
        // value |= (value << 16); // For a 32-bit type
        // value |= (value << 8);
        // value |= (value << 4);
        // value |= (value << 2);
        // value |= (value << 1);

        return value;
      }

      /// <summary>
      /// <para>This is the traditional SWAR algorithm that recursively "folds" the upper bits into the lower bits, i.e. folded right or towards the LSB.</para>
      /// </summary>
      /// <typeparam name="TValue"></typeparam>
      /// <param name="value"></param>
      /// <returns></returns>
      public TInteger ScratchBitFoldRight()
      {
        // Or loop to accomodate dynamic data types, but works like traditional unrolled SWAR below:
        for (var shift = GetBitCount(value); shift > 0; shift >>= 1)
          value |= value >>> shift; // Unsigned shift right.

        // value |= (value >> 64); // For a 128-bit type.
        // value |= (value >> 32); // For a 64-bit type.
        // value |= (value >> 16); // For a 32-bit type
        // value |= (value >> 8);
        // value |= (value >> 4);
        // value |= (value >> 2);
        // value |= (value >> 1);

        return value;
      }

#endif

      #endregion

      #region Catalan number

      /// <summary>
      /// <para>Returns the Catalan number for the specified <paramref name="number"/>.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Catalan_number"/></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="number"></param>
      /// <returns></returns>
      public static TInteger GetCatalanNumber(TInteger n)
        => Factorial(n + n) / (Factorial(n + TInteger.One) * Factorial(n));

      /// <summary>
      /// <para>Creates a new sequence with Catalan numbers.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Catalan_number"/></para>
      /// </summary>
      /// <remarks>This function runs indefinitely, if allowed.</remarks>
      /// <typeparam name="TInteger"></typeparam>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<TInteger> GetCatalanSequence()
        => Number.LoopVerge(TInteger.Zero, TInteger.One).AsParallel().AsOrdered().Select(GetCatalanNumber);

      #endregion

      #region Centered Polygonal number

      /// <summary>
      /// <para>Creates a new sequence of </para>
      /// <para><see href="https://en.wikipedia.org/wiki/Centered_polygonal_number"/></para>
      /// </summary>
      /// <remarks>This function runs indefinitely, if allowed.</remarks>
      /// <typeparam name="TSelf"></typeparam>
      /// <param name="numberOfSides"></param>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<(TInteger LayerCount, TInteger CenterPolygonalNumber)> GetCenteredPolygonalLayers(TInteger k)
        => GetCenteredPolygonalNumberSequence(k).PartitionTuple2(false, (previous, current, index) => (TInteger.CreateChecked(index + 2), current)).Prepend((TInteger.One, TInteger.One));
      //{
      //  yield return (TInteger.One, TInteger.One);

      //  foreach (var v in GetCenteredPolygonalNumberSequence(k).PartitionTuple2(false, (previous, current, index) => (previous, current, index)))
      //    yield return (TInteger.CreateChecked(v.index + 2), v.current);
      //}

      /// <summary></summary>
      /// <see href="https://en.wikipedia.org/wiki/Centered_polygonal_number"/>
      public static TInteger GetCenteredPolygonalNumber(TInteger k, TInteger n)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(n);
        System.ArgumentOutOfRangeException.ThrowIfLessThan(k, TInteger.CreateChecked(3));

        return checked(k * n * (n + TInteger.One) / TInteger.CreateChecked(2) + TInteger.One);
      }

      /// <summary></summary>
      /// <see href="https://en.wikipedia.org/wiki/Centered_polygonal_number"/>
      /// <remarks>This function runs indefinitely, if allowed.</remarks>
      public static System.Collections.Generic.IEnumerable<TInteger> GetCenteredPolygonalNumberSequence(TInteger k)
        => Number.LoopVerge(TInteger.Zero, TInteger.One).Select(n => GetCenteredPolygonalNumber(k, n));

      #endregion

      #region Composite numbers (including highly composite)

      /// <summary>
      /// <para>Generates a new sequence of composite numbers.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Composite_number"/></para>
      /// </summary>
      /// <remarks>This function generate results until the type <typeparamref name="TInteger"/> under/overflows in any calculation. No exception is thrown.</remarks>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<TInteger> GetCompositeNumbers()
        => Number.LoopVerge(TInteger.One, TInteger.One).AsParallel().AsOrdered().Where(IsCompositeNumber);

      /// <summary>
      /// <para>Creates a new sequence of highly composite numbers.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Highly_composite_number"/></para>
      /// </summary>
      /// <remarks>This function generate results until the type <typeparamref name="TInteger"/> under/overflows in any calculation. No exception is thrown.</remarks>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<(TInteger Number, TInteger Count)> GetHighlyCompositeNumbers()
      {
        var largestCount = TInteger.Zero;

        foreach (var tuple in Number.LoopVerge(TInteger.One, TInteger.One).AsParallel().AsOrdered().Select(n => (Number: n, Count: CountDivisors(n))))
          if (tuple.Count > largestCount)
          {
            yield return tuple;

            largestCount = tuple.Count;
          }
      }

      /// <summary>
      /// <para>Determines if the <paramref name="number"/> is a composite number, i.e. not a prime and not a unit (1).</para>
      /// </summary>
      /// <param name="n"></param>
      /// <returns></returns>
      public static bool IsCompositeNumber(TInteger n)
        => !IsPrimeNumber(n) // If it's not a prime..
        && n > TInteger.One; // ..and not a unit (1).

      /// <summary>
      /// <para>Determines if the <paramref name="number"/> is a highly composite number.</para>
      /// </summary>
      /// <param name="n"></param>
      /// <returns></returns>
      public static bool IsHighlyCompositeNumber(TInteger n)
      {
        var ncd = CountDivisors(n); // Count number of divisors of N

        for (var i = n - TInteger.One; i >= TInteger.One; i--) // Loop to count number of factors of every number less than n.
        {
          var icd = CountDivisors(i);

          if (icd >= ncd) // If any number less than n has more factors than n, then return false.
            return false;
        }

        return true;
      }

      #endregion

      #region Count..With.. (combinations & permutations)

      /// <summary>
      /// <para>Combinations with repetition, also known as combinations with replacement, are a way to select items from a set where the order does not matter, and items can be chosen more than once.</para>
      /// </summary>
      /// <param name="k">The number of items to choose.</param>
      /// <returns></returns>
      public static TInteger CountCombinationsWithRepetition(TInteger n, TInteger k)
        => BinomialCoefficient(n + k - TInteger.One, k);

      /// <summary>
      /// <para>Combinations without repetition refer to the selection of items from a larger set, where the order of selection does not matter and each item can only be chosen once.</para>
      /// </summary>
      /// <param name="k">The number of items to choose.</param>
      /// <returns></returns>
      public static TInteger CountCombinationsWithoutRepetition(TInteger n, TInteger k)
        => BinomialCoefficient(n, k);

      /// <summary>
      /// <para>Permutations with repetition involve arranging a set of objects where some objects are identical. This concept is useful in various practical scenarios, such as arranging students of different grades or cars of certain colors without distinguishing between identical items.</para>
      /// <para>A permutation is an ordered combination.</para>
      /// </summary>
      /// <param name="k">The number of items to choose.</param>
      /// <returns></returns>
      public static TInteger CountPermutationsWithRepetition(TInteger n, TInteger k)
        => TInteger.CreateChecked(System.Numerics.BigInteger.Pow(System.Numerics.BigInteger.CreateChecked(n), int.CreateChecked(k)));

      /// <summary>
      /// <para>Permutations without repetition refer to different groups of elements that can be done, so that two groups differ from each other only in the order the elements are placed. This situation frequently occurs when you’re working with unique physical objects that can occur only once in a permutation.</para>
      /// <para>A permutation is an ordered combination.</para>
      /// </summary>
      /// <param name="k">The number of items to choose.</param>
      /// <returns></returns>
      public static TInteger CountPermutationsWithoutRepetition(TInteger n, TInteger k)
        => FallingFactorial(n, k);

      #endregion

      #region CreateBitMask..

      /// <summary>
      /// <para>Create a bit-mask with <paramref name="count"/> most-significant-bits (a.k.a. high-order or left-most bits) set to 1.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="count">Can be up to the number of storage bits (bit-count) available in <typeparamref name="TInteger"/>.</param>
      /// <returns></returns>
      /// <remarks><c>PLEASE NOTE THAT THE FIRST ARGUMENT (<paramref name="count"/> for extension method) IS THE NUMBER OF BITS (to account for).</c></remarks>
      public static TInteger CreateBitMaskLeft(TInteger count)
      {
        var bitMaskRight = CreateBitMaskRight(count);

        return bitMaskRight << (bitMaskRight.GetBitCount() - int.CreateChecked(count));
      }

      /// <summary>
      /// <para>Create a bit-mask with <paramref name="bitLength"/> number of most-significant-bits (a.k.a. high-order or left-most bits) from <paramref name="bitMask"/> of <paramref name="bitMaskLength"/> filled repeatedly from least-to-most-significant-bits over the integer.</para>
      /// </summary>
      /// <remarks><c>PLEASE NOTE THAT THE FIRST ARGUMENT (<paramref name="bitMask"/> for extension method) IS THE BIT-MASK (to account for).</c></remarks>
      public static TInteger CreateBitMaskLeft(TInteger bitMask, int bitMaskLength, int bitLength)
      {
        bitMask &= TInteger.CreateChecked((1 << bitMaskLength) - 1); // Ensure only count number of bits in bit-mask in least-significant-bits.

        var (q, r) = int.DivRem(bitLength, bitMaskLength);

        var result = bitMask;

        for (var i = q - 1; i > 0; i--) // Loop bit-count divided by count (minus one) times, hence we skip equal-to zero in the condition.
          result = bitMask | (result << bitMaskLength); // Shift the mask count bits and | (OR) in count most-significant-bits from bit-mask.

        if (r > 0)
          result = (result << r) | (bitMask >>> (bitMaskLength - r));

        return result;
      }

      /// <summary>
      /// <para>Create a bit-mask with <paramref name="count"/> least-significant-bits (a.k.a. low-order or right-most bits) set to 1.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="count">Can be up to the number of storage bits (bit-count) available in <typeparamref name="TInteger"/>.</param>
      /// <returns></returns>
      /// <remarks><c>PLEASE NOTE THAT THE FIRST ARGUMENT (<paramref name="count"/> for extension method) IS THE NUMBER OF BITS (to account for).</c></remarks>
      public static TInteger CreateBitMaskRight(TInteger count)
      {
        if (TInteger.IsZero(count))
          return count;

        return (((TInteger.One << (int.CreateChecked(count) - 1)) - TInteger.One) << 1) | TInteger.One;
      }

      /// <summary>
      /// <para>Create a bit-mask with <paramref name="bitLength"/> number of least-significant-bits (a.k.a. low-order or right-most bits) from <paramref name="bitMask"/> of <paramref name="bitMaskLength"/> filled repeatedly from most-to-least-significant-bits over the <typeparamref name="TBitMask"/>.</para>
      /// </summary>
      /// <remarks><c>PLEASE NOTE THAT THE FIRST ARGUMENT (<paramref name="bitMask"/> for extension method) IS THE BIT-MASK (to account for).</c></remarks>
      public static TInteger CreateBitMaskRight(TInteger bitMask, int bitMaskLength, int bitLength)
      {
        bitMask &= TInteger.CreateChecked((1 << bitMaskLength) - 1); // Ensure only count number of bits in bit-mask in least-significant-bits.

        var (q, r) = int.DivRem(bitLength, bitMaskLength);

        var result = bitMask;

        for (var i = q - 1; i > 0; i--) // Loop bit-count divided by count (minus one) times, hence we skip equal-to zero in the condition.
          result = bitMask | (result << bitMaskLength); // Shift the mask count bits and | (OR) in count most-significant-bits from bit-mask.

        if (r > 0)
          result |= (bitMask & TInteger.CreateChecked((1 << r) - 1)) << (bitLength - r);

        return result;
      }

      #endregion

      #region DirichletConvolution

      /// <summary>
      /// <para>Computes the Dirichlet convolution of two arithmetic functions f and g for a given n.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Dirichlet_convolution"/></para>
      /// </summary>
      /// <param name="n">The positive integer for which to compute the convolution.</param>
      /// <param name="f">Function f: int -> long</param>
      /// <param name="g">Function g: int -> long</param>
      /// <returns>The value of (f * g)(n)</returns>
      public static TInteger DirichletConvolution(TInteger n, Func<TInteger, TInteger> f, Func<TInteger, TInteger> g)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(n);

        return GetDivisors(n).Select(d => f(d) * g(n / d)).Sum();
      }

      ///// <summary>
      ///// <para></para>
      ///// <para><see href="https://en.wikipedia.org/wiki/Dirichlet_convolution"/></para>
      ///// </summary>
      ///// <param name="n"></param>
      ///// <returns></returns>
      //public static TInteger DirichletConvolutionFunction(TInteger n)
      //{
      //  var result = TInteger.Zero;

      //  for (var d = TInteger.One; d <= n; d++)
      //    if (TInteger.IsZero(n % d)) // If d is a divisor of n, add f(d) * g(n//d) to the result.
      //      result += SumDivisors(d).Sum * EulerTotient(n / d);

      //  return result;
      //}

      #endregion

      #region ..DivRem

      /// <summary>
      /// <para>Ceiling division, where the remainder has the opposite sign of that of the divisor.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Modulo"/></para>
      /// <para><see href="https://stackoverflow.com/a/20638659/3178666"/></para>
      /// </summary>
      /// <param name="a"></param>
      /// <param name="n"></param>
      /// <returns>
      /// <para><c>q = ceiling(a / n)</c></para>
      /// <para><c>r = a - n * q</c></para>
      /// </returns>
      public static (TInteger Quotient, TInteger Remainder) CeilingDivRem(TInteger a, TInteger n)
      {
        if (TInteger.IsZero(n)) throw new System.DivideByZeroException();

        var q = (TInteger.IsNegative(a) == TInteger.IsNegative(n) ? (a + (n - TInteger.CopySign(TInteger.One, n))) : a) / n;

        return (q, a - n * q);
      }

      /// <summary>
      /// <para>Closest division.</para>
      /// </summary>
      /// <param name="a"></param>
      /// <param name="n"></param>
      /// <returns></returns>
      public static (TInteger Quotient, TInteger Remainder) ClosestDivRem(TInteger a, TInteger n)
      {
        if (TInteger.IsZero(n)) throw new System.DivideByZeroException();

        var ndiv2 = n / TInteger.CreateChecked(2);

        var q = (TInteger.IsNegative(a) == TInteger.IsNegative(n) ? (a + ndiv2) : (a - ndiv2)) / n;

        return (q, a - n * q);
      }

      /// <summary>
      /// <para>Enveloped (opposite of truncated, in that it envelops the entire fractional side to the next whole integer) division, where the quotient is ceiling for positive and floor for negative.</para>
      /// </summary>
      /// <param name="a"></param>
      /// <param name="n"></param>
      /// <returns>
      /// <para><c>q = envelop(a / n)</c></para>
      /// <para><c>r = a - n * q</c></para>
      /// </returns>
      public static (TInteger Quotient, TInteger Remainder) EnvelopedDivRem(TInteger a, TInteger n)
      {
        if (TInteger.IsZero(n)) throw new System.DivideByZeroException();

        var onecs = TInteger.CopySign(TInteger.One, n);

        var q = (
          TInteger.IsNegative(a) == TInteger.IsNegative(n) // XOR on signs:
          ? (a + (n - onecs)) // Both terms equal makes a positive; perform a ceiling-div-rem.
          : (a - (n - onecs)) // Terms are unequal makes a negative; perform a floored-div-rem.
        ) / n;

        return (q, a - n * q);
      }

      /// <summary>
      /// <para>Euclidean division, where the remainder is always positive.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Euclidean_division"/></para>
      /// <para><see href="https://en.wikipedia.org/wiki/Modulo"/></para>
      /// <para><see href="https://stackoverflow.com/a/20638659/3178666"/></para>
      /// </summary>
      /// <param name="a"></param>
      /// <param name="n"></param>
      /// <returns>
      /// <para><c>q = sgn(n) * floor(a / abs(n))</c></para>
      /// <para><c>r = a - n * q</c></para>
      /// </returns>
      public static (TInteger Quotient, TInteger Remainder) EuclideanDivRem(TInteger a, TInteger n)
      {
        if (TInteger.IsZero(n)) throw new System.DivideByZeroException();

        var q = TInteger.CreateChecked(Number.Sign(n)) * ((TInteger.IsNegative(a) == TInteger.IsNegative(n) ? a : (a - (n - TInteger.CopySign(TInteger.One, n)))) / n);

        return (q, a - TInteger.Abs(n) * q);
      }

      /// <summary>
      /// <para>Floored division, where the remainder has the same sign as the divisor.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Modulo"/></para>
      /// <para><see href="https://stackoverflow.com/a/20638659/3178666"/></para>
      /// </summary>
      /// <param name="a"></param>
      /// <param name="n"></param>
      /// <returns>
      /// <para><c>q = floor(a / n)</c></para>
      /// <para><c>r = a - n * q</c></para>
      /// </returns>
      public static (TInteger Quotient, TInteger Remainder) FlooredDivRem(TInteger a, TInteger n)
      {
        if (TInteger.IsZero(n)) throw new System.DivideByZeroException();

        var q = (TInteger.IsNegative(a) != TInteger.IsNegative(n) ? (a - (n - TInteger.CopySign(TInteger.One, n))) : a) / n;

        return (q, a - n * q);
      }

      /// <summary>
      /// <para>Rounded division, where the sign of the remainder depends on the rounding strategy, which is <see cref="MidpointRounding.ToEven"/>.</para>
      /// </summary>
      /// <param name="a"></param>
      /// <param name="n"></param>
      /// <returns>
      /// <para><c>q = round(a / n)</c> // rounding half to even</para>
      /// <para><c>r = a - n * q</c></para>
      /// </returns>
      public static (TInteger Quotient, TInteger Remainder) RoundedDivRem(TInteger a, TInteger n)
      {
        var q = FlooredDivRem(a, n).Quotient;

        if (!TInteger.IsEvenInteger(q))
          q = CeilingDivRem(a, n).Quotient;

        return (q, a - n * q);
      }

      #endregion

      #region Divisor functions

      /// <summary>
      /// <para>Creates a new list of factors (a.k.a. divisors) for the specified number.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Divisor"/></para>
      /// </summary>
      /// <remarks>This implementaion does not order the result.</remarks>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="value"></param>
      /// <param name="proper"></param>
      /// <returns></returns>
      public static void AddDivisors(TInteger n, System.Collections.Generic.ICollection<TInteger> divisors)
      {
        if (n > TInteger.Zero)
          for (var i = TInteger.One; (i * i) <= n; i++)
          {
            var (q, r) = TInteger.DivRem(n, i);

            if (TInteger.IsZero(r))
            {
              divisors.Add(i);

              if (q != i)
                divisors.Add(q);
            }
          }
      }

      /// <summary>
      /// <para>σ0()</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Divisor"/></para>
      /// <para><see href="https://cp-algorithms.com/algebra/divisors.html"/></para>
      /// </summary>
      public static TInteger CountDivisors(TInteger n)
      {
        var count = TInteger.One;

        for (var i = TInteger.CreateChecked(2); (i * i) <= n; i++)
        {
          if (TInteger.IsZero(n % i))
          {
            var e = TInteger.Zero;

            do
            {
              e++;

              n /= i;
            }
            while (TInteger.IsZero(n % i));

            count *= e + TInteger.One;
          }
        }

        if (n > TInteger.One)
          count <<= 1;

        return count;
      }

      public static System.Collections.Generic.List<TInteger> GetDivisors(TInteger n, bool sort = false)
      {
        var divisors = new System.Collections.Generic.List<TInteger>();
        AddDivisors(n, divisors);
        if (sort) divisors.Sort();
        return divisors;
      }

      /// <summary>Determines whether the <paramref name="number"/> is a deficient number.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Deficient_number"/>
      /// <seealso cref="https://en.wikipedia.org/wiki/Divisor#Further_notions_and_facts"/>
      public static bool IsDeficientNumber(TInteger n)
        => SumDivisors(n).AliquotSum < n;

      /// <summary>Determines whether the <paramref name="number"/> is a perfect number.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Perfect_number"/>
      public static bool IsPerfectNumber(TInteger n)
        => SumDivisors(n).AliquotSum == n;

      /// <summary>
      /// <para>σ1()</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Divisor"/></para>
      /// <para><see href="https://cp-algorithms.com/algebra/divisors.html"/></para>
      /// </summary>
      public static (TInteger Sum, TInteger AliquotSum) SumDivisors(TInteger n)
      {
        var sum = TInteger.One;

        var aliquot = n;

        for (var i = TInteger.CreateChecked(2); i * i <= n; i++)
        {
          if (TInteger.IsZero(n % i))
          {
            var e = 0;

            do
            {
              e++;

              n /= i;
            }
            while (TInteger.IsZero(n % i));

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

        if (n > TInteger.One)
          sum *= TInteger.One + n;

        return (sum, sum - aliquot);
      }

      #endregion

      #region DoubleFactorial

      /// <summary>
      /// <para>The double factorial of a number n, denoted by n‼, is the product of all the positive integers up to n that have the same parity (odd or even) as n.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Double_factorial"/></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="n"></param>
      /// <returns></returns>
      public static TInteger DoubleFactorial(TInteger n)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(n);

        var result = TInteger.One;

        for (var two = result + result; n > TInteger.Zero; n -= two)
          result *= n;

        return result;
      }

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

      #region Factorial

      /// <summary>
      /// <para>The factorial of a non-negative integer n, denoted by n!, is the product of all positive integers less than or equal to n.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Factorial"/></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="n"></param>
      /// <returns></returns>
      public static TInteger Factorial(TInteger n)
      {
        if (StaticFactorial(n) is var sf && !TInteger.IsZero(sf))
          return sf;

        if (n < TInteger.CreateChecked(47))
          return NaiveFactorial(n);

        return SplitFactorial(n);
      }

      #endregion

      #region FallingFactorial

      /// <summary>
      /// <para>When n is a positive integer, the falling factorial, (x)_n, gives the number of n-permutations (sequences of distinct elements) from an n-element set.</para>
      /// <example>
      /// <para>The number (3) of different podiums (assignments of gold, silver, and bronze medals) possible in an eight-person race: <c>FallingFactorial(8, 3)</c></para>
      /// </example>
      /// <para><see href="https://en.wikipedia.org/wiki/Falling_and_rising_factorials"/></para>
      /// <para><see href="https://dmitrybrant.com/2008/04/29/binomial-coefficients-stirling-numbers-csharp"/></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="x">The base, or starting value of the sequence of factors. Plays the same role as in ordinary factorial‑like expressions.</param>
      /// <param name="n">The order, or number of factors in the product. Must be non-negative. If 0, the defined result is 1.</param>
      /// <returns>
      /// <para>The count of permutations no repetitions.</para>
      /// </returns>
      public static TInteger FallingFactorial(TInteger x, TInteger n)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(n);

        //System.ArgumentOutOfRangeException.ThrowIfNegative(x); // Combinatorics only.

        //if (n > x)
        //  return TInteger.Zero; // Combinatorics: No arrangements possible. This check does not apply for rising factorial.

        TInteger result = TInteger.One;

        checked
        {
          while (--n >= TInteger.Zero) // Compute the falling factorial, decreasing x for each term.
            result *= x--;
        }

        return result;
      }

      #endregion

      #region Fibonacci number/sequence

      /// <summary>
      /// <para>Creates a new sequence of <typeparamref name="TInteger"/> with Fibonacci numbers.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Fibonacci_number"/></para>
      /// </summary>
      /// <remarks>This function generate results until the type <typeparamref name="TInteger"/> under/overflows in any calculation. No exception is thrown.</remarks>
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

      /// <summary>
      /// <para>Determines whether the <paramref name="number"/> is a Fibonacci number.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Fibonacci_number"/></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="number"></param>
      /// <returns></returns>
      public static bool IsFibonacciNumber(TInteger n)
      {
        var four = TInteger.CreateChecked(4);

        var fivens = TInteger.CreateChecked(5) * n * n;
        var fp4 = fivens + four;
        var fp4sr = IntegerSqrt(fp4);
        var fm4 = fivens - four;
        var fm4sr = IntegerSqrt(fm4);

        return fp4sr * fp4sr == fp4 || fm4sr * fm4sr == fm4;
      }

      #endregion

      #region Gray

      /// <summary>
      /// <para>Converts a binary number to a reflected binary Gray code.</para>
      /// <see href="https://en.wikipedia.org/wiki/Gray_code"/>
      /// </summary>
      public static TInteger ConvertBinaryToGray(TInteger value)
        => value ^ (value >>> 1);

      /// <summary>
      /// <para>Converts a reflected binary gray code to a binary number.</para>
      /// <see href="https://en.wikipedia.org/wiki/Gray_code"/>
      /// </summary>
      public static TInteger ConvertGrayToBinary(TInteger value)
      {
        var mask = value;

        while (!TInteger.IsZero(mask))
        {
          mask >>>= 1;
          value ^= mask;
        }

        return value;
      }

      #endregion

      #region GenerateSubRanges

      /// <summary>
      /// <para>Generates a new sequence of <see cref="System.Range"/> objects, each with <paramref name="subLength"/> (the last may contain less) elements from the total length of a super-sequence.</para>
      /// <para>How many items do you want in each sub-range?</para>
      /// </summary>
      /// <param name="length"></param>
      /// <param name="subLength"></param>
      /// <returns></returns>
      public static System.Collections.Generic.List<System.Range> GenerateSubRangesBySubLength(TInteger length, TInteger subLength)
      {
        var subRanges = new System.Collections.Generic.List<System.Range>();

        for (var index = TInteger.Zero; index < length; index += subLength)
          subRanges.Add(RangeExtensions.FromOffsetAndLength(int.CreateChecked(index), int.CreateChecked(TInteger.Min(subLength, length - index))));

        return subRanges;
      }

      /// <summary>
      /// <para>Generates a new sequence with <paramref name="count"/> <see cref="System.Range"/> objects.</para>
      /// <para>How many sub-ranges do you want?</para>
      /// </summary>
      /// <param name="length"></param>
      /// <param name="count"></param>
      /// <returns></returns>
      public static System.Collections.Generic.List<System.Range> GenerateCountSubRanges(TInteger length, TInteger count)
        => GenerateSubRangesBySubLength(length, CeilingDivRem(length, count).Quotient);

      #endregion

      #region GetFormatStringWithCountDecimals

      /// <summary>
      /// <para>Returns a string format for a specified number of fractional digits.</para>
      /// </summary>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public static string GetFormatStringWithCountDecimals(TInteger count)
      {
        System.ArgumentOutOfRangeException.ThrowIfLessThan(count, TInteger.One);
        System.ArgumentOutOfRangeException.ThrowIfGreaterThan(count, TInteger.CreateChecked(339));

        return "0." + new string('#', int.CreateChecked(count));
      }

      #endregion

      #region Greatest common divisor (GCD)

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
        #region Binary GCD

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

        #endregion

        #region Euclid GCD

        //while (b != TInteger.Zero)
        //  (a, b) = (b, a % b);
        ////{
        ////  var t = b;
        ////  b = a % b;
        ////  a = t;
        ////}

        //return TInteger.Abs(a);

        #endregion

        #region LehmerGcd

        //a = TInteger.Abs(a);
        //b = TInteger.Abs(b);

        //if (TInteger.IsZero(a)) return b;
        //if (TInteger.IsZero(b)) return a;

        //if (b > a)
        //  (a, b) = (b, a);

        //while (b > TInteger.Zero)
        //{
        //  var shift = int.Max(a.GetBitLength(), b.GetBitLength()) - 64;

        //  var aHigh = a >> shift;
        //  var bHigh = b >> shift;

        //  TInteger A = TInteger.One, B = TInteger.Zero, C = TInteger.Zero, D = TInteger.One;

        //  while (true)
        //  {
        //    if (TInteger.IsZero(bHigh + C) || TInteger.IsZero(bHigh + D))
        //      break;

        //    var q = ((aHigh + A) / (bHigh + C));
        //    var q2 = ((aHigh + B) / (bHigh + D));

        //    if (q != q2)
        //      break;

        //    (A, C) = (C, A - q * C);

        //    (B, D) = (D, B - q * D);

        //    (aHigh, bHigh) = (bHigh, aHigh - q * bHigh);
        //  }

        //  (a, b) = TInteger.IsZero(B)
        //    ? (b, a % b) // Single Euclid step.
        //    : (A * a + B * b, C * a + D * b); // Apply transform.

        //  if (TInteger.IsNegative(b))
        //    b = -b; // Ensure positive
        //}

        //return a;

        #endregion
      }

      /// <summary>
      /// <para>the extended Euclidean algorithm is an extension to the Euclidean algorithm, and computes, in addition to the greatest common divisor (gcd) of integers a and b, also the coefficients of Bézout's identity, which are integers x and y such that "<c>ax + by = gcd(a, b)</c>".</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Extended_Euclidean_algorithm"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/B%C3%A9zout%27s_identity"/></para>
      /// </summary>
      /// <remarks>When a and b are coprime (i.e. GCD equals 1), x is the modular multiplicative inverse of a modulo b, and y is the modular multiplicative inverse of b modulo a.</remarks>
      /// <param name="a"></param>
      /// <param name="b"></param>
      /// <param name="x"></param>
      /// <param name="y"></param>
      /// <returns></returns>
      public static TInteger GcdExt(TInteger a, TInteger b, out TInteger x, out TInteger y)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(a);
        System.ArgumentOutOfRangeException.ThrowIfNegative(b);

        x = TInteger.One;
        y = TInteger.Zero;

        var u = TInteger.Zero;
        var v = TInteger.One;

        while (!TInteger.IsZero(b))
        {
          a = b;
          b = a % b;

          var q = a / b;

          var u1 = x - q * u;
          var v1 = y - q * v;

          x = u;
          y = v;

          u = u1;
          v = v1;
        }

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

      #region IntegerLog/10/E

      /// <summary>
      /// <para>Returns the integer (toward-zero, away-from-zero) logarithm of specified a <paramref name="value"/> in a specified <paramref name="radix"/>.</para>
      /// </summary>
      /// <typeparam name="TRadix"></typeparam>
      /// <param name="value"></param>
      /// <param name="radix"></param>
      /// <returns></returns>
      public static (TInteger IntegralLogTowardZero, TInteger IntegralLogAwayFromZero) IntegerLog<TRadix>(TInteger value, TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        if (TInteger.IsZero(value))
          return (value, value);

        var abs = TInteger.Abs(value);

        var logR = System.Numerics.BigInteger.Log(System.Numerics.BigInteger.CreateChecked(abs), double.CreateChecked(Units.Radix.AssertMember(radix)));

        var ilogR = TInteger.CreateChecked(FloatingPoint.IsNearInteger(logR, out var integer) ? integer : double.Floor(logR));

        return (TInteger.CopySign(ilogR, value), TInteger.CopySign(IsPowOf(abs, radix) ? ilogR : ilogR + TInteger.One, value));
      }

      /// <summary>
      /// <para>Returns the integer (floor) logarithm of specified a <paramref name="value"/> in a base/radix 10.</para>
      /// </summary>
      /// <typeparam name="TRadix"></typeparam>
      /// <param name="value"></param>
      /// <returns></returns>
      public static TInteger IntegerLog10<TRadix>(TInteger value)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        if (TInteger.IsZero(value))
          return value;

        var log10 = System.Numerics.BigInteger.Log10(System.Numerics.BigInteger.CreateChecked(TInteger.Abs(value)));

        var ilog10 = FloatingPoint.IsNearInteger(log10, out var integer) ? integer : double.Floor(log10);

        return TInteger.CopySign(TInteger.CreateChecked(ilog10), value);
      }

      /// <summary>
      /// <para>Returns the integer (floor) natural logarithm of specified a <paramref name="value"/> in a base/radix E.</para>
      /// </summary>
      /// <typeparam name="TRadix"></typeparam>
      /// <param name="value"></param>
      /// <returns></returns>
      public static TInteger IntegerLogE<TRadix>(TInteger value)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        if (TInteger.IsZero(value))
          return value;

        var logE = System.Numerics.BigInteger.Log(System.Numerics.BigInteger.CreateChecked(TInteger.Abs(value)));

        var ilogE = FloatingPoint.IsNearInteger(logE, out var integer) ? integer : double.Floor(logE);

        return TInteger.CopySign(TInteger.CreateChecked(ilogE), value);
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

      #region JosephusProblem

      /// <summary>
      /// <para>Calculates the last longest surviving position (it's not a 0-based index) of the Flavius Josephus problem where <paramref name="value"/> people stand in a circle and every <paramref name="k"/> person commits suicide.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Josephus_problem"/></para>
      /// </summary>
      /// <remarks>This is about counting positions, so it is 1-based position that is computed.</remarks>
      /// <param name="value">The number of people in the initial circle.</param>
      /// <param name="k">The count of each step. I.e. k-1 people are skipped and the k-th is executed.</param>
      /// <returns>The 1-indexed position that the survivor occupies.</returns>
      public static TInteger JosephusProblem(TInteger n, TInteger k)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(k);
        System.ArgumentOutOfRangeException.ThrowIfGreaterThan(k, n);

        var survivingPosition = TInteger.Zero;

        for (var positionCounter = TInteger.One; positionCounter <= n; positionCounter++)
          survivingPosition = (survivingPosition + k) % positionCounter;

        return survivingPosition + TInteger.One;
      }

      #endregion

      #region LahNumber

      /// <summary>
      /// <para>In mathematics, the (signed and unsigned) Lah numbers are coefficients expressing rising factorials in terms of falling factorials and vice versa.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Lah_number"/></para>
      /// <para><see href="https://rosettacode.org/wiki/Lah_numbers"/></para>
      /// </summary>
      /// <remarks>Lah numbers are sometimes called Stirling numbers of the third kind.</remarks>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="n"></param>
      /// <param name="k"></param>
      /// <returns></returns>
      public static TInteger LahNumber(TInteger n, TInteger k)
      {
        if (k == TInteger.One)
          return Factorial(n);

        if (k == n)
          return TInteger.One;

        if (k > n)
          return TInteger.Zero;

        if (k < TInteger.One || n < TInteger.One)
          return TInteger.Zero;

        checked
        {
          var fnM1 = Factorial(n - TInteger.One);
          var fkM1 = Factorial(k - TInteger.One);

          return (fnM1 * n * fnM1) / (fkM1 * k * fkM1) / Factorial(n - k);
        }
      }

      #endregion

      #region Least common multiple (LCM)

      /// <summary>
      /// <para>In arithmetic and number theory, the least common multiple (LCM) of two integers a and b, usually denoted by lcm(a, b), is the smallest positive integer that is divisible by both a and b. Since division of integers by zero is undefined, this definition has meaning only if a and b are both different from zero. However, some authors define lcm(a, 0) as 0 for all a, since 0 is the only common multiple of a and 0.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Least_common_multiple"/></para>
      /// </summary>
      /// <param name="a"></param>
      /// <param name="b"></param>
      /// <returns></returns>
      public static TInteger Lcm(TInteger a, TInteger b)
        => a / Gcd(a, b) * b;

      /// <summary>The same as <see cref="Lcm{TInteger}(TInteger, TInteger)"/> but accepts two or more integers.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Least_common_multiple"/>
      public static TInteger LeastCommonMultiple(TInteger a, params TInteger[] other)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(other.Length);

        return Lcm(a, other.Aggregate(Lcm));
      }

      #endregion

      #region Leonardo sequence

      /// <summary>
      /// <para>Creates a new sequence with Leonardo numbers.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Leonardo_number"/></para>
      /// </summary>
      /// <remarks>This function generate results until the type <typeparamref name="TInteger"/> under/overflows in any calculation. No exception is thrown.</remarks>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="first"></param>
      /// <param name="second"></param>
      /// <param name="step"></param>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<TInteger> GetLeonardoSequence(TInteger first, TInteger second, TInteger step)
      {
        while (true)
        {
          yield return first;

          checked { (first, second) = (second, first + second + step); }
        }
      }

      #endregion

      #region Mersenne numbers

      /// <summary>
      /// <para>Computes the Mersenne number for the specified <paramref name="value"/>.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Mersenne_number"/></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="value"></param>
      /// <returns></returns>
      public static TInteger GetMersenneNumber(TInteger n)
        => checked((TInteger.One << int.CreateChecked(n)) - TInteger.One);

      /// <summary>`
      /// <para>Creates a new sequence of Mersenne numbers.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Mersenne_number"/></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<TInteger> GetMersenneNumberSequence()
        => Number.LoopVerge(TInteger.One, TInteger.One).Select(GetMersenneNumber);

      /// <summary>
      /// <para>Creates a new sequence of Mersenne primes.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Mersenne_number"/></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<TInteger> GetMersennePrimeSequence()
        => GetMersenneNumberSequence<TInteger>().Where(IsPrimeNumber);

      #endregion

      #region Modular arithmetic

      /// <summary>
      /// <para>Modular addition.</para>
      /// </summary>
      /// <param name="b"></param>
      /// <param name="m"></param>
      /// <returns></returns>
      public static TInteger ModAdd(TInteger a, TInteger b, TInteger m)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(m);

        return (a + b) % m;
      }

      /// <summary>
      /// <para>Modular division.</para>
      /// </summary>
      /// <param name="b"></param>
      /// <param name="m"></param>
      /// <returns></returns>
      public static TInteger ModDiv(TInteger a, TInteger b, TInteger m)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(m);

        a %= m;

        var inv = ModInv(b, m);

        return (a * inv) % m;
      }

      /// <summary>
      /// <para>Modular multiplicative inverse of an integer <paramref name="a"/> and the modulus <paramref name="m"/>.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Modular_multiplicative_inverse"/></para>
      /// <para><see href="https://en.wikipedia.org/wiki/Modular_arithmetic"/></para>
      /// </summary>
      /// <remarks>
      /// <para>A modular multiplicative inverse may not exists for the specified parameters. In that case an arithmetic exception is thrown.</para>
      /// <para><c>var mi = ModInv(4, 7);</c> // mi = 2, i.e. "2 is the modular multiplicative inverse of 4 (and vice versa), mod 7".</para>
      /// <para><c>var mi = ModInv(8, 11);</c> // mi = 7, i.e. "7 is the modular inverse of 8, mod 11".</para>
      /// </remarks>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="a"></param>
      /// <param name="m"></param>
      /// <returns></returns>
      /// <exception cref="System.ArithmeticException"></exception>
      public static TInteger ModInv(TInteger a, TInteger m)
      {
        System.ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(m, TInteger.One);

        var t = TInteger.Zero;
        var newt = TInteger.One;
        var r = m;
        var newr = a;

        while (!TInteger.IsZero(newr))
        {
          var q = r / newr;
          (t, newt) = (newt, t - q * newt);
          (r, newr) = (newr, r - q * newr);
        }

        if (r > TInteger.One)
          throw new System.ArithmeticException();

        if (TInteger.IsNegative(t))
          t += m;

        return t;
      }

      /// <summary>
      /// <para>Modular multiplication.</para>
      /// </summary>
      /// <param name="b"></param>
      /// <param name="m"></param>
      /// <returns></returns>
      public static TInteger ModMul(TInteger a, TInteger b, TInteger m)
        => ((a % m) * (b % m)) % m;

      /// <summary>
      /// <para>Modular exponentiation of <paramref name="dividend"/> and <paramref name="divisor"/>.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Modular_exponentiation"/></para>
      /// <para><see href="https://en.wikipedia.org/wiki/Modular_arithmetic"/></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="b"></param>
      /// <param name="e"></param>
      /// <param name="m"></param>
      /// <returns></returns>
      public static TInteger ModPow(TInteger a, TInteger e, TInteger m)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(m);

        if (m == TInteger.One)
          return TInteger.Zero;

        var r = TInteger.One;

        a = a % m;

        while (e > TInteger.Zero)
        {
          if ((e % TInteger.CreateChecked(2)) == TInteger.One)
            r = (r * a) % m;

          a = (a * a) % m;

          e >>= 1;
        }

        return r;
      }

      /// <summary>
      /// <para>Modular subtraction.</para>
      /// </summary>
      /// <param name="b"></param>
      /// <param name="m"></param>
      /// <returns></returns>
      public static TInteger ModSub(TInteger a, TInteger b, TInteger m)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(m);

        return (a - b) % m;
      }

      #endregion

      #region GetMoserDeBruijnSequence

      /// <summary>Creates a sequence of Moser/DeBruijn numbers.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Moser%E2%80%93De_Bruijn_sequence"/>
      /// <seealso cref="https://www.geeksforgeeks.org/moser-de-bruijn-sequence/"/>
      public static System.Collections.Generic.List<TInteger> GetMoserDeBruijnSequence(TInteger n)
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

      #region MultiFactorial

      /// <summary>
      /// <para>Naive implementation of n! (k = 1, factorial), n!! (k = 2, a.k.a. double factorial), n!!! (k = 3, triple factorial), etc.</para>
      /// </summary>
      /// <param name="n"></param>
      /// <param name="k"></param>
      /// <returns></returns>
      public static TInteger MultiFactorial(TInteger n, TInteger k)
      {
        var result = TInteger.One;

        while (n > TInteger.Zero)
        {
          result *= n;

          n -= k;
        }

        return result;
      }

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

      #region ..OrdinalIndicator..

      /// <summary>
      /// <para>Gets the ordinal indicator suffix for <paramref name="value"/>. E.g. "st" for 1 and "nd" for 122.</para>
      /// </summary>
      /// <remarks>The suffixes "st", "nd" and "rd" are consistent for all numbers ending in 1, 2 and 3, resp., except numbers ending with 11, 12 and 13, which instead uses the suffix "th".</remarks>
      public static string GetOrdinalIndicatorSuffix(TInteger value)
      {
        var hundreds = int.CreateChecked(TInteger.Abs(value) % TInteger.CreateChecked(100)); // Trim the value (to 2 digits) before making it fit in an int (since the value could be larger).

        var (tens, ones) = int.DivRem(hundreds, 10); // ones only needs "% 10", but tens need "/ 10"..

        tens %= 10; // ..and also a "% 10".

        if (tens != 1) // If tens = 1 then variations are possible, if tens != 1 there are no variations.
          switch (ones)
          {
            case 1: return "st";
            case 2: return "nd";
            case 3: return "rd";
          }

        return "th";
      }

      #endregion

      #region Padovan sequence

      /// <summary>
      /// <para>Creates a new sequence with Padovan numbers.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Padovan_sequence"/></para>
      /// </summary>
      /// <remarks>This function generate results until the type <typeparamref name="TInteger"/> under/overflows in any calculation. No exception is thrown.</remarks>
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
      /// <remarks>This function generate results until the type <typeparamref name="TInteger"/> under/overflows in any calculation. No exception is thrown.</remarks>
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

      // PockHammer (ambiguous, removed) - use FallingFactorial/RisingFactorial instead. There is also generalized versions in the IFloatingPoint extensions class.

      #region Pow

      /// <summary>
      /// <para>Indicates whether <paramref name="value"/> is a power of <paramref name="radix"/>.</para>
      /// </summary>
      /// <remarks>This version also handles negative values simply by mirroring the corresponding positive value. Zero return as false.</remarks>
      public static bool IsPowOf<TRadix>(TInteger value, TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        var log = System.Numerics.BigInteger.Log(System.Numerics.BigInteger.CreateChecked(value), double.CreateChecked(Units.Radix.AssertMember(radix)));

        return FloatingPoint.IsNearInteger(log, out var ilog) && Pow(TInteger.CreateChecked(radix), TInteger.CreateChecked(ilog)) == value;
      }

      /// <summary>
      /// <para>Computes <paramref name="value"/> raised to the power of <paramref name="exponent"/>.</para>
      /// <para>Uses the built-in <see cref="System.Numerics.BigInteger"/> function.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="exponent">The exponent with which to raise the value.</param>
      /// <returns>The value raised to the <paramref name="exponent"/>-of.</returns>
      /// <remarks>If <paramref name="value"/> and/or <paramref name="exponent"/> are zero, 1 is returned. I.e. 0&#x2070;, x&#x2070; and 0&#x02E3; all return 1 in this version.</remarks>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public static TInteger Pow<TExponent>(TInteger value, TExponent exponent)
        where TExponent : System.Numerics.IBinaryInteger<TExponent>
        => TInteger.CreateChecked(System.Numerics.BigInteger.Pow(System.Numerics.BigInteger.CreateChecked(value), int.CreateChecked(exponent)));

      #endregion

      #region Prime numbers

      /// <summary>
      /// <para>Creates a new sequence of ascending possible primes, greater-than-or-equal-to a specified number.</para>
      /// </summary>
      /// <param name="n"></param>
      /// <returns></returns>
      private static System.Collections.Generic.IEnumerable<TInteger> GetAscendingPrimeCandidates(TInteger n)
      {
        var one = TInteger.CreateChecked(1);
        var two = TInteger.CreateChecked(2);
        var three = TInteger.CreateChecked(3);
        var six = TInteger.CreateChecked(6);

        if (n < two)
          n = two;

        if (n <= two)
          yield return two;

        if (n <= three)
          yield return three;

        checked
        {
          var (q, r) = FlooredDivRem(n, six);

          var hi = q * six;

          if (r > one)
            hi += six;

          if (r <= one)
          {
            try { n = hi + one; } catch { yield break; }
            yield return n;

            try { hi += six; } catch { yield break; }
          }

          while (true)
          {
            try { n = hi - one; } catch { break; }
            yield return n;

            try { n = hi + one; } catch { break; }
            yield return n;

            try { hi += six; } catch { break; }
          }
        }
      }

      /// <summary>
      /// <para>Creates a new sequence ascending prime numbers, greater-than-or-equal-to a specified number.</para>
      /// </summary>
      /// <remarks>This function generate results until the type <typeparamref name="TInteger"/> under/overflows in any calculation. No exception is thrown.</remarks>
      /// <param name="n"></param>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<TInteger> GetAscendingPrimes(TInteger n)
        => GetAscendingPrimeCandidates(n).AsParallel().AsOrdered().Where(IsPrimeNumber);

      /// <summary>Creates a new sequence of descending possible primes, less-than-or-equal-to a specified number.</summary>
      private static System.Collections.Generic.IEnumerable<TInteger> GetDescendingPrimeCandidates(TInteger n)
      {
        var zero = TInteger.CreateChecked(0);
        var one = TInteger.CreateChecked(1);
        var two = TInteger.CreateChecked(2);
        var three = TInteger.CreateChecked(3);
        var five = TInteger.CreateChecked(5);
        var six = TInteger.CreateChecked(6);

        if (n < two)
          n = two;

        checked
        {
          var (q, r) = FlooredDivRem(n, six);

          var lo = q * six;

          if (r == five)
            lo += six;

          if (r == zero || r == five)
          {
            try { n = lo - one; } catch { yield break; }
            yield return n;

            try { lo -= six; } catch { yield break; }
          }

          while (lo > zero)
          {
            try { n = lo + one; } catch { break; }
            yield return n;

            try { n = lo - one; } catch { break; }
            yield return n;

            try { lo -= six; } catch { break; }
          }
        }

        if (n >= three)
          yield return three;

        if (n >= two)
          yield return two;
      }

      /// <summary>
      /// <para>Creates a new sequence descending prime numbers, less-than-or-equal-to a specified number.</para>
      /// </summary>
      /// <remarks>This function generate results until the type <typeparamref name="TInteger"/> under/overflows in any calculation. No exception is thrown.</remarks>
      /// <param name="n"></param>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<TInteger> GetDescendingPrimes(TInteger n)
        => GetDescendingPrimeCandidates(n).AsParallel().AsOrdered().Where(IsPrimeNumber);

      /// <summary>
      /// <para>Determines if the number is a prime candidate. If so, it's possible a prime, and if not, it's definitely a composite.</para>
      /// </summary>
      /// <param name="n"></param>
      /// <returns></returns>
      public static bool IsPrimeCandidate(TInteger n)
      {
        if (n < TInteger.CreateChecked(2)) return false; // Less than 2 is no prime.
        if (n <= TInteger.CreateChecked(3)) return true; // Less than or equal to 3, is either 2 or 3, so a prime.

        return n % TInteger.CreateChecked(6) is var m && (m == TInteger.One || m == TInteger.CreateChecked(5)); // All other +-1 of any multiple of 6 is a prime candidate.
      }
      //=> n >= TInteger.CreateChecked(2) && (n <= TInteger.CreateChecked(3) || ());

      /// <summary>
      /// <para>Indicates whether a number is a prime.</para>
      /// <para>This implementation uses a Miller-Rabin deterministic algorithm for numbers less than 64-bit, and a Miller-Rabin probabilistic algorithm for larger numbers.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Primality_test"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Prime_number"/></para>
      /// </summary>
      /// <param name="n"></param>
      /// <returns></returns>
      public static bool IsPrimeNumber(TInteger n)
      {
        var bi = System.Numerics.BigInteger.CreateChecked(n);

        // If less-or-equal-to an unsigned 64-bit value, use Miller-Rabin 64-bit deterministic algorithm.

        if (bi <= ulong.MaxValue)
          return ulong.IsPrime(ulong.CreateChecked(n));

        // Otherwise use Miller-Rabin probabilistic algorithm.

        var log = System.Numerics.BigInteger.Log(bi.GetBitLength(), 1.15); // Log(bit-length, 1.17) yields an approximately 15 iterations @ 10 bits, 30 @ 100, 44 @ 1000, 59 @ 10000, and can be lowered for a higher iteration (k) count.

        return MillerRabinProbabilisticIsPrime(bi, int.CreateChecked(log)); // Pass the log value as k parameter.
      }

      ///// <summary>
      ///// <para>Computes prime candidates for a number.</para>
      ///// </summary>
      ///// <typeparam name="TInteger"></typeparam>
      ///// <param name="n"></param>
      ///// <returns></returns>
      //public static (TInteger TowardZero, TInteger AwayFromZero) GetPossiblePrimes(TInteger n)
      //{
      //  var n3 = TInteger.CreateChecked(3);
      //  var n5 = TInteger.CreateChecked(5);

      //  if (n >= n5)
      //  {
      //    var r = n % TInteger.CreateChecked(6);

      //    if (r == TInteger.One || r == n5) // E.g. 11 or 13 (12 = mod 6)
      //      return (n, n);

      //    if (TInteger.IsZero(r)) // E.g. 12 (mod 6)
      //      return (n - TInteger.One, n + TInteger.One);

      //    return (n - r + TInteger.One, n + n5 - r); // For all others we can use a formula.
      //  }
      //  else if (n == TInteger.CreateChecked(4))
      //    return (n3, n5);
      //  else if (n == n3)
      //    return (n3, n3);
      //  else // Less-than-or-equal-to 2:
      //  {
      //    var n2 = TInteger.CreateChecked(2);

      //    return (n2, n2);
      //  }
      //}

      #endregion

      #region Prime Omega functions

      /// <summary>
      /// <para>The number of prime factors that make up a number.</para>
      /// </summary>
      /// <returns></returns>
      public static (int TotalCount, int DistinctCount) CountPrimeFactors(TInteger n)
      {
        var pf = GetPrimeFactors(n);

        return (pf.Count, pf.Distinct().Count());
      }

      public static System.Collections.Generic.List<TInteger> GetPrimeFactors(TInteger n)
      {
        var primeFactors = new System.Collections.Generic.List<TInteger>();
        GetPrimeFactors(n, primeFactors);
        return (primeFactors);
      }

      /// <summary>
      /// <para>Creates a list of prime factors (a.k.a. divisors) for the <paramref name="value"/> using wheel factorization.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Factorization"/></para>
      /// <para><see href="https://en.wikipedia.org/wiki/Wheel_factorization"/></para>
      /// <para><see href="https://en.wikipedia.org/wiki/Integer_factorization"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Divisor"/></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="value"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public static void GetPrimeFactors(TInteger n, System.Collections.Generic.ICollection<TInteger> primeFactors)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(n);

        var two = TInteger.CreateChecked(2);
        var four = TInteger.CreateChecked(4);
        var six = TInteger.CreateChecked(6);

        var m_primeFactorWheelIncrements = new TInteger[] { four, two, four, two, four, six, two, six };

        while (TInteger.IsZero(n % two))
        {
          primeFactors.Add(two);
          n /= two;
        }

        var three = TInteger.CreateChecked(3);

        while (TInteger.IsZero(n % three))
        {
          primeFactors.Add(three);
          n /= three;
        }

        var five = TInteger.CreateChecked(5);

        while (TInteger.IsZero(n % five))
        {
          primeFactors.Add(five);
          n /= five;
        }

        TInteger k = TInteger.CreateChecked(7), k2 = k * k;

        var index = 0;

        while (k2 <= n)
        {
          if (TInteger.IsZero(n % k))
          {
            primeFactors.Add(k);
            n /= k;
          }
          else
          {
            k += m_primeFactorWheelIncrements[index++];
            k2 = k * k;

            if (index >= m_primeFactorWheelIncrements.Length)
              index = 0;
          }
        }

        if (n > TInteger.One)
          primeFactors.Add(n);
      }

      #endregion

      #region ReverseBits

      /// <summary>
      /// <para>Reverses the bits of an integer. The LSBs (least significant bits) becomes the MSBs (most significant bits) and vice versa, i.e. the bits are mirrored across the integer storage space. It's a reversal of all storage bits.</para>
      /// </summary>
      /// <remarks>See <see cref="ReverseBytes{TInteger}(TInteger)"/> for byte reversal.</remarks>
      public static TInteger ReverseBits(TInteger value)
      {
        var count = value.GetByteCount();

        var bytes = System.Buffers.ArrayPool<byte>.Shared.Rent(count); // Retrieve the byte size of the number, which will be the basis for the bit reversal.

        var span = bytes.AsSpan(0, count);

        value.WriteLittleEndian(span); // Write as LittleEndian (increasing numeric significance in increasing memory addresses).

        for (var i = span.Length - 1; i >= 0; i--)  // After this loop, all bits are reversed.
          byte.ReverseBitsInPlace(ref span[i]); // Mirror (reverse) bits in each byte.

        var result = TInteger.ReadBigEndian(span, value.GetType().IsIUnsignedNumber()); // Read as BigEndian (decreasing numeric significance in increasing memory addresses).

        System.Buffers.ArrayPool<byte>.Shared.Return(bytes);

        return result;
      }

      #endregion

      #region ReverseBytes

      /// <summary>
      /// <para>Reverses the bytes of an integer. The LSBs (least significant bytes) becomes the MSBs (most significant bytes) and vice versa, i.e. the bytes are mirrored across the integer storage space. It's a reversal of all bytes, i.e. all 8-bit segments.</para>
      /// </summary>
      /// <remarks>See <see cref="ReverseBits{TInteger}(TInteger)"/> for bit reversal.</remarks>
      public static TInteger ReverseBytes(TInteger value)
      {
        var count = value.GetByteCount();

        var bytes = System.Buffers.ArrayPool<byte>.Shared.Rent(count); // Retrieve the byte size of the number, which will be the basis for the bit reversal.

        var span = bytes.AsSpan(0, count);

        // We can use either direction here, write-LE/read-BE or write-BE/read-LE, doesn't really matter, since the end result is the same.

        value.WriteLittleEndian(span); // Write as LittleEndian (increasing numeric significance in increasing memory addresses).

        var result = TInteger.ReadBigEndian(span, value.GetType().IsIUnsignedNumber()); // Read as BigEndian (decreasing numeric significance in increasing memory addresses).

        System.Buffers.ArrayPool<byte>.Shared.Return(bytes);

        return result;
      }

      #endregion

      #region RisingFactorial

      /// <summary>
      /// <para>The rising factorial, x^(n), gives the number of partitions of an n-element set into x ordered sequences (possibly empty).</para>
      /// <example>
      /// <para>The "the number of ways to arrange n flags on x flagpoles", where all flags must be used and each flagpole can have any number of flags.</para>
      /// <para>Equivalently, this is the number of ways to partition a set of size n (e.g. 3 flags) into x distinguishable parts (e.g. 2 poles), with a linear order on the elements assigned to each part (the order of the flags on a given pole). <c>RisingFactorial(2, 3);</c></para>
      /// </example>
      /// <para><see href="https://en.wikipedia.org/wiki/Falling_and_rising_factorials"/></para>
      /// </summary>
      /// <param name="x">The base, or starting value of the sequence of factors. Plays the same role as in ordinary factorial‑like expressions.</param>
      /// <param name="n">The order, or number of factors in the product. Must be non-negative. If 0, the defined result is 1.</param>
      /// <returns></returns>
      public static TInteger RisingFactorial(TInteger x, TInteger n)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(n);

        var result = TInteger.One;

        checked
        {
          while (--n >= TInteger.Zero) // Calculate the product x * (x+1) * (x+2) * ... * (x+n-1).
            result *= x++;
        }

        return result;
      }

      #endregion

      #region Root functions (GetSequenceOfNthRoot, IntegerCbrt, IsCbrt, IsPerfectCbrt, IntegerRootN, IsRootN, IsPerfectRootN, IntegerSqrt, IsSqrt, IsPerfectSqrt)

      /// <summary>
      /// <para>Creates a sequence of powers-of-radix values.</para>
      /// </summary>
      /// <typeparam name="TMinMaxInteger"></typeparam>
      /// <param name="nth"></param>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<(TInteger Root, TInteger Number)> GetSequenceOfNthRoot(TInteger nth)
      {
        System.ArgumentOutOfRangeException.ThrowIfLessThan(nth, TInteger.CreateChecked(2));

        checked
        {
          TInteger result;

          foreach (var root in Number.LoopVerge(TInteger.One, TInteger.One))
          {
            try { result = Pow(root, nth); } catch { break; }

            yield return (root, result);
          }
        }
      }

      /// <summary>
      /// <para>Computes the integer (floor) cube-root of a value.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <returns>The square-root of the value.</returns>
      public static TInteger IntegerCbrt(TInteger value)
        => IntegerRootN(value, 3);

      /// <summary>
      /// <para>Indicates whether <paramref name="value"/> is the integer (not necessarily perfect) square of <paramref name="root"/>.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="value">The square value to find the square-<paramref name="root"/> of.</param>
      /// <param name="root">The resulting square-root of <paramref name="value"/>.</param>
      /// <returns>Whether the <paramref name="value"/> is the integer (not necessarily perfect) square of <paramref name="root"/>.</returns>
      public static bool IsIntegerCbrt(TInteger value, TInteger root)
        => value >= (root * root * root) // If GTE to cube of root.
        && value < (root + TInteger.One) * (root + TInteger.One) * (root + TInteger.One); // And if LT to cube of (root + 1).

      /// <summary>
      /// <para>Indicates whether <paramref name="square"/> is a perfect square of <paramref name="root"/>.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="square">The square value to find the square-<paramref name="root"/> of.</param>
      /// <param name="root">The resulting square-root of <paramref name="square"/>.</param>
      /// <returns>Whether the <paramref name="square"/> is a perfect square of <paramref name="root"/>.</returns>
      /// <remarks>Not using "y == (x * x)" because risk of overflow.</remarks>
      public static bool IsPerfectIntegerCbrt(TInteger value, TInteger root)
        => value == (root * root * root);

      /// <summary>
      /// <para>Computes the integer nth-root of a value.</para>
      /// </summary>
      /// <typeparam name="TNth"></typeparam>
      /// <param name="value"></param>
      /// <param name="exponent"></param>
      /// <returns></returns>
      public static TInteger IntegerRootN<TNth>(TInteger value, TNth nth)
        where TNth : System.Numerics.IBinaryInteger<TNth>
        => TInteger.CreateChecked(NewtonRaphsonRootN(System.Numerics.BigInteger.CreateChecked(value), int.CreateChecked(nth)));

      public static bool IsIntegerRootN<TNth>(TInteger value, TNth n, TInteger root)
        where TNth : System.Numerics.IBinaryInteger<TNth>
        => value >= Pow(root, n) // If GTE to nth of root.
        && value < Pow(root + TInteger.One, n); // And if LT to nth of (root + 1).

      public static bool IsPerfectIntegerRootN<TNth>(TInteger value, TNth n, TInteger root)
        where TNth : System.Numerics.IBinaryInteger<TNth>
        => value == Pow(root, n);

      /// <summary>
      /// <para>Computes the integer square-root of a value.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <returns></returns>
      public static TInteger IntegerSqrt(TInteger value)
        => IntegerRootN(value, 2);

      /// <summary>
      /// <para>Indicates whether <paramref name="value"/> is the integer (not necessarily perfect) square of <paramref name="root"/>.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="value">The square value to find the square-<paramref name="root"/> of.</param>
      /// <param name="root">The resulting square-root of <paramref name="value"/>.</param>
      /// <returns>Whether the <paramref name="value"/> is the integer (not necessarily perfect) square of <paramref name="root"/>.</returns>
      public static bool IsIntegerSqrt(TInteger value, TInteger root)
        => value >= (root * root) // If GTE to square of root.
        && value < (root + TInteger.One) * (root + TInteger.One); // And if LT to square of (root + 1).

      /// <summary>
      /// <para>Indicates whether <paramref name="square"/> is a perfect square of <paramref name="root"/>.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="square">The square value to find the square-<paramref name="root"/> of.</param>
      /// <param name="root">The resulting square-root of <paramref name="square"/>.</param>
      /// <returns>Whether the <paramref name="square"/> is a perfect square of <paramref name="root"/>.</returns>
      /// <remarks>Not using "y == (x * x)" because risk of overflow.</remarks>
      public static bool IsPerfectIntegerSqrt(TInteger value, TInteger root)
        => value == (root * root);

      #endregion

      #region Round..ToPowerOf2

      public static TInteger RoundUpToPowerOf2(TInteger value, bool unequal)
      {
        var ms1b = MostSignificant1Bit(TInteger.Abs(value));

        if (unequal || ms1b != value)
          ms1b <<= 1;

        return TInteger.CopySign(ms1b, value);
      }

      public static TInteger RoundDownToPowerOf2(TInteger value, bool unequal)
      {
        var ms1b = MostSignificant1Bit(TInteger.Abs(value));

        if (unequal && ms1b == value)
          ms1b >>= 1;

        return TInteger.CopySign(ms1b, value);
      }

      #endregion

      #region Sheffer polynomial/sequence

      /// <summary>
      /// <para>Compute the nth Sheffer polynomial at x.</para>
      /// </summary>
      /// <param name="n">The degree of the polynomial.</param>
      /// <param name="x">The variable for which the polynomial is evaluated.</param>
      /// <returns></returns>
      public static TFloat ShefferPolynomial<TFloat>(TInteger n, TFloat x)
        where TFloat : System.Numerics.IFloatingPoint<TFloat>
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(n);

        if (TInteger.IsZero(n))
          return TFloat.One; // P_0(x) = 1

        if (n == TInteger.One)
          return x; // P_1(x) = x

        // Recurrence relation: P_n(x) = x * P_(n-1)(x) - (n-1) * P_(n-2)(x)

        var prev1 = x; // P_1(x)
        var prev2 = TFloat.One; // P_0(x)
        var current = TFloat.Zero;

        for (var i = TInteger.CreateChecked(2); i <= n; i++)
        {
          current = x * prev1 - TFloat.CreateChecked(i - TInteger.One) * prev2;
          prev2 = prev1;
          prev1 = current;
        }

        return current;
      }

      #endregion

      #region ShuffleBytes

      /// <summary>
      /// <para>Shuffles all bytes of an integer.</para>
      /// </summary>
      public static TInteger ShuffleBytes(TInteger value, System.Random? rng = null)
      {
        rng ??= System.Random.Shared;

        var bytes = System.Buffers.ArrayPool<byte>.Shared.Rent(value.GetByteCount());

        value.WriteLittleEndian(bytes);

        rng.Shuffle(bytes);

        var result = TInteger.ReadLittleEndian(bytes, value.GetType().IsIUnsignedNumber());

        System.Buffers.ArrayPool<byte>.Shared.Return(bytes);

        return result;
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

      #endregion

      #region ..Significant1Bit

      /// <summary>
      /// <para>Clear <paramref name="value"/> of its least-significant-1-bit.</para>
      /// </summary>
      /// <see href="https://aggregate.org/MAGIC/#Least%20Significant%201%20Bit"/>
      public static TInteger ClearLeastSignificant1Bit(TInteger value)
        => value & (value - TInteger.One);

      /// <summary>
      /// <para>Clear <paramref name="value"/> of its least-significant-1-bit.</para>
      /// </summary>
      /// <see href="https://aggregate.org/MAGIC/#Most%20Significant%201%20Bit"/>
      public static TInteger ClearMostSignificant1Bit(TInteger value)
        => value - MostSignificant1Bit(value);

      /// <summary>
      /// <para>Extracts the lowest numbered element of a bit set (<paramref name="value"/>). Given a 2's complement binary integer value, this is the least-significant-1-bit.</para>
      /// </summary>
      /// <remarks>The LS1B is the largest power of two that is also a divisor of <paramref name="value"/>.</remarks>
      /// <see href="https://aggregate.org/MAGIC/#Least%20Significant%201%20Bit"/>
      public static TInteger LeastSignificant1Bit(TInteger value)
        => value & ((~value) + TInteger.One);
      //=> (value & -value); // <<< This optimized version does not work on unsigned integers, obviously since the number has to be negated.

      /// <summary>
      /// <para>Extracts the highest numbered element of a bit set (<paramref name="value"/>). Given a 2's complement binary integer value, this is the most-significant-1-bit.</para>
      /// <list type="bullet">
      /// <item>If <paramref name="value"/> equal zero, zero is returned.</item>
      /// <item>If <paramref name="value"/> is negative, min-value of the signed type is returned (i.e. the top most-significant-bit that the type is able to represent).</item>
      /// <item>Otherwise the most-significant-1-bit is returned, which also happens to be the same as Log2(<paramref name="value"/>).</item>
      /// </list>
      /// </summary>
      /// <remarks>Note that for dynamic types, e.g. <see cref="System.Numerics.BigInteger"/>, the number of bits depends on the storage size used for the <paramref name="value"/>.</remarks>
      public static TInteger MostSignificant1Bit(TInteger value)
        => TInteger.IsZero(value) ? value : TInteger.One << (value.GetBitLength() - 1);

#if INCLUDE_SCRATCH

            public static TInteger ScratchLeastSignificant1Bit(TInteger value)
              => value & ((~value) + TInteger.One); // Works on signed or unsigned integers.
            // => (value ^ (value & (value - TInteger.One))); // Alternative to the above.
            // => (value & -value); // Does not work on unsigned integers.

            public static TInteger ScratchMostSignificant1Bit(TInteger value)
            {
              value = ScratchBitFoldRight(value);

              return value & ~(value >> 1);
            }

#endif

      #endregion

      #region GetSphenicNumbers

      /// <summary>
      /// <para>Yields a sequence of all sphenic numbers less than <paramref name="n"/>.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Sphenic_number"/></para>
      /// </summary>
      /// <typeparam name="TSelf"></typeparam>
      /// <param name="n"></param>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<TInteger> GetSphenicNumbers(TInteger n)
      {
        checked
        {
          for (var i = TInteger.CreateChecked(30); i < n; i++)
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

              if (count == 0 && j > n / (j * j))
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

      #region Stirling numbers

      /// <summary>
      /// <para>Stirling numbers of the first kind arise in the study of permutations. In particular, the unsigned Stirling numbers of the first kind count permutations according to their number of cycles (counting fixed points as cycles of length one).</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Stirling_number"/></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="n"></param>
      /// <param name="k"></param>
      /// <param name="dp"></param>
      /// <returns></returns>
      public static TInteger StirlingNumber1stKind(TInteger n, TInteger k, out TInteger[,]? dp)
      {
        if (TInteger.IsNegative(n) || TInteger.IsNegative(k) || k > n)
        {
          dp = null;

          return TInteger.Zero;
        }

        var ni = int.CreateChecked(n);
        var ki = int.CreateChecked(k);

        dp = new TInteger[ni + 1, ki + 1];

        dp[0, 0] = TInteger.One; // c(0, 0) = 1

        for (var i = 1; i <= ni; i++)
          dp[i, 0] = TInteger.Zero; // c(n, 0) = 0 for n > 0

        for (var j = 1; j <= ki; j++)
          dp[0, j] = TInteger.Zero; // c(0, k) = 0 for k > 0

        for (var i = 1; i <= ni; i++)
          for (var j = 1; j <= ki; j++)
            dp[i, j] = dp[i - 1, j - 1] + TInteger.CreateChecked(i - 1) * dp[i - 1, j]; // Fill the table using the recurrence relation.

        return dp[ni, ki];
      }

      public static TInteger StirlingNumber1stKind(TInteger n, TInteger k)
        => StirlingNumber1stKind(n, k, out var _);

      /// <summary>
      /// <para>a Stirling number of the second kind (or Stirling partition number) is the number of ways to partition a set of n objects into k non-empty subsets.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Stirling_number"/></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="n"></param>
      /// <param name="k"></param>
      /// <returns></returns>
      public static TInteger StirlingNumber2ndKind(TInteger n, TInteger k)
      {
        var sum = TInteger.Zero;
        var neg = TInteger.One;

        if ((TInteger.IsZero(n) ^ TInteger.IsZero(k)) || (k > n)) return sum;
        if (n == k) return neg;

        checked
        {
          for (var i = sum; i <= k; i++)
          {
            sum += neg * BinomialCoefficient(k, i) * Pow(k - i, n);
            neg = -neg;
          }
        }

        sum /= Factorial(k);

        return sum;
      }

      #endregion

      #region ToFractionalPart

      /// <summary>
      /// <para>Converts an integer value to a decimal fraction, e.g. "123 => 0.123".</para>
      /// </summary>
      /// <typeparam name="TRadix"></typeparam>
      /// <param name="value"></param>
      /// <param name="radix"></param>
      /// <returns></returns>
      public static decimal ToFractionalPart<TRadix>(TInteger value, TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        var digitCount = Units.Radix.DigitCount(value, radix); // Digit count of the "integer part", e.g. an integer 123 = 3 digits.

        var fractionalPart = Pow(radix, digitCount); // With the digit count we can create a power-of-radix of the same magnitude as the digit count, e.g. 3 digits = 1000 (radix = 10).

        return decimal.CreateChecked(value) / decimal.CreateChecked(fractionalPart); // E.g. 123. / 1000 = .123 
      }

      #endregion

      #region Twelvefold way

      /// <summary>
      /// <para><see href="https://en.wikipedia.org/wiki/Twelvefold_way"/></para>
      /// </summary>
      /// <param name="x"></param>
      /// <param name="n"></param>
      /// <returns></returns>
      public static TInteger AnyDistinct(TInteger x, TInteger n) => Pow(x, n);

      /// <summary>
      /// <para><see href="https://en.wikipedia.org/wiki/Twelvefold_way"/></para>
      /// </summary>
      /// <param name="x"></param>
      /// <param name="n"></param>
      /// <returns></returns>
      public static TInteger InjectiveDistinct(TInteger x, TInteger n) => FallingFactorial(x, n);

      /// <summary>
      /// <para><see href="https://en.wikipedia.org/wiki/Twelvefold_way"/></para>
      /// </summary>
      /// <param name="x"></param>
      /// <param name="n"></param>
      /// <returns></returns>
      public static TInteger SurjectiveDistinct(TInteger x, TInteger n) => Factorial(x) * StirlingNumber2ndKind(n, x);

      /// <summary>
      /// <para><see href="https://en.wikipedia.org/wiki/Twelvefold_way"/></para>
      /// </summary>
      /// <param name="x"></param>
      /// <param name="n"></param>
      /// <returns></returns>
      public static TInteger AnySnOrbits(TInteger x, TInteger n) => BinomialCoefficient(x + n - TInteger.One, n);

      /// <summary>
      /// <para><see href="https://en.wikipedia.org/wiki/Twelvefold_way"/></para>
      /// </summary>
      /// <param name="x"></param>
      /// <param name="n"></param>
      /// <returns></returns>
      public static TInteger InjectiveSnOrbits(TInteger x, TInteger n) => BinomialCoefficient(x, n);

      /// <summary>
      /// <para><see href="https://en.wikipedia.org/wiki/Twelvefold_way"/></para>
      /// </summary>
      /// <param name="x"></param>
      /// <param name="n"></param>
      /// <returns></returns>
      public static TInteger SurjectiveSnOrbits(TInteger x, TInteger n) => BinomialCoefficient(n - TInteger.One, n - x);

      /// <summary>
      /// <para><see href="https://en.wikipedia.org/wiki/Twelvefold_way"/></para>
      /// </summary>
      /// <param name="x"></param>
      /// <param name="n"></param>
      /// <returns></returns>
      public static TInteger AnySxOrbits(TInteger x, TInteger n) => throw new System.NotImplementedException();

      /// <summary>
      /// <para><see href="https://en.wikipedia.org/wiki/Twelvefold_way"/></para>
      /// </summary>
      /// <param name="x"></param>
      /// <param name="n"></param>
      /// <returns></returns>
      public static TInteger InjectiveSxOrbits(TInteger x, TInteger n) => throw new System.NotImplementedException();

      /// <summary>
      /// <para><see href="https://en.wikipedia.org/wiki/Twelvefold_way"/></para>
      /// </summary>
      /// <param name="x"></param>
      /// <param name="n"></param>
      /// <returns></returns>
      public static TInteger SurjectiveSxOrbits(TInteger x, TInteger n) => StirlingNumber2ndKind(n, x);

      /// <summary>
      /// <para><see href="https://en.wikipedia.org/wiki/Twelvefold_way"/></para>
      /// </summary>
      /// <param name="x"></param>
      /// <param name="n"></param>
      /// <returns></returns>
      public static TInteger AnySnSxOrbits(TInteger x, TInteger n) => throw new System.NotImplementedException();

      /// <summary>
      /// <para><see href="https://en.wikipedia.org/wiki/Twelvefold_way"/></para>
      /// </summary>
      /// <param name="x"></param>
      /// <param name="n"></param>
      /// <returns></returns>
      public static TInteger InjectiveSnSxOrbits(TInteger x, TInteger n) => throw new System.NotImplementedException();

      /// <summary>
      /// <para><see href="https://en.wikipedia.org/wiki/Twelvefold_way"/></para>
      /// </summary>
      /// <param name="x"></param>
      /// <param name="n"></param>
      /// <returns></returns>
      public static TInteger SurjectiveSnSxOrbits(TInteger x, TInteger n) => throw new System.NotImplementedException();

      #endregion

      #region Van Eck's sequence

      /// <summary>
      /// <para>Creates a new Van Eck's sequence, starting with the specified <paramref name="n"/> (where 0 yields the original sequence).</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Van_Eck%27s_sequence"/></para>
      /// </summary>
      /// <param name="n"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public static System.Collections.Generic.IEnumerable<TInteger> GetVanEckSequence(TInteger n)
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

      #endregion
    }

    extension<TInteger>(TInteger value) // Instance type members.
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      #region GetBitLength

      /// <summary>
      /// <para>Gets the size, in bits, of the shortest two's-complement representation, if <paramref name="value"/> is positive. If <paramref name="value"/> is negative, the bit-length represents the storage size of the <typeparamref name="TInteger"/>, based on byte-count (times 8).</para>
      /// </summary>
      /// <remarks>
      /// <para>The <c>bit-length(<paramref name="value"/>)</c> is the bit position (i.e. a 1-based bit-index) of the <c>most-significant-1-bit(<paramref name="value"/>)</c>. A zero-based bit-index is equal to <c>(bit-length(<paramref name="value"/>) - 1)</c>, which is also the same as calling <c>log2(<paramref name="value"/>)</c>.</para>
      /// </remarks>
      public int GetBitLength()
        => TInteger.IsNegative(value)
        ? value.GetBitCount() // When value is negative, return the bit-count (i.e. based on the storage strategy).
        : value.GetShortestBitLength(); // Otherwise, return the .NET shortest-bit-length.

#if INCLUDE_SCRATCH

      /// <summary>
      /// <para><see href="https://aggregate.org/MAGIC/#Log2%20of%20an%20Integer"/></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="value"></param>
      /// <returns></returns>
      public TInteger ScratchBitLength()
        => ScratchLog2(value) + TInteger.One;

#endif

      #endregion

      #region Get..Count()

      /// <summary>
      /// <para>Returns the size, in number of bits, needed to store <paramref name="value"/>.</para>
      /// <para>Most types returns the underlying storage size of the type itself, e.g. <see langword="int"/> = 32 or <see langword="long"/> = 64.</para>
      /// </summary>
      /// <remarks>
      /// <para>Some data types, e.g. <see cref="System.Numerics.BigInteger"/>, use dynamic storage strategies, making <see cref="GetBitCount{TValue}"/> dynamic, and depends on the actual number stored.</para>
      /// </remarks>
      public int GetBitCount()
        => value.GetByteCount() * 8;

      ///// <summary>
      ///// <para>Using the built-in <see cref="System.Numerics.IBinaryInteger{TInteger}.GetByteCount(TInteger)"/>.</para>
      ///// </summary>
      ///// <remarks>
      ///// <para>Note that some datatypes, e.g. <see cref="System.Numerics.BigInteger"/>, use dynamic storage strategies, making <see cref="GetByteCount{TValue}"/> dynamic also.</para>
      ///// </remarks>
      //public int GetByteCount()
      //  => value.GetByteCount();

      ///// <summary>
      ///// <para>Using the built-in <see cref="System.Numerics.IBinaryInteger{TInteger}.PopCount(TInteger)"/>.</para>
      ///// </summary>
      ///// <returns>The population count of <paramref name="value"/>, i.e. the number of bits set to 1 in <paramref name="value"/>.</returns>
      //public int GetPopCount()
      //  => int.CreateChecked(TInteger.PopCount(value));

#if INCLUDE_SCRATCH

      public int ScratchGetPopCount()
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(value);

        var count = 0;

        while (value > TInteger.Zero)
        {
          count++;

          value &= value - TInteger.One; // Clear the LS1B.
        }

        return count;
      }

#endif

      #endregion

      #region To..OrdinalColumnName(s)

      /// <summary>
      /// <para>Returns a generic <paramref name="columnNamePrefix"/> for the <paramref name="value"/> as if it was an index of a 0-based column-structure.</para>
      /// <para>+1 is added to the <paramref name="value"/> so that the first column (the zeroth) is always "Column1", and the second column (#1) is "Column2", i.e. the column names are ordinal.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="value"></param>
      /// <param name="numericWidth"></param>
      /// <param name="columnNamePrefix"></param>
      /// <returns></returns>
      public string ToSingleOrdinalColumnName(int numericWidth, string columnNamePrefix = "Column")
        => columnNamePrefix + (value + TInteger.One).ToString($"D{numericWidth}", null);

      /// <summary>
      /// <para>Returns a generic <paramref name="columnNamePrefix"/> for the <paramref name="value"/> as if it was an index of a 0-based column-structure.</para>
      /// <para>+1 is added to the <paramref name="value"/> so that the first column (the zeroth) is always "Column1", and the second column (#1) is "Column2", i.e. the column names are ordinal.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="value"></param>
      /// <param name="columnNamePrefix"></param>
      /// <returns></returns>
      public string ToSingleOrdinalColumnName(string columnNamePrefix = "Column")
        => value.ToSingleOrdinalColumnName(int.CreateChecked(value <= TInteger.Zero ? TInteger.Zero : Units.Radix.DigitCount(value, TInteger.CreateChecked(10))), columnNamePrefix);

      /// <summary>
      /// <para>Creates an array of generic column-<paramref name="columnNamePrefix"/>s for <paramref name="value"/> amount of columns.</para>
      /// <example><paramref name="value"/> = 3, returns <c>["Column1", "Column2", "Column3"]</c></example>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="value"></param>
      /// <param name="columnNamePrefix"></param>
      /// <returns></returns>
      public string[] ToMultipleOrdinalColumnNames(string columnNamePrefix = "Column")
      {
        var maxWidth = int.CreateChecked(Units.Radix.DigitCount(value, 10));

        return [.. System.Linq.Enumerable.Range(1, int.CreateChecked(value)).Select(i => i.ToSingleOrdinalColumnName(maxWidth, columnNamePrefix))];
      }

      #endregion

      /// <summary>
      /// <para>Converts a <paramref name="value"/> to a binary (base 2) string based on <paramref name="minLength"/> and an <paramref name="alphabet"/> (<see cref="Base64Alphabet"/> if null).</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="value"></param>
      /// <param name="minLength"></param>
      /// <param name="alphabet"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public string ToBinaryString(int minLength = 1, string? alphabet = null)
      {
        if (minLength <= 0) minLength = value.GetBitCount();

        alphabet ??= Units.Radix.Base62;

        if (alphabet.Length < 2) throw new System.ArgumentOutOfRangeException(nameof(alphabet));

        var indices = new System.Collections.Generic.List<int>();

        for (var bitIndex = int.Min(int.Max(value.GetBitLength(), minLength), value.GetBitCount()) - 1; bitIndex >= 0; bitIndex--)
        {
          var bitValue = int.CreateChecked((value >>> bitIndex) & TInteger.One);

          if (bitValue > 0 || indices.Count > 0 || bitIndex < minLength)
            indices.Add(bitValue);
        }

        indices.TryTransposePositionalNotationIndicesToSymbols(alphabet, out System.Collections.Generic.List<char> symbols);

        return symbols.AsSpan().ToString();
      }

      /// <summary>
      /// <para>Converts a <paramref name="value"/> to a decimal (base 10) string based on <paramref name="minLength"/>, <paramref name="negativeSymbol"/> and an <paramref name="alphabet"/> (<see cref="Base64Alphabet"/> if null).</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="value"></param>
      /// <param name="minLength"></param>
      /// <param name="negativeSymbol"></param>
      /// <param name="alphabet">If <see langword="null"/> then <see cref="Units.Radix.Base62"/>.</param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public string ToDecimalString(int minLength = 1, char negativeSymbol = '\u002D', string? alphabet = null)
      {
        if (minLength <= 0) minLength = Units.Radix.GetMaxDigitCount(value.GetBitCount(), 10, value.GetType().IsISignedNumber());

        alphabet ??= Units.Radix.Base62;

        if (alphabet.Length < 10) throw new System.ArgumentOutOfRangeException(nameof(alphabet));

        var abs = TInteger.Abs(value);

        abs.TryConvertNumberToPositionalNotationIndices(10, out var indices);

        while (indices.Count < minLength)
          indices.Insert(0, 0); // Pad left with zeroth element.

        indices.TryTransposePositionalNotationIndicesToSymbols(alphabet, out System.Collections.Generic.List<char> symbols);

        if (TInteger.IsNegative(value))
          symbols.Insert(0, negativeSymbol); // If the value is negative AND base-2 (radix) is 10 (decimal)...

        return symbols.AsSpan().ToString();
      }

      /// <summary>
      /// <para>Converts a <paramref name="value"/> to a hexadecimal (base 16) string based on <paramref name="minLength"/> and an <paramref name="alphabet"/> (<see cref="Units.Radix.Base62"/> if null).</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="value"></param>
      /// <param name="minLength"></param>
      /// <param name="alphabet">If <see langword="null"/> then <see cref="Units.Radix.Base62"/>.</param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public string ToHexadecimalString(int minLength = 1, string? alphabet = null)
      {
        if (minLength <= 0) minLength = Units.Radix.GetMaxDigitCount(value.GetBitCount(), 16, value.GetType().IsISignedNumber());

        alphabet ??= Units.Radix.Base62;

        if (alphabet.Length < 16) throw new System.ArgumentOutOfRangeException(nameof(alphabet));

        var indices = new System.Collections.Generic.List<int>();

        for (var nibbleIndex = (value.GetByteCount() << 1) - 1; nibbleIndex >= 0; nibbleIndex--)
        {
          var nibbleValue = int.CreateChecked((value >>> (nibbleIndex << 2)) & TInteger.CreateChecked(0xF));

          if (nibbleValue > 0 || indices.Count > 0 || nibbleIndex < minLength)
            indices.Add(nibbleValue);
        }

        indices.TryTransposePositionalNotationIndicesToSymbols(alphabet, out System.Collections.Generic.List<char> symbols);

        return symbols.AsSpan().ToString();
      }

      /// <summary>
      /// <para>Converts a <paramref name="value"/> to a octal (base 8) string based on <paramref name="minLength"/> and an <paramref name="alphabet"/> (<see cref="Base64Alphabet"/> if null).</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="value"></param>
      /// <param name="minLength"></param>
      /// <param name="alphabet">If <see langword="null"/> then <see cref="Units.Radix.Base62"/>.</param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public string ToOctalString(int minLength = 1, string? alphabet = null)
      {
        if (minLength <= 0) minLength = Units.Radix.GetMaxDigitCount(value.GetBitCount(), 8, value.GetType().IsISignedNumber());

        alphabet ??= Units.Radix.Base62;

        if (alphabet.Length < 8) throw new System.ArgumentOutOfRangeException(nameof(alphabet));

        value.TryConvertNumberToPositionalNotationIndices(8, out var indices);

        while (indices.Count < minLength)
          indices.Insert(0, 0); // Pad left with zeroth element.

        indices.TryTransposePositionalNotationIndicesToSymbols(alphabet, out System.Collections.Generic.List<char> symbols);

        return symbols.AsSpan().ToString();
      }

      /// <summary>
      /// <para>Creates a new string with <paramref name="value"/> and its ordinal indicator. E.g. "1st" for 1 and "122nd" for 122.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="value"></param>
      /// <returns></returns>
      public string ToOrdinalIndicatorString()
        => value.ToString() + GetOrdinalIndicatorSuffix(value);

      /// <summary>
      /// <para>Converts a <paramref name="value"/> to text based on <paramref name="radix"/>, <paramref name="minLength"/> and an <paramref name="alphabet"/> (<see cref="Base64Alphabet"/> if null).</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <typeparam name="TRadix"></typeparam>
      /// <param name="value"></param>
      /// <param name="radix"></param>
      /// <param name="minLength"></param>
      /// <param name="alphabet">If <see langword="null"/> then <see cref="Units.Radix.Base62"/>.</param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public string ToRadixString<TRadix>(TRadix radix, int minLength = 1, string? alphabet = null)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        var rdx = int.CreateChecked(Units.Radix.AssertMember(radix));

        if (rdx == 2)
          return value.ToBinaryString(minLength, alphabet);
        else if (rdx == 8)
          return value.ToOctalString(minLength, alphabet);
        else if (rdx == 10)
          return value.ToDecimalString(minLength, alphabet: alphabet);
        else if (rdx == 16)
          return value.ToHexadecimalString(minLength, alphabet);
        else
        {
          if (minLength <= 0) minLength = Units.Radix.GetMaxDigitCount(value.GetBitCount(), rdx, value.GetType().IsISignedNumber());

          alphabet ??= Units.Radix.Base62;

          if (alphabet.Length < rdx) throw new System.ArgumentOutOfRangeException(nameof(alphabet));

          value.TryConvertNumberToPositionalNotationIndices(radix, out var indices);

          while (indices.Count < minLength)
            indices.Insert(0, 0); // Pad left with zeroth element.

          indices.TryTransposePositionalNotationIndicesToSymbols(alphabet, out System.Collections.Generic.List<char> symbols);

          return symbols.AsSpan().ToString();
        }
      }

      /// <summary>
      /// <para>Converts a <paramref name="value"/> to subscript text using <paramref name="radix"/> (base) and a <paramref name="minLength"/>.</para>
      /// </summary>
      /// <remarks>Subscript can operate with up to base-10 (<paramref name="radix"/>).</remarks>
      /// <typeparam name="TRadix"></typeparam>
      /// <param name="radix"></param>
      /// <param name="minLength"></param>
      /// <returns></returns>
      public string ToSubscriptString<TRadix>(TRadix radix, int minLength = 1)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        var alphabet = "\u2080\u2081\u2082\u2083\u2084\u2085\u2086\u2087\u2088\u2089";

        return ToRadixString(value, Units.Radix.AssertMember(radix, TRadix.CreateChecked(alphabet.Length)), minLength, alphabet); // Extra top-limit to radix (only 10 characters in subscript alphabet).
      }

      /// <summary>
      /// <para>Creates a new superscript string from an integer in the specified <paramref name="radix"/> (base) and <paramref name="minLength"/>.</para>
      /// </summary>
      /// <remarks>Superscript can operate with up to base-16 (<paramref name="radix"/>).</remarks>
      /// <typeparam name="TRadix"></typeparam>
      /// <param name="radix"></param>
      /// <param name="minLength"></param>
      /// <param name="upperCase"></param>
      /// <returns></returns>
      public string ToSuperscriptString<TRadix>(TRadix radix, int minLength = 1, bool upperCase = false)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        var alphabet = "\u2070\u00B9\u00B2\u00B3\u2074\u2075\u2076\u2077\u2078\u2079";

        alphabet += upperCase ? "\u1D2C\u1D2E\uA7F2\u1D30\u1D31\uA7F3" : "\u1D43\u1D47\u1D9C\u1D48\u1D49\u1DA0";

        return ToRadixString(value, Units.Radix.AssertMember(radix, TRadix.CreateChecked(alphabet.Length)), minLength, alphabet); // Extra top-limit to radix (only 16 characters in superscript alphabet, but choice of lower/upper case).
      }
    }

    #region ..DeBruijnSequence.. (has nested methods)

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
    public static int DeBruijnSequenceLength(int k, int n)
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
      var sequence = new System.Collections.Generic.List<int>(DeBruijnSequenceLength(k, n));

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

    #region ..Factorial (private helpers)

    /// <summary>
    /// <para>Computes the factorial of <paramref name="value"/>, e.g. <c>Factorial(5) => 1 * 2 * 3 * 4 * 5</c></para>
    /// <para><see href="https://en.wikipedia.org/wiki/Factorial"/></para>
    /// </summary>
    /// <remarks>This plain-and-simple iterative version of factorials is faster with numbers smaller than 60 or so, and starts loosing with larger numbers.</remarks>
    /// <typeparam name="TInteger"></typeparam>
    private static TInteger NaiveFactorial<TInteger>(TInteger n)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      if (TryStaticFactorial(n, out var f))
        return f;

      f = TInteger.One;

      if (n > f) // Only loop if value is greater than 1.
        checked
        {
          f++;

          for (var m = f + TInteger.One; m <= n; m++)
            f *= m;
        }

      return f;
    }

    /// <summary>
    /// <para>Compute the factorial using divide-and-conquer, a.k.a. split-factorial of <paramref name="value"/>.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Factorial"/></para>
    /// <para><see href="http://www.luschny.de/math/factorial/csharp/FactorialSplit.cs.html"/></para>
    /// </summary>
    /// <typeparam name="TInteger"></typeparam>
    private static TInteger SplitFactorial<TInteger>(this TInteger n)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      if (TryStaticFactorial(n, out var f))
        return f;

      var two = (TInteger.One + TInteger.One);

      var p = TInteger.One;
      var r = TInteger.One;
      var currentN = TInteger.One;

      var h = TInteger.Zero;
      var shift = TInteger.Zero;
      var high = TInteger.One;

      var log2n = int.CreateChecked(TInteger.Log2(n));

      while (h != n)
        checked
        {
          shift += h;
          h = n >>> log2n--;
          var len = high;
          high = (h - TInteger.One) | TInteger.One;
          len = (high - len) >>> 1;

          if (len > TInteger.Zero)
          {
            p *= Product(len);
            r *= p;
          }
        }

      return r << int.CreateChecked(shift);

      TInteger Product(TInteger n)
      {
        checked
        {
          var m = n >> 1;

          if (TInteger.IsZero(m))
            return currentN += two;

          if (n == two)
            return (currentN += two) * (currentN += two);

          return Product(n - m) * Product(m);
        }
      }
    }

    private readonly static long[] StaticFactorials = [1, 1, 2, 6, 24, 120, 720, 5040, 40320, 362880, 3628800, 39916800, 479001600, 6227020800, 87178291200, 1307674368000, 20922789888000, 355687428096000, 6402373705728000, 121645100408832000, 2432902008176640000];

    private static TInteger StaticFactorial<TInteger>(TInteger n)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      System.ArgumentOutOfRangeException.ThrowIfNegative(n);

      if (n < TInteger.CreateChecked(StaticFactorials.Length))
        return TInteger.CreateChecked(StaticFactorials[int.CreateChecked(n)]);

      return TInteger.Zero;
    }

    private static bool TryStaticFactorial<TInteger>(TInteger n, out TInteger factorial)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      try
      {
        factorial = StaticFactorial(n);
      }
      catch
      {
        factorial = TInteger.Zero;
      }

      return !TInteger.IsZero(factorial);
    }

    #endregion

    #region MillerRabinProbabilistic.. (private helpers, BigInteger primality test)

    /// <summary>
    /// <para>Probabilistic Miller–Rabin primality test with parallel rounds.</para>
    /// </summary>
    /// <param name="n"></param>
    /// <param name="k"></param>
    /// <returns></returns>
    private static bool MillerRabinProbabilisticIsPrime(System.Numerics.BigInteger n, int k)
    {
      if (n <= 3) return n == 2 || n == 3;
      if ((n % 2).IsZero) return false;

      // Write n-1 as d*2^r
      var d = n - 1;
      while (d % 2 == 0) d /= 2;

      var isPrime = true;
      var lockObj = new object();

      System.Threading.Tasks.Parallel.For(0, k, (i, state) =>
      {
        if (!isPrime) { state.Stop(); return; }

        System.Numerics.BigInteger a; // Random base in [2, n-2]

        lock (lockObj) { a = System.Random.Shared.NextNumber(2, n - 2); }

        if (!MillerRabinProbabilisticTest(d, n, a))
        {
          lock (lockObj) { isPrime = false; }

          state.Stop();
        }
      });

      return isPrime;
    }

    /// <summary>
    /// <para>Miller–Rabin test for a single base</para>
    /// </summary>
    /// <param name="d"></param>
    /// <param name="n"></param>
    /// <param name="a"></param>
    /// <returns></returns>
    private static bool MillerRabinProbabilisticTest(System.Numerics.BigInteger d, System.Numerics.BigInteger n, System.Numerics.BigInteger a)
    {
      var x = System.Numerics.BigInteger.ModPow(a, d, n);

      if (x == 1 || x == n - 1) return true;

      while (d != n - 1)
      {
        x = (x * x) % n;
        d *= 2;

        if (x == 1) return false;
        if (x == n - 1) return true;
      }

      return false;
    }

    #endregion

    #region NewtonRaphsonRootN (private helpers, faster than binary-search)

    private static System.Numerics.BigInteger NewtonRaphsonRootN(System.Numerics.BigInteger value, int n)
    {
      System.ArgumentOutOfRangeException.ThrowIfNegative(value);
      System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(n);

      if (value == 0 || value == 1 || n == 1)
        return value;

      var guess = System.Numerics.BigInteger.One << (int)(System.Numerics.BigInteger.Log(value) / n); // Initial guess: 2^(log2(x)/n)

      while (true)
      {
        var previousGuess = guess;

        var t = System.Numerics.BigInteger.Pow(guess, n - 1);

        if (t == 0)
          throw new System.ArithmeticException(); // Avoid division by zero (shouldn't happen for valid inputs).

        guess = ((n - 1) * guess + value / t) / n; // Newton iteration.

        if (System.Numerics.BigInteger.Abs(guess - previousGuess) <= 1) // Adjust to ensure r^n <= x
        {
          while (System.Numerics.BigInteger.Pow(guess + 1, n) <= value)
            guess++;

          while (System.Numerics.BigInteger.Pow(guess, n) > value)
            guess--;

          return guess;
        }
      }
    }

    #endregion

    #region BinarySearchRootN (private helpers, currently unused, remarked out, not as fast as Newton-Raphson)

    //private static System.Numerics.BigInteger BinarySearchRootN(System.Numerics.BigInteger value, int n)
    //{
    //  System.ArgumentOutOfRangeException.ThrowIfNegative(value);
    //  System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(n);

    //  if (value == 0 || value == 1 || n == 1)
    //    return value;

    //  var low = System.Numerics.BigInteger.One;
    //  var high = value;
    //  var result = System.Numerics.BigInteger.One;

    //  while (low <= high)
    //  {
    //    var mid = (low + high) / 2;
    //    var midPow = System.Numerics.BigInteger.Pow(mid, n);

    //    if (midPow == value)
    //      return mid;
    //    else if (midPow < value)
    //    {
    //      result = mid;
    //      low = mid + 1;
    //    }
    //    else
    //    {
    //      high = mid - 1;
    //    }
    //  }

    //  return result;
    //}

    #endregion
  }
}
