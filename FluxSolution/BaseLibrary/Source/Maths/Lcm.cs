

using System.Linq;

namespace Flux
{
  public static partial class Maths
  {

    
    /// <summary>Returns the least common multiple of all (and at least two) values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    public static System.Numerics.BigInteger LcmX(params System.Numerics.BigInteger[] values)
      => (values?.Length ?? throw new System.ArgumentNullException(nameof(values))) >= 2 ? values.Skip(1).Aggregate(values[0], LeastCommonMultipleX) : throw new System.ArgumentOutOfRangeException(nameof(values));

    /// <summary>Returns the least common multiple of two System.Numerics.BigInteger values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    public static System.Numerics.BigInteger LeastCommonMultipleX(System.Numerics.BigInteger a, System.Numerics.BigInteger b)
    {
      if (a < 0) throw new System.ArgumentOutOfRangeException(nameof(a));
      if (b < 0) throw new System.ArgumentOutOfRangeException(nameof(b));

      var i = b;
      while (b % a != 0)
        b += i;
      return b;
    }

    
    /// <summary>Returns the least common multiple of all (and at least two) values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    public static System.Int32 LcmX(params System.Int32[] values)
      => (values?.Length ?? throw new System.ArgumentNullException(nameof(values))) >= 2 ? values.Skip(1).Aggregate(values[0], LeastCommonMultipleX) : throw new System.ArgumentOutOfRangeException(nameof(values));

    /// <summary>Returns the least common multiple of two System.Numerics.BigInteger values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    public static System.Int32 LeastCommonMultipleX(System.Int32 a, System.Int32 b)
    {
      if (a < 0) throw new System.ArgumentOutOfRangeException(nameof(a));
      if (b < 0) throw new System.ArgumentOutOfRangeException(nameof(b));

      var i = b;
      while (b % a != 0)
        b += i;
      return b;
    }

    
    /// <summary>Returns the least common multiple of all (and at least two) values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    public static System.Int64 LcmX(params System.Int64[] values)
      => (values?.Length ?? throw new System.ArgumentNullException(nameof(values))) >= 2 ? values.Skip(1).Aggregate(values[0], LeastCommonMultipleX) : throw new System.ArgumentOutOfRangeException(nameof(values));

    /// <summary>Returns the least common multiple of two System.Numerics.BigInteger values.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Least_common_multiple"/>
    public static System.Int64 LeastCommonMultipleX(System.Int64 a, System.Int64 b)
    {
      if (a < 0) throw new System.ArgumentOutOfRangeException(nameof(a));
      if (b < 0) throw new System.ArgumentOutOfRangeException(nameof(b));

      var i = b;
      while (b % a != 0)
        b += i;
      return b;
    }

    
  }
}
