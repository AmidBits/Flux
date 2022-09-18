#if NET7_0_OR_GREATER
namespace Flux
{
  public static partial class BinaryInteger
  {
    /// <summary>PREVIEW! Drop the leading digit of the number.</summary>
    public static TSelf DropLeadingDigit<TSelf>(this TSelf source, TSelf radix)
      where TSelf : System.Numerics.IBinaryInteger<TSelf>, System.Numerics.IPowerFunctions<TSelf>
      => DigitCount(source, radix) is var dc && dc <= TSelf.One ? TSelf.Zero : source % TSelf.Pow(radix, dc - TSelf.One);
  }
}
#endif
