namespace Flux
{
  public static partial class BinaryInteger
  {
    extension<TInteger>(TInteger)
      where TInteger : System.Numerics.IBinaryInteger<TInteger>
    {
      //#region CartesianToLinearIndex (2D & 3D)

      ///// <summary>
      ///// <para>Converts cartesian-coordinates (<paramref name="x"/>, <paramref name="y"/>) to a linear index of a grid with the specified <paramref name="width"/> (the length of the x-axis).</para>
      ///// </summary>
      //public static TInteger CartesianToLinearIndex(TInteger x, TInteger y, TInteger width)
      //{
      //  System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(width);

      //  checked
      //  {
      //    return x + (y * width);
      //  }
      //}

      ///// <summary>
      ///// <para>Converts cartesian-coordinates (<paramref name="x"/>, <paramref name="y"/>, <paramref name="z"/>) to a linear index of a cube with the specified <paramref name="width"/> (the length of the x-axis) and <paramref name="height"/> (the length of the y-axis).</para>
      ///// </summary>
      //public static TInteger CartesianToLinearIndex(TInteger x, TInteger y, TInteger z, TInteger width, TInteger height)
      //{
      //  System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(width);
      //  System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(height);

      //  checked
      //  {
      //    return x + (y * width) + (z * width * height);
      //  }
      //}

      //#endregion

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

      //#region Gray

      ///// <summary>
      ///// <para>Converts a binary number to a reflected binary Gray code.</para>
      ///// <see href="https://en.wikipedia.org/wiki/Gray_code"/>
      ///// </summary>
      //public static TInteger BinaryToGray(TInteger value)
      //  => value ^ (value >>> 1);

      ///// <summary>
      ///// <para>Converts a reflected binary gray code to a binary number.</para>
      ///// <see href="https://en.wikipedia.org/wiki/Gray_code"/>
      ///// </summary>
      //public static TInteger GrayToBinary(TInteger value)
      //{
      //  var mask = value;

      //  while (!TInteger.IsZero(mask))
      //  {
      //    mask >>>= 1;
      //    value ^= mask;
      //  }

      //  return value;
      //}

      //#endregion

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
        => GenerateSubRangesBySubLength(length, Number.IntegerDivRemCeiling(length, count).Quotient);

      #endregion

      #region CubeRoot functions

      /// <summary>
      /// <para>Computes the integer (floor) cube-root of a value.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <returns>The square-root of the value.</returns>
      public static TInteger CubeRoot(TInteger value)
      {
        if (TInteger.IsZero(value))
          return value;

        var abs = TInteger.Abs(value);
        var sign = TInteger.CopySign(TInteger.One, value);

        if (TryConvertTo(abs, out ulong ulv))
          return TInteger.CreateChecked(ulong.Cbrt(ulv)) * sign;

        return TInteger.CreateChecked(System.Numerics.BigInteger.Cbrt(System.Numerics.BigInteger.CreateChecked(abs))) * sign;
      }

      /// <summary>
      /// <para>Indicates whether <paramref name="value"/> is the integer (not necessarily perfect) square of <paramref name="root"/>.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="value">The square value to find the square-<paramref name="root"/> of.</param>
      /// <param name="root">The resulting square-root of <paramref name="value"/>.</param>
      /// <returns>Whether the <paramref name="value"/> is the integer (not necessarily perfect) square of <paramref name="root"/>.</returns>
      public static bool IsCubeRoot(TInteger value, TInteger root)
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
      public static bool IsPerfectCubeRoot(TInteger value, TInteger root)
        => value == (root * root * root);

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

      #region Log

      /// <summary>
      /// <para>Returns the integer (toward-zero, away-from-zero) logarithm of specified a <paramref name="value"/> in a specified <paramref name="radix"/>.</para>
      /// </summary>
      /// <typeparam name="TRadix"></typeparam>
      /// <param name="value"></param>
      /// <param name="radix"></param>
      /// <returns></returns>
      public static (TInteger LogTowardZero, TInteger LogAwayFromZero, bool IsExactLog) Log<TRadix>(TInteger value, TRadix radix)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        System.ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(radix, TRadix.One);

        if (TInteger.IsZero(value))
          return (value, value, false);

        var abs = TInteger.Abs(value);
        var sign = TInteger.CopySign(TInteger.One, value);

        if (TryConvertTo(abs, out ulong ulv) && TryConvertTo(radix, out ulong ulr))
        {
          var (LogF, LogC, IsExactLog) = ulong.Log(ulv, ulr);

          return (TInteger.CreateChecked(LogF) * sign, TInteger.CreateChecked(LogC) * sign, IsExactLog);
        }
        else // Fall back on BigInteger LOG implementation.
        {
          var (LogF, LogC, IsExactLog) = System.Numerics.BigInteger.Log(System.Numerics.BigInteger.CreateChecked(abs), System.Numerics.BigInteger.CreateChecked(radix));

          return (TInteger.CreateChecked(LogF) * sign, TInteger.CreateChecked(LogC) * sign, IsExactLog);
        }
      }

      #endregion

      #region LogE

      /// <summary>
      /// <para>Returns the integer (floor) natural logarithm of specified a <paramref name="value"/> in a base/radix E.</para>
      /// </summary>
      /// <typeparam name="TRadix"></typeparam>
      /// <param name="value"></param>
      /// <returns></returns>
      public static (TInteger LogTowardZero, TInteger LogAwayFromZero) LogE<TRadix>(TInteger value)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        if (TInteger.IsZero(value))
          return (value, value);

        var absV = TInteger.Abs(value);
        var sign = TInteger.CopySign(TInteger.One, value);

        if (TryConvertTo(absV, out ulong ulv))
        {
          var (ilogf, ilogc) = ulong.LogE(ulv);

          return (TInteger.CreateChecked(ilogf) * sign, TInteger.CreateChecked(ilogc) * sign);
        }
        else // Fall-back on BigInteger ILog, which is more expensive but can handle any size of integer.
        {
          var (ilogf, ilogc) = System.Numerics.BigInteger.LogE(System.Numerics.BigInteger.CreateChecked(absV));

          return (TInteger.CreateChecked(ilogf) * sign, TInteger.CreateChecked(ilogc) * sign);
        }
      }

      #endregion

      #region RootN functions

      //return TInteger.CreateChecked(BigIntegerExtensions.RootN(System.Numerics.BigInteger.CreateChecked(value), int.CreateChecked(degree)));

      private static TInteger RootNCore(TInteger value, TInteger degree)
      {
        if (value <= TInteger.One || degree <= TInteger.One)
          return value;

        var x = TInteger.One << int.CreateChecked(TInteger.Log2(value) / degree); // Initial estimate: 2^(floor(log2(value)/degree))

        while (true)
        {
          var prev = x;

          var t = Pow(x, degree - TInteger.One, value);

          if (t == TInteger.Zero)
            throw new ArithmeticException();

          x = ((degree - TInteger.One) * x + value / t) / degree;

          if (x == prev)
          {
            while (Pow(x + TInteger.One, degree, value) <= value) // Floor-root correction.
              x++;

            while (Pow(x, degree, value) > value)
              x--;

            return x;
          }
        }
      }

      /// <summary>
      /// <para>Computes the integer nth-root of a value.</para>
      /// </summary>
      /// <typeparam name="TNth"></typeparam>
      /// <param name="value"></param>
      /// <param name="exponent"></param>
      /// <returns></returns>
      public static TInteger RootN(TInteger value, TInteger degree)
      {
        //return TInteger.CreateChecked(BigIntegerExtensions.RootN(System.Numerics.BigInteger.CreateChecked(value), int.CreateChecked(degree)));
        System.ArgumentOutOfRangeException.ThrowIfNegative(value);
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(degree);

        return RootNCore(value, degree);
      }

      public static bool TryGetExactRoot(TInteger value, TInteger degree, out TInteger root)
      {
        root = RootNCore(value, degree);

        return Pow(root, degree, value) == value;
      }

      public static bool IsRootN<TNth>(TInteger value, TNth n, TInteger root)
        where TNth : System.Numerics.IBinaryInteger<TNth>
        => value >= Pow(root, n) // If GTE to nth of root.
        && value < Pow(root + TInteger.One, n); // And if LT to nth of (root + 1).

      public static bool IsPerfectRootN<TNth>(TInteger value, TNth n, TInteger root)
        where TNth : System.Numerics.IBinaryInteger<TNth>
        => value == Pow(root, n);

      #endregion

      #region SquareRoot functions

      /// <summary>
      /// <para>Computes the integer square-root of a value.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <returns></returns>
      public static TInteger SquareRoot(TInteger value)
      {
        if (TInteger.IsZero(value))
          return value;

        var abs = TInteger.Abs(value);
        var sign = TInteger.CopySign(TInteger.One, value);

        if (TryConvertTo(abs, out ulong ulv))
          return TInteger.CreateChecked(ulong.Sqrt(ulv)) * sign;
        else // Fall back on BigInteger SQRT implementation.
          return TInteger.CreateChecked(System.Numerics.BigInteger.Sqrt(System.Numerics.BigInteger.CreateChecked(abs))) * sign;
      }

      /// <summary>
      /// <para>Indicates whether <paramref name="value"/> is the integer (not necessarily perfect) square of <paramref name="root"/>.</para>
      /// </summary>
      /// <typeparam name="TInteger"></typeparam>
      /// <param name="value">The square value to find the square-<paramref name="root"/> of.</param>
      /// <param name="root">The resulting square-root of <paramref name="value"/>.</param>
      /// <returns>Whether the <paramref name="value"/> is the integer (not necessarily perfect) square of <paramref name="root"/>.</returns>
      public static bool IsSquareRoot(TInteger value, TInteger root)
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
      public static bool IsPerfectSquareRoot(TInteger value, TInteger root)
        => value == (root * root);

      #endregion

      //public static TInteger ISqrt(TInteger value)
      //{
      //  System.ArgumentOutOfRangeException.ThrowIfNegative(value);

      //  if (TInteger.IsZero(value))
      //    return value;

      //  var log2 = int.CreateChecked(TInteger.Log2(value));

      //  if (log2 < 128) // First check if the 128‑bit restoring fast path is applicable.
      //  {
      //    var n = value;
      //    var r = TInteger.Zero;
      //    var bit = TInteger.One << (int.CreateChecked(TInteger.Log2(value)) & ~1);

      //    bit >>= int.CreateChecked((-(bit > n ? TInteger.One : TInteger.Zero)) & TInteger.CreateChecked(2)); // Align bit without branch

      //    while (bit != TInteger.Zero)
      //    {
      //      var t = r + bit;
      //      var ge = -(n >= t ? TInteger.One : TInteger.Zero);
      //      n -= t & ge;
      //      r = (r >> 1) + (bit & ge);
      //      bit >>= 2;
      //    }

      //    return TInteger.CreateChecked(r);
      //  }
      //  else // For larger values, fall back to the integer Newton iteration.
      //  {
      //    var x = TInteger.One << (value.GetBitLength() / 2); // Initial approximation: 2^(bitLength/2)

      //    while (true)
      //    {
      //      var y = (x + value / x) >> 1;

      //      if (x - y is var diff && diff <= TInteger.One && diff >= TInteger.Zero)
      //        return y;

      //      x = y;
      //    }
      //  }
      //}

      //#region LinearIndexToCartesian (2D & 3D)

      ///// <summary>
      ///// <para>Converts a <paramref name="linearIndex"/> of a grid with the specified <paramref name="width"/> (the length of the x-axis) to cartesian-coordinates (x, y).</para>
      ///// </summary>
      ///// <param name="linearIndex"></param>
      ///// <param name="width"></param>
      ///// <returns>A 2D cartesian-coordinate.</returns>
      //public static (TInteger x, TInteger y) LinearIndexToCartesian(TInteger linearIndex, TInteger width)
      //{
      //  System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(width);

      //  return (
      //    linearIndex % width,
      //    linearIndex / width
      //  );
      //}

      ///// <summary>
      ///// <para>Converts a <paramref name="linearIndex"/> of a cube with the <paramref name="width"/> (the length of the x-axis) and <paramref name="height"/> (the length of the y-axis), to cartesian 3D (x, y, z) coordinates.</para>
      ///// </summary>
      ///// <param name="linearIndex"></param>
      ///// <param name="width"></param>
      ///// <param name="height"></param>
      ///// <returns>A 3D cartesian-coordinate.</returns>
      //public static (TInteger x, TInteger y, TInteger z) LinearIndexToCartesian(TInteger linearIndex, TInteger width, TInteger height)
      //{
      //  System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(width);
      //  System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(height);

      //  var xy = checked(width * height);
      //  var irxy = linearIndex % xy;

      //  return (
      //    irxy % width,
      //    irxy / width,
      //    linearIndex / xy
      //  );
      //}

      //#endregion

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
      /// <para></para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="exponent"></param>
      /// <returns></returns>
      public static TInteger Pow<TExponent>(TInteger value, TExponent exponent)
        where TExponent : System.Numerics.IBinaryInteger<TExponent>
      {
        System.ArgumentOutOfRangeException.ThrowIfNegative(exponent);

        if (TInteger.IsZero(value)) return TInteger.Zero;
        if (TExponent.IsZero(exponent)) return TInteger.One;

        var result = TInteger.One;
        var current = value;

        while (exponent > TExponent.Zero)
          checked // Detect overflow.
          {
            if (!TExponent.IsZero(exponent & TExponent.One))
              result *= current;

            exponent >>= 1;

            if (exponent > TExponent.Zero)
              current *= current;
          }

        return result;
      }

      ///// <summary>
      ///// <para>Computes <paramref name="value"/> raised to the power of <paramref name="exponent"/>.</para>
      ///// <para>Uses the built-in <see cref="System.Numerics.BigInteger"/> function.</para>
      ///// </summary>
      ///// <typeparam name="TInteger"></typeparam>
      ///// <param name="exponent">The exponent with which to raise the value.</param>
      ///// <returns>The value raised to the <paramref name="exponent"/>-of.</returns>
      ///// <remarks>If <paramref name="value"/> and/or <paramref name="exponent"/> are zero, 1 is returned. I.e. 0&#x2070;, x&#x2070; and 0&#x02E3; all return 1 in this version.</remarks>
      ///// <exception cref="System.ArgumentOutOfRangeException"></exception>
      //public static TInteger Pow<TExponent>(TInteger value, TExponent exponent)
      //  where TExponent : System.Numerics.IBinaryInteger<TExponent>
      //  => TInteger.CreateChecked(System.Numerics.BigInteger.Pow(System.Numerics.BigInteger.CreateChecked(value), int.CreateChecked(exponent)));

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

          if (TInteger.IsZero(exponent))
            break;

          if (value > limit / value) // Check: value * value > limit ?
            return overflow;

          value *= value;

          if (value > limit)
            return overflow;
        }

        return result;
      }

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

      //#region TileToLatLon

      ///// <summary>
      ///// <para>Convert XYZ Tile Scheme (z, x, y) to geographic coordinates.</para>
      ///// </summary>
      ///// <param name="z">
      ///// <para>Controls how many tiles exists:</para>
      ///// <list type="bullet">
      ///// <item>zoom = 0 → 1 tile</item>
      ///// <item>zoom = 1 → 2×2 tiles</item>
      ///// <item>zoom = 2 → 4×4 tiles</item>
      ///// <item>zoom = 18–19 → very detailed street‑level tiles</item>
      ///// </list>
      ///// </param>
      ///// <param name="x">
      ///// <list type="bullet">
      ///// <item>x = 0 is the far west</item>
      ///// <item>x increases as you move east</item>
      ///// </list>
      ///// </param>
      ///// <param name="y">
      ///// <list type="bullet">
      ///// <item>y = 0 is the top(north)</item>
      ///// <item>y increases as you move south</item>
      ///// </list>
      ///// </param>
      ///// <returns></returns>
      //public static void TileToLatLon<TFloat>(TInteger z, TInteger x, TInteger y, out TFloat latitude, out TFloat longitude)
      //  where TFloat : System.Numerics.IFloatingPointIeee754<TFloat>
      //{
      //  var n = TFloat.Pow(TFloat.CreateChecked(2), TFloat.CreateChecked(z));

      //  longitude = TFloat.RadiansToDegrees(TFloat.CreateChecked(x) / n * TFloat.Tau - TFloat.Pi); // West corner of tile.
      //  latitude = TFloat.RadiansToDegrees(TFloat.Atan(TFloat.Sinh(TFloat.Pi * (TFloat.One - TFloat.CreateChecked(2) * TFloat.CreateChecked(y) / n)))); // North corner of tile.
      //}

      //#endregion

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

      public static TInteger Wrap(TInteger value, TInteger min, TInteger max, IntervalNotation notation)
      {
        var range = max - min;

        if (notation == IntervalNotation.Closed)
          range += TInteger.One;
        else if (notation == IntervalNotation.Open)
          range -= TInteger.One;

        var shift = min;

        if (notation == IntervalNotation.HalfOpenLeft)
          shift = max;
        else if (notation == IntervalNotation.Open)
          shift += TInteger.One;

        return shift + Number.EuclideanModulo(value - shift, range);
      }

      #region WrapToInterval

      /// <summary>
      /// <para>Wraps a value to a specified interval, with options for wrap mode and interval notation.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="minValue"></param>
      /// <param name="maxValue"></param>
      /// <param name="wrapMode"></param>
      /// <param name="intervalNotation"></param>
      /// <returns></returns>
      public static TInteger WrapToInterval(TInteger value, TInteger minValue, TInteger maxValue, IntervalNotation intervalNotation = IntervalNotation.HalfOpenRight)
      {
        var range = maxValue - minValue;
        if (range == TInteger.Zero)
          return minValue;

        var wrapped = (value - minValue) % range;
        if (wrapped < TInteger.Zero)
          wrapped += range;

        var result = wrapped + minValue;

        return intervalNotation switch
        {
          IntervalNotation.Closed => result,
          IntervalNotation.HalfOpenRight => result == maxValue ? minValue : result,
          IntervalNotation.HalfOpenLeft => result == minValue ? maxValue : result,
          IntervalNotation.Open => (result == minValue) ? minValue + TInteger.One : (result == maxValue) ? maxValue - TInteger.One : result,
          _ => result,
        };
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
