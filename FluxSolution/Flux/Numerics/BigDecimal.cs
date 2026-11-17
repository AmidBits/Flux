namespace Flux
{
  namespace Numerics
  {
    public readonly record struct BigDecimal
      : System.IComparable, System.IComparable<BigDecimal>
    //, System.IConvertible
      , System.IFormattable
      , System.Numerics.IAdditiveIdentity<BigDecimal, BigDecimal>
      , System.Numerics.IAdditionOperators<BigDecimal, BigDecimal, BigDecimal>, System.Numerics.IAdditionOperators<BigDecimal, System.Numerics.BigInteger, BigDecimal>
    //, System.Numerics.IBinaryNumber<BigDecimal>
    //, System.Numerics.IBitwiseOperators<BigDecimal, BigDecimal, BigDecimal>
      , System.Numerics.IComparisonOperators<BigDecimal, BigDecimal, bool>
      , System.Numerics.IDecrementOperators<BigDecimal>
      , System.Numerics.IDivisionOperators<BigDecimal, BigDecimal, BigDecimal>, System.Numerics.IDivisionOperators<BigDecimal, System.Numerics.BigInteger, BigDecimal>
      , System.Numerics.IEqualityOperators<BigDecimal, BigDecimal, bool>
    //, System.Numerics.IFloatingPoint<BigDecimal>
    //, System.Numerics.IFloatingPointConstants<BigDecimal>
      , System.Numerics.IIncrementOperators<BigDecimal>
    //, System.Numerics.ILogarithmicFunctions<BigDecimal>
      , System.Numerics.IModulusOperators<BigDecimal, BigDecimal, BigDecimal>, System.Numerics.IModulusOperators<BigDecimal, System.Numerics.BigInteger, BigDecimal>
      , System.Numerics.IMultiplicativeIdentity<BigDecimal, BigDecimal>
      , System.Numerics.IMultiplyOperators<BigDecimal, BigDecimal, BigDecimal>, System.Numerics.IMultiplyOperators<BigDecimal, System.Numerics.BigInteger, BigDecimal>
      , System.Numerics.INumber<BigDecimal>
      , System.Numerics.INumberBase<BigDecimal>
    //, System.Numerics.IPowerFunctions<BigDecimal>
    //, System.Numerics.IRootFunctions<BigDecimal>
      , System.Numerics.ISignedNumber<BigDecimal>
      , System.Numerics.ISubtractionOperators<BigDecimal, BigDecimal, BigDecimal>, System.Numerics.ISubtractionOperators<BigDecimal, System.Numerics.BigInteger, BigDecimal>
      , System.Numerics.IUnaryNegationOperators<BigDecimal, BigDecimal>
      , System.Numerics.IUnaryPlusOperators<BigDecimal, BigDecimal>
    //, Units.IValueQuantifiable<double>
    {
      public sealed record Context
      {
        public int FractionalDigits { get; init; } = 100;

        public NearestRoundingRule RoundingRule { get; init; } = NearestRoundingRule.ToEven;
      }

      public static Context DefaultContext { get; set; } = new();

      private const bool DefaultNormalization = true;

      private readonly System.Numerics.BigInteger m_coefficient;
      public System.Numerics.BigInteger Coefficient => m_coefficient;

      private readonly int m_scale;
      public int Scale => m_scale;

      public System.Numerics.Decimal128 Value => ToValue<System.Numerics.Decimal128>();

      //public decimal AsDecimal
      //{
      //  get
      //  {

      //    return double.CreateChecked(m_coefficient) / double.Pow(10, m_scale);
      //  }
      //}

      public BigDecimal(System.Numerics.BigInteger coefficient, int scale, bool normalize = DefaultNormalization)
        => (m_coefficient, m_scale) = normalize ? Normalize(coefficient, scale) : (coefficient, scale);

      public BigDecimal(System.Numerics.BigInteger coefficient) // Integer.
        => (m_coefficient, m_scale) = (coefficient, 0);

      public void Deconstruct(out System.Numerics.BigInteger coefficient, out int scale)
      {
        coefficient = m_coefficient;
        scale = m_scale;
      }

      public decimal AsDecimal => ToValue<decimal>(); // decimal.CreateChecked(m_coefficient * System.Numerics.BigInteger.Pow(10, m_scale)); // decimal does not have a Pow().
      public System.Numerics.Decimal128 AsDecimal128 => ToValue<System.Numerics.Decimal128>();
      public double AsDouble => ToValue<double>();

      [System.Obsolete("NEED TO REMOVE THE INTERMEDIARY CREATECHECKED(UINT128) WHEN DECIMALNNN CAN BE CREATECHECKED(BIGINTEGER) - JUST NOT THERE RIGHT NOW!!!")]
      public TFloat ToValue<TFloat>()
        where TFloat : System.Numerics.IFloatingPoint<TFloat>//, System.Numerics.IPowerFunctions<TFloat>
        // NEED TO REMOVE THE INTERMEDIARY CREATECHECKED(UINT128) WHEN DECIMALNNN CAN BE CREATECHECKED(BIGINTEGER) - JUST NOT THERE RIGHT NOW!!!
        => TFloat.CreateChecked(System.UInt128.CreateChecked(m_coefficient)) / TFloat.CreateChecked(System.UInt128.CreateChecked(System.Numerics.BigInteger.Pow(10, m_scale)));
      //=> TFloat.CreateChecked(m_coefficient) / TFloat.CreateChecked(System.Numerics.BigInteger.Pow(10, m_scale));

      public BigDecimal RoundToSignificantDigits(int precision, DirectedRoundingMode roundingMode)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(precision);

        if (m_coefficient.IsZero)
          return this;

        var digitCount = (int)BinaryInteger.DigitCount(System.Numerics.BigInteger.Abs(m_coefficient), 10);

        if (digitCount <= precision)
          return this;

        var digitsToRemove = digitCount - precision;

        var divisor = System.Numerics.BigInteger.Pow(10, digitsToRemove);

        var quotient = System.Numerics.BigInteger.DivRem(m_coefficient, divisor, out var remainder);

        if (!remainder.IsZero)
          quotient = RoundDirected(quotient, remainder, divisor, roundingMode);

        return new BigDecimal(quotient, checked(m_scale - digitsToRemove));
      }
      public BigDecimal RoundToSignificantDigits(int precision, NearestRoundingRule roundingRule)
      {
        System.ArgumentOutOfRangeException.ThrowIfNegativeOrZero(precision);

        if (m_coefficient.IsZero)
          return this;

        var digitCount = (int)BinaryInteger.DigitCount(System.Numerics.BigInteger.Abs(m_coefficient), 10);

        if (digitCount <= precision)
          return this;

        var digitsToRemove = digitCount - precision;

        var divisor = System.Numerics.BigInteger.Pow(10, digitsToRemove);

        var quotient = System.Numerics.BigInteger.DivRem(m_coefficient, divisor, out var remainder);

        if (!remainder.IsZero)
          quotient = RoundNearest(quotient, remainder, divisor, roundingRule);

        return new BigDecimal(quotient, checked(m_scale - digitsToRemove));
      }

      public static bool CanNormalize(System.Numerics.BigInteger coefficient, int scale) => scale > 0 && coefficient % 10 == 0;
      public static (System.Numerics.BigInteger coefficient, int scale) Normalize(System.Numerics.BigInteger coefficient, int scale)
      {
        if (coefficient.IsZero)
          return (System.Numerics.BigInteger.Zero, 0);

        while (scale > 0 && coefficient % 10 == 0)
        {
          coefficient /= 10;
          scale--;
        }

        return (coefficient, scale);
      }

      public static BigDecimal Ten => new(10, 0);
      public static BigDecimal NegativeOne => new(-1, 0);

      public static BigDecimal CreateFromBinaryN<TFloat>(TFloat value)
        where TFloat : System.Numerics.IBinaryFloatingPointIeee754<TFloat>
      {
        var (binarySign, binaryExponent, binaryFraction) = BinaryFloatingPointIeee754.ToBinary(value);

        var (_, unbiasedExponent, fullSignificand, _, _, _, _, _, _) = BinaryFloatingPointIeee754.AnalyzeBinary(binarySign, binaryExponent, binaryFraction, out TFloat _);

        var (_, _, fractionWidth) = BinaryFloatingPointIeee754.BinaryWidth<TFloat>();

        var coefficient = System.Numerics.BigInteger.CreateChecked(fullSignificand);
        var scale = 0;

        var effectiveExponent = (int)unbiasedExponent - fractionWidth;

        if (effectiveExponent >= 0)
        {
          coefficient <<= effectiveExponent;
        }
        else
        {
          var k = -effectiveExponent;

          coefficient *= System.Numerics.BigInteger.Pow(5, k);
          scale = k;
        }

        return new(coefficient, scale, true);
      }
      public static BigDecimal CreateFromDecimal(decimal value)
      {
        var (sign, scale, magnitude) = DecimalExtensions.ExtractDecimalFields(value);

        return new(sign * magnitude, scale);
      }
      public static BigDecimal CreateFromDecimalN<TDecimal>(TDecimal value)
        where TDecimal : System.Numerics.IDecimalFloatingPointIeee754<TDecimal>
      {
        var (_, exponent, coefficient) = DecimalFloatingPointIeee754.AnalyzeDecimal(value);

        var scale = -exponent;

        return new BigDecimal(coefficient, scale, true);
      }

      #region Implemented interfaces

      #region System.Numerics.IAdditiveIdentity<>

      public static BigDecimal AdditiveIdentity => Zero;

      #endregion

      #region System.Numerics.IAdditionOperators<>

      public static BigDecimal operator +(BigDecimal left, BigDecimal right)
      {
        if (left.m_coefficient.IsZero)
          return right;

        if (right.m_coefficient.IsZero)
          return left;

        if (left.m_scale == right.m_scale)
          return new(left.m_coefficient + right.m_coefficient, left.m_scale);

        if (left.m_scale > right.m_scale)
        {
          var exponent = checked(left.m_scale - right.m_scale);

          return new(left.m_coefficient + right.m_coefficient * System.Numerics.BigInteger.Pow(10, exponent), left.m_scale);
        }

        var otherExponent = checked(right.m_scale - left.m_scale);

        return new(left.m_coefficient * System.Numerics.BigInteger.Pow(10, otherExponent) + right.m_coefficient, right.m_scale);
      }

      public static BigDecimal operator +(BigDecimal left, System.Numerics.BigInteger right) => left + new BigDecimal(right);

      #endregion

      #region System.Numerics.IComparisonOperators<>

      public static bool operator <(BigDecimal left, BigDecimal right) => left.CompareTo(right) < 0;
      public static bool operator <=(BigDecimal left, BigDecimal right) => left.CompareTo(right) <= 0;
      public static bool operator >(BigDecimal left, BigDecimal right) => left.CompareTo(right) > 0;
      public static bool operator >=(BigDecimal left, BigDecimal right) => left.CompareTo(right) >= 0;

      #endregion

      #region System.Numerics.IDecrementOperators<>

      public static BigDecimal operator --(BigDecimal value) => value - One;

      #endregion

      #region System.Numerics.IDivisionOperators<>

      public static BigDecimal operator /(BigDecimal left, BigDecimal right)
        => Divide(left, right, DefaultContext.FractionalDigits, DefaultContext.RoundingRule);
      public static BigDecimal operator /(BigDecimal left, System.Numerics.BigInteger right)
        => left / new BigDecimal(right);

      public static BigDecimal Divide(BigDecimal left, BigDecimal right, int precision, DirectedRoundingMode roundingMode)
      {
        if (right.m_coefficient.IsZero) throw new System.DivideByZeroException();

        var scaledDividend = left.m_coefficient * System.Numerics.BigInteger.Pow(10, precision);

        var quotient = System.Numerics.BigInteger.DivRem(scaledDividend, right.m_coefficient, out var remainder);

        if (!remainder.IsZero)
          quotient = RoundDirected(quotient, remainder, right.m_coefficient, roundingMode);

        return new(quotient, checked(left.m_scale - right.m_scale + precision));
      }
      public static BigDecimal Divide(BigDecimal left, BigDecimal right, int precision, NearestRoundingRule roundingRule)
      {
        if (right.m_coefficient.IsZero) throw new System.DivideByZeroException();

        var scaledDividend = left.m_coefficient * System.Numerics.BigInteger.Pow(10, precision);

        var quotient = System.Numerics.BigInteger.DivRem(scaledDividend, right.m_coefficient, out var remainder);

        if (!remainder.IsZero)
          quotient = RoundNearest(quotient, remainder, right.m_coefficient, roundingRule);

        return new(quotient, checked(left.m_scale - right.m_scale + precision));
      }

      private static System.Numerics.BigInteger RoundDirected(System.Numerics.BigInteger quotient, System.Numerics.BigInteger remainder, System.Numerics.BigInteger divisor, DirectedRoundingMode mode)
      {
        if (remainder.IsZero)
          return quotient;

        var sign = remainder.Sign * divisor.Sign;

        return mode switch
        {
          DirectedRoundingMode.TowardZero => quotient,
          DirectedRoundingMode.AwayFromZero => quotient + sign,
          DirectedRoundingMode.TowardNegativeInfinity => sign < 0 ? quotient - 1 : quotient,
          DirectedRoundingMode.TowardPositiveInfinity => sign > 0 ? quotient + 1 : quotient,
          DirectedRoundingMode.Random => Random.Shared.Next(2) == 0 ? quotient : quotient + sign,
          DirectedRoundingMode.Stochastic => StochasticRound(quotient, remainder, divisor, sign),
          _ => throw new ArgumentOutOfRangeException(nameof(mode))
        };
      }
      private static System.Numerics.BigInteger RoundNearest(System.Numerics.BigInteger quotient, System.Numerics.BigInteger remainder, System.Numerics.BigInteger divisor, NearestRoundingRule rule)
      {
        var sign = remainder.Sign * divisor.Sign;

        var twiceRemainder = System.Numerics.BigInteger.Abs(remainder) * 2;

        var absDivisor = System.Numerics.BigInteger.Abs(divisor);

        var comparison = twiceRemainder.CompareTo(absDivisor);

        //
        // Not a midpoint.
        //
        if (comparison < 0)
          return quotient;

        if (comparison > 0)
          return quotient + sign;

        //
        // Exact midpoint.
        //
        return rule switch
        {
          NearestRoundingRule.TowardZero => quotient,
          NearestRoundingRule.AwayFromZero => quotient + sign,
          NearestRoundingRule.TowardNegativeInfinity => sign < 0 ? quotient - 1 : quotient,
          NearestRoundingRule.TowardPositiveInfinity => sign > 0 ? quotient + 1 : quotient,
          NearestRoundingRule.ToEven => quotient.IsEven ? quotient : quotient + sign,
          NearestRoundingRule.ToOdd => !quotient.IsEven ? quotient : quotient + sign,
          NearestRoundingRule.Random => Random.Shared.Next(2) == 0 ? quotient : quotient + sign,
          _ => throw new ArgumentOutOfRangeException(nameof(rule))
        };
      }

      private static System.Numerics.BigInteger StochasticRound(System.Numerics.BigInteger quotient, System.Numerics.BigInteger remainder, System.Numerics.BigInteger divisor, int sign)
      {
        var numerator = (double)System.Numerics.BigInteger.Abs(remainder);
        var denominator = (double)System.Numerics.BigInteger.Abs(divisor);

        return Random.Shared.NextDouble() < numerator / denominator
          ? quotient + sign
          : quotient;
      }

      #endregion

      #region System.Numerics.IIncrementOperators<>

      public static BigDecimal operator ++(BigDecimal value) => value + One;

      #endregion

      #region System.Numerics.IModulusOperators<>

      public static BigDecimal operator %(BigDecimal a, BigDecimal b) => throw new NotImplementedException();
      public static BigDecimal operator %(BigDecimal a, System.Numerics.BigInteger b) => throw new NotImplementedException();

      #endregion

      #region System.Numerics.IMultiplicativeIdentity<>

      public static BigDecimal MultiplicativeIdentity => One;

      #endregion

      #region System.Numerics.IMultiplyOperators<> 

      public static BigDecimal operator *(BigDecimal left, BigDecimal right)
        => new(left.m_coefficient * right.m_coefficient, checked(left.m_scale + right.m_scale));

      public static BigDecimal operator *(BigDecimal left, System.Numerics.BigInteger right) => left * new BigDecimal(right);

      #endregion

      #region System.Numerics.INumber<>

      //public static BigDecimal Max(BigDecimal a, BigDecimal b) => a >= b ? a : b;
      //public static BigDecimal MaxNumber(BigDecimal a, BigDecimal b) => a >= b ? a : b;
      //public static BigDecimal Min(BigDecimal a, BigDecimal b) => a <= b ? a : b;
      //public static BigDecimal MinNumber(BigDecimal a, BigDecimal b) => a <= b ? a : b;

      public static BigDecimal Parse(System.ReadOnlySpan<char> s, System.IFormatProvider? provider) => throw new NotImplementedException();
      public static BigDecimal Parse(string s, System.IFormatProvider? provider) => throw new NotImplementedException();
      public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, System.IFormatProvider? provider)
      {
        var s = ToString(format.ToString(), provider);
        s.CopyTo(destination);
        charsWritten = s.Length;
        return true;
      }
      public static bool TryParse(ReadOnlySpan<char> s, System.IFormatProvider? provider, out BigDecimal result) => throw new NotImplementedException();
      public static bool TryParse(string? s, System.IFormatProvider? provider, out BigDecimal result) => throw new NotImplementedException();


      #endregion

      #region System.Numerics.INumberBase<>

      public static BigDecimal One => new(System.Numerics.BigInteger.One);
      public static int Radix { get; } = 10;
      public static BigDecimal Zero => new(System.Numerics.BigInteger.Zero);

      public static BigDecimal Abs(BigDecimal value) => new(System.Numerics.BigInteger.Abs(value.m_coefficient), value.m_scale, false);
      public static bool IsCanonical(BigDecimal value) => !CanNormalize(value.m_coefficient, value.m_scale);
      public static bool IsComplexNumber(BigDecimal value) => false;
      public static bool IsEvenInteger(BigDecimal value) => value.m_scale == 0 && System.Numerics.BigInteger.IsEvenInteger(value.m_coefficient);
      public static bool IsFinite(BigDecimal value) => true;
      public static bool IsImaginaryNumber(BigDecimal value) => false;
      public static bool IsInfinity(BigDecimal value) => false;
      public static bool IsInteger(BigDecimal value) => value.m_scale == 0;
      public static bool IsNaN(BigDecimal value) => false;
      public static bool IsNegative(BigDecimal value) => System.Numerics.BigInteger.IsNegative(value.m_coefficient);
      public static bool IsNegativeInfinity(BigDecimal value) => false;
      public static bool IsNormal(BigDecimal value) => true;
      public static bool IsOddInteger(BigDecimal value) => value.m_scale == 0 && System.Numerics.BigInteger.IsOddInteger(value.m_coefficient);
      public static bool IsPositive(BigDecimal value) => System.Numerics.BigInteger.IsPositive(value.m_coefficient);
      public static bool IsPositiveInfinity(BigDecimal value) => false;
      public static bool IsRealNumber(BigDecimal value) => true;
      public static bool IsSubnormal(BigDecimal value) => false;
      public static bool IsZero(BigDecimal value) => value.m_coefficient.IsZero;
      public static BigDecimal MaxMagnitude(BigDecimal a, BigDecimal b) => Abs(a) >= Abs(b) ? a : b;
      public static BigDecimal MaxMagnitudeNumber(BigDecimal a, BigDecimal b) => Abs(a) >= Abs(b) ? a : b;
      public static BigDecimal MinMagnitude(BigDecimal a, BigDecimal b) => Abs(a) <= Abs(b) ? a : b;
      public static BigDecimal MinMagnitudeNumber(BigDecimal a, BigDecimal b) => Abs(a) <= Abs(b) ? a : b;

      public static BigDecimal Parse(ReadOnlySpan<char> s, System.Globalization.NumberStyles style, System.IFormatProvider? provider) => throw new NotImplementedException();
      public static BigDecimal Parse(string s, System.Globalization.NumberStyles style, System.IFormatProvider? provider) => throw new NotImplementedException();
      static bool System.Numerics.INumberBase<BigDecimal>.TryConvertFromChecked<TOther>(TOther value, out BigDecimal result)
      {
        try
        {
          if (NumericTypeInfo<TOther>.ImplementsIBinaryInteger)
          {
            result = new(System.Numerics.BigInteger.CreateChecked(value));
            return true;
          }
          //if (NumericTypeInfo<TOther>.ImplementsIFloatingPoint)
          //  return ApproximateRational(double.CreateChecked(value));
          else if (value is BigDecimal bd)
          {
            result = bd;
            return true;
          }
        }
        catch { }

        result = default;
        return false;
      }
      static bool System.Numerics.INumberBase<BigDecimal>.TryConvertFromSaturating<TOther>(TOther value, out BigDecimal result)
      {
        try
        {
          if (NumericTypeInfo<TOther>.ImplementsIBinaryInteger)
          {
            result = new(System.Numerics.BigInteger.CreateSaturating(value));
            return true;
          }
          //if (NumericTypeInfo<TOther>.ImplementsIFloatingPoint)
          //  return ApproximateRational(double.CreateSaturating(value));
          else if (value is BigDecimal bd)
          {
            result = bd;
            return true;
          }
        }
        catch { }

        result = default;
        return false;
      }
      static bool System.Numerics.INumberBase<BigDecimal>.TryConvertFromTruncating<TOther>(TOther value, out BigDecimal result)
      {
        try
        {
          if (NumericTypeInfo<TOther>.ImplementsIBinaryInteger)
          {
            result = new(System.Numerics.BigInteger.CreateTruncating(value));
            return true;
          }
          //if (NumericTypeInfo<TOther>.ImplementsIFloatingPoint)
          //  return ApproximateRational(double.CreateTruncating(value));
          else if (value is BigDecimal bd)
          {
            result = bd;
            return true;
          }
        }
        catch { }

        result = default;
        return false;
      }
      static bool System.Numerics.INumberBase<BigDecimal>.TryConvertToChecked<TOther>(BigDecimal value, out TOther result)
      {
        result = default!;
        return false;
      }
      static bool System.Numerics.INumberBase<BigDecimal>.TryConvertToSaturating<TOther>(BigDecimal value, out TOther result)
      {
        result = default!;
        return false;
      }
      static bool System.Numerics.INumberBase<BigDecimal>.TryConvertToTruncating<TOther>(BigDecimal value, out TOther result)
      {
        result = default!;
        return false;
      }
      public static bool TryParse(System.ReadOnlySpan<char> s, System.Globalization.NumberStyles style, System.IFormatProvider? provider, out BigDecimal result) => throw new NotImplementedException();
      public static bool TryParse(string? s, System.Globalization.NumberStyles style, System.IFormatProvider? provider, out BigDecimal result) => throw new NotImplementedException();

      #endregion

      public static BigDecimal Pow(BigDecimal value, int exponent)
      {
        if (exponent == 0)
          return One;

        if (exponent < 0)
          return One / Pow(value, -exponent);

        return new(
          System.Numerics.BigInteger.Pow(value.Coefficient, exponent),
          checked(value.Scale * exponent)
        );
      }

      /// <summary>
      /// <para>Exponentiation by squaring integer exponent.</para>
      /// </summary>
      /// <param name="value"></param>
      /// <param name="exponent"></param>
      /// <returns></returns>
      public static BigDecimal Pow(BigDecimal value, System.Numerics.BigInteger exponent)
      {
        if (exponent < 0)
          return One / Pow(value, -exponent);

        var result = One;
        var factor = value;

        while (exponent > 0)
        {
          if ((exponent & 1) != 0)
            result *= factor;

          factor *= factor;
          exponent >>= 1;
        }

        return result;
      }

      #region System.Numerics.ISubtractionOperators<>

      public static BigDecimal operator -(BigDecimal left, BigDecimal right)
      {
        if (right.m_coefficient.IsZero)
          return left;

        if (left.m_scale == right.m_scale)
          return new(left.m_coefficient - right.m_coefficient, left.m_scale);

        if (left.m_scale > right.m_scale)
        {
          var exponent = checked(left.m_scale - right.m_scale);

          return new(left.m_coefficient - right.m_coefficient * System.Numerics.BigInteger.Pow(10, exponent), left.m_scale);
        }

        var otherExponent = checked(right.m_scale - left.m_scale);

        return new(left.m_coefficient * System.Numerics.BigInteger.Pow(10, otherExponent) - right.m_coefficient, right.m_scale);
      }

      public static BigDecimal operator -(BigDecimal left, System.Numerics.BigInteger right) => left - new BigDecimal(right, 0);

      #endregion

      #region System.Numerics.IUnaryNegationOperators<>

      public static BigDecimal operator -(BigDecimal value) => new(-value.m_coefficient, value.m_scale, false);

      #endregion

      #region System.Numerics.IUnaryPlusOperators<>

      public static BigDecimal operator +(BigDecimal value) => value;

      #endregion

      // IFormattable
      public string ToString(string? format, IFormatProvider? formatProvider)
        => throw new NotImplementedException();

      // IComparable
      public int CompareTo(object? obj)
        => obj switch
        {
          BigDecimal value => CompareTo(value),
          null => 1,
          _ => throw new System.ArgumentException($"Object must be of type {nameof(BigDecimal)}.")
        };

      // IComparable<>
      public int CompareTo(BigDecimal other)
      {
        if (m_coefficient.Sign != other.m_coefficient.Sign)
          return m_coefficient.Sign.CompareTo(other.m_coefficient.Sign);

        if (m_coefficient.IsZero)
          return 0;

        if (m_scale == other.m_scale)
          return m_coefficient.CompareTo(other.m_coefficient);

        if (m_coefficient == other.m_coefficient)
          return other.m_scale.CompareTo(m_scale);

        var digitsA = BinaryInteger.DigitCount(System.Numerics.BigInteger.Abs(m_coefficient), 10);
        var digitsB = BinaryInteger.DigitCount(System.Numerics.BigInteger.Abs(other.m_coefficient), 10);

        var magnitudeA = (long)digitsA - m_scale;
        var magnitudeB = (long)digitsB - other.m_scale;

        if (magnitudeA != magnitudeB)
        {
          var comparison = magnitudeA.CompareTo(magnitudeB);

          return m_coefficient.Sign > 0
            ? comparison
            : -comparison;
        }

        // At this point: "digitsA - m_scale == digitsB - other.m_scale"
        // And therefore: "digitsA - digitsB == m_scale - other.m_scale"

        var digitDelta = digitsA - digitsB; // Use digitDelta rather than scaleDelta since it is naturally bounded and directly suitable for BigInteger.Pow().

        if (digitDelta > 0)
          return m_coefficient.CompareTo(other.m_coefficient * System.Numerics.BigInteger.Pow(10, digitDelta));

        if (digitDelta < 0)
          return (m_coefficient * System.Numerics.BigInteger.Pow(10, -digitDelta)).CompareTo(other.m_coefficient);

        return m_coefficient.CompareTo(other.m_coefficient);
      }

      //public int Sign => m_coefficient.Sign;

      #endregion

      public override string ToString()
        => $"{m_coefficient}e-{m_scale}";
    }
  }
}
