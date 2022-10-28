#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Returns the individual digits (as numbers) of <paramref name="number"/> using base <paramref name="radix"/>.</summary>
    public static System.Span<TSelf> GetDigits<TSelf>(this TSelf number, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      var reversed = GetDigitsReversed(number, radix);
      reversed.Reverse();
      return reversed;
    }

    /// <summary>PREVIEW! Returns the place value digits (as numbers) of <paramref name="number"/> using base <paramref name="radix"/>, in reverse order.</summary>
    public static System.Span<TSelf> GetDigitsReversed<TSelf>(this TSelf number, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      AssertRadix(radix);

      var list = new System.Collections.Generic.List<TSelf>();

      if (TSelf.IsZero(number))
        list.Add(TSelf.Zero);
      else
        while (!TSelf.IsZero(number))
        {
          list.Add(number % radix);
          number /= radix;
        }

      return System.Runtime.InteropServices.CollectionsMarshal.AsSpan(list);
    }
  }
}
#endif
