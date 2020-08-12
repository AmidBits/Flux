using System.Linq;

// <seealso cref="https://graphics.stanford.edu/~seander/bithacks.html"/>
// <seealso cref="https://aggregate.org/MAGIC/"/>
// <seealso cref="http://www.hackersdelight.org/"/>

namespace Flux
{
  public static partial class XtensionsNumerics
  {
    //public const string csMustBeAPositiveNumber = @"Must be a positive number.";

    /// <summary>Returns the number of bits in the minimal two's-complement representation of this BigInteger</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Bit-length"/>
    //public static int BitLength(this System.Numerics.BigInteger source)
    //{
    //  var length = 0;

    //  for (source = System.Numerics.BigInteger.Abs(source); source > 0; source >>= 1)
    //  {
    //    length++;
    //  }

    //  return length;
    //}
    /// <summary>Returns the number of bits used to represent the BigInteger. This implementation also returns the ones bit count as an out variable.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Bit-length"/>
    //public static int BitLength(this System.Numerics.BigInteger source, out int onesCount)
    //{
    //  onesCount = 0;

    //  var length = 0;

    //  for (source = System.Numerics.BigInteger.Abs(source); source > 0; source >>= 1)
    //  {
    //    onesCount += (int)(source & 1);

    //    length++;
    //  }

    //  return length;
    //}
    /// <summary>Returns the number of bits used to represent the BigInteger. This implementation also returns the ones bit count and the zero bit count as out variables.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Bit-length"/>
    //public static int BitLength(this System.Numerics.BigInteger source, out int onesCount, out int zeroesCount)
    //{
    //  zeroesCount = source.BitLength(out onesCount) is var length && length > 0 ? length - onesCount : 0;

    //  return length;
    //}

    /// <summary>Returns a number that is cropped between a minimum and a maximum number.</summary>
    //public static System.Numerics.BigInteger Constrain(this System.Numerics.BigInteger source, System.Numerics.BigInteger minimum, System.Numerics.BigInteger maximum) => source < minimum ? minimum : source > maximum ? maximum : source;

    //public static byte ConvertToByte(this System.Numerics.BigInteger source) => source < byte.MinValue ? byte.MinValue : source > byte.MaxValue ? byte.MaxValue : (byte)source;
    //public static decimal ConvertToDecimal(this System.Numerics.BigInteger source) => source < (System.Numerics.BigInteger)decimal.MinValue ? decimal.MinValue : source > (System.Numerics.BigInteger)decimal.MaxValue ? decimal.MaxValue : (decimal)source;
    //public static double ConvertToDouble(this System.Numerics.BigInteger source) => source < (System.Numerics.BigInteger)double.MinValue ? double.MinValue : source > (System.Numerics.BigInteger)double.MaxValue ? double.MaxValue : (double)source;
    //public static short ConvertToInt16(this System.Numerics.BigInteger source) => source < short.MinValue ? short.MinValue : source > short.MaxValue ? short.MaxValue : (short)source;
    //public static int ConvertToInt32(this System.Numerics.BigInteger source) => source < int.MinValue ? int.MinValue : source > int.MaxValue ? int.MaxValue : (int)source;
    //public static long ConvertToInt64(this System.Numerics.BigInteger source) => source < long.MinValue ? long.MinValue : source > long.MaxValue ? long.MaxValue : (long)source;
    //public static sbyte ConvertToSByte(this System.Numerics.BigInteger source) => source < sbyte.MinValue ? sbyte.MinValue : source > sbyte.MaxValue ? sbyte.MaxValue : (sbyte)source;
    //public static float ConvertToSingle(this System.Numerics.BigInteger source) => source < (System.Numerics.BigInteger)float.MinValue ? float.MinValue : source > (System.Numerics.BigInteger)float.MaxValue ? float.MaxValue : (float)source;
    //public static ushort ConvertToUInt16(this System.Numerics.BigInteger source) => source < ushort.MinValue ? ushort.MinValue : source > ushort.MaxValue ? ushort.MaxValue : (ushort)source;
    //public static uint ConvertToUInt32(this System.Numerics.BigInteger source) => source < uint.MinValue ? uint.MinValue : source > uint.MaxValue ? uint.MaxValue : (uint)source;
    //public static ulong ConvertToUInt64(this System.Numerics.BigInteger source) => source < ulong.MinValue ? ulong.MinValue : source > ulong.MaxValue ? ulong.MaxValue : (ulong)source;

    /// <summary>Count Leading Zeros (clz) counts the number of zero bits preceding the most significant one bit.</summary>
    /// <remarks>The bit position can easily be calculated by subtracting 1 from the resulting return value.</remarks>
    /// <param name="bitWidth">The number of bits in the set. E.g. 32, 64 or 128 for built-in integer data type sizes.</param>
    /// <see cref="https://en.wikipedia.org/wiki/Find_first_set#CLZ"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Bit-length"/>
    //public static System.Numerics.BigInteger CountLeadingZeros(this System.Numerics.BigInteger source, int bitWidth) => bitWidth - source.MostSignificant1Bit();

    /// <summary>Count Trailing Zeros (ctz) counts the number of zero bits succeeding the least significant one bit.</summary>
    /// <remarks>The bit position can easily be calculated by subtracting 1 from the resulting return value.</remarks>
    /// <remarks>This implementation is relatively fast.</remarks>
    /// <see cref="https://en.wikipedia.org/wiki/Find_first_set#CTZ"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Bit-length"/>
    //public static System.Numerics.BigInteger CountTrailingZeros(this System.Numerics.BigInteger source) => source.LeastSignificant1Bit() is var ls1b && ls1b > 0 ? ls1b - 1 : 0;

    /// <summary>Returns the highest decimal place value that is closest to the number. E.g. 10s, 100s, 1000s, 1000000s</summary>
    //public static System.Numerics.BigInteger DecimalPlaceValue(this System.Numerics.BigInteger source) => System.Numerics.BigInteger.Pow(10, (int)source.DigitCount(10) - 1);

    /// <summary>Returns the number of digits the number consists of in radix (base) 10.</summary>
    //public static System.Numerics.BigInteger DigitCount(this System.Numerics.BigInteger source) => source.DigitCount(10);
    /// <summary>Returns the number of digits the number consists of in the specified radix. The algorithm (floor(log(a) / log(b)) + 1) yields is the number of digits of a in radix b.</remarks>
    //public static System.Numerics.BigInteger DigitCount(this System.Numerics.BigInteger source, int radix)
    //{
    //  if (source < 0)
    //  {
    //    source = System.Numerics.BigInteger.Abs(source);
    //  }

    //  if (source > 0)
    //  {
    //    var count = 0;

    //    while (source > 0)
    //    {
    //      count++;

    //      source /= radix;
    //    }

    //    return count;
    //  }

    //  return 1;
    //}

    /// <summary>Returns the sum of all digits in the source number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Digit_sum"/>
    //public static System.Numerics.BigInteger DigitSum(this System.Numerics.BigInteger source) => source.ToString().Aggregate(System.Numerics.BigInteger.Zero, (accumulator, current) => accumulator + (current - 48));
    /// <summary>Returns the sum of all digits in the number, using the specified base.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Digit_sum"/>
    //public static System.Numerics.BigInteger DigitSum(this System.Numerics.BigInteger source, int radix) => source.ToRadixString(radix).Aggregate(System.Numerics.BigInteger.Zero, (accumulator, current) => accumulator + (current >= '0' && current <= '9' ? (current - 48) : current >= 'A' && current <= 'Z' ? (current - 'A') : current >= 'a' && current <= 'z' ? (current - 'a') : throw new System.NotSupportedException($"Unrecognized character '{current}'.")));

    /// <summary>Drop the leading digit of the number.</summary>
    //public static System.Numerics.BigInteger DropLeadingDigit(this System.Numerics.BigInteger source) => (source % (System.Numerics.BigInteger)System.Math.Pow(10, System.Math.Floor(System.Numerics.BigInteger.Log(source) / System.Math.Log(10))));
    /// <summary>Drop the trailing digit of the number.</summary>
    //public static System.Numerics.BigInteger DropTrailingDigit(this System.Numerics.BigInteger source) => (source / 10);

    /// <summary>Creates a new sequence of BigInteger starting at source and iterates a specified number of times. The sign of count indicates whether the sequence goes in the positive or negative direction.</summary>
    //public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> Enumerate(this System.Numerics.BigInteger source, System.Numerics.BigInteger count)
    //{
    //  var target = source + count;

    //  for (var sign = count.Sign; source != target; source += sign)
    //  {
    //    yield return source;
    //  }
    //}
    //public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> Enumerate(this System.Numerics.BigInteger source, System.Func<System.Numerics.BigInteger, System.Numerics.BigInteger> nextSelector)
    //{
    //  for(nextSelector(source))
    //  yield return source + stepCountSelector;
    //}
    /// <summary>Creates a new sequence of BigInteger values from startAt to the number (inclusive). Works in both positive and negative directions.</summary>
    //public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> EnumerateFrom(this System.Numerics.BigInteger source, System.Numerics.BigInteger startAt) => startAt.Enumerate((source - startAt) + 1);
    /// <summary>Creates a new sequence of BigInteger values from the number to the specified endWith. Works in both positive and negative directions.</summary>
    //public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> EnumerateTo(this System.Numerics.BigInteger source, System.Numerics.BigInteger endWith) => source.Enumerate((endWith - source) + 1);

    /// <summary>Returns the factorial (serial product) of the specified number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Factorial"/>
    /// <see cref="https://sites.google.com/site/examath/research/factorials"/>
    //public static System.Numerics.BigInteger Factorial(this System.Numerics.BigInteger source)
    //{
    //  var n = (source.IsEven ? source : source - 1);

    //  System.Numerics.BigInteger value = n, product = n;

    //  for (n -= 2; n >= 2; n -= 2)
    //  {
    //    value += n;

    //    product *= value;
    //  }

    //  if (!source.IsEven)
    //  {
    //    product *= source;
    //  }

    //  return product;
    //}

    /// <summary>This is the same as what is often refered to as "folding" upper bits to lower bits, i.e. set all bits less than the MSB to 1.</summary>
    /// <see cref="http://aggregate.org/MAGIC/"/>
    //public static System.Numerics.BigInteger FillBits(this System.Numerics.BigInteger source)
    //{
    //  if (source > 0)
    //  {
    //    for (var bit = 1; bit < source; bit <<= 1)
    //    {
    //      source |= (source >> bit);
    //    }

    //    return source;
    //  }
    //  else if (source == 0)
    //  {
    //    return default;
    //  }

    //  throw new System.ArgumentOutOfRangeException(nameof(source), source, csMustBeAPositiveNumber);
    //}

    /// <summary>Returns the greatest common divisor of all System.Numerics.BigInteger values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Greatest_common_divisor"/>
    //public static System.Numerics.BigInteger Gcd(this System.Numerics.BigInteger source, params System.Numerics.BigInteger[] values) => values.Aggregate(source, (accumulator, value) => System.Numerics.BigInteger.GreatestCommonDivisor(accumulator, value));

    /// <summary>Results in a sequence of divisors for the specified number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Divisor"/>
    //public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetDivisors(this System.Numerics.BigInteger source)
    //{
    //  yield return System.Numerics.BigInteger.One;
    //  yield return source;

    //  for (System.Numerics.BigInteger divisor = 2, cutoff = source; divisor < cutoff; divisor++)
    //  {
    //    if (System.Numerics.BigInteger.DivRem(source, divisor, out var remainder) is System.Numerics.BigInteger quotient && remainder == 0)
    //    {
    //      yield return divisor;

    //      if (quotient != divisor)
    //      {
    //        yield return quotient;
    //      }

    //      cutoff = quotient;
    //    }
    //  }
    //}

    /// <summary>Results in a new sequence of numbers representing the gaps between the numbers in the sequence, including the last and the first number.</summary>
    public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetGaps(this System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> source) => source.PartitionTuple(true, (leading, trailing, index) => trailing - leading);

    /// <summary>Returns a new sequence of ascending prime numbers greater than the specified number.</summary>
    //public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetNextPrimes(this System.Numerics.BigInteger source)
    //{
    //  for (var prime = source.NextPrime(); ; prime = prime.NextPrime())
    //  {
    //    yield return prime;
    //  }
    //}

    /// <summary>Creates a new sequence of numbers from the source in their respective ordinal position. All other positions returns -1.</summary>
    //public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetOrdinalRange(this System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> source)
    //{
    //  if (source.Distinct().OrderBy(bi => bi).GetEnumerator() is System.Collections.Generic.IEnumerator<System.Numerics.BigInteger> numbers && numbers.MoveNext())
    //  {
    //    var first = numbers.Current;

    //    while (numbers.MoveNext())
    //    {
    //      yield return first;

    //      for (var number = first + 1; number < numbers.Current; number++)
    //      {
    //        yield return -1;
    //      }

    //      first = numbers.Current;
    //    }

    //    yield return first;
    //  }
    //}
    /// <summary>Creates a new sequence of numbers in the range from startAt and count. Numbers in the source are returned in their respective ordinal position. All other positions returns -1.</summary>
    //public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetOrdinalRange(this System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> source, System.Numerics.BigInteger startWith, System.Numerics.BigInteger count)
    //{
    //  if (source.Distinct().Where(n => n >= startWith && n < (startWith + count)).OrderBy(bi => bi).GetEnumerator() is System.Collections.Generic.IEnumerator<System.Numerics.BigInteger> numbers && numbers.MoveNext() is bool movedNext)
    //  {
    //    for (var number = startWith; number < (startWith + count); number++)
    //    {
    //      if (movedNext && number == numbers.Current)
    //      {
    //        yield return number;

    //        movedNext = numbers.MoveNext();
    //      }
    //      else
    //      {
    //        yield return -1;
    //      }
    //    }
    //  }
    //}

    /// <summary>Returns a new sequence of ascending prime numbers greater than the specified number.</summary>
    //public static System.Collections.Generic.IEnumerable<System.Numerics.BigInteger> GetPreviousPrimes(this System.Numerics.BigInteger source)
    //{
    //  for (var prime = source.PreviousPrime(); prime < source; source = prime, prime = prime.PreviousPrime())
    //  {
    //    yield return prime;
    //  }
    //}

    /// <summary>Indicates whether the number is a congruent modulo.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Congruence_relation"/>
    //public static bool IsCongruentModulo(this System.Numerics.BigInteger source, int a, int d) => (source % a == d);

    /// <summary>Indicates whether this instance is jumbled (i.e. no neighboring digits having gaps larger than 1).</summary>
    /// <see cref="http://www.geeksforgeeks.org/check-if-a-number-is-jumbled-or-not/"/>
    //public static bool IsJumbled(this System.Numerics.BigInteger source)
    //{
    //  for (var quotient = source; quotient != 0 && quotient / 10 != 0;)
    //  {
    //    quotient = System.Numerics.BigInteger.DivRem(quotient, 10, out var remainder);

    //    if (System.Numerics.BigInteger.Abs((quotient % 10) - (remainder)) > 1) // if the previous digit minus current digit is greater than 1, then the number is not jumbled.
    //    {
    //      return false;
    //    }
    //  }

    //  return true;
    //}

    /// <summary>Determines whether  the number is a power of the specified exponent.</summary>
    //public static bool IsPowerOf(this System.Numerics.BigInteger source, int exponent)
    //{
    //  if (source > 1)
    //  {
    //    while (System.Numerics.BigInteger.DivRem(source, exponent, out var remainder) is var quotient && remainder == 0)
    //    {
    //      source = quotient;
    //    }
    //  }

    //  return source == 1;
    //}
    /// <summary>Checks if the specified integer is power of 2.</summary>
    //public static bool IsPowerOf2(this System.Numerics.BigInteger value) => (value > 0) ? ((value & (value - 1)) == 0) : false;

    /// <summary>Indicates whether a specified number is a prime.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Primality_test"/>
    /// <seealso cref="https://en.wikipedia.org/wiki/Prime_number"/>
    //public static bool IsPrime(this System.Numerics.BigInteger source)
    //{
    //  if (source <= 1)
    //  {
    //    return false;
    //  }
    //  else if (source <= 3)
    //  {
    //    return true;
    //  }
    //  else if (source % 2 == 0 || source % 3 == 0)
    //  {
    //    return false;
    //  }

    //  var root = source.Sqrt();

    //  for (System.Numerics.BigInteger i = 5; i <= root; i += 6)
    //  {
    //    if ((source % i) == 0 || (source % (i + 2)) == 0)
    //    {
    //      return false;
    //    }
    //  }

    //  return true;
    //}

    /// <summary>Indicates whether the specified number is a square of the specified root.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Square_root"/>
    //public static bool IsSqrt(this System.Numerics.BigInteger source, System.Numerics.BigInteger root) => (root * root is var lowerBound) && (lowerBound + root + root + 1 is var upperBound) ? (source >= lowerBound && source < upperBound) : throw new System.Exception();

    /// <summary>Returns the least common multiple of all System.Numerics.BigInteger values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    //public static System.Numerics.BigInteger Lcm(this System.Numerics.BigInteger source, params System.Numerics.BigInteger[] values) => values.Aggregate(source, (accumulator, value) => Numerics.BigInteger.LeastCommonMultiple(accumulator, value));

    /// <summary>The least significant bit (LSB) is the bit position in a binary integer giving the units value, that is, determining whether the number is even or odd. The LSB is sometimes referred to as the right-most bit, due to the convention in positional notation of writing less significant digits further to the right. Given a 2's complement binary integer value, (value & -value) is the least significant 1 bit.</summary>
    /// <remarks>This implementation is relatively fast for a BigInteger.</remarks>
    /// <returns>The bit value of the LSB that is set to one.</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Bit_numbering#Least_significant_bit"/>
    /// <seealso cref="http://aggregate.org/MAGIC/"/>
    //public static System.Numerics.BigInteger LeastSignificant1Bit(this System.Numerics.BigInteger source) => (source & -source);
    /// <summary>The least significant bit (LSB) is the bit position in a binary integer giving the units value, that is, determining whether the number is even or odd. The LSB is sometimes referred to as the right-most bit, due to the convention in positional notation of writing less significant digits further to the right. Given a 2's complement binary integer value, (value & -value) is the least significant 1 bit.</summary>
    /// <remarks>This implementation is relatively fast for a BigInteger.</remarks>
    /// <returns>The bit value of the LSB that is set to one.</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Bit_numbering#Least_significant_bit"/>
    /// <seealso cref="http://aggregate.org/MAGIC/"/>
    //public static System.Numerics.BigInteger LeastSignificant1Bit(this System.Numerics.BigInteger source, out int indexOf)
    //{
    //  if (source > 0)
    //  {
    //    var ls1b = source = (source & -source);

    //    for (indexOf = 0; source.IsEven; source >>= 1, indexOf++) ;

    //    return ls1b;
    //  }
    //  else if (source == 0)
    //  {
    //    indexOf = -1;

    //    return default;
    //  }

    //  throw new System.ArgumentOutOfRangeException(nameof(source), source, csMustBeAPositiveNumber);
    //}

    /// <summary>Returns the base 2 (or binary) logarithm of the number.</summary>
    /// <returns>The power of the number (i.e. base 2 or binary logarithm).</returns>
    //public static int Log2(this System.Numerics.BigInteger value)
    //{
    //  var index = 0;

    //  while ((value >>= 1) > 0)
    //  {
    //    index++;
    //  }

    //  return index;
    //}
    /// <summary>Returns the base 2 (or binary) logarithm and the exponent of the number.</summary>
    /// <returns>The power of the number (i.e. base 2 or binary logarithm) and the exponent as an out var.</returns>
    //public static int Log2(this System.Numerics.BigInteger value, out System.Numerics.BigInteger exponent)
    //{
    //  var index = 0;

    //  while ((value >>= 1) > 0)
    //  {
    //    index++;
    //  }

    //  exponent = System.Numerics.BigInteger.One << index;

    //  return index;
    //}

    ///// <summary>Canonical modulus. The result has the sign of the divisor.</summary>
    ///// <see cref="https://en.wikipedia.org/wiki/Modulo_operation"/>
    //public static System.Numerics.BigInteger Mod(this System.Numerics.BigInteger dividend, System.Numerics.BigInteger divisor) => ((dividend % divisor) + divisor) % divisor;
    ///// <summary>Results in a inverse modulo.</summary>
    ///// <see cref="https://en.wikipedia.org/wiki/Modular_multiplicative_inverse"/>
    //public static System.Numerics.BigInteger ModInv(this System.Numerics.BigInteger source, System.Numerics.BigInteger modulus)
    //{
    //  source %= modulus;

    //  for (var counter = System.Numerics.BigInteger.One; counter < modulus; counter++)
    //  {
    //    if ((source * counter) % modulus == 1)
    //    {
    //      return counter;
    //    }
    //  }

    //  throw new System.ArithmeticException();
    //}
    ///// <summary>Canonical modulus. The result has the sign of the divisor.</summary>
    ///// <see cref="https://en.wikipedia.org/wiki/Modulo_operation"/>
    //public static System.Numerics.BigInteger ModRem(System.Numerics.BigInteger dividend, System.Numerics.BigInteger divisor, out System.Numerics.BigInteger remainder) => ((remainder = dividend % divisor) + divisor) % divisor;

    /// <summary>The most significant bit (MSB, also called the high-order bit) is the bit position in a binary number having the greatest value. The MSB is sometimes referred to as the left-most bit due to the convention in positional notation of writing more significant digits further to the left.</summary>
    /// <returns>The bit value of the MSB that is set to one.</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Bit_numbering#Most_significant_bit"/>
    /// <seealso cref="http://aggregate.org/MAGIC/"/>
    //public static System.Numerics.BigInteger MostSignificant1Bit(this System.Numerics.BigInteger source)
    //{
    //  if (source > 0)
    //  {
    //    var ms1b = System.Numerics.BigInteger.One;

    //    while ((source >>= 1) > 0)
    //    {
    //      ms1b <<= 1;
    //    }

    //    return ms1b;
    //  }
    //  else if (source == 0)
    //  {
    //    return default;
    //  }

    //  throw new System.ArgumentOutOfRangeException(nameof(source), source, csMustBeAPositiveNumber);
    //}
    /// <summary>The most significant bit (MSB, also called the high-order bit) is the bit position in a binary number having the greatest value. The MSB is sometimes referred to as the left-most bit due to the convention in positional notation of writing more significant digits further to the left.</summary>
    /// <param name="indexOf">The index out parameter will contain the 0-based position/index of the MSB set to one, or -1 if no bits are set.</param>
    /// <returns>The bit value of the MSB that is set to one.</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Bit_numbering#Most_significant_bit"/>
    /// <seealso cref="http://aggregate.org/MAGIC/"/>
    //public static System.Numerics.BigInteger MostSignificant1Bit(this System.Numerics.BigInteger source, out int indexOf)
    //{
    //  if (source > 0)
    //  {
    //    indexOf = 0;

    //    while ((source >>= 1) > 0)
    //    {
    //      indexOf++;
    //    }

    //    return (System.Numerics.BigInteger.One << indexOf);
    //  }
    //  else if (source == 0)
    //  {
    //    indexOf = -1;

    //    return default;
    //  }

    //  throw new System.ArgumentOutOfRangeException(nameof(source), source, csMustBeAPositiveNumber);
    //}
    /// <summary>The most significant bit (MSB, also called the high-order bit) is the bit position in a binary number having the greatest value. The MSB is sometimes referred to as the left-most bit due to the convention in positional notation of writing more significant digits further to the left.</summary>
    /// <param name="indexOf">The index out parameter will contain the 0-based position/index of the MSB set to one, or -1 if no bits are set.</param>
    /// <returns>The bit value of the MSB that is set to one.</returns>
    /// <see cref="https://en.wikipedia.org/wiki/Bit_numbering#Most_significant_bit"/>
    /// <seealso cref="http://aggregate.org/MAGIC/"/>
    //public static System.Numerics.BigInteger MostSignificant1Bit(this System.Numerics.BigInteger source, out int indexOf, out int countOnes)
    //{
    //  if (source > 0)
    //  {
    //    for (countOnes = source.IsEven ? 0 : 1, indexOf = 0; (source >>= 1) > 0; indexOf++)
    //    {
    //      countOnes += source.IsEven ? 0 : 1;
    //    }

    //    return (System.Numerics.BigInteger.One << indexOf);
    //  }
    //  else if (source == 0)
    //  {
    //    countOnes = 0;

    //    indexOf = -1;

    //    return default;
    //  }

    //  throw new System.ArgumentOutOfRangeException(nameof(source), source, csMustBeAPositiveNumber);
    //}

    /// <summary>Returns the next larger even number.</summary>
    //public static System.Numerics.BigInteger NextEven(this System.Numerics.BigInteger source) => source + (source.IsEven ? 2 : 1);

    /// <summary>Determine the next larger power of specified radix value for the number.</summary>
    //public static System.Numerics.BigInteger NextLargestPowerOf(this System.Numerics.BigInteger source, int radix) => source.PowerOf(radix) * radix;
    /// <summary>Determine the next larger power of 2 value for the number. Given a binary integer value x, the next largest power of 2 can be computed by a SWAR algorithm that recursively "folds" the upper bits into the lower bits. This process yields a bit vector with the same most significant 1 as x, but all 1's below it. Adding 1 to that value yields the next largest power of 2.</summary>
    //public static System.Numerics.BigInteger NextLargestPowerOf2(this System.Numerics.BigInteger source)
    //{
    //  if (source < 0)
    //  {
    //    source = System.Numerics.BigInteger.Abs(source);
    //  }

    //  if (source > 0)
    //  {
    //    var bit = System.Numerics.BigInteger.One;

    //    while (bit <= source)
    //    {
    //      bit <<= 1;
    //    }

    //    return bit;
    //  }

    //  return 0;
    //}

    /// <summary>Returns the next larger odd number.</summary>
    //public static System.Numerics.BigInteger NextOdd(this System.Numerics.BigInteger source) => source + (source.IsEven ? 1 : 2);

    /// <summary>Returns the next prime number.</summary>
    //public static System.Numerics.BigInteger NextPrime(this System.Numerics.BigInteger source)
    //{
    //  if (source < 2)
    //  {
    //    return 2;
    //  }
    //  else if (source < 3)
    //  {
    //    return 3;
    //  }

    //  for (source += (source.IsEven ? 1 : 2); System.Numerics.BigInteger.ModPow(2, source, source) != 2 || System.Numerics.BigInteger.ModPow(3, source, source) != 3; source += 2) ;

    //  return source.IsPrime() ? source : NextPrime(source);
    //}

    /// <summary>Returns the specified radix raised to the power of the number.</summary>
    //public static System.Numerics.BigInteger PowerOf(this System.Numerics.BigInteger source, int radix) => source == 0 ? source : System.Numerics.BigInteger.Pow(radix, (int)source.DigitCount(radix) - 1);
    /// <summary>Returns the specified radix raised to the power of the number. It also returns the number of digits in the number.</summary>
    //public static System.Numerics.BigInteger PowerOf(this System.Numerics.BigInteger source, int radix, out int digits) => ((digits = (int)source.DigitCount(radix)) - 1) is var exponent && source == 0 ? source : System.Numerics.BigInteger.Pow(radix, exponent);

    /// <summary>Returns 2 raised to the power of the number.</summary>
    //public static System.Numerics.BigInteger PowerOf2(this System.Numerics.BigInteger source) => (source >= 0) ? (System.Numerics.BigInteger.One << (int)source) : 0;

    /// <summary>Returns the next smaller even number.</summary>
    //public static System.Numerics.BigInteger PreviousEven(this System.Numerics.BigInteger source) => source - (source.IsEven ? 2 : 1);

    /// <summary>Returns the next smaller odd number.</summary>
    //public static System.Numerics.BigInteger PreviousOdd(this System.Numerics.BigInteger source) => source - (source.IsEven ? 1 : 2);

    /// <summary>Returns the previous prime number.</summary>
    //public static System.Numerics.BigInteger PreviousPrime(this System.Numerics.BigInteger source)
    //{
    //  if (source > 5)
    //  {
    //    for (source -= (source.IsEven ? 1 : 2); System.Numerics.BigInteger.ModPow(2, source, source) != 2 || System.Numerics.BigInteger.ModPow(3, source, source) != 3; source -= 2) ;

    //    return source.IsPrime() ? source : PreviousPrime(source);
    //  }
    //  else if (source > 3)
    //  {
    //    return 3;
    //  }

    //  return 2;
    //}

    /// <summary>Determine the previous smaller power of specified radix value for the number.</summary>
    //public static System.Numerics.BigInteger PreviousSmallestPowerOf(this System.Numerics.BigInteger source, int radix) => source.PowerOf(radix) / radix;
    /// <summary>Determine the previous smaller power of 2 value for the number.</summary>
    //public static System.Numerics.BigInteger PreviousSmallestPowerOf2(this System.Numerics.BigInteger source)
    //{
    //  if (source < 0)
    //  {
    //    source = System.Numerics.BigInteger.Abs(source);
    //  }

    //  if (source > 0)
    //  {
    //    var bit = System.Numerics.BigInteger.One;

    //    while (bit <= source)
    //    {
    //      bit <<= 1;
    //    }

    //    return bit >> 2;
    //  }

    //  return 0;
    //}

    /// <summary>Computes the reverse bit mask of a number.</summary>
    //public static System.Numerics.BigInteger ReverseBits(this System.Numerics.BigInteger source)
    //{
    //  var target = System.Numerics.BigInteger.Zero;

    //  for (int sourceBitMask = 1, targetBitMask = 1 << (source.BitLength() - 1); targetBitMask > 0; sourceBitMask <<= 1, targetBitMask >>= 1)
    //  {
    //    if ((source & sourceBitMask) != 0)
    //    {
    //      target |= targetBitMask;
    //    }
    //  }

    //  return target;
    //}

    /// <summary>Returns the reverse of this instance, e.g. 13 becomes 31. This is a calculated reversal, not using strings.</summary>
    //public static System.Numerics.BigInteger ReverseDigits(this System.Numerics.BigInteger source)
    //{
    //  var reverse = System.Numerics.BigInteger.Zero;

    //  while (source > 0)
    //  {
    //    source = System.Numerics.BigInteger.DivRem(source, 10, out var remainder);

    //    reverse = (reverse * 10 + remainder);
    //  }

    //  return reverse;
    //}

    /// <summary>Returns the square root of the System.Numerics.BigInteger.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Square_root"/>
    //public static System.Numerics.BigInteger Sqrt(this System.Numerics.BigInteger source)
    //{
    //  if (source == 0)
    //  {
    //    return 0;
    //  }

    //  if (source > 0)
    //  {
    //    var bitLength = System.Convert.ToInt32(System.Math.Ceiling(System.Numerics.BigInteger.Log(source, 2)));

    //    var root = System.Numerics.BigInteger.One << (bitLength >> 1);

    //    while (!source.IsSqrt(root))
    //    {
    //      root += (source / root);

    //      root >>= 1;
    //    }

    //    return root;
    //  }

    //  throw new System.ArithmeticException();
    //}
    /// <summary>Returns the square root of a specified number, using the exponential identity method. This is an approximation with lesser accuracy the higher the number.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Methods_of_computing_square_roots"/>
    //public static double SqrtByExponentialIdentity(this System.Numerics.BigInteger source) => System.Math.Exp(System.Numerics.BigInteger.Log(source) / 2.0);
    /// <summary>Returns the square root of a specified number, using an implementation from Java.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Square_root"/>
    //public static System.Numerics.BigInteger SqrtByJavaAdoption(this System.Numerics.BigInteger source)
    //{
    //  if (source == 0)
    //  {
    //    return 0;
    //  }

    //  if (source > System.Numerics.BigInteger.Zero)
    //  {
    //    var bitLength = System.Convert.ToInt32(System.Math.Ceiling(System.Numerics.BigInteger.Log(source, 2)));

    //    var root = System.Numerics.BigInteger.One << (bitLength / 2);

    //    while (!source.IsSqrt(root))
    //    {
    //      root += source / root;

    //      root /= 2;
    //    }

    //    return root;
    //  }

    //  throw new System.ArithmeticException();
    //}

    //public static System.Numerics.BigInteger ToBigInteger(this byte source) => new System.Numerics.BigInteger(source);
    //public static System.Numerics.BigInteger ToBigInteger(this decimal source) => new System.Numerics.BigInteger(source);
    //public static System.Numerics.BigInteger ToBigInteger(this double source) => new System.Numerics.BigInteger(source);
    //public static System.Numerics.BigInteger ToBigInteger(this short source) => new System.Numerics.BigInteger(source);
    //public static System.Numerics.BigInteger ToBigInteger(this int source) => new System.Numerics.BigInteger(source);
    //public static System.Numerics.BigInteger ToBigInteger(this long source) => new System.Numerics.BigInteger(source);
    //public static System.Numerics.BigInteger ToBigInteger(this sbyte source) => new System.Numerics.BigInteger(source);
    //public static System.Numerics.BigInteger ToBigInteger(this float source) => new System.Numerics.BigInteger(source);
    //public static System.Numerics.BigInteger ToBigInteger(this ushort source) => new System.Numerics.BigInteger(source);
    //public static System.Numerics.BigInteger ToBigInteger(this uint source) => new System.Numerics.BigInteger(source);
    //public static System.Numerics.BigInteger ToBigInteger(this ulong source) => new System.Numerics.BigInteger(source);

    /// <summary>Returns a numeric string of the number formatted using number groupings.</summary>
    //public static string ToGroupString(this System.Numerics.BigInteger source) => source.ToString("#,###0");

    /// <summary>Returns the string formatted using the specified base, 2 for binary, 10 for decimal, 16 for hexadecimal, etc.</summary>
    //public static string ToRadixString(this System.Numerics.BigInteger source, int radix)
    //{
    //  switch (radix)
    //  {
    //    case int i when i == 10:
    //      return source.ToString();
    //    case int i when i == 16:
    //      return source.ToString("X");
    //    case int i when i >= 2 && i <= 61:
    //      var sb = new System.Text.StringBuilder();
    //      while (source > 0 && (source % radix) is System.Numerics.BigInteger remainder)
    //      {
    //        switch (remainder)
    //        {
    //          case var r when r <= 9:
    //            sb.Insert(0, (char)(r + '0'));
    //            break;
    //          case var r when r <= 35:
    //            sb.Insert(0, (char)((r - 10) + 'A'));
    //            break;
    //          case var r when r <= 61:
    //            sb.Insert(0, (char)((r - 36) + 'a'));
    //            break;
    //        }

    //        source /= radix;
    //      }
    //      return sb.ToString();
    //    default:
    //      throw new System.NotSupportedException(string.Format("Base {0} is not supported, only base 2-61 is supported (0-9, A-Z, a-z).", radix));
    //  }
    //}

    /// <summary>Returns a value that is wrapped (overflowed) around a minimum and a maximum number.</summary>
    //public static System.Numerics.BigInteger Wrap(this System.Numerics.BigInteger source, System.Numerics.BigInteger minimum, System.Numerics.BigInteger maximum) => (source < minimum || source > maximum) && (maximum - minimum) is var range ? ((((source - minimum) % range) + range) % range + minimum) : source;
  }
}

namespace Flux.Numerics
{
  public static partial class BigInteger
  {
    //public static readonly System.Numerics.BigInteger DecimalMaxValue = (System.Numerics.BigInteger)decimal.MaxValue;
    //public static readonly System.Numerics.BigInteger DecimalMinValue = (System.Numerics.BigInteger)decimal.MinValue;

    //public static readonly System.Numerics.BigInteger DoubleMaxValue = (System.Numerics.BigInteger)double.MaxValue;
    //public static readonly System.Numerics.BigInteger DoubleMinValue = (System.Numerics.BigInteger)double.MinValue;

    //public static readonly System.Numerics.BigInteger SingleMaxValue = (System.Numerics.BigInteger)float.MaxValue;
    //public static readonly System.Numerics.BigInteger SingleMinValue = (System.Numerics.BigInteger)float.MinValue;

    /// <summary>Returns the least common multiple of two System.Numerics.BigInteger values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    //public static System.Numerics.BigInteger LeastCommonMultiple(System.Numerics.BigInteger a, System.Numerics.BigInteger b)
    //{
    //  for (var i = b; b % a != 0; b += i)
    //  {
    //    ;
    //  }

    //  return b;
    //}

    ///// <summary>Optimized routines for 2 values.</summary>
    //public static System.Numerics.BigInteger Max(System.Numerics.BigInteger a, System.Numerics.BigInteger b) => a > b ? a : b;
    ///// <summary>Optimized routines for 3 values.</summary>
    //public static System.Numerics.BigInteger Max(System.Numerics.BigInteger a, System.Numerics.BigInteger b, System.Numerics.BigInteger c) => (a > b) ? (a > c ? a : c) : (b > c ? b : c);
    ///// <summary>Optimized routines for 4 values.</summary>
    //public static System.Numerics.BigInteger Max(System.Numerics.BigInteger a, System.Numerics.BigInteger b, System.Numerics.BigInteger c, System.Numerics.BigInteger d) => (a > b) ? (a > c ? (a > d ? a : d) : (c > d ? c : d)) : (b > c ? (b > d ? b : d) : (c > d ? c : d));

    ///// <summary>Optimized routines for 2 values.</summary>
    //public static System.Numerics.BigInteger Min(System.Numerics.BigInteger a, System.Numerics.BigInteger b) => a < b ? a : b;
    ///// <summary>Optimized routines for 3 values.</summary>
    //public static System.Numerics.BigInteger Min(System.Numerics.BigInteger a, System.Numerics.BigInteger b, System.Numerics.BigInteger c) => (a < b) ? (a < c ? a : c) : (b < c ? b : c);
    ///// <summary>Optimized routines for 4 values.</summary>
    //public static System.Numerics.BigInteger Min(System.Numerics.BigInteger a, System.Numerics.BigInteger b, System.Numerics.BigInteger c, System.Numerics.BigInteger d) => (a < b) ? (a < c ? (a < d ? a : d) : (c < d ? c : d)) : (b < c ? (b < d ? b : d) : (c < d ? c : d));

    public static class BinaryToText
    {
      #region Character Codes
      public static readonly char[] NumericAlpha = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };

      /// <summary>Base 2</summary>
      public static readonly char[] Binary = NumericAlpha.Take(2).ToArray();
      /// <summary>Base 3</summary>
      public static readonly char[] Ternary = NumericAlpha.Take(3).ToArray();
      /// <summary>Base 4</summary>
      public static readonly char[] Quaternary = NumericAlpha.Take(4).ToArray();
      /// <summary>Base 5</summary>
      public static readonly char[] Quinary = NumericAlpha.Take(5).ToArray();
      /// <summary>Base 6</summary>
      public static readonly char[] Senary = NumericAlpha.Take(6).ToArray();
      /// <summary>Base 8</summary>
      public static readonly char[] Octal = NumericAlpha.Take(8).ToArray();
      /// <summary>Base 10</summary>
      public static readonly char[] Decimal = NumericAlpha.Take(10).ToArray();
      /// <summary>Base 12</summary>
      public static readonly char[] Duodecimal = NumericAlpha.Take(12).ToArray();
      /// <summary>Base 16</summary>
      public static readonly char[] Hexadecimal = NumericAlpha.Take(16).ToArray();
      /// <summary>Base 20</summary>
      public static readonly char[] Vigesimal = NumericAlpha.Take(20).ToArray();
      /// <summary>Duotrigesimal (https://en.wikipedia.org/wiki/Base32)</summary>
      public static readonly char[] Base32 = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', '2', '3', '4', '5', '6', '7' };
      /// <summary>Duotrigesimal (https://en.wikipedia.org/wiki/Natural_Area_Code)</summary>
      public static readonly char[] NaturalAreaCode = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'B', 'C', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'V', 'W', 'X', 'Z' };
      /// <summary>Base 36</summary>
      public static readonly char[] Hexatrigesimal = NumericAlpha.Take(36).ToArray();
      /// <summary>Tetrasexagesimal (https://en.wikipedia.org/wiki/Base64)</summary>
      public static readonly char[] Base64 = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '+', '/' };
      /// <summary>Pentoctogesimal (https://en.wikipedia.org/wiki/Ascii85)</summary>
      public static readonly char[] Ascii85 = System.Linq.Enumerable.Range(32, 85).Select(i => System.Convert.ToChar(i)).ToArray();
      /// <summary>Base 95 (https://en.wikipedia.org/wiki/ASCII)</summary>
      public static readonly char[] PrintableAscii = System.Linq.Enumerable.Range(32, 95).Select(i => System.Convert.ToChar(i)).ToArray();
      /// <summary>Base 94 (https://en.wikipedia.org/wiki/ASCII)</summary>
      public static readonly char[] PrintableAsciiNoSpace = PrintableAscii.Skip(1).ToArray();
      /// <summary>Base 94 (https://en.wikipedia.org/wiki/Base64#Radix-64_applications_not_compatible_with_Base64)</summary>
      public static readonly char[] Uuencoding = System.Linq.Enumerable.Range(32, 64).Select(i => System.Convert.ToChar(i)).ToArray();
      #endregion

      public static string QuotedPrintable(byte value) => value >= 33 && value <= 126 && value != 61 ? new string(System.Text.Encoding.UTF8.GetChars(new byte[] { value })) : value.ToString("X2");

      public static string Encode(System.Numerics.BigInteger number, char[] baseCharacters)
      {
        var sb = new System.Text.StringBuilder(64);

        if (number.IsZero)
        {
          sb.Append('0');
        }
        else if (System.Numerics.BigInteger.Abs(number) is System.Numerics.BigInteger abs)
        {
          for (; abs != 0; abs /= baseCharacters.Length)
          {
            System.Numerics.BigInteger.DivRem(abs, baseCharacters.Length, out var remainder);

            sb.Insert(0, baseCharacters[(int)remainder]);
          }

          if (number.Sign < 0)
          {
            sb.Insert(0, '-');
          }
        }

        return sb.ToString();
      }

      public static System.Numerics.BigInteger Decode(string number, char[] baseCharacters)
      {
        var result = System.Numerics.BigInteger.Zero;

        if (!System.Text.RegularExpressions.Regex.IsMatch(number, "^-?0?$") && number.Replace(@"-", string.Empty, System.StringComparison.Ordinal) is string s && s.Length > 0)
        {
          var multiplier = System.Numerics.BigInteger.Pow(baseCharacters.Length, s.Length - 1);

          foreach (var c in s)
          {
            result += System.Array.FindIndex(baseCharacters, bc => bc == c) * multiplier;

            multiplier /= baseCharacters.Length;
          }

          if (number[0] == '-')
          {
            result *= -1;
          }
        }

        return result;
      }
    }
  }
}
