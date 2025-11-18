namespace Flux
{
  // https://en.wikipedia.org/wiki/Multicast_address
  public enum MulticastV4Block
    : long
  {
    LocalSubnetwork = unchecked((long)0xE0000000E00000FF),
    InternetworkControl = unchecked((long)0xE0000100E00001FF),
    AdHocBlock1 = unchecked((long)0xE0000200E000FFFF),
    Reserved1 = unchecked((long)0xE0010000E001FFFF),
    AdHocBlock2 = unchecked((long)0xE0030000E004FFFF),
    Reserved2 = unchecked((long)0xE1000000E7FFFFFF),
    SourceSpecificMulticast = unchecked((long)0xE8000000E8FFFFFF),
    GlopAddressing = unchecked((long)0xE9000000E9FBFFFF),
    AdHocBlock3 = unchecked((long)0xE9FC0000E9FFFFFF),
    UnicastPrefixBased = unchecked((long)0xEA000000EAFFFFFF),
    Reserved3 = unchecked((long)0xEB000000EEFFFFFF),
    AdministrativelyScoped = unchecked((long)0xEF000000EFFFFFFF),
  }

  public static partial class MulticastV4BlockExtensions
  {
    /// <summary>Creates a new sequence of all IP addresses in the specified MulticastV4 block.</summary>
    public static System.Collections.Generic.IEnumerable<System.Net.IPAddress> GetAllIPAddresses(this MulticastV4Block source)
      => source.GetMinIPAddress().GetRange(source.GetMaxIPAddress());

    /// <summary>Returns the maximum IP address in the specified MulticastV4 block.</summary>
    public static System.Net.IPAddress GetMaxIPAddress(this MulticastV4Block source)
      => new(GetMaxValue(source).ReverseBytes());

    /// <summary>Returns the maximum address (big endian) in the specified MulticastV4 block.</summary>
    public static long GetMaxValue(this MulticastV4Block source)
      => (uint)unchecked((ulong)source & 0xFFFFFFFFU);

    /// <summary>Returns the minimum IP address in the specified MulticastV4 block.</summary>
    public static System.Net.IPAddress GetMinIPAddress(this MulticastV4Block source)
      => new(GetMinValue(source).ReverseBytes());

    /// <summary>Returns the minimum address (big endian) in the specified MulticastV4 block.</summary>
    public static long GetMinValue(this MulticastV4Block source)
      => (uint)unchecked((ulong)source >> 32);
  }
}
