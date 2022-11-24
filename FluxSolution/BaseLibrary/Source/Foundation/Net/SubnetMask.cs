namespace Flux.Net
{
  /// <summary>Is this API really needed with IPv6 around anyways?</summary>
  public static class SubnetMask
  {
    public static readonly System.Net.IPAddress MaskClassA = System.Net.IPAddress.Parse("255.0.0.0");
    public static readonly System.Net.IPAddress MaskClassB = System.Net.IPAddress.Parse("255.255.0.0");
    public static readonly System.Net.IPAddress MaskClassC = System.Net.IPAddress.Parse("255.255.255.0");

    public static System.Net.IPAddress CreateByHostBitLength(int hostPartLength)
      => (32 - hostPartLength) is int netPartLength ? new System.Net.IPAddress((System.Linq.Enumerable.Range(0, 4).Select(i => (i * 8 + 8 <= netPartLength) ? (byte)255 : (i * 8 > netPartLength) ? (byte)0 : System.Convert.ToByte(string.Empty.PadLeft(netPartLength - i * 8, '1').PadRight(8, '0'), 2)).ToArray())) : throw new System.Exception();

    public static System.Net.IPAddress CreateByNetBitLength(int netPartLength)
      => CreateByHostBitLength(32 - netPartLength);

    public static System.Net.IPAddress CreateByHostCount(int numberOfHosts)
      => CreateByHostBitLength(System.Convert.ToString(numberOfHosts + 1, 2).Length);
  }
}
