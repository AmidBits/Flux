#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class ExtensionMethods
  {
    /// <summary>Returns the greatest common divisor of a and b.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/Greatest_common_divisor"/>
    public static TSelf GreatestCommonDivisor<TSelf>(this TSelf a, TSelf b)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>
    {
      while (b != TSelf.Zero)
      {
        var t = b;
        b = a % b;
        a = t;
      }

      return TSelf.Abs(a);
    }
  }
}
#endif
