#if INumber
namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static TSelf AssertRadix<TSelf>(TSelf radix)
      where TSelf : INumber<TSelf>
      => radix > TSelf.One ? radix : throw new System.ArgumentOutOfRangeException(nameof(radix));

    /// <summary>Returns the count of all digits in the value using the specified radix.</summary>
    //public static int DigitCount<TSelf>(this TSelf value, TSelf radix)
    //  where TSelf : INumber<TSelf>
    //  => value == TSelf.Zero
    //  ? 0
    //  : System.Convert.ToInt32(System.Math.Floor(System.Numerics.BigInteger.Log(System.Numerics.BigInteger.Abs(value), AssertRadix(radix))) + 1);

    /// <summary>Returns the sum of all digits in the value using the specified radix.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Digit_sum"/>
    public static TSelf DigitSum<TSelf>(this TSelf value, TSelf radix)
      where TSelf : INumber<TSelf>
    {
      AssertRadix(radix);

      var sum = TSelf.Zero;

      while (value != TSelf.Zero)
      {
        var (quotient, remainder) = TSelf.DivRem(value, radix);

        sum += remainder;
        value = quotient;
      }

      return sum;
    }

    /// <summary>Drop the leading digit of the number.</summary>
    //public static TSelf DropLeadingDigit<TSelf>(this TSelf source, TSelf radix)
    //  where TSelf : INumber<TSelf>
    //  => DigitCount(source, radix) is var dc && dc <= 1 ? 0 : source % System.Numerics.BigInteger.Pow(radix, dc - 1);

    /// <summary>Drop the trailing digit of the number.</summary>
    public static TSelf DropTrailingDigit<TSelf>(this TSelf value, TSelf radix)
      where TSelf : INumber<TSelf>
      => value / AssertRadix(radix);

    /// <summary>Returns the digits (as numbers) of a value.</summary>
    public static System.Span<TSelf> GetDigits<TSelf>(this TSelf value, TSelf radix)
      where TSelf : INumber<TSelf>
    {
      AssertRadix(radix);

      var list = new System.Collections.Generic.List<TSelf>();

      while (value != TSelf.Zero)
      {
        list.Insert(0, value % radix);
        value /= radix;
      }

      return System.Runtime.InteropServices.CollectionsMarshal.AsSpan(list);
    }

    /// <summary>Returns the place value digits (as numbers) of a value, in reverse order.</summary>
    public static System.Span<TSelf> GetDigitsReversed<TSelf>(this TSelf value, TSelf radix)
      where TSelf : INumber<TSelf>
    {
      AssertRadix(radix);

      var list = new System.Collections.Generic.List<TSelf>();

      while (value != TSelf.Zero)
      {
        list.Add(value % radix);
        value /= radix;
      }

      return System.Runtime.InteropServices.CollectionsMarshal.AsSpan(list);
    }

    /// <summary>Returns the digit components of the value. E.g. 1234 return { 4 (for 4 * ones), 30 (for 3 * tens), 200 (for 2 * hundreds), 1000 (for 1 * thousands) }.</summary>
    //public static System.Span<TSelf> GetPlaceValues<TSelf>(this TSelf value, TSelf radix, bool skipZeroes = false)
    //  where TSelf : INumber<TSelf>
    //{
    //  var list = GetDigitsReversed(value, radix);

    //  for (var index = 0; index < list.Length; index++)
    //    if (list[index] != TSelf.Zero || !skipZeroes)
    //      list[index] *= TSelf.Pow(radix, index);

    //  return list;
    //}

    /// <summary>Perform a comparison of the difference against radix (base) raised (negated) to the power of the specified number of digits.</summary>
    /// <see cref="https://stackoverflow.com/questions/9180385/is-this-a-valid-float-comparison-that-accounts-for-a-set-number-of-decimal-place"/>
    /// <param name="digitCount">The tolerance, as a number of digits (to either side of the decimal point) considered, before finding inequality. Using a negative value allows for left side tolerance.</param>
    /// <param name="radix">The radix (or base) to use when testing equality.</param>
    /// <example>Flux.Maths.IsEqualToSignificantDigits(1000.02, 1000.015, 2, 10);</example>
    //public static bool IsEqualToSignificantDigits<TSelf>(TSelf a, TSelf b, TSelf digitCount, TSelf radix)
    //  where TSelf : INumber<TSelf>
    //  => TSelf.Abs(a - b) <= TSelf.Pow(radix, -digitCount);

    /// <summary>Indicates whether the instance is jumbled (i.e. no neighboring digits having a difference larger than 1).</summary>
    /// <see cref="http://www.geeksforgeeks.org/check-if-a-number-is-jumbled-or-not/"/>
    public static bool IsJumbled<TSelf>(this TSelf value, TSelf radix)
      where TSelf : INumber<TSelf>
    {
      while (value != TSelf.Zero)
      {
        var (quotient, remainder) = TSelf.DivRem(value, radix);

        value = quotient;

        if (value == TSelf.Zero)
          break;
        else if (TSelf.Abs((value % radix) - remainder) > TSelf.One) // If the difference to the digit is greater than 1, then the number cannot jumbled.
          return false;
      }

      return true;
    }

    /// <summary>Determines if the number is a power of the specified radix. The sign is ignored so the function can be used on negative numbers as well.</summary>
    //public static bool IsPowerOf<TSelf>(this TSelf value, TSelf radix)
    //  where TSelf : INumber<TSelf>
    //{
    //  AssertRadix(radix);

    //  if (value < TSelf.Zero) // Make it work on negative numbers.
    //    value = -value;

    //  if (value == radix) // If the value is equal to the radix, then it's a power of that radix.
    //    return true;

    //  if (radix == (TSelf.One + TSelf.One)) // Special case for binary numbers.
    //    return value != TSelf.Zero && (value & (value - TSelf.One)) == 0;

    //  if (value > TSelf.One)
    //    while (TSelf.DivRem(value, radix) is var (quotient, remainder) && remainder == TSelf.Zero)
    //      value = quotient;

    //  return value == TSelf.One;
    //}

    /// <summary>Indicates whether the instance is single digit, i.e. in the range [-9, 9].</summary>
    //public static bool IsSingleDigit<TSelf>(this TSelf value, TSelf radix)
    //  where TSelf : INumber<TSelf>
    //  => AssertRadix(radix) == radix && value < radix;

    /// <summary>Returns the power of the specified number and radix.</summary>
    //public static TSelf PowerOf<TSelf>(this TSelf value, TSelf radix)
    //  where TSelf : INumber<TSelf>
    //  => value == TSelf.Zero ? TSelf.One : TSelf.Pow(radix, DigitCount(value, radix) - 1);

    public static TSelf ReverseDigits<TSelf>(this TSelf value, TSelf radix)
      where TSelf : INumber<TSelf>
    {
      AssertRadix(radix);

      var reverse = TSelf.Zero;

      while (value != TSelf.Zero)
      {
        var (quotient, remainder) = TSelf.DivRem(value, radix);

        value = quotient;

        reverse = reverse * radix + remainder;
      }

      return reverse;
    }
  }
}
#endif
