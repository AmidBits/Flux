namespace Flux
{
  namespace Units
  {
    /// <summary>Radix, unit of natural number.</summary>
    /// <seealso cref="https://en.wikipedia.org/wiki/Radix"/>
    public readonly record struct Radix
    : System.IComparable<Radix>, IQuantifiable<int>
    {
      private readonly int m_value;

      public Radix(int radix) => m_value = Assert(radix);

      #region Static methods

      /// <summary>Asserts the number is a valid <paramref name="radix"/> (throws an exception with an optional <paramref name="paramName"/>, if not).</summary>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public static TSelf Assert<TSelf>(TSelf radix, string? paramName = null)
        where TSelf : System.Numerics.INumber<TSelf>
        => Is(radix) ? radix : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(radix), "Must be an integer, greater than or equal to 2.");

      /// <summary>Asserts the number is a valid <paramref name="radix"/>, with an <paramref name="upperLimit"/> (throws an exception with an optional <paramref name="paramName"/>, if not).</summary>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public static TSelf Assert<TSelf>(TSelf radix, TSelf upperLimit, string? paramName = null)
        where TSelf : System.Numerics.INumber<TSelf>
        => Is(radix, upperLimit) ? radix : throw new System.ArgumentOutOfRangeException(paramName ?? nameof(radix), $"Must be an integer, greater than or equal to 2 and less than or equal to {upperLimit}.");

      /// <summary>Converts a positional notation list of <paramref name="positionalNotationIndices"/> with <paramref name="radix"/> to a numerical value.</summary>
      public static bool TryConvertPositionalNotationIndicesToNumber<TRadix, TValue>(System.Collections.Generic.IList<int> positionalNotationIndices, TRadix radix, out TValue number)
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
        where TValue : System.Numerics.IBinaryInteger<TValue>
      {
        number = TValue.Zero;

        try
        {
          for (var index = 0; index < positionalNotationIndices.Count; index++)
          {
            number *= TValue.CreateChecked(radix);

            number += TValue.CreateChecked(positionalNotationIndices[index]);
          }

          return true;
        }
        catch { }

        return false;
      }

      /// <summary>Converts a numerical <paramref name="number"/> using <paramref name="radix"/> to a positional notation list of indices.</summary>
      public static bool TryConvertNumberToPositionalNotationIndices<TValue, TRadix>(TValue number, TRadix radix, out System.Collections.Generic.List<int> positionalNotationIndices)
        where TValue : System.Numerics.IBinaryInteger<TValue>
        where TRadix : System.Numerics.IBinaryInteger<TRadix>
      {
        if (TValue.IsNegative(number))
          return TryConvertNumberToPositionalNotationIndices(TValue.Abs(number), radix, out positionalNotationIndices);

        positionalNotationIndices = new System.Collections.Generic.List<int>();

        try
        {
          while (!TValue.IsZero(number))
          {
            (number, var remainder) = TValue.DivRem(number, TValue.CreateChecked(radix));

            positionalNotationIndices.Insert(0, int.CreateChecked(remainder));
          }

          return true;
        }
        catch { }

        return false;
      }

      /// <summary>Returns the count of all single digits in <paramref name="value"/> using base <paramref name="radix"/>.</summary>
      public static TSelf DigitCount<TSelf>(TSelf value, TSelf radix)
        where TSelf : System.Numerics.IBinaryInteger<TSelf>
      {
        Assert(radix);

        var count = TSelf.Zero;

        while (!TSelf.IsZero(value))
        {
          count++;

          value /= radix;
        }

        return count;
      }

      /// <summary>Returns the sum of all single digits in <paramref name="value"/> using base <paramref name="radix"/>.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Digit_sum"/>
      public static TSelf DigitSum<TSelf>(TSelf value, TSelf radix)
        where TSelf : System.Numerics.IBinaryInteger<TSelf>
      {
        Assert(radix);

        var sum = TSelf.Zero;

        while (!TSelf.IsZero(value))
        {
          sum += value % radix;

          value /= radix;
        }

        return sum;
      }

      /// <summary>Drop the trailing (least significant) digit of <paramref name="number"/> using base <paramref name="radix"/>.</summary>
      public static TSelf DropLeastSignificantDigit<TSelf>(TSelf number, TSelf radix)
        where TSelf : System.Numerics.IBinaryInteger<TSelf>
        => number / Assert(radix);

      /// <summary>Drop <paramref name="count"/> trailing (least significant) digits from <paramref name="number"/> using base <paramref name="radix"/>.</summary>
      public static TSelf DropLeastSignificantDigits<TSelf>(TSelf number, TSelf radix, TSelf count)
        where TSelf : System.Numerics.IBinaryInteger<TSelf>
        => number / Maths.IntegerPow(Assert(radix), count);

      /// <summary>Drop <paramref name="count"/> leading (most significant) digits of <paramref name="number"/> using base <paramref name="radix"/>.</summary>
      public static TSelf DropMostSignificantDigits<TSelf>(TSelf number, TSelf radix, TSelf count)
        where TSelf : System.Numerics.IBinaryInteger<TSelf>
        => number % Maths.IntegerPow(radix, DigitCount(number, radix) - count);

      /// <summary>Returns a maximum of <paramref name="count"/> digits (as <typeparamref name="TSelf"/>) of <paramref name="number"/> using base <paramref name="radix"/>.</summary>
      public static System.Collections.Generic.List<TSelf> GetDigits<TSelf>(TSelf number, TSelf radix, int count = int.MaxValue)
        where TSelf : System.Numerics.IBinaryInteger<TSelf>
      {
        var reversed = GetDigitsReversed(number, radix, count);
        reversed.Reverse();
        return reversed;
      }

      /// <summary>Returns a maximum of <paramref name="count"/> digits (as <typeparamref name="TSelf"/>) of <paramref name="number"/> using base <paramref name="radix"/>, in reverse order.</summary>
      public static System.Collections.Generic.List<TSelf> GetDigitsReversed<TSelf>(TSelf number, TSelf radix, int count = int.MaxValue)
        where TSelf : System.Numerics.IBinaryInteger<TSelf>
      {
        Assert(radix);

        if (TSelf.IsNegative(number))
          number = -number;

        var list = new System.Collections.Generic.List<TSelf>();

        if (TSelf.IsZero(number))
          list.Add(TSelf.Zero);
        else
          while (!TSelf.IsZero(number) && list.Count < count)
          {
            list.Add(number % radix);
            number /= radix;
          }

        return list;
      }

      /// <summary>Returns the digit place value components of <paramref name="number"/> using base <paramref name="radix"/>. E.g. 1234 return [4 (for 4 * ones), 30 (for 3 * tens), 200 (for 2 * hundreds), 1000 (for 1 * thousands)].</summary>
      public static System.Collections.Generic.List<TSelf> GetPlaceValues<TSelf>(TSelf number, TSelf radix)
        where TSelf : System.Numerics.IBinaryInteger<TSelf>
      {
        var list = GetDigitsReversed(number, radix); // Already asserts radix.

        var power = TSelf.One;

        for (int index = 0; index < list.Count; index++)
        {
          list[index] *= power;

          power *= radix;
        }

        return list;
      }

      /// <summary>
      /// <para>Convert a value to a Gray code with the given base and digits. Iterating through a sequence of values would result in a sequence of Gray codes in which only one digit changes at a time.</para>
      /// <see href="https://en.wikipedia.org/wiki/Gray_code"/>
      /// </summary>
      /// <remarks>Experimental adaption from wikipedia.</remarks>
      /// <exception cref="System.ArgumentNullException"></exception>
      public static TSelf[] BinaryToGray<TSelf>(TSelf value, TSelf radix)
        where TSelf : System.Numerics.IBinaryInteger<TSelf>, System.Numerics.IUnsignedNumber<TSelf>
      {
        var gray = new TSelf[int.CreateChecked(DigitCount(value, radix))];

        var baseN = new TSelf[gray.Length]; // Stores the ordinary base-N number, one digit per entry

        for (var index = 0; index < gray.Length; index++) // Put the normal baseN number into the baseN array. For base 10, 109 would be stored as [9,0,1]
        {
          baseN[index] = value % radix;

          value /= radix;
        }

        var shift = TSelf.Zero; // Convert the normal baseN number into the Gray code equivalent. Note that the loop starts at the most significant digit and goes down.

        for (var index = gray.Length - 1; index >= 0; index--) // The Gray digit gets shifted down by the sum of the higher digits.
        {
          gray[index] = (baseN[index] + shift) % radix;

          shift = shift + radix - gray[index]; // Subtract from base so shift is positive
        }

        return gray;
      }

      /// <summary>
      /// <para>Computes the floor integer-log-<paramref name="radix"/> of <paramref name="value"/>.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Logarithm"/></para>
      /// </summary>
      /// <remarks>The ceiling integer log: (<paramref name="value"/> >= 1 ? IntegerLogFloor(<paramref name="value"/> - 1, <paramref name="radix"/>) + 1 : 0).</remarks>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public static TSelf IntegerLogFloor<TSelf>(TSelf value, TSelf radix)
        where TSelf : System.Numerics.INumber<TSelf>
      {
        Maths.AssertNonNegative(value);
        Assert(radix);

        var ilog = TSelf.Zero;

        if (!TSelf.IsZero(value))
          while (value >= radix)
          {
            value /= radix;

            ilog++;
          }

        return ilog;
      }

      /// <summary>
      /// <para>Computes the integer log <paramref name="ilogTowardZero"/> and <paramref name="ilogAwayFromZero"/> of <paramref name="value"/> in <paramref name="radix"/>. Optionally <paramref name="proper"/>.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/Logarithm"/></para>
      /// </summary>
      /// <param name="value">The value for which <paramref name="ilogTowardZero"/> and <paramref name="ilogAwayFromZero"/> will be found.</param>
      /// <param name="radix">The power of alignment.</param>
      /// <param name="ilogTowardZero"></param>
      /// <param name="ilogAwayFromZero"></param>
      public static (TSelf IlogTowardZero, TSelf IlogAwayFromZero) IntegerLog<TSelf>(TSelf value, TSelf radix)
        where TSelf : System.Numerics.IBinaryInteger<TSelf>
      {
        TSelf ilogTowardZero, ilogAwayFromZero;

        if (TSelf.IsNegative(value))
        {
          (ilogTowardZero, ilogAwayFromZero) = IntegerLog(TSelf.Abs(value), radix);

          return (-ilogTowardZero, -ilogAwayFromZero);
        }

        ilogTowardZero = IntegerLogFloor(value, radix);
        ilogAwayFromZero = IsPowOf(value, radix) ? ilogTowardZero : ilogTowardZero + TSelf.One;

        return (ilogTowardZero, ilogAwayFromZero);
      }

      /// <summary>Returns whether the number is a valid <paramref name="radix"/>.</summary>
      public static bool Is<TSelf>(TSelf radix)
        where TSelf : System.Numerics.INumber<TSelf> // Accomodate INumber so that other types than integer can be used if needed.
        => radix > TSelf.One && TSelf.IsInteger(radix);

      /// <summary>Returns whether the number is a valid <paramref name="radix"/>, with an <paramref name="upperLimit"/>.</summary>
      public static bool Is<TSelf>(TSelf radix, TSelf upperLimit)
        where TSelf : System.Numerics.INumber<TSelf> // Accomodate INumber so that other types than integer can be used if needed.
        => radix > TSelf.One && radix <= upperLimit && TSelf.IsInteger(radix);

      /// <summary>Indicates whether <paramref name="number"/> using base <paramref name="radix"/> is jumbled (i.e. no neighboring digits having a difference larger than 1).</summary>
      /// <see cref="http://www.geeksforgeeks.org/check-if-a-number-is-jumbled-or-not/"/>
      public static bool IsJumbled<TSelf>(TSelf number, TSelf radix)
        where TSelf : System.Numerics.IBinaryInteger<TSelf>
      {
        Assert(radix);

        while (!TSelf.IsZero(number))
        {
          var remainder = number % radix;

          number /= radix;

          if (TSelf.IsZero(number))
            break;
          else if (TSelf.Abs((number % radix) - remainder) > TSelf.One) // If the difference to the digit is greater than 1, then the number cannot jumbled.
            return false;
        }

        return true;
      }

      /// <summary>Determines if <paramref name="value"/> is a power of <paramref name="radix"/>.</summary>
      public static bool IsPowOf<TSelf>(TSelf value, TSelf radix)
        where TSelf : System.Numerics.IBinaryInteger<TSelf>
      {
        Maths.AssertNonNegative(value);
        Assert(radix);

        if (value == radix) // If the value is equal to the radix, then it's a power of the radix.
          return true;

        if (radix == (TSelf.One + TSelf.One)) // Special case for binary numbers, we can use dedicated IsPow2().
          return TSelf.IsPow2(value);

        if (value > TSelf.One)
          while (TSelf.IsZero(value % radix))
            value /= radix;

        return value == TSelf.One;
      }

      /// <summary>Indicates whether the <paramref name="number"/> is single digit using the base <paramref name="radix"/>, i.e. in the range [-<paramref name="radix"/>, <paramref name="radix"/>].</summary>
      public static bool IsSingleDigit<TSelf>(TSelf number, TSelf radix)
        where TSelf : System.Numerics.IBinaryInteger<TSelf>
      {
        Assert(radix);

        return (TSelf.IsZero(number) || (TSelf.IsPositive(number) && number < radix) || (TSelf.IsNegative(number) && number > -radix));
      }

      /// <summary>Retreive <paramref name="count"/> least significant digits of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
      public static TSelf KeepLeastSignificantDigit<TSelf>(TSelf value, TSelf radix)
        where TSelf : System.Numerics.IBinaryInteger<TSelf>
        => value % Assert(radix);

      /// <summary>Retreive <paramref name="count"/> least significant digits of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
      public static TSelf KeepLeastSignificantDigits<TSelf>(TSelf value, TSelf radix, TSelf count)
        where TSelf : System.Numerics.IBinaryInteger<TSelf>
        => value % Maths.IntegerPow(Assert(radix), count);

      /// <summary>Drop the leading digit of <paramref name="value"/> using base <paramref name="radix"/>.</summary>
      public static TSelf KeepMostSignificantDigits<TSelf>(TSelf value, TSelf radix, TSelf count)
        where TSelf : System.Numerics.IBinaryInteger<TSelf>
        => value / Maths.IntegerPow(radix, DigitCount(value, radix) - count);

      /// <summary>Compute the floor power-of-<paramref name="radix"/> of <paramref name="value"/>.</summary>
      /// <param name="value">The value for which the toward-zero (floor if positive) power-of-<paramref name="radix"/> will be found.</param>
      /// <param name="radix">The power of alignment.</param>
      /// <returns>The floor power-of-<paramref name="radix"/> of <paramref name="value"/>.</returns>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public static TSelf PowOf<TSelf>(TSelf value, TSelf radix)
        where TSelf : System.Numerics.IBinaryInteger<TSelf>
        => Maths.IntegerPow(radix, TSelf.CreateChecked(IntegerLogFloor(value, radix)));

      /// <summary>Compute the two closest (toward-zero and away-from-zero) power-of-<paramref name="radix"/> of <paramref name="value"/>. Specify <paramref name="proper"/> to ensure results that are not equal to value.</summary>
      /// <param name="value">The value for which the nearest power-of-radix will be found.</param>
      /// <param name="radix">The power of alignment.</param>
      /// <param name="proper">Proper means nearest but do not include <paramref name="value"/> if it's a power-of-<paramref name="radix"/>, i.e. the two power-of-<paramref name="radix"/> will be properly nearest (but not the same) or LT/GT rather than LTE/GTE.</param>
      /// <returns>The two closest (toward-zero and away-from-zero) power-of-<paramref name="radix"/> to <paramref name="value"/>.</returns>
      public static (TSelf powOfTowardsZero, TSelf powOfAwayFromZero) PowOf<TSelf>(TSelf value, TSelf radix, bool proper)
        where TSelf : System.Numerics.IBinaryInteger<TSelf>
      {
        TSelf powOfTowardsZero, powOfAwayFromZero;

        if (TSelf.IsNegative(value))
        {
          (powOfTowardsZero, powOfAwayFromZero) = PowOf(TSelf.Abs(value), radix, proper);

          return (-powOfTowardsZero, -powOfAwayFromZero);
        }

        powOfTowardsZero = PowOf(value, radix);
        powOfAwayFromZero = powOfTowardsZero != value ? powOfTowardsZero * radix : powOfTowardsZero; // If toward-zero is not equal to value, make away-from-zero the next power-of.

        return proper && powOfTowardsZero == powOfAwayFromZero
          ? (powOfTowardsZero /= radix, powOfAwayFromZero *= radix)
          : (powOfTowardsZero, powOfAwayFromZero);
      }

      /// <summary>Compute the nearest power-of-<paramref name="radix"/> from <paramref name="powOfTowardsZero"/>/<paramref name="powOfAwayFromZero"/> of <paramref name="value"/>. Specify <paramref name="proper"/> to ensure results that are not equal to value.</summary>
      /// <param name="value">The value for which the nearest power-of-radix will be found.</param>
      /// <param name="radix">The power of alignment.</param>
      /// <param name="proper">Proper means nearest but do not include <paramref name="value"/> if it's a power-of-<paramref name="radix"/>, i.e. the two power-of-<paramref name="radix"/> will be properly nearest (but not the same) or LT/GT rather than LTE/GTE.</param>
      /// <returns>The nearest power-of-<paramref name="radix"/> to <paramref name="value"/>.</returns>
      public static TSelf RoundToPowOf<TSelf>(TSelf value, TSelf radix, bool proper, RoundingMode mode, out TSelf powOfTowardsZero, out TSelf powOfAwayFromZero)
        where TSelf : System.Numerics.IBinaryInteger<TSelf>
      {
        (powOfTowardsZero, powOfAwayFromZero) = PowOf(value, radix, proper);

        return value.RoundToBoundaries(mode, powOfTowardsZero, powOfAwayFromZero);
      }

      /// <summary>Reverse the digits of <paramref name="number"/> using base <paramref name="radix"/>, obtaining a new number.</summary>
      public static TSelf ReverseDigits<TSelf>(TSelf number, TSelf radix)
        where TSelf : System.Numerics.IBinaryInteger<TSelf>
      {
        Assert(radix);

        var reversed = TSelf.Zero;

        while (!TSelf.IsZero(number))
        {
          reversed = (reversed * radix) + (number % radix);

          number /= radix;
        }

        return reversed;
      }

      /// <summary>Returns whether <paramref name="number"/> using base <paramref name="radix"/> is a self number. A self number, Colombian number or Devlali number, in a given number base (radix) is a natural number that cannot be written as the sum of any other natural number n and the individual digits of n.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Self_number"/>
      public static bool IsSelfNumber<TSelf>(TSelf number, TSelf radix)
        where TSelf : System.Numerics.IBinaryInteger<TSelf>
      {
        for (var value = SelfNumberLowBound(number, radix); value < number; value++)
          if (DigitSum(value, radix) + value == number)
            return false;

        return true;
      }

      /// <summary>Returns the minimum possible number that can make <paramref name="value"/> a self number using base <paramref name="radix"/>.</summary>
      public static TSelf SelfNumberLowBound<TSelf>(TSelf value, TSelf radix)
        where TSelf : System.Numerics.IBinaryInteger<TSelf>
      {
        if (value <= TSelf.Zero) throw new System.ArgumentOutOfRangeException(nameof(value));

        var logRadix = TSelf.CreateChecked(IntegerLogFloor(value, radix));
        var maxDistinct = (TSelf.CreateChecked(9) * logRadix) + (value / Maths.IntegerPow(radix, logRadix));

        return TSelf.Max(value - maxDistinct, TSelf.Zero);
      }

      private static string m_subscriptDecimalDigits = "\u2080\u2081\u2082\u2083\u2084\u2085\u2086\u2087\u2088\u2089";

      /// <summary>Converts <paramref name="number"/> to text using base <paramref name="radix"/>.</summary>
      public static string ToSubscriptString<TSelf>(TSelf number, int radix)
        where TSelf : System.Numerics.IBinaryInteger<TSelf>
        => PositionalNotation.NumberToText(number, m_subscriptDecimalDigits.AsSpan()[..Assert(radix, m_subscriptDecimalDigits.Length)], (char)UnicodeCodepoint.HyphenMinus).ToString();

      private static string m_superscriptDecimalDigits = "\u2070\u00B9\u00B2\u00B3\u2074\u2075\u2076\u2077\u2078\u2079";

      /// <summary>Converts <paramref name="number"/> to text using base <paramref name="radix"/>.</summary>
      public static string ToSuperscriptString<TSelf>(TSelf number, int radix)
        where TSelf : System.Numerics.IBinaryInteger<TSelf>
        => PositionalNotation.NumberToText(number, m_superscriptDecimalDigits.AsSpan()[..Assert(radix, m_superscriptDecimalDigits.Length)], (char)UnicodeCodepoint.HyphenMinus).ToString();

      #endregion Static methods

      #region Overloaded operators
      public static implicit operator Radix(System.Byte v) => new(v);
      public static implicit operator Radix(System.Int16 v) => new(v);
      public static implicit operator Radix(System.Int32 v) => new(v);
      public static implicit operator Radix(System.Int64 v) => new((int)v);
#if NET7_0_OR_GREATER
      public static implicit operator Radix(System.Int128 v) => new((int)v);
#endif
      public static implicit operator Radix(System.Numerics.BigInteger v) => new((int)v);

      public static bool operator <(Radix a, Radix b) => a.CompareTo(b) < 0;
      public static bool operator <=(Radix a, Radix b) => a.CompareTo(b) <= 0;
      public static bool operator >(Radix a, Radix b) => a.CompareTo(b) > 0;
      public static bool operator >=(Radix a, Radix b) => a.CompareTo(b) >= 0;

      public static Radix operator -(Radix v) => new(-v.m_value);
      public static Radix operator +(Radix a, int b) => new(a.m_value + b);
      public static Radix operator +(Radix a, Radix b) => a + b.m_value;
      public static Radix operator /(Radix a, int b) => new(a.m_value / b);
      public static Radix operator /(Radix a, Radix b) => a / b.m_value;
      public static Radix operator *(Radix a, int b) => new(a.m_value * b);
      public static Radix operator *(Radix a, Radix b) => a * b.m_value;
      public static Radix operator %(Radix a, int b) => new(a.m_value % b);
      public static Radix operator %(Radix a, Radix b) => a % b.m_value;
      public static Radix operator -(Radix a, int b) => new(a.m_value - b);
      public static Radix operator -(Radix a, Radix b) => a - b.m_value;
      #endregion Overloaded operators

      #region Implemented interfaces
      // IComparable<>
      public int CompareTo(Radix other) => m_value.CompareTo(other.m_value);
      // IComparable
      public int CompareTo(object? other) => other is not null && other is Radix o ? CompareTo(o) : -1;

      // IQuantifiable<>
      public string ToQuantityString(string? format, bool preferUnicode = false, bool useFullName = false) => $"{m_value}";
      public int Value => m_value;
      #endregion Implemented interfaces

      /// <summary>Creates a string containing the scientific pitch notation of the specified MIDI note.</summary>
      /// <see cref="https://en.wikipedia.org/wiki/Scientific_pitch_notation#Table_of_note_frequencies"/>
      public override string ToString() => ToQuantityString(null, false, false);
    }
  }
}
