namespace Flux
{
  public static partial class Maths
  {
    /// <summary>Returns the place value digits (as numbers) of a value, in reverse order.</summary>
    public static System.Span<System.Numerics.BigInteger> GetDigitsReversed(this System.Numerics.BigInteger value, int radix)
    {
      AssertRadix(radix);

      var list = new System.Collections.Generic.List<System.Numerics.BigInteger>();

      while (value != 0)
      {
        list.Add(value % radix);
        value /= radix;
      }

      return System.Runtime.InteropServices.CollectionsMarshal.AsSpan(list);
    }

    /// <summary>Returns the place value digits (as numbers) of a value, in reverse order.</summary>
    public static System.Span<int> GetDigitsReversed(this int value, int radix)
    {
      AssertRadix(radix);

      var list = new System.Collections.Generic.List<int>();

      while (value != 0)
      {
        list.Add(value % radix);
        value /= radix;
      }

      return System.Runtime.InteropServices.CollectionsMarshal.AsSpan(list);
    }
    /// <summary>Returns the place value digits (as numbers) of a value, in reverse order.</summary>
    public static System.Span<long> GetDigitsReversed(this long value, int radix)
    {
      AssertRadix(radix);

      var list = new System.Collections.Generic.List<long>();

      while (value != 0)
      {
        list.Add(value % radix);
        value /= radix;
      }

      return System.Runtime.InteropServices.CollectionsMarshal.AsSpan(list);
    }
  }
}
