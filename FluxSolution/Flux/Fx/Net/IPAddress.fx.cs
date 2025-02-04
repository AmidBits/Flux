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
    public static short[] GetAddressWords(this System.Net.IPAddress source)
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
    public static System.Net.IPAddress GetNetworkAddressIPv4(this System.Net.IPAddress source, System.Net.IPAddress subnetMask)
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
    public static bool IsIPv4(this System.Net.IPAddress source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return source.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork;
    }

    /// <summary>
    /// <para>Indicates whether two addresses are in the same subnet.</para>
    /// <para><see href="https://blogs.msdn.microsoft.com/knom/2008/12/31/ip-address-calculations-with-c-subnetmasks-networks/"/></para>
    /// </summary>
    public static bool InSameSubnetIPv4(this System.Net.IPAddress source, System.Net.IPAddress other, System.Net.IPAddress subnetMask)
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
    public static System.Numerics.BigInteger ToBigInteger(this System.Net.IPAddress source)
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
