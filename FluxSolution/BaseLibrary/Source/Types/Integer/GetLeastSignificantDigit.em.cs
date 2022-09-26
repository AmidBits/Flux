namespace Flux
{
  public static partial class IntEm
  {
    public static System.Numerics.BigInteger GetLeastSignificantDigit(System.Numerics.BigInteger source, int radix = 10)
      => source % radix;

    public static int GetLeastSignificantDigit(int source, int radix = 10)
      => source % radix;

    public static long GetLeastSignificantDigit(long source, int radix = 10)
        => source % radix;
  }
}
