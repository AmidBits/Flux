namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>Computes the next power of 2 greater than or optionally equal to the specified number. This is equivalent to (MSB << 1).</summary>
    [System.CLSCompliant(false)]
    public static uint NextHighestPowerOf2(uint value)
      => FoldRight(value) + 1;
    /// <summary>Computes the next power of 2 greater than or optionally equal to the specified number. This is equivalent to (MSB << 1).</summary>
    [System.CLSCompliant(false)]
    public static ulong NextHighestPowerOf2(ulong value)
      => FoldRight(value) + 1;
  }
}
