using System.Linq;

namespace Flux
{
  public static partial class NetworkInterfaceEm
  {
    public static System.Collections.Generic.IEnumerable<string> GetMACs(this System.Collections.Generic.IEnumerable<System.Net.NetworkInformation.NetworkInterface> source)
      => source.Select(ni => ni.GetPhysicalAddress()).Where(pa => pa.GetAddressBytes().Length == 6).Select(pa => pa.ToStringMAC());

    public static System.Collections.Generic.IEnumerable<System.Net.NetworkInformation.NetworkInterface> GetOperational(this System.Collections.Generic.IEnumerable<System.Net.NetworkInformation.NetworkInterface> source, bool includeLoopBacksAndTunnels, bool excludeWithoutGateways)
      => source.Where(ni => ni.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up && (includeLoopBacksAndTunnels || (ni.NetworkInterfaceType != System.Net.NetworkInformation.NetworkInterfaceType.Loopback && ni.NetworkInterfaceType != System.Net.NetworkInformation.NetworkInterfaceType.Tunnel)) && (!excludeWithoutGateways || ni.GetIPProperties().GatewayAddresses.Any()));

    ///// <example>Flux.Network.GetUnicastByIsLoopback(false); // Get all NON-loopback addresses.</example>
    //public static System.Net.IPAddress[] GetUnicastByIsLoopback(this System.Collections.Generic.IEnumerable<System.Net.NetworkInformation.NetworkInterface> source, bool isLookBack)
    //  => source.SelectMany(ni => ni.GetIPProperties().UnicastAddresses).Select(a => a.Address).Where(a => System.Net.IPAddress.IsLoopback(a) == isLookBack).ToArray();
    ///// <example>Flux.Network.GetUnicastByPrefixOrigin(System.Net.NetworkInformation.PrefixOrigin.Dhcp); // Get all DHCP addresses.</example>
    //public static System.Net.IPAddress[] GetUnicastByPrefixOrigin(this System.Collections.Generic.IEnumerable<System.Net.NetworkInformation.NetworkInterface> source, System.Net.NetworkInformation.PrefixOrigin prefixOrigin)
    //  => source.SelectMany(ni => ni.GetIPProperties().UnicastAddresses).Where(a => a.PrefixOrigin == prefixOrigin).Select(a => a.Address).ToArray();
    ///// <example>Flux.Network.GetUnicastByOperationalStatus(System.Net.NetworkInformation.OperationalStatus.Up); // Get all active addresses.</example>
    //public static System.Net.IPAddress[] GetUnicastByOperationalStatus(this System.Collections.Generic.IEnumerable<System.Net.NetworkInformation.NetworkInterface> source, System.Net.NetworkInformation.OperationalStatus operationalStatus)
    //  => source.Where(ni => ni.OperationalStatus == operationalStatus).SelectMany(ni => ni.GetIPProperties().UnicastAddresses).Select(a => a.Address).ToArray();
    ///// <example>Flux.Network.GetUnicastByAddressFamily(System.Net.Sockets.AddressFamily.InterNetwork); // Get all IPv4 addresses.</example>
    //public static System.Net.IPAddress[] GetUnicastByAddressFamily(this System.Collections.Generic.IEnumerable<System.Net.NetworkInformation.NetworkInterface> source, System.Net.Sockets.AddressFamily addressFamily = System.Net.Sockets.AddressFamily.InterNetwork)
    //  => source.SelectMany(ni => ni.GetIPProperties().UnicastAddresses).Where(a => a.Address.AddressFamily == addressFamily).Select(a => a.Address).ToArray();

    //public static void Is(this System.Net.IPAddress source, System.Func<int, int, int, int, bool> evaluator)
    //{
    //  //var b = System.Net.IPAddress.Any.GetAddressBytes() is byte[] ipa ? evaluator(ipa[0], ipa[1], ipa[2], ipa[3]) : false;
    //  //var bs = System.Net.IPAddress.Any.GetAddressBytes().Select(b => (int)b);
    //  //System.Net.IPAddress.Any.Is((a,b,c,d)=>)
    //}
  }
}
