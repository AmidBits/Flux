#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class GenericMath
  {
    /// <summary>PREVIEW! Reverse the digits of <paramref name="x"/> using base <paramref name="b"/>, obtaining a new number.</summary>
    public static TSelf ReverseDigits<TSelf>(this TSelf x, TSelf b)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      AssertRadix(b);

      var reverse = TSelf.Zero;

      while (!TSelf.IsZero(x))
      {
        reverse = reverse * b + (x % b);

        x /= b;
      }

      return reverse;
    }
  }
}
#endif
