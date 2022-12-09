namespace Flux
{
  public static partial class IPAddressEm
  {
    /// <summary>Returns the 16-bit words (as integers) of an IP address. This is mainly useful for IPv6 addresses.</summary>
    /// <see cref="https://en.wikipedia.org/wiki/IPv6"/>
    public static System.Collections.Generic.IEnumerable<int> GetAddressWords(this System.Net.IPAddress source)
    {
      var bytes = (source ?? throw new System.ArgumentNullException(nameof(source))).GetAddressBytes();

      for (var index = 0; index < bytes.Length; index += 2)
        yield return bytes[index] << 8 | bytes[index + 1];
    }

    /// <summary>There is no such thing as a broadcast address in IPv6.</summary>
    /// <see cref="https://blogs.msdn.microsoft.com/knom/2008/12/31/ip-address-calculations-with-c-subnetmasks-networks/"/>
    public static System.Net.IPAddress GetBroadcastAddressIPv4(this System.Net.IPAddress source, System.Net.IPAddress subnetMask)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (subnetMask is null) throw new System.ArgumentNullException(nameof(subnetMask));

      var sourceBytes = source.GetAddressBytes();
      var subnetMaskBytes = subnetMask.GetAddressBytes();

      if (sourceBytes.Length != subnetMaskBytes.Length) throw new System.ArgumentException(@"Incompatible source address and subnet mask.");

      var broadcastAddress = new byte[sourceBytes.Length];
      for (var i = 0; i < broadcastAddress.Length; i++)
        broadcastAddress[i] = (byte)(sourceBytes[i] | (subnetMaskBytes[i] ^ 255));
      return new System.Net.IPAddress(broadcastAddress);
    }

    /// <summary></summary>
    /// <see cref="https://blogs.msdn.microsoft.com/knom/2008/12/31/ip-address-calculations-with-c-subnetmasks-networks/"/>
    public static System.Net.IPAddress GetNetworkAddress(this System.Net.IPAddress source, System.Net.IPAddress subnetMask)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));
      if (subnetMask is null) throw new System.ArgumentNullException(nameof(subnetMask));

      var sourceBytes = source.GetAddressBytes();
      var subnetMaskBytes = subnetMask.GetAddressBytes();

      if (sourceBytes.Length != subnetMaskBytes.Length) throw new System.ArgumentException(@"Incompatible source address and subnet mask.");

      var networkAddress = new byte[sourceBytes.Length];
      for (var i = 0; i < networkAddress.Length; i++)
        networkAddress[i] = (byte)(sourceBytes[i] & (subnetMaskBytes[i]));
      return new System.Net.IPAddress(networkAddress);
    }

    /// <summary>The lower 64 bits identify the address of the interface or node, and is derived from the actual physical or MAC address using IEEE’s Extended Unique Identifier (EUI-64) format.</summary>
    public static long Get64BitsLowerIPv6(this System.Net.IPAddress source)
      => (source ?? throw new System.ArgumentNullException(nameof(source))).AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6 ? unchecked((long)(source.ToBigInteger() & 0xFFFFFFFFFFFFFFFF)) : throw new System.InvalidOperationException();
    /// <summary>The upper 64 bits is split into 2 blocks of 48 and 16 bits respectively, where the lower 16 bits are used for subnets on an internal networks, and are controlled by a network administrator.</summary>
    public static long Get64BitsUpperIPv6(this System.Net.IPAddress source)
      => (source ?? throw new System.ArgumentNullException(nameof(source))).AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6 ? unchecked((long)((source.ToBigInteger() >> 64) & 0xFFFFFFFFFFFFFFFF)) : throw new System.InvalidOperationException();

    /// <summary>Creates a new sequence of IP addresses between the source and target (inclusive).</summary>
    public static System.Collections.Generic.IEnumerable<System.Net.IPAddress> GetRange(this System.Net.IPAddress source, System.Net.IPAddress target)
    {
      var sourceBi = ToBigInteger(source);
      var targetBi = ToBigInteger(target);

      for (var ipBi = sourceBi; ipBi < targetBi; ipBi++)
        yield return ToIPAddress(ipBi);

      yield return ToIPAddress(targetBi);
    }

    /// <summary>Returns whether the address is between the specified minimum and maximum (inclusive).</summary>
    public static bool IsBetweenIPv4(this System.Net.IPAddress source, System.Net.IPAddress min, System.Net.IPAddress max)
      => source.ToBigInteger() is var bi && bi >= min.ToBigInteger() && bi <= max.ToBigInteger();

    /// <summary>Determines whether two addresses are in the same subnet.</summary>
    /// <see cref="https://blogs.msdn.microsoft.com/knom/2008/12/31/ip-address-calculations-with-c-subnetmasks-networks/"/>
    public static bool InSameSubnet(this System.Net.IPAddress source, System.Net.IPAddress other, System.Net.IPAddress subnetMask)
      => source.GetNetworkAddress(subnetMask).Equals(other.GetNetworkAddress(subnetMask));

    /// <summary>Convert the IP address to a BigInteger. Works on both IPv4 and IPv6.</summary>
    public static System.Numerics.BigInteger ToBigInteger(this System.Net.IPAddress source)
    {
      if (source is null) throw new System.ArgumentNullException(nameof(source));

      var addressBytes = source.GetAddressBytes();
      System.Array.Reverse(addressBytes);

      return addressBytes.ToBigInteger();
    }

    /// <summary>Convert the BigInteger to an IP address.</summary>
    public static System.Net.IPAddress ToIPAddress(this System.Numerics.BigInteger source)
    {
      var maxValueIPv6 = System.Numerics.BigInteger.Parse(@"340282366920938463463374607431768211456", System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.CurrentCulture);

      if (source < 0 || source > maxValueIPv6) throw new System.ArgumentOutOfRangeException(nameof(source));

      var byteArray = source.ToByteArrayEx(out var _);
      if (byteArray.Length < 4)
        System.Array.Resize(ref byteArray, 4);
      System.Array.Reverse(byteArray);

      return new System.Net.IPAddress(byteArray);
    }
  }
}
