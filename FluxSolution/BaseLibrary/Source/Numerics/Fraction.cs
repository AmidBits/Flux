namespace Flux
{
  /// <summary>Simple fraction, unit of rational number, i.e. in the form of numerator and denominator.</summary>
  /// <see cref="https://en.wikipedia.org/wiki/Fraction#Simple,_common,_or_vulgar_fractions"/>
  public readonly record struct Fraction<TSelf>
    : System.IComparable, System.IComparable<Fraction<TSelf>>, System.IConvertible, Quantities.IQuantifiable<double>
    //, System.Numerics.IComparisonOperators<Fraction<TSelf>, Fraction<TSelf>, Fraction<TSelf>>,
    , System.Numerics.INumberBase<Fraction<TSelf>>
    where TSelf : System.Numerics.IBinaryInteger<TSelf>
  {
    public static readonly Fraction<TSelf> EpsilonLikeSingle = new(TSelf.One, TSelf.CreateChecked(1000000));
    public static readonly Fraction<TSelf> EpsilonLikeDouble = new(TSelf.One, TSelf.CreateChecked(1000000000000000));

    /// <summary>Represents a fraction of the Golden Ratio.</summary>
    public static readonly Fraction<TSelf> GoldenRatio = new(TSelf.CreateChecked(7540113804746346429L), TSelf.CreateChecked(4660046610375530309L), true);

    /// <summary>Represents a fraction of PI.</summary>
    public static Fraction<TSelf> Pi => new(TSelf.CreateChecked(2646693125139304345L), TSelf.CreateChecked(842468587426513207L), true);

    /// <summary>Represents a fraction of TAU (2 * PI).</summary>
    public static Fraction<TSelf> Tau => Pi.Multiply(2);

    private readonly TSelf m_numerator;
    private readonly TSelf m_denominator;

    /// <summary>Creates a new simple fraction from the specified numerator and denominator. Optionally the fraction can be reduced, if possible.</summary>
    /// <param name="numerator"></param>
    /// <param name="denominator"></param>
    /// <param name="reduceIfPossible">If true, reduce if possible, and if false, do not attempt to reduce.</param>
    public Fraction(TSelf numerator, TSelf denominator, bool reduceIfPossible)
    {
      if (TSelf.IsZero(denominator)) throw new System.DivideByZeroException();

      if (TSelf.Sign(denominator) < 0)
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
    public Fraction(TSelf whole, TSelf numerator, TSelf denominator)
      : this(whole * denominator + numerator, denominator, true)
    { }
    /// <summary>Creates a new simple fraction from the specified numerator and denominator. If the fraction can be reduced, it will be.</summary>
    /// <param name="numerator"></param>
    /// <param name="denominator"></param>
    public Fraction(TSelf numerator, TSelf denominator)
      : this(numerator, denominator, true)
    { }
    public Fraction(TSelf value)
      : this(value, TSelf.One, false)
    { }

    public TSelf Numerator
      => m_numerator;
    public TSelf Denominator
      => m_denominator;

    /// <summary>A fraction is proper if its absolute value is strictly less than 1, i.e. if it is greater than -1 and less than 1.</summary>
    public bool IsProper
      => m_numerator < m_denominator;

    /// <summary>A fraction is a unit fraction if its numerator is equal to 1.</summary>
    public bool IsUnitFraction
      => m_numerator == TSelf.One;

    ///// <summary>Indicates whether the number is 0.</summary>
    //public bool IsZero => TSelf.IsZero(m_numerator) && m_denominator == TSelf.One;

    /// <summary>Indicates the sign of the number, i.e. 1, 0 or -1.</summary>
    //public int Sign
    //  => TSelf.Sign(m_numerator) is var ns && ns == 0
    //  ? 0
    //  : TSelf.Sign(m_denominator) is var ds && (ns < 0 && ds > 0) || (ds < 0 && ns >= 0)
    //  ? -1
    //  : 1;

    /// <summary>Returns the integer quotient and an out variable containing the remainder.</summary>
    public TSelf ToDivRem(out TSelf remainder)
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

        if (remainder > TSelf.Zero)
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

    /// <summary>Compute the square root of the specified value.</summary>
    public static Fraction<TSelf> Cbrt(Fraction<TSelf> value)
      => NthRoot(value, 3, EpsilonLikeDouble);

    /// <summary>Returns the value with the sign of the second argument.</summary>
    public static Fraction<TSelf> CopySign(Fraction<TSelf> value, Fraction<TSelf> sign)
      => (Sign(sign) == 0 || IsZero(value))
      ? Zero
      : (TSelf.Sign(value.m_numerator) < 0 && Sign(sign) > 0) || (TSelf.Sign(value.m_numerator) > 0 && Sign(sign) < 0)
      ? new(-value.m_numerator, value.m_denominator, true)
      : value;

    public static Fraction<TSelf> CreateChecked<TOther>(TOther o)
      where TOther : System.Numerics.INumberBase<TOther>
    {
      if (o.IsIBinaryInteger())
      {
        var v = TSelf.CreateChecked(o);
        return new(v, v);
      }
      else if (o is Fraction<TSelf> f)
        return new(f.Numerator, f.Denominator);

      throw new System.NotSupportedException();
    }
    public static Fraction<TSelf> CreateSaturating<TOther>(TOther o)
      where TOther : System.Numerics.INumberBase<TOther>
    {
      if (o.IsIBinaryInteger())
      {
        var v = TSelf.CreateSaturating(o);
        return new(v, v);
      }
      else if (o is Fraction<TSelf> f)
        return new(f.Numerator, f.Denominator);

      throw new System.NotSupportedException();
    }
    public static Fraction<TSelf> CreateTruncating<TOther>(TOther o)
      where TOther : System.Numerics.INumberBase<TOther>
    {
      if (o.IsIBinaryInteger())
      {
        var v = TSelf.CreateTruncating(o);
        return new(v, v);
      }
      else if (o is Fraction<TSelf> f)
        return new(f.Numerator, f.Denominator);

      throw new System.NotSupportedException();
    }

    /// <summary>Returns the greatest common divisor (GCD) of two values.</summary>
    /// <remarks>The result is guaranteed to be a reduced fraction. If you try to further simplify this to: (gcd(a,c) * gcd(b,d)) / (|b*d|), then the result will not be reduced, and the operation actually takes about 60% longer.</remarks>
    /// <example>gcd((a/b),(c/d)) = gcd(a,c) / lcm(b,d) = gcd(a,c) / (|b*d|/gcd(b,d))</example>
    public static Fraction<TSelf> GreatestCommonDivisor(Fraction<TSelf> a, Fraction<TSelf> b)
      => IsZero(a)
      ? Abs(b)
      : IsZero(b)
      ? Abs(a)
      : new(
          GenericMath.GreatestCommonDivisor(a.m_numerator, b.m_numerator),
          TSelf.Abs(a.m_denominator * b.m_denominator) / GenericMath.GreatestCommonDivisor(a.m_denominator, b.m_denominator),
          true
        );

    /// <summary>Returns the least common multiple (LCM) of two values.</summary>
    /// <remarks>The result is guaranteed to be a reduced fraction. If you try to further simplify this to: |a*c| / (gcd(a,c) * gcd(b,d)), then the result will not be reduced, and the operation actually takes about 60% longer.</remarks>
    /// <example>lcm((a/b),(c/d)) = lcm(a,c) / gcd(b,d) = (|a*c| / gcd(a,c)) / gcd(b,d)</example>
    public static Fraction<TSelf> LeastCommonMultiple(Fraction<TSelf> a, Fraction<TSelf> b)
      => (IsZero(a) || IsZero(b))
      ? Zero
      : new(
          TSelf.Abs(a.m_numerator * b.m_numerator) / GenericMath.GreatestCommonDivisor(a.m_numerator, b.m_numerator),
          GenericMath.GreatestCommonDivisor(a.m_denominator, b.m_denominator),
          true
        );

    /// <summary>Returns the mediant of two values.</summary>
    public static Fraction<TSelf> Mediant(Fraction<TSelf> a, Fraction<TSelf> b)
      => new(a.m_numerator + b.m_numerator, a.m_denominator + b.m_denominator, false);

    /// <summary>Returns the nth root of the value.</summary>
    private static Fraction<TSelf> NthRoot(Fraction<TSelf> value, int n, Fraction<TSelf> maxError)
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

      var initialGuess = new Fraction<TSelf>(guessNumerator.value, guessDenominator.value);

      // If we got exact roots for numerator and denominator, then we know the guess is exact.

      if (guessNumerator.isExact && guessDenominator.isExact)
        return initialGuess;

      // Otherwise we use the implementation of nth-root algorithm: https://en.wikipedia.org/wiki/Nth_root_algorithm

      var x = initialGuess;

      while (true)
      {
        var diff = (value / Pow(x, n - 1) - x) / TSelf.CreateChecked(n);

        x += diff;

        if (Abs(diff).CompareTo(maxError) < 0)
          break;
      }

      return x;

      static (TSelf value, bool isExact) IntegerNthRoot(TSelf a, int n)
      {
        if (TSelf.IsZero(a))
          return (TSelf.Zero, true);

        if (n == 1 || a == TSelf.One)
          return (a, true);

        // Solve for x:  x^n = a
        // Start by computing a lower/upper bound on x.

        var lowerX = TSelf.One;
        var lowerPow = TSelf.One;

        var upperX = TSelf.CreateChecked(2);
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
    public static Fraction<TSelf> Pow(Fraction<TSelf> value, int exponent)
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
    public static Fraction<TSelf> Reciprocal(Fraction<TSelf> value)
      => IsZero(value)
      ? throw new System.DivideByZeroException(@"Reciprocal of zero.")
      : new(value.m_denominator, value.m_numerator, true);

    /// <summary>Indicates the sign of the number, i.e. 1, 0 or -1.</summary>
    public static int Sign(Fraction<TSelf> value)
      => TSelf.Sign(value.Numerator) is var ns && ns == 0
      ? 0
      : TSelf.Sign(value.Denominator) is var ds && (ns < 0 && ds > 0) || (ds < 0 && ns >= 0)
      ? -1
      : 1;

    /// <summary>Compute the square root of the specified value.</summary>
    public static Fraction<TSelf> Sqrt(Fraction<TSelf> value)
      => NthRoot(value, 2, EpsilonLikeDouble);

    #endregion Static methods

    #region Overloaded operators
    public static explicit operator double(Fraction<TSelf> v) => v.Value;

    public static bool operator <(Fraction<TSelf> a, Fraction<TSelf> b) => a.CompareTo(b) < 0;
    public static bool operator <=(Fraction<TSelf> a, Fraction<TSelf> b) => a.CompareTo(b) <= 0;
    public static bool operator >(Fraction<TSelf> a, Fraction<TSelf> b) => a.CompareTo(b) > 0;
    public static bool operator >=(Fraction<TSelf> a, Fraction<TSelf> b) => a.CompareTo(b) >= 0;

    //static Fraction<TSelf> System.Numerics.IComparisonOperators<Fraction<TSelf>, Fraction<TSelf>, Fraction<TSelf>>.operator <(Fraction<TSelf> a, Fraction<TSelf> b) => MinMagnitude(a, b);
    //static Fraction<TSelf> System.Numerics.IComparisonOperators<Fraction<TSelf>, Fraction<TSelf>, Fraction<TSelf>>.operator <=(Fraction<TSelf> a, Fraction<TSelf> b) => MinMagnitude(a, b);
    //static Fraction<TSelf> System.Numerics.IComparisonOperators<Fraction<TSelf>, Fraction<TSelf>, Fraction<TSelf>>.operator >(Fraction<TSelf> a, Fraction<TSelf> b) => MaxMagnitude(a, b);
    //static Fraction<TSelf> System.Numerics.IComparisonOperators<Fraction<TSelf>, Fraction<TSelf>, Fraction<TSelf>>.operator >=(Fraction<TSelf> a, Fraction<TSelf> b) => MaxMagnitude(a, b);

    public static Fraction<TSelf> operator --(Fraction<TSelf> f) => new(f.Numerator - TSelf.One, f.Denominator);
    public static Fraction<TSelf> operator ++(Fraction<TSelf> f) => new(f.Numerator + TSelf.One, f.Denominator);

    public static Fraction<TSelf> operator -(Fraction<TSelf> f) => new(f.m_numerator, -f.m_denominator, false);
    public static Fraction<TSelf> operator +(Fraction<TSelf> f) => new(f.m_numerator, f.m_denominator, false);

    public static Fraction<TSelf> operator +(Fraction<TSelf> a, Fraction<TSelf> b)
    {
      var lcm = GenericMath.LeastCommonMultiple(a.m_denominator, b.m_denominator);

      var an = lcm / a.m_denominator * a.m_numerator;
      var bn = lcm / b.m_denominator * b.m_numerator;

      return new(an + bn, lcm);
    }
    public static Fraction<TSelf> operator +(Fraction<TSelf> a, TSelf b)
      => a + new Fraction<TSelf>(b);

    public static Fraction<TSelf> operator /(Fraction<TSelf> a, Fraction<TSelf> b)
      => new(a.m_numerator * b.m_denominator, a.m_denominator * b.m_numerator);
    public static Fraction<TSelf> operator /(Fraction<TSelf> a, TSelf b)
      => a / new Fraction<TSelf>(b);

    public static Fraction<TSelf> operator *(Fraction<TSelf> a, Fraction<TSelf> b)
      => new(a.m_numerator * b.m_numerator, a.m_denominator * b.m_denominator);
    public static Fraction<TSelf> operator *(Fraction<TSelf> a, TSelf b)
      => a * new Fraction<TSelf>(b);

    public static Fraction<TSelf> operator %(Fraction<TSelf> a, TSelf b)
      => new(a.m_numerator % (a.m_denominator * b), a.m_denominator);

    public static Fraction<TSelf> operator -(Fraction<TSelf> a, Fraction<TSelf> b)
    {
      var lcm = GenericMath.LeastCommonMultiple(a.m_denominator, b.m_denominator);

      var an = lcm / a.m_denominator * a.m_numerator;
      var bn = lcm / b.m_denominator * b.m_numerator;

      return new(an - bn, lcm);
    }
    public static Fraction<TSelf> operator -(Fraction<TSelf> a, TSelf b)
      => a - new Fraction<TSelf>(b);

    #endregion Overloaded operators

    string System.IFormattable.ToString(string? format, System.IFormatProvider? provider) => ToQuantityString();

    #region Implemented interfaces

    #region System.Numerics.INumberBase<>

    public static Fraction<TSelf> AdditiveIdentity => Zero;
    public static Fraction<TSelf> MultiplicativeIdentity => One;
    public static Fraction<TSelf> One => new(TSelf.One, TSelf.One, false);
    public static int Radix => 2;
    public static Fraction<TSelf> Zero => new(TSelf.Zero, TSelf.One, false);
    public static Fraction<TSelf> Abs(Fraction<TSelf> value) => CopySign(value, One);
    public static bool IsCanonical(Fraction<TSelf> f) => false;
    public static bool IsComplexNumber(Fraction<TSelf> f) => false;
    public static bool IsEvenInteger(Fraction<TSelf> f) => IsInteger(f) && TSelf.IsEvenInteger(f.m_numerator);
    public static bool IsFinite(Fraction<TSelf> f) => false;
    public static bool IsImaginaryNumber(Fraction<TSelf> f) => false;
    public static bool IsInfinity(Fraction<TSelf> f) => false;
    public static bool IsInteger(Fraction<TSelf> f) => f.m_denominator == TSelf.One;
    public static bool IsNaN(Fraction<TSelf> f) => TSelf.IsNaN(f.m_numerator) || TSelf.IsNaN(f.m_denominator);
    public static bool IsNegative(Fraction<TSelf> f) => TSelf.IsNegative(f.m_numerator) || TSelf.IsNegative(f.m_denominator);
    public static bool IsNegativeInfinity(Fraction<TSelf> f) => TSelf.IsNegativeInfinity(f.m_numerator) || TSelf.IsNegativeInfinity(f.m_denominator);
    public static bool IsNormal(Fraction<TSelf> f) => false;
    public static bool IsOddInteger(Fraction<TSelf> f) => IsInteger(f) && TSelf.IsOddInteger(f.m_numerator);
    public static bool IsPositive(Fraction<TSelf> f) => TSelf.IsPositive(f.m_numerator) && TSelf.IsPositive(f.m_denominator);
    public static bool IsPositiveInfinity(Fraction<TSelf> f) => TSelf.IsPositiveInfinity(f.m_numerator) || TSelf.IsPositiveInfinity(f.m_denominator);
    public static bool IsRealNumber(Fraction<TSelf> f) => true;
    public static bool IsReducible(TSelf numerator, TSelf denominator, out TSelf gcd)
      => (gcd = GenericMath.GreatestCommonDivisor(numerator, denominator)) > TSelf.One;
    public static bool IsSubnormal(Fraction<TSelf> f) => false;
    public static bool IsZero(Fraction<TSelf> f) => TSelf.IsZero(f.m_numerator) && f.m_denominator == TSelf.One;
    public static Fraction<TSelf> MaxMagnitude(Fraction<TSelf> a, Fraction<TSelf> b) => a >= b ? a : b;
    public static Fraction<TSelf> MaxMagnitudeNumber(Fraction<TSelf> a, Fraction<TSelf> b) => a >= b ? a : b;
    public static Fraction<TSelf> MinMagnitude(Fraction<TSelf> a, Fraction<TSelf> b) => a <= b ? a : b;
    public static Fraction<TSelf> MinMagnitudeNumber(Fraction<TSelf> a, Fraction<TSelf> b) => a <= b ? a : b;
    public static Fraction<TSelf> Parse(ReadOnlySpan<char> s, System.Globalization.NumberStyles style, IFormatProvider? provider)
    {
      throw new NotImplementedException();
    }
    public static Fraction<TSelf> Parse(string s, System.Globalization.NumberStyles style, IFormatProvider? provider)
    {
      throw new NotImplementedException();
    }
    public static Fraction<TSelf> Parse(System.ReadOnlySpan<char> s, System.IFormatProvider? provider) => throw new NotImplementedException();
    public static Fraction<TSelf> Parse(string s, System.IFormatProvider? provider) => throw new NotImplementedException();
    public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, System.IFormatProvider? provider)
    {
      charsWritten = default;
      return true;
    }
    public static bool TryParse(System.ReadOnlySpan<char> s, System.Globalization.NumberStyles style, System.IFormatProvider? provider, out Fraction<TSelf> result) => throw new NotImplementedException();
    public static bool TryParse(string? s, System.Globalization.NumberStyles style, System.IFormatProvider? provider, out Fraction<TSelf> result)
    {
      throw new NotImplementedException();
    }
    public static bool TryParse(ReadOnlySpan<char> s, System.IFormatProvider? provider, out Fraction<TSelf> result) => throw new NotImplementedException();
    public static bool TryParse(string? s, System.IFormatProvider? provider, out Fraction<TSelf> result) => throw new NotImplementedException();
    static bool System.Numerics.INumberBase<Fraction<TSelf>>.TryConvertFromChecked<TOther>(TOther value, out Fraction<TSelf> result)
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
    static bool System.Numerics.INumberBase<Fraction<TSelf>>.TryConvertFromSaturating<TOther>(TOther value, out Fraction<TSelf> result)
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
    static bool System.Numerics.INumberBase<Fraction<TSelf>>.TryConvertFromTruncating<TOther>(TOther value, out Fraction<TSelf> result)
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
    static bool System.Numerics.INumberBase<Fraction<TSelf>>.TryConvertToChecked<TOther>(Fraction<TSelf> value, out TOther result)
    {
      result = default!;
      return false;
    }
    static bool System.Numerics.INumberBase<Fraction<TSelf>>.TryConvertToSaturating<TOther>(Fraction<TSelf> value, out TOther result)
    {
      result = default!;
      return false;
    }
    static bool System.Numerics.INumberBase<Fraction<TSelf>>.TryConvertToTruncating<TOther>(Fraction<TSelf> value, out TOther result)
    {
      result = default!;
      return false;
    }

    #endregion // System.Numerics.INumberBase<>

    // IComparable<>

    public int CompareTo(Fraction<TSelf> other)
      => (Sign(this) != Sign(other))
      ? (Sign(this) - Sign(other))
      : (m_denominator.Equals(other.m_denominator))
      ? m_numerator.CompareTo(other.m_numerator)
      : (m_numerator * other.m_denominator).CompareTo(m_denominator * other.m_numerator);

    // IComparable

    public int CompareTo(object? other) => other is Fraction<TSelf> o ? CompareTo(o) : -1;

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
