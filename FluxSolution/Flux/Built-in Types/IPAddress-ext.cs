namespace Flux
{
  public static class IPAddressExtensions
  {
    extension(System.Net.IPAddress)
    {
      #region FromBigInteger/ToBigInteger
      /// <summary>
      /// <para>Convert the <paramref name="source"/> to an IP address.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentNullException"></exception>
      /// <exception cref="System.ArgumentOutOfRangeException"></exception>
      public static System.Net.IPAddress FromBigInteger(System.Numerics.BigInteger source)
      {
        var bytes = new byte[source >= 0 && source <= System.UInt64.MaxValue ? 4 : source > System.UInt64.MaxValue && source <= System.UInt128.MaxValue ? 16 : throw new System.ArgumentOutOfRangeException(nameof(source))];

        switch (bytes.Length)
        {
          case >= 1 and <= 4:
            System.Buffers.Binary.BinaryPrimitives.WriteUInt32LittleEndian(bytes, checked((uint)source));
            break;
          case >= 5 and <= 16:
            System.Buffers.Binary.BinaryPrimitives.WriteUInt128LittleEndian(bytes, checked((System.UInt128)source));
            break;
          default:
            throw new ArgumentOutOfRangeException(nameof(source));
        }

        return new System.Net.IPAddress(bytes);
      }

      /// <summary>
      /// <para>Convert the IP address to a BigInteger. Works on both IPv4 and IPv6.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentNullException"></exception>
      public static System.Numerics.BigInteger ToBigInteger(System.Net.IPAddress source)
      {
        System.ArgumentNullException.ThrowIfNull(source);

        var bytes = source.GetAddressBytes();

        return bytes.Length switch
        {
          >= 1 and <= 4 => System.Buffers.Binary.BinaryPrimitives.ReadUInt32LittleEndian(bytes),
          >= 5 and <= 16 => System.Buffers.Binary.BinaryPrimitives.ReadUInt128LittleEndian(bytes),
          _ => throw new ArgumentOutOfRangeException(nameof(source))
        };
      }

      #endregion

      #region GetBroadcastAddressIPv4

      /// <summary>
      /// <para>There is no such thing as a broadcast address in IPv6.</para>
      /// <para><see href="https://blogs.msdn.microsoft.com/knom/2008/12/31/ip-address-calculations-with-c-subnetmasks-networks/"/></para>
      /// </summary>
      public static System.Net.IPAddress GetBroadcastAddressIPv4(System.Net.IPAddress source, System.Net.IPAddress subnetMask)
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

      #endregion

      #region GetNetworkAddressIPv4

      /// <summary>
      /// <para></para>
      /// <para><see href="https://blogs.msdn.microsoft.com/knom/2008/12/31/ip-address-calculations-with-c-subnetmasks-networks/"/></para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="subnetMask"></param>
      /// <returns></returns>
      /// <exception cref="System.ArgumentException"></exception>
      public static System.Net.IPAddress GetNetworkAddressIPv4(System.Net.IPAddress source, System.Net.IPAddress subnetMask)
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

      #endregion

      #region GetRange

      /// <summary>
      /// <para>Creates a new sequence of IP addresses in the range [source, target] (inclusive).</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="target"></param>
      /// <returns></returns>
      public static System.Collections.Generic.IEnumerable<System.Net.IPAddress> GetRange(System.Net.IPAddress source, System.Net.IPAddress target)
      {
        System.ArgumentNullException.ThrowIfNull(source);
        System.ArgumentNullException.ThrowIfNull(target);

        var sourceBi = ToBigInteger(source);
        var targetBi = ToBigInteger(target);

        for (var ipBi = sourceBi; ipBi < targetBi; ipBi++)
          yield return FromBigInteger(ipBi);

        yield return FromBigInteger(targetBi);
      }

      #endregion

      #region InSameSubnetIPv4

      /// <summary>
      /// <para>Indicates whether two addresses are in the same subnet.</para>
      /// <para><see href="https://blogs.msdn.microsoft.com/knom/2008/12/31/ip-address-calculations-with-c-subnetmasks-networks/"/></para>
      /// </summary>
      public static bool InSameSubnetIPv4(System.Net.IPAddress source, System.Net.IPAddress other, System.Net.IPAddress subnetMask)
      {
        System.ArgumentNullException.ThrowIfNull(source);
        System.ArgumentNullException.ThrowIfNull(other);
        System.ArgumentNullException.ThrowIfNull(subnetMask);

        return GetNetworkAddressIPv4(source, subnetMask).Equals(GetNetworkAddressIPv4(other, subnetMask));
      }

      #endregion

      #region IsBetweenIPv4

      /// <summary>
      /// <para>Returns whether the address is between the specified minimum and maximum (inclusive).</para>
      /// </summary>
      /// <param name="source"></param>
      /// <param name="min"></param>
      /// <param name="max"></param>
      /// <returns></returns>
      public static bool IsBetweenIPv4(System.Net.IPAddress source, System.Net.IPAddress min, System.Net.IPAddress max)
      {
        System.ArgumentNullException.ThrowIfNull(source);
        System.ArgumentNullException.ThrowIfNull(min);
        System.ArgumentNullException.ThrowIfNull(max);

        return ToBigInteger(source) is var bi && bi >= ToBigInteger(min) && bi <= ToBigInteger(max);
      }

      #endregion

      #region ..GlobalAddress..

      /// <summary>
      /// <para>Obtains the global IP address of the system.</para>
      /// <para>A global IP address is the address that the system has on the outside of the local area network.</para>
      /// </summary>
      public static System.Net.IPAddress GlobalAddress
        => TryGetMyGlobalAddress(out var myGlobalAddress)
        ? myGlobalAddress
        : System.Net.IPAddress.None;

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

      /// <summary>
      /// <para>Attempts to contact all hosts in parallel returning the first public or global address to become available in <paramref name="myGlobalAddressViaInternetHosts"/>.</para>
      /// </summary>
      /// <param name="myGlobalAddressViaInternetHosts"></param>
      /// <returns></returns>
      public static bool TryGetMyGlobalAddressesViaHosts(out System.Net.IPAddress[] myGlobalAddressesViaHosts)
      {
        myGlobalAddressesViaHosts = m_internetHosts.AsParallel().Select(host => (Success: TryGetMyGlobalAddressViaHost(host, out var ipa), Address: ipa)).Where(e => e.Success).Select(e => e.Address).ToArray();

        return myGlobalAddressesViaHosts is not null && myGlobalAddressesViaHosts.Length > 0;
      }

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

      #endregion

      #region ..LocalAddress..

      /// <summary>
      /// <para>Gets the local IP address of the system.</para>
      /// <para>A local IP address is </para>
      /// </summary>
      public static System.Net.IPAddress LocalAddress
        => TryGetMyLocalAddress(out var myLocalAddress)
        ? myLocalAddress
        : System.Net.IPAddress.None;

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
          myLocalAddress = IEnumerableExtensions.GetRandomElement(myLocalAddressesViaNics);
          return true;
        }

        myLocalAddress = System.Net.IPAddress.None;
        return false;
      }

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
      /// <remarks>
      /// <list type="bullet">
      /// <item>Works even if multiple network interfaces exist.</item>
      /// <item>Avoids loopback (127.0.0.1) and inactive adapters.</item>
      /// <item>Does not require an actual internet connection to succeed — only a valid network route.</item>
      /// <item>Returns the exact IP your system would use for outbound traffic.</item>
      /// </list>
      /// </remarks>
      /// </summary>
      /// <returns></returns>
      public static bool TryGetMyLocalAddressViaUdp(out System.Net.IPAddress myLocalAddressViaUdp)
      {
        try
        {
          var socket = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Dgram, 0); // Use a UDP socket to determine the outbound IP without sending data.

          socket.Connect("8.8.8.8", 65530); // Connect to a public IP (Google DNS) - no packets are actually sent.
          // socket.Connect(System.Net.IPAddress.None, 0); // This actually works, but I am not sure about the reliability.

          myLocalAddressViaUdp = (socket.LocalEndPoint as System.Net.IPEndPoint)?.Address ?? System.Net.IPAddress.None;
        }
        catch
        {
          myLocalAddressViaUdp = System.Net.IPAddress.None;
        }

        return myLocalAddressViaUdp != System.Net.IPAddress.None;
      }

      #endregion
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
      /// <para>Indicates whether the <paramref name="source"/> is an IPv4 address.</para>
      /// </summary>
      /// <param name="source"></param>
      /// <returns></returns>
      public bool IsIPv4
      {
        get
        {
          System.ArgumentNullException.ThrowIfNull(source);

          return source.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork;
        }
      }

      /// <summary>
      /// <para>Determintes whether the address is a multicast address. Works on both IPv4 and IPv6.</para>
      /// </summary>
      public bool IsMulticast
      {
        get
        {
          System.ArgumentNullException.ThrowIfNull(source);

          if (source.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
            return (source.GetAddressBytes()[0] & 0xF0) == 0xE0;

          if (source.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6)
            return source.GetAddressBytes()[0] == 0xFF;

          return false;
        }
      }
    }

    #region Helpers

    private static readonly System.Collections.Generic.IReadOnlyList<string> m_internetHosts = ["https://api.ipify.org", "https://ipinfo.io/ip", "https://ifconfig.me/ip", "https://ipv4.getmyip.dev/"];

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
          .Where(uai => !System.Net.IPAddress.IsLoopback(uai.Address))
      ];

    #endregion
  }
}
