#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class Radix
  {
    /// <summary>PREVIEW! Returns the digits (as numbers) of a value.</summary>
    public static System.Span<TSelf> GetDigits<TSelf>(this TSelf value, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
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
  }
}
#endif
