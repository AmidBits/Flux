namespace Flux
{
  public static partial class ExtensionMethods
  {
    public static System.Numerics.BigInteger GetLeastSignificantDigit(this System.Numerics.BigInteger source, int radix = 10)
      => source % radix;

    public static int GetLeastSignificantDigit(this int source, int radix = 10)
      => source % radix;

    public static long GetLeastSignificantDigit(this long source, int radix = 10)
        => source % radix;
  }
}
