namespace Flux
{
  public static partial class NetworkInterfaces
  {
    /// <summary>
    /// <para>Creates a new string with the MAC-address of the network interface.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static string GetMacAddressString(this System.Net.NetworkInformation.NetworkInterface source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return source.GetPhysicalAddress().ToMacAddressString();
    }

    /// <summary>
    /// <para>Indicates whether the network interface has active traffic recorded.</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool HasActiveTraffic(this System.Net.NetworkInformation.NetworkInterface source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return source.GetIPStatistics() is var ips && ips.BytesReceived > 0 && ips.BytesSent > 0;
    }

    /// <summary>
    /// <para>Indicates whether the network interfaces is operational (i.e. "up").</para>
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static bool IsOperationallyUp(this System.Net.NetworkInformation.NetworkInterface source)
    {
      System.ArgumentNullException.ThrowIfNull(source);

      return source.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up;
    }

    //public static void Is(this System.Net.IPAddress source, System.Func<int, int, int, int, bool> evaluator)
    //{
    //  //var b = System.Net.IPAddress.Any.GetAddressBytes() is byte[] ipa ? evaluator(ipa[0], ipa[1], ipa[2], ipa[3]) : false;
    //  //var bs = System.Net.IPAddress.Any.GetAddressBytes().Select(b => (int)b);
    //  //System.Net.IPAddress.Any.Is((a,b,c,d)=>)
    //}
  }
}
