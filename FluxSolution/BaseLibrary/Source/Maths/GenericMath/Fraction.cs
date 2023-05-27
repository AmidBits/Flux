#if NET7_0_OR_GREATER
namespace Flux
{
  //** A rational number (commonly called a fraction) is a ratio
  //** between two integers.  For example (3/6) = (2/4) = (1/2)
  //**
  //** Arithmetic
  //** ----------
  //** a/b = c/d, iff ad = bc
  //** -(a/b)     == (-a)/b
  //** (a/b)^(-1) == b/a, if a != 0
  //**
  //** Reduction Algorithm
  //** ------------------------
  //** Euclid's algorithm is used to simplify the fraction.
  //** Calculating the greatest common divisor of two n-digit
  //** numbers can be found in
  //**
  //** O(n(log n)^5 (log log n)) steps as n -> +infinity

  /// <summary>
  /// <para>Simple fraction, unit of rational number, i.e. in the form of numerator and denominator.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Fraction"/></para>
  /// <para><seealso href="https://github.com/kiprobinson/BigFraction"/></para>
  /// <para><seealso href="https://github.com/bazzilic/BigFraction"/></para>
  /// </summary>
  public readonly record struct Fraction
    : System.IComparable, System.IComparable<Fraction>
    , System.IConvertible
    , System.Numerics.IAdditiveIdentity<Fraction, Fraction>
    , System.Numerics.IAdditionOperators<Fraction, Fraction, Fraction>, System.Numerics.IAdditionOperators<Fraction, System.Numerics.BigInteger, Fraction>
    , System.Numerics.IComparisonOperators<Fraction, Fraction, bool>
    , System.Numerics.IDecrementOperators<Fraction>
    , System.Numerics.IDivisionOperators<Fraction, Fraction, Fraction>, System.Numerics.IDivisionOperators<Fraction, System.Numerics.BigInteger, Fraction>
    , System.Numerics.IEqualityOperators<Fraction, Fraction, bool>
    , System.Numerics.IFloatingPointConstants<Fraction>
    , System.Numerics.IIncrementOperators<Fraction>
    , System.Numerics.IModulusOperators<Fraction, Fraction, Fraction>, System.Numerics.IModulusOperators<Fraction, System.Numerics.BigInteger, Fraction>
    , System.Numerics.IMultiplicativeIdentity<Fraction, Fraction>
    , System.Numerics.IMultiplyOperators<Fraction, Fraction, Fraction>, System.Numerics.IMultiplyOperators<Fraction, System.Numerics.BigInteger, Fraction>
    , System.Numerics.INumber<Fraction>
    , System.Numerics.INumberBase<Fraction>
    , System.Numerics.IPowerFunctions<Fraction>
    , System.Numerics.IRootFunctions<Fraction>
    , System.Numerics.ISignedNumber<Fraction>
    , System.Numerics.ISubtractionOperators<Fraction, Fraction, Fraction>, System.Numerics.ISubtractionOperators<Fraction, System.Numerics.BigInteger, Fraction>
    , System.Numerics.IUnaryNegationOperators<Fraction, Fraction>
    , System.Numerics.IUnaryPlusOperators<Fraction, Fraction>
    , IQuantifiable<double>
  {
    public static readonly Fraction EpsilonLikeSingle = new(1, 1_000_000);
    public static readonly Fraction EpsilonLikeDouble = new(1, 1_000_000_000_000_000);

    /// <summary>Represents an approximate fraction of the irrational Golden Ratio.</summary>
    public static readonly Fraction GoldenRatio = new(7540113804746346429L, 4660046610375530309L, false);

    private readonly System.Numerics.BigInteger m_numerator; // Can be negative and zero.
    private readonly System.Numerics.BigInteger m_denominator; // Cannot be negative or zero.

    /// <summary>Creates a new simple fraction from the specified numerator and denominator. Optionally the fraction can be reduced, if possible.</summary>
    /// <param name="numerator"></param>
    /// <param name="denominator"></param>
    /// <param name="reduceIfPossible">If true, reduce if possible, and if false, do not attempt to reduce.</param>
    public Fraction(System.Numerics.BigInteger numerator, System.Numerics.BigInteger denominator, bool reduceIfPossible)
    {
      if (denominator.IsZero) throw new System.DivideByZeroException();

      if (denominator.Sign < 0) // Ensure denominator remain positivity.
      {
        numerator = -numerator;
        denominator = -denominator;
      }

      //if (reduceIfPossible && IsReducible(numerator, denominator, out var gcd))
      //{
      //  m_numerator = numerator / gcd;
      //  m_denominator = denominator / gcd;
      //}
      //else
      //{
      //  m_numerator = numerator;
      //  m_denominator = denominator;
      //}

      if (reduceIfPossible)
        TryReduce(numerator, denominator, out numerator, out denominator);

      m_numerator = numerator;
      m_denominator = denominator;
    }
    /// <summary>Creates a new simple fraction from the specified numerator and denominator. If the fraction can be reduced, it will be.</summary>
    /// <param name="numerator"></param>
    /// <param name="denominator"></param>
    public Fraction(System.Numerics.BigInteger numerator, System.Numerics.BigInteger denominator) : this(numerator, denominator, true) { }
    public Fraction(System.Numerics.BigInteger value) : this(value, System.Numerics.BigInteger.One, false) { }
    public Fraction(System.Numerics.BigInteger whole, System.Numerics.BigInteger numerator, System.Numerics.BigInteger denominator) : this(whole * denominator + numerator, denominator, true) { }

    public void Deconstruct(out System.Numerics.BigInteger numerator, out System.Numerics.BigInteger denominator) { numerator = m_numerator; denominator = m_denominator; }

    /// <summary>The numerator of the fraction. This carries the sign and can also be zero.</summary>
    public System.Numerics.BigInteger Numerator => m_numerator;
    /// <summary>The denominator of the fraction. It is always positive and non-zero.</summary>
    public System.Numerics.BigInteger Denominator => m_denominator;

    public System.Numerics.BigInteger GetWholePart() => System.Numerics.BigInteger.Divide(m_numerator, m_denominator);

    public Fraction GetFractionPart() => new(System.Numerics.BigInteger.Remainder(m_numerator, m_denominator), m_denominator);

    ///// <summary>A fraction is proper if its absolute value is strictly less than 1, i.e. if it is greater than -1 and less than 1.</summary>
    //public bool IsProper => System.Numerics.BigInteger.Abs(m_numerator) < m_denominator;

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

    #region Static methods

    /// <summary>Calculates rational approximations to a given real number.</summary>
    /// <param name="x"></param>
    /// <param name="maxIterations"></param>
    /// <returns></returns>
    /// <see href="https://stackoverflow.com/questions/12098461/how-can-i-detect-if-a-float-has-a-repeating-decimal-expansion-in-c/12101996#12101996"/>
    public static Fraction ApproximateRational(double x, int maxIterations = 101)
    {
      if (x == 0) return Zero;

      var Am = (Item1: System.Numerics.BigInteger.Zero, Item2: System.Numerics.BigInteger.One);
      var Bm = (Item1: System.Numerics.BigInteger.One, Item2: System.Numerics.BigInteger.Zero);

      System.Numerics.BigInteger A;
      System.Numerics.BigInteger B;

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

    public static Fraction CreateChecked<TOther>(TOther o)
      where TOther : System.Numerics.INumberBase<TOther>
    {
      if (o.ImplementsBinaryInteger())
        return new(System.Numerics.BigInteger.CreateChecked(o));
      else if (o.ImplementsFloatingPoint())
        return ApproximateRational(double.CreateChecked(o));
      else if (o is Fraction f)
        return new(f.Numerator, f.Denominator);

      throw new System.NotSupportedException();
    }
    public static Fraction CreateSaturating<TOther>(TOther o)
      where TOther : System.Numerics.INumberBase<TOther>
    {
      if (o.ImplementsBinaryInteger())
        return new(System.Numerics.BigInteger.CreateSaturating(o));
      else if (o.ImplementsFloatingPoint())
        return ApproximateRational(double.CreateSaturating(o));
      else if (o is Fraction f)
        return new(f.Numerator, f.Denominator);

      throw new System.NotSupportedException();
    }
    public static Fraction CreateTruncating<TOther>(TOther o)
      where TOther : System.Numerics.INumberBase<TOther>
    {
      if (o.ImplementsBinaryInteger())
        return new(System.Numerics.BigInteger.CreateTruncating(o));
      else if (o.ImplementsFloatingPoint())
        return ApproximateRational(double.CreateTruncating(o));
      else if (o is Fraction f)
        return new(f.Numerator, f.Denominator);

      throw new System.NotSupportedException();
    }

    public static char GetSymbolFractionSlash(bool preferUnicode = true) => preferUnicode ? '\u2044' : '\u002F';

    /// <summary>Returns the greatest common divisor (GCD) of two values.</summary>
    /// <remarks>The result is guaranteed to be a reduced fraction. If you try to further simplify this to: (gcd(a,c) * gcd(b,d)) / (|b*d|), then the result will not be reduced, and the operation actually takes about 60% longer.</remarks>
    /// <example>gcd((a/b),(c/d)) = gcd(a,c) / lcm(b,d) = gcd(a,c) / (|b*d|/gcd(b,d))</example>
    public static Fraction GreatestCommonDivisor(Fraction a, Fraction b)
      => IsZero(a)
      ? Abs(b)
      : IsZero(b)
      ? Abs(a)
      : new(
          System.Numerics.BigInteger.GreatestCommonDivisor(a.m_numerator, b.m_numerator),
          System.Numerics.BigInteger.Abs(a.m_denominator * b.m_denominator) / System.Numerics.BigInteger.GreatestCommonDivisor(a.m_denominator, b.m_denominator),
          true
        );

    /// <summary>Determines whether the <paramref name="numerator"/> / <paramref name="denominator"/> represents a mixed fraction.</summary>
    public static bool IsMixedFraction(System.Numerics.BigInteger numerator, System.Numerics.BigInteger denominator)
      => denominator > System.Numerics.BigInteger.One && System.Numerics.BigInteger.Abs(numerator) > denominator;

    /// <summary>Determines whether the <paramref name="numerator"/> / <paramref name="denominator"/> represents a proper fraction.</summary>
    /// <remarks>A fraction is proper if its absolute value is strictly less than 1, i.e. if it is greater than -1 and less than 1.</remarks>
    public static bool IsProperFraction(System.Numerics.BigInteger numerator, System.Numerics.BigInteger denominator)
      => System.Numerics.BigInteger.Abs(numerator) < denominator;

    /// <summary>Determines whether the <paramref name="numerator"/> / <paramref name="denominator"/> represents a whole numbered fraction.</summary>
    public static bool IsWholeFraction(System.Numerics.BigInteger numerator, System.Numerics.BigInteger denominator)
      => denominator == System.Numerics.BigInteger.One || numerator == denominator;

    /// <summary>Determines whether the <paramref name="numerator"/> / <paramref name="denominator"/> represents a reducible fraction, and if so, returns the <paramref name="greatestCommonDenominator"/> (Greatest Common Denominator) as an out parameter.</summary>
    public static bool IsReducibleFraction(System.Numerics.BigInteger numerator, System.Numerics.BigInteger denominator, out System.Numerics.BigInteger greatestCommonDenominator)
      => (greatestCommonDenominator = System.Numerics.BigInteger.GreatestCommonDivisor(numerator, denominator)) > System.Numerics.BigInteger.One;

    /// <summary>Determines whether the <paramref name="numerator"/> / <paramref name="denominator"/> represents a unit fraction.</summary>
    /// <remarks>A fraction is a unit fraction if its <paramref name="numerator"/> is equal to 1.</remarks>
    public static bool IsUnitFraction(System.Numerics.BigInteger numerator, System.Numerics.BigInteger denominator)
      => System.Numerics.BigInteger.IsNegative(denominator) || denominator.IsZero ? throw new System.ArgumentOutOfRangeException(nameof(denominator), "Must be a positive natural number.") : numerator == System.Numerics.BigInteger.One;

    /// <summary>Returns the least common multiple (LCM) of <paramref name="a"/> and <paramref name="b"/>.</summary>
    /// <remarks>The result is guaranteed to be a reduced fraction. If you try to further simplify this to: |a*c| / (gcd(a,c) * gcd(b,d)), then the result will not be reduced, and the operation actually takes about 60% longer.</remarks>
    /// <example>lcm((a/b),(c/d)) = lcm(a,c) / gcd(b,d) = (|a*c| / gcd(a,c)) / gcd(b,d)</example>
    public static Fraction LeastCommonMultiple(Fraction a, Fraction b)
      => (IsZero(a) || IsZero(b))
      ? Zero
      : new(
          System.Numerics.BigInteger.Abs(a.m_numerator * b.m_numerator) / System.Numerics.BigInteger.GreatestCommonDivisor(a.m_numerator, b.m_numerator),
          System.Numerics.BigInteger.GreatestCommonDivisor(a.m_denominator, b.m_denominator),
          true
        );

    /// <summary>Returns the mediant of <paramref name="a"/> and <paramref name="b"/>.</summary>
    public static Fraction Mediant(Fraction a, Fraction b)
      => new(a.m_numerator + b.m_numerator, a.m_denominator + b.m_denominator, false);

    /// <summary>Returns the nth root of <paramref name="value"/>.</summary>
    private static Fraction NthRoot(Fraction value, int nth, Fraction maxError)
    {
      if (nth < 0)
        return NthRoot(Reciprocal(value), -nth, maxError);

      if (nth == 0) throw new System.DivideByZeroException("Zeroth root is not defined.");
      if (nth == int.MinValue) throw new System.OverflowException("Value cannot be negated.");


      if (IsNegative(value))
      {
        if ((nth & 1) == 0) throw new System.ArithmeticException("Cannot compute even root of a negative number.");

        return -NthRoot(-value, nth, maxError);
      }

      if (IsNegative(maxError) || IsZero(maxError)) throw new System.ArgumentOutOfRangeException(nameof(maxError), "Epsilon must be positive");

      if (value == Zero)
        return Zero;
      if (nth == 1)
        return value;
      if (value == One)
        return value;

      // First, get the closest integer to the root of the numerator and the denominator as an initial guess.

      var guessNumerator = IntegerNthRoot(value.Numerator, nth);
      var guessDenominator = IntegerNthRoot(value.Denominator, nth);

      var initialGuess = new Fraction(guessNumerator.value, guessDenominator.value);

      // If we got exact roots for numerator and denominator, then we know the guess is exact.

      if (guessNumerator.isExact && guessDenominator.isExact)
        return initialGuess;

      // Otherwise we use the implementation of nth-root algorithm: https://en.wikipedia.org/wiki/Nth_root_algorithm

      var x = initialGuess;

      while (true)
      {
        var diff = (value / Pow(x, nth - 1) - x) / System.Numerics.BigInteger.CreateChecked(nth);

        x += diff;

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
    private static Fraction NthRoot(Fraction value, System.Numerics.BigInteger nth, Fraction maxError)
      => nth >= int.MinValue && nth <= int.MaxValue
      ? NthRoot(value, int.CreateChecked(nth), maxError)
      : throw new System.ArgumentOutOfRangeException(nameof(nth));

    /// <summary>Returns this^exponent. Note: 0^0 will return 1/1.</summary>
    public static Fraction Pow(Fraction value, int exponent)
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
    private static Fraction Pow(Fraction value, System.Numerics.BigInteger exponent)
      => exponent >= int.MinValue && exponent <= int.MaxValue
      ? Pow(value, int.CreateChecked(exponent))
      : throw new System.ArgumentOutOfRangeException(nameof(exponent));

    /// <summary>Returns the reciprocal of <paramref name="value"/>.</summary>
    public static Fraction Reciprocal(Fraction value)
      => IsZero(value)
      ? throw new System.DivideByZeroException(@"Reciprocal of zero.")
      : new(value.m_denominator, value.m_numerator, true);

    public static bool TryGetMixedParts(System.Numerics.BigInteger numerator, System.Numerics.BigInteger denominator, out System.Numerics.BigInteger whole, out System.Numerics.BigInteger properNumerator, out System.Numerics.BigInteger properDenominator)
    {
      try
      {
        if (IsMixedFraction(numerator, denominator))
        {
          (whole, properNumerator) = System.Numerics.BigInteger.DivRem(numerator, denominator);

          properDenominator = denominator;

          return true;
        }
      }
      catch { }

      whole = properNumerator = properDenominator = System.Numerics.BigInteger.Zero;

      return false;
    }

    public static bool TryReduce(System.Numerics.BigInteger numerator, System.Numerics.BigInteger denominator, out System.Numerics.BigInteger reducedNumerator, out System.Numerics.BigInteger reducedDenominator)
    {
      if (IsReducibleFraction(numerator, denominator, out var greatestCommonDenominator))
      {
        reducedNumerator = numerator / greatestCommonDenominator;
        reducedDenominator = denominator / greatestCommonDenominator;

        return true;
      }

      reducedNumerator = numerator;
      reducedDenominator = denominator;

      return false;
    }

    #endregion Static methods

    #region Overloaded operators

    public static explicit operator double(Fraction v) => v.Value;

    #endregion Overloaded operators

    string System.IFormattable.ToString(string? format, System.IFormatProvider? provider) => ToQuantityString();

    #region Implemented interfaces

    // System.Numerics.IAdditionOperators<>
    // a/b + c/d == (ad + bc)/bd
    public static Fraction operator +(Fraction a, Fraction b) => new(a.m_numerator * b.m_denominator + a.m_denominator * b.m_numerator, a.m_denominator * b.m_denominator, true);
    public static Fraction operator +(Fraction a, System.Numerics.BigInteger b) => new(a.m_numerator + a.m_denominator * b, a.m_denominator, true);

    // System.Numerics.IAdditiveIdentity<>
    public static Fraction AdditiveIdentity => Zero;

    // System.Numerics.IComparisonOperators<>
    public static bool operator <(Fraction a, Fraction b) => a.CompareTo(b) < 0;
    public static bool operator <=(Fraction a, Fraction b) => a.CompareTo(b) <= 0;
    public static bool operator >(Fraction a, Fraction b) => a.CompareTo(b) > 0;
    public static bool operator >=(Fraction a, Fraction b) => a.CompareTo(b) >= 0;

    // System.Numerics.IDecrementOperators<>
    public static Fraction operator --(Fraction value) => value - System.Numerics.BigInteger.One;

    // System.Numerics.IDivisionOperators<>
    // a/b / c/d == (ad)/(bc)
    public static Fraction operator /(Fraction a, Fraction b) => new(a.m_numerator * b.m_denominator, a.m_denominator * b.m_numerator, true);
    public static Fraction operator /(Fraction a, System.Numerics.BigInteger b) => new(a.m_numerator, a.m_denominator * b, true);

    // System.Numerics.IFloatingPointConstants<>
    public static Fraction E => new(System.Numerics.BigInteger.Parse("611070150698522592097"), System.Numerics.BigInteger.Parse("224800145555521536000"), false);
    public static Fraction Pi => new(System.Numerics.BigInteger.Parse("2646693125139304345"), System.Numerics.BigInteger.Parse("842468587426513207"), false);
    public static Fraction Tau => Pi.Multiply(2);

    // System.Numerics.IIncrementOperators<>
    public static Fraction operator ++(Fraction value) => value + System.Numerics.BigInteger.One;

    // System.Numerics.IModulusOperators<>
    // a/b % c/d == (ad % bc)/bd
    public static Fraction operator %(Fraction a, Fraction b) => new((a.m_numerator * b.m_denominator) % (a.m_denominator * b.m_numerator), a.m_denominator * b.m_denominator, true);
    public static Fraction operator %(Fraction a, System.Numerics.BigInteger b) => new(a.m_numerator % (a.m_denominator * b), a.m_denominator, true);

    // System.Numerics.IMultiplicativeIdentity<>
    public static Fraction MultiplicativeIdentity => One;

    // System.Numerics.IMultiplyOperators<> // a/b * c/d == (ac)/(bd)
    public static Fraction operator *(Fraction a, Fraction b) => new(a.m_numerator * b.m_numerator, a.m_denominator * b.m_denominator, true);
    public static Fraction operator *(Fraction a, System.Numerics.BigInteger b) => new(a.m_numerator * b, a.m_denominator, true);

    // System.Numerics.INumber<>
    public static Fraction Clamp(Fraction value, Fraction min, Fraction max) => value < min ? min : value > max ? max : value;
    public static Fraction CopySign(Fraction a, Fraction b) => a * Sign(b);
    public static Fraction Max(Fraction a, Fraction b) => a >= b ? a : b;
    public static Fraction MaxNumber(Fraction a, Fraction b) => Max(a, b);
    public static Fraction Min(Fraction a, Fraction b) => a <= b ? a : b;
    public static Fraction MinNumber(Fraction a, Fraction b) => Min(a, b);
    public static int Sign(Fraction value) => value.m_numerator.Sign;

    #region System.Numerics.INumberBase<>

    public static Fraction One => new(System.Numerics.BigInteger.One);
    public static int Radix => 2;
    public static Fraction Zero => new(System.Numerics.BigInteger.Zero);
    public static Fraction Abs(Fraction value) => new(System.Numerics.BigInteger.Abs(value.m_numerator), System.Numerics.BigInteger.Abs(value.m_denominator));
    public static bool IsCanonical(Fraction f) => true;
    public static bool IsComplexNumber(Fraction f) => false;
    public static bool IsEvenInteger(Fraction f) => f.m_numerator.IsEven && IsInteger(f); // When the numerator is even and the fraction is an integer.
    public static bool IsFinite(Fraction f) => true;
    public static bool IsImaginaryNumber(Fraction f) => false;
    public static bool IsInfinity(Fraction f) => false;
    public static bool IsInteger(Fraction f) => IsWholeFraction(f.Numerator, f.Denominator); // In order to be an integer, the denominator must be one.
    public static bool IsNaN(Fraction f) => false;
    public static bool IsNegative(Fraction f) => System.Numerics.BigInteger.IsNegative(f.m_numerator); // The denominator will not be negative, see constructor.
    public static bool IsNegativeInfinity(Fraction f) => false;
    public static bool IsNormal(Fraction f) => !IsZero(f);
    public static bool IsOddInteger(Fraction f) => !f.m_numerator.IsEven && IsInteger(f); // When the numerator is odd and the fraction is an integer.
    public static bool IsPositive(Fraction f) => System.Numerics.BigInteger.IsPositive(f.m_numerator); // If the rational is negative it would only be via the numerator, see constructor.
    public static bool IsPositiveInfinity(Fraction f) => false;
    public static bool IsRealNumber(Fraction f) => !IsNaN(f);
    public static bool IsSubnormal(Fraction f) => false;
    public static bool IsZero(Fraction f) => f.m_numerator.IsZero; // When the numerator is zero (a zero denominator results in divide-by-zero exception).
    public static Fraction MaxMagnitude(Fraction a, Fraction b) => a >= b ? a : b;
    public static Fraction MaxMagnitudeNumber(Fraction a, Fraction b) => a >= b ? a : b;
    public static Fraction MinMagnitude(Fraction a, Fraction b) => a <= b ? a : b;
    public static Fraction MinMagnitudeNumber(Fraction a, Fraction b) => a <= b ? a : b;
    public static Fraction Parse(ReadOnlySpan<char> s, System.Globalization.NumberStyles style, IFormatProvider? provider) => throw new NotImplementedException();
    public static Fraction Parse(string s, System.Globalization.NumberStyles style, IFormatProvider? provider) => throw new NotImplementedException();
    public static Fraction Parse(System.ReadOnlySpan<char> s, System.IFormatProvider? provider) => throw new NotImplementedException();
    public static Fraction Parse(string s, System.IFormatProvider? provider) => throw new NotImplementedException();
    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, System.IFormatProvider? provider)
    {
      charsWritten = default;
      return true;
    }
    public static bool TryParse(System.ReadOnlySpan<char> s, System.Globalization.NumberStyles style, System.IFormatProvider? provider, out Fraction result) => throw new NotImplementedException();
    public static bool TryParse(string? s, System.Globalization.NumberStyles style, System.IFormatProvider? provider, out Fraction result) => throw new NotImplementedException();
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

    // System.Numerics.IPowerFunctions<>
    public static Fraction Pow(Fraction x, Fraction y) => Pow(NthRoot(x, y.m_denominator, EpsilonLikeDouble), y.m_numerator); // is var abspow && IsNegative(exponent) ? One / abspow : abspow;

    // System.Numerics.IRootFunctions<>
    public static Fraction Cbrt(Fraction x) => NthRoot(x, 3, EpsilonLikeDouble);
    public static Fraction Hypot(Fraction x, Fraction y) => Sqrt(x * x + y * y);
    public static Fraction RootN(Fraction x, int n) => NthRoot(x, n, EpsilonLikeDouble);
    public static Fraction Sqrt(Fraction x) => NthRoot(x, 2, EpsilonLikeDouble);

    // System.Numerics.ISignedNumber<>
    public static Fraction NegativeOne => new(System.Numerics.BigInteger.MinusOne);

    // System.Numerics.ISubtractionOperators<>
    // a/b - c/d == (ad - bc)/bd
    public static Fraction operator -(Fraction a, Fraction b) => new(a.m_numerator * b.m_denominator - a.m_denominator * b.m_numerator, a.m_denominator * b.m_denominator, true);
    public static Fraction operator -(Fraction a, System.Numerics.BigInteger b) => new(a.m_numerator - a.m_denominator * b, a.m_denominator, true);

    // System.Numerics.IUnaryNegationOperators<>
    public static Fraction operator -(Fraction value) => new(-value.m_numerator, value.m_denominator, false);

    // System.Numerics.IUnaryPlusOperators<>
    public static Fraction operator +(Fraction value) => value;

    // IComparable<>
    public int CompareTo(Fraction other)
      => Sign(this) is var signthis && Sign(other) is var signother && signthis != signother
      ? signthis - signother
      : m_denominator.Equals(other.m_denominator)
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
    public string ToString(System.IFormatProvider? provider) => string.Format(provider, "{0}\u2044{1}", Value);
    public object ToType(System.Type conversionType, System.IFormatProvider? provider) => System.Convert.ChangeType(Value, conversionType, provider);
    [System.CLSCompliant(false)] public ushort ToUInt16(System.IFormatProvider? provider) => System.Convert.ToUInt16(Value);
    [System.CLSCompliant(false)] public uint ToUInt32(System.IFormatProvider? provider) => System.Convert.ToUInt32(Value);
    [System.CLSCompliant(false)] public ulong ToUInt64(System.IFormatProvider? provider) => System.Convert.ToUInt64(Value);
    #endregion IConvertible

    // IQuantifiable<>
    public string ToQuantityString(string? format = null, bool preferUnicode = false, bool useFullName = false)
      => IsProperFraction(m_numerator, m_denominator)
      ? $"{m_numerator}\u2044{m_denominator}"
      : TryGetMixedParts(m_numerator, m_denominator, out var whole, out var properNumerator, out var properDenominator)
      ? $"{whole} {properNumerator}\u2044{properDenominator}"
      : m_numerator.ToString(); // It is a whole number and we return a simple integer string.

    public double Value => double.CreateChecked(m_numerator) / double.CreateChecked(m_denominator);

    #endregion Implemented interfaces

    public override string ToString() => ToQuantityString();
  }
}
#endif
