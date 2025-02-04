//namespace Flux
//{
//  namespace Quantities
//  {
//    public readonly record struct Fraction
//      : System.IComparable, System.IComparable<Fraction>
//      , System.IFormattable
//      , System.Numerics.IAdditiveIdentity<Fraction, Fraction>
//      , System.Numerics.IAdditionOperators<Fraction, Fraction, Fraction>
//      , System.Numerics.IComparisonOperators<Fraction, Fraction, bool>
//      , System.Numerics.IDecrementOperators<Fraction>
//      , System.Numerics.IDivisionOperators<Fraction, Fraction, Fraction>
//      , System.Numerics.IEqualityOperators<Fraction, Fraction, bool>
//      , System.Numerics.IFloatingPointConstants<Fraction>
//      , System.Numerics.IIncrementOperators<Fraction>
//      , System.Numerics.IModulusOperators<Fraction, Fraction, Fraction>
//      , System.Numerics.IMultiplicativeIdentity<Fraction, Fraction>
//      , System.Numerics.IMultiplyOperators<Fraction, Fraction, Fraction>
//      , System.Numerics.INumber<Fraction>
//      , System.Numerics.INumberBase<Fraction>
//      , System.Numerics.IPowerFunctions<Fraction>
//      , System.Numerics.IRootFunctions<Fraction>
//      , System.Numerics.ISignedNumber<Fraction>
//      , System.Numerics.ISubtractionOperators<Fraction, Fraction, Fraction>
//      , System.Numerics.IUnaryNegationOperators<Fraction, Fraction>
//      , System.Numerics.IUnaryPlusOperators<Fraction, Fraction>
//      , IValueQuantifiable<double>
//    {
//      private static readonly Fraction EpsilonLikeSingle = new(1, 1_000_000);
//      private static readonly Fraction EpsilonLikeDouble = new(1, 1_000_000_000_000_000);

//      public static Fraction E { get; } = new(System.Numerics.BigInteger.Parse("611070150698522592097"), System.Numerics.BigInteger.Parse("224800145555521536000"));
//      /// <summary>Represents an approximate fraction of the irrational Golden Ratio.</summary>
//      public static Fraction GoldenRatio { get; } = new(7540113804746346429L, 4660046610375530309L);
//      public static Fraction NegativeOne { get; } = new(System.Numerics.BigInteger.MinusOne);
//      public static Fraction One { get; } = new(System.Numerics.BigInteger.One);
//      public static Fraction Pi { get; } = new(System.Numerics.BigInteger.Parse("2646693125139304345"), System.Numerics.BigInteger.Parse("842468587426513207"));
//      public static Fraction Tau { get; } = Add(Pi, Pi);
//      public static Fraction Zero { get; } = new(System.Numerics.BigInteger.Zero);

//      /// <summary>
//      /// <para>Can be negative, zero and positive.</para>
//      /// </summary>
//      private readonly System.Numerics.BigInteger m_numerator;

//      /// <summary>
//      /// <para>Can only be positive. It cannot be zero (division-by-zero) or negative (a negative rational is always reflected by the <see cref="m_numerator"/>), i.e. "<see cref="m_denominator"/> > 0".</para>
//      /// </summary>
//      private readonly System.Numerics.BigInteger m_denominator;

//      private Fraction(System.Numerics.BigInteger numerator, System.Numerics.BigInteger denominator)
//      {
//        m_numerator = numerator;
//        m_denominator = denominator;
//      }

//      /// <summary>
//      /// <para>Creates a new <see cref="Fraction"/> from an <paramref name="integer"/>, i.e. the <paramref name="integer"/> represents a numerator and the denominator is 1. No need to reduce the fraction.</para>
//      /// <para>The resulting <see cref="Fraction"/> represents a natural number, or integer.</para>
//      /// </summary>
//      /// <param name="integer"></param>
//      public Fraction(System.Numerics.BigInteger integer)
//        : this(integer, System.Numerics.BigInteger.One)
//      { } // Create easy integers.

//      /// <summary>The numerator of the fraction. This carries the sign and can also be zero.</summary>
//      public System.Numerics.BigInteger Numerator => m_numerator;

//      /// <summary>The denominator of the fraction. It is always positive and non-zero.</summary>
//      public System.Numerics.BigInteger Denominator => m_denominator;

//      /// <summary>Determines whether the <paramref name="numerator"/> / <paramref name="denominator"/> represents a proper fraction.</summary>
//      /// <remarks>A fraction is proper if its absolute value is strictly less than 1, i.e. if it is greater than -1 and less than 1.</remarks>
//      public bool IsProper => System.Numerics.BigInteger.Abs(m_numerator) < m_denominator;

//      public System.Numerics.BigInteger GetWholePart() => System.Numerics.BigInteger.Divide(m_numerator, m_denominator);

//      public Fraction GetFractionPart() => new(System.Numerics.BigInteger.Remainder(m_numerator, m_denominator), m_denominator);

//      public string ToImproperString() => $"{m_numerator}\u2044{m_denominator}";

//      /// <summary>Convert the ratio to a fraction. If numerator and/or denominator are not integers, the fraction is approximated.</summary>
//      public Ratio ToRatio() => new(double.CreateChecked(m_numerator), double.CreateChecked(m_denominator));

//      //public bool IsMixed => m_denominator > System.Numerics.BigInteger.One && System.Numerics.BigInteger.Abs(m_numerator) > m_denominator;

//      //public bool IsWhole => m_denominator == System.Numerics.BigInteger.One || m_numerator == m_denominator;

//      //public bool IsMixedFraction(out System.Numerics.BigInteger quotient, out System.Numerics.BigInteger remainder)
//      //{
//      //  if (m_denominator == 0)
//      //  {
//      //    quotient = default;
//      //    remainder = default;

//      //    return false;
//      //  }

//      //  (quotient, remainder) = System.Numerics.BigInteger.DivRem(m_numerator, m_denominator);

//      //  return System.Numerics.BigInteger.Abs(remainder) > System.Numerics.BigInteger.Zero;
//      //}

//      ///// <summary>A fraction is a unit fraction if its numerator is equal to 1.</summary>
//      //public bool IsUnitFraction => m_numerator == System.Numerics.BigInteger.One;

//      //public bool TryGetMixed(out System.Numerics.BigInteger whole, out Fraction part)
//      //{
//      //  var (quotient, remainder) = System.Numerics.BigInteger.DivRem(m_numerator, m_denominator);

//      //  whole = quotient;
//      //  part = new Fraction(remainder, m_denominator);

//      //  return IsMixed;
//      //}

//      /// <summary>
//      /// <para>One 32-bit integer (numerator count) + the actual numerator byte count + another 32-bit integer (denominator count) + the actual denominator byte count.</para>
//      /// </summary>
//      /// <returns></returns>
//      public int GetByteCount() => 4 + m_numerator.GetByteCount() + 4 + m_denominator.GetByteCount();

//      #region Static methods

//      public static Fraction Abs(Fraction value)
//        => new(System.Numerics.BigInteger.Abs(value.m_numerator), value.m_denominator);

//      // a/b + c/d == (ad + bc)/bd
//      public static Fraction Add(Fraction a, Fraction b)
//        => new(a.m_numerator * b.m_denominator + a.m_denominator * b.m_numerator, a.m_denominator * b.m_denominator);

//      public static Fraction Add(Fraction a, System.Numerics.BigInteger i)
//        => new(a.m_numerator + (a.m_denominator * i), a.m_denominator);

//      public static Fraction Clamp(Fraction value, Fraction min, Fraction max)
//        => value < min ? min : value > max ? max : value;

//      public static Fraction CopySign(Fraction a, Fraction b)
//        => Multiply(a, Sign(b));

//      public static Fraction CreateChecked<TOther>(TOther o)
//        where TOther : System.Numerics.INumberBase<TOther>
//      {
//        if (o.GetType().IsAssignableToGenericType(typeof(System.Numerics.IBinaryInteger<>)))
//          return new(System.Numerics.BigInteger.CreateChecked(o));
//        else if (o.GetType().IsAssignableToGenericType(typeof(System.Numerics.IFloatingPoint<>)))
//          return ApproximateRational(double.CreateChecked(o));
//        else if (o is Fraction br)
//          return br;

//        throw new System.NotSupportedException();
//      }

//      public static Fraction CreateSaturating<TOther>(TOther o)
//        where TOther : System.Numerics.INumberBase<TOther>
//      {
//        if (o.GetType().IsAssignableToGenericType(typeof(System.Numerics.IBinaryInteger<>)))
//          return new(System.Numerics.BigInteger.CreateSaturating(o));
//        else if (o.GetType().IsAssignableToGenericType(typeof(System.Numerics.IFloatingPoint<>)))
//          return ApproximateRational(double.CreateSaturating(o));
//        else if (o is Fraction br)
//          return br;

//        throw new System.NotSupportedException();
//      }

//      public static Fraction CreateTruncating<TOther>(TOther o)
//        where TOther : System.Numerics.INumberBase<TOther>
//      {
//        if (o.GetType().IsAssignableToGenericType(typeof(System.Numerics.IBinaryInteger<>)))
//          return new(System.Numerics.BigInteger.CreateTruncating(o));
//        else if (o.GetType().IsAssignableToGenericType(typeof(System.Numerics.IFloatingPoint<>)))
//          return ApproximateRational(double.CreateTruncating(o));
//        else if (o is Fraction br)
//          return br;

//        throw new System.NotSupportedException();
//      }

//      // a/b / c/d == (ad)/(bc)
//      public static Fraction Divide(Fraction a, Fraction b)
//        => new(a.m_numerator * b.m_denominator, a.m_denominator * b.m_numerator);

//      public static Fraction Divide(Fraction a, System.Numerics.BigInteger s)
//        => new(a.m_numerator, a.m_denominator * s);

//      /// <summary>Returns the greatest common divisor (GCD) of two values.</summary>
//      /// <remarks>The result is guaranteed to be a reduced fraction. If you try to further simplify this to: (gcd(a,c) * gcd(b,d)) / (|b*d|), then the result will not be reduced, and the operation actually takes about 60% longer.</remarks>
//      /// <example>gcd((a/b),(c/d)) = gcd(a,c) / lcm(b,d) = gcd(a,c) / (|b*d|/gcd(b,d))</example>
//      public static Fraction GreatestCommonDivisor(Fraction a, Fraction b)
//        => IsZero(a)
//        ? Abs(b)
//        : IsZero(b)
//        ? Abs(a)
//        : new(
//            System.Numerics.BigInteger.GreatestCommonDivisor(a.m_numerator, b.m_numerator),
//            System.Numerics.BigInteger.Abs(a.m_denominator * b.m_denominator) / System.Numerics.BigInteger.GreatestCommonDivisor(a.m_denominator, b.m_denominator)
//          );

//      public static bool IsEvenInteger(Fraction value)
//        => value.m_numerator.IsEven && IsInteger(value); // When the numerator is even and the fraction is an integer.

//      public static bool IsInteger(Fraction value)
//        => value.m_denominator == System.Numerics.BigInteger.One;

//      /// <summary>Determines whether <paramref name="value"/> represents a mixed numbered fraction.</summary>
//      public static bool IsMixedNumber(Fraction value)
//        => value.m_denominator != System.Numerics.BigInteger.One // If the denominator is one, it is a whole number, not a mixed number.
//          && System.Numerics.BigInteger.Abs(value.m_numerator) > value.m_denominator;

//      public static bool IsNegative(Fraction value)
//        => System.Numerics.BigInteger.IsNegative(value.m_numerator); // The denominator cannot be negative, see constructor.

//      public static bool IsOddInteger(Fraction value)
//        => !value.m_numerator.IsEven && IsInteger(value); // When the numerator is odd and the fraction is an integer.

//      public static bool IsPositive(Fraction value)
//        => System.Numerics.BigInteger.IsPositive(value.m_numerator); // If the rational is negative it would only be via the numerator, see constructor.

//      /// <summary>
//      /// <para>Determines whether the <paramref name="numerator"/>/<paramref name="denominator"/> represents a reducible fraction, and if so, outputs the <paramref name="greatestCommonDivisor"/> (a.k.a. Greatest Common Denominator or GCD) of the fraction parts.</para>
//      /// </summary>
//      /// <param name="numerator"></param>
//      /// <param name="denominator"></param>
//      /// <param name="greatestCommonDivisor"></param>
//      /// <returns></returns>
//      /// <exception cref="System.DivideByZeroException"></exception>
//      public static bool IsReducible(System.Numerics.BigInteger numerator, System.Numerics.BigInteger denominator, out System.Numerics.BigInteger greatestCommonDivisor)
//      {
//        if (denominator.IsZero)
//        {
//          greatestCommonDivisor = denominator;

//          return false;
//        }

//        TryAdjustSignum(numerator, denominator, out numerator, out denominator);

//        return (greatestCommonDivisor = System.Numerics.BigInteger.GreatestCommonDivisor(numerator, denominator)) > System.Numerics.BigInteger.One;
//      }

//      /// <summary>Determines whether <paramref name="value"/> represents a unit fraction.</summary>
//      /// <remarks>A fraction is a unit fraction if its <paramref name="numerator"/> is equal to 1.</remarks>
//      public static bool IsUnitFraction(Fraction value)
//        => value.m_numerator == System.Numerics.BigInteger.One;

//      public static bool IsZero(Fraction value)
//        => value.m_numerator.IsZero; // When the numerator is zero (a zero denominator results in divide-by-zero exception).

//      /// <summary>Returns the least common multiple (LCM) of <paramref name="a"/> and <paramref name="b"/>.</summary>
//      /// <remarks>The result is guaranteed to be a reduced fraction. If you try to further simplify this to: |a*c| / (gcd(a,c) * gcd(b,d)), then the result will not be reduced, and the operation actually takes about 60% longer.</remarks>
//      /// <example>lcm((a/b),(c/d)) = lcm(a,c) / gcd(b,d) = (|a*c| / gcd(a,c)) / gcd(b,d)</example>
//      public static Fraction LeastCommonMultiple(Fraction a, Fraction b)
//        => (IsZero(a) || IsZero(b))
//        ? Zero
//        : new(
//            System.Numerics.BigInteger.Abs(a.m_numerator * b.m_numerator) / System.Numerics.BigInteger.GreatestCommonDivisor(a.m_numerator, b.m_numerator),
//            System.Numerics.BigInteger.GreatestCommonDivisor(a.m_denominator, b.m_denominator)
//          );

//      public static Fraction Max(Fraction a, Fraction b)
//        => a >= b ? a : b;

//      /// <summary>
//      /// <para>The mediant of two fractions is a new fraction between two fractions, obtained by adding the numerators and denominators of the given fractions1234. It is also known as the Farey mean.</para>
//      /// </summary>
//      /// <param name="a"></param>
//      /// <param name="b"></param>
//      /// <returns>The fraction between <paramref name="a"/> and <paramref name="b"/>.</returns>
//      public static Fraction Mediant(Fraction a, Fraction b)
//        => new(a.m_numerator + b.m_numerator, a.m_denominator + b.m_denominator);

//      public static Fraction Min(Fraction a, Fraction b)
//        => a <= b ? a : b;

//      // a/b * c/d == (ac)/(bd)
//      public static Fraction Multiply(Fraction a, Fraction b)
//        => new(a.m_numerator * b.m_numerator, a.m_denominator * b.m_denominator);

//      public static Fraction Multiply(Fraction a, System.Numerics.BigInteger i)
//        => new(a.m_numerator * i, a.m_denominator);

//      public static Fraction Negate(Fraction value)
//        => new(-value.m_numerator, value.m_denominator);

//      /// <summary>Returns the nth root of <paramref name="value"/>.</summary>
//      private static Fraction NthRoot(Fraction value, int nth, Fraction maxError)
//      {
//        if (nth < 0)
//          return NthRoot(Reciprocal(value), -nth, maxError);

//        if (nth == 0) throw new System.DivideByZeroException("Zeroth root is not defined.");
//        if (nth == int.MinValue) throw new System.OverflowException("Value cannot be negated.");

//        if (IsNegative(value))
//        {
//          if ((nth & 1) == 0) throw new System.ArithmeticException("Cannot compute even root of a negative number.");

//          return Negate(NthRoot(Negate(value), nth, maxError));
//        }

//        if (IsNegative(maxError) || IsZero(maxError)) throw new System.ArgumentOutOfRangeException(nameof(maxError), "Epsilon must be positive");

//        if (value == Zero) return Zero;
//        if (nth == 1) return value;
//        if (value == One) return value;

//        // First, get the closest integer to the root of the numerator and the denominator as an initial guess.
//        var guessNumerator = IntegerNthRoot(value.Numerator, nth);
//        var guessDenominator = IntegerNthRoot(value.Denominator, nth);

//        var x = new Fraction(guessNumerator.value, guessDenominator.value); // Initial guess.

//        // If we got exact roots for numerator and denominator, then we know the guess is exact.
//        if (guessNumerator.isExact && guessDenominator.isExact)
//          return x;

//        // Otherwise we use the implementation of nth-root algorithm: https://en.wikipedia.org/wiki/Nth_root_algorithm

//        while (true)
//        {
//          var diff = Divide((Divide(value, Pow(x, nth - 1)) - x), System.Numerics.BigInteger.CreateChecked(nth));

//          x = Add(x, diff);

//          if (Abs(diff).CompareTo(maxError) < 0)
//            break;
//        }

//        return x;

//        static (System.Numerics.BigInteger value, bool isExact) IntegerNthRoot(System.Numerics.BigInteger a, System.Numerics.BigInteger n)
//        {
//          if (a.IsZero)
//            return (System.Numerics.BigInteger.Zero, true);

//          if (n == 1 || a == System.Numerics.BigInteger.One)
//            return (a, true);

//          // Solve for x: x^n = a
//          // Start by computing a lower/upper bound on x.

//          var lowerX = System.Numerics.BigInteger.One;
//          var lowerPow = System.Numerics.BigInteger.One;

//          var upperX = System.Numerics.BigInteger.CreateChecked(2);
//          var upperPow = upperX.IntegerPow(n);

//          while (upperPow.CompareTo(a) < 0)
//          {
//            lowerX = upperX;
//            lowerPow = upperPow;
//            upperX = (lowerX + lowerX);
//            upperPow = upperX.IntegerPow(n);
//          }

//          if (upperPow.Equals(a))
//            return (upperX, true); // If it's the exact answer, return it.

//          // Now we know lowerX < x < upperX.
//          // Next do binary search between lowerX and upperX.

//          while (true)
//          {
//            var testX = (lowerX + upperX) / 2;

//            if (testX.Equals(lowerX) || testX.Equals(upperX))
//              break;

//            var testPow = testX.IntegerPow(n);

//            if (testPow.Equals(a))
//              return (testX, true); // We found an exact answer.
//            else if (testPow.CompareTo(a) > 0) // Still too high so set upper to the test value.
//            {
//              upperX = testX;
//              upperPow = testPow;
//            }
//            else // Still too low.
//            {
//              lowerX = testX;
//              lowerPow = testPow;
//            }
//          }

//          //we didn't get an exact answer, but we know the two integers closest to the exact value.
//          //now we just need to figure out which is closer and return that

//          return (a - lowerPow).CompareTo(upperPow - a) < 0 ? (lowerX, false) : (upperX, false);
//        }
//      }

//      private static Fraction NthRoot(Fraction value, System.Numerics.BigInteger nth, Fraction maxError)
//        => nth >= int.MinValue && nth <= int.MaxValue
//        ? NthRoot(value, int.CreateChecked(nth), maxError)
//        : throw new System.ArgumentOutOfRangeException(nameof(nth));

//      /// <summary>Returns this^exponent. Note: 0^0 will return 1/1.</summary>
//      public static Fraction Pow(Fraction value, int exponent)
//      {
//        if (exponent < 0)
//        {
//          if (IsZero(value)) throw new System.DivideByZeroException("Raise to the power of a negative exponent.");
//          else if (exponent == int.MinValue) throw new System.OverflowException("The negative exponent cannot be negated."); // Edge case: because we negate the exponent if it's negative, we would get into an infinite loop because -MIN_VALUE == MIN_VALUE.
//          else return new(System.Numerics.BigInteger.Pow(value.Denominator, -exponent), System.Numerics.BigInteger.Pow(value.Numerator, -exponent));
//        }

//        if (exponent == 0) return One;
//        else if (IsZero(value)) return Zero;
//        else if (exponent == 1) return value;
//        else return new(System.Numerics.BigInteger.Pow(value.Numerator, exponent), System.Numerics.BigInteger.Pow(value.Denominator, exponent));
//      }

//      private static Fraction Pow(Fraction value, System.Numerics.BigInteger exponent)
//        => exponent >= int.MinValue && exponent <= int.MaxValue
//        ? Pow(value, int.CreateChecked(exponent))
//        : throw new System.ArgumentOutOfRangeException(nameof(exponent));

//      /// <summary>Returns the reciprocal of <paramref name="value"/>.</summary>
//      public static Fraction Reciprocal(Fraction value)
//        => new(value.m_denominator, value.m_numerator);

//      // a/b % c/d == (ad % bc)/bd
//      public static Fraction Remainder(Fraction a, Fraction b)
//        => new((a.m_numerator * b.m_denominator) % (a.m_denominator * b.m_numerator), a.m_denominator * b.m_denominator);

//      public static Fraction Remainder(Fraction a, System.Numerics.BigInteger i)
//        => new(a.m_numerator % (a.m_denominator * i), a.m_denominator);

//      public static int Sign(Fraction value)
//        => value.m_numerator.Sign;

//      // a/b - c/d == (ad - bc)/bd
//      public static Fraction Subtract(Fraction a, Fraction b)
//        => new(a.m_numerator * b.m_denominator - a.m_denominator * b.m_numerator, a.m_denominator * b.m_denominator);

//      public static Fraction Subtract(Fraction a, System.Numerics.BigInteger i)
//        => new(a.m_numerator - (a.m_denominator * i), a.m_denominator);


//      /// <summary>Calculates rational approximations to a given real number.</summary>
//      /// <param name="x"></param>
//      /// <param name="maxIterations"></param>
//      /// <returns></returns>
//      /// <see href="https://stackoverflow.com/questions/12098461/how-can-i-detect-if-a-float-has-a-repeating-decimal-expansion-in-c/12101996#12101996"/>
//      public static Fraction ApproximateRational(double x, int maxIterations = 101)
//      {
//        if (x == 0) return Zero;

//        var Am = (Item1: System.Numerics.BigInteger.Zero, Item2: System.Numerics.BigInteger.One);
//        var Bm = (Item1: System.Numerics.BigInteger.One, Item2: System.Numerics.BigInteger.Zero);

//        System.Numerics.BigInteger A;
//        System.Numerics.BigInteger B;

//        var a = System.Numerics.BigInteger.Zero;
//        var b = System.Numerics.BigInteger.Zero;

//        if (x > 1)
//        {
//          var xW = double.Truncate(x);

//          var ar = ApproximateRational(x - xW, maxIterations);

//          return ar + System.Numerics.BigInteger.CreateChecked(xW);
//        }

//        var counter = 0;

//        for (; counter < maxIterations && x != 0; counter++)
//        {
//          var r = 1 / x;
//          var rR = double.Round(r);

//          var rT = System.Numerics.BigInteger.CreateChecked(rR);

//          A = Am.Item2 + rT * Am.Item1;
//          B = Bm.Item2 + rT * Bm.Item1;

//          if (double.IsInfinity(double.CreateChecked(A)) || double.IsInfinity(double.CreateChecked(B)))
//            break;

//          a = A;
//          b = B;

//          Am = (A, Am.Item1);
//          Bm = (B, Bm.Item1);

//          x = r - rR;
//        }

//        return new(a, b);
//      }

//      /// <summary>
//      /// <para>Tries to adjust the sign of the <paramref name="numerator"/>/<paramref name="denominator"/> into <paramref name="adjustedNumerator"/>/<paramref name="adjustedDenominator"/> and returns whether successful.</para>
//      /// </summary>
//      /// <param name="numerator"></param>
//      /// <param name="denominator"></param>
//      /// <param name="adjustedNumerator"></param>
//      /// <param name="adjustedDenominator"></param>
//      /// <returns></returns>
//      public static bool TryAdjustSignum(System.Numerics.BigInteger numerator, System.Numerics.BigInteger denominator, out System.Numerics.BigInteger adjustedNumerator, out System.Numerics.BigInteger adjustedDenominator)
//      {
//        if (System.Numerics.BigInteger.IsNegative(denominator)) // Ensure denominator remain positivity.
//        {
//          adjustedNumerator = -numerator;
//          adjustedDenominator = -denominator;

//          return true;
//        }
//        else
//        {
//          adjustedNumerator = numerator;
//          adjustedDenominator = denominator;

//          return false;
//        }
//      }

//      public static char GetSymbolFractionSlash(bool preferUnicode = true) => preferUnicode ? '\u2044' : '\u002F';

//      public static bool TryGetMixedParts(Fraction value, out System.Numerics.BigInteger wholeNumber, out System.Numerics.BigInteger properNumerator, out System.Numerics.BigInteger properDenominator)
//      {
//        if (IsMixedNumber(value))
//        {
//          (wholeNumber, properNumerator) = System.Numerics.BigInteger.DivRem(value.m_numerator, value.m_denominator);

//          if (System.Numerics.BigInteger.IsNegative(properNumerator))
//            properNumerator = System.Numerics.BigInteger.Abs(properNumerator);

//          properDenominator = value.m_denominator;

//          return true;
//        }
//        else
//        {
//          wholeNumber = properNumerator = properDenominator = System.Numerics.BigInteger.Zero;

//          return false;
//        }
//      }

//      /// <summary>
//      /// <para>Euclid's algorithm is used to simplify a fraction.</para>
//      /// </summary>
//      /// <param name="numerator"></param>
//      /// <param name="denominator"></param>
//      /// <param name="reducedNumerator"></param>
//      /// <param name="reducedDenominator"></param>
//      /// <returns></returns>
//      public static bool TryReduce(System.Numerics.BigInteger numerator, System.Numerics.BigInteger denominator, out System.Numerics.BigInteger reducedNumerator, out System.Numerics.BigInteger reducedDenominator)
//      {
//        if (IsReducible(numerator, denominator, out var greatestCommonDenominator))
//        {
//          reducedNumerator = numerator / greatestCommonDenominator;
//          reducedDenominator = denominator /= greatestCommonDenominator;

//          return true;
//        }
//        else
//        {
//          reducedNumerator = numerator;
//          reducedDenominator = denominator;

//          return false;
//        }
//      }

//      #region Methods to read and write binary.

//      //// Read Binary representation of BigRational.
//      public static BigRational ReadBytes(ReadOnlySpan<byte> source, bool isUnsigned = false, bool isBigEndian = true)
//      {
//        var bytesInNumerator = System.Buffers.Binary.BinaryPrimitives.ReadInt32BigEndian(source[..4]);

//        var numerator = new System.Numerics.BigInteger(source.Slice(4, bytesInNumerator), isUnsigned, isBigEndian);

//        var bytesInDenominator = System.Buffers.Binary.BinaryPrimitives.ReadInt32BigEndian(source.Slice(4 + bytesInNumerator, 4));

//        var denominator = new System.Numerics.BigInteger(source.Slice(4 + bytesInNumerator + 4, bytesInDenominator), isUnsigned, isBigEndian);

//        return new BigRational(numerator, denominator);
//      }

//      // Try Read Binary representation of BigRational.
//      public static bool TryReadBytes(ReadOnlySpan<byte> source, out BigRational value, bool isUnsigned = false, bool isBigEndian = true)
//      {
//        try
//        {
//          value = ReadBytes(source, isUnsigned, isBigEndian);
//          return true;
//        }
//        catch { }

//        value = default;
//        return false;
//      }

//      // Write Binary representation of BigRational.
//      public int WriteBytes(Span<byte> source, bool isUnsigned = false, bool isBigEndian = true)
//      {
//        var bytesInNumerator = m_numerator.GetByteCount();

//        if (isBigEndian) System.Buffers.Binary.BinaryPrimitives.WriteInt32BigEndian(source[..4], bytesInNumerator);
//        else System.Buffers.Binary.BinaryPrimitives.WriteInt32LittleEndian(source[..4], bytesInNumerator);

//        if (!m_numerator.TryWriteBytes(source.Slice(4, bytesInNumerator), out var bytesWrittenNumerator, isUnsigned, isBigEndian))
//          throw new System.InvalidOperationException();

//        var bytesInDenominator = m_denominator.GetByteCount();

//        if (isBigEndian) System.Buffers.Binary.BinaryPrimitives.WriteInt32BigEndian(source.Slice(4 + bytesInNumerator, 4), bytesInDenominator);
//        else System.Buffers.Binary.BinaryPrimitives.WriteInt32LittleEndian(source.Slice(4 + bytesInNumerator, 4), bytesInDenominator);

//        if (!m_denominator.TryWriteBytes(source[(4 + bytesInNumerator + 4)..], out var bytesWrittenDenominator, isUnsigned, isBigEndian))
//          throw new System.InvalidOperationException();

//        return 4 + bytesInNumerator + 4 + bytesInDenominator;
//      }

//      // Try Write Binary representation of BigRational.
//      public bool TryWriteBytes(Span<byte> source, out int bytesWritten, bool isUnsigned = false, bool isBigEndian = true)
//      {
//        try
//        {
//          bytesWritten = WriteBytes(source, isUnsigned, isBigEndian);
//          return true;
//        }
//        catch { }

//        bytesWritten = 0;
//        return false;
//      }

//      #endregion // Methods to read and write binary.

//      #endregion // Static methods

//      #region Overloaded operators

//      #endregion // Overloaded operators

//      #region Implemented interfaces

//      // System.Numerics.IComparisonOperators<>
//      public static bool operator <(Fraction a, Fraction b) => a.CompareTo(b) < 0;
//      public static bool operator <=(Fraction a, Fraction b) => a.CompareTo(b) <= 0;
//      public static bool operator >(Fraction a, Fraction b) => a.CompareTo(b) > 0;
//      public static bool operator >=(Fraction a, Fraction b) => a.CompareTo(b) >= 0;

//      // System.Numerics.IAdditionOperators<>
//      public static Fraction operator +(Fraction a, Fraction b) => Add(a, b);
//      public static Fraction operator +(Fraction a, System.Numerics.BigInteger b) => Add(a, b);

//      // System.Numerics.IAdditiveIdentity<>
//      public static Fraction AdditiveIdentity => Zero;

//      // System.Numerics.IDecrementOperators<>
//      public static Fraction operator --(Fraction value) => Subtract(value, System.Numerics.BigInteger.One);

//      // System.Numerics.IDivisionOperators<>
//      public static Fraction operator /(Fraction a, Fraction b) => Divide(a, b);
//      public static Fraction operator /(Fraction a, System.Numerics.BigInteger b) => Divide(a, b);

//      // System.Numerics.IIncrementOperators<>
//      public static Fraction operator ++(Fraction value) => Add(value, System.Numerics.BigInteger.One);

//      // System.Numerics.IModulusOperators<> // a/b % c/d == (ad % bc)/bd
//      public static Fraction operator %(Fraction a, Fraction b) => Remainder(a, b);
//      public static Fraction operator %(Fraction a, System.Numerics.BigInteger b) => Remainder(a, b);

//      // System.Numerics.IMultiplicativeIdentity<>
//      public static Fraction MultiplicativeIdentity => One;

//      // System.Numerics.IMultiplyOperators<> // a/b * c/d == (ac)/(bd)
//      public static Fraction operator *(Fraction a, Fraction b) => Multiply(a, b);
//      public static Fraction operator *(Fraction a, System.Numerics.BigInteger b) => Multiply(a, b);

//      #region System.Numerics.INumberBase<>

//      public static int Radix => 2;
//      public static bool IsCanonical(Fraction value) => !IsReducible(value.m_numerator, value.m_denominator, out var _);
//      public static bool IsComplexNumber(Fraction value) => false;
//      public static bool IsFinite(Fraction value) => true;
//      public static bool IsImaginaryNumber(Fraction value) => false;
//      public static bool IsInfinity(Fraction value) => false;
//      public static bool IsNaN(Fraction value) => false;
//      public static bool IsNegativeInfinity(Fraction value) => false;
//      public static bool IsNormal(Fraction value) => !IsZero(value);
//      public static bool IsPositiveInfinity(Fraction value) => false;
//      public static bool IsRealNumber(Fraction value) => true;
//      public static bool IsSubnormal(Fraction value) => false;
//      public static Fraction MaxMagnitude(Fraction a, Fraction b) => Max(a, b);
//      public static Fraction MaxMagnitudeNumber(Fraction a, Fraction b) => Max(a, b);
//      public static Fraction MinMagnitude(Fraction a, Fraction b) => Min(a, b);
//      public static Fraction MinMagnitudeNumber(Fraction a, Fraction b) => Min(a, b);
//      public static Fraction Parse(ReadOnlySpan<char> s, System.Globalization.NumberStyles style, System.IFormatProvider? provider) => throw new NotImplementedException();
//      public static Fraction Parse(string s, System.Globalization.NumberStyles style, System.IFormatProvider? provider) => throw new NotImplementedException();
//      public static Fraction Parse(System.ReadOnlySpan<char> s, System.IFormatProvider? provider) => throw new NotImplementedException();
//      public static Fraction Parse(string s, System.IFormatProvider? provider) => throw new NotImplementedException();
//      public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, System.IFormatProvider? provider)
//      {
//        charsWritten = default;
//        return true;
//      }
//      public static bool TryParse(System.ReadOnlySpan<char> s, System.Globalization.NumberStyles style, System.IFormatProvider? provider, out Fraction result) => throw new NotImplementedException();
//      public static bool TryParse(string? s, System.Globalization.NumberStyles style, System.IFormatProvider? provider, out Fraction result) => throw new NotImplementedException();
//      public static bool TryParse(ReadOnlySpan<char> s, System.IFormatProvider? provider, out Fraction result) => throw new NotImplementedException();
//      public static bool TryParse(string? s, System.IFormatProvider? provider, out Fraction result) => throw new NotImplementedException();

//      static bool System.Numerics.INumberBase<Fraction>.TryConvertFromChecked<TOther>(TOther value, out Fraction result)
//      {
//        try
//        {
//          result = CreateChecked(value);
//          return true;
//        }
//        catch
//        {
//          result = default;
//          return false;
//        }
//      }
//      static bool System.Numerics.INumberBase<Fraction>.TryConvertFromSaturating<TOther>(TOther value, out Fraction result)
//      {
//        try
//        {
//          result = CreateSaturating(value);
//          return true;
//        }
//        catch
//        {
//          result = default;
//          return false;
//        }
//      }
//      static bool System.Numerics.INumberBase<Fraction>.TryConvertFromTruncating<TOther>(TOther value, out Fraction result)
//      {
//        try
//        {
//          result = CreateTruncating(value);
//          return true;
//        }
//        catch
//        {
//          result = default;
//          return false;
//        }
//      }
//      static bool System.Numerics.INumberBase<Fraction>.TryConvertToChecked<TOther>(Fraction value, out TOther result)
//      {
//        result = default!;
//        return false;
//      }
//      static bool System.Numerics.INumberBase<Fraction>.TryConvertToSaturating<TOther>(Fraction value, out TOther result)
//      {
//        result = default!;
//        return false;
//      }
//      static bool System.Numerics.INumberBase<Fraction>.TryConvertToTruncating<TOther>(Fraction value, out TOther result)
//      {
//        result = default!;
//        return false;
//      }

//      #endregion // System.Numerics.INumberBase<>

//      // System.Numerics.IPowerFunctions<>
//      public static Fraction Pow(Fraction x, Fraction y) => Pow(NthRoot(x, y.m_denominator, EpsilonLikeDouble), y.m_numerator); // is var abspow && IsNegative(exponent) ? One / abspow : abspow;

//      // System.Numerics.IRootFunctions<>
//      public static Fraction Cbrt(Fraction x) => NthRoot(x, 3, EpsilonLikeDouble);
//      public static Fraction Hypot(Fraction x, Fraction y) => Sqrt(x * x + y * y);
//      public static Fraction RootN(Fraction x, int n) => NthRoot(x, n, EpsilonLikeDouble);
//      public static Fraction Sqrt(Fraction x) => NthRoot(x, 2, EpsilonLikeDouble);

//      // System.Numerics.ISubtractionOperators<>
//      public static Fraction operator -(Fraction a, Fraction b) => Subtract(a, b);
//      public static Fraction operator -(Fraction a, System.Numerics.BigInteger b) => Subtract(a, b);

//      // System.Numerics.IUnaryNegationOperators<>
//      public static Fraction operator -(Fraction value) => Negate(value);

//      // System.Numerics.IUnaryPlusOperators<>
//      public static Fraction operator +(Fraction value) => value;

//      // IComparable<>
//      public int CompareTo(Fraction other)
//        => Sign(this) is var thisSign && Sign(other) is var otherSign
//        && thisSign < otherSign
//        ? -1
//        : thisSign > otherSign
//        ? 1
//        : m_denominator.Equals(other.m_denominator)
//        ? m_numerator.CompareTo(other.m_numerator)
//        : (m_numerator * other.m_denominator).CompareTo(m_denominator * other.m_numerator);

//      // IComparable
//      public int CompareTo(object? other) => other is Fraction o ? CompareTo(o) : -1;

//      // IFormattable
//      public string ToString(string? format, System.IFormatProvider? formatProvider)
//        => IsProper
//        ? RatioDisplay.AslashB.ToRatioString(m_numerator, m_denominator, format, formatProvider)
//        : TryGetMixedParts(this, out var wholeNumber, out var properNumerator, out var properDenominator)
//        ? $"{wholeNumber} {RatioDisplay.AslashB.ToRatioString(properNumerator, properDenominator, format, formatProvider)}"
//        : m_numerator.ToString(); // It is a whole number and we return a simple integer string.

//      #region IValueQuantifiable<>

//      public double Value => double.CreateChecked(m_numerator) / double.CreateChecked(m_denominator);

//      #endregion // IValueQuantifiable<>

//      #endregion // Implemented interfaces

//      public override string ToString() => ToString(null, null);
//    }
//  }
//}
