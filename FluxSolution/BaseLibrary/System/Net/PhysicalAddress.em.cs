using System.Linq;

namespace Flux
{
  public static partial class Fx
  {
    /// <summary>The magic packet is a broadcast frame containing anywhere within its payload 6 bytes of all 255 (FF FF FF FF FF FF in hexadecimal), followed by sixteen repetitions of the target computer's 48-bit MAC address, for a total of 102 bytes.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Wake-on-LAN"/>
    public static System.Collections.Generic.IEnumerable<byte> CreateMagicPacket(this System.Net.NetworkInformation.PhysicalAddress source)
      => new byte[] { (byte)0xFF, (byte)0xFF, (byte)0xFF, (byte)0xFF, (byte)0xFF, (byte)0xFF }.Concat(System.Linq.Enumerable.Range(0, 16).SelectMany(i => source.GetAddressBytes()));
    /// <summary>Certain NICs support a security feature called "SecureOn". It allows users to store within the NIC a hexadecimal password of 6 bytes. Clients have to append this password to the magic packet. Any network eavesdropping will expose the cleartext password.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Wake-on-LAN"/>
    public static System.Collections.Generic.IEnumerable<byte> CreateMagicPacket(this System.Net.NetworkInformation.PhysicalAddress source, byte[] secureOn)
      => source.CreateMagicPacket().Concat(secureOn);

    /// <summary>Send a WOL (magic packet) to the specified macAddress and broadcast address.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Wake-on-LAN"/>
    public static void SendWakeOnLan(this System.Net.NetworkInformation.PhysicalAddress macAddress, System.Net.IPEndPoint broadcastAddress)
    {
      using var udpClient = new System.Net.Sockets.UdpClient();
      var udpDatagram = macAddress.CreateMagicPacket().ToArray();
      udpClient.Send(udpDatagram, udpDatagram.Length, broadcastAddress);
    }
    /// <summary>Send a WOL (magic packet) to the specified macAddress, address, port and subnetMask.</summary>
    /// <see href="https://en.wikipedia.org/wiki/Wake-on-LAN"/>
    public static void SendWakeOnLanIPv4(this System.Net.NetworkInformation.PhysicalAddress macAddress, System.Net.IPAddress ipAddress, System.Net.IPAddress subnetMask, int port = 0)
      => SendWakeOnLan(macAddress, new System.Net.IPEndPoint(ipAddress.GetBroadcastAddressIPv4(subnetMask), port));

    public static string ToStringMAC(this System.Net.NetworkInformation.PhysicalAddress source, char separator = '-')
      => string.Join(separator.ToString(), (source ?? throw new System.ArgumentNullException(nameof(source))).GetAddressBytes().Select(b => b.ToString(@"X2", System.Globalization.CultureInfo.InvariantCulture)));
  }
}
