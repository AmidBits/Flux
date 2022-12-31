namespace Flux
{
  /// <summary>Simple fraction, unit of rational number, i.e. in the form of numerator and denominator.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Fraction#Simple,_common,_or_vulgar_fractions"/>
  public readonly record struct Fraction
    : System.IComparable, System.IComparable<Fraction>, System.IConvertible, Quantities.IQuantifiable<double>
    , System.Numerics.IAdditiveIdentity<Fraction, Fraction>
    , System.Numerics.IAdditionOperators<Fraction, Fraction, Fraction>
    , System.Numerics.IDecrementOperators<Fraction>
    , System.Numerics.IDivisionOperators<Fraction, Fraction, Fraction>
    , System.Numerics.IIncrementOperators<Fraction>
    , System.Numerics.IModulusOperators<Fraction, Fraction, Fraction>
    , System.Numerics.IMultiplicativeIdentity<Fraction, Fraction>
    , System.Numerics.IMultiplyOperators<Fraction, Fraction, Fraction>
    , System.Numerics.IRootFunctions<Fraction>
    , System.Numerics.ISignedNumber<Fraction>
    , System.Numerics.ISubtractionOperators<Fraction, Fraction, Fraction>
    , System.Numerics.IUnaryNegationOperators<Fraction, Fraction>
    , System.Numerics.IUnaryPlusOperators<Fraction, Fraction>
    , System.Numerics.INumberBase<Fraction>
  {
    public static readonly Fraction EpsilonLikeSingle = new(1, 1000000);
    public static readonly Fraction EpsilonLikeDouble = new(1, 1000000000000000);

    /// <summary>Represents a fraction of the Golden Ratio.</summary>
    public static readonly Fraction GoldenRatio = new(7540113804746346429L, 4660046610375530309L, false);

    private readonly System.Numerics.BigInteger m_numerator;
    private readonly System.Numerics.BigInteger m_denominator;

    /// <summary>Creates a new simple fraction from the specified numerator and denominator. Optionally the fraction can be reduced, if possible.</summary>
    /// <param name="numerator"></param>
    /// <param name="denominator"></param>
    /// <param name="reduceIfPossible">If true, reduce if possible, and if false, do not attempt to reduce.</param>
    public Fraction(System.Numerics.BigInteger numerator, System.Numerics.BigInteger denominator, bool reduceIfPossible)
    {
      if (denominator.IsZero) throw new System.DivideByZeroException();

      if (denominator.Sign < 0)
      {
        numerator = -numerator;
        denominator = -denominator;
      }

      if (reduceIfPossible && IsReducible(numerator, denominator, out var gcd))
      {
        m_numerator = numerator / gcd;
        m_denominator = denominator / gcd;
      }
      else
      {
        m_numerator = numerator;
        m_denominator = denominator;
      }
    }
    public Fraction(System.Numerics.BigInteger whole, System.Numerics.BigInteger numerator, System.Numerics.BigInteger denominator)
      : this(whole * denominator + numerator, denominator, true)
    { }
    /// <summary>Creates a new simple fraction from the specified numerator and denominator. If the fraction can be reduced, it will be.</summary>
    /// <param name="numerator"></param>
    /// <param name="denominator"></param>
    public Fraction(System.Numerics.BigInteger numerator, System.Numerics.BigInteger denominator)
      : this(numerator, denominator, true)
    { }
    public Fraction(System.Numerics.BigInteger value)
      : this(value, System.Numerics.BigInteger.One, false)
    { }

    public System.Numerics.BigInteger Numerator
      => m_numerator;
    public System.Numerics.BigInteger Denominator
      => m_denominator;

    /// <summary>A fraction is proper if its absolute value is strictly less than 1, i.e. if it is greater than -1 and less than 1.</summary>
    public bool IsProper
      => m_numerator < m_denominator;

    /// <summary>A fraction is a unit fraction if its numerator is equal to 1.</summary>
    public bool IsUnitFraction
      => m_numerator == System.Numerics.BigInteger.One;

    ///// <summary>Indicates whether the number is 0.</summary>
    //public bool IsZero => System.Numerics.BigInteger.IsZero(m_numerator) && m_denominator == System.Numerics.BigInteger.One;

    /// <summary>Indicates the sign of the number, i.e. 1, 0 or -1.</summary>
    //public int Sign
    //  => System.Numerics.BigInteger.Sign(m_numerator) is var ns && ns == 0
    //  ? 0
    //  : System.Numerics.BigInteger.Sign(m_denominator) is var ds && (ns < 0 && ds > 0) || (ds < 0 && ns >= 0)
    //  ? -1
    //  : 1;

    /// <summary>Returns the integer quotient and an out variable containing the remainder.</summary>
    public System.Numerics.BigInteger ToDivRem(out System.Numerics.BigInteger remainder)
    {
      remainder = m_numerator % m_denominator;

      return m_numerator / m_denominator;
    }

    /// <summary>Yields a string with the fraction in improper (if applicable) fractional notation.</summary>
    public string ToImproperString()
    {
      var sb = new System.Text.StringBuilder();

      if (IsProper)
        return ToProperString();
      else
      {
        sb.Append(ToDivRem(out var remainder));

        if (remainder > System.Numerics.BigInteger.Zero)
        {
          sb.Append(' ');
          sb.Append(remainder);
          sb.Append('\u2044');
          sb.Append(m_denominator);
        }
      }

      return sb.ToString();
    }

    /// <summary>Yields a string with the fraction in proper fractional notation.</summary>
    public string ToProperString()
      => $"{m_numerator}\u2044{m_denominator}";

    /// <summary>Returns the quotient result from division of numerator / denominator.</summary>
    public double ToQuotient()
      => double.CreateChecked(m_numerator) / double.CreateChecked(m_denominator);

    #region Static methods

    /// <summary>Calculates rational approximations to a given real number.</summary>
    /// <param name="x"></param>
    /// <param name="maxIterations"></param>
    /// <returns></returns>
    /// <see href="https://stackoverflow.com/questions/12098461/how-can-i-detect-if-a-float-has-a-repeating-decimal-expansion-in-c/12101996#12101996"/>
    public static Fraction ApproximateRational(double x, int maxIterations = 101)
    {
      var Am = (System.Numerics.BigInteger.Zero, System.Numerics.BigInteger.One);
      var Bm = (System.Numerics.BigInteger.One, System.Numerics.BigInteger.Zero);

      var A = System.Numerics.BigInteger.Zero;
      var B = System.Numerics.BigInteger.Zero;

      var a = System.Numerics.BigInteger.Zero;
      var b = System.Numerics.BigInteger.Zero;

      if (x > 1)
      {
        var xW = double.Truncate(x);

        var ar = ApproximateRational(x - xW, maxIterations);

        return ar + System.Numerics.BigInteger.CreateChecked(xW);
      }

      var counter = 0;

      for (; counter < maxIterations && x != 0; counter++)
      {
        var r = 1 / x;
        var rR = double.Round(r);

        var rT = System.Numerics.BigInteger.CreateChecked(rR);

        A = Am.Item2 + rT * Am.Item1;
        B = Bm.Item2 + rT * Bm.Item1;

        if (double.IsInfinity(double.CreateChecked(A)) || double.IsInfinity(double.CreateChecked(B)))
          break;

        a = A;
        b = B;

        Am = (A, Am.Item1);
        Bm = (B, Bm.Item1);

        x = r - rR;
      }

      return new(a, b);
    }

    /// <summary>Returns the value with the sign of the second argument.</summary>
    public static Fraction CopySign(Fraction value, Fraction sign)
      => (Sign(sign) == 0 || IsZero(value))
      ? Zero
      : (value.m_numerator.Sign < 0 && Sign(sign) > 0) || (value.m_numerator.Sign > 0 && Sign(sign) < 0)
      ? new(-value.m_numerator, value.m_denominator, true)
      : value;

    public static Fraction CreateChecked<TOther>(TOther o)
      where TOther : System.Numerics.INumberBase<TOther>
    {
      if (o.IsIBinaryInteger())
        return new(System.Numerics.BigInteger.CreateChecked(o));
      else if (o.IsIFloatingPoint())
        return ApproximateRational(double.CreateChecked(o));
      else if (o is Fraction f)
        return new(f.Numerator, f.Denominator);

      throw new System.NotSupportedException();
    }
    public static Fraction CreateSaturating<TOther>(TOther o)
      where TOther : System.Numerics.INumberBase<TOther>
    {
      if (o.IsIBinaryInteger())
        return new(System.Numerics.BigInteger.CreateSaturating(o));
      else if (o.IsIFloatingPoint())
        return ApproximateRational(double.CreateSaturating(o));
      else if (o is Fraction f)
        return new(f.Numerator, f.Denominator);

      throw new System.NotSupportedException();
    }
    public static Fraction CreateTruncating<TOther>(TOther o)
      where TOther : System.Numerics.INumberBase<TOther>
    {
      if (o.IsIBinaryInteger())
        return new(System.Numerics.BigInteger.CreateTruncating(o));
      else if (o.IsIFloatingPoint())
        return ApproximateRational(double.CreateTruncating(o));
      else if (o is Fraction f)
        return new(f.Numerator, f.Denominator);

      throw new System.NotSupportedException();
    }

    /// <summary>Returns the greatest common divisor (GCD) of two values.</summary>
    /// <remarks>The result is guaranteed to be a reduced fraction. If you try to further simplify this to: (gcd(a,c) * gcd(b,d)) / (|b*d|), then the result will not be reduced, and the operation actually takes about 60% longer.</remarks>
    /// <example>gcd((a/b),(c/d)) = gcd(a,c) / lcm(b,d) = gcd(a,c) / (|b*d|/gcd(b,d))</example>
    public static Fraction GreatestCommonDivisor(Fraction a, Fraction b)
      => IsZero(a)
      ? Abs(b)
      : IsZero(b)
      ? Abs(a)
      : new(
          GenericMath.GreatestCommonDivisor(a.m_numerator, b.m_numerator),
          System.Numerics.BigInteger.Abs(a.m_denominator * b.m_denominator) / GenericMath.GreatestCommonDivisor(a.m_denominator, b.m_denominator),
          true
        );

    /// <summary>Returns the least common multiple (LCM) of two values.</summary>
    /// <remarks>The result is guaranteed to be a reduced fraction. If you try to further simplify this to: |a*c| / (gcd(a,c) * gcd(b,d)), then the result will not be reduced, and the operation actually takes about 60% longer.</remarks>
    /// <example>lcm((a/b),(c/d)) = lcm(a,c) / gcd(b,d) = (|a*c| / gcd(a,c)) / gcd(b,d)</example>
    public static Fraction LeastCommonMultiple(Fraction a, Fraction b)
      => (IsZero(a) || IsZero(b))
      ? Zero
      : new(
          System.Numerics.BigInteger.Abs(a.m_numerator * b.m_numerator) / GenericMath.GreatestCommonDivisor(a.m_numerator, b.m_numerator),
          GenericMath.GreatestCommonDivisor(a.m_denominator, b.m_denominator),
          true
        );

    /// <summary>Returns the mediant of two values.</summary>
    public static Fraction Mediant(Fraction a, Fraction b)
      => new(a.m_numerator + b.m_numerator, a.m_denominator + b.m_denominator, false);

    /// <summary>Returns the nth root of the value.</summary>
    private static Fraction NthRoot(Fraction value, int n, Fraction maxError)
    {
      if (n < 0)
        return NthRoot(Reciprocal(value), -n, maxError);

      if (n == 0) throw new System.DivideByZeroException("Zeroth root is not defined.");
      if (n == int.MinValue) throw new System.OverflowException("Value cannot be negated.");


      if (Sign(value) < 0)
      {
        if ((n & 1) == 0) throw new System.ArithmeticException("Cannot compute even root of a negative number.");

        return -NthRoot(-value, n, maxError);
      }

      if (Sign(maxError) <= 0) throw new System.ArgumentOutOfRangeException(nameof(maxError), "Epsilon must be positive");

      if (value == Zero)
        return Zero;
      if (n == 1)
        return value;
      if (value == One)
        return value;

      // First, get the closest integer to the root of the numerator and the denominator as an initial guess.

      var guessNumerator = IntegerNthRoot(value.Numerator, n);
      var guessDenominator = IntegerNthRoot(value.Denominator, n);

      var initialGuess = new Fraction(guessNumerator.value, guessDenominator.value);

      // If we got exact roots for numerator and denominator, then we know the guess is exact.

      if (guessNumerator.isExact && guessDenominator.isExact)
        return initialGuess;

      // Otherwise we use the implementation of nth-root algorithm: https://en.wikipedia.org/wiki/Nth_root_algorithm

      var x = initialGuess;

      while (true)
      {
        var diff = (value / Pow(x, n - 1) - x) / System.Numerics.BigInteger.CreateChecked(n);

        x += diff;

        if (Abs(diff).CompareTo(maxError) < 0)
          break;
      }

      return x;

      static (System.Numerics.BigInteger value, bool isExact) IntegerNthRoot(System.Numerics.BigInteger a, int n)
      {
        if (a.IsZero)
          return (System.Numerics.BigInteger.Zero, true);

        if (n == 1 || a == System.Numerics.BigInteger.One)
          return (a, true);

        // Solve for x:  x^n = a
        // Start by computing a lower/upper bound on x.

        var lowerX = System.Numerics.BigInteger.One;
        var lowerPow = System.Numerics.BigInteger.One;

        var upperX = System.Numerics.BigInteger.CreateChecked(2);
        var upperPow = upperX.IntegerPow(n);

        while (upperPow.CompareTo(a) < 0)
        {
          lowerX = upperX;
          lowerPow = upperPow;
          upperX = lowerX.Multiply(2);
          upperPow = upperX.IntegerPow(n);
        }

        if (upperPow.Equals(a))
          return (upperX, true); // If it's the exact answer, return it.

        // Now we know lowerX < x < upperX.
        // Next do binary search between lowerX and upperX.

        while (true)
        {
          var testX = (lowerX + upperX).Divide(2);

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

    /// <summary>Returns this^exponent. Note: 0^0 will return 1/1.</summary>
    public static Fraction Pow(Fraction value, int exponent)
    {
      if (exponent < 0)
      {
        if (IsZero(value)) throw new System.DivideByZeroException(@"Raising zero to negative exponent.");
        else if (exponent == int.MinValue) throw new System.OverflowException(@"Exponent cannot be negated."); // edge case: because we negate the exponent if it's negative, we would get into an infinite loop because -MIN_VALUE == MIN_VALUE
        else return new(value.Denominator.IntegerPow(-exponent), value.Numerator.IntegerPow(-exponent), true);
      }

      if (exponent == 0) return One;
      else if (IsZero(value)) return Zero;
      else if (exponent == 1) return value;
      else return new(value.Numerator.IntegerPow(exponent), value.Denominator.IntegerPow(exponent), true);
    }

    /// <summary>Returns the reciprocal of a value.</summary>
    public static Fraction Reciprocal(Fraction value)
      => IsZero(value)
      ? throw new System.DivideByZeroException(@"Reciprocal of zero.")
      : new(value.m_denominator, value.m_numerator, true);

    /// <summary>Indicates the sign of the number, i.e. 1, 0 or -1.</summary>
    public static int Sign(Fraction value)
      => value.m_numerator.Sign is var ns && ns == 0
      ? 0
      : value.m_denominator.Sign is var ds && (ns < 0 && ds > 0) || (ds < 0 && ns >= 0)
      ? -1
      : 1;

    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Fraction v) => v.Value;

    public static bool operator <(Fraction a, Fraction b) => a.CompareTo(b) < 0;
    public static bool operator <=(Fraction a, Fraction b) => a.CompareTo(b) <= 0;
    public static bool operator >(Fraction a, Fraction b) => a.CompareTo(b) > 0;
    public static bool operator >=(Fraction a, Fraction b) => a.CompareTo(b) >= 0;

    //static Fraction System.Numerics.IComparisonOperators<Fraction, Fraction, Fraction>.operator <(Fraction a, Fraction b) => MinMagnitude(a, b);
    //static Fraction System.Numerics.IComparisonOperators<Fraction, Fraction, Fraction>.operator <=(Fraction a, Fraction b) => MinMagnitude(a, b);
    //static Fraction System.Numerics.IComparisonOperators<Fraction, Fraction, Fraction>.operator >(Fraction a, Fraction b) => MaxMagnitude(a, b);
    //static Fraction System.Numerics.IComparisonOperators<Fraction, Fraction, Fraction>.operator >=(Fraction a, Fraction b) => MaxMagnitude(a, b);

    #endregion Overloaded operators

    string System.IFormattable.ToString(string? format, System.IFormatProvider? provider) => ToQuantityString();

    #region Implemented interfaces

    // System.Numerics.IAdditionOperators<>
    public static Fraction operator +(Fraction a, Fraction b)
    {
      var lcm = GenericMath.LeastCommonMultiple(a.m_denominator, b.m_denominator);

      var an = lcm / a.m_denominator * a.m_numerator;
      var bn = lcm / b.m_denominator * b.m_numerator;

      return new(an + bn, lcm);
    }
    public static Fraction operator +(Fraction a, System.Numerics.BigInteger b)
      => a + new Fraction(b);

    // System.Numerics.IAdditiveIdentity<>
    public static Fraction AdditiveIdentity => Zero;

    // System.Numerics.IDecrementOperators<>
    public static Fraction operator --(Fraction f) => new(f.Numerator - System.Numerics.BigInteger.One, f.Denominator);

    // System.Numerics.IDivisionOperators<>
    public static Fraction operator /(Fraction a, Fraction b)
      => new(a.m_numerator * b.m_denominator, a.m_denominator * b.m_numerator);
    public static Fraction operator /(Fraction a, System.Numerics.BigInteger b)
      => a / new Fraction(b);

    // System.Numerics.IFloatingPointConstants<>
    public static Fraction E => new Fraction(System.Numerics.BigInteger.Parse("611070150698522592097"), System.Numerics.BigInteger.Parse("224800145555521536000"), false);
    public static Fraction Pi => new(System.Numerics.BigInteger.Parse("2646693125139304345"), System.Numerics.BigInteger.Parse("842468587426513207"), false);
    public static Fraction Tau => Pi.Multiply(2);

    // System.Numerics.IIncrementOperators<>
    public static Fraction operator ++(Fraction f) => new(f.Numerator + System.Numerics.BigInteger.One, f.Denominator);

    // System.Numerics.IModulusOperators<>
    public static Fraction operator %(Fraction a, Fraction b)
      => Fraction.IsInteger(b) ? a % b.m_numerator : throw new System.ArithmeticException("Second argument must be an integer.");
    public static Fraction operator %(Fraction a, System.Numerics.BigInteger b)
      => new(a.m_numerator % (a.m_denominator * b), a.m_denominator);

    // System.Numerics.IMultiplicativeIdentity<>
    public static Fraction MultiplicativeIdentity => One;

    // System.Numerics.IMultiplyOperators<>
    public static Fraction operator *(Fraction a, Fraction b)
      => new(a.m_numerator * b.m_numerator, a.m_denominator * b.m_denominator);
    public static Fraction operator *(Fraction a, System.Numerics.BigInteger b)
      => a * new Fraction(b);

    // System.Numerics.IRootFunctions<>
    public static Fraction Cbrt(Fraction value) => NthRoot(value, 3, EpsilonLikeDouble);
    public static Fraction Hypot(Fraction v1, Fraction v2) => Sqrt(v1 * v1 + v2 * v2);
    public static Fraction RootN(Fraction value, int n) => NthRoot(value, n, EpsilonLikeDouble);
    public static Fraction Sqrt(Fraction value) => NthRoot(value, 2, EpsilonLikeDouble);

    // System.Numerics.ISignedNumber<>
    public static Fraction NegativeOne => -One;

    // System.Numerics.ISubtractionOperators<>
    public static Fraction operator -(Fraction a, Fraction b)
    {
      var lcm = GenericMath.LeastCommonMultiple(a.m_denominator, b.m_denominator);

      var an = lcm / a.m_denominator * a.m_numerator;
      var bn = lcm / b.m_denominator * b.m_numerator;

      return new(an - bn, lcm);
    }
    public static Fraction operator -(Fraction a, System.Numerics.BigInteger b)
      => a - new Fraction(b);

    // System.Numerics.IUnaryNegationOperators<>
    public static Fraction operator -(Fraction f) => new(f.m_numerator, -f.m_denominator, false);

    // System.Numerics.IUnaryPlusOperators<>
    public static Fraction operator +(Fraction f) => new(f.m_numerator, f.m_denominator, false);

    #region System.Numerics.INumberBase<>

    public static Fraction One => new(System.Numerics.BigInteger.One, System.Numerics.BigInteger.One, false);
    public static int Radix => 2;
    public static Fraction Zero => new(System.Numerics.BigInteger.Zero, System.Numerics.BigInteger.One, false);
    public static Fraction Abs(Fraction value) => CopySign(value, One);
    public static bool IsCanonical(Fraction f) => false;
    public static bool IsComplexNumber(Fraction f) => false;
    public static bool IsEvenInteger(Fraction f) => IsInteger(f) && System.Numerics.BigInteger.IsEvenInteger(f.m_numerator);
    public static bool IsFinite(Fraction f) => false;
    public static bool IsImaginaryNumber(Fraction f) => false;
    public static bool IsInfinity(Fraction f) => false;
    public static bool IsInteger(Fraction f) => f.m_denominator == System.Numerics.BigInteger.One;
    public static bool IsNaN(Fraction f) => false;
    public static bool IsNegative(Fraction f) => System.Numerics.BigInteger.IsNegative(f.m_numerator) || System.Numerics.BigInteger.IsNegative(f.m_denominator);
    public static bool IsNegativeInfinity(Fraction f) => false;
    public static bool IsNormal(Fraction f) => false;
    public static bool IsOddInteger(Fraction f) => IsInteger(f) && System.Numerics.BigInteger.IsOddInteger(f.m_numerator);
    public static bool IsPositive(Fraction f) => System.Numerics.BigInteger.IsPositive(f.m_numerator) && System.Numerics.BigInteger.IsPositive(f.m_denominator);
    public static bool IsPositiveInfinity(Fraction f) => false;
    public static bool IsRealNumber(Fraction f) => true;
    public static bool IsReducible(System.Numerics.BigInteger numerator, System.Numerics.BigInteger denominator, out System.Numerics.BigInteger gcd)
      => (gcd = GenericMath.GreatestCommonDivisor(numerator, denominator)) > System.Numerics.BigInteger.One;
    public static bool IsSubnormal(Fraction f) => false;
    public static bool IsZero(Fraction f) => f.m_numerator.IsZero && f.m_denominator == System.Numerics.BigInteger.One;
    public static Fraction MaxMagnitude(Fraction a, Fraction b) => a >= b ? a : b;
    public static Fraction MaxMagnitudeNumber(Fraction a, Fraction b) => a >= b ? a : b;
    public static Fraction MinMagnitude(Fraction a, Fraction b) => a <= b ? a : b;
    public static Fraction MinMagnitudeNumber(Fraction a, Fraction b) => a <= b ? a : b;
    public static Fraction Parse(ReadOnlySpan<char> s, System.Globalization.NumberStyles style, IFormatProvider? provider)
    {
      throw new NotImplementedException();
    }
    public static Fraction Parse(string s, System.Globalization.NumberStyles style, IFormatProvider? provider)
    {
      throw new NotImplementedException();
    }
    public static Fraction Parse(System.ReadOnlySpan<char> s, System.IFormatProvider? provider) => throw new NotImplementedException();
    public static Fraction Parse(string s, System.IFormatProvider? provider) => throw new NotImplementedException();
    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, System.IFormatProvider? provider)
    {
      charsWritten = default;
      return true;
    }
    public static bool TryParse(System.ReadOnlySpan<char> s, System.Globalization.NumberStyles style, System.IFormatProvider? provider, out Fraction result) => throw new NotImplementedException();
    public static bool TryParse(string? s, System.Globalization.NumberStyles style, System.IFormatProvider? provider, out Fraction result)
    {
      throw new NotImplementedException();
    }
    public static bool TryParse(ReadOnlySpan<char> s, System.IFormatProvider? provider, out Fraction result) => throw new NotImplementedException();
    public static bool TryParse(string? s, System.IFormatProvider? provider, out Fraction result) => throw new NotImplementedException();
    static bool System.Numerics.INumberBase<Fraction>.TryConvertFromChecked<TOther>(TOther value, out Fraction result)
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
    static bool System.Numerics.INumberBase<Fraction>.TryConvertFromSaturating<TOther>(TOther value, out Fraction result)
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
    static bool System.Numerics.INumberBase<Fraction>.TryConvertFromTruncating<TOther>(TOther value, out Fraction result)
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
    static bool System.Numerics.INumberBase<Fraction>.TryConvertToChecked<TOther>(Fraction value, out TOther result)
    {
      result = default!;
      return false;
    }
    static bool System.Numerics.INumberBase<Fraction>.TryConvertToSaturating<TOther>(Fraction value, out TOther result)
    {
      result = default!;
      return false;
    }
    static bool System.Numerics.INumberBase<Fraction>.TryConvertToTruncating<TOther>(Fraction value, out TOther result)
    {
      result = default!;
      return false;
    }

    #endregion // System.Numerics.INumberBase<>

    // IComparable<>

    public int CompareTo(Fraction other)
      => (Sign(this) != Sign(other))
      ? (Sign(this) - Sign(other))
      : (m_denominator.Equals(other.m_denominator))
      ? m_numerator.CompareTo(other.m_numerator)
      : (m_numerator * other.m_denominator).CompareTo(m_denominator * other.m_numerator);

    // IComparable

    public int CompareTo(object? other) => other is Fraction o ? CompareTo(o) : -1;

    #region IConvertible
    public System.TypeCode GetTypeCode() => System.TypeCode.Object;
    public bool ToBoolean(System.IFormatProvider? provider) => Value != 0;
    public byte ToByte(System.IFormatProvider? provider) => System.Convert.ToByte(Value);
    public char ToChar(System.IFormatProvider? provider) => System.Convert.ToChar(Value);
    public System.DateTime ToDateTime(System.IFormatProvider? provider) => System.Convert.ToDateTime(Value);
    public decimal ToDecimal(System.IFormatProvider? provider) => System.Convert.ToDecimal(Value);
    public double ToDouble(System.IFormatProvider? provider) => System.Convert.ToDouble(Value);
    public short ToInt16(System.IFormatProvider? provider) => System.Convert.ToInt16(Value);
    public int ToInt32(System.IFormatProvider? provider) => System.Convert.ToInt32(Value);
    public long ToInt64(System.IFormatProvider? provider) => System.Convert.ToInt64(Value);
    [System.CLSCompliant(false)] public sbyte ToSByte(System.IFormatProvider? provider) => System.Convert.ToSByte(Value);
    public float ToSingle(System.IFormatProvider? provider) => System.Convert.ToSingle(Value);
    public string ToString(System.IFormatProvider? provider) => string.Format(provider, "{0}", Value);
    public object ToType(System.Type conversionType, System.IFormatProvider? provider) => System.Convert.ChangeType(Value, conversionType, provider);
    [System.CLSCompliant(false)] public ushort ToUInt16(System.IFormatProvider? provider) => System.Convert.ToUInt16(Value);
    [System.CLSCompliant(false)] public uint ToUInt32(System.IFormatProvider? provider) => System.Convert.ToUInt32(Value);
    [System.CLSCompliant(false)] public ulong ToUInt64(System.IFormatProvider? provider) => System.Convert.ToUInt64(Value);
    #endregion IConvertible

    // IQuantifiable<>

    public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false)
    {
      var sb = new System.Text.StringBuilder();

      sb.Append(GetType().Name);
      sb.Append(" { ");

      sb.Append(ToProperString());

      var mixedString = ToImproperString();
      if (!sb.EndsWith(mixedString))
      {
        sb.Append(" = ");
        sb.Append(mixedString);
      }

      sb.Append(" = ");
      sb.Append(ToQuotient());
      sb.Append(" } ");

      return sb.ToString();
    }

    public double Value => ToQuotient();

    #endregion Implemented interfaces

    #region Object overrides
    public override string ToString() => ToQuantityString();
    #endregion Object overrides
  }
}
