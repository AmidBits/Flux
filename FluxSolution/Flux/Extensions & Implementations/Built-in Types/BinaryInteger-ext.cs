namespace Flux
{
  public static class BinaryInteger
  {
    #region m_factorialTable (static factorial table)

    private static readonly long[] m_factorialTable =
    {
      1L,                          // 0!
      1L,                          // 1!
      2L,                          // 2!
      6L,                          // 3!
      24L,                         // 4!
      120L,                        // 5!
      720L,                        // 6!
      5040L,                       // 7!
      40320L,                      // 8!
      362880L,                     // 9!
      3628800L,                    // 10!
      39916800L,                   // 11!
      479001600L,                  // 12!
      6227020800L,                 // 13!
      87178291200L,                // 14!
      1307674368000L,              // 15!
      20922789888000L,             // 16!
      355687428096000L,            // 17!
      6402373705728000L,           // 18!
      121645100408832000L,         // 19!
      2432902008176640000L         // 20!
    };

    #endregion

    #region m_doubleFactorialTable (static double factorial table)

    private static readonly long[] m_doubleFactorialTable =
    {
      1L,                                   // 0!!
      1L,                                   // 1!!
      2L,                                   // 2!!
      3L,                                   // 3!!
      8L,                                   // 4!!
      15L,                                  // 5!!
      48L,                                  // 6!!
      105L,                                 // 7!!
      384L,                                 // 8!!
      945L,                                 // 9!!
      3840L,                                // 10!!
      10395L,                               // 11!!
      46080L,                               // 12!!
      135135L,                              // 13!!
      645120L,                              // 14!!
      2027025L,                             // 15!!
      10321920L,                            // 16!!
      34459425L,                            // 17!!
      185794560L,                           // 18!!
      654729075L,                           // 19!!
      3715891200L,                          // 20!!
      155195155200L,                        // 21!!
      817496064640L,                        // 22!!
      18803915073920L,                      // 23!!
      19619905551360L,                      // 24!!
      11281445692032000L,                   // 25!!
      293317587993000000L,                  // 26!!
      7919574875811000000L,                 // 27!!
    };

    #endregion

    extension<TInteger>(TInteger)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
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

      ///// <summary>
      ///// <para>A more optimal approach that accumulates the falling power while dividing by each factor of the factorial in place. This minimizes the risk of overflow errors, and allow for larger coefficients to be calculated. The disadvantage of this algorithm is the necessary use of floating-point math.</para>
      ///// </summary>
      ///// <param name="n"></param>
      ///// <param name="k"></param>
      ///// <returns></returns>
      //public static TInteger BinomialCoefficientFP(TInteger n, TInteger k)
      //{
      //  if (TInteger.IsNegative(k) || k > n)
      //    return TInteger.Zero;

      //  if (k > n >> 1)
      //    k = n - k;

      //  var a = 1.0;

      //  for (var i = TInteger.One; i <= k; i++)
      //    a = a * double.CreateChecked(n - k + i) / double.CreateChecked(i);

      //  return TInteger.CreateChecked(a + 0.5);
      //}

      #endregion

      #region BitOperations

      #region BitFold functions

      #region BitFoldLeft

      /// <summary>
      /// <para>Recursively "folds" all 1-bits, starting at the least-significant-1-bit, into the left-most or higher-order bits.</para>
      /// <para>Yields a bit vector with the same least-significant-1-bit as <paramref name="value"/>, and with all 1's above it.</para>
      /// </summary>
      /// <returns>The left-most or higher-order bits, to the least-significant-1-bit of <paramref name="value"/>, set to 1. If <paramref name="value"/> is negative, -1 is returned (all bits set to 1). Zero returns 0.</returns>
      public static TInteger BitFoldLeft(TInteger value)
        => TInteger.IsZero(value)
        ? value
        : (value is System.Numerics.BigInteger ? CreateBitMaskRight(TInteger.CreateChecked(GetBitCount(value))) : ~TInteger.Zero) << int.CreateChecked(TInteger.TrailingZeroCount(value));
      //var tzc = value.GetTrailingZeroCount();
      //return BitFoldRight(value << value.GetLeadingZeroCount()) >> tzc << tzc;

      #endregion

      #region BitFoldRight

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

      #endregion

      #region ContainsAll1Bits

      /// <summary>
      /// <para>Checks whether a <paramref name="value"/> contains all 1-bits of a <paramref name="bitMask"/>.</para>
      /// </summary>
      public static bool ContainsAll1Bits<TBitMask>(TInteger value, TBitMask bitMask)
        where TBitMask : System.Numerics.IBinaryInteger<TBitMask>
        => TInteger.IsZero(~value & TInteger.CreateChecked(bitMask));

      #endregion

      #region ContainsAny1Bits

      /// <summary>
      /// <para>Checks whether a <paramref name="value"/> contains any 1-bits of a <paramref name="bitMask"/>.</para>
      /// </summary>
      public static bool ContainsAny1Bits<TBitMask>(TInteger value, TBitMask bitMask)
        where TBitMask : System.Numerics.IBinaryInteger<TBitMask>
        => !TInteger.IsZero(value & TInteger.CreateChecked(bitMask));

      #endregion

      #region CreateBitMask functions

      #region CreateBitMaskLeft

      /// <summary>
      /// <para>Create a bit-mask with <paramref name="count"/> most-significant-bits (a.k.a. high-order or left-most bits) set to 1.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="count">Can be up to the number of storage bits (bit-count) available in <typeparamref name="TInteger"/>.</param>
      /// <returns></returns>
      /// <remarks><c>PLEASE NOTE THAT THE FIRST ARGUMENT (<paramref name="count"/> for extension method) IS THE NUMBER OF BITS (to account for).</c></remarks>
      public static TInteger CreateBitMaskLeft(TInteger count)
        => TInteger.IsZero(count)
        ? count
        : CreateBitMaskRight(count) << (GetBitCount(TInteger.Zero) - int.CreateChecked(count));

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

      #endregion

      #region CreateBitMaskRight

      /// <summary>
      /// <para>Create a bit-mask with <paramref name="count"/> least-significant-bits (a.k.a. low-order or right-most bits) set to 1.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="count">Can be up to the number of storage bits (bit-count) available in <typeparamref name="TInteger"/>.</param>
      /// <returns></returns>
      /// <remarks><c>PLEASE NOTE THAT THE FIRST ARGUMENT (<paramref name="count"/> for extension method) IS THE NUMBER OF BITS (to account for).</c></remarks>
      public static TInteger CreateBitMaskRight(TInteger count)
        => TInteger.IsZero(count)
        ? count
        : (((TInteger.One << (int.CreateChecked(count) - 1)) - TInteger.One) << 1) | TInteger.One;

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

      #endregion

      #endregion

      #region CartesianToLinearIndex (2D & 3D)

      /// <summary>
      /// <para>Converts cartesian-coordinates (<paramref name="x"/>, <paramref name="y"/>) to a linear index of a grid with the specified <paramref name="width"/> (the length of the x-axis).</para>
      /// </summary>
      public static TInteger CartesianToLinearIndex(TInteger x, TInteger y, TInteger width)
        => x + (y * width);

      /// <summary>
      /// <para>Converts cartesian-coordinates (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>) to a linear index of a cube with the specified <paramref name="width"/> (the length of the x-axis) and <paramref name="height"/> (the length of the y-axis).</para>
      /// </summary>
      public static TInteger CartesianToLinearIndex(TInteger x, TInteger y, TInteger z, TInteger width, TInteger height)
        => x + (y * width) + (z * width * height);

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
      public static System.Collections.Generic.IEnumerable<(TInteger LayerCount, TInteger CenterPolygonalNumber)> GenerateCenteredPolygonalLayers(TInteger k)
        => GenerateCenteredPolygonalNumberSequence(k).PartitionTuple2(false, (previous, current, index) => (TInteger.CreateChecked(index + 2), current)).Prepend((TInteger.One, TInteger.One));
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
      public static System.Collections.Generic.IEnumerable<TInteger> GenerateCenteredPolygonalNumberSequence(TInteger k)
        => Number.ArithmeticSequence(TInteger.Zero, TInteger.One).Select(n => GetCenteredPolygonalNumber(k, n));

      #endregion

      #region Combinatorics Count Combination & Permutation functions

      #region CountCombinationsWithRepetition

      /// <summary>
      /// <para>Combinations with repetition are a way to select items from a set where the order does not matter (combination), and items can be chosen more than once (with repeats).</para>
      /// <para>Computes the number of combinations with repeats:</para>
      /// <para><c>"n multichoose r"</c> = <c>Cr(n + r - 1, r)</c> = <c>(n + r - 1)! / (r! * (n - 1)!)</c></para>
      /// <para>A <i>combination</i> is a way of choosing items from a set when the order of choice does not matter. If you rearrange the same chosen items, it still counts as the same combination.</para>
      /// <para><i><b>With</b> repetition</i> means that each item can be chosen more than once.</para>
      /// </summary>
      /// <param name="total">Number of distinct item types.</param>
      /// <param name="choose">Number of items to choose.</param>
      /// <returns></returns>
      public static TInteger CountCombinationsWithRepetition(TInteger total, TInteger choose)
        => BinomialCoefficient(total + choose - TInteger.One, choose);

      #endregion

      #region CountCombinationsWithoutRepetition

      /// <summary>
      /// <para>Combinations without repetition refer to the selection of items from a larger set, where the order of selection does not matter (combination), and each item can only be chosen once (no repeats).</para>
      /// <para>Computes combinations without repeats:</para>
      /// <para><c>"n choose r"</c> = <c>C(n, k)</c> = <c>n! / (k! * (n - k)!)</c></para>
      /// <para>A <i>combination</i> is a way of choosing items from a set when the order of choice does not matter. If you rearrange the same chosen items, it still counts as the same combination.</para>
      /// <para><i><b>Without</b> repetition</i> means that once an item is chosen, it cannot be selected again.</para>
      /// </summary>
      /// <param name="total">Number of distinct item types.</param>
      /// <param name="choose">Number of items to choose.</param>
      /// <returns></returns>
      public static TInteger CountCombinationsWithoutRepetition(TInteger total, TInteger choose)
        => BinomialCoefficient(total, choose);

      #endregion

      #region CountPermutationsWithRepetition

      /// <summary>
      /// <para>Permutations with repetition involve arranging a set of objects where some objects are identical. This concept is useful in various practical scenarios, such as arranging students of different grades or cars of certain colors without distinguishing between identical items.</para>
      /// <para>Computes permutations with repeats:</para>
      /// <para>"<c>each of the k positions has n choices</c>" = <c>P(n, k)</c> = <c>n^k</c></para>
      /// <para>A <i>permutation</i> is an arrangement of items where the order matters. If you change the order, you create a different permutation.</para>
      /// <para><i><b>With</b> repetition</i> means that each item can be chosen more than once.</para>
      /// </summary>
      /// <param name="total">Number of distinct item types.</param>
      /// <param name="choose">Number of items to choose.</param>
      /// <returns></returns>
      public static TInteger CountPermutationsWithRepetition(TInteger total, TInteger choose)
        => TInteger.CreateChecked(System.Numerics.BigInteger.Pow(System.Numerics.BigInteger.CreateChecked(total), int.CreateChecked(choose)));

      #endregion

      #region CountPermutationsWithoutRepetition

      /// <summary>
      /// <para>Permutations without repetition refer to different groups of elements that can be done, so that two groups differ from each other only in the order the elements are placed. This situation frequently occurs when you’re working with unique physical objects that can occur only once in a permutation.</para>
      /// <para>Computes permutations without repeats:</para>
      /// <para>"<c>k items chosen from n distinct items</c>" = <c>P(n, k)</c> = <c>n! / (n - k)!</c></para>
      /// <para>A <i>permutation</i> is an arrangement of items where the order matters. If you change the order, you create a different permutation.</para>
      /// <para><i><b>Without</b> repetition</i> means that once an item is chosen, it cannot be chosen again.</para>
      /// </summary>
      /// <param name="total">Number of distinct item types.</param>
      /// <param name="choose">Number of items to choose.</param>
      /// <returns></returns>
      public static TInteger CountPermutationsWithoutRepetition(TInteger total, TInteger choose)
        => FallingFactorial(total, choose);

      #endregion

      #endregion

      #region CreateFormatStringWithCountDecimals

      /// <summary>
      /// <para>Returns a string format for a specified number of fractional digits.</para>
      /// </summary>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public static string CreateFormatStringWithCountDecimals(TInteger count)
      {
        System.ArgumentOutOfRangeException.ThrowIfLessThan(count, TInteger.One);
        System.ArgumentOutOfRangeException.ThrowIfGreaterThan(count, TInteger.CreateChecked(339));

        return "0." + new string('#', int.CreateChecked(count));
      }

      #endregion

      #region Digit-based functions

      #region DigitCount

      /// <summary>
      /// <para>Gets the count of all digits in a number using the specified <paramref name="radix"/>.</para>
      /// </summary>
      /// <remarks>DigitCount is log-floor + 1.</remarks>
      public static TInteger DigitCount<TRadix>(TInteger value, TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
        => IsSingleDigit(value, radix)
        ? TInteger.One
        : IntegerLog(value, radix).IntegralLogAwayFromZero;
      //{
      //  var rdx = TInteger.CreateChecked(Units.Radix.AssertMember(radix));

      //  var count = TInteger.Zero;

      //  while (!TInteger.IsZero(value))
      //  {
      //    count++;

      //    value /= rdx;
      //  }

      //  return count;
      //}

      public static TInteger DigitCount2(TInteger n, TInteger b)
      {
        n = TInteger.Abs(n);

        TInteger count = TInteger.One;

        while (n >= b)
        {
          n /= b;
          count++;
        }

        return count;
      }

      #endregion

      #region DigitPlaceValues

      /// <summary>
      /// <para>Creates a new list with the digit place value components of <paramref name="value"/> using base <paramref name="radix"/>. E.g. 1234 return [4 (for 4 * ones), 30 (for 3 * tens), 200 (for 2 * hundreds), 1000 (for 1 * thousands)].</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <typeparam name="TRadix"></typeparam>
      /// <param name="value"></param>
      /// <param name="radix"></param>
      /// <returns></returns>
      public static List<TInteger> DigitPlaceValues(TInteger n, TInteger b)
      {
        var result = new List<TInteger>();

        var place = TInteger.One;
        while (n > TInteger.Zero)
        {
          var digit = n % b;
          result.Add(digit * place);

          n /= b;
          place *= b;
        }

        return result;
      }

      #endregion

      public static TInteger DigitProduct(TInteger n, TInteger radix)
      {
        n = TInteger.Abs(n);

        if (n < radix)
          return n;

        var product = TInteger.One;

        while (n > TInteger.Zero)
        {
          var digit = n % radix;

          if (TInteger.IsZero(digit))
            return digit;

          product *= digit;
          n /= radix;
        }

        return product;
      }

      #region DigitSum

      /// <summary>
      /// <para>Returns the sum of all single digits in <paramref name="value"/> using base <paramref name="radix"/>.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Digit_sum"/></para>
      /// </summary>
      public static TInteger DigitSum<TRadix>(TInteger value, TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        System.ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(radix, TRadix.One);

        var rdx = TInteger.CreateChecked(radix);

        var sum = TInteger.Zero;

        while (!TInteger.IsZero(value))
        {
          sum += value % rdx;

          value /= rdx;
        }

        return sum;
      }

      #endregion

      #region DropLeastSignificantDigits

      /// <summary>
      /// <para>Drop <paramref name="count"/> trailing (least significant) digits from <paramref name="value"/> using base <paramref name="radix"/>.</para>
      /// </summary>
      public static TInteger DropLeastSignificantDigits<TRadix>(TInteger value, TRadix radix, TInteger count)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        System.ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(radix, TRadix.One);

        return value / TInteger.CreateChecked(Pow(radix, count));
      }

      #endregion

      #region DropMostSignificantDigits

      /// <summary>
      /// <para>Drop <paramref name="count"/> leading (most significant) digits of <paramref name="value"/> using base <paramref name="radix"/>.</para>
      /// </summary>
      public static TInteger DropMostSignificantDigits<TRadix>(TInteger value, TRadix radix, TInteger count)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
        => value % TInteger.CreateChecked(Pow(radix, DigitCount(value, radix) - count)); // DigitCount() already checks lower radix bound.

      #endregion

      #region GetDigits

      /// <summary>
      /// <para>Creates a new list of digits representing the <paramref name="value"/> in base <paramref name="radix"/>.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <typeparam name="TRadix"></typeparam>
      /// <param name="value"></param>
      /// <param name="radix"></param>
      /// <returns></returns>
      public static System.Collections.Generic.List<TInteger> GetDigits<TRadix>(TInteger value, TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        var list = GetDigitsReversed(value, radix); // GetDigitsReversed() already check the lower radix bound.
        list.Reverse();
        return list;
      }

      #endregion

      #region GetDigitsReversed

      /// <summary>
      /// <para>Creates a new list of digits, in reverse order, representing the <paramref name="value"/> in base <paramref name="radix"/>.</para>
      /// </summary>
      public static System.Collections.Generic.List<TInteger> GetDigitsReversed<TRadix>(TInteger value, TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        System.ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(radix, TRadix.One);

        var rdx = TInteger.CreateChecked(radix);

        if (TInteger.IsNegative(value))
          value = TInteger.Abs(value);

        var list = new System.Collections.Generic.List<TInteger>();

        if (TInteger.IsZero(value))
          list.Add(TInteger.Zero);
        else
          while (!TInteger.IsZero(value))
          {
            list.Add(value % rdx);

            value /= rdx;
          }

        return list;
      }

      #endregion

      #region KeepLeastSignificantDigits

      /// <summary>
      /// <para>Retreive <paramref name="count"/> least significant digits of <paramref name="value"/> using base <paramref name="radix"/>.</para>
      /// </summary>
      public static TInteger KeepLeastSignificantDigits<TRadix>(TInteger value, TRadix radix, TInteger count)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        System.ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(radix, TRadix.One);

        return value % TInteger.CreateChecked(Pow(radix, count));
      }

      #endregion

      #region KeepMostSignificantDigits

      /// <summary>
      /// <para>Drop the leading digit of <paramref name="value"/> using base <paramref name="radix"/>.</para>
      /// </summary>
      public static TInteger KeepMostSignificantDigits<TRadix>(TInteger value, TRadix radix, TInteger count)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
        => value / TInteger.CreateChecked(Pow(radix, DigitCount(value, radix) - count));

      #endregion

      #region ReverseDigits

      /// <summary>
      /// <para>Reverse the digits a <paramref name="value"/> in base <paramref name="radix"/>, obtaining a new number.</para>
      /// </summary>
      public static TInteger ReverseDigits<TRadix>(TInteger value, TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        System.ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(radix, TRadix.One);

        var rdx = TInteger.CreateChecked(radix);

        var reversed = TInteger.Zero;

        while (!TInteger.IsZero(value))
        {
          reversed = (reversed * rdx) + (value % rdx);

          value /= rdx;
        }

        return reversed;
      }

      #endregion

      #region RotateDigits

      public static TInteger RotateDigits(TInteger n, TInteger k, TInteger radix, bool left = true)
      {
        if (n < radix) // If n is a single digit in the given radix, rotation has no effect.
          return n;

        var digitCount = TInteger.Zero;
        var t = n;
        while (t > TInteger.Zero)
        {
          t /= radix;
          digitCount++;
        }

        k %= digitCount;

        if (TInteger.IsZero(k))
          return n;

        if (!left)
          k = digitCount - k; // Reverse rotation.

        var powK = TInteger.One; // Compute b^k and b^(digits-k)
        for (var i = TInteger.Zero; i < k; i++)
          powK *= radix;

        var powRest = TInteger.One;
        for (var i = TInteger.Zero; i < digitCount - k; i++)
          powRest *= radix;

        var hiSplit = n / powRest; // first k digits
        var loSplit = n % powRest; // remaining digits

        return loSplit * powK + hiSplit; // Recombine.
      }

      #endregion

      #region SumLeastSignificantDigits

      /// <summary>
      /// <para>Sum <paramref name="count"/> least significant digits of <paramref name="value"/> in the given <paramref name="radix"/>.</para>
      /// </summary>
      /// <typeparam name="TRadix"></typeparam>
      /// <param name="value"></param>
      /// <param name="radix"></param>
      /// <param name="count"></param>
      /// <returns></returns>
      public static TInteger SumLeastSignificantDigits<TRadix>(TInteger value, TRadix radix, TInteger count)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        System.ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(radix, TRadix.One);
        var rdx = TInteger.CreateChecked(radix);

        value = TInteger.Abs(value);

        var sum = TInteger.Zero;

        for (var i = TInteger.Zero; i < count && value > TInteger.Zero; i++)
        {
          sum += value % rdx;

          value /= rdx;
        }

        return sum;
      }

      #endregion

      #region SumMostSignificantDigits

      /// <summary>
      /// <para>Sum <paramref name="count"/> most significant digits of <paramref name="value"/> in the given <paramref name="radix"/>.</para>
      /// </summary>
      /// <typeparam name="TRadix"></typeparam>
      /// <param name="value"></param>
      /// <param name="radix"></param>
      /// <param name="count"></param>
      /// <returns></returns>
      public static TInteger SumMostSignificantDigits<TRadix>(TInteger value, TRadix radix, TInteger count)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        System.ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(radix, TRadix.One);
        var rdx = TInteger.CreateChecked(radix);

        value = TInteger.Abs(value);
        var digits = DigitCount(value, radix);
        value /= Pow(rdx, digits - TInteger.Min(count, digits));

        var sum = TInteger.Zero;

        while (value > TInteger.Zero)
        {
          sum += value % rdx;

          value /= rdx;
        }

        return sum;
      }
      #endregion

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

      #region DivRem functions

      #region CeilingDivRem

      /// <summary>
      /// <para>Ceiling division, where the remainder has the opposite sign of that of the divisor.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Modulo"/></para>
      /// <para><see href="https://stackoverflow.com/a/20638659/3178666"/></para>
      /// </summary>
      /// <remarks>
      /// <para>The quotient (q) direction toward-positive-infinity:</para>
      /// <para><c>q = ceiling(a/n)</c></para>
      /// <para>The remainder (r) always have the opposite sign of n:</para>
      /// <para><c>r = a − n * q</c></para>
      /// </remarks>
      /// <param name="a"></param>
      /// <param name="n"></param>
      /// <returns>
      /// <para><c>q = ceiling(a / n)</c></para>
      /// <para><c>r = a - n * q</c></para>
      /// </returns>
      public static (TInteger Quotient, TInteger Remainder) CeilingDivRem(TInteger a, TInteger n)
      {
        if (TInteger.IsZero(n)) throw new System.DivideByZeroException();

        var q = a / n;   // truncating quotient
        var r = a % n;   // remainder with C# sign rules

        // If remainder is nonzero and the division truncated downward, adjust to ceiling division.
        if (!TInteger.IsZero(r) && TInteger.IsNegative(r) == TInteger.IsNegative(n))
        {
          q += TInteger.One;
          r -= n;
        }

        return (q, r);
      }

      #endregion

      #region ClosestDivRem

      /// <summary>
      /// <para>Closest division (nearest, midpoint‑even).</para>
      /// </summary>
      /// <param name="a"></param>
      /// <param name="n"></param>
      /// <returns></returns>
      public static (TInteger Quotient, TInteger Remainder) ClosestDivRem(TInteger a, TInteger n)
      {
        if (TInteger.IsZero(n)) throw new System.DivideByZeroException();

        var q0 = a / n;
        var r0 = a - q0 * n;

        if (TInteger.IsZero(r0))
          return (q0, r0);

        var absR = TInteger.Abs(r0);
        var absN = TInteger.Abs(n);

        var twiceR = absR + absR;

        // Compare 2|r| with |n|
        if (twiceR < absN)
          return (q0, r0);

        var step = TInteger.CreateChecked(TInteger.Sign(r0) * TInteger.Sign(n));

        if (twiceR > absN)
        {
          var q = q0 + step;
          return (q, a - q * n);
        }

        // Tie → round to even
        if (TInteger.IsEvenInteger(q0))
          return (q0, r0);

        var qEven = q0 + step;
        return (qEven, a - qEven * n);
      }

      #endregion

      #region EnvelopedDivRem

      /// <summary>
      /// <para>Enveloped (opposite of truncated, in that it envelops the entire fractional side to the next whole integer, away from zero) division, where the quotient is ceiling for positive and floor for negative.</para>
      /// </summary>
      /// <remarks>
      /// <para>The quotient (q) round-away-from-0:</para>
      /// <list type="bullet">
      /// <item>If <c><![CDATA[a/n > 0]]></c>, q = ceiling</item>
      /// <item>If <c><![CDATA[a/n < 0]]></c>, q = floor</item>
      /// </list>
      /// <para>The remainder (r) always have the opposite sign of q:</para>
      /// <para><c>r = a − n * q</c></para>
      /// </remarks>
      /// <param name="a"></param>
      /// <param name="n"></param>
      /// <returns>
      /// <para><c>q = envelop(a / n)</c></para>
      /// <para><c>r = a - n * q</c></para>
      /// </returns>
      public static (TInteger Quotient, TInteger Remainder) EnvelopedDivRem(TInteger a, TInteger n)
      {
        if (TInteger.IsZero(n)) throw new System.DivideByZeroException();

        var q0 = a / n; // Truncate toward zero.
        var r0 = a - q0 * n; // Remainder, same sign as a.

        if (TInteger.IsZero(r0))
          return (q0, r0);

        var step = TInteger.CreateChecked(TInteger.Sign(a) * TInteger.Sign(n)); // sign(a/n) = sign(a) * sign(n)

        return (q0 + step, r0 - n * step);
      }

      #endregion

      #region EuclideanDivRem

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

        var q = a / n;
        var r = a - q * n;

        if (TInteger.IsNegative(r))
        {
          r += TInteger.Abs(n);
          q -= TInteger.CreateChecked(TInteger.Sign(n));
        }

        return (q, r);
      }

      #endregion

      #region FlooredDivRem

      /// <summary>
      /// <para>Floored division, where the remainder has the same sign as the divisor.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Modulo"/></para>
      /// <para><see href="https://stackoverflow.com/a/20638659/3178666"/></para>
      /// </summary>
      /// <remarks>
      /// <para>The quotient (q) direction toward-negative-infinity:</para>
      /// <para><c>q = floor(a/n)</c></para>
      /// <para>The remainder (r) always have the same sign of n:</para>
      /// <para><c>r = a − n * q</c></para>
      /// </remarks>
      /// <param name="a"></param>
      /// <param name="n"></param>
      /// <returns>
      /// <para><c>q = floor(a / n)</c></para>
      /// <para><c>r = a - n * q</c></para>
      /// </returns>
      public static (TInteger Quotient, TInteger Remainder) FlooredDivRem(TInteger a, TInteger n)
      {
        if (TInteger.IsZero(n)) throw new System.DivideByZeroException();

        var q = a / n; // truncating quotient
        var r = a % n; // remainder with C# sign rules

        if (!TInteger.IsZero(r) && ((n > TInteger.Zero && TInteger.IsNegative(r)) || (TInteger.IsNegative(n) && r > TInteger.Zero))) // If signs differ and remainder is nonzero, adjust to floor division.
        {
          q -= TInteger.One;
          r += n;
        }

        return (q, r);
        //if (TInteger.IsZero(n)) throw new System.DivideByZeroException();

        //var q = (TInteger.IsNegative(a) != TInteger.IsNegative(n) ? (a - (n - TInteger.CopySign(TInteger.One, n))) : a) / n;

        //return (q, a - n * q);
      }

      #endregion

      #region RoundedDivRem

      /// <summary>
      /// <para>Rounded division (nearest, ties away from zero).</para>
      /// </summary>
      /// <param name="a"></param>
      /// <param name="n"></param>
      /// <returns>
      /// <para><c>q = round(a / n, away-from-zero)</c> // rounding, ties away from zero</para>
      /// <para><c>r = a - n * q</c></para>
      /// </returns>
      public static (TInteger Quotient, TInteger Remainder) RoundedDivRem(TInteger a, TInteger n, MidpointRounding mode = System.MidpointRounding.AwayFromZero)
        => mode switch
        {
          MidpointRounding.ToEven => ClosestDivRem(a, n),
          MidpointRounding.AwayFromZero => EnvelopedDivRem(a, n),
          MidpointRounding.ToZero => SymmetricDivRem(a, n),
          MidpointRounding.ToNegativeInfinity => FlooredDivRem(a, n),
          MidpointRounding.ToPositiveInfinity => CeilingDivRem(a, n),
          _ => throw new System.ArgumentOutOfRangeException(nameof(mode)),
        };

      #endregion

      #region SymmetricDivRem

      /// <summary>
      /// <para>Symmetric division (nearest-integer division with ties toward zero) chooses the quotient so that the remainder is as close to zero as possible.</para>
      /// <list type="bullet">
      /// <item>The remainder is always in the interval: <c><![CDATA[-|b|/2, |b|/2]]></c></item>
      /// <item>The quotient is the nearest integer to <c>a / b</c>.</item>
      /// <item>If the remainder is exactly halfway, it is rounded toward zero.</item>
      /// </list>
      /// </summary>
      /// <param name="a"></param>
      /// <param name="b"></param>
      /// <returns></returns>
      public static (TInteger Quotient, TInteger Remainder) SymmetricDivRem(TInteger a, TInteger b)
      {
        var q0 = a / b;
        var r0 = a - q0 * b;

        var half = TInteger.Abs(b) / TInteger.CreateChecked(2); // Half divisor magnitude

        var bumpUp = TInteger.CreateChecked((r0 > half) ? 1 : 0); // Branch‑free mask.
        var bumpDown = TInteger.CreateChecked((r0 < -half) ? 1 : 0); // Branch‑free mask.

        var q = q0 + bumpUp - bumpDown;
        var r = a - q * b;

        return (q, r);
      }

      #endregion

      #endregion

      #region Divisor functions

      #region CountDivisors

      /// <summary>
      /// <para>σ0()</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Divisor"/></para>
      /// <para><see href="https://cp-algorithms.com/algebra/divisors.html"/></para>
      /// </summary>
      public static TInteger CountDivisors(TInteger number)
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
        => SumDivisors(number).AliquotSum < number;

      #endregion

      #region IsPerfectNumber

      /// <summary>Determines whether the <paramref name="number"/> is a perfect number.</summary>
      /// <see href="https://en.wikipedia.org/wiki/Perfect_number"/>
      public static bool IsPerfectNumber(TInteger number)
        => SumDivisors(number).AliquotSum == number;

      #endregion

      #region SumDivisors

      /// <summary>
      /// <para>σ1()</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Divisor"/></para>
      /// <para><see href="https://cp-algorithms.com/algebra/divisors.html"/></para>
      /// </summary>
      public static (TInteger Sum, TInteger AliquotSum) SumDivisors(TInteger number)
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

      #region Factorial functions

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

        if (TryGetStaticDoubleFactorial(n, out var sdf))
          return TInteger.CreateChecked(sdf);

        var result = TInteger.One;
        var two = TInteger.CreateChecked(2);

        checked
        {
          for (var i = n; i > TInteger.One; i -= two)
            result *= i;
        }

        return result;
      }

      private static bool TryGetStaticDoubleFactorial(TInteger n, out long value)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(n);

        var i = uint.CreateChecked(n);

        if (i < m_doubleFactorialTable.Length)
        {
          value = m_doubleFactorialTable[i];
          return true;
        }

        value = 0;
        return false;
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
        if (TryGetStaticFactorial(n, out var sf))
          return TInteger.CreateChecked(sf);

        if (n < TInteger.CreateChecked(47))
          return NaiveFactorial(n);

        return SplitFactorial(n);
      }

      /// <summary>
      /// <para>Computes the factorial of <paramref name="value"/>, e.g. <c>Factorial(5) => 1 * 2 * 3 * 4 * 5</c></para>
      /// <para><see href="https://en.wikipedia.org/wiki/Factorial"/></para>
      /// </summary>
      /// <remarks>This plain-and-simple iterative version of factorials is faster with numbers smaller than 60 or so, and starts loosing with larger numbers.</remarks>
      /// <typeparam name="TInteger"></typeparam>
      private static TInteger NaiveFactorial(TInteger n)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(n);

        var f = TInteger.One;

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
      private static TInteger SplitFactorial(TInteger n)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(n);

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

      private static bool TryGetStaticFactorial(TInteger n, out long factorial)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(n);

        var i = uint.CreateChecked(n);

        if (i < m_factorialTable.Length)
        {
          factorial = m_factorialTable[i];
          return true;
        }

        factorial = 0;
        return false;
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

        if (n > TInteger.CreateChecked(uint.MaxValue)) throw new System.ArgumentOutOfRangeException(nameof(n), "n is too large to iterate."); // n must fit in uint for a practical loop.

        var count = uint.CreateChecked(n);

        if (count == 0)
          return TInteger.One;
        if (count == 1)
          return x;

        var one = TInteger.One;
        var result = one;

        checked
        {
          var term = x;
          for (var i = 0u; i < count; i++)
          {
            result *= term;
            term -= one;
          }
        }

        return result;
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

        if (n > TInteger.CreateChecked(uint.MaxValue)) throw new System.ArgumentOutOfRangeException(nameof(n), "n is too large to iterate."); // n must fit in uint for a practical loop.

        var count = uint.CreateChecked(n);

        if (count == 0)
          return TInteger.One;
        if (count == 1)
          return x;

        var one = TInteger.One;
        var result = one;

        checked
        {
          var term = x;
          for (var i = 0u; i < count; i++)
          {
            result *= term;
            term += one;
          }
        }

        return result;
      }

      #endregion

      #endregion

      #region GetBitCount

      /// <summary>
      /// <para>Returns the size, in number of bits, needed to store <paramref name="value"/>.</para>
      /// <para>Most types returns the underlying storage size of the type itself, e.g. <see langword="int"/> = 32 or <see langword="long"/> = 64.</para>
      /// </summary>
      /// <remarks>
      /// <para>Some data types, e.g. <see cref="System.Numerics.BigInteger"/>, use dynamic storage strategies.</para>
      /// </remarks>
      public static int GetBitCount(TInteger value)
        => value.GetByteCount() * 8;

      #endregion

      #region GetByteCount

      /// <summary>
      /// <para>Using the built-in <see cref="System.Numerics.IBinaryInteger{TInteger}.GetByteCount()"/>.</para>
      /// </summary>
      /// <remarks>
      /// <para>Note that some datatypes, e.g. <see cref="System.Numerics.BigInteger"/>, use dynamic storage strategies.</para>
      /// </remarks>
      public static int GetByteCount(TInteger value)
        => value.GetByteCount();

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

      #region GetMaxDigitCount

      /// <summary>
      /// <para>Computes the max number of digits that can be represented by the specified <paramref name="bitLength"/> (number of bits) in <paramref name="radix"/> (number base) and whether to <paramref name="accountForSignBit"/>.</para>
      /// <code>var mdcf = (10).GetMaxDigitCount(10, false); // Yields 4, because a max value of 1023 can be represented (all bits can be used in an unsigned value).</code>
      /// <code>var mdct = (10).GetMaxDigitCount(10, true); // Yields 3, because a max value of 511 can be represented (excluding the MSB used for negative values of signed types).</code>
      /// </summary>
      /// <typeparam name="TRadix"></typeparam>
      /// <param name="bitLength">This is the number of bits to take into account.</param>
      /// <param name="radix">This is the radix (base) to use.</param>
      /// <param name="accountForSignBit">Indicates whether <paramref name="value"/> use one bit for the sign.</param>
      /// <returns></returns>
      public static int GetMaxDigitCount<TRadix>(TInteger bitLength, TRadix radix, bool accountForSignBit)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        var mask = CreateBitMaskRight(System.Numerics.BigInteger.CreateChecked(TInteger.Abs(bitLength))); // Create a bit-mask representing the greatest value for the bit-length.

        if (accountForSignBit || TInteger.IsNegative(bitLength)) // If accounting for a sign-bit, shift the SWAR to properly represent the max of a signed type.
          mask >>>= 1;

        return int.CreateChecked(DigitCount(mask, radix));
      }

      #endregion

      #region Gray

      /// <summary>
      /// <para>Converts a binary number to a reflected binary Gray code.</para>
      /// <see href="https://en.wikipedia.org/wiki/Gray_code"/>
      /// </summary>
      public static TInteger BinaryToGray(TInteger value)
        => value ^ (value >>> 1);

      /// <summary>
      /// <para>Converts a reflected binary gray code to a binary number.</para>
      /// <see href="https://en.wikipedia.org/wiki/Gray_code"/>
      /// </summary>
      public static TInteger GrayToBinary(TInteger value)
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

      #region Number sequences and associated functions

      #region AbundantNumbers

      /// <summary>
      /// <para>Creates a new sequence of 2-tuples, each with an abundant number and its aliquot sum.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Abundant_number"/></para>
      /// </summary>
      /// <remarks>This function generate results until the type <typeparamref name="TInteger"/> under/overflows in any calculation. No exception is thrown.</remarks>
      /// <typeparam name="TSelf"></typeparam>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<(TInteger Number, TInteger AliqoutSum)> AbundantNumbers()
        => Number.ArithmeticSequence(TInteger.CreateChecked(3), TInteger.One).AsParallel().AsOrdered().Select(n => (Number: n, SumDivisors(n).AliquotSum)).Where(x => x.AliquotSum > x.Number);

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
          if (SumDivisors(index).Sum is var sumOfDivisors && sumOfDivisors > largestSumOfDivisors)
          {
            yield return (index, sumOfDivisors);

            largestSumOfDivisors = sumOfDivisors;
          }
      }

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

      #region IsAbundantNumber

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

      #region BellNumbers

      /// <summary>
      /// <para>Creates a new sequence of Bell numbers.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Bell_number"/></para>
      /// </summary>
      /// <remarks>This function generate results until the type <typeparamref name="TInteger"/> under/overflows in any calculation. No exception is thrown.</remarks>
      /// <typeparam name="TInteger"></typeparam>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<TInteger> BellNumbers()
        => BellTriangle<TInteger>().Select(a => a[0]);

      #endregion

      #region BellTriangle

      /// <summary>
      /// <para>Creates a new sequence with arrays (i.e. row) of Bell numbers in a Bell triangle.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Bell_triangle"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Bell_number"/></para>
      /// </summary>
      /// <remarks>This function generate results until the type <typeparamref name="TInteger"/> under/overflows in any calculation. No exception is thrown.</remarks>
      /// <typeparam name="TInteger"></typeparam>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<TInteger>> BellTriangle()
      {
        foreach (var list in BellTriangleAugmented<TInteger>().Skip(1))
        {
          list.RemoveAt(0);

          yield return list;
        }
      }

      #endregion

      #region BellTriangleAugmented

      /// <summary>
      /// <para>Creates a new sequence with arrays (i.e. row) of Bell numbers in an augmented Bell triangle.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Bell_triangle"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Bell_number"/></para>
      /// </summary>
      /// <remarks>This function generate results until the type <typeparamref name="TInteger"/> under/overflows in any calculation. No exception is thrown.</remarks>
      /// <typeparam name="TInteger"></typeparam>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<TInteger>> BellTriangleAugmented()
      {
        var l1 = new System.Collections.Generic.List<TInteger>() { TInteger.One }; // This is the current.
        var l0 = new System.Collections.Generic.List<TInteger>(); // This is the previous.

        while (true)
        {
          yield return l1.ToList();

          try
          {
            checked
            {
              (l1, l0) = (l0, l1); // Rotate and reuse the lists is much faster and less resource intensive.

              l1.Clear(); // This is now the current, and l0 became the previous.

              l1.Add(l0[^1]);
              l1.Insert(0, l1[0] - l0[0]);

              for (var i = 2; i <= l0.Count; i++)
                l1.Add(l0[i - 1] + l1[i - 1]);
            }
          }
          catch { break; }
        }
      }

      #endregion

      #region GetCatalanNumber

      /// <summary>
      /// <para>Returns the Catalan number for the specified <paramref name="number"/>.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Catalan_number"/></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="number"></param>
      /// <returns></returns>
      public static TInteger GetCatalanNumber(TInteger n)
        => checked(Factorial(n + n) / (Factorial(n + TInteger.One) * Factorial(n)));

      #endregion

      #region CatalanSequence

      /// <summary>
      /// <para>Creates a new sequence with Catalan numbers.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Catalan_number"/></para>
      /// </summary>
      /// <remarks>This function runs indefinitely, if allowed.</remarks>
      /// <typeparam name="TInteger"></typeparam>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<TInteger> CatalanSequence()
        => Number.ArithmeticSequence(TInteger.Zero, TInteger.One).AsParallel().AsOrdered().Select(GetCatalanNumber);

      #endregion

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

        foreach (var tuple in Number.ArithmeticSequence(TInteger.One, TInteger.One).AsParallel().AsOrdered().Select(n => (Number: n, Count: CountDivisors(n))))
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
          var fp4sr = IntegerSquareRoot(fp4);
          var fm4 = fivens - four;
          var fm4sr = IntegerSquareRoot(fm4);

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

      #region GetMersenneNumber

      /// <summary>
      /// <para>Computes the Mersenne number for the specified <paramref name="value"/>.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Mersenne_number"/></para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="value"></param>
      /// <returns></returns>
      public static TInteger GetMersenneNumber(TInteger number)
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
        => Number.ArithmeticSequence(TInteger.One, TInteger.One).Select(GetMersenneNumber);

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

      #region PrimeSequenceAscending

      /// <summary>
      /// <para>Creates a new sequence of ascending possible primes, greater-than-or-equal-to a specified number.</para>
      /// </summary>
      /// <param name="n"></param>
      /// <returns></returns>
      private static System.Collections.Generic.IEnumerable<TInteger> PrimeCandidatesAscending(TInteger n)
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
      public static System.Collections.Generic.IEnumerable<TInteger> PrimeSequenceAscending(TInteger n)
        => PrimeCandidatesAscending(n).AsParallel().AsOrdered().Where(IsPrimeNumber);

      #endregion

      #region PrimeSequenceDescending

      /// <summary>Creates a new sequence of descending possible primes, less-than-or-equal-to a specified number.</summary>
      private static System.Collections.Generic.IEnumerable<TInteger> PrimeCandidatesDescending(TInteger n)
      {
        var six = TInteger.CreateChecked(6);

        var maxPrimeMultiple = BitFoldLeft(TInteger.One);

        if (TypeExtensions.ImplementsISignedNumber(typeof(TInteger)))
          maxPrimeMultiple >>>= 1;

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
      public static System.Collections.Generic.IEnumerable<TInteger> PrimeSequenceDescending(TInteger n)
        => PrimeCandidatesDescending(n).AsParallel().AsOrdered().Where(IsPrimeNumber);

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
        if (TInteger.IsNegative(n))
          return false; // Negative numbers are not prime.

        var bi = System.Numerics.BigInteger.CreateChecked(n);

        if (bi <= uint.MaxValue) // If less-or-equal-to an unsigned 32-bit value, use straight 6k ± 1 prime test algorithm.
          return uint.IsPrime(uint.CreateChecked(n));

        if (bi <= ulong.MaxValue) // If less-or-equal-to an unsigned 64-bit value, use Miller-Rabin 64-bit deterministic algorithm.
          return ulong.IsPrime(ulong.CreateChecked(n));

        // Otherwise use Miller-Rabin probabilistic algorithm.

        // Log(bit-length, 1.17) yields an approximately 15 iterations @ 10 bits, 30 @ 100, 44 @ 1000, 59 @ 10000, and can be lowered for a higher iteration (k) count.
        // The lower bit-length, the higher count.

        var log = System.Numerics.BigInteger.Log(bi.GetBitLength(), 1.15);

        return System.Numerics.BigInteger.IsPrime(bi, int.CreateChecked(log)); // Pass the log value as k parameter.
      }

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

      #endregion

      #region Greatest Common Divisor functions

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

      #region IntegerCubeRoot functions

      /// <summary>
      /// <para>Computes the integer (floor) cube-root of a value.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <returns>The square-root of the value.</returns>
      public static TInteger IntegerCubeRoot(TInteger value)
      {
        if (TInteger.IsZero(value))
          return value;

        var abs = TInteger.Abs(value);
        var sign = TInteger.CopySign(TInteger.One, value);

        if (TryConvertTo(abs, out ulong ulv))
          return TInteger.CreateChecked(ulong.ICbrt(ulv)) * sign;

        return TInteger.CreateChecked(System.Numerics.BigInteger.ICbrt(System.Numerics.BigInteger.CreateChecked(abs))) * sign;
      }

      /// <summary>
      /// <para>Indicates whether <paramref name="value"/> is the integer (not necessarily perfect) square of <paramref name="root"/>.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="value">The square value to find the square-<paramref name="root"/> of.</param>
      /// <param name="root">The resulting square-root of <paramref name="value"/>.</param>
      /// <returns>Whether the <paramref name="value"/> is the integer (not necessarily perfect) square of <paramref name="root"/>.</returns>
      public static bool IsIntegerCubeRoot(TInteger value, TInteger root)
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
      public static bool IsPerfectIntegerCubeRoot(TInteger value, TInteger root)
        => value == (root * root * root);

      #endregion

      #region IntegerLog

      /// <summary>
      /// <para>Returns the integer (toward-zero, away-from-zero) logarithm of specified a <paramref name="value"/> in a specified <paramref name="radix"/>.</para>
      /// </summary>
      /// <typeparam name="TRadix"></typeparam>
      /// <param name="value"></param>
      /// <param name="radix"></param>
      /// <returns></returns>
      public static (TInteger IntegralLogTowardZero, TInteger IntegralLogAwayFromZero, bool IsExactLog) IntegerLog<TRadix>(TInteger value, TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        System.ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(radix, TRadix.One);

        if (TInteger.IsZero(value))
          return (value, value, false);

        var absV = TInteger.Abs(value);
        var sign = TInteger.CopySign(TInteger.One, value);

        if (TryConvertTo(absV, out ulong ulv) && TryConvertTo(radix, out ulong ulr))
        {
          var (LogF, LogC, IsExactLog) = ulong.ILog(ulv, ulr);

          return (TInteger.CreateChecked(LogF) * sign, TInteger.CreateChecked(LogC) * sign, IsExactLog);
        }
        else // Fall-back on BigInteger ILog, which is more expensive but can handle any size of integer.
        {
          var (LogF, LogC, IsExactLog) = System.Numerics.BigInteger.ILog(System.Numerics.BigInteger.CreateChecked(absV), System.Numerics.BigInteger.CreateChecked(radix));

          return (TInteger.CreateChecked(LogF) * sign, TInteger.CreateChecked(LogC) * sign, IsExactLog);
        }
      }

      #endregion

      #region IntegerLogE

      /// <summary>
      /// <para>Returns the integer (floor) natural logarithm of specified a <paramref name="value"/> in a base/radix E.</para>
      /// </summary>
      /// <typeparam name="TRadix"></typeparam>
      /// <param name="value"></param>
      /// <returns></returns>
      public static (TInteger IntegralLogTowardZero, TInteger IntegralLogAwayFromZero) IntegerLogE<TRadix>(TInteger value)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        if (TInteger.IsZero(value))
          return (value, value);

        var absV = TInteger.Abs(value);
        var sign = TInteger.CopySign(TInteger.One, value);

        if (TryConvertTo(absV, out ulong ulv))
        {
          var (ilogf, ilogc) = ulong.ILogE(ulv);

          return (TInteger.CreateChecked(ilogf) * sign, TInteger.CreateChecked(ilogc) * sign);
        }
        else // Fall-back on BigInteger ILog, which is more expensive but can handle any size of integer.
        {
          var (ilogf, ilogc) = System.Numerics.BigInteger.ILogE(System.Numerics.BigInteger.CreateChecked(absV));

          return (TInteger.CreateChecked(ilogf) * sign, TInteger.CreateChecked(ilogc) * sign);
        }
      }

      #endregion

      #region IntegerRootN functions

      /// <summary>
      /// <para>Computes the integer nth-root of a value.</para>
      /// </summary>
      /// <typeparam name="TNth"></typeparam>
      /// <param name="value"></param>
      /// <param name="exponent"></param>
      /// <returns></returns>
      public static TInteger IntegerRootN(TInteger value, TInteger degree)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(value);
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(degree);

        if (value <= TInteger.One || degree <= TInteger.One)
          return value;

        // ---------------------------------------------------------
        // REAL PERFECT‑POWER FAST PATH (complete, fast, exact)
        // ---------------------------------------------------------
        {
          // For every possible exponent e dividing 'degree'
          // check whether value is an exact e-th power.
          //
          // This is the mathematically correct perfect-power test:
          // If value = k^degree, then for every divisor e of degree,
          // value is also a perfect e-th power.
          //
          // So we test all divisors of 'degree'.

          var d = degree;
          var one = TInteger.One;

          for (TInteger e = one; e * e <= d; e++) // Enumerate all divisors of degree.
          {
            if ((d % e) == TInteger.Zero)
            {
              if (IsExactPower(value, e, out var rootE)) // divisor e.
                return rootE;

              var f = d / e; // divisor degree/e
              if (f != e && IsExactPower(value, f, out var rootF))
                return rootF;
            }
          }
        }

        var x = TInteger.One << int.CreateChecked(TInteger.Log2(value) / degree);

        while (true)
        {
          var prev = x;

          var t = Pow(x, degree - TInteger.One, value); // Overflow‑safe power with early exit.

          if (t == TInteger.Zero)
            throw new ArithmeticException();

          x = ((degree - TInteger.One) * x + value / t) / degree;

          if (x == prev) // Final correction (Newton may be off by ±1).
          {
            while (Pow(x + TInteger.One, degree, value) <= value)
              x++;

            while (Pow(x, degree, value) > value)
              x--;

            return x;
          }
        }
      }

      private static bool IsExactPower(TInteger value, TInteger exponent, out TInteger root)
      {
        if (exponent == TInteger.One) // Quick reject.
        {
          root = value;
          return true;
        }

        root = TInteger.One << int.CreateChecked(TInteger.Log2(value) / exponent); // Initial guess.


        var t = Pow(root, exponent - TInteger.One, value); // One Newton refinement (not recursive).
        if (!TInteger.IsZero(t))
          root = ((exponent - TInteger.One) * root + value / t) / exponent;

        // Verify exactness
        return Pow(root, exponent, value) == value;
      }

      public static bool IsIntegerRootN<TNth>(TInteger value, TNth n, TInteger root)
        where TNth : System.Numerics.IBinaryInteger<TNth>
        => value >= Pow(root, n) // If GTE to nth of root.
        && value < Pow(root + TInteger.One, n); // And if LT to nth of (root + 1).

      public static bool IsPerfectIntegerRootN<TNth>(TInteger value, TNth n, TInteger root)
        where TNth : System.Numerics.IBinaryInteger<TNth>
        => value == Pow(root, n);

      #endregion

      #region IntegerSquareRoot functions

      /// <summary>
      /// <para>Computes the integer square-root of a value.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <returns></returns>
      public static TInteger IntegerSquareRoot(TInteger value)
      {
        if (TInteger.IsZero(value))
          return value;

        var abs = TInteger.Abs(value);
        var sign = TInteger.CopySign(TInteger.One, value);

        if (TryConvertTo(abs, out ulong ulv))
          return TInteger.CreateChecked(ulong.ISqrt(ulv)) * sign;

        return TInteger.CreateChecked(System.Numerics.BigInteger.ISqrt(System.Numerics.BigInteger.CreateChecked(abs))) * sign;
      }

      /// <summary>
      /// <para>Indicates whether <paramref name="value"/> is the integer (not necessarily perfect) square of <paramref name="root"/>.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="value">The square value to find the square-<paramref name="root"/> of.</param>
      /// <param name="root">The resulting square-root of <paramref name="value"/>.</param>
      /// <returns>Whether the <paramref name="value"/> is the integer (not necessarily perfect) square of <paramref name="root"/>.</returns>
      public static bool IsIntegerSquareRoot(TInteger value, TInteger root)
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
      public static bool IsPerfectIntegerSquareRoot(TInteger value, TInteger root)
        => value == (root * root);

      #endregion

      public static TInteger ISqrt(TInteger value)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(value);

        if (TInteger.IsZero(value))
          return value;

        var log2 = int.CreateChecked(TInteger.Log2(value));

        if (log2 < 128) // First check if the 128‑bit restoring fast path is applicable.
        {
          var n = value;
          var r = TInteger.Zero;
          var bit = TInteger.One << (int.CreateChecked(TInteger.Log2(value)) & ~1);

          bit >>= int.CreateChecked((-(bit > n ? TInteger.One : TInteger.Zero)) & TInteger.CreateChecked(2)); // Align bit without branch

          while (bit != TInteger.Zero)
          {
            var t = r + bit;
            var ge = -(n >= t ? TInteger.One : TInteger.Zero);
            n -= t & ge;
            r = (r >> 1) + (bit & ge);
            bit >>= 2;
          }

          return TInteger.CreateChecked(r);
        }
        else // For larger values, fall back to the integer Newton iteration.
        {
          var x = TInteger.One << (value.GetBitLength() / 2); // Initial approximation: 2^(bitLength/2)

          while (true)
          {
            var y = (x + value / x) >> 1;

            if (x - y is var diff && diff <= TInteger.One && diff >= TInteger.Zero)
              return y;

            x = y;
          }
        }
      }

      #region IsBalanced

      public static bool IsBalanced<TRadix>(TInteger value, TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        var digits = GetDigits(value, radix); // Already checks lower radix bound.

        var ceilingHalf = int.EnvelopedDivRem(digits.Count, 2).Quotient;

        var left = digits[..ceilingHalf].Sum();
        var right = digits[^ceilingHalf..].Sum();

        //var rgt = SumLeastSignificantDigits(value, radix, TInteger.CreateChecked(ceilingHalf));

        return left == right;
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

      #region IsJumbled

      /// <summary>
      /// <para>Indicates whether <paramref name="value"/> using base <paramref name="radix"/> is jumbled (i.e. no neighboring digits having a difference larger than 1).</para>
      /// <para><see cref="http://www.geeksforgeeks.org/check-if-a-number-is-jumbled-or-not/"/></para>
      /// </summary>
      public static bool IsJumbled<TRadix>(TInteger value, TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        System.ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(radix, TRadix.One);

        var rdx = TInteger.CreateChecked(radix);

        while (!TInteger.IsZero(value))
        {
          var remainder = value % rdx;

          value /= rdx;

          if (TInteger.IsZero(value))
            break;
          else if (TInteger.Abs((value % rdx) - remainder) > TInteger.One) // If the difference to the digit is greater than 1, then the number cannot jumbled.
            return false;
        }

        return true;
      }

      #endregion

      #region IsSelfNumber

      /// <summary>
      /// <para>A self number in a given number base b is a natural number that cannot be written as the sum of any other natural number n and the individual digits of number n.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <typeparam name="TRadix"></typeparam>
      /// <param name="number"></param>
      /// <param name="radix"></param>
      /// <returns></returns>
      public static bool IsSelfNumber<TRadix>(TInteger number, TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        for (var n = number - TInteger.One; n > TInteger.Zero; n--)
          if (number == n + DigitSum(n, radix))
            return false;

        return true;
      }

      #endregion

      #region IsSingleDigit

      /// <summary>
      /// <para>Indicates whether the <paramref name="value"/> is single digit using the base <paramref name="radix"/>, i.e. in the interval [2, <paramref name="radix"/>).</para>
      /// </summary>
      public static bool IsSingleDigit<TRadix>(TInteger value, TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        System.ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(radix, TRadix.One);

        return TInteger.Abs(value) < TInteger.CreateChecked(radix);
      }

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

      #region Least Common Multiple functions

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

      #region LinearIndexToCartesian (2D & 3D)

      /// <summary>
      /// <para>Converts a <paramref name="linearIndex"/> of a grid with the specified <paramref name="width"/> (the length of the x-axis) to cartesian-coordinates (x, y).</para>
      /// </summary>
      /// <param name="linearIndex"></param>
      /// <param name="width"></param>
      /// <returns>A 2D cartesian-coordinate.</returns>
      public static (TInteger x, TInteger y) LinearIndexToCartesian(TInteger linearIndex, TInteger width)
         => (
          linearIndex % width,
          linearIndex / width
        );

      /// <summary>
      /// <para>Converts a <paramref name="linearIndex"/> of a cube with the <paramref name="width"/> (the length of the x-axis) and <paramref name="height"/> (the length of the y-axis), to cartesian 3D (x, y, z) coordinates.</para>
      /// </summary>
      /// <param name="linearIndex"></param>
      /// <param name="width"></param>
      /// <param name="height"></param>
      /// <returns>A 3D cartesian-coordinate.</returns>
      public static (TInteger x, TInteger y, TInteger z) LinearIndexToCartesian(TInteger linearIndex, TInteger width, TInteger height)
      {
        var xy = width * height;
        var irxy = linearIndex % xy;

        return (
          irxy % width,
          irxy / width,
          linearIndex / xy
        );
      }

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

        a %= m;

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

      // PockHammer (ambiguous, removed) - use FallingFactorial/RisingFactorial instead. There is also generalized versions in the FloatingPoint extensions class.

      #region Pow

      /// <summary>
      /// <para>Indicates whether <paramref name="value"/> is a power of <paramref name="radix"/> (base).</para>
      /// </summary>
      /// <remarks>This version also handles negative values simply by mirroring the corresponding positive value. Zero return as false.</remarks>
      public static bool IsPowOf<TRadix>(TInteger value, TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        if (value <= TInteger.Zero)
          return false;

        System.ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(radix, TRadix.One);

        if (radix == TRadix.One + TRadix.One) // Fast path for radix = 2 (bit trick).
          return (value & (value - TInteger.One)) == TInteger.Zero;

        var v = value;
        var r = TInteger.CreateChecked(radix);

        while (true) // General case: repeatedly divide by radix until it no longer divides, which is O(log n) but extremely fast in practice.
        {
          var q = v / r;
          var p = q * r;

          if (p != v) // If not divisible, it's not a power.
            return false;

          if (q == TInteger.One) // If quotient is 1, we reached radix^k.
            return true;

          v = q;
        }
      }
      //{
      //  if (value <= TInteger.Zero)
      //    return false;

      //  System.ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(radix, TRadix.One);

      //  var log = System.Numerics.BigInteger.Log(System.Numerics.BigInteger.CreateChecked(value), double.CreateChecked(radix));

      //  return FloatingPoint.IsNearInteger(log, out var ilog) && Pow(TInteger.CreateChecked(radix), TInteger.CreateChecked(ilog)) == value;
      //}

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

      #region Pow - Overflow‑safe exponentiation.

      /// <summary>
      /// <para>Overflow‑safe exponentiation: x^k, stops early if exceeding limit.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="exponent"></param>
      /// <param name="limit"></param>
      /// <returns></returns>
      public static TInteger Pow(TInteger value, TInteger exponent, TInteger limit)
      {
        if (TInteger.IsZero(value))
          return TInteger.IsZero(exponent) ? TInteger.One : TInteger.Zero;

        var result = TInteger.One;
        var overflow = limit + TInteger.One;

        while (exponent > TInteger.Zero)
        {
          if (!TInteger.IsZero(exponent & TInteger.One))
          {
            if (result > limit / value) // Check: result * value > limit ?
              return overflow;

            result *= value;

            if (result > limit)
              return overflow;
          }

          exponent >>= 1;

          if (value > limit / value) // Check: value * value > limit ?
            return overflow;

          value *= value;

          if (value > limit)
            return overflow;
        }

        return result;
      }

      #endregion

      #region Prime-Omega functions

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

      #region Probability functions

      /// <summary>
      /// <para>Returns the probability that at least 2 events are equal. This is computation P(A), which is the complement to P(A') computed in (<see cref="OfNoDuplicates(System.Numerics.BigInteger, System.Numerics.BigInteger)"/>).</para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Birthday_problem"/></para>
      /// <para><seealso href="https://en.wikipedia.org/wiki/Conditional_probability"/></para>
      /// </summary>
      /// <returns>The probability, which is in the range [0, 1].</returns>
      public static double ProbabilityOfDuplicates(TInteger whenCount, TInteger ofTotalCount)
        => 1.0 - ProbabilityOfNoDuplicates(whenCount, ofTotalCount);

      /// <summary>
      /// <para>Returns the probability that specified event count in a group of total event count are all different (or unique). This is the computation P(A').</para>
      /// <para><seealso cref="https://en.wikipedia.org/wiki/Birthday_problem"/></para>
      /// <para><seealso cref="https://en.wikipedia.org/wiki/Conditional_probability"/></para>
      /// </summary>
      /// <returns>The probability, which is in the range [0, 1].</returns>
      public static double ProbabilityOfNoDuplicates(TInteger whenCount, TInteger ofTotalCount)
      {
        var accumulation = 1.0;
        for (var index = ofTotalCount - whenCount + TInteger.One; index < ofTotalCount; index++)
          accumulation *= double.CreateChecked(index) / double.CreateChecked(ofTotalCount);
        return accumulation;
      }

      #endregion

      #region ProcessDigits

      /// <summary>
      /// <para>Gets the count, the sum, whether it is jumbled, is a power of, the number reversed, the place values, and the reverse digits, of <paramref name="value"/> using base <paramref name="radix"/>.</para>
      /// </summary>
      public static (TInteger DigitCount, TInteger DigitSum, bool IsJumbled, bool IsPowOf, TInteger NumberReversed, System.Collections.Generic.List<TInteger> PlaceValues, System.Collections.Generic.List<TInteger> ReverseDigits) ProcessDigits<TRadix>(TInteger value, TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        System.ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(radix, TRadix.One);

        var rdx = TInteger.CreateChecked(radix);

        var count = TInteger.Zero;
        var isJumbled = true;
        var numberReversed = TInteger.Zero;
        var placeValues = new System.Collections.Generic.List<TInteger>();
        var reverseDigits = new System.Collections.Generic.List<TInteger>();
        var sum = TInteger.Zero;

        var power = TInteger.One;

        while (!TInteger.IsZero(value))
        {
          var rem = value % rdx;

          count++;
          numberReversed = (numberReversed * rdx) + rem;
          placeValues.Add(rem * power);
          reverseDigits.Add(rem);
          sum += rem;

          power *= rdx;

          value /= rdx;

          if (isJumbled && (TInteger.Abs((value % rdx) - rem) > TInteger.One))
            isJumbled = false;
        }

        if (TInteger.IsZero(count))
        {
          placeValues.Add(count);
          reverseDigits.Add(count);
        }

        return (count, sum, isJumbled, sum == TInteger.One, numberReversed, placeValues, reverseDigits);
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

        var bytes = (stackalloc byte[count]); // Retrieve the byte size of the number, which will be the basis for the bit reversal.

        value.WriteLittleEndian(bytes); // Write as LittleEndian (increasing numeric significance in increasing memory addresses).

        for (var i = bytes.Length - 1; i >= 0; i--)  // After this loop, all bits are reversed.
          byte.ReverseBitsInPlace(ref bytes[i]); // Mirror (reverse) bits in each byte.

        return TInteger.ReadBigEndian(bytes, value.GetType().ImplementsIUnsignedNumber()); // Read as BigEndian (decreasing numeric significance in increasing memory addresses).
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

        var bytes = (stackalloc byte[count]); // Retrieve the byte size of the number, which will be the basis for the bit reversal.

        // We can use either direction here, write-LE/read-BE or write-BE/read-LE, doesn't really matter, since the end result is the same.

        value.WriteLittleEndian(bytes); // Write as LittleEndian (increasing numeric significance in increasing memory addresses).

        return TInteger.ReadBigEndian(bytes, value.GetType().ImplementsIUnsignedNumber()); // Read as BigEndian (decreasing numeric significance in increasing memory addresses).
      }

      #endregion

      #region Round..ToPowerOf2 functions

      #region RoundUpToPowerOf2

      public static TInteger RoundUpToPowerOf2(TInteger value, bool unequal)
      {
        var ms1b = MostSignificant1Bit(TInteger.Abs(value));

        if (unequal || ms1b != value)
          ms1b <<= 1;

        return TInteger.CopySign(ms1b, value);
      }

      #endregion

      #region RoundDownToPowerOf2

      public static TInteger RoundDownToPowerOf2(TInteger value, bool unequal)
      {
        var ms1b = MostSignificant1Bit(TInteger.Abs(value));

        if (unequal && ms1b == value)
          ms1b >>= 1;

        return TInteger.CopySign(ms1b, value);
      }

      #endregion

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

        var bytes = (stackalloc byte[value.GetByteCount()]);

        value.WriteLittleEndian(bytes);

        rng.Shuffle(bytes);

        return TInteger.ReadLittleEndian(bytes, value.GetType().ImplementsIUnsignedNumber());
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

      #region ..Significant1Bit functions

      #region ClearLeastSignificant1Bit

      /// <summary>
      /// <para>Clear <paramref name="value"/> of its least-significant-1-bit.</para>
      /// </summary>
      /// <see href="https://aggregate.org/MAGIC/#Least%20Significant%201%20Bit"/>
      public static TInteger ClearLeastSignificant1Bit(TInteger value)
        => value & (value - TInteger.One);

      #endregion

      #region ClearMostSignificant1Bit

      /// <summary>
      /// <para>Clear <paramref name="value"/> of its least-significant-1-bit.</para>
      /// </summary>
      /// <see href="https://aggregate.org/MAGIC/#Most%20Significant%201%20Bit"/>
      public static TInteger ClearMostSignificant1Bit(TInteger value)
        => value - MostSignificant1Bit(value);

      #endregion

      #region LeastSignificant1Bit

      /// <summary>
      /// <para>Extracts the lowest numbered element of a bit set (<paramref name="value"/>). Given a 2's complement binary integer value, this is the least-significant-1-bit.</para>
      /// </summary>
      /// <remarks>The LS1B is the largest power of two that is also a divisor of <paramref name="value"/>.</remarks>
      /// <see href="https://aggregate.org/MAGIC/#Least%20Significant%201%20Bit"/>
      public static TInteger LeastSignificant1Bit(TInteger value)
        => value & ((~value) + TInteger.One);
      //=> (value & -value); // <<< This optimized version does not work on unsigned integers, obviously since the number has to be negated.

      #endregion

      #region MostSignificant1Bit

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
        => TInteger.IsZero(value) ? value : TInteger.One << int.CreateChecked(TInteger.Log2(value)); // TInteger.One << (value.GetBitLength() - 1);

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
      /// <para><see href="https://dmitrybrant.com/2008/04/29/binomial-coefficients-stirling-numbers-csharp"/></para>
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
        var digitCount = DigitCount(value, radix); // Digit count of the "integer part", e.g. an integer 123 = 3 digits.

        var fractionalPart = Pow(radix, digitCount); // With the digit count we can create a power-of-radix of the same magnitude as the digit count, e.g. 3 digits = 1000 (radix = 10).

        return decimal.CreateChecked(value) / decimal.CreateChecked(fractionalPart); // E.g. 123. / 1000 = .123 
      }

      #endregion

      #region ToOrdinalFieldName(s)

      /// <summary>
      /// <para>Returns a generic <paramref name="fieldNamePrefix"/> for the <paramref name="fieldIndex"/> as if it was an index of a 0-based column-structure.</para>
      /// <para>+1 is added to the <paramref name="fieldIndex"/> so that the first column (the zeroth) is always "Column1", and the second column (#1) is "Column2", i.e. the column names are ordinal.</para>
      /// </summary>
      /// <param name="fieldIndex"></param>
      /// <param name="numericWidth"></param>
      /// <param name="fieldNamePrefix"></param>
      /// <returns></returns>
      public static string ToOrdinalFieldName(TInteger fieldIndex, int numericWidth, string fieldNamePrefix = "Column")
        => fieldNamePrefix + (fieldIndex + TInteger.One).ToString($"D{numericWidth}", null);

      /// <summary>
      /// <para>Returns a generic <paramref name="fieldNamePrefix"/> for the <paramref name="fieldIndex"/> as if it was an index of a 0-based column-structure.</para>
      /// <para>+1 is added to the <paramref name="fieldIndex"/> so that the first column (the zeroth) is always "Column1", and the second column (#1) is "Column2", i.e. the column names are ordinal.</para>
      /// </summary>
      /// <param name="fieldIndex"></param>
      /// <param name="fieldNamePrefix"></param>
      /// <returns></returns>
      public static string ToOrdinalFieldName(TInteger fieldIndex, string fieldNamePrefix = "Column")
        => ToOrdinalFieldName(fieldIndex, int.CreateChecked(fieldIndex <= TInteger.Zero ? TInteger.Zero : DigitCount(fieldIndex, TInteger.CreateChecked(10))), fieldNamePrefix);

      /// <summary>
      /// <para>Creates an array of generic column-<paramref name="fieldNamePrefix"/>s for <paramref name="fieldCount"/> amount of columns.</para>
      /// <example><paramref name="fieldCount"/> = 3, returns <c>["Column1", "Column2", "Column3"]</c></example>
      /// </summary>
      /// <param name="fieldCount"></param>
      /// <param name="fieldNamePrefix"></param>
      /// <returns></returns>
      public static string[] ToOrdinalFieldNames(TInteger fieldCount, string fieldNamePrefix = "Column")
      {
        var maxWidth = int.CreateChecked(DigitCount(fieldCount, 10));

        return [.. Number.ArithmeticSequence(TInteger.One, fieldCount).Select(ci => ToOrdinalFieldName(ci, maxWidth, fieldNamePrefix))];
      }

      #endregion

      public static bool TryConvertTo<TTarget>(TInteger source, out TTarget target)
        where TTarget : System.Numerics.IBinaryInteger<TTarget>
      {
        target = TTarget.CreateTruncating(source); // Convert TInteger → TTarget (truncating, never throws)

        var roundTrip = TInteger.CreateTruncating(target); // Convert back TTarget → TInteger (truncating, never throws)

        return roundTrip == source; // If round-trip preserved the value, it fits exactly.
      }

      //#region Twelvefold way

      ///// <summary>
      ///// <para><see href="https://en.wikipedia.org/wiki/Twelvefold_way"/></para>
      ///// </summary>
      ///// <param name="x"></param>
      ///// <param name="n"></param>
      ///// <returns></returns>
      //public static TInteger AnyDistinct(TInteger x, TInteger n) => Pow(x, n);

      ///// <summary>
      ///// <para><see href="https://en.wikipedia.org/wiki/Twelvefold_way"/></para>
      ///// </summary>
      ///// <param name="x"></param>
      ///// <param name="n"></param>
      ///// <returns></returns>
      //public static TInteger InjectiveDistinct(TInteger x, TInteger n) => FallingFactorial(x, n);

      ///// <summary>
      ///// <para><see href="https://en.wikipedia.org/wiki/Twelvefold_way"/></para>
      ///// </summary>
      ///// <param name="x"></param>
      ///// <param name="n"></param>
      ///// <returns></returns>
      //public static TInteger SurjectiveDistinct(TInteger x, TInteger n) => Factorial(x) * StirlingNumber2ndKind(n, x);

      ///// <summary>
      ///// <para><see href="https://en.wikipedia.org/wiki/Twelvefold_way"/></para>
      ///// </summary>
      ///// <param name="x"></param>
      ///// <param name="n"></param>
      ///// <returns></returns>
      //public static TInteger AnySnOrbits(TInteger x, TInteger n) => BinomialCoefficient(x + n - TInteger.One, n);

      ///// <summary>
      ///// <para><see href="https://en.wikipedia.org/wiki/Twelvefold_way"/></para>
      ///// </summary>
      ///// <param name="x"></param>
      ///// <param name="n"></param>
      ///// <returns></returns>
      //public static TInteger InjectiveSnOrbits(TInteger x, TInteger n) => BinomialCoefficient(x, n);

      ///// <summary>
      ///// <para><see href="https://en.wikipedia.org/wiki/Twelvefold_way"/></para>
      ///// </summary>
      ///// <param name="x"></param>
      ///// <param name="n"></param>
      ///// <returns></returns>
      //public static TInteger SurjectiveSnOrbits(TInteger x, TInteger n) => BinomialCoefficient(n - TInteger.One, n - x);

      ///// <summary>
      ///// <para><see href="https://en.wikipedia.org/wiki/Twelvefold_way"/></para>
      ///// </summary>
      ///// <param name="x"></param>
      ///// <param name="n"></param>
      ///// <returns></returns>
      //public static TInteger AnySxOrbits(TInteger x, TInteger n) => throw new System.NotImplementedException();

      ///// <summary>
      ///// <para><see href="https://en.wikipedia.org/wiki/Twelvefold_way"/></para>
      ///// </summary>
      ///// <param name="x"></param>
      ///// <param name="n"></param>
      ///// <returns></returns>
      //public static TInteger InjectiveSxOrbits(TInteger x, TInteger n) => throw new System.NotImplementedException();

      ///// <summary>
      ///// <para><see href="https://en.wikipedia.org/wiki/Twelvefold_way"/></para>
      ///// </summary>
      ///// <param name="x"></param>
      ///// <param name="n"></param>
      ///// <returns></returns>
      //public static TInteger SurjectiveSxOrbits(TInteger x, TInteger n) => StirlingNumber2ndKind(n, x);

      ///// <summary>
      ///// <para><see href="https://en.wikipedia.org/wiki/Twelvefold_way"/></para>
      ///// </summary>
      ///// <param name="x"></param>
      ///// <param name="n"></param>
      ///// <returns></returns>
      //public static TInteger AnySnSxOrbits(TInteger x, TInteger n) => throw new System.NotImplementedException();

      ///// <summary>
      ///// <para><see href="https://en.wikipedia.org/wiki/Twelvefold_way"/></para>
      ///// </summary>
      ///// <param name="x"></param>
      ///// <param name="n"></param>
      ///// <returns></returns>
      //public static TInteger InjectiveSnSxOrbits(TInteger x, TInteger n) => throw new System.NotImplementedException();

      ///// <summary>
      ///// <para><see href="https://en.wikipedia.org/wiki/Twelvefold_way"/></para>
      ///// </summary>
      ///// <param name="x"></param>
      ///// <param name="n"></param>
      ///// <returns></returns>
      //public static TInteger SurjectiveSnSxOrbits(TInteger x, TInteger n) => throw new System.NotImplementedException();

      //#endregion
    }




    extension<TInteger>(TInteger value)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      #region GetBitLength

      /// <summary>
      /// <para>Returns the size, in bits, of the shortest two's-complement representation, if <paramref name="value"/> is positive. If <paramref name="value"/> is negative, the bit-length represents the storage size of the <typeparamref name="TInteger"/>, based on byte-count (times 8).</para>
      /// </summary>
      /// <remarks>
      /// <para>The <c>bit-length(<paramref name="value"/>)</c> is the bit position (i.e. a 1-based bit-index) of the <c>most-significant-1-bit(<paramref name="value"/>)</c>. A zero-based bit-index is equal to <c>(bit-length(<paramref name="value"/>) - 1)</c>, which is also the same as calling <c>log2(<paramref name="value"/>)</c>.</para>
      /// </remarks>
      public int GetBitLength()
        => TInteger.IsNegative(value)
        ? GetBitCount(value) // When value is negative, return the bit-count (i.e. based on the storage strategy).
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

      #region To..String functions

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
        if (minLength <= 0) minLength = GetBitCount(value);

        alphabet ??= System.Text.Encoding.Base62;

        if (alphabet.Length < 2) throw new System.ArgumentOutOfRangeException(nameof(alphabet));

        var indices = new System.Collections.Generic.List<int>();

        for (var bitIndex = int.Min(int.Max(value.GetBitLength(), minLength), GetBitCount(value)) - 1; bitIndex >= 0; bitIndex--)
        {
          var bitValue = int.CreateChecked((value >>> bitIndex) & TInteger.One);

          if (bitValue > 0 || indices.Count > 0 || bitIndex < minLength)
            indices.Add(bitValue);
        }

        indices.TryTransposePositionalNotationIndicesToSymbols(alphabet, out System.Collections.Generic.List<char> symbols);

        return symbols.AsSpan().ToString();
      }

      /// <summary>
      /// <para>Converts a <paramref name="value"/> to a decimal (base 10) string based on <paramref name="minLength"/>, <paramref name="negativeSymbol"/> and an <paramref name="alphabet"/> (<see cref="string.Base62"/> if null).</para>
      /// </summary>
      /// <param name="minLength"></param>
      /// <param name="negativeSymbol"></param>
      /// <param name="alphabet">If <see langword="null"/> then <see cref="string.Base62"/>.</param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public string ToDecimalString(int minLength = 1, string? alphabet = null, char negativeSymbol = '\u002D')
      {
        if (minLength <= 0) minLength = GetMaxDigitCount(GetBitCount(value), 10, value.GetType().ImplementsISignedNumber());

        alphabet ??= System.Text.Encoding.Base62;

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
      /// <para>Converts a <paramref name="value"/> to a hexadecimal (base 16) string based on <paramref name="minLength"/> and an <paramref name="alphabet"/> (<see cref="string.Base62"/> if null).</para>
      /// </summary>
      /// <param name="minLength"></param>
      /// <param name="alphabet">If <see langword="null"/> then <see cref="string.Base62"/>.</param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public string ToHexadecimalString(int minLength = 1, string? alphabet = null)
      {
        if (minLength <= 0) minLength = GetMaxDigitCount(GetBitCount(value), 16, value.GetType().ImplementsISignedNumber());

        alphabet ??= System.Text.Encoding.Base62;

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
      /// <para>Converts a <paramref name="value"/> to a octal (base 8) string based on <paramref name="minLength"/> and an <paramref name="alphabet"/> (<see cref="string.Base62"/> if null).</para>
      /// </summary>
      /// <param name="minLength"></param>
      /// <param name="alphabet">If <see langword="null"/> then <see cref="string.Base62"/>.</param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public string ToOctalString(int minLength = 1, string? alphabet = null)
      {
        if (minLength <= 0) minLength = GetMaxDigitCount(GetBitCount(value), 8, value.GetType().ImplementsISignedNumber());

        alphabet ??= System.Text.Encoding.Base62;

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
      /// <para>Converts a <paramref name="value"/> to text based on <paramref name="radix"/>, <paramref name="minLength"/> and an <paramref name="alphabet"/> (<see cref="string.Base62"/> if null).</para>
      /// </summary>
      /// <typeparam name="TRadix"></typeparam>
      /// <param name="radix"></param>
      /// <param name="minLength"></param>
      /// <param name="alphabet">If <see langword="null"/> then <see cref="string.Base62"/>.</param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public string ToRadixString<TRadix>(TRadix radix, int minLength = 1, string? alphabet = null)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        System.ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(radix, TRadix.One);

        var rdx = int.CreateChecked(radix);

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
          if (minLength <= 0) minLength = GetMaxDigitCount(GetBitCount(value), rdx, value.GetType().ImplementsISignedNumber());

          alphabet ??= System.Text.Encoding.Base62;

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

        return ToRadixString(value, radix, minLength, alphabet); // Extra top-limit to radix (only 10 characters in subscript alphabet).
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

        return ToRadixString(value, radix, minLength, alphabet); // Extra top-limit to radix (only 16 characters in superscript alphabet, but choice of lower/upper case).
      }

      #endregion
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
    public static System.Collections.Generic.List<int> GenerateDeBruijnSequence(int k, int n)
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
    public static System.Collections.Generic.IEnumerable<int[]> GenerateDeBruijnSequenceExpanded(int k, int n)
      => GenerateDeBruijnSequence(k, n).PartitionNgram(n, (e, i) => e.ToArray());

    /// <summary>
    /// <para>Creates a new expanded DeBruijn sequence of symbols based on <paramref name="k"/>, <paramref name="n"/> and <paramref name="alphabet"/>.</para>
    /// </summary>
    /// <typeparam name="TSymbol"></typeparam>
    /// <param name="k"></param>
    /// <param name="n"></param>
    /// <param name="alphabet"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<System.Collections.Generic.List<TSymbol>> GenerateDeBruijnSequenceExpandedSymbols<TSymbol>(int k, int n, params TSymbol[] alphabet)
      => GenerateDeBruijnSequence(k, n).PartitionNgram(n, (e, i) => e.Select(i => alphabet[i]).ToList());

    #endregion
  }
}

// <seealso cref="http://aggregate.org/MAGIC/"/>
// <seealso cref="http://graphics.stanford.edu/~seander/bithacks.html"/>
