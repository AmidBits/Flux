namespace Flux
{
  public static partial class BitOps
  {
    /// <summary>Computes the greater power of 2 for the specified number.</summary>
    public static System.Numerics.BigInteger RoundToNearestPowerOf2(this System.Numerics.BigInteger value, out System.Numerics.BigInteger greaterThan, out System.Numerics.BigInteger lessThan)
    {
      greaterThan = FoldRight(value - 1) + 1;
      lessThan = greaterThan >> 1;

      return (greaterThan - value) > (value - lessThan) ? lessThan : greaterThan;
    }

    /// <summary>Computes the greater power of 2 for the specified number.</summary>
    public static int RoundToNearestPowerOf2(this int value, out int greaterThan, out int lessThan)
    {
      greaterThan = FoldRight(value - 1) + 1;
      lessThan = greaterThan >> 1;

      return (greaterThan - value) > (value - lessThan) ? lessThan : greaterThan;
    }

    /// <summary>Computes the greater power of 2 for the specified number.</summary>
    public static long RoundToNearestPowerOf2(this long value, out long greaterThan, out long lessThan)
    {
      greaterThan = FoldRight(value - 1) + 1;
      lessThan = greaterThan >> 1;

      return (greaterThan - value) > (value - lessThan) ? lessThan : greaterThan;
    }

    /// <summary>Computes the greater power of 2 for the specified number.</summary>
    [System.CLSCompliant(false)]
    public static uint RoundToNearestPowerOf2(this uint value, out uint greaterThan, out uint lessThan)
    {
      greaterThan = FoldRight(value - 1) + 1;
      lessThan = greaterThan >> 1;

      return (greaterThan - value) > (value - lessThan) ? lessThan : greaterThan;
    }

    /// <summary>Computes the greater power of 2 for the specified number.</summary>
    [System.CLSCompliant(false)]
    public static ulong RoundToNearestPowerOf2(this ulong value, out ulong greaterThan, out ulong lessThan)
    {
      greaterThan = FoldRight(value - 1) + 1;
      lessThan = greaterThan >> 1;

      return (greaterThan - value) > (value - lessThan) ? lessThan : greaterThan;
    }
  }
}
