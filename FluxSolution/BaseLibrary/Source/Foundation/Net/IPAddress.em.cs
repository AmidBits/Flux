namespace Flux.Net
{
  public static partial class IP
  {
    public static readonly System.Net.IPEndPoint RemoteTest = new(System.Net.IPAddress.Parse(@"4.3.2.1"), 4321);

    public static bool TryGetLocalIPAddressByUdp(out System.Net.IPAddress result)
    {
      try
      {
        using var socket = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork, System.Net.Sockets.SocketType.Dgram, 0);

        socket.Connect(RemoteTest);

        if (socket.LocalEndPoint is null)
          throw new System.NullReferenceException();

        result = ((System.Net.IPEndPoint)socket.LocalEndPoint).Address;
        return true;
      }
      catch { }

      result = default!;
      return false;
    }

    /// <summary>Gets the theoretical max IPv4 address.</summary>
    public static System.Numerics.BigInteger MaxValueIPv4
      => System.Numerics.BigInteger.Parse(@"4294967296", System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.CurrentCulture);
    /// <summary>Gets the theoretical max IPv6 address.</summary>
    public static System.Numerics.BigInteger MaxValueIPv6
      => System.Numerics.BigInteger.Parse(@"340282366920938463463374607431768211456", System.Globalization.NumberStyles.Integer, System.Globalization.CultureInfo.CurrentCulture);
    /// <summary>Gets the theoretical min IP address.</summary>
    public static System.Numerics.BigInteger MinValue
      => System.Numerics.BigInteger.Zero;
  }
}
