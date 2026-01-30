namespace Flux
{
  /// <summary>
  /// <para>All IPv4 blocks where each entry is the start and end address combined in a <see cref="long"/>.</para>
  /// <para>The most-significant 32-bits is a start address and the least-significant 32-bits is an end address.</para>
  /// <para><see href="https://en.wikipedia.org/wiki/Multicast_address"/></para>
  /// </summary>
  public enum MulticastV4Block
    : long
  {
    LocalSubnetwork = unchecked((long)0xff0000e0000000e0UL), // 224.0.0.0 to 224.0.0.255
    InternetworkControl = unchecked((long)0xff0100e0000100e0UL), // 224.0.1.0 to 224.0.1.255
    AdHocBlock1 = unchecked((long)0xffff00e0000200e0UL), // 224.0.2.0 to 224.0.255.255
    Reserved1 = unchecked((long)0xffff01e0000001e0UL), // 224.1.0.0 to 224.1.255.255
    SdpSapBlock = unchecked((long)0xffff02e0000002e0), // 224.2.0.0 to 224.2.255.255
    AdHocBlock2 = unchecked((long)0xffff04e0000003e0UL), // 224.3.0.0 to 224.4.255.255
    Reserved2 = unchecked((long)0xffffffe0000005e0UL), // 224.5.0.0 to 224.255.255.255
    Reserved3 = unchecked((long)0xffffffe7000000e1UL), // 225.0.0.0 to 231.255.255.255
    SourceSpecificMulticast = unchecked((long)0xffffffe8000000e8UL), // 232.0.0.0 to 232.255.255.255
    GlopAddressing = unchecked((long)0xfffffbe9000000e9UL), // 233.0.0.0 to 233.251.255.255
    AdHocBlock3 = unchecked((long)0xffffffe90000fce9UL), // 233.252.0.0 to 233.255.255.255
    UnicastPrefixBased = unchecked((long)0xffffffea000000eaUL), // 234.0.0.0 to 234.255.255.255
    Reserved4 = unchecked((long)0xffffffee000000ebUL), // 235.0.0.0 to 238.255.255.255
    AdministrativelyScoped = unchecked((long)0xffffffef000000efUL), // 239.0.0.0 to 239.255.255.255
  }

  public static partial class MulticastV4BlockExtensions
  {
    /// <summary>
    /// <para>Returns the minimum and maximum IP address in the specified MulticastV4 block.</summary>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static (System.Net.IPAddress StartAddress, System.Net.IPAddress EndAddress) GetIPAddresses(this MulticastV4Block source)
    {
      var minAddress = new System.Net.IPAddress((uint)source & 0xFFFFFFFFU);
      var maxAddress = new System.Net.IPAddress((uint)((ulong)source >>> 32));

      return (minAddress, maxAddress);
    }

    /// <summary>
    /// <para>Creates a new sequence with all IP addresses in the specified MulticastV4 block.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<System.Net.IPAddress> GetIPAddressRange(this MulticastV4Block source)
    {
      var (minAddress, maxAddress) = GetIPAddresses(source);

      return System.Net.IPAddress.GetRange(minAddress, maxAddress);
    }
  }
}
