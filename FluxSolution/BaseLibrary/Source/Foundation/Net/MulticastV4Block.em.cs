namespace Flux
{
  public static partial class MulticastV4BlockEm
  {
    /// <summary>Creates a new sequence of all IP addresses in the specified MulticastV4 block.</summary>
    public static System.Collections.Generic.IEnumerable<System.Net.IPAddress> GetAllIPAddresses(this Net.IPv4.MulticastV4Block source)
      => source.GetMinIPAddress().GetRange(source.GetMaxIPAddress());

    /// <summary>Returns the maximum IP address in the specified MulticastV4 block.</summary>
    public static System.Net.IPAddress GetMaxIPAddress(this Net.IPv4.MulticastV4Block source)
      => new(BitOps.ReverseBytes((uint)GetMaxValue(source)));

    /// <summary>Returns the maximum address (big endian) in the specified MulticastV4 block.</summary>
    public static long GetMaxValue(this Net.IPv4.MulticastV4Block source)
      => (uint)unchecked((ulong)source & 0xFFFFFFFFU);

    /// <summary>Returns the minimum IP address in the specified MulticastV4 block.</summary>
    public static System.Net.IPAddress GetMinIPAddress(this Net.IPv4.MulticastV4Block source)
      => new(BitOps.ReverseBytes((uint)GetMinValue(source)));

    /// <summary>Returns the minimum address (big endian) in the specified MulticastV4 block.</summary>
    public static long GetMinValue(this Net.IPv4.MulticastV4Block source)
      => (uint)unchecked((ulong)source >> 32);
  }

  namespace Net
  {
    public static partial class IPv4
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

      /// <summary>Determintes whether the address is a multicast address. Works on both IPv4 and IPv6.</summary>
      public static bool IsMulticast(this System.Net.IPAddress source)
      {
        if (source is null) throw new System.ArgumentNullException(nameof(source));

        if (source.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
          return (source.GetAddressBytes()[0] & 0xF0) == 0xE0;

        if (source.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
          return source.GetAddressBytes()[0] == 0xFF;

        return false;
      }
    }
  }
}
