using System.Linq;

namespace Flux
{
  public static partial class Math
  {
    /// <summary>Represents the largest prime number possible in a byte (unsigned).</summary>
    public const byte LargestPrimeByte = 251;
    /// <summary>Represents the largest prime number possible in a 16-bit integer.</summary>
    public const short LargestPrimeInt16 = 32749;
    /// <summary>Represents the largest prime number possible in a 32-bit integer.</summary>
    public const int LargestPrimeInt32 = 2147483647;
    /// <summary>Represents the largest prime number possible in a 64-bit integer.</summary>
    public const long LargestPrimeInt64 = 9223372036854775783;
    /// <summary>Represents the largest prime number possible in a signed byte.</summary>
    [System.CLSCompliant(false)]
    public const sbyte LargestPrimeSByte = 127;
    /// <summary>Represents the largest prime number possible in a 16-bit unsigned integer.</summary>
    [System.CLSCompliant(false)]
    public const ushort LargestPrimeUInt16 = 65521;
    /// <summary>Represents the largest prime number possible in a 32-bit unsigned integer.</summary>
    [System.CLSCompliant(false)]
    public const uint LargestPrimeUInt32 = 4294967291;
    /// <summary>Represents the largest prime number possible in a 64-bit unsigned integer.</summary>
    [System.CLSCompliant(false)]
    public const ulong LargestPrimeUInt64 = 18446744073709551557;

    /// <summary>Represents the smallest prime number.</summary>
    public const int SmallestPrime = 2;
  }
}
