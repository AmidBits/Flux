namespace Flux.Net
{
  public static class IPAddresses
  {
    /// <summary>
    /// <para>There is really only one way to do this reliably, and that is to call out to certain sites that reply with your global/public IP address.</para>
    /// </summary>
    /// <param name="myGlobalAddress"></param>
    /// <returns></returns>
    public static bool TryGetMyGlobalAddress(out System.Net.IPAddress myGlobalAddress)
    {
      if (TryGetMyGlobalAddressesViaHosts(out var myGlobalAddressesViaHosts))
      {
        myGlobalAddress = myGlobalAddressesViaHosts[0];
        return true;
      }

      myGlobalAddress = System.Net.IPAddress.None;
      return false;
    }

    private static System.Collections.Generic.IReadOnlyList<string> InternetHosts { get; } = ["https://api.ipify.org", "https://ipinfo.io/ip", "https://ifconfig.me/ip", "https://ipv4.getmyip.dev/"];

    /// <summary>
    /// <para>Attempts to get the public or global address of a network from the <paramref name="host"/> in <paramref name="myGlobalAddressViaInternetHosts"/>. All clients on a LAN uses the same global address on the outside WAN. This is the address known on the Internet for all LAN clients.</para>
    /// </summary>
    /// <param name="host"></param>
    /// <param name="myGlobalAddressViaInternetHosts"></param>
    /// <returns></returns>
    public static bool TryGetMyGlobalAddressViaHost(string host, out System.Net.IPAddress myGlobalAddressViaInternetHosts)
    {
      try
      {
        using var hc = new System.Net.Http.HttpClient();

        myGlobalAddressViaInternetHosts = System.Net.IPAddress.Parse(hc.GetStringAsync(host).Result);

        return myGlobalAddressViaInternetHosts != default;
      }
      catch { }

      myGlobalAddressViaInternetHosts = default!;
      return false;
    }

    /// <summary>
    /// <para>Attempts to contact all hosts in parallel returning the first public or global address to become available in <paramref name="myGlobalAddressViaInternetHosts"/>.</para>
    /// </summary>
    /// <param name="myGlobalAddressViaInternetHosts"></param>
    /// <returns></returns>
    public static bool TryGetMyGlobalAddressesViaHosts(out System.Net.IPAddress[] myGlobalAddressesViaHosts)
    {
      myGlobalAddressesViaHosts = InternetHosts.AsParallel().Select(host => (Success: TryGetMyGlobalAddressViaHost(host, out var ipa), Address: ipa)).Where(e => e.Success).Select(e => e.Address).ToArray();

      return myGlobalAddressesViaHosts is not null && myGlobalAddressesViaHosts.Length > 0;
    }

    /// <summary>
    /// <para>There are a few different ways to find local IP addresses (since computers can have multiple IP addresses). Most of the time, however, a computer only have and use one single address.</para>
    /// </summary>
    /// <param name="myLocalAddress"></param>
    /// <returns></returns>
    public static bool TryGetMyLocalAddress(out System.Net.IPAddress myLocalAddress)
    {
      if (TryGetMyLocalAddressViaUdp(out var myLocalAddressViaUdp))
      {
        myLocalAddress = myLocalAddressViaUdp;
        return true;
      }

      if (TryGetMyLocalAddressesViaNics(out var myLocalAddressesViaNics))
      {
        myLocalAddress = myLocalAddressesViaNics.Random();
        return true;
      }

      myLocalAddress = System.Net.IPAddress.None;
      return false;
    }

    /// <summary>
    /// <para>Gets all unicast IP addresses that are "up", have a gateway address and is not a loopback address.</para>
    /// </summary>
    /// <returns></returns>
    private static System.Net.NetworkInformation.UnicastIPAddressInformation[] GetAllUnicastIpAddresses()
      => [.. System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces()
      .Where(ni => ni.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up)
      .Select(ni => ni.GetIPProperties())
      .Where(p => p.GatewayAddresses.Count > 0)
      .SelectMany(ipp => ipp.UnicastAddresses)
      .Where(uai => !System.Net.IPAddress.IsLoopback(uai.Address))];

    /// <summary>
    /// <para>Attempts to get all local addresses any available NICs.</para>
    /// </summary>
    /// <param name="myLocalAddressesViaNics">Returns all IP addresses found associated with the computer, or empty if none found.</param>
    /// <returns></returns>
    public static bool TryGetMyLocalAddressesViaNics(out System.Net.IPAddress[] myLocalAddressesViaNics)
    {
      try
      {
        myLocalAddressesViaNics = [.. GetAllUnicastIpAddresses().Select(uai => uai.Address)];

        return myLocalAddressesViaNics is not null && myLocalAddressesViaNics.Length > 0;
      }
      catch { }

      myLocalAddressesViaNics = [];
      return false;
    }

    /// <summary>
    /// <para>Attempts to get a more likely local address off of available NICs.</para>
    /// <para>This is "windows" only API.</para>
    /// </summary>
    /// <param name="myMoreLikelyLocalAddressViaNics"></param>
    /// <returns></returns>
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public static bool TryGetMyLocalAddressViaNics(out System.Net.IPAddress? myMoreLikelyLocalAddressViaNics)
    {
      System.Net.NetworkInformation.UnicastIPAddressInformation? moreLikelyLocalAddress = default;

      var uas = GetAllUnicastIpAddresses();

      foreach (var ua in uas)
      {
        if (!ua.IsDnsEligible)
        {
          if (moreLikelyLocalAddress == default)
            moreLikelyLocalAddress = ua;

          continue;
        }

        if (ua.PrefixOrigin != System.Net.NetworkInformation.PrefixOrigin.Dhcp)
        {
          if (moreLikelyLocalAddress == default || !moreLikelyLocalAddress.IsDnsEligible)
            moreLikelyLocalAddress = ua;

          continue;
        }

        moreLikelyLocalAddress = ua;
        break;
      }

      myMoreLikelyLocalAddressViaNics = moreLikelyLocalAddress?.Address;
      return myMoreLikelyLocalAddressViaNics != default;
    }

    /// <summary>
    /// <para>Attempts to get the local address via UDP.</para>
    /// <remarks>No outside connection is being made.</remarks>
    /// </summary>
    /// <returns></returns>
    public static bool TryGetMyLocalAddressViaUdp(out System.Net.IPAddress myLocalAddressViaUdp)
    {
      try
      {
        var socket = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Dgram, 0);

        socket.Connect(System.Net.IPAddress.None, 0);

        myLocalAddressViaUdp = (socket.LocalEndPoint as System.Net.IPEndPoint)?.Address ?? System.Net.IPAddress.None;
      }
      catch
      {
        myLocalAddressViaUdp = System.Net.IPAddress.None;
      }

      return myLocalAddressViaUdp != System.Net.IPAddress.None;
    }
  }
}
