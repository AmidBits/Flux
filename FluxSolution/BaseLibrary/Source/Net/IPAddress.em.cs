using System.Data.Common;
using System.Linq;

namespace Flux
{
  public static partial class XtensionsNet
  {
    /// <summary></summary>
    /// <see cref="https://en.wikipedia.org/wiki/IPv6"/>
    public static System.Collections.Generic.IEnumerable<int> GetAddressWords(this System.Net.IPAddress source)
    {
      var bytes = source.GetAddressBytes();

      for (var index = 0; index < bytes.Length; index += 2)
      {
        yield return (bytes[index] << 8 | bytes[index + 1]);
      }
    }

    /// <summary></summary>
    /// <see cref="https://blogs.msdn.microsoft.com/knom/2008/12/31/ip-address-calculations-with-c-subnetmasks-networks/"/>
    public static System.Net.IPAddress GetBroadcastAddress(this System.Net.IPAddress source, System.Net.IPAddress subnetMask)
    {
      var sourceBytes = source.GetAddressBytes();
      var subnetMaskBytes = subnetMask.GetAddressBytes();

      if (sourceBytes.Length != subnetMaskBytes.Length) throw new System.ArgumentException(@"Incompatible source address and subnet mask.");

      var broadcastAddress = new byte[sourceBytes.Length];

      for (var i = 0; i < broadcastAddress.Length; i++)
      {
        broadcastAddress[i] = (byte)(sourceBytes[i] | (subnetMaskBytes[i] ^ 255));
      }

      return new System.Net.IPAddress(broadcastAddress);
    }

    public static System.Numerics.BigInteger GetMaxValue()
    => System.Numerics.BigInteger.Parse(@"340282366920938463463374607431768211456");
    public static System.Numerics.BigInteger GetMinValue()
      => System.Numerics.BigInteger.Zero;

    /// <summary></summary>
    /// <see cref="https://blogs.msdn.microsoft.com/knom/2008/12/31/ip-address-calculations-with-c-subnetmasks-networks/"/>
    public static System.Net.IPAddress GetNetworkAddress(this System.Net.IPAddress source, System.Net.IPAddress subnetMask)
    {
      var sourceBytes = source.GetAddressBytes();
      var subnetMaskBytes = subnetMask.GetAddressBytes();

      if (sourceBytes.Length != subnetMaskBytes.Length) throw new System.ArgumentException(@"Incompatible source address and subnet mask.");

      var networkAddress = new byte[sourceBytes.Length];

      for (var i = 0; i < networkAddress.Length; i++)
      {
        networkAddress[i] = (byte)(sourceBytes[i] & (subnetMaskBytes[i]));
      }

      return new System.Net.IPAddress(networkAddress);
    }

    public static System.Collections.Generic.IEnumerable<System.Net.IPAddress> GetRange(this System.Net.IPAddress source, System.Net.IPAddress target)
    {
      var sourceBi = ToBigInteger(source);
      var targetBi = ToBigInteger(target);

      for (var ipBi = sourceBi; ipBi < targetBi; ipBi++)
      {
        yield return ToIPAddress(ipBi);
      }

      yield return ToIPAddress(targetBi);
    }

    /// <summary>Determines whether two addresses are in the same subnet.</summary>
    /// <see cref="https://blogs.msdn.microsoft.com/knom/2008/12/31/ip-address-calculations-with-c-subnetmasks-networks/"/>
    public static bool InSameSubnet(this System.Net.IPAddress source, System.Net.IPAddress other, System.Net.IPAddress subnetMask)
      => source.GetNetworkAddress(subnetMask).Equals(other.GetNetworkAddress(subnetMask));

    public static bool IsBetween(this System.Net.IPAddress source, System.Net.IPAddress min, System.Net.IPAddress max)
      => source.ToBigInteger() is var bi && bi >= min.ToBigInteger() && bi <= max.ToBigInteger();

    /// <summary>Determintes whether the address is an IPv4 multicast address.</summary>
    public static bool IsIPv4Multicast(this System.Net.IPAddress source)
      => source.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork ? source.GetAddressBytes() is var bytes && bytes[0] >= 224 && bytes[0] <= 239 : throw new System.ArgumentOutOfRangeException(nameof(source), $"Not an IPv4 address ({source.AddressFamily.ToString()}).");

    public static System.Numerics.BigInteger ToBigInteger(this System.Net.IPAddress source)
      => source.GetAddressBytes().Reverse().ToBigInteger();

    public static System.Net.IPAddress ToIPAddress(this System.Numerics.BigInteger source)
    {
      if (source < 0 || source > GetMaxValue()) throw new System.ArgumentOutOfRangeException(nameof(source));

      var byteArray = source.ToByteArrayEx(out var _);
      System.Array.Reverse(byteArray);
      return new System.Net.IPAddress(byteArray);
    }
  }

  public static partial class Net
  {
    public static readonly System.Net.IPEndPoint RemoteTest = new System.Net.IPEndPoint(System.Net.IPAddress.Parse(@"4.3.2.1"), 4321);

    public static bool TryGetLocalIPAddressByUdp(out System.Net.IPAddress result)
    {
      try
      {
        using var socket = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Dgram, 0);

        socket.Connect(RemoteTest);

        result = ((System.Net.IPEndPoint)socket.LocalEndPoint).Address;
        return true;
      }
#pragma warning disable CA1031 // Do not catch general exception types.
      catch
#pragma warning restore CA1031 // Do not catch general exception types.
      { }

      result = default!;
      return false;
    }
  }
}
