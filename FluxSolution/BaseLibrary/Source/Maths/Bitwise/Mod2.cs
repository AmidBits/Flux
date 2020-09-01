namespace Flux
{
  public static partial class Bitwise
  {
    /// <summary>Calculates the mod of a power of 2 specified by the index.</summary>
    public static System.Numerics.BigInteger Mod2(System.Numerics.BigInteger value, int powerOf2Index)
      => value & (Pow2(powerOf2Index) - 1);
  }
}
