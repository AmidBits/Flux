namespace Flux.Numerics
{
  // A rational number (commonly called a fraction) is a ratio between two integers. For example (3/6) = (2/4) = (1/2)
  //
  // a/b = c/d, iff ad = bc
  // -(a/b)     == (-a)/b
  // (a/b)^(-1) == b/a, if a != 0
  //
  // Euclid's algorithm is used to simplify the fraction.
  // Calculating the greatest common divisor of two n-digit numbers can be found in O(n(log n)^5 (log log n)) steps as n -> +infinity

  /// <summary>
  /// <para>BigRational is a ratio of two <see cref="System.Numerics.BigInteger"/>s, i.e. in the form of numerator and denominator, or commonly called a fraction.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Rational_number"/></para>
  /// <para><seealso href="https://github.com/kiprobinson/BigFraction"/></para>
  /// <para><seealso href="https://github.com/bazzilic/BigFraction"/></para>
  /// <para></para>
  /// </summary>
  public readonly record struct BigRational
    : System.IComparable, System.IComparable<BigRational>
    //, System.IConvertible
    , System.IFormattable
    , System.Numerics.IAdditiveIdentity<BigRational, BigRational>
    , System.Numerics.IAdditionOperators<BigRational, BigRational, BigRational>, System.Numerics.IAdditionOperators<BigRational, System.Numerics.BigInteger, BigRational>
    , System.Numerics.IBitwiseOperators<BigRational, BigRational, BigRational>
    , System.Numerics.IComparisonOperators<BigRational, BigRational, bool>
    , System.Numerics.IDecrementOperators<BigRational>
    , System.Numerics.IDivisionOperators<BigRational, BigRational, BigRational>, System.Numerics.IDivisionOperators<BigRational, System.Numerics.BigInteger, BigRational>
    , System.Numerics.IEqualityOperators<BigRational, BigRational, bool>
    , System.Numerics.IFloatingPointConstants<BigRational>
    , System.Numerics.IIncrementOperators<BigRational>
    //, System.Numerics.ILogarithmicFunctions<BigRational>
    , System.Numerics.IModulusOperators<BigRational, BigRational, BigRational>, System.Numerics.IModulusOperators<BigRational, System.Numerics.BigInteger, BigRational>
    , System.Numerics.IMultiplicativeIdentity<BigRational, BigRational>
    , System.Numerics.IMultiplyOperators<BigRational, BigRational, BigRational>, System.Numerics.IMultiplyOperators<BigRational, System.Numerics.BigInteger, BigRational>
    , System.Numerics.INumber<BigRational>
    , System.Numerics.INumberBase<BigRational>
    , System.Numerics.IPowerFunctions<BigRational>
    , System.Numerics.IRootFunctions<BigRational>
    , System.Numerics.ISignedNumber<BigRational>
    , System.Numerics.ISubtractionOperators<BigRational, BigRational, BigRational>, System.Numerics.ISubtractionOperators<BigRational, System.Numerics.BigInteger, BigRational>
    , System.Numerics.IUnaryNegationOperators<BigRational, BigRational>
    , System.Numerics.IUnaryPlusOperators<BigRational, BigRational>
    , Units.IValueQuantifiable<double>
  {
    //private static readonly BigRational EpsilonLikeSingle = new(1, 1_000_000, false);
    private static readonly BigRational EpsilonLikeDouble = new(1, 1_000_000_000_000_000, false);

    /// <summary>
    /// <para>Represents an approximate fraction of the irrational Golden Ratio.</para>
    /// </summary>
    public static BigRational GoldenRatio { get; } = new(7540113804746346429L, 4660046610375530309L, false);

    public static BigRational OneHalf { get; } = new(1, 2, false);

    /// <summary>
    /// <para>Can be negative, zero and positive.</para>
    /// </summary>
    private readonly System.Numerics.BigInteger m_numerator;

    /// <summary>
    /// <para>Can only be positive. It cannot be zero (division-by-zero) or negative (a negative rational is always reflected by the <see cref="m_numerator"/>), i.e. "<see cref="m_denominator"/> > 0".</para>
    /// </summary>
    private readonly System.Numerics.BigInteger m_denominator;

    /// <summary>Creates a new <see cref="BigRational"/> from a simple fraction specified by <paramref name="numerator"/> and <paramref name="denominator"/>. Optionally reduce, if possible.</summary>
    /// <param name="numerator"></param>
    /// <param name="denominator"></param>
    /// <param name="reduceIfPossible">If true, reduce if possible, and if false, do not attempt to reduce.</param>
    private BigRational(System.Numerics.BigInteger numerator, System.Numerics.BigInteger denominator, bool reduceIfPossible) // Private shortcut for speed when known operations produce already reduced fractions.
    {
      if (denominator.IsZero) throw new System.DivideByZeroException();

      if (System.Numerics.BigInteger.IsNegative(denominator)) // Normalize negative, i.e. make it so that the only part that can ever be negative is the numerator.
      {
        numerator = -numerator;
        denominator = -denominator;
      }

      TryAdjustSignum(numerator, denominator, out numerator, out denominator);

      if (reduceIfPossible)
        TryReduce(numerator, denominator, out numerator, out denominator);

      m_numerator = numerator;
      m_denominator = denominator;
    }

    /// <summary>Creates a new <see cref="BigRational"/> from a simple fraction specified by <paramref name="numerator"/> and <paramref name="denominator"/>. If possible, the fraction is reduced.</summary>
    /// <param name="numerator"></param>
    /// <param name="denominator"></param>
    public BigRational(System.Numerics.BigInteger numerator, System.Numerics.BigInteger denominator) : this(numerator, denominator, true) { }

    /// <summary>
    /// <para>Creates a new <see cref="BigRational"/> from an <paramref name="integer"/>, i.e. the <paramref name="integer"/> represents a numerator and the denominator is 1. No need to reduce the fraction.</para>
    /// <para>The resulting <see cref="BigRational"/> represents a natural number, or integer.</para>
    /// </summary>
    /// <param name="integer"></param>
    public BigRational(System.Numerics.BigInteger integer) : this(integer, System.Numerics.BigInteger.One, false) { } // Create easy integers.

    /// <summary>
    /// <para>Creates a new <see cref="BigRational"/> from the parts <paramref name="whole"/> plus <paramref name="numerator"/> / <paramref name="denominator"/>. If possible, the fraction is reduced.</para>
    /// </summary>
    /// <param name="whole"></param>
    /// <param name="numerator"></param>
    /// <param name="denominator"></param>
    public BigRational(System.Numerics.BigInteger whole, System.Numerics.BigInteger numerator, System.Numerics.BigInteger denominator) : this(whole * denominator + numerator, denominator, true) { }

    public void Deconstruct(out System.Numerics.BigInteger numerator, out System.Numerics.BigInteger denominator) { numerator = m_numerator; denominator = m_denominator; }

    /// <summary>The numerator of the fraction. This carries the sign and can also be zero.</summary>
    public System.Numerics.BigInteger Numerator => m_numerator;

    /// <summary>The denominator of the fraction. It is always positive and non-zero.</summary>
    public System.Numerics.BigInteger Denominator => m_denominator;

    public BigRational GetFractionPart()
      => new(System.Numerics.BigInteger.Remainder(m_numerator, m_denominator), m_denominator, true);

    public System.Numerics.BigInteger GetIntegerPart()
      => System.Numerics.BigInteger.Divide(m_numerator, m_denominator);

    public string ToImproperString()
      => $"{m_numerator}\u2044{m_denominator}";

    /// <summary>
    /// <para>Convert the fraction to a ratio.</para>
    /// </summary>
    public Units.Ratio ToRatio() => new(double.CreateChecked(m_numerator), double.CreateChecked(m_denominator));

    //public bool IsMixed => m_denominator > System.Numerics.BigInteger.One && System.Numerics.BigInteger.Abs(m_numerator) > m_denominator;

    //public bool IsWhole => m_denominator == System.Numerics.BigInteger.One || m_numerator == m_denominator;

    //public bool IsMixedFraction(out System.Numerics.BigInteger quotient, out System.Numerics.BigInteger remainder)
    //{
    //  if (m_denominator == 0)
    //  {
    //    quotient = default;
    //    remainder = default;

    //    return false;
    //  }

    //  (quotient, remainder) = System.Numerics.BigInteger.DivRem(m_numerator, m_denominator);

    //  return System.Numerics.BigInteger.Abs(remainder) > System.Numerics.BigInteger.Zero;
    //}

    ///// <summary>A fraction is a unit fraction if its numerator is equal to 1.</summary>
    //public bool IsUnitFraction => m_numerator == System.Numerics.BigInteger.One;

    //public bool TryGetMixed(out System.Numerics.BigInteger whole, out Fraction part)
    //{
    //  var (quotient, remainder) = System.Numerics.BigInteger.DivRem(m_numerator, m_denominator);

    //  whole = quotient;
    //  part = new Fraction(remainder, m_denominator);

    //  return IsMixed;
    //}

    /// <summary>
    /// <para>One 32-bit integer (numerator count) + the actual numerator byte count + another 32-bit integer (denominator count) + the actual denominator byte count.</para>
    /// </summary>
    /// <returns></returns>
    public int GetByteCount() => 4 + m_numerator.GetByteCount() + 4 + m_denominator.GetByteCount();

    #region Static methods

    /// <summary>
    /// <para>Returns the absolute value of the <paramref name="r"/>.</para>
    /// </summary>
    /// <param name="r"></param>
    /// <returns></returns>
    public static BigRational Abs(BigRational r)
      => new(System.Numerics.BigInteger.Abs(r.m_numerator), r.m_denominator, false);

    /// <summary>
    /// <para>Returns the sum of <paramref name="a"/> and <paramref name="b"/>.</para>
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static BigRational Add(BigRational a, BigRational b)
      => new(a.m_numerator * b.m_denominator + a.m_denominator * b.m_numerator, a.m_denominator * b.m_denominator, true);

    /// <summary>
    /// <para>Returns the sum of <paramref name="a"/> and <paramref name="b"/>.</para>
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static BigRational Add(BigRational a, System.Numerics.BigInteger b)
      => new(a.m_numerator + (a.m_denominator * b), a.m_denominator, true);

    /// <summary>
    /// <para>Calculates rational approximations to a given real number.</para>
    /// </summary>
    /// <param name="x"></param>
    /// <param name="maxIterations"></param>
    /// <returns></returns>
    /// <see href="https://stackoverflow.com/questions/12098461/how-can-i-detect-if-a-float-has-a-repeating-decimal-expansion-in-c/12101996#12101996"/>
    public static BigRational ApproximateRational(double x, int maxIterations = 101)
    {
      if (x == 0) return Zero;

      var Am1 = System.Numerics.BigInteger.Zero;
      var Am2 = System.Numerics.BigInteger.One;
      var Bm1 = System.Numerics.BigInteger.One;
      var Bm2 = System.Numerics.BigInteger.Zero;
      //var Am = (Item1: System.Numerics.BigInteger.Zero, Item2: System.Numerics.BigInteger.One);
      //var Bm = (Item1: System.Numerics.BigInteger.One, Item2: System.Numerics.BigInteger.Zero);

      System.Numerics.BigInteger A;
      System.Numerics.BigInteger B;

      var a = System.Numerics.BigInteger.Zero;
      var b = System.Numerics.BigInteger.Zero;

      if (x > 1)
      {
        var xW = double.Truncate(x);

        var ar = ApproximateRational(x - xW, maxIterations);

        return Add(ar, System.Numerics.BigInteger.CreateChecked(xW));
      }

      var counter = 0;

      for (; counter < maxIterations && x != 0; counter++)
      {
        var r = 1 / x;
        var rR = double.Round(r);

        var rT = System.Numerics.BigInteger.CreateChecked(rR);

        A = Am2 + rT * Am1;
        B = Bm2 + rT * Bm1;
        //A = Am.Item2 + rT * Am.Item1;
        //B = Bm.Item2 + rT * Bm.Item1;

        if (double.IsInfinity(double.CreateChecked(A)) || double.IsInfinity(double.CreateChecked(B)))
          break;

        a = A;
        b = B;

        Am1 = A;
        Am2 = Am1;
        Bm1 = B;
        Bm2 = Bm1;
        //Am = (A, Am.Item1);
        //Bm = (B, Bm.Item1);

        x = r - rR;
      }

      return new(a, b, true);
    }

    public static BigRational BitwiseAnd(BigRational a, BigRational b)
      => new(a.m_numerator & b.m_numerator, a.m_denominator & b.m_denominator, true);

    public static BigRational BitwiseNot(BigRational value)
      => new(~value.m_numerator, ~value.m_denominator, true);

    public static BigRational BitwiseOr(BigRational a, BigRational b)
      => new(a.m_numerator | b.m_numerator, a.m_denominator | b.m_denominator, true);

    public static BigRational BitwiseXor(BigRational a, BigRational b)
      => new(a.m_numerator ^ b.m_numerator, a.m_denominator ^ b.m_denominator, true);

    /// <summary>
    /// <para>Clamps the <paramref name="value"/> between <paramref name="min"/> and <paramref name="max"/>.</para>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    public static BigRational Clamp(BigRational value, BigRational min, BigRational max)
      => value < min ? min : value > max ? max : value;

    public static BigRational CopySign(BigRational a, BigRational b)
      => Multiply(a, Sign(b));

    public static BigRational CreateChecked<TOther>(TOther o)
      where TOther : System.Numerics.INumberBase<TOther>
    {
      if (o.GetType().IsAssignableToGenericType(typeof(System.Numerics.IBinaryInteger<>)))
        return new(System.Numerics.BigInteger.CreateChecked(o));
      else if (o.GetType().IsAssignableToGenericType(typeof(System.Numerics.IFloatingPoint<>)))
        return ApproximateRational(double.CreateChecked(o));
      else if (o is BigRational br)
        return br;

      throw new System.NotSupportedException();
    }

    public static BigRational CreateSaturating<TOther>(TOther o)
      where TOther : System.Numerics.INumberBase<TOther>
    {
      if (o.GetType().IsAssignableToGenericType(typeof(System.Numerics.IBinaryInteger<>)))
        return new(System.Numerics.BigInteger.CreateSaturating(o));
      else if (o.GetType().IsAssignableToGenericType(typeof(System.Numerics.IFloatingPoint<>)))
        return ApproximateRational(double.CreateSaturating(o));
      else if (o is BigRational br)
        return br;

      throw new System.NotSupportedException();
    }

    public static BigRational CreateTruncating<TOther>(TOther o)
      where TOther : System.Numerics.INumberBase<TOther>
    {
      if (o.GetType().IsAssignableToGenericType(typeof(System.Numerics.IBinaryInteger<>)))
        return new(System.Numerics.BigInteger.CreateTruncating(o));
      else if (o.GetType().IsAssignableToGenericType(typeof(System.Numerics.IFloatingPoint<>)))
        return ApproximateRational(double.CreateTruncating(o));
      else if (o is BigRational br)
        return br;

      throw new System.NotSupportedException();
    }

    public static void NormalizeSign(ref System.Numerics.BigInteger numerator, ref System.Numerics.BigInteger denominator)
    {
      if (System.Numerics.BigInteger.IsNegative(denominator)) // Normalize negative, i.e. make it so that the only part that can ever be negative is the numerator.
      {
        numerator = -numerator;
        denominator = -denominator;
      }
    }

    /// <summary>
    /// <para>Tries to adjust the sign of the <paramref name="numerator"/>/<paramref name="denominator"/> into <paramref name="adjustedNumerator"/>/<paramref name="adjustedDenominator"/> and returns whether successful.</para>
    /// </summary>
    /// <param name="numerator"></param>
    /// <param name="denominator"></param>
    /// <param name="adjustedNumerator"></param>
    /// <param name="adjustedDenominator"></param>
    /// <returns></returns>
    public static bool TryAdjustSignum(System.Numerics.BigInteger numerator, System.Numerics.BigInteger denominator, out System.Numerics.BigInteger adjustedNumerator, out System.Numerics.BigInteger adjustedDenominator)
    {
      if (System.Numerics.BigInteger.IsNegative(denominator)) // Ensure denominator remain positivity.
      {
        adjustedNumerator = -numerator;
        adjustedDenominator = -denominator;

        return true;
      }
      else
      {
        adjustedNumerator = numerator;
        adjustedDenominator = denominator;

        return false;
      }
    }

    /// <summary>
    /// <para>Returns the quotient of <paramref name="a"/> and <paramref name="b"/>.</para>
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static BigRational Divide(BigRational a, BigRational b)
      => new(a.m_numerator * b.m_denominator, a.m_denominator * b.m_numerator, true);

    /// <summary>
    /// <para>Returns the quotient of <paramref name="a"/> and <paramref name="b"/>.</para>
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static BigRational Divide(BigRational a, System.Numerics.BigInteger b)
      => new(a.m_numerator, a.m_denominator * b, true);

    public static BigRational Ceiling(BigRational v)
      => new(v.m_numerator + ((v.m_denominator - v.m_numerator % v.m_denominator) % v.m_denominator), v.m_denominator);

    public static BigRational Floor(BigRational v)
      => new(v.m_numerator - v.m_numerator % v.m_denominator, v.m_denominator);

    public static System.Collections.Generic.List<System.Numerics.BigInteger> GenerateContinuedFractionCoefficients(System.Numerics.BigInteger p, System.Numerics.BigInteger q)
    {
      var a = new System.Collections.Generic.List<System.Numerics.BigInteger>();

      while (q != 0)
      {
        a.Add(p / q);

        (p, q) = (q, p % q);
      }

      return a;
    }

    public static char GetSymbolFractionSlash(bool preferUnicode = true)
      => preferUnicode ? '\u2044' : '\u002F';

    /// <summary>Returns the greatest common divisor (GCD) of two values.</summary>
    /// <remarks>The result is guaranteed to be a reduced fraction. If you try to further simplify this to: (gcd(a,c) * gcd(b,d)) / (|b*d|), then the result will not be reduced, and the operation actually takes about 60% longer.</remarks>
    /// <example>gcd((a/b),(c/d)) = gcd(a,c) / lcm(b,d) = gcd(a,c) / (|b*d|/gcd(b,d))</example>
    public static BigRational GreatestCommonDivisor(BigRational a, BigRational b)
      => IsZero(a)
      ? Abs(b)
      : IsZero(b)
      ? Abs(a)
      : new(
          System.Numerics.BigInteger.GreatestCommonDivisor(a.m_numerator, b.m_numerator),
          System.Numerics.BigInteger.Abs(a.m_denominator * b.m_denominator) / System.Numerics.BigInteger.GreatestCommonDivisor(a.m_denominator, b.m_denominator),
          true
        );

    public static bool IsDyadicFraction(BigRational value)
      => System.Numerics.BigInteger.IsPow2(value.m_denominator);

    public static bool IsEvenInteger(BigRational value)
      => value.m_numerator.IsEven && IsInteger(value); // When the numerator is even and the fraction is an integer.

    /// <summary>Determines whether <paramref name="value"/> represents an integer, i.e. whole numbered fraction.</summary>
    public static bool IsInteger(BigRational value)
      => value.m_denominator == System.Numerics.BigInteger.One;

    /// <summary>Determines whether <paramref name="value"/> represents a mixed numbered fraction.</summary>
    public static bool IsMixedFraction(BigRational value)
      => System.Numerics.BigInteger.Abs(value.m_numerator) > value.m_denominator;

    public static bool IsNegative(BigRational value)
      => System.Numerics.BigInteger.IsNegative(value.m_numerator); // Only need to check the numerator, see constructor.

    public static bool IsOddInteger(BigRational value)
      => !value.m_numerator.IsEven && IsInteger(value); // When the numerator is odd and the fraction is an integer.

    public static bool IsPositive(BigRational value)
      => System.Numerics.BigInteger.IsPositive(value.m_numerator); // If the rational is negative it would only be via the numerator, see constructor.

    /// <summary>Determines whether the <paramref name="numerator"/> / <paramref name="denominator"/> represents a proper fraction.</summary>
    /// <remarks>A fraction is proper if its absolute value is strictly less than 1, i.e. if it is greater than -1 and less than 1.</remarks>
    public static bool IsProperFraction(BigRational value)
      => !value.m_numerator.IsZero && System.Numerics.BigInteger.Abs(value.m_numerator) < value.m_denominator; // If the numerator is not zero and strictly less than the denominator, it is a proper fraction.

    /// <summary>
    /// <para>Determines whether the <paramref name="numerator"/>/<paramref name="denominator"/> represents a reducible fraction, and if so, outputs the <paramref name="greatestCommonDivisor"/> (a.k.a. Greatest Common Denominator or GCD) of the fraction parts.</para>
    /// </summary>
    /// <param name="numerator"></param>
    /// <param name="denominator"></param>
    /// <param name="greatestCommonDivisor"></param>
    /// <returns></returns>
    /// <exception cref="System.DivideByZeroException"></exception>
    public static bool IsReducible(System.Numerics.BigInteger numerator, System.Numerics.BigInteger denominator, out System.Numerics.BigInteger greatestCommonDivisor)
    {
      if (denominator.IsZero)
      {
        greatestCommonDivisor = denominator;

        return false;
      }

      NormalizeSign(ref numerator, ref denominator);

      return (greatestCommonDivisor = System.Numerics.BigInteger.GreatestCommonDivisor(numerator, denominator)) > System.Numerics.BigInteger.One;
    }

    /// <summary>Determines whether <paramref name="value"/> represents a unit fraction.</summary>
    /// <remarks>A fraction is a unit fraction if its <paramref name="numerator"/> is equal to 1.</remarks>
    public static bool IsUnitFraction(BigRational value)
      => value.m_numerator == System.Numerics.BigInteger.One;

    public static bool IsZero(BigRational value)
      => value.m_numerator.IsZero; // When the numerator is zero, the fraction is zero.

    /// <summary>Returns the least common multiple (LCM) of <paramref name="a"/> and <paramref name="b"/>.</summary>
    /// <remarks>The result is guaranteed to be a reduced fraction. If you try to further simplify this to: |a*c| / (gcd(a,c) * gcd(b,d)), then the result will not be reduced, and the operation actually takes about 60% longer.</remarks>
    /// <example>lcm((a/b),(c/d)) = lcm(a,c) / gcd(b,d) = (|a*c| / gcd(a,c)) / gcd(b,d)</example>
    public static BigRational LeastCommonMultiple(BigRational a, BigRational b)
      => (IsZero(a) || IsZero(b))
      ? Zero
      : new(
          System.Numerics.BigInteger.Abs(a.m_numerator * b.m_numerator) / System.Numerics.BigInteger.GreatestCommonDivisor(a.m_numerator, b.m_numerator),
          System.Numerics.BigInteger.GreatestCommonDivisor(a.m_denominator, b.m_denominator),
          true
        );

    public static BigRational Log2(BigRational value)
    {
      var numerator = System.Numerics.BigInteger.Log2(value.m_numerator);
      var denominator = System.Numerics.BigInteger.Max(1, System.Numerics.BigInteger.Log2(value.m_denominator));
      return new BigRational(numerator, denominator);
    }

    public static BigRational Max(BigRational a, BigRational b)
      => a >= b ? a : b;

    /// <summary>Returns the mediant of <paramref name="a"/> and <paramref name="b"/>.</summary>
    public static BigRational Mediant(BigRational a, BigRational b)
      => new(a.m_numerator + b.m_numerator, a.m_denominator + b.m_denominator, false);

    public static BigRational Min(BigRational a, BigRational b)
      => a <= b ? a : b;

    /// <summary>
    /// <para>Returns the product of <paramref name="a"/> and <paramref name="b"/>.</para>
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static BigRational Multiply(BigRational a, BigRational b)
      => new(a.m_numerator * b.m_numerator, a.m_denominator * b.m_denominator, true);

    /// <summary>
    /// <para>Returns the product of <paramref name="a"/> and <paramref name="b"/>.</para>
    /// </summary>
    /// <param name="a"></param>
    /// <param name="s"></param>
    /// <returns></returns>
    public static BigRational Multiply(BigRational a, System.Numerics.BigInteger s)
      => new(a.m_numerator * s, a.m_denominator, true);

    public static BigRational Negate(BigRational number)
      => new(-number.m_numerator, number.m_denominator, false);

    /// <summary>Returns the nth root of <paramref name="value"/>.</summary>
    private static BigRational NthRoot(BigRational value, int nth, BigRational maxError)
    {
      if (nth < 0)
        return NthRoot(Reciprocal(value), -nth, maxError);

      if (nth == 0) throw new System.DivideByZeroException("Zeroth root is not defined.");
      if (nth == int.MinValue) throw new System.OverflowException("Value cannot be negated.");

      if (IsNegative(value))
      {
        if ((nth & 1) == 0) throw new System.ArithmeticException("Cannot compute even root of a negative number.");

        return Negate(NthRoot(Negate(value), nth, maxError));
      }

      if (IsNegative(maxError) || IsZero(maxError)) throw new System.ArgumentOutOfRangeException(nameof(maxError), "Epsilon must be positive");

      if (value == Zero) return Zero;
      if (nth == 1) return value;
      if (value == One) return value;

      // First, get the closest integer to the root of the numerator and the denominator as an initial guess.
      var guessNumerator = IntegerNthRoot(value.Numerator, nth);
      var guessDenominator = IntegerNthRoot(value.Denominator, nth);

      var x = new BigRational(guessNumerator.value, guessDenominator.value, true); // Initial guess.

      // If we got exact roots for numerator and denominator, then we know the guess is exact.
      if (guessNumerator.isExact && guessDenominator.isExact)
        return x;

      // Otherwise we use the implementation of nth-root algorithm: https://en.wikipedia.org/wiki/Nth_root_algorithm

      while (true)
      {
        var diff = Divide(Subtract(Divide(value, Pow(x, nth - 1)), x), System.Numerics.BigInteger.CreateChecked(nth));

        x = Add(x, diff);

        if (Abs(diff).CompareTo(maxError) < 0)
          break;
      }

      return x;

      static (System.Numerics.BigInteger value, bool isExact) IntegerNthRoot(System.Numerics.BigInteger a, System.Numerics.BigInteger n)
      {
        if (a.IsZero)
          return (System.Numerics.BigInteger.Zero, true);

        if (n == 1 || a == System.Numerics.BigInteger.One)
          return (a, true);

        // Solve for x: x^n = a
        // Start by computing a lower/upper bound on x.

        var lowerX = System.Numerics.BigInteger.One;
        var lowerPow = System.Numerics.BigInteger.One;

        var upperX = System.Numerics.BigInteger.CreateChecked(2);
        var upperPow = upperX.IntegerPow(n);

        while (upperPow.CompareTo(a) < 0)
        {
          lowerX = upperX;
          lowerPow = upperPow;
          upperX = (lowerX + lowerX);
          upperPow = upperX.IntegerPow(n);
        }

        if (upperPow.Equals(a))
          return (upperX, true); // If it's the exact answer, return it.

        // Now we know lowerX < x < upperX.
        // Next do binary search between lowerX and upperX.

        while (true)
        {
          var testX = (lowerX + upperX) >>> 1;

          if (testX.Equals(lowerX) || testX.Equals(upperX))
            break;

          var testPow = testX.IntegerPow(n);

          if (testPow.Equals(a))
            return (testX, true); // We found an exact answer.
          else if (testPow.CompareTo(a) > 0) // Still too high so set upper to the test value.
          {
            upperX = testX;
            upperPow = testPow;
          }
          else // Still too low.
          {
            lowerX = testX;
            lowerPow = testPow;
          }
        }

        //we didn't get an exact answer, but we know the two integers closest to the exact value.
        //now we just need to figure out which is closer and return that

        return (a - lowerPow).CompareTo(upperPow - a) < 0 ? (lowerX, false) : (upperX, false);
      }
    }

    private static BigRational NthRoot(BigRational value, System.Numerics.BigInteger nth, BigRational maxError)
      => nth >= int.MinValue && nth <= int.MaxValue
      ? NthRoot(value, int.CreateChecked(nth), maxError)
      : throw new System.ArgumentOutOfRangeException(nameof(nth));

    /// <summary>Returns this^exponent. Note: 0^0 will return 1/1.</summary>
    public static BigRational Pow(BigRational value, int exponent)
    {
      if (exponent < 0)
      {
        if (IsZero(value)) throw new System.DivideByZeroException("Raise to the power of a negative exponent.");
        else if (exponent == int.MinValue) throw new System.OverflowException("The negative exponent cannot be negated."); // Edge case: because we negate the exponent if it's negative, we would get into an infinite loop because -MIN_VALUE == MIN_VALUE.
        else return new(System.Numerics.BigInteger.Pow(value.Denominator, -exponent), System.Numerics.BigInteger.Pow(value.Numerator, -exponent), true);
      }

      if (exponent == 0) return One;
      else if (IsZero(value)) return Zero;
      else if (exponent == 1) return value;
      else return new(System.Numerics.BigInteger.Pow(value.Numerator, exponent), System.Numerics.BigInteger.Pow(value.Denominator, exponent), true);
    }

    private static BigRational Pow(BigRational value, System.Numerics.BigInteger exponent)
      => exponent >= int.MinValue && exponent <= int.MaxValue
      ? Pow(value, int.CreateChecked(exponent))
      : throw new System.ArgumentOutOfRangeException(nameof(exponent));

    /// <summary>Returns the reciprocal of <paramref name="value"/>.</summary>
    public static BigRational Reciprocal(BigRational value) => new(value.m_denominator, value.m_numerator, true);

    public static void Simplify(ref System.Numerics.BigInteger numerator, ref System.Numerics.BigInteger denominator)
    {
      if (IsReducible(numerator, denominator, out var greatestCommonDenominator))
      {
        numerator /= greatestCommonDenominator;
        denominator /= greatestCommonDenominator;
      }
    }

    /// <summary>
    /// <para>Returns the modulus (remainder) of <paramref name="a"/> divided by <paramref name="b"/>.</para>
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static BigRational Remainder(BigRational a, BigRational b)
      => new((a.m_numerator * b.m_denominator) % (a.m_denominator * b.m_numerator), a.m_denominator * b.m_denominator, true);

    /// <summary>
    /// <para>Returns the modulus (remainder) of <paramref name="a"/> divided by <paramref name="b"/>.</para>
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static BigRational Remainder(BigRational a, System.Numerics.BigInteger b)
      => new(a.m_numerator % (a.m_denominator * b), a.m_denominator, true);

    /// <summary>
    /// <para>Returns the sign of a <see cref="BigRational"/> value.</para>
    /// </summary>
    /// <param name="value"></param>
    /// <returns>-1 if negative, 0 if zero, and 1 if positive.</returns>
    public static int Sign(BigRational value)
      => System.Numerics.BigInteger.IsNegative(value.m_numerator) ? -1 : value.m_numerator.IsZero ? 0 : 1;

    /// <summary>
    /// <para>Returns the difference between <paramref name="a"/> and <paramref name="b"/>.</para>
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static BigRational Subtract(BigRational a, BigRational b)
      => new(a.m_numerator * b.m_denominator - a.m_denominator * b.m_numerator, a.m_denominator * b.m_denominator, true);

    /// <summary>
    /// <para>Returns the difference between <paramref name="a"/> and <paramref name="b"/>.</para>
    /// </summary>
    /// <param name="a"></param>
    /// <param name="b"></param>
    /// <returns></returns>
    public static BigRational Subtract(BigRational a, System.Numerics.BigInteger b)
      => new(a.m_numerator - (a.m_denominator * b), a.m_denominator, true);

    public static BigRational Truncate(BigRational v)
      => IsNegative(v) ? Ceiling(v) : Floor(v);

    public static bool TryGetMixedParts(BigRational value, out System.Numerics.BigInteger wholeNumber, out System.Numerics.BigInteger properNumerator, out System.Numerics.BigInteger properDenominator)
    {
      if (IsMixedFraction(value))
      {
        (wholeNumber, properNumerator) = System.Numerics.BigInteger.DivRem(value.m_numerator, value.m_denominator);

        if (System.Numerics.BigInteger.IsNegative(properNumerator))
          properNumerator = System.Numerics.BigInteger.Abs(properNumerator);

        properDenominator = value.m_denominator;

        return true;
      }
      else
      {
        wholeNumber = properNumerator = properDenominator = System.Numerics.BigInteger.Zero;

        return false;
      }
    }

    /// <summary>
    /// <para>Euclid's algorithm is used to simplify a fraction.</para>
    /// </summary>
    /// <param name="numerator"></param>
    /// <param name="denominator"></param>
    /// <param name="reducedNumerator"></param>
    /// <param name="reducedDenominator"></param>
    /// <returns></returns>
    public static bool TryReduce(System.Numerics.BigInteger numerator, System.Numerics.BigInteger denominator, out System.Numerics.BigInteger reducedNumerator, out System.Numerics.BigInteger reducedDenominator)
    {
      if (IsReducible(numerator, denominator, out var greatestCommonDenominator))
      {
        reducedNumerator = numerator / greatestCommonDenominator;
        reducedDenominator = denominator /= greatestCommonDenominator;

        return true;
      }
      else
      {
        reducedNumerator = numerator;
        reducedDenominator = denominator;

        return false;
      }
    }

    #region Methods to read and write binary.

    //// Read Binary representation of BigRational.
    public static BigRational ReadBytes(ReadOnlySpan<byte> source, bool isUnsigned = false, bool isBigEndian = true)
    {
      var bytesInNumerator = System.Buffers.Binary.BinaryPrimitives.ReadInt32BigEndian(source[..4]);

      var numerator = new System.Numerics.BigInteger(source.Slice(4, bytesInNumerator), isUnsigned, isBigEndian);

      var bytesInDenominator = System.Buffers.Binary.BinaryPrimitives.ReadInt32BigEndian(source.Slice(4 + bytesInNumerator, 4));

      var denominator = new System.Numerics.BigInteger(source.Slice(4 + bytesInNumerator + 4, bytesInDenominator), isUnsigned, isBigEndian);

      return new BigRational(numerator, denominator);
    }

    // Try Read Binary representation of BigRational.
    public static bool TryReadBytes(ReadOnlySpan<byte> source, out BigRational value, bool isUnsigned = false, bool isBigEndian = true)
    {
      try
      {
        value = ReadBytes(source, isUnsigned, isBigEndian);
        return true;
      }
      catch { }

      value = default;
      return false;
    }

    // Write Binary representation of BigRational.
    public int WriteBytes(Span<byte> target, bool isUnsigned = false, bool isBigEndian = true)
    {
      var bytesInNumerator = m_numerator.GetByteCount();

      if (isBigEndian) System.Buffers.Binary.BinaryPrimitives.WriteInt32BigEndian(target[..4], bytesInNumerator);
      else System.Buffers.Binary.BinaryPrimitives.WriteInt32LittleEndian(target[..4], bytesInNumerator);

      if (!m_numerator.TryWriteBytes(target.Slice(4, bytesInNumerator), out var _, isUnsigned, isBigEndian))
        throw new System.InvalidOperationException();

      var bytesInDenominator = m_denominator.GetByteCount();

      if (isBigEndian) System.Buffers.Binary.BinaryPrimitives.WriteInt32BigEndian(target.Slice(4 + bytesInNumerator, 4), bytesInDenominator);
      else System.Buffers.Binary.BinaryPrimitives.WriteInt32LittleEndian(target.Slice(4 + bytesInNumerator, 4), bytesInDenominator);

      if (!m_denominator.TryWriteBytes(target[(4 + bytesInNumerator + 4)..], out var _, isUnsigned, isBigEndian))
        throw new System.InvalidOperationException();

      return 4 + bytesInNumerator + 4 + bytesInDenominator;
    }

    // Try Write Binary representation of BigRational.
    public bool TryWriteBytes(Span<byte> target, out int bytesWritten, bool isUnsigned = false, bool isBigEndian = true)
    {
      try
      {
        bytesWritten = WriteBytes(target, isUnsigned, isBigEndian);
        return true;
      }
      catch { }

      bytesWritten = 0;
      return false;
    }

    #endregion // Methods to read and write binary.

    #endregion // Static methods

    #region Overloaded operators

    #endregion // Overloaded operators

    #region Implemented interfaces

    #region System.Numerics.IAdditionOperators<>

    // a/b + c/d == (ad + bc)/bd

    public static BigRational operator +(BigRational a, BigRational b) => Add(a, b);
    public static BigRational operator +(BigRational a, System.Numerics.BigInteger b) => Add(a, b);

    #endregion

    #region System.Numerics.IAdditiveIdentity<>

    public static BigRational AdditiveIdentity => Zero;

    #endregion

    #region System.Numerics.IBitwiseOperators<>

    public static BigRational operator &(BigRational left, BigRational right) => BitwiseAnd(left, right);
    public static BigRational operator |(BigRational left, BigRational right) => BitwiseOr(left, right);
    public static BigRational operator ^(BigRational left, BigRational right) => BitwiseXor(left, right);
    public static BigRational operator ~(BigRational value) => BitwiseNot(value);

    #endregion

    #region System.Numerics.IComparisonOperators<>

    public static bool operator <(BigRational a, BigRational b) => a.CompareTo(b) < 0;
    public static bool operator <=(BigRational a, BigRational b) => a.CompareTo(b) <= 0;
    public static bool operator >(BigRational a, BigRational b) => a.CompareTo(b) > 0;
    public static bool operator >=(BigRational a, BigRational b) => a.CompareTo(b) >= 0;

    #endregion

    #region System.Numerics.IDecrementOperators<>

    public static BigRational operator --(BigRational value) => Subtract(value, System.Numerics.BigInteger.One);

    #endregion

    #region System.Numerics.IDivisionOperators<>

    // a/b / c/d == (ad)/(bc)

    public static BigRational operator /(BigRational a, BigRational b) => Divide(a, b);
    public static BigRational operator /(BigRational a, System.Numerics.BigInteger b) => Divide(a, b);

    #endregion

    #region System.Numerics.IFloatingPointConstants<>

    public static BigRational E { get; } = new(System.Numerics.BigInteger.Parse("611070150698522592097"), System.Numerics.BigInteger.Parse("224800145555521536000"), false);
    public static BigRational Pi { get; } = new(2646693125139304345L, 842468587426513207L, false);
    public static BigRational Tau { get; } = Add(Pi, Pi);

    #endregion

    #region System.Numerics.IIncrementOperators<>

    public static BigRational operator ++(BigRational value) => Add(value, System.Numerics.BigInteger.One);

    #endregion

    #region System.Numerics.IModulusOperators<>

    // a/b % c/d == (ad % bc)/bd

    public static BigRational operator %(BigRational a, BigRational b) => Remainder(a, b);
    public static BigRational operator %(BigRational a, System.Numerics.BigInteger b) => Remainder(a, b);

    #endregion

    #region System.Numerics.IMultiplicativeIdentity<>

    public static BigRational MultiplicativeIdentity => One;

    #endregion

    #region System.Numerics.IMultiplyOperators<> 

    // a/b * c/d == (ac)/(bd)

    public static BigRational operator *(BigRational a, BigRational b) => Multiply(a, b);
    public static BigRational operator *(BigRational a, System.Numerics.BigInteger b) => Multiply(a, b);

    #endregion

    #region System.Numerics.INumber<>

    public static BigRational MaxNumber(BigRational a, BigRational b) => Max(a, b);
    public static BigRational MinNumber(BigRational a, BigRational b) => Min(a, b);

    #endregion

    #region System.Numerics.INumberBase<>

    public static BigRational One { get; } = new(System.Numerics.BigInteger.One);
    public static int Radix { get; } = 2;
    public static BigRational Zero { get; } = new(System.Numerics.BigInteger.Zero);
    public static bool IsCanonical(BigRational value) => !IsReducible(value.m_numerator, value.m_denominator, out var _);
    public static bool IsComplexNumber(BigRational value) => false;
    public static bool IsFinite(BigRational value) => true;
    public static bool IsImaginaryNumber(BigRational value) => false;
    public static bool IsInfinity(BigRational value) => false;
    public static bool IsNaN(BigRational value) => false;
    public static bool IsNegativeInfinity(BigRational value) => false;
    public static bool IsNormal(BigRational value) => !IsZero(value);
    public static bool IsPositiveInfinity(BigRational value) => false;
    public static bool IsRealNumber(BigRational value) => true;
    public static bool IsSubnormal(BigRational value) => false;
    public static BigRational MaxMagnitude(BigRational a, BigRational b) => Max(a, b);
    public static BigRational MaxMagnitudeNumber(BigRational a, BigRational b) => Max(a, b);
    public static BigRational MinMagnitude(BigRational a, BigRational b) => Min(a, b);
    public static BigRational MinMagnitudeNumber(BigRational a, BigRational b) => Min(a, b);
    public static BigRational Parse(ReadOnlySpan<char> s, System.Globalization.NumberStyles style, System.IFormatProvider? provider) => throw new NotImplementedException();
    public static BigRational Parse(string s, System.Globalization.NumberStyles style, System.IFormatProvider? provider) => throw new NotImplementedException();
    public static BigRational Parse(System.ReadOnlySpan<char> s, System.IFormatProvider? provider) => throw new NotImplementedException();
    public static BigRational Parse(string s, System.IFormatProvider? provider) => throw new NotImplementedException();
    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, System.IFormatProvider? provider)
    {
      var s = ToString(format.ToString(), provider);
      s.CopyTo(destination);
      charsWritten = s.Length;
      return true;
    }
    public static bool TryParse(System.ReadOnlySpan<char> s, System.Globalization.NumberStyles style, System.IFormatProvider? provider, out BigRational result) => throw new NotImplementedException();
    public static bool TryParse(string? s, System.Globalization.NumberStyles style, System.IFormatProvider? provider, out BigRational result) => throw new NotImplementedException();
    public static bool TryParse(ReadOnlySpan<char> s, System.IFormatProvider? provider, out BigRational result) => throw new NotImplementedException();
    public static bool TryParse(string? s, System.IFormatProvider? provider, out BigRational result) => throw new NotImplementedException();
    static bool System.Numerics.INumberBase<BigRational>.TryConvertFromChecked<TOther>(TOther value, out BigRational result)
    {
      try
      {
        result = CreateChecked(value);
        return true;
      }
      catch
      {
        result = default;
        return false;
      }
    }
    static bool System.Numerics.INumberBase<BigRational>.TryConvertFromSaturating<TOther>(TOther value, out BigRational result)
    {
      try
      {
        result = CreateSaturating(value);
        return true;
      }
      catch
      {
        result = default;
        return false;
      }
    }
    static bool System.Numerics.INumberBase<BigRational>.TryConvertFromTruncating<TOther>(TOther value, out BigRational result)
    {
      try
      {
        result = CreateTruncating(value);
        return true;
      }
      catch
      {
        result = default;
        return false;
      }
    }
    static bool System.Numerics.INumberBase<BigRational>.TryConvertToChecked<TOther>(BigRational value, out TOther result)
    {
      result = default!;
      return false;
    }
    static bool System.Numerics.INumberBase<BigRational>.TryConvertToSaturating<TOther>(BigRational value, out TOther result)
    {
      result = default!;
      return false;
    }
    static bool System.Numerics.INumberBase<BigRational>.TryConvertToTruncating<TOther>(BigRational value, out TOther result)
    {
      result = default!;
      return false;
    }

    #endregion // System.Numerics.INumberBase<>

    #region System.Numerics.IPowerFunctions<>

    public static BigRational Pow(BigRational x, BigRational y) => Pow(NthRoot(x, y.m_denominator, EpsilonLikeDouble), y.m_numerator); // is var abspow && IsNegative(exponent) ? One / abspow : abspow;

    #endregion

    #region System.Numerics.IRootFunctions<>

    public static BigRational Cbrt(BigRational x) => NthRoot(x, 3, EpsilonLikeDouble);
    public static BigRational Hypot(BigRational x, BigRational y) => Sqrt(x * x + y * y);
    public static BigRational RootN(BigRational x, int n) => NthRoot(x, n, EpsilonLikeDouble);
    public static BigRational Sqrt(BigRational x) => NthRoot(x, 2, EpsilonLikeDouble);

    #endregion

    #region System.Numerics.ISignedNumber<>

    public static BigRational NegativeOne { get; } = Negate(One);

    #endregion

    #region System.Numerics.ISubtractionOperators<>

    // a/b - c/d == (ad - bc)/bd

    public static BigRational operator -(BigRational a, BigRational b) => Subtract(a, b);
    public static BigRational operator -(BigRational a, System.Numerics.BigInteger b) => Subtract(a, b);

    #endregion

    #region System.Numerics.IUnaryNegationOperators<>

    public static BigRational operator -(BigRational value) => Negate(value);

    #endregion

    #region System.Numerics.IUnaryPlusOperators<>

    public static BigRational operator +(BigRational value) => value;

    #endregion

    // IComparable<>
    public int CompareTo(BigRational other)
      => Sign(this) is var thisSign && Sign(other) is var otherSign
      && thisSign < otherSign
      ? -1
      : thisSign > otherSign
      ? 1
      : m_denominator.Equals(other.m_denominator)
      ? m_numerator.CompareTo(other.m_numerator)
      : (m_numerator * other.m_denominator).CompareTo(m_denominator * other.m_numerator);

    // IComparable
    public int CompareTo(object? other) => other is BigRational o ? CompareTo(o) : -1;

    #region IConvertible

    //public System.TypeCode GetTypeCode() => System.TypeCode.Object;
    //public bool ToBoolean(System.IFormatProvider? provider) => Value != 0;
    //public byte ToByte(System.IFormatProvider? provider) => System.Convert.ToByte(Value);
    //public char ToChar(System.IFormatProvider? provider) => System.Convert.ToChar(Value);
    //public System.DateTime ToDateTime(System.IFormatProvider? provider) => System.Convert.ToDateTime(Value);
    //public decimal ToDecimal(System.IFormatProvider? provider) => System.Convert.ToDecimal(Value);
    //public double ToDouble(System.IFormatProvider? provider) => System.Convert.ToDouble(Value);
    //public short ToInt16(System.IFormatProvider? provider) => System.Convert.ToInt16(Value);
    //public int ToInt32(System.IFormatProvider? provider) => System.Convert.ToInt32(Value);
    //public long ToInt64(System.IFormatProvider? provider) => System.Convert.ToInt64(Value);
    //[System.CLSCompliant(false)] public sbyte ToSByte(System.IFormatProvider? provider) => System.Convert.ToSByte(Value);
    //public float ToSingle(System.IFormatProvider? provider) => System.Convert.ToSingle(Value);
    //public string ToString(System.IFormatProvider? provider) => ToString(null, provider);
    //public object ToType(System.Type conversionType, System.IFormatProvider? provider) => System.Convert.ChangeType(Value, conversionType, provider);
    //[System.CLSCompliant(false)] public ushort ToUInt16(System.IFormatProvider? provider) => System.Convert.ToUInt16(Value);
    //[System.CLSCompliant(false)] public uint ToUInt32(System.IFormatProvider? provider) => System.Convert.ToUInt32(Value);
    //[System.CLSCompliant(false)] public ulong ToUInt64(System.IFormatProvider? provider) => System.Convert.ToUInt64(Value);

    #endregion IConvertible

    // IFormattable
    public string ToString(string? format, System.IFormatProvider? formatProvider)
      => IsProperFraction(this)
      ? Units.Ratio.ToRatioNotationString(Units.RatioNotation.AslashB, m_numerator.ToString(format, formatProvider), m_denominator.ToString(format, formatProvider))
      : TryGetMixedParts(this, out var wholeNumber, out var properNumerator, out var properDenominator)
      ? $"{wholeNumber} {Units.Ratio.ToRatioNotationString(Units.RatioNotation.AslashB, properNumerator.ToString(format, formatProvider), properDenominator.ToString(format, formatProvider))}"
      : m_numerator.ToString(); // It is a whole number and we return a simple integer string.

    #region IValueQuantifiable<>

    public double Value => double.CreateChecked(m_numerator) / double.CreateChecked(m_denominator);

    #endregion // IValueQuantifiable<>

    #endregion // Implemented interfaces

    public override string ToString() => ToString(null, null);
  }
}
