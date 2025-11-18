namespace Flux
{
  public static class IPAddressExtensions
  {
    extension(System.Net.IPAddress)
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

      private static System.Collections.Generic.IReadOnlyList<string> InternetHosts => ["https://api.ipify.org", "https://ipinfo.io/ip", "https://ifconfig.me/ip", "https://ipv4.getmyip.dev/"];

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
        myGlobalAddressesViaHosts = System.Net.IPAddress.InternetHosts.AsParallel().Select(host => (Success: TryGetMyGlobalAddressViaHost(host, out var ipa), Address: ipa)).Where(e => e.Success).Select(e => e.Address).ToArray();

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

    extension(System.Net.IPAddress source)
    {
      /// <summary>
      /// <para>Returns the 16-bit words (as integers) of an IP address. This is mainly useful for IPv6 addresses.</para>
      /// <para><see href="https://en.wikipedia.org/wiki/IPv6"/></para>
      /// </summary>
      /// <param name="source"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentNullException"></exception>
      public short[] GetAddressWords()
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var bytes = source.GetAddressBytes();

        var words = new short[bytes.Length / 2];

        for (var index = 0; index < bytes.Length; index += 2)
          words[index / 2] = unchecked((short)(bytes[index] << 8 | bytes[index + 1]));

        return words;
      }

      /// <summary>
      /// <para>There is no such thing as a broadcast address in IPv6.</para>
      /// <para><see href="https://blogs.msdn.microsoft.com/knom/2008/12/31/ip-address-calculations-with-c-subnetmasks-networks/"/></para>
      /// </summary>
      public System.Net.IPAddress GetBroadcastAddressIPv4(System.Net.IPAddress subnetMask)
      {
        System.ArgumentNullException.ThrowIfNull(source);
        System.ArgumentNullException.ThrowIfNull(subnetMask);

        var sourceBytes = source.GetAddressBytes();
        var subnetMaskBytes = subnetMask.GetAddressBytes();

        if (sourceBytes.Length != subnetMaskBytes.Length) throw new System.ArgumentException(@"Incompatible source address and subnet mask.");

        var broadcastAddress = new byte[sourceBytes.Length];
        for (var i = 0; i < broadcastAddress.Length; i++)
          broadcastAddress[i] = (byte)(sourceBytes[i] | (subnetMaskBytes[i] ^ 255));
        return new System.Net.IPAddress(broadcastAddress);
      }

      /// <summary>
      /// <para></para>
      /// <para><see href="https://blogs.msdn.microsoft.com/knom/2008/12/31/ip-address-calculations-with-c-subnetmasks-networks/"/></para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="subnetMask"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentException"></exception>
      public System.Net.IPAddress GetNetworkAddressIPv4(System.Net.IPAddress subnetMask)
      {
        System.ArgumentNullException.ThrowIfNull(source);
        System.ArgumentNullException.ThrowIfNull(subnetMask);

        var sourceBytes = source.GetAddressBytes();
        var subnetMaskBytes = subnetMask.GetAddressBytes();

        if (sourceBytes.Length != subnetMaskBytes.Length) throw new System.ArgumentException(@"Incompatible source address and subnet mask.");

        var networkAddress = new byte[sourceBytes.Length];
        for (var i = 0; i < networkAddress.Length; i++)
          networkAddress[i] = (byte)(sourceBytes[i] & (subnetMaskBytes[i]));
        return new System.Net.IPAddress(networkAddress);
      }

      /// <summary>
      /// <para>Creates a new sequence of IP addresses in the range [source, target] (inclusive).</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="target"></param>
      /// <returns></returns>
      public System.Collections.Generic.IEnumerable<System.Net.IPAddress> GetRange(System.Net.IPAddress target)
      {
        System.ArgumentNullException.ThrowIfNull(source);
        System.ArgumentNullException.ThrowIfNull(target);

        var sourceBi = ToBigInteger(source);
        var targetBi = ToBigInteger(target);

        for (var ipBi = sourceBi; ipBi < targetBi; ipBi++)
          yield return ToIPAddress(ipBi);

        yield return ToIPAddress(targetBi);
      }

      /// <summary>
      /// <para>Returns whether the address is between the specified minimum and maximum (inclusive).</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="min"></param>
      /// <param name="max"></param>
      /// <returns></returns>
      public bool IsBetweenIPv4(System.Net.IPAddress min, System.Net.IPAddress max)
      {
        System.ArgumentNullException.ThrowIfNull(source);
        System.ArgumentNullException.ThrowIfNull(min);
        System.ArgumentNullException.ThrowIfNull(max);

        return source.ToBigInteger() is var bi && bi >= min.ToBigInteger() && bi <= max.ToBigInteger();
      }

      /// <summary>
      /// <para>Indicates whether the <paramref name="source"/> is an IPv4 address.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <returns></returns>
      public bool IsIPv4()
      {
        System.ArgumentNullException.ThrowIfNull(source);

        return source.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork;
      }

      /// <summary>
      /// <para>Determintes whether the address is a multicast address. Works on both IPv4 and IPv6.</para>
      /// </summary>
      public bool IsMulticast()
      {
        System.ArgumentNullException.ThrowIfNull(source);

        if (source.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
          return (source.GetAddressBytes()[0] & 0xF0) == 0xE0;

        if (source.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
          return source.GetAddressBytes()[0] == 0xFF;

        return false;
      }

      /// <summary>
      /// <para>Indicates whether two addresses are in the same subnet.</para>
      /// <para><see href="https://blogs.msdn.microsoft.com/knom/2008/12/31/ip-address-calculations-with-c-subnetmasks-networks/"/></para>
      /// </summary>
      public bool InSameSubnetIPv4(System.Net.IPAddress other, System.Net.IPAddress subnetMask)
      {
        System.ArgumentNullException.ThrowIfNull(source);
        System.ArgumentNullException.ThrowIfNull(other);
        System.ArgumentNullException.ThrowIfNull(subnetMask);

        return source.GetNetworkAddressIPv4(subnetMask).Equals(other.GetNetworkAddressIPv4(subnetMask));
      }

      /// <summary>
      /// <para>Convert the IP address to a BigInteger. Works on both IPv4 and IPv6.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentNullException"></exception>
      public System.Numerics.BigInteger ToBigInteger()
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var span = source.GetAddressBytes().AsReadOnlySpan();

        return span.Length switch
        {
          >= 1 and <= 4 => span.ReadUInt32(Endianess.NetworkOrder),
          >= 5 and <= 16 => span.ReadUInt128(Endianess.NetworkOrder),
          _ => throw new ArgumentOutOfRangeException(nameof(source))
        };
      }
    }

    /// <summary>
    /// <para>Convert the <paramref name="source"/> to an IP address.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentNullException"></exception>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static System.Net.IPAddress ToIPAddress(this System.Numerics.BigInteger source)
    {
      var bytes = new byte[source >= 0 && source <= System.UInt64.MaxValue ? 4 : source > System.UInt64.MaxValue && source <= System.UInt128.MaxValue ? 16 : throw new System.ArgumentOutOfRangeException(nameof(source))];

      System.UInt128.CreateChecked(source).WriteBytes(bytes, Endianess.LittleEndian);

      return new System.Net.IPAddress(bytes);
    }
  }
}
