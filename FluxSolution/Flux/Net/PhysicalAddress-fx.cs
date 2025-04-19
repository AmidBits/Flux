namespace Flux
{
  public static partial class PhysicalAddresses
  {
    /// <summary>
    /// <para>The magic packet is a broadcast frame containing anywhere within its payload 6 bytes, each a value of 255, so [0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF] in hexadecimal, followed by sixteen repeated copies of the target computer's 48-bit MAC address, for a total of 102 bytes.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Wake-on-LAN"/></para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="secureOn">Certain NICs support a security feature called "SecureOn". It allows users to store within the NIC a hexadecimal password of 6 bytes. Clients have to append this password to the magic packet. Any network eavesdropping will expose the cleartext password.</param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static System.ReadOnlySpan<byte> CreateMagicPacket(this System.Net.NetworkInformation.PhysicalAddress source, byte[]? secureOn = default)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      if (secureOn is not null && secureOn.Length != 6) throw new System.ArgumentOutOfRangeException(nameof(secureOn));

      System.Span<byte> magicPacket;

      if (secureOn is not null)
      {
        magicPacket = System.Buffers.ArrayPool<byte>.Shared.Rent(108).AsSpan()[..108];

        secureOn.CopyTo(magicPacket.Slice(102, 6));
      }
      else // Magic packet without secureOn.
        magicPacket = System.Buffers.ArrayPool<byte>.Shared.Rent(102).AsSpan()[..102];

      magicPacket[0] = 0xFF;
      magicPacket[1] = 0xFF;
      magicPacket[2] = 0xFF;
      magicPacket[3] = 0xFF;
      magicPacket[4] = 0xFF;
      magicPacket[5] = 0xFF;

      var addressBytes = source.GetAddressBytes().AsReadOnlySpan(); // Copy the 6 address-bytes 16 times after the 6 byte payload by doubling after the first two copy calls.

      addressBytes.CopyTo(magicPacket.Slice(6, 6)); // Copy once.
      addressBytes.CopyTo(magicPacket.Slice(12, 6)); // Copy twice.

      magicPacket.Slice(6, 12).CopyTo(magicPacket[18..]); // Copy the two to four.
      magicPacket.Slice(6, 24).CopyTo(magicPacket[30..]); // Copy the four to eight.
      magicPacket.Slice(6, 48).CopyTo(magicPacket[54..]); // Copy the eight to sixteen.

      return magicPacket;
    }

    /// <summary>
    /// <para>Send a WOL (magic packet) to the specified macAddress and broadcast address.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Wake-on-LAN"/></para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="broadcastAddress"></param>
    /// <param name="secureOn">Certain NICs support a security feature called "SecureOn". It allows users to store within the NIC a hexadecimal password of 6 bytes. Clients have to append this password to the magic packet. Any network eavesdropping will expose the cleartext password.</param>
    public static void SendWakeOnLan(this System.Net.NetworkInformation.PhysicalAddress source, System.Net.IPEndPoint broadcastAddress, byte[]? secureOn = default)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      using var udpClient = new System.Net.Sockets.UdpClient();

      udpClient.Send(source.CreateMagicPacket(secureOn), broadcastAddress);
    }

    /// <summary>
    /// <para>Send a WOL (magic packet) to the specified macAddress, address, port and subnetMask.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/Wake-on-LAN"/></para>
    /// </summary>
    /// <param name="macAddress"></param>
    /// <param name="ipAddress"></param>
    /// <param name="subnetMask"></param>
    /// <param name="port"></param>
    /// <param name="secureOn">Certain NICs support a security feature called "SecureOn". It allows users to store within the NIC a hexadecimal password of 6 bytes. Clients have to append this password to the magic packet. Any network eavesdropping will expose the cleartext password.</param>
    public static void SendWakeOnLanIPv4(this System.Net.NetworkInformation.PhysicalAddress macAddress, System.Net.IPAddress ipAddress, System.Net.IPAddress subnetMask, int port = 0, byte[]? secureOn = default)
      => SendWakeOnLan(macAddress, new System.Net.IPEndPoint(ipAddress.GetBroadcastAddressIPv4(subnetMask), port), secureOn);

    /// <summary>
    /// <para>Creates a string with the 6 bytes from the <paramref name="source"/> as 6 hexadecimal 2 "digit" separated with the specified <paramref name="separator"/>.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="separator"></param>
    /// <returns></returns>
    public static string ToMacAddressString(this System.Net.NetworkInformation.PhysicalAddress source, char separator = '-')
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return string.Join(separator, source.GetAddressBytes().Select(b => b.ToString(@"X2", System.Globalization.CultureInfo.InvariantCulture)));
    }
  }
}
