using System.Linq;

namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Returns the least common multiple of all (and at least two) values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    public static System.Numerics.BigInteger Lcm(params System.Numerics.BigInteger[] values)
      => (values?.Length ?? throw new System.ArgumentNullException(nameof(values))) >= 2 ? values.Skip(1).Aggregate(values[0], LeastCommonMultiple) : throw new System.ArgumentOutOfRangeException(nameof(values));

    /// <summary>Returns the least common multiple of all (and at least two) values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    public static int Lcm(params int[] values)
      => (values?.Length ?? throw new System.ArgumentNullException(nameof(values))) >= 2 ? values.Skip(1).Aggregate(values[0], LeastCommonMultiple) : throw new System.ArgumentOutOfRangeException(nameof(values));
    /// <summary>Returns the least common multiple of all (and at least two) values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    public static long Lcm(params long[] values)
      => (values?.Length ?? throw new System.ArgumentNullException(nameof(values))) >= 2 ? values.Skip(1).Aggregate(values[0], LeastCommonMultiple) : throw new System.ArgumentOutOfRangeException(nameof(values));

    /// <summary>Returns the least common multiple of all (and at least two) values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    [System.CLSCompliant(false)]
    public static uint Lcm(params uint[] values)
      => (values?.Length ?? throw new System.ArgumentNullException(nameof(values))) >= 2 ? values.Skip(1).Aggregate(values[0], LeastCommonMultiple) : throw new System.ArgumentOutOfRangeException(nameof(values));
    /// <summary>Returns the least common multiple of all (and at least two) values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    [System.CLSCompliant(false)]
    public static ulong Lcm(params ulong[] values)
      => (values?.Length ?? throw new System.ArgumentNullException(nameof(values))) >= 2 ? values.Skip(1).Aggregate(values[0], LeastCommonMultiple) : throw new System.ArgumentOutOfRangeException(nameof(values));

    /// <summary>Returns the least common multiple of the two specified values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    public static System.Numerics.BigInteger LeastCommonMultiple(System.Numerics.BigInteger a, System.Numerics.BigInteger b)
      => a / System.Numerics.BigInteger.GreatestCommonDivisor(a, b) * b;
    //{
    //  if (a < 0) throw new System.ArgumentOutOfRangeException(nameof(a));
    //  if (b < 0) throw new System.ArgumentOutOfRangeException(nameof(b));

    //  var i = b;
    //  while (b % a != 0)
    //    b += i;
    //  return b;
    //}

    /// <summary>Returns the least common multiple of the two specified values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    public static int LeastCommonMultiple(int a, int b)
      => a / GreatestCommonDivisor(a, b) * b;
    //{
    //  if (a < 0) throw new System.ArgumentOutOfRangeException(nameof(a));
    //  if (b < 0) throw new System.ArgumentOutOfRangeException(nameof(b));

    //  var i = b;
    //  while (b % a != 0)
    //    b += i;
    //  return b;
    //}
    /// <summary>Returns the least common multiple of the two specified values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    public static long LeastCommonMultiple(long a, long b)
      => a / GreatestCommonDivisor(a, b) * b;
    //{
    //  if (a < 0) throw new System.ArgumentOutOfRangeException(nameof(a));
    //  if (b < 0) throw new System.ArgumentOutOfRangeException(nameof(b));

    //  var i = b;
    //  while (b % a != 0)
    //    b += i;
    //  return b;
    //}

    /// <summary>Returns the least common multiple of the two specified values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    [System.CLSCompliant(false)]
    public static uint LeastCommonMultiple(uint a, uint b)
      => a / GreatestCommonDivisor(a, b) * b;
    //{
    //  var i = b;
    //  while (b % a != 0)
    //    b += i;
    //  return b;
    //}
    /// <summary>Returns the least common multiple of the two specified values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    [System.CLSCompliant(false)]
    public static ulong LeastCommonMultiple(ulong a, ulong b)
      => a / GreatestCommonDivisor(a, b) * b;
    //{
    //  var i = b;
    //  while (b % a != 0)
    //    b += i;
    //  return b;
    //}
  }
}
