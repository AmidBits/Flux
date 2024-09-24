namespace Flux
{
  public static partial class Fx
  {
    /// <summary>
    /// <para>Returns the 16-bit words (as integers) of an IP address. This is mainly useful for IPv6 addresses.</para>
    /// <para><see href="https://en.wikipedia.org/wiki/IPv6"/></para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentNullException"></exception>
    public static System.Collections.Generic.IEnumerable<int> GetAddressWords(this System.Net.IPAddress source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var bytes = source.GetAddressBytes();

      for (var index = 0; index < bytes.Length; index += 2)
        yield return bytes[index] << 8 | bytes[index + 1];
    }

    /// <summary>
    /// <para>There is no such thing as a broadcast address in IPv6.</para>
    /// <para><see href="https://blogs.msdn.microsoft.com/knom/2008/12/31/ip-address-calculations-with-c-subnetmasks-networks/"/></para>
    /// </summary>
    public static System.Net.IPAddress GetBroadcastAddressIPv4(this System.Net.IPAddress source, System.Net.IPAddress subnetMask)
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
    public static System.Net.IPAddress GetNetworkAddress(this System.Net.IPAddress source, System.Net.IPAddress subnetMask)
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
    /// <para>The lower 64 bits identify the address of the interface or node, and is derived from the actual physical or MAC address using IEEE’s Extended Unique Identifier (EUI-64) format.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentNullException"></exception>
    /// <exception cref="System.InvalidOperationException"></exception>
    public static long Get64BitsLowerIPv6(this System.Net.IPAddress source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return source.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6 ? unchecked((long)(source.ToBigInteger() & 0xFFFFFFFFFFFFFFFF)) : throw new System.InvalidOperationException();
    }

    /// <summary>
    /// <para>The upper 64 bits is split into 2 blocks of 48 and 16 bits respectively, where the lower 16 bits are used for subnets on an internal networks, and are controlled by a network administrator.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentNullException"></exception>
    /// <exception cref="System.InvalidOperationException"></exception>
    public static long Get64BitsUpperIPv6(this System.Net.IPAddress source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return source.AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6 ? unchecked((long)((source.ToBigInteger() >> 64) & 0xFFFFFFFFFFFFFFFF)) : throw new System.InvalidOperationException();
    }

    /// <summary>
    /// <para>Creates a new sequence of IP addresses in the range [source, target] (inclusive).</para>
    /// </summary>
    /// <param name="source"></param>
    /// <param name="target"></param>
    /// <returns></returns>
    public static System.Collections.Generic.IEnumerable<System.Net.IPAddress> GetRange(this System.Net.IPAddress source, System.Net.IPAddress target)
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
    public static bool IsBetweenIPv4(this System.Net.IPAddress source, System.Net.IPAddress min, System.Net.IPAddress max)
      => source.ToBigInteger() is var bi && bi >= min.ToBigInteger() && bi <= max.ToBigInteger();

    /// <summary>
    /// <para>Indicates whether the <paramref name="source"/> is an IPv4 address.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool IsIPv4(this System.Net.IPAddress source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return source.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork;
    }

    /// <summary>
    /// <para>Indicates whether the <paramref name="source"/> is a loopback IP address.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool IsLoopback(this System.Net.IPAddress source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return System.Net.IPAddress.IsLoopback(source);
    }

    /// <summary>
    /// <para>Indicates whether two addresses are in the same subnet.</para>
    /// <para><see href="https://blogs.msdn.microsoft.com/knom/2008/12/31/ip-address-calculations-with-c-subnetmasks-networks/"/></para>
    /// </summary>
    public static bool InSameSubnet(this System.Net.IPAddress source, System.Net.IPAddress other, System.Net.IPAddress subnetMask)
      => source.GetNetworkAddress(subnetMask).Equals(other.GetNetworkAddress(subnetMask));

    /// <summary>
    /// <para>Convert the IP address to a BigInteger. Works on both IPv4 and IPv6.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static System.Numerics.BigInteger ToBigInteger(this System.Net.IPAddress source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      var addressBytes = source.GetAddressBytes();
      System.Array.Reverse(addressBytes);

      return addressBytes.ToBigInteger();
    }

    /// <summary>
    /// <para>Convert the <paramref name="source"/> to an IP address.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    /// <exception cref="System.ArgumentOutOfRangeException"></exception>
    public static System.Net.IPAddress ToIPAddress(this System.Numerics.BigInteger source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

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
